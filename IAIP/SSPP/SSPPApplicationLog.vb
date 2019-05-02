Imports System.Data.SqlClient
Imports Iaip.Apb.Facilities

Public Class SSPPApplicationLog

#Region " Private properties "

    Private Property dtApplicationLog As DataTable
    Private Property selectedApp As String = ""

#End Region

#Region " Background worker fields "

    ' Fields to be used by LoadDataGrid (as backgroundworker) to 
    ' prevent cross-thread operation exceptions.
    ' (Yes, it would be more efficient to pass these as DoWorkEventArgs.)
    Private FieldType1 As String 'cboFieldType1.Text
    Private FieldType2 As String 'cboFieldType2.Text
    Private AppType As String 'cboApplicationType.Text
    Private AppUnit As String 'cboApplicationUnit.SelectedValue
    Private AppUnitText As String 'cboApplicationUnit.Text
    Private AppStatus As String 'cboApplicationStatus.Text
    Private Engineer As String 'cboEngineer.SelectedValue.ToString
    Private SubpartSIP1 As String 'cboSIP1.SelectedValue.ToString
    Private SubpartSIP2 As String 'cboSIP2.SelectedValue.ToString
    Private SubpartNESHAP1 As String 'cboNESHAP1.SelectedValue.ToString
    Private SubpartNESHAP2 As String 'cboNESHAP2.SelectedValue.ToString
    Private SubpartNSPS1 As String 'cboNSPS1.SelectedValue.ToString
    Private SubpartNSPS2 As String 'cboNSPS2.SelectedValue.ToString
    Private SubpartMACT1 As String 'cboMACT1.SelectedValue.ToString
    Private SubpartMACT2 As String 'cboMACT2.SelectedValue.ToString
    Private SortOrder1 As String 'cboSortOrder1.Text
    Private SortOrder2 As String 'cboSortOrder2.Text
    Private Sort1 As String 'cboSort1.Text
    Private Sort2 As String 'cboSort2.Text

    Private SearchText1 As String 'txtSearchText1.Text
    Private SearchText1b As String 'cboSearchText1.Text
    Private SearchDate1 As String 'DTPSearchDate1.Text
    Private SearchDate1b As String 'DTPSearchDate1b.Text
    Private SearchText2 As String 'txtSearchText2.Text
    Private SearchText2b As String 'cboSearchText2.Text
    Private SearchDate2 As String 'DTPSearchDate2.Text
    Private SearchDate2b As String 'DTPSearchDate2b.Text

#End Region

#Region " Page load "

    Private Sub SSPPApplicationLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        dgvApplicationLog.Visible = False
        lblMessage.Text = "Loading..."
        lblMessage.Visible = True

        btnFind.Enabled = True
        btnResetSearch.Enabled = True
        btnOpen.Enabled = False
        btnExport.Enabled = False
        mmiResetSearch.Enabled = True
        mmiOpen.Enabled = False
        mmiExport.Enabled = False

        LoadComboBoxes()
        LoadDefaults()
        RunSearch()
    End Sub

    Private Sub LoadComboBoxes()
        cboFieldType1.Items.Add("AIRS No.")
        cboFieldType1.Items.Add("Applicable Rules")
        cboFieldType1.Items.Add("Application No.")
        cboFieldType1.Items.Add("Application Status")
        cboFieldType1.Items.Add("Application Type")
        cboFieldType1.Items.Add("Application Unit")
        cboFieldType1.Items.Add("Applog Comments")
        cboFieldType1.Items.Add("Date Acknowledged")
        cboFieldType1.Items.Add("Date APL Dated")
        cboFieldType1.Items.Add("Date APL Received")
        cboFieldType1.Items.Add("Date Assigned")
        cboFieldType1.Items.Add("Date Draft Issued")
        cboFieldType1.Items.Add("Date Finalized")
        cboFieldType1.Items.Add("Date PA Expires")
        cboFieldType1.Items.Add("Date PN Expires")
        cboFieldType1.Items.Add("Date Reassigned")
        cboFieldType1.Items.Add("Date to BC")
        cboFieldType1.Items.Add("Date to DO")
        cboFieldType1.Items.Add("Date to PM")
        cboFieldType1.Items.Add("Date to UC")
        cboFieldType1.Items.Add("Deadline")
        cboFieldType1.Items.Add("Engineer Firstname")
        cboFieldType1.Items.Add("Engineer Lastname")
        cboFieldType1.Items.Add("EPA 45-day Waived")
        cboFieldType1.Items.Add("EPA 45-day Ends")
        cboFieldType1.Items.Add("Facility City")
        cboFieldType1.Items.Add("Facility County")
        cboFieldType1.Items.Add("Facility Name")
        cboFieldType1.Items.Add("Facility Street")
        cboFieldType1.Items.Add("HAPs Major")
        cboFieldType1.Items.Add("NAA 1Hr-Yes")
        cboFieldType1.Items.Add("NAA 1Hr-Contr.")
        cboFieldType1.Items.Add("NAA 1Hr-No")
        cboFieldType1.Items.Add("NAA 8Hr-Atlanta")
        cboFieldType1.Items.Add("NAA 8Hr-Macon")
        cboFieldType1.Items.Add("NAA 8Hr-No")
        cboFieldType1.Items.Add("NAA PM-Atlanta")
        cboFieldType1.Items.Add("NAA PM-Chattanooga")
        cboFieldType1.Items.Add("NAA PM-Floyd")
        cboFieldType1.Items.Add("NAA PM-Macon")
        cboFieldType1.Items.Add("NAA PM-No")
        cboFieldType1.Items.Add("NSR/PSD Major")
        cboFieldType1.Items.Add("PA Ready")
        cboFieldType1.Items.Add("Permit Number")
        cboFieldType1.Items.Add("Permit Type")
        cboFieldType1.Items.Add("Plant Description")
        cboFieldType1.Items.Add("PN Ready")
        cboFieldType1.Items.Add("Public Advisory")
        cboFieldType1.Items.Add("Reason APL Submitted")
        cboFieldType1.Items.Add("Regional District")
        cboFieldType1.Items.Add("SIC Code")
        cboFieldType1.Items.Add("Subpart - SIP")
        cboFieldType1.Items.Add("Subpart - NESHAP (Part 61)")
        cboFieldType1.Items.Add("Subpart - NSPS (Part 60)")
        cboFieldType1.Items.Add("Subpart - MACT (Part 63)")

        cboFieldType2.Items.Add("AIRS No.")
        cboFieldType2.Items.Add("Applicable Rules")
        cboFieldType2.Items.Add("Application No.")
        cboFieldType2.Items.Add("Application Status")
        cboFieldType2.Items.Add("Application Type")
        cboFieldType2.Items.Add("Application Unit")
        cboFieldType2.Items.Add("Applog Comments")
        cboFieldType2.Items.Add("Date Acknowledged")
        cboFieldType2.Items.Add("Date APL Dated")
        cboFieldType2.Items.Add("Date APL Received")
        cboFieldType2.Items.Add("Date Assigned")
        cboFieldType2.Items.Add("Date Draft Issued")
        cboFieldType2.Items.Add("Date Finalized")
        cboFieldType2.Items.Add("Date PA Expires")
        cboFieldType2.Items.Add("Date PN Expires")
        cboFieldType2.Items.Add("Date Reassigned")
        cboFieldType2.Items.Add("Date to BC")
        cboFieldType2.Items.Add("Date to DO")
        cboFieldType2.Items.Add("Date to PM")
        cboFieldType2.Items.Add("Date to UC")
        cboFieldType2.Items.Add("Deadline")
        cboFieldType2.Items.Add("Engineer Firstname")
        cboFieldType2.Items.Add("Engineer Lastname")
        cboFieldType2.Items.Add("EPA 45-day Waived")
        cboFieldType2.Items.Add("EPA 45-day Ends")
        cboFieldType2.Items.Add("Facility City")
        cboFieldType2.Items.Add("Facility County")
        cboFieldType2.Items.Add("Facility Name")
        cboFieldType2.Items.Add("Facility Street")
        cboFieldType2.Items.Add("HAPs Major")
        cboFieldType2.Items.Add("NAA 1Hr-Yes")
        cboFieldType2.Items.Add("NAA 1Hr-Contr.")
        cboFieldType2.Items.Add("NAA 1Hr-No")
        cboFieldType2.Items.Add("NAA 8Hr-Atlanta")
        cboFieldType2.Items.Add("NAA 8Hr-Macon")
        cboFieldType2.Items.Add("NAA 8Hr-No")
        cboFieldType2.Items.Add("NAA PM-Atlanta")
        cboFieldType2.Items.Add("NAA PM-Chattanooga")
        cboFieldType2.Items.Add("NAA PM-Floyd")
        cboFieldType2.Items.Add("NAA PM-Macon")
        cboFieldType2.Items.Add("NAA PM-No")
        cboFieldType2.Items.Add("NSR/PSD Major")
        cboFieldType2.Items.Add("PA Ready")
        cboFieldType2.Items.Add("Permit Number")
        cboFieldType2.Items.Add("Permit Type")
        cboFieldType2.Items.Add("Plant Description")
        cboFieldType2.Items.Add("PN Ready")
        cboFieldType2.Items.Add("Public Advisory")
        cboFieldType2.Items.Add("Reason APL Submitted")
        cboFieldType2.Items.Add("Regional District")
        cboFieldType2.Items.Add("SIC Code")
        cboFieldType2.Items.Add("Subpart - SIP")
        cboFieldType2.Items.Add("Subpart - NESHAP (Part 61)")
        cboFieldType2.Items.Add("Subpart - NSPS (Part 60)")
        cboFieldType2.Items.Add("Subpart - MACT (Part 63)")

        cboSort1.Items.Add("AIRS No.")
        cboSort1.Items.Add("Application No.")
        cboSort1.Items.Add("Application Status")
        cboSort1.Items.Add("Application Type")
        cboSort1.Items.Add("Date APL Received")
        cboSort1.Items.Add("Date Finalized")
        cboSort1.Items.Add("Engineer Unit Code")
        cboSort1.Items.Add("Facility County")
        cboSort1.Items.Add("Facility Name")
        cboSort1.Items.Add("Permit Number")
        cboSort1.Items.Add("Permit Type")
        cboSort1.Items.Add("Plant Description")
        cboSort1.Items.Add("SIC Code")

        cboSort2.Items.Add("AIRS No.")
        cboSort2.Items.Add("Application No.")
        cboSort2.Items.Add("Application Status")
        cboSort2.Items.Add("Application Type")
        cboSort2.Items.Add("Date APL Received")
        cboSort2.Items.Add("Date Finalized")
        cboSort2.Items.Add("Engineer Unit Code")
        cboSort2.Items.Add("Facility County")
        cboSort2.Items.Add("Facility Name")
        cboSort2.Items.Add("Permit Number")
        cboSort2.Items.Add("Permit Type")
        cboSort2.Items.Add("Plant Description")
        cboSort2.Items.Add("SIC Code")

        cboSortOrder1.Items.Add("Ascending Order")
        cboSortOrder1.Items.Add("Descending Order")

        cboSortOrder2.Items.Add("Ascending Order")
        cboSortOrder2.Items.Add("Descending Order")

        cboApplicationType.Items.Add("All")
        cboApplicationType.Items.Add("Title V")
        cboApplicationType.Items.Add("SIP (Non Title V)")

        cboApplicationStatus.Items.Add("All")
        cboApplicationStatus.Items.Add("Active")
        cboApplicationStatus.Items.Add("Closed")

        Dim SQL As String = "select 'All' as strUnitDesc, 'All' as numUnitCode UNION " &
        "select strUnitDesc, convert(varchar,numUnitCode) " &
        "from LookUpEPDUnits " &
        "where numProgramCode = 5 " &
        "order by strUnitDesc "

        With cboApplicationUnit
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "strUnitDesc"
            .ValueMember = "numUnitCode"
            .SelectedIndex = 0
        End With

        SQL = "Select 'All' as EngineerName, 'XXX' as numUserID, '' as strLastName UNION " &
        "Select Distinct concat(strLastName, ', ', strFirstName) as EngineerName,  " &
        "convert(varchar, numUserID), strLastName " &
        "from EPDUserProfiles inner join SSPPApplicationMaster  " &
        "on SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
        "order by strLastName "

        With cboEngineer
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "EngineerName"
            .ValueMember = "numUserID"
            .SelectedIndex = 0
        End With

        Dim dtSIP As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.SIP.ToString)

        With cboSIP1
            .DataSource = dtSIP
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        With cboSIP2
            .DataSource = dtSIP.Copy
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        Dim dtNESHAP As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NESHAP.ToString)

        With cboNESHAP1
            .DataSource = dtNESHAP
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        With cboNESHAP2
            .DataSource = dtNESHAP.Copy
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        Dim dtNSPS As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NSPS.ToString)

        With cboNSPS1
            .DataSource = dtNSPS
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        With cboNSPS2
            .DataSource = dtNSPS.Copy
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        Dim dtMACT As DataTable = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.MACT.ToString)

        With cboMACT1
            .DataSource = dtMACT
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With

        With cboMACT2
            .DataSource = dtMACT.Copy
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadDefaults()
        cboFieldType1.Text = "Facility Name"
        cboFieldType2.Text = "Application No."
        cboSort1.Text = "Facility Name"
        cboSort2.Text = "Application No."
        cboSortOrder1.Text = "Ascending Order"
        cboSortOrder2.Text = "Descending Order"
        cboApplicationType.Text = "All"
        cboEngineer.SelectedIndex = 0

        DTPSearchDate1.Value = Today
        DTPSearchDate1b.Value = Today
        DTPSearchDate2.Value = Today
        DTPSearchDate2b.Value = Today

        txtSearchText1.Clear()
        txtSearchText2.Clear()

        cboApplicationStatus.Text = "Active"

        If AccountFormAccess(3, 3) = "1" And CurrentUser.UnitId = 0 Then
            'All active Applications
            cboApplicationType.Text = "All"
        ElseIf AccountFormAccess(3, 3) = "1" And CurrentUser.UnitId <> 0 Then
            'All Active Applications from UC's Unit
            If CurrentUser.ProgramID = 5 Then
                cboApplicationUnit.SelectedValue = CurrentUser.UnitId
            Else
                cboEngineer.SelectedValue = CurrentUser.UserID
            End If
        Else
            cboEngineer.SelectedValue = CurrentUser.UserID
        End If

        If AccountFormAccess(3, 4) = "1" Then
            mmiNewApplication.Visible = True
        End If
    End Sub

#End Region

#Region "Background worker / Search procedures"

    Private Sub RunSearch()
        CancelSearch()

        selectedApp = ""

        dgvApplicationLog.Visible = False
        lblMessage.Text = "Loading..."
        lblMessage.Visible = True

        btnFind.Enabled = False
        btnResetSearch.Enabled = False
        btnOpen.Enabled = False
        btnExport.Enabled = False
        mmiResetSearch.Enabled = False
        mmiOpen.Enabled = False
        mmiExport.Enabled = False

        ' Load form data into variables for use by background worker
        FieldType1 = cboFieldType1.Text
        FieldType2 = cboFieldType2.Text
        AppType = cboApplicationType.Text
        AppUnit = cboApplicationUnit.SelectedValue
        AppUnitText = cboApplicationUnit.Text
        AppStatus = cboApplicationStatus.Text
        Engineer = If(cboEngineer.SelectedValue = "", "XXX", cboEngineer.SelectedValue.ToString)
        SubpartSIP1 = cboSIP1.SelectedValue.ToString
        SubpartSIP2 = cboSIP2.SelectedValue.ToString
        SubpartNESHAP1 = cboNESHAP1.SelectedValue.ToString
        SubpartNESHAP2 = cboNESHAP2.SelectedValue.ToString
        SubpartNSPS1 = cboNSPS1.SelectedValue.ToString
        SubpartNSPS2 = cboNSPS2.SelectedValue.ToString
        SubpartMACT1 = cboMACT1.SelectedValue.ToString
        SubpartMACT2 = cboMACT2.SelectedValue.ToString
        SortOrder1 = cboSortOrder1.Text
        Sort1 = cboSort1.Text
        SortOrder2 = cboSortOrder2.Text
        Sort2 = cboSort2.Text
        SearchText1 = txtSearchText1.Text
        SearchText1b = cboSearchText1.Text
        SearchDate1 = DTPSearchDate1.Text
        SearchDate1b = DTPSearchDate1b.Text
        SearchText2 = txtSearchText2.Text
        SearchText2b = cboSearchText2.Text
        SearchDate2 = DTPSearchDate2.Text
        SearchDate2b = DTPSearchDate2b.Text

        dtApplicationLog = Nothing

        If bgwApplicationLog.IsBusy = False Then
            bgwApplicationLog.RunWorkerAsync()
        End If
    End Sub

    Private Sub CancelSearch()
        If bgwApplicationLog.IsBusy Then bgwApplicationLog.CancelAsync()
    End Sub

    Private Sub FetchData(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwApplicationLog.DoWork
        Dim SQL As String
        Dim SQLLine As String
        Dim SQLSearch1 As String = ""
        Dim SQLSearch2 As String = ""
        Dim SQLOrder As String

        Try
            SQL = "SELECT DISTINCT
                CONVERT(int, m.STRAPPLICATIONNUMBER) AS strApplicationNumber,
                CASE WHEN la.STRAPPLICATIONTYPEDESC IS NULL THEN '' 
                ELSE la.STRAPPLICATIONTYPEDESC END AS strApplicationType,
                t.DATRECEIVEDDATE,
                CASE WHEN d.STRPERMITNUMBER IS NULL THEN '' 
                ELSE CONCAT(SUBSTRING(d.STRPERMITNUMBER, 1, 4), '-', SUBSTRING(d.STRPERMITNUMBER, 5, 3), '-', SUBSTRING(d.STRPERMITNUMBER, 8, 4), '-', SUBSTRING(d.STRPERMITNUMBER, 12, 1), '-', SUBSTRING(d.STRPERMITNUMBER, 13, 2), '-', SUBSTRING(d.STRPERMITNUMBER, 15, 1)) END AS strPermitNumber,
                t.DATPERMITISSUED,
                CASE WHEN u.NUMUSERID = '0' THEN '' 
                WHEN u.NUMUSERID IS NULL THEN '' 
                ELSE CONCAT(u.STRLASTNAME, ', ', u.STRFIRSTNAME) END AS StaffResponsible,
                CASE WHEN d.STRFACILITYNAME IS NULL THEN '' 
                ELSE d.STRFACILITYNAME END AS strFacilityName,
                CASE WHEN m.STRAIRSNUMBER IS NULL THEN '' 
                WHEN m.STRAIRSNUMBER = '0413' THEN '' 
                ELSE SUBSTRING(m.STRAIRSNUMBER, 5, 8) END AS strAIRSNumber,
                CASE WHEN t.DATPERMITISSUED IS NOT NULL OR m.DATFINALIZEDDATE IS NOT NULL THEN '11 - Closed Out' 
                WHEN t.DATTODIRECTOR IS NOT NULL AND m.DATFINALIZEDDATE IS NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR) THEN '09 - Administrative Review' 
                WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND m.DATFINALIZEDDATE IS NULL AND t.DATTODIRECTOR IS NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN '09 - Administrative Review' 
                WHEN t.DATEPAENDS IS NOT NULL THEN '08 - EPA 45-day Review' 
                WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN '07 - Public Notice Expired' 
                WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN '06 - Public Notice' 
                WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN '05 - Draft Issued' 
                WHEN t.DATTOPMII IS NOT NULL THEN '04 - AT PM' 
                WHEN t.DATTOPMI IS NOT NULL THEN '03 - At UC' 
                WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0') THEN '02 - Internal Review' 
                WHEN m.STRSTAFFRESPONSIBLE IS NULL OR m.STRSTAFFRESPONSIBLE = '0' THEN '0 - Unassigned' 
                ELSE '01 - At Engineer' END AS AppStatus,
                CASE WHEN lp.STRPERMITTYPEDESCRIPTION IS NULL THEN '' 
                ELSE lp.STRPERMITTYPEDESCRIPTION END AS strPermitType,
                CASE WHEN t.DATPERMITISSUED IS NOT NULL THEN t.DATPERMITISSUED
                WHEN convert(date, m.DATFINALIZEDDATE) = '1776-07-04' THEN NULL
                WHEN m.DATFINALIZEDDATE IS NOT NULL THEN m.DATFINALIZEDDATE
                WHEN t.DATTODIRECTOR IS NOT NULL AND m.DATFINALIZEDDATE IS NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR) THEN t.DATTODIRECTOR
                WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND m.DATFINALIZEDDATE IS NULL AND t.DATTODIRECTOR IS NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN t.DATTOBRANCHCHEIF
                WHEN t.DATEPAENDS IS NOT NULL THEN t.DATEPAENDS
                WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN t.DATPNEXPIRES
                WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN t.DATPNEXPIRES
                WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN t.DATDRAFTISSUED
                WHEN t.DATTOPMII IS NOT NULL THEN t.DATTOPMII
                WHEN t.DATTOPMI IS NOT NULL THEN t.DATTOPMI
                WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0') THEN t.DATREVIEWSUBMITTED
                WHEN m.STRSTAFFRESPONSIBLE IS NULL OR m.STRSTAFFRESPONSIBLE = '0' THEN null 
                ELSE t.DATASSIGNEDTOENGINEER END AS StatusDate, 
                d.STRSICCODE, d.STRPLANTDESCRIPTION,
                lc.STRCOUNTYNAME,
                case when e.STRUNITDESC is null then '' else e.STRUNITDESC end as APBUnit,
                case when substring(strTrackedRules, 7, 1) = '1' then 'Expedited Permit' else ' ' end ExpeditedPermitRule
                FROM SSPPApplicationMaster AS m
                LEFT JOIN SSPPApplicationData AS d ON m.STRAPPLICATIONNUMBER = d.STRAPPLICATIONNUMBER
                LEFT JOIN SSPPApplicationTracking AS t ON m.STRAPPLICATIONNUMBER = t.STRAPPLICATIONNUMBER
                LEFT JOIN SSPPSubpartData AS s ON m.STRAPPLICATIONNUMBER = s.STRAPPLICATIONNUMBER
                LEFT JOIN APBHeaderData AS h ON m.STRAIRSNUMBER = h.STRAIRSNUMBER
                LEFT JOIN EPDUSerProfiles AS u ON m.STRSTAFFRESPONSIBLE = u.NUMUSERID
                LEFT JOIN LookUpApplicationTypes AS la ON m.STRAPPLICATIONTYPE = la.STRAPPLICATIONTYPECODE
                LEFT JOIN LookUpPermitTypes AS lp ON m.STRPERMITTYPE = lp.STRPERMITTYPECODE
                LEFT JOIN LookUpEPDUnits AS e ON m.APBUNIT = CONVERT( varchar(10), e.NUMUNITCODE)
                LEFT JOIN LookUpCountyInformation AS lc ON SUBSTRING(m.strAIRSNumber, 5, 3) = lc.STRCOUNTYCODE
                LEFT JOIN LookUPDistrictInformation AS ldi ON lc.STRCOUNTYCODE = ldi.STRDISTRICTCOUNTY
                LEFT JOIN LookUpDistricts AS ld ON ldi.STRDISTRICTCODE = ld.STRDISTRICTCODE
                WHERE 1=1 "

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            Select Case FieldType1
                Case "AIRS No."
                    SQLSearch1 = " m.strAIRSNumber like @SearchText1 "
                Case "Applicable Rules"
                    Select Case SearchText1b
                        Case "Any Rule"
                            SQLSearch1 = " d.strTrackedRules <> '0000000000' "
                        Case "PSD"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 1, 1) = '1' "
                        Case "NAA NSR"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 2, 1) = '1' "
                        Case "112(g)"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 3, 1) = '1' "
                        Case "Rule (tt) RACT"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 4, 1) = '1' "
                        Case "Rule (yy) RACT"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 5, 1) = '1' "
                        Case "Actuals PAL"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 6, 1) = '1' "
                        Case "Expedited Permit"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 7, 1) = '1' "
                        Case "Confidential information submitted"
                            SQLSearch1 = " SUBSTRING(d.strTrackedRules, 8, 1) = '1' "
                        Case Else
                            SQLSearch1 = " "
                    End Select
                Case "Application No."
                    SQLSearch1 = " m.strApplicationNumber like @SearchText1 "
                Case "Application Status"
                    Select Case SearchText1b
                        Case "0 - Unassigned"
                            SQLSearch1 = " (strStaffResponsible is Null or strStaffResponsible = '0') and datFinalizedDate is Null  and datPNExpires is Null  and datDraftIssued is Null and datToPMI is Null and datToPMII is Null and datReviewSubmitted Is Null "
                        Case "1 - At Engineer"
                            SQLSearch1 = " (datFinalizedDate is Null and datToDirector is Null and datToBranchCheif is Null and DatEPAEnds is Null and DatPNExpires is Null and datDraftIssued is Null and datToPMII is Null and datToPMI is null and datReviewSubmitted is Null and strStaffResponsible is Not Null and strStaffResponsible <> '0')  and datDraftIssued is Null "
                        Case "2 - Internal Review"
                            SQLSearch1 = " (datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')) and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null and datPNExpires is Null  and datDraftIssued is Null and datToPMI is Null "
                        Case "3 - At UC"
                            SQLSearch1 = " dattoPMI is Not Null and datToPMII is Null and datFinalizedDate is Null and datToBranchCheif is Null  and datToDirector is NUll and datPNExpires is Null and datDraftIssued is Null  "
                        Case "4 - At PM"
                            SQLSearch1 = " dattoPMII is Not Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is NUll and datPNExpires is Null and datDraftIssued is Null "
                        Case "5 - Draft Issued"
                            SQLSearch1 = "  datDraftIssued is Not Null and datPNExpires is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "6 - Public Notice"
                            SQLSearch1 = " datPNExpires is Not Null and datPNExpires >= GETDATE() and datFinalizedDate is Null and datEPAEnds is Null "
                        Case "7 - Public Notice Expired"
                            SQLSearch1 = " datPNExpires is Not Null and datPNExpires < GETDATE() and datToDirector is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "8 - EPA 45-day Review"
                            SQLSearch1 = " datEPAEnds is not Null and datFinalizedDate is Null and datDraftIssued is Not Null "
                        Case "9 - Administrative Review"
                            SQLSearch1 = " (datToBranchCheif is Not Null and datFinalizedDate is Null   and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) "
                        Case "11 - Closed Out"
                            SQLSearch1 = " datFinalizedDate is Not Null "
                        Case Else
                            SQLSearch1 = " "
                    End Select
                Case "Application Type"
                    If SearchText1b = "Other" Then
                        SQLSearch1 = " (strApplicationTypeDesc <>  'Acid Rain' and strApplicationTypeDesc <>  'AA' " &
                        " and strApplicationTypeDesc <>  'NC' and  strApplicationTypeDesc <>  'SAW' " &
                        "and strApplicationTypeDesc <>  'SAWO' and strApplicationTypeDesc <>  'MAW' " &
                        "and strApplicationTypeDesc <>  'MAWO' and strApplicationTypeDesc <>  'TV-Renewal' " &
                        "and strApplicationTypeDesc <>  '502(b)10' and strApplicationTypeDesc <>  'TV-Initial' " &
                        "and strApplicationTypeDesc <>  'SM' and strApplicationTypeDesc <>  'Closed' " &
                        "and strApplicationTypeDesc <>  'ERC' and strApplicationTypeDesc <>  'OFF PERMIT' " &
                        "and strApplicationTypeDesc <>  'PBR' and strApplicationTypeDesc <>  'SIP') "
                    Else
                        SQLSearch1 = " strApplicationTypeDesc like @SearchText1b "
                    End If
                Case "Application Unit"
                    SQLSearch1 = " e.strUnitDesc like @SearchText1b "
                Case "Applog Comments"
                    SQLSearch1 = " d.strComments like @SearchText1 "
                Case "Date Acknowledged"
                    SQLSearch1 = " datAcknowledgementLetterSent between @SearchDate1 and @SearchDate1b "
                Case "Date APL Completed"
                    SQLSearch1 = " datApplicationPackageComplete between @SearchDate1 and @SearchDate1b "
                Case "Date APL Dated"
                    SQLSearch1 = " datSentByFacility between @SearchDate1 and @SearchDate1b "
                Case "Date APL Received"
                    SQLSearch1 = " datReceivedDate between @SearchDate1 and @SearchDate1b "
                Case "Date Assigned"
                    SQLSearch1 = " datAssignedtoEngineer between @SearchDate1 and @SearchDate1b "
                Case "Date Draft Issued"
                    SQLSearch1 = " datDraftIssued between @SearchDate1 and @SearchDate1b "
                Case "Date PA Expires"
                    SQLSearch1 = " datPAExpires between  @SearchDate1 and @SearchDate1b "
                Case "Date Finalized"
                    SQLSearch1 = " datPermitIssued between  @SearchDate1 and @SearchDate1b "
                Case "Date PN Expires"
                    SQLSearch1 = " datPNExpires between @SearchDate1 and @SearchDate1b "
                Case "Date Reassigned"
                    SQLSearch1 = " datReassignedToEngineer between @SearchDate1 and @SearchDate1b "
                Case "Date Started Review"
                    SQLSearch1 = " datApplicationStarted between @SearchDate1 and @SearchDate1b "
                Case "Date to BC"
                    SQLSearch1 = " datToBranchCheif between @SearchDate1 and @SearchDate1b "
                Case "Date to DO"
                    SQLSearch1 = " datToDirector between @SearchDate1 and @SearchDate1b "
                Case "Date to PM"
                    SQLSearch1 = " datToPMII between @SearchDate1 and @SearchDate1b "
                Case "Date to UC"
                    SQLSearch1 = " datToPMI between @SearchDate1 and @SearchDate1b "
                Case "Date Withdrawn"
                    SQLSearch1 = " datWithdrawn between @SearchDate1 and @SearchDate1b "
                Case "Deadline"
                    SQLSearch1 = " datApplicationDeadLine between @SearchDate1 and @SearchDate1b "
                Case "Engineer Firstname"
                    SQLSearch1 = " strFirstName like @SearchText1 "
                Case "Engineer Lastname"
                    SQLSearch1 = " strLastName like @SearchText1 "
                Case "Engineer Unit Code"
                    SQLSearch1 = " APBUnit like @SearchText1 "
                Case "EPA 45-day Waived"
                    SQLSearch1 = " datEPAWaived between @SearchDate1 and @SearchDate1b "
                Case "EPA 45-day Ends"
                    SQLSearch1 = " datEPAEnds between @SearchDate1 and @SearchDate1b "
                Case "Facility City"
                    SQLSearch1 = " strFacilityCity like @SearchText1 "
                Case "Facility County"
                    SQLSearch1 = " strCountyName like @SearchText1 "
                Case "Facility Name"
                    SQLSearch1 = " strFacilityName like @SearchText1 "
                Case "Facility Street"
                    SQLSearch1 = " strFacilityStreet1 like @SearchText1 "
                Case "HAPs Major"
                    SQLSearch1 = " h.strStateProgramCodes like '_1___' "
                Case "NAA 1Hr-Yes"
                    SQLSearch1 = " h.strAttainmentStatus like '_1___' "
                Case "NAA 1Hr-Contr."
                    SQLSearch1 = " h.strAttainmentStatus like '_2___' "
                Case "NAA 1Hr-No"
                    SQLSearch1 = " h.strAttainmentStatus like '_0___' "
                Case "NAA 8Hr-Atlanta"
                    SQLSearch1 = " h.strAttainmentStatus like '__1__' "
                Case "NAA 8Hr-Macon"
                    SQLSearch1 = " h.strAttainmentStatus like '__2__' "
                Case "NAA 8Hr-No"
                    SQLSearch1 = " h.strAttainmentStatus like '__0__' "
                Case "NAA PM-Atlanta"
                    SQLSearch1 = " h.strAttainmentStatus like '___1_' "
                Case "NAA PM-Chattanooga"
                    SQLSearch1 = " h.strAttainmentStatus like '___2_' "
                Case "NAA PM-Floyd"
                    SQLSearch1 = " h.strAttainmentStatus like '___3_' "
                Case "NAA PM-Macon"
                    SQLSearch1 = " h.strAttainmentStatus like '___4_' "
                Case "NAA PM-No"
                    SQLSearch1 = " h.strAttainmentStatus like '___0_' "
                Case "NSR/PSD Major"
                    SQLSearch1 = " h.strStateProgramCodes like '1____' "
                Case "PA Ready"
                    SQLSearch1 = " strPAReady is Not Null and strPAReady = 'True' "
                Case "Permit Number"
                    SQLSearch1 = " strPermitNumber like @strPermitNumber "
                Case "Permit Type"
                    If SearchText1b = "Other" Then
                        SQLSearch1 = " strPermitType is Null "
                    Else
                        Select Case SearchText1b
                            Case "Amendment"
                                SQLSearch1 = " strPermitType = '1' "
                            Case "Denied"
                                SQLSearch1 = " strPermitType = '2' "
                            Case "Draft"
                                SQLSearch1 = " strPermitType = '3' "
                            Case "New Permit"
                                SQLSearch1 = " strPermitType = '4' "
                            Case "NPR"
                                SQLSearch1 = " strPermitType = '5' "
                            Case "PBR"
                                SQLSearch1 = " strPermitType = '6' "
                            Case "Permit"
                                SQLSearch1 = " strPermitType = '7' "
                            Case "PRMT-DNL"
                                SQLSearch1 = " strPermitType = '8' "
                            Case "Returned"
                                SQLSearch1 = " strPermitType = '9' "
                            Case "Revoked"
                                SQLSearch1 = " strPermitType = '10' "
                            Case "Withdrawn"
                                SQLSearch1 = " strPermitType = '11' "
                            Case "Initial Title V Permit"
                                SQLSearch1 = " strPermitType = '12' "
                            Case "Renewal Title V Permit"
                                SQLSearch1 = " strPermitType = '13' "
                            Case "N/A"
                                SQLSearch1 = " strPermitType = '0' "
                            Case Else
                                SQLSearch1 = " strPermitType is Null "
                        End Select
                    End If
                Case "Plant Description"
                    SQLSearch1 = " d.strPlantDescription like @SearchText1 "
                Case "PN Ready"
                    SQLSearch1 = " strPNReady is Not Null and strPNReady = 'True' "
                Case "Public Advisory"
                    Select Case SearchText1b
                        Case "PA Not Needed"
                            SQLSearch1 = " strPublicInvolvement = '2' "
                        Case "PA Needed"
                            SQLSearch1 = " strPublicInvolvement = '1' "
                        Case "Not Decided"
                            SQLSearch1 = " strPublicInvolvement = '0' "
                        Case Else
                            SQLSearch1 = " strPublicInvolvement = '0' "
                    End Select
                Case "Reason APL Submitted"
                    SQLSearch1 = " strApplicationNotes like @SearchText1 "
                Case "Regional District"
                    SQLSearch1 = " strDistrictName like @SearchText1b "
                Case "Status Date"
                    SQLSearch1 = " StatusDate between @SearchDate1 and @SearchDate1b "
                Case "SIC Code"
                    SQLSearch1 = " d.strSICCode like @SearchText1 "
                Case "Subpart - SIP"
                    SQLSearch1 = " ( s.strSubpart = @SubpartSIP1 " &
                    "and right(s.strSubpartKey, 1) = '0' ) "
                Case "Subpart - NESHAP (Part 61)"
                    SQLSearch1 = " ( s.strSubpart = @SubpartNESHAP1 " &
                    "and right(s.strSubpartKey, 1) = '8' ) "
                Case "Subpart - NSPS (Part 60)"
                    SQLSearch1 = " ( s.strSubpart = @SubpartNSPS1 " &
                    "and right(s.strSubpartKey, 1) = '9' ) "
                Case "Subpart - MACT (Part 63)"
                    SQLSearch1 = " ( s.strSubpart = @SubpartMACT1  " &
                    "and right(s.strSubpartKey, 1) = 'M' ) "
            End Select

            Select Case FieldType2
                Case "AIRS No."
                    SQLSearch2 = " m.strAIRSNumber like @SearchText2 "
                Case "Applicable Rules"
                    Select Case SearchText2b
                        Case "Any Rule"
                            SQLSearch2 = " d.strTrackedRules <> '0000000000' "
                        Case "PSD"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 1, 1) = '1' "
                        Case "NAA NSR"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 2, 1) = '1' "
                        Case "112(g)"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 3, 1) = '1' "
                        Case "Rule (tt) RACT"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 4, 1) = '1' "
                        Case "Rule (yy) RACT"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 5, 1) = '1' "
                        Case "Actuals PAL"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 6, 1) = '1' "
                        Case "Expedited Permit"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 7, 1) = '1' "
                        Case "Confidential information submitted"
                            SQLSearch2 = " SUBSTRING(d.strTrackedRules, 8, 1) = '1' "
                        Case Else
                            SQLSearch2 = " "
                    End Select
                Case "Application No."
                    SQLSearch2 = " m.strApplicationNumber like @SearchText2 "
                Case "Application Status"
                    Select Case SearchText2b
                        Case "0 - Unassigned"
                            SQLSearch2 = " (strStaffResponsible is Null or strStaffResponsible = '0') and datFinalizedDate is Null  and datPNExpires is Null  and datDraftIssued is Null and datToPMI is Null and datToPMII is Null and datReviewSubmitted Is Null "
                        Case "1 - At Engineer"
                            SQLSearch2 = " (datFinalizedDate is Null and datToDirector is Null and datToBranchCheif is Null and DatEPAEnds is Null and DatPNExpires is Null and datDraftIssued is Null and datToPMII is Null and datToPMI is null and datReviewSubmitted is Null and strStaffResponsible is Not Null and strStaffResponsible <> '0')  and datDraftIssued is Null "
                        Case "2 - Internal Review"
                            SQLSearch2 = " (datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')) and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null and datPNExpires is Null  and datDraftIssued is Null and datToPMI is Null "
                        Case "3 - At UC"
                            SQLSearch2 = " dattoPMI is Not Null and datToPMII is Null and datFinalizedDate is Null and datToBranchCheif is Null  and datToDirector is NUll and datPNExpires is Null and datDraftIssued is Null  "
                        Case "4 - At PM"
                            SQLSearch2 = " dattoPMII is Not Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is NUll and datPNExpires is Null and datDraftIssued is Null "
                        Case "5 - Draft Issued"
                            SQLSearch2 = "  datDraftIssued is Not Null and datPNExpires is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "6 - Public Notice"
                            SQLSearch2 = " datPNExpires is Not Null and datPNExpires >= GETDATE() and datFinalizedDate is Null and datEPAEnds is Null "
                        Case "7 - Public Notice Expired"
                            SQLSearch2 = " datPNExpires is Not Null and datPNExpires < GETDATE() and datToDirector is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "8 - EPA 45-day Review"
                            SQLSearch2 = " datEPAEnds is not Null and datFinalizedDate is Null and datDraftIssued is Not Null "
                        Case "9 - Administrative Review"
                            SQLSearch2 = " (datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) "
                        Case "11 - Closed Out"
                            SQLSearch2 = " datFinalizedDate is Not Null "
                        Case Else
                            SQLSearch2 = " "
                    End Select
                Case "Application Type"
                    If SearchText2b = "Other" Then
                        SQLSearch2 = " (strApplicationTypeDesc <>  'Acid Rain' and strApplicationTypeDesc <>  'AA' " &
                        " and strApplicationTypeDesc <>  'NC' and  strApplicationTypeDesc <>  'SAW' " &
                        "and strApplicationTypeDesc <>  'SAWO' and strApplicationTypeDesc <>  'MAW' " &
                        "and strApplicationTypeDesc <>  'MAWO' and strApplicationTypeDesc <>  'TV-Renewal' " &
                        "and strApplicationTypeDesc <>  '502(b)10' and strApplicationTypeDesc <>  'TV-Initial' " &
                        "and strApplicationTypeDesc <>  'SM' and strApplicationTypeDesc <>  'Closed' " &
                        "and strApplicationTypeDesc <>  'ERC' and strApplicationTypeDesc <>  'OFF PERMIT' " &
                        "and strApplicationTypeDesc <>  'PBR' and strApplicationTypeDesc <>  'SIP') "
                    Else
                        SQLSearch2 = " strApplicationTypeDesc like @SearchText2b "
                    End If
                Case "Application Unit"
                    SQLSearch2 = " e.strUnitDesc like @SearchText2b "
                Case "Applog Comments"
                    SQLSearch2 = " d.strComments like @SearchText2 "
                Case "Date Acknowledged"
                    SQLSearch2 = " datAcknowledgementLetterSent between @SearchDate2 and @SearchDate2b  "
                Case "Date APL Completed"
                    SQLSearch2 = " datApplicationPackageComplete between @SearchDate2 and @SearchDate2b  "
                Case "Date APL Dated"
                    SQLSearch2 = " datSentByFacility between @SearchDate2 and @SearchDate2b  "
                Case "Date APL Received"
                    SQLSearch2 = " datReceivedDate between @SearchDate2 and @SearchDate2b  "
                Case "Date Assigned"
                    SQLSearch2 = " datAssignedtoEngineer between @SearchDate2 and @SearchDate2b  "
                Case "Date Draft Issued"
                    SQLSearch2 = " datDraftIssued between @SearchDate2 and @SearchDate2b  "
                Case "Date PA Expires"
                    SQLSearch2 = " datPAExpires between @SearchDate2 and @SearchDate2b  "
                Case "Date Finalized"
                    SQLSearch2 = " datPermitIssued between @SearchDate2 and @SearchDate2b  "
                Case "Date PN Expires"
                    SQLSearch2 = " datPNExpires between @SearchDate2 and @SearchDate2b  "
                Case "Date Reassigned"
                    SQLSearch2 = " datReassignedToEngineer between @SearchDate2 and @SearchDate2b  "
                Case "Date Started Review"
                    SQLSearch2 = " datApplicationStarted between @SearchDate2 and @SearchDate2b  "
                Case "Date to BC"
                    SQLSearch2 = " datToBranchCheif between @SearchDate2 and @SearchDate2b  "
                Case "Date to DO"
                    SQLSearch2 = " datToDirector between @SearchDate2 and @SearchDate2b  "
                Case "Date to PM"
                    SQLSearch2 = " datToPMII between @SearchDate2 and @SearchDate2b  "
                Case "Date to UC"
                    SQLSearch2 = " datToPMI between @SearchDate2 and @SearchDate2b  "
                Case "Date Withdrawn"
                    SQLSearch2 = " datWithdrawn between @SearchDate2 and @SearchDate2b  "
                Case "Deadline"
                    SQLSearch2 = " datApplicationDeadLine between @SearchDate2 and @SearchDate2b  "
                Case "Engineer Firstname"
                    SQLSearch2 = " strFirstName like @SearchText2 "
                Case "Engineer Lastname"
                    SQLSearch2 = " strLastName like @SearchText2 "
                Case "Engineer Unit Code"
                    SQLSearch2 = " APBUnit like @SearchText2 "
                Case "EPA 45-day Waived"
                    SQLSearch2 = " datEPAWaived between @SearchDate2 and @SearchDate2b  "
                Case "EPA 45-day Ends"
                    SQLSearch2 = " datEPAEnds between @SearchDate2 and @SearchDate2b  "
                Case "Facility City"
                    SQLSearch2 = " strFacilityCity like @SearchText2 "
                Case "Facility County"
                    SQLSearch2 = " strCountyName like @SearchText2 "
                Case "Facility Name"
                    SQLSearch2 = " strFacilityName like @SearchText2 "
                Case "Facility Street"
                    SQLSearch2 = " strFacilityStreet1 like @SearchText2 "
                Case "HAPs Major"
                    SQLSearch2 = " h.strStateProgramCodes like '1____' "
                Case "NAA 1Hr-Yes"
                    SQLSearch2 = " h.strAttainmentStatus like '_1___' "
                Case "NAA 1Hr-Contr."
                    SQLSearch2 = " h.strAttainmentStatus like '_2___' "
                Case "NAA 1Hr-No"
                    SQLSearch2 = " h.strAttainmentStatus like '_0___' "
                Case "NAA 8Hr-Atlanta"
                    SQLSearch2 = " h.strAttainmentStatus like '__1__' "
                Case "NAA 8Hr-Macon"
                    SQLSearch2 = " h.strAttainmentStatus like '__2__' "
                Case "NAA 8Hr-No"
                    SQLSearch2 = " h.strAttainmentStatus like '__0__' "
                Case "NAA PM-Atlanta"
                    SQLSearch2 = " h.strAttainmentStatus like '___1_' "
                Case "NAA PM-Chattanooga"
                    SQLSearch2 = " h.strAttainmentStatus like '___2_' "
                Case "NAA PM-Floyd"
                    SQLSearch2 = " h.strAttainmentStatus like '___3_' "
                Case "NAA PM-Macon"
                    SQLSearch2 = " h.strAttainmentStatus like '___4_' "
                Case "NAA PM-No"
                    SQLSearch2 = " h.strAttainmentStatus like '___0_' "
                Case "NSR/PSD Major"
                    SQLSearch2 = " h.strStateProgramCodes like '1____' "
                Case "PA Ready"
                    SQLSearch2 = " strPAReady is Not Null and strPAReady = 'True' "
                Case "Permit Number"
                    SQLSearch2 = " strPermitNumber like @strPermitNumber2 "
                Case "Permit Type"
                    If SearchText2b = "Other" Then
                        SQLSearch2 = " strPermitType is Null "
                    Else
                        Select Case SearchText2b
                            Case "Amendment"
                                SQLSearch2 = " strPermitType = '1' "
                            Case "Denied"
                                SQLSearch2 = " strPermitType = '2' "
                            Case "Draft"
                                SQLSearch2 = " strPermitType = '3' "
                            Case "New Permit"
                                SQLSearch2 = " strPermitType = '4' "
                            Case "NPR"
                                SQLSearch2 = " strPermitType = '5' "
                            Case "PBR"
                                SQLSearch2 = " strPermitType = '6' "
                            Case "Permit"
                                SQLSearch2 = " strPermitType = '7' "
                            Case "PRMT-DNL"
                                SQLSearch2 = " strPermitType = '8' "
                            Case "Returned"
                                SQLSearch2 = " strPermitType = '9' "
                            Case "Revoked"
                                SQLSearch2 = " strPermitType = '10' "
                            Case "Withdrawn"
                                SQLSearch2 = " strPermitType = '11' "
                            Case "Initial Title V Permit"
                                SQLSearch2 = " strPermitType = '12' "
                            Case "Renewal Title V Permit"
                                SQLSearch2 = " strPermitType = '13' "
                            Case "N/A"
                                SQLSearch2 = " strPermitType = '0' "
                            Case Else
                                SQLSearch2 = " strPermitType is Null "
                        End Select
                    End If
                Case "Plant Description"
                    SQLSearch2 = " d.strPlantDescription like @SearchText2 "
                Case "PN Ready"
                    SQLSearch2 = " strPNReady is Not Null and strPNReady = 'True' "
                Case "Public Advisory"
                    Select Case SearchText2b
                        Case "PA Not Needed"
                            SQLSearch2 = " strPublicInvolvement = '2' "
                        Case "PA Needed"
                            SQLSearch2 = " strPublicInvolvement = '1' "
                        Case "Not Decided"
                            SQLSearch2 = " strPublicInvolvement = '0' "
                        Case Else
                            SQLSearch2 = " strPublicInvolvement = '0' "
                    End Select
                Case "Reason APL Submitted"
                    SQLSearch2 = " strApplicationNotes like @SearchText2 "
                Case "Regional District"
                    SQLSearch2 = " strDistrictName like @SearchText2b "
                Case "SIC Code"
                    SQLSearch2 = " d.strSICCode like @SearchText2 "
                Case "Status Date"
                    SQLSearch2 = " StatusDate between @SearchDate2 and @SearchDate2b "
                Case "Subpart - SIP"
                    SQLSearch2 = " ( s.strSubpart = @SubpartSIP2 " &
                    "and right(s.strSubpartKey, 1) = '0' ) "
                Case "Subpart - NESHAP (Part 61)"
                    SQLSearch2 = " ( s.strSubpart = @SubpartNESHAP2 " &
                    "and right(s.strSubpartKey, 1) = '8' ) "
                Case "Subpart - NSPS (Part 60)"
                    SQLSearch2 = " ( s.strSubpart = @SubpartNSPS2 " &
                    "and right(s.strSubpartKey, 1) = '9' ) "
                Case "Subpart - MACT (Part 63)"
                    SQLSearch2 = " ( s.strSubpart = @SubpartMACT2 " &
                    "and right(s.strSubpartKey, 1) = 'M' ) "
            End Select

            If FieldType1 = FieldType2 Then
                SQLLine = "and (" & SQLSearch1 & " or " & SQLSearch2 & ") "
            Else
                SQLLine = "and " & SQLSearch1 & " and " & SQLSearch2
            End If

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            If AppType <> "All" Then
                Select Case AppType
                    Case "Title V"
                        SQLLine = SQLLine & "and strApplicationTypeDesc like '%TV%' "
                    Case "SIP (Non Title V)"
                        SQLLine = SQLLine & "and strApplicationTypeDesc not like '%TV%' "
                End Select
            End If
            If AppUnitText <> "All" Then
                SQLLine = SQLLine & "and (e.strUnitDesc = @AppUnitText " &
                "or APBUnit = @AppUnit) "
            End If
            If AppStatus <> "All" Then
                Select Case AppStatus
                    Case "Active"
                        SQLLine = SQLLine & "and datFinalizedDate is NULL "
                    Case "Closed"
                        SQLLine = SQLLine & "and datFinalizedDate is Not NULL "
                End Select
            End If
            If Engineer <> "XXX" Then
                SQLLine = SQLLine & "and numUserID like @Engineer "
            End If

            SQLOrder = " order by "

            If SortOrder1 = "Descending Order" Then
                SortOrder1 = "Desc"
            Else
                SortOrder1 = " "
            End If

            Select Case Sort1
                Case "AIRS No."
                    SQLOrder = SQLOrder & " strAIRSNumber " & SortOrder1
                Case "Application No."
                    SQLOrder = SQLOrder & " strApplicationNumber " & SortOrder1
                Case "Application Status"
                    SQLOrder = SQLOrder & " AppStatus " & SortOrder1
                Case "Application Type"
                    SQLOrder = SQLOrder & " strApplicationType " & SortOrder1
                Case "Date APL Received"
                    SQLOrder = SQLOrder & " datReceivedDate " & SortOrder1
                Case "Date Finalized"
                    SQLOrder = SQLOrder & " datPermitIssued " & SortOrder1
                Case "Engineer Unit Code"
                    SQLOrder = SQLOrder & " APBUnit " & SortOrder1
                Case "Facility County"
                    SQLOrder = SQLOrder & " strCountyName " & SortOrder1
                Case "Facility Name"
                    SQLOrder = SQLOrder & " strFacilityName " & SortOrder1
                Case "Permit Number"
                    SQLOrder = SQLOrder & " strPermitNumber " & SortOrder1
                Case "Permit Type"
                    SQLOrder = SQLOrder & " strPermitType " & SortOrder1
                Case "Plant Description"
                    SQLOrder = SQLOrder & " strPlantDescription " & SortOrder1
                Case "SIC Code"
                    SQLOrder = SQLOrder & " strSICCode " & SortOrder1
            End Select

            If SQLOrder <> " order by " Then
                SQLOrder = SQLOrder & ", "
            End If

            If SortOrder2 = "Descending Order" Then
                SortOrder2 = "Desc"
            Else
                SortOrder2 = " "
            End If

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            If Sort1 <> Sort2 Then
                Select Case Sort2
                    Case "AIRS No."
                        SQLOrder = SQLOrder & " strAIRSNumber " & SortOrder2
                    Case "Application No."
                        SQLOrder = SQLOrder & " strApplicationNumber " & SortOrder2
                    Case "Application Status"
                        SQLOrder = SQLOrder & " AppStatus " & SortOrder2
                    Case "Application Type"
                        SQLOrder = SQLOrder & " strApplicationType " & SortOrder2
                    Case "Date APL Received"
                        SQLOrder = SQLOrder & " datReceivedDate " & SortOrder2
                    Case "Date Finalized"
                        SQLOrder = SQLOrder & " datPermitIssued " & SortOrder2
                    Case "Engineer Unit Code"
                        SQLOrder = SQLOrder & " APBUnit " & SortOrder2
                    Case "Facility County"
                        SQLOrder = SQLOrder & " strCountyName " & SortOrder2
                    Case "Facility Name"
                        SQLOrder = SQLOrder & " strFacilityName " & SortOrder2
                    Case "Permit Number"
                        SQLOrder = SQLOrder & " strPermitNumber " & SortOrder2
                    Case "Permit Type"
                        SQLOrder = SQLOrder & " strPermitType " & SortOrder2
                    Case "Plant Description"
                        SQLOrder = SQLOrder & " strPlantDescription " & SortOrder2
                    Case "SIC Code"
                        SQLOrder = SQLOrder & " strSICCode " & SortOrder2
                End Select

                If bgwApplicationLog.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            If Mid(SQLOrder, (Len(SQLOrder) - 1)) = ", " Then
                SQLOrder = Mid(SQLOrder, 1, (Len(SQLOrder) - 2))
            End If

            SQLLine = SQLLine & SQLOrder

            SQL = SQL & SQLLine

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@SearchText1", "%" & SearchText1 & "%"),
                New SqlParameter("@SearchText1b", SearchText1b & "%"),
                New SqlParameter("@SearchDate1", SearchDate1),
                New SqlParameter("@SearchDate1b", SearchDate1b),
                New SqlParameter("@SearchText2", "%" & SearchText2 & "%"),
                New SqlParameter("@SearchText2b", "%" & SearchText2b & "%"),
                New SqlParameter("@SearchDate2", SearchDate2),
                New SqlParameter("@SearchDate2b", SearchDate2b),
                New SqlParameter("@SubpartSIP1", SubpartSIP1),
                New SqlParameter("@SubpartSIP2", SubpartSIP2),
                New SqlParameter("@SubpartNSPS1", SubpartNSPS1),
                New SqlParameter("@SubpartNSPS2", SubpartNSPS2),
                New SqlParameter("@SubpartNESHAP1", SubpartNESHAP1),
                New SqlParameter("@SubpartNESHAP2", SubpartNESHAP2),
                New SqlParameter("@SubpartMACT1", SubpartMACT1),
                New SqlParameter("@SubpartMACT2", SubpartMACT2),
                New SqlParameter("@AppUnit", AppUnit),
                New SqlParameter("@AppUnitText", AppUnitText),
                New SqlParameter("@AppStatus", AppStatus),
                New SqlParameter("@Engineer", Engineer),
                New SqlParameter("@strPermitNumber", "%" & Replace(SearchText1, "-", "") & "%"),
                New SqlParameter("@strPermitNumber2", "%" & Replace(SearchText2, "-", "") & "%")
            }

            dtApplicationLog = DB.GetDataTable(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If bgwApplicationLog.CancellationPending Then e.Cancel = True
        End Try
    End Sub

    Private Sub bgwApplicationLog_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwApplicationLog.RunWorkerCompleted
        btnFind.Enabled = True
        btnResetSearch.Enabled = True
        mmiResetSearch.Enabled = True
        btnOpen.Enabled = False
        mmiOpen.Enabled = False
        btnExport.Enabled = False
        mmiExport.Enabled = False

        If e.Cancelled Then
            dtApplicationLog = Nothing

            dgvApplicationLog.Visible = False
            lblMessage.Text = "Search canceled"
            lblMessage.Visible = True

            Exit Sub
        End If

        If dtApplicationLog Is Nothing OrElse dtApplicationLog.Rows.Count = 0 Then
            dgvApplicationLog.Visible = False
            Panel1.Text = "No applications found"
            lblMessage.Text = "No applications found"
            lblMessage.Visible = True
        Else
            dgvApplicationLog.DataSource = dtApplicationLog

            dgvApplicationLog.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvApplicationLog.Columns("strApplicationNumber").DisplayIndex = 0
            dgvApplicationLog.Columns("strApplicationType").HeaderText = "APL Type"
            dgvApplicationLog.Columns("strApplicationType").DisplayIndex = 4
            dgvApplicationLog.Columns("datReceivedDate").HeaderText = "APL Rcvd"
            dgvApplicationLog.Columns("datReceivedDate").DisplayIndex = 5
            dgvApplicationLog.Columns("datReceivedDate").DefaultCellStyle.Format = DateFormat
            dgvApplicationLog.Columns("datPermitIssued").HeaderText = "Permit Issued"
            dgvApplicationLog.Columns("datPermitIssued").DisplayIndex = 6
            dgvApplicationLog.Columns("datPermitIssued").Visible = False
            dgvApplicationLog.Columns("datPermitIssued").DefaultCellStyle.Format = DateFormat
            dgvApplicationLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvApplicationLog.Columns("StaffResponsible").DisplayIndex = 3
            dgvApplicationLog.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvApplicationLog.Columns("strFacilityName").DisplayIndex = 1
            dgvApplicationLog.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvApplicationLog.Columns("strAIRSNumber").DisplayIndex = 2
            dgvApplicationLog.Columns("strPermitNumber").HeaderText = "Permit Number"
            dgvApplicationLog.Columns("strPermitNumber").DisplayIndex = 7
            dgvApplicationLog.Columns("strPermitType").HeaderText = "Permit Type"
            dgvApplicationLog.Columns("strPermitType").DisplayIndex = 8
            dgvApplicationLog.Columns("AppStatus").HeaderText = "App Status"
            dgvApplicationLog.Columns("AppStatus").DisplayIndex = 9
            dgvApplicationLog.Columns("StatusDate").HeaderText = "Status Date"
            dgvApplicationLog.Columns("StatusDate").DisplayIndex = 10
            dgvApplicationLog.Columns("StatusDate").DefaultCellStyle.Format = DateFormat
            'dgvApplicationLog.Columns("StatusDate").DefaultCellStyle.NullValue = "Unknown"
            dgvApplicationLog.Columns("strSICCode").HeaderText = "SIC Code"
            dgvApplicationLog.Columns("strSICCode").DisplayIndex = 11
            dgvApplicationLog.Columns("strPlantDescription").HeaderText = "Plant Description"
            dgvApplicationLog.Columns("strPlantDescription").DisplayIndex = 12
            dgvApplicationLog.Columns("strCountyName").HeaderText = "County"
            dgvApplicationLog.Columns("APBUnit").HeaderText = "APL Unit"
            dgvApplicationLog.Columns("ExpeditedPermitRule").HeaderText = "Expedited Permit"

            If dtApplicationLog.Rows.Count = 1 Then
                Panel1.Text = dtApplicationLog.Rows.Count & " application found"
            Else
                Panel1.Text = dtApplicationLog.Rows.Count & " applications found"
            End If

            dgvApplicationLog.MakeColumnLookLikeLinks(0)
            dgvApplicationLog.Visible = True
            dgvApplicationLog.SanelyResizeColumns()

            lblMessage.Visible = False
            btnExport.Enabled = True
            mmiExport.Enabled = True
        End If
    End Sub

#End Region

#Region " Other procedures "

    Private Sub ExportToExcel()
        dgvApplicationLog.ExportToExcel(Me)
    End Sub

    Private Sub StartNewApplication()
        If AccountFormAccess(3, 4) = "1" Then
            OpenFormNewPermitApplication()
        Else
            MessageBox.Show("You do not have sufficient permissions to start a new application.")
        End If
    End Sub

    Private Sub OpenApplication(applicationNumber As String)
        OpenFormPermitApplication(applicationNumber)
    End Sub

#End Region

#Region " Field Type ComboBox Events"

    Private Sub cboFieldType1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFieldType1.SelectedIndexChanged
        Select Case cboFieldType1.Text
            Case "AIRS No."
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Applicable Rules"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("Any Rule")
                cboSearchText1.Items.Add("PSD")
                cboSearchText1.Items.Add("NAA NSR")
                cboSearchText1.Items.Add("112(g)")
                cboSearchText1.Items.Add("Rule (tt) RACT")
                cboSearchText1.Items.Add("Rule (yy) RACT")
                cboSearchText1.Items.Add("Actuals PAL")
                cboSearchText1.Items.Add("Expedited Permit")
                cboSearchText1.Items.Add("Confidential information submitted")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)
            Case "Application No."
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

            Case "Application Status"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("0 - Unassigned")
                cboSearchText1.Items.Add("1 - At Engineer")
                cboSearchText1.Items.Add("2 - Internal Review")
                cboSearchText1.Items.Add("3 - At UC")
                cboSearchText1.Items.Add("4 - At PM")
                cboSearchText1.Items.Add("5 - Draft Issued")
                cboSearchText1.Items.Add("6 - Public Notice")
                cboSearchText1.Items.Add("7 - Public Notice Expired")
                cboSearchText1.Items.Add("8 - EPA 45-day Review")
                cboSearchText1.Items.Add("9 - Administrative Review")
                cboSearchText1.Items.Add("11 - Closed Out")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)
            Case "Application Type"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("502(b)10")
                cboSearchText1.Items.Add("Acid Rain")
                cboSearchText1.Items.Add("AA")
                cboSearchText1.Items.Add("Closed")
                cboSearchText1.Items.Add("ERC")
                cboSearchText1.Items.Add("SAW")
                cboSearchText1.Items.Add("SAWO")
                cboSearchText1.Items.Add("MAW")
                cboSearchText1.Items.Add("MAWO")
                cboSearchText1.Items.Add("NC")
                cboSearchText1.Items.Add("OFF Permit")
                cboSearchText1.Items.Add("Other")
                cboSearchText1.Items.Add("PBR")
                cboSearchText1.Items.Add("SIP")
                cboSearchText1.Items.Add("SM")
                cboSearchText1.Items.Add("TV-Initial")
                cboSearchText1.Items.Add("TV-Renewal")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)

            Case "Application Unit"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("Chemical Permitting")
                cboSearchText1.Items.Add("Combustion Permitting")
                cboSearchText1.Items.Add("Mineral Permitting")
                cboSearchText1.Items.Add("NOx Permitting")
                cboSearchText1.Items.Add("SSPP Administrative")
                cboSearchText1.Items.Add("VOC Permitting")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)

            Case "Applog Comments"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Acknowledged"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date APL Completed"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date APL Dated"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date APL Received"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Assigned"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Draft Issued"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date PA Expires"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Finalized"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date PN Expires"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Reassigned"
                txtSearchText1.Visible = False
                cboSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Started Review"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date to BC"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date to DO"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date to PM"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date to UC"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Date Withdrawn"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Deadline"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Engineer Firstname"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Engineer Lastname"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Engineer Unit Code"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "EPA 45-day Waived"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "EPA 45-day Ends"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Facility City"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Facility County"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Facility Name"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Facility Street"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "HAPs Major"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 1Hr-Yes"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 1Hr-Contr."
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 1Hr-No"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 8Hr-Atlanta"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 8Hr-Macon"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA 8Hr-No"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA PM-Atlanta"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA PM-Chattanooga"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA PM-Floyd"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA PM-Macon"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NAA PM-No"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "NSR/PSD Major"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "PA Ready"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Permit Number"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Permit Type"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("Denied")
                cboSearchText1.Items.Add("N/A")
                cboSearchText1.Items.Add("NPR")
                cboSearchText1.Items.Add("Other")
                cboSearchText1.Items.Add("PBR")
                cboSearchText1.Items.Add("Permit")
                cboSearchText1.Items.Add("Returned")
                cboSearchText1.Items.Add("Revoked")
                cboSearchText1.Items.Add("Withdrawn")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)
            Case "Plant Description"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "PN Ready"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Public Advisory"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("PA Not Needed")
                cboSearchText1.Items.Add("PA Needed")
                cboSearchText1.Items.Add("Not Decided")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)

            Case "Reason APL Submitted"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Regional District"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = True
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False

                cboSearchText1.Items.Clear()
                cboSearchText1.Items.Add("Coastal B")
                cboSearchText1.Items.Add("Coastal S")
                cboSearchText1.Items.Add("East Central")
                cboSearchText1.Items.Add("Mountain A")
                cboSearchText1.Items.Add("Mountain C")
                cboSearchText1.Items.Add("Northeast")
                cboSearchText1.Items.Add("Southwest")
                cboSearchText1.Items.Add("Statewide")
                cboSearchText1.Items.Add("West Central")
                cboSearchText1.Text = cboSearchText1.Items.Item(0)

            Case "Status Date"
                txtSearchText1.Visible = False
                DTPSearchDate1.Visible = True
                DTPSearchDate1b.Visible = True
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "SIC Code"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Subpart - SIP"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = True
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Subpart - NESHAP (Part 61)"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = True
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
            Case "Subpart - NSPS (Part 60)"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = True
                cboMACT1.Visible = False
            Case "Subpart - MACT (Part 63)"
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = True
            Case Else
                txtSearchText1.Visible = True
                DTPSearchDate1.Visible = False
                DTPSearchDate1b.Visible = False
                cboSearchText1.Visible = False
                cboSIP1.Visible = False
                cboNESHAP1.Visible = False
                cboNSPS1.Visible = False
                cboMACT1.Visible = False
        End Select
    End Sub

    Private Sub cboFieldType2_TextChanged(sender As Object, e As EventArgs) Handles cboFieldType2.SelectedIndexChanged
        Select Case cboFieldType2.Text
            Case "AIRS No."
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Applicable Rules"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("Any Rule")
                cboSearchText2.Items.Add("PSD")
                cboSearchText2.Items.Add("NAA NSR")
                cboSearchText2.Items.Add("112(g)")
                cboSearchText2.Items.Add("Rule (tt) RACT")
                cboSearchText2.Items.Add("Rule (yy) RACT")
                cboSearchText2.Items.Add("Actuals PAL")
                cboSearchText2.Items.Add("Expedited Permit")
                cboSearchText2.Items.Add("Confidential information submitted")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)
            Case "Application No."
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Application Status"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("0 - Unassigned")
                cboSearchText2.Items.Add("1 - At Engineer")
                cboSearchText2.Items.Add("2 - Internal Review")
                cboSearchText2.Items.Add("3 - At UC")
                cboSearchText2.Items.Add("4 - At PM")
                cboSearchText2.Items.Add("5 - Draft Issued")
                cboSearchText2.Items.Add("6 - Public Notice")
                cboSearchText2.Items.Add("7 - Public Notice Expired")
                cboSearchText2.Items.Add("8 - EPA 45-day Review")
                cboSearchText2.Items.Add("9 - Administrative Review")
                cboSearchText2.Items.Add("11 - Closed Out")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)

            Case "Application Type"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("502(b)10")
                cboSearchText2.Items.Add("Acid Rain")
                cboSearchText2.Items.Add("AA")
                cboSearchText2.Items.Add("Closed")
                cboSearchText2.Items.Add("ERC")
                cboSearchText2.Items.Add("SAW")
                cboSearchText2.Items.Add("SAWO")
                cboSearchText2.Items.Add("MAW")
                cboSearchText2.Items.Add("MAWO")
                cboSearchText2.Items.Add("NC")
                cboSearchText2.Items.Add("OFF Permit")
                cboSearchText2.Items.Add("Other")
                cboSearchText2.Items.Add("PBR")
                cboSearchText2.Items.Add("SIP")
                cboSearchText2.Items.Add("SM")
                cboSearchText2.Items.Add("TV-Initial")
                cboSearchText2.Items.Add("TV-Renewal")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)

            Case "Application Unit"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("Chemical Permitting")
                cboSearchText2.Items.Add("Combustion Permitting")
                cboSearchText2.Items.Add("Mineral Permitting")
                cboSearchText2.Items.Add("NOx Permitting")
                cboSearchText2.Items.Add("SSPP Administrative")
                cboSearchText2.Items.Add("VOC Permitting")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)

            Case "Applog Comments"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Acknowledged"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date APL Completed"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date APL Dated"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date APL Received"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Assigned"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Draft Issued"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date PA Expires"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Finalized"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date PN Expires"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Reassigned"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Started Review"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date to BC"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date to DO"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date to PM"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date to UC"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Date Withdrawn"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Deadline"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Engineer Firstname"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Engineer Lastname"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Engineer Unit Code"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "EPA 45-day Waived"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "EPA 45-day Ends"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Facility City"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Facility County"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Facility Name"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Facility Street"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "HAPs Major"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 1Hr-Yes"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 1Hr-Contr."
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 1Hr-No"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 8Hr-Atlanta"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 8Hr-Macon"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA 8Hr-No"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA PM-Atlanta"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA PM-Chattanooga"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA PM-Floyd"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA PM-Macon"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NAA PM-No"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "NSR/PSD Major"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "PA Ready"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Permit Number"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Permit Type"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("Denied")
                cboSearchText2.Items.Add("N/A")
                cboSearchText2.Items.Add("NPR")
                cboSearchText2.Items.Add("Other")
                cboSearchText2.Items.Add("PBR")
                cboSearchText2.Items.Add("Permit")
                cboSearchText2.Items.Add("Returned")
                cboSearchText2.Items.Add("Revoked")
                cboSearchText2.Items.Add("Withdrawn")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)
            Case "Plant Description"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "PN Ready"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Public Advisory"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("PA Not Needed")
                cboSearchText2.Items.Add("PA Needed")
                cboSearchText2.Items.Add("Not Decided")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)

            Case "Reason APL Submitted"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Regional District"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = True
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

                cboSearchText2.Items.Clear()
                cboSearchText2.Items.Add("Coastal B")
                cboSearchText2.Items.Add("Coastal S")
                cboSearchText2.Items.Add("East Central")
                cboSearchText2.Items.Add("Mountain A")
                cboSearchText2.Items.Add("Mountain C")
                cboSearchText2.Items.Add("Northeast")
                cboSearchText2.Items.Add("Southwest")
                cboSearchText2.Items.Add("Statewide")
                cboSearchText2.Items.Add("West Central")
                cboSearchText2.Text = cboSearchText2.Items.Item(0)

            Case "Status Date"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = True
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "SIC Code"
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False

            Case "Subpart - SIP"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = True
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Subpart - NESHAP (Part 61)"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = True
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
            Case "Subpart - NSPS (Part 60)"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = True
                cboMACT2.Visible = False
            Case "Subpart - MACT (Part 63)"
                txtSearchText2.Visible = False
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = True
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = True
            Case Else
                txtSearchText2.Visible = True
                DTPSearchDate2.Visible = False
                DTPSearchDate2b.Visible = False
                cboSearchText2.Visible = False
                cboSIP2.Visible = False
                cboNESHAP2.Visible = False
                cboNSPS2.Visible = False
                cboMACT2.Visible = False
        End Select
    End Sub

#End Region

#Region " DataGridView Events "

    Private Sub dgvApplicationLog_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicationLog.CellMouseEnter
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        If e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index And e.RowIndex <> -1 Then
            dgvApplicationLog.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, True)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicationLog.CellMouseLeave
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index And e.RowIndex <> -1 Then
            dgvApplicationLog.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, False)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicationLog.CellClick
        ' Anywhere in any cell in any non-header row in grid
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount Then
            selectedApp = dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value
            btnOpen.Enabled = True
            mmiOpen.Enabled = True
        End If

        ' Only within the cell content of first column (App #)
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount _
            And e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index Then
            OpenApplication(dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicationLog.CellDoubleClick
        'Double-click within the cell content (exclude first column to avoid double-firing)
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount _
            And e.ColumnIndex <> dgvApplicationLog.Columns("strApplicationNumber").Index Then
            OpenApplication(dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplicationLog.CellEnter
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount Then
            selectedApp = dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value
            btnOpen.Enabled = True
            mmiOpen.Enabled = True
        End If
    End Sub

#End Region

#Region "Menu, buttons, and toolbar"

    Private Sub NewApplication_Click(sender As Object, e As EventArgs) Handles mmiNewApplication.Click
        StartNewApplication()
    End Sub

    Private Sub mmiClose_Click(sender As Object, e As EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiOpen_Click(sender As Object, e As EventArgs) Handles mmiOpen.Click, btnOpen.Click
        If selectedApp <> "" Then OpenApplication(selectedApp)
    End Sub

    Private Sub mmiReset_Click(sender As Object, e As EventArgs) Handles mmiResetSearch.Click, btnResetSearch.Click
        LoadDefaults()
    End Sub

    Private Sub mmiExport_Click(sender As Object, e As EventArgs) Handles mmiExport.Click, btnExport.Click
        ExportToExcel()
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        RunSearch()
    End Sub

#End Region

End Class