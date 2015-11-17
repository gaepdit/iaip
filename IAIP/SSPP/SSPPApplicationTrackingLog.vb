Imports System.DateTime
Imports Oracle.ManagedDataAccess.Client
Imports System.IO
Imports System.Net.Mail
Imports System.Collections.Generic

Public Class SSPPApplicationTrackingLog
    Dim SQL, SQL2, SQL3 As String
    Dim SQL4, SQL5, SQL6 As String
    Dim SQL7, SQL8, SQL9 As String
    Dim SQL10, SQL11 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim MasterApp As String
    Dim TimeStamp As String
    Dim FormStatus As String
    Dim dsEngineerList As DataSet
    Dim daEngineerList As OracleDataAdapter
    Dim dsSSCPStaff As DataSet
    Dim daSSCPStaff As OracleDataAdapter
    Dim dsISMPStaff As DataSet
    Dim daISMPStaff As OracleDataAdapter
    Dim dsCountyList As DataSet
    Dim daCountyList As OracleDataAdapter
    Dim dsApplicationType As DataSet
    Dim daApplicationType As OracleDataAdapter
    Dim dsPermitType As DataSet
    Dim daPermitType As OracleDataAdapter
    Dim dsCity As DataSet
    Dim daCity As OracleDataAdapter
    Dim dsAPBUnit As DataSet
    Dim daAPBUnit As OracleDataAdapter
    Dim dsFacAppHistory As DataSet
    Dim daFacAppHistory As OracleDataAdapter
    Dim dsFacInfoHistory As DataSet
    Dim daFacInfoHistory As OracleDataAdapter
    Dim dsPart60 As DataSet
    Dim daPart60 As OracleDataAdapter
    Dim dsPart61 As DataSet
    Dim daPart61 As OracleDataAdapter
    Dim dsPart63 As DataSet
    Dim daPart63 As OracleDataAdapter
    Dim dsSIP As DataSet
    Dim daSIP As OracleDataAdapter

    Private Sub SSPPPermitTrackingLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try


            FormStatus = "Loading"
            LoadDefaults()
            FormStatus = ""
            LoadComboBoxes()

            TCApplicationTrackingLog.TabPages.Remove(TPTrackingLog)
            TCApplicationTrackingLog.TabPages.Remove(TPOtherInfo)
            TCApplicationTrackingLog.TabPages.Remove(TPReviews)
            TCApplicationTrackingLog.TabPages.Remove(TPApplicationHistroy)
            TCApplicationTrackingLog.TabPages.Remove(TPInformationRequests)
            TCApplicationTrackingLog.TabPages.Remove(TPWebPublisher)
            TCApplicationTrackingLog.TabPages.Remove(TPContactInformation)
            TCApplicationTrackingLog.TabPages.Remove(TPPermitUploader)
            TCApplicationTrackingLog.TabPages.Remove(TPSubPartEditor)

            LoadPermissions()

            TCApplicationTrackingLog.TabPages.Add(TPSubPartEditor)
            LoadSubPartData()

            TCSupParts.TabPages.Remove(TPEditSubParts)
            
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub


#Region "Page Load Functions"
    Sub LoadDefaults()
        Try
             

            'Application Tracking Tab
            txtApplicationNumber.Clear()
            txtAIRSNumber.Clear()
            txtOutstandingApplication.Clear()
            txtFacilityName.Clear()
            txtFacilityStreetAddress.Clear()
            txtFacilityZipCode.Clear()
            txtDistrict.Clear()
            txtOffice.Clear()
            txtSICCode.Clear()
            txtNAICSCode.Clear()
            txt1HourOzone.Clear()
            txt8HROzone.Clear()
            txtPM.Clear()
            txtPlantDescription.Clear()
            txtPermitNumber.Clear()
            txtReasonAppSubmitted.Clear()
            txtComments.Clear()
            cboEngineer.Items.Clear()
            cboApplicationUnit.Items.Clear()
            cboApplicationType.Items.Clear()
            cboFacilityCity.Items.Clear()
            cboCounty.Items.Clear()
            cboOperationalStatus.Items.Clear()
            cboClassification.Items.Clear()
            cboPublicAdvisory.Items.Clear()
            cboPermitAction.Items.Clear()
            DTPFinalizedDate.Text = OracleDate
            DTPFinalizedDate.Checked = False
            DTPDateSent.Text = OracleDate
            DTPDateSent.Checked = False
            DTPDateReceived.Text = OracleDate
            DTPDateReceived.Checked = False
            DTPDateAssigned.Text = OracleDate
            DTPDateAssigned.Checked = False
            DTPDateReassigned.Text = OracleDate
            DTPDateReassigned.Checked = False
            DTPDateAcknowledge.Text = OracleDate
            DTPDateAcknowledge.Checked = False
            DTPDatePAExpires.Text = OracleDate
            DTPDatePAExpires.Checked = False
            DTPDateToUC.Text = OracleDate
            DTPDateToUC.Checked = False
            DTPDateToPM.Text = OracleDate
            DTPDateToPM.Checked = False
            DTPDraftIssued.Text = OracleDate
            DTPDraftIssued.Checked = False
            DTPDatePNExpires.Text = OracleDate
            DTPDatePNExpires.Checked = False
            DTPEPAWaived.Text = OracleDate
            DTPEPAWaived.Checked = False
            DTPEPAEnds.Text = OracleDate
            DTPEPAEnds.Checked = False
            DTPDateToBC.Text = OracleDate
            DTPDateToBC.Checked = False
            DTPDateToDO.Text = OracleDate
            DTPDateToDO.Checked = False
            DTPFinalAction.Text = OracleDate
            DTPFinalAction.Checked = False
            DTPDeadline.Text = OracleDate
            DTPDeadline.Checked = False
            chbClosedOut.Checked = False
            chbPAReady.Checked = False
            chbPNReady.Checked = False
            chbCDS_0.Checked = True
            chbCDS_6.Checked = False
            chbCDS_7.Checked = False
            chbCDS_8.Checked = False
            chbCDS_9.Checked = False
            chbCDS_M.Checked = False
            chbCDS_V.Checked = False
            chbCDS_A.Checked = False
            chbCDS_RMP.Checked = False
            chbNSRMajor.Checked = False
            chbHAPsMajor.Checked = False
            rtbFacilityInformation.Clear()

            'Other Tab
            chbPSD.Checked = False
            chbPSD.Enabled = False
            chbNAANSR.Checked = False
            chbNAANSR.Enabled = False
            chb112g.Checked = False
            chb112g.Enabled = False
            chbRulett.Checked = False
            chbRulett.Enabled = False
            chbRuleyy.Checked = False
            chbRuleyy.Enabled = False
            chbPal.Checked = False
            chbPal.Enabled = False
            chbExpedited.Checked = False
            chbExpedited.Enabled = False
            chbConfidential.Checked = False
            chbConfidential.Enabled = False

            'ISMP and SSCP Reviews Tab
            txtISMPComments.Clear()
            txtISMPComments.ReadOnly = True
            txtSSCPComments.Clear()
            txtSSCPComments.ReadOnly = True
            cboSSCPUnits.Items.Clear()
            cboISMPUnits.Items.Clear()
            cboSSCPStaff.Items.Clear()
            cboSSCPStaff.Enabled = False
            cboISMPStaff.Items.Clear()
            cboISMPStaff.Enabled = False
            DTPReviewSubmitted.Text = OracleDate
            DTPReviewSubmitted.Checked = False
            DTPISMPReview.Text = OracleDate
            DTPISMPReview.Checked = False
            DTPSSCPReview.Text = OracleDate
            DTPSSCPReview.Checked = False
            rdbSSCPYes.Checked = False
            rdbSSCPNo.Checked = False
            rdbISMPYes.Checked = False
            rdbISMPNo.Checked = False

            'Facility Application History Tab
            txtApplicationNumberHistory.Clear()
            txtApplicationTypeHistory.Clear()
            txtApplicationDatedHistory.Clear()
            txtApplicationUnitHistory.Clear()
            txtEngineerHistory.Clear()
            txtHistoryAppComments.Clear()
            txtHistoryComments.Clear()
            chbClosedOutHistory.Checked = False
            lbLinkApplications.Items.Clear()

            'Information Requested Tab
            txtInformationRequested.Clear()
            txtInformationReceived.Clear()
            txtInformationRequestedKey.Clear()
            DTPInformationRequested.Text = OracleDate
            DTPInformationRequested.Checked = False
            DTPInformationReceived.Text = OracleDate
            DTPInformationReceived.Checked = False

            'Web Publisher Tab
            txtEPATargetedComments.Clear()

            DTPNotifiedAppReceived.Text = OracleDate
            DTPNotifiedAppReceived.Checked = False
            DTPDraftOnWeb.Text = OracleDate
            DTPDraftOnWeb.Checked = False
            DTPEPAStatesNotified.Text = OracleDate
            DTPEPAStatesNotified.Checked = False
            DTPFinalOnWeb.Text = OracleDate
            DTPFinalOnWeb.Checked = False
            DTPEPANotifiedPermitOnWeb.Text = OracleDate
            DTPEPANotifiedPermitOnWeb.Checked = False
            DTPEffectiveDateofPermit.Text = OracleDate
            DTPEffectiveDateofPermit.Checked = False
            DTPExperationDate.Text = OracleDate
            DTPExperationDate.Checked = False
            DTPPNExpires.Checked = False
            DTPPNExpires.Text = OracleDate
            DTPPNExpires.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Sub LoadComboBoxes()
        Dim dtEngineerList As New DataTable
        Dim dtSSCPList As New DataTable
        Dim dtISMPList As New DataTable
        Dim dtCountyList As New DataTable
        Dim dtApplicationType As New DataTable
        Dim dtPermitType As New DataTable
        Dim dtCity As New DataTable
        Dim dtSSPPUnit As New DataTable
        Dim dtSSCPUnit As New DataTable
        Dim dtISMPUnit As New DataTable

        Dim drNewRow As DataRow
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drDSRow3 As DataRow
        Dim drDSRow4 As DataRow
        Dim drDSRow5 As DataRow
        Dim drDSRow6 As DataRow
        Dim drDSRow7 As DataRow
        Dim drDSRow8 As DataRow
        Dim drDSRow9 As DataRow
        Dim drDSRow10 As DataRow

        Try
             
            cboOperationalStatus.Items.Add("O - Operating")
            cboOperationalStatus.Items.Add("P - Planned")
            cboOperationalStatus.Items.Add("C - Under Construction")
            cboOperationalStatus.Items.Add("T - Temporarily Closed")
            cboOperationalStatus.Items.Add("X - Permanently Closed")
            cboOperationalStatus.Items.Add("I - Seasonal Operation")

            cboClassification.Items.Add("A - MAJOR")
            cboClassification.Items.Add("B - MINOR")
            cboClassification.Items.Add("C - UNKNOWN")
            cboClassification.Items.Add("SM - SYNTHETIC MINOR")
            cboClassification.Items.Add("PR - PERMIT BY RULE")
            'cboClassification.Items.Add("U - UNDEFINED")

            cboPublicAdvisory.Items.Add("Not Decided")
            cboPublicAdvisory.Items.Add("PA Needed")
            cboPublicAdvisory.Items.Add("PA Not Needed")

            SQL = "Select " & _
            "(strLastName||', ' ||strFirstName) as EngineerName, " & _
            "numUserID " & _
            "from AIRBranch.EPDUserProfiles " & _
            "where numProgram = '5'  " & _
            "order by strLastName "

            SQL11 = "Select " & _
            "distinct(strLastName||', ' ||strFirstName) as EngineerName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSPPApplicationMaster  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID " & _
            "and numProgram <> '5' "

            SQL2 = "Select " & _
            "strCountyCode, strCountyName, " & _
            "strAttainmentStatus " & _
            "from AIRBRANCH.LookUpCountyInformation " & _
            "order by strCountyName "

            SQL3 = "Select " & _
            "strApplicationTypeCode, strApplicationTypeDesc " & _
            "from AIRBRANCH.LookUpApplicationTypes " & _
            "where strAPplicationTypeUsed <> 'False' " & _
            "or strApplicationTypeUsed is NULL " & _
            "order by strApplicationTypeDesc "

            SQL4 = "Select " & _
            "strPermitTypeCode, strPermitTypeDescription " & _
            "from AIRBRANCH.LookUPPermitTypes  " & _
            "where strTypeUsed <> 'False' " & _
            "or strTypeUsed is NULL " & _
            "order by strPermitTypeDescription "

            SQL5 = "Select " & _
            "City " & _
            "from AIRBRANCH.VW_Cities " & _
            "order by City"

            SQL6 = "Select " & _
            "(strLastName|| ', ' ||strFirstName) as EngineerName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles  " & _
            "where numProgram = '4' " & _
            "order by strLastName "

            SQL7 = "Select " & _
            "(strLastName|| ', ' ||strFirstName) as EngineerName, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles  " & _
            "where numProgram = '3' " & _
            "order by strLastName "

            SQL8 = "Select strUnitDesc, numUnitCode " & _
            "from AIRBRANCH.LookUpEPDUnits  " & _
            "where numProgramCode = '5' " & _
            "order by strUnitDesc "

            SQL9 = "Select strUnitDesc, numUnitCode " & _
            "from AIRBRANCH.LookUpEPDUnits  " & _
            "where numProgramCode = '4' " & _
            "order by strUnitDesc "

            SQL10 = "Select strUnitDesc, numUnitCode " & _
            "from AIRBRANCH.LookUpEPDUnits  " & _
            "where numProgramCode = '3' " & _
            "order by strUnitDesc "

            dsEngineerList = New DataSet
            daEngineerList = New OracleDataAdapter(SQL, CurrentConnection)

            dsSSCPStaff = New DataSet
            daSSCPStaff = New OracleDataAdapter(SQL6, CurrentConnection)

            dsISMPStaff = New DataSet
            daISMPStaff = New OracleDataAdapter(SQL7, CurrentConnection)

            dsCountyList = New DataSet
            daCountyList = New OracleDataAdapter(SQL2, CurrentConnection)

            dsApplicationType = New DataSet
            daApplicationType = New OracleDataAdapter(SQL3, CurrentConnection)

            dsPermitType = New DataSet
            daPermitType = New OracleDataAdapter(SQL4, CurrentConnection)

            dsCity = New DataSet
            daCity = New OracleDataAdapter(SQL5, CurrentConnection)

            dsAPBUnit = New DataSet
            daAPBUnit = New OracleDataAdapter(SQL8, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEngineerList.Fill(dsEngineerList, "EngineerList")

            daEngineerList = New OracleDataAdapter(SQL11, CurrentConnection)
            daEngineerList.Fill(dsEngineerList, "EngineerList2")

            daSSCPStaff.Fill(dsSSCPStaff, "SSCPStaff")
            daISMPStaff.Fill(dsISMPStaff, "ISMPStaff")
            daCountyList.Fill(dsCountyList, "CountyList")
            daApplicationType.Fill(dsApplicationType, "ApplicationType")
            daPermitType.Fill(dsPermitType, "PermitType")
            daCity.Fill(dsCity, "CityList")
            daAPBUnit.Fill(dsAPBUnit, "SSPPUnit")
            daAPBUnit = New OracleDataAdapter(SQL9, CurrentConnection)
            daAPBUnit.Fill(dsAPBUnit, "SSCPUnit")
            daAPBUnit = New OracleDataAdapter(SQL10, CurrentConnection)
            daAPBUnit.Fill(dsAPBUnit, "ISMPUnit")

            dtEngineerList.Columns.Add("EngineerName", GetType(System.String))
            dtEngineerList.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtEngineerList.NewRow()
            drNewRow("EngineerName") = "N/A"
            drNewRow("numUserID") = "0"
            dtEngineerList.Rows.Add(drNewRow)

            For Each drDSRow In dsEngineerList.Tables("EngineerList").Rows()
                drNewRow = dtEngineerList.NewRow
                drNewRow("EngineerName") = drDSRow("EngineerName")
                drNewRow("numUserID") = drDSRow("numUSerID")
                dtEngineerList.Rows.Add(drNewRow)
            Next

            For Each drDSRow In dsEngineerList.Tables("EngineerList2").Rows()
                drNewRow = dtEngineerList.NewRow
                drNewRow("EngineerName") = drDSRow("EngineerName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtEngineerList.Rows.Add(drNewRow)
            Next

            With cboEngineer
                .DataSource = dtEngineerList
                .DisplayMember = "EngineerName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtSSCPList.Columns.Add("EngineerName", GetType(System.String))
            dtSSCPList.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtSSCPList.NewRow()
            drNewRow("EngineerName") = "N/A"
            drNewRow("numUserID") = "0"
            dtSSCPList.Rows.Add(drNewRow)

            For Each drDSRow6 In dsSSCPStaff.Tables("SSCPStaff").Rows()
                drNewRow = dtSSCPList.NewRow
                drNewRow("EngineerName") = drDSRow6("EngineerName")
                drNewRow("numUserID") = drDSRow6("numUserID")
                dtSSCPList.Rows.Add(drNewRow)
            Next

            With cboSSCPStaff
                .DataSource = dtSSCPList
                .DisplayMember = "EngineerName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtISMPList.Columns.Add("EngineerName", GetType(System.String))
            dtISMPList.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtISMPList.NewRow()
            drNewRow("EngineerName") = "N/A"
            drNewRow("numUserID") = "0"
            dtISMPList.Rows.Add(drNewRow)

            For Each drDSRow7 In dsISMPStaff.Tables("ISMPStaff").Rows()
                drNewRow = dtISMPList.NewRow
                drNewRow("EngineerName") = drDSRow7("EngineerName")
                drNewRow("numUserID") = drDSRow7("numUserID")
                dtISMPList.Rows.Add(drNewRow)
            Next

            With cboISMPStaff
                .DataSource = dtISMPList
                .DisplayMember = "EngineerName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtCountyList.Columns.Add("strCountyCode", GetType(System.String))
            dtCountyList.Columns.Add("strCountyName", GetType(System.String))
            dtCountyList.Columns.Add("strAttainmentStatus", GetType(System.String))

            drNewRow = dtCountyList.NewRow()
            drNewRow("strCountyCode") = "000"
            drNewRow("strCountyName") = "N/A"
            dtCountyList.Rows.Add(drNewRow)

            For Each drDSRow2 In dsCountyList.Tables("CountyList").Rows()
                drNewRow = dtCountyList.NewRow
                drNewRow("strCountyCode") = drDSRow2("strCountyCode")
                drNewRow("strCountyName") = drDSRow2("strCountyName")
                dtCountyList.Rows.Add(drNewRow)
            Next

            With cboCounty
                .DataSource = dtCountyList
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With

            dtApplicationType.Columns.Add("strApplicationTypeCode", GetType(System.String))
            dtApplicationType.Columns.Add("strApplicationTypeDesc", GetType(System.String))

            drNewRow = dtApplicationType.NewRow()
            drNewRow("strApplicationTypeCode") = "0"
            drNewRow("strApplicationTypeDesc") = "N/A"
            dtApplicationType.Rows.Add(drNewRow)

            For Each drDSRow3 In dsApplicationType.Tables("ApplicationType").Rows()
                drNewRow = dtApplicationType.NewRow
                drNewRow("strApplicationTypeCode") = drDSRow3("strApplicationTypeCode")
                drNewRow("strApplicationTypeDesc") = drDSRow3("strApplicationTypeDesc")
                dtApplicationType.Rows.Add(drNewRow)
            Next

            With cboApplicationType
                .DataSource = dtApplicationType
                .DisplayMember = "strApplicationTypeDesc"
                .ValueMember = "strApplicationTypeCode"
                .SelectedIndex = 0
            End With

            dtPermitType.Columns.Add("strPermitTypeCode", GetType(System.String))
            dtPermitType.Columns.Add("strPermitTypeDescription", GetType(System.String))

            drNewRow = dtPermitType.NewRow
            drNewRow("strPermitTypeCode") = " "
            drNewRow("strPermitTypeDescription") = " "
            dtPermitType.Rows.Add(drNewRow)

            For Each drDSRow4 In dsPermitType.Tables("PermitType").Rows()
                drNewRow = dtPermitType.NewRow
                drNewRow("strPermitTypeDescription") = drDSRow4("strPermitTypeDescription")
                drNewRow("strPermitTypeCode") = drDSRow4("strPermitTypeCode")
                dtPermitType.Rows.Add(drNewRow)
            Next

            With cboPermitAction
                .DataSource = dtPermitType
                .DisplayMember = "strPermitTypeDescription"
                .ValueMember = "strPermitTypeCode"
                .SelectedIndex = 0
            End With

            dtCity.Columns.Add("City", GetType(System.String))

            drNewRow = dtCity.NewRow
            drNewRow("City") = " "
            dtCity.Rows.Add(drNewRow)

            For Each drDSRow5 In dsCity.Tables("CityList").Rows()
                drNewRow = dtCity.NewRow
                drNewRow("City") = drDSRow5("City")
                dtCity.Rows.Add(drNewRow)
            Next

            With cboFacilityCity
                .DataSource = dtCity
                .DisplayMember = "City"
                .ValueMember = "City"
                .SelectedIndex = 0
            End With

            dtSSPPUnit.Columns.Add("strUnitDesc", GetType(System.String))
            dtSSPPUnit.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtSSPPUnit.NewRow
            drNewRow("strUnitDesc") = " "
            drNewRow("numUnitCode") = ""
            dtSSPPUnit.Rows.Add(drNewRow)

            For Each drDSRow8 In dsAPBUnit.Tables("SSPPUnit").Rows()
                drNewRow = dtSSPPUnit.NewRow
                drNewRow("strUnitDesc") = drDSRow8("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow8("numUnitCode")
                dtSSPPUnit.Rows.Add(drNewRow)
            Next

            With cboApplicationUnit
                .DataSource = dtSSPPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

            dtSSCPUnit.Columns.Add("strUnitDesc", GetType(System.String))
            dtSSCPUnit.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtSSCPUnit.NewRow
            drNewRow("strUnitDesc") = "No Review Needed"
            drNewRow("numUnitCode") = "0"
            dtSSCPUnit.Rows.Add(drNewRow)

            For Each drDSRow9 In dsAPBUnit.Tables("SSCPUnit").Rows()
                drNewRow = dtSSCPUnit.NewRow
                drNewRow("strUnitDesc") = drDSRow9("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow9("numUnitCode")
                dtSSCPUnit.Rows.Add(drNewRow)
            Next

            With cboSSCPUnits
                .DataSource = dtSSCPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

            dtISMPUnit.Columns.Add("strUnitDesc", GetType(System.String))
            dtISMPUnit.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtISMPUnit.NewRow
            drNewRow("strUnitDesc") = "No Review Needed"
            drNewRow("numUnitCode") = "0"
            dtISMPUnit.Rows.Add(drNewRow)

            For Each drDSRow10 In dsAPBUnit.Tables("ISMPUnit").Rows()
                drNewRow = dtISMPUnit.NewRow
                drNewRow("strUnitDesc") = drDSRow10("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow10("numUnitCode")
                dtISMPUnit.Rows.Add(drNewRow)
            Next

            With cboISMPUnits
                .DataSource = dtISMPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Sub LoadPermissions()
        Try
            'DMU developers and DMU Manager  - VALIDATED 
            'AccountArray(129, 3) = "1"

            'SSPP Program Manager - VALIDATED
            'AccountArray(24, 3) = "1" and AccountArray(3,4) = "1" and AccountArray(12,1) = "1" and AccountArray(12,2) = "0"  

            'ISMP Program Manager - Validated 
            'AccountArray(17, 3) = "1" and AccountArray(17,4) = "0" 

            'SSCP Program Manager - 
            'AccountArray(22, 4) = "1" and AccountArray(22,3) = "0" 

            'SSPP Unit Manager - VALIDATED
            'AccountArray(24, 3) = "1" and accountArray(12, 1)= "1" and accountArray(12,2) = "0" and accountArray(3,4) = "0" 

            'ISMP Unit Manager - VALIDATED 
            'AccountArray(17, 1) = "0" and AccountArray(17,2) = "1" and AccountArray(17,3) = "1" 

            'SSCP Unit Manager - 
            'AccountArray(22, 4) = "0" and AccountArray(22,3) = "1" 

            'SSPP Admin (Kella Johnson & Cathy Toney) - VALIDATED 
            'AccountArray(51, 4) = "1" and AccountArray(23, 3) = "1" and AccountArray(138,1) = "1" 

            'SSPP Administrator 2 (Nancy Johns) - Validated 
            'AccountArray(51, 4) = "1" and accountArray(12,1) = "1" and AccountArray(138, 0) is nothing 

            'Web USers i.e. Lynn - Validated 
            'AccountArray(131, 2) = "1" and AccountArray(127,3) = "1" and accountArray(127,4) = "0" 

            'ISMP Users - Validated 
            'AccountArray(67, 2) = "1" 

            'SSCP Staff, District Users, RMP Specialist - Validated 
            'AccountArray(48, 2) = "1"  and AccountArray(48, 3) = "0" 

            'SSPP Engineer - Validated 
            'AccountArray(3, 2) = "1" and AccountArray(3, 4) = "0" 

            'Branch Chief - Validated 
            'AccountArray(51, 3) = "1" and accountArray(20, 3)= "1" and AccountArray(51,1) = "0" 

            'All others 

            'VALIDATE ALL CODES FROM LOOK UP IAIP ACCOUNTS

  
            If TCApplicationTrackingLog.TabPages.Contains(TPTrackingLog) Then
            Else
                TCApplicationTrackingLog.TabPages.Add(TPTrackingLog)
            End If

            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                 AccountFormAccess(67, 2) = "1" Or _
                (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                If TCApplicationTrackingLog.TabPages.Contains(TPReviews) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPReviews)
                End If
            End If
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                If TCApplicationTrackingLog.TabPages.Contains(TPApplicationHistroy) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPApplicationHistroy)
                End If
            End If
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                 (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                If TCApplicationTrackingLog.TabPages.Contains(TPInformationRequests) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPInformationRequests)
                End If
            End If
            If AccountFormAccess(129, 3) = "1" Or _
                  (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                  (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                   AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0" Then
                If TCApplicationTrackingLog.TabPages.Contains(TPWebPublisher) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPWebPublisher)
                End If
            End If

            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
               AccountFormAccess(67, 2) = "1" Or _
               (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                If TCApplicationTrackingLog.TabPages.Contains(TPOtherInfo) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPOtherInfo)
                End If
            End If
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
                 AccountFormAccess(67, 2) = "1" Or _
                (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                If TCApplicationTrackingLog.TabPages.Contains(TPContactInformation) Then
                Else
                    TCApplicationTrackingLog.TabPages.Add(TPContactInformation)
                End If
            End If

            If TCApplicationTrackingLog.TabPages.Contains(TPOtherInfo) Then
            Else
                TCApplicationTrackingLog.TabPages.Add(TPOtherInfo)
            End If
            If TCApplicationTrackingLog.TabPages.Contains(TPContactInformation) Then
            Else
                TCApplicationTrackingLog.TabPages.Add(TPContactInformation)
                btnGetCurrentPermittingContact.Visible = False
            End If

            If TCApplicationTrackingLog.TabPages.Contains(TPPermitUploader) Then
            Else
                TCApplicationTrackingLog.TabPages.Add(TPPermitUploader)
            End If

            'btnAddApplicationToList 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddApplicationToList.Enabled = True
            Else
                btnAddApplicationToList.Enabled = False
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0")  Then
                btnAddApplicationToList.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddApplicationToList.BackColor = Color.Yellow
            End If

            'btnAddNewMactSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddNewMACTSubpart.Enabled = True
            End If
            'btnAddNewNESHAPSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddNewNESHAPSubpart.Enabled = True
            End If
            'btnAddNewNSPSSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddNewNSPSSubpart.Enabled = True
            End If
            'btnAddNewSIPSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnAddNewSIPSubpart.Enabled = True
            End If
            'btnClearAddModifiedMACTs 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedMACTs.Enabled = True
            End If
            'btnClearAddModifiedNESHAPs 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedNESHAPs.Enabled = True
            End If
            'btnClearAddModifiedNSPSs 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedNSPSs.Enabled = True
            End If
            'btnClearAddModifiedSIPs 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedSIPs.Enabled = True
            End If
            'btnClearMACTDeletes 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearMACTDeletes.Enabled = True
            End If
            'btnClearNESHAPDeletes 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearNESHAPDeletes.Enabled = True
            End If
            'btnClearNSPSDeletes 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearNSPSDeletes.Enabled = True
            End If
            'btnClearSIPDeletes 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearSIPDeletes.Enabled = True
            End If
            'btnMACTDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTDelete.Enabled = True
            End If
            'btnMACTDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTDeleteAll.Enabled = True
            End If
            'btnMACTEdit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTEdit.Enabled = True
            End If
            'btnMACTEditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTEditAll.Enabled = True
            End If
            'btnMACTunDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTUndelete.Enabled = True
            End If
            'btnMACTunDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTUndeleteAll.Enabled = True
            End If
            'btnMACTUnedit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTUnedit.Enabled = True
            End If
            'btnMACTUneditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnMACTUneditAll.Enabled = True
            End If
            'btnNESHAPDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPDelete.Enabled = True
            End If
            'btnNESHAPDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPDeleteAll.Enabled = True
            End If
            'btnNESHAPEdit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPEdit.Enabled = True
            End If
            'btnNESHAPEditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPEditAll.Enabled = True
            End If
            'btnNESHAPunDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUndelete.Enabled = True
            End If
            'btnNESHAPunDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUndeleteAll.Enabled = True
            End If
            'btnNESHAPUnedit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUnedit.Enabled = True
            End If
            'btnNESHAPUneditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUneditAll.Enabled = True
            End If
            'btnNSPSDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSDelete.Enabled = True
            End If
            'btnNSPSDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSDeleteAll.Enabled = True
            End If
            'btnNSPSEdit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSEdit.Enabled = True
            End If
            'btnNSPSEditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSEditAll.Enabled = True
            End If
            'btnNSPSunDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSUndelete.Enabled = True
            End If
            'btnNSPSunDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSUndeleteAll.Enabled = True
            End If
            'btnNSPSUnedit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSUnedit.Enabled = True
            End If
            'btnNSPSUneditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnNSPSUneditAll.Enabled = True
            End If
            'btnSIPDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPDelete.Enabled = True
            End If
            'btnSIPDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPDeleteAll.Enabled = True
            End If
            'btnSIPEdit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPEdit.Enabled = True
            End If
            'btnSIPEditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPEditAll.Enabled = True
            End If
            'btnSIPunDelete 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPUndelete.Enabled = True
            End If
            'btnSIPunDeleteAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPUndeleteAll.Enabled = True
            End If
            'btnSIPUnedit 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPUnedit.Enabled = True
            End If
            'btnSIPUneditAll 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSIPUneditAll.Enabled = True
            End If
            'btnSaveMACTSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveMACTSubpart.Enabled = True
            End If
            'btnSaveNESHAPSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveNESHAPSubpart.Enabled = True
            End If
            'btnSaveNSPSSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveNSPSSubpart.Enabled = True
            End If
            'btnSaveSIPSubpart 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveSIPSubpart.Enabled = True
            End If

            'btnClearInformationRequest
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.BackColor = Color.Yellow
            End If


            'btnClearLinks
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.BackColor = Color.Yellow
            End If

            'btnClearList
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearList.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnClearList.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnClearList.BackColor = Color.Yellow
            End If

            'btnDeleteInformationRequest
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.BackColor = Color.Yellow
            End If
            'btnGoToFeeContact 

            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 3) = "1" And AccountFormAccess(20, 3) = "1" And AccountFormAccess(51, 1) = "0") Then
                btnGoToFeeContact.Visible = True
            Else
                btnGoToFeeContact.Visible = False
            End If

            'btnLinkApplications
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.BackColor = Color.Yellow
            End If

            'btnLoadFacilityApplicationHistory
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnLoadFacilityApplicationHistory.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnLoadFacilityApplicationHistory.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnLoadFacilityApplicationHistory.BackColor = Color.Yellow
            End If

            'btnSaveInformationRequest
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.BackColor = Color.Yellow
            End If
            'btnViewInformationRequests
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnViewInformationRequests.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                btnViewInformationRequests.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnViewInformationRequests.BackColor = Color.Yellow
            End If
            'BtnRefreshAIRSNo
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                btnRefreshAIRSNo.Enabled = True
                btnRefreshAIRSNo.Visible = True
            Else
                btnRefreshAIRSNo.Visible = False
            End If


            'btnSaveSIPSubpart
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveSIPSubpart.Visible = True
            Else
                btnSaveSIPSubpart.Visible = False
            End If
            'btnSaveMACTSubpart
            If AccountFormAccess(129, 3) = "1" Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveMACTSubpart.Visible = True
            Else
                btnSaveMACTSubpart.Visible = False
            End If
            'btnSaveNESHAPSubpart
            If AccountFormAccess(129, 3) = "1" Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveNESHAPSubpart.Visible = True
            Else
                btnSaveNESHAPSubpart.Visible = False
            End If
            'btnSaveNSPSSubpart
            If AccountFormAccess(129, 3) = "1" Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                btnSaveNSPSSubpart.Visible = True
            Else
                btnSaveNSPSSubpart.Visible = False
            End If

            'btnSaveWebPublisher
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                btnSaveWebPublisher.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                btnSaveWebPublisher.BackColor = Color.PeachPuff
            End If
            'btnGetCurrentPermittingContact
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                btnGetCurrentPermittingContact.Visible = True
            Else
                btnGetCurrentPermittingContact.Visible = False
            End If
            'cboApplicationType
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.BackColor = Color.LightGreen
                lblApplicationType.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                cboApplicationType.BackColor = Color.PeachPuff
                lblApplicationType.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.BackColor = Color.Yellow
                lblApplicationType.BackColor = Color.Yellow
            End If

            'cboApplicationUnit
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                cboApplicationUnit.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboApplicationUnit.BackColor = Color.LightBlue
                lblApplicationUnit.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                cboApplicationUnit.BackColor = Color.PeachPuff
                lblApplicationUnit.BackColor = Color.PeachPuff
            End If

            'cboClassification
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboClassification.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboClassification.BackColor = Color.LightGreen
                lblClassification.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboClassification.BackColor = Color.Yellow
                lblClassification.BackColor = Color.Yellow
            End If

            'cboCounty
            If AccountFormAccess(129, 3) = "1" Then
                cboCounty.Enabled = True
            End If

            'cboEngineer
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboEngineer.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboEngineer.BackColor = Color.LightBlue
                lblEngineer.BackColor = Color.LightBlue
            End If

            'cboFacilityCity
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                cboFacilityCity.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.BackColor = Color.Yellow
            End If
            'cboISMPStaff
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                cboISMPStaff.BackColor = Color.Yellow
                lblISMPStaff.BackColor = Color.Yellow
            End If
            'cboOperationalStatus
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.BackColor = Color.LightGreen
                lblOperationalStatus.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                cboOperationalStatus.BackColor = Color.PeachPuff
                lblOperationalStatus.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.BackColor = Color.Yellow
                lblOperationalStatus.BackColor = Color.Yellow
            End If
            'cboPermitAction
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                  (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.BackColor = Color.LightGreen
                lblPermitAction.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.BackColor = Color.Yellow
                lblPermitAction.BackColor = Color.Yellow
            End If
            'cboPublicAdvisory
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Then
                cboPublicAdvisory.BackColor = Color.LightBlue
                lblPublicAdvisory.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.BackColor = Color.LightGreen
                lblPublicAdvisory.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.BackColor = Color.Yellow
                lblPublicAdvisory.BackColor = Color.Yellow
            End If
            'cboSSCPStaff 
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                AccountFormAccess(67, 2) = "1" Then
                cboSSCPStaff.BackColor = Color.Yellow
                lblSSCPStaff.BackColor = Color.Yellow
            End If
            'chbNAANSR
            If AccountFormAccess(129, 3) = "1" Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbNAANSR.Enabled = True
            End If
            'chb112
            If AccountFormAccess(129, 3) = "1" Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chb112g.Enabled = True
            End If
            'chbCDS_0
            If AccountFormAccess(129, 3) = "1" Then
                chbCDS_0.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_0.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_0.BackColor = Color.Yellow
            End If
            'chbCDS_6
            If AccountFormAccess(129, 3) = "1" Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.BackColor = Color.Yellow
            End If
            'chbCDS_7
            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.BackColor = Color.Yellow
            End If
            'chbCDS_8
            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.BackColor = Color.Yellow
            End If
            'chbCDS_9
            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.BackColor = Color.Yellow
            End If
            'chbCDS_A
            If AccountFormAccess(129, 3) = "1" Or _
          (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
          (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
          (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
          (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.BackColor = Color.Yellow
            End If
            'chbCDS_M
            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.BackColor = Color.Yellow
            End If
            'chbCDS_RMP
            If AccountFormAccess(129, 3) = "1" Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
           (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.BackColor = Color.Yellow
            End If
            'chbCDS_V
            If AccountFormAccess(129, 3) = "1" Or _
          (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
          (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
          (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
          (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.BackColor = Color.Yellow
            End If
            'chbClosedOut 
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                chbClosedOut.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbClosedOut.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                chbClosedOut.BackColor = Color.PeachPuff
            End If
            'chbHAPsMajor
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbHAPsMajor.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbHAPsMajor.BackColor = Color.LightBlue
            End If
            'chbNSRMajor
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbNSRMajor.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbNSRMajor.BackColor = Color.LightBlue
            End If
            'chbPal
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbPal.Enabled = True
            End If
            'chbExpedited
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbExpedited.Enabled = True
            End If
            'chbConfidential
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                CurrentUser.IaipAccountCodes.Contains(29) Then
                chbConfidential.Enabled = True
            End If
            'chbPAReady
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1" Or _
                AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing Or _
                AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0" Then
                chbPAReady.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbPAReady.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbPAReady.BackColor = Color.Yellow
            End If
            'chbPNReady
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1" Or _
                AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing Or _
                AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0" Then
                chbPNReady.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbPNReady.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                chbPNReady.BackColor = Color.Yellow
            End If
            If (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                chbPNReady.Enabled = True
            End If
            'chbPSD
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbPSD.Enabled = True
            End If
            'chbRulett
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbRulett.Enabled = True
            End If
            'chbRuleyy
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                chbRuleyy.Enabled = True
            End If
            'DTPDateAcknowledge 
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                DTPDateAcknowledge.Enabled = True
            End If

            'DTPDateAssigned
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                DTPDateAssigned.Enabled = True
            End If
            'DTPDatePAExpires
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPDatePAExpires.Enabled = True
            End If
            'DTPDatePNExpires
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPDatePNExpires.Enabled = True
            End If
            'DTPDateReassigned
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                DTPDateReassigned.Enabled = True
            End If
            'DTPDateReceived
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                DTPDateReceived.Enabled = True
            End If
            'DTPDateSent
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                DTPDateSent.Enabled = True
            End If
            'DTPDateToBC
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPDateToBC.Enabled = True
            End If
            'DTPDateToDO
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPDateToDO.Enabled = True
            End If
            'DTPDateToPM
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                DTPDateToPM.Enabled = True
            End If
            'DTPDateToUC
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                DTPDateToUC.Enabled = True
            End If
            'DTPDeadline
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                DTPDeadline.Enabled = True
            End If
            'DTPDraftIssued
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPDraftIssued.Enabled = True
            End If
            'DTPDraftOnWeb
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPDraftOnWeb.Enabled = True
            End If
            'DTPEffectiveDateofPermit
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPEffectiveDateofPermit.Enabled = True
            End If
            'DTPEPAEnds
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPEPAEnds.Enabled = True
            End If
            'DTPEPANotifiedPermitOnWeb
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPEPANotifiedPermitOnWeb.Enabled = True
            End If
            'DTPEPAStatesNotified
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPEPAStatesNotified.Enabled = True
            End If
            'DTPEPAWaived
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPEPAWaived.Enabled = True
            End If
            'DTPExperationDate
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPExperationDate.Enabled = True
            End If
            'DTPFinalAction
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                DTPFinalAction.Enabled = True
            End If
            'DTPFinalOnWeb
            If AccountFormAccess(129, 3) = "1" Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPFinalOnWeb.Enabled = True
            End If
            'DTPInformationReceived
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                DTPInformationReceived.Enabled = True
            End If
            'DTPInformationRequested
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                DTPInformationRequested.Enabled = True
            End If
            'DTPNotifiedAppReceived
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPNotifiedAppReceived.Enabled = True
            End If
            'DTPPNExpires
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                DTPPNExpires.Enabled = True
            End If
            'DTPReviewSubmitted
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                DTPReviewSubmitted.Enabled = True
            End If
            'DTPISMPReview
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
               AccountFormAccess(67, 2) = "1" Then
                DTPISMPReview.Enabled = True
            End If
            'DTPSSCPReview
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                DTPSSCPReview.Enabled = True
            End If
            'lbEPAStatesNotified
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                  (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lbEPAStatesNotified.BackColor = Color.PeachPuff
            End If
            'lblDateAcknowledge
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDateAcknowledge.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                lblDateAcknowledge.BackColor = Color.Yellow
            End If
            'lblDateAssigned
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDateAssigned.BackColor = Color.LightBlue
            End If
            'lblDated
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblDated.BackColor = Color.PeachPuff
            End If
            'lblDatePAExpires
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblDatePAExpires.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblDatePAExpires.BackColor = Color.Yellow
            End If
            'lblDatePNExpires
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblDatePNExpires.BackColor = Color.PeachPuff
            End If
            'lblDateReassigned
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDateReassigned.BackColor = Color.LightBlue
            End If
            'lblDatetoBC
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblDatetoBC.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblDatetoBC.BackColor = Color.Yellow
            End If
            'lblDateToDO
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblDateToDO.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblDateToDO.BackColor = Color.Yellow
            End If

            'lblDateToPM
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDateToPM.BackColor = Color.LightBlue
            End If
            'lblDateToUC
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDateToUC.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
            (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                lblDateToUC.BackColor = Color.Yellow
            End If
            'lblDeadline
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblDeadline.BackColor = Color.LightBlue
            End If
            'lblDraftIssued
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblDraftIssued.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblDraftIssued.BackColor = Color.Yellow
            End If

            'lblDraftOnWeb
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblDraftOnWeb.BackColor = Color.PeachPuff
            End If
            'lblEffectiveDateofPermit
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblEffectiveDateofPermit.BackColor = Color.PeachPuff
            End If
            'lblEPAEnds
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblEPAEnds.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblEPAEnds.BackColor = Color.Yellow
            End If
            'lblEPANotifiedFinalOnWeb
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblEPANotifiedFinalOnWeb.BackColor = Color.PeachPuff
            End If
            'lblEPAWaived
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblEPAWaived.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblEPAWaived.BackColor = Color.Yellow
            End If
            'lblExperationDate
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblExperationDate.BackColor = Color.PeachPuff
            End If
            'lblFinalAction
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
              (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblFinalAction.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                lblFinalAction.BackColor = Color.Yellow
            End If
            'lblFinalOnWeb
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblFinalOnWeb.BackColor = Color.PeachPuff
            End If
            'lblInformationReceived
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblInformationReceived.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                lblInformationReceived.BackColor = Color.Yellow
            End If
            'lblInformationRequested
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
           (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblInformationRequested.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
             (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                lblInformationRequested.BackColor = Color.Yellow
            End If
            'lblISMPReview
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
              AccountFormAccess(67, 2) = "1" Then
                lblISMPReview.BackColor = Color.Yellow
            End If
            'lblNotifiedAppReceived
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblNotifiedAppReceived.BackColor = Color.PeachPuff
            End If
            'lblPermitAction
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblPermitAction.BackColor = Color.LightGreen
            End If
            'lblPermitNumber
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblPermitNumber.BackColor = Color.LightGreen
            End If
            'lblPNExpires
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                lblPNExpires.BackColor = Color.PeachPuff
            End If
            'lblPublicAdvisory
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Then
                lblPublicAdvisory.BackColor = Color.LightGreen
            End If
            'lblReceived
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
            (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                lblReceived.BackColor = Color.PeachPuff
            End If
            'lblReviewSubmitted
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                lblReviewSubmitted.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
              (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                lblReviewSubmitted.BackColor = Color.Yellow
            End If
            'lblSSCPReview
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
              (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                lblSSCPReview.BackColor = Color.Yellow
            End If
            'mmiNewApplication
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                mmiNewApplication.Enabled = True
                mmiNewApplication.Visible = True
            End If
            'rdbISMPNo
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                 AccountFormAccess(67, 2) = "1" Then
                rdbISMPNo.BackColor = Color.Yellow
            End If
            'rdbISMPYes
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                 AccountFormAccess(67, 2) = "1" Then
                rdbISMPYes.BackColor = Color.Yellow
            End If
            'rdbSSCPNo
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
             (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                rdbSSCPNo.BackColor = Color.Yellow
            End If
            'rdbSSCPYes
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
           (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                rdbSSCPYes.BackColor = Color.Yellow
            End If
            'txtAIRSNumber
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtAIRSNumber.ReadOnly = False
            Else
                txtAIRSNumber.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtAIRSNumber.BackColor = Color.PeachPuff
            End If
            'txtApplicationNumber
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtApplicationNumber.ReadOnly = False
            Else
                txtApplicationNumber.ReadOnly = True
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtApplicationNumber.BackColor = Color.PeachPuff
            End If
            'txtComments
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtComments.ReadOnly = False
            Else
                txtComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtComments.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtComments.BackColor = Color.Yellow
            End If
            'txtEPATargetedComments
            If AccountFormAccess(129, 3) = "1" Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                txtEPATargetedComments.ReadOnly = False
            Else
                txtEPATargetedComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
             (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Then
                txtEPATargetedComments.BackColor = Color.PeachPuff
            End If
            'txtFacilityName
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.ReadOnly = False
            Else
                txtFacilityName.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtFacilityName.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.BackColor = Color.Yellow
            End If
            'txtFacilityStreetAddress
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                     (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.ReadOnly = False
            Else
                txtFacilityStreetAddress.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtFacilityStreetAddress.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.BackColor = Color.Yellow
            End If
            'txtFacilityZipCode
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                     (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.ReadOnly = False
            Else
                txtFacilityZipCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtFacilityZipCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.BackColor = Color.Yellow
            End If
            'txtInformationReceived
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                 (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.ReadOnly = False
            Else
                txtInformationReceived.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
              (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
              (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.BackColor = Color.Yellow
            End If
            'txtInformationRequested
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                 (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.ReadOnly = False
            Else
                txtInformationRequested.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
              (AccountFormAccess(131, 2) = "1" And AccountFormAccess(127, 3) = "1" And AccountFormAccess(127, 4) = "0") Or _
              (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.BackColor = Color.Yellow
            End If
            'txtISMPComments
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
                AccountFormAccess(67, 2) = "1" Then
                txtISMPComments.BackColor = Color.Yellow
            End If
            'txtNAICSCode
            If AccountFormAccess(129, 3) = "1" Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                 (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                 (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                     (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.ReadOnly = False
            Else
                txtNAICSCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtNAICSCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
              (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.BackColor = Color.Yellow
            End If
            'txtPermitNumber
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.ReadOnly = False
            Else
                txtPermitNumber.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
              (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.BackColor = Color.Yellow
            End If
            'txtPlantDescription
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                    (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.ReadOnly = False
            Else
                txtPlantDescription.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtPlantDescription.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.BackColor = Color.Yellow
            End If
            'txtReasonAppSubmitted
            If AccountFormAccess(129, 3) = "1" Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
                (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
                (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                    (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.ReadOnly = False
            Else
                txtReasonAppSubmitted.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtReasonAppSubmitted.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.BackColor = Color.Yellow
            End If
            'txtSICCode
            If AccountFormAccess(129, 3) = "1" Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
              (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
              (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Or _
              (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
                  (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtSICCode.ReadOnly = False
            Else
                txtSICCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtSICCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(23, 3) = "1" And AccountFormAccess(138, 1) = "1") Then
                txtSICCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtSICCode.BackColor = Color.Yellow
            End If
            'txtSignificantComments
            If AccountFormAccess(129, 3) = "1" Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
               (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Or _
               (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Or _
               (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Then
                txtSignificantComments.ReadOnly = False
            Else
                txtSignificantComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" And AccountFormAccess(3, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0") Or _
            (AccountFormAccess(24, 3) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(12, 2) = "0" And AccountFormAccess(3, 4) = "0") Then
                txtSignificantComments.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" And AccountFormAccess(12, 1) = "1" And AccountFormAccess(138, 0) Is Nothing) Then
                txtSignificantComments.BackColor = Color.Yellow
            End If
            'txtSSCPComments
            If (AccountFormAccess(3, 2) = "1" And AccountFormAccess(3, 4) = "0") Or _
              (AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0") Then
                txtSSCPComments.BackColor = Color.Yellow
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFacilityApplicationHistory()
        Dim AIRSNumber As String = ""

        Try
             
            SQL = "Select strAIRSNumber " & _
            "from AIRBRANCH.SSPPApplicationMaster " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                AIRSNumber = dr.Item("strAIRSNumber")
            End If
            dr.Close()

            If AIRSNumber = "" Then
                AIRSNumber = txtAIRSNumber.Text
            End If

            SQL = "Select to_Number(AIRBRANCH.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber, " & _
            "case " & _
            "    when strApplicationTypeDesc is Null then ' ' " & _
            "Else strApplicationTypeDesc " & _
            "End strApplicationTypeDesc, " & _
            "case " & _
            "    when AIRBRANCH.SSPPApplicationMaster.strStaffResponsible is Null then ' ' " & _
            "else (strLastName||', '||strFirstName) " & _
            "end staffResponsible, " & _
            "case " & _
            "    when AIRBRANCH.SSPPApplicationMaster.datFinalizedDate is Null then ' ' " & _
            "else to_char(AIRBRANCH.SSPPApplicationMaster.datFinalizedDate, 'FMMonth DD, YYYY') " & _
            "end FinalizedDate, " & _
            "case " & _
            "    when AIRBRANCH.SSPPApplicationTracking.datSentByFacility is Null then ' ' " & _
            "else to_char(AIRBRANCH.SSPPApplicationTracking.datSentByFacility, 'FMMonth DD, YYYY') " & _
            "end DateSent, " & _
            "case " & _
            "    when AIRBRANCH.LookUpEPDUnits.strUnitDesc is Null then ' ' " & _
            "Else AIRBRANCH.LookUpEPDUnits.strUnitDesc " & _
            "End strUnitTitle, " & _
            "case " & _
            "    when AIRBRANCH.SSPPApplicationData.strComments is Null then ' ' " & _
            "else AIRBRANCH.SSPPApplicationData.strComments " & _
            "end strComments, " & _
            "case " & _
            "    when AIRBRANCH.SSPPApplicationData.strApplicationNotes is Null then ' ' " & _
            "else AIRBRANCH.SSPPApplicationData.strApplicationNotes " & _
            "end strApplicationNotes " & _
            "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationTracking, " & _
            "AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits, AIRBRANCH.LookUpApplicationTypes  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
            "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSPPApplicationMaster.strStaffResponsible " & _
            "and AIRBRANCH.SSPPApplicationMaster.APBUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+) " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
            "and AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = '" & AIRSNumber & "' "

            dsFacAppHistory = New DataSet
            daFacAppHistory = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFacAppHistory.Fill(dsFacAppHistory, "FacAppHistory")
            dgvFacilityAppHistory.DataSource = dsFacAppHistory
            dgvFacilityAppHistory.DataMember = "FacAppHistory"

            dgvFacilityAppHistory.RowHeadersVisible = False
            dgvFacilityAppHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFacilityAppHistory.AllowUserToResizeColumns = True
            dgvFacilityAppHistory.AllowUserToAddRows = False
            dgvFacilityAppHistory.AllowUserToDeleteRows = False
            dgvFacilityAppHistory.AllowUserToOrderColumns = True
            dgvFacilityAppHistory.AllowUserToResizeRows = True

            dgvFacilityAppHistory.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvFacilityAppHistory.Columns("strApplicationNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvFacilityAppHistory.Columns("strApplicationNumber").DisplayIndex = 0
            dgvFacilityAppHistory.Columns("strComments").HeaderText = "Comments"
            dgvFacilityAppHistory.Columns("strComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvFacilityAppHistory.Columns("strComments").DisplayIndex = 1
            dgvFacilityAppHistory.Columns("strApplicationNotes").HeaderText = "Reason Application Submitted"
            dgvFacilityAppHistory.Columns("strApplicationNotes").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("strApplicationNotes").DisplayIndex = 2
            dgvFacilityAppHistory.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvFacilityAppHistory.Columns("StaffResponsible").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("StaffResponsible").DisplayIndex = 3
            dgvFacilityAppHistory.Columns("FinalizedDate").HeaderText = "Date Finalized"
            dgvFacilityAppHistory.Columns("FinalizedDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("FinalizedDate").DisplayIndex = 4
            dgvFacilityAppHistory.Columns("DateSent").HeaderText = "Date Sent"
            dgvFacilityAppHistory.Columns("DateSent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("DateSent").DisplayIndex = 5
            dgvFacilityAppHistory.Columns("strUnitTitle").HeaderText = "Unit"
            dgvFacilityAppHistory.Columns("strUnitTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("strUnitTitle").DisplayIndex = 6
            dgvFacilityAppHistory.Columns("strApplicationTypeDesc").HeaderText = "Application Type"
            dgvFacilityAppHistory.Columns("strApplicationTypeDesc").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvFacilityAppHistory.Columns("strApplicationTypeDesc").DisplayIndex = 7

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Sub LoadInformationRequestedHistory()
        Try
             
            SQL = "Select " & _
               "strApplicationNumber, strRequestKey, " & _
               "Case " & _
               "   when datInformationRequested is Null then ' ' " & _
               "else to_char(datInformationRequested, 'FMMonth DD, YYYY') " & _
               "End InformationRequested, " & _
               "case " & _
               "when strInformationrequested is Null then ' ' " & _
               "else strInformationrequested " & _
               "end strInformationrequested, " & _
               "case " & _
               "    when datInformationReceived is null then ' ' " & _
               "else to_char(datInformationReceived, 'FMMonth DD, YYYY') " & _
               "End InformationReceived, " & _
               "case " & _
               "when strInformationReceived is Null then ' ' " & _
               "else strInformationReceived " & _
               "end strInformationReceived " & _
               "from AIRBRANCH.SSPPApplicationInformation " & _
               "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
               "order by strRequestKey "

            dsFacInfoHistory = New DataSet
            daFacInfoHistory = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFacInfoHistory.Fill(dsFacInfoHistory, "FacInfoHistory")
            dgvInformationRequested.DataSource = dsFacInfoHistory
            dgvInformationRequested.DataMember = "FacInfoHistory"

            dgvInformationRequested.RowHeadersVisible = False
            dgvInformationRequested.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInformationRequested.AllowUserToResizeColumns = True
            dgvInformationRequested.AllowUserToAddRows = False
            dgvInformationRequested.AllowUserToDeleteRows = False
            dgvInformationRequested.AllowUserToOrderColumns = True
            dgvInformationRequested.AllowUserToResizeRows = True

            dgvInformationRequested.Columns("strRequestKey").HeaderText = "Request Key"
            dgvInformationRequested.Columns("strRequestKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvInformationRequested.Columns("strRequestKey").DisplayIndex = 0
            dgvInformationRequested.Columns("InformationRequested").HeaderText = "Date Requested"
            dgvInformationRequested.Columns("InformationRequested").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvInformationRequested.Columns("InformationRequested").DisplayIndex = 1
            dgvInformationRequested.Columns("strInformationrequested").HeaderText = "Requested Information"
            dgvInformationRequested.Columns("strInformationrequested").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvInformationRequested.Columns("strInformationrequested").DisplayIndex = 2
            dgvInformationRequested.Columns("InformationReceived").HeaderText = "Date Received"
            dgvInformationRequested.Columns("InformationReceived").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvInformationRequested.Columns("InformationReceived").DisplayIndex = 3
            dgvInformationRequested.Columns("strInformationReceived").HeaderText = "Received Information"
            dgvInformationRequested.Columns("strInformationReceived").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvInformationRequested.Columns("strInformationReceived").DisplayIndex = 4
            dgvInformationRequested.Columns("strApplicationNumber").HeaderText = "Application #"
            dgvInformationRequested.Columns("strApplicationNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvInformationRequested.Columns("strApplicationNumber").DisplayIndex = 5
            dgvInformationRequested.Columns("strApplicationNumber").Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Sub LoadSubPartData()
        Try
            Dim dtPart60 As New DataTable
            Dim dtPart61 As New DataTable
            Dim dtPart63 As New DataTable
            Dim dtSIP As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drDSRow4 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select * from AIRBRANCH.LookupSubPart60 order by strSubpart "
            SQL2 = "Select * from AIRBRANCH.LookupSubPart61 order by strSubpart "
            SQL3 = "Select * from AIRBRANCH.LookupSubPart63 order by strSubpart "
            SQL4 = "Select * from AIRBRANCH.LookUpSubPartSIP order by strSubPart "

            dsPart60 = New DataSet
            dsPart61 = New DataSet
            dsPart63 = New DataSet
            dsSIP = New DataSet

            daPart60 = New OracleDataAdapter(SQL, CurrentConnection)
            daPart61 = New OracleDataAdapter(SQL2, CurrentConnection)
            daPart63 = New OracleDataAdapter(SQL3, CurrentConnection)
            daSIP = New OracleDataAdapter(SQL4, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPart60.Fill(dsPart60, "Part60")
            daPart61.Fill(dsPart61, "Part61")
            daPart63.Fill(dsPart63, "Part63")
            daSIP.Fill(dsSIP, "SIP")

            dgvNSPS.DataSource = dsPart60
            dgvNSPS.DataMember = "Part60"
            dgvNESHAP.DataSource = dsPart61
            dgvNESHAP.DataMember = "Part61"
            dgvMACT.DataSource = dsPart63
            dgvMACT.DataMember = "Part63"
            dgvSIP.DataSource = dsSIP
            dgvSIP.DataMember = "SIP"

            dgvNSPS.RowHeadersVisible = False
            dgvNSPS.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPS.AllowUserToResizeColumns = True
            dgvNSPS.AllowUserToAddRows = False
            dgvNSPS.AllowUserToDeleteRows = False
            dgvNSPS.AllowUserToOrderColumns = True
            dgvNSPS.AllowUserToResizeRows = True
            dgvNSPS.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNSPS.Columns("strSubPart").DisplayIndex = 0
            dgvNSPS.Columns("strDescription").HeaderText = "Description"
            dgvNSPS.Columns("strdescription").DisplayIndex = 1
            dgvNSPS.Columns("strdescription").Width = 500

            dgvNESHAP.RowHeadersVisible = False
            dgvNESHAP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAP.AllowUserToResizeColumns = True
            dgvNESHAP.AllowUserToAddRows = False
            dgvNESHAP.AllowUserToDeleteRows = False
            dgvNESHAP.AllowUserToOrderColumns = True
            dgvNESHAP.AllowUserToResizeRows = True
            dgvNESHAP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNESHAP.Columns("strSubPart").DisplayIndex = 0
            dgvNESHAP.Columns("strDescription").HeaderText = "Description"
            dgvNESHAP.Columns("strdescription").DisplayIndex = 1
            dgvNESHAP.Columns("strdescription").Width = 500

            dgvMACT.RowHeadersVisible = False
            dgvMACT.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACT.AllowUserToResizeColumns = True
            dgvMACT.AllowUserToAddRows = False
            dgvMACT.AllowUserToDeleteRows = False
            dgvMACT.AllowUserToOrderColumns = True
            dgvMACT.AllowUserToResizeRows = True
            dgvMACT.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvMACT.Columns("strSubPart").DisplayIndex = 0
            dgvMACT.Columns("strDescription").HeaderText = "Description"
            dgvMACT.Columns("strdescription").DisplayIndex = 1
            dgvMACT.Columns("strdescription").Width = 500

            dgvSIP.RowHeadersVisible = False
            dgvSIP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIP.AllowUserToResizeColumns = True
            dgvSIP.AllowUserToAddRows = False
            dgvSIP.AllowUserToDeleteRows = False
            dgvSIP.AllowUserToOrderColumns = True
            dgvSIP.AllowUserToResizeRows = True
            dgvSIP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvSIP.Columns("strSubPart").DisplayIndex = 0
            dgvSIP.Columns("strDescription").HeaderText = "Description"
            dgvSIP.Columns("strdescription").DisplayIndex = 1
            dgvSIP.Columns("strdescription").Width = 500

            dtSIP.Columns.Add("strSubPart", GetType(System.String))
            dtSIP.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtSIP.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtSIP.Rows.Add(drNewRow)

            For Each drDSRow4 In dsSIP.Tables("SIP").Rows()
                drNewRow = dtSIP.NewRow()
                drNewRow("strSubPart") = drDSRow4("strSubPart")
                drNewRow("strDescription") = drDSRow4("strSubPart") & " - " & drDSRow4("strDescription")
                dtSIP.Rows.Add(drNewRow)
            Next

            With cboSIPSubpart
                .DataSource = dtSIP
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart60.Columns.Add("strSubPart", GetType(System.String))
            dtPart60.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart60.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart60.Rows.Add(drNewRow)

            For Each drDSRow In dsPart60.Tables("Part60").Rows()
                drNewRow = dtPart60.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("strDescription") = drDSRow("strSubPart") & " - " & drDSRow("strDescription")
                'drNewRow("strDescription") = drDSRow("strDescription")
                dtPart60.Rows.Add(drNewRow)
            Next

            With cboNSPSSubpart
                .DataSource = dtPart60
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart61.Columns.Add("strSubPart", GetType(System.String))
            dtPart61.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart61.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart61.Rows.Add(drNewRow)

            For Each drDSRow2 In dsPart61.Tables("Part61").Rows()
                drNewRow = dtPart61.NewRow()
                drNewRow("strSubPart") = drDSRow2("strSubPart")
                drNewRow("strDescription") = drDSRow2("strSubPart") & " - " & drDSRow2("strDescription")
                dtPart61.Rows.Add(drNewRow)
            Next

            With cboNESHAPSubpart
                .DataSource = dtPart61
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart63.Columns.Add("strSubPart", GetType(System.String))
            dtPart63.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart63.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart63.Rows.Add(drNewRow)

            For Each drDSRow3 In dsPart63.Tables("Part63").Rows()
                drNewRow = dtPart63.NewRow()
                drNewRow("strSubPart") = drDSRow3("strSubPart")
                drNewRow("strDescription") = drDSRow3("strSubPart") & " - " & drDSRow3("strDescription")
                dtPart63.Rows.Add(drNewRow)
            Next

            With cboMACTSubPart
                .DataSource = dtPart63
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub

#End Region

#Region "Subs and Functions"
    Sub LoadBasicFacilityInfo()
        Dim Facilityname As String = ""
        Dim FacilityStreet As String = ""
        Dim FacilityCity As String = ""
        Dim FacilityZipCode As String = ""
        Dim OperationalStatus As String = ""
        Dim OperationalStatusLine As String = ""
        Dim Classification As String = ""
        Dim ClassificationLine As String = ""
        Dim AirProgramCodes As String = ""
        Dim AirPrograms As String = ""
        Dim AirProgramCheck As String = ""
        Dim AirProgramLine As String = ""
        Dim SIC As String = ""
        Dim SICLine As String = ""
        Dim NAICS As String = ""
        Dim NAICSLine As String = "NAICS Code - "
        Dim CountyName As String = ""
        Dim OfficeName As String = ""
        Dim District As String = ""
        Dim Attainment As String = ""
        Dim AttainmentStatus As String = ""
        Dim StateProgramCodes As String = ""
        Dim StatePrograms As String = ""
        Dim PlantDesc As String = ""
        Dim PlantLine As String = ""
        Dim DistResponsible As String = "False"

        Try
            SQL = "Select " & _
            "strFacilityName, strFacilityStreet1, " & _
            "strFacilityCity, strFacilityZipCode, " & _
            "strOperationalStatus, strClass, " & _
            "strAirProgramCodes, strSICCode, " & _
            "strNAICSCode, " & _
            "strCountyName, strOfficeName, " & _
            "strDistrictName, " & _
            "strPlantDescription, " & _
            "AIRBRANCH.APBHeaderData.strAttainmentStatus, " & _
            "strStateProgramCodes, strDistrictResponsible " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBHeaderData, " & _
            "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.LooKUPDistrictOffice, " & _
            "AIRBRANCH.LookUpDistrictInformation, AIRBRANCH.LookUPDistricts, " & _
            "AIRBRANCH.APBSupplamentalData, AIRBRANCH.SSCPDistrictResponsible " & _
            "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            "and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
            "and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpDistrictInformation.strDistrictCounty " & _
            "and AIRBRANCH.LookUpDistrictinformation.strDistrictCode = AIRBRANCH.LooKUPDistrictOffice.strDistrictCode " & _
            "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUPDistricts.strDistrictCode " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCPDistrictResponsible.strAIRSnumber (+) " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    Facilityname = "N/A"
                Else
                    Facilityname = dr.Item("strFacilityname")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    FacilityStreet = "N/A"
                Else
                    FacilityStreet = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacilityCity = "N/A"
                Else
                    FacilityCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    FacilityZipCode = "N/A"
                Else
                    FacilityZipCode = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    OperationalStatus = "N/A"
                Else
                    OperationalStatus = dr.Item("strOperationalStatus")
                    OperationalStatusLine = "Operating Status - " & OperationalStatus
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = "N/A"
                Else
                    Classification = dr.Item("strClass")
                    ClassificationLine = "Classification - " & Classification
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    AirProgramCodes = "000000000000000"
                Else
                    AirProgramCodes = dr.Item("strAirProgramCodes")
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    SIC = "N/A"
                Else
                    SIC = dr.Item("strSICCode")
                    SICLine = "SIC Code - " & SIC
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    NAICS = "N/A"
                Else
                    NAICS = dr.Item("strNAICSCode")
                    NAICSLine = "NAICS Code - " & NAICS
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    CountyName = "N/A"
                Else
                    CountyName = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strOfficeName")) Then
                    OfficeName = "N/A"
                Else
                    OfficeName = dr.Item("strOfficeName")
                End If
                If IsDBNull(dr.Item("strDistrictName")) Then
                    District = "N/A"
                Else
                    District = dr.Item("strDistrictName")
                End If
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    Attainment = "00000"
                Else
                    Attainment = dr.Item("strAttainmentstatus")
                End If
                If IsDBNull(dr.Item("strStateProgramCodes")) Then
                    StateProgramCodes = "00000"
                Else
                    StateProgramCodes = dr.Item("strStateProgramCodes")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    PlantDesc = "N/A"
                Else
                    PlantDesc = dr.Item("strPlantDescription")
                End If
                PlantLine = "Plant Description - " & PlantDesc
                If IsDBNull(dr.Item("strDistrictResponsible")) Then
                    DistResponsible = "False"
                Else
                    DistResponsible = dr.Item("strDistrictResponsible")
                End If
            Else
                Facilityname = "N/A"
                FacilityStreet = "N/A"
                FacilityCity = "N/A"
                FacilityZipCode = "N/A"
                OperationalStatus = "N/A"
                OperationalStatusLine = "Operating Status - "
                Classification = "N/A"
                ClassificationLine = "Classification - "
                AirProgramCodes = "000000000000000"
                SIC = "N/A"
                SICLine = "SIC Code - "
                NAICS = "N/A"
                NAICSLine = "NAICS Code - "
                CountyName = "N/A"
                OfficeName = "N/A"
                District = "N/A"
                Attainment = "00000"
                StateProgramCodes = "00000"
                PlantDesc = "N/A"
                PlantLine = "Plant Description - "
                DistResponsible = "False"
            End If

            If Mid(AirProgramCodes, 1, 1) = 1 Then
                AirPrograms = "   0 - SIP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 2, 1) = 1 Then
                AirPrograms = AirPrograms & "   1 - Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCodes, 3, 1) = 1 Then
                AirPrograms = AirPrograms & "   3 - Non-Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCodes, 4, 1) = 1 Then
                AirPrograms = AirPrograms & "   4 - CFC Tracking" & vbCrLf
            End If
            If Mid(AirProgramCodes, 5, 1) = 1 Then
                AirPrograms = AirPrograms & "   6 - PSD" & vbCrLf
            End If
            If Mid(AirProgramCodes, 6, 1) = 1 Then
                AirPrograms = AirPrograms & "   7 - NSR" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 7, 1) = 1 Then
                AirPrograms = AirPrograms & "   8 - NESHAP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 8, 1) = 1 Then
                AirPrograms = AirPrograms & "   9 - NSPS" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 9, 1) = 1 Then
                AirPrograms = AirPrograms & "   F - FESOP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 10, 1) = 1 Then
                AirPrograms = AirPrograms & "   A - Acid Precipitation" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 11, 1) = 1 Then
                AirPrograms = AirPrograms & "   I - Native American" & vbCrLf
            End If
            If Mid(AirProgramCodes, 12, 1) = 1 Then
                AirPrograms = AirPrograms & "   M - MACT" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 13, 1) = 1 Then
                AirPrograms = AirPrograms & "   V - Title V Permit" & vbCrLf
            Else
            End If
            AirProgramLine = "Air Program(s) - " & vbCrLf & AirPrograms

            Select Case Mid(Attainment, 2, 1)
                Case 0
                    AttainmentStatus = ""
                Case 1
                    AttainmentStatus = "   1-hr Ozone"
                Case 2
                    AttainmentStatus = "   1-hr Ozone (Contributing)"
                Case Else
                    AttainmentStatus = ""
            End Select
            Select Case Mid(Attainment, 3, 1)
                Case 0
                    AttainmentStatus = AttainmentStatus & ""
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "8-hr Atlanta"
                    Else
                        AttainmentStatus = "   8-hr Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "8-hr Macon"
                    Else
                        AttainmentStatus = "   8-hr Macon"
                    End If
                Case Else
                    AttainmentStatus = AttainmentStatus & ""
            End Select
            Select Case Mid(Attainment, 4, 1)
                Case 0
                    AttainmentStatus = AttainmentStatus & ""
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Atlanta"
                    Else
                        AttainmentStatus = "   PM 2.5 Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5  Chattanooga"
                    Else
                        AttainmentStatus = "   PM 2.5  Chattanooga"
                    End If
                Case 3
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Floyd"
                    Else
                        AttainmentStatus = "   PM 2.5 Floyd"
                    End If
                Case 4
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Macon"
                    Else
                        AttainmentStatus = "   PM 2.5 Macon"
                    End If
                Case Else
                    AttainmentStatus = AttainmentStatus & ""
            End Select

            If AttainmentStatus = "" Then
                AttainmentStatus = "Non Attainment Area - N/A"
            Else
                AttainmentStatus = "Non Attainment Area - " & vbCrLf & AttainmentStatus
            End If

            Select Case Mid(StateProgramCodes, 1, 1)
                Case 0
                    StatePrograms = ""
                Case 1
                    StatePrograms = "   NSR/PSD"
                Case Else
                    StatePrograms = ""
            End Select
            Select Case Mid(StateProgramCodes, 2, 1)
                Case 0
                    StatePrograms = StatePrograms & ""
                Case 1
                    If StatePrograms <> "" Then
                        StatePrograms = StatePrograms & "HAPs Major"
                    Else
                        StatePrograms = "   HAPs Major"
                    End If
                Case Else
                    StatePrograms = StatePrograms & ""
            End Select
            If StatePrograms = "" Then
                StatePrograms = "State Codes - N/A"
            Else
                StatePrograms = "State Codes - " & vbCrLf & StatePrograms
            End If

            rtbFacilityInformation.Clear()
            rtbFacilityInformation.Text = "AIRS # - " & txtAIRSNumber.Text & vbCrLf & vbCrLf & _
            Facilityname & vbCrLf & _
            FacilityStreet & vbCrLf & _
            FacilityCity & ", GA " & FacilityZipCode & vbCrLf & vbCrLf & _
            OperationalStatusLine & vbCrLf & _
            ClassificationLine & vbCrLf & _
            SICLine & vbCrLf & _
            NAICSLine & vbCrLf & _
            AirProgramLine & _
            StatePrograms & vbCrLf & _
            "County - " & CountyName & vbCrLf & _
            "District - " & District & vbCrLf & _
            "District Office - " & OfficeName & vbCrLf & _
            AttainmentStatus & vbCrLf & vbCrLf & _
            PlantLine

            cboCounty.SelectedIndex = cboCounty.FindString(CountyName)
            txtDistrict.Text = District
            txtOffice.Text = OfficeName

            If txtFacilityName.Text = "" Then
                txtFacilityName.Text = Facilityname
                txtFacilityStreetAddress.Text = FacilityStreet
                cboFacilityCity.SelectedIndex = cboFacilityCity.FindString(FacilityCity)
                txtFacilityZipCode.Text = FacilityZipCode
                txtSICCode.Text = SIC
                txtNAICSCode.Text = NAICS
                Select Case OperationalStatus
                    Case "O"
                        cboOperationalStatus.Text = "O - Operating"
                    Case "P"
                        cboOperationalStatus.Text = "P - Planned"
                    Case "C"
                        cboOperationalStatus.Text = "C - Under Construction"
                    Case "T"
                        cboOperationalStatus.Text = "T - Temporarily Closed"
                    Case "X"
                        cboOperationalStatus.Text = "X - Permanently Closed"
                    Case "I"
                        cboOperationalStatus.Text = "I - Seasonal Operation"
                    Case Else
                        cboOperationalStatus.Text = ""
                End Select
                Select Case Classification
                    Case "A"
                        cboClassification.Text = "A - MAJOR"
                    Case "B"
                        cboClassification.Text = "B - MINOR"
                    Case "C"
                        cboClassification.Text = "C - UNKNOWN"
                    Case "SM"
                        cboClassification.Text = "SM - SYNTHETIC MINOR"
                    Case "PR"
                        cboClassification.Text = "PR - PERMIT BY RULE"
                        'Case "U"
                        '   cboClassification.Text = "U - UNDEFINED"
                    Case Else
                        cboClassification.Text = ""
                End Select
                txtPlantDescription.Text = PlantDesc

                If Mid(AirProgramCodes, 1, 1) = 1 Then
                    chbCDS_0.Checked = True
                Else
                    chbCDS_0.Checked = True
                End If
                If Mid(AirProgramCodes, 5, 1) = 1 Then
                    chbCDS_6.Checked = True
                Else
                    chbCDS_6.Checked = False
                End If
                If Mid(AirProgramCodes, 6, 1) = 1 Then
                    chbCDS_7.Checked = True
                Else
                    chbCDS_7.Checked = False
                End If
                If Mid(AirProgramCodes, 7, 1) = 1 Then
                    chbCDS_8.Checked = True
                Else
                    chbCDS_8.Checked = False
                End If
                If Mid(AirProgramCodes, 8, 1) = 1 Then
                    chbCDS_9.Checked = True
                Else
                    chbCDS_9.Checked = False
                End If
                If Mid(AirProgramCodes, 10, 1) = 1 Then
                    chbCDS_A.Checked = True
                Else
                    chbCDS_A.Checked = False
                End If
                If Mid(AirProgramCodes, 12, 1) = 1 Then
                    chbCDS_M.Checked = True
                Else
                    chbCDS_M.Checked = False
                End If
                If Mid(AirProgramCodes, 13, 1) = 1 Then
                    chbCDS_V.Checked = True
                Else
                    chbCDS_V.Checked = False
                End If
                If Mid(AirProgramCodes, 14, 1) = 1 Then
                    chbCDS_RMP.Checked = True
                Else
                    chbCDS_RMP.Checked = False
                End If

                Select Case Mid(Attainment, 2, 1)
                    Case 0
                        txt1HourOzone.Text = "No"
                    Case 1
                        txt1HourOzone.Text = "Yes"
                    Case 2
                        txt1HourOzone.Text = "Contributing"
                    Case Else
                        txt1HourOzone.Text = "No"
                End Select
                Select Case Mid(Attainment, 3, 1)
                    Case 0
                        txt8HROzone.Text = "No"
                    Case 1
                        txt8HROzone.Text = "Atlanta"
                    Case 2
                        txt8HROzone.Text = "Macon"
                    Case Else
                        txt8HROzone.Text = "No"
                End Select
                Select Case Mid(Attainment, 4, 1)
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
                Select Case Mid(StateProgramCodes, 1, 1)
                    Case 0
                        chbNSRMajor.Checked = False
                    Case 1
                        chbNSRMajor.Checked = True
                    Case Else
                        chbNSRMajor.Checked = False
                End Select
                Select Case Mid(StateProgramCodes, 2, 1)
                    Case 0
                        chbHAPsMajor.Checked = False
                    Case 1
                        chbHAPsMajor.Checked = True
                    Case Else
                        chbHAPsMajor.Checked = False
                End Select
            Else
                If txtFacilityName.Text <> Facilityname Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(Facilityname)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtFacilityStreetAddress.Text <> FacilityStreet Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityStreet)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If cboFacilityCity.Text <> FacilityCity Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityCity)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtFacilityZipCode.Text <> FacilityZipCode Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityZipCode)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtSICCode.Text <> SIC Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(SICLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtNAICSCode.Text <> NAICS Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(NAICSLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If Mid(cboOperationalStatus.Text, 1, 1) <> OperationalStatus Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(OperationalStatusLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If Mid(cboClassification.Text, 1, 1) <> Mid(Classification, 1, 1) Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(ClassificationLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtPlantDescription.Text <> PlantDesc Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(PlantLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                AirProgramCheck = "000000000000000"
                If chbCDS_0.Checked = True Then
                    AirProgramCheck = "1" & Mid(AirProgramCheck, 2)
                End If
                If chbCDS_6.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 4) & "1" & Mid(AirProgramCheck, 6)
                End If
                If chbCDS_7.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 5) & "1" & Mid(AirProgramCheck, 7)
                End If
                If chbCDS_8.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 6) & "1" & Mid(AirProgramCheck, 8)
                End If
                If chbCDS_9.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 7) & "1" & Mid(AirProgramCheck, 9)
                End If
                If chbCDS_A.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 9) & "1" & Mid(AirProgramCheck, 11)
                End If
                If chbCDS_M.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 11) & "1" & Mid(AirProgramCheck, 13)
                End If
                If chbCDS_V.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 12) & "1" & Mid(AirProgramCheck, 14)
                End If
                If chbCDS_RMP.Checked = True Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 13) & "1" & Mid(AirProgramCheck, 15)
                End If

                AirProgramCodes = Mid(AirProgramCodes, 1, 1) & "000" & Mid(AirProgramCodes, 5, 4) & "0" & Mid(AirProgramCodes, 10, 1) & "0" & Mid(AirProgramCodes, 12, 2) & "00"

                If AirProgramCheck <> AirProgramCodes Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Air Program(s) - ")
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If

                If txt1HourOzone.Text = "No" Then
                    If Mid(Attainment, 2, 1) <> 0 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                Else
                    If txt1HourOzone.Text = "Yes" Then
                        If Mid(Attainment, 2, 1) <> 1 Then
                            rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                            rtbFacilityInformation.SelectionColor = Color.Tomato
                        Else
                            If txt1HourOzone.Text = "Contributing" Then
                                If Mid(Attainment, 2, 1) <> 2 Then
                                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                    rtbFacilityInformation.SelectionColor = Color.Tomato
                                End If
                            End If
                        End If
                    End If
                End If
                If txt8HROzone.Text = "No" Then
                    If Mid(Attainment, 3, 1) <> 0 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                Else
                    If txt8HROzone.Text = "Atlanta" Then
                        If Mid(Attainment, 3, 1) <> 1 Then
                            rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                            rtbFacilityInformation.SelectionColor = Color.Tomato
                        End If
                    Else
                        If txt8HROzone.Text = "Macon" Then
                            If Mid(Attainment, 3, 1) <> 2 Then
                                rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                rtbFacilityInformation.SelectionColor = Color.Tomato
                            End If
                        End If
                    End If
                End If
                If txtPM.Text = "No" Then
                    If Mid(Attainment, 4, 1) <> 0 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                Else
                    If txtPM.Text = "Atlanta" Then
                        If Mid(Attainment, 4, 1) <> 1 Then
                            rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                            rtbFacilityInformation.SelectionColor = Color.Tomato
                        End If
                    Else
                        If txtPM.Text = "Chattanooga" Then
                            If Mid(Attainment, 4, 1) <> 2 Then
                                rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                rtbFacilityInformation.SelectionColor = Color.Tomato
                            End If
                        Else
                            If txtPM.Text = "Floyd" Then
                                If Mid(Attainment, 4, 1) <> 3 Then
                                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                    rtbFacilityInformation.SelectionColor = Color.Tomato
                                End If
                            Else
                                If txtPM.Text = "Macon" Then
                                    If Mid(Attainment, 4, 1) <> 4 Then
                                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                        rtbFacilityInformation.SelectionColor = Color.Tomato
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If chbNSRMajor.Checked = False Then
                    If Mid(StateProgramCodes, 1, 1) <> 0 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("State Codes - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                Else
                    If Mid(StateProgramCodes, 1, 1) <> 1 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("State Codes - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                End If
                If chbHAPsMajor.Checked = False Then
                    If Mid(StateProgramCodes, 2, 1) <> 0 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("State Codes - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                Else
                    If Mid(StateProgramCodes, 2, 1) <> 1 Then
                        rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("State Codes - ")
                        rtbFacilityInformation.SelectionColor = Color.Tomato
                    End If
                End If

            End If

            If DistResponsible = "False" Then
                txtDistrict.BackColor = Color.LightGray
            Else
                txtDistrict.BackColor = Color.Tomato
            End If
        Catch ex As Exception
            ErrorReport(ex, txtAIRSNumber.Text, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadMiscData()
        Try

            Dim Attainment As String = ""

            If txtAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strAttainmentStatus " & _
                "from AIRBRANCH.APBHeaderData " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        Attainment = "00000"
                    Else
                        Attainment = dr.Item("strAttainmentstatus")
                    End If
                Else
                    Attainment = "00000"
                End If
                dr.Close()
            End If

            Select Case Mid(Attainment, 2, 1)
                Case 0
                    txt1HourOzone.Text = "No"
                Case 1
                    txt1HourOzone.Text = "Yes"
                Case 2
                    txt1HourOzone.Text = "Contributing"
                Case Else
                    txt1HourOzone.Text = "No"
            End Select
            Select Case Mid(Attainment, 3, 1)
                Case 0
                    txt8HROzone.Text = "No"
                Case 1
                    txt8HROzone.Text = "Atlanta"
                Case 2
                    txt8HROzone.Text = "Macon"
                Case Else
                    txt8HROzone.Text = "No"
            End Select
            Select Case Mid(Attainment, 4, 1)
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadContactData()
        Try
            SQL = "Select strApplicationNumber " & _
            "From AIRBRANCH.SSPPApplicationContact " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " & _
                "strContactFirstName, " & _
                "strContactLastName, " & _
                "strContactPrefix, " & _
                "strContactSuffix, " & _
                "strContactTitle, " & _
                "strContactCompanyName, " & _
                "strContactPhoneNumber1, " & _
                "strContactFaxNumber, " & _
                "strContactEmail, " & _
                "strContactAddress1, " & _
                "strContactCity, " & _
                "strContactState, " & _
                "strContactZipCode, " & _
                "strContactDescription " & _
                "from AIRBRANCH.SSPPApplicationContact " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strContactFirstname")) Then
                        txtContactFirstName.Clear()
                    Else
                        txtContactFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtContactLastName.Clear()
                    Else
                        txtContactLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtContactSocialTitle.Clear()
                    Else
                        txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtContactPedigree.Clear()
                    Else
                        txtContactPedigree.Text = dr.Item("strContactSuffix")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtContactTitle.Clear()
                    Else
                        txtContactTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtContactCompanyName.Clear()
                    Else
                        txtContactCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                        mtbContactPhoneNumber.Clear()
                    Else
                        temp = dr.Item("strContactPhoneNumber1")
                        mtbContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
                    End If
                    If IsDBNull(dr.Item("strContactFaxNumber")) Then
                        mtbContactFaxNumber.Clear()
                    Else
                        mtbContactFaxNumber.Text = dr.Item("strContactFaxNumber")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtContactEmailAddress.Clear()
                    Else
                        txtContactEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtContactStreetAddress.Clear()
                    Else
                        txtContactStreetAddress.Text = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtContactCity.Clear()
                    Else
                        txtContactCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        txtContactState.Clear()
                    Else
                        txtContactState.Text = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        mtbContactZipCode.Clear()
                    Else
                        mtbContactZipCode.Text = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactDescription")) Then
                        txtContactDescription.Clear()
                    Else
                        txtContactDescription.Text = dr.Item("strContactDescription")
                    End If
                End While
            Else
                '30
                SQL = "Select " & _
                "strContactFirstName, " & _
                "strContactLastName, " & _
                "strContactPrefix, " & _
                "strContactSuffix, " & _
                "strContactTitle, " & _
                "strContactCompanyName, " & _
                "strContactPhoneNumber1, " & _
                "strContactFaxNumber, " & _
                "strContactEmail, " & _
                "strContactAddress1, " & _
                "strContactCity, " & _
                "strContactState, " & _
                "strContactZipCode, " & _
                "strContactDescription " & _
                "from AIRBRANCH.APBContactInformation " & _
                "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadApplicationData()
        Dim AIRSNumber As String = ""
        Dim CloseOut As String = ""
        Dim temp As String = ""

        Try


            txtAIRSNumber.Clear()

            SQL = "Select " & _
            "strApplicationNumber " & _
            "from AIRBRANCH.SSPPApplicationMaster " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

            temp = txtApplicationNumber.Text

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " & _
                "datModifingdate " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("datModifingdate")) Then
                        TimeStamp = ""
                    Else
                        TimeStamp = dr.Item("datModifingdate")
                    End If
                End While
                dr.Close()

                If TCApplicationTrackingLog.TabPages.Contains(TPTrackingLog) Then
                    SQL = "Select " & _
                    "strAIRSNumber, strStaffResponsible,  " & _
                    "strApplicationType, strPermitType,  " & _
                    "APBUnit, datFinalizedDate,  " & _
                    "strFacilityName, strFacilityStreet1,  " & _
                    "strFacilityCity, strFacilityZipCode,  " & _
                    "strOperationalStatus, strClass,  " & _
                    "strAirProgramCodes, strSICCode,  " & _
                    "strNAICSCode, " & _
                    "strPermitNumber, strPlantDescription,  " & _
                    "AIRBRANCH.SSPPApplicationData.strComments as DataComments,  " & _
                    "strApplicationNotes, " & _
                    "datFinalizedDate, " & _
                    "strStateProgramCodes,  " & _
                    "datReceivedDate, datSentByFacility,  " & _
                    "datAssignedToEngineer, datReassignedToEngineer,  " & _
                    "datAcknowledgementLetterSent, strPublicInvolvement,  " & _
                    "datToPMI, datToPMII,  " & _
                    "datReturnedToEngineer, datPermitIssued,  " & _
                    "datApplicationDeadline, datDraftIssued,  " & _
                    "strPAReady,  " & _
                    "strPNReady, datEPAWaived,  " & _
                    "datEPAEnds, datToBranchCheif,  " & _
                    "datToDirector, " & _
                    "datPAExpires, datPNExpires, " & _
                    "strStateprogramcodes, " & _
                    "strTrackedRules, STRSIGNIFICANTCOMMENTS, " & _
                    "strPAPosted, strPNPosted " & _
                    "from  " & _
                    "AIRBRANCH.SSPPApplicationMaster,  " & _
                    "AIRBRANCH.SSPPApplicationTracking,  " & _
                    "AIRBRANCH.SSPPApplicationData " & _
                    "where " & _
                    "    AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+)  " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+)  " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("strStaffResponsible")) Then
                            cboEngineer.SelectedIndex = 0
                        Else
                            cboEngineer.SelectedValue = dr.Item("strStaffResponsible")
                        End If
                        If IsDBNull(dr.Item("strTrackedRules")) Then
                            chbPSD.Checked = False
                            chbNAANSR.Checked = False
                            chb112g.Checked = False
                            chbRulett.Checked = False
                            chbRuleyy.Checked = False
                            chbPal.Checked = False
                            chbExpedited.Checked = False
                            chbConfidential.Checked = False
                        Else
                            If Mid(dr.Item("strTrackedRules"), 1, 1) = "0" Then
                                chbPSD.Checked = False
                            Else
                                chbPSD.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 2, 1) = "0" Then
                                chbNAANSR.Checked = False
                            Else
                                chbNAANSR.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 3, 1) = "0" Then
                                chb112g.Checked = False
                            Else
                                chb112g.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 4, 1) = "0" Then
                                chbRulett.Checked = False
                            Else
                                chbRulett.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 5, 1) = "0" Then
                                chbRuleyy.Checked = False
                            Else
                                chbRuleyy.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 6, 1) = "0" Then
                                chbPal.Checked = False
                            Else
                                chbPal.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 7, 1) = "0" Then
                                chbExpedited.Checked = False
                            Else
                                chbExpedited.Checked = True
                            End If
                            If Mid(dr.Item("strTrackedRules"), 8, 1) = "0" Then
                                chbConfidential.Checked = False
                            Else
                                chbConfidential.Checked = True
                            End If
                        End If
                        If IsDBNull(dr.Item("strApplicationType")) Then
                            cboApplicationType.SelectedIndex = 0
                        Else
                            cboApplicationType.SelectedValue = dr.Item("strApplicationType")
                            If cboApplicationType.SelectedValue Is Nothing Then
                                temp = dr.Item("strApplicationType")
                                Select Case temp
                                    Case 1
                                        cboApplicationType.Text = "112(g)"
                                    Case 5
                                        cboApplicationType.Text = "FESOP"
                                    Case 6
                                        cboApplicationType.Text = "LTR"
                                    Case 7
                                        cboApplicationType.Text = "NPR"
                                    Case 10
                                        cboApplicationType.Text = "PSD"
                                    Case 13
                                        cboApplicationType.Text = "SM(TV)"
                                    Case 17
                                        cboApplicationType.Text = "TV-Amend"
                                    Case 18
                                        cboApplicationType.Text = "SLSM"
                                    Case 23
                                        cboApplicationType.Text = "SM80"
                                    Case 24
                                        cboApplicationType.Text = "PCP"
                                    Case 27
                                        cboApplicationType.Text = "Title V"
                                End Select
                            End If
                        End If
                        If IsDBNull(dr.Item("strPermitType")) Then
                            cboPermitAction.SelectedIndex = 0
                        Else
                            cboPermitAction.SelectedValue = dr.Item("strPermitType")
                            If cboPermitAction.SelectedValue Is Nothing Then
                                temp = dr.Item("strPermitType")
                                Select Case temp
                                    Case 1
                                        cboPermitAction.Text = "Amendment"
                                    Case 3
                                        cboPermitAction.Text = "Draft"
                                    Case 4
                                        cboPermitAction.Text = "New Permit"
                                    Case 8
                                        cboPermitAction.Text = "PRMT-DNL"
                                    Case 10
                                        cboPermitAction.Text = "Revoked"
                                    Case 12
                                        cboPermitAction.Text = "Initial Title V Permit"
                                    Case 13
                                        cboPermitAction.Text = "Renewal Title V Permit"
                                End Select
                            End If
                        End If
                        If IsDBNull(dr.Item("APBUnit")) Then
                            cboApplicationUnit.SelectedIndex = 0
                        Else
                            cboApplicationUnit.SelectedValue = dr.Item("APBUnit")
                        End If
                        If IsDBNull(dr.Item("datFinalizedDate")) Then
                            DTPFinalizedDate.Text = OracleDate
                            chbClosedOut.Checked = False
                        Else
                            DTPFinalizedDate.Text = dr.Item("datFinalizedDate")
                            chbClosedOut.Checked = True
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            txtFacilityName.Clear()
                        Else
                            txtFacilityName.Text = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet1")) Then
                            txtFacilityStreetAddress.Clear()
                        Else
                            txtFacilityStreetAddress.Text = dr.Item("strFacilityStreet1")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            cboFacilityCity.SelectedIndex = 0
                        Else
                            cboFacilityCity.SelectedIndex = cboFacilityCity.FindString(dr.Item("strFacilityCity"))
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode")) Then
                            txtFacilityZipCode.Clear()
                        Else
                            txtFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                        End If
                        If IsDBNull(dr.Item("strOperationalStatus")) Then
                            cboOperationalStatus.Text = ""
                        Else
                            temp = dr.Item("strOperationalStatus")
                            Select Case temp
                                Case "O"
                                    cboOperationalStatus.Text = "O - Operating"
                                Case "P"
                                    cboOperationalStatus.Text = "P - Planned"
                                Case "C"
                                    cboOperationalStatus.Text = "C - Under Construction"
                                Case "T"
                                    cboOperationalStatus.Text = "T - Temporarily Closed"
                                Case "X"
                                    cboOperationalStatus.Text = "X - Permanently Closed"
                                Case "I"
                                    cboOperationalStatus.Text = "I - Seasonal Operation"
                                Case Else
                                    cboOperationalStatus.Text = ""
                            End Select
                        End If
                        If IsDBNull(dr.Item("strClass")) Then
                            cboClassification.Text = ""
                        Else
                            temp = dr.Item("strClass")
                            Select Case temp
                                Case "A"
                                    cboClassification.Text = "A - MAJOR"
                                Case "B"
                                    cboClassification.Text = "B - MINOR"
                                Case "C"
                                    cboClassification.Text = "C - UNKNOWN"
                                Case "SM"
                                    cboClassification.Text = "SM - SYNTHETIC MINOR"
                                Case "PR"
                                    cboClassification.Text = "PR - PERMIT BY RULE"
                                Case "U"
                                    cboClassification.Text = "U - UNDEFINED"
                                Case Else
                                    cboClassification.Text = ""
                            End Select
                        End If
                        If IsDBNull(dr.Item("strAirProgramCodes")) Then
                            chbCDS_0.Checked = True
                            chbCDS_6.Checked = False
                            chbCDS_7.Checked = False
                            chbCDS_8.Checked = False
                            chbCDS_9.Checked = False
                            chbCDS_A.Checked = False
                            chbCDS_M.Checked = False
                            chbCDS_V.Checked = False
                            chbCDS_RMP.Checked = False
                        Else
                            If Mid(dr.Item("strAirProgramCodes"), 1, 1) = 1 Then
                                chbCDS_0.Checked = True
                            Else
                                chbCDS_0.Checked = True
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 5, 1) = 1 Then
                                chbCDS_6.Checked = True
                            Else
                                chbCDS_6.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 6, 1) = 1 Then
                                chbCDS_7.Checked = True
                            Else
                                chbCDS_7.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 7, 1) = 1 Then
                                chbCDS_8.Checked = True
                            Else
                                chbCDS_8.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 8, 1) = 1 Then
                                chbCDS_9.Checked = True
                            Else
                                chbCDS_9.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 10, 1) = 1 Then
                                chbCDS_A.Checked = True
                            Else
                                chbCDS_A.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 12, 1) = 1 Then
                                chbCDS_M.Checked = True
                            Else
                                chbCDS_M.Checked = False
                            End If
                            If Mid(dr.Item("strAirProgramCodes"), 13, 1) = 1 Then
                                chbCDS_V.Checked = True
                            Else
                                chbCDS_V.Checked = False
                            End If
                            If Mid(dr.Item("strAIRProgramCodes"), 14, 1) = 1 Then
                                chbCDS_RMP.Checked = True
                            Else
                                chbCDS_RMP.Checked = False
                            End If
                        End If
                        If IsDBNull(dr.Item("strSICCode")) Then
                            txtSICCode.Clear()
                        Else
                            txtSICCode.Text = dr.Item("strSICCode")
                        End If
                        If IsDBNull(dr.Item("strNAICSCode")) Then
                            txtNAICSCode.Clear()
                        Else
                            txtNAICSCode.Text = dr.Item("strNAICSCode")
                        End If
                        If IsDBNull(dr.Item("strPermitNumber")) Then
                            txtPermitNumber.Clear()
                        Else
                            If cboApplicationType.Text = "ERC" Then
                                Select Case Len(dr.Item("strPermitNumber"))
                                    Case Is <= 3
                                        txtPermitNumber.Text = "ERC"
                                    Case Is > 8
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 3) & "-" & Mid(dr.Item("strPermitNumber"), 4, 5) & "-" & Mid(dr.Item("strPermitNumber"), 9)
                                    Case Is > 3 <= 8
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 3) & "-" & Mid(dr.Item("strPermitNumber"), 4)

                                    Case Else
                                        txtPermitNumber.Text = dr.Item("strPermitNumber")
                                End Select
                            Else
                                Select Case Len(dr.Item("strPermitNumber"))
                                    Case 15
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5, 3) & _
                                        "-" & Mid(dr.Item("strPermitNumber"), 8, 4) & "-" & Mid(dr.Item("strPermitNumber"), 12, 1) & _
                                        "-" & Mid(dr.Item("strPermitNumber"), 13, 2) & "-" & Mid(dr.Item("strPermitNumber"), 15, 1)
                                    Case Is >= 13 = 14
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5, 3) _
                                        & "-" & Mid(dr.Item("strPermitNumber"), 8, 4) & "-" & Mid(dr.Item("strPermitNumber"), 12, 1) _
                                        & "-" & Mid(dr.Item("strPermitNumber"), 13)
                                    Case 12
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5, 3) _
                                        & "-" & Mid(dr.Item("strPermitNumber"), 8, 4) & "-" & Mid(dr.Item("strPermitNumber"), 12, 1)
                                    Case Is >= 8 <= 11
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5, 3) _
                                        & "-" & Mid(dr.Item("strPermitNumber"), 8)
                                    Case Is >= 5 <= 7
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5)
                                    Case Is <= 4
                                        txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1)
                                    Case Else
                                        txtPermitNumber.Text = dr.Item("strPermitNumber")
                                End Select
                            End If
                        End If
                        If IsDBNull(dr.Item("strPlantDescription")) Then
                            txtPlantDescription.Clear()
                        Else
                            txtPlantDescription.Text = dr.Item("strPlantDescription")
                        End If
                        If IsDBNull(dr.Item("DataComments")) Then
                            txtComments.Clear()
                        Else
                            txtComments.Text = dr.Item("DataComments")
                        End If
                        If IsDBNull(dr.Item("strApplicationNotes")) Then
                            txtReasonAppSubmitted.Clear()
                        Else
                            txtReasonAppSubmitted.Text = dr.Item("strApplicationNotes")
                        End If

                        If IsDBNull(dr.Item("datFinalizedDate")) Then
                            CloseOut = "False"
                        Else
                            CloseOut = "True"
                        End If
                        If IsDBNull(dr.Item("datReceivedDate")) Then
                            DTPDateReceived.Text = OracleDate
                        Else
                            DTPDateReceived.Text = dr.Item("datReceivedDate")
                        End If
                        If IsDBNull(dr.Item("datSentByFacility")) Then
                            DTPDateSent.Text = OracleDate
                        Else
                            DTPDateSent.Text = dr.Item("datSentByFacility")
                        End If
                        If IsDBNull(dr.Item("datAssignedToEngineer")) Then
                            DTPDateAssigned.Text = OracleDate
                            DTPDateAssigned.Checked = False
                        Else
                            DTPDateAssigned.Text = dr.Item("datAssignedToEngineer")
                            DTPDateAssigned.Checked = True
                        End If
                        If IsDBNull(dr.Item("datReassignedToEngineer")) Then
                            DTPDateReassigned.Text = OracleDate
                            DTPDateReassigned.Checked = False
                        Else
                            DTPDateReassigned.Text = dr.Item("datReassignedToEngineer")
                            DTPDateReassigned.Checked = True
                        End If

                        If IsDBNull(dr.Item("datAcknowledgementLetterSent")) Then
                            DTPDateAcknowledge.Text = OracleDate
                            DTPDateAcknowledge.Checked = False
                        Else
                            DTPDateAcknowledge.Text = dr.Item("datAcknowledgementLetterSent")
                            DTPDateAcknowledge.Checked = True
                        End If
                        If IsDBNull(dr.Item("strPublicInvolvement")) Then
                            cboPublicAdvisory.Text = ""
                        Else
                            temp = dr.Item("strPublicInvolvement")
                            Select Case temp
                                Case "0"
                                    cboPublicAdvisory.Text = "Not Decided"
                                Case "1"
                                    cboPublicAdvisory.Text = "PA Needed"
                                Case "2"
                                    cboPublicAdvisory.Text = "PA Not Needed"
                                Case Else
                                    cboPublicAdvisory.Text = "Not Decided"
                            End Select
                        End If
                        If IsDBNull(dr.Item("datToPMI")) Then
                            DTPDateToUC.Text = OracleDate
                            DTPDateToUC.Checked = False
                        Else
                            DTPDateToUC.Text = dr.Item("datToPMI")
                            DTPDateToUC.Checked = True
                        End If
                        If IsDBNull(dr.Item("datToPMII")) Then
                            DTPDateToPM.Text = OracleDate
                            DTPDateToPM.Checked = False
                        Else
                            DTPDateToPM.Text = dr.Item("datToPMII")
                            DTPDateToPM.Checked = True
                        End If
                        If IsDBNull(dr.Item("datPermitIssued")) Then
                            DTPFinalAction.Text = OracleDate
                            DTPFinalAction.Checked = False
                        Else
                            DTPFinalAction.Text = dr.Item("datPermitIssued")
                            DTPFinalAction.Checked = True
                        End If
                        If IsDBNull(dr.Item("datApplicationDeadLine")) Then
                            DTPDeadline.Text = OracleDate
                            DTPDeadline.Checked = False
                        Else
                            DTPDeadline.Text = dr.Item("datApplicationDeadline")
                            DTPDeadline.Checked = True
                        End If
                        If IsDBNull(dr.Item("datDraftIssued")) Then
                            DTPDraftIssued.Text = OracleDate
                            DTPDraftIssued.Checked = False
                        Else
                            DTPDraftIssued.Text = dr.Item("datDraftIssued")
                            DTPDraftIssued.Checked = True
                        End If
                        If IsDBNull(dr.Item("datPAExpires")) Then
                            DTPDatePAExpires.Text = OracleDate
                            DTPDatePAExpires.Checked = False
                        Else
                            DTPDatePAExpires.Text = dr.Item("datPAExpires")
                            DTPDatePAExpires.Checked = True
                        End If
                        If IsDBNull(dr.Item("datPNExpires")) Then
                            DTPDatePNExpires.Text = OracleDate
                            DTPDatePNExpires.Checked = False
                        Else
                            DTPDatePNExpires.Text = dr.Item("datPNExpires")
                            DTPDatePNExpires.Checked = True
                        End If
                        If IsDBNull(dr.Item("strPAReady")) Then
                            chbPAReady.Checked = False
                        Else
                            If dr.Item("strPAReady") = "True" Then
                                chbPAReady.Checked = True
                            Else
                                chbPAReady.Checked = False
                            End If
                        End If
                        If IsDBNull(dr.Item("strPNReady")) Then
                            chbPNReady.Checked = False
                        Else
                            If dr.Item("strPNReady") = "True" Then
                                chbPNReady.Checked = True
                            Else
                                chbPNReady.Checked = False
                            End If
                        End If
                        If IsDBNull(dr.Item("datEPAWaived")) Then
                            DTPEPAWaived.Text = OracleDate
                            DTPEPAWaived.Checked = False
                        Else
                            DTPEPAWaived.Text = dr.Item("datEPAWaived")
                            DTPEPAWaived.Checked = True
                        End If
                        If IsDBNull(dr.Item("datEPAEnds")) Then
                            DTPEPAEnds.Text = OracleDate
                            DTPEPAEnds.Checked = False
                        Else
                            DTPEPAEnds.Text = dr.Item("datEPAEnds")
                            DTPEPAEnds.Checked = True
                        End If
                        If IsDBNull(dr.Item("datToBranchCheif")) Then
                            DTPDateToBC.Text = OracleDate
                            DTPDateToBC.Checked = False
                        Else
                            DTPDateToBC.Text = dr.Item("datToBranchCheif")
                            DTPDateToBC.Checked = True
                        End If
                        If IsDBNull(dr.Item("datToDirector")) Then
                            DTPDateToDO.Text = OracleDate
                            DTPDateToDO.Checked = False
                        Else
                            DTPDateToDO.Text = dr.Item("datToDirector")
                            DTPDateToDO.Checked = True
                        End If
                        If IsDBNull(dr.Item("strStateprogramcodes")) Then
                            chbNSRMajor.Checked = False
                            chbHAPsMajor.Checked = False
                        Else
                            Select Case Mid(dr.Item("strStateprogramcodes"), 1, 1)
                                Case 0
                                    chbNSRMajor.Checked = False
                                Case 1
                                    chbNSRMajor.Checked = True
                                Case Else
                                    chbNSRMajor.Checked = False
                            End Select
                            Select Case Mid(dr.Item("strStateprogramcodes"), 2, 1)
                                Case 0
                                    chbHAPsMajor.Checked = False
                                Case 1
                                    chbHAPsMajor.Checked = True
                                Case Else
                                    chbHAPsMajor.Checked = False
                            End Select
                        End If

                        If IsDBNull(dr.Item("STRSIGNIFICANTCOMMENTS")) Then
                            txtSignificantComments.Clear()
                        Else
                            txtSignificantComments.Text = dr.Item("strSignificantComments")
                        End If
                        If IsDBNull(dr.Item("strPAPosted")) Then
                            lblPAReady.Text = ""
                        Else
                            lblPAReady.Text = dr.Item("strPAPosted")
                        End If
                        If IsDBNull(dr.Item("strPNPosted")) Then
                            lblPNReady.Text = ""
                        Else
                            lblPNReady.Text = dr.Item("strPNPosted")
                        End If
                    End If
                    dr.Close()

                End If
                If TCApplicationTrackingLog.TabPages.Contains(TPReviews) Then
                    SQL = "select " & _
                    "datReviewsubmitted, strSSCPUnit, " & _
                    "strSSCPReviewer, datSSCPReviewDate, " & _
                    "strSSCPComments, strISMPUnit, " & _
                    "strISMPReviewer, datISMPReviewDate, " & _
                    "strISMPComments " & _
                    "from AIRBRANCH.SSPPApplicationData, " & _
                    "AIRBRANCH.SSPPApplicationTracking " & _
                    "where AIRBRANCH.SSPPApplicationData.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
                    "and AIRBRANCH.SSPPApplicationData.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("datReviewsubmitted")) Then
                            DTPReviewSubmitted.Text = OracleDate
                            DTPReviewSubmitted.Checked = False
                        Else
                            DTPReviewSubmitted.Text = dr.Item("datReviewsubmitted")
                            DTPReviewSubmitted.Checked = True
                        End If
                        If IsDBNull(dr.Item("strSSCPUnit")) Then
                            cboSSCPUnits.SelectedValue = 0
                        Else
                            cboSSCPUnits.SelectedValue = dr.Item("strSSCPUnit")
                        End If
                        If IsDBNull(dr.Item("strISMPUnit")) Then
                            cboISMPUnits.SelectedValue = 0
                        Else
                            cboISMPUnits.SelectedValue = dr.Item("strISMPUnit")
                        End If
                        If IsDBNull(dr.Item("datSSCPReviewDate")) Then
                            DTPSSCPReview.Text = OracleDate
                            DTPSSCPReview.Checked = False
                        Else
                            DTPSSCPReview.Text = dr.Item("datSSCPReviewDate")
                            DTPSSCPReview.Checked = True
                        End If
                        If IsDBNull(dr.Item("strSSCPReviewer")) Then
                            cboSSCPStaff.SelectedValue = 0
                        Else
                            cboSSCPStaff.SelectedValue = dr.Item("strSSCPReviewer")
                        End If
                        If IsDBNull(dr.Item("strSSCPComments")) Then
                            rdbSSCPNo.Checked = True
                            txtSSCPComments.Clear()
                        Else
                            If dr.Item("strSSCPComments") = "N/A" Then
                                rdbSSCPNo.Checked = True
                                txtSSCPComments.Text = ""
                            Else
                                rdbSSCPYes.Checked = True
                                txtSSCPComments.Text = dr.Item("strSSCPComments")
                            End If
                        End If

                        If IsDBNull(dr.Item("datISMPReviewDate")) Then
                            DTPISMPReview.Text = OracleDate
                            DTPISMPReview.Checked = False
                        Else
                            DTPISMPReview.Text = dr.Item("datISMPReviewDate")
                            DTPISMPReview.Checked = True
                        End If
                        If IsDBNull(dr.Item("strISMPReviewer")) Then
                            cboISMPStaff.SelectedValue = 0
                        Else
                            cboISMPStaff.SelectedValue = dr.Item("strISMPReviewer")
                        End If
                        If IsDBNull(dr.Item("strISMPComments")) Then
                            rdbISMPNo.Checked = True
                            txtISMPComments.Clear()
                        Else
                            If dr.Item("strISMPComments") = "N/A" Then
                                rdbISMPNo.Checked = True
                                txtISMPComments.Text = ""
                            Else
                                rdbISMPYes.Checked = True
                                txtISMPComments.Text = dr.Item("strISMPComments")
                            End If
                        End If
                    End If
                    dr.Close()

                End If
                If TCApplicationTrackingLog.TabPages.Contains(TPApplicationHistroy) Then

                End If
                If TCApplicationTrackingLog.TabPages.Contains(TPInformationRequests) Then

                End If

                If TCApplicationTrackingLog.TabPages.Contains(TPWebPublisher) Then
                    SQL = "Select " & _
                    "datDraftOnWeb, " & _
                    "datEPAStatesNotified,  " & _
                    "datFinalONWeb, " & _
                    "datEPANotified,  " & _
                    "datEffective," & _
                    " datEPAStatesNotifiedAppRec,  " & _
                    "datExperationDate, strTargeted, " & _
                    "datPNExpires " & _
                    "from  " & _
                    "AIRBRANCH.SSPPApplicationMaster,  " & _
                    "AIRBRANCH.SSPPApplicationTracking,  " & _
                    "AIRBRANCH.SSPPApplicationData " & _
                    "where " & _
                    "    AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+)  " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+)  " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("datEPAStatesNotifiedAppRec")) Then
                            DTPNotifiedAppReceived.Text = OracleDate
                            DTPNotifiedAppReceived.Checked = False
                        Else
                            DTPNotifiedAppReceived.Text = dr.Item("datEPAStatesNotifiedAppRec")
                            DTPNotifiedAppReceived.Checked = True
                        End If
                        If IsDBNull(dr.Item("datDraftOnWeb")) Then
                            DTPDraftOnWeb.Text = OracleDate
                            DTPDraftOnWeb.Checked = False
                        Else
                            DTPDraftOnWeb.Text = dr.Item("datDraftOnWeb")
                            DTPDraftOnWeb.Checked = True
                        End If
                        If IsDBNull(dr.Item("datEPAStatesNotified")) Then
                            DTPEPAStatesNotified.Text = OracleDate
                            DTPEPAStatesNotified.Checked = False
                        Else
                            DTPEPAStatesNotified.Text = dr.Item("datEPAStatesNotified")
                            DTPEPAStatesNotified.Checked = True
                        End If
                        If IsDBNull(dr.Item("datFinalOnWeb")) Then
                            DTPFinalOnWeb.Text = OracleDate
                            DTPFinalOnWeb.Checked = False
                        Else
                            DTPFinalOnWeb.Text = dr.Item("datFinalOnWeb")
                            DTPFinalOnWeb.Checked = True
                        End If
                        If IsDBNull(dr.Item("DatEPANotified")) Then
                            DTPEPANotifiedPermitOnWeb.Text = OracleDate
                            DTPEPANotifiedPermitOnWeb.Checked = False
                        Else
                            DTPEPANotifiedPermitOnWeb.Text = dr.Item("DatEPANotified")
                            DTPEPANotifiedPermitOnWeb.Checked = True
                        End If
                        If IsDBNull(dr.Item("DatEffective")) Then
                            DTPEffectiveDateofPermit.Text = OracleDate
                            DTPEffectiveDateofPermit.Checked = False
                        Else
                            DTPEffectiveDateofPermit.Text = dr.Item("datEffective")
                            DTPEffectiveDateofPermit.Checked = True
                        End If
                        If IsDBNull(dr.Item("datExperationDate")) Then
                            DTPExperationDate.Text = OracleDate
                            DTPExperationDate.Checked = False
                        Else
                            DTPExperationDate.Checked = True
                            DTPExperationDate.Text = dr.Item("datExperationDate")
                        End If
                        If IsDBNull(dr.Item("strTargeted")) Then
                            txtEPATargetedComments.Text = ""
                        Else
                            txtEPATargetedComments.Text = dr.Item("strTargeted")
                        End If
                        If IsDBNull(dr.Item("datPNExpires")) Then
                            DTPPNExpires.Text = OracleDate
                            DTPPNExpires.Checked = False
                        Else
                            DTPPNExpires.Checked = True
                            DTPPNExpires.Text = dr.Item("datPNExpires")
                        End If

                    End If
                    dr.Close()
                End If
            End If

            If AIRSNumber <> "" Then
                txtAIRSNumber.Text = Mid(AIRSNumber, 5)
            End If
            CheckForLinks()

            If CloseOut <> "" Then
                CloseOutApplication(CloseOut)
            End If

        Catch ex As Exception
            ErrorReport(ex, temp, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ReLoadBasicFacilityInfo()
        Try
            Dim Facilityname As String = ""
            Dim FacilityStreet As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityZipCode As String = ""
            Dim OperationalStatus As String = ""
            Dim OperationalStatusLine As String = ""
            Dim Classification As String = ""
            Dim ClassificationLine As String = ""
            Dim AirProgramCodes As String = ""
            Dim AirPrograms As String = ""
            Dim AirProgramLine As String = ""
            Dim SIC As String = ""
            Dim SICLine As String = ""
            Dim NAICS As String = ""
            Dim NAICSLine As String = "NAICS Code - "
            Dim CountyName As String = ""
            Dim OfficeName As String = ""
            Dim District As String = ""
            Dim Attainment As String = ""
            Dim AttainmentStatus As String = ""
            Dim StateProgramCodes As String = ""
            Dim StatePrograms As String = ""
            Dim PlantDesc As String = ""
            Dim PlantLine As String = ""

            SQL = "Select " & _
            "strFacilityName, strFacilityStreet1,  " & _
            "strFacilityCity, strFacilityZipCode,  " & _
            "strSICCode, strClass,  " & _
            "strNAICSCode, " & _
            "strOperationalStatus, strAirProgramCodes,  " & _
            "strPlantDescription,  " & _
            "AIRBRANCH.APBHeaderData.strAttainmentStatus,  " & _
            "strStateProgramCodes,  " & _
            "strcountyName,  " & _
            "strOfficeName,  " & _
            "strDistrictName  " & _
            "from AIRBRANCH.APBFacilityInformation,  " & _
            "AIRBRANCH.APBHeaderData, AIRBRANCH.LookUpCountyInformation,  " & _
            "AIRBRANCH.LookUpDistrictOffice, AIRBRANCH.LookUpDistricts,  " & _
            "AIRBRANCH.LookUpDistrictInformation  " & _
            "where AIRBRANCH.APBFacilityInformation.strAIRSnumber = AIRBRANCH.APBHeaderData.strAIRSnumber " & _
            "and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
            "and substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpDistrictInformation.strDistrictCounty  " & _
            "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUpDistrictOffice.strDistrictCode  " & _
            "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUpDistrictOffice.strDistrictCode  " & _
            "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUpDistricts.strDistrictCode  " & _
            "and AIRBRANCH.APBHeaderData.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    Facilityname = "N/A"
                Else
                    Facilityname = dr.Item("strFacilityname")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    FacilityStreet = "N/A"
                Else
                    FacilityStreet = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacilityCity = "N/A"
                Else
                    FacilityCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    FacilityZipCode = "N/A"
                Else
                    FacilityZipCode = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    OperationalStatus = "N/A"
                Else
                    OperationalStatus = dr.Item("strOperationalStatus")
                    OperationalStatusLine = "Operating Status - " & OperationalStatus
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = "N/A"
                Else
                    Classification = dr.Item("strClass")
                    ClassificationLine = "Classification - " & Classification
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    AirProgramCodes = "000000000000000"
                Else
                    AirProgramCodes = dr.Item("strAirProgramCodes")
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    SIC = "N/A"
                Else
                    SIC = dr.Item("strSICCode")
                    SICLine = "SIC Code - " & SIC
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    NAICS = "N/A"
                Else
                    NAICS = dr.Item("strNAICSCode")
                    NAICSLine = "NAICS Code - " & NAICS
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    CountyName = "N/A"
                Else
                    CountyName = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strOfficeName")) Then
                    OfficeName = "N/A"
                Else
                    OfficeName = dr.Item("strOfficeName")
                End If
                If IsDBNull(dr.Item("strDistrictName")) Then
                    District = "N/A"
                Else
                    District = dr.Item("strDistrictName")
                End If
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    Attainment = "00000"
                Else
                    Attainment = dr.Item("strAttainmentstatus")
                End If
                If IsDBNull(dr.Item("strStateProgramCodes")) Then
                    StateProgramCodes = "00000"
                Else
                    StateProgramCodes = dr.Item("strStateProgramCodes")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    PlantDesc = "N/A"
                Else
                    PlantDesc = dr.Item("strPlantDescription")
                End If
                PlantLine = "Plant Description - " & PlantDesc
            Else
                Facilityname = "N/A"
                FacilityStreet = "N/A"
                FacilityCity = "N/A"
                FacilityZipCode = "N/A"
                OperationalStatus = "N/A"
                OperationalStatusLine = "Operating Status - "
                Classification = "N/A"
                ClassificationLine = "Classification - "
                AirProgramCodes = "000000000000000"
                SIC = "N/A"
                SICLine = "SIC Code - "
                NAICS = "N/A"
                NAICSLine = "NAICS Code - "
                CountyName = "N/A"
                OfficeName = "N/A"
                District = "N/A"
                Attainment = "00000"
                StateProgramCodes = "00000"
                PlantDesc = "N/A"
                PlantLine = "Plant Description - "
            End If

            If Mid(AirProgramCodes, 1, 1) = 1 Then
                AirPrograms = "   0 - SIP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 2, 1) = 1 Then
                AirPrograms = AirPrograms & "   1 - Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCodes, 3, 1) = 1 Then
                AirPrograms = AirPrograms & "   3 - Non-Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCodes, 4, 1) = 1 Then
                AirPrograms = AirPrograms & "   4 - CFC Tracking" & vbCrLf
            End If
            If Mid(AirProgramCodes, 5, 1) = 1 Then
                AirPrograms = AirPrograms & "   6 - PSD" & vbCrLf
            End If
            If Mid(AirProgramCodes, 6, 1) = 1 Then
                AirPrograms = AirPrograms & "   7 - NSR" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 7, 1) = 1 Then
                AirPrograms = AirPrograms & "   8 - NESHAP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 8, 1) = 1 Then
                AirPrograms = AirPrograms & "   9 - NSPS" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 9, 1) = 1 Then
                AirPrograms = AirPrograms & "   F - FESOP" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 10, 1) = 1 Then
                AirPrograms = AirPrograms & "   A - Acid Precipitation" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 11, 1) = 1 Then
                AirPrograms = AirPrograms & "   I - Native American" & vbCrLf
            End If
            If Mid(AirProgramCodes, 12, 1) = 1 Then
                AirPrograms = AirPrograms & "   M - MACT" & vbCrLf
            Else
            End If
            If Mid(AirProgramCodes, 13, 1) = 1 Then
                AirPrograms = AirPrograms & "   V - Title V Permit" & vbCrLf
            Else
            End If
            AirProgramLine = "Air Program(s) - " & vbCrLf & AirPrograms

            Select Case Mid(Attainment, 2, 1)
                Case 0
                    AttainmentStatus = ""
                Case 1
                    AttainmentStatus = "   1-hr Ozone"
                Case 2
                    AttainmentStatus = "   1-hr Ozone (Contributing)"
                Case Else
                    AttainmentStatus = ""
            End Select
            Select Case Mid(Attainment, 3, 1)
                Case 0
                    AttainmentStatus = AttainmentStatus & ""
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "8-hr Atlanta"
                    Else
                        AttainmentStatus = "   8-hr Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "8-hr Macon"
                    Else
                        AttainmentStatus = "   8-hr Macon"
                    End If
                Case Else
                    AttainmentStatus = AttainmentStatus & ""
            End Select
            Select Case Mid(Attainment, 4, 1)
                Case 0
                    AttainmentStatus = AttainmentStatus & ""
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Atlanta"
                    Else
                        AttainmentStatus = "   PM 2.5 Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5  Chattanooga"
                    Else
                        AttainmentStatus = "   PM 2.5  Chattanooga"
                    End If
                Case 3
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Floyd"
                    Else
                        AttainmentStatus = "   PM 2.5 Floyd"
                    End If
                Case 4
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbCrLf & "PM 2.5 Macon"
                    Else
                        AttainmentStatus = "   PM 2.5 Macon"
                    End If
                Case Else
                    AttainmentStatus = AttainmentStatus & ""
            End Select

            If AttainmentStatus = "" Then
                AttainmentStatus = "Non Attainment Area - N/A"
            Else
                AttainmentStatus = "Non Attainment Area - " & vbCrLf & AttainmentStatus
            End If

            Select Case Mid(StateProgramCodes, 1, 1)
                Case 0
                    StatePrograms = ""
                Case 1
                    StatePrograms = "   NSR/PSD"
                Case Else
                    StatePrograms = ""
            End Select
            Select Case Mid(StateProgramCodes, 2, 1)
                Case 0
                    StatePrograms = StatePrograms & ""
                Case 1
                    If StatePrograms <> "" Then
                        StatePrograms = StatePrograms & "HAPs Major"
                    Else
                        StatePrograms = "   HAPs Major"
                    End If
                Case Else
                    StatePrograms = StatePrograms & ""
            End Select
            If StatePrograms = "" Then
                StatePrograms = "State Codes - N/A"
            Else
                StatePrograms = "State Codes - " & vbCrLf & StatePrograms
            End If

            rtbFacilityInformation.Clear()
            rtbFacilityInformation.Text = "AIRS # - " & txtAIRSNumber.Text & vbCrLf & vbCrLf & _
            Facilityname & vbCrLf & _
            FacilityStreet & vbCrLf & _
            FacilityCity & ", GA " & FacilityZipCode & vbCrLf & vbCrLf & _
            OperationalStatusLine & vbCrLf & _
            ClassificationLine & vbCrLf & _
            SICLine & vbCrLf & _
            NAICSLine & vbCrLf & _
            AirProgramLine & _
            StatePrograms & vbCrLf & _
            "County - " & CountyName & vbCrLf & _
            "District - " & District & vbCrLf & _
            "District Office - " & OfficeName & vbCrLf & _
            AttainmentStatus & vbCrLf & vbCrLf & _
            PlantLine

            cboCounty.SelectedIndex = cboCounty.FindString(CountyName)
            txtDistrict.Text = District
            txtOffice.Text = OfficeName

            txtFacilityName.Text = Facilityname
            txtFacilityStreetAddress.Text = FacilityStreet
            cboFacilityCity.SelectedIndex = cboFacilityCity.FindString(FacilityCity)
            txtFacilityZipCode.Text = FacilityZipCode
            txtSICCode.Text = SIC
            txtNAICSCode.Text = NAICS
            Select Case OperationalStatus
                Case "O"
                    cboOperationalStatus.Text = "O - Operating"
                Case "P"
                    cboOperationalStatus.Text = "P - Planned"
                Case "C"
                    cboOperationalStatus.Text = "C - Under Construction"
                Case "T"
                    cboOperationalStatus.Text = "T - Temporarily Closed"
                Case "X"
                    cboOperationalStatus.Text = "X - Permanently Closed"
                Case "I"
                    cboOperationalStatus.Text = "I - Seasonal Operation"
                Case Else
                    cboOperationalStatus.Text = ""
            End Select
            Select Case Classification
                Case "A"
                    cboClassification.Text = "A - MAJOR"
                Case "B"
                    cboClassification.Text = "B - MINOR"
                Case "C"
                    cboClassification.Text = "C - UNKNOWN"
                Case "SM"
                    cboClassification.Text = "SM - SYNTHETIC MINOR"
                Case "PR"
                    cboClassification.Text = "PR - PERMIT BY RULE"
                    'Case "U"
                    '   cboClassification.Text = "U - UNDEFINED"
                Case Else
                    cboClassification.Text = ""
            End Select
            txtPlantDescription.Text = PlantDesc

            If Mid(AirProgramCodes, 1, 1) = 1 Then
                chbCDS_0.Checked = True
            Else
                chbCDS_0.Checked = True
            End If
            If Mid(AirProgramCodes, 5, 1) = 1 Then
                chbCDS_6.Checked = True
            Else
                chbCDS_6.Checked = False
            End If
            If Mid(AirProgramCodes, 6, 1) = 1 Then
                chbCDS_7.Checked = True
            Else
                chbCDS_7.Checked = False
            End If
            If Mid(AirProgramCodes, 7, 1) = 1 Then
                chbCDS_8.Checked = True
            Else
                chbCDS_8.Checked = False
            End If
            If Mid(AirProgramCodes, 8, 1) = 1 Then
                chbCDS_9.Checked = True
            Else
                chbCDS_9.Checked = False
            End If
            If Mid(AirProgramCodes, 10, 1) = 1 Then
                chbCDS_A.Checked = True
            Else
                chbCDS_A.Checked = False
            End If
            If Mid(AirProgramCodes, 12, 1) = 1 Then
                chbCDS_M.Checked = True
            Else
                chbCDS_M.Checked = False
            End If
            If Mid(AirProgramCodes, 13, 1) = 1 Then
                chbCDS_V.Checked = True
            Else
                chbCDS_V.Checked = False
            End If
            If Mid(AirProgramCodes, 14, 1) = 1 Then
                chbCDS_RMP.Checked = True
            Else
                chbCDS_RMP.Checked = False
            End If

            Select Case Mid(Attainment, 2, 1)
                Case 0
                    txt1HourOzone.Text = "No"
                Case 1
                    txt1HourOzone.Text = "Yes"
                Case 2
                    txt1HourOzone.Text = "Contributing"
                Case Else
                    txt1HourOzone.Text = "No"
            End Select
            Select Case Mid(Attainment, 3, 1)
                Case 0
                    txt8HROzone.Text = "No"
                Case 1
                    txt8HROzone.Text = "Atlanta"
                Case 2
                    txt8HROzone.Text = "Macon"
                Case Else
                    txt8HROzone.Text = "No"
            End Select
            Select Case Mid(Attainment, 4, 1)
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
            Select Case Mid(StateProgramCodes, 1, 1)
                Case 0
                    chbNSRMajor.Checked = False
                Case 1
                    chbNSRMajor.Checked = True
                Case Else
                    chbNSRMajor.Checked = False
            End Select
            Select Case Mid(StateProgramCodes, 2, 1)
                Case 0
                    chbHAPsMajor.Checked = False
                Case 1
                    chbHAPsMajor.Checked = True
                Case Else
                    chbHAPsMajor.Checked = False
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub CheckOpenApplications()
        Try

            SQL = "select count(*) as ApplicationCount " & _
               "from AIRBRANCH.SSPPApplicationMaster " & _
               "where datFinalizedDate Is Null " & _
               "and strAirsNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            txtOutstandingApplication.Clear()
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                txtOutstandingApplication.Text = dr.Item("ApplicationCount")
            Else
                txtOutstandingApplication.Text = "0"
            End If

            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub CheckForLinks()
        Dim MasterApplication As String
        Dim ApplicationCount As String = 0

        Try

            MasterApplication = ""
            txtMasterApp.Clear()
            txtMasterAppLock.Text = ""
            txtApplicationCount.Text = ""
            lbLinkApplications.Items.Clear()

            If txtApplicationNumber.Text <> "" Then
                SQL = "Select " & _
                "strMasterApplication, strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    MasterApplication = dr.Item("strMasterApplication")
                    txtMasterApp.Text = MasterApplication
                    txtMasterAppLock.Text = MasterApplication
                Else
                    MasterApplication = ""
                    txtMasterApp.Clear()
                    txtMasterAppLock.Text = ""
                    txtApplicationCount.Text = ""
                    lbLinkApplications.Items.Clear()
                    lblLinkWarning.Visible = False
                End If
                If MasterApplication <> "" Then
                    SQL = "Select " & _
                    "strMasterApplication, strApplicationNumber " & _
                    "from AIRBRANCH.SSPPApplicationLinking " & _
                    "where strMasterApplication = '" & MasterApplication & "' " & _
                    "order by strApplicationNumber "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        lbLinkApplications.Items.Add(dr.Item("strApplicationNumber"))
                        ApplicationCount += 1
                    End While
                    txtApplicationCount.Text = ApplicationCount
                    lblLinkWarning.Visible = True
                End If
            Else
                txtMasterApp.Clear()
                txtMasterAppLock.Clear()
                txtMasterAppLock.Clear()
                lbLinkApplications.Items.Clear()
                lblLinkWarning.Visible = False
            End If
            If lbLinkApplications.Items.Contains(txtApplicationNumber.Text) Then
            Else
                lbLinkApplications.Items.Add(txtApplicationNumber.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveApplicationData()
        Dim StaffResponsible As String = ""
        Dim ApplicationType As String = ""
        Dim PermitType As String = ""
        Dim Unit As String = ""
        Dim DateFinalized As String = ""
        Dim FacilityName As String = ""
        Dim FacilityAddress As String = ""
        Dim FacilityCity As String = ""
        Dim FacilityZipCode As String = ""
        Dim OperationalStatus As String = ""
        Dim Classification As String = ""
        Dim AirProgramCodes As String = ""
        Dim SIC As String = ""
        Dim NAICS As String = ""
        Dim PermitNumber As String = ""
        Dim PlantDesc As String = ""
        Dim Comments As String = ""
        Dim ApplicationNotes As String = ""
        Dim ReceivedDate As String = ""
        Dim SentByDate As String = ""
        Dim AssignedToEngineer As String = ""
        Dim ReAssignedToEngineer As String = ""
        Dim PackageCompleteDate As String = ""
        Dim AcknowledgementLetter As String = ""
        Dim PublicInvolved As String = ""
        Dim ToPMI As String = ""
        Dim ToPMII As String = ""
        Dim ReturnToEngineer As String = ""
        Dim PermitIssued As String = ""
        Dim AppDeadline As String = ""
        Dim Withdrawn As String = ""
        Dim DraftIssued As String = ""
        Dim EPAWaived As String = ""
        Dim EPAEnds As String = ""
        Dim ToBC As String = ""
        Dim ToDO As String = ""
        Dim PAReady As String = ""
        Dim PNReady As String = ""
        Dim TrackedRules As String = ""
        Dim StateProgramCodes As String = ""
        Dim AttainmentStatus As String = ""
        Dim SignificantComments As String = ""
        Dim PAExpires As String = ""
        Dim PNExpires As String = ""

        Try
            If txtApplicationNumber.Text <> "" Then
                If Not DAL.FacilityHeaderDataData.SicCodeIsValid(txtSICCode.Text) Then
                    MsgBox("ERROR" & vbCrLf & "The SIC Code is not valid and must be fixed before proceeding.", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                If DAL.FacilityHeaderDataData.NaicsCodeIsValid(txtNAICSCode.Text) = False Then
                    MsgBox("ERROR" & vbCrLf & "The NAICS Code is not valid and must be fixed before proceeding.", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into AIRBRANCH.SSPPApplicationMaster " & _
                    "(strApplicationNumber, strAIRSNumber, " & _
                    "strModifingPerson, datModifingDate) " & _
                    "values " & _
                    "('" & txtApplicationNumber.Text & "', '0413" & txtAIRSNumber.Text & "', " & _
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Read()
                    dr.Close()

                    SQL = "Insert into AIRBRANCH.SSPPApplicationData " & _
                    "(strApplicationNumber, strModifingPerson, " & _
                    "datModifingDate) " & _
                    "values " & _
                    "('" & txtApplicationNumber.Text & "', '" & CurrentUser.UserID & "', " & _
                    "'" & OracleDate & "') "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Read()
                    dr.Close()

                    SQL = "Insert into AIRBRANCH.SSPPApplicationTracking " & _
                    "(strApplicationNumber, strSubmittalNumber, " & _
                    "datApplicationStarted, strModifingPerson, " & _
                    "datModifingDate) " & _
                    "values " & _
                    "('" & txtApplicationNumber.Text & "', '1', " & _
                    "'', '" & CurrentUser.UserID & "', " & _
                    "'" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Read()
                    dr.Close()
                Else

                End If

                If cboEngineer.Text <> "" Then
                    StaffResponsible = cboEngineer.SelectedValue
                End If
                If cboApplicationType.Text <> "" Then
                    ApplicationType = cboApplicationType.SelectedValue
                    If cboApplicationType.SelectedValue Is Nothing Then
                        Select Case cboApplicationType.Text
                            Case "112(g)"
                                ApplicationType = "1"
                            Case "FESOP"
                                ApplicationType = "5"
                            Case "LTR"
                                ApplicationType = "6"
                            Case "NPR"
                                ApplicationType = "7"
                            Case "PSD"
                                ApplicationType = "10"
                            Case "SM(TV)"
                                ApplicationType = "13"
                            Case "TV-Amend"
                                ApplicationType = "17"
                            Case "SLSM"
                                ApplicationType = "18"
                            Case "SM80"
                                ApplicationType = "23"
                            Case "PCP"
                                ApplicationType = "24"
                            Case "Title V"
                                ApplicationType = "27"
                        End Select
                    End If
                End If
                If cboPermitAction.Text <> "" Then
                    PermitType = cboPermitAction.SelectedValue
                    If cboPermitAction.SelectedValue Is Nothing Then
                        Select Case cboPermitAction.Text
                            Case "Amendment"
                                PermitType = "1"
                            Case "Draft"
                                PermitType = "3"
                            Case "New Permit"
                                PermitType = "4"
                            Case "PRMT-DNL"
                                PermitType = "8"
                            Case "Revoked"
                                PermitType = "10"
                            Case "Initial Title V Permit"
                                PermitType = "12"
                            Case "Renewal Title V Permit"
                                PermitType = "13"
                        End Select
                    End If
                End If
                If cboApplicationUnit.Text <> "" Then
                    Unit = cboApplicationUnit.SelectedValue
                End If
                If chbClosedOut.Checked = True Then
                    DateFinalized = OracleDate
                Else
                    DateFinalized = ""
                End If

                SQL = "Update AIRBRANCH.SSPPApplicationMaster set " & _
               "strAIRSNumber = '0413" & txtAIRSNumber.Text & "', " & _
               "strStaffResponsible = '" & Replace(StaffResponsible, "'", "''") & "', " & _
               "strApplicationType = '" & Replace(ApplicationType, "'", "''") & "', " & _
               "strPermitType = '" & Replace(PermitType, "'", "''") & "', " & _
               "APBUnit = '" & Replace(Unit, "'", "''") & "', " & _
               "datFinalizedDate = '" & DateFinalized & "', " & _
               "strModifingperson = '" & CurrentUser.UserID & "', " & _
               "datModifingdate = (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
               "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " & _
                "datModifingdate " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("datModifingdate")) Then
                        TimeStamp = ""
                    Else
                        TimeStamp = dr.Item("datModifingdate")
                    End If
                End While
                dr.Close()

                txtFacilityName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(txtFacilityName.Text)
                FacilityName = txtFacilityName.Text
                FacilityAddress = Me.txtFacilityStreetAddress.Text
                If cboFacilityCity.Text <> "" Then
                    FacilityCity = cboFacilityCity.SelectedValue
                End If
                FacilityZipCode = Replace(txtFacilityZipCode.Text, "-", "")
                If cboOperationalStatus.Text <> "" Then
                    Select Case cboOperationalStatus.Text
                        Case "O - Operating"
                            OperationalStatus = "O"
                        Case "P - Planned"
                            OperationalStatus = "P"
                        Case "C - Under Construction"
                            OperationalStatus = "C"
                        Case "T - Temporarily Closed"
                            OperationalStatus = "T"
                        Case "X - Permanently Closed"
                            OperationalStatus = "X"
                        Case "I - Seasonal Operation"
                            OperationalStatus = "I"
                        Case Else
                            OperationalStatus = "O"
                    End Select
                Else
                    OperationalStatus = "O"
                End If
                If cboClassification.Text <> "" Then
                    Select Case cboClassification.Text
                        Case "A - MAJOR"
                            Classification = "A"
                        Case "B - MINOR"
                            Classification = "B"
                        Case "C - UNKNOWN"
                            Classification = "C"
                        Case "SM - SYNTHETIC MINOR"
                            Classification = "SM"
                        Case "PR - PERMIT BY RULE"
                            Classification = "PR"
                        Case "U - UNDEFINED"
                            Classification = "U"
                        Case Else
                            Classification = "C"
                    End Select
                Else
                    Classification = "C"
                End If
                AirProgramCodes = "000000000000000"
                If chbCDS_0.Checked = True Then
                    AirProgramCodes = "1" & Mid(AirProgramCodes, 2)
                End If
                If chbCDS_6.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 4) & "1" & Mid(AirProgramCodes, 6)
                End If
                If chbCDS_7.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 5) & "1" & Mid(AirProgramCodes, 7)
                End If
                If chbCDS_8.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 6) & "1" & Mid(AirProgramCodes, 8)
                End If
                If chbCDS_9.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 7) & "1" & Mid(AirProgramCodes, 9)
                End If
                If chbCDS_A.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 9) & "1" & Mid(AirProgramCodes, 11)
                End If
                If chbCDS_M.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 11) & "1" & Mid(AirProgramCodes, 13)
                End If
                If chbCDS_V.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 12) & "1" & Mid(AirProgramCodes, 14)
                End If
                If chbCDS_RMP.Checked = True Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 13) & "1" & Mid(AirProgramCodes, 15)
                End If
                AirProgramCodes = AirProgramCodes
                SIC = txtSICCode.Text
                NAICS = txtNAICSCode.Text

                PermitNumber = Replace(txtPermitNumber.Text, "-", "")
                PlantDesc = txtPlantDescription.Text
                Comments = txtComments.Text
                ApplicationNotes = txtReasonAppSubmitted.Text

                StateProgramCodes = "00000"
                If Me.chbNSRMajor.Checked = True Then
                    StateProgramCodes = "10000"
                Else
                    StateProgramCodes = "00000"
                End If
                If Me.chbHAPsMajor.Checked = True Then
                    StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "1000"
                Else
                    StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "0000"
                End If
                Select Case Me.txt1HourOzone.Text
                    Case "No"
                        AttainmentStatus = "00000"
                    Case "Yes"
                        AttainmentStatus = "01000"
                    Case "Contributing"

                    Case Else
                        AttainmentStatus = "00000"
                End Select
                Select Case txt8HROzone.Text
                    Case "No"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
                    Case "Atlanta"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
                    Case "Macon"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
                    Case Else
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
                End Select
                Select Case txtPM.Text
                    Case "No"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
                    Case "Atlanta"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
                    Case "Chattanooga"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
                    Case "Floyd"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
                    Case "Macon"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
                    Case Else
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
                End Select

                TrackedRules = "0000000000"
                If chbPSD.Checked = True Then
                    TrackedRules = "1" & Mid(TrackedRules, 2)
                End If
                If chbNAANSR.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 1) & "1" & Mid(TrackedRules, 3)
                End If
                If chb112g.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 2) & "1" & Mid(TrackedRules, 4)
                End If
                If chbRulett.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 3) & "1" & Mid(TrackedRules, 5)
                End If
                If chbRuleyy.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 4) & "1" & Mid(TrackedRules, 6)
                End If
                If chbPal.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 5) & "1" & Mid(TrackedRules, 7)
                End If
                If chbExpedited.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 6) & "1" & Mid(TrackedRules, 8)
                End If
                If chbConfidential.Checked = True Then
                    TrackedRules = Mid(TrackedRules, 1, 7) & "1" & Mid(TrackedRules, 9)
                End If

                If chbPAReady.Checked = True Then
                    PAReady = "True"
                Else
                    PAReady = "False"
                End If
                If chbPNReady.Checked = True Then
                    PNReady = "True"
                Else
                    PNReady = "False"
                End If
                If txtSignificantComments.Text = "" Then
                    SignificantComments = ""
                Else
                    SignificantComments = txtSignificantComments.Text
                End If
                If cboPublicAdvisory.Visible = False Then
                    PublicInvolved = "0"
                Else
                    If cboPublicAdvisory.Text <> "" Then
                        Select Case cboPublicAdvisory.Text
                            Case "Not Decided"
                                PublicInvolved = "0"
                            Case "PA Needed"
                                PublicInvolved = "1"
                            Case "PA Not Needed"
                                PublicInvolved = "2"
                            Case Else
                                PublicInvolved = "0"
                        End Select
                    Else
                        PublicInvolved = "0"
                    End If
                End If

                SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', " & _
                "strFacilityStreet1 = '" & Replace(FacilityAddress, "'", "''") & "', " & _
                "strFacilityStreet2 = 'N/A', " & _
                "strFacilityCity = '" & Replace(FacilityCity, "'", "''") & "', " & _
                "strFacilityState = 'GA', " & _
                "strFacilityZipCode = '" & Replace(FacilityZipCode, "'", "''") & "', " & _
                "strOperationalStatus = '" & Replace(OperationalStatus, "'", "''") & "', " & _
                "strClass = '" & Classification & "', " & _
                "strAirProgramCodes = '" & AirProgramCodes & "', " & _
                "strSICCode = '" & SIC & "', " & _
                "strNAICSCode = '" & NAICS & "', " & _
                "strPermitNumber = '" & Replace(PermitNumber, "'", "''") & "', " & _
                "strPlantDescription = '" & Replace(PlantDesc, "'", "''") & "', " & _
                "strComments = '" & Replace(Comments, "'", "''") & "', " & _
                "strApplicationNotes = '" & Replace(ApplicationNotes, "'", "''") & "', " & _
                "strTrackedRules = '" & TrackedRules & "', " & _
                "strStateProgramCodes = '" & StateProgramCodes & "', " & _
                "strPAReady = '" & PAReady & "', " & _
                "strPNReady = '" & PNReady & "', " & _
                "STRSIGNIFICANTCOMMENTS = '" & Replace(SignificantComments, "'", "''") & "', " & _
                "strPublicInvolvement = '" & PublicInvolved & "', " & _
                "strModifingperson = '" & CurrentUser.UserID & "', " & _
                "datModifingdate = '" & OracleDate & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                Try

                    dr = cmd.ExecuteReader
                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try

                dr.Read()
                dr.Close()

                ReceivedDate = DTPDateReceived.Text
                SentByDate = DTPDateSent.Text
                If DTPDateAssigned.Checked = True Then
                    AssignedToEngineer = DTPDateAssigned.Text
                Else
                    AssignedToEngineer = ""
                End If
                If DTPDateReassigned.Checked = True Then
                    ReAssignedToEngineer = DTPDateReassigned.Text
                Else
                    ReAssignedToEngineer = ""
                End If
                If DTPDateAcknowledge.Checked = True Then
                    AcknowledgementLetter = DTPDateAcknowledge.Text
                Else
                    AcknowledgementLetter = ""
                End If
                If DTPDateToUC.Checked = True Then
                    ToPMI = DTPDateToUC.Text
                Else
                    ToPMI = ""
                End If
                If DTPDateToPM.Checked = True Then
                    ToPMII = DTPDateToPM.Text
                Else
                    ToPMII = ""
                End If
                ReturnToEngineer = ""

                If DTPFinalAction.Checked = True Then
                    PermitIssued = DTPFinalAction.Text
                Else
                    PermitIssued = ""
                    'If chbClosedOut.Checked = True Then
                    '    PermitIssued = DateFinalized
                    'Else
                    '    PermitIssued = ""
                    'End If
                End If
                If DTPDeadline.Checked = True Then
                    AppDeadline = DTPDeadline.Text
                Else
                    AppDeadline = ""
                End If
                If DTPDraftIssued.Checked = True Then
                    DraftIssued = DTPDraftIssued.Text
                Else
                    DraftIssued = ""
                End If
                If DTPEPAWaived.Checked = True Then
                    EPAWaived = DTPEPAWaived.Text
                Else
                    EPAWaived = ""
                End If
                If DTPEPAEnds.Checked = True Then
                    EPAEnds = DTPEPAEnds.Text
                Else
                    EPAEnds = ""
                End If
                If DTPDateToBC.Checked = True Then
                    ToBC = DTPDateToBC.Text
                Else
                    ToBC = ""
                End If
                If DTPDateToDO.Checked = True Then
                    ToDO = DTPDateToDO.Text
                Else
                    ToDO = ""
                End If
                If DTPDatePAExpires.Checked = True Then
                    PAExpires = DTPDatePAExpires.Text
                Else
                    PAExpires = ""
                End If
                If DTPDatePNExpires.Checked = True Then
                    PNExpires = DTPDatePNExpires.Text
                Else
                    PNExpires = ""
                End If

                SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                "datReceivedDate = '" & ReceivedDate & "', " & _
                "datSentByFacility = '" & SentByDate & "', " & _
                "datAssignedToEngineer = '" & AssignedToEngineer & "', " & _
                "datReassignedToEngineer = '" & ReAssignedToEngineer & "', " & _
                "datApplicationPackageComplete = '" & PackageCompleteDate & "', " & _
                "datAcknowledgementLetterSent = '" & AcknowledgementLetter & "', " & _
                "datToPMI = '" & ToPMI & "', " & _
                "datToPMII = '" & ToPMII & "', " & _
                "datReturnedtoEngineer = '" & ReturnToEngineer & "', " & _
                "datPermitIssued = '" & PermitIssued & "', " & _
                "datApplicationDeadline = '" & AppDeadline & "', " & _
                "datWithdrawn = '" & Withdrawn & "', " & _
                "datDraftIssued = '" & DraftIssued & "', " & _
                "strModifingPerson = '" & CurrentUser.UserID & "', " & _
                "datModifingDate = '" & OracleDate & "', " & _
                "datEPAWaived = '" & EPAWaived & "', " & _
                "datEPAEnds = '" & EPAEnds & "', " & _
                "datToBranchCheif = '" & ToBC & "', " & _
                "datToDirector = '" & ToDO & "', " & _
                "datPAExpires = '" & PAExpires & "', " & _
                "datpnexpires = '" & PNExpires & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                If lblLinkWarning.Visible = True Then
                    Dim LinkedApplication As String
                    Dim i As Integer

                    For i = 0 To lbLinkApplications.Items.Count - 1
                        If lbLinkApplications.Items.Item(i) <> txtApplicationNumber.Text Then
                            LinkedApplication = lbLinkApplications.Items.Item(i)
                        Else
                            LinkedApplication = ""
                        End If
                        If LinkedApplication <> "" Then
                            SQL = "Update AIRBRANCH.SSPPApplicationMaster set " & _
                            "datFinalizedDate = '" & DateFinalized & "', " & _
                            "strPermitType = '" & Replace(PermitType, "'", "''") & "', " & _
                            "strModifingperson = '" & CurrentUser.UserID & "', " & _
                            "datModifingdate = '" & OracleDate & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                           "strOperationalStatus = '" & Replace(OperationalStatus, "'", "''") & "', " & _
                           "strClass = '" & Classification & "', " & _
                           "strAirProgramCodes = '" & AirProgramCodes & "', " & _
                           "strSICCode = '" & SIC & "', " & _
                           "strPermitNumber = '" & Replace(PermitNumber, "'", "''") & "', " & _
                           "strPlantDescription = '" & Replace(PlantDesc, "'", "''") & "', " & _
                           "strStateProgramCodes = '" & StateProgramCodes & "', " & _
                           "strPAReady = '" & PAReady & "', " & _
                           "strPNReady = '" & PNReady & "', " & _
                           "strSignificantComments = '" & Replace(SignificantComments, "'", "''") & "', " & _
                           "strPublicInvolvement = '" & Replace(PublicInvolved, "'", "''") & "', " & _
                           "strModifingperson = '" & CurrentUser.UserID & "', " & _
                           "datModifingdate = '" & OracleDate & "' " & _
                           "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                            "datPermitIssued = '" & PermitIssued & "', " & _
                            "datDraftIssued = '" & DraftIssued & "', " & _
                            "datEPAWaived = '" & EPAWaived & "', " & _
                            "datEPAEnds = '" & EPAEnds & "', " & _
                            "datPAExpires  = '" & PAExpires & "', " & _
                            "datPNExpires = '" & PNExpires & "', " & _
                            "datToBranchCheif = '" & ToBC & "', " & _
                            "datToDirector = '" & ToDO & "', " & _
                            "strModifingPerson = '" & CurrentUser.UserID & "', " & _
                            "datModifingDate = '" & OracleDate & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Select " & _
                            "datToPMI, datToPMII " & _
                            "from AIRBRANCH.SSPPApplicationTracking " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                SQL = ""
                                If IsDBNull(dr.Item("datToPMI")) Then
                                    SQL = SQL & "Update AIRBRANCH.SSPPApplicationTracking set datToPMI = '" & ToPMI & "'"
                                    If IsDBNull(dr.Item("datToPMII")) Then
                                        SQL = SQL & ", datToPMII = '" & ToPMII & "' where strApplicationNumber = '" & LinkedApplication & "' "
                                    Else
                                        SQL = SQL & " where strApplicationNumber = '" & LinkedApplication & "' "
                                    End If
                                Else
                                    If IsDBNull(dr.Item("datToPMII")) Then
                                        SQL = SQL & "Update AIRBRANCH.SSPPApplicationTracking set " & _
                                        "datToPMII = '" & ToPMII & "' where strApplicationNumber = '" & LinkedApplication & "' "
                                    End If
                                End If
                            End While
                            dr.Close()
                            If SQL <> "" Then
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If

                        End If
                    Next
                End If

                If DTPFinalAction.Checked And chbClosedOut.Checked And Apb.ApbFacilityId.IsValidAirsNumberFormat(txtAIRSNumber.Text) Then

                    If Not DAL.FacilityHeaderDataData.SicCodeIsValid(txtSICCode.Text) Then
                        MessageBox.Show("The SIC code entered is not valid. The application cannot be closed out without a valid SIC code.", _
                                        "Invalid SIC code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        chbClosedOut.Checked = False
                        Exit Sub
                    End If

                    If cboPermitAction.SelectedValue = "1" Or cboPermitAction.SelectedValue = "4" _
                              Or cboPermitAction.SelectedValue = "5" Or cboPermitAction.SelectedValue = "7" _
                              Or cboPermitAction.SelectedValue = "10" Or cboPermitAction.SelectedValue = "12" _
                              Or cboPermitAction.SelectedValue = "13" Then
                        ' Note that of these, only 5, 7, & 10 are currently active types - DW
                        ' 
                        ' Active types selected here:
                        '  5    NPR
                        '  7    Permit
                        ' 10    Revoked
                        '
                        ' Active types not selected here:
                        '  0    N/A
                        '  2    Denied
                        '  6    PBR
                        '  9    Returned
                        ' 11    Withdrawn
                        '
                        GenerateAFSEntry()
                    End If

                    Dim dresult As DialogResult = MessageBox.Show("Do you want to update Facility Information with this Application?", _
                                                  "Permit Tracking Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If dresult = DialogResult.Yes Then
                        UpdateAPBTables()
                    End If

                    If Apb.Sspp.Permit.IsValidPermitNumber(txtPermitNumber.Text) Then
                        PermitRevocationQuery()
                        SaveIssuedPermit()
                    End If

                End If

                FormStatus = "Loading"
                LoadBasicFacilityInfo()
                FormStatus = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub PermitRevocationQuery()
        ' Check for existing permits first
        Dim activePermits As List(Of Apb.Sspp.Permit) = DAL.Sspp.GetActivePermitsAsList(txtAIRSNumber.Text)

        activePermits.RemoveAll(Function(permit As Apb.Sspp.Permit) permit.Equals(New Apb.Sspp.Permit(txtPermitNumber.Text)))

        If activePermits IsNot Nothing AndAlso activePermits.Count > 0 Then

            Dim permitRevocationDialog As New SsppPermitRevocationDialog
            permitRevocationDialog.ActivePermits = activePermits ' Send list of existing permits to dialog
            permitRevocationDialog.ShowDialog()
            Dim revokedPermits As List(Of Apb.Sspp.Permit) = permitRevocationDialog.PermitsToRevoke

            If revokedPermits IsNot Nothing AndAlso revokedPermits.Count > 0 Then
                For Each p As Apb.Sspp.Permit In revokedPermits
                    p.RevokedDate = DTPFinalAction.Value
                    p.Active = False
                Next

                Dim result As Boolean = DAL.Sspp.UpdatePermits(revokedPermits)
                If Not result Then
                    MessageBox.Show("There was an error revoking permits." & vbNewLine & _
                                    "Please contact the Data Management Unit.", "Error", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub

    Private Sub SaveIssuedPermit()
        Dim result As Boolean = False
        Dim permit As Apb.Sspp.Permit

        If Not DAL.Sspp.PermitExists(txtPermitNumber.Text) Then
            permit = New Apb.Sspp.Permit(txtAIRSNumber.Text, txtPermitNumber.Text, _
                                         DTPFinalAction.Value, True, cboApplicationType.SelectedValue)
            result = DAL.Sspp.AddPermit(permit)
        Else
            permit = DAL.Sspp.GetPermit(txtPermitNumber.Text)
            permit.IssuedDate = DTPFinalAction.Value
            permit.PermitTypeCode = cboApplicationType.SelectedValue
            result = DAL.Sspp.UpdatePermit(permit)
        End If

        If Not result Then
            MessageBox.Show("There was an error saving the permit." & vbNewLine & _
                            "Please contact the Data Management Unit.", "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub SaveInformationRequest()
        Dim InformationRequestKey As String = ""
        Dim InformationRequested As String = ""
        Dim InformationReceived As String = ""
        Dim DateInfoRequested As String
        Dim DateInfoReceived As String

        Try

            If txtApplicationNumber.Text <> "" Then
                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If txtInformationRequestedKey.Text = "" Then
                        SQL = "Select max(strRequestKey) as RequestKey " & _
                        "from AIRBRANCH.SSPPApplicationInformation " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "'"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            If IsDBNull(dr.Item("RequestKey")) Then
                                InformationRequestKey = "0"
                            Else
                                InformationRequestKey = dr.Item("RequestKey")
                            End If

                        End If

                        dr.Close()

                        If InformationRequestKey <> "" Then
                            InformationRequestKey = CInt((InformationRequestKey) + 1)
                        Else
                            InformationRequestKey = "1"
                        End If
                    Else
                        InformationRequestKey = txtInformationRequestedKey.Text
                    End If

                    SQL = "Select strApplicationNumber " & _
                    "from AIRBRANCH.SSPPApplicationInformation " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                    "and strRequestKey = '" & InformationRequestKey & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()

                    InformationRequested = Replace(Mid(txtInformationRequested.Text, 1, 4000), "'", "''")

                    InformationReceived = Mid(Replace(txtInformationReceived.Text, "'", "''"), 1, 4000)

                    If DTPInformationRequested.Checked = True Then
                        DateInfoRequested = DTPInformationRequested.Text
                    Else
                        DateInfoRequested = ""
                    End If
                    If DTPInformationReceived.Checked = True Then
                        DateInfoReceived = DTPInformationReceived.Text
                    Else
                        DateInfoReceived = ""
                    End If

                    If recExist = True Then
                        'Update
                        SQL = "Update AIRBRANCH.SSPPApplicationInformation set " & _
                        "datInformationRequested = '" & DateInfoRequested & "', " & _
                        "strInformationRequested = '" & InformationRequested & "', " & _
                        "datInformationReceived = '" & DateInfoReceived & "', " & _
                        "strInformationReceived = '" & InformationReceived & "', " & _
                        "strModifingPerson = '" & CurrentUser.UserID & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                        "and strRequestKey = '" & InformationRequestKey & "' "
                    Else
                        'Insert 
                        SQL = "Insert into AIRBRANCH.SSPPApplicationInformation " & _
                        "(strApplicationNumber, strRequestKey, " & _
                        "datInformationRequested, strInformationRequested, " & _
                        "datInformationReceived, strInformationReceived, " & _
                        "strModifingPerson, datModifingDate) " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & InformationRequestKey & "', " & _
                        "'" & DateInfoRequested & "', '" & InformationRequested & "', " & _
                        "'" & DateInfoReceived & "', '" & InformationReceived & "', " & _
                        "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                    End If
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    LoadInformationRequestedHistory()

                Else
                    MsgBox("The application must be saved before an information request can be made.", MsgBoxStyle.Information, "Permit Tracking Log")
                End If
            Else
                MsgBox("An Application Number must be available before Information requested can be saved.", MsgBoxStyle.Information, "Permit Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub DeleteInformationRequest()
        Dim InformationRequestKey As String

        Try

            If txtInformationRequestedKey.Text <> "" Then
                InformationRequestKey = txtInformationRequestedKey.Text
                SQL = "Delete AIRBRANCH.SSPPApplicationInformation " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                "and strRequestKey = '" & InformationRequestKey & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                txtInformationRequestedKey.Clear()
                DTPInformationRequested.Text = OracleDate
                txtInformationRequested.Clear()
                DTPInformationReceived.Text = OracleDate
                txtInformationReceived.Clear()
                LoadInformationRequestedHistory()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveApplicationSubmitForReview()
        Dim DateReviewSubmitted As String = OracleDate

        Try

            If txtApplicationNumber.Text <> "" Then
                If DTPReviewSubmitted.Checked = True Then
                    DateReviewSubmitted = DTPReviewSubmitted.Text
                Else
                    DateReviewSubmitted = OracleDate
                End If


                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Select strApplicationNumber " & _
                    "from AIRBRANCH.SSPPApplicationTracking " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()

                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                        "strSSCPUnit = '" & cboSSCPUnits.SelectedValue & "', " & _
                        "strISMPUnit = '" & cboISMPUnits.SelectedValue & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datReviewSubmitted = '" & DateReviewSubmitted & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else

                    End If

                Else
                    MsgBox("The application must be saved before it can be submitted for review by the other programs.", MsgBoxStyle.Information, "Permit Tracking Log")
                End If
            Else
                MsgBox("An Application Number must be available before Information requested can be saved.", MsgBoxStyle.Information, "Permit Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveSSCPReview()
        Dim SSCPComments As String

        Try

            If txtApplicationNumber.Text <> "" Then
                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If rdbSSCPNo.Checked = True Then
                        SSCPComments = "N/A"
                    Else
                        SSCPComments = txtSSCPComments.Text
                    End If

                    SQL = "Select StrApplicationNumber " & _
                    "from AIRBRANCH.SSPPApplicationMaster " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                        "strSSCPReviewer = '" & cboSSCPStaff.SelectedValue & "', " & _
                        "strSSCPComments = '" & Replace(SSCPComments, "'", "''") & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datSSCPReviewDate = '" & DTPSSCPReview.Text & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        'MsgBox("Done", MsgBoxStyle.Information, "Permit Tracking Log")
                    Else
                        MsgBox("This Application has not been submitted for review yet.", MsgBoxStyle.Information, "Permit Tracking Log")
                    End If
                Else
                    MsgBox("The application must be saved before it can be submitted for review by the other programs.", MsgBoxStyle.Information, "Permit Tracking Log")
                End If
            Else
                MsgBox("An Application Number must be available before Information requested can be saved.", MsgBoxStyle.Information, "Permit Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveISMPReview()
        Dim ISMPComments As String

        Try

            If txtApplicationNumber.Text <> "" Then
                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then

                    If rdbISMPNo.Checked = True Then
                        ISMPComments = "N/A"
                    Else
                        ISMPComments = txtISMPComments.Text
                    End If

                    SQL = "select StrApplicationNumber " & _
                    "from AIRBRANCH.SSPPApplicationMaster " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                        "strISMPReviewer = '" & cboISMPStaff.SelectedValue & "', " & _
                        "strISMPComments = '" & Replace(ISMPComments, "'", "''") & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datISMPReviewDate = '" & DTPISMPReview.Text & "' " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        MsgBox("This Application has not been submitted for review yet.", MsgBoxStyle.Information, "Permit Tracking Log")
                    End If
                Else
                    MsgBox("The application must be saved before it can be submitted for review by the other programs.", MsgBoxStyle.Information, "Permit Tracking Log")
                End If
            Else
                MsgBox("An Application Number must be available before Information requested can be saved.", MsgBoxStyle.Information, "Permit Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SaveApplicationContact()
        Try
            Dim MaxKey As String = ""
            Dim i As Integer
            Dim ContactFirstName As String = " "
            Dim ContactLastname As String = " "
            Dim ContactPrefix As String = " "
            Dim ContactSuffix As String = " "
            Dim ContactTitle As String = " "
            Dim ContactCompany As String = " "
            Dim ContactPhone As String = " "
            Dim ContactFax As String = " "
            Dim ContactEmail As String = " "
            Dim ContactAddress As String = " "
            Dim ContactCity As String = " "
            Dim ContactState As String = " "
            Dim ContactZipCode As String = " "
            Dim ContactDescription As String = " "

            If txtContactFirstName.Text <> "" Then
                ContactFirstName = txtContactFirstName.Text
            Else
                ContactFirstName = " "
            End If
            If txtContactLastName.Text <> "" Then
                ContactLastname = txtContactLastName.Text
            Else
                ContactLastname = " "
            End If
            If txtContactSocialTitle.Text <> "" Then
                ContactPrefix = txtContactSocialTitle.Text
            Else
                ContactPrefix = " "
            End If
            If txtContactPedigree.Text <> "" Then
                ContactSuffix = txtContactPedigree.Text
            Else
                ContactSuffix = " "
            End If
            If txtContactTitle.Text <> "" Then
                ContactTitle = txtContactTitle.Text
            Else
                ContactTitle = " "
            End If
            If txtContactCompanyName.Text <> "" Then
                ContactCompany = txtContactCompanyName.Text
            Else
                ContactCompany = " "
            End If
            If mtbContactPhoneNumber.Text <> "" Then
                ContactPhone = mtbContactPhoneNumber.Text
            Else
                ContactPhone = "0000000000"
            End If
            If mtbContactFaxNumber.Text <> "" Then
                ContactFax = mtbContactFaxNumber.Text
            Else
                ContactFax = "0000000000"
            End If
            If txtContactEmailAddress.Text <> "" Then
                ContactEmail = txtContactEmailAddress.Text
            Else
                ContactEmail = " "
            End If
            If txtContactStreetAddress.Text <> "" Then
                ContactAddress = txtContactStreetAddress.Text
            Else
                ContactAddress = " "
            End If
            If txtContactCity.Text <> "" Then
                ContactCity = txtContactCity.Text
            Else
                ContactCity = " "
            End If
            If txtContactState.Text <> "" Then
                ContactState = txtContactState.Text
            Else
                ContactState = " "
            End If
            If mtbContactZipCode.Text <> "" Then
                ContactZipCode = mtbContactZipCode.Text
            Else
                ContactZipCode = "00000"
            End If
            If txtContactDescription.Text <> "" Then
                ContactDescription = txtContactDescription.Text
            Else
                If txtApplicationNumber.Text <> "" Then
                    ContactDescription = "From App #- " & txtApplicationNumber.Text
                Else
                    ContactDescription = " "
                End If
            End If

            SQL = "Select strApplicationNumber " & _
            "from AIRBRANCH.SSPPApplicationContact " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                'update
                SQL = "Update AIRBRANCH.SSPPApplicationContact set " & _
                "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                "strContactLastName = '" & Replace(ContactLastname, "'", "''") & "', " & _
                "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                "strContactSuffix = '" & Replace(ContactSuffix, "'", "''") & "', " & _
                "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " & _
                "strContactCompanyName = '" & Replace(ContactCompany, "'", "''") & "', " & _
                "strContactPhoneNumber1 = '" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "strContactfaxnumber = '" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "strContactemail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                "strContactAddress1 = '" & Replace(ContactAddress, "'", "''") & "', " & _
                "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                "strContactZipCode = '" & Replace(ContactZipCode, "-", "") & "', " & _
                "strContactDescription = '" & Replace(ContactDescription, "'", "''") & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            Else
                'insert 
                SQL = "Insert into AIRBRANCH.SSPPApplicationContact " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', " & _
                "'" & Replace(ContactFirstName, "'", "''") & "', " & _
                "'" & Replace(ContactLastname, "'", "''") & "', " & _
                "'" & Replace(ContactPrefix, "'", "''") & "', " & _
                "'" & Replace(ContactSuffix, "'", "''") & "', " & _
                "'" & Replace(ContactTitle, "'", "''") & "', " & _
                "'" & Replace(ContactCompany, "'", "''") & "', " & _
                "'" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "'" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "'" & Replace(ContactEmail, "'", "''") & "', " & _
                "'" & Replace(ContactAddress, "'", "''") & "', " & _
                "'" & Replace(ContactCity, "'", "''") & "', " & _
                "'" & Replace(ContactState, "'", "''") & "', " & _
                "'" & Replace(ContactZipCode, "-", "") & "', " & _
                "'" & Replace(ContactDescription, "'", "''") & "') "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            'This line was changed 09-14-2009 per Eric Cornwells request. The check for Final Action was removed 
            'If DTPFinalAction.Checked = True And chbClosedOut.Checked = True And txtAIRSNumber.Text.Length = 8 Then
            If chbClosedOut.Checked = True And txtAIRSNumber.Text.Length = 8 And IsNumeric(txtAIRSNumber.Text) Then
                SQL = "select strKey " & _
                "from AIRBRANCH.APBContactInformation, AIRBRANCH.SSPPApplicationContact  " & _
                "where AIRBRANCH.APBContactInformation.strContactKey = '0413" & txtAIRSNumber.Text & "30'  " & _
                "and AIRBRANCH.SSPPApplicationContact.strApplicationNumber = '" & txtApplicationNumber.Text & "'  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactFirstName) = Upper(AIRBRANCH.SSPPApplicationContact.strContactFirstName) " & _
                "and upper(AIRBRANCH.APBContactInformation.strContactLastName) = Upper(AIRBRANCH.SSPPApplicationContact.strContactLastName)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactPrefix) = Upper(AIRBRANCH.SSPPApplicationContact.strContactPrefix) " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactSuffix) = Upper(AIRBRANCH.SSPPApplicationContact.strContactSuffix)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactTitle) = Upper(AIRBRANCH.SSPPApplicationContact.strContactTitle)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactCompanyName) = Upper(AIRBRANCH.SSPPApplicationContact.strContactCompanyName)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactPhoneNumber1) = " & _
                "Upper(AIRBRANCH.SSPPApplicationContact.strContactPhoneNumber1)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactFaxNumber) = Upper(AIRBRANCH.SSPPApplicationContact.strContactFaxNumber)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactEmail) = Upper(AIRBRANCH.SSPPApplicationContact.strContactEmail)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strCOntactAddress1) = Upper(AIRBRANCH.SSPPApplicationContact.strContactAddress1)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strCOntactCity) = Upper(AIRBRANCH.SSPPApplicationContact.strContactCity)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactZipCode) = Upper(AIRBRANCH.SSPPApplicationcontact.strContactZipCode)  " & _
                "and Upper(AIRBRANCH.APBContactInformation.strContactDescription) = Upper(AIRBRANCH.SSPPApplicationContact.strContactDescription)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "select Max(strKey) as MaxKey " & _
                    "from AIRBRANCH.APBContactInformation " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                    "and substr(strkey, 1, 1) = '3' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        MaxKey = dr.Item("MaxKey")
                    End While
                    dr.Close()

                    i = CInt(Mid(MaxKey, 2))

                    SQL = "Insert into AIRBRANCH.APBContactInformation " & _
                    "(strContactKey, strAIRSnumber, strKey, " & _
                    "strContactFirstName, strContactLastName,  " & _
                    "strContactPrefix, strContactSuffix,  " & _
                    "strContactTitle, strContactCompanyName,  " & _
                    "strContactPhoneNumber1, strContactPhoneNumber2,  " & _
                    "strContactFaxNumber, strContactEmail,  " & _
                    "strContactAddress1, strContactAddress2,  " & _
                    "strContactCity, strContactState,  " & _
                    "strContactZipCode, strModifingPerson,  " & _
                    "datModifingDate, strContactDescription)  " & _
                    "select  " & _
                    "'0' || (strContactKey+1) as strContactKey,  " & _
                    "strAIRSnumber,  " & _
                    "(strKey+1) as strKey, " & _
                    "strContactFirstName, strContactLastName,  " & _
                    "strContactPrefix, strContactSuffix,  " & _
                    "strContactTitle, strContactCompanyName,  " & _
                    "strContactPhoneNumber1, strContactPhoneNumber2,  " & _
                    "strContactFaxNumber, strContactEmail,  " & _
                    "strContactAddress1, strContactAddress2,  " & _
                    "strContactCity, strContactState,  " & _
                    "strContactZipCode, strModifingPerson,  " & _
                    "datModifingDate, strContactDescription " & _
                    "from AIRBRANCH.APBContactInformation  " & _
                    "where strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                    "and strKey = '3" & i & "'  "

                    If MaxKey = "39" Then
                    Else
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If


                    Do While i <> 0
                        SQL = "Select strKey " & _
                        "from AIRBRANCH.APBContactInformation " & _
                        "where strAirsnumber = '0413" & txtAIRSNumber.Text & "' " & _
                        "and strKey = '3" & i & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        i -= 1

                        If recExist = True Then
                            SQL = "Update AIRBRANCH.APBContactInformation set " & _
                            "strContactFirstName = (select strContactFirstName from AIRBRANCH.APBContactInformation " & _
                            "where strContactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactLastname = (select strContactLastname from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactPrefix = (select strContactPrefix from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactSuffix = (select strContactSuffix from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactTitle = (select strContactTitle from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactCompanyName = (select strContactCompanyName from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactPhoneNumber1 = (select strContactPhoneNumber1 from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactPhoneNumber2 = (select strContactPhoneNumber2 from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactFaxNumber = (select strContactFaxNumber from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactEmail = (select strContactEmail from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactAddress1 = (select strContactAddress1 from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactAddress2 = (select strContactAddress2 from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactCity = (select strContactCity from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactState = (select strContactState from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactZipCode = (select strContactZipCode from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strModifingPerson = (select strModifingPerson from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "datModifingDate = (select datModifingDate from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "'),  " & _
                            "strContactDescription = (select strContactDescription from AIRBRANCH.APBContactInformation " & _
                            "Where strCOntactKey = '0413" & txtAIRSNumber.Text & "3" & i & "') " & _
                            "where strContactKey = '0413" & txtAIRSNumber.Text & "3" & i + 1 & "' "
                        Else
                            SQL = "Insert into AIRBRANCH.APBContactInformation " & _
                            "(strContactKey, strAIRSnumber, strKey, " & _
                            "strContactFirstName, strContactLastName,  " & _
                            "strContactPrefix, strContactSuffix,  " & _
                            "strContactTitle, strContactCompanyName,  " & _
                            "strContactPhoneNumber1, strContactPhoneNumber2,  " & _
                            "strContactFaxNumber, strContactEmail,  " & _
                            "strContactAddress1, strContactAddress2,  " & _
                            "strContactCity, strContactState,  " & _
                            "strContactZipCode, strModifingPerson,  " & _
                            "datModifingDate, strContactDescription)  " & _
                            "select  " & _
                            "'0' || (strContactKey+1) as strContactKey,  " & _
                            "strAIRSnumber,  " & _
                            "(strKey+1) as strKey, " & _
                            "strContactFirstName, strContactLastName,  " & _
                            "strContactPrefix, strContactSuffix,  " & _
                            "strContactTitle, strContactCompanyName,  " & _
                            "strContactPhoneNumber1, strContactPhoneNumber2,  " & _
                            "strContactFaxNumber, strContactEmail,  " & _
                            "strContactAddress1, strContactAddress2,  " & _
                            "strContactCity, strContactState,  " & _
                            "strContactZipCode, strModifingPerson,  " & _
                            "datModifingDate, strContactDescription " & _
                            "from AIRBRANCH.APBContactInformation  " & _
                            "where strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                            "and strKey = '3" & i + 1 & "'  "
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                    Loop

                    SQL = "Update AIRBRANCH.APBContactInformation set " & _
                           "strContactFirstName = (select strContactFirstName from AIRBRANCH.SSPPApplicationContact " & _
                           "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactLastname = (select strContactLastname from AIRBRANCH.SSPPApplicationContact " & _
                           "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactPrefix = (select strContactPrefix from AIRBRANCH.SSPPApplicationContact " & _
                           "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactSuffix = (select strContactSuffix from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactTitle = (select strContactTitle from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactCompanyName = (select strContactCompanyName from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactPhoneNumber1 = (select strContactPhoneNumber1 from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactPhoneNumber2 = (select strContactPhoneNumber2 from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactFaxNumber = (select strContactFaxNumber from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactEmail = (select strContactEmail from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactAddress1 = (select strContactAddress1 from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactAddress2 = (select strContactAddress2 from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactCity = (select strContactCity from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactState = (select strContactState from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactZipCode = (select strContactZipCode from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strModifingPerson = (select strModifingPerson from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "datModifingDate = (select datModifingDate from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "'),  " & _
                           "strContactDescription = (select strContactDescription from AIRBRANCH.SSPPApplicationContact " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "')  " & _
                           "where strContactKey = '0413" & txtAIRSNumber.Text & "3" & i & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub CloseOutApplication(ByVal Status As String)
        Try

            Select Case Status
                Case "True"
                    txtFacilityName.ReadOnly = True
                    txtFacilityStreetAddress.ReadOnly = True
                    cboFacilityCity.Enabled = False
                    cboCounty.Enabled = False
                    txtSICCode.ReadOnly = True
                    txtNAICSCode.ReadOnly = True
                    cboOperationalStatus.Enabled = False
                    cboClassification.Enabled = False
                    chbCDS_0.Enabled = False
                    chbCDS_6.Enabled = False
                    chbCDS_7.Enabled = False
                    chbCDS_8.Enabled = False
                    chbCDS_9.Enabled = False
                    chbCDS_M.Enabled = False
                    chbCDS_V.Enabled = False
                    chbCDS_A.Enabled = False
                    chbCDS_RMP.Enabled = False

                    txtPlantDescription.ReadOnly = True
                    DTPDateSent.Enabled = False
                    DTPDateReceived.Enabled = False
                    DTPDateAssigned.Enabled = False
                    DTPDateAssigned.Enabled = False
                    DTPDateReassigned.Enabled = False
                    DTPDateReassigned.Enabled = False
                    DTPDateAcknowledge.Enabled = False
                    DTPDateAcknowledge.Enabled = False
                    DTPDatePAExpires.Enabled = False
                    DTPDatePAExpires.Enabled = False
                    DTPDatePNExpires.Enabled = False
                    DTPDatePNExpires.Enabled = False
                    DTPDeadline.Enabled = False
                    DTPDeadline.Enabled = False
                    DTPDateToUC.Enabled = False
                    DTPDateToUC.Enabled = False
                    DTPDateToPM.Enabled = False
                    DTPDateToPM.Enabled = False
                    DTPFinalAction.Enabled = False
                    DTPFinalAction.Enabled = False
                    DTPDraftIssued.Enabled = False
                    DTPDraftIssued.Enabled = False
                    txtPermitNumber.ReadOnly = True
                    cboPermitAction.Enabled = False
                    cboPublicAdvisory.Enabled = False
                    txtReasonAppSubmitted.ReadOnly = True
                    txtComments.ReadOnly = True
                    btnSaveInformationRequest.Enabled = False
                    btnClearInformationRequest.Enabled = False
                    DTPInformationRequested.Enabled = False
                    DTPInformationReceived.Enabled = False
                    txtInformationRequested.ReadOnly = True
                    txtInformationReceived.ReadOnly = True
                    DTPReviewSubmitted.Enabled = False
                    cboSSCPUnits.Enabled = False
                    cboISMPUnits.Enabled = False
                    DTPReviewSubmitted.Enabled = False
                    cboSSCPStaff.Enabled = False
                    rdbSSCPYes.Enabled = False
                    rdbSSCPNo.Enabled = False
                    txtSSCPComments.ReadOnly = True
                    cboEngineer.Enabled = False
                    cboApplicationUnit.Enabled = False
                    cboApplicationType.Enabled = False
                    chbNSRMajor.Enabled = False
                    chbHAPsMajor.Enabled = False
                    chbPSD.Enabled = False
                    chbNAANSR.Enabled = False
                    chb112g.Enabled = False
                    chbRulett.Enabled = False
                    chbRuleyy.Enabled = False
                    chbPal.Enabled = False
                    chbExpedited.Enabled = False
                    chbConfidential.Enabled = False
                    txtSignificantComments.ReadOnly = True

                    'Facility Application History 
                    btnAddApplicationToList.Enabled = False
                    btnClearList.Enabled = False
                    btnLinkApplications.Enabled = False
                    btnClearLinks.Enabled = False

                    'Information Request History
                    DTPInformationRequested.Enabled = False
                    DTPInformationReceived.Enabled = False
                    btnClearInformationRequest.Enabled = False
                    btnSaveInformationRequest.Enabled = False
                    btnDeleteInformationRequest.Enabled = False
                    txtInformationRequested.ReadOnly = True
                    txtInformationReceived.ReadOnly = True

                    'Application Review Submission
                    DTPReviewSubmitted.Enabled = False
                    cboSSCPUnits.Enabled = False
                    cboISMPUnits.Enabled = False
                    DTPReviewSubmitted.Enabled = False
                    cboISMPStaff.Enabled = False
                    rdbISMPYes.Enabled = False
                    rdbISMPNo.Enabled = False
                    txtISMPComments.ReadOnly = True

                    'Subpart Tools
                    btnSaveSIPSubpart.Enabled = False
                    btnClearSIPDeletes.Enabled = False
                    btnClearAddModifiedSIPs.Enabled = False
                    btnAddNewSIPSubpart.Enabled = False
                    btnSIPDelete.Enabled = False
                    btnSIPUndelete.Enabled = False
                    btnSIPDeleteAll.Enabled = False
                    btnSIPUndeleteAll.Enabled = False
                    btnSIPEdit.Enabled = False
                    btnSIPUnedit.Enabled = False
                    btnSIPEditAll.Enabled = False
                    btnSIPUneditAll.Enabled = False

                    btnSaveNESHAPSubpart.Enabled = False
                    btnClearNESHAPDeletes.Enabled = False
                    btnClearAddModifiedNESHAPs.Enabled = False
                    btnAddNewNESHAPSubpart.Enabled = False
                    btnNESHAPDelete.Enabled = False
                    btnNESHAPUndelete.Enabled = False
                    btnNESHAPDeleteAll.Enabled = False
                    btnNESHAPUndeleteAll.Enabled = False
                    btnNESHAPEdit.Enabled = False
                    btnNESHAPUnedit.Enabled = False
                    btnNESHAPEditAll.Enabled = False
                    btnNESHAPUneditAll.Enabled = False

                    btnSaveNSPSSubpart.Enabled = False
                    btnClearNSPSDeletes.Enabled = False
                    btnClearAddModifiedNSPSs.Enabled = False
                    btnAddNewNSPSSubpart.Enabled = False
                    btnNSPSDelete.Enabled = False
                    btnNSPSUndelete.Enabled = False
                    btnNSPSDeleteAll.Enabled = False
                    btnNSPSUndeleteAll.Enabled = False
                    btnNSPSEdit.Enabled = False
                    btnNSPSUnedit.Enabled = False
                    btnNSPSEditAll.Enabled = False
                    btnNSPSUneditAll.Enabled = False

                    btnSaveMACTSubpart.Enabled = False
                    btnClearMACTDeletes.Enabled = False
                    btnClearAddModifiedMACTs.Enabled = False
                    btnAddNewMACTSubpart.Enabled = False
                    btnMACTDelete.Enabled = False
                    btnMACTUndelete.Enabled = False
                    btnMACTDeleteAll.Enabled = False
                    btnMACTUndeleteAll.Enabled = False
                    btnMACTEdit.Enabled = False
                    btnMACTUnedit.Enabled = False
                    btnMACTEditAll.Enabled = False
                    btnMACTUneditAll.Enabled = False
                Case "False"
                    LoadPermissions()
                Case Else
                    LoadPermissions()
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LinkApplications()
        Dim MasterApplication As String
        Dim i As Integer

        Try

            If lbLinkApplications.Items.Count > 1 Then
                MasterApplication = txtApplicationNumber.Text

                For i = 0 To lbLinkApplications.Items.Count - 1
                    If lbLinkApplications.Items.Item(i) <> txtApplicationNumber.Text Then
                        SQL = "select " & _
                        "strApplicationType " & _
                        "from AIRBRANCH.SSPPApplicationMaster " & _
                        "where strApplicationnumber = '" & lbLinkApplications.Items.Item(i) & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strApplicationType")) Then
                                MasterApp = MasterApp
                            Else
                                temp = dr.Item("strApplicationType")
                            End If
                        End While
                        dr.Close()
                        Select Case temp
                            Case "22"
                                MasterApplication = lbLinkApplications.Items.Item(i)
                                MasterApp = temp
                            Case Is = "16", Is = "17", Is = "21", Is = "2"
                                If MasterApp <> "22" Or MasterApp <> "16" Or MasterApp <> "17" Or MasterApp <> "21" Or MasterApp <> "2" Then
                                    MasterApplication = lbLinkApplications.Items.Item(i)
                                    MasterApp = temp
                                End If
                            Case Is = "20", Is = "15", Is = "9", Is = "11", Is = "12"
                                If MasterApp <> "22" Or MasterApp <> "16" Or MasterApp <> "17" Or MasterApp <> "21" Or MasterApp <> "2" _
                                  Or MasterApp <> "20" Or MasterApp <> "15" Or MasterApp <> "9" Or MasterApp <> "11" Or MasterApp <> "12" Then
                                    MasterApplication = lbLinkApplications.Items.Item(i)
                                    MasterApp = temp
                                End If
                            Case Else
                                MasterApp = MasterApp
                        End Select
                    End If
                Next

                temp = ""
                For i = 0 To lbLinkApplications.Items.Count - 1
                    temp = temp & "strApplicationNumber = '" & lbLinkApplications.Items.Item(i) & "' or "
                Next
                If temp <> "" Then
                    temp = Mid(temp, 1, (temp.Length - 4))
                End If

                SQL = "Select distinct(strMasterApplication) as MasterApp " & _
                "from AIRBRANCH.SSPPApplicationLinking " & _
                "where " & temp
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                i = 0
                If recExist = True Then
                    For i = 0 To lbLinkApplications.Items.Count - 1
                        SQL = "Select strApplicationNumber " & _
                        "from AIRBRANCH.SSPPApplicationLinking " & _
                        "where strApplicationNumber = '" & lbLinkApplications.Items.Item(i) & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.SSPPApplicationLinking set " & _
                            "strMasterApplication = '" & MasterApplication & "', " & _
                            "strApplicationNumber = '" & lbLinkApplications.Items.Item(i) & "' " & _
                            "where strApplicationnumber = '" & lbLinkApplications.Items.Item(i) & "' "
                        Else
                            SQL = "Insert into AIRBRANCH.SSPPApplicationLinking " & _
                            "values " & _
                            "('" & MasterApplication & "', '" & lbLinkApplications.Items.Item(i) & "') "
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                Else
                    For i = 0 To lbLinkApplications.Items.Count - 1
                        SQL = "Insert into AIRBRANCH.SSPPApplicationLinking " & _
                        "values " & _
                        "('" & MasterApplication & "', '" & lbLinkApplications.Items.Item(i) & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                End If
                lblLinkWarning.Visible = True
                MsgBox("Applications Linked", MsgBoxStyle.Information, "Application Tracking Log")
            Else
                MsgBox("A minimum of two applications are needed in order to link an application.", MsgBoxStyle.Information, "Application Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, SQL & vbCrLf & txtAIRSNumber.Text, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ClearApplicationLinks()
        Dim MasterLink As String = ""

        Try

            If txtMasterApp.Text <> "" Then
                SQL = "Select strMasterApplication " & _
                "from AIRBRANCH.SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & txtMasterApp.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    MasterLink = dr.Item("strMasterApplication")
                End If
                dr.Close()
                If MasterLink <> "" Then
                    SQL = "Delete AIRBRANCH.SSPPApplicationLinking " & _
                    "where strMasterApplication = '" & MasterLink & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Read()
                    dr.Close()

                    MasterLink = ""
                    txtMasterApp.Clear()
                    txtMasterAppLock.Text = ""
                    txtApplicationCount.Text = ""
                    lbLinkApplications.Items.Clear()
                    MsgBox("Applications Unlinked", MsgBoxStyle.Information, "Application Tracking Log")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveWebPublisherData()
        Dim EPAStatesNotifiedAppRec As String
        Dim DraftOnWeb As String
        Dim EPAStatesNotified As String
        Dim FinalOnWeb As String
        Dim EPANotifiedPermitOnWeb As String
        Dim EffectiveDateOnPermit As String
        Dim TargetedComments As String
        Dim ExperationDate As String
        Dim PNExpires As String

        Try


            If DTPNotifiedAppReceived.Checked = True Then
                EPAStatesNotifiedAppRec = DTPNotifiedAppReceived.Text
            Else
                EPAStatesNotifiedAppRec = ""
            End If
            If DTPDraftOnWeb.Checked = True Then
                DraftOnWeb = DTPDraftOnWeb.Text
            Else
                DraftOnWeb = ""
            End If
            If DTPEPAStatesNotified.Checked = True Then
                EPAStatesNotified = Me.DTPEPAStatesNotified.Text
            Else
                EPAStatesNotified = ""
            End If
            If DTPFinalOnWeb.Checked = True Then
                FinalOnWeb = DTPFinalOnWeb.Text
            Else
                FinalOnWeb = ""
            End If
            If DTPEPANotifiedPermitOnWeb.Checked = True Then
                EPANotifiedPermitOnWeb = DTPEPANotifiedPermitOnWeb.Text
            Else
                EPANotifiedPermitOnWeb = ""
            End If
            If DTPEffectiveDateofPermit.Checked = True Then
                EffectiveDateOnPermit = DTPEffectiveDateofPermit.Text
            Else
                EffectiveDateOnPermit = ""
            End If
            If DTPExperationDate.Checked = True Then
                ExperationDate = DTPExperationDate.Text
            Else
                ExperationDate = ""
            End If
            If txtEPATargetedComments.Text <> "" Then
                TargetedComments = Replace(txtEPATargetedComments.Text, "'", "''")
                TargetedComments = Mid(TargetedComments, 1, 4000)
            Else
                TargetedComments = ""
            End If
            If DTPPNExpires.Checked = True Then
                PNExpires = DTPPNExpires.Text
            Else
                PNExpires = ""
            End If

            If txtApplicationNumber.Text <> "" Then
                SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                "datDraftOnWeb = '" & DraftOnWeb & "', " & _
                "datEPAStatesNotified = '" & EPAStatesNotified & "', " & _
                "datFinalOnWeb = '" & FinalOnWeb & "', " & _
                "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " & _
                "datEffective = '" & EffectiveDateOnPermit & "', " & _
                "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "',  " & _
                "datExperationDate = '" & ExperationDate & "', " & _
                "datPNExpires = '" & PNExpires & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                "strTargeted = '" & TargetedComments & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If lblLinkWarning.Visible = True Then
                    Dim LinkedApplication As String
                    Dim i As Integer

                    For i = 0 To lbLinkApplications.Items.Count - 1
                        If lbLinkApplications.Items.Item(i) <> txtApplicationNumber.Text Then
                            LinkedApplication = lbLinkApplications.Items.Item(i)
                        Else
                            LinkedApplication = ""
                        End If
                        If LinkedApplication <> "" Then
                            SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                            "datDraftOnWeb = '" & DraftOnWeb & "', " & _
                            "datEPAStatesNotified = '" & EPAStatesNotified & "', " & _
                            "datFinalOnWeb = '" & FinalOnWeb & "', " & _
                            "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " & _
                            "datEffective = '" & EffectiveDateOnPermit & "', " & _
                            "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "',  " & _
                            "datExperationDate = '" & ExperationDate & "', " & _
                            "datPNExpires = '" & PNExpires & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                            "strTargeted = '" & TargetedComments & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    Next
                End If

                MsgBox("Web Information Saved", MsgBoxStyle.Information, "Application Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub GenerateAFSEntry()
        Dim ActionNumber As String = ""
        Dim UpdateStatus As String

        Try

            SQL = "Select " & _
            "strUpdateStatus " & _
            "from AIRBRANCH.AFSSSPPRecords " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                UpdateStatus = dr.Item("strUpdateStatus")
            Else
                UpdateStatus = "A"
            End If
            dr.Close()

            If UpdateStatus = "N" Then
                UpdateStatus = "C"
            Else
                UpdateStatus = UpdateStatus
            End If

            If recExist = True Then
                SQL = "Update AIRBRANCH.AFSSSPPRecords set " & _
                "strUpdateStatus = '" & UpdateStatus & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Close()
            Else
                SQL = "Select strAFSActionNumber " & _
                "from AIRBRANCH.APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    ActionNumber = dr.Item("strAFSActionNumber")
                End While
                dr.Close()
                
                SQL = "Insert into AIRBRANCH.AFSSSPPRecords " & _
                "(strApplicationNumber, strAFSActionNumber, " & _
                "strUpDateStatus, strModifingPerson, " & _
                "datModifingDate) " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', '" & ActionNumber & "', " & _
                "'" & UpdateStatus & "', '" & CurrentUser.UserID & "', " & _
                "'" & OracleDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                ActionNumber = CInt(ActionNumber) + 1

                SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                "strAFSActionNumber = '" & ActionNumber & "' " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub UpdateAPBTables()
        Dim FacilityName As String = ""
        Dim FacilityStreet1 As String = ""
        Dim FacilityStreet2 As String = ""
        Dim City As String = ""
        Dim ZipCode As String = ""
        Dim OpStatus As String = ""
        Dim Classification As String = ""
        Dim AirProgramCodes As String = ""
        Dim SICCode As String = ""
        Dim NAICSCode As String = ""
        Dim PlantDescription As String = ""
        Dim StateProgramCodes As String = ""
        Dim temp As String = "0"
        Dim Subpart As String = ""

        Try

            SQL = "Select " & _
            "strFacilityName, strFacilityStreet1, " & _
            "strFacilityStreet2, strFacilityCity, " & _
            "strFacilityState, strFacilityZipCode, " & _
            "strOperationalStatus, strClass, " & _
            "strAIRProgramCodes, strSICCode, " & _
            "strNAICSCode, " & _
            "strPermitNumber, strPlantDescription, " & _
            "strStateProgramCodes " & _
            "from AIRBRANCH.SSPPApplicationData " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = "N/A"
                Else
                    FacilityName = Apb.Facilities.Facility.SanitizeFacilityNameForDb(dr.Item("strFacilityName"))
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    FacilityStreet1 = "N/A"
                Else
                    FacilityStreet1 = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityStreet2")) Then
                    FacilityStreet2 = "N/A"
                Else
                    FacilityStreet2 = dr.Item("strFacilityStreet2")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    City = "N/A"
                Else
                    City = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    ZipCode = "00000"
                Else
                    ZipCode = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    OpStatus = "O"
                Else
                    OpStatus = dr.Item("strOperationalStatus")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = "B"
                Else
                    Classification = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AirProgramCodes = "000000000000000"
                Else
                    AirProgramCodes = dr.Item("strAIRProgramCodes")
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    SICCode = "0000"
                Else
                    SICCode = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    NAICSCode = "000000"
                Else
                    NAICSCode = dr.Item("strNAICSCode")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    PlantDescription = ""
                Else
                    PlantDescription = dr.Item("strPlantDescription")
                End If
                If IsDBNull(dr.Item("strStateProgramCodes")) Then
                    StateProgramCodes = "00000"
                Else
                    StateProgramCodes = dr.Item("strStateProgramCodes")
                End If
            End While
            dr.Close()

            temp = "1"

            SQL = "Update AIRBRANCH.APBFacilityInformation set " & _
            "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', " & _
            "strFacilityStreet1 = '" & Replace(FacilityStreet1, "'", "''") & "', " & _
            "strFacilityStreet2 = '" & Replace(FacilityStreet2, "'", "''") & "', " & _
            "strFacilityCity = '" & Replace(City, "'", "''") & "', " & _
            "strFacilityZipCode = '" & ZipCode & "', " & _
            "strComments = 'Updated by " & CurrentUser.AlphaName & ", through Permitting Action.', " & _
            "strModifingLocation = '1', " & _
            "strModifingPerson = '" & CurrentUser.UserID & "', " & _
            "datModifingdate = '" & OracleDate & "' " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update AIRBRANCH.OLAPUserAccess set " & _
            "strFacilityName = '" & Replace(FacilityName, "'", "''") & "' " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            temp = "2"

            SQL = "Update AIRBRANCH.APBHeaderData set " & _
            "strOperationalStatus = '" & OpStatus & "', " & _
            "strClass = '" & Classification & "', " & _
            "strAIRProgramCodes = '" & AirProgramCodes & "', " & _
            "strSICCode = '" & SICCode & "', " & _
            "strNAICSCode = '" & NAICSCode & "', " & _
            "strPlantDescription = '" & Replace(PlantDescription, "'", "''") & "', " & _
            "strStateProgramCodes = '" & StateProgramCodes & "', " & _
            "strComments = 'Updated by " & CurrentUser.AlphaName & ", through Permitting Action.', " & _
            "strModifingLocation = '1', " & _
            "strModifingPerson = '" & CurrentUser.UserID & "', " & _
            "datModifingDate = '" & OracleDate & "' " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If AirProgramCodes <> "000000000000000" Then
                If Mid(AirProgramCodes, 1, 1) = "1" Then
                    temp = "3"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "0' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "4"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "0', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                        "strOperationalStatus = '" & OpStatus & "' " & _
                        "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "0' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "0' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "0'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                    For i As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                        Subpart = dgvSIPSubPartDelete(0, i).Value

                        SQL = "Update AIRBRANCH.APBSubpartData set " & _
                        "Active = '0', " & _
                        "updateUser = '" & CurrentUser.UserID & "', " & _
                        "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
                        "and strSubpart = '" & Subpart & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next

                    For i As Integer = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                        Subpart = dgvSIPSubpartAddEdit(0, i).Value

                        SQL = "Select " & _
                        "Active " & _
                        "from AIRBRANCH.APBSubpartData " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
                        "and strSubpart = '" & Subpart & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.APBSubpartData set " & _
                            "Active = '1', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
                            "and strSubpart = '" & Subpart & "' "
                        Else
                            SQL = "INSERT INTO AIRBRANCH.APBSUBPARTDATA " & _
                            "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " & _
                            "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " & _
                            "  ) VALUES " & _
                            "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "0', " & _
                            "'" & Replace(Subpart, "'", "''") & "', '" & CurrentUser.UserID & "', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')), '1', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                Else
                    SQL = "Update AIRBRANCH.APBSubPartData set " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "UpdateDateTime = '" & OracleDate & "', " & _
                    "Active = '0' " & _
                    "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If Mid(AirProgramCodes, 2, 1) = "1" Then
                    temp = "5"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "1' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "6"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "1', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "1' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "1' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "1'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 3, 1) = "1" Then
                    temp = "7"
                    SQL = "Select strPollutantKey " & _
                     "from AIRBRANCH.APBAirProgramPollutants " & _
                     "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "3' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "8 "
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "3', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "3' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "3' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "3'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 4, 1) = "1" Then
                    temp = "9"
                    SQL = "Select strPollutantKey " & _
                     "from AIRBRANCH.APBAirProgramPollutants " & _
                     "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "4' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "10"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "4', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "4' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "4' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "4'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 5, 1) = "1" Then
                    temp = "11"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "6' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "12"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "6', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "6' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "6' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "6'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 6, 1) = "1" Then
                    temp = "13"
                    SQL = "Select strPollutantKey " & _
                  "from AIRBRANCH.APBAirProgramPollutants " & _
                  "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "7' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "14"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "7', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "7' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "7' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "7'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 7, 1) = "1" Then
                    temp = "15"
                    SQL = "Select strPollutantKey " & _
                     "from AIRBRANCH.APBAirProgramPollutants " & _
                     "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "8' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "16"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "8', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "8' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "8' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "8'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                    For i As Integer = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                        Subpart = dgvNESHAPSubPartDelete(0, i).Value

                        SQL = "Update AIRBRANCH.APBSubpartData set " & _
                        "Active = '0', " & _
                        "updateUser = '" & CurrentUser.UserID & "', " & _
                        "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
                        "and strSubpart = '" & Subpart & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next

                    For i As Integer = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                        Subpart = dgvNESHAPSubpartAddEdit(0, i).Value

                        SQL = "Select " & _
                        "Active " & _
                        "from AIRBRANCH.APBSubpartData " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
                        "and strSubpart = '" & Subpart & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.APBSubpartData set " & _
                            "Active = '1', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
                            "and strSubpart = '" & Subpart & "' "
                        Else
                            SQL = "INSERT INTO AIRBRANCH.APBSUBPARTDATA " & _
                            "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " & _
                            "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " & _
                            "  ) VALUES " & _
                            "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "8', " & _
                            "'" & Replace(Subpart, "'", "''") & "', '" & CurrentUser.UserID & "', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')), '1', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) ) "
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                Else
                    SQL = "Update AIRBRANCH.APBSubPartData set " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "UpdateDateTime = '" & OracleDate & "', " & _
                    "Active = '0' " & _
                    "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If Mid(AirProgramCodes, 8, 1) = "1" Then
                    temp = "17"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "9' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "18"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "9' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "9' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "9'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                    For i As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                        Subpart = dgvNSPSSubPartDelete(0, i).Value

                        SQL = "Update AIRBRANCH.APBSubpartData set " & _
                        "Active = '0', " & _
                        "updateUser = '" & CurrentUser.UserID & "', " & _
                        "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                        "and strSubpart = '" & Subpart & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next

                    For i As Integer = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                        Subpart = dgvNSPSSubpartAddEdit(0, i).Value

                        SQL = "Select " & _
                        "Active " & _
                        "from AIRBRANCH.APBSubpartData " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                        "and strSubpart = '" & Subpart & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.APBSubpartData set " & _
                            "Active = '1', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                            "and strSubpart = '" & Subpart & "' "
                        Else
                            SQL = "INSERT INTO AIRBRANCH.APBSUBPARTDATA " & _
                            "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " & _
                            "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " & _
                            "  ) VALUES " & _
                            "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                            "'" & Replace(Subpart, "'", "''") & "', '" & CurrentUser.UserID & "', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')), '1', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) )"
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                Else
                    SQL = "Update AIRBRANCH.APBSubPartData set " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "UpdateDateTime = '" & OracleDate & "', " & _
                    "Active = '0' " & _
                    "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If Mid(AirProgramCodes, 9, 1) = "1" Then
                    temp = "19"
                    SQL = "Select strPollutantKey " & _
                     "from AIRBRANCH.APBAirProgramPollutants " & _
                     "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "F' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "20"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "F', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "F' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "F' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "F'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 10, 1) = "1" Then
                    temp = "21"
                    SQL = "Select strPollutantKey " & _
                   "from AIRBRANCH.APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "A' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "22"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "A', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "A' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "A' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "A'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 11, 1) = "1" Then
                    temp = "23"
                    SQL = "Select strPollutantKey " & _
                   "from AIRBRANCH.APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "I' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "24"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "I', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "I' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "I' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "I'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If
                If Mid(AirProgramCodes, 12, 1) = "1" Then
                    temp = "25"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "M' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "26"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "M', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "M' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "M' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "M' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                    For i As Integer = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                        Subpart = dgvMACTSubPartDelete(0, i).Value

                        SQL = "Update AIRBRANCH.APBSubpartData set " & _
                        "Active = '0', " & _
                        "updateUser = '" & CurrentUser.UserID & "', " & _
                        "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
                        "and strSubpart = '" & Subpart & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next

                    For i As Integer = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                        Subpart = dgvMACTSubpartAddEdit(0, i).Value

                        SQL = "Select " & _
                        "Active " & _
                        "from AIRBRANCH.APBSubpartData " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
                        "and strSubpart = '" & Subpart & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.APBSubpartData set " & _
                            "Active = '1', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
                            "and strSubpart = '" & Subpart & "' "
                        Else
                            SQL = "INSERT INTO AIRBRANCH.APBSUBPARTDATA " & _
                            "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " & _
                            "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " & _
                            "  ) VALUES " & _
                            "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "M', " & _
                            "'" & Replace(Subpart, "'", "''") & "', '" & CurrentUser.UserID & "', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')), '1', " & _
                            "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) )"
                        End If
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Next
                Else
                    SQL = "Update AIRBRANCH.APBSubPartData set " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "UpdateDateTime = '" & OracleDate & "', " & _
                    "Active = '0' " & _
                    "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If Mid(AirProgramCodes, 13, 1) = "1" Then
                    temp = "27"
                    SQL = "Select strPollutantKey " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "V' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        temp = "28"
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "V', " & _
                         "'OT', 'C', " & _
                         "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                       "strOperationalStatus = '" & OpStatus & "' " & _
                       "where strAirPOllutantKey = '0413" & txtAIRSNumber.Text & "V' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()

                        SQL = "Select strUpdateStatus " & _
                        "from AIRBRANCH.AFSAirPollutantData " & _
                        "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "V' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            temp = dr.Item("strUpdateStatus")
                        End While
                        dr.Close()
                        If temp = "N" Then
                            SQL = "update AIRBRANCH.AFSAirPollutantData set " & _
                            "strUpdateStatus = 'C' " & _
                            "where strAIRPollutantKey = '0413" & txtAIRSNumber.Text & "V'"
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, SQL & vbCrLf & temp & vbCrLf & txtAIRSNumber.Text & vbCrLf & txtApplicationNumber.Text, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DisplayPermitPanel()
        Try
            '130, 307

            If rdbTitleVPermit.Checked = True Then
                PanelTitleV.Visible = True
                PanelPSD.Visible = False
                PanelOther.Visible = False
                PanelTitleV.Location = New System.Drawing.Point(100, 25)
            Else
                PanelTitleV.Visible = False
                If rdbPSDPermit.Checked = True Then
                    PanelTitleV.Visible = False
                    PanelPSD.Visible = True
                    PanelOther.Visible = False
                    PanelPSD.Location = New System.Drawing.Point(100, 25)
                Else
                    If rdbOtherPermit.Checked = True Then
                        PanelTitleV.Visible = False
                        PanelPSD.Visible = False
                        PanelOther.Visible = True
                        PanelOther.Location = New System.Drawing.Point(100, 25)
                    Else
                        PanelTitleV.Visible = False
                        PanelPSD.Visible = False
                        PanelOther.Visible = False
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FindMasterApp()
        Dim temp As String = ""
        Dim AppType As String = ""

        Try
            AppType = cboApplicationType.Text

            SQL = "select strMasterApplication " & _
              "from AIRBRANCH.SSPPApplicationLinking " & _
              "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MasterApp = dr.Item("strMasterApplication")
            Else
                MasterApp = txtApplicationNumber.Text
            End If
            dr.Close()

            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

            SQL = "select " & _
            "distinct(AIRBRANCH.APBPermits.strFileName)  " & _
            "from AIRBRANCH.APBpermits, AIRBRANCH.SSPPApplicationLinking " & _
            "where substr(AIRBRANCH.APBpermits.strFileName, 4) = AIRBRANCH.SSPPAPPlicationLinking.strmasterapplication (+) " & _
            "and (AIRBRANCH.SSPPApplicationLinking.strApplicationNumber = '" & MasterApp & "' " & _
            "or AIRBRANCH.APBPermits.strFileName like '%-" & MasterApp & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                temp = Mid(dr.Item("strFileName"), 1, 1)
                Select Case temp
                    Case "V"
                        rdbTitleVPermit.Checked = True
                    Case "P"
                        rdbPSDPermit.Checked = True
                    Case "O"
                        rdbOtherPermit.Checked = True
                    Case Else
                        rdbOtherPermit.Checked = True
                End Select
                lblPermitNumber.Visible = False
                llbPermitNumber.Visible = True
            Else
                Select Case AppType
                    Case "SM(TV)", "TV-Initial", "TV-Renewal", "TV-Amend", "Title V"
                        rdbTitleVPermit.Checked = True
                    Case "PSD", "SAW", "SAWO", "MAW"
                        rdbPSDPermit.Checked = True
                    Case ""
                        rdbTitleVPermit.Checked = False
                        rdbPSDPermit.Checked = False
                        rdbOtherPermit.Checked = False
                    Case Else
                        rdbOtherPermit.Checked = True
                End Select
                lblPermitNumber.Visible = True
                llbPermitNumber.Visible = False
            End If
            dr.Close()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub UploadFile(ByVal FileName As String, ByVal DocLocation As String, ByVal PDFLocation As String, ByVal DocOnFile As String)
        Try
            Dim Flag As String = "00"
            Dim DocFile As String = ""
            Dim ResultDoc As DialogResult
            Dim PDFFile As String = ""
            Dim ResultPDF As DialogResult

            SQL = "Select " & _
            "strDOCFileSize, strPDFFileSize " & _
            "From AIRBRANCH.ApbPermits " & _
            "where strFileName = '" & FileName & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strDocFileSize")) Then
                    DocFile = ""
                Else
                    DocFile = dr.Item("strDocFileSize")
                End If
                If IsDBNull(dr.Item("strPDFFileSize")) Then
                    PDFFile = ""
                Else
                    PDFFile = dr.Item("strPDFFileSize")
                End If
            Else
                DocFile = ""
                PDFFile = ""
            End If
            dr.Close()
            If DocFile <> "" And DocLocation <> "" Then
                Select Case Mid(FileName, 1, 2)
                    Case "VN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Draft Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Public Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Final Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PA"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Application Summary." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Preliminary Determination." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PT"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Draft Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Public Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PH"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Hearing Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Determination." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PI"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "ON"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "OP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case Else
                        ResultDoc = MessageBox.Show("A Word file currently exists for this 'Unknown' application." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                End Select
                Select Case ResultDoc
                    Case DialogResult.Yes
                        Flag = "10"
                    Case DialogResult.No
                        Flag = "00"
                    Case DialogResult.Cancel
                        Flag = "00"
                    Case Else
                        Flag = "00"
                End Select
            Else
                If DocLocation <> "" Then
                    Flag = "10"
                Else
                    Flag = "00"
                End If
            End If
            If (PDFFile <> "" And Mid(Flag, 1, 1) = "1") Or DocOnFile = "On File" Then
                SQL = "update AIRBRANCH.APBPermits set " & _
                "PDFPermitData = '', " & _
                "strPDFFileSize = '', " & _
                "strPDFModifingPerson = '', " & _
                "datPDFModifingDate = '' " & _
                "where strFileName = '" & FileName & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()
            Else
                If PDFFile <> "" And PDFLocation <> "" Then
                    Select Case Mid(FileName, 1, 2)
                        Case "VN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Draft Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Public Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Final Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PA"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Application Summary." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Preliminary Determination." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PT"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Draft Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Public Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PH"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Hearing Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Determination." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PI"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "ON"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "OP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case Else
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this 'Unknown' application." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    End Select
                    Select Case ResultPDF
                        Case DialogResult.Yes
                            Flag = Mid(Flag, 1, 1) & "1"
                        Case DialogResult.No
                            Flag = Mid(Flag, 1, 1) & "0"
                        Case DialogResult.Cancel
                            Flag = Mid(Flag, 1, 1) & "0"
                        Case Else
                            Flag = Mid(Flag, 1, 1) & "0"
                    End Select
                Else
                    If PDFLocation <> "" Then
                        Flag = Mid(Flag, 1, 1) & "1"
                    Else
                        Flag = Mid(Flag, 1, 1) & "0"
                    End If
                End If
            End If
            If Flag <> "00" Then
                Dim rowCount As String = ""
                Dim da As OracleDataAdapter
                Dim ds As DataSet

                SQL = "Delete AIRBRANCH.APBPermits " & _
                   "where strFileName = '" & FileName & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "rowCount " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = '" & FileName & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("rowCount")) Then
                        rowCount = ""
                    Else
                        rowCount = dr.Item("RowCount")
                    End If
                End While
                dr.Close()

                If rowCount = "" Then
                    SQL = "select " & _
                    "(max(rowCount) + 1) as RowCount " & _
                    "from AIRBRANCH.APBPermits "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("RowCount")) Then
                            rowCount = "1"
                        Else
                            rowCount = dr.Item("RowCount")
                        End If
                    End While
                    dr.Close()
                End If

                Dim fs As FileStream
                If DocLocation <> "" And Mid(Flag, 1, 1) = "1" Then
                    fs = New FileStream(DocLocation, FileMode.OpenOrCreate, FileAccess.Read)
                Else
                    fs = New FileStream(PDFLocation, FileMode.OpenOrCreate, FileAccess.Read)
                End If

                Dim rawData() As Byte = New Byte(fs.Length) {}
                fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length))
                fs.Close()

                SQL = "Select * from AIRBRANCH.APBPermits " & _
                "where strFileName = '" & FileName & "' "
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da = New OracleDataAdapter(SQL, CurrentConnection)
                Dim cmdCB As OracleCommandBuilder = New OracleCommandBuilder(da)
                ds = New DataSet("PDF")
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                da.Fill(ds, "PDF")
                Dim row As DataRow = ds.Tables("PDF").NewRow()
                row("rowCount") = rowCount
                row("strFileName") = FileName
                If DocLocation <> "" And Mid(Flag, 1, 1) = "1" Then
                    row("docPermitData") = rawData
                    row("strDocFileSize") = rawData.Length
                    row("strDocModifingPerson") = CurrentUser.UserID
                    row("datDocModifingDate") = OracleDate
                Else
                    row("pdfPermitData") = rawData
                    row("strPDFFileSize") = rawData.Length
                    row("strPDFModifingPerson") = CurrentUser.UserID
                    row("datPDFModifingDate") = OracleDate
                End If
                ds.Tables("PDF").Rows.Add(row)
                da.Update(ds, "PDF")

                MsgBox("Done", MsgBoxStyle.Information, "Permit Uploader")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DownloadFile(ByVal FileName As String, ByVal FileType As String)
        Try
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"

            If FileType <> "00" Then

                Select Case FileType
                    Case "10"
                        'path.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = FileName
                        path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                        path.FilterIndex = 1
                        path.DefaultExt = ".doc"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If
                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "DocPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()

                        End If
                    Case "01"
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = FileName
                        path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                        path.FilterIndex = 1
                        path.DefaultExt = ".pdf"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If

                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "pdfPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()

                        End If
                    Case "11"
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = FileName
                        path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                        path.FilterIndex = 1
                        path.DefaultExt = ".doc"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If
                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "DocPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()

                        End If
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = FileName
                        path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                        path.FilterIndex = 1
                        path.DefaultExt = ".pdf"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If

                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "pdfPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()

                        End If
                    Case Else
                End Select
                If DestFilePath <> "N/A" Then
                    Diagnostics.Process.Start(DestFilePath)
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
#Region "Clears"
    Sub ClearApplicationData()
        Try
             
            txtApplicationNumber.Clear()
            txtAIRSNumber.Clear()
            txtOutstandingApplication.Clear()
            chbClosedOut.Checked = False
            DTPFinalizedDate.Text = OracleDate
            DTPFinalizedDate.Visible = False
            rtbFacilityInformation.Clear()
            cboEngineer.SelectedIndex = 0
            cboApplicationUnit.SelectedIndex = 0
            cboApplicationType.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Sub ClearApplicationTab()
        Try
             
            txtFacilityName.Clear()
            txtFacilityStreetAddress.Clear()
            cboFacilityCity.SelectedIndex = 0
            txtFacilityZipCode.Clear()
            cboCounty.SelectedIndex = 0
            txtSICCode.Clear()
            txtNAICSCode.Clear()
            cboOperationalStatus.Text = ""
            cboClassification.Text = ""
            chbCDS_0.Checked = True
            chbCDS_6.Checked = False
            chbCDS_7.Checked = False
            chbCDS_8.Checked = False
            chbCDS_9.Checked = False
            chbCDS_M.Checked = False
            chbCDS_V.Checked = False
            chbCDS_A.Checked = False
            chbCDS_RMP.Checked = False

            txtPlantDescription.Clear()
            DTPDateSent.Text = OracleDate
            DTPDateReceived.Text = OracleDate
            DTPDateAssigned.Checked = False
            DTPDateAssigned.Text = OracleDate
            DTPDateReassigned.Checked = False
            DTPDateReassigned.Text = OracleDate
            DTPDateAcknowledge.Checked = False
            DTPDateAcknowledge.Text = OracleDate
            DTPDatePAExpires.Checked = False
            DTPDatePAExpires.Text = OracleDate
            DTPDatePNExpires.Checked = False
            DTPDatePNExpires.Text = OracleDate
            DTPDeadline.Checked = False
            DTPDeadline.Text = OracleDate
            DTPDateToUC.Checked = False
            DTPDateToUC.Text = OracleDate
            DTPDateToPM.Checked = False
            DTPDateToPM.Text = OracleDate
            DTPFinalAction.Checked = False
            DTPFinalAction.Text = OracleDate
            DTPDraftIssued.Checked = False
            DTPDraftIssued.Text = OracleDate
            txtPermitNumber.Clear()
            cboPermitAction.SelectedIndex = 0
            cboPublicAdvisory.Text = ""
            txtReasonAppSubmitted.Clear()
            txtComments.Clear()
            txtSignificantComments.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Sub ClearFacilityApplicationHistoryTab()
        Try
             
            'dgrFacilityAppHistory()
            txtHistoryAppComments.Clear()
            txtHistoryComments.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Sub ClearInformationRequestedTab()
        Try
             
            'dgrInformationRequested()
            DTPInformationRequested.Text = OracleDate
            DTPInformationReceived.Text = OracleDate
            txtInformationRequested.Clear()
            txtInformationReceived.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Sub ClearReviewTab()
        Try
             
            DTPReviewSubmitted.Text = OracleDate
            cboSSCPStaff.SelectedIndex = 0
            DTPSSCPReview.Text = OracleDate
            rdbSSCPYes.Checked = False
            rdbSSCPNo.Checked = False
            txtSSCPComments.Clear()
            cboISMPStaff.SelectedIndex = 0
            DTPISMPReview.Text = OracleDate
            rdbISMPYes.Checked = False
            rdbISMPNo.Checked = False
            txtISMPComments.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
#End Region
#Region "Check Box Changes"
    Private Sub chbClosedOut_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbClosedOut.CheckedChanged
        Try
             
            If chbClosedOut.Checked = True Then
                CloseOutApplication("True")
                DTPFinalizedDate.Text = OracleDate
                DTPFinalizedDate.Visible = False
            Else
                CloseOutApplication("False")
                DTPFinalizedDate.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
#End Region
#Region "Delarations"
    Private Sub txtApplicationNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtApplicationNumber.TextChanged
        Try
             
            'If txtApplicationNumber.Text <> "" Then
            '     
            '    LoadApplicationData()
            '    LoadMiscData()
            '    LoadContactData()

            '    If dsFacAppHistory Is Nothing Then
            '    Else
            '        dsFacAppHistory.Clear()
            '    End If
            '    If dsFacInfoHistory Is Nothing Then
            '    Else
            '        dsFacInfoHistory.Clear()
            '    End If
            '     
            'End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub txtPermitNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPermitNumber.TextChanged
        Dim PermitNumber As String
        Dim SICCode As String
        Dim AIRSNumber As String

        Try
            If AccountFormAccess(51, 3) = "0" And AccountFormAccess(51, 4) = "0" Then
                If txtPermitNumber.Text <> "" Then
                    If cboApplicationType.Text = "ERC" Then
                        If Mid(txtPermitNumber.Text, 1, 3) <> "ERC" Then
                            txtPermitNumber.Text = "ERC"
                        Else
                            txtPermitNumber.Text = txtPermitNumber.Text
                        End If
                    Else
                        If txtSICCode.Text.Length = 4 Then
                            SICCode = txtSICCode.Text
                        Else
                            SICCode = ""
                        End If
                        If txtAIRSNumber.Text.Length = 8 Then
                            AIRSNumber = Mid(txtAIRSNumber.Text, 1, 3) & "-" & Mid(txtAIRSNumber.Text, 5, 4)
                        Else
                            AIRSNumber = ""
                        End If
                        PermitNumber = SICCode & "-" & AIRSNumber & "-"

                        If Mid(txtPermitNumber.Text, 1, 14) <> PermitNumber And SICCode <> "" And AIRSNumber <> "" Then
                            txtPermitNumber.Text = txtSICCode.Text & "-" & Mid(txtAIRSNumber.Text, 1, 3) & "-" & Mid(txtAIRSNumber.Text, 5, 4) & "-"
                        End If
                    End If
                End If
            Else
                If txtPermitNumber.Text = "" Then
                    If cboApplicationType.Text = "ERC" Then
                        If Mid(txtPermitNumber.Text, 1, 3) <> "ERC" Then
                            txtPermitNumber.Text = "ERC"
                        Else
                            txtPermitNumber.Text = txtPermitNumber.Text
                        End If
                    Else
                        If txtSICCode.Text.Length = 4 Then
                            SICCode = txtSICCode.Text
                        Else
                            SICCode = ""
                        End If
                        If txtAIRSNumber.Text.Length = 8 Then
                            AIRSNumber = Mid(txtAIRSNumber.Text, 1, 3) & "-" & Mid(txtAIRSNumber.Text, 5, 4)
                        Else
                            AIRSNumber = ""
                        End If
                        PermitNumber = SICCode & "-" & AIRSNumber & "-"

                        If Mid(txtPermitNumber.Text, 1, 14) <> PermitNumber And SICCode <> "" And AIRSNumber <> "" Then
                            txtPermitNumber.Text = txtSICCode.Text & "-" & Mid(txtAIRSNumber.Text, 1, 3) & "-" & Mid(txtAIRSNumber.Text, 5, 4) & "-"
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveInformationRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveInformationRequest.Click
        Try
             
            SaveInformationRequest()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnDeleteInformationRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteInformationRequest.Click
        Try
             
            DeleteInformationRequest()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnClearInformationRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInformationRequest.Click
        Try
             
            txtInformationRequestedKey.Clear()
            DTPInformationRequested.Text = OracleDate
            DTPInformationRequested.Checked = False
            txtInformationRequested.Clear()
            DTPInformationReceived.Text = OracleDate
            DTPInformationReceived.Checked = False
            txtInformationReceived.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbSSCPYes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSSCPYes.CheckedChanged
        Try
             
            If rdbSSCPYes.Checked = True Then
                txtSSCPComments.Enabled = True
                txtSSCPComments.ReadOnly = False
            Else
                txtSSCPComments.Enabled = False
                txtSSCPComments.ReadOnly = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbSSCPNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSSCPNo.CheckedChanged
        Try
             
            If rdbSSCPNo.Checked = True Then
                txtSSCPComments.Enabled = False
                txtSSCPComments.ReadOnly = True
            Else
                txtSSCPComments.Enabled = True
                txtSSCPComments.ReadOnly = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbISMPYes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbISMPYes.CheckedChanged
        Try
             
            If rdbISMPYes.Checked = True Then
                txtISMPComments.Enabled = True
                txtISMPComments.ReadOnly = False
            Else
                txtISMPComments.Enabled = False
                txtISMPComments.ReadOnly = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbISMPNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbISMPNo.CheckedChanged
        Try
             
            If rdbISMPNo.Checked = True Then
                txtISMPComments.Enabled = False
                txtISMPComments.ReadOnly = True
            Else
                txtISMPComments.Enabled = True
                txtISMPComments.ReadOnly = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnAddApplicationToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddApplicationToList.Click
        Try
             
            If txtApplicationNumberHistory.Text <> "" And chbClosedOutHistory.Checked = False Then
                If txtMasterApp.Text = "" Then
                    txtMasterApp.Text = txtApplicationNumberHistory.Text
                End If
                If lbLinkApplications.Items.Contains(txtApplicationNumberHistory.Text) Then
                Else
                    lbLinkApplications.Items.Add(txtApplicationNumberHistory.Text)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnClearList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearList.Click
        Try
             
            lbLinkApplications.Items.Clear()
            txtMasterApp.Clear()
            txtMasterAppLock.Clear()
            txtApplicationCount.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnLinkApplications_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkApplications.Click
        Try
             
            LinkApplications()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnClearLinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLinks.Click
        Try
             
            ClearApplicationLinks()
            If lbLinkApplications.Items.Contains(txtApplicationNumber.Text) Then
            Else
                lbLinkApplications.Items.Add(txtApplicationNumber.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub lbLinkApplications_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbLinkApplications.MouseUp
        Try
             
            If txtMasterAppLock.Text = "" Then
                If lbLinkApplications.Items.Count > 0 Then
                    txtMasterApp.Text = lbLinkApplications.SelectedItem
                End If
            Else
                txtMasterApp.Text = txtMasterAppLock.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnLoadFacilityApplicationHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadFacilityApplicationHistory.Click
        Try
             
            LoadFacilityApplicationHistory()

            lbLinkApplications.Items.Clear()
            txtMasterApp.Clear()
            txtMasterAppLock.Clear()
            txtApplicationCount.Clear()

            CheckForLinks()

             
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnViewInformationRequests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewInformationRequests.Click
        Try
             
             
            LoadInformationRequestedHistory()
             
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnSaveWebPublisher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWebPublisher.Click
        Try
             
            SaveWebPublisherData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub

    Private Sub PreSaveCheckThenSave()
        If CurrentUser.ProgramID = 5 _
            Or (AccountFormAccess(51, 1) = "1" And CurrentUser.UnitId = 14) _
            Or AccountFormAccess(51, 3) = "1" _
            Or AccountFormAccess(51, 4) = "1" Then  'SSPP users and Web Users 

            SQL = "Select datModifingDate " & _
            "from AIRBRANCH.SSPPApplicationMaster " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datModifingDate")) Then
                    temp = ""
                Else
                    temp = dr.Item("datModifingDate")
                End If
            End While
            dr.Close()

            If TimeStamp = "" Then
            Else
                If TimeStamp = temp Then
                Else
                    MessageBox.Show("The application has been updated since you last opened it." & vbCrLf & _
                                "Please reopen the application to save any changes." & vbCrLf & vbCrLf & _
                                "NO DATA SAVED", _
                                "Application Tracking Log", MessageBoxButtons.OK)
                    Exit Sub
                End If
            End If
            SaveApplicationData()
        End If

        If DTPReviewSubmitted.Checked = True Then
            SaveApplicationSubmitForReview()
        End If
        If DTPSSCPReview.Checked = True Then
            SaveSSCPReview()
        End If
        If DTPISMPReview.Checked = True Then
            SaveISMPReview()
        End If
        If TCApplicationTrackingLog.Contains(Me.TPContactInformation) Then
            If Me.txtContactFirstName.Text <> "" And txtContactLastName.Text <> "" Then
                SaveApplicationContact()
            End If
        End If

        MsgBox("Application Information Saved.", MsgBoxStyle.Information, "Application Tracking Log")
    End Sub

    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        PreSaveCheckThenSave()
    End Sub
    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub
    Private Sub DTPReviewSubmitted_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPReviewSubmitted.ValueChanged
        Try
            If DTPReviewSubmitted.Checked = True Then
                cboSSCPUnits.Enabled = True
                cboISMPUnits.Enabled = True
            Else
                cboSSCPUnits.Enabled = False
                cboISMPUnits.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub DTPISMPReview_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPISMPReview.ValueChanged
        Try
            If DTPISMPReview.Checked = True Then
                cboISMPStaff.Enabled = True
                rdbISMPYes.Enabled = True
                rdbISMPNo.Enabled = True
                txtISMPComments.Enabled = True
            Else
                cboISMPStaff.Enabled = False
                rdbISMPYes.Enabled = False
                rdbISMPNo.Enabled = False
                txtISMPComments.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub DTPSSCPReview_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPSSCPReview.ValueChanged
        Try
            If DTPSSCPReview.Checked = True Then
                cboSSCPStaff.Enabled = True
                rdbSSCPYes.Enabled = True
                rdbSSCPNo.Enabled = True
                txtSSCPComments.Enabled = True
            Else
                cboSSCPStaff.Enabled = False
                rdbSSCPYes.Enabled = False
                rdbSSCPNo.Enabled = False
                txtSSCPComments.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub dgrFacilityAppHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFacilityAppHistory.MouseUp

        Dim hti As DataGridView.HitTestInfo = dgvFacilityAppHistory.HitTest(e.X, e.Y)
        Dim temp As String = ""

        Try
             

            If dgvFacilityAppHistory.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFacilityAppHistory.Columns(0).HeaderText = "APL #" Then
                    txtApplicationNumberHistory.Text = dgvFacilityAppHistory(0, hti.RowIndex).Value
                    txtHistoryComments.Text = dgvFacilityAppHistory(6, hti.RowIndex).Value
                    txtHistoryAppComments.Text = dgvFacilityAppHistory(7, hti.RowIndex).Value
                    txtEngineerHistory.Text = dgvFacilityAppHistory(2, hti.RowIndex).Value
                    temp = dgvFacilityAppHistory(3, hti.RowIndex).Value
                    If temp = " " Then
                        chbClosedOutHistory.Checked = False
                    Else
                        chbClosedOutHistory.Checked = True
                    End If
                    txtApplicationDatedHistory.Text = dgvFacilityAppHistory(4, hti.RowIndex).Value
                    txtApplicationUnitHistory.Text = dgvFacilityAppHistory(5, hti.RowIndex).Value
                    txtApplicationTypeHistory.Text = dgvFacilityAppHistory(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Private Sub dgvInformationRequested_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInformationRequested.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvInformationRequested.HitTest(e.X, e.Y)
        Dim temp As String = ""

        Try
             

            If dgvInformationRequested.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvInformationRequested.Columns(1).HeaderText = "Request Key" Then
                    txtInformationRequestedKey.Text = dgvInformationRequested(1, hti.RowIndex).Value
                    temp = dgvInformationRequested(2, hti.RowIndex).Value
                    If temp = " " Then
                        DTPInformationRequested.Text = OracleDate
                        DTPInformationRequested.Checked = False
                    Else
                        DTPInformationRequested.Text = temp
                        DTPInformationRequested.Checked = True
                    End If
                    txtInformationRequested.Text = dgvInformationRequested(3, hti.RowIndex).Value
                    temp = dgvInformationRequested(4, hti.RowIndex).Value
                    If temp = " " Then
                        DTPInformationReceived.Text = OracleDate
                        DTPInformationReceived.Checked = False
                    Else
                        DTPInformationReceived.Text = temp
                        DTPInformationReceived.Checked = True
                    End If
                    txtInformationReceived.Text = dgvInformationRequested(5, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub mmiNewApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNewApplication.Click
        Dim ApplicationNum As String = ""

        Try
             
            SQL = "Select AIRBRANCH.SSPPApplicationKey.nextval from dual "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                ApplicationNum = dr.Item(0)
            End While

            txtApplicationNumber.Text = ApplicationNum

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub cboClassification_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClassification.TextChanged
        Try
             

            If cboClassification.Text = "A - MAJOR" Then
                GBOther.Visible = True
            Else
                GBOther.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub cboApplicationType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboApplicationType.SelectedValueChanged
        Try
             

            DTPEPAWaived.Visible = True
            lblEPAWaived.Visible = True
            DTPEPAEnds.Visible = True
            lblEPAEnds.Visible = True
            DTPDraftIssued.Visible = True
            lblDraftIssued.Visible = True
            DTPDatePNExpires.Visible = True
            lblDatePNExpires.Visible = True
            chbPNReady.Visible = True
            DTPDatePAExpires.Visible = True
            lblDatePAExpires.Visible = True
            chbPAReady.Visible = True
            cboPublicAdvisory.Visible = True
            lblPublicAdvisory.Visible = True
            GBSignificationComments.Visible = False

            Select Case cboApplicationType.Text
                Case "502(b)10"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "AA"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "Acid Rain"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "Closed"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    txtPermitNumber.Clear()
                Case "ERC"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 3) <> "ERC" Then
                        txtPermitNumber.Text = " "
                    End If
                Case "MAW"
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If

                    chbPNReady.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                Case "MAWO"
                    cboPublicAdvisory.Visible = False
                    lblPublicAdvisory.Visible = False
                    DTPDatePAExpires.Visible = False
                    lblDatePAExpires.Visible = False
                    chbPAReady.Visible = False
                    chbPNReady.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    DTPEPAWaived.Visible = True
                    lblEPAWaived.Visible = True
                    DTPEPAEnds.Visible = True
                    lblEPAEnds.Visible = True

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "NC"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    chbPNReady.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "OFF PERMIT"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    txtPermitNumber.Clear()

                Case "PBR"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "SAW"
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                    GBSignificationComments.Visible = True
                Case "SAWO"
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                    GBSignificationComments.Visible = True
                Case "SIP"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    If chbPSD.Checked = True Or chbNAANSR.Checked = True Or chb112g.Checked = True Then
                        DTPDraftIssued.Visible = True
                        lblDraftIssued.Visible = True
                        DTPDatePNExpires.Visible = True
                        lblDatePNExpires.Visible = True
                        chbPNReady.Visible = True
                    Else
                        DTPDraftIssued.Visible = False
                        lblDraftIssued.Visible = False
                        DTPDatePNExpires.Visible = False
                        lblDatePNExpires.Visible = False
                        chbPNReady.Visible = False
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "SM"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "TV-Initial"
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case "TV-Renewal"
                    If cboPublicAdvisory.Text = "Not Decided" Or cboPublicAdvisory.Text = "" Or cboPublicAdvisory.Text = "PA Not Needed" Then
                        cboPublicAdvisory.Visible = False
                        lblPublicAdvisory.Visible = False
                        DTPDatePAExpires.Visible = False
                        lblDatePAExpires.Visible = False
                        chbPAReady.Visible = False
                    Else
                        cboPublicAdvisory.Visible = True
                        lblPublicAdvisory.Visible = True
                        DTPDatePAExpires.Visible = True
                        lblDatePAExpires.Visible = True
                        chbPAReady.Visible = True
                    End If
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                Case Else



            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chb112_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb112g.CheckedChanged
        Try
             
            If cboApplicationType.Text = "SIP" Then
                DTPDraftIssued.Visible = True
                lblDraftIssued.Visible = True
                DTPDatePNExpires.Visible = True
                lblDatePNExpires.Visible = True
                chbPNReady.Visible = True
            Else
                DTPDraftIssued.Visible = False
                lblDraftIssued.Visible = False
                DTPDatePNExpires.Visible = False
                lblDatePNExpires.Visible = False
                chbPNReady.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbPSD.CheckedChanged
        Try
             
            If cboApplicationType.Text = "SIP" Then
                DTPDraftIssued.Visible = True
                lblDraftIssued.Visible = True
                DTPDatePNExpires.Visible = True
                lblDatePNExpires.Visible = True
                chbPNReady.Visible = True
            Else
                DTPDraftIssued.Visible = False
                lblDraftIssued.Visible = False
                DTPDatePNExpires.Visible = False
                lblDatePNExpires.Visible = False
                chbPNReady.Visible = False
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbNAANSR_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbNAANSR.CheckedChanged
        Try
             
            If cboApplicationType.Text = "SIP" Then
                DTPDraftIssued.Visible = True
                lblDraftIssued.Visible = True
                DTPDatePNExpires.Visible = True
                lblDatePNExpires.Visible = True
                chbPNReady.Visible = True
            Else
                DTPDraftIssued.Visible = False
                lblDraftIssued.Visible = False
                DTPDatePNExpires.Visible = False
                lblDatePNExpires.Visible = False
                chbPNReady.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub cboPublicAdvisory_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPublicAdvisory.TextChanged
        Try
            If cboPublicAdvisory.Text = "PA Not Needed" Then
                chbPAReady.Checked = False
                chbPAReady.Visible = False
                DTPDatePAExpires.Text = OracleDate
                DTPDatePAExpires.Visible = False
                lblDatePAExpires.Visible = False
            Else
                If cboPublicAdvisory.Visible = True Then
                    chbPAReady.Visible = True
                    DTPDatePAExpires.Visible = True
                    lblDatePAExpires.Visible = True
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub

#End Region
    Sub LoadApplication()
        Try
            If txtApplicationNumber.Text <> "" Then
                 
                'LoadBasicFacilityInfo()
                FormStatus = "Loading"
                LoadApplicationData()
                LoadMiscData()
                LoadBasicFacilityInfo()
                FormStatus = ""
                CheckOpenApplications()

                LoadContactData()

                If dsFacAppHistory Is Nothing Then
                Else
                    dsFacAppHistory.Clear()
                    txtApplicationNumberHistory.Clear()
                    txtApplicationTypeHistory.Clear()
                    txtApplicationDatedHistory.Clear()
                    txtApplicationUnitHistory.Clear()
                    txtEngineerHistory.Clear()
                    txtHistoryAppComments.Clear()
                    txtHistoryComments.Clear()
                    chbClosedOutHistory.Checked = False
                    lbLinkApplications.Items.Clear()
                End If
                If dsFacInfoHistory Is Nothing Then
                Else
                    dsFacInfoHistory.Clear()
                    txtInformationRequested.Clear()
                    txtInformationReceived.Clear()
                    txtInformationRequestedKey.Clear()
                    DTPInformationRequested.Text = OracleDate
                    DTPInformationRequested.Checked = False
                    DTPInformationReceived.Text = OracleDate
                    DTPInformationReceived.Checked = False
                End If
                 

                FindMasterApp()

                LoadSSPPSIPSubPartInformation()
                LoadSSPPNSPSSubPartInformation()
                LoadSSPPNESHAPSubPartInformation()
                LoadSSPPMACTSubPartInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnRefreshAppNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshAppNo.Click
        Try

            LoadApplication()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnRefreshAIRSNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshAIRSNo.Click
        Try
            If txtAIRSNumber.Text.Length = 8 Then
                FormStatus = "Loading"
                ReLoadBasicFacilityInfo()
                FormStatus = ""
                CheckOpenApplications()
                LoadContactData()

                LoadSSPPSIPSubPartInformation()
                LoadSSPPNSPSSubPartInformation()
                LoadSSPPNESHAPSubPartInformation()
                LoadSSPPMACTSubPartInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbTitleVPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTitleVPermit.CheckedChanged
        Try
            Dim TVNarrative As String = ""
            Dim TVDraft As String = ""
            Dim TVNotice As String = ""
            Dim TVFinal As String = ""

            chbTVNarrative.Checked = False
            chbTVDraft.Checked = False
            chbTVPublicNotice.Checked = False
            chbTVFinal.Checked = False

            DisplayPermitPanel()

            If rdbTitleVPermit.Checked = True Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'V_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "VN"
                                TVNarrative = "True"
                            Case "VD"
                                TVDraft = "True"
                            Case "VP"
                                TVNotice = "True"
                            Case "VF"
                                TVFinal = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If TVNarrative = "True" Then
                    chbTVNarrative.Checked = True
                End If
                If TVDraft = "True" Then
                    chbTVDraft.Checked = True
                End If
                If TVNotice = "True" Then
                    chbTVPublicNotice.Checked = True
                End If
                If TVFinal = "True" Then
                    chbTVFinal.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbPSDPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPSDPermit.CheckedChanged
        Try
            Dim PSDAppSummary As String = ""
            Dim PSDPrelimDet As String = ""
            Dim PSDNarrative As String = ""
            Dim PSDDraft As String = ""
            Dim PSDNotice As String = ""
            Dim PSDHearing As String = ""
            Dim PSDFinal As String = ""
            Dim PSDPermit As String = ""

            chbPSDApplicationSummary.Checked = False
            chbPSDPrelimDet.Checked = False
            chbPSDNarrative.Checked = False
            chbPSDDraftPermit.Checked = False
            chbPSDPublicNotice.Checked = False
            chbPSDHearingNotice.Checked = False
            chbPSDFinalDet.Checked = False
            chbPSDFinalPermit.Checked = False

            DisplayPermitPanel()

            If rdbPSDPermit.Checked = True And MasterApp <> "" Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'P_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "PA"
                                PSDAppSummary = "True"
                            Case "PP"
                                PSDPrelimDet = "True"
                            Case "PT"
                                PSDNarrative = "True"
                            Case "PD"
                                PSDDraft = "True"
                            Case "PN"
                                PSDNotice = "True"
                            Case "PH"
                                PSDHearing = "True"
                            Case "PF"
                                PSDFinal = "True"
                            Case "PI"
                                PSDPermit = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If PSDAppSummary = "True" Then
                    chbPSDApplicationSummary.Checked = True
                End If
                If PSDPrelimDet = "True" Then
                    chbPSDPrelimDet.Checked = True
                End If
                If PSDNarrative = "True" Then
                    chbPSDNarrative.Checked = True
                End If
                If PSDDraft = "True" Then
                    chbPSDDraftPermit.Checked = True
                End If
                If PSDNotice = "True" Then
                    chbPSDPublicNotice.Checked = True
                End If
                If PSDHearing = "True" Then
                    chbPSDHearingNotice.Checked = True
                End If
                If PSDFinal = "True" Then
                    chbPSDFinalDet.Checked = True
                End If
                If PSDPermit = "True" Then
                    chbPSDFinalPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub rdbOtherPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOtherPermit.CheckedChanged
        Try
            Dim OtherNarrative As String = ""
            Dim OtherPermit As String = ""

            chbOtherNarrative.Checked = False
            chbOtherPermit.Checked = False

            DisplayPermitPanel()

            If rdbOtherPermit.Checked = True Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'O_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "ON"
                                OtherNarrative = "True"
                            Case "OP"
                                OtherPermit = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If OtherNarrative = "True" Then
                    chbOtherNarrative.Checked = True
                End If
                If OtherPermit = "True" Then
                    chbOtherPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbTVNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVNarrative.CheckedChanged
        Try

            If chbTVNarrative.Checked = True And MasterApp <> "" Then

                txtTVNarrativeDoc.Visible = True
                txtTVNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VN-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'VN-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VN-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVNarrativeDoc.Text = ""
                    Else
                        txtTVNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVNarrativeSRDoc.Visible = False
                    Else
                        lblTVNarrativeSRDoc.Visible = True
                        lblTVNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVNarrativeDUDoc.Visible = False
                    Else
                        lblTVNarrativeDUDoc.Visible = True
                        lblTVNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVNarrativePDF.Text = ""
                    Else
                        txtTVNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVNarrativeSRPDF.Visible = False
                    Else
                        lblTVNarrativeSRPDF.Visible = True
                        lblTVNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVNarrativeDUPDF.Visible = False
                    Else
                        lblTVNarrativeDUPDF.Visible = True
                        lblTVNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVNarrativeDoc.Text = "On File" Or txtTVNarrativePDF.Text = "On File" Then
                    btnTVNarrativeDownload.Visible = True
                Else
                    btnTVNarrativeDownload.Visible = False
                End If
            Else
                txtTVNarrativeDoc.Clear()
                txtTVNarrativePDF.Clear()
                txtTVNarrativeDoc.Visible = False
                lblTVNarrativeSRDoc.Visible = False
                lblTVNarrativeDUDoc.Visible = False
                txtTVNarrativePDF.Visible = False
                lblTVNarrativeSRPDF.Visible = False
                lblTVNarrativeDUPDF.Visible = False
                btnTVNarrativeDownload.Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbTVDraft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVDraft.CheckedChanged
        Try

            If chbTVDraft.Checked = True And MasterApp <> "" Then
                txtTVDraftDoc.Visible = True
                txtTVDraftPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VD-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numuserID = strPDFModifingPerson " & _
                "and strFileName = 'VD-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VD-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVDraftDoc.Text = ""
                    Else
                        txtTVDraftDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVDraftSRDoc.Visible = False
                    Else
                        lblTVDraftSRDoc.Visible = True
                        lblTVDraftSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVDraftDUDoc.Visible = False
                    Else
                        lblTVDraftDUDoc.Visible = True
                        lblTVDraftDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVDraftPDF.Text = ""
                    Else
                        txtTVDraftPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVDraftSRPDF.Visible = False
                    Else
                        lblTVDraftSRPDF.Visible = True
                        lblTVDraftSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVDraftDUPDF.Visible = False
                    Else
                        lblTVDraftDUPDF.Visible = True
                        lblTVDraftDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVDraftDoc.Text = "On File" Or txtTVDraftPDF.Text = "On File" Then
                    btnTVDraftDownload.Visible = True
                Else
                    btnTVDraftDownload.Visible = False
                End If
            Else
                txtTVDraftDoc.Clear()
                txtTVDraftPDF.Clear()
                txtTVDraftDoc.Visible = False
                lblTVDraftSRDoc.Visible = False
                lblTVDraftDUDoc.Visible = False
                txtTVDraftPDF.Visible = False
                lblTVDraftSRPDF.Visible = False
                lblTVDraftDUPDF.Visible = False
                btnTVDraftDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbTVPublicNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVPublicNotice.CheckedChanged
        Try

            If chbTVPublicNotice.Checked = True And MasterApp <> "" Then
                txtTVPublicNoticeDoc.Visible = True
                txtTVPublicNoticePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName|| ' '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VP-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'VP-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVPublicNoticeDoc.Text = ""
                    Else
                        txtTVPublicNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVPublicNoticeSRDoc.Visible = False
                    Else
                        lblTVPublicNoticeSRDoc.Visible = True
                        lblTVPublicNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVPublicNoticeDUDoc.Visible = False
                    Else
                        lblTVPublicNoticeDUDoc.Visible = True
                        lblTVPublicNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVPublicNoticePDF.Text = ""
                    Else
                        txtTVPublicNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVPublicNoticeSRPDF.Visible = False
                    Else
                        lblTVPublicNoticeSRPDF.Visible = True
                        lblTVPublicNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVPublicNoticeDUPDF.Visible = False
                    Else
                        lblTVPublicNoticeDUPDF.Visible = True
                        lblTVPublicNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVPublicNoticeDoc.Text = "On File" Or txtTVPublicNoticePDF.Text = "On File" Then
                    btnTVPublicNoticeDownload.Visible = True
                Else
                    btnTVPublicNoticeDownload.Visible = False
                End If
            Else
                txtTVPublicNoticeDoc.Clear()
                txtTVPublicNoticePDF.Clear()
                txtTVPublicNoticeDoc.Visible = False
                lblTVPublicNoticeSRDoc.Visible = False
                lblTVPublicNoticeDUDoc.Visible = False
                txtTVPublicNoticePDF.Visible = False
                lblTVPublicNoticeSRPDF.Visible = False
                lblTVPublicNoticeDUPDF.Visible = False

                btnTVPublicNoticeDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbTVFinal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVFinal.CheckedChanged
        Try

            If chbTVFinal.Checked = True And MasterApp <> "" Then
                txtTVFinalDoc.Visible = True
                txtTVFinalPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'VF-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'VF-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'VF-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVFinalDoc.Text = ""
                    Else
                        txtTVFinalDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVFinalSRDoc.Visible = False
                    Else
                        lblTVFinalSRDoc.Visible = True
                        lblTVFinalSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVFinalDUDoc.Visible = False
                    Else
                        lblTVFinalDUDoc.Visible = True
                        lblTVFinalDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVFinalPDF.Text = ""
                    Else
                        txtTVFinalPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVFinalSRPDF.Visible = False
                    Else
                        lblTVFinalSRPDF.Visible = True
                        lblTVFinalSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVFinalDUPDF.Visible = False
                    Else
                        lblTVFinalDUPDF.Visible = True
                        lblTVFinalDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVFinalDoc.Text = "On File" Or txtTVFinalPDF.Text = "On File" Then
                    btnTVFinalDownload.Visible = True
                Else
                    btnTVFinalDownload.Visible = False
                End If
            Else
                txtTVFinalDoc.Clear()
                txtTVFinalPDF.Clear()
                txtTVFinalDoc.Visible = False
                lblTVFinalSRDoc.Visible = False
                lblTVFinalDUDoc.Visible = False
                txtTVFinalPDF.Visible = False
                lblTVFinalSRPDF.Visible = False
                lblTVFinalDUPDF.Visible = False
                btnTVFinalDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDApplicationSummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDApplicationSummary.CheckedChanged
        Try

            If chbPSDApplicationSummary.Checked = True And MasterApp <> "" Then
                txtPSDAppSummaryDoc.Visible = True
                txtPSDAppSummaryPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PA-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PA-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PA-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDAppSummaryDoc.Text = ""
                    Else
                        txtPSDAppSummaryDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDAppSummarySRDoc.Visible = False
                    Else
                        lblPSDAppSummarySRDoc.Visible = True
                        lblPSDAppSummarySRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDAppSummaryDUDoc.Visible = False
                    Else
                        lblPSDAppSummaryDUDoc.Visible = True
                        lblPSDAppSummaryDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDAppSummaryPDF.Text = ""
                    Else
                        txtPSDAppSummaryPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDAppSummarySRPDF.Visible = False
                    Else
                        lblPSDAppSummarySRPDF.Visible = True
                        lblPSDAppSummarySRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDAppSummaryDUPDF.Visible = False
                    Else
                        lblPSDAppSummaryDUPDF.Visible = True
                        lblPSDAppSummaryDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDAppSummaryDoc.Text = "On File" Or txtPSDAppSummaryPDF.Text = "On File" Then
                    btnPSDAppSummaryDownload.Visible = True
                Else
                    btnPSDAppSummaryDownload.Visible = False
                End If
            Else
                txtPSDAppSummaryDoc.Clear()
                txtPSDAppSummaryPDF.Clear()
                txtPSDAppSummaryDoc.Visible = False
                lblPSDAppSummarySRDoc.Visible = False
                lblPSDAppSummaryDUDoc.Visible = False
                txtPSDAppSummaryPDF.Visible = False
                lblPSDAppSummarySRPDF.Visible = False
                lblPSDAppSummaryDUPDF.Visible = False
                btnPSDAppSummaryDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDPrelimDet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDPrelimDet.CheckedChanged
        Try

            If chbPSDPrelimDet.Checked = True And MasterApp <> "" Then
                txtPSDPrelimDetDoc.Visible = True
                txtPSDPrelimDetPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PP-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PP-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDPrelimDetDoc.Text = ""
                    Else
                        txtPSDPrelimDetDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDPrelimDetSRDoc.Visible = False
                    Else
                        lblPSDPrelimDetSRDoc.Visible = True
                        lblPSDPrelimDetSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDPrelimDetDUDoc.Visible = False
                    Else
                        lblPSDPrelimDetDUDoc.Visible = True
                        lblPSDPrelimDetDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDPrelimDetPDF.Text = ""
                    Else
                        txtPSDPrelimDetPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDPrelimDetSRPDF.Visible = False
                    Else
                        lblPSDPrelimDetSRPDF.Visible = True
                        lblPSDPrelimDetSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDPrelimDetDUPDF.Visible = False
                    Else
                        lblPSDPrelimDetDUPDF.Visible = True
                        lblPSDPrelimDetDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDPrelimDetDoc.Text = "On File" Or txtPSDPrelimDetPDF.Text = "On File" Then
                    btnPSDPrelimDetDownload.Visible = True
                Else
                    btnPSDPrelimDetDownload.Visible = False
                End If
            Else
                txtPSDPrelimDetDoc.Clear()
                txtPSDPrelimDetPDF.Clear()
                txtPSDPrelimDetDoc.Visible = False
                lblPSDPrelimDetSRDoc.Visible = False
                lblPSDPrelimDetDUDoc.Visible = False
                txtPSDPrelimDetPDF.Visible = False
                lblPSDPrelimDetSRPDF.Visible = False
                lblPSDPrelimDetDUPDF.Visible = False
                btnPSDPrelimDetDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDNarrative.CheckedChanged
        Try

            If chbPSDNarrative.Checked = True And MasterApp <> "" Then
                txtPSDNarrativeDoc.Visible = True
                txtPSDNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PT-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PT-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PT-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDNarrativeDoc.Text = ""
                    Else
                        txtPSDNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDNarrativeSRDoc.Visible = False
                    Else
                        lblPSDNarrativeSRDoc.Visible = True
                        lblPSDNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDNarrativeDUDoc.Visible = False
                    Else
                        lblPSDNarrativeDUDoc.Visible = True
                        lblPSDNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDNarrativePDF.Text = ""
                    Else
                        txtPSDNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDNarrativeSRPDF.Visible = False
                    Else
                        lblPSDNarrativeSRPDF.Visible = True
                        lblPSDNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDNarrativeDUPDF.Visible = False
                    Else
                        lblPSDNarrativeDUPDF.Visible = True
                        lblPSDNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDNarrativeDoc.Text = "On File" Or txtPSDNarrativePDF.Text = "On File" Then
                    btnPSDNarrativeDownload.Visible = True
                Else
                    btnPSDNarrativeDownload.Visible = False
                End If
            Else
                txtPSDNarrativeDoc.Clear()
                txtPSDNarrativePDF.Clear()
                txtPSDNarrativeDoc.Visible = False
                lblPSDNarrativeSRDoc.Visible = False
                lblPSDNarrativeDUDoc.Visible = False
                txtPSDNarrativePDF.Visible = False
                lblPSDNarrativeSRPDF.Visible = False
                lblPSDNarrativeDUPDF.Visible = False
                btnPSDNarrativeDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDDraftPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDDraftPermit.CheckedChanged
        Try

            If chbPSDDraftPermit.Checked = True And MasterApp <> "" Then
                txtPSDDraftPermitDoc.Visible = True
                txtPSDDraftPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PD-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PD-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PD-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDDraftPermitDoc.Text = ""
                    Else
                        txtPSDDraftPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDDraftPermitSRDoc.Visible = False
                    Else
                        lblPSDDraftPermitSRDoc.Visible = True
                        lblPSDDraftPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDDraftPermitDUDoc.Visible = False
                    Else
                        lblPSDDraftPermitDUDoc.Visible = True
                        lblPSDDraftPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDDraftPermitPDF.Text = ""
                    Else
                        txtPSDDraftPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDDraftPermitSRPDF.Visible = False
                    Else
                        lblPSDDraftPermitSRPDF.Visible = True
                        lblPSDDraftPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDDraftPermitDUPDF.Visible = False
                    Else
                        lblPSDDraftPermitDUPDF.Visible = True
                        lblPSDDraftPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDDraftPermitDoc.Text = "On File" Or txtPSDDraftPermitPDF.Text = "On File" Then
                    btnPSDDraftPermitDownload.Visible = True
                Else
                    btnPSDDraftPermitDownload.Visible = False
                End If
            Else
                txtPSDDraftPermitDoc.Clear()
                txtPSDDraftPermitPDF.Clear()
                txtPSDDraftPermitDoc.Visible = False
                lblPSDDraftPermitSRDoc.Visible = False
                lblPSDDraftPermitDUDoc.Visible = False
                txtPSDDraftPermitPDF.Visible = False
                lblPSDDraftPermitSRPDF.Visible = False
                lblPSDDraftPermitDUPDF.Visible = False
                btnPSDDraftPermitDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDPublicNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDPublicNotice.CheckedChanged
        Try

            If chbPSDPublicNotice.Checked = True And MasterApp <> "" Then
                txtPSDPublicNoticeDoc.Visible = True
                txtPSDPublicNoticePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'PN-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastname||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'PN-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'PN-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDPublicNoticeDoc.Text = ""
                    Else
                        txtPSDPublicNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDPublicNoticeSRDoc.Visible = False
                    Else
                        lblPSDPublicNoticeSRDoc.Visible = True
                        lblPSDPublicNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDPublicNoticeDUDoc.Visible = False
                    Else
                        lblPSDPublicNoticeDUDoc.Visible = True
                        lblPSDPublicNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDPublicNoticePDF.Text = ""
                    Else
                        txtPSDPublicNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDPublicNoticeSRPDF.Visible = False
                    Else
                        lblPSDPublicNoticeSRPDF.Visible = True
                        lblPSDPublicNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDPublicNoticeDUPDF.Visible = False
                    Else
                        lblPSDPublicNoticeDUPDF.Visible = True
                        lblPSDPublicNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDPublicNoticeDoc.Text = "On File" Or txtPSDPublicNoticePDF.Text = "On File" Then
                    btnPSDPublicNoticeDownload.Visible = True
                Else
                    btnPSDPublicNoticeDownload.Visible = False
                End If
            Else
                txtPSDPublicNoticeDoc.Clear()
                txtPSDPublicNoticePDF.Clear()
                txtPSDPublicNoticeDoc.Visible = False
                lblPSDPublicNoticeSRDoc.Visible = False
                lblPSDPublicNoticeDUDoc.Visible = False
                txtPSDPublicNoticePDF.Visible = False
                lblPSDPublicNoticeSRPDF.Visible = False
                lblPSDPublicNoticeDUPDF.Visible = False
                btnPSDPublicNoticeDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDHearingNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDHearingNotice.CheckedChanged
        Try

            If chbPSDHearingNotice.Checked = True And MasterApp <> "" Then
                txtPSDHearingNoticeDoc.Visible = True
                txtPSDHearingNoticePDF.Visible = True
              lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numuserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PH-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastname||', '||strFirstname) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PH-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PH-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDHearingNoticeDoc.Text = ""
                    Else
                        txtPSDHearingNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDHearingNoticeSRDoc.Visible = False
                    Else
                        lblPSDHearingNoticeSRDoc.Visible = True
                        lblPSDHearingNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDHearingNoticeDUDoc.Visible = False
                    Else
                        lblPSDHearingNoticeDUDoc.Visible = True
                        lblPSDHearingNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDHearingNoticePDF.Text = ""
                    Else
                        txtPSDHearingNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDHearingNoticeSRPDF.Visible = False
                    Else
                        lblPSDHearingNoticeSRPDF.Visible = True
                        lblPSDHearingNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDHearingNoticeDUPDF.Visible = False
                    Else
                        lblPSDHearingNoticeDUPDF.Visible = True
                        lblPSDHearingNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDHearingNoticeDoc.Text = "On File" Or txtPSDHearingNoticePDF.Text = "On File" Then
                    btnPSDHearingNoticeDownload.Visible = True
                Else
                    btnPSDHearingNoticeDownload.Visible = False
                End If
            Else
                txtPSDHearingNoticeDoc.Clear()
                txtPSDHearingNoticePDF.Clear()
                txtPSDHearingNoticeDoc.Visible = False
                lblPSDHearingNoticeSRDoc.Visible = False
                lblPSDHearingNoticeDUDoc.Visible = False
                txtPSDHearingNoticePDF.Visible = False
                lblPSDHearingNoticeSRPDF.Visible = False
                lblPSDHearingNoticeDUPDF.Visible = False
                btnPSDHearingNoticeDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDFinalDet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDFinalDet.CheckedChanged
        Try

            If chbPSDFinalDet.Checked = True And MasterApp <> "" Then
                txtPSDFinalDetDoc.Visible = True
                txtPSDFinalDetPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PF-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PF-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PF-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDFinalDetDoc.Text = ""
                    Else
                        txtPSDFinalDetDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDFinalDetSRDoc.Visible = False
                    Else
                        lblPSDFinalDetSRDoc.Visible = True
                        lblPSDFinalDetSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDFinalDetDUDoc.Visible = False
                    Else
                        lblPSDFinalDetDUDoc.Visible = True
                        lblPSDFinalDetDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDFinalDetPDF.Text = ""
                    Else
                        txtPSDFinalDetPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDFinalDetSRPDF.Visible = False
                    Else
                        lblPSDFinalDetSRPDF.Visible = True
                        lblPSDFinalDetSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDFinalDetDUPDF.Visible = False
                    Else
                        lblPSDFinalDetDUPDF.Visible = True
                        lblPSDFinalDetDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDFinalDetDoc.Text = "On File" Or txtPSDFinalDetPDF.Text = "On File" Then
                    btnPSDFinalDetDownload.Visible = True
                Else
                    btnPSDFinalDetDownload.Visible = False
                End If
            Else
                txtPSDFinalDetDoc.Clear()
                txtPSDFinalDetPDF.Clear()
                txtPSDFinalDetDoc.Visible = False
                lblPSDFinalDetSRDoc.Visible = False
                lblPSDFinalDetDUDoc.Visible = False
                txtPSDFinalDetPDF.Visible = False
                lblPSDFinalDetSRPDF.Visible = False
                lblPSDFinalDetDUPDF.Visible = False
                btnPSDFinalDetDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbPSDFinalPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDFinalPermit.CheckedChanged
        Try

            If chbPSDFinalPermit.Checked = True And MasterApp <> "" Then
                txtPSDFinalPermitDoc.Visible = True
                txtPSDFinalPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'PI-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'PI-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'PI-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDFinalPermitDoc.Text = ""
                    Else
                        txtPSDFinalPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDFinalPermitSRDoc.Visible = False
                    Else
                        lblPSDFinalPermitSRDoc.Visible = True
                        lblPSDFinalPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDFinalPermitDUDoc.Visible = False
                    Else
                        lblPSDFinalPermitDUDoc.Visible = True
                        lblPSDFinalPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDFinalPermitPDF.Text = ""
                    Else
                        txtPSDFinalPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDFinalPermitSRPDF.Visible = False
                    Else
                        lblPSDFinalPermitSRPDF.Visible = True
                        lblPSDFinalPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDFinalPermitDUPDF.Visible = False
                    Else
                        lblPSDFinalPermitDUPDF.Visible = True
                        lblPSDFinalPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDFinalPermitDoc.Text = "On File" Or txtPSDFinalPermitPDF.Text = "On File" Then
                    btnPSDFinalPermitDownload.Visible = True
                Else
                    btnPSDFinalPermitDownload.Visible = False
                End If
            Else
                txtPSDFinalPermitDoc.Clear()
                txtPSDFinalPermitPDF.Clear()
                txtPSDFinalPermitDoc.Visible = False
                lblPSDFinalPermitSRDoc.Visible = False
                lblPSDFinalPermitDUDoc.Visible = False
                txtPSDFinalPermitPDF.Visible = False
                lblPSDFinalPermitSRPDF.Visible = False
                lblPSDFinalPermitDUPDF.Visible = False
                 btnPSDFinalPermitDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbOtherNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbOtherNarrative.CheckedChanged
        Try
            If chbOtherNarrative.Checked = True And MasterApp <> "" Then
                txtOtherNarrativeDoc.Visible = True
                txtOtherNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'ON-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUSerProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUSerProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'ON-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'ON-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtOtherNarrativeDoc.Text = ""
                    Else
                        txtOtherNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblOtherNarrativeSRDoc.Visible = False
                    Else
                        lblOtherNarrativeSRDoc.Visible = True
                        lblOtherNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblOtherNarrativeDUDoc.Visible = False
                    Else
                        lblOtherNarrativeDUDoc.Visible = True
                        lblOtherNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtOtherNarrativePDF.Text = ""
                    Else
                        txtOtherNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblOtherNarrativeSRPDF.Visible = False
                    Else
                        lblOtherNarrativeSRPDF.Visible = True
                        lblOtherNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblOtherNarrativeDUPDF.Visible = False
                    Else
                        lblOtherNarrativeDUPDF.Visible = True
                        lblOtherNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtOtherNarrativeDoc.Text = "On File" Or txtOtherNarrativePDF.Text = "On File" Then
                    btnOtherNarrativeDownload.Visible = True
                Else
                    btnOtherNarrativeDownload.Visible = False
                End If
            Else
                txtOtherNarrativeDoc.Clear()
                txtOtherNarrativePDF.Clear()
                txtOtherNarrativeDoc.Visible = False
                lblOtherNarrativeSRDoc.Visible = False
                lblOtherNarrativeDUDoc.Visible = False
                txtOtherNarrativePDF.Clear()
                txtOtherNarrativePDF.Visible = False
                lblOtherNarrativeSRPDF.Visible = False
                lblOtherNarrativeDUPDF.Visible = False
                btnOtherNarrativeDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub chbOtherPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbOtherPermit.CheckedChanged
        Try

            If chbOtherPermit.Checked = True And MasterApp <> "" Then
                txtOtherPermitDoc.Visible = True
                txtOtherPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                  "case " & _
                  "when docPermitData is Null then '' " & _
                  "Else 'True' " & _
                  "End DocData, " & _
                  "case " & _
                  "when strDocModifingPerson is Null then '' " & _
                  "else (select (strLastName||', '||strFirstName) as StaffName " & _
                  "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                  "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                  "and numUserID = strDocModifingPerson " & _
                  "and strFileName = 'OP-" & MasterApp & "') " & _
                  "end DocStaffResponsible, " & _
                  "case " & _
                  "when datDocModifingDate is Null then '' " & _
                  "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                  "End datDocModifingDate, " & _
                  "case " & _
                  "when pdfPermitData is Null then '' " & _
                  "Else 'True' " & _
                  "End PDFData, " & _
                  "case " & _
                  "when strPDFModifingPerson is Null then '' " & _
                  "else (select (strLastName||', '||strFirstName) as StaffName " & _
                  "from AIRBRANCH.APBPermits, AIRBRANCH.epduserprofiles " & _
                  "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                  "and numUserID = strPDFModifingPerson " & _
                  "and strFileName = 'OP-" & MasterApp & "') " & _
                  "end PDFStaffResponsible, " & _
                  "case " & _
                  "when datPDFModifingDate is Null then '' " & _
                  "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                  "End datPDFModifingDate " & _
                  "from AIRBRANCH.APBPermits " & _
                  "where strFileName = 'OP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtOtherPermitDoc.Text = ""
                    Else
                        txtOtherPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblOtherPermitSRDoc.Visible = False
                    Else
                        lblOtherPermitSRDoc.Visible = True
                        lblOtherPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblOtherPermitDUDoc.Visible = False
                    Else
                        lblOtherPermitDUDoc.Visible = True
                        lblOtherPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtOtherPermitPDF.Text = ""
                    Else
                        txtOtherPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblOtherPermitSRPDF.Visible = False
                    Else
                        lblOtherPermitSRPDF.Visible = True
                        lblOtherPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblOtherPermitDUPDF.Visible = False
                    Else
                        lblOtherPermitDUPDF.Visible = True
                        lblOtherPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtOtherPermitDoc.Text = "On File" Or txtOtherPermitPDF.Text = "On File" Then
                    btnOtherPermitDownload.Visible = True
                Else
                    btnOtherPermitDownload.Visible = False
                End If
            Else
                txtOtherPermitDoc.Clear()
                txtOtherPermitPDF.Clear()
                txtOtherPermitDoc.Visible = False
                lblOtherPermitSRDoc.Visible = False
                lblOtherPermitDUDoc.Visible = False
                txtOtherPermitPDF.Visible = False
                lblOtherPermitSRPDF.Visible = False
                lblOtherPermitDUPDF.Visible = False
                btnOtherPermitDownload.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub btnOtherNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtherNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherNarrativeDoc.Text = "On File" Or txtOtherNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtOtherNarrativeDoc.Text = "On File" And txtOtherNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("ON-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("ON-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("ON-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtOtherNarrativeDoc.Text = "On File" Then
                        DownloadFile("ON-" & MasterApp, "10")
                    End If
                    If txtOtherNarrativePDF.Text = "On File" Then
                        DownloadFile("ON-" & MasterApp, "01")
                    End If
                End If


            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnOtherPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtherPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherPermitDoc.Text = "On File" Or txtOtherPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtOtherPermitDoc.Text = "On File" And txtOtherPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("OP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("OP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("OP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtOtherPermitDoc.Text = "On File" Then
                        DownloadFile("OP-" & MasterApp, "10")
                    End If
                    If txtOtherPermitPDF.Text = "On File" Then
                        DownloadFile("OP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVNarrativeDoc.Text = "On File" Or txtTVNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVNarrativeDoc.Text = "On File" And txtTVNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VN-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VN-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VN-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVNarrativeDoc.Text = "On File" Then
                        DownloadFile("VN-" & MasterApp, "10")
                    End If
                    If txtTVNarrativePDF.Text = "On File" Then
                        DownloadFile("VN-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVDraftDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVDraftDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVDraftDoc.Text = "On File" Or txtTVDraftPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVDraftDoc.Text = "On File" And txtTVDraftPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VD-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VD-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VD-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVDraftDoc.Text = "On File" Then
                        DownloadFile("VD-" & MasterApp, "10")
                    End If
                    If txtTVDraftPDF.Text = "On File" Then
                        DownloadFile("VD-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVPublicNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVPublicNoticeDoc.Text = "On File" Or txtTVPublicNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVPublicNoticeDoc.Text = "On File" And txtTVPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVPublicNoticeDoc.Text = "On File" Then
                        DownloadFile("VP-" & MasterApp, "10")
                    End If
                    If txtTVPublicNoticePDF.Text = "On File" Then
                        DownloadFile("VP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVFinalDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVFinalDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVFinalDoc.Text = "On File" Or txtTVFinalPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVFinalDoc.Text = "On File" And txtTVFinalPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VF-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VF-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VF-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVFinalDoc.Text = "On File" Then
                        DownloadFile("VF-" & MasterApp, "10")
                    End If
                    If txtTVFinalPDF.Text = "On File" Then
                        DownloadFile("VF-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDAppSummaryDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDAppSummaryDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDAppSummaryDoc.Text = "On File" Or txtPSDAppSummaryPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDAppSummaryDoc.Text = "On File" And txtPSDAppSummaryPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PA-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PA-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PA-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDAppSummaryDoc.Text = "On File" Then
                        DownloadFile("PA-" & MasterApp, "10")
                    End If
                    If txtPSDAppSummaryPDF.Text = "On File" Then
                        DownloadFile("PA-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDPrelimDetDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDPrelimDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPrelimDetDoc.Text = "On File" Or txtPSDPrelimDetPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDPrelimDetDoc.Text = "On File" And txtPSDPrelimDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDPrelimDetDoc.Text = "On File" Then
                        DownloadFile("PP-" & MasterApp, "10")
                    End If
                    If txtPSDPrelimDetPDF.Text = "On File" Then
                        DownloadFile("PP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDNarrativeDoc.Text = "On File" Or txtPSDNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDNarrativeDoc.Text = "On File" And txtPSDNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PT-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PT-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PT-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDNarrativeDoc.Text = "On File" Then
                        DownloadFile("PT-" & MasterApp, "10")
                    End If
                    If txtPSDNarrativePDF.Text = "On File" Then
                        DownloadFile("PT-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDDraftPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDDraftPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDDraftPermitDoc.Text = "On File" Or txtPSDDraftPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDDraftPermitDoc.Text = "On File" And txtPSDDraftPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PD-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PD-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PD-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDDraftPermitDoc.Text = "On File" Then
                        DownloadFile("PD-" & MasterApp, "10")
                    End If
                    If txtPSDDraftPermitPDF.Text = "On File" Then
                        DownloadFile("PD-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDPublicNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPublicNoticeDoc.Text = "On File" Or txtPSDPublicNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDPublicNoticeDoc.Text = "On File" And txtPSDPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PN-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PN-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PN-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDPublicNoticeDoc.Text = "On File" Then
                        DownloadFile("PN-" & MasterApp, "10")
                    End If
                    If txtPSDPublicNoticePDF.Text = "On File" Then
                        DownloadFile("PN-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDHearingNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDHearingNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDHearingNoticeDoc.Text = "On File" Or txtPSDHearingNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDHearingNoticeDoc.Text = "On File" And txtPSDHearingNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PH-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PH-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PH-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDHearingNoticeDoc.Text = "On File" Then
                        DownloadFile("PH-" & MasterApp, "10")
                    End If
                    If txtPSDHearingNoticePDF.Text = "On File" Then
                        DownloadFile("PH-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDFinalDetDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDFinalDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalDetDoc.Text = "On File" Or txtPSDFinalDetPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDFinalDetDoc.Text = "On File" And txtPSDFinalDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PF-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PF-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PF-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDFinalDetDoc.Text = "On File" Then
                        DownloadFile("PF-" & MasterApp, "10")
                    End If
                    If txtPSDFinalDetPDF.Text = "On File" Then
                        DownloadFile("PF-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDFinalPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDFinalPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalPermitDoc.Text = "On File" Or txtPSDFinalPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDFinalPermitDoc.Text = "On File" And txtPSDFinalPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PI-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PI-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PI-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDFinalPermitDoc.Text = "On File" Then
                        DownloadFile("PI-" & MasterApp, "10")
                    End If
                    If txtPSDFinalPermitPDF.Text = "On File" Then
                        DownloadFile("PI-" & MasterApp, "01")
                    End If
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub llbPermitNumber_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPermitNumber.LinkClicked
        Try
            Dim URL As String = ""
            Dim PDFFile As String = ""

            SQL = "select " & _
            "distinct(AIRBRANCH.APBPermits.strFileName),  " & _
            "strDocFileSize, strPDFFileSize " & _
            "from AIRBRANCH.APBpermits, AIRBRANCH.SSPPApplicationLinking " & _
            "where substr(AIRBRANCH.APBpermits.strFileName, 4) = " & _
            "AIRBRANCH.SSPPAPPlicationLinking.strmasterapplication (+) " & _
            "and (AIRBRANCH.SSPPApplicationLinking.strApplicationNumber = '" & MasterApp & "' " & _
            "or AIRBRANCH.APBPermits.strFileName like '%-" & MasterApp & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                temp = dr.Item("strFileName")
                If IsDBNull(dr.Item("strPDFFileSize")) Then
                    PDFFile = ""
                Else
                    PDFFile = dr.Item("strPDFFileSize")
                End If
            End While
            dr.Close()

            Select Case Mid(temp, 1, 1)
                Case "V"
                    If PDFFile <> "" Then
                        URL = "http://search.georgiaair.org/permit.aspx?id=PDF-VF-" & txtApplicationNumber.Text
                    Else
                        URL = "http://search.georgiaair.org/permit.aspx?id=DOC-VF-" & txtApplicationNumber.Text
                    End If
                Case "P"
                    If PDFFile <> "" Then
                        URL = "http://search.georgiaair.org/permit.aspx?id=PDF-PI-" & txtApplicationNumber.Text
                    Else
                        URL = "http://search.georgiaair.org/permit.aspx?id=DOC-PI-" & txtApplicationNumber.Text
                    End If
                Case Else
                    If PDFFile <> "" Then
                        URL = "http://search.georgiaair.org/permit.aspx?id=PDF-OP-" & txtApplicationNumber.Text
                    Else
                        URL = "http://search.georgiaair.org/permit.aspx?id=DOC-OP-" & txtApplicationNumber.Text
                    End If
            End Select

            If URL <> "" Then OpenUri(New Uri(URL), Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnGetCurrentPermittingContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCurrentPermittingContact.Click
        Try

            SQL = "Select " & _
             "strContactFirstName, " & _
             "strContactLastName, " & _
             "strContactPrefix, " & _
             "strContactSuffix, " & _
             "strContactTitle, " & _
             "strContactCompanyName, " & _
             "strContactPhoneNumber1, " & _
             "strContactFaxNumber, " & _
             "strContactEmail, " & _
             "strContactAddress1, " & _
             "strContactCity, " & _
             "strContactState, " & _
             "strContactZipCode, " & _
             "strContactDescription " & _
             "from AIRBRANCH.APBContactInformation " & _
             "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstname")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtContactSocialTitle.Clear()
                Else
                    txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtContactPedigree.Clear()
                Else
                    txtContactPedigree.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtContactCompanyName.Clear()
                Else
                    txtContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    mtbContactPhoneNumber.Clear()
                Else
                    temp = dr.Item("strContactPhoneNumber1")
                    mtbContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    mtbContactFaxNumber.Clear()
                Else
                    mtbContactFaxNumber.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtContactEmailAddress.Clear()
                Else
                    txtContactEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtContactStreetAddress.Clear()
                Else
                    txtContactStreetAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtContactCity.Clear()
                Else
                    txtContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtContactState.Clear()
                Else
                    txtContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbContactZipCode.Clear()
                Else
                    mtbContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtContactDescription.Clear()
                Else
                    txtContactDescription.Text = dr.Item("strContactDescription")
                End If
                txtContactDescription.Text = "From App #- " & txtApplicationNumber.Text & vbCrLf & txtContactDescription.Text
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditSIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSIP.Click
        Try
            If txtSIPCode.Text <> "" And txtSIPDescription.Text <> "" Then
                txtSIPCode.BackColor = Color.White
                txtSIPDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From AIRBRANCH.LookUpSubpartSIP " & _
                "where strSubPart = '" & txtSIPCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.LookUpSubpartSIP set " & _
                    "strDescription = '" & Replace(txtSIPDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtSIPCode.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.LookUpSubpartSIP " & _
                    "values " & _
                    "('" & Replace(txtSIPCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtSIPDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtSIPCode.Text = "" Then
                    txtSIPCode.BackColor = Color.Tomato
                Else
                    txtSIPCode.BackColor = Color.White
                End If
                If txtSIPDescription.Text = "" Then
                    txtSIPDescription.BackColor = Color.Tomato
                Else
                    txtSIPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNSPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditNSPS.Click
        Try
            If txtNSPSCode.Text <> "" And txtNSPSDescription.Text <> "" Then
                txtNSPSCode.BackColor = Color.White
                txtNSPSDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From AIRBRANCH.LookUpSubpart60 " & _
                "where strSubPart = '" & txtNSPSCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.LookUpSubpart60 set " & _
                    "strDescription = '" & Replace(txtNSPSDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtNSPSCode.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.LookUpSubpart60 " & _
                    "values " & _
                    "('" & Replace(txtNSPSCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtNSPSDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNSPSCode.Text = "" Then
                    txtNSPSCode.BackColor = Color.Tomato
                Else
                    txtNSPSCode.BackColor = Color.White
                End If
                If txtNSPSDescription.Text = "" Then
                    txtNSPSDescription.BackColor = Color.Tomato
                Else
                    txtNSPSDescription.BackColor = Color.White
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNESHAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditNESHAP.Click
        Try
            If txtNESHAPCode.Text <> "" And txtNESHAPDescription.Text <> "" Then
                txtNESHAPCode.BackColor = Color.White
                txtNESHAPDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From AIRBRANCH.LookUpSubpart61 " & _
                "where strSubPart = '" & txtNESHAPCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.LookUpSubpart61 set " & _
                    "strDescription = '" & Replace(txtNESHAPDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtNESHAPCode.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.LookUpSubpart61 " & _
                    "values " & _
                    "('" & Replace(txtNESHAPCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtNESHAPDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNESHAPCode.Text = "" Then
                    txtNESHAPCode.BackColor = Color.Tomato
                Else
                    txtNESHAPCode.BackColor = Color.White
                End If
                If txtNESHAPDescription.Text = "" Then
                    txtNESHAPDescription.BackColor = Color.Tomato
                Else
                    txtNESHAPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditMACT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditMACT.Click
        Try
            If txtMACTCode.Text <> "" And txtMACTDescription.Text <> "" Then
                txtMACTCode.BackColor = Color.White
                txtMACTDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From AIRBRANCH.LookUpSubpart63 " & _
                "where strSubPart = '" & txtMACTCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.LookUpSubpart63 set " & _
                    "strDescription = '" & Replace(txtMACTDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtMACTCode.Text & "' "
                Else
                    SQL = "Insert into AIRBRANCH.LookUpSubpart63 " & _
                    "values " & _
                    "('" & Replace(txtMACTCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtMACTDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtMACTCode.Text = "" Then
                    txtMACTCode.BackColor = Color.Tomato
                Else
                    txtMACTCode.BackColor = Color.White
                End If
                If txtMACTDescription.Text = "" Then
                    txtMACTDescription.BackColor = Color.Tomato
                Else
                    txtMACTDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteSIPSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSIPSubpart.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpSubpartSIP " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNSPSSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNSPSSubpart.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpSubpart60 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNESHAPSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNESHAPSubpart.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpSubpart61 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteMACTSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteMACTSubpart.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpSubpart63 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearSIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSIP.Click
        Try
            txtSIPCode.Clear()
            txtSIPCode.BackColor = Color.White
            txtSIPDescription.Clear()
            txtSIPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNSPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNSPS.Click
        Try
            txtNSPSCode.Clear()
            txtNSPSCode.BackColor = Color.White
            txtNSPSDescription.Clear()
            txtNSPSDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNESHAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNESHAP.Click
        Try
            txtNESHAPCode.Clear()
            txtNESHAPCode.BackColor = Color.White
            txtNESHAPDescription.Clear()
            txtNESHAPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearMACT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMACT.Click
        Try
            txtMACTCode.Clear()
            txtMACTCode.BackColor = Color.White
            txtMACTDescription.Clear()
            txtMACTDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvSIP_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSIP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSIP.HitTest(e.X, e.Y)
            If dgvSIP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvSIP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtSIPCode.BackColor = Color.White
                    txtSIPDescription.BackColor = Color.White

                    txtSIPCode.Text = dgvSIP(0, hti.RowIndex).Value
                    txtSIPDescription.Text = dgvSIP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNSPS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNSPS.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNSPS.HitTest(e.X, e.Y)
            If dgvNSPS.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNSPS.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNSPSCode.BackColor = Color.White
                    txtNSPSDescription.BackColor = Color.White

                    txtNSPSCode.Text = dgvNSPS(0, hti.RowIndex).Value
                    txtNSPSDescription.Text = dgvNSPS(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNESHAP_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNESHAP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNESHAP.HitTest(e.X, e.Y)
            If dgvNESHAP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNESHAP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNESHAPCode.BackColor = Color.White
                    txtNESHAPDescription.BackColor = Color.White

                    txtNESHAPCode.Text = dgvNESHAP(0, hti.RowIndex).Value
                    txtNESHAPDescription.Text = dgvNESHAP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvMACT_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvMACT.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvMACT.HitTest(e.X, e.Y)
            If dgvMACT.Columns(0).HeaderText = "Subpart Code" Then
                If dgvMACT.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtMACTCode.BackColor = Color.White
                    txtMACTDescription.BackColor = Color.White

                    txtMACTCode.Text = dgvMACT(0, hti.RowIndex).Value
                    txtMACTDescription.Text = dgvMACT(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnAcknowledgementLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcknowledgementLetter.Click
        Try
            If txtContactSocialTitle.Text = "" Or txtContactSocialTitle.Text = "N/A" Or txtContactSocialTitle.Text = " " Then
                MessageBox.Show("Invalid Social Title" & vbCrLf & "Please correct.", "SSPP Application Tracking Log", _
                                MessageBoxButtons.OK)
                Exit Sub
            End If
            If txtApplicationNumber.Text <> "" Then
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtPrintType.Text = "Letter"
                PrintOut.txtOther.Text = "SSPP Confirm"
                PrintOut.txtAIRSNumber.Text = txtApplicationNumber.Text
                PrintOut.Show()
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnEmailAcknowledgmentLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailAcknowledgmentLetter.Click
        Try
            Dim EmailAddress As String = ""
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim StaffPhone As String = ""
            Dim StaffEmail As String = ""

            If txtContactSocialTitle.Text = "" Or txtContactSocialTitle.Text = "N/A" Or txtContactSocialTitle.Text = " " Then
                MessageBox.Show("Invalid Social Title" & vbCrLf & "Please correct.", "SSPP Application Tracking Log", _
                                MessageBoxButtons.OK)
                Exit Sub
            End If

            If Not IsValidEmailAddress(txtContactEmailAddress.Text) Then
                MessageBox.Show("Invalid Email Address", "Application Tracking Log", MessageBoxButtons.OKCancel)
                Exit Sub
            Else
                EmailAddress = txtContactEmailAddress.Text
            End If

            Me.Cursor = Cursors.AppStarting

            SQL = "select " & _
            "'('||substr(strPhone, 1, 3)||') '||substr(strPhone, 4,3)||'-'||substr(strPhone, 7) as StaffPhone, " & _
            "strEmailAddress, strPhone " & _
            "from AIRBranch.EPDUserProfiles " & _
            "where numUserID = '" & CurrentUser.UserID & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("StaffPhone")) Then
                    StaffPhone = ""
                Else
                    StaffPhone = dr.Item("StaffPhone")
                End If
                If IsDBNull(dr.Item("strEmailAddress")) Then
                    StaffEmail = ""
                Else
                    StaffEmail = dr.Item("strEmailAddress")
                End If
            End While
            dr.Close()

            Subject = "GA Air Application No. " & txtApplicationNumber.Text & ", dated: " & DTPDateSent.Text

            Body = "Dear " & txtContactSocialTitle.Text & " " & txtContactLastName.Text & ", " &
                vbNewLine & vbNewLine &
                "This is to acknowledge the receipt of your GA Air Quality Permit application for " &
                txtFacilityName.Text & " (Airs No. " & txtAIRSNumber.Text & ") in " & cboFacilityCity.Text &
                ", GA. After our initial review of the information and technical data in this application, " &
                "we will notify you if more information is needed to complete " &
                "the application so that we can finish our review. " &
                vbNewLine & vbNewLine &
                "Other environmental permits may be required. For Industrial Stormwater permits, contact " &
                "the Watershed Protection Branch at (404) 675-1605; for Solid Waste permits, contact the " &
                "Land Protection Branch at (404) 362-2537. For more info, http://epd.georgia.gov" &
                vbNewLine & vbNewLine &
                "GEOS, the new web-based permit application system is now operational at: " &
                "https://geos.epd.georgia.gov/GA/GEOS/Public/EnSuite/Shared/Pages/Main/Login.aspx " &
                vbNewLine & vbNewLine &
                "To track the status of the air quality permit application, log on to Georgia Environmental " &
                "Protection Divisionís Georgia Environmental Connections Online (GECO) at the web address " &
                "http://geco.georgiaair.org (registration required) and follow the online instructions." &
                vbNewLine & vbNewLine &
                "If your company qualifies as a small business (generally those with fewer than 100 " &
                "employees), you may contact our Small Business Environmental Assistance Program at " &
                "(404) 362-4842 for free and confidential permitting assistance." &
                vbNewLine & vbNewLine &
                "If you have any questions or concerns regarding your application, please contact me at " &
                StaffPhone & " or via e-mail at " & StaffEmail & ". Any written correspondence " &
                "should reference the above application number that has been assigned to this application " &
                "and the facility's AIRS number."

            If Not CreateEmail(Subject, Body, {EmailAddress}) Then
                MsgBox("There was an error sending the message. Please try again.", MsgBoxStyle.OkOnly, "Error")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Nothing
        End Try
    End Sub
#Region "SIP Subpart"
    Sub LoadSSPPSIPSubPartInformation()
        Try
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            dgvSIPSubParts.Rows.Clear()
            dgvSIPSubParts.Columns.Clear()
            dgvSIPSubPartDelete.Rows.Clear()
            dgvSIPSubPartDelete.Columns.Clear()
            dgvSIPSubpartAddEdit.Rows.Clear()
            dgvSIPSubpartAddEdit.Columns.Clear()

            SQL = "select distinct   " & _
            "strAIRSnumber, " & _
            "'' as AppNum, " & _
            "AIRBRANCH.apbsubpartdata.strSubpart, " & _
            "strDescription, CreateDateTime " & _
            "from AIRBRANCH.APBsubpartdata, AIRBRANCH.LookUpSubPartSIP   " & _
            "where AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubpartSIP.strSubpart   " & _
            "and AIRBRANCH.APBSubPartData.strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
            "and Active = '1' "

            dgvSIPSubParts.RowHeadersVisible = False
            dgvSIPSubParts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIPSubParts.AllowUserToResizeColumns = True
            dgvSIPSubParts.AllowUserToAddRows = False
            dgvSIPSubParts.AllowUserToDeleteRows = False
            dgvSIPSubParts.AllowUserToOrderColumns = True
            dgvSIPSubParts.AllowUserToResizeRows = True
            dgvSIPSubParts.ColumnHeadersHeight = "35"

            dgvSIPSubParts.Columns.Add("strApplicationNumber", "App #")
            dgvSIPSubParts.Columns("strApplicationNumber").DisplayIndex = 0
            dgvSIPSubParts.Columns("strApplicationNumber").Width = 50
            dgvSIPSubParts.Columns("strApplicationNumber").Visible = True

            dgvSIPSubParts.Columns.Add("strSubpart", "Subpart")
            dgvSIPSubParts.Columns("strSubpart").DisplayIndex = 1
            dgvSIPSubParts.Columns("strSubpart").Width = 75
            dgvSIPSubParts.Columns("strSubpart").Visible = True

            dgvSIPSubParts.Columns.Add("strDescription", "Desc.")
            dgvSIPSubParts.Columns("strDescription").DisplayIndex = 2
            dgvSIPSubParts.Columns("strDescription").Width = 200
            dgvSIPSubParts.Columns("strDescription").ReadOnly = True

            dgvSIPSubParts.Columns.Add("CreateDateTime", "Action Date")
            dgvSIPSubParts.Columns("CreateDateTime").DisplayIndex = 3
            dgvSIPSubParts.Columns("CreateDateTime").Width = 100
            dgvSIPSubParts.Columns("CreateDateTime").ReadOnly = True
            dgvSIPSubParts.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvSIPSubParts.Columns.Add("Origins", "Action")
            dgvSIPSubParts.Columns("Origins").DisplayIndex = 4
            dgvSIPSubParts.Columns("Origins").Width = 50
            dgvSIPSubParts.Columns("Origins").ReadOnly = True

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSIPSubParts)
                If IsDBNull(dr.Item("AppNum")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("AppNum")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If
                dgvRow.Cells(4).Value = "Existing"
                dgvSIPSubParts.Rows.Add(dgvRow)
            End While
            dr.Close()

            If dgvSIPSubParts.RowCount > 0 Then
                For i = 0 To dgvSIPSubParts.RowCount - 1
                    SubPart = dgvSIPSubParts.Item(1, i).Value
                    SQL = "select " & _
                    "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strSubpart, strApplicationActivity,   " & _
                    "CreateDateTime " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPSubpartData   " & _
                    "where AIRBRANCH.SSPPSubpartData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber  " & _
                    "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                    "and substr(strSubpartkey, 6,1) = '0'  " & _
                    "and strSubpart = '" & SubPart & "'  " & _
                    "and AIRBRANCH.SSPPSubpartData.strApplicationNumber  = '" & txtApplicationNumber.Text & "'  " & _
                    "order by createdatetime "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                        Else
                            dgvSIPSubParts(0, i).Value = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strApplicationActivity")) Then
                        Else
                            Select Case dr.Item("strApplicationActivity").ToString
                                Case "1"
                                    'Added' 
                                    dgvSIPSubParts(4, i).Value = "Added"
                                Case "2"
                                    'Modified' 
                                    dgvSIPSubParts(4, i).Value = "Modify"
                                Case Else
                                    'Existing
                                    dgvSIPSubParts(4, i).Value = "Existing"
                            End Select
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                        Else
                            dgvSIPSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr.Close()
                Next
            End If

            dgvSIPSubPartDelete.RowHeadersVisible = False
            dgvSIPSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIPSubPartDelete.AllowUserToResizeColumns = True
            dgvSIPSubPartDelete.AllowUserToAddRows = False
            dgvSIPSubPartDelete.AllowUserToDeleteRows = False
            dgvSIPSubPartDelete.AllowUserToOrderColumns = True
            dgvSIPSubPartDelete.AllowUserToResizeRows = True
            dgvSIPSubPartDelete.ColumnHeadersHeight = "35"

            dgvSIPSubPartDelete.Columns.Add("strSubpart", "Subpart")
            dgvSIPSubPartDelete.Columns("strSubpart").DisplayIndex = 0
            dgvSIPSubPartDelete.Columns("strSubpart").Width = 75
            dgvSIPSubPartDelete.Columns("strSubpart").Visible = True

            dgvSIPSubPartDelete.Columns.Add("strDescription", "Desc.")
            dgvSIPSubPartDelete.Columns("strDescription").DisplayIndex = 1
            dgvSIPSubPartDelete.Columns("strDescription").Width = 100
            dgvSIPSubPartDelete.Columns("strDescription").ReadOnly = True

            dgvSIPSubpartAddEdit.RowHeadersVisible = False
            dgvSIPSubpartAddEdit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIPSubpartAddEdit.AllowUserToResizeColumns = True
            dgvSIPSubpartAddEdit.AllowUserToAddRows = False
            dgvSIPSubpartAddEdit.AllowUserToDeleteRows = False
            dgvSIPSubpartAddEdit.AllowUserToOrderColumns = True
            dgvSIPSubpartAddEdit.AllowUserToResizeRows = True
            dgvSIPSubpartAddEdit.ReadOnly = True
            dgvSIPSubpartAddEdit.ColumnHeadersHeight = "35"

            dgvSIPSubpartAddEdit.Columns.Add("strSubpart", "Subpart")
            dgvSIPSubpartAddEdit.Columns("strSubpart").DisplayIndex = 0
            dgvSIPSubpartAddEdit.Columns("strSubpart").Width = 75
            dgvSIPSubpartAddEdit.Columns("strSubpart").Visible = True

            dgvSIPSubpartAddEdit.Columns.Add("strDescription", "Desc.")
            dgvSIPSubpartAddEdit.Columns("strDescription").DisplayIndex = 1
            dgvSIPSubpartAddEdit.Columns("strDescription").Width = 100
            dgvSIPSubpartAddEdit.Columns("strDescription").ReadOnly = True

            dgvSIPSubpartAddEdit.Columns.Add("CreateDateTime", "Action Date")
            dgvSIPSubpartAddEdit.Columns("CreateDateTime").DisplayIndex = 2
            dgvSIPSubpartAddEdit.Columns("CreateDateTime").Width = 100
            dgvSIPSubpartAddEdit.Columns("CreateDateTime").ReadOnly = True

            dgvSIPSubpartAddEdit.Columns.Add("Origins", "Action")
            dgvSIPSubpartAddEdit.Columns("Origins").DisplayIndex = 3
            dgvSIPSubpartAddEdit.Columns("Origins").Width = 100
            dgvSIPSubpartAddEdit.Columns("Origins").ReadOnly = True

            SQL = "select  " & _
            "strAIRSNumber, " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
            "AIRBRANCH.SSPPSubPartData.strSubpart, strDescription,  " & _
            "case when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity ='1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "else strApplicationActivity  " & _
            "end Action,  " & _
            "CreatedateTime  " & _
            "from AIRBRANCH.SSPPSubpartData, AIRBRANCH.SSPPApplicationMaster,   " & _
            "AIRBRANCH.LookUpSubPartSIP   " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & _
            "AIRBRANCH.SSPPSubpartData.strApplicationNumber   " & _
            "and AIRBRANCH.SSPPSubPartData.strSubPart = AIRBRANCH.LookUpSubPartSIP.strSubPart  " & _
            "and AIRBRANCH.SSPPSubpartData.strSubpartKey  = '" & txtApplicationNumber.Text & "0'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Desc = ""
                Else
                    Desc = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("Action")) Then
                    Action = ""
                Else
                    Action = dr.Item("Action")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    CreateDateTime = ""
                Else
                    CreateDateTime = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If
                Select Case Action
                    Case "Removed"
                        temp = ""
                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            If SubPart = dgvSIPSubParts(1, i).Value Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                dgvSIPSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                With Me.dgvSIPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.Tomato
                                End With
                            End If
                        Next
                        If temp = "" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvSIPSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Removed"
                            dgvSIPSubParts.Rows.Add(dgvRow)
                            i = dgvSIPSubParts.Rows.Count - 1
                            With Me.dgvSIPSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.Tomato
                            End With
                        End If
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            If SubPart = dgvSIPSubParts(1, i).Value Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                dgvSIPSubParts(4, i).Value = "Added"
                                temp = "Added"
                                With Me.dgvSIPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightGreen
                                End With
                            End If
                        Next
                        If temp <> "Added" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvSIPSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Added"
                            dgvSIPSubParts.Rows.Add(dgvRow)
                            i = dgvSIPSubParts.Rows.Count - 1
                            With Me.dgvSIPSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.LightGreen
                            End With
                        End If
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvSIPSubpartAddEdit.Rows.Count - 1
                        With Me.dgvSIPSubpartAddEdit.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightGreen
                        End With
                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            If SubPart = dgvSIPSubParts(1, i).Value Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                With Me.dgvSIPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightBlue
                                End With
                            End If
                        Next
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                    Case Else
                End Select
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPDelete.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
            Action = dgvSIPSubParts(4, dgvSIPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                If dgvSIPSubpartAddEdit(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvSIPSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvSIPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                        dgvSIPSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value

                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPUndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvSIPSubPartDelete.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvSIPSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvSIPSubPartDelete(0, dgvSIPSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvSIPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
                dgvSIPSubPartDelete.Rows.Remove(dgvSIPSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPDeleteAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim j As Integer = 0

            For i = 0 To dgvSIPSubParts.Rows.Count - 1
                Subpart = dgvSIPSubParts(1, i).Value
                Action = dgvSIPSubParts(4, i).Value

                For j = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    If dgvSIPSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be deleted from the Facility.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                        dgvSIPSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvSIPSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPUndeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPUndeleteAll.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvSIPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvSIPSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearSIPDeletes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSIPDeletes.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvSIPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvSIPSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewSIPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewSIPSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If chbCDS_0.Checked = False Then
                MsgBox("The SIP Subpart is not checked on the Tracking Log tab. " & vbCrLf & _
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation, _
                        "Application Tracking")
                Exit Sub
            End If

            Subpart = cboSIPSubpart.SelectedValue
            If Subpart <> "" Then
                Desc = Replace(cboSIPSubpart.Text, Subpart & " - ", "")
            End If

            temp2 = ""
            For i = 0 To dgvSIPSubParts.Rows.Count - 1
                If dgvSIPSubParts(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The SIP Subpart already exists for this application.", MsgBoxStyle.Information, _
                           "Application Tracking")
                    Exit Sub
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSIPSubParts)
                dgvRow.Cells(0).Value = txtApplicationNumber.Text
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = OracleDate
                dgvRow.Cells(4).Value = "Added"
                dgvSIPSubParts.Rows.Add(dgvRow)
                i = dgvSIPSubParts.Rows.Count - 1
                With Me.dgvSIPSubParts.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If

            temp2 = ""
            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                If dgvSIPSubpartAddEdit(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = OracleDate
                dgvRow.Cells(3).Value = "Added"
                dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvSIPSubpartAddEdit.Rows.Count - 1
                With Me.dgvSIPSubpartAddEdit.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If


            Exit Sub
            Subpart = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
            Desc = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value

            If i > 0 Then
                temp = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            Else
                dgvRow.CreateCells(dgvSIPSubPartDelete)
                dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                dgvSIPSubPartDelete.Rows.Add(dgvRow)
                With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                    .DefaultCellStyle.BackColor = Color.Tomato
                End With
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPEdit.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
            Action = dgvSIPSubParts(4, dgvSIPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                If dgvSIPSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be Modified by this Application.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvSIPSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    If dgvSIPSubpartAddEdit(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(2).Value = dgvSIPSubParts(3, dgvSIPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvSIPSubParts(3, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                    With Me.dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.LightBlue
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPUnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubpartAddEdit.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvSIPSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvSIPSubpartAddEdit(0, dgvSIPSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvSIPSubpartAddEdit(3, dgvSIPSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    With Me.dgvSIPSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    dgvSIPSubpartAddEdit.Rows.Remove(dgvSIPSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPEditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                Subpart = dgvSIPSubParts(1, i).Value
                Action = dgvSIPSubParts(4, i).Value

                For j As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be Modified by this Application.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    If dgvSIPSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                        dgvRow.Cells(2).Value = dgvSIPSubParts(3, i).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvSIPSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSIPUneditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSIPUneditAll.Click
        Try
            Dim i As Integer
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvSIPSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        If dgvSIPSubParts(4, j).Value = "Existing" Then
                            With Me.dgvSIPSubParts.Rows(j)
                                .DefaultCellStyle.BackColor = Color.White
                            End With
                            TempRemove = i & "," & TempRemove
                        End If
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                ' If Action <> "Added" Then
                dgvSIPSubpartAddEdit.Rows.RemoveAt(i)
                'End If
                TempRemove = Replace(TempRemove, i & ",", "")
            Loop
            'dgvSIPSubpartAddEdit.Rows.Clear()
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAddModifiedSIPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddModifiedSIPs.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvSIPSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    temp = dgvSIPSubParts(1, j).Value
                    If dgvSIPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvSIPSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    With Me.dgvSIPSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    If Action = "Added" Then
                        dgvSIPSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvSIPSubpartAddEdit.Rows.Clear()
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveSIPSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "0' " & _
            "and strApplicationActivity <> '1' "

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "0' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "0', " & _
                "'" & Subpart & "', '0', " & _
                "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Action = dgvSIPSubpartAddEdit(3, i).Value
                Subpart = dgvSIPSubpartAddEdit(0, i).Value

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "0'  " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                Select Case Action
                    Case "Added"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "0', " & _
                        "'" & Subpart & "', '1', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case "Modify"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "0', " & _
                        "'" & Subpart & "', '2', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case Else
                        SQL = ""
                End Select
                If SQL <> "" Then
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Next

            LoadSSPPSIPSubPartInformation()


            Exit Sub


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSIPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSIPSubpart.Click
        Try
            If chbCDS_0.Checked = False Then
                MsgBox("WARNING DATA NOT SAVED:" & vbCrLf & _
                       "On the Tracking Log tab select the air program code 0 - SIP. " & _
                       "If you do not check this air program code the subpart(s) cannot be saved.", _
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else

            End If
            SaveSIPSubpart()
            MsgBox("SIP Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "NSPS Subpart"
    Sub LoadSSPPNSPSSubPartInformation()
        Try
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            dgvNSPSSubParts.Rows.Clear()
            dgvNSPSSubParts.Columns.Clear()
            dgvNSPSSubPartDelete.Rows.Clear()
            dgvNSPSSubPartDelete.Columns.Clear()
            dgvNSPSSubpartAddEdit.Rows.Clear()
            dgvNSPSSubpartAddEdit.Columns.Clear()

            SQL = "select distinct   " & _
            "strAIRSnumber, " & _
            "'' as AppNum, " & _
            "AIRBRANCH.apbsubpartdata.strSubpart, " & _
            "strDescription, CreateDateTime " & _
            "from AIRBRANCH.APBsubpartdata, AIRBRANCH.LookUpSubPart60  " & _
            "where AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubpart60.strSubpart   " & _
            "and AIRBRANCH.APBSubPartData.strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
            "and Active = '1' "

            dgvNSPSSubParts.RowHeadersVisible = False
            dgvNSPSSubParts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSSubParts.AllowUserToResizeColumns = True
            dgvNSPSSubParts.AllowUserToAddRows = False
            dgvNSPSSubParts.AllowUserToDeleteRows = False
            dgvNSPSSubParts.AllowUserToOrderColumns = True
            dgvNSPSSubParts.AllowUserToResizeRows = True
            dgvNSPSSubParts.ColumnHeadersHeight = "35"

            dgvNSPSSubParts.Columns.Add("strApplicationNumber", "App #")
            dgvNSPSSubParts.Columns("strApplicationNumber").DisplayIndex = 0
            dgvNSPSSubParts.Columns("strApplicationNumber").Width = 50
            dgvNSPSSubParts.Columns("strApplicationNumber").Visible = True

            dgvNSPSSubParts.Columns.Add("strSubpart", "Subpart")
            dgvNSPSSubParts.Columns("strSubpart").DisplayIndex = 1
            dgvNSPSSubParts.Columns("strSubpart").Width = 75
            dgvNSPSSubParts.Columns("strSubpart").Visible = True

            dgvNSPSSubParts.Columns.Add("strDescription", "Desc.")
            dgvNSPSSubParts.Columns("strDescription").DisplayIndex = 2
            dgvNSPSSubParts.Columns("strDescription").Width = 200
            dgvNSPSSubParts.Columns("strDescription").ReadOnly = True

            dgvNSPSSubParts.Columns.Add("CreateDateTime", "Action Date")
            dgvNSPSSubParts.Columns("CreateDateTime").DisplayIndex = 3
            dgvNSPSSubParts.Columns("CreateDateTime").Width = 100
            dgvNSPSSubParts.Columns("CreateDateTime").ReadOnly = True
            dgvNSPSSubParts.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvNSPSSubParts.Columns.Add("Origins", "Action")
            dgvNSPSSubParts.Columns("Origins").DisplayIndex = 4
            dgvNSPSSubParts.Columns("Origins").Width = 50
            dgvNSPSSubParts.Columns("Origins").ReadOnly = True

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSSubParts)
                If IsDBNull(dr.Item("AppNum")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("AppNum")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If
                dgvRow.Cells(4).Value = "Existing"
                dgvNSPSSubParts.Rows.Add(dgvRow)
            End While
            dr.Close()

            If dgvNSPSSubParts.RowCount > 0 Then
                For i = 0 To dgvNSPSSubParts.RowCount - 1
                    SubPart = dgvNSPSSubParts.Item(1, i).Value
                    SQL = "select " & _
                    "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strSubpart, strApplicationActivity,   " & _
                    "CreateDateTime " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPSubpartData   " & _
                    "where AIRBRANCH.SSPPSubpartData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber  " & _
                    "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                    "and substr(strSubpartkey, 6,1) = '9'  " & _
                    "and strSubpart = '" & SubPart & "'  " & _
                    "and AIRBRANCH.SSPPSubpartData.strApplicationNumber  = '" & txtApplicationNumber.Text & "'  " & _
                    "order by createdatetime "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                        Else
                            dgvNSPSSubParts(0, i).Value = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strApplicationActivity")) Then
                        Else
                            Select Case dr.Item("strApplicationActivity").ToString
                                Case "1"
                                    'Added' 
                                    dgvNSPSSubParts(4, i).Value = "Added"
                                Case "2"
                                    'Modified' 
                                    dgvNSPSSubParts(4, i).Value = "Modify"
                                Case Else
                                    'Existing
                                    dgvNSPSSubParts(4, i).Value = "Existing"
                            End Select
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                        Else
                            dgvNSPSSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr.Close()
                Next
            End If

            dgvNSPSSubPartDelete.RowHeadersVisible = False
            dgvNSPSSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSSubPartDelete.AllowUserToResizeColumns = True
            dgvNSPSSubPartDelete.AllowUserToAddRows = False
            dgvNSPSSubPartDelete.AllowUserToDeleteRows = False
            dgvNSPSSubPartDelete.AllowUserToOrderColumns = True
            dgvNSPSSubPartDelete.AllowUserToResizeRows = True
            dgvNSPSSubPartDelete.ColumnHeadersHeight = "35"

            dgvNSPSSubPartDelete.Columns.Add("strSubpart", "Subpart")
            dgvNSPSSubPartDelete.Columns("strSubpart").DisplayIndex = 0
            dgvNSPSSubPartDelete.Columns("strSubpart").Width = 75
            dgvNSPSSubPartDelete.Columns("strSubpart").Visible = True

            dgvNSPSSubPartDelete.Columns.Add("strDescription", "Desc.")
            dgvNSPSSubPartDelete.Columns("strDescription").DisplayIndex = 1
            dgvNSPSSubPartDelete.Columns("strDescription").Width = 100
            dgvNSPSSubPartDelete.Columns("strDescription").ReadOnly = True

            dgvNSPSSubpartAddEdit.RowHeadersVisible = False
            dgvNSPSSubpartAddEdit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSSubpartAddEdit.AllowUserToResizeColumns = True
            dgvNSPSSubpartAddEdit.AllowUserToAddRows = False
            dgvNSPSSubpartAddEdit.AllowUserToDeleteRows = False
            dgvNSPSSubpartAddEdit.AllowUserToOrderColumns = True
            dgvNSPSSubpartAddEdit.AllowUserToResizeRows = True
            dgvNSPSSubpartAddEdit.ReadOnly = True
            dgvNSPSSubpartAddEdit.ColumnHeadersHeight = "35"

            dgvNSPSSubpartAddEdit.Columns.Add("strSubpart", "Subpart")
            dgvNSPSSubpartAddEdit.Columns("strSubpart").DisplayIndex = 0
            dgvNSPSSubpartAddEdit.Columns("strSubpart").Width = 75
            dgvNSPSSubpartAddEdit.Columns("strSubpart").Visible = True

            dgvNSPSSubpartAddEdit.Columns.Add("strDescription", "Desc.")
            dgvNSPSSubpartAddEdit.Columns("strDescription").DisplayIndex = 1
            dgvNSPSSubpartAddEdit.Columns("strDescription").Width = 100
            dgvNSPSSubpartAddEdit.Columns("strDescription").ReadOnly = True

            dgvNSPSSubpartAddEdit.Columns.Add("CreateDateTime", "Action Date")
            dgvNSPSSubpartAddEdit.Columns("CreateDateTime").DisplayIndex = 2
            dgvNSPSSubpartAddEdit.Columns("CreateDateTime").Width = 100
            dgvNSPSSubpartAddEdit.Columns("CreateDateTime").ReadOnly = True

            dgvNSPSSubpartAddEdit.Columns.Add("Origins", "Action")
            dgvNSPSSubpartAddEdit.Columns("Origins").DisplayIndex = 3
            dgvNSPSSubpartAddEdit.Columns("Origins").Width = 100
            dgvNSPSSubpartAddEdit.Columns("Origins").ReadOnly = True

            SQL = "select  " & _
            "strAIRSNumber, " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
            "AIRBRANCH.SSPPSubPartData.strSubpart, strDescription,  " & _
            "case when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity ='1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "else strApplicationActivity  " & _
            "end Action,  " & _
            "CreatedateTime  " & _
            "from AIRBRANCH.SSPPSubpartData, AIRBRANCH.SSPPApplicationMaster,   " & _
            "AIRBRANCH.LookUpSubPart60   " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & _
            "AIRBRANCH.SSPPSubpartData.strApplicationNumber   " & _
            "and AIRBRANCH.SSPPSubPartData.strSubPart = AIRBRANCH.LookUpSubPart60.strSubPart  " & _
            "and AIRBRANCH.SSPPSubpartData.strSubPartKey  = '" & txtApplicationNumber.Text & "9'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Desc = ""
                Else
                    Desc = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("Action")) Then
                    Action = ""
                Else
                    Action = dr.Item("Action")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    CreateDateTime = ""
                Else
                    CreateDateTime = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If

                Select Case Action
                    Case "Removed"
                        temp = ""
                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            If SubPart = dgvNSPSSubParts(1, i).Value Then
                                dgvNSPSSubParts(0, i).Value = AppNum
                                dgvNSPSSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                With Me.dgvNSPSSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.Tomato
                                End With
                            End If
                        Next

                        If temp = "" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvNSPSSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Removed"
                            dgvNSPSSubParts.Rows.Add(dgvRow)
                            i = dgvNSPSSubParts.Rows.Count - 1
                            With Me.dgvNSPSSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.Tomato
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            If SubPart = dgvNSPSSubParts(1, i).Value Then
                                dgvNSPSSubParts(4, i).Value = "Added"
                                temp = "Added"
                                With Me.dgvNSPSSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightGreen
                                End With
                            End If
                        Next
                        If temp <> "Added" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvNSPSSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Added"
                            dgvNSPSSubParts.Rows.Add(dgvRow)
                            i = dgvNSPSSubParts.Rows.Count - 1
                            With Me.dgvNSPSSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.LightGreen
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvNSPSSubpartAddEdit.Rows.Count - 1
                        With Me.dgvNSPSSubpartAddEdit.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightGreen
                        End With

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            If SubPart = dgvNSPSSubParts(1, i).Value Then
                                dgvNSPSSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                With Me.dgvNSPSSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightBlue
                                End With
                            End If
                        Next

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                    Case Else

                End Select

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSDelete.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
            Action = dgvNSPSSubParts(4, dgvNSPSSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                If dgvNSPSSubpartAddEdit(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvNSPSSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvNSPSSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                        dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value

                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSUndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvNSPSSubPartDelete.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvNSPSSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvNSPSSubPartDelete(0, dgvNSPSSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNSPSSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
                dgvNSPSSubPartDelete.Rows.Remove(dgvNSPSSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSDeleteAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim j As Integer = 0

            For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                Subpart = dgvNSPSSubParts(1, i).Value
                Action = dgvNSPSSubParts(4, i).Value

                For j = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    If dgvNSPSSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be deleted from the Facility.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                        dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvNSPSSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSUndeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSUndeleteAll.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNSPSSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvNSPSSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearNSPSDeletes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNSPSDeletes.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNSPSSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvNSPSSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewNSPSSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewNSPSSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If chbCDS_9.Checked = False Then
                MsgBox("The NSPS Subpart is not checked on the Tracking Log tab. " & vbCrLf & _
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation, _
                        "Application Tracking")
                Exit Sub
            End If

            Subpart = cboNSPSSubpart.SelectedValue
            If Subpart <> "" Then
                Desc = Replace(cboNSPSSubpart.Text, Subpart & " - ", "")
            End If

            temp2 = ""
            For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                If dgvNSPSSubParts(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The NSPS Subpart already exists for this application.", MsgBoxStyle.Information, _
                        "Application Tracking")
                    Exit Sub
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSSubParts)
                dgvRow.Cells(0).Value = txtApplicationNumber.Text
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = OracleDate
                dgvRow.Cells(4).Value = "Added"
                dgvNSPSSubParts.Rows.Add(dgvRow)
                i = dgvNSPSSubParts.Rows.Count - 1
                With Me.dgvNSPSSubParts.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If

            temp2 = ""
            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                If dgvNSPSSubpartAddEdit(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = OracleDate
                dgvRow.Cells(3).Value = "Added"
                dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvNSPSSubpartAddEdit.Rows.Count - 1
                With Me.dgvNSPSSubpartAddEdit.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If


            Exit Sub
            Subpart = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
            Desc = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value

            If i > 0 Then
                temp = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            Else
                dgvRow.CreateCells(dgvNSPSSubPartDelete)
                dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                    .DefaultCellStyle.BackColor = Color.Tomato
                End With
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSEdit.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
            Action = dgvNSPSSubParts(4, dgvNSPSSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                If dgvNSPSSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be Modified by this Application.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvNSPSSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    If dgvNSPSSubpartAddEdit(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(2).Value = dgvNSPSSubParts(3, dgvNSPSSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNSPSSubParts(3, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                    With Me.dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.LightBlue
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSUnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubpartAddEdit.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvNSPSSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvNSPSSubpartAddEdit(0, dgvNSPSSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvNSPSSubpartAddEdit(3, dgvNSPSSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    With Me.dgvNSPSSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    dgvNSPSSubpartAddEdit.Rows.Remove(dgvNSPSSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSEditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                Subpart = dgvNSPSSubParts(1, i).Value
                Action = dgvNSPSSubParts(4, i).Value

                For j As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be Modified by this Application.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    If dgvNSPSSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                        dgvRow.Cells(2).Value = dgvNSPSSubParts(3, i).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvNSPSSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNSPSUneditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        If dgvNSPSSubParts(4, j).Value = "Existing" Then
                            With Me.dgvNSPSSubParts.Rows(j)
                                .DefaultCellStyle.BackColor = Color.White
                            End With
                            TempRemove = i & "," & TempRemove
                        End If
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvNSPSSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = Replace(TempRemove, i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAddModifiedNSPSs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddModifiedNSPSs.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    temp = dgvNSPSSubParts(1, j).Value
                    If dgvNSPSSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvNSPSSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    With Me.dgvNSPSSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    If Action = "Added" Then
                        dgvNSPSSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvNSPSSubpartAddEdit.Rows.Clear()
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveNSPSSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
          "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
          "and strApplicationActivity <> '1' "

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "9' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                "'" & Subpart & "', '0', " & _
                "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Action = dgvNSPSSubpartAddEdit(3, i).Value
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "9'  " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                Select Case Action
                    Case "Added"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                        "'" & Subpart & "', '1', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case "Modify"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                        "'" & Subpart & "', '2', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case Else
                        SQL = ""
                End Select
                If SQL <> "" Then
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Next

            LoadSSPPNSPSSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveNSPSSubpart2()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            SQL = "Select " & _
            "strSubpart " & _
            "from AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
            "and strApplicationActivity = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strSubpart")) Then
                    Subpart = ""
                Else
                    Subpart = dr.Item("strSubpart")
                End If

                temp = ""
                If Subpart <> "" Then
                    For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                        If Subpart = dgvNSPSSubParts(1, i).Value Then
                            temp = "Ignore"
                        End If
                    Next
                    If temp <> "Ignore" Then
                        SQL = "Update AIRBRANCH.APBSubpartData set " & _
                        "Active = '9', " & _
                        "updateUser = '" & CurrentUser.UserID & "', " & _
                        "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                        "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                        "and strSubpart = '" & Subpart & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
                        "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
                        "and strApplicationActivity = '1' " & _
                        "and strSubpart = '" & Subpart & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()
                    End If
                End If
            End While
            dr.Close()

            For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                Subpart = dgvNSPSSubParts(1, i).Value

                SQL = "Select strSubPart " & _
                "from AIRBRANCH.APBSubpartData " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.APBSubpartData set " & _
                    "Active = '1', " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                    "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                    "and strSubpart = '" & Subpart & "' "
                Else
                    SQL = "INSERT INTO AIRBRANCH.APBSUBPARTDATA " & _
                    "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " & _
                    "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " & _
                    "  ) VALUES " & _
                    "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                    "'" & Replace(Subpart, "'", "''") & "', '" & CurrentUser.UserID & "', " & _
                    "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')), '1', " & _
                    "(to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')))"
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
                "and strSubpart = '" & Subpart & "' " & _
                "and strApplicationActivity = '1' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then

                End If
            Next

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
            "and strApplicationActivity <> '1' "

            'SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            '"where strSubpartKey = '" & txtApplicationNumber.Text & "9' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            'Removed 
            For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value

                SQL = "Update AIRBRANCH.APBSubpartData set " & _
                "Active = '9', " & _
                "updateUser = '" & CurrentUser.UserID & "', " & _
                "updateDateTime = (to_date(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                "and strSubpart = '" & Subpart & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "9' " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update AIRBRANCH.SSPPSubpartData set " & _
                    "strApplicationActivity = '9', " & _
                    "updateUser = '" & CurrentUser.UserID & "', " & _
                    "updateDateTime = (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                    "and strSubPart = '" & Subpart & "' " & _
                    "and strSubPartKey = '" & txtApplicationNumber.Text & "9' "
                Else
                    SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                    "values " & _
                    "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                    "'" & Subpart & "', '9', " & _
                    "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                    "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            'Added/Modified
            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Action = dgvNSPSSubpartAddEdit(3, i).Value
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "9'  " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                Select Case Action
                    Case "Added"
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.SSPPSubpartData set " & _
                            "strApplicationActivity = '1', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                            "and strSubPart = '" & Subpart & "' " & _
                            "and strSubPartKey = '" & txtApplicationNumber.Text & "9' "
                        Else
                            SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                            "values " & _
                            "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                            "'" & Subpart & "', '1', " & _
                            "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                            "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                        End If
                    Case "Modify"
                        If recExist = True Then
                            SQL = "Update AIRBRANCH.SSPPSubpartData set " & _
                            "strApplicationActivity = '2', " & _
                            "updateUser = '" & CurrentUser.UserID & "', " & _
                            "updateDateTime = (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')) " & _
                            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' " & _
                            "and strSubPart = '" & Subpart & "' " & _
                            "and strSubPartKey = '" & txtApplicationNumber.Text & "9' "
                        Else
                            SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                            "values " & _
                            "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "9', " & _
                            "'" & Subpart & "', '2', " & _
                            "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                            "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                        End If
                    Case Else
                        SQL = ""
                End Select
                If SQL <> "" Then
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

            Next

            SQL = "Select " & _
            "strPollutantKey " & _
            "from AIRBRANCH.AFSAirPollutantData " & _
            "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "9' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update AIRBRANCH.AFSAirPollutantData set " & _
                "strUpdateStatus = 'C' " & _
                "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "9' " & _
                "and strUpdateStatus <> 'A' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Else
                SQL = "INSERT INTO AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " & _
                "  ( " & _
                "    STRAIRSNUMBER , STRAIRPOLLUTANTKEY , STRPOLLUTANTKEY , " & _
                "    STRCOMPLIANCESTATUS , STRMODIFINGPERSON , DATMODIFINGDATE , " & _
                "    STROPERATIONALSTATUS " & _
                "  ) " & _
                "  VALUES" & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                "'OT', '3', " & _
                "'" & CurrentUser.UserID & "', '" & OracleDate & "', " & _
                "'O') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Insert into AIRBRANCH.AFSAirPollutantData " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                "'OT', 'A', " & _
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            LoadSSPPNSPSSubPartInformation()

            MsgBox("NSPS Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveNSPSSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNSPSSubpart.Click
        Try
            If chbCDS_9.Checked = False Then
                MsgBox("WARNING DATA NOT SAVED:" & vbCrLf & _
                       "On the Tracking Log tab select the air program code 9 - NSPS. " & _
                       "If you do not check this air program code the subpart(s) cannot be saved.", _
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            End If
            SaveNSPSSubpart()
            MsgBox("NSPS Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "NESHAP Subpart"
    Sub LoadSSPPNESHAPSubPartInformation()
        Try
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            dgvNESHAPSubParts.Rows.Clear()
            dgvNESHAPSubParts.Columns.Clear()
            dgvNESHAPSubPartDelete.Rows.Clear()
            dgvNESHAPSubPartDelete.Columns.Clear()
            dgvNESHAPSubpartAddEdit.Rows.Clear()
            dgvNESHAPSubpartAddEdit.Columns.Clear()

            SQL = "select distinct   " & _
            "strAIRSnumber, " & _
            "'' as AppNum, " & _
            "AIRBRANCH.apbsubpartdata.strSubpart, " & _
            "strDescription, CreateDateTime " & _
            "from AIRBRANCH.APBsubpartdata, AIRBRANCH.LookUpSubPart61   " & _
            "where AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPart61.strSubpart   " & _
            "and AIRBRANCH.APBSubPartData.strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
            "and Active = '1' "

            dgvNESHAPSubParts.RowHeadersVisible = False
            dgvNESHAPSubParts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAPSubParts.AllowUserToResizeColumns = True
            dgvNESHAPSubParts.AllowUserToAddRows = False
            dgvNESHAPSubParts.AllowUserToDeleteRows = False
            dgvNESHAPSubParts.AllowUserToOrderColumns = True
            dgvNESHAPSubParts.AllowUserToResizeRows = True
            dgvNESHAPSubParts.ColumnHeadersHeight = "35"

            dgvNESHAPSubParts.Columns.Add("strApplicationNumber", "App #")
            dgvNESHAPSubParts.Columns("strApplicationNumber").DisplayIndex = 0
            dgvNESHAPSubParts.Columns("strApplicationNumber").Width = 50
            dgvNESHAPSubParts.Columns("strApplicationNumber").Visible = True

            dgvNESHAPSubParts.Columns.Add("strSubpart", "Subpart")
            dgvNESHAPSubParts.Columns("strSubpart").DisplayIndex = 1
            dgvNESHAPSubParts.Columns("strSubpart").Width = 75
            dgvNESHAPSubParts.Columns("strSubpart").Visible = True

            dgvNESHAPSubParts.Columns.Add("strDescription", "Desc.")
            dgvNESHAPSubParts.Columns("strDescription").DisplayIndex = 2
            dgvNESHAPSubParts.Columns("strDescription").Width = 200
            dgvNESHAPSubParts.Columns("strDescription").ReadOnly = True

            dgvNESHAPSubParts.Columns.Add("CreateDateTime", "Action Date")
            dgvNESHAPSubParts.Columns("CreateDateTime").DisplayIndex = 3
            dgvNESHAPSubParts.Columns("CreateDateTime").Width = 100
            dgvNESHAPSubParts.Columns("CreateDateTime").ReadOnly = True
            dgvNESHAPSubParts.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvNESHAPSubParts.Columns.Add("Origins", "Action")
            dgvNESHAPSubParts.Columns("Origins").DisplayIndex = 4
            dgvNESHAPSubParts.Columns("Origins").Width = 50
            dgvNESHAPSubParts.Columns("Origins").ReadOnly = True

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNESHAPSubParts)
                If IsDBNull(dr.Item("AppNum")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("AppNum")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If
                dgvRow.Cells(4).Value = "Existing"
                dgvNESHAPSubParts.Rows.Add(dgvRow)
            End While
            dr.Close()

            If dgvNESHAPSubParts.RowCount > 0 Then
                For i = 0 To dgvNESHAPSubParts.RowCount - 1
                    SubPart = dgvNESHAPSubParts.Item(1, i).Value
                    SQL = "select " & _
                    "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strSubpart, strApplicationActivity,   " & _
                    "CreateDateTime " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPSubpartData   " & _
                    "where AIRBRANCH.SSPPSubpartData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber  " & _
                    "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                    "and substr(strSubpartkey, 6,1) = '8'  " & _
                    "and strSubpart = '" & SubPart & "'  " & _
                        "and AIRBRANCH.SSPPSubpartData.strApplicationNumber  = '" & txtApplicationNumber.Text & "'  " & _
                    "order by createdatetime "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                        Else
                            dgvNESHAPSubParts(0, i).Value = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strApplicationActivity")) Then
                        Else
                            Select Case dr.Item("strApplicationActivity").ToString
                                Case "1"
                                    'Added' 
                                    dgvNESHAPSubParts(4, i).Value = "Added"
                                Case "2"
                                    'Modified' 
                                    dgvNESHAPSubParts(4, i).Value = "Modify"
                                Case Else
                                    'Existing
                                    dgvNESHAPSubParts(4, i).Value = "Existing"
                            End Select
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                        Else
                            dgvNESHAPSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr.Close()
                Next
            End If

            dgvNESHAPSubPartDelete.RowHeadersVisible = False
            dgvNESHAPSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAPSubPartDelete.AllowUserToResizeColumns = True
            dgvNESHAPSubPartDelete.AllowUserToAddRows = False
            dgvNESHAPSubPartDelete.AllowUserToDeleteRows = False
            dgvNESHAPSubPartDelete.AllowUserToOrderColumns = True
            dgvNESHAPSubPartDelete.AllowUserToResizeRows = True
            dgvNESHAPSubPartDelete.ColumnHeadersHeight = "35"

            dgvNESHAPSubPartDelete.Columns.Add("strSubpart", "Subpart")
            dgvNESHAPSubPartDelete.Columns("strSubpart").DisplayIndex = 0
            dgvNESHAPSubPartDelete.Columns("strSubpart").Width = 75
            dgvNESHAPSubPartDelete.Columns("strSubpart").Visible = True

            dgvNESHAPSubPartDelete.Columns.Add("strDescription", "Desc.")
            dgvNESHAPSubPartDelete.Columns("strDescription").DisplayIndex = 1
            dgvNESHAPSubPartDelete.Columns("strDescription").Width = 100
            dgvNESHAPSubPartDelete.Columns("strDescription").ReadOnly = True

            dgvNESHAPSubpartAddEdit.RowHeadersVisible = False
            dgvNESHAPSubpartAddEdit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAPSubpartAddEdit.AllowUserToResizeColumns = True
            dgvNESHAPSubpartAddEdit.AllowUserToAddRows = False
            dgvNESHAPSubpartAddEdit.AllowUserToDeleteRows = False
            dgvNESHAPSubpartAddEdit.AllowUserToOrderColumns = True
            dgvNESHAPSubpartAddEdit.AllowUserToResizeRows = True
            dgvNESHAPSubpartAddEdit.ReadOnly = True
            dgvNESHAPSubpartAddEdit.ColumnHeadersHeight = "35"

            dgvNESHAPSubpartAddEdit.Columns.Add("strSubpart", "Subpart")
            dgvNESHAPSubpartAddEdit.Columns("strSubpart").DisplayIndex = 0
            dgvNESHAPSubpartAddEdit.Columns("strSubpart").Width = 75
            dgvNESHAPSubpartAddEdit.Columns("strSubpart").Visible = True

            dgvNESHAPSubpartAddEdit.Columns.Add("strDescription", "Desc.")
            dgvNESHAPSubpartAddEdit.Columns("strDescription").DisplayIndex = 1
            dgvNESHAPSubpartAddEdit.Columns("strDescription").Width = 100
            dgvNESHAPSubpartAddEdit.Columns("strDescription").ReadOnly = True

            dgvNESHAPSubpartAddEdit.Columns.Add("CreateDateTime", "Action Date")
            dgvNESHAPSubpartAddEdit.Columns("CreateDateTime").DisplayIndex = 2
            dgvNESHAPSubpartAddEdit.Columns("CreateDateTime").Width = 100
            dgvNESHAPSubpartAddEdit.Columns("CreateDateTime").ReadOnly = True

            dgvNESHAPSubpartAddEdit.Columns.Add("Origins", "Action")
            dgvNESHAPSubpartAddEdit.Columns("Origins").DisplayIndex = 3
            dgvNESHAPSubpartAddEdit.Columns("Origins").Width = 100
            dgvNESHAPSubpartAddEdit.Columns("Origins").ReadOnly = True

            SQL = "select  " & _
            "strAIRSNumber, " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
            "AIRBRANCH.SSPPSubPartData.strSubpart, strDescription,  " & _
            "case when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity ='1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "else strApplicationActivity  " & _
            "end Action,  " & _
            "CreatedateTime  " & _
            "from AIRBRANCH.SSPPSubpartData, AIRBRANCH.SSPPApplicationMaster,   " & _
            "AIRBRANCH.LookUpSubPart61   " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & _
            "AIRBRANCH.SSPPSubpartData.strApplicationNumber   " & _
            "and AIRBRANCH.SSPPSubPartData.strSubPart = AIRBRANCH.LookUpSubPart61.strSubPart  " & _
            "and AIRBRANCH.SSPPSubpartData.strSubpartKey  = '" & txtApplicationNumber.Text & "8'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Desc = ""
                Else
                    Desc = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("Action")) Then
                    Action = ""
                Else
                    Action = dr.Item("Action")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    CreateDateTime = ""
                Else
                    CreateDateTime = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If

                Select Case Action
                    Case "Removed"
                        temp = ""
                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            If SubPart = dgvNESHAPSubParts(1, i).Value Then
                                dgvNESHAPSubParts(0, i).Value = AppNum
                                dgvNESHAPSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                With Me.dgvNESHAPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.Tomato
                                End With
                            End If
                        Next

                        If temp = "" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvNESHAPSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Removed"
                            dgvNESHAPSubParts.Rows.Add(dgvRow)
                            i = dgvNESHAPSubParts.Rows.Count - 1
                            With Me.dgvNESHAPSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.Tomato
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            If SubPart = dgvNESHAPSubParts(1, i).Value Then
                                dgvNESHAPSubParts(4, i).Value = "Added"
                                temp = "Added"
                                With Me.dgvNESHAPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightGreen
                                End With
                            End If
                        Next
                        If temp <> "Added" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvNESHAPSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Added"
                            dgvNESHAPSubParts.Rows.Add(dgvRow)
                            i = dgvNESHAPSubParts.Rows.Count - 1
                            With Me.dgvNESHAPSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.LightGreen
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvNESHAPSubpartAddEdit.Rows.Count - 1
                        With Me.dgvNESHAPSubpartAddEdit.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightGreen
                        End With

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            If SubPart = dgvNESHAPSubParts(1, i).Value Then
                                dgvNESHAPSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                With Me.dgvNESHAPSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightBlue
                                End With
                            End If
                        Next

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                    Case Else

                End Select

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPDelete.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
            Action = dgvNESHAPSubParts(4, dgvNESHAPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                If dgvNESHAPSubpartAddEdit(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvNESHAPSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                        dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value

                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPUndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvNESHAPSubPartDelete.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvNESHAPSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvNESHAPSubPartDelete(0, dgvNESHAPSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNESHAPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
                dgvNESHAPSubPartDelete.Rows.Remove(dgvNESHAPSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPDeleteAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim j As Integer = 0

            For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                Subpart = dgvNESHAPSubParts(1, i).Value
                Action = dgvNESHAPSubParts(4, i).Value

                For j = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                    If dgvNESHAPSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be deleted from the Facility.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                        dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvNESHAPSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPUndeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPUndeleteAll.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNESHAPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNESHAPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvNESHAPSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearNESHAPDeletes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNESHAPDeletes.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNESHAPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvNESHAPSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvNESHAPSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewNESHAPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewNESHAPSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If chbCDS_8.Checked = False Then
                MsgBox("The NESHAP Subpart is not checked on the Tracking Log tab. " & vbCrLf & _
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation, _
                        "Application Tracking")
                Exit Sub
            End If

            Subpart = cboNESHAPSubpart.SelectedValue
            If Subpart <> "" Then
                Desc = Replace(cboNESHAPSubpart.Text, Subpart & " - ", "")
            End If

            temp2 = ""
            If dgvNESHAPSubParts.Rows.Count <> 0 Then
                For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, i).Value = Subpart Then
                        temp2 = "Ignore"
                        MsgBox("The NESHAP Subpart already exists for this application.", MsgBoxStyle.Information, _
                        "Application Tracking")
                        Exit Sub
                    End If
                Next
            End If
            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNESHAPSubParts)
                dgvRow.Cells(0).Value = txtApplicationNumber.Text
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = OracleDate
                dgvRow.Cells(4).Value = "Added"
                dgvNESHAPSubParts.Rows.Add(dgvRow)
                i = dgvNESHAPSubParts.Rows.Count - 1
                With Me.dgvNESHAPSubParts.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If

            temp2 = ""
            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                If dgvNESHAPSubpartAddEdit(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = OracleDate
                dgvRow.Cells(3).Value = "Added"
                dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvNESHAPSubpartAddEdit.Rows.Count - 1
                With Me.dgvNESHAPSubpartAddEdit.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If


            Exit Sub
            Subpart = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
            Desc = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value

            If i > 0 Then
                temp = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            Else
                dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                    .DefaultCellStyle.BackColor = Color.Tomato
                End With
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPEdit.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
            Action = dgvNESHAPSubParts(4, dgvNESHAPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                If dgvNESHAPSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be Modified by this Application.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvNESHAPSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                    If dgvNESHAPSubpartAddEdit(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, dgvNESHAPSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                    With Me.dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.LightBlue
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPUnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubpartAddEdit.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvNESHAPSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvNESHAPSubpartAddEdit(0, dgvNESHAPSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvNESHAPSubpartAddEdit(3, dgvNESHAPSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    With Me.dgvNESHAPSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    dgvNESHAPSubpartAddEdit.Rows.Remove(dgvNESHAPSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPEditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                Subpart = dgvNESHAPSubParts(1, i).Value
                Action = dgvNESHAPSubParts(4, i).Value

                For j As Integer = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be Modified by this Application.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                    If dgvNESHAPSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                        dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, i).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvNESHAPSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNESHAPUneditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNESHAPUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNESHAPSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        If dgvNESHAPSubParts(4, j).Value = "Existing" Then
                            With Me.dgvNESHAPSubParts.Rows(j)
                                .DefaultCellStyle.BackColor = Color.White
                            End With
                            TempRemove = i & "," & TempRemove
                        End If
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvNESHAPSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = Replace(TempRemove, i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAddModifiedNESHAPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddModifiedNESHAPs.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNESHAPSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    temp = dgvNESHAPSubParts(1, j).Value
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvNESHAPSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    With Me.dgvNESHAPSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    If Action = "Added" Then
                        dgvNESHAPSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvNESHAPSubpartAddEdit.Rows.Clear()
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveNESHAPSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "8' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNESHAPSubPartDelete(0, i).Value
                SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "8', " & _
                "'" & Subpart & "', '0', " & _
                "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                Action = dgvNESHAPSubpartAddEdit(3, i).Value
                Subpart = dgvNESHAPSubpartAddEdit(0, i).Value

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "8'  " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                Select Case Action
                    Case "Added"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "8', " & _
                        "'" & Subpart & "', '1', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case "Modify"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "8', " & _
                        "'" & Subpart & "', '2', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case Else
                        SQL = ""
                End Select
                If SQL <> "" Then
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Next

            LoadSSPPNESHAPSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveNESHAPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNESHAPSubpart.Click
        Try
            If chbCDS_8.Checked = False Then
                MsgBox("WARNING DATA NOT SAVED:" & vbCrLf & _
                       "On the Tracking Log tab select the air program code 8 - NESHAP. " & _
                       "If you do not check this air program code the subpart(s) cannot be saved.", _
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            End If
            SaveNESHAPSubpart()

            MsgBox("NESHAP Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "MACT Subpart"
    Sub LoadSSPPMACTSubPartInformation()
        Try
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            dgvMACTSubParts.Rows.Clear()
            dgvMACTSubParts.Columns.Clear()
            dgvMACTSubPartDelete.Rows.Clear()
            dgvMACTSubPartDelete.Columns.Clear()
            dgvMACTSubpartAddEdit.Rows.Clear()
            dgvMACTSubpartAddEdit.Columns.Clear()

            SQL = "select distinct   " & _
            "strAIRSnumber, " & _
            "'' as AppNum, " & _
            "AIRBRANCH.apbsubpartdata.strSubpart, " & _
            "strDescription, CreateDateTime " & _
            "from AIRBRANCH.APBsubpartdata, AIRBRANCH.LookUpSubPart63   " & _
            "where AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPart63.strSubpart   " & _
            "and AIRBRANCH.APBSubPartData.strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
            "and Active = '1' "


            dgvMACTSubParts.RowHeadersVisible = False
            dgvMACTSubParts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACTSubParts.AllowUserToResizeColumns = True
            dgvMACTSubParts.AllowUserToAddRows = False
            dgvMACTSubParts.AllowUserToDeleteRows = False
            dgvMACTSubParts.AllowUserToOrderColumns = True
            dgvMACTSubParts.AllowUserToResizeRows = True
            dgvMACTSubParts.ColumnHeadersHeight = "35"

            dgvMACTSubParts.Columns.Add("strApplicationNumber", "App #")
            dgvMACTSubParts.Columns("strApplicationNumber").DisplayIndex = 0
            dgvMACTSubParts.Columns("strApplicationNumber").Width = 50
            dgvMACTSubParts.Columns("strApplicationNumber").Visible = True

            dgvMACTSubParts.Columns.Add("strSubpart", "Subpart")
            dgvMACTSubParts.Columns("strSubpart").DisplayIndex = 1
            dgvMACTSubParts.Columns("strSubpart").Width = 75
            dgvMACTSubParts.Columns("strSubpart").Visible = True

            dgvMACTSubParts.Columns.Add("strDescription", "Desc.")
            dgvMACTSubParts.Columns("strDescription").DisplayIndex = 2
            dgvMACTSubParts.Columns("strDescription").Width = 200
            dgvMACTSubParts.Columns("strDescription").ReadOnly = True

            dgvMACTSubParts.Columns.Add("CreateDateTime", "Action Date")
            dgvMACTSubParts.Columns("CreateDateTime").DisplayIndex = 3
            dgvMACTSubParts.Columns("CreateDateTime").Width = 100
            dgvMACTSubParts.Columns("CreateDateTime").ReadOnly = True
            dgvMACTSubParts.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvMACTSubParts.Columns.Add("Origins", "Action")
            dgvMACTSubParts.Columns("Origins").DisplayIndex = 4
            dgvMACTSubParts.Columns("Origins").Width = 50
            dgvMACTSubParts.Columns("Origins").ReadOnly = True

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvMACTSubParts)
                If IsDBNull(dr.Item("AppNum")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("AppNum")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If
                dgvRow.Cells(4).Value = "Existing"
                dgvMACTSubParts.Rows.Add(dgvRow)
            End While
            dr.Close()

            If dgvMACTSubParts.RowCount > 0 Then
                For i = 0 To dgvMACTSubParts.RowCount - 1
                    SubPart = dgvMACTSubParts.Item(1, i).Value
                    SQL = "select " & _
                    "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strSubpart, strApplicationActivity,   " & _
                    "CreateDateTime " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPSubpartData   " & _
                    "where AIRBRANCH.SSPPSubpartData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber  " & _
                    "and strAIRSnumber = '0413" & txtAIRSNumber.Text & "'  " & _
                    "and substr(strSubpartkey, 6,1) = 'M'  " & _
                    "and strSubpart = '" & SubPart & "'  " & _
                    "and AIRBRANCH.SSPPSubpartData.strApplicationNumber  = '" & txtApplicationNumber.Text & "'  " & _
                    "order by createdatetime "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                        Else
                            dgvMACTSubParts(0, i).Value = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strApplicationActivity")) Then
                        Else
                            Select Case dr.Item("strApplicationActivity").ToString
                                Case "1"
                                    'Added' 
                                    dgvMACTSubParts(4, i).Value = "Added"
                                Case "2"
                                    'Modified' 
                                    dgvMACTSubParts(4, i).Value = "Modify"
                                Case Else
                                    'Existing
                                    dgvMACTSubParts(4, i).Value = "Existing"
                            End Select
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                        Else
                            dgvMACTSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr.Close()
                Next
            End If

            dgvMACTSubPartDelete.RowHeadersVisible = False
            dgvMACTSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACTSubPartDelete.AllowUserToResizeColumns = True
            dgvMACTSubPartDelete.AllowUserToAddRows = False
            dgvMACTSubPartDelete.AllowUserToDeleteRows = False
            dgvMACTSubPartDelete.AllowUserToOrderColumns = True
            dgvMACTSubPartDelete.AllowUserToResizeRows = True
            dgvMACTSubPartDelete.ColumnHeadersHeight = "35"

            dgvMACTSubPartDelete.Columns.Add("strSubpart", "Subpart")
            dgvMACTSubPartDelete.Columns("strSubpart").DisplayIndex = 0
            dgvMACTSubPartDelete.Columns("strSubpart").Width = 75
            dgvMACTSubPartDelete.Columns("strSubpart").Visible = True

            dgvMACTSubPartDelete.Columns.Add("strDescription", "Desc.")
            dgvMACTSubPartDelete.Columns("strDescription").DisplayIndex = 1
            dgvMACTSubPartDelete.Columns("strDescription").Width = 100
            dgvMACTSubPartDelete.Columns("strDescription").ReadOnly = True

            dgvMACTSubpartAddEdit.RowHeadersVisible = False
            dgvMACTSubpartAddEdit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACTSubpartAddEdit.AllowUserToResizeColumns = True
            dgvMACTSubpartAddEdit.AllowUserToAddRows = False
            dgvMACTSubpartAddEdit.AllowUserToDeleteRows = False
            dgvMACTSubpartAddEdit.AllowUserToOrderColumns = True
            dgvMACTSubpartAddEdit.AllowUserToResizeRows = True
            dgvMACTSubpartAddEdit.ReadOnly = True
            dgvMACTSubpartAddEdit.ColumnHeadersHeight = "35"

            dgvMACTSubpartAddEdit.Columns.Add("strSubpart", "Subpart")
            dgvMACTSubpartAddEdit.Columns("strSubpart").DisplayIndex = 0
            dgvMACTSubpartAddEdit.Columns("strSubpart").Width = 75
            dgvMACTSubpartAddEdit.Columns("strSubpart").Visible = True

            dgvMACTSubpartAddEdit.Columns.Add("strDescription", "Desc.")
            dgvMACTSubpartAddEdit.Columns("strDescription").DisplayIndex = 1
            dgvMACTSubpartAddEdit.Columns("strDescription").Width = 100
            dgvMACTSubpartAddEdit.Columns("strDescription").ReadOnly = True

            dgvMACTSubpartAddEdit.Columns.Add("CreateDateTime", "Action Date")
            dgvMACTSubpartAddEdit.Columns("CreateDateTime").DisplayIndex = 2
            dgvMACTSubpartAddEdit.Columns("CreateDateTime").Width = 100
            dgvMACTSubpartAddEdit.Columns("CreateDateTime").ReadOnly = True

            dgvMACTSubpartAddEdit.Columns.Add("Origins", "Action")
            dgvMACTSubpartAddEdit.Columns("Origins").DisplayIndex = 3
            dgvMACTSubpartAddEdit.Columns("Origins").Width = 100
            dgvMACTSubpartAddEdit.Columns("Origins").ReadOnly = True

            SQL = "select  " & _
            "strAIRSNumber, " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
            "AIRBRANCH.SSPPSubPartData.strSubpart, strDescription,  " & _
            "case when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity ='1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "else strApplicationActivity  " & _
            "end Action,  " & _
            "CreatedateTime  " & _
            "from AIRBRANCH.SSPPSubpartData, AIRBRANCH.SSPPApplicationMaster,   " & _
            "AIRBRANCH.LookUpSubPart63   " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & _
            "AIRBRANCH.SSPPSubpartData.strApplicationNumber   " & _
            "and AIRBRANCH.SSPPSubPartData.strSubPart = AIRBRANCH.LookUpSubPart63.strSubPart  " & _
            "and AIRBRANCH.SSPPSubpartData.strSubpartKey  = '" & txtApplicationNumber.Text & "M'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strSubpart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Desc = ""
                Else
                    Desc = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("Action")) Then
                    Action = ""
                Else
                    Action = dr.Item("Action")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    CreateDateTime = ""
                Else
                    CreateDateTime = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                End If

                Select Case Action
                    Case "Removed"
                        temp = ""
                        For i = 0 To dgvMACTSubParts.Rows.Count - 1
                            If SubPart = dgvMACTSubParts(1, i).Value Then
                                dgvMACTSubParts(0, i).Value = AppNum
                                dgvMACTSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                With Me.dgvMACTSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.Tomato
                                End With
                            End If
                        Next

                        If temp = "" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvMACTSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Removed"
                            dgvMACTSubParts.Rows.Add(dgvRow)
                            i = dgvMACTSubParts.Rows.Count - 1
                            With Me.dgvMACTSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.Tomato
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvMACTSubParts.Rows.Count - 1
                            If SubPart = dgvMACTSubParts(1, i).Value Then
                                dgvMACTSubParts(4, i).Value = "Added"
                                temp = "Added"
                                With Me.dgvMACTSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightGreen
                                End With
                            End If
                        Next
                        If temp <> "Added" Then
                            dgvRow = New DataGridViewRow
                            dgvRow.CreateCells(dgvMACTSubParts)
                            dgvRow.Cells(0).Value = AppNum
                            dgvRow.Cells(1).Value = SubPart
                            dgvRow.Cells(2).Value = Desc
                            dgvRow.Cells(3).Value = CreateDateTime
                            dgvRow.Cells(4).Value = "Added"
                            dgvMACTSubParts.Rows.Add(dgvRow)
                            i = dgvMACTSubParts.Rows.Count - 1
                            With Me.dgvMACTSubParts.Rows(i)
                                .DefaultCellStyle.BackColor = Color.LightGreen
                            End With
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvMACTSubpartAddEdit.Rows.Count - 1
                        With Me.dgvMACTSubpartAddEdit.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightGreen
                        End With

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvMACTSubParts.Rows.Count - 1
                            If SubPart = dgvMACTSubParts(1, i).Value Then
                                dgvMACTSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                With Me.dgvMACTSubParts.Rows(i)
                                    .DefaultCellStyle.BackColor = Color.LightBlue
                                End With
                            End If
                        Next

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                    Case Else

                End Select

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTDelete.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
            Action = dgvMACTSubParts(4, dgvMACTSubParts.CurrentRow.Index).Value

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                If dgvMACTSubpartAddEdit(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvMACTSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                    If dgvMACTSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvMACTSubPartDelete)
                        dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                        dgvMACTSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value

                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTUndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvMACTSubPartDelete.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvMACTSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvMACTSubPartDelete(0, dgvMACTSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvMACTSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
                dgvMACTSubPartDelete.Rows.Remove(dgvMACTSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTDeleteAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim j As Integer = 0

            For i = 0 To dgvMACTSubParts.Rows.Count - 1
                Subpart = dgvMACTSubParts(1, i).Value
                Action = dgvMACTSubParts(4, i).Value

                For j = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                    If dgvMACTSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be deleted from the Facility.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                    If dgvMACTSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubPartDelete)
                        dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                        dgvMACTSubPartDelete.Rows.Add(dgvRow)
                        With Me.dgvMACTSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.Tomato
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTUndeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTUndeleteAll.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                Subpart = dgvMACTSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvMACTSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvMACTSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearMACTDeletes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMACTDeletes.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                Subpart = dgvMACTSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                With Me.dgvMACTSubParts.Rows(temp2)
                    .DefaultCellStyle.BackColor = Color.White
                End With
            Next
            dgvMACTSubPartDelete.Rows.Clear()
            Exit Sub
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewMACTSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewMACTSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If chbCDS_M.Checked = False Then
                MsgBox("The MACT Subpart is not checked on the Tracking Log tab. " & vbCrLf & _
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation, _
                        "Application Tracking")
                Exit Sub
            End If

            Subpart = cboMACTSubpart.SelectedValue
            If Subpart <> "" Then
                Desc = Replace(cboMACTSubpart.Text, Subpart & " - ", "")
            End If

            temp2 = ""
            For i = 0 To dgvMACTSubParts.Rows.Count - 1
                If dgvMACTSubParts(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The MACT Subpart already exists for this application.", MsgBoxStyle.Information, _
                        "Application Tracking")
                    Exit Sub
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvMACTSubParts)
                dgvRow.Cells(0).Value = txtApplicationNumber.Text
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = OracleDate
                dgvRow.Cells(4).Value = "Added"
                dgvMACTSubParts.Rows.Add(dgvRow)
                i = dgvMACTSubParts.Rows.Count - 1
                With Me.dgvMACTSubParts.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If

            temp2 = ""
            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                If dgvMACTSubpartAddEdit(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = OracleDate
                dgvRow.Cells(3).Value = "Added"
                dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvMACTSubpartAddEdit.Rows.Count - 1
                With Me.dgvMACTSubpartAddEdit.Rows(i)
                    .DefaultCellStyle.BackColor = Color.LightGreen
                End With
            End If


            Exit Sub
            Subpart = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
            Desc = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value

            If i > 0 Then
                temp = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                    If dgvMACTSubPartDelete(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.Tomato
                    End With
                End If
            Else
                dgvRow.CreateCells(dgvMACTSubPartDelete)
                dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                dgvMACTSubPartDelete.Rows.Add(dgvRow)
                With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                    .DefaultCellStyle.BackColor = Color.Tomato
                End With
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTEdit.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubParts.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            Subpart = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
            Action = dgvMACTSubParts(4, dgvMACTSubParts.CurrentRow.Index).Value

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                If dgvMACTSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                       "The subpart must be removed from this list before it can be Modified by this Application.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            Else
                temp2 = ""
            End If

            i = dgvMACTSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                    If dgvMACTSubpartAddEdit(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(2).Value = dgvMACTSubParts(3, dgvMACTSubParts.CurrentRow.Index).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvMACTSubParts(3, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                    With Me.dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index)
                        .DefaultCellStyle.BackColor = Color.LightBlue
                    End With
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTUnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubpartAddEdit.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If

            If dgvMACTSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvMACTSubpartAddEdit(0, dgvMACTSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvMACTSubpartAddEdit(3, dgvMACTSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    With Me.dgvMACTSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    dgvMACTSubpartAddEdit.Rows.Remove(dgvMACTSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTEditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i = 0 To dgvMACTSubParts.Rows.Count - 1
                Subpart = dgvMACTSubParts(1, i).Value
                Action = dgvMACTSubParts(4, i).Value

                For j As Integer = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                    If dgvMACTSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbCrLf & _
                           "The subpart must be removed from this list before it can be Modified by this Application.", _
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Exit Sub
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                    If dgvMACTSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    If Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                        dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                        dgvRow.Cells(2).Value = dgvMACTSubParts(3, i).Value
                        dgvRow.Cells(3).Value = "Modify"
                        dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                        With Me.dgvMACTSubParts.Rows(i)
                            .DefaultCellStyle.BackColor = Color.LightBlue
                        End With
                    End If
                End If
            Next
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMACTUneditAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMACTUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Subpart = dgvMACTSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        If dgvMACTSubParts(4, j).Value = "Existing" Then
                            With Me.dgvMACTSubParts.Rows(j)
                                .DefaultCellStyle.BackColor = Color.White
                            End With
                            TempRemove = i & "," & TempRemove
                        End If
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvMACTSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = Replace(TempRemove, i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAddModifiedMACTs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddModifiedMACTs.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Subpart = dgvMACTSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    temp = dgvMACTSubParts(1, j).Value
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvMACTSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    With Me.dgvMACTSubParts.Rows(temp2)
                        .DefaultCellStyle.BackColor = Color.White
                    End With
                    If Action = "Added" Then
                        dgvMACTSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvMACTSubpartAddEdit.Rows.Clear()
            Exit Sub

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveMACTSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            SQL = "Delete AIRBRANCH.SSPPSubpartData " & _
            "where strSubpartKey = '" & txtApplicationNumber.Text & "M' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                Subpart = dgvMACTSubPartDelete(0, i).Value
                SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "M', " & _
                "'" & Subpart & "', '0', " & _
                "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Next

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Action = dgvMACTSubpartAddEdit(3, i).Value
                Subpart = dgvMACTSubpartAddEdit(0, i).Value

                SQL = "Select " & _
                "strSubpart " & _
                "from AIRBRANCH.SSPPSubpartData " & _
                "where strSubpartKey = '" & txtApplicationNumber.Text & "M'  " & _
                "and strSubpart = '" & Replace(Subpart, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                Select Case Action
                    Case "Added"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "M', " & _
                        "'" & Subpart & "', '1', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case "Modify"
                        SQL = "Insert into AIRBRANCH.SSPPSubpartData " & _
                        "values " & _
                        "('" & txtApplicationNumber.Text & "', '" & txtApplicationNumber.Text & "M', " & _
                        "'" & Subpart & "', '2', " & _
                        "'" & CurrentUser.UserID & "', (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
                        "(to_char(sysdate, 'DD-Mon-YY HH12:MI:SS'))) "
                    Case Else
                        SQL = ""
                End Select
                If SQL <> "" Then
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Next

            LoadSSPPMACTSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveMACTSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMACTSubpart.Click
        Try
            If chbCDS_M.Checked = False Then
                MsgBox("WARNING DATA NOT SAVED:" & vbCrLf & _
                       "On the Tracking Log tab select the air program code M - MACT. " & _
                       "If you do not check this air program code the subpart(s) cannot be saved.", _
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Exit Sub
            End If
            SaveMACTSubpart()
            MsgBox("MACT Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub chbCDS_0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCDS_0.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" Then
                If chbCDS_0.CheckState = CheckState.Unchecked Then
                    If dgvSIPSubParts.RowCount > 0 Then


                        For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                            Subpart = dgvSIPSubpartAddEdit(0, i).Value
                            temp2 = ""
                            Action = ""
                            For j = 0 To dgvSIPSubParts.Rows.Count - 1
                                temp = dgvSIPSubParts(1, j).Value
                                If dgvSIPSubParts(1, j).Value = Subpart Then
                                    temp2 = j
                                    Action = dgvSIPSubParts(4, j).Value
                                End If
                            Next
                            If temp2 <> "" Then
                                With Me.dgvSIPSubParts.Rows(temp2)
                                    .DefaultCellStyle.BackColor = Color.White
                                End With
                                If Action = "Added" Then
                                    dgvSIPSubParts.Rows.RemoveAt(temp2)
                                End If
                            End If
                        Next
                        dgvSIPSubpartAddEdit.Rows.Clear()

                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            Subpart = dgvSIPSubParts(1, i).Value
                            Action = dgvSIPSubParts(4, i).Value

                            For j = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                                If dgvSIPSubpartAddEdit(0, j).Value = Subpart Then
                                    temp2 = "Message"
                                End If
                            Next
                            If temp2 = "Message" Then
                                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                                Exit Sub
                            Else
                                temp2 = ""
                            End If

                            temp2 = ""
                            For j = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                                If dgvSIPSubPartDelete(0, j).Value = Subpart Then
                                    temp2 = "Ignore"
                                End If
                            Next
                            If temp2 <> "Ignore" Then
                                If Action <> "Added" Then
                                    dgvRow = New DataGridViewRow
                                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                                    With Me.dgvSIPSubParts.Rows(i)
                                        .DefaultCellStyle.BackColor = Color.Tomato
                                    End With
                                End If
                            End If
                        Next

                        SaveSIPSubpart()


                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbCDS_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCDS_8.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" Then
                If chbCDS_8.CheckState = CheckState.Unchecked Then
                    If dgvNESHAPSubParts.RowCount > 0 Then
                         

                        For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                            Subpart = dgvNESHAPSubpartAddEdit(0, i).Value
                            temp2 = ""
                            Action = ""
                            For j = 0 To dgvNESHAPSubParts.Rows.Count - 1
                                temp = dgvNESHAPSubParts(1, j).Value
                                If dgvNESHAPSubParts(1, j).Value = Subpart Then
                                    temp2 = j
                                    Action = dgvNESHAPSubParts(4, j).Value
                                End If
                            Next
                            If temp2 <> "" Then
                                With Me.dgvNESHAPSubParts.Rows(temp2)
                                    .DefaultCellStyle.BackColor = Color.White
                                End With
                                If Action = "Added" Then
                                    dgvNESHAPSubParts.Rows.RemoveAt(temp2)
                                End If
                            End If
                        Next
                        dgvNESHAPSubpartAddEdit.Rows.Clear()

                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            Subpart = dgvNESHAPSubParts(1, i).Value
                            Action = dgvNESHAPSubParts(4, i).Value

                            For j = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                                If dgvNESHAPSubpartAddEdit(0, j).Value = Subpart Then
                                    temp2 = "Message"
                                End If
                            Next
                            If temp2 = "Message" Then
                                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                                Exit Sub
                            Else
                                temp2 = ""
                            End If

                            temp2 = ""
                            For j = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                                If dgvNESHAPSubPartDelete(0, j).Value = Subpart Then
                                    temp2 = "Ignore"
                                End If
                            Next
                            If temp2 <> "Ignore" Then
                                If Action <> "Added" Then
                                    dgvRow = New DataGridViewRow
                                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                                    With Me.dgvNESHAPSubParts.Rows(i)
                                        .DefaultCellStyle.BackColor = Color.Tomato
                                    End With
                                End If
                            End If
                        Next

                        SaveNESHAPSubpart()
                         

                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbCDS_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCDS_9.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" Then
                If chbCDS_9.CheckState = CheckState.Unchecked Then
                    If dgvNSPSSubParts.RowCount > 0 Then
                         

                        For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                            Subpart = dgvNSPSSubpartAddEdit(0, i).Value
                            temp2 = ""
                            Action = ""
                            For j = 0 To dgvNSPSSubParts.Rows.Count - 1
                                temp = dgvNSPSSubParts(1, j).Value
                                If dgvNSPSSubParts(1, j).Value = Subpart Then
                                    temp2 = j
                                    Action = dgvNSPSSubParts(4, j).Value
                                End If
                            Next
                            If temp2 <> "" Then
                                With Me.dgvNSPSSubParts.Rows(temp2)
                                    .DefaultCellStyle.BackColor = Color.White
                                End With
                                If Action = "Added" Then
                                    dgvNSPSSubParts.Rows.RemoveAt(temp2)
                                End If
                            End If
                        Next
                        dgvNSPSSubpartAddEdit.Rows.Clear()

                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            Subpart = dgvNSPSSubParts(1, i).Value
                            Action = dgvNSPSSubParts(4, i).Value

                            For j = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                                If dgvNSPSSubpartAddEdit(0, j).Value = Subpart Then
                                    temp2 = "Message"
                                End If
                            Next
                            If temp2 = "Message" Then
                                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                                Exit Sub
                            Else
                                temp2 = ""
                            End If

                            temp2 = ""
                            For j = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                                If dgvNSPSSubPartDelete(0, j).Value = Subpart Then
                                    temp2 = "Ignore"
                                End If
                            Next
                            If temp2 <> "Ignore" Then
                                If Action <> "Added" Then
                                    dgvRow = New DataGridViewRow
                                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                                    With Me.dgvNSPSSubParts.Rows(i)
                                        .DefaultCellStyle.BackColor = Color.Tomato
                                    End With
                                End If
                            End If
                        Next

                        SaveNSPSSubpart()
                         

                    End If
                End If
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbCDS_M_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCDS_M.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" Then
                If chbCDS_M.CheckState = CheckState.Unchecked Then
                    If dgvMACTSubParts.RowCount > 0 Then
                         

                        For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                            Subpart = dgvMACTSubpartAddEdit(0, i).Value
                            temp2 = ""
                            Action = ""
                            For j = 0 To dgvMACTSubParts.Rows.Count - 1
                                temp = dgvMACTSubParts(1, j).Value
                                If dgvMACTSubParts(1, j).Value = Subpart Then
                                    temp2 = j
                                    Action = dgvMACTSubParts(4, j).Value
                                End If
                            Next
                            If temp2 <> "" Then
                                With Me.dgvMACTSubParts.Rows(temp2)
                                    .DefaultCellStyle.BackColor = Color.White
                                End With
                                If Action = "Added" Then
                                    dgvMACTSubParts.Rows.RemoveAt(temp2)
                                End If
                            End If
                        Next
                        dgvMACTSubpartAddEdit.Rows.Clear()

                        For i = 0 To dgvMACTSubParts.Rows.Count - 1
                            Subpart = dgvMACTSubParts(1, i).Value
                            Action = dgvMACTSubParts(4, i).Value

                            For j = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                                If dgvMACTSubpartAddEdit(0, j).Value = Subpart Then
                                    temp2 = "Message"
                                End If
                            Next
                            If temp2 = "Message" Then
                                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbCrLf & _
                                       "The subpart must be removed from this list before it can be deleted from the Facility.", _
                                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                                Exit Sub
                            Else
                                temp2 = ""
                            End If

                            temp2 = ""
                            For j = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                                If dgvMACTSubPartDelete(0, j).Value = Subpart Then
                                    temp2 = "Ignore"
                                End If
                            Next
                            If temp2 <> "Ignore" Then
                                If Action <> "Added" Then
                                    dgvRow = New DataGridViewRow
                                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                                    With Me.dgvMACTSubParts.Rows(i)
                                        .DefaultCellStyle.BackColor = Color.Tomato
                                    End With
                                End If
                            End If
                        Next

                        SaveMACTSubpart()
                         

                    End If
                End If
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateFeeContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoToFeeContact.Click
        Try
            FeeContact = Nothing
            If FeeContact Is Nothing Then FeeContact = New SSPP_FeeContact
            FeeContact.Show()
            FeeContact.txtAIRSNumber.Text = txtAIRSNumber.Text
            FeeContact.txtApplicationNumber.Text = txtApplicationNumber.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        PreSaveCheckThenSave()
    End Sub
End Class