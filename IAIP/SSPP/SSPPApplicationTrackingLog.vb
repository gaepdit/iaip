Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports EpdIt
Imports Iaip.Apb
Imports Iaip.Apb.Facilities.Facility
Imports Iaip.Apb.Facilities.FacilityEnums
Imports Iaip.Apb.Facilities.FacilityHeaderData
Imports Iaip.Apb.Finance
Imports Iaip.Apb.Sspp
Imports Iaip.Apb.Sspp.Permit
Imports Iaip.DAL.FacilityHeaderDataData
Imports Iaip.DAL.Finance
Imports Iaip.DAL.Sspp
Imports Iaip.UrlHelpers

Public Class SSPPApplicationTrackingLog

#Region " Properties and fields "

    Private Property AppNumber As Integer

    Private Property AirsId As ApbFacilityId
    Private Property NewApplication As Boolean

    Private Property LastModificationDateAsLoaded As DateTimeOffset = Nothing
    Private Property FacilityApplicationHistoryLoaded As Boolean

    Private MasterApp As String
    Private FormStatus As String
    Private UpdatingValues As Boolean
    Private FeeChangesAllowed As Boolean = True

    Private _applicationFeeAmount As Decimal = 0
    Private _expeditedFeeAmount As Decimal = 0
    Private _totalFeeAmount As Decimal = 0

    Private Property ApplicationFeeAmount As Decimal
        Get
            Return _applicationFeeAmount
        End Get
        Set(value As Decimal)
            If _applicationFeeAmount <> value Then
                _applicationFeeAmount = value
                txtAppFeeAmount.Amount = value

                TotalFeeAmount = value + _expeditedFeeAmount
            End If
        End Set
    End Property

    Private Property ExpeditedFeeAmount As Decimal
        Get
            Return _expeditedFeeAmount
        End Get
        Set(value As Decimal)
            If _expeditedFeeAmount <> value Then
                _expeditedFeeAmount = value
                txtExpFeeAmount.Amount = value

                TotalFeeAmount = value + _applicationFeeAmount
            End If
        End Set
    End Property

    Private Property TotalFeeAmount As Decimal
        Get
            Return _totalFeeAmount
        End Get
        Set(value As Decimal)
            If _totalFeeAmount <> value Then
                _totalFeeAmount = value
                txtFeeTotal.Text = String.Format(Globalization.CultureInfo.CurrentCulture, "{0:C}", value)

                If value = 0 Then
                    txtFeeTotal.Visible = False
                    lblTotalFee.Visible = False
                    chbFeeDataFinalized.Visible = False
                Else
                    txtFeeTotal.Visible = True
                    lblTotalFee.Visible = True
                    chbFeeDataFinalized.Visible = FeeChangesAllowed
                End If
            End If
        End Set
    End Property

#End Region

    Protected Overrides Sub OnLoad(e As EventArgs)
        FormStatus = "Loading"
        LoadDefaultDates()
        LoadComboBoxes()
        LoadPermissions()
        LoadSubPartData()

        ' Parse parameters & load data
        ParseParameters()

        If AppNumber > 0 Then
            If ApplicationExists(AppNumber) Then
                AddBreadcrumb("SSPP Application Tracking Log: load application", New Dictionary(Of String, Object) From {{"Application #", AppNumber}}, Me)
                LoadApplication()
            Else
                MessageBox.Show("Application #" & AppNumber.ToString & " does not exist.")
                Close()
            End If
        Else
            SetUpForNewApplication()
        End If

        TPTrackingLog.Focus()
        FormStatus = ""

        MyBase.OnLoad(e)
    End Sub

#Region "Page Load Functions"

    Private Sub ParseParameters()
        If Parameters IsNot Nothing AndAlso Parameters.ContainsKey(FormParameter.AppNumber) Then
            AppNumber = CInt(Parameters(FormParameter.AppNumber))
        End If
    End Sub

    Private Sub SetUpForNewApplication()
        AddBreadcrumb("SSPP Application Tracking Log: new application", Me)
        If Not CurrentUser.HasPermission(UserCan.CreatePermitApp) Then
            MessageBox.Show("You do not have permission to create a new application. Please contact your manager.", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        AppNumber = 0
        NewApplication = True
        txtNewApplicationNumber.Text = DB.GetInteger("Select next value for SSPPAPPLICATIONKEY").ToString
        txtNewApplicationNumber.Visible = True
        btnFetchNewAppNumber.Visible = True

        TCApplicationTrackingLog.TabPages.Remove(TPReviews)
        TCApplicationTrackingLog.TabPages.Remove(TPInformationRequests)
        TCApplicationTrackingLog.TabPages.Remove(TPWebPublisher)
        TCApplicationTrackingLog.TabPages.Remove(TPDocuments)
        TCApplicationTrackingLog.TabPages.Remove(TPSubPartEditor)
        TCApplicationTrackingLog.TabPages.Remove(TPFees)

        chbClosedOut.Enabled = False

        btnEmailAcknowledgmentLetter.Enabled = False
    End Sub

    Private Sub LoadDefaultDates()
        'Other info
        chbFederallyOwned.Checked = False
        chbNspsFeeExempt.Checked = False

        'Application Tracking Tab
        DTPDateSent.Value = Today
        DTPDateReceived.Value = Today
        DTPDateAssigned.Value = Today
        DTPDateAssigned.Checked = False
        DTPDateReassigned.Value = Today
        DTPDateReassigned.Checked = False
        DTPDateAcknowledge.Value = Today
        DTPDateAcknowledge.Checked = False
        DTPDatePAExpires.Value = Today
        DTPDatePAExpires.Checked = False
        DTPDateToUC.Value = Today
        DTPDateToUC.Checked = False
        DTPDateToPM.Value = Today
        DTPDateToPM.Checked = False
        DTPDraftIssued.Value = Today
        DTPDraftIssued.Checked = False
        DTPDatePNExpires.Value = Today
        DTPDatePNExpires.Checked = False
        DTPEPAWaived.Value = Today
        DTPEPAWaived.Checked = False
        DTPEPAEnds.Value = Today
        DTPEPAEnds.Checked = False
        DTPDateToBC.Value = Today
        DTPDateToBC.Checked = False
        DTPDateToDO.Value = Today
        DTPDateToDO.Checked = False
        DTPFinalAction.Value = Today
        DTPFinalAction.Checked = False
        DTPDeadline.Value = Today
        DTPDeadline.Checked = False

        'ISMP and SSCP Reviews Tab
        DTPReviewSubmitted.Value = Today
        DTPReviewSubmitted.Checked = False
        DTPISMPReview.Value = Today
        DTPISMPReview.Checked = False
        DTPSSCPReview.Value = Today
        DTPSSCPReview.Checked = False

        'Information Requested Tab
        DTPInformationRequested.Value = Today
        DTPInformationRequested.Checked = False
        DTPInformationReceived.Value = Today
        DTPInformationReceived.Checked = False

        'Web Publisher Tab
        DTPNotifiedAppReceived.Value = Today
        DTPNotifiedAppReceived.Checked = False
        DTPDraftOnWeb.Value = Today
        DTPDraftOnWeb.Checked = False
        DTPEPAStatesNotified.Value = Today
        DTPEPAStatesNotified.Checked = False
        DTPFinalOnWeb.Value = Today
        DTPFinalOnWeb.Checked = False
        DTPEPANotifiedPermitOnWeb.Value = Today
        DTPEPANotifiedPermitOnWeb.Checked = False
        DTPEffectiveDateofPermit.Value = Today
        DTPEffectiveDateofPermit.Checked = False
        DTPExperationDate.Value = Today
        DTPExperationDate.Checked = False
        DTPPNExpires.Value = Today
        DTPPNExpires.Checked = False
    End Sub

    Private Sub LoadComboBoxes()
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

            cboPublicAdvisory.Items.Add("Not Decided")
            cboPublicAdvisory.Items.Add("PA Needed")
            cboPublicAdvisory.Items.Add("PA Not Needed")

            Dim query As String = "SELECT 'N/A' AS EngineerName, 0 AS NUMUSERID " &
                " " &
                "UNION " &
                "SELECT concat(u.STRLASTNAME , ', ' , u.STRFIRSTNAME) AS EngineerName " &
                "  , u.NUMUSERID " &
                "FROM EPDUSERPROFILES u " &
                "WHERE u.NUMPROGRAM = 5 " &
                "UNION " &
                "SELECT concat(u.STRLASTNAME , ', ' , u.STRFIRSTNAME) AS " &
                "  EngineerName, u.NUMUSERID " &
                "FROM EPDUSERPROFILES u " &
                "INNER JOIN SSPPAPPLICATIONMASTER a ON " &
                "  a.STRSTAFFRESPONSIBLE = u.NUMUSERID " &
                "WHERE u.NUMPROGRAM <> 5 AND u.NUMUSERID <> 0 " &
                "ORDER BY EngineerName"
            Dim dtEngineerList As DataTable = DB.GetDataTable(query)
            With cboEngineer
                .DataSource = dtEngineerList
                .DisplayMember = "EngineerName"
                .ValueMember = "NUMUSERID"
                .SelectedValue = 0
            End With

            query = "SELECT 'N/A' AS EngineerName, 0 AS NUMUSERID " &
                "UNION " &
                "SELECT concat(STRLASTNAME , ', ' ,STRFIRSTNAME) AS EngineerName, " &
                "  NUMUSERID " &
                "FROM EPDUSERPROFILES " &
                "WHERE NUMPROGRAM = '4' " &
                "ORDER BY EngineerName"
            Dim dtSSCPList As DataTable = DB.GetDataTable(query)
            With cboSSCPStaff
                .DataSource = dtSSCPList
                .DisplayMember = "EngineerName"
                .ValueMember = "NUMUSERID"
                .SelectedValue = 0
            End With

            query = "SELECT 'N/A' AS EngineerName, 0 AS NUMUSERID " &
                "UNION " &
                "SELECT concat(STRLASTNAME , ', ' ,STRFIRSTNAME) AS EngineerName, " &
                "  NUMUSERID " &
                "FROM EPDUSERPROFILES " &
                "WHERE NUMPROGRAM = '3' " &
                "ORDER BY EngineerName"
            Dim dtISMPList As DataTable = DB.GetDataTable(query)
            With cboISMPStaff
                .DataSource = dtISMPList
                .DisplayMember = "EngineerName"
                .ValueMember = "numUserID"
                .SelectedValue = 0
            End With

            query = "SELECT STRCOUNTYCODE, STRCOUNTYNAME " &
                "FROM LOOKUPCOUNTYINFORMATION " &
                "UNION " &
                "SELECT '000', ' N/A' ORDER BY STRCOUNTYNAME"
            Dim dtCountyList As DataTable = DB.GetDataTable(query)
            With cboCounty
                .DataSource = dtCountyList
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With

            With cboApplicationType
                .DataSource = GetApplicationTypes()
                .DisplayMember = "Application Type"
                .ValueMember = "Application Type Code"
                .SelectedValue = 0
            End With

            query = "SELECT STRPERMITTYPECODE, STRPERMITTYPEDESCRIPTION " &
                "FROM LOOKUPPERMITTYPES " &
                "WHERE STRTYPEUSED <> 'False' OR STRTYPEUSED IS NULL " &
                "UNION " &
                "SELECT '', ' ' ORDER BY STRPERMITTYPEDESCRIPTION"
            With cboPermitAction
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "strPermitTypeDescription"
                .ValueMember = "strPermitTypeCode"
                .SelectedIndex = 0
            End With

            query = "SELECT CITY FROM VW_CITIES ORDER BY CITY"
            Dim dtCity As DataTable = DB.GetDataTable(query)
            With cboFacilityCity
                .DataSource = dtCity
                .DisplayMember = "City"
                .ValueMember = "City"
                .SelectedIndex = -1
            End With

            query = "SELECT STRUNITDESC, NUMUNITCODE " &
                "FROM LOOKUPEPDUNITS " &
                "WHERE NUMPROGRAMCODE = 5 and Active = 1 " &
                "UNION " &
                "SELECT ' ', 0 ORDER BY STRUNITDESC"
            Dim dtSSPPUnit As DataTable = DB.GetDataTable(query)
            With cboApplicationUnit
                .DataSource = dtSSPPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

            query = "SELECT STRUNITDESC, NUMUNITCODE " &
                "FROM LOOKUPEPDUNITS " &
                "WHERE NUMPROGRAMCODE = 4 and Active = 1 " &
                "UNION " &
                "SELECT 'No Review Needed', 0 ORDER BY STRUNITDESC"
            Dim dtSSCPUnit As DataTable = DB.GetDataTable(query)
            With cboSSCPUnits
                .DataSource = dtSSCPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

            query = "SELECT STRUNITDESC, NUMUNITCODE " &
                "FROM LOOKUPEPDUNITS " &
                "WHERE NUMPROGRAMCODE = 3 and Active = 1 " &
                "UNION " &
                "SELECT 'No Review Needed', 0 ORDER BY STRUNITDESC"
            Dim dtISMPUnit As DataTable = DB.GetDataTable(query)
            With cboISMPUnits
                .DataSource = dtISMPUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedValue = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPermissions()
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

            If Not (AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                 AccountFormAccess(67, 2) = "1" OrElse
                (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0")) Then

                TCApplicationTrackingLog.TabPages.Remove(TPReviews)

            End If

            If Not (AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0")) Then

                TCApplicationTrackingLog.TabPages.Remove(TPApplicationHistory)

            End If

            If Not (AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                 (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0")) Then

                TCApplicationTrackingLog.TabPages.Remove(TPInformationRequests)

            End If

            If Not (AccountFormAccess(129, 3) = "1" OrElse
                  (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                  (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                   AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then

                TCApplicationTrackingLog.TabPages.Remove(TPWebPublisher)

            End If

            If Not (AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") OrElse
               AccountFormAccess(67, 2) = "1" OrElse
               (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0")) Then

                TCApplicationTrackingLog.TabPages.Remove(TPOtherInfo)

            End If

            If Not (AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") OrElse
                 AccountFormAccess(67, 2) = "1" OrElse
                (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0")) Then

                btnGetCurrentPermittingContact.Visible = False

            End If

            'btnAddApplicationToList 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddApplicationToList.Enabled = True
            Else
                btnAddApplicationToList.Enabled = False
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddApplicationToList.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddApplicationToList.BackColor = Color.Yellow
            End If

            'btnAddNewMactSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddNewMACTSubpart.Enabled = True
            End If

            'btnAddNewNESHAPSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddNewNESHAPSubpart.Enabled = True
            End If

            'btnAddNewNSPSSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddNewNSPSSubpart.Enabled = True
            End If

            'btnAddNewSIPSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnAddNewSIPSubpart.Enabled = True
            End If

            'btnClearAddModifiedMACTs 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedMACTs.Enabled = True
            End If

            'btnClearAddModifiedNESHAPs 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedNESHAPs.Enabled = True
            End If

            'btnClearAddModifiedNSPSs 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedNSPSs.Enabled = True
            End If

            'btnClearAddModifiedSIPs 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearAddModifiedSIPs.Enabled = True
            End If

            'btnClearMACTDeletes 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearMACTDeletes.Enabled = True
            End If

            'btnClearNESHAPDeletes 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearNESHAPDeletes.Enabled = True
            End If

            'btnClearNSPSDeletes 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearNSPSDeletes.Enabled = True
            End If

            'btnClearSIPDeletes 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearSIPDeletes.Enabled = True
            End If

            'btnMACTDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTDelete.Enabled = True
            End If

            'btnMACTDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTDeleteAll.Enabled = True
            End If

            'btnMACTEdit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTEdit.Enabled = True
            End If

            'btnMACTEditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTEditAll.Enabled = True
            End If

            'btnMACTunDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTUndelete.Enabled = True
            End If

            'btnMACTunDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTUndeleteAll.Enabled = True
            End If

            'btnMACTUnedit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTUnedit.Enabled = True
            End If

            'btnMACTUneditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnMACTUneditAll.Enabled = True
            End If

            'btnNESHAPDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPDelete.Enabled = True
            End If

            'btnNESHAPDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPDeleteAll.Enabled = True
            End If

            'btnNESHAPEdit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPEdit.Enabled = True
            End If

            'btnNESHAPEditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPEditAll.Enabled = True
            End If

            'btnNESHAPunDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUndelete.Enabled = True
            End If

            'btnNESHAPunDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUndeleteAll.Enabled = True
            End If

            'btnNESHAPUnedit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUnedit.Enabled = True
            End If

            'btnNESHAPUneditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNESHAPUneditAll.Enabled = True
            End If

            'btnNSPSDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSDelete.Enabled = True
            End If

            'btnNSPSDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSDeleteAll.Enabled = True
            End If

            'btnNSPSEdit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSEdit.Enabled = True
            End If

            'btnNSPSEditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSEditAll.Enabled = True
            End If

            'btnNSPSunDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSUndelete.Enabled = True
            End If

            'btnNSPSunDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSUndeleteAll.Enabled = True
            End If

            'btnNSPSUnedit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSUnedit.Enabled = True
            End If

            'btnNSPSUneditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnNSPSUneditAll.Enabled = True
            End If

            'btnSIPDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPDelete.Enabled = True
            End If

            'btnSIPDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPDeleteAll.Enabled = True
            End If

            'btnSIPEdit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPEdit.Enabled = True
            End If

            'btnSIPEditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPEditAll.Enabled = True
            End If

            'btnSIPunDelete 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPUndelete.Enabled = True
            End If

            'btnSIPunDeleteAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPUndeleteAll.Enabled = True
            End If

            'btnSIPUnedit 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPUnedit.Enabled = True
            End If

            'btnSIPUneditAll 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSIPUneditAll.Enabled = True
            End If

            'btnSaveMACTSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveMACTSubpart.Enabled = True
            End If

            'btnSaveNESHAPSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveNESHAPSubpart.Enabled = True
            End If

            'btnSaveNSPSSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveNSPSSubpart.Enabled = True
            End If

            'btnSaveSIPSubpart 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveSIPSubpart.Enabled = True
            End If

            'btnClearInformationRequest
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearInformationRequest.BackColor = Color.Yellow
            End If

            'btnClearLinks
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnClearLinks.BackColor = Color.Yellow
            End If

            'btnDeleteInformationRequest
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnDeleteInformationRequest.BackColor = Color.Yellow
            End If

            'btnLinkApplications
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnLinkApplications.BackColor = Color.Yellow
            End If

            'btnSaveInformationRequest
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveInformationRequest.BackColor = Color.Yellow
            End If

            'BtnRefreshAIRSNo
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                btnRefreshAIRSNo.Visible = True
            Else
                btnRefreshAIRSNo.Visible = False
            End If

            'btnSaveSIPSubpart
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveSIPSubpart.Visible = True
            Else
                btnSaveSIPSubpart.Visible = False
            End If

            'btnSaveMACTSubpart
            If AccountFormAccess(129, 3) = "1" OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveMACTSubpart.Visible = True
            Else
                btnSaveMACTSubpart.Visible = False
            End If

            'btnSaveNESHAPSubpart
            If AccountFormAccess(129, 3) = "1" OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveNESHAPSubpart.Visible = True
            Else
                btnSaveNESHAPSubpart.Visible = False
            End If

            'btnSaveNSPSSubpart
            If AccountFormAccess(129, 3) = "1" OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                btnSaveNSPSSubpart.Visible = True
            Else
                btnSaveNSPSSubpart.Visible = False
            End If

            'btnSaveWebPublisher
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                btnSaveWebPublisher.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                btnSaveWebPublisher.BackColor = Color.PeachPuff
            End If

            'btnGetCurrentPermittingContact
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                btnGetCurrentPermittingContact.Visible = True
            Else
                btnGetCurrentPermittingContact.Visible = False
            End If

            'cboApplicationType
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.BackColor = Color.LightGreen
                lblApplicationType.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                cboApplicationType.BackColor = Color.PeachPuff
                lblApplicationType.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboApplicationType.BackColor = Color.Yellow
                lblApplicationType.BackColor = Color.Yellow
            End If

            'cboApplicationUnit
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                cboApplicationUnit.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboApplicationUnit.BackColor = Color.LightBlue
                lblApplicationUnit.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                cboApplicationUnit.BackColor = Color.PeachPuff
                lblApplicationUnit.BackColor = Color.PeachPuff
            End If

            'cboClassification
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboClassification.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboClassification.BackColor = Color.LightGreen
                lblClassification.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboClassification.BackColor = Color.Yellow
                lblClassification.BackColor = Color.Yellow
            End If

            'cboCounty
            If AccountFormAccess(129, 3) = "1" Then
                cboCounty.Enabled = True
            End If

            'cboEngineer
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboEngineer.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboEngineer.BackColor = Color.LightBlue
                lblEngineer.BackColor = Color.LightBlue
            End If

            'cboFacilityCity
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                cboFacilityCity.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboFacilityCity.BackColor = Color.Yellow
            End If

            'cboISMPStaff
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                cboISMPStaff.BackColor = Color.Yellow
                lblISMPStaff.BackColor = Color.Yellow
            End If

            'cboOperationalStatus
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.BackColor = Color.LightGreen
                lblOperationalStatus.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                cboOperationalStatus.BackColor = Color.PeachPuff
                lblOperationalStatus.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboOperationalStatus.BackColor = Color.Yellow
                lblOperationalStatus.BackColor = Color.Yellow
            End If

            'cboPermitAction
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                  (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.BackColor = Color.LightGreen
                lblPermitAction.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPermitAction.BackColor = Color.Yellow
                lblPermitAction.BackColor = Color.Yellow
            End If

            'cboPublicAdvisory
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") Then
                cboPublicAdvisory.BackColor = Color.LightBlue
                lblPublicAdvisory.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.BackColor = Color.LightGreen
                lblPublicAdvisory.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                cboPublicAdvisory.BackColor = Color.Yellow
                lblPublicAdvisory.BackColor = Color.Yellow
            End If

            'cboSSCPStaff 
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                AccountFormAccess(67, 2) = "1" Then
                cboSSCPStaff.BackColor = Color.Yellow
                lblSSCPStaff.BackColor = Color.Yellow
            End If

            'chbNAANSR
            If AccountFormAccess(129, 3) = "1" OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbNAANSR.Enabled = True
            End If

            'chb112
            If AccountFormAccess(129, 3) = "1" OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chb112g.Enabled = True
            End If

            'chbCDS_0
            If AccountFormAccess(129, 3) = "1" Then
                chbCDS_0.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_0.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_0.BackColor = Color.Yellow
            End If

            'chbCDS_6
            If AccountFormAccess(129, 3) = "1" OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
             (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_6.BackColor = Color.Yellow
            End If

            'chbCDS_7
            If AccountFormAccess(129, 3) = "1" OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
           (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_7.BackColor = Color.Yellow
            End If

            'chbCDS_8
            If AccountFormAccess(129, 3) = "1" OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
           (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_8.BackColor = Color.Yellow
            End If

            'chbCDS_9
            If AccountFormAccess(129, 3) = "1" OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
           (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_9.BackColor = Color.Yellow
            End If

            'chbCDS_A
            If AccountFormAccess(129, 3) = "1" OrElse
          (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
          (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
          (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
          (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_A.BackColor = Color.Yellow
            End If

            'chbCDS_M
            If AccountFormAccess(129, 3) = "1" OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
           (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_M.BackColor = Color.Yellow
            End If

            'chbCDS_RMP
            If AccountFormAccess(129, 3) = "1" OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
           (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_RMP.BackColor = Color.Yellow
            End If

            'chbCDS_V
            If AccountFormAccess(129, 3) = "1" OrElse
          (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
          (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
          (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
          (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbCDS_V.BackColor = Color.Yellow
            End If

            'chbClosedOut 
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                chbClosedOut.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbClosedOut.BackColor = Color.LightBlue
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                chbClosedOut.BackColor = Color.PeachPuff
            End If

            'chbHAPsMajor
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbHAPsMajor.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbHAPsMajor.BackColor = Color.LightBlue
            End If

            'chbNSRMajor
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbNSRMajor.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbNSRMajor.BackColor = Color.LightBlue
            End If

            'chbPal
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPal.Enabled = True
            End If

            'chbExpeditedReview
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbExpFee.Enabled = True
            End If

            'chbConfidential
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                CurrentUser.HasRole(29) Then
                chbConfidential.Enabled = True
            End If

            'chbPAReady
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPAReady.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPAReady.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPAReady.BackColor = Color.Yellow
            End If

            'chbPNReady
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1" OrElse
                AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing OrElse
                AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0" Then
                chbPNReady.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPNReady.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPNReady.BackColor = Color.Yellow
            End If
            If (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                chbPNReady.Enabled = True
            End If

            'chbPSD
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbPSD.Enabled = True
            End If

            'chbRulett
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbRulett.Enabled = True
            End If

            'chbRuleyy
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbRuleyy.Enabled = True
            End If

            'DTPDateAcknowledge 
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDateAcknowledge.Enabled = True
            End If

            'DTPDateAssigned
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDateAssigned.Enabled = True
            End If

            'DTPDatePAExpires
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPDatePAExpires.Enabled = True
            End If

            'DTPDatePNExpires
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPDatePNExpires.Enabled = True
            End If

            'DTPDateReassigned
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDateReassigned.Enabled = True
            End If

            'DTPDateReceived
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                DTPDateReceived.Enabled = True
            End If

            'DTPDateSent
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                DTPDateSent.Enabled = True
            End If

            'DTPDateToBC
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPDateToBC.Enabled = True
            End If

            'DTPDateToDO
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPDateToDO.Enabled = True
            End If

            'DTPDateToPM
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDateToPM.Enabled = True
            End If

            'DTPDateToUC
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDateToUC.Enabled = True
            End If

            'DTPDeadline
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPDeadline.Enabled = True
            End If

            'DTPDraftIssued
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPDraftIssued.Enabled = True
            End If

            'DTPDraftOnWeb
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPDraftOnWeb.Enabled = True
            End If

            'DTPEffectiveDateofPermit
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPEffectiveDateofPermit.Enabled = True
            End If

            'DTPEPAEnds
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPEPAEnds.Enabled = True
            End If

            'DTPEPANotifiedPermitOnWeb
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPEPANotifiedPermitOnWeb.Enabled = True
            End If

            'DTPEPAStatesNotified
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPEPAStatesNotified.Enabled = True
            End If

            'DTPEPAWaived
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPEPAWaived.Enabled = True
            End If

            'DTPExperationDate
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPExperationDate.Enabled = True
            End If

            'DTPFinalAction
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                DTPFinalAction.Enabled = True
            End If

            'DTPFinalOnWeb
            If AccountFormAccess(129, 3) = "1" OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPFinalOnWeb.Enabled = True
            End If

            'DTPInformationReceived
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPInformationReceived.Enabled = True
            End If

            'DTPInformationRequested
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPInformationRequested.Enabled = True
            End If

            'DTPNotifiedAppReceived
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPNotifiedAppReceived.Enabled = True
            End If

            'DTPPNExpires
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                DTPPNExpires.Enabled = True
            End If

            'DTPReviewSubmitted
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                DTPReviewSubmitted.Enabled = True
            End If

            'DTPISMPReview
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               AccountFormAccess(67, 2) = "1" Then
                DTPISMPReview.Enabled = True
            End If

            'DTPSSCPReview
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                DTPSSCPReview.Enabled = True
            End If

            'lbEPAStatesNotified
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                  (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lbEPAStatesNotified.BackColor = Color.PeachPuff
            End If

            'lblDateAcknowledge
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateAcknowledge.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateAcknowledge.BackColor = Color.Yellow
            End If

            'lblDateAssigned
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateAssigned.BackColor = Color.LightBlue
            End If

            'lblDated
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblDated.BackColor = Color.PeachPuff
            End If

            'lblDatePAExpires
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblDatePAExpires.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblDatePAExpires.BackColor = Color.Yellow
            End If

            'lblDatePNExpires
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblDatePNExpires.BackColor = Color.PeachPuff
            End If

            'lblDateReassigned
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateReassigned.BackColor = Color.LightBlue
            End If

            'lblDatetoBC
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblDatetoBC.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblDatetoBC.BackColor = Color.Yellow
            End If

            'lblDateToDO
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblDateToDO.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblDateToDO.BackColor = Color.Yellow
            End If

            'lblDateToPM
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateToPM.BackColor = Color.LightBlue
            End If

            'lblDateToUC
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateToUC.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
            (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDateToUC.BackColor = Color.Yellow
            End If

            'lblDeadline
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblDeadline.BackColor = Color.LightBlue
            End If

            'lblDraftIssued
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblDraftIssued.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblDraftIssued.BackColor = Color.Yellow
            End If

            'lblDraftOnWeb
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblDraftOnWeb.BackColor = Color.PeachPuff
            End If

            'lblEffectiveDateofPermit
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblEffectiveDateofPermit.BackColor = Color.PeachPuff
            End If

            'lblEPAEnds
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblEPAEnds.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblEPAEnds.BackColor = Color.Yellow
            End If

            'lblEPANotifiedFinalOnWeb
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblEPANotifiedFinalOnWeb.BackColor = Color.PeachPuff
            End If

            'lblEPAWaived
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblEPAWaived.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblEPAWaived.BackColor = Color.Yellow
            End If

            'lblExperationDate
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblExperationDate.BackColor = Color.PeachPuff
            End If

            'lblFinalAction
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
              (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblFinalAction.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                lblFinalAction.BackColor = Color.Yellow
            End If

            'lblFinalOnWeb
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblFinalOnWeb.BackColor = Color.PeachPuff
            End If

            'lblInformationReceived
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblInformationReceived.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblInformationReceived.BackColor = Color.Yellow
            End If

            'lblInformationRequested
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
           (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblInformationRequested.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
             (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblInformationRequested.BackColor = Color.Yellow
            End If

            'lblISMPReview
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
              AccountFormAccess(67, 2) = "1" Then
                lblISMPReview.BackColor = Color.Yellow
            End If

            'lblNotifiedAppReceived
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblNotifiedAppReceived.BackColor = Color.PeachPuff
            End If

            'lblPermitAction
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblPermitAction.BackColor = Color.LightGreen
            End If

            'lblPermitNumber
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblPermitNumber.BackColor = Color.LightGreen
            End If

            'lblPNExpires
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                lblPNExpires.BackColor = Color.PeachPuff
            End If

            'lblPublicAdvisory
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") Then
                lblPublicAdvisory.BackColor = Color.LightGreen
            End If

            'lblReceived
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
            (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                lblReceived.BackColor = Color.PeachPuff
            End If

            'lblReviewSubmitted
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblReviewSubmitted.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
              (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                lblReviewSubmitted.BackColor = Color.Yellow
            End If

            'lblSSCPReview
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
              (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                lblSSCPReview.BackColor = Color.Yellow
            End If

            'rdbISMPNo
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 AccountFormAccess(67, 2) = "1" Then
                rdbISMPNo.BackColor = Color.Yellow
            End If

            'rdbISMPYes
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 AccountFormAccess(67, 2) = "1" Then
                rdbISMPYes.BackColor = Color.Yellow
            End If

            'rdbSSCPNo
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
             (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                rdbSSCPNo.BackColor = Color.Yellow
            End If

            'rdbSSCPYes
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
           (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                rdbSSCPYes.BackColor = Color.Yellow
            End If

            'txtAIRSNumber
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtAIRSNumber.ReadOnly = False
            Else
                txtAIRSNumber.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtAIRSNumber.TextBoxBackColor = Color.PeachPuff
            End If

            'txtComments
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtComments.ReadOnly = False
            Else
                txtComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtComments.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtComments.BackColor = Color.Yellow
            End If

            'txtEPATargetedComments
            If AccountFormAccess(129, 3) = "1" OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                txtEPATargetedComments.ReadOnly = False
            Else
                txtEPATargetedComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
             (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") Then
                txtEPATargetedComments.BackColor = Color.PeachPuff
            End If

            'txtFacilityName
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.ReadOnly = False
            Else
                txtFacilityName.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtFacilityName.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityName.BackColor = Color.Yellow
            End If

            'txtFacilityStreetAddress
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                     (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.ReadOnly = False
            Else
                txtFacilityStreetAddress.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtFacilityStreetAddress.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityStreetAddress.BackColor = Color.Yellow
            End If

            'txtFacilityZipCode
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                     (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.ReadOnly = False
            Else
                txtFacilityZipCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtFacilityZipCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtFacilityZipCode.BackColor = Color.Yellow
            End If

            'txtInformationReceived
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                 (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.ReadOnly = False
            Else
                txtInformationReceived.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
              (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") OrElse
              (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationReceived.BackColor = Color.Yellow
            End If

            'txtInformationRequested
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                 (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.ReadOnly = False
            Else
                txtInformationRequested.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
              (AccountFormAccess(131, 2) = "1" AndAlso AccountFormAccess(127, 3) = "1" AndAlso AccountFormAccess(127, 4) = "0") OrElse
              (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtInformationRequested.BackColor = Color.Yellow
            End If

            'txtISMPComments
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                AccountFormAccess(67, 2) = "1" Then
                txtISMPComments.BackColor = Color.Yellow
            End If

            'txtNAICSCode
            If AccountFormAccess(129, 3) = "1" OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                 (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                 (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                     (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.ReadOnly = False
            Else
                txtNAICSCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtNAICSCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
              (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtNAICSCode.BackColor = Color.Yellow
            End If

            'chbFederallyOwned
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbFederallyOwned.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbFederallyOwned.BackColor = Color.LightBlue
            End If

            'chbNspsFeeExempt
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbNspsFeeExempt.Enabled = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                chbNspsFeeExempt.BackColor = Color.LightBlue
            End If

            'txtPermitNumber
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.ReadOnly = False
            Else
                txtPermitNumber.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
              (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPermitNumber.BackColor = Color.Yellow
            End If

            'txtPlantDescription
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                    (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.ReadOnly = False
            Else
                txtPlantDescription.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtPlantDescription.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtPlantDescription.BackColor = Color.Yellow
            End If

            'txtReasonAppSubmitted
            If AccountFormAccess(129, 3) = "1" OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
                (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
                (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                    (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.ReadOnly = False
            Else
                txtReasonAppSubmitted.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtReasonAppSubmitted.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtReasonAppSubmitted.BackColor = Color.Yellow
            End If

            'txtSICCode
            If AccountFormAccess(129, 3) = "1" OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
              (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
              (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") OrElse
              (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
                  (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtSICCode.ReadOnly = False
            Else
                txtSICCode.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtSICCode.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(23, 3) = "1" AndAlso AccountFormAccess(138, 1) = "1") Then
                txtSICCode.BackColor = Color.PeachPuff
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtSICCode.BackColor = Color.Yellow
            End If

            'txtSignificantComments
            If AccountFormAccess(129, 3) = "1" OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
               (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") OrElse
               (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) OrElse
               (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtSignificantComments.ReadOnly = False
            Else
                txtSignificantComments.ReadOnly = True
            End If
            If (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(3, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0") OrElse
            (AccountFormAccess(24, 3) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(12, 2) = "0" AndAlso AccountFormAccess(3, 4) = "0") Then
                txtSignificantComments.BackColor = Color.LightGreen
            End If
            If (AccountFormAccess(51, 4) = "1" AndAlso AccountFormAccess(12, 1) = "1" AndAlso AccountFormAccess(138, 0) Is Nothing) Then
                txtSignificantComments.BackColor = Color.Yellow
            End If

            'txtSSCPComments
            If (AccountFormAccess(3, 2) = "1" AndAlso AccountFormAccess(3, 4) = "0") OrElse
              (AccountFormAccess(48, 2) = "1" AndAlso AccountFormAccess(48, 3) = "0" AndAlso AccountFormAccess(48, 4) = "0") Then
                txtSSCPComments.BackColor = Color.Yellow
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFacilityApplicationHistory()
        If AppNumber = 0 OrElse FacilityApplicationHistoryLoaded OrElse AirsId Is Nothing Then
            Return
        End If

        Dim query As String = "Select CONVERT(int, SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber, " &
            "case " &
            "    when strApplicationTypeDesc is Null then ' ' " &
            "Else strApplicationTypeDesc " &
            "End strApplicationTypeDesc, " &
            "case " &
            "    when SSPPApplicationMaster.strStaffResponsible is Null then ' ' " &
            "else concat(strLastName,', ',strFirstName) " &
            "end staffResponsible, " &
            "case " &
            "    when SSPPApplicationMaster.datFinalizedDate is Null " &
            " or SSPPApplicationMaster.datFinalizedDate = '1776-07-04' then ' ' " &
            "else format(SSPPApplicationMaster.datFinalizedDate, 'MMMM d, yyyy') " &
            "end FinalizedDate, " &
            "case " &
            "    when SSPPApplicationTracking.datSentByFacility is Null then ' ' " &
            "else format(SSPPApplicationTracking.datSentByFacility, 'MMMM d, yyyy') " &
            "end DateSent, " &
            "case " &
            "    when LookUpEPDUnits.strUnitDesc is Null then ' ' " &
            "Else LookUpEPDUnits.strUnitDesc " &
            "End strUnitTitle, " &
            "case " &
            "    when SSPPApplicationData.strComments is Null then ' ' " &
            "else SSPPApplicationData.strComments " &
            "end strComments, " &
            "case " &
            "    when SSPPApplicationData.strApplicationNotes is Null then ' ' " &
            "else SSPPApplicationData.strApplicationNotes " &
            "end strApplicationNotes " &
            "from SSPPApplicationData " &
            "inner join SSPPApplicationMaster " &
            "on SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "inner join SSPPApplicationTracking " &
            "on SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "inner join EPDUserProfiles " &
            "on EPDUserProfiles.numUserID = SSPPApplicationMaster.strStaffResponsible " &
            "left join LookUpEPDUnits " &
            "on SSPPApplicationMaster.APBUnit = LookUpEPDUnits.numUnitCode " &
            "left join LookUpApplicationTypes " &
            "on SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "where SSPPApplicationMaster.strAIRSNumber = @AIRSNumber "

        Dim parameter As New SqlParameter("@AIRSNumber", AirsId.DbFormattedString)

        Dim dtFacAppHistory As DataTable = DB.GetDataTable(query, parameter)

        If dtFacAppHistory Is Nothing Then
            dgvFacilityAppHistory.DataSource = Nothing
            Return
        End If

        dgvFacilityAppHistory.DataSource = dtFacAppHistory

        dgvFacilityAppHistory.RowHeadersVisible = False
        dgvFacilityAppHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvFacilityAppHistory.AllowUserToResizeColumns = True
        dgvFacilityAppHistory.AllowUserToAddRows = False
        dgvFacilityAppHistory.AllowUserToDeleteRows = False
        dgvFacilityAppHistory.AllowUserToOrderColumns = True
        dgvFacilityAppHistory.AllowUserToResizeRows = False

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

        CheckForLinkedApplications()

        FacilityApplicationHistoryLoaded = True
    End Sub

    Private Sub LoadInformationRequestHistory()
        If AppNumber = 0 Then
            Return
        End If

        Dim query As String = "Select " &
            "strApplicationNumber, strRequestKey, " &
            "Case " &
            "   when datInformationRequested is Null then ' ' " &
            "else format(datInformationRequested, 'MMMM d, yyyy') " &
            "End InformationRequested, " &
            "case " &
            "when strInformationrequested is Null then ' ' " &
            "else strInformationrequested " &
            "end strInformationrequested, " &
            "case " &
            "    when datInformationReceived is null then ' ' " &
            "else format(datInformationReceived, 'MMMM d, yyyy') " &
            "End InformationReceived, " &
            "case " &
            "when strInformationReceived is Null then ' ' " &
            "else strInformationReceived " &
            "end strInformationReceived " &
            "from SSPPApplicationInformation " &
            "where strApplicationNumber = @appnumber " &
            "order by strRequestKey "

        Dim parameter As New SqlParameter("@appnumber", AppNumber)

        Dim dtFacInfoHistory As DataTable = DB.GetDataTable(query, parameter)

        If dtFacInfoHistory Is Nothing Then
            dgvInformationRequested.DataSource = Nothing
            Return
        End If

        dgvInformationRequested.DataSource = dtFacInfoHistory

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
    End Sub

    Private Sub LoadSubPartData()
        Try
            Dim dtPart60 As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NSPS.ToString)
            Dim dtPart61 As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NESHAP.ToString)
            Dim dtPart63 As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.MACT.ToString)
            Dim dtSIP As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.SIP.ToString)

            With cboSIPSubpart
                .DataSource = dtSIP
                .DisplayMember = "Long Description"
                .ValueMember = "SubPart"
                .SelectedIndex = 0
            End With

            With cboNSPSSubpart
                .DataSource = dtPart60
                .DisplayMember = "Description"
                .ValueMember = "SubPart"
                .SelectedIndex = 0
            End With

            With cboNESHAPSubpart
                .DataSource = dtPart61
                .DisplayMember = "Description"
                .ValueMember = "SubPart"
                .SelectedIndex = 0
            End With

            With cboMACTSubpart
                .DataSource = dtPart63
                .DisplayMember = "Description"
                .ValueMember = "SubPart"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub LoadBasicFacilityInfo()
        If AirsId Is Nothing Then
            Return
        End If

        Dim Facilityname As String = "N/A"
        Dim FacilityStreet As String = "N/A"
        Dim FacilityCity As String = "N/A"
        Dim FacilityZipCode As String = "N/A"
        Dim OperationalStatus As String = "N/A"
        Dim OperationalStatusLine As String = "Operating Status - "
        Dim Classification As String = "N/A"
        Dim ClassificationLine As String = "Classification - "
        Dim AirProgramCodes As String = "000000000000000"
        Dim AirPrograms As String = ""
        Dim AirProgramCheck As String
        Dim AirProgramLine As String
        Dim SIC As String = "N/A"
        Dim SICLine As String = "SIC Code - "
        Dim NAICS As String = "N/A"
        Dim NAICSLine As String = "NAICS Code - "
        Dim CountyName As String = "N/A"
        Dim District As String = "N/A"
        Dim Attainment As String = "00000"
        Dim AttainmentStatus As String
        Dim StateProgramCodes As String = "00000"
        Dim StatePrograms As String
        Dim PlantDesc As String = "N/A"
        Dim PlantLine As String = "Plant Description - "
        Dim DistResponsible As String = "False"
        Dim OwnershipTypeCode As String = Nothing
        Dim NspsFeeExempt As Boolean = False

        Try
            Dim query As String = "select
                f.strFacilityName,
                f.strFacilityStreet1,
                f.strFacilityCity,
                f.strFacilityZipCode,
                h.strOperationalStatus,
                h.strClass,
                h.strAirProgramCodes,
                h.strSICCode,
                h.strNAICSCode,
                c.strCountyName,
                d.strDistrictName,
                h.strPlantDescription,
                h.strAttainmentStatus,
                h.strStateProgramCodes,
                r.strDistrictResponsible,
                s.FacilityOwnershipTypeCode,
                s.NspsFeeExempt
            from APBFACILITYINFORMATION f
                inner join APBHEADERDATA h
                    on f.strAIRSNumber = h.strAIRSNumber
                inner join LOOKUPCOUNTYINFORMATION c
                    on SUBSTRING(f.strAIRSNumber, 5, 3) = c.strCountyCode
                inner join LOOKUPDISTRICTINFORMATION i
                    on SUBSTRING(f.strAIRSNumber, 5, 3) = i.strDistrictCounty
                inner join LOOKUPDISTRICTS d
                    on d.strDistrictCode = i.strDistrictCode
                inner join APBSUPPLAMENTALDATA s
                    on f.strAIRSNumber = s.strAIRSNumber
                left join SSCPDISTRICTRESPONSIBLE r
                    on f.strAIRSNumber = r.strAIRSnumber
            where f.strAIRSNumber = @AirsNumber "

            Dim parameter As New SqlParameter("@AirsNumber", AirsId.DbFormattedString)

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
                Facilityname = If(DBUtilities.GetNullableString(dr.Item("strFacilityname")), "N/A")
                FacilityStreet = If(DBUtilities.GetNullableString(dr.Item("strFacilityStreet1")), "N/A")
                FacilityCity = If(DBUtilities.GetNullableString(dr.Item("strFacilityCity")), "N/A")
                FacilityZipCode = If(DBUtilities.GetNullableString(dr.Item("strFacilityZipCode")), "N/A")
                OperationalStatus = If(DBUtilities.GetNullableString(dr.Item("strOperationalStatus")), "N/A")
                OperationalStatusLine = "Operating Status - " & OperationalStatus
                Classification = If(DBUtilities.GetNullableString(dr.Item("strClass")), "N/A")
                ClassificationLine = "Classification - " & Classification
                AirProgramCodes = If(DBUtilities.GetNullableString(dr.Item("strAirProgramCodes")), "000000000000000")
                SIC = If(DBUtilities.GetNullableString(dr.Item("strSICCode")), "N/A")
                SICLine = "SIC Code - " & SIC
                NAICS = If(DBUtilities.GetNullableString(dr.Item("strNAICSCode")), "N/A")
                NAICSLine = "NAICS Code - " & NAICS
                OwnershipTypeCode = DBUtilities.GetNullableString(dr.Item("FacilityOwnershipTypeCode"))
                NspsFeeExempt = dr.Item("NspsFeeExempt")
                CountyName = If(DBUtilities.GetNullableString(dr.Item("strCountyName")), "N/A")
                District = If(DBUtilities.GetNullableString(dr.Item("strDistrictName")), "N/A")
                Attainment = If(DBUtilities.GetNullableString(dr.Item("strAttainmentstatus")), "00000")
                StateProgramCodes = If(DBUtilities.GetNullableString(dr.Item("strStateProgramCodes")), "00000")
                PlantDesc = If(DBUtilities.GetNullableString(dr.Item("strPlantDescription")), "N/A")
                PlantLine = "Plant Description - " & PlantDesc
                DistResponsible = If(DBUtilities.GetNullableString(dr.Item("strDistrictResponsible")), "False")
            End If

            If CInt(Mid(AirProgramCodes, 1, 1)) = 1 Then
                AirPrograms = "   0 - SIP" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 2, 1)) = 1 Then
                AirPrograms = AirPrograms & "   1 - Federal SIP" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 3, 1)) = 1 Then
                AirPrograms = AirPrograms & "   3 - Non-Federal SIP" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 4, 1)) = 1 Then
                AirPrograms = AirPrograms & "   4 - CFC Tracking" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 5, 1)) = 1 Then
                AirPrograms = AirPrograms & "   6 - PSD" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 6, 1)) = 1 Then
                AirPrograms = AirPrograms & "   7 - NSR" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 7, 1)) = 1 Then
                AirPrograms = AirPrograms & "   8 - NESHAP" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 8, 1)) = 1 Then
                AirPrograms = AirPrograms & "   9 - NSPS" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 9, 1)) = 1 Then
                AirPrograms = AirPrograms & "   F - FESOP" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 10, 1)) = 1 Then
                AirPrograms = AirPrograms & "   A - Acid Precipitation" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 11, 1)) = 1 Then
                AirPrograms = AirPrograms & "   I - Native American" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 12, 1)) = 1 Then
                AirPrograms = AirPrograms & "   M - MACT" & vbNewLine
            End If
            If CInt(Mid(AirProgramCodes, 13, 1)) = 1 Then
                AirPrograms = AirPrograms & "   V - Title V Permit" & vbNewLine
            End If
            AirProgramLine = "Air Programs - " & vbNewLine & AirPrograms

            Select Case CInt(Mid(Attainment, 2, 1))
                Case 0
                    AttainmentStatus = ""
                Case 1
                    AttainmentStatus = "   1-hr Ozone"
                Case 2
                    AttainmentStatus = "   1-hr Ozone (Contributing)"
                Case Else
                    AttainmentStatus = ""
            End Select

            Select Case CInt(Mid(Attainment, 3, 1))
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "8-hr Atlanta"
                    Else
                        AttainmentStatus = "   8-hr Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "8-hr Macon"
                    Else
                        AttainmentStatus = "   8-hr Macon"
                    End If
            End Select

            Select Case CInt(Mid(Attainment, 4, 1))
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Atlanta"
                    Else
                        AttainmentStatus = "   PM 2.5 Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5  Chattanooga"
                    Else
                        AttainmentStatus = "   PM 2.5  Chattanooga"
                    End If
                Case 3
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Floyd"
                    Else
                        AttainmentStatus = "   PM 2.5 Floyd"
                    End If
                Case 4
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Macon"
                    Else
                        AttainmentStatus = "   PM 2.5 Macon"
                    End If
            End Select

            If AttainmentStatus = "" Then
                AttainmentStatus = "Non Attainment Area - N/A"
            Else
                AttainmentStatus = "Non Attainment Area - " & vbNewLine & AttainmentStatus
            End If

            Select Case CInt(Mid(StateProgramCodes, 1, 1))
                Case 0
                    StatePrograms = ""
                Case 1
                    StatePrograms = "   NSR/PSD"
                Case Else
                    StatePrograms = ""
            End Select

            Select Case CInt(Mid(StateProgramCodes, 2, 1))
                Case 1
                    If StatePrograms <> "" Then
                        StatePrograms &= "HAPs Major"
                    Else
                        StatePrograms = "   HAPs Major"
                    End If
            End Select

            If StatePrograms = "" Then
                StatePrograms = "State Codes - N/A"
            Else
                StatePrograms = "State Codes - " & vbNewLine & StatePrograms
            End If

            rtbFacilityInformation.Clear()

            rtbFacilityInformation.Text = AirsId.FormattedString & vbNewLine &
                Facilityname & vbNewLine &
                FacilityStreet & vbNewLine &
                FacilityCity & ", GA " & FacilityZipCode & vbNewLine & vbNewLine &
                OperationalStatusLine & vbNewLine &
                ClassificationLine & vbNewLine &
                SICLine & vbNewLine &
                NAICSLine & vbNewLine &
                AirProgramLine &
                StatePrograms & vbNewLine &
            CountyName & " County" & vbNewLine &
                "District - " & District & vbNewLine &
                AttainmentStatus & vbNewLine & vbNewLine &
                PlantLine

            cboCounty.SelectedIndex = cboCounty.FindString(CountyName)
            txtDistrict.Text = District

            If txtFacilityName.Text = "" Then
                txtFacilityName.Text = Facilityname
                txtFacilityStreetAddress.Text = FacilityStreet

                If String.IsNullOrWhiteSpace(FacilityCity) OrElse FacilityCity = "N/A" Then
                    cboFacilityCity.Text = ""
                Else
                    cboFacilityCity.Text = FacilityCity
                End If

                txtFacilityZipCode.Text = FacilityZipCode
                txtSICCode.Text = SIC
                txtNAICSCode.Text = NAICS

                ' Currently we are only tracking federally-owned facilities. Eventually 
                ' this could be expanded to use a drop-down with all ownership types.
                chbFederallyOwned.Checked = OwnershipTypeCode = FederallyOwnedTypeCode

                chbNspsFeeExempt.Checked = NspsFeeExempt

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
                    Case Else
                        cboClassification.Text = ""
                End Select

                txtPlantDescription.Text = PlantDesc

                chbCDS_0.Checked = True
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
                If txtFacilityName.Text <> Facilityname AndAlso rtbFacilityInformation.Find(Facilityname) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(Facilityname)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtFacilityStreetAddress.Text <> FacilityStreet AndAlso rtbFacilityInformation.Find(FacilityStreet) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityStreet)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If cboFacilityCity.Text <> FacilityCity AndAlso rtbFacilityInformation.Find(FacilityCity) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityCity)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtFacilityZipCode.Text <> FacilityZipCode AndAlso rtbFacilityInformation.Find(FacilityZipCode) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(FacilityZipCode)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtSICCode.Text <> SIC AndAlso rtbFacilityInformation.Find(SICLine) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(SICLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtNAICSCode.Text <> NAICS AndAlso rtbFacilityInformation.Find(NAICSLine) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(NAICSLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If Mid(cboOperationalStatus.Text, 1, 1) <> OperationalStatus AndAlso rtbFacilityInformation.Find(OperationalStatusLine) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(OperationalStatusLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If Mid(cboClassification.Text, 1, 1) <> Mid(Classification, 1, 1) AndAlso rtbFacilityInformation.Find(ClassificationLine) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(ClassificationLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                If txtPlantDescription.Text <> PlantDesc AndAlso rtbFacilityInformation.Find(PlantLine) <> -1 Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find(PlantLine)
                    rtbFacilityInformation.SelectionColor = Color.Tomato
                End If
                AirProgramCheck = "000000000000000"
                If chbCDS_0.Checked Then
                    AirProgramCheck = "1" & Mid(AirProgramCheck, 2)
                End If
                If chbCDS_6.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 4) & "1" & Mid(AirProgramCheck, 6)
                End If
                If chbCDS_7.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 5) & "1" & Mid(AirProgramCheck, 7)
                End If
                If chbCDS_8.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 6) & "1" & Mid(AirProgramCheck, 8)
                End If
                If chbCDS_9.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 7) & "1" & Mid(AirProgramCheck, 9)
                End If
                If chbCDS_A.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 9) & "1" & Mid(AirProgramCheck, 11)
                End If
                If chbCDS_M.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 11) & "1" & Mid(AirProgramCheck, 13)
                End If
                If chbCDS_V.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 12) & "1" & Mid(AirProgramCheck, 14)
                End If
                If chbCDS_RMP.Checked Then
                    AirProgramCheck = Mid(AirProgramCheck, 1, 13) & "1" & Mid(AirProgramCheck, 15)
                End If

                AirProgramCodes = Mid(AirProgramCodes, 1, 1) & "000" & Mid(AirProgramCodes, 5, 4) & "0" & Mid(AirProgramCodes, 10, 1) & "0" & Mid(AirProgramCodes, 12, 2) & "00"

                If AirProgramCheck <> AirProgramCodes Then
                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Air Programs - ")
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
                            If txt1HourOzone.Text = "Contributing" AndAlso Mid(Attainment, 2, 1) <> 2 Then
                                rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                rtbFacilityInformation.SelectionColor = Color.Tomato
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
                        If txt8HROzone.Text = "Macon" AndAlso Mid(Attainment, 3, 1) <> 2 Then
                            rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                            rtbFacilityInformation.SelectionColor = Color.Tomato
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
                                If txtPM.Text = "Macon" AndAlso Mid(Attainment, 4, 1) <> 4 Then
                                    rtbFacilityInformation.SelectionStart = rtbFacilityInformation.Find("Non Attainment Area - ")
                                    rtbFacilityInformation.SelectionColor = Color.Tomato
                                End If
                            End If
                        End If
                    End If
                End If
                If Not chbNSRMajor.Checked Then
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
                If Not chbHAPsMajor.Checked Then
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
            ErrorReport(ex, AirsId.FormattedString, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFacilityAttainmentStatus()
        If AirsId Is Nothing Then
            Return
        End If

        Try
            Dim Attainment As String = ""

            Dim query As String = "Select " &
            "strAttainmentStatus " &
            "from APBHeaderData " &
            "where strAIRSNumber = @airsnumber "

            Dim parameter As New SqlParameter("@airsnumber", AirsId.DbFormattedString)

            Attainment = DB.GetString(query, parameter)
            If Attainment = "" Then Attainment = "00000"

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadContactData()
        Try
            Dim query As String = "Select " &
            "strContactFirstName, " &
            "strContactLastName, " &
            "strContactPrefix, " &
            "strContactSuffix, " &
            "strContactTitle, " &
            "strContactCompanyName, " &
            "strContactPhoneNumber1, " &
            "strContactFaxNumber, " &
            "strContactEmail, " &
            "strContactAddress1, " &
            "strContactCity, " &
            "strContactState, " &
            "strContactZipCode, " &
            "strContactDescription " &
            "from SSPPApplicationContact " &
            "where strApplicationNumber = @appnumber "

            Dim parameter As New SqlParameter("@appnumber", AppNumber)

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
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
                    txtContactPhoneNumber.Clear()
                Else
                    txtContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
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
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadApplicationData()
        Dim CloseOut As Boolean = False
        Dim temp As String = ""
        Dim dr As DataRow
        Dim query As String
        Dim parameter As New SqlParameter("@appnumber", AppNumber)

        Try
            LastModificationDateAsLoaded = GetWhenLastModified(AppNumber)

            query = "select strAIRSNumber,
                       strStaffResponsible,
                       strApplicationType,
                       strPermitType,
                       APBUnit,
                       datFinalizedDate,
                       strFacilityName,
                       strFacilityStreet1,
                       strFacilityCity,
                       strFacilityZipCode,
                       strOperationalStatus,
                       strClass,
                       strAirProgramCodes,
                       strSICCode,
                       strNAICSCode,
                       strPermitNumber,
                       strPlantDescription,
                       strComments                                      as DataComments,
                       strApplicationNotes,
                       strStateProgramCodes,
                       datReceivedDate,
                       datSentByFacility,
                       datAssignedToEngineer,
                       datReassignedToEngineer,
                       datAcknowledgementLetterSent,
                       strPublicInvolvement,
                       datToPMI,
                       datToPMII,
                       datReturnedToEngineer,
                       datPermitIssued,
                       datApplicationDeadline,
                       datDraftIssued,
                       strPAReady,
                       strPNReady,
                       datEPAWaived,
                       datEPAEnds,
                       datToBranchCheif,
                       datToDirector,
                       datPAExpires,
                       datPNExpires,
                       strStateprogramcodes,
                       strTrackedRules,
                       STRSIGNIFICANTCOMMENTS,
                       strPAPosted,
                       strPNPosted,
                       FacilityOwnershipTypeCode,
                       NspsFeeExempt,
                       CASE
                           WHEN t.DATPERMITISSUED IS NOT NULL THEN 'Permit Issued'
                           WHEN t.DATTODIRECTOR IS NOT NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR)
                               THEN 'To Director''s Office'
                           WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND t.DATTODIRECTOR IS NULL AND
                                (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN 'To Branch Chief'
                           WHEN t.DATEPAENDS IS NOT NULL THEN 'EPA 45-day Review'
                           WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN 'Public Notice Expired'
                           WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN 'Public Notice'
                           WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN 'Draft Issued'
                           WHEN t.DATTOPMII IS NOT NULL THEN 'To Program Manager'
                           WHEN t.DATTOPMI IS NOT NULL THEN 'To Unit Manager'
                           WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0')
                               THEN 'Internal Review'
                           WHEN m.STRSTAFFRESPONSIBLE IS NULL OR m.STRSTAFFRESPONSIBLE = '0' THEN '00 - Received'
                           ELSE 'At Staff'
                       END                                              AS [Application Status],
                       convert(date,
                               CASE
                                   WHEN t.DATPERMITISSUED IS NOT NULL THEN t.DATPERMITISSUED
                                   WHEN t.DATTODIRECTOR IS NOT NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR)
                                       THEN t.DATTODIRECTOR
                                   WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND t.DATTODIRECTOR IS NULL AND
                                        (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN t.DATTOBRANCHCHEIF
                                   WHEN t.DATEPAENDS IS NOT NULL THEN t.DATEPAENDS
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN t.DATPNEXPIRES
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN t.DATPNEXPIRES
                                   WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN t.DATDRAFTISSUED
                                   WHEN t.DATTOPMII IS NOT NULL THEN t.DATTOPMII
                                   WHEN t.DATTOPMI IS NOT NULL THEN t.DATTOPMI
                                   WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0')
                                       THEN t.DATREVIEWSUBMITTED
                                   WHEN m.STRSTAFFRESPONSIBLE IS NULL OR m.STRSTAFFRESPONSIBLE = '0' THEN t.DATRECEIVEDDATE
                                   ELSE t.DATASSIGNEDTOENGINEER
                               END)                                     AS [Status Date],
                       convert(bit, substring(d.STRTRACKEDRULES, 7, 1)) AS [Expedited Permit]
                from SSPPApplicationMaster m
                    left join SSPPApplicationTracking t
                    on m.strApplicationNumber = t.strApplicationNumber
                    left join SSPPApplicationData d
                    on m.strApplicationNumber = d.strApplicationNumber
                where m.strApplicationNumber = @appnumber"

            dr = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
                If Not IsDBNull(dr.Item("strAIRSNumber")) AndAlso ApbFacilityId.IsValidAirsNumberFormat(dr.Item("strAIRSNumber").ToString) Then
                    AirsId = New ApbFacilityId(dr.Item("strAIRSNumber").ToString)
                    txtAIRSNumber.AirsNumber = AirsId
                Else
                    AirsId = Nothing
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
                    chbExpFee.Checked = False
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
                        chbExpFee.Checked = False
                    Else
                        chbExpFee.Checked = True
                    End If
                    If Mid(dr.Item("strTrackedRules"), 8, 1) = "0" Then
                        chbConfidential.Checked = False
                    Else
                        chbConfidential.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strApplicationType")) Then
                    cboApplicationType.SelectedValue = 0
                Else
                    cboApplicationType.SelectedValue = dr.Item("strApplicationType")
                End If
                If IsDBNull(dr.Item("strPermitType")) OrElse String.IsNullOrWhiteSpace(dr.Item("strPermitType").ToString) Then
                    cboPermitAction.SelectedIndex = 0
                Else
                    cboPermitAction.SelectedValue = dr.Item("strPermitType").ToString

                    If cboPermitAction.SelectedValue Is Nothing OrElse String.IsNullOrEmpty(cboPermitAction.SelectedValue.ToString) Then
                        Dim dt As DataTable = CType(cboPermitAction.DataSource, DataTable)
                        Dim newRow As DataRow = dt.NewRow
                        newRow.Item("STRPERMITTYPECODE") = dr.Item("strPermitType").ToString

                        Select Case dr.Item("strPermitType").ToString
                            Case "1"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "Amendment"
                            Case "3"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "Draft"
                            Case "4"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "New Permit"
                            Case "8"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "PRMT-DNL"
                            Case "10"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "Revoked"
                            Case "12"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "Initial Title V Permit"
                            Case "13"
                                newRow.Item("STRPERMITTYPEDESCRIPTION") = "Renewal Title V Permit"
                        End Select

                        dt.Rows.Add(newRow)

                        cboPermitAction.SelectedValue = dr.Item("strPermitType").ToString
                    End If
                End If
                If IsDBNull(dr.Item("APBUnit")) Then
                    cboApplicationUnit.SelectedIndex = 0
                Else
                    cboApplicationUnit.SelectedValue = dr.Item("APBUnit")
                End If
                If IsDBNull(dr.Item("datFinalizedDate")) Then
                    chbClosedOut.Checked = False
                Else
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

                If IsDBNull(dr.Item("strFacilityCity")) OrElse
                    String.IsNullOrWhiteSpace(dr.Item("strFacilityCity").ToString) OrElse
                    dr.Item("strFacilityCity").ToString = "N/A" Then
                    cboFacilityCity.Text = ""
                Else
                    cboFacilityCity.Text = dr.Item("strFacilityCity").ToString
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
                    chbCDS_0.Checked = True
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

                chbFederallyOwned.Checked = DBUtilities.GetNullableString(dr.Item("FacilityOwnershipTypeCode")) = FederallyOwnedTypeCode

                chbNspsFeeExempt.Checked = dr.Item("NspsFeeExempt")

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
                                txtPermitNumber.Text = Mid(dr.Item("strPermitNumber"), 1, 4) & "-" & Mid(dr.Item("strPermitNumber"), 5, 3) &
                                "-" & Mid(dr.Item("strPermitNumber"), 8, 4) & "-" & Mid(dr.Item("strPermitNumber"), 12, 1) &
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
                    CloseOut = False
                Else
                    CloseOut = True
                End If
                If IsDBNull(dr.Item("datReceivedDate")) Then
                    DTPDateReceived.Value = Today
                Else
                    DTPDateReceived.Value = dr.Item("datReceivedDate")
                End If
                If IsDBNull(dr.Item("datSentByFacility")) Then
                    DTPDateSent.Value = Today
                Else
                    DTPDateSent.Value = dr.Item("datSentByFacility")
                End If
                If IsDBNull(dr.Item("datAssignedToEngineer")) Then
                    DTPDateAssigned.Value = Today
                    DTPDateAssigned.Checked = False
                Else
                    DTPDateAssigned.Value = dr.Item("datAssignedToEngineer")
                    DTPDateAssigned.Checked = True
                End If
                If IsDBNull(dr.Item("datReassignedToEngineer")) Then
                    DTPDateReassigned.Value = Today
                    DTPDateReassigned.Checked = False
                Else
                    DTPDateReassigned.Value = dr.Item("datReassignedToEngineer")
                    DTPDateReassigned.Checked = True
                End If

                If IsDBNull(dr.Item("datAcknowledgementLetterSent")) Then
                    DTPDateAcknowledge.Value = Today
                    DTPDateAcknowledge.Checked = False
                Else
                    DTPDateAcknowledge.Value = dr.Item("datAcknowledgementLetterSent")
                    DTPDateAcknowledge.Checked = True
                End If
                If IsDBNull(dr.Item("strPublicInvolvement")) Then
                    cboPublicAdvisory.Text = "Not Decided"
                Else
                    temp = dr.Item("strPublicInvolvement")
                    Select Case temp
                        Case "1"
                            cboPublicAdvisory.Text = "PA Needed"
                        Case "2"
                            cboPublicAdvisory.Text = "PA Not Needed"
                        Case Else
                            cboPublicAdvisory.Text = "Not Decided"
                    End Select
                End If
                If IsDBNull(dr.Item("datToPMI")) Then
                    DTPDateToUC.Value = Today
                    DTPDateToUC.Checked = False
                Else
                    DTPDateToUC.Value = dr.Item("datToPMI")
                    DTPDateToUC.Checked = True
                End If
                If IsDBNull(dr.Item("datToPMII")) Then
                    DTPDateToPM.Value = Today
                    DTPDateToPM.Checked = False
                Else
                    DTPDateToPM.Value = dr.Item("datToPMII")
                    DTPDateToPM.Checked = True
                End If
                If IsDBNull(dr.Item("datPermitIssued")) Then
                    DTPFinalAction.Value = Today
                    DTPFinalAction.Checked = False
                Else
                    DTPFinalAction.Value = dr.Item("datPermitIssued")
                    DTPFinalAction.Checked = True
                End If
                If IsDBNull(dr.Item("datApplicationDeadLine")) Then
                    DTPDeadline.Value = Today
                    DTPDeadline.Checked = False
                Else
                    DTPDeadline.Value = dr.Item("datApplicationDeadline")
                    DTPDeadline.Checked = True
                End If
                If IsDBNull(dr.Item("datDraftIssued")) Then
                    DTPDraftIssued.Value = Today
                    DTPDraftIssued.Checked = False
                Else
                    DTPDraftIssued.Value = dr.Item("datDraftIssued")
                    DTPDraftIssued.Checked = True
                End If
                If IsDBNull(dr.Item("datPAExpires")) Then
                    DTPDatePAExpires.Value = Today
                    DTPDatePAExpires.Checked = False
                Else
                    DTPDatePAExpires.Value = dr.Item("datPAExpires")
                    DTPDatePAExpires.Checked = True
                End If
                If IsDBNull(dr.Item("datPNExpires")) Then
                    DTPDatePNExpires.Value = Today
                    DTPDatePNExpires.Checked = False
                Else
                    DTPDatePNExpires.Value = dr.Item("datPNExpires")
                    DTPDatePNExpires.Checked = True
                End If
                If IsDBNull(dr.Item("strPAReady")) Then
                    chbPAReady.Checked = False
                Else
                    If dr.Item("strPAReady").ToString = "True" Then
                        chbPAReady.Checked = True
                    Else
                        chbPAReady.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strPNReady")) Then
                    chbPNReady.Checked = False
                Else
                    If dr.Item("strPNReady").ToString = "True" Then
                        chbPNReady.Checked = True
                    Else
                        chbPNReady.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("datEPAWaived")) Then
                    DTPEPAWaived.Value = Today
                    DTPEPAWaived.Checked = False
                Else
                    DTPEPAWaived.Value = dr.Item("datEPAWaived")
                    DTPEPAWaived.Checked = True
                End If
                If IsDBNull(dr.Item("datEPAEnds")) Then
                    DTPEPAEnds.Value = Today
                    DTPEPAEnds.Checked = False
                Else
                    DTPEPAEnds.Value = dr.Item("datEPAEnds")
                    DTPEPAEnds.Checked = True
                End If
                If IsDBNull(dr.Item("datToBranchCheif")) Then
                    DTPDateToBC.Value = Today
                    DTPDateToBC.Checked = False
                Else
                    DTPDateToBC.Value = dr.Item("datToBranchCheif")
                    DTPDateToBC.Checked = True
                End If
                If IsDBNull(dr.Item("datToDirector")) Then
                    DTPDateToDO.Value = Today
                    DTPDateToDO.Checked = False
                Else
                    DTPDateToDO.Value = dr.Item("datToDirector")
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
                    chbPAReady.Checked = True
                    chbPAReady.Enabled = False
                End If
                If IsDBNull(dr.Item("strPNPosted")) Then
                    lblPNReady.Text = ""
                Else
                    lblPNReady.Text = dr.Item("strPNPosted")
                End If

                pnlStatusPanel.Visible = True
                Dim colorPair As ColorPair = GetStatusPanelColor(dr.Item("Expedited Permit"), dr.Item("Status Date"))
                pnlStatusPanel.BackColor = colorPair.BackgroundColor
                pnlStatusPanel.ForeColor = colorPair.ForegroundColor
                lblExpeditedLabel.Visible = CBool(dr.Item("Expedited Permit"))
                lblStatusDateLabel.Text = CDate(dr.Item("Status Date")).ToString(AppGlobals.DateFormat)
                lblStatusLabel.Text = dr.Item("Application Status").ToString()

            End If

            If TCApplicationTrackingLog.TabPages.Contains(TPReviews) Then
                query = "select " &
                "datReviewsubmitted, strSSCPUnit, " &
                "strSSCPReviewer, datSSCPReviewDate, " &
                "strSSCPComments, strISMPUnit, " &
                "strISMPReviewer, datISMPReviewDate, " &
                "strISMPComments " &
                "from SSPPApplicationData, " &
                "SSPPApplicationTracking " &
                "where SSPPApplicationData.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                "and SSPPApplicationData.strApplicationNumber = @appnumber"

                dr = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("datReviewsubmitted")) Then
                        DTPReviewSubmitted.Value = Today
                        DTPReviewSubmitted.Checked = False
                    Else
                        DTPReviewSubmitted.Value = CDate(dr.Item("datReviewsubmitted"))
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
                        DTPSSCPReview.Value = Today
                        DTPSSCPReview.Checked = False
                    Else
                        DTPSSCPReview.Value = dr.Item("datSSCPReviewDate")
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
                        If dr.Item("strSSCPComments").ToString = "N/A" Then
                            rdbSSCPNo.Checked = True
                            txtSSCPComments.Text = ""
                        Else
                            rdbSSCPYes.Checked = True
                            txtSSCPComments.Text = dr.Item("strSSCPComments")
                        End If
                    End If

                    If IsDBNull(dr.Item("datISMPReviewDate")) Then
                        DTPISMPReview.Value = Today
                        DTPISMPReview.Checked = False
                    Else
                        DTPISMPReview.Value = dr.Item("datISMPReviewDate")
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
                        If dr.Item("strISMPComments").ToString = "N/A" Then
                            rdbISMPNo.Checked = True
                            txtISMPComments.Text = ""
                        Else
                            rdbISMPYes.Checked = True
                            txtISMPComments.Text = dr.Item("strISMPComments")
                        End If
                    End If
                End If
            End If

            If TCApplicationTrackingLog.TabPages.Contains(TPWebPublisher) Then
                query = "Select " &
                "datDraftOnWeb, " &
                "datEPAStatesNotified,  " &
                "datFinalONWeb, " &
                "datEPANotified,  " &
                "datEffective," &
                " datEPAStatesNotifiedAppRec,  " &
                "datExperationDate, strTargeted, " &
                "datPNExpires " &
                "from  " &
                "SSPPApplicationMaster  " &
                "left join SSPPApplicationTracking  " &
                "on SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                "left join SSPPApplicationData " &
                "on SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                "where SSPPApplicationMaster.strApplicationNumber = @appnumber"

                dr = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("datEPAStatesNotifiedAppRec")) Then
                        DTPNotifiedAppReceived.Value = Today
                        DTPNotifiedAppReceived.Checked = False
                    Else
                        DTPNotifiedAppReceived.Value = dr.Item("datEPAStatesNotifiedAppRec")
                        DTPNotifiedAppReceived.Checked = True
                    End If
                    If IsDBNull(dr.Item("datDraftOnWeb")) Then
                        DTPDraftOnWeb.Value = Today
                        DTPDraftOnWeb.Checked = False
                    Else
                        DTPDraftOnWeb.Value = dr.Item("datDraftOnWeb")
                        DTPDraftOnWeb.Checked = True
                    End If
                    If IsDBNull(dr.Item("datEPAStatesNotified")) Then
                        DTPEPAStatesNotified.Value = Today
                        DTPEPAStatesNotified.Checked = False
                    Else
                        DTPEPAStatesNotified.Value = dr.Item("datEPAStatesNotified")
                        DTPEPAStatesNotified.Checked = True
                    End If
                    If IsDBNull(dr.Item("datFinalOnWeb")) Then
                        DTPFinalOnWeb.Value = Today
                        DTPFinalOnWeb.Checked = False
                    Else
                        DTPFinalOnWeb.Value = dr.Item("datFinalOnWeb")
                        DTPFinalOnWeb.Checked = True
                    End If
                    If IsDBNull(dr.Item("DatEPANotified")) Then
                        DTPEPANotifiedPermitOnWeb.Value = Today
                        DTPEPANotifiedPermitOnWeb.Checked = False
                    Else
                        DTPEPANotifiedPermitOnWeb.Value = dr.Item("DatEPANotified")
                        DTPEPANotifiedPermitOnWeb.Checked = True
                    End If
                    If IsDBNull(dr.Item("DatEffective")) Then
                        DTPEffectiveDateofPermit.Value = Today
                        DTPEffectiveDateofPermit.Checked = False
                    Else
                        DTPEffectiveDateofPermit.Value = dr.Item("datEffective")
                        DTPEffectiveDateofPermit.Checked = True
                    End If
                    If IsDBNull(dr.Item("datExperationDate")) Then
                        DTPExperationDate.Value = Today
                        DTPExperationDate.Checked = False
                    Else
                        DTPExperationDate.Checked = True
                        DTPExperationDate.Value = dr.Item("datExperationDate")
                    End If
                    If IsDBNull(dr.Item("strTargeted")) Then
                        txtEPATargetedComments.Text = ""
                    Else
                        txtEPATargetedComments.Text = dr.Item("strTargeted")
                    End If
                    If IsDBNull(dr.Item("datPNExpires")) Then
                        DTPPNExpires.Value = Today
                        DTPPNExpires.Checked = False
                    Else
                        DTPPNExpires.Checked = True
                        DTPPNExpires.Value = dr.Item("datPNExpires")
                    End If

                End If
            End If

            CheckForLinkedApplications()

            If CloseOut Then
                CloseOutApplication(CloseOut)
            End If

        Catch ex As Exception
            ErrorReport(ex, "App #: " & AppNumber & "; temp: " & temp, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Shared Function GetStatusPanelColor(expedited As Boolean, statusDate As Date) As ColorPair
        If expedited Then
            Select Case DateDiff(DateInterval.Day, statusDate, Today)
                Case > 60, < 0
                    Return IaipColors.ErrorColorPair
                Case > 30
                    Return IaipColors.WarningColorPair
                Case > 15
                    Return IaipColors.InfoColorPair
                Case Else
                    Return IaipColors.SuccessColorPair
            End Select
        End If

        Select Case DateDiff(DateInterval.Day, statusDate, Today)
            Case > 60, < 0
                Return IaipColors.ErrorColorPair
            Case > 60
                Return IaipColors.WarningColorPair
            Case > 30
                Return IaipColors.InfoColorPair
            Case Else
                Return IaipColors.SuccessColorPair
        End Select
    End Function

    Private Sub ReLoadBasicFacilityInfo()
        ' TODO is this the same as LoadBasicFacilityData?
        Try
            Dim Facilityname As String = "N/A"
            Dim FacilityStreet As String = "N/A"
            Dim FacilityCity As String = "N/A"
            Dim FacilityZipCode As String = "N/A"
            Dim OperationalStatus As String = "N/A"
            Dim OperationalStatusLine As String = "Operating Status - "
            Dim Classification As String = "N/A"
            Dim ClassificationLine As String = "Classification - "
            Dim AirProgramCodes As String = "000000000000000"
            Dim AirPrograms As String = ""
            Dim AirProgramLine As String = ""
            Dim SIC As String = "N/A"
            Dim SICLine As String = "SIC Code - "
            Dim NAICS As String = "N/A"
            Dim NAICSLine As String = "NAICS Code - "
            Dim CountyName As String = "N/A"
            Dim District As String = "N/A"
            Dim Attainment As String = "00000"
            Dim AttainmentStatus As String = ""
            Dim StateProgramCodes As String = "00000"
            Dim StatePrograms As String = ""
            Dim PlantDesc As String = "N/A"
            Dim PlantLine As String = "Plant Description - "
            Dim OwnershipTypeCode As String = Nothing
            Dim NspsFeeExempt As Boolean = False

            Dim query As String = "Select
                strFacilityName,
                strFacilityStreet1,
                strFacilityCity,
                strFacilityZipCode,
                strSICCode,
                strClass,
                strNAICSCode,
                strOperationalStatus,
                strAirProgramCodes,
                strPlantDescription,
                h.strAttainmentStatus,
                strStateProgramCodes,
                strcountyName,
                strDistrictName,
                s.FacilityOwnershipTypeCode,
                s.NspsFeeExempt
            from APBFACILITYINFORMATION f
                inner join APBHEADERDATA h
                    on f.STRAIRSNUMBER = h.STRAIRSNUMBER
                inner join LookUpCountyInformation c
                    on SUBSTRING(f.STRAIRSNUMBER, 5, 3) = c.STRCOUNTYCODE
                inner join LOOKUPDISTRICTINFORMATION i
                    on SUBSTRING(f.STRAIRSNUMBER, 5, 3) = i.STRDISTRICTCOUNTY
                inner join LOOKUPDISTRICTS d
                    on i.STRDISTRICTCODE = d.STRDISTRICTCODE
                inner join APBSUPPLAMENTALDATA s
                on s.STRAIRSNUMBER=f.STRAIRSNUMBER
            where h.STRAIRSNUMBER = @airsnumber"

            Dim parameter As New SqlParameter("@airsnumber", AirsId.DbFormattedString)

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
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

                OwnershipTypeCode = DBUtilities.GetNullableString(dr.Item("FacilityOwnershipTypeCode"))

                NspsFeeExempt = dr.Item("NspsFeeExempt")

                If IsDBNull(dr.Item("strCountyName")) Then
                    CountyName = "N/A"
                Else
                    CountyName = dr.Item("strCountyName")
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
            End If

            If Mid(AirProgramCodes, 1, 1) = 1 Then
                AirPrograms = "   0 - SIP" & vbNewLine
            End If
            If Mid(AirProgramCodes, 2, 1) = 1 Then
                AirPrograms = AirPrograms & "   1 - Federal SIP" & vbNewLine
            End If
            If Mid(AirProgramCodes, 3, 1) = 1 Then
                AirPrograms = AirPrograms & "   3 - Non-Federal SIP" & vbNewLine
            End If
            If Mid(AirProgramCodes, 4, 1) = 1 Then
                AirPrograms = AirPrograms & "   4 - CFC Tracking" & vbNewLine
            End If
            If Mid(AirProgramCodes, 5, 1) = 1 Then
                AirPrograms = AirPrograms & "   6 - PSD" & vbNewLine
            End If
            If Mid(AirProgramCodes, 6, 1) = 1 Then
                AirPrograms = AirPrograms & "   7 - NSR" & vbNewLine
            End If
            If Mid(AirProgramCodes, 7, 1) = 1 Then
                AirPrograms = AirPrograms & "   8 - NESHAP" & vbNewLine
            End If
            If Mid(AirProgramCodes, 8, 1) = 1 Then
                AirPrograms = AirPrograms & "   9 - NSPS" & vbNewLine
            End If
            If Mid(AirProgramCodes, 9, 1) = 1 Then
                AirPrograms = AirPrograms & "   F - FESOP" & vbNewLine
            End If
            If Mid(AirProgramCodes, 10, 1) = 1 Then
                AirPrograms = AirPrograms & "   A - Acid Precipitation" & vbNewLine
            End If
            If Mid(AirProgramCodes, 11, 1) = 1 Then
                AirPrograms = AirPrograms & "   I - Native American" & vbNewLine
            End If
            If Mid(AirProgramCodes, 12, 1) = 1 Then
                AirPrograms = AirPrograms & "   M - MACT" & vbNewLine
            End If
            If Mid(AirProgramCodes, 13, 1) = 1 Then
                AirPrograms = AirPrograms & "   V - Title V Permit" & vbNewLine
            End If
            AirProgramLine = "Air Programs - " & vbNewLine & AirPrograms

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
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "8-hr Atlanta"
                    Else
                        AttainmentStatus = "   8-hr Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "8-hr Macon"
                    Else
                        AttainmentStatus = "   8-hr Macon"
                    End If
            End Select
            Select Case Mid(Attainment, 4, 1)
                Case 1
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Atlanta"
                    Else
                        AttainmentStatus = "   PM 2.5 Atlanta"
                    End If
                Case 2
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5  Chattanooga"
                    Else
                        AttainmentStatus = "   PM 2.5  Chattanooga"
                    End If
                Case 3
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Floyd"
                    Else
                        AttainmentStatus = "   PM 2.5 Floyd"
                    End If
                Case 4
                    If AttainmentStatus <> "" Then
                        AttainmentStatus = AttainmentStatus & vbNewLine & "PM 2.5 Macon"
                    Else
                        AttainmentStatus = "   PM 2.5 Macon"
                    End If
            End Select

            If AttainmentStatus = "" Then
                AttainmentStatus = "Non Attainment Area - N/A"
            Else
                AttainmentStatus = "Non Attainment Area - " & vbNewLine & AttainmentStatus
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
                Case 1
                    If StatePrograms <> "" Then
                        StatePrograms &= "HAPs Major"
                    Else
                        StatePrograms = "   HAPs Major"
                    End If
            End Select
            If StatePrograms = "" Then
                StatePrograms = "State Codes - N/A"
            Else
                StatePrograms = "State Codes - " & vbNewLine & StatePrograms
            End If

            rtbFacilityInformation.Clear()

            rtbFacilityInformation.Text = AirsId.FormattedString & vbNewLine &
                Facilityname & vbNewLine &
                FacilityStreet & vbNewLine &
                FacilityCity & ", GA " & FacilityZipCode & vbNewLine & vbNewLine &
                OperationalStatusLine & vbNewLine &
                ClassificationLine & vbNewLine &
                SICLine & vbNewLine &
                NAICSLine & vbNewLine &
                AirProgramLine &
                StatePrograms & vbNewLine &
            CountyName & " County" & vbNewLine &
                "District - " & District & vbNewLine &
                AttainmentStatus & vbNewLine & vbNewLine &
                PlantLine

            cboCounty.SelectedIndex = cboCounty.FindString(CountyName)
            txtDistrict.Text = District

            txtFacilityName.Text = Facilityname
            txtFacilityStreetAddress.Text = FacilityStreet

            If String.IsNullOrWhiteSpace(FacilityCity) OrElse FacilityCity = "N/A" Then
                cboFacilityCity.Text = ""
            Else
                cboFacilityCity.Text = FacilityCity
            End If

            txtFacilityZipCode.Text = FacilityZipCode
            txtSICCode.Text = SIC
            txtNAICSCode.Text = NAICS

            ' Currently we are only tracking federally-owned facilities. Eventually  
            ' this could be expanded to use a drop-down with all ownership types. 
            chbFederallyOwned.Checked = OwnershipTypeCode = FederallyOwnedTypeCode

            chbNspsFeeExempt.Checked = NspsFeeExempt

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
                Case Else
                    cboClassification.Text = ""
            End Select
            txtPlantDescription.Text = PlantDesc

            chbCDS_0.Checked = True
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadOpenApplications()
        If AirsId Is Nothing Then
            Return
        End If

        Try
            Dim query As String = "select count(*) " &
               "from SSPPApplicationMaster " &
               "where datFinalizedDate Is Null " &
               "and strAirsNumber = @airsnumber"

            Dim parameter As New SqlParameter("@airsnumber", AirsId.DbFormattedString)

            txtOutstandingApplication.Text = DB.GetInteger(query, parameter).ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub CheckForLinkedApplications()
        Dim ApplicationCount As Integer = 0
        Dim query As String
        Dim parameter As New SqlParameter("@appnumber", AppNumber)

        Try

            txtMasterApp.Clear()
            txtMasterAppLock.Clear()
            txtApplicationCount.Text = ""
            lbLinkApplications.Items.Clear()

            If AppNumber > 0 Then
                query = "Select " &
                    "strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @appnumber"

                Dim MasterApplication As String = DB.GetString(query, parameter)

                If String.IsNullOrEmpty(MasterApplication) Then
                    txtMasterApp.Clear()
                    txtMasterAppLock.Clear()
                    txtApplicationCount.Text = ""
                    lbLinkApplications.Items.Clear()
                    lblLinkWarning.Visible = False
                Else
                    txtMasterApp.Text = MasterApplication
                    txtMasterAppLock.Text = MasterApplication

                    query = "Select " &
                        " strApplicationNumber " &
                        "from SSPPApplicationLinking " &
                        "where strMasterApplication = @appnumber " &
                        "order by strApplicationNumber "

                    Dim dt As DataTable = DB.GetDataTable(query, parameter)

                    For Each dr As DataRow In dt.Rows
                        lbLinkApplications.Items.Add(CInt(dr.Item("strApplicationNumber")))
                        ApplicationCount += 1
                    Next

                    txtApplicationCount.Text = ApplicationCount.ToString
                    lblLinkWarning.Visible = True
                End If
            Else
                txtMasterApp.Clear()
                txtMasterAppLock.Clear()
                lbLinkApplications.Items.Clear()
                lblLinkWarning.Visible = False
            End If

            If Not lbLinkApplications.Items.Contains(AppNumber) Then
                lbLinkApplications.Items.Add(AppNumber)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFetchNewAppNumber_Click(sender As Object, e As EventArgs) Handles btnFetchNewAppNumber.Click
        txtNewApplicationNumber.Text = DB.GetInteger("Select next value for SSPPAPPLICATIONKEY").ToString
        AppNumber = 0
    End Sub

    Private Function CreateNewApplication() As Boolean
        Dim queriesList As New List(Of String)
        Dim parametersList As New List(Of SqlParameter())

        queriesList.Add("Insert into SSPPApplicationMaster " &
            "(strApplicationNumber, strAIRSNumber, " &
            "strModifingPerson, datModifingDate) " &
            "values (@appnumber, @airsnumber, @updateuser, GETDATE() ) ")

        parametersList.Add({
            New SqlParameter("@appnumber", AppNumber),
            New SqlParameter("@airsnumber", AirsId?.DbFormattedString),
            New SqlParameter("@updateuser", CurrentUser.UserID)
        })

        queriesList.Add("Insert into SSPPApplicationData " &
            "(strApplicationNumber, strModifingPerson, " &
            "datModifingDate) " &
            "values (@appnumber, @updateuser, GETDATE() ) ")

        parametersList.Add({
            New SqlParameter("@appnumber", AppNumber),
            New SqlParameter("@updateuser", CurrentUser.UserID)
        })

        queriesList.Add("Insert into SSPPApplicationTracking " &
            "(strApplicationNumber, strSubmittalNumber, " &
            " strModifingPerson, datModifingDate) " &
            "values (@appnumber, @submittalnumber, @updateuser, GETDATE() ) ")

        parametersList.Add({
            New SqlParameter("@appnumber", AppNumber),
            New SqlParameter("@submittalnumber", 1),
            New SqlParameter("@updateuser", CurrentUser.UserID)
        })

        Return DB.RunCommand(queriesList, parametersList)
    End Function

    Private Function SaveApplicationData() As Boolean
        Dim StaffResponsible As String = ""
        Dim ApplicationType As String = ""
        Dim PermitType As String = ""
        Dim Unit As String = ""
        Dim DateFinalized As String
        Dim FacilityName As String
        Dim FacilityAddress As String
        Dim FacilityCity As String
        Dim FacilityZipCode As String
        Dim OperationalStatus As String
        Dim Classification As String
        Dim AirProgramCodes As String
        Dim SIC As String
        Dim NAICS As String
        Dim PermitNumber As String
        Dim PlantDesc As String
        Dim Comments As String
        Dim ApplicationNotes As String
        Dim ReceivedDate As Date?
        Dim SentByDate As Date?
        Dim AssignedToEngineer As Date?
        Dim ReAssignedToEngineer As Date?
        Dim PackageCompleteDate As String = Nothing
        Dim AcknowledgementLetter As Date?
        Dim PublicInvolved As String
        Dim ToPMI As Date?
        Dim ToPMII As Date?
        Dim ReturnToEngineer As String
        Dim PermitIssued As Date?
        Dim AppDeadline As Date?
        Dim DraftIssued As Date?
        Dim EPAWaived As Date?
        Dim EPAEnds As Date?
        Dim ToBC As Date?
        Dim ToDO As Date?
        Dim PAReady As String
        Dim PNReady As String
        Dim TrackedRules As String
        Dim StateProgramCodes As String
        Dim AttainmentStatus As String
        Dim SignificantComments As String
        Dim PAExpires As Date?
        Dim PNExpires As Date?
        Dim OwnershipTypeCode As String
        Dim NspsFeeExempt As Boolean

        Dim queriesList As New List(Of String)
        Dim parametersList As New List(Of SqlParameter())

        Try
            If cboEngineer.Text <> "" Then
                StaffResponsible = cboEngineer.SelectedValue.ToString
            End If

            If cboApplicationType.Text <> "" Then
                ApplicationType = cboApplicationType.SelectedValue.ToString
            End If

            If Not String.IsNullOrWhiteSpace(cboPermitAction.Text) Then
                PermitType = cboPermitAction.SelectedValue.ToString
            End If

            If cboApplicationUnit.Text <> "" Then
                Unit = cboApplicationUnit.SelectedValue.ToString
            End If

            If chbClosedOut.Checked Then
                DateFinalized = TodayFormatted
            Else
                DateFinalized = Nothing
            End If

            queriesList.Add("Update SSPPApplicationMaster set " &
                "strAIRSNumber = @airsnumber, " &
                "strStaffResponsible = @staff, " &
                "strApplicationType = @applicationtype, " &
                "strPermitType = @permittype, " &
                "APBUnit = @unit, " &
                "datFinalizedDate = @datefinalized, " &
                "strModifingperson = @updateuser, " &
                "datModifingdate = GETDATE() " &
                "where strApplicationNumber = @appnumber ")

            parametersList.Add({
                New SqlParameter("@airsnumber", AirsId?.DbFormattedString),
                New SqlParameter("@staff", StaffResponsible),
                New SqlParameter("@applicationtype", ApplicationType),
                New SqlParameter("@permittype", PermitType),
                New SqlParameter("@unit", Unit),
                New SqlParameter("@datefinalized", DateFinalized),
                New SqlParameter("@updateuser", CurrentUser.UserID),
                New SqlParameter("@appnumber", AppNumber)
            })

            txtFacilityName.Text = SanitizeFacilityNameForDb(txtFacilityName.Text)
            FacilityName = txtFacilityName.Text
            FacilityAddress = txtFacilityStreetAddress.Text

            If String.IsNullOrWhiteSpace(cboFacilityCity.Text) Then
                FacilityCity = "N/A"
            Else
                FacilityCity = cboFacilityCity.Text
            End If

            FacilityZipCode = txtFacilityZipCode.Text?.Replace("-", "")

            If Not String.IsNullOrEmpty(FacilityZipCode) Then
                FacilityZipCode = FacilityZipCode.Substring(0, Math.Min(FacilityZipCode.Length, 9))
            End If

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

            If chbCDS_0.Checked Then
                AirProgramCodes = "1" & Mid(AirProgramCodes, 2)
            End If
            If chbCDS_6.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 4) & "1" & Mid(AirProgramCodes, 6)
            End If
            If chbCDS_7.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 5) & "1" & Mid(AirProgramCodes, 7)
            End If
            If chbCDS_8.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 6) & "1" & Mid(AirProgramCodes, 8)
            End If
            If chbCDS_9.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 7) & "1" & Mid(AirProgramCodes, 9)
            End If
            If chbCDS_A.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 9) & "1" & Mid(AirProgramCodes, 11)
            End If
            If chbCDS_M.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 11) & "1" & Mid(AirProgramCodes, 13)
            End If
            If chbCDS_V.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 12) & "1" & Mid(AirProgramCodes, 14)
            End If
            If chbCDS_RMP.Checked Then
                AirProgramCodes = Mid(AirProgramCodes, 1, 13) & "1" & Mid(AirProgramCodes, 15)
            End If

            SIC = txtSICCode.Text
            NAICS = txtNAICSCode.Text
            OwnershipTypeCode = If(chbFederallyOwned.Checked, FederallyOwnedTypeCode, Nothing)
            NspsFeeExempt = chbNspsFeeExempt.Checked
            PermitNumber = txtPermitNumber.Text?.Replace("-", "")
            PlantDesc = txtPlantDescription.Text
            Comments = txtComments.Text
            ApplicationNotes = txtReasonAppSubmitted.Text

            StateProgramCodes = "00000"

            If Me.chbNSRMajor.Checked Then
                StateProgramCodes = "10000"
            Else
                StateProgramCodes = "00000"
            End If
            If Me.chbHAPsMajor.Checked Then
                StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "1000"
            Else
                StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "0000"
            End If

            Select Case txt1HourOzone.Text
                Case "No"
                    AttainmentStatus = "00000"
                Case "Yes"
                    AttainmentStatus = "01000"
                Case "Contributing"
                    AttainmentStatus = ""
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

            If chbPSD.Checked Then
                TrackedRules = "1" & Mid(TrackedRules, 2)
            End If
            If chbNAANSR.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 1) & "1" & Mid(TrackedRules, 3)
            End If
            If chb112g.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 2) & "1" & Mid(TrackedRules, 4)
            End If
            If chbRulett.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 3) & "1" & Mid(TrackedRules, 5)
            End If
            If chbRuleyy.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 4) & "1" & Mid(TrackedRules, 6)
            End If
            If chbPal.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 5) & "1" & Mid(TrackedRules, 7)
            End If
            If chbExpFee.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 6) & "1" & Mid(TrackedRules, 8)
            End If
            If chbConfidential.Checked Then
                TrackedRules = Mid(TrackedRules, 1, 7) & "1" & Mid(TrackedRules, 9)
            End If

            PAReady = chbPAReady.Checked.ToString
            PNReady = chbPNReady.Checked.ToString
            SignificantComments = txtSignificantComments.Text

            If Not cboPublicAdvisory.Visible Then
                PublicInvolved = "0"
            Else
                Select Case cboPublicAdvisory.Text
                    Case "PA Needed"
                        PublicInvolved = "1"
                    Case "PA Not Needed"
                        PublicInvolved = "2"
                    Case Else
                        PublicInvolved = "0"
                End Select
            End If

            queriesList.Add("Update SSPPApplicationData set " &
            "strFacilityName = @FacilityName, " &
            "strFacilityStreet1 = @FacilityAddress, " &
            "strFacilityStreet2 = 'N/A', " &
            "strFacilityCity = @FacilityCity, " &
            "strFacilityState = 'GA', " &
            "strFacilityZipCode = @FacilityZipCode, " &
            "strOperationalStatus = @OperationalStatus, " &
            "strClass = @Classification, " &
            "strAirProgramCodes = @AirProgramCodes, " &
            "strSICCode = @SIC, " &
            "FacilityOwnershipTypeCode = @OwnershipTypeCode, " &
            "NspsFeeExempt= @NspsFeeExempt, " &
            "strNAICSCode = @NAICS, " &
            "strPermitNumber = @PermitNumber, " &
            "strPlantDescription = @PlantDesc, " &
            "strComments = @Comments, " &
            "strApplicationNotes = @ApplicationNotes, " &
            "strTrackedRules = @TrackedRules, " &
            "strStateProgramCodes = @StateProgramCodes, " &
            "strPAReady = @PAReady, " &
            "strPNReady = @PNReady, " &
            "STRSIGNIFICANTCOMMENTS = @SignificantComments, " &
            "strPublicInvolvement = @PublicInvolved, " &
            "strModifingperson = @UserGCode, " &
            "datModifingdate = GETDATE() " &
            "where strApplicationNumber = @txtApplicationNumber ")

            parametersList.Add({
                New SqlParameter("@FacilityName", FacilityName),
                New SqlParameter("@FacilityAddress", FacilityAddress),
                New SqlParameter("@FacilityCity", FacilityCity),
                New SqlParameter("@FacilityZipCode", FacilityZipCode),
                New SqlParameter("@OperationalStatus", OperationalStatus),
                New SqlParameter("@Classification", Classification),
                New SqlParameter("@AirProgramCodes", AirProgramCodes),
                New SqlParameter("@SIC", RealStringOrNothing(SIC)),
                New SqlParameter("@NAICS", RealStringOrNothing(NAICS)),
                New SqlParameter("@OwnershipTypeCode", OwnershipTypeCode),
                New SqlParameter("@NspsFeeExempt", NspsFeeExempt),
                New SqlParameter("@PermitNumber", PermitNumber),
                New SqlParameter("@PlantDesc", PlantDesc),
                New SqlParameter("@Comments", Comments),
                New SqlParameter("@ApplicationNotes", ApplicationNotes),
                New SqlParameter("@TrackedRules", TrackedRules),
                New SqlParameter("@StateProgramCodes", StateProgramCodes),
                New SqlParameter("@PAReady", PAReady),
                New SqlParameter("@PNReady", PNReady),
                New SqlParameter("@SignificantComments", SignificantComments),
                New SqlParameter("@PublicInvolved", PublicInvolved),
                New SqlParameter("@UserGCode", CurrentUser.UserID),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            ReceivedDate = DTPDateReceived.Value
            SentByDate = DTPDateSent.Value

            If DTPDateAssigned.Checked Then
                AssignedToEngineer = DTPDateAssigned.Value
            End If

            If DTPDateReassigned.Checked Then
                ReAssignedToEngineer = DTPDateReassigned.Value
            End If

            If DTPDateAcknowledge.Checked Then
                AcknowledgementLetter = DTPDateAcknowledge.Value
            End If

            If DTPDateToUC.Checked Then
                ToPMI = DTPDateToUC.Value
            End If

            If DTPDateToPM.Checked Then
                ToPMII = DTPDateToPM.Value
            End If

            ReturnToEngineer = Nothing

            If DTPFinalAction.Checked Then
                PermitIssued = DTPFinalAction.Value
            End If

            If DTPDeadline.Checked Then
                AppDeadline = DTPDeadline.Value
            End If

            If DTPDraftIssued.Checked Then
                DraftIssued = DTPDraftIssued.Value
            End If

            If DTPEPAWaived.Checked Then
                EPAWaived = DTPEPAWaived.Value
            End If

            If DTPEPAEnds.Checked Then
                EPAEnds = DTPEPAEnds.Value
            End If

            If DTPDateToBC.Checked Then
                ToBC = DTPDateToBC.Value
            End If

            If DTPDateToDO.Checked Then
                ToDO = DTPDateToDO.Value
            End If

            If DTPDatePAExpires.Checked Then
                PAExpires = DTPDatePAExpires.Value
            End If

            If DTPDatePNExpires.Checked Then
                PNExpires = DTPDatePNExpires.Value
            End If

            queriesList.Add("Update SSPPApplicationTracking set " &
            "datReceivedDate = @ReceivedDate, " &
            "datSentByFacility = @SentByDate, " &
            "datAssignedToEngineer = @AssignedToEngineer, " &
            "datReassignedToEngineer = @ReAssignedToEngineer, " &
            "datApplicationPackageComplete = @PackageCompleteDate, " &
            "datAcknowledgementLetterSent = @AcknowledgementLetter, " &
            "datToPMI = @ToPMI, " &
            "datToPMII = @ToPMII, " &
            "datReturnedtoEngineer = @ReturnToEngineer, " &
            "datPermitIssued = @PermitIssued, " &
            "datApplicationDeadline = @AppDeadline, " &
            "datDraftIssued = @DraftIssued, " &
            "strModifingPerson = @UserGCode, " &
            "datModifingDate = GETDATE() , " &
            "datEPAWaived = @EPAWaived, " &
            "datEPAEnds = @EPAEnds, " &
            "datToBranchCheif = @ToBC, " &
            "datToDirector = @ToDO, " &
            "datPAExpires = @PAExpires, " &
            "datpnexpires = @PNExpires " &
            "where strApplicationNumber = @txtApplicationNumber ")

            parametersList.Add({
                New SqlParameter("@ReceivedDate", ReceivedDate),
                New SqlParameter("@SentByDate", SentByDate),
                New SqlParameter("@AssignedToEngineer", AssignedToEngineer),
                New SqlParameter("@ReAssignedToEngineer", ReAssignedToEngineer),
                New SqlParameter("@PackageCompleteDate", PackageCompleteDate),
                New SqlParameter("@AcknowledgementLetter", AcknowledgementLetter),
                New SqlParameter("@ToPMI", ToPMI),
                New SqlParameter("@ToPMII", ToPMII),
                New SqlParameter("@ReturnToEngineer", ReturnToEngineer),
                New SqlParameter("@PermitIssued", PermitIssued),
                New SqlParameter("@AppDeadline", AppDeadline),
                New SqlParameter("@DraftIssued", DraftIssued),
                New SqlParameter("@UserGCode", CurrentUser.UserID),
                New SqlParameter("@EPAWaived", EPAWaived),
                New SqlParameter("@EPAEnds", EPAEnds),
                New SqlParameter("@ToBC", ToBC),
                New SqlParameter("@ToDO", ToDO),
                New SqlParameter("@PAExpires", PAExpires),
                New SqlParameter("@PNExpires", PNExpires),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            If lblLinkWarning.Visible AndAlso lbLinkApplications.Items.Count > 0 Then
                Dim LinkedApplication As Integer
                Dim i As Integer

                For i = 0 To lbLinkApplications.Items.Count - 1
                    If CInt(lbLinkApplications.Items.Item(i)) <> AppNumber Then
                        LinkedApplication = CInt(lbLinkApplications.Items.Item(i))

                        queriesList.Add("Update SSPPApplicationMaster set " &
                            "datFinalizedDate = @DateFinalized, " &
                            "strPermitType = @PermitType, " &
                            "strModifingperson = @UserGCode, " &
                            "datModifingdate = GETDATE() " &
                            "where strApplicationNumber = @LinkedApplication ")

                        parametersList.Add({
                            New SqlParameter("@DateFinalized", DateFinalized),
                            New SqlParameter("@PermitType", PermitType),
                            New SqlParameter("@UserGCode", CurrentUser.UserID),
                            New SqlParameter("@LinkedApplication", LinkedApplication)
                        })

                        queriesList.Add("Update SSPPApplicationData set " &
                            "strOperationalStatus = @OperationalStatus, " &
                            "strClass = @Classification , " &
                            "strAirProgramCodes = @AirProgramCodes , " &
                            "strSICCode = @SIC , " &
                            "strPermitNumber = @PermitNumber, " &
                            "strPlantDescription = @PlantDesc, " &
                            "strStateProgramCodes = @StateProgramCodes , " &
                            "strPAReady = @PAReady , " &
                            "strPNReady = @PNReady , " &
                            "strSignificantComments = @SignificantComments, " &
                            "strPublicInvolvement = @PublicInvolved, " &
                            "strModifingperson = @UserGCode , " &
                            "datModifingdate = GETDATE() " &
                            "where strApplicationNumber = @LinkedApplication ")

                        parametersList.Add({
                            New SqlParameter("@OperationalStatus", OperationalStatus),
                            New SqlParameter("@Classification", Classification),
                            New SqlParameter("@AirProgramCodes", AirProgramCodes),
                            New SqlParameter("@SIC", SIC),
                            New SqlParameter("@PermitNumber", PermitNumber),
                            New SqlParameter("@PlantDesc", PlantDesc),
                            New SqlParameter("@StateProgramCodes", StateProgramCodes),
                            New SqlParameter("@PAReady", PAReady),
                            New SqlParameter("@PNReady", PNReady),
                            New SqlParameter("@SignificantComments", SignificantComments),
                            New SqlParameter("@PublicInvolved", PublicInvolved),
                            New SqlParameter("@UserGCode", CurrentUser.UserID),
                            New SqlParameter("@LinkedApplication", LinkedApplication)
                        })

                        queriesList.Add("Update SSPPApplicationTracking set " &
                            "datPermitIssued = @PermitIssued , " &
                            "datDraftIssued = @DraftIssued , " &
                            "datEPAWaived = @EPAWaived , " &
                            "datEPAEnds = @EPAEnds , " &
                            "datPAExpires  = @PAExpires , " &
                            "datPNExpires = @PNExpires , " &
                            "datToBranchCheif = @ToBC , " &
                            "datToDirector = @ToDO , " &
                            "strModifingPerson = @UserGCode , " &
                            "datModifingDate = GETDATE() " &
                            "where strApplicationNumber = @LinkedApplication ")

                        parametersList.Add({
                            New SqlParameter("@PermitIssued", PermitIssued),
                            New SqlParameter("@DraftIssued", DraftIssued),
                            New SqlParameter("@EPAWaived", EPAWaived),
                            New SqlParameter("@EPAEnds", EPAEnds),
                            New SqlParameter("@PAExpires", PAExpires),
                            New SqlParameter("@PNExpires", PNExpires),
                            New SqlParameter("@ToBC", ToBC),
                            New SqlParameter("@ToDO", ToDO),
                            New SqlParameter("@UserGCode", CurrentUser.UserID),
                            New SqlParameter("@LinkedApplication", LinkedApplication)
                        })

                        Dim query As String = "Select " &
                            "datToPMI, datToPMII " &
                            "from SSPPApplicationTracking " &
                            "where strApplicationNumber = @LinkedApplication "

                        Dim param As New SqlParameter("@LinkedApplication", LinkedApplication)

                        Dim dr As DataRow = DB.GetDataRow(query, param)

                        If dr IsNot Nothing Then
                            Dim query2 As String = ""

                            If IsDBNull(dr.Item("datToPMI")) Then
                                query2 = "Update SSPPApplicationTracking set datToPMI = @ToPMI "

                                If IsDBNull(dr.Item("datToPMII")) Then
                                    query2 &= ", datToPMII = @ToPMII "
                                End If

                                query2 &= " where strApplicationNumber = @LinkedApplication  "
                            ElseIf IsDBNull(dr.Item("datToPMII")) Then
                                query2 = "Update SSPPApplicationTracking set " &
                                    "datToPMII = @ToPMII where strApplicationNumber = @LinkedApplication "
                            End If

                            If Not String.IsNullOrWhiteSpace(query2) Then
                                queriesList.Add(query2)

                                parametersList.Add({
                                    New SqlParameter("@ToPMI", ToPMI),
                                    New SqlParameter("@ToPMII", ToPMII),
                                    New SqlParameter("@LinkedApplication", LinkedApplication)
                                })
                            End If
                        End If
                    End If
                Next
            End If

            Return DB.RunCommand(queriesList, parametersList)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub PermitRevocationQuery()
        ' Check for existing permits first
        Dim activePermits As List(Of Permit) = GetActivePermitsAsList(AirsId)

        If activePermits IsNot Nothing AndAlso activePermits.Count > 0 Then
            activePermits.RemoveAll(Function(permit As Permit) permit.Equals(New Permit(txtPermitNumber.Text)))
        End If

        If activePermits IsNot Nothing AndAlso activePermits.Count > 0 Then
            Using permitRevocationDialog As New SsppPermitRevocationDialog With {
                .ActivePermits = activePermits ' Send list of existing permits to dialog
            }
                permitRevocationDialog.ShowDialog()

                Dim revokedPermits As List(Of Permit) = permitRevocationDialog.PermitsToRevoke

                If revokedPermits IsNot Nothing AndAlso revokedPermits.Count > 0 Then
                    Dim result As Boolean = RevokePermits(revokedPermits, DTPFinalAction.Value)
                    If Not result Then
                        MessageBox.Show("There was an error revoking permits." & vbNewLine &
                                        "Please contact the Data Management Unit.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End Using
        End If
    End Sub

    Private Sub SaveIssuedPermit()
        Dim result As Boolean
        Dim permit As Permit

        If Not PermitExists(txtPermitNumber.Text) Then
            permit = New Permit(AirsId, txtPermitNumber.Text,
                                         DTPFinalAction.Value, True, cboApplicationType.SelectedValue.ToString)
            result = AddPermit(permit)
        Else
            permit = GetPermit(txtPermitNumber.Text)
            permit.IssuedDate = DTPFinalAction.Value
            permit.PermitTypeCode = cboApplicationType.SelectedValue.ToString
            result = UpdatePermit(permit)
        End If

        If Not result Then
            MessageBox.Show("There was an error saving the permit." & vbNewLine &
                            "Please contact the Data Management Unit.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub SaveInformationRequest()
        Dim InformationRequestKey As Integer
        Dim InformationRequested As String
        Dim InformationReceived As String
        Dim DateInfoRequested As DateTime?
        Dim DateInfoReceived As DateTime?
        Dim query As String
        Dim parameter As SqlParameter()

        Try

            If ApplicationExists(AppNumber) Then
                If txtInformationRequestedKey.Text = "" Then
                    query = "SELECT ISNULL(MAX(strRequestKey), 0) + 1 " &
                    "from SSPPApplicationInformation " &
                    "where strApplicationNumber = @txtApplicationNumber"

                    parameter = {New SqlParameter("@txtApplicationNumber", AppNumber)}

                    InformationRequestKey = DB.GetInteger(query, parameter)
                Else
                    InformationRequestKey = CInt(txtInformationRequestedKey.Text)
                End If

                InformationRequested = Mid(txtInformationRequested.Text, 1, 4000)

                InformationReceived = Mid(txtInformationReceived.Text, 1, 4000)

                If DTPInformationRequested.Checked Then
                    DateInfoRequested = DTPInformationRequested.Value
                Else
                    DateInfoRequested = Nothing
                End If

                If DTPInformationReceived.Checked Then
                    DateInfoReceived = DTPInformationReceived.Value
                Else
                    DateInfoReceived = Nothing
                End If

                query = "Select strApplicationNumber " &
                "from SSPPApplicationInformation " &
                "where strApplicationNumber = @txtApplicationNumber " &
                "and strRequestKey = @InformationRequestKey "

                parameter = {
                    New SqlParameter("@txtApplicationNumber", AppNumber),
                    New SqlParameter("@InformationRequestKey", InformationRequestKey)
                }

                If DB.ValueExists(query, parameter) Then
                    'Update
                    query = "Update SSPPApplicationInformation set " &
                    "datInformationRequested = @DateInfoRequested , " &
                    "strInformationRequested = @InformationRequested , " &
                    "datInformationReceived = @DateInfoReceived , " &
                    "strInformationReceived = @InformationReceived , " &
                    "strModifingPerson = @UserGCode , " &
                    "datModifingDate = GETDATE() " &
                    "where strApplicationNumber = @txtApplicationNumber " &
                    "and strRequestKey = @InformationRequestKey  "

                    parameter = {
                        New SqlParameter("@DateInfoRequested", DateInfoRequested),
                        New SqlParameter("@InformationRequested", InformationRequested),
                        New SqlParameter("@DateInfoReceived", DateInfoReceived),
                        New SqlParameter("@InformationReceived", InformationReceived),
                        New SqlParameter("@UserGCode", CurrentUser.UserID),
                        New SqlParameter("@txtApplicationNumber", AppNumber),
                        New SqlParameter("@InformationRequestKey", InformationRequestKey)
                    }
                Else
                    'Insert 
                    query = "Insert into SSPPApplicationInformation " &
                    "(strApplicationNumber, strRequestKey, " &
                    "datInformationRequested, strInformationRequested, " &
                    "datInformationReceived, strInformationReceived, " &
                    "strModifingPerson, datModifingDate) " &
                    "values " &
                    "(@txtApplicationNumber, @InformationRequestKey , " &
                    "@DateInfoRequested , @InformationRequested , " &
                    "@DateInfoReceived , @InformationReceived , " &
                    "@UserGCode , GETDATE() ) "

                    parameter = {
                        New SqlParameter("@txtApplicationNumber", AppNumber),
                        New SqlParameter("@InformationRequestKey", InformationRequestKey),
                        New SqlParameter("@DateInfoRequested", DateInfoRequested),
                        New SqlParameter("@InformationRequested", InformationRequested),
                        New SqlParameter("@DateInfoReceived", DateInfoReceived),
                        New SqlParameter("@InformationReceived", InformationReceived),
                        New SqlParameter("@UserGCode", CurrentUser.UserID)
                    }
                End If

                DB.RunCommand(query, parameter)

                LoadInformationRequestHistory()

            Else
                MsgBox("The application must be saved before an information request can be made.", MsgBoxStyle.Information, "Permit Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteInformationRequest()
        Dim InformationRequestKey As Integer

        Try

            If txtInformationRequestedKey.Text <> "" Then
                InformationRequestKey = CInt(txtInformationRequestedKey.Text)

                Dim query As String = "Delete from SSPPApplicationInformation " &
                "where strApplicationNumber = @txtApplicationNumber " &
                "and strRequestKey = @InformationRequestKey "

                Dim parameter As SqlParameter() = {
                    New SqlParameter("@txtApplicationNumber", AppNumber),
                    New SqlParameter("@InformationRequestKey", InformationRequestKey)
                }

                DB.RunCommand(query, parameter)

                txtInformationRequestedKey.Clear()
                DTPInformationRequested.Value = Today
                txtInformationRequested.Clear()
                DTPInformationReceived.Value = Today
                txtInformationReceived.Clear()
                LoadInformationRequestHistory()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveApplicationSubmitForReview()

        Dim DateReviewSubmitted As Date? = Nothing

        If DTPReviewSubmitted.Checked Then
            DateReviewSubmitted = DTPReviewSubmitted.Value
        End If

        Dim SscpReviewUnit As String = If(DTPReviewSubmitted.Checked, cboSSCPUnits.SelectedValue.ToString, Nothing)
        Dim IsmpReviewUnit As String = If(DTPReviewSubmitted.Checked, cboISMPUnits.SelectedValue.ToString, Nothing)

        Dim queryList As New List(Of String)
        Dim paramList As New List(Of SqlParameter())

        queryList.Add("Update SSPPApplicationData set " &
                      "strSSCPUnit = @cboSSCPUnits, " &
                      "strISMPUnit = @cboISMPUnits " &
                      "where strApplicationNumber = @txtApplicationNumber ")

        paramList.Add({
                      New SqlParameter("@cboSSCPUnits", SscpReviewUnit),
                      New SqlParameter("@cboISMPUnits", IsmpReviewUnit),
                      New SqlParameter("@txtApplicationNumber", AppNumber)
                      })

        queryList.Add("Update SSPPApplicationTracking set " &
                      "datReviewSubmitted = @DateReviewSubmitted " &
                      "where strApplicationNumber = @txtApplicationNumber ")

        paramList.Add({
                      New SqlParameter("@DateReviewSubmitted", DateReviewSubmitted),
                      New SqlParameter("@txtApplicationNumber", AppNumber)
                      })

        DB.RunCommand(queryList, paramList)
    End Sub

    Private Sub SaveSSCPReview()
        Try
            Dim SSCPComments As String

            If rdbSSCPNo.Checked Then
                SSCPComments = "N/A"
            Else
                SSCPComments = txtSSCPComments.Text
            End If

            Dim queryList As New List(Of String)
            Dim paramList As New List(Of SqlParameter())

            queryList.Add("Update SSPPApplicationData set " &
                "strSSCPReviewer = @cboSSCPStaff, " &
                "strSSCPComments = @SSCPComments " &
                "where strApplicationNumber = @txtApplicationNumber")

            paramList.Add({
                New SqlParameter("@cboSSCPStaff", cboSSCPStaff.SelectedValue),
                New SqlParameter("@SSCPComments", SSCPComments),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            queryList.Add("Update SSPPApplicationTracking set " &
                "datSSCPReviewDate = @DTPSSCPReview " &
                "where strApplicationNumber = @txtApplicationNumber ")

            paramList.Add({
                New SqlParameter("@DTPSSCPReview", DTPSSCPReview.Value),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            DB.RunCommand(queryList, paramList)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveISMPReview()
        Try
            Dim ISMPComments As String

            Dim queryList As New List(Of String)
            Dim paramList As New List(Of SqlParameter())

            If rdbISMPNo.Checked Then
                ISMPComments = "N/A"
            Else
                ISMPComments = txtISMPComments.Text
            End If

            queryList.Add("Update SSPPApplicationData set " &
                "strISMPReviewer = @cboISMPStaff , " &
                "strISMPComments = @ISMPComments " &
                "where strApplicationNumber = @txtApplicationNumber")

            paramList.Add({
                New SqlParameter("@cboISMPStaff", cboISMPStaff.SelectedValue),
                New SqlParameter("@ISMPComments", ISMPComments),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            queryList.Add("Update SSPPApplicationTracking set " &
                "datISMPReviewDate = @DTPISMPReview " &
                "where strApplicationNumber = @txtApplicationNumber")

            paramList.Add({
                New SqlParameter("@DTPISMPReview", DTPISMPReview.Value),
                New SqlParameter("@txtApplicationNumber", AppNumber)
            })

            DB.RunCommand(queryList, paramList)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveApplicationContact()
        If String.IsNullOrEmpty(txtContactFirstName.Text) OrElse String.IsNullOrEmpty(txtContactLastName.Text) Then
            MessageBox.Show("Contact not saved because name is missing.")
            Return
        End If

        Try
            Dim ContactFirstName As String = " "
            Dim ContactLastname As String = " "
            Dim ContactPrefix As String = " "
            Dim ContactSuffix As String = " "
            Dim ContactTitle As String = " "
            Dim ContactCompany As String = " "
            Dim ContactPhone As String = "0000000000"
            Dim ContactFax As String = "0000000000"
            Dim ContactEmail As String = " "
            Dim ContactAddress As String = " "
            Dim ContactCity As String = " "
            Dim ContactState As String = " "
            Dim ContactZipCode As String = "00000"
            Dim ContactDescription As String = " "
            Dim query As String
            Dim params As SqlParameter()

            If txtContactFirstName.Text <> "" Then
                ContactFirstName = txtContactFirstName.Text
            End If
            If txtContactLastName.Text <> "" Then
                ContactLastname = txtContactLastName.Text
            End If
            If txtContactSocialTitle.Text <> "" Then
                ContactPrefix = txtContactSocialTitle.Text
            End If
            If txtContactPedigree.Text <> "" Then
                ContactSuffix = txtContactPedigree.Text
            End If
            If txtContactTitle.Text <> "" Then
                ContactTitle = txtContactTitle.Text
            End If
            If txtContactCompanyName.Text <> "" Then
                ContactCompany = txtContactCompanyName.Text
            End If
            If txtContactPhoneNumber.Text <> "" Then
                ContactPhone = txtContactPhoneNumber.Text
            End If
            If mtbContactFaxNumber.Text <> "" Then
                ContactFax = mtbContactFaxNumber.Text
            End If
            If txtContactEmailAddress.Text <> "" Then
                ContactEmail = txtContactEmailAddress.Text
            End If
            If txtContactStreetAddress.Text <> "" Then
                ContactAddress = txtContactStreetAddress.Text
            End If
            If txtContactCity.Text <> "" Then
                ContactCity = txtContactCity.Text
            End If
            If txtContactState.Text <> "" Then
                ContactState = txtContactState.Text
            End If
            If mtbContactZipCode.Text <> "" Then
                ContactZipCode = mtbContactZipCode.Text
            End If

            If txtContactDescription.Text <> "" Then
                ContactDescription = txtContactDescription.Text
            Else
                ContactDescription = "From App #- " & AppNumber
            End If

            query = "select convert(bit, count(*)) from SSPPAPPLICATIONCONTACT where STRAPPLICATIONNUMBER = @appNumber "

            params = {
                New SqlParameter("@appNumber", AppNumber)
            }

            If DB.GetBoolean(query, params) Then
                'update
                query = "Update SSPPApplicationContact set " &
                "strContactFirstName = @ContactFirstName, " &
                "strContactLastName = @ContactLastname, " &
                "strContactPrefix = @ContactPrefix, " &
                "strContactSuffix = @ContactSuffix, " &
                "strContactTitle = @ContactTitle, " &
                "strContactCompanyName = @ContactCompany, " &
                "strContactPhoneNumber1 = @ContactPhone, " &
                "strContactfaxnumber = @ContactFax, " &
                "strContactemail = @ContactEmail, " &
                "strContactAddress1 = @ContactAddress, " &
                "strContactCity = @ContactCity, " &
                "strContactState = @ContactState, " &
                "strContactZipCode = @ContactZipCode, " &
                "strContactDescription = @ContactDescription " &
                "where strApplicationNumber = @txtApplicationNumber "

                params = {
                    New SqlParameter("@ContactFirstName", ContactFirstName),
                    New SqlParameter("@ContactLastname", ContactLastname),
                    New SqlParameter("@ContactPrefix", ContactPrefix),
                    New SqlParameter("@ContactSuffix", ContactSuffix),
                    New SqlParameter("@ContactTitle", ContactTitle),
                    New SqlParameter("@ContactCompany", ContactCompany),
                    New SqlParameter("@ContactPhone", ContactPhone),
                    New SqlParameter("@ContactFax", ContactFax?.Replace("(", "")?.Replace(")", "")?.Replace("-", "")?.Replace(" ", "")),
                    New SqlParameter("@ContactEmail", ContactEmail),
                    New SqlParameter("@ContactAddress", ContactAddress),
                    New SqlParameter("@ContactCity", ContactCity),
                    New SqlParameter("@ContactState", ContactState),
                    New SqlParameter("@ContactZipCode", ContactZipCode?.Replace("-", "")),
                    New SqlParameter("@ContactDescription", ContactDescription),
                    New SqlParameter("@txtApplicationNumber", AppNumber)
                }
            Else
                'insert 
                query = "Insert into SSPPApplicationContact " &
                "values " &
                "(@txtApplicationNumber, " &
                "@ContactFirstName, " &
                "@ContactLastname, " &
                "@ContactPrefix, " &
                "@ContactSuffix, " &
                "@ContactTitle, " &
                "@ContactCompany, " &
                "@ContactPhone, " &
                "@ContactFax, " &
                "@ContactEmail, " &
                "@ContactAddress, " &
                "@ContactCity, " &
                "@ContactState, " &
                "@ContactZipCode, " &
                "@ContactDescription) "

                params = {
                    New SqlParameter("@txtApplicationNumber", AppNumber),
                    New SqlParameter("@ContactFirstName", ContactFirstName),
                    New SqlParameter("@ContactLastname", ContactLastname),
                    New SqlParameter("@ContactPrefix", ContactPrefix),
                    New SqlParameter("@ContactSuffix", ContactSuffix),
                    New SqlParameter("@ContactTitle", ContactTitle),
                    New SqlParameter("@ContactCompany", ContactCompany),
                    New SqlParameter("@ContactPhone", ContactPhone),
                    New SqlParameter("@ContactFax", ContactFax?.Replace("(", "")?.Replace(")", "")?.Replace("-", "")?.Replace(" ", "")),
                    New SqlParameter("@ContactEmail", ContactEmail),
                    New SqlParameter("@ContactAddress", ContactAddress),
                    New SqlParameter("@ContactCity", ContactCity),
                    New SqlParameter("@ContactState", ContactState),
                    New SqlParameter("@ContactZipCode", ContactZipCode.Replace("-", "")),
                    New SqlParameter("@ContactDescription", ContactDescription)
                }
            End If

            DB.RunCommand(query, params)

            If chbClosedOut.Checked AndAlso AirsId IsNot Nothing Then

                query = "select strKey " &
                    "from APBContactInformation inner join SSPPApplicationContact  " &
                    "on APBContactInformation.strContactFirstName = SSPPApplicationContact.strContactFirstName " &
                    "and APBContactInformation.strContactLastName = SSPPApplicationContact.strContactLastName  " &
                    "and APBContactInformation.strContactPrefix = SSPPApplicationContact.strContactPrefix " &
                    "and APBContactInformation.strContactSuffix = SSPPApplicationContact.strContactSuffix  " &
                    "and APBContactInformation.strContactTitle = SSPPApplicationContact.strContactTitle  " &
                    "and APBContactInformation.strContactCompanyName = SSPPApplicationContact.strContactCompanyName  " &
                    "and APBContactInformation.strContactPhoneNumber1 = SSPPApplicationContact.strContactPhoneNumber1  " &
                    "and APBContactInformation.strContactFaxNumber = SSPPApplicationContact.strContactFaxNumber  " &
                    "and APBContactInformation.strContactEmail = SSPPApplicationContact.strContactEmail  " &
                    "and APBContactInformation.strCOntactAddress1 = SSPPApplicationContact.strContactAddress1  " &
                    "and APBContactInformation.strCOntactCity = SSPPApplicationContact.strContactCity  " &
                    "and APBContactInformation.strContactZipCode = SSPPApplicationcontact.strContactZipCode  " &
                    "and APBContactInformation.strContactDescription = SSPPApplicationContact.strContactDescription  " &
                    "where APBContactInformation.strContactKey = @pKey " &
                    "and SSPPApplicationContact.strApplicationNumber = @app "

                params = {
                    New SqlParameter("@pKey", AirsId.DbFormattedString & "30"),
                    New SqlParameter("@app", AppNumber)
                }

                If Not DB.ValueExists(query, params) Then

                    Dim spName As String = "iaip_facility.SaveApbContact"

                    Dim p As SqlParameter() = {
                        New SqlParameter("@key", "30"),
                        New SqlParameter("@facilityId", AirsId.DbFormattedString),
                        New SqlParameter("@firstName", Trim(ContactFirstName)),
                        New SqlParameter("@lastName", Trim(ContactLastname)),
                        New SqlParameter("@prefix", Trim(ContactPrefix)),
                        New SqlParameter("@suffix", Trim(ContactSuffix)),
                        New SqlParameter("@title", Trim(ContactTitle)),
                        New SqlParameter("@organization", Trim(ContactCompany)),
                        New SqlParameter("@telephone", Trim(ContactPhone)),
                        New SqlParameter("@telephone2", Nothing),
                        New SqlParameter("@fax", Trim(ContactFax?.Replace("(", "")?.Replace(")", "")?.Replace("-", "")?.Replace(" ", ""))),
                        New SqlParameter("@email", Trim(ContactEmail)),
                        New SqlParameter("@address1", Trim(ContactAddress)),
                        New SqlParameter("@address2", Nothing),
                        New SqlParameter("@city", Trim(ContactCity)),
                        New SqlParameter("@state", Trim(ContactState)),
                        New SqlParameter("@postalCode", Trim(ContactZipCode.Replace("-", ""))),
                        New SqlParameter("@userId", CurrentUser.UserID),
                        New SqlParameter("@description", Trim(ContactDescription))
                    }

                    DB.SPRunCommand(spName, p)

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub CloseOutApplication(Status As Boolean)
        Try

            If Status Then
                btnRefreshAIRSNo.Visible = False
                txtFacilityName.ReadOnly = True
                txtFacilityStreetAddress.ReadOnly = True
                cboFacilityCity.Enabled = False
                cboCounty.Enabled = False
                txtSICCode.ReadOnly = True
                txtNAICSCode.ReadOnly = True
                chbFederallyOwned.Enabled = False
                chbNspsFeeExempt.Enabled = False
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
                DTPDateReassigned.Enabled = False
                DTPDateAcknowledge.Enabled = False
                DTPDatePAExpires.Enabled = False
                DTPDatePNExpires.Enabled = False
                DTPDeadline.Enabled = False
                DTPDateToUC.Enabled = False
                DTPDateToPM.Enabled = False
                DTPFinalAction.Enabled = False
                DTPDraftIssued.Enabled = False
                txtPermitNumber.ReadOnly = True
                cboPermitAction.Enabled = False
                cboPublicAdvisory.Enabled = False
                chbPAReady.Enabled = False
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
                chbExpFee.Enabled = False
                chbConfidential.Enabled = False
                txtSignificantComments.ReadOnly = True

                'Facility Application History 
                btnAddApplicationToList.Enabled = False
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
            Else
                LoadPermissions()
                CheckPASettings()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub CheckPASettings()
        If Not String.IsNullOrEmpty(lblPAReady.Text) Then chbPAReady.Enabled = False
        If chbPAReady.Checked Then cboPublicAdvisory.Enabled = False
    End Sub

    Private Sub LinkApplications()
        If AppNumber = 0 Then
            Return
        End If

        Dim MasterAppType As String = ""
        Dim i As Integer
        Dim query As String
        Dim params As SqlParameter()
        Dim appType As String

        Try

            If lbLinkApplications.Items.Count > 1 Then

                For i = 0 To lbLinkApplications.Items.Count - 1
                    If CInt(lbLinkApplications.Items.Item(i)) <> AppNumber Then
                        query = "select " &
                        "strApplicationType " &
                        "from SSPPApplicationMaster " &
                        "where strApplicationnumber = @item "

                        params = {New SqlParameter("@item", CInt(lbLinkApplications.Items.Item(i)))}

                        appType = DB.GetString(query, params)

                        Select Case appType
                            Case "22"
                                MasterApp = lbLinkApplications.Items.Item(i).ToString
                                MasterAppType = appType
                            Case Is = "16", Is = "17", Is = "21", Is = "2"
                                If MasterAppType <> "22" OrElse MasterAppType <> "16" OrElse MasterAppType <> "17" OrElse MasterAppType <> "21" OrElse MasterAppType <> "2" Then
                                    MasterApp = lbLinkApplications.Items.Item(i).ToString
                                    MasterAppType = appType
                                End If
                            Case Is = "20", Is = "15", Is = "9", Is = "11", Is = "12"
                                If MasterAppType <> "22" OrElse MasterAppType <> "16" OrElse MasterAppType <> "17" OrElse MasterAppType <> "21" OrElse MasterAppType <> "2" OrElse
                                    MasterAppType <> "20" OrElse MasterAppType <> "15" OrElse MasterAppType <> "9" OrElse MasterAppType <> "11" OrElse MasterAppType <> "12" Then
                                    MasterApp = lbLinkApplications.Items.Item(i).ToString
                                    MasterAppType = appType
                                End If
                            Case Else
                        End Select
                    End If
                Next

                For i = 0 To lbLinkApplications.Items.Count - 1
                    query = "Select strApplicationNumber " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @appnumber "

                    params = {New SqlParameter("@appnumber", CInt(lbLinkApplications.Items.Item(i)))}

                    If DB.ValueExists(query, params) Then
                        query = "Update SSPPApplicationLinking set " &
                        "strMasterApplication = @MasterApp " &
                        "where strApplicationnumber = @appItem "
                    Else
                        query = "Insert into SSPPApplicationLinking " &
                        "(STRMASTERAPPLICATION, STRAPPLICATIONNUMBER) " &
                        "values " &
                        "(@MasterApp, @appItem) "
                    End If

                    params = {
                        New SqlParameter("@MasterApp", MasterApp),
                        New SqlParameter("@appItem", CInt(lbLinkApplications.Items.Item(i)))
                    }

                    DB.RunCommand(query, params)
                Next

                lblLinkWarning.Visible = True

                MsgBox("Applications Linked", MsgBoxStyle.Information, "Application Tracking Log")
            Else
                MsgBox("A minimum of two applications are needed in order to link an application.", MsgBoxStyle.Information, "Application Tracking Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, AirsId?.FormattedString, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearApplicationLinks()
        If AppNumber = 0 Then
            Return
        End If

        Dim MasterLink As String
        Dim query As String
        Dim param As SqlParameter

        Try

            If txtMasterApp.Text <> "" Then
                query = "Select strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = @pMaster"
                param = New SqlParameter("@pMaster", txtMasterApp.Text)
                MasterLink = DB.GetString(query, param)

                If MasterLink <> "" Then
                    query = "Delete SSPPApplicationLinking " &
                    "where strMasterApplication = @pMaster"
                    param = New SqlParameter("@pMaster", MasterLink)
                    DB.RunCommand(query, param)

                    txtMasterApp.Clear()
                    txtMasterAppLock.Clear()
                    txtApplicationCount.Text = ""
                    lbLinkApplications.Items.Clear()
                    MsgBox("Applications Unlinked", MsgBoxStyle.Information, "Application Tracking Log")
                End If
            End If

            If Not lbLinkApplications.Items.Contains(AppNumber) Then
                lbLinkApplications.Items.Add(AppNumber)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveWebPublisherData()
        Dim EPAStatesNotifiedAppRec As Date?
        Dim DraftOnWeb As Date?
        Dim EPAStatesNotified As Date?
        Dim FinalOnWeb As Date?
        Dim EPANotifiedPermitOnWeb As Date?
        Dim EffectiveDateOnPermit As Date?
        Dim TargetedComments As String
        Dim ExperationDate As Date?
        Dim PNExpires As Date?
        Dim queryList As New List(Of String)
        Dim paramsList As New List(Of SqlParameter())

        Try
            If DTPNotifiedAppReceived.Checked Then
                EPAStatesNotifiedAppRec = DTPNotifiedAppReceived.Value
            End If
            If DTPDraftOnWeb.Checked Then
                DraftOnWeb = DTPDraftOnWeb.Value
            End If
            If DTPEPAStatesNotified.Checked Then
                EPAStatesNotified = DTPEPAStatesNotified.Value
            End If
            If DTPFinalOnWeb.Checked Then
                FinalOnWeb = DTPFinalOnWeb.Value
            End If
            If DTPEPANotifiedPermitOnWeb.Checked Then
                EPANotifiedPermitOnWeb = DTPEPANotifiedPermitOnWeb.Value
            End If
            If DTPEffectiveDateofPermit.Checked Then
                EffectiveDateOnPermit = DTPEffectiveDateofPermit.Value
            End If
            If DTPExperationDate.Checked Then
                ExperationDate = DTPExperationDate.Value
            Else
                ExperationDate = Nothing
            End If
            If txtEPATargetedComments.Text <> "" Then
                TargetedComments = Mid(txtEPATargetedComments.Text, 1, 4000)
            Else
                TargetedComments = ""
            End If
            If DTPPNExpires.Checked Then
                PNExpires = DTPPNExpires.Value
            End If

            queryList.Add("Update SSPPApplicationTracking set " &
                          "datDraftOnWeb = @DraftOnWeb, " &
                          "datEPAStatesNotified = @EPAStatesNotified , " &
                          "datFinalOnWeb = @FinalOnWeb , " &
                          "datEPANotified = @EPANotifiedPermitOnWeb , " &
                          "datEffective = @EffectiveDateOnPermit , " &
                          "datEPAStatesNotifiedAppRec = @EPAStatesNotifiedAppRec ,  " &
                          "datExperationDate = @ExperationDate , " &
                          "datPNExpires = @PNExpires  " &
                          "where strApplicationNumber = @txtApplicationNumber ")
            paramsList.Add(
                    {New SqlParameter("@DraftOnWeb", DraftOnWeb),
                     New SqlParameter("@EPAStatesNotified", EPAStatesNotified),
                     New SqlParameter("@FinalOnWeb", FinalOnWeb),
                     New SqlParameter("@EPANotifiedPermitOnWeb", EPANotifiedPermitOnWeb),
                     New SqlParameter("@EffectiveDateOnPermit", EffectiveDateOnPermit),
                     New SqlParameter("@EPAStatesNotifiedAppRec", EPAStatesNotifiedAppRec),
                     New SqlParameter("@ExperationDate", ExperationDate),
                     New SqlParameter("@PNExpires", PNExpires),
                     New SqlParameter("@txtApplicationNumber", AppNumber)
                    })

            queryList.Add("Update SSPPApplicationData set " &
                              "strTargeted = @TargetedComments " &
                              "where strApplicationNumber = @txtApplicationNumber ")
            paramsList.Add(
                    {New SqlParameter("@TargetedComments", TargetedComments),
                     New SqlParameter("@txtApplicationNumber", AppNumber)
                    })

            DB.RunCommand(queryList, paramsList)

            If lblLinkWarning.Visible Then
                Dim LinkedApplication As String
                Dim i As Integer

                For i = 0 To lbLinkApplications.Items.Count - 1
                    If CInt(lbLinkApplications.Items.Item(i)) <> AppNumber Then
                        LinkedApplication = lbLinkApplications.Items.Item(i).ToString
                    Else
                        LinkedApplication = ""
                    End If
                    If LinkedApplication <> "" Then
                        queryList.Clear()
                        paramsList.Clear()

                        queryList.Add("Update SSPPApplicationTracking set " &
                            "datDraftOnWeb = @DraftOnWeb, " &
                            "datEPAStatesNotified = @EPAStatesNotified , " &
                            "datFinalOnWeb = @FinalOnWeb , " &
                            "datEPANotified = @EPANotifiedPermitOnWeb , " &
                            "datEffective = @EffectiveDateOnPermit , " &
                            "datEPAStatesNotifiedAppRec = @EPAStatesNotifiedAppRec ,  " &
                            "datExperationDate = @ExperationDate , " &
                            "datPNExpires = @PNExpires  " &
                            "where strApplicationNumber = @LinkedApplication ")
                        paramsList.Add(
                                {New SqlParameter("@DraftOnWeb", DraftOnWeb),
                                 New SqlParameter("@EPAStatesNotified", EPAStatesNotified),
                                 New SqlParameter("@FinalOnWeb", FinalOnWeb),
                                 New SqlParameter("@EPANotifiedPermitOnWeb", EPANotifiedPermitOnWeb),
                                 New SqlParameter("@EffectiveDateOnPermit", EffectiveDateOnPermit),
                                 New SqlParameter("@EPAStatesNotifiedAppRec", EPAStatesNotifiedAppRec),
                                 New SqlParameter("@ExperationDate", ExperationDate),
                                 New SqlParameter("@PNExpires", PNExpires),
                                 New SqlParameter("@LinkedApplication", LinkedApplication)
                                })

                        queryList.Add("Update SSPPApplicationData set " &
                            "strTargeted = @TargetedComments " &
                            "where strApplicationNumber = @LinkedApplication ")
                        paramsList.Add(
                                {New SqlParameter("@TargetedComments", TargetedComments),
                                 New SqlParameter("@LinkedApplication", LinkedApplication)
                                })

                        DB.RunCommand(queryList, paramsList)
                    End If
                Next
            End If

            MsgBox("Web Information Saved", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub GenerateAFSEntry()
        If AirsId Is Nothing Then
            Return
        End If

        Dim ActionNumber As String
        Dim UpdateStatus As String
        Dim query As String
        Dim params As SqlParameter()
        Dim recExists As Boolean = True

        Try

            query = "Select " &
            "strUpdateStatus " &
            "from AFSSSPPRecords " &
            "where strApplicationNumber = @appnum "
            params = {New SqlParameter("@appnum", AppNumber)}

            UpdateStatus = DB.GetString(query, params)

            If String.IsNullOrEmpty(UpdateStatus) Then
                UpdateStatus = "A"
                recExists = False
            End If

            If UpdateStatus = "N" Then UpdateStatus = "C"

            If recExists Then
                query = "Update AFSSSPPRecords set " &
                    "strUpdateStatus = @UpdateStatus " &
                    "where strApplicationNumber = @appnum "

                params = {
                    New SqlParameter("@UpdateStatus", UpdateStatus),
                    New SqlParameter("@appnum", AppNumber)
                }

                DB.RunCommand(query, params)
            Else
                query = "Select strAFSActionNumber " &
                    "from APBSupplamentalData " &
                    "where strAIRSNumber = @airs"

                params = {New SqlParameter("@airs", AirsId.DbFormattedString)}

                ActionNumber = DB.GetString(query, params)

                query = "Insert into AFSSSPPRecords " &
                    "(strApplicationNumber, strAFSActionNumber, " &
                    "strUpDateStatus, strModifingPerson, " &
                    "datModifingDate) " &
                    "values " &
                    "(@txtApplicationNumber, @ActionNumber , " &
                    "@UpdateStatus , @UserGCode , " &
                    " GETDATE() ) "

                params = {
                    New SqlParameter("@txtApplicationNumber", AppNumber),
                    New SqlParameter("@ActionNumber", ActionNumber),
                    New SqlParameter("@UpdateStatus", UpdateStatus),
                    New SqlParameter("@UserGCode", CurrentUser.UserID)
                }

                DB.RunCommand(query, params)

                ActionNumber = (CInt(ActionNumber) + 1).ToString

                query = "Update APBSupplamentalData set " &
                "strAFSActionNumber = @ActionNumber " &
                "where strAIRSNumber = @airs "

                params = {
                    New SqlParameter("@ActionNumber", ActionNumber),
                    New SqlParameter("@airs", AirsId.DbFormattedString)
                }

                DB.RunCommand(query, params)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UpdateAPBTables()
        If AirsId Is Nothing Then
            MessageBox.Show("Error: No AIRS number selected.")
            Return
        End If

        Dim FacilityName As String
        Dim FacilityStreet1 As String
        Dim FacilityStreet2 As String
        Dim City As String
        Dim ZipCode As String
        Dim OpStatus As String = ""
        Dim Classification As String
        Dim AirProgramCodes As String = ""
        Dim SICCode As String
        Dim NAICSCode As String
        Dim OwnershipTypeCode As String
        Dim NspsFeeExempt As Boolean
        Dim PlantDescription As String
        Dim StateProgramCodes As String

        Dim query As String
        Dim params As SqlParameter()
        Dim queryList As New List(Of String)
        Dim paramsList As New List(Of SqlParameter())
        Dim subpartList As New List(Of String)

        Try

            query = "Select " &
            "strFacilityName, strFacilityStreet1, " &
            "strFacilityStreet2, strFacilityCity, " &
            "strFacilityState, strFacilityZipCode, " &
            "strOperationalStatus, strClass, " &
            "strAIRProgramCodes, strSICCode, " &
            "strNAICSCode, FacilityOwnershipTypeCode, NspsFeeExempt, " &
            "strPlantDescription, " &
            "strStateProgramCodes " &
            "from SSPPApplicationData " &
            "where strApplicationNumber = @appnumber "

            params = {New SqlParameter("@appnumber", AppNumber)}

            Dim dr As DataRow = DB.GetDataRow(query, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = "N/A"
                Else
                    FacilityName = SanitizeFacilityNameForDb(dr.Item("strFacilityName"))
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

                OwnershipTypeCode = DBUtilities.GetNullableString(dr.Item("FacilityOwnershipTypeCode"))

                NspsFeeExempt = dr.Item("NspsFeeExempt")

                queryList.Add("Update APBFacilityInformation set " &
                    "strFacilityName = @FacilityName, " &
                    "strFacilityStreet1 = @FacilityStreet1, " &
                    "strFacilityStreet2 = @FacilityStreet2, " &
                    "strFacilityCity = @City, " &
                    "strFacilityZipCode = @ZipCode , " &
                    "strComments = @Comments , " &
                    "strModifingLocation = '1', " &
                    "strModifingPerson = @UserGCode , " &
                    "datModifingdate = GETDATE() " &
                    "where strAIRSNumber = @airs ")

                paramsList.Add(
                    {New SqlParameter("@FacilityName", FacilityName),
                    New SqlParameter("@FacilityStreet1", FacilityStreet1),
                    New SqlParameter("@FacilityStreet2", FacilityStreet2),
                    New SqlParameter("@City", City),
                    New SqlParameter("@ZipCode", ZipCode),
                    New SqlParameter("@Comments", "Updated by " & CurrentUser.AlphaName & ", through Permitting Action."),
                    New SqlParameter("@UserGCode", CurrentUser.UserID),
                    New SqlParameter("@airs", AirsId.DbFormattedString)
                    })

                queryList.Add("Update APBHeaderData set " &
                    "strOperationalStatus = @OpStatus , " &
                    "strClass = @Classification , " &
                    "strAIRProgramCodes = @AirProgramCodes , " &
                    "strSICCode = @SICCode, " &
                    "strNAICSCode = @NAICSCode , " &
                    "strPlantDescription = @PlantDescription, " &
                    "strStateProgramCodes = @StateProgramCodes , " &
                    "strComments = @Comments , " &
                    "strModifingLocation = '1', " &
                    "strModifingPerson = @UserGCode , " &
                    "datModifingDate = GETDATE() " &
                    "where strAIRSNumber = @airs ")

                paramsList.Add(
                    {New SqlParameter("@OpStatus", OpStatus),
                    New SqlParameter("@Classification", Classification),
                    New SqlParameter("@AirProgramCodes", AirProgramCodes),
                    New SqlParameter("@SICCode", RealStringOrNothing(SICCode)),
                    New SqlParameter("@NAICSCode", RealStringOrNothing(NAICSCode)),
                    New SqlParameter("@PlantDescription", PlantDescription),
                    New SqlParameter("@StateProgramCodes", StateProgramCodes),
                    New SqlParameter("@Comments", "Updated by " & CurrentUser.AlphaName & ", through Permitting Action."),
                    New SqlParameter("@UserGCode", CurrentUser.UserID),
                    New SqlParameter("@airs", AirsId.DbFormattedString)
                    })

                queryList.Add("update APBSUPPLAMENTALDATA
                    set FacilityOwnershipTypeCode = @OwnershipTypeCode,
                        NspsFeeExempt             = @NspsFeeExempt
                    where STRAIRSNUMBER = @airs")
                paramsList.Add(
                    {New SqlParameter("@OwnershipTypeCode", OwnershipTypeCode),
                    New SqlParameter("@NspsFeeExempt", NspsFeeExempt),
                    New SqlParameter("@airs", AirsId.DbFormattedString)
                    })

                DB.RunCommand(queryList, paramsList)
                queryList.Clear()
                paramsList.Clear()
            End If

            If AirProgramCodes <> "000000000000000" Then
                If Mid(AirProgramCodes, 1, 1) = "1" Then
                    UpdateProgramPollutantKey("0", OpStatus)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvSIPSubPartDelete.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateDeletedSubpartData("0", subpartList)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvSIPSubpartAddEdit.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateAddedSubpartData("0", subpartList)
                Else
                    UpdateDeletedSubpartData("0")
                End If

                If Mid(AirProgramCodes, 2, 1) = "1" Then
                    UpdateProgramPollutantKey("1", OpStatus)
                End If

                If Mid(AirProgramCodes, 3, 1) = "1" Then
                    UpdateProgramPollutantKey("3", OpStatus)
                End If

                If Mid(AirProgramCodes, 4, 1) = "1" Then
                    UpdateProgramPollutantKey("4", OpStatus)
                End If

                If Mid(AirProgramCodes, 5, 1) = "1" Then
                    UpdateProgramPollutantKey("6", OpStatus)
                End If

                If Mid(AirProgramCodes, 6, 1) = "1" Then
                    UpdateProgramPollutantKey("7", OpStatus)
                End If

                If Mid(AirProgramCodes, 7, 1) = "1" Then
                    UpdateProgramPollutantKey("8", OpStatus)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvNESHAPSubPartDelete.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateDeletedSubpartData("8", subpartList)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvNESHAPSubpartAddEdit.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateAddedSubpartData("8", subpartList)
                Else
                    UpdateDeletedSubpartData("8")
                End If

                If Mid(AirProgramCodes, 8, 1) = "1" Then
                    UpdateProgramPollutantKey("9", OpStatus)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvNSPSSubPartDelete.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateDeletedSubpartData("9", subpartList)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvNSPSSubpartAddEdit.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateAddedSubpartData("9", subpartList)
                Else
                    UpdateDeletedSubpartData("9")
                End If

                If Mid(AirProgramCodes, 9, 1) = "1" Then
                    UpdateProgramPollutantKey("F", OpStatus)
                End If

                If Mid(AirProgramCodes, 10, 1) = "1" Then
                    UpdateProgramPollutantKey("A", OpStatus)
                End If

                If Mid(AirProgramCodes, 11, 1) = "1" Then
                    UpdateProgramPollutantKey("I", OpStatus)
                End If

                If Mid(AirProgramCodes, 12, 1) = "1" Then
                    UpdateProgramPollutantKey("M", OpStatus)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvMACTSubPartDelete.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateDeletedSubpartData("M", subpartList)

                    subpartList.Clear()
                    For Each row As DataGridViewRow In dgvMACTSubpartAddEdit.Rows
                        subpartList.Add(row.Cells(0).Value.ToString)
                    Next
                    If subpartList.Count > 0 Then UpdateAddedSubpartData("M", subpartList)
                Else
                    UpdateDeletedSubpartData("M")
                End If

                If Mid(AirProgramCodes, 13, 1) = "1" Then
                    UpdateProgramPollutantKey("V", OpStatus)
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, AirsId?.FormattedString & vbNewLine & AppNumber.ToString, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UpdateProgramPollutantKey(key As String, OpStatus As String)
        Dim pKey As String = AirsId.DbFormattedString & key
        Dim query As String
        Dim params As SqlParameter()
        Dim queryList As New List(Of String)
        Dim paramsList As New List(Of SqlParameter())

        query = "Select strPollutantKey " &
                "from APBAirProgramPollutants " &
                "where strAIRPollutantKey = @pKey "
        params = {New SqlParameter("@pKey", pKey)}

        If Not DB.ValueExists(query, params) Then
            query = "Insert into APBAirProgramPollutants " &
             "(strAirsNumber, strAirPollutantKey, " &
             "strPollutantKey, strComplianceStatus, " &
             "strModifingPerson, datModifingDate, " &
             "strOperationalStatus) " &
             "values " &
             "(@airs, @pKey, " &
             "'OT', 'C', " &
             "@UserGCode , GETDATE() , " &
             "'O')"
            params = {
                New SqlParameter("@airs", AirsId.DbFormattedString),
                New SqlParameter("@pKey", pKey),
                New SqlParameter("@UserGCode", CurrentUser.UserID)
            }
            DB.RunCommand(query, params)
        Else
            queryList.Add("Update APBAirProgramPollutants set " &
            "strOperationalStatus = @OpStatus  " &
            "where strAirPOllutantKey = @pKey ")
            paramsList.Add({
                New SqlParameter("@OpStatus", OpStatus),
                New SqlParameter("@pKey", pKey)
            })

            queryList.Add("update AFSAirPollutantData set " &
                "strUpdateStatus = 'C' " &
                "where strUpdateStatus = 'N' and strAIRPollutantKey = @pKey ")
            paramsList.Add({New SqlParameter("@pKey", AirsId.DbFormattedString & key)})

            DB.RunCommand(queryList, paramsList)
        End If
    End Sub

    Private Sub UpdateDeletedSubpartData(key As String, Optional subpartList As List(Of String) = Nothing)
        Dim queryList As New List(Of String)
        Dim paramsList As New List(Of SqlParameter())

        If subpartList Is Nothing OrElse subpartList.Count = 0 Then
            queryList.Add("Update APBSubpartData set " &
                          "Active = '0', " &
                          "updateUser = @UserGCode , " &
                          "updateDateTime = GETDATE() " &
                          "where strSubpartKey = @pKey ")
            paramsList.Add(
                {
                    New SqlParameter("@UserGCode", CurrentUser.UserID),
                    New SqlParameter("@pKey", AirsId.DbFormattedString & key)
                })
        Else
            For Each subpart As String In subpartList
                queryList.Add("Update APBSubpartData set " &
                              "Active = '0', " &
                              "updateUser = @UserGCode , " &
                              "updateDateTime = GETDATE() " &
                              "where strSubpartKey = @pKey " &
                              "and strSubpart = @Subpart ")
                paramsList.Add(
                    {
                        New SqlParameter("@UserGCode", CurrentUser.UserID),
                        New SqlParameter("@pKey", AirsId.DbFormattedString & key),
                        New SqlParameter("@Subpart", subpart)
                    })
            Next
        End If

        DB.RunCommand(queryList, paramsList)
    End Sub

    Private Sub UpdateAddedSubpartData(key As String, subpartList As List(Of String))
        If subpartList Is Nothing OrElse subpartList.Count = 0 Then
            Return
        End If

        Dim pKey As String = AirsId.DbFormattedString & key
        Dim query As String = ""
        Dim params As SqlParameter() = Nothing

        For Each subpart As String In subpartList
            query = "Select Active from APBSubpartData " &
                "where strSubpartKey = @pKey " &
                "and strSubpart = @subpart "
            params = {
                New SqlParameter("@pKey", pKey),
                New SqlParameter("@subpart", subpart)
            }

            If DB.ValueExists(query, params) Then
                query = "Update APBSubpartData set " &
                    "Active = '1', " &
                    "updateUser = @UserGCode , " &
                    "updateDateTime = GETDATE() " &
                    "where strSubpartKey = @pKey " &
                    "and strSubpart = @subpart "
                params = {
                    New SqlParameter("@UserGCode", CurrentUser.UserID),
                    New SqlParameter("@pKey", pKey),
                    New SqlParameter("@subpart", subpart)
                }
            Else
                query = "INSERT INTO APBSUBPARTDATA " &
                    "  ( STRAIRSNUMBER, STRSUBPARTKEY, STRSUBPART, UPDATEUSER , " &
                    "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " &
                    "  ) VALUES " &
                    "(@airs, @pKey, @subpart, @UserGCode , " &
                    "GETDATE(), '1', GETDATE())"
                params = {
                    New SqlParameter("@airs", AirsId.DbFormattedString),
                    New SqlParameter("@pKey", pKey),
                    New SqlParameter("@subpart", subpart),
                    New SqlParameter("@UserGCode", CurrentUser.UserID)
                }
            End If
        Next
        DB.RunCommand(query, params)
    End Sub

    Private Sub DisplayPermitPanel()
        If rdbTitleVPermit.Checked Then
            PanelTitleV.Visible = True
            PanelPSD.Visible = False
            PanelOther.Visible = False
            PanelTitleV.Location = New Point(100, 25)

            Return
        End If

        If rdbPSDPermit.Checked Then
            PanelTitleV.Visible = False
            PanelPSD.Visible = True
            PanelOther.Visible = False
            PanelPSD.Location = New Point(100, 25)

            Return
        End If

        If rdbOtherPermit.Checked Then
            PanelTitleV.Visible = False
            PanelPSD.Visible = False
            PanelOther.Visible = True
            PanelOther.Location = New Point(100, 25)

            Return
        End If

        PanelTitleV.Visible = False
        PanelPSD.Visible = False
        PanelOther.Visible = False
    End Sub

    Private Sub FindMasterApp()
        Dim AppType As String
        Dim query As String
        Dim parameter As SqlParameter()

        Try
            AppType = cboApplicationType.Text

            query = "select strMasterApplication " &
              "from SSPPApplicationLinking " &
              "where strApplicationNumber = @appnumber "
            parameter = {New SqlParameter("@appnumber", AppNumber)}

            MasterApp = DB.GetString(query, parameter)
            If MasterApp = "" Then MasterApp = AppNumber.ToString

            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

            query = "select " &
            "distinct(APBPermits.strFileName)  " &
            "from APBpermits " &
            "left join SSPPApplicationLinking " &
            "on SUBSTRING(APBpermits.strFileName, 4,10) = SSPPAPPlicationLinking.strmasterapplication " &
            "where (SSPPApplicationLinking.strApplicationNumber = @MasterApp " &
            "or APBPermits.strFileName like @MasterAppFn ) "

            parameter = {
                New SqlParameter("@MasterApp", MasterApp),
                New SqlParameter("@MasterAppFn", "%-" & MasterApp)
            }

            Dim fn As String = DB.GetString(query, parameter)

            If fn <> "" Then
                Dim temp As String = Mid(fn, 1, 1)
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DownloadFile(fileName As String, fileType As String)
        If fileType = "00" Then
            Return
        End If
        Try
            Dim saveFilePath As String
            Dim query As String = ""
            Dim parameter As New SqlParameter("@FileName", fileName)

            Dim sfd As New SaveFileDialog With {
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation),
                .FileName = fileName,
                .FilterIndex = 1
            }

            Select Case fileType
                Case "10", "11"
                    sfd.Filter = "Microsoft Office Work file (*.doc)|.doc"
                    sfd.DefaultExt = ".doc"
                    query = "select " &
                        "DocPermitData " &
                        "from APBPermits " &
                        "where strFileName = @FileName "
                Case "01"
                    sfd.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                    sfd.DefaultExt = ".pdf"
                    query = "select " &
                        "PdfPermitData " &
                        "from APBPermits " &
                        "where strFileName = @FileName "
            End Select

            If sfd.ShowDialog = DialogResult.OK Then
                saveFilePath = sfd.FileName.ToString
                SaveBinaryFileFromDB(saveFilePath, query, parameter)

                If IO.Path.GetDirectoryName(sfd.FileName) <> sfd.InitialDirectory Then
                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(sfd.FileName))
                    sfd.InitialDirectory = IO.Path.GetDirectoryName(sfd.FileName)
                End If
            End If

            If fileType = "11" Then
                sfd.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                sfd.DefaultExt = ".pdf"
                query = "select " &
                    "PdfPermitData " &
                    "from APBPermits " &
                    "where strFileName = @FileName "

                If sfd.ShowDialog = DialogResult.OK Then
                    saveFilePath = sfd.FileName.ToString
                    SaveBinaryFileFromDB(saveFilePath, query, parameter)

                    If IO.Path.GetDirectoryName(sfd.FileName) <> sfd.InitialDirectory Then
                        SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(sfd.FileName))
                    End If
                End If
            End If

            sfd.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbClosedOut_CheckedChanged(sender As Object, e As EventArgs) Handles chbClosedOut.CheckedChanged
        CloseOutApplication(chbClosedOut.Checked)
    End Sub

    Private Sub txtPermitNumber_TextChanged(sender As Object, e As EventArgs) Handles txtPermitNumber.TextChanged
        If Not txtPermitNumber.ReadOnly AndAlso txtPermitNumber.Text = "" Then
            If cboApplicationType.Text = "ERC" Then
                txtPermitNumber.Text = "ERC"
            ElseIf txtSICCode.Text.Length = 4 AndAlso AirsId IsNot Nothing Then
                txtPermitNumber.Text = txtSICCode.Text & "-" & AirsId.PermitFormattedString & "-"
            End If
        End If
    End Sub

    Private Sub btnSaveInformationRequest_Click(sender As Object, e As EventArgs) Handles btnSaveInformationRequest.Click
        SaveInformationRequest()
    End Sub

    Private Sub btnDeleteInformationRequest_Click(sender As Object, e As EventArgs) Handles btnDeleteInformationRequest.Click
        DeleteInformationRequest()
    End Sub

    Private Sub btnClearInformationRequest_Click(sender As Object, e As EventArgs) Handles btnClearInformationRequest.Click
        txtInformationRequestedKey.Clear()
        DTPInformationRequested.Value = Today
        DTPInformationRequested.Checked = False
        txtInformationRequested.Clear()
        DTPInformationReceived.Value = Today
        DTPInformationReceived.Checked = False
        txtInformationReceived.Clear()
    End Sub

    Private Sub rdbSSCPYes_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSSCPYes.CheckedChanged
        txtSSCPComments.Enabled = rdbSSCPYes.Checked
        txtSSCPComments.ReadOnly = Not rdbSSCPYes.Checked
    End Sub

    Private Sub rdbSSCPNo_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSSCPNo.CheckedChanged
        txtSSCPComments.Enabled = Not rdbSSCPNo.Checked
        txtSSCPComments.ReadOnly = rdbSSCPNo.Checked
    End Sub

    Private Sub rdbISMPYes_CheckedChanged(sender As Object, e As EventArgs) Handles rdbISMPYes.CheckedChanged
        txtISMPComments.Enabled = rdbISMPYes.Checked
        txtISMPComments.ReadOnly = Not rdbISMPYes.Checked
    End Sub

    Private Sub rdbISMPNo_CheckedChanged(sender As Object, e As EventArgs) Handles rdbISMPNo.CheckedChanged
        txtISMPComments.Enabled = Not rdbISMPNo.Checked
        txtISMPComments.ReadOnly = rdbISMPNo.Checked
    End Sub

    Private Sub btnAddApplicationToList_Click(sender As Object, e As EventArgs) Handles btnAddApplicationToList.Click
        Try
            If txtApplicationNumberHistory.Text <> "" AndAlso Not chbClosedOutHistory.Checked Then
                If txtMasterApp.Text = "" Then
                    txtMasterApp.Text = txtApplicationNumberHistory.Text
                End If

                If Not lbLinkApplications.Items.Contains(CInt(txtApplicationNumberHistory.Text)) Then
                    lbLinkApplications.Items.Add(CInt(txtApplicationNumberHistory.Text))
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLinkApplications_Click(sender As Object, e As EventArgs) Handles btnLinkApplications.Click
        LinkApplications()
    End Sub

    Private Sub btnClearLinks_Click(sender As Object, e As EventArgs) Handles btnClearLinks.Click
        ClearApplicationLinks()
    End Sub

    Private Sub lbLinkApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbLinkApplications.SelectedIndexChanged
        Try
            If txtMasterAppLock.Text = "" Then
                If lbLinkApplications.Items.Count > 0 AndAlso lbLinkApplications.SelectedItem IsNot Nothing Then
                    txtMasterApp.Text = lbLinkApplications.SelectedItem.ToString
                End If
            Else
                txtMasterApp.Text = txtMasterAppLock.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveWebPublisher_Click(sender As Object, e As EventArgs) Handles btnSaveWebPublisher.Click
        SaveWebPublisherData()
    End Sub

    Private Sub PreSaveCheckThenSave()

        If NewApplication Then
            If Not CurrentUser.HasPermission(UserCan.CreatePermitApp) Then
                MessageBox.Show("You do not have permission to create a new application. Please contact your manager.", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If

            If Not Integer.TryParse(txtNewApplicationNumber.Text, AppNumber) Then
                MessageBox.Show("The selected application number is not valid. Please enter a new application number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If ApplicationExists(AppNumber) Then
                MessageBox.Show("The selected application number already exists. Please enter a new application number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim result As DialogResult = MessageBox.Show("This will create a new permit application. Are you sure you want to proceed?", "New Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                Return
            End If

            If CreateNewApplication() Then
                MessageBox.Show("New application created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("There was an error creating the new application. Please contact EPD-IT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        If CurrentUser.HasPermission(UserCan.EditPermitApp) Then

            Dim dateModifiedInDb As DateTimeOffset = GetWhenLastModified(AppNumber)

            If Not NewApplication AndAlso LastModificationDateAsLoaded < dateModifiedInDb Then
                MessageBox.Show("The application has been updated since you last opened it." & vbNewLine &
                            "Please reopen the application to save any changes." & vbNewLine & vbNewLine &
                            "NO DATA SAVED",
                            "Application Tracking Log", MessageBoxButtons.OK)
                Return
            End If

            If ValidateForm() Then
                If Not SaveApplicationData() Then
                    MessageBox.Show("There was an error saving the application data. Please contact EPD-IT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    ' After validating form and saving main application data, proceed with remaining data updates and cleanup
                    LastModificationDateAsLoaded = GetWhenLastModified(AppNumber)

                    SaveApplicationContact()
                    SaveApplicationFees()

                    If DTPFinalAction.Checked AndAlso chbClosedOut.Checked AndAlso AirsId IsNot Nothing Then
                        Select Case cboPermitAction.SelectedValue.ToString
                            Case "1", "4", "5", "7", "10", "12", "13"
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
                                GenerateAFSEntry()
                        End Select

                        Dim dresult As DialogResult = MessageBox.Show("Do you want to update Facility Information with this Application?",
                                                                      "Permit Tracking Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                                      MessageBoxDefaultButton.Button1)
                        If dresult = DialogResult.Yes Then
                            UpdateAPBTables()
                        End If

                        If AirsId IsNot Nothing AndAlso IsValidPermitNumber(txtPermitNumber.Text) Then
                            PermitRevocationQuery()
                            SaveIssuedPermit()
                        End If
                    End If

                    If Not NewApplication Then
                        SaveApplicationSubmitForReview()

                        If DTPSSCPReview.Checked Then
                            SaveSSCPReview()
                        End If

                        If DTPISMPReview.Checked Then
                            SaveISMPReview()
                        End If
                    End If

                    MessageBox.Show("Application data saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If

        If NewApplication Then
            Me.Close()
            OpenFormPermitApplication(AppNumber)
        Else
            LoadBasicFacilityInfo()
        End If
    End Sub

    Private Function ValidateForm() As Boolean
        Dim valid As Boolean = True

        If Not SicCodeIsValid(txtSICCode.Text) Then
            MessageBox.Show("The SIC Code is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If Not NaicsCodeIsValid(txtNAICSCode.Text) Then
            MessageBox.Show("The NAICS Code is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If txtPermitNumber.Text.Length > 15 AndAlso txtPermitNumber.Text.Replace("-", "").Length > 15 Then
            MessageBox.Show("The Permit Number cannot be more than 15 characters (not counting hyphens).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If txtFacilityZipCode.Text.Length > 9 AndAlso txtFacilityZipCode.Text.Replace("-", "").Length > 9 Then
            MessageBox.Show("The ZIP code cannot be more than 9 characters (not counting hyphen).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If chbAppFee.Checked AndAlso cmbAppFeeType.SelectedIndex = -1 Then
            MessageBox.Show("An application fee type is not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If chbExpFee.Checked AndAlso cmbExpFeeType.SelectedIndex = -1 Then
            MessageBox.Show("An expedited review fee type is not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If chbAppFeeOverride.Checked AndAlso String.IsNullOrWhiteSpace(txtAppFeeOverrideReason.Text) Then
            MessageBox.Show("Please enter an explanation for overriding the application fee amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        If chbExpFeeOverride.Checked AndAlso String.IsNullOrWhiteSpace(txtExpFeeOverrideReason.Text) Then
            MessageBox.Show("Please enter an explanation for overriding the application fee amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            valid = False
        End If

        Return valid
    End Function

    Private Sub DTPReviewSubmitted_ValueChanged(sender As Object, e As EventArgs) Handles DTPReviewSubmitted.ValueChanged
        cboSSCPUnits.Enabled = DTPReviewSubmitted.Checked
        cboISMPUnits.Enabled = DTPReviewSubmitted.Checked
    End Sub

    Private Sub DTPISMPReview_ValueChanged(sender As Object, e As EventArgs) Handles DTPISMPReview.ValueChanged
        cboISMPStaff.Enabled = DTPISMPReview.Checked
        rdbISMPYes.Enabled = DTPISMPReview.Checked
        rdbISMPNo.Enabled = DTPISMPReview.Checked
        txtISMPComments.Enabled = DTPISMPReview.Checked
    End Sub

    Private Sub DTPSSCPReview_ValueChanged(sender As Object, e As EventArgs) Handles DTPSSCPReview.ValueChanged
        cboSSCPStaff.Enabled = DTPSSCPReview.Checked
        rdbSSCPYes.Enabled = DTPSSCPReview.Checked
        rdbSSCPNo.Enabled = DTPSSCPReview.Checked
        txtSSCPComments.Enabled = DTPSSCPReview.Checked
    End Sub

    Private Sub dgrFacilityAppHistory_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvFacilityAppHistory.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFacilityAppHistory.HitTest(e.X, e.Y)
        Dim temp As String

        Try

            If dgvFacilityAppHistory.RowCount > 0 AndAlso hti.RowIndex <> -1 AndAlso dgvFacilityAppHistory.Columns(0).HeaderText = "APL #" Then
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvInformationRequested_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvInformationRequested.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvInformationRequested.HitTest(e.X, e.Y)
        Dim tempString As String

        Try

            If dgvInformationRequested.RowCount > 0 AndAlso hti.RowIndex <> -1 AndAlso dgvInformationRequested.Columns(1).HeaderText = "Request Key" Then

                txtInformationRequestedKey.Text = dgvInformationRequested(1, hti.RowIndex).Value
                tempString = dgvInformationRequested(2, hti.RowIndex).Value
                If tempString = " " Then
                    DTPInformationRequested.Value = Today
                    DTPInformationRequested.Checked = False
                Else
                    DTPInformationRequested.Value = CDate(tempString)
                    DTPInformationRequested.Checked = True
                End If
                txtInformationRequested.Text = dgvInformationRequested(3, hti.RowIndex).Value
                tempString = dgvInformationRequested(4, hti.RowIndex).Value
                If tempString = " " Then
                    DTPInformationReceived.Value = Today
                    DTPInformationReceived.Checked = False
                Else
                    DTPInformationReceived.Value = CDate(tempString)
                    DTPInformationReceived.Checked = True
                End If
                txtInformationReceived.Text = dgvInformationRequested(5, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboClassification_TextChanged(sender As Object, e As EventArgs) Handles cboClassification.TextChanged
        If cboClassification.Text = "A - MAJOR" Then
            chbNSRMajor.Visible = True
            chbHAPsMajor.Visible = True
        Else
            chbNSRMajor.Visible = False
            chbHAPsMajor.Visible = False
        End If
    End Sub

    Private Sub cboApplicationType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboApplicationType.SelectedValueChanged
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
            GBSignificationComments.Visible = False
            SetPAVisibility(True)

            Select Case cboApplicationType.Text
                Case "502(b)10", "PBR"
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

                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
                    End If

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If

                Case "Acid Rain"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False

                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
                    End If

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If

                Case "Closed", "OFF PERMIT"
                    DTPEPAWaived.Visible = False
                    lblEPAWaived.Visible = False
                    DTPEPAEnds.Visible = False
                    lblEPAEnds.Visible = False
                    DTPDraftIssued.Visible = False
                    lblDraftIssued.Visible = False
                    DTPDatePNExpires.Visible = False
                    lblDatePNExpires.Visible = False
                    chbPNReady.Visible = False

                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
                    End If

                    txtPermitNumber.Text = " "

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

                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
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
                    SetPAVisibility(False)
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

                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
                    End If

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If

                Case "SAW"
                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
                    GBSignificationComments.Visible = True

                Case "SAWO"
                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
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
                    If chbPSD.Checked OrElse chbNAANSR.Checked OrElse chb112g.Checked Then
                        DTPDraftIssued.Visible = True
                        lblDraftIssued.Visible = True
                    Else
                        DTPDraftIssued.Visible = False
                        lblDraftIssued.Visible = False
                    End If
                    DTPDatePNExpires.Visible = True
                    lblDatePNExpires.Visible = True
                    chbPNReady.Visible = True

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
                    DTPDatePNExpires.Visible = True
                    lblDatePNExpires.Visible = True
                    chbPNReady.Visible = True

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If

                Case "TV-Initial", "TV-Renewal"
                    If cboPublicAdvisory.Text = "PA Needed" Then
                        SetPAVisibility(True)
                    Else
                        SetPAVisibility(False)
                    End If

                    If Mid(txtPermitNumber.Text, 1, 4) <> txtSICCode.Text Then
                        txtPermitNumber.Text = " "
                    End If
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SetPAVisibility(visible As Boolean)
        cboPublicAdvisory.Visible = visible
        lblPublicAdvisory.Visible = visible
        DTPDatePAExpires.Visible = visible
        lblDatePAExpires.Visible = visible
        chbPAReady.Visible = visible
        lblPAReady.Visible = visible
    End Sub

    Private Sub chb112_CheckedChanged(sender As Object, e As EventArgs) Handles chb112g.CheckedChanged
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
    End Sub

    Private Sub chbPSD_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSD.CheckedChanged
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
    End Sub

    Private Sub chbNAANSR_CheckedChanged(sender As Object, e As EventArgs) Handles chbNAANSR.CheckedChanged
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
    End Sub

    Private Sub cboPublicAdvisory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPublicAdvisory.SelectedIndexChanged
        If cboPublicAdvisory.Text = "PA Needed" Then
            chbPAReady.Visible = True
            DTPDatePAExpires.Visible = True
            lblDatePAExpires.Visible = True
            lblPAReady.Visible = True
        Else
            chbPAReady.Checked = False
            DTPDatePAExpires.Value = Today
            DTPDatePAExpires.Checked = False

            chbPAReady.Visible = False
            DTPDatePAExpires.Visible = False
            lblDatePAExpires.Visible = False
            lblPAReady.Visible = False
        End If
    End Sub

    Private Sub chbPAReady_CheckedChanged(sender As Object, e As EventArgs) Handles chbPAReady.CheckedChanged
        If chbPAReady.Checked Then
            cboPublicAdvisory.Text = "PA Needed"
            cboPublicAdvisory.Enabled = False
        Else
            cboPublicAdvisory.Enabled = True
        End If
    End Sub

    Public Sub LoadApplication()
        Try
            lblAppNumber.Text = "Application #" & AppNumber.ToString
            Me.Text = AppNumber.ToString & " - " & Me.Text

            LoadApplicationData()

            LoadFacilityAttainmentStatus()
            LoadBasicFacilityInfo()
            LoadOpenApplications()

            LoadContactData()
            LoadFeeRatesComboBoxes()
            LoadFeesData()

            FindMasterApp()

            LoadSSPPSIPSubPartInformation()
            LoadSSPPNSPSSubPartInformation()
            LoadSSPPNESHAPSubPartInformation()
            LoadSSPPMACTSubPartInformation()

            SetUpPublicAppViewLink()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SetUpPublicAppViewLink()
        lklOpenAppOnline.Visible = True
        ToolTip1.SetToolTip(lklOpenAppOnline, GetPermitApplicationUrl(AppNumber).ToString())
    End Sub

    Private Sub btnRefreshAIRSNo_Click(sender As Object, e As EventArgs) Handles btnRefreshAIRSNo.Click
        If txtAIRSNumber.ValidationStatus = DAL.AirsNumberValidationResult.Valid Then
            AirsId = txtAIRSNumber.AirsNumber

            FormStatus = "Loading"
            ReLoadBasicFacilityInfo()
            LoadOpenApplications()
            LoadSSPPSIPSubPartInformation()
            LoadSSPPNSPSSubPartInformation()
            LoadSSPPNESHAPSubPartInformation()
            LoadSSPPMACTSubPartInformation()
            dgvFacilityAppHistory.DataSource = Nothing
            FacilityApplicationHistoryLoaded = False
            FormStatus = ""
        Else
            MessageBox.Show("AIRS number does not exist.")
        End If
    End Sub

    Private Sub rdbTitleVPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTitleVPermit.CheckedChanged
        Try
            Dim TVNarrative As Boolean = False
            Dim TVDraft As Boolean = False
            Dim TVNotice As Boolean = False
            Dim TVFinal As Boolean = False

            chbTVNarrative.Checked = False
            chbTVDraft.Checked = False
            chbTVPublicNotice.Checked = False
            chbTVFinal.Checked = False

            DisplayPermitPanel()

            If rdbTitleVPermit.Checked Then
                Dim query As String = "select " &
                    "strFileName " &
                    "from APBPermits " &
                    "where strFileName like @filename "
                Dim parameter As New SqlParameter("@filename", "V_-" & MasterApp)
                Dim fn As DataTable = DB.GetDataTable(query, parameter)

                If fn IsNot Nothing Then
                    For Each row As DataRow In fn.Rows
                        If row IsNot Nothing Then
                            Select Case Mid(row(0), 1, 2)
                                Case "VN"
                                    TVNarrative = True
                                Case "VD"
                                    TVDraft = True
                                Case "VP"
                                    TVNotice = True
                                Case "VF"
                                    TVFinal = True
                            End Select
                        End If
                    Next
                End If

                If TVNarrative Then
                    chbTVNarrative.Checked = True
                End If
                If TVDraft Then
                    chbTVDraft.Checked = True
                End If
                If TVNotice Then
                    chbTVPublicNotice.Checked = True
                End If
                If TVFinal Then
                    chbTVFinal.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbPSDPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPSDPermit.CheckedChanged
        Try
            Dim PSDAppSummary As Boolean = False
            Dim PSDPrelimDet As Boolean = False
            Dim PSDNarrative As Boolean = False
            Dim PSDDraft As Boolean = False
            Dim PSDNotice As Boolean = False
            Dim PSDHearing As Boolean = False
            Dim PSDFinal As Boolean = False
            Dim PSDPermit As Boolean = False

            chbPSDApplicationSummary.Checked = False
            chbPSDPrelimDet.Checked = False
            chbPSDNarrative.Checked = False
            chbPSDDraftPermit.Checked = False
            chbPSDPublicNotice.Checked = False
            chbPSDHearingNotice.Checked = False
            chbPSDFinalDet.Checked = False
            chbPSDFinalPermit.Checked = False

            DisplayPermitPanel()

            If rdbPSDPermit.Checked AndAlso MasterApp <> "" Then

                Dim query As String = "select " &
                    "strFileName " &
                    "from APBPermits " &
                    "where strFileName like @filename "
                Dim parameter As New SqlParameter("@filename", "P_-" & MasterApp)
                Dim fn As DataTable = DB.GetDataTable(query, parameter)

                If fn IsNot Nothing Then
                    For Each row As DataRow In fn.Rows
                        If row IsNot Nothing Then
                            Select Case Mid(row(0), 1, 2)
                                Case "PA"
                                    PSDAppSummary = True
                                Case "PP"
                                    PSDPrelimDet = True
                                Case "PT"
                                    PSDNarrative = True
                                Case "PD"
                                    PSDDraft = True
                                Case "PN"
                                    PSDNotice = True
                                Case "PH"
                                    PSDHearing = True
                                Case "PF"
                                    PSDFinal = True
                                Case "PI"
                                    PSDPermit = True
                            End Select
                        End If
                    Next
                End If

                If PSDAppSummary Then
                    chbPSDApplicationSummary.Checked = True
                End If
                If PSDPrelimDet Then
                    chbPSDPrelimDet.Checked = True
                End If
                If PSDNarrative Then
                    chbPSDNarrative.Checked = True
                End If
                If PSDDraft Then
                    chbPSDDraftPermit.Checked = True
                End If
                If PSDNotice Then
                    chbPSDPublicNotice.Checked = True
                End If
                If PSDHearing Then
                    chbPSDHearingNotice.Checked = True
                End If
                If PSDFinal Then
                    chbPSDFinalDet.Checked = True
                End If
                If PSDPermit Then
                    chbPSDFinalPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbOtherPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOtherPermit.CheckedChanged
        Try
            Dim OtherNarrative As Boolean = False
            Dim OtherPermit As Boolean = False

            chbOtherNarrative.Checked = False
            chbOtherPermit.Checked = False

            DisplayPermitPanel()

            If rdbOtherPermit.Checked Then

                Dim query As String = "select " &
                    "strFileName " &
                    "from APBPermits " &
                    "where strFileName like @filename "
                Dim parameter As New SqlParameter("@filename", "O_-" & MasterApp)
                Dim fn As DataTable = DB.GetDataTable(query, parameter)

                If fn IsNot Nothing Then
                    For Each row As DataRow In fn.Rows
                        If row IsNot Nothing Then
                            Select Case Mid(row(0), 1, 2)
                                Case "ON"
                                    OtherNarrative = True
                                Case "OP"
                                    OtherPermit = True
                            End Select
                        End If
                    Next
                End If

                If OtherNarrative Then
                    chbOtherNarrative.Checked = True
                End If
                If OtherPermit Then
                    chbOtherPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region " Some Checkboxes "

    Private Sub chbTVNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVNarrative.CheckedChanged
        Try

            If chbTVNarrative.Checked AndAlso MasterApp <> "" Then

                txtTVNarrativeDoc.Visible = True
                txtTVNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @filename ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then null " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @filename ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then null " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "VN-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtTVNarrativeDoc.Text = "On File" OrElse txtTVNarrativePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbTVDraft_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVDraft.CheckedChanged
        Try

            If chbTVDraft.Checked AndAlso MasterApp <> "" Then
                txtTVDraftDoc.Visible = True
                txtTVDraftPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @filename ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then null " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numuserID = strPDFModifingPerson " &
                "and strFileName = @filename) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then null " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "VD-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtTVDraftDoc.Text = "On File" OrElse txtTVDraftPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbTVPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVPublicNotice.CheckedChanged
        Try

            If chbTVPublicNotice.Checked AndAlso MasterApp <> "" Then
                txtTVPublicNoticeDoc.Visible = True
                txtTVPublicNoticePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName, ' ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @filename ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then null " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @filename ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then null " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "VP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtTVPublicNoticeDoc.Text = "On File" OrElse txtTVPublicNoticePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbTVFinal_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVFinal.CheckedChanged
        Try

            If chbTVFinal.Checked AndAlso MasterApp <> "" Then
                txtTVFinalDoc.Visible = True
                txtTVFinalPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "VF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtTVFinalDoc.Text = "On File" OrElse txtTVFinalPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDApplicationSummary_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDApplicationSummary.CheckedChanged
        Try

            If chbPSDApplicationSummary.Checked AndAlso MasterApp <> "" Then
                txtPSDAppSummaryDoc.Visible = True
                txtPSDAppSummaryPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "VF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDAppSummaryDoc.Text = "On File" OrElse txtPSDAppSummaryPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDPrelimDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPrelimDet.CheckedChanged
        Try

            If chbPSDPrelimDet.Checked AndAlso MasterApp <> "" Then
                txtPSDPrelimDetDoc.Visible = True
                txtPSDPrelimDetPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDPrelimDetDoc.Text = "On File" OrElse txtPSDPrelimDetPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDNarrative.CheckedChanged
        Try

            If chbPSDNarrative.Checked AndAlso MasterApp <> "" Then
                txtPSDNarrativeDoc.Visible = True
                txtPSDNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename )" &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PT-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDNarrativeDoc.Text = "On File" OrElse txtPSDNarrativePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDDraftPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDDraftPermit.CheckedChanged
        Try

            If chbPSDDraftPermit.Checked AndAlso MasterApp <> "" Then
                txtPSDDraftPermitDoc.Visible = True
                txtPSDDraftPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PD-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDDraftPermitDoc.Text = "On File" OrElse txtPSDDraftPermitPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPublicNotice.CheckedChanged
        Try

            If chbPSDPublicNotice.Checked AndAlso MasterApp <> "" Then
                txtPSDPublicNoticeDoc.Visible = True
                txtPSDPublicNoticePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @filename ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then null " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @filename ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then null " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PN-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDPublicNoticeDoc.Text = "On File" OrElse txtPSDPublicNoticePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDHearingNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDHearingNotice.CheckedChanged
        Try

            If chbPSDHearingNotice.Checked AndAlso MasterApp <> "" Then
                txtPSDHearingNoticeDoc.Visible = True
                txtPSDHearingNoticePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numuserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PH-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDHearingNoticeDoc.Text = "On File" OrElse txtPSDHearingNoticePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDFinalDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalDet.CheckedChanged
        Try

            If chbPSDFinalDet.Checked AndAlso MasterApp <> "" Then
                txtPSDFinalDetDoc.Visible = True
                txtPSDFinalDetPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDFinalDetDoc.Text = "On File" OrElse txtPSDFinalDetPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbPSDFinalPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalPermit.CheckedChanged
        Try

            If chbPSDFinalPermit.Checked AndAlso MasterApp <> "" Then
                txtPSDFinalPermitDoc.Visible = True
                txtPSDFinalPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @filename ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then null " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @filename ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then null " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "PF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtPSDFinalPermitDoc.Text = "On File" OrElse txtPSDFinalPermitPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbOtherNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherNarrative.CheckedChanged
        Try
            If chbOtherNarrative.Checked AndAlso MasterApp <> "" Then
                txtOtherNarrativeDoc.Visible = True
                txtOtherNarrativePDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                 "case " &
                 "when docPermitData is Null then null " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then null " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then null " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then null " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUSerProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUSerProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @filename ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then null " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "ON-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtOtherNarrativeDoc.Text = "On File" OrElse txtOtherNarrativePDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbOtherPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherPermit.CheckedChanged
        Try

            If chbOtherPermit.Checked AndAlso MasterApp <> "" Then
                txtOtherPermitDoc.Visible = True
                txtOtherPermitPDF.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                Dim query As String = "select " &
                  "case " &
                  "when docPermitData is Null then null " &
                  "Else 'True' " &
                  "End DocData, " &
                  "case " &
                  "when strDocModifingPerson is Null then null " &
                  "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                  "from APBPermits, EPDUserProfiles " &
                  "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                  "and numUserID = strDocModifingPerson " &
                  "and strFileName = @filename ) " &
                  "end DocStaffResponsible, " &
                  "case " &
                  "when datDocModifingDate is Null then null " &
                  "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                  "End datDocModifingDate, " &
                  "case " &
                  "when pdfPermitData is Null then null " &
                  "Else 'True' " &
                  "End PDFData, " &
                  "case " &
                  "when strPDFModifingPerson is Null then null " &
                  "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                  "from APBPermits, epduserprofiles " &
                  "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                  "and numUserID = strPDFModifingPerson " &
                  "and strFileName = @filename ) " &
                  "end PDFStaffResponsible, " &
                  "case " &
                  "when datPDFModifingDate is Null then null " &
                  "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                  "End datPDFModifingDate " &
                  "from APBPermits " &
                  "where strFileName = @filename "

                Dim parameter As New SqlParameter("@filename", "OP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(query, parameter)

                If dr IsNot Nothing Then
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

                If txtOtherPermitDoc.Text = "On File" OrElse txtOtherPermitPDF.Text = "On File" Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Download files "

    Private Sub btnOtherNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnOtherNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherNarrativeDoc.Text = "On File" OrElse txtOtherNarrativePDF.Text = "On File") Then
                If txtOtherNarrativeDoc.Text = "On File" AndAlso txtOtherNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOtherPermitDownload_Click(sender As Object, e As EventArgs) Handles btnOtherPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherPermitDoc.Text = "On File" OrElse txtOtherPermitPDF.Text = "On File") Then
                If txtOtherPermitDoc.Text = "On File" AndAlso txtOtherPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnTVNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnTVNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVNarrativeDoc.Text = "On File" OrElse txtTVNarrativePDF.Text = "On File") Then
                If txtTVNarrativeDoc.Text = "On File" AndAlso txtTVNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnTVDraftDownload_Click(sender As Object, e As EventArgs) Handles btnTVDraftDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVDraftDoc.Text = "On File" OrElse txtTVDraftPDF.Text = "On File") Then
                If txtTVDraftDoc.Text = "On File" AndAlso txtTVDraftPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnTVPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnTVPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVPublicNoticeDoc.Text = "On File" OrElse txtTVPublicNoticePDF.Text = "On File") Then
                If txtTVPublicNoticeDoc.Text = "On File" AndAlso txtTVPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnTVFinalDownload_Click(sender As Object, e As EventArgs) Handles btnTVFinalDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVFinalDoc.Text = "On File" OrElse txtTVFinalPDF.Text = "On File") Then
                If txtTVFinalDoc.Text = "On File" AndAlso txtTVFinalPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDAppSummaryDownload_Click(sender As Object, e As EventArgs) Handles btnPSDAppSummaryDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDAppSummaryDoc.Text = "On File" OrElse txtPSDAppSummaryPDF.Text = "On File") Then
                If txtPSDAppSummaryDoc.Text = "On File" AndAlso txtPSDAppSummaryPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDPrelimDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPrelimDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPrelimDetDoc.Text = "On File" OrElse txtPSDPrelimDetPDF.Text = "On File") Then
                If txtPSDPrelimDetDoc.Text = "On File" AndAlso txtPSDPrelimDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDNarrativeDoc.Text = "On File" OrElse txtPSDNarrativePDF.Text = "On File") Then
                If txtPSDNarrativeDoc.Text = "On File" AndAlso txtPSDNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDDraftPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDDraftPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDDraftPermitDoc.Text = "On File" OrElse txtPSDDraftPermitPDF.Text = "On File") Then
                If txtPSDDraftPermitDoc.Text = "On File" AndAlso txtPSDDraftPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPublicNoticeDoc.Text = "On File" OrElse txtPSDPublicNoticePDF.Text = "On File") Then
                If txtPSDPublicNoticeDoc.Text = "On File" AndAlso txtPSDPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDHearingNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDHearingNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDHearingNoticeDoc.Text = "On File" OrElse txtPSDHearingNoticePDF.Text = "On File") Then
                If txtPSDHearingNoticeDoc.Text = "On File" AndAlso txtPSDHearingNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDFinalDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalDetDoc.Text = "On File" OrElse txtPSDFinalDetPDF.Text = "On File") Then
                If txtPSDFinalDetDoc.Text = "On File" AndAlso txtPSDFinalDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPSDFinalPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalPermitDoc.Text = "On File" OrElse txtPSDFinalPermitPDF.Text = "On File") Then
                If txtPSDFinalPermitDoc.Text = "On File" AndAlso txtPSDFinalPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbNewLine &
                    "If you want to download the pdf file type 'pdf'." & vbNewLine &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub llbPermitNumber_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbPermitNumber.LinkClicked
        Try
            Dim temp As String = ""
            Dim URL As String = ""
            Dim PDFFile As String = ""

            Dim query As String = "SELECT strFileName
                                         , strDocFileSize
                                         , strPDFFileSize
                                    FROM   APBpermits
                                    WHERE  strFileName LIKE @MasterAppFn
                                           AND (strFileName LIKE '%VF%'
                                                OR strFileName LIKE '%PI%'
                                                OR strFileName LIKE '%OP%') "

            Dim parameter As SqlParameter() = {
                New SqlParameter("@MasterAppFn", "%-" & MasterApp)
            }

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
                temp = dr.Item("strFileName")
                If IsDBNull(dr.Item("strPDFFileSize")) Then
                    PDFFile = ""
                Else
                    PDFFile = dr.Item("strPDFFileSize")
                End If
            End If

            Select Case Mid(temp, 1, 1)
                Case "V"
                    If PDFFile <> "" Then
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=PDF-VF-" & MasterApp
                    Else
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=DOC-VF-" & MasterApp
                    End If
                Case "P"
                    If PDFFile <> "" Then
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=PDF-PI-" & MasterApp
                    Else
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=DOC-PI-" & MasterApp
                    End If
                Case Else
                    If PDFFile <> "" Then
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=PDF-OP-" & MasterApp
                    Else
                        URL = "https://permitsearch.gaepd.org/permit.aspx?id=DOC-OP-" & MasterApp
                    End If
            End Select

            If URL <> "" Then OpenUrl(New Uri(URL), Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGetCurrentPermittingContact_Click(sender As Object, e As EventArgs) Handles btnGetCurrentPermittingContact.Click
        Try
            If AirsId Is Nothing Then
                MessageBox.Show("Select a facility first.")
                Return
            End If

            Dim query As String = "Select " &
             "strContactFirstName, " &
             "strContactLastName, " &
             "strContactPrefix, " &
             "strContactSuffix, " &
             "strContactTitle, " &
             "strContactCompanyName, " &
             "strContactPhoneNumber1, " &
             "strContactFaxNumber, " &
             "strContactEmail, " &
             "strContactAddress1, " &
             "strContactCity, " &
             "strContactState, " &
             "strContactZipCode, " &
             "strContactDescription " &
             "from APBContactInformation " &
             "where strContactKey = @pKey "

            Dim parameter As New SqlParameter("@pKey", AirsId.DbFormattedString & "30")

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
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
                    txtContactPhoneNumber.Clear()
                Else
                    txtContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
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
                txtContactDescription.Text = "From App #- " & AppNumber & vbNewLine & txtContactDescription.Text
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEmailAcknowledgmentLetter_Click(sender As Object, e As EventArgs) Handles btnEmailAcknowledgmentLetter.Click
        Try
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim StaffPhone As String = ""
            Dim StaffEmail As String = ""

            If Not txtContactEmailAddress.Text.IsValidEmailAddress Then
                MessageBox.Show("The contact email address is not valid. Please enter a valid email and try again.", "Email not sent", MessageBoxButtons.OK)
                Return
            End If

            Me.Cursor = Cursors.AppStarting

            Dim query As String = "select " &
                "strEmailAddress, strPhone " &
                "from EPDUserProfiles " &
                "where numUserID = @UserGCode "

            Dim parameter As New SqlParameter("@UserGCode", CurrentUser.UserID)

            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            If dr IsNot Nothing Then
                StaffPhone = FormatDigitsAsPhoneNumber(DBUtilities.GetNullable(Of String)(dr.Item("strPhone")), True)
                StaffEmail = DBUtilities.GetNullable(Of String)(dr.Item("strEmailAddress"))
            End If

            Subject = "GA Air Application No. " & AppNumber.ToString & ", dated: " & DTPDateSent.Text

            Body = "This is to acknowledge the receipt of your GA Air Quality Permit application for " &
                txtFacilityName.Text & " (Airs No. " & AirsId?.FormattedString & ") in " & cboFacilityCity.Text &
                ", GA. After our initial review of the information and technical data in this application, " &
                "we will notify you if more information is needed to complete " &
                "the application so that we can finish our review. " &
                vbNewLine & vbNewLine &
                "Other environmental permits may be required. For Industrial Stormwater permits, contact " &
                "the Watershed Protection Branch at (404) 675-1605; for Solid Waste permits, contact the " &
                "Land Protection Branch at (404) 362-2537. For more info, https://epd.georgia.gov" &
                vbNewLine & vbNewLine &
                "GEOS, the new web-based permit application system is now operational at: " &
                "https://geos.epd.georgia.gov/GA/GEOS/Public/EnSuite/Shared/Pages/Main/Login.aspx " &
                vbNewLine & vbNewLine &
                "To track the status of the air quality permit application, log on to Georgia Environmental " &
                "Protection Division's Georgia Environmental Connections Online (GECO) at the web address " &
                "https://geco.gaepd.org/ (registration required) and follow the online instructions." &
                vbNewLine & vbNewLine &
                "If your company qualifies as a small business (generally those with fewer than 100 " &
                "employees), you may contact our Small Business Environmental Assistance Program at " &
                "(404) 362-4842 for free and confidential permitting assistance." &
                vbNewLine & vbNewLine &
                "If you have any questions or concerns regarding your application, please contact me at " &
                StaffPhone & " or via e-mail at " & StaffEmail & ". Any written correspondence " &
                "should reference the above application number that has been assigned to this application " &
                "and the facility's AIRS number."

            Select Case CreateEmail(Subject, Body, {txtContactEmailAddress.Text})

                Case CreateEmailResult.Failure, CreateEmailResult.FunctionError
                    MessageBox.Show("There was an error sending the message. Please try again.", "Error", MessageBoxButtons.OK)

                Case CreateEmailResult.InvalidEmail
                    MessageBox.Show("The email address is not valid.", "Application Tracking Log", MessageBoxButtons.OK)

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Me.Cursor = Nothing
        End Try
    End Sub

#Region "SIP Subpart"

    Private Sub LoadSSPPSIPSubPartInformation()
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0
            Dim query As String
            Dim parameter As SqlParameter()

            dgvSIPSubParts.Rows.Clear()
            dgvSIPSubParts.Columns.Clear()
            dgvSIPSubPartDelete.Rows.Clear()
            dgvSIPSubPartDelete.Columns.Clear()
            dgvSIPSubpartAddEdit.Rows.Clear()
            dgvSIPSubpartAddEdit.Columns.Clear()

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

            If AirsId IsNot Nothing Then
                query = "select distinct   " &
                "strAIRSnumber, " &
                "'' as AppNum, " &
                "apbsubpartdata.strSubpart, " &
                "strDescription, CreateDateTime " &
                "from APBsubpartdata, LookUpSubPartSIP   " &
                "where APBSubpartData.strSubPart = LookUpSubpartSIP.strSubpart   " &
                "and APBSubPartData.strSubpartKey = @pKey " &
                "and Active = '1' "

                parameter = {New SqlParameter("@pKey", AirsId.DbFormattedString & "0")}

                Dim dt As DataTable = DB.GetDataTable(query, parameter)

                For Each dr As DataRow In dt.Rows
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
                Next

                If dgvSIPSubParts.RowCount > 0 Then
                    For i = 0 To dgvSIPSubParts.RowCount - 1
                        SubPart = dgvSIPSubParts.Item(1, i).Value

                        query = "select " &
                        "SSPPSubpartData.strApplicationNumber,  " &
                        "strSubpart, strApplicationActivity,   " &
                        "CreateDateTime " &
                        "from SSPPApplicationMaster, SSPPSubpartData   " &
                        "where SSPPSubpartData.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber  " &
                        "and strAIRSnumber = @airs " &
                        "and SUBSTRING(strSubpartkey, 6,1) = '0'  " &
                        "and strSubpart = @SubPart " &
                        "and SSPPSubpartData.strApplicationNumber  = @appnum " &
                        "order by createdatetime "

                        parameter = {
                            New SqlParameter("@airs", AirsId.DbFormattedString),
                            New SqlParameter("@SubPart", SubPart),
                            New SqlParameter("@appnum", AppNumber)
                        }

                        Dim dr As DataRow = DB.GetDataRow(query, parameter)

                        If dr IsNot Nothing Then
                            If Not IsDBNull(dr.Item("strApplicationNumber")) Then
                                dgvSIPSubParts(0, i).Value = dr.Item("strApplicationNumber")
                            End If
                            If Not IsDBNull(dr.Item("strApplicationActivity")) Then
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
                            If Not IsDBNull(dr.Item("CreateDateTime")) Then
                                dgvSIPSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                            End If
                        End If
                    Next
                End If
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

            query = "select  " &
            "strAIRSNumber, " &
            "SSPPSubpartData.strApplicationNumber,  " &
            "SSPPSubPartData.strSubpart, strDescription,  " &
            "case when strApplicationActivity = '0' then 'Removed'  " &
            "when strApplicationActivity ='1' then 'Added'  " &
            "when strApplicationActivity = '2' then 'Modified'  " &
            "else strApplicationActivity  " &
            "end Action,  " &
            "CreatedateTime  " &
            "from SSPPSubpartData, SSPPApplicationMaster,   " &
            "LookUpSubPartSIP   " &
            "where SSPPApplicationMaster.strApplicationNumber = " &
            "SSPPSubpartData.strApplicationNumber   " &
            "and SSPPSubPartData.strSubPart = LookUpSubPartSIP.strSubPart  " &
            "and SSPPSubpartData.strSubpartKey  = @pKey "

            parameter = {New SqlParameter("@pKey", AppNumber & "0")}

            Dim dt2 As DataTable = DB.GetDataTable(query, parameter)

            For Each dr As DataRow In dt2.Rows
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
                            If SubPart = dgvSIPSubParts(1, i).Value.ToString Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                dgvSIPSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
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
                            dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                        End If
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            If SubPart = dgvSIPSubParts(1, i).Value.ToString Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                dgvSIPSubParts(4, i).Value = "Added"
                                temp = "Added"
                                dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                            dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                        End If
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvSIPSubpartAddEdit.Rows.Count - 1
                        dgvSIPSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvSIPSubParts.Rows.Count - 1
                            If SubPart = dgvSIPSubParts(1, i).Value.ToString Then
                                dgvSIPSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
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
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPDelete_Click(sender As Object, e As EventArgs) Handles btnSIPDelete.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
            Action = dgvSIPSubParts(4, dgvSIPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                If dgvSIPSubpartAddEdit(0, i).Value.ToString = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be deleted from the Facility.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            Else
                temp2 = ""
            End If

            i = dgvSIPSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value

                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPUndelete_Click(sender As Object, e As EventArgs) Handles btnSIPUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvSIPSubPartDelete.CurrentRow Is Nothing Then
                Return
            End If

            If dgvSIPSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvSIPSubPartDelete(0, dgvSIPSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                dgvSIPSubPartDelete.Rows.Remove(dgvSIPSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPDeleteAll_Click(sender As Object, e As EventArgs) Handles btnSIPDeleteAll.Click
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
                    If dgvSIPSubpartAddEdit(0, j).Value.ToString = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be deleted from the Facility.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, j).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvSIPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                    dgvSIPSubPartDelete.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPUndeleteAll_Click(sender As Object, e As EventArgs) Handles btnSIPUndeleteAll.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvSIPSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearSIPDeletes_Click(sender As Object, e As EventArgs) Handles btnClearSIPDeletes.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvSIPSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewSIPSubpart_Click(sender As Object, e As EventArgs) Handles btnAddNewSIPSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If Not chbCDS_0.Checked Then
                MsgBox("The SIP Subpart is not checked on the Tracking Log tab. " & vbNewLine &
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation,
                        "Application Tracking")
                Return
            End If

            Subpart = cboSIPSubpart.SelectedValue.ToString
            If Subpart <> "" Then
                Desc = cboSIPSubpart.Text
            End If

            temp2 = ""
            For i = 0 To dgvSIPSubParts.Rows.Count - 1
                If dgvSIPSubParts(1, i).Value.ToString = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The SIP Subpart already exists for this application.", MsgBoxStyle.Information,
                           "Application Tracking")
                    Return
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSIPSubParts)
                dgvRow.Cells(0).Value = AppNumber
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = TodayFormatted
                dgvRow.Cells(4).Value = "Added"
                dgvSIPSubParts.Rows.Add(dgvRow)
                i = dgvSIPSubParts.Rows.Count - 1
                dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If

            temp2 = ""
            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                If dgvSIPSubpartAddEdit(1, i).Value.ToString = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = TodayFormatted
                dgvRow.Cells(3).Value = "Added"
                dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvSIPSubpartAddEdit.Rows.Count - 1
                dgvSIPSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPEdit_Click(sender As Object, e As EventArgs) Handles btnSIPEdit.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
            Action = dgvSIPSubParts(4, dgvSIPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                If dgvSIPSubPartDelete(0, i).Value.ToString = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be Modified by this Application.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            Else
                temp2 = ""
            End If

            i = dgvSIPSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    If dgvSIPSubpartAddEdit(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvSIPSubParts(3, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvSIPSubParts(3, dgvSIPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(dgvSIPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPUnedit_Click(sender As Object, e As EventArgs) Handles btnSIPUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvSIPSubpartAddEdit.CurrentRow Is Nothing Then
                Return
            End If

            If dgvSIPSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvSIPSubpartAddEdit(0, dgvSIPSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvSIPSubpartAddEdit(3, dgvSIPSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    dgvSIPSubpartAddEdit.Rows.Remove(dgvSIPSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPEditAll_Click(sender As Object, e As EventArgs) Handles btnSIPEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                Subpart = dgvSIPSubParts(1, i).Value
                Action = dgvSIPSubParts(4, i).Value

                For j As Integer = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                    If dgvSIPSubPartDelete(0, j).Value.ToString = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be Modified by this Application.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    If dgvSIPSubpartAddEdit(0, j).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvSIPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                    dgvRow.Cells(2).Value = dgvSIPSubParts(3, i).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvSIPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSIPUneditAll_Click(sender As Object, e As EventArgs) Handles btnSIPUneditAll.Click
        Try
            Dim i As Integer
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvSIPSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart AndAlso dgvSIPSubParts(4, j).Value.ToString = "Existing" Then
                        dgvSIPSubParts.Rows(j).DefaultCellStyle.BackColor = Color.White
                        TempRemove = i & "," & TempRemove
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvSIPSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = TempRemove?.Replace(i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddModifiedSIPs_Click(sender As Object, e As EventArgs) Handles btnClearAddModifiedSIPs.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvSIPSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvSIPSubParts.Rows.Count - 1
                    If dgvSIPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                        Action = dgvSIPSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    If Action = "Added" Then
                        dgvSIPSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvSIPSubpartAddEdit.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveSIPSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            DeleteProgramSubparts(AppNumber, "0")

            For i = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                Subpart = dgvSIPSubPartDelete(0, i).Value
                SaveProgramSubpartData(AppNumber, "0", Subpart, "0")
            Next

            For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                Action = dgvSIPSubpartAddEdit(3, i).Value
                Subpart = dgvSIPSubpartAddEdit(0, i).Value

                Select Case Action
                    Case "Added"
                        SaveProgramSubpartData(AppNumber, "0", Subpart, "1")
                    Case "Modify"
                        SaveProgramSubpartData(AppNumber, "0", Subpart, "2")
                End Select
            Next

            LoadSSPPSIPSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveSIPSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveSIPSubpart.Click
        Try
            If Not chbCDS_0.Checked Then
                MsgBox("WARNING DATA NOT SAVED:" & vbNewLine &
                       "On the Tracking Log tab select the air program code 0 - SIP. " &
                       "If you do not check this air program code the subparts cannot be saved.",
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            End If
            SaveSIPSubpart()
            MsgBox("SIP Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "NSPS Subpart"

    Private Sub LoadSSPPNSPSSubPartInformation()
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0
            Dim query As String
            Dim parameter As SqlParameter()

            dgvNSPSSubParts.Rows.Clear()
            dgvNSPSSubParts.Columns.Clear()
            dgvNSPSSubPartDelete.Rows.Clear()
            dgvNSPSSubPartDelete.Columns.Clear()
            dgvNSPSSubpartAddEdit.Rows.Clear()
            dgvNSPSSubpartAddEdit.Columns.Clear()

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

            If AirsId IsNot Nothing Then
                query = "select
                    STRAIRSNUMBER,
                    '' as AppNum,
                    s.STRSUBPART,
                    ICIS_PROGRAM_SUBPART_DESC as strDescription,
                    CREATEDATETIME
                from APBSUBPARTDATA s
                    inner join LK_ICIS_PROGRAM_SUBPART l
                        on s.STRSUBPART = l.LK_SUBPART_CODE
                           and right(STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE
                where s.STRAIRSNUMBER = @airs
                      and l.LGCY_PROGRAM_CODE = @key
                      and ACTIVE = '1' "

                parameter = {
                New SqlParameter("@airs", AirsId.DbFormattedString),
                New SqlParameter("@key", "9")
            }

                Dim dt As DataTable = DB.GetDataTable(query, parameter)

                For Each dr As DataRow In dt.Rows
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
                Next

                If dgvNSPSSubParts.RowCount > 0 Then
                    For i = 0 To dgvNSPSSubParts.RowCount - 1
                        SubPart = dgvNSPSSubParts.Item(1, i).Value

                        query = "select
                        s.STRAPPLICATIONNUMBER,
                        strSubpart,
                        strApplicationActivity,
                        CREATEDATETIME
                    from SSPPApplicationMaster m
                        inner join SSPPSubpartData s
                            on s.strApplicationNumber = m.strApplicationNumber
                    where strAIRSnumber = @airsnum
                          and right(strSubpartkey, 1) = @key
                          and strSubpart = @subpart
                          and s.strApplicationNumber = @appnum
                    order by createdatetime "

                        parameter = {
                        New SqlParameter("@airsnum", AirsId.DbFormattedString),
                        New SqlParameter("@key", "9"),
                        New SqlParameter("@subpart", SubPart),
                        New SqlParameter("@appnum", AppNumber)
                    }

                        Dim dr As DataRow = DB.GetDataRow(query, parameter)

                        If dr IsNot Nothing Then
                            If Not IsDBNull(dr.Item("strApplicationNumber")) Then
                                dgvNSPSSubParts(0, i).Value = dr.Item("strApplicationNumber")
                            End If
                            If Not IsDBNull(dr.Item("strApplicationActivity")) Then
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
                            If Not IsDBNull(dr.Item("CreateDateTime")) Then
                                dgvNSPSSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                            End If
                        End If

                    Next
                End If
            End If

            dgvNSPSSubPartDelete.RowHeadersVisible = False
            dgvNSPSSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSSubPartDelete.AllowUserToResizeColumns = True
            dgvNSPSSubPartDelete.AllowUserToAddRows = False
            dgvNSPSSubPartDelete.AllowUserToDeleteRows = False
            dgvNSPSSubPartDelete.AllowUserToOrderColumns = True
            dgvNSPSSubPartDelete.AllowUserToResizeRows = True
            dgvNSPSSubPartDelete.ColumnHeadersHeight = 35

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
            dgvNSPSSubpartAddEdit.ColumnHeadersHeight = 35

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

            query = "select
                strAIRSNumber,
                s.strApplicationNumber,
                s.strSubpart,
                l.ICIS_PROGRAM_SUBPART_DESC as strDescription,
                case when strApplicationActivity = '0'
                    then 'Removed'
                when strApplicationActivity = '1'
                    then 'Added'
                when strApplicationActivity = '2'
                    then 'Modified'
                else strApplicationActivity
                end                            Action,
                CreatedateTime
            from SSPPSubpartData s
                inner join SSPPApplicationMaster m
                    on s.STRAPPLICATIONNUMBER = m.STRAPPLICATIONNUMBER
                inner join LK_ICIS_PROGRAM_SUBPART l
                    on l.LK_SUBPART_CODE = s.STRSUBPART
                       and right(s.STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE
            where m.STRAPPLICATIONNUMBER = @appnum
                  and l.LGCY_PROGRAM_CODE = @key "

            parameter = {
                New SqlParameter("@appnum", AppNumber),
                New SqlParameter("@key", "9")
            }

            Dim dt2 As DataTable = DB.GetDataTable(query, parameter)

            For Each dr As DataRow In dt2.Rows
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
                            If SubPart = dgvNSPSSubParts(1, i).Value.ToString Then
                                dgvNSPSSubParts(0, i).Value = AppNum
                                dgvNSPSSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
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
                            dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            If SubPart = dgvNSPSSubParts(1, i).Value.ToString Then
                                dgvNSPSSubParts(4, i).Value = "Added"
                                temp = "Added"
                                dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                            dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvNSPSSubpartAddEdit.Rows.Count - 1
                        dgvNSPSSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                            If SubPart = dgvNSPSSubParts(1, i).Value.ToString Then
                                dgvNSPSSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                            End If
                        Next

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                End Select
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSDelete_Click(sender As Object, e As EventArgs) Handles btnNSPSDelete.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
            Action = dgvNSPSSubParts(4, dgvNSPSSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                If dgvNSPSSubpartAddEdit(0, i).Value.ToString = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be deleted from the Facility.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            Else
                temp2 = ""
            End If

            i = dgvNSPSSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value

                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSUndelete_Click(sender As Object, e As EventArgs) Handles btnNSPSUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvNSPSSubPartDelete.CurrentRow Is Nothing Then
                Return
            End If

            If dgvNSPSSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvNSPSSubPartDelete(0, dgvNSPSSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                dgvNSPSSubPartDelete.Rows.Remove(dgvNSPSSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSDeleteAll_Click(sender As Object, e As EventArgs) Handles btnNSPSDeleteAll.Click
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
                    If dgvNSPSSubpartAddEdit(0, j).Value.ToString = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be deleted from the Facility.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, j).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvNSPSSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                    dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSUndeleteAll_Click(sender As Object, e As EventArgs) Handles btnNSPSUndeleteAll.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvNSPSSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearNSPSDeletes_Click(sender As Object, e As EventArgs) Handles btnClearNSPSDeletes.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvNSPSSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewNSPSSubpart_Click(sender As Object, e As EventArgs) Handles btnAddNewNSPSSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If Not chbCDS_9.Checked Then
                MsgBox("The NSPS Subpart is not checked on the Tracking Log tab. " & vbNewLine &
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation,
                        "Application Tracking")
                Return
            End If

            Subpart = cboNSPSSubpart.SelectedValue.ToString
            If Subpart <> "" Then
                Desc = cboNSPSSubpart.Text
            End If

            temp2 = ""
            For i = 0 To dgvNSPSSubParts.Rows.Count - 1
                If dgvNSPSSubParts(1, i).Value.ToString = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The NSPS Subpart already exists for this application.", MsgBoxStyle.Information,
                        "Application Tracking")
                    Return
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSSubParts)
                dgvRow.Cells(0).Value = AppNumber
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = TodayFormatted
                dgvRow.Cells(4).Value = "Added"
                dgvNSPSSubParts.Rows.Add(dgvRow)
                i = dgvNSPSSubParts.Rows.Count - 1
                dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If

            temp2 = ""
            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                If dgvNSPSSubpartAddEdit(1, i).Value.ToString = Subpart Then
                    temp2 = "Ignore"
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                dgvRow.Cells(0).Value = Subpart
                dgvRow.Cells(1).Value = Desc
                dgvRow.Cells(2).Value = TodayFormatted
                dgvRow.Cells(3).Value = "Added"
                dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvNSPSSubpartAddEdit.Rows.Count - 1
                dgvNSPSSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSEdit_Click(sender As Object, e As EventArgs) Handles btnNSPSEdit.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
            Action = dgvNSPSSubParts(4, dgvNSPSSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                If dgvNSPSSubPartDelete(0, i).Value.ToString = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be Modified by this Application.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            Else
                temp2 = ""
            End If

            i = dgvNSPSSubpartAddEdit.Rows.Count

            If i > 0 Then
                temp = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    If dgvNSPSSubpartAddEdit(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNSPSSubParts(3, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNSPSSubParts(3, dgvNSPSSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(dgvNSPSSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSUnedit_Click(sender As Object, e As EventArgs) Handles btnNSPSUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNSPSSubpartAddEdit.CurrentRow Is Nothing Then
                Return
            End If

            If dgvNSPSSubpartAddEdit.Rows.Count > 0 Then
                Subpart = dgvNSPSSubpartAddEdit(0, dgvNSPSSubpartAddEdit.CurrentRow.Index).Value
                Action = dgvNSPSSubpartAddEdit(3, dgvNSPSSubpartAddEdit.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                If Action <> "Added" Then
                    dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    dgvNSPSSubpartAddEdit.Rows.Remove(dgvNSPSSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSEditAll_Click(sender As Object, e As EventArgs) Handles btnNSPSEditAll.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                Subpart = dgvNSPSSubParts(1, i).Value
                Action = dgvNSPSSubParts(4, i).Value

                For j As Integer = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                    If dgvNSPSSubPartDelete(0, j).Value.ToString = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be Modified by this Application.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    If dgvNSPSSubpartAddEdit(0, j).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvNSPSSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                    dgvRow.Cells(2).Value = dgvNSPSSubParts(3, i).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNSPSSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNSPSUneditAll_Click(sender As Object, e As EventArgs) Handles btnNSPSUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvNSPSSubParts.Rows.Count - 1
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart AndAlso dgvNSPSSubParts(4, j).Value.ToString = "Existing" Then
                        dgvNSPSSubParts.Rows(j).DefaultCellStyle.BackColor = Color.White
                        TempRemove = i & "," & TempRemove
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvNSPSSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = TempRemove?.Replace(i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddModifiedNSPSs_Click(sender As Object, e As EventArgs) Handles btnClearAddModifiedNSPSs.Click
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
                    If dgvNSPSSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                        Action = dgvNSPSSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    If Action = "Added" Then
                        dgvNSPSSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvNSPSSubpartAddEdit.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveNSPSSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            DeleteProgramSubparts(AppNumber, "9")

            For i = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                SaveProgramSubpartData(AppNumber, "9", Subpart, "0")
            Next

            For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                Action = dgvNSPSSubpartAddEdit(3, i).Value
                Subpart = dgvNSPSSubpartAddEdit(0, i).Value

                Select Case Action
                    Case "Added"
                        SaveProgramSubpartData(AppNumber, "9", Subpart, "1")
                    Case "Modify"
                        SaveProgramSubpartData(AppNumber, "9", Subpart, "2")
                End Select
            Next

            LoadSSPPNSPSSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveNSPSSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveNSPSSubpart.Click
        Try
            If Not chbCDS_9.Checked Then
                MsgBox("WARNING DATA NOT SAVED:" & vbNewLine &
                       "On the Tracking Log tab select the air program code 9 - NSPS. " &
                       "If you do not check this air program code the subparts cannot be saved.",
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            End If
            SaveNSPSSubpart()
            MsgBox("NSPS Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "NESHAP Subpart"

    Private Sub LoadSSPPNESHAPSubPartInformation()
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0
            Dim query As String
            Dim parameter As SqlParameter()

            dgvNESHAPSubParts.Rows.Clear()
            dgvNESHAPSubParts.Columns.Clear()
            dgvNESHAPSubPartDelete.Rows.Clear()
            dgvNESHAPSubPartDelete.Columns.Clear()
            dgvNESHAPSubpartAddEdit.Rows.Clear()
            dgvNESHAPSubpartAddEdit.Columns.Clear()

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

            If AirsId IsNot Nothing Then
                query = "select 
                    STRAIRSNUMBER, 
                    '' as AppNum, 
                    s.STRSUBPART, 
                    ICIS_PROGRAM_SUBPART_DESC as strDescription, 
                    CREATEDATETIME 
                from APBSUBPARTDATA s 
                    inner join LK_ICIS_PROGRAM_SUBPART l 
                        on s.STRSUBPART = l.LK_SUBPART_CODE 
                           and right(STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE 
                where s.STRAIRSNUMBER = @airs 
                      and l.LGCY_PROGRAM_CODE = @key 
                      and ACTIVE = '1' "

                parameter = {
                New SqlParameter("@airs", AirsId.DbFormattedString),
                New SqlParameter("@key", "8")
            }

                Dim dt As DataTable = DB.GetDataTable(query, parameter)

                For Each dr As DataRow In dt.Rows
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
                Next

                If dgvNESHAPSubParts.RowCount > 0 Then
                    For i = 0 To dgvNESHAPSubParts.RowCount - 1
                        SubPart = dgvNESHAPSubParts.Item(1, i).Value

                        query = "select 
                        s.STRAPPLICATIONNUMBER, 
                        strSubpart, 
                        strApplicationActivity, 
                        CREATEDATETIME 
                    from SSPPApplicationMaster m 
                        inner join SSPPSubpartData s 
                            on s.strApplicationNumber = m.strApplicationNumber 
                    where strAIRSnumber = @airsnum 
                          and right(strSubpartkey, 1) = @key 
                          and strSubpart = @subpart 
                          and s.strApplicationNumber = @appnum 
                    order by createdatetime "

                        parameter = {
                        New SqlParameter("@airsnum", AirsId.DbFormattedString),
                        New SqlParameter("@key", "8"),
                        New SqlParameter("@subpart", SubPart),
                        New SqlParameter("@appnum", AppNumber)
                    }

                        Dim dr As DataRow = DB.GetDataRow(query, parameter)

                        If dr IsNot Nothing Then
                            If Not IsDBNull(dr.Item("strApplicationNumber")) Then
                                dgvNESHAPSubParts(0, i).Value = dr.Item("strApplicationNumber")
                            End If
                            If Not IsDBNull(dr.Item("strApplicationActivity")) Then
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
                            If Not IsDBNull(dr.Item("CreateDateTime")) Then
                                dgvNESHAPSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                            End If
                        End If
                    Next
                End If
            End If

            dgvNESHAPSubPartDelete.RowHeadersVisible = False
            dgvNESHAPSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAPSubPartDelete.AllowUserToResizeColumns = True
            dgvNESHAPSubPartDelete.AllowUserToAddRows = False
            dgvNESHAPSubPartDelete.AllowUserToDeleteRows = False
            dgvNESHAPSubPartDelete.AllowUserToOrderColumns = True
            dgvNESHAPSubPartDelete.AllowUserToResizeRows = True
            dgvNESHAPSubPartDelete.ColumnHeadersHeight = 35

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
            dgvNESHAPSubpartAddEdit.ColumnHeadersHeight = 35

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

            query = "select 
                strAIRSNumber, 
                s.strApplicationNumber, 
                s.strSubpart, 
                l.ICIS_PROGRAM_SUBPART_DESC as strDescription, 
                case when strApplicationActivity = '0' 
                    then 'Removed' 
                when strApplicationActivity = '1' 
                    then 'Added' 
                when strApplicationActivity = '2' 
                    then 'Modified' 
                else strApplicationActivity 
                end                            Action, 
                CreatedateTime 
            from SSPPSubpartData s 
                inner join SSPPApplicationMaster m 
                    on s.STRAPPLICATIONNUMBER = m.STRAPPLICATIONNUMBER 
                inner join LK_ICIS_PROGRAM_SUBPART l 
                    on l.LK_SUBPART_CODE = s.STRSUBPART 
                       and right(s.STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE 
            where m.STRAPPLICATIONNUMBER = @appnum 
                  and l.LGCY_PROGRAM_CODE = @key "

            parameter = {
                New SqlParameter("@appnum", AppNumber),
                New SqlParameter("@key", "8")
            }

            Dim dt2 As DataTable = DB.GetDataTable(query, parameter)

            For Each dr As DataRow In dt2.Rows
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
                            If SubPart = dgvNESHAPSubParts(1, i).Value.ToString Then
                                dgvNESHAPSubParts(0, i).Value = AppNum
                                dgvNESHAPSubParts(4, i).Value = "Removed"
                                temp = "Removed"
                                dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
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
                            dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    Case "Added"
                        temp = ""
                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            If SubPart = dgvNESHAPSubParts(1, i).Value.ToString Then
                                dgvNESHAPSubParts(4, i).Value = "Added"
                                temp = "Added"
                                dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                            dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvNESHAPSubpartAddEdit.Rows.Count - 1
                        dgvNESHAPSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                            If SubPart = dgvNESHAPSubParts(1, i).Value.ToString Then
                                dgvNESHAPSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                            End If
                        Next

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Modify"
                        dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                End Select
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPDelete_Click(sender As Object, e As EventArgs) Handles btnNESHAPDelete.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
            Action = dgvNESHAPSubParts(4, dgvNESHAPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                If dgvNESHAPSubpartAddEdit(0, i).Value.ToString = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be deleted from the Facility.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            Else
                temp2 = ""
            End If

            i = dgvNESHAPSubPartDelete.Rows.Count

            If i > 0 Then
                temp = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value

                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPUndelete_Click(sender As Object, e As EventArgs) Handles btnNESHAPUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvNESHAPSubPartDelete.CurrentRow Is Nothing Then
                Return
            End If

            If dgvNESHAPSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvNESHAPSubPartDelete(0, dgvNESHAPSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                dgvNESHAPSubPartDelete.Rows.Remove(dgvNESHAPSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPDeleteAll_Click(sender As Object, e As EventArgs) Handles btnNESHAPDeleteAll.Click
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
                    If dgvNESHAPSubpartAddEdit(0, j).Value.ToString = Subpart Then
                        temp2 = "Message"
                    End If
                Next
                If temp2 = "Message" Then
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be deleted from the Facility.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                    If dgvNESHAPSubPartDelete(0, j).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                    dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPUndeleteAll_Click(sender As Object, e As EventArgs) Handles btnNESHAPUndeleteAll.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNESHAPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvNESHAPSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearNESHAPDeletes_Click(sender As Object, e As EventArgs) Handles btnClearNESHAPDeletes.Click
        Try
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNESHAPSubPartDelete(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value.ToString = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvNESHAPSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewNESHAPSubpart_Click(sender As Object, e As EventArgs) Handles btnAddNewNESHAPSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If Not chbCDS_8.Checked Then
                MsgBox("The NESHAP Subpart is not checked on the Tracking Log tab. " & vbNewLine &
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation,
                        "Application Tracking")
                Return
            End If

            Subpart = cboNESHAPSubpart.SelectedValue.ToString
            If Subpart <> "" Then
                Desc = cboNESHAPSubpart.Text
            End If

            temp2 = ""
            If dgvNESHAPSubParts.Rows.Count <> 0 Then
                For i = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, i).Value.ToString = Subpart Then
                        temp2 = "Ignore"
                        MsgBox("The NESHAP Subpart already exists for this application.", MsgBoxStyle.Information,
                        "Application Tracking")
                        Return
                    End If
                Next
            End If
            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNESHAPSubParts)
                dgvRow.Cells(0).Value = AppNumber
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = TodayFormatted
                dgvRow.Cells(4).Value = "Added"
                dgvNESHAPSubParts.Rows.Add(dgvRow)
                i = dgvNESHAPSubParts.Rows.Count - 1
                dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                dgvRow.Cells(2).Value = TodayFormatted
                dgvRow.Cells(3).Value = "Added"
                dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvNESHAPSubpartAddEdit.Rows.Count - 1
                dgvNESHAPSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPEdit_Click(sender As Object, e As EventArgs) Handles btnNESHAPEdit.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
            Action = dgvNESHAPSubParts(4, dgvNESHAPSubParts.CurrentRow.Index).Value

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                If dgvNESHAPSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be Modified by this Application.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
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
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, dgvNESHAPSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(dgvNESHAPSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPUnedit_Click(sender As Object, e As EventArgs) Handles btnNESHAPUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvNESHAPSubpartAddEdit.CurrentRow Is Nothing Then
                Return
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
                    dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    dgvNESHAPSubpartAddEdit.Rows.Remove(dgvNESHAPSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPEditAll_Click(sender As Object, e As EventArgs) Handles btnNESHAPEditAll.Click
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
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be Modified by this Application.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                    If dgvNESHAPSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvNESHAPSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                    dgvRow.Cells(2).Value = dgvNESHAPSubParts(3, i).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvNESHAPSubpartAddEdit.Rows.Add(dgvRow)
                    dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNESHAPUneditAll_Click(sender As Object, e As EventArgs) Handles btnNESHAPUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                Subpart = dgvNESHAPSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvNESHAPSubParts.Rows.Count - 1
                    If dgvNESHAPSubParts(1, j).Value = Subpart AndAlso dgvNESHAPSubParts(4, j).Value = "Existing" Then
                        dgvNESHAPSubParts.Rows(j).DefaultCellStyle.BackColor = Color.White
                        TempRemove = i & "," & TempRemove
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvNESHAPSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = TempRemove?.Replace(i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddModifiedNESHAPs_Click(sender As Object, e As EventArgs) Handles btnClearAddModifiedNESHAPs.Click
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
                    If dgvNESHAPSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvNESHAPSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    If Action = "Added" Then
                        dgvNESHAPSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvNESHAPSubpartAddEdit.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveNESHAPSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            DeleteProgramSubparts(AppNumber, "8")

            For i = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                Subpart = dgvNSPSSubPartDelete(0, i).Value
                SaveProgramSubpartData(AppNumber, "8", Subpart, "0")
            Next

            For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                Action = dgvNESHAPSubpartAddEdit(3, i).Value
                Subpart = dgvNESHAPSubpartAddEdit(0, i).Value

                Select Case Action
                    Case "Added"
                        SaveProgramSubpartData(AppNumber, "8", Subpart, "1")
                    Case "Modify"
                        SaveProgramSubpartData(AppNumber, "8", Subpart, "2")
                End Select
            Next

            LoadSSPPNESHAPSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveNESHAPSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveNESHAPSubpart.Click
        Try
            If Not chbCDS_8.Checked Then
                MsgBox("WARNING DATA NOT SAVED:" & vbNewLine &
                       "On the Tracking Log tab select the air program code 8 - NESHAP. " &
                       "If you do not check this air program code the subparts cannot be saved.",
                     MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            End If
            SaveNESHAPSubpart()

            MsgBox("NESHAP Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "MACT Subpart"

    Private Sub LoadSSPPMACTSubPartInformation()
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim AppNum As String = ""
            Dim SubPart As String = ""
            Dim Desc As String = ""
            Dim CreateDateTime As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0
            Dim query As String
            Dim parameter As SqlParameter()

            dgvMACTSubParts.Rows.Clear()
            dgvMACTSubParts.Columns.Clear()
            dgvMACTSubPartDelete.Rows.Clear()
            dgvMACTSubPartDelete.Columns.Clear()
            dgvMACTSubpartAddEdit.Rows.Clear()
            dgvMACTSubpartAddEdit.Columns.Clear()

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

            If AirsId IsNot Nothing Then
                query = "select 
                    STRAIRSNUMBER, 
                    '' as AppNum, 
                    s.STRSUBPART, 
                    ICIS_PROGRAM_SUBPART_DESC as strDescription, 
                    CREATEDATETIME 
                from APBSUBPARTDATA s 
                    inner join LK_ICIS_PROGRAM_SUBPART l 
                        on s.STRSUBPART = l.LK_SUBPART_CODE 
                           and right(STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE 
                where s.STRAIRSNUMBER = @airs 
                      and l.LGCY_PROGRAM_CODE = @key 
                      and ACTIVE = '1' "

                parameter = {
                New SqlParameter("@airs", AirsId.DbFormattedString),
                New SqlParameter("@key", "M")
            }

                Dim dt As DataTable = DB.GetDataTable(query, parameter)

                For Each dr As DataRow In dt.Rows
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
                Next

                If dgvMACTSubParts.RowCount > 0 Then
                    For i = 0 To dgvMACTSubParts.RowCount - 1
                        SubPart = dgvMACTSubParts.Item(1, i).Value

                        query = "select 
                        s.STRAPPLICATIONNUMBER, 
                        strSubpart, 
                        strApplicationActivity, 
                        CREATEDATETIME 
                    from SSPPApplicationMaster m 
                        inner join SSPPSubpartData s 
                            on s.strApplicationNumber = m.strApplicationNumber 
                    where strAIRSnumber = @airsnum 
                          and right(strSubpartkey, 1) = @key 
                          and strSubpart = @subpart 
                          and s.strApplicationNumber = @appnum 
                    order by createdatetime "

                        parameter = {
                        New SqlParameter("@airsnum", AirsId.DbFormattedString),
                        New SqlParameter("@key", "M"),
                        New SqlParameter("@subpart", SubPart),
                        New SqlParameter("@appnum", AppNumber)
                    }

                        Dim dr As DataRow = DB.GetDataRow(query, parameter)

                        If dr IsNot Nothing Then
                            If Not IsDBNull(dr.Item("strApplicationNumber")) Then
                                dgvMACTSubParts(0, i).Value = dr.Item("strApplicationNumber")
                            End If
                            If Not IsDBNull(dr.Item("strApplicationActivity")) Then
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
                            If Not IsDBNull(dr.Item("CreateDateTime")) Then
                                dgvMACTSubParts(3, i).Value = Format(dr.Item("CreateDateTime"), "dd-MMM-yyyy")
                            End If
                        End If
                    Next
                End If
            End If

            dgvMACTSubPartDelete.RowHeadersVisible = False
            dgvMACTSubPartDelete.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACTSubPartDelete.AllowUserToResizeColumns = True
            dgvMACTSubPartDelete.AllowUserToAddRows = False
            dgvMACTSubPartDelete.AllowUserToDeleteRows = False
            dgvMACTSubPartDelete.AllowUserToOrderColumns = True
            dgvMACTSubPartDelete.AllowUserToResizeRows = True
            dgvMACTSubPartDelete.ColumnHeadersHeight = 35

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
            dgvMACTSubpartAddEdit.ColumnHeadersHeight = 35

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

            query = "select 
                strAIRSNumber, 
                s.strApplicationNumber, 
                s.strSubpart, 
                l.ICIS_PROGRAM_SUBPART_DESC as strDescription, 
                case when strApplicationActivity = '0' 
                    then 'Removed' 
                when strApplicationActivity = '1' 
                    then 'Added' 
                when strApplicationActivity = '2' 
                    then 'Modified' 
                else strApplicationActivity 
                end                            Action, 
                CreatedateTime 
            from SSPPSubpartData s 
                inner join SSPPApplicationMaster m 
                    on s.STRAPPLICATIONNUMBER = m.STRAPPLICATIONNUMBER 
                inner join LK_ICIS_PROGRAM_SUBPART l 
                    on l.LK_SUBPART_CODE = s.STRSUBPART 
                       and right(s.STRSUBPARTKEY, 1) = l.LGCY_PROGRAM_CODE 
            where m.STRAPPLICATIONNUMBER = @appnum 
                  and l.LGCY_PROGRAM_CODE = @key "

            parameter = {
                New SqlParameter("@appnum", AppNumber),
                New SqlParameter("@key", "M")
            }

            Dim dt2 As DataTable = DB.GetDataTable(query, parameter)

            For Each dr As DataRow In dt2.Rows
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
                                dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
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
                            dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
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
                                dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                            dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                        End If

                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                        dgvRow.Cells(0).Value = SubPart
                        dgvRow.Cells(1).Value = Desc
                        dgvRow.Cells(2).Value = CreateDateTime
                        dgvRow.Cells(3).Value = "Added"
                        dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                        i = dgvMACTSubpartAddEdit.Rows.Count - 1
                        dgvMACTSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                    Case "Modified"
                        temp = ""
                        For i = 0 To dgvMACTSubParts.Rows.Count - 1
                            If SubPart = dgvMACTSubParts(1, i).Value Then
                                dgvMACTSubParts(0, i).Value = AppNum
                                temp = "Modify"
                                dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
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

            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTDelete_Click(sender As Object, e As EventArgs) Handles btnMACTDelete.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
            Action = dgvMACTSubParts(4, dgvMACTSubParts.CurrentRow.Index).Value

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                If dgvMACTSubpartAddEdit(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be deleted from the Facility.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
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
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value

                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Tomato
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTUndelete_Click(sender As Object, e As EventArgs) Handles btnMACTUndelete.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""


            If dgvMACTSubPartDelete.CurrentRow Is Nothing Then
                Return
            End If

            If dgvMACTSubPartDelete.Rows.Count > 0 Then
                Subpart = dgvMACTSubPartDelete(0, dgvMACTSubPartDelete.CurrentRow.Index).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                    End If
                Next
                dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                dgvMACTSubPartDelete.Rows.Remove(dgvMACTSubPartDelete.CurrentRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTDeleteAll_Click(sender As Object, e As EventArgs) Handles btnMACTDeleteAll.Click
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
                    MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be deleted from the Facility.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                    If dgvMACTSubPartDelete(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvMACTSubPartDelete)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                    dgvMACTSubPartDelete.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTUndeleteAll_Click(sender As Object, e As EventArgs) Handles btnMACTUndeleteAll.Click
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
                dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvMACTSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearMACTDeletes_Click(sender As Object, e As EventArgs) Handles btnClearMACTDeletes.Click
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
                dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
            Next
            dgvMACTSubPartDelete.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewMACTSubpart_Click(sender As Object, e As EventArgs) Handles btnAddNewMACTSubpart.Click
        Try
            Dim Subpart As String = ""
            Dim Desc As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""

            If Not chbCDS_M.Checked Then
                MsgBox("The MACT Subpart is not checked on the Tracking Log tab. " & vbNewLine &
                       "This must be done before Adding new Subparts.", MsgBoxStyle.Exclamation,
                        "Application Tracking")
                Return
            End If

            Subpart = cboMACTSubpart.SelectedValue.ToString
            If Subpart <> "" Then
                Desc = cboMACTSubpart.Text
            End If

            temp2 = ""
            For i = 0 To dgvMACTSubParts.Rows.Count - 1
                If dgvMACTSubParts(1, i).Value = Subpart Then
                    temp2 = "Ignore"
                    MsgBox("The MACT Subpart already exists for this application.", MsgBoxStyle.Information,
                        "Application Tracking")
                    Return
                End If
            Next

            If temp2 <> "Ignore" Then
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvMACTSubParts)
                dgvRow.Cells(0).Value = AppNumber
                dgvRow.Cells(1).Value = Subpart
                dgvRow.Cells(2).Value = Desc
                dgvRow.Cells(3).Value = TodayFormatted
                dgvRow.Cells(4).Value = "Added"
                dgvMACTSubParts.Rows.Add(dgvRow)
                i = dgvMACTSubParts.Rows.Count - 1
                dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
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
                dgvRow.Cells(2).Value = TodayFormatted
                dgvRow.Cells(3).Value = "Added"
                dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                i = dgvMACTSubpartAddEdit.Rows.Count - 1
                dgvMACTSubpartAddEdit.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTEdit_Click(sender As Object, e As EventArgs) Handles btnMACTEdit.Click
        Try
            Dim temp As String
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubParts.CurrentRow Is Nothing Then
                Return
            End If

            Subpart = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
            Action = dgvMACTSubParts(4, dgvMACTSubParts.CurrentRow.Index).Value

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                If dgvMACTSubPartDelete(0, i).Value = Subpart Then
                    temp2 = "Message"
                End If
            Next
            If temp2 = "Message" Then
                MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                       "The subpart must be removed from this list before it can be Modified by this Application.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
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
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvMACTSubParts(3, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Else
                If Action <> "Added" Then
                    dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvMACTSubParts(3, dgvMACTSubParts.CurrentRow.Index).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(dgvMACTSubParts.CurrentRow.Index).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTUnedit_Click(sender As Object, e As EventArgs) Handles btnMACTUnedit.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If dgvMACTSubpartAddEdit.CurrentRow Is Nothing Then
                Return
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
                    dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    dgvMACTSubpartAddEdit.Rows.Remove(dgvMACTSubpartAddEdit.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTEditAll_Click(sender As Object, e As EventArgs) Handles btnMACTEditAll.Click
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
                    MsgBox("Subpart " & Subpart & " is currently listed in the Removed by list. " & vbNewLine &
                           "The subpart must be removed from this list before it can be Modified by this Application.",
                           MsgBoxStyle.Exclamation, "Application Tracking Log")
                    Return
                Else
                    temp2 = ""
                End If

                temp2 = ""
                For j As Integer = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                    If dgvMACTSubpartAddEdit(0, j).Value = Subpart Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvMACTSubpartAddEdit)
                    dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                    dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                    dgvRow.Cells(2).Value = dgvMACTSubParts(3, i).Value
                    dgvRow.Cells(3).Value = "Modify"
                    dgvMACTSubpartAddEdit.Rows.Add(dgvRow)
                    dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMACTUneditAll_Click(sender As Object, e As EventArgs) Handles btnMACTUneditAll.Click
        Try
            Dim i As Integer = 0
            Dim Subpart As String = ""
            Dim TempRemove As String = ""

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Subpart = dgvMACTSubpartAddEdit(0, i).Value
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart AndAlso dgvMACTSubParts(4, j).Value = "Existing" Then
                        dgvMACTSubParts.Rows(j).DefaultCellStyle.BackColor = Color.White
                        TempRemove = i & "," & TempRemove
                    End If
                Next
            Next

            Do While TempRemove <> ""
                i = Mid(TempRemove, 1, InStr(TempRemove, ",", CompareMethod.Text))
                dgvMACTSubpartAddEdit.Rows.RemoveAt(i)
                TempRemove = TempRemove?.Replace(i & ",", "")
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddModifiedMACTs_Click(sender As Object, e As EventArgs) Handles btnClearAddModifiedMACTs.Click
        Try
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            For i As Integer = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Subpart = dgvMACTSubpartAddEdit(0, i).Value
                temp2 = ""
                Action = ""
                For j As Integer = 0 To dgvMACTSubParts.Rows.Count - 1
                    If dgvMACTSubParts(1, j).Value = Subpart Then
                        temp2 = j
                        Action = dgvMACTSubParts(4, j).Value
                    End If
                Next
                If temp2 <> "" Then
                    dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
                    If Action = "Added" Then
                        dgvMACTSubParts.Rows.RemoveAt(temp2)
                    End If
                End If
            Next
            dgvMACTSubpartAddEdit.Rows.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveMACTSubpart()
        Try
            Dim Subpart As String = ""
            Dim Action As String = ""
            Dim i As Integer = 0

            DeleteProgramSubparts(AppNumber, "M")

            For i = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                Subpart = dgvMACTSubPartDelete(0, i).Value
                SaveProgramSubpartData(AppNumber, "M", Subpart, "0")
            Next

            For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                Action = dgvMACTSubpartAddEdit(3, i).Value
                Subpart = dgvMACTSubpartAddEdit(0, i).Value

                Select Case Action
                    Case "Added"
                        SaveProgramSubpartData(AppNumber, "M", Subpart, "1")
                    Case "Modify"
                        SaveProgramSubpartData(AppNumber, "M", Subpart, "2")
                End Select
            Next

            LoadSSPPMACTSubPartInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveMACTSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveMACTSubpart.Click
        Try
            If Not chbCDS_M.Checked Then
                MsgBox("WARNING DATA NOT SAVED:" & vbNewLine &
                       "On the Tracking Log tab select the air program code M - MACT. " &
                       "If you do not check this air program code the subparts cannot be saved.",
                       MsgBoxStyle.Exclamation, "Application Tracking Log")
                Return
            End If
            SaveMACTSubpart()
            MsgBox("MACT Updated", MsgBoxStyle.Information, "Application Tracking Log")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub chbCDS_0_CheckedChanged(sender As Object, e As EventArgs) Handles chbCDS_0.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" AndAlso chbCDS_0.CheckState = CheckState.Unchecked AndAlso dgvSIPSubParts.RowCount > 0 Then

                For i = 0 To dgvSIPSubpartAddEdit.Rows.Count - 1
                    Subpart = dgvSIPSubpartAddEdit(0, i).Value
                    temp2 = ""
                    Action = ""
                    For j = 0 To dgvSIPSubParts.Rows.Count - 1
                        If dgvSIPSubParts(1, j).Value = Subpart Then
                            temp2 = j
                            Action = dgvSIPSubParts(4, j).Value
                        End If
                    Next
                    If temp2 <> "" Then
                        dgvSIPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
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
                        MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                               "The subpart must be removed from this list before it can be deleted from the Facility.",
                               MsgBoxStyle.Exclamation, "Application Tracking Log")
                        Return
                    Else
                        temp2 = ""
                    End If

                    temp2 = ""
                    For j = 0 To dgvSIPSubPartDelete.Rows.Count - 1
                        If dgvSIPSubPartDelete(0, j).Value = Subpart Then
                            temp2 = "Ignore"
                        End If
                    Next
                    If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvSIPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvSIPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvSIPSubParts(2, i).Value
                        dgvSIPSubPartDelete.Rows.Add(dgvRow)
                        dgvSIPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    End If
                Next

                SaveSIPSubpart()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbCDS_8_CheckedChanged(sender As Object, e As EventArgs) Handles chbCDS_8.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" AndAlso chbCDS_8.CheckState = CheckState.Unchecked AndAlso dgvNESHAPSubParts.RowCount > 0 Then

                For i = 0 To dgvNESHAPSubpartAddEdit.Rows.Count - 1
                    Subpart = dgvNESHAPSubpartAddEdit(0, i).Value
                    temp2 = ""
                    Action = ""
                    For j = 0 To dgvNESHAPSubParts.Rows.Count - 1
                        If dgvNESHAPSubParts(1, j).Value = Subpart Then
                            temp2 = j
                            Action = dgvNESHAPSubParts(4, j).Value
                        End If
                    Next
                    If temp2 <> "" Then
                        dgvNESHAPSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
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
                        MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                               "The subpart must be removed from this list before it can be deleted from the Facility.",
                               MsgBoxStyle.Exclamation, "Application Tracking Log")
                        Return
                    Else
                        temp2 = ""
                    End If

                    temp2 = ""
                    For j = 0 To dgvNESHAPSubPartDelete.Rows.Count - 1
                        If dgvNESHAPSubPartDelete(0, j).Value = Subpart Then
                            temp2 = "Ignore"
                        End If
                    Next
                    If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNESHAPSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNESHAPSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNESHAPSubParts(2, i).Value
                        dgvNESHAPSubPartDelete.Rows.Add(dgvRow)
                        dgvNESHAPSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    End If
                Next

                SaveNESHAPSubpart()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbCDS_9_CheckedChanged(sender As Object, e As EventArgs) Handles chbCDS_9.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" AndAlso chbCDS_9.CheckState = CheckState.Unchecked AndAlso dgvNSPSSubParts.RowCount > 0 Then

                For i = 0 To dgvNSPSSubpartAddEdit.Rows.Count - 1
                    Subpart = dgvNSPSSubpartAddEdit(0, i).Value
                    temp2 = ""
                    Action = ""
                    For j = 0 To dgvNSPSSubParts.Rows.Count - 1
                        If dgvNSPSSubParts(1, j).Value = Subpart Then
                            temp2 = j
                            Action = dgvNSPSSubParts(4, j).Value
                        End If
                    Next
                    If temp2 <> "" Then
                        dgvNSPSSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
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
                        MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                               "The subpart must be removed from this list before it can be deleted from the Facility.",
                               MsgBoxStyle.Exclamation, "Application Tracking Log")
                        Return
                    Else
                        temp2 = ""
                    End If

                    temp2 = ""
                    For j = 0 To dgvNSPSSubPartDelete.Rows.Count - 1
                        If dgvNSPSSubPartDelete(0, j).Value = Subpart Then
                            temp2 = "Ignore"
                        End If
                    Next
                    If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSSubPartDelete)
                        dgvRow.Cells(0).Value = dgvNSPSSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvNSPSSubParts(2, i).Value
                        dgvNSPSSubPartDelete.Rows.Add(dgvRow)
                        dgvNSPSSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    End If
                Next

                SaveNSPSSubpart()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbCDS_M_CheckedChanged(sender As Object, e As EventArgs) Handles chbCDS_M.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp2 As String = ""
            Dim Subpart As String = ""
            Dim Action As String = ""

            If FormStatus = "" AndAlso chbCDS_M.CheckState = CheckState.Unchecked AndAlso dgvMACTSubParts.RowCount > 0 Then

                For i = 0 To dgvMACTSubpartAddEdit.Rows.Count - 1
                    Subpart = dgvMACTSubpartAddEdit(0, i).Value
                    temp2 = ""
                    Action = ""
                    For j = 0 To dgvMACTSubParts.Rows.Count - 1
                        If dgvMACTSubParts(1, j).Value = Subpart Then
                            temp2 = j
                            Action = dgvMACTSubParts(4, j).Value
                        End If
                    Next
                    If temp2 <> "" Then
                        dgvMACTSubParts.Rows(temp2).DefaultCellStyle.BackColor = Color.White
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
                        MsgBox("Subpart " & Subpart & " is currently listed in the Added/Modify list. " & vbNewLine &
                               "The subpart must be removed from this list before it can be deleted from the Facility.",
                               MsgBoxStyle.Exclamation, "Application Tracking Log")
                        Return
                    Else
                        temp2 = ""
                    End If

                    temp2 = ""
                    For j = 0 To dgvMACTSubPartDelete.Rows.Count - 1
                        If dgvMACTSubPartDelete(0, j).Value = Subpart Then
                            temp2 = "Ignore"
                        End If
                    Next
                    If temp2 <> "Ignore" AndAlso Action <> "Added" Then
                        dgvRow = New DataGridViewRow
                        dgvRow.CreateCells(dgvMACTSubPartDelete)
                        dgvRow.Cells(0).Value = dgvMACTSubParts(1, i).Value
                        dgvRow.Cells(1).Value = dgvMACTSubParts(2, i).Value
                        dgvMACTSubPartDelete.Rows.Add(dgvRow)
                        dgvMACTSubParts.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    End If
                Next

                SaveMACTSubpart()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtAIRSNumber_Enter(sender As Object, e As EventArgs) Handles txtAIRSNumber.AirsTextEnter
        AcceptButton = btnRefreshAIRSNo
    End Sub

    Private Sub txtAIRSNumber_Leave(sender As Object, e As EventArgs) Handles txtAIRSNumber.AirsTextLeave
        AcceptButton = Nothing
    End Sub

    Private Sub TCApplicationTrackingLog_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCApplicationTrackingLog.SelectedIndexChanged
        Select Case TCApplicationTrackingLog.SelectedTab.Name
            Case TPApplicationHistory.Name
                LoadFacilityApplicationHistory()

            Case TPInformationRequests.Name
                LoadInformationRequestHistory()

            Case TPFees.Name
                If dgvApplicationInvoices.Visible Then
                    dgvApplicationInvoices.SanelyResizeColumns()
                    dgvApplicationInvoices.SelectNone()
                End If

                If dgvApplicationPayments.Visible Then
                    dgvApplicationPayments.SanelyResizeColumns()
                    dgvApplicationPayments.SelectNone()
                End If

        End Select
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        PreSaveCheckThenSave()
    End Sub

#Region " Application fees "

    Private Sub chbApplicationFee_CheckedChanged(sender As Object, e As EventArgs) Handles chbAppFee.CheckedChanged
        If Not chbAppFee.Checked Then
            UpdatingValues = True
            cmbAppFeeType.SelectedIndex = -1
            chbAppFeeOverride.Checked = False
            ApplicationFeeAmount = 0
            UpdatingValues = False
        End If

        AdjustFeesUI()
    End Sub

    Private Sub chbExpeditedReview_CheckedChanged(sender As Object, e As EventArgs) Handles chbExpFee.CheckedChanged
        If Not chbExpFee.Checked Then
            UpdatingValues = True
            cmbExpFeeType.SelectedIndex = -1
            chbExpFeeOverride.Checked = False
            ExpeditedFeeAmount = 0
            UpdatingValues = False
        End If

        AdjustFeesUI()
    End Sub

    Private Sub cmbAppFeeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAppFeeType.SelectedIndexChanged
        If Not chbAppFeeOverride.Checked Then
            If cmbAppFeeType.SelectedIndex = -1 Then
                ApplicationFeeAmount = 0
            Else
                ApplicationFeeAmount = GetFeeRateAsOf(CType(cmbAppFeeType.SelectedValue, Integer), DTPDateReceived.Value)
            End If
        End If
    End Sub

    Private Sub cmbExpFeeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbExpFeeType.SelectedIndexChanged
        If Not chbExpFeeOverride.Checked Then
            If cmbExpFeeType.SelectedIndex = -1 Then
                ExpeditedFeeAmount = 0
            Else
                ExpeditedFeeAmount = GetFeeRateAsOf(CType(cmbExpFeeType.SelectedValue, Integer), DTPDateReceived.Value)
            End If
        End If
    End Sub

    Private Sub chbAppFeeOverride_CheckedChanged(sender As Object, e As EventArgs) Handles chbAppFeeOverride.CheckedChanged
        If chbAppFeeOverride.Checked Then
            chbAppFeeOverride.Text = "Override fee amount (give reason)"
        Else
            txtAppFeeOverrideReason.Clear()
            chbAppFeeOverride.Text = "Override fee amount"

            If Not chbAppFee.Checked OrElse cmbAppFeeType.SelectedIndex = -1 Then
                ApplicationFeeAmount = 0
            Else
                ApplicationFeeAmount = GetFeeRateAsOf(CType(cmbAppFeeType.SelectedValue, Integer), DTPDateReceived.Value)
            End If
        End If

        AdjustFeesUI()
    End Sub

    Private Sub chkExpFeeOverride_CheckedChanged(sender As Object, e As EventArgs) Handles chbExpFeeOverride.CheckedChanged
        If chbExpFeeOverride.Checked Then
            chbExpFeeOverride.Text = "Override fee amount (give reason)"
        Else
            txtExpFeeOverrideReason.Clear()
            chbExpFeeOverride.Text = "Override fee amount"

            If Not chbExpFee.Checked OrElse cmbExpFeeType.SelectedIndex = -1 Then
                ExpeditedFeeAmount = 0
            Else
                ExpeditedFeeAmount = GetFeeRateAsOf(CType(cmbExpFeeType.SelectedValue, Integer), DTPDateReceived.Value)
            End If
        End If

        AdjustFeesUI()
    End Sub

    Private Sub chbFeeDataFinalized_CheckedChanged(sender As Object, e As EventArgs) Handles chbFeeDataFinalized.CheckedChanged
        AdjustFeesUI()
    End Sub

    Private Sub txtAppFeeAmount_Validated(sender As Object, e As EventArgs) Handles txtAppFeeAmount.AmountChanged
        If txtAppFeeAmount.IsValid Then
            ApplicationFeeAmount = txtAppFeeAmount.Amount
        Else
            ApplicationFeeAmount = 0
        End If
    End Sub

    Private Sub txtExpFeeAmount_Validated(sender As Object, e As EventArgs) Handles txtExpFeeAmount.AmountChanged
        If txtExpFeeAmount.IsValid Then
            ExpeditedFeeAmount = txtExpFeeAmount.Amount
        Else
            ExpeditedFeeAmount = 0
        End If
    End Sub

    Private Sub DTPDateReceived_Leave(sender As Object, e As EventArgs) Handles DTPDateReceived.Leave
        LoadFeeRatesComboBoxes()
    End Sub

    Private Sub AdjustFeesUI()
        If AirsId Is Nothing OrElse AppNumber = 0 Then
            TPFees.Enabled = False
            Return
        End If

        If Not UpdatingValues Then

            ' Application fees
            chbAppFee.Enabled = FeeChangesAllowed AndAlso cmbAppFeeType.Items.Count > 0 AndAlso Not chbFeeDataFinalized.Checked
            cmbAppFeeType.Enabled = chbAppFee.Enabled AndAlso Not chbAppFeeOverride.Checked
            chbAppFeeOverride.Enabled = chbAppFee.Enabled AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount)
            txtAppFeeOverrideReason.Enabled = chbAppFee.Enabled AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount)

            cmbAppFeeType.Visible = chbAppFee.Checked
            lblAppFee.Visible = chbAppFee.Checked
            txtAppFeeAmount.Visible = chbAppFee.Checked
            txtAppFeeAmount.ReadOnly = chbFeeDataFinalized.Checked OrElse Not (chbAppFeeOverride.Checked AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount))
            chbAppFeeOverride.Visible = chbAppFee.Checked AndAlso (chbAppFeeOverride.Checked OrElse CurrentUser.HasPermission(UserCan.OverrideFeeAmount))
            txtAppFeeOverrideReason.Visible = chbAppFeeOverride.Checked AndAlso chbAppFee.Checked

            ' Expedited fees
            chbExpFee.Enabled = FeeChangesAllowed AndAlso cmbExpFeeType.Items.Count > 0 AndAlso Not chbFeeDataFinalized.Checked
            cmbExpFeeType.Enabled = chbExpFee.Enabled AndAlso Not chbExpFeeOverride.Checked
            chbExpFeeOverride.Enabled = chbExpFee.Enabled AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount)
            txtExpFeeOverrideReason.Enabled = chbExpFee.Enabled AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount)

            cmbExpFeeType.Visible = chbExpFee.Checked
            lblExpFee.Visible = chbExpFee.Checked
            txtExpFeeAmount.Visible = chbExpFee.Checked
            txtExpFeeAmount.ReadOnly = chbFeeDataFinalized.Checked OrElse Not (chbExpFeeOverride.Checked AndAlso CurrentUser.HasPermission(UserCan.OverrideFeeAmount))
            chbExpFeeOverride.Visible = chbExpFee.Checked AndAlso (chbExpFeeOverride.Checked OrElse CurrentUser.HasPermission(UserCan.OverrideFeeAmount))
            txtExpFeeOverrideReason.Visible = chbExpFeeOverride.Checked AndAlso chbExpFee.Checked

            ' Invoicing
            chbFeeDataFinalized.Visible = FeeChangesAllowed AndAlso TotalFeeAmount > 0 AndAlso (chbAppFee.Checked OrElse chbExpFee.Checked)
            lblFeeChangesNotAllowedWarning.Visible = FeeChangesAllowed AndAlso TotalFeeAmount > 0 AndAlso (chbAppFee.Checked OrElse chbExpFee.Checked) AndAlso chbFeeDataFinalized.Checked

            pnlFeeDataFinalized.Visible = Not FeeChangesAllowed AndAlso TotalFeeAmount > 0

            ' Fees not applicable labels
            If chbAppFee.Enabled OrElse chbAppFee.Checked Then
                chbAppFee.Text = "Permit Application Fee"
            Else
                chbAppFee.Text = "No Permit Application Fees Applicable"
            End If

            If chbExpFee.Enabled OrElse chbExpFee.Checked Then
                chbExpFee.Text = "Expedited Review"
            Else
                chbExpFee.Text = "No Expedited Review Fees Applicable"
            End If

        End If
    End Sub

    Private Sub LoadFeeRatesComboBoxes()
        UpdatingValues = True

        Dim feeRates As List(Of FeeRateItem) = FeeRateItemsAsOf(DTPDateReceived.Value)

        Dim currentFeeDataFinalized As Boolean = chbFeeDataFinalized.Checked AndAlso Not (cmbAppFeeType.Items.Count = 0 AndAlso cmbExpFeeType.Items.Count = 0)

        ' Application Fees
        Dim currentFeeTypeSelection As Integer = -1
        If cmbAppFeeType.Items.Count > 0 AndAlso cmbAppFeeType.SelectedIndex > -1 Then
            currentFeeTypeSelection = CType(cmbAppFeeType.SelectedValue, Integer)
        End If

        Dim appFeeRates As List(Of FeeRateItem) = feeRates.Where(Function(m) m.RateCategory = FeeRateCategory.PermitApplication).ToList()
        cmbAppFeeType.DisplayMember = "Description"
        cmbAppFeeType.ValueMember = "FeeRateItemID"
        cmbAppFeeType.DataSource = appFeeRates
        cmbAppFeeType.SetDropDownWidth()

        If currentFeeTypeSelection > -1 AndAlso appFeeRates.Any(Function(m) m.FeeRateItemID = currentFeeTypeSelection) Then
            cmbAppFeeType.SelectedValue = currentFeeTypeSelection
        Else
            chbAppFee.Checked = False
            ApplicationFeeAmount = 0
            cmbAppFeeType.SelectedIndex = -1
        End If

        ' Expedited fees
        currentFeeTypeSelection = -1
        If cmbExpFeeType.Items.Count > 0 AndAlso cmbExpFeeType.SelectedIndex > -1 Then
            currentFeeTypeSelection = CType(cmbExpFeeType.SelectedValue, Integer)
        End If

        Dim expFeeRates As List(Of FeeRateItem) = feeRates.Where(Function(m) m.RateCategory = FeeRateCategory.ExpeditedReview).ToList()
        cmbExpFeeType.DisplayMember = "Description"
        cmbExpFeeType.ValueMember = "FeeRateItemID"
        cmbExpFeeType.DataSource = expFeeRates
        cmbExpFeeType.SetDropDownWidth()

        If currentFeeTypeSelection > -1 AndAlso expFeeRates.Any(Function(m) m.FeeRateItemID = currentFeeTypeSelection) Then
            cmbExpFeeType.SelectedValue = currentFeeTypeSelection
        Else
            chbExpFee.Checked = False
            ExpeditedFeeAmount = 0
            cmbExpFeeType.SelectedIndex = -1
        End If

        If CType(cmbAppFeeType.DataSource, ICollection).Count = 0 AndAlso CType(cmbExpFeeType.DataSource, ICollection).Count = 0 Then
            chbFeeDataFinalized.Checked = True
        Else
            chbFeeDataFinalized.Checked = currentFeeDataFinalized
        End If

        UpdatingValues = False
        AdjustFeesUI()
    End Sub

    Private Sub SaveApplicationFees()
        If AirsId Is Nothing OrElse AppNumber = 0 Then
            Return
        End If

        Dim dateFacilityNotified As Date? = If(dtpFacilityFeeNotified.Checked, dtpFacilityFeeNotified.Value, CType(Nothing, Date?))

        If Not FeeChangesAllowed Then
            If pnlFeeDataFinalized.Visible Then
                SaveApplicationFeesDateFacilityNotified(AppNumber, dateFacilityNotified)
            End If

            Return
        End If

        Dim feesInfo As New ApplicationFeeInfo() With {
            .ApplicationFeeAmount = ApplicationFeeAmount,
            .ApplicationFeeApplies = chbAppFee.Checked,
            .ApplicationFeeOverride = chbAppFeeOverride.Checked,
            .ApplicationFeeOverrideReason = txtAppFeeOverrideReason.Text,
            .ApplicationFeeType = If(cmbAppFeeType.SelectedIndex > -1, CType(cmbAppFeeType.SelectedValue, Integer), CType(Nothing, Integer?)),
            .ApplicationID = AppNumber,
            .ApplicationWithdrawn = (cboPermitAction.SelectedValue.ToString = "11"),
            .DateFacilityNotifiedOfFees = dateFacilityNotified,
            .DateFeeDataFinalized = If(chbFeeDataFinalized.Checked, dtpFeeDataFinalized.Value, CType(Nothing, Date?)),
            .ExpeditedFeeAmount = ExpeditedFeeAmount,
            .ExpeditedFeeApplies = chbExpFee.Checked,
            .ExpeditedFeeOverride = chbExpFeeOverride.Checked,
            .ExpeditedFeeOverrideReason = txtExpFeeOverrideReason.Text,
            .ExpeditedFeeType = If(cmbExpFeeType.SelectedIndex > -1, CType(cmbExpFeeType.SelectedValue, Integer), CType(Nothing, Integer?)),
            .FacilityID = AirsId,
            .FeeDataFinalized = chbFeeDataFinalized.Checked
        }

        Select Case SaveApplicationFeesData(feesInfo)
            Case SaveApplicationFeesDataResult.Success

                If feesInfo.FeeDataFinalized Then
                    Dim invoiceId As Integer
                    Dim result As GenerateInvoiceResult = GenerateInvoice(AppNumber, CurrentUser.UserID, invoiceId)

                    Select Case result
                        Case GenerateInvoiceResult.Success
                            LoadFeesData()

                        Case GenerateInvoiceResult.InvoiceExists
                            MessageBox.Show("An invoice has already been generated for this application. Please verify fees information before proceeding.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            LoadFeesData()

                        Case Else
                            MessageBox.Show("There was an error generating the application fee invoice. Please contact EPD-IT.",
                                            $"Error ({Convert.ToInt32(result)})", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Select
                End If

            Case SaveApplicationFeesDataResult.ApplicationDoesNotExist
                MessageBox.Show("There was an error saving the application fees. Application number not found. Please contact EPD-IT.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Case SaveApplicationFeesDataResult.InvoiceAlreadyGenerated
                MessageBox.Show("An invoice has already been generated for this application. Please verify fees information before proceeding.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                LoadFeesData()

            Case Else
                MessageBox.Show("There was an error saving the application fees. Please contact EPD-IT.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Select
    End Sub

    Private Sub LoadFeesData()
        UpdatingValues = True

        Dim feesInfo As ApplicationFeeInfo = GetApplicationFeesInfo(AppNumber)

        If feesInfo Is Nothing Then
            UpdatingValues = False
            Return
        End If

        With feesInfo
            ' Application fee
            chbAppFee.Checked = .ApplicationFeeApplies
            ApplicationFeeAmount = .ApplicationFeeAmount
            chbAppFeeOverride.Checked = .ApplicationFeeOverride
            txtAppFeeOverrideReason.Text = .ApplicationFeeOverrideReason

            If .ApplicationFeeType.HasValue AndAlso cmbAppFeeType.Items.Count > 0 Then
                cmbAppFeeType.SelectedValue = .ApplicationFeeType.Value
            End If

            ' Expedited fee
            chbExpFee.Checked = .ExpeditedFeeApplies
            ExpeditedFeeAmount = .ExpeditedFeeAmount
            chbExpFeeOverride.Checked = .ExpeditedFeeOverride
            txtExpFeeOverrideReason.Text = .ExpeditedFeeOverrideReason

            If .ExpeditedFeeType.HasValue AndAlso cmbExpFeeType.Items.Count > 0 Then
                cmbExpFeeType.SelectedValue = .ExpeditedFeeType.Value
            End If

            ' Invoicing
            RemoveHandler chbFeeDataFinalized.CheckedChanged, AddressOf chbFeeDataFinalized_CheckedChanged

            If cmbAppFeeType.Items.Count = 0 AndAlso cmbExpFeeType.Items.Count = 0 Then
                chbFeeDataFinalized.Checked = True
            Else
                chbFeeDataFinalized.Checked = .FeeDataFinalized
            End If

            dtpFeeDataFinalized.Value = If(.DateFeeDataFinalized, Today)
            AddHandler chbFeeDataFinalized.CheckedChanged, AddressOf chbFeeDataFinalized_CheckedChanged

            dtpFacilityFeeNotified.Value = If(.DateFacilityNotifiedOfFees, Today)
            dtpFacilityFeeNotified.Checked = .DateFacilityNotifiedOfFees.HasValue

            If .FeeDataFinalized Then
                ShowControls({dgvApplicationInvoices, dgvApplicationPayments, lblInvoices, lblPayments, lblFeeTotalInvoiced, lblFeeTotalPaid, txtFeeTotalInvoiced, txtFeeTotalPaid})
                SplitContainer1.BackColor = SystemColors.ActiveCaption
                SplitContainer1.IsSplitterFixed = False

                dgvApplicationInvoices.DataSource = .Invoices

                If .Invoices IsNot Nothing AndAlso .Invoices.Rows.Count > 0 Then
                    txtFeeTotalInvoiced.Amount = .Invoices.AsEnumerable().
                    Sum(Function(x) x.Field(Of Decimal)("Amount"))
                End If

                dgvApplicationPayments.DataSource = .Payments

                If .Payments IsNot Nothing AndAlso .Payments.Rows.Count > 0 Then
                    txtFeeTotalPaid.Amount = .Payments.AsEnumerable().
                    Sum(Function(x) x.Field(Of Decimal)("Payment"))
                End If
            Else
                HideControls({dgvApplicationInvoices, dgvApplicationPayments, lblInvoices, lblPayments, lblFeeTotalInvoiced, lblFeeTotalPaid, txtFeeTotalInvoiced, txtFeeTotalPaid})
                SplitContainer1.BackColor = SystemColors.ControlLightLight
                SplitContainer1.IsSplitterFixed = True
            End If

            If .Invoices IsNot Nothing AndAlso .Invoices.Rows.Count > 0 Then
                FeeChangesAllowed = False
            End If
        End With

        UpdatingValues = False
        AdjustFeesUI()
    End Sub

    Private Sub dgvApplicationInvoices_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvApplicationInvoices.CellLinkActivated
        OpenInvoiceView(CInt(e.LinkValue))
    End Sub

    Private Sub dgvApplicationPayments_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvApplicationPayments.CellLinkActivated
        OpenDepositView(CInt(e.LinkValue))
    End Sub

    Private Sub lklGenerateEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lklGenerateEmail.LinkClicked
        If Not txtContactEmailAddress.Text.IsValidEmailAddress Then
            MessageBox.Show("The contact email address is not valid. Please enter a valid email and try again.", "Email not sent", MessageBoxButtons.OK)
            Return
        End If

        UseWaitCursor = True

        Dim subject As String = String.Format("Application No. {0}; Fee ", AppNumber)

        If chbExpFee.Checked Then
            subject &= "and Expedited Acceptance "
        End If

        subject &= "Notification"

        Dim body As New StringBuilder()
        body.AppendLine(String.Format("Facility: {0}", txtFacilityName.Text))
        body.AppendLine(String.Format("Location: {0}, {1} County", cboFacilityCity.Text, cboCounty.Text))
        body.AppendLine(String.Format("AIRS: {0}", txtAIRSNumber.AirsNumber.FormattedString))
        body.AppendLine(String.Format("App Type: {0}", cboApplicationType.Text))
        body.AppendLine()
        body.AppendLine(String.Format("Application Fee: {0}", If(chbAppFee.Checked, txtAppFeeAmount.Text, "Not Applicable")))
        body.AppendLine(String.Format("Expedited Review Fee: {0}", If(chbExpFee.Checked, txtExpFeeAmount.Text, "Not Applicable")))
        body.AppendLine(String.Format("Total: {0}", txtFeeTotal.Text))
        body.AppendLine()
        body.AppendLine("This email serves to acknowledge receipt of your Air Quality Permit application.")
        body.AppendLine()

        If chbExpFee.Checked Then
            body.Append("This email also serves as notice of acceptance of the application into the Expedited Permitting Program. ")
            body.Append("The applicant must complete the invitation process by responding to this email to acknowledge entry into the program ")
            body.Append("within three business days of this email. The expedited time frame begins upon the applicant’s acceptance of the invitation. ")
            body.Append("Failure to comply with the deadlines listed in this email may result in removal of the application from the ")
            body.Append("Expedited Permitting Program. If the applicant decides to decline participation in the program they ")
            body.Append("must immediately respond, requesting that the application no longer be considered.")
            body.AppendLine()
            body.AppendLine()
        End If

        body.Append("The applicant must complete the submittal process by paying the above referenced fee. ")
        body.AppendFormat("A printable fee invoice is accessible online at {0} ", GetPermitApplicationUrl(AppNumber).ToString())
        body.AppendLine()
        body.AppendLine()
        body.Append("FEE PAYMENT INSTRUCTIONS: Don’t send payment to the Air Branch office address; pay by check to the PO Box ")
        body.Append("listed on the invoice or by credit card following the instructions on the invoice.")
        body.AppendLine()
        body.AppendLine()
        body.Append("The fee must be submitted within 10 business days of the date of this email. Permitting actions will not be finalized ")
        body.Append("prior to the Division’s receipt of the fee payment.")
        body.AppendLine()
        body.AppendLine()
        body.Append("Other environmental permits may be required. For Industrial Stormwater permits, contact the Watershed Protection Branch ")
        body.Append("at (404) 675-1605; for Solid Waste permits, contact the Land Protection Branch at (404) 362-2537. ")
        body.Append("For more info, see https://epd.georgia.gov/ ")
        body.AppendLine()
        body.AppendLine()
        body.AppendLine("Please contact me if you have any questions.")

        Dim recipient As String() = Nothing

        If txtContactEmailAddress.Text.IsValidEmailAddress Then
            recipient = {txtContactEmailAddress.Text}
        End If

        Select Case CreateEmail(subject, body.ToString(), recipient)

            Case CreateEmailResult.Failure, CreateEmailResult.FunctionError
                MessageBox.Show("There was an error sending the message. Please try again.", "Error", MessageBoxButtons.OK)

        End Select

        UseWaitCursor = False
    End Sub

    Private Sub lklOpenAppOnline_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lklOpenAppOnline.LinkClicked
        OpenPermitApplicationUrl(AppNumber)
    End Sub

#End Region

End Class
