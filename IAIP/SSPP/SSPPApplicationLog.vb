Imports Oracle.DataAccess.Client

Public Class SSPPApplicationLog
    Inherits BaseForm

#Region "Initialize variables"
    ' Local variables
    Private SQL As String
    Private cmd As OracleCommand
    Private dr As OracleDataReader
    Private dsApplication As DataSet
    'Private daApplication As OracleDataAdapter
    Private dsUnitList As DataSet
    Private daUnitList As OracleDataAdapter
    Private dsEngineerList As DataSet
    Private daEngineerList As OracleDataAdapter
    Private dsSubpart As DataSet
    Private daSubpart As OracleDataAdapter
    Private SQLLine As String
    Private SQLSearch1 As String
    Private SQLSearch2 As String
    Private SQLOrder As String
    Private temp As String
    Private selectedApp As String = ""

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

#Region "Page load/unload procedures"
    Private Sub SSPPApplicationLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            dgvApplicationLog.Visible = False
            'dgvApplicationLog.DataSource = dsApplication
            lblMessage.Text = "Loading…"
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadComboBoxes()
        Dim dtUnitList As New DataTable
        Dim dtEngineerList As New DataTable
        Dim dtSIP As New DataTable
        Dim dtSIP2 As New DataTable
        Dim dtNSPS As New DataTable
        Dim dtNSPS2 As New DataTable
        Dim dtNESHAP As New DataTable
        Dim dtNESHAP2 As New DataTable
        Dim dtMACT As New DataTable
        Dim dtMACT2 As New DataTable

        Dim drNewRow As DataRow
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drDSRow3 As DataRow

        Try

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
            'cboFieldType1.Items.Add("Engineer Unit Code")
            cboFieldType1.Items.Add("EPA 45-day Waived")
            cboFieldType1.Items.Add("EPA 45-day Ends")
            cboFieldType1.Items.Add("Facilty City")
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
            cboFieldType1.Items.Add("Regional Office")
            cboFieldType1.Items.Add("SIC Code")
            cboFieldType1.Items.Add("Subpart - 0-SIP")
            cboFieldType1.Items.Add("Subpart - 8-NESHAP (Part 61)")
            cboFieldType1.Items.Add("Subpart - 9-NSPS (Part 60)")
            cboFieldType1.Items.Add("Subpart - M-MACT (Part 63)")

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
            'cboFieldType2.Items.Add("Engineer Unit Code")
            cboFieldType2.Items.Add("EPA 45-day Waived")
            cboFieldType2.Items.Add("EPA 45-day Ends")
            cboFieldType2.Items.Add("Facilty City")
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
            cboFieldType2.Items.Add("Regional Office")
            cboFieldType2.Items.Add("SIC Code")
            cboFieldType2.Items.Add("Subpart - 0-SIP")
            cboFieldType2.Items.Add("Subpart - 8-NESHAP (Part 61)")
            cboFieldType2.Items.Add("Subpart - 9-NSPS (Part 60)")
            cboFieldType2.Items.Add("Subpart - M-MACT (Part 63)")

            cboSort1.Items.Add("AIRS No.")
            cboSort1.Items.Add("Applicable Rules")
            cboSort1.Items.Add("Application No.")
            cboSort1.Items.Add("Application Status")
            cboSort1.Items.Add("Application Type")
            cboSort1.Items.Add("Application Unit")
            cboSort1.Items.Add("Applog Comments")
            cboSort1.Items.Add("Date Acknowledged")
            cboSort1.Items.Add("Date APL Dated")
            cboSort1.Items.Add("Date APL Received")
            cboSort1.Items.Add("Date Assigned")
            cboSort1.Items.Add("Date Draft Issued")
            cboSort1.Items.Add("Date PA Expires")
            cboSort1.Items.Add("Date Finalized")
            cboSort1.Items.Add("Date PN Expires")
            cboSort1.Items.Add("Date Reassigned")
            cboSort1.Items.Add("Date to BC")
            cboSort1.Items.Add("Date to DO")
            cboSort1.Items.Add("Date to PM")
            cboSort1.Items.Add("Date to UC")
            cboSort1.Items.Add("Deadline")
            cboSort1.Items.Add("Engineer Firstname")
            cboSort1.Items.Add("Engineer Lastname")
            'cboSort1.Items.Add("Engineer Unit Code")
            cboSort1.Items.Add("EPA 45-day Waived")
            cboSort1.Items.Add("EPA 45-day Ends")
            cboSort1.Items.Add("Facilty City")
            cboSort1.Items.Add("Facility County")
            cboSort1.Items.Add("Facility Name")
            cboSort1.Items.Add("Facility Street")
            cboSort1.Items.Add("NAA 1Hr-Yes")
            cboSort1.Items.Add("NAA 1Hr-Contr.")
            cboSort1.Items.Add("NAA 1Hr-No")
            cboSort1.Items.Add("NAA 8Hr-Atlanta")
            cboSort1.Items.Add("NAA 8Hr-Macon")
            cboSort1.Items.Add("NAA 8Hr-No")
            cboSort1.Items.Add("NAA PM-Atlanta")
            cboSort1.Items.Add("NAA PM-Chattanooga")
            cboSort1.Items.Add("NAA PM-Floyd")
            cboSort1.Items.Add("NAA PM-Macon")
            cboSort1.Items.Add("NAA PM-No")
            cboSort1.Items.Add("PA Ready")
            cboSort1.Items.Add("Permit Number")
            cboSort1.Items.Add("Permit Type")
            cboSort1.Items.Add("Plant Description")
            cboSort1.Items.Add("PN Ready")
            cboSort1.Items.Add("Public Advisory")
            cboSort1.Items.Add("Reason APL Submitted")
            cboSort1.Items.Add("Regional District")
            cboSort1.Items.Add("Regional Office")
            cboSort1.Items.Add("SIC Code")
            cboSort1.Items.Add("Subpart - 0-SIP")
            cboSort1.Items.Add("Subpart - 8-NESHAP (Part 61)")
            cboSort1.Items.Add("Subpart - 9-NSPS (Part 60)")
            cboSort1.Items.Add("Subpart - M-MACT (Part 63)")

            cboSort2.Items.Add("AIRS No.")
            cboSort2.Items.Add("Applicable Rules")
            cboSort2.Items.Add("Application No.")
            cboSort2.Items.Add("Application Status")
            cboSort2.Items.Add("Application Type")
            cboSort2.Items.Add("Application Unit")
            cboSort2.Items.Add("Applog Comments")
            cboSort2.Items.Add("Date Acknowledged")
            cboSort2.Items.Add("Date APL Dated")
            cboSort2.Items.Add("Date APL Received")
            cboSort2.Items.Add("Date Assigned")
            cboSort2.Items.Add("Date Draft Issued")
            cboSort2.Items.Add("Date PA Expires")
            cboSort2.Items.Add("Date Finalized")
            cboSort2.Items.Add("Date PN Expires")
            cboSort2.Items.Add("Date Reassigned")
            cboSort2.Items.Add("Date to BC")
            cboSort2.Items.Add("Date to DO")
            cboSort2.Items.Add("Date to PM")
            cboSort2.Items.Add("Date to UC")
            cboSort2.Items.Add("Deadline")
            cboSort2.Items.Add("Engineer Firstname")
            cboSort2.Items.Add("Engineer Lastname")
            'cboSort2.Items.Add("Engineer Unit Code")
            cboSort2.Items.Add("EPA 45-day Waived")
            cboSort2.Items.Add("EPA 45-day Ends")
            cboSort2.Items.Add("Facilty City")
            cboSort2.Items.Add("Facility County")
            cboSort2.Items.Add("Facility Name")
            cboSort2.Items.Add("Facility Street")
            cboSort2.Items.Add("NAA 1Hr-Yes")
            cboSort2.Items.Add("NAA 1Hr-Contr.")
            cboSort2.Items.Add("NAA 1Hr-No")
            cboSort2.Items.Add("NAA 8Hr-Atlanta")
            cboSort2.Items.Add("NAA 8Hr-Macon")
            cboSort2.Items.Add("NAA 8Hr-No")
            cboSort2.Items.Add("NAA PM-Atlanta")
            cboSort2.Items.Add("NAA PM-Chattanooga")
            cboSort2.Items.Add("NAA PM-Floyd")
            cboSort2.Items.Add("NAA PM-Macon")
            cboSort2.Items.Add("NAA PM-No")
            cboSort2.Items.Add("PA Ready")
            cboSort2.Items.Add("Permit Number")
            cboSort2.Items.Add("Permit Type")
            cboSort2.Items.Add("Plant Description")
            cboSort2.Items.Add("PN Ready")
            cboSort2.Items.Add("Public Advisory")
            cboSort2.Items.Add("Reason APL Submitted")
            cboSort2.Items.Add("Regional District")
            cboSort2.Items.Add("Regional Office")
            cboSort2.Items.Add("SIC Code")
            cboSort2.Items.Add("Subpart - 0-SIP")
            cboSort2.Items.Add("Subpart - 8-NESHAP (Part 61)")
            cboSort2.Items.Add("Subpart - 9-NSPS (Part 60)")
            cboSort2.Items.Add("Subpart - M-MACT (Part 63)")

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

            SQL = "select " & _
            "strUnitDesc, numUnitCode " & _
            "from AIRBRANCH.LookUpEPDUnits " & _
            "where numProgramCode = '5' " & _
            "order by strUnitDesc "

            dsUnitList = New DataSet
            daUnitList = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daUnitList.Fill(dsUnitList, "UnitList")

            dtUnitList.Columns.Add("strUnitDesc", GetType(System.String))
            dtUnitList.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtUnitList.NewRow()
            drNewRow("strUnitDesc") = "All"
            drNewRow("numUnitCode") = "All"
            dtUnitList.Rows.Add(drNewRow)

            For Each drDSRow2 In dsUnitList.Tables("UnitList").Rows()
                drNewRow = dtUnitList.NewRow
                drNewRow("strUnitDesc") = drDSRow2("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow2("numUnitCode")
                dtUnitList.Rows.Add(drNewRow)
            Next

            With cboApplicationUnit
                .DataSource = dtUnitList
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

            SQL = "Select " & _
            "Distinct((strLastName|| ', ' ||strFirstName)) as EngineerName,  " & _
            "numUserID, strLastName   " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSPPApplicationMaster  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID " & _
            "order by strLastName "

            dsEngineerList = New DataSet
            daEngineerList = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEngineerList.Fill(dsEngineerList, "EngineerList")

            dtEngineerList.Columns.Add("EngineerName", GetType(System.String))
            dtEngineerList.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtEngineerList.NewRow()
            drNewRow("EngineerName") = "All"
            drNewRow("numUserID") = "XXX"
            dtEngineerList.Rows.Add(drNewRow)

            For Each drDSRow In dsEngineerList.Tables("EngineerList").Rows()
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

            dsSubpart = New DataSet

            SQL = "Select " & _
            "strSubpart, " & _
            "(strSubpart||' - '||strDescription) as Subpart " & _
            "from AIRBRANCH.LookUpSubpartSIP " & _
            "order by strSubpart "

            daSubpart = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSubpart.Fill(dsSubpart, "SubpartSIP")

            dtSIP.Columns.Add("strSubpart", GetType(System.String))
            dtSIP.Columns.Add("Subpart", GetType(System.String))

            dtSIP2.Columns.Add("strSubpart", GetType(System.String))
            dtSIP2.Columns.Add("Subpart", GetType(System.String))

            drNewRow = dtSIP.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtSIP.Rows.Add(drNewRow)

            drNewRow = dtSIP2.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtSIP2.Rows.Add(drNewRow)

            For Each drDSRow3 In dsSubpart.Tables("SubpartSIP").Rows()
                drNewRow = dtSIP.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtSIP.Rows.Add(drNewRow)

                drNewRow = dtSIP2.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtSIP2.Rows.Add(drNewRow)
            Next

            With cboSIP1
                .DataSource = dtSIP
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            With cboSIP2
                .DataSource = dtSIP2
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            SQL = "Select " & _
            "strSubpart, " & _
            "(strSubpart||' - '||strDescription) as Subpart " & _
            "from AIRBRANCH.LookUpSubpart61 " & _
            "order by strSubpart "

            daSubpart = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSubpart.Fill(dsSubpart, "SubpartNESHAP")

            dtNESHAP.Columns.Add("strSubpart", GetType(System.String))
            dtNESHAP.Columns.Add("Subpart", GetType(System.String))

            dtNESHAP2.Columns.Add("strSubpart", GetType(System.String))
            dtNESHAP2.Columns.Add("Subpart", GetType(System.String))

            drNewRow = dtNESHAP.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtNESHAP.Rows.Add(drNewRow)

            drNewRow = dtNESHAP2.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtNESHAP2.Rows.Add(drNewRow)

            For Each drDSRow3 In dsSubpart.Tables("SubpartNESHAP").Rows()
                drNewRow = dtNESHAP.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtNESHAP.Rows.Add(drNewRow)

                drNewRow = dtNESHAP2.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtNESHAP2.Rows.Add(drNewRow)
            Next

            With cboNESHAP1
                .DataSource = dtNESHAP
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            With cboNESHAP2
                .DataSource = dtNESHAP2
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            SQL = "Select " & _
            "strSubpart, " & _
            "(strSubpart||' - '||strDescription) as Subpart " & _
            "from AIRBRANCH.LookUpSubpart60 " & _
            "order by strSubpart "

            daSubpart = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSubpart.Fill(dsSubpart, "SubpartNSPS")

            dtNSPS.Columns.Add("strSubpart", GetType(System.String))
            dtNSPS.Columns.Add("Subpart", GetType(System.String))

            dtNSPS2.Columns.Add("strSubpart", GetType(System.String))
            dtNSPS2.Columns.Add("Subpart", GetType(System.String))

            drNewRow = dtNSPS.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtNSPS.Rows.Add(drNewRow)

            drNewRow = dtNSPS2.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtNSPS2.Rows.Add(drNewRow)

            For Each drDSRow3 In dsSubpart.Tables("SubpartNSPS").Rows()
                drNewRow = dtNSPS.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtNSPS.Rows.Add(drNewRow)

                drNewRow = dtNSPS2.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtNSPS2.Rows.Add(drNewRow)
            Next

            With cboNSPS1
                .DataSource = dtNSPS
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            With cboNSPS2
                .DataSource = dtNSPS2
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With


            SQL = "Select " & _
            "strSubpart, " & _
            "(strSubpart||' - '||strDescription) as Subpart " & _
            "from AIRBRANCH.LookUpSubpart63 " & _
            "order by strSubpart "

            daSubpart = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSubpart.Fill(dsSubpart, "SubpartMACT")

            dtMACT.Columns.Add("strSubpart", GetType(System.String))
            dtMACT.Columns.Add("Subpart", GetType(System.String))

            dtMACT2.Columns.Add("strSubpart", GetType(System.String))
            dtMACT2.Columns.Add("Subpart", GetType(System.String))

            drNewRow = dtMACT.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtMACT.Rows.Add(drNewRow)

            drNewRow = dtMACT2.NewRow()
            drNewRow("strSubpart") = ""
            drNewRow("SubPart") = ""
            dtMACT2.Rows.Add(drNewRow)

            For Each drDSRow3 In dsSubpart.Tables("SubpartMACT").Rows()
                drNewRow = dtMACT.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtMACT.Rows.Add(drNewRow)

                drNewRow = dtMACT2.NewRow
                drNewRow("strSubpart") = drDSRow3("strSubpart")
                drNewRow("Subpart") = drDSRow3("Subpart")
                dtMACT2.Rows.Add(drNewRow)
            Next

            With cboMACT1
                .DataSource = dtMACT
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

            With cboMACT2
                .DataSource = dtMACT2
                .DisplayMember = "Subpart"
                .ValueMember = "strSubpart"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadDefaults()
        Try

            cboFieldType1.Text = "Facility Name"
            cboFieldType2.Text = "Application No."
            cboSort1.Text = "Facility Name"
            cboSort2.Text = "Application No."
            cboSortOrder1.Text = "Ascending Order"
            cboSortOrder2.Text = "Descending Order"
            cboApplicationType.Text = "All"
            cboApplicationUnit.Text = "All"
            chbShowAll.Checked = False

            If cboEngineer.SelectedIndex > -1 Then
                cboEngineer.SelectedIndex = 0
            End If

            DTPSearchDate1.Text = OracleDate
            DTPSearchDate1b.Text = OracleDate
            DTPSearchDate2.Text = OracleDate
            DTPSearchDate2b.Text = OracleDate
            txtSearchText1.Clear()
            txtSearchText2.Clear()

            cboApplicationStatus.Text = "Active"
            If AccountFormAccess(3, 3) = "1" And UserUnit = "---" Then
                'All active Applications
                cboApplicationType.Text = "All"
                'cboApplicationType.Text = "Title V"
            Else
                If AccountFormAccess(3, 3) = "1" And UserUnit <> "---" Then
                    'All Active Applications from UC's Unit
                    SQL = "Select numUnit " & _
                    "from AIRBRANCH.EPDUserProfiles " & _
                    "where numUserID = '" & UserGCode & "' " & _
                    "and numProgram = '5' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    Try

                        dr = cmd.ExecuteReader
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try


                    Dim recexist As Boolean = dr.Read
                    If recexist = True Then
                        cboApplicationUnit.SelectedValue = dr.Item("numUnit")
                    End If
                    dr.Close()
                Else
                    cboEngineer.SelectedValue = UserGCode
                    'If AccountArray(3, 2) = "1" Then
                    '    cboEngineer.SelectedValue = UserGCode
                    'Else
                    '    cboEngineer.SelectedValue = UserGCode
                    'End If
                    'If cboEngineer.SelectedIndex < 0 Then
                    '    cboEngineer.SelectedIndex = 0
                    'End If
                End If
            End If
            If AccountFormAccess(3, 4) = "1" Then
                mmiNewApplication.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub SSPPApplicationLog_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Dispose()
    End Sub
#End Region

#Region "Background worker / Search procedures"
    Private Sub RunSearch()
        CancelSearch()

        selectedApp = ""

        dgvApplicationLog.Visible = False
        lblMessage.Text = "Loading…"
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

        dsApplication = New DataSet
        'dgvApplicationLog.DataSource = dsApplication
        ' This line was causing a fatal error when an initial RunSearch returned no results

        Try
            If bgwApplicationLog.IsBusy = False Then
                bgwApplicationLog.RunWorkerAsync()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub CancelSearch()
        If bgwApplicationLog.IsBusy = True Then
            bgwApplicationLog.CancelAsync()
        End If
    End Sub
    Private Sub FetchData(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwApplicationLog.DoWork
        Try
            If chbShowAll.Checked = True Then
                SQL = "Select  " & _
                "  distinct(to_Number(AIRBRANCH.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,  " & _
                "   case  " & _
                "   	when strApplicationTypeDesc IS Null then ''  " & _
                "   Else strApplicationTypeDesc  " & _
                "   End as strApplicationType,  " & _
                "   case  " & _
                "   	when datReceivedDate is Null then ''  " & _
                "   Else to_char(datReceivedDate, 'RRRR-MM-dd')  " & _
                "   End as datReceivedDate,  " & _
                "   case   " & _
                "when strPermitNumber is NULL then ''   " & _
                " else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'   " & _
                "||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                "||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)  " & _
                "   end As strPermitNumber,  " & _
                "   case  " & _
                "   	when datPermitIssued is Null then ''  " & _
                "   else to_char(datPermitIssued, 'RRRR-MM-dd')  " & _
                "   end as datPermitIssued,  " & _
                "   case  " & _
                "   	when numUserID = '0' then ''  " & _
                "   	when numUserID is Null then ''  " & _
                "   else (strLastName|| ', ' ||strFirstName)  " & _
                "   end as StaffResponsible,  " & _
                "   case  " & _
                "   	when AIRBRANCH.SSPPApplicationData.strFacilityName is Null then ''  " & _
                "   else AIRBRANCH.SSPPApplicationData.strFacilityName  " & _
                "   end as strFacilityName,  " & _
                "   case  " & _
                "   	when AIRBRANCH.SSPPApplicationMaster.strAIRSNumber is Null then ''  " & _
                "   	when AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = '0413' then ''  " & _
                "   else substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5)  " & _
                "   end as strAIRSNumber,  " & _
                "     case  " & _
                "   when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'  " & _
                "   when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '09 - Administrative Review'  " & _
                "   when datToBranchCheif is Not Null and datFinalizedDate is Null  " & _
                "   and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - Administrative Review'  " & _
                "   when datEPAEnds is not Null then '08 - EPA 45-day Review'  " & _
                "   when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'  " & _
                "   when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'   " & _
                "   when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'   " & _
                "   when dattoPMII is Not Null then '04 - AT PM'   " & _
                "   when dattoPMI is Not Null then '03 - At UC'   " & _
                "   when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'  " & _
                "   when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'    " & _
                "   else '01 - At Engineer'   " & _
                "   end as AppStatus,  " & _
                "   AIRBRANCH.SSPPApplicationData.strSICCode,  " & _
                "   AIRBRANCH.SSPPApplicationData.strPlantDescription,  " & _
                "   Case  " & _
                "   	when APBUnit is Null then ''  " & _
                "   Else strUnitDesc    " & _
                "   End as APBUnit,  " & _
                "   case  " & _
                "   	when datApplicationStarted is Null then ''  " & _
                "   Else to_char(datApplicationStarted, 'RRRR-MM-dd')  " & _
                "   End as datApplicationStarted,  " & _
                "   case  " & _
                "   	when datSentByFacility is Null then ''  " & _
                "   else to_char(datSentByFacility, 'RRRR-MM-dd')  " & _
                "   End as datSentByFacility,  " & _
                "   case  " & _
                "   	when datAssignedToEngineer is Null then ''  " & _
                "   else to_char(datAssignedtoEngineer, 'RRRR-MM-dd')  " & _
                "   ENd as datAssignedtoEngineer,  " & _
                "   case  " & _
                "   	when datReassignedToEngineer is Null then ''  " & _
                "   else to_char(datReassignedToEngineer, 'RRRR-MM-dd')  " & _
                "   End as datReassignedToEngineer,  " & _
                "   case  " & _
                "   	when datApplicationPackageComplete is Null then ''  " & _
                "   else to_char(datApplicationPackageComplete, 'RRRR-MM-dd')  " & _
                "   End as datApplicationPackageComplete,  " & _
                "  Case  " & _
                "   	when datAcknowledgementLetterSent is NUll then ''  " & _
                "   else to_char(datAcknowledgementLetterSent, 'RRRR-MM-dd')  " & _
                "   End as datAcknowledgementLetterSent,  " & _
                "   case  " & _
                "        when strPublicInvolvement = '0' Then 'Not Decided'  " & _
                "        when strPublicInvolvement = '1' Then 'PA Needed'  " & _
                "        when strPublicInvolvement = '2' Then 'PA Not Needed'  " & _
                "   Else 'Not Decided'  " & _
                "   end strPublicInvolvement,  " & _
                "   case  " & _
                "   	when datPNExpires is Null then ''  " & _
                "   else to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                "   End as datPNExpires,  " & _
                "   case  " & _
                "   	when datPAExpires is Null then ''  " & _
                "   else to_char(datPAExpires, 'RRRR-MM-dd')  " & _
                "   End as datPAExpires,  " & _
                "   case  " & _
                "   	when datToPMI is Null then ''  " & _
                "   else to_char(datToPMI, 'RRRR-MM-dd')  " & _
                "   End as datToPMI,  " & _
                "   case  " & _
                "   	when datToPMII is Null then ''  " & _
                "   else to_char(datToPMII, 'RRRR-MM-dd')  " & _
                "   end as datToPMII,  " & _
                "   case  " & _
                "   	when datDraftIssued is NUll then ''  " & _
                "   else to_char(datDraftIssued, 'RRRR-MM-dd')  " & _
                "   end as datDraftIssued,  " & _
                "   case  " & _
                "   	when AIRBRANCH.SSPPApplicationData.strComments is Null then ''  " & _
                "   else AIRBRANCH.SSPPApplicationData.strComments  " & _
                "   End as strComments,   " & _
                "   case  " & _
                "   	when datWithdrawn is Null  then ''  " & _
                "   else to_char(datWithdrawn, 'RRRR-MM-dd')  " & _
                "   end as datWithdrawn,  " & _
                "   Case  " & _
                "   	when datApplicationDeadLine is Null then ''  " & _
                "   else to_char(datApplicationDeadLine, 'RRRR-MM-dd')  " & _
                "   End as datApplicationDeadLine,  " & _
                "   case  " & _
                "   	when datFinalizedDate is Null then ''  " & _
                "   else to_char(datFinalizedDate, 'RRRR-MM-dd')  " & _
                "   end as datFinalizedDate,  " & _
                "   case  " & _
                "   	when strPermitTypeDescription is Null then ''  " & _
                "   else strPermitTypeDescription  " & _
                "   End as strPermitType,  " & _
                "   case  " & _
                "   	when strApplicationNotes is Null then ''  " & _
                "   else strApplicationNotes  " & _
                "   end as strApplicationNotes,  " & _
                "   case  " & _
                "   	when strLastName is Null then ''  " & _
                "   	when strLastName = 'System' then ' '  " & _
                "   else strLastName  " & _
                "   end as strLastname, " & _
                "   case  " & _
                "   	when strFirstName is Null then ''  " & _
                "   else strFirstName  " & _
                "   end as strFirstName, " & _
                "   '' as strAFSGCode,  " & _
                "   case  " & _
                "   	when numUserID is null then 0  " & _
                "   	when numUserID = '0' then 0  " & _
                "   else numUserID  " & _
                "   end as numUserID, " & _
                "   Case  " & _
                "   	when strUnitDesc is Null then ''  " & _
                "   else strUnitDesc  " & _
                "   end as strUserUnit,  " & _
                "   case  " & _
                "   	when AIRBRANCH.SSPPApplicationData.strFacilityStreet1 is NUll then ''  " & _
                "   else AIRBRANCH.SSPPApplicationData.strFacilityStreet1  " & _
                "   end as strFacilityStreet1,  " & _
                "   case  " & _
                "   	when AIRBRANCH.SSPPApplicationData.strFacilityCity is Null then ''  " & _
                "   else AIRBRANCH.SSPPApplicationData.strFacilityCity  " & _
                "   end as strFacilityCity,  " & _
                "   case  " & _
                "   	when strCountyName is Null then ''  " & _
                "   else strCountyName  " & _
                "   end as strCountyName,  " & _
                "   case  " & _
                "   	when strDistrictName is Null then ''  " & _
                "   else strDistrictName  " & _
                "   end as strDistrictName,  " & _
                "   case  " & _
                "     when strOfficeName is Null then ''  " & _
                "   else strOfficeName  " & _
                "   End as strOfficeName,  " & _
                "   case  " & _
                "   	     when AIRBRANCH.APBHeaderData.strAttainmentStatus is Null then ''  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 2, 1) = '0' then 'No'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 2, 1) = '1' then '1-hr Ozone'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 2, 1) = '2' then '1-hr Ozone Contribute'  " & _
                "   end as OneHrOzone,  " & _
                "   case  " & _
                "   	     when AIRBRANCH.APBHeaderData.strAttainmentStatus is Null then ''  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 3, 1) = '0' then 'No'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 3, 1) = '1' then '8-hr Ozone Atlanta'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 3, 1) = '2' then '8-hr Ozone Macon'  " & _
                "   end as EightHrOzone,  " & _
                "   case  " & _
                "   	     when AIRBRANCH.APBHeaderData.strAttainmentStatus is Null then ''  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 4, 1) = '0' then 'No'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 4, 1) = '1' then 'PM - Atlanta'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 4, 1) = '2' then 'PM - Chattanooga'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 4, 1) = '3' then 'PM - Floyd'  " & _
                "when substr(AIRBRANCH.APBHeaderData.strAttainmentstatus, 4, 1) = '4' then 'PM - Macon'  " & _
                "   end as PMFine,  " & _
                "   case  " & _
                "      when strPAReady is Null then ''   " & _
                "      when strPAReady = 'True' then 'PA Ready'   " & _
                "      when strPAReady = 'False' then ''  " & _
                "   end as strPAReady,   " & _
                "   case   " & _
                "      when strPNready is Null then ''   " & _
                "      when strPNready = 'True' then 'PN Ready'  " & _
                "      when strPNReady = 'False' then ''   " & _
                "   end as strPNReady,   " & _
                "   case  " & _
                "   when datEPAWaived is Null then ''  " & _
                "   else to_char(datEPAWaived, 'RRRR-MM-dd')  " & _
                "   end as datEPAWaived,  " & _
                "   Case  " & _
                "   when datEPAEnds is Null then ''  " & _
                "   else to_char(datEPAEnds, 'RRRR-MM-dd')  " & _
                "   end as datEPAEnds,  " & _
                "   case  " & _
                "   when datToBranchCheif is Null then ''  " & _
                "   else to_char(datToBranchCheif, 'RRRR-MM-dd')  " & _
                "   end as datToBranchCheif,  " & _
                "   case  " & _
                "   when datToDirector is Null then ''  " & _
                "   else to_char(datToDirector, 'RRRR-MM-dd')  " & _
                "   end as datToDirector,  " & _
                "   case  " & _
                "        when AIRBRANCH.APBHeaderData.strStateProgramCodes is Null then ''  " & _
                "        when substr(AIRBRANCH.APBHeaderData.strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major'  " & _
                "   End as NSRMajor,  " & _
                "   Case  " & _
                "        when AIRBRANCH.APBHeaderData.strStateProgramCodes is Null then ''  " & _
                "        when substr(AIRBRANCH.APBHeaderData.strStateProgramCodes, 2, 1) = '1' then 'HAPs Major'  " & _
                "   End as HAPsMajor,  " & _
                "   case   " & _
                "   when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')     " & _
                "   when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')  " & _
                "   when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')  " & _
                "   when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')   " & _
                "   when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')    " & _
                "   when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')    " & _
                "   when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                "   when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')     " & _
                "   when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')     " & _
                "   when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')     " & _
                "   when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')    " & _
                "   when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'     " & _
                "   else to_char(datAssignedToEngineer, 'RRRR-MM-dd')     " & _
                "   end as StatusDate,   " & _
                "   case  " & _
                "   when substr(strTrackedRules, 1, 1) = '1' then 'PSD - Rule'  " & _
                "   else ' '  " & _
                "   end PSDRule,  " & _
                "   case   " & _
                "   when substr(strTrackedRules, 2, 1) = '1' then 'NAA - Rule'   " & _
                "   else ' '   " & _
                "   end NAARule,   " & _
                "   case   " & _
                "   when substr(strTrackedRules, 3, 1) = '1' then '112(g) - Rule'   " & _
                "   else ' '   " & _
                "   end gRule,   " & _
                "   case   " & _
                "   when substr(strTrackedRules, 4, 1) = '1' then 'Rule (tt) RACT'   " & _
                "   else ' '  " & _
                "   end ttRACT,  " & _
                "   case   " & _
                "   when substr(strTrackedRules, 5, 1) = '1' then 'Rule (yy) RACT'   " & _
                "   else ' '   " & _
                "   end yyRACT,   " & _
                "   case   " & _
                "   when substr(strTrackedRules, 6, 1) = '1' then 'Actual PAL Rule'   " & _
                "   else ' '   " & _
                "   end PALRule, " & _
                "   case   " & _
                "   when substr(strTrackedRules, 7, 1) = '1' then 'Expedited Permit'   " & _
                "   else ' '   " & _
                "   end ExpeditedPermitRule,   " & _
                " (substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) ||' - '||AIRBRANCH.SSPPSubpartData.strSubpart) as strSubpart " & _
                "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationTracking,  " & _
                "  AIRBRANCH.SSPPApplicationData, AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpPermitTypes, " & _
                "  AIRBRANCH.LookUpCountyInformation, AIRBRANCH.LookUPDistrictInformation, " & _
                "  AIRBRANCH.LookUpDistricts, AIRBRANCH.LookUpDistrictOffice, AIRBRANCH.APBHeaderData, " & _
                "  AIRBRANCH.EPDUSerProfiles, AIRBRANCH.LookUpEPDUnits, " & _
                "AIRBRANCH.SSPPSubpartData " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpAPplicationTypes.strApplicationTypeCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strPermitType = AIRBRANCH.LookUpPermitTypes.strPermitTypeCode (+)     " & _
                "and AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber (+)     " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode (+) " & _
                "and AIRBRANCH.LookUpCountyInformation.strCountyCode = AIRBRANCH.LookUpDistrictInformation.strDistrictCounty (+)  " & _
                "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUPDistricts.strDistrictCode (+)  " & _
                "and AIRBRANCH.LookUPDistricts.strDistrictCode = AIRBRANCH.LooKUPDistrictOffice.strDistrictCode (+)  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and AIRBRANCH.SSPPApplicationMaster.APBUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPSubpartData.strApplicationNumber (+) "
            Else
                SQL = "Select  " & _
                    "  distinct(to_Number(AIRBRANCH.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,  " & _
                    "  case   " & _
                    " 	when strApplicationTypeDesc IS Null then ''   " & _
                    " Else strApplicationTypeDesc   " & _
                    " End as strApplicationType,   " & _
                    " case   " & _
                    " 	when datReceivedDate is Null then ''   " & _
                    " Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
                    " End as datReceivedDate,   " & _
                    " case    " & _
                    "         when strPermitNumber is NULL then ''    " & _
                    "          else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
                    "         ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
                    "         ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
                    " end As strPermitNumber,   " & _
                    " case   " & _
                    " 	when datPermitIssued is Null then ''   " & _
                    " else to_char(datPermitIssued, 'RRRR-MM-dd')   " & _
                    " end as datPermitIssued,   " & _
                    " case   " & _
                    " 	when numUserID = '0' then ''   " & _
                    " 	when numUserID is Null then ''   " & _
                    " else (strLastName|| ', ' ||strFirstName)   " & _
                    " end as StaffResponsible,   " & _
                    " case   " & _
                    " 	when AIRBRANCH.SSPPApplicationData.strFacilityName is Null then ''   " & _
                    " else AIRBRANCH.SSPPApplicationData.strFacilityName   " & _
                    " end as strFacilityName,  " & _
                    " case   " & _
                    " 	when AIRBRANCH.SSPPApplicationMaster.strAIRSNumber is Null then ''   " & _
                    " 	when AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = '0413' then ''   " & _
                    " else substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5)   " & _
                    " end as strAIRSNumber,   " & _
                    "case   " & _
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '09 - Administrative Review'   " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - Administrative Review'   " & _
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
                    " case   " & _
                    "	when strPermitTypeDescription is Null then ''   " & _
                    "else strPermitTypeDescription   " & _
                    "End as strPermitType,  " & _
                    "case    " & _
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
                    "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null   " & _
                    "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
                    "end as StatusDate,   " & _
                    "AIRBRANCH.SSPPApplicationData.strSICCode,   " & _
                    "AIRBRANCH.SSPPApplicationData.strPlantDescription   " & _
                    "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationTracking,   " & _
                    "  AIRBRANCH.SSPPApplicationData, AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpPermitTypes,  " & _
                    "  AIRBRANCH.LookUpCountyInformation, AIRBRANCH.LookUPDistrictInformation,  " & _
                    "  AIRBRANCH.LookUpDistricts, AIRBRANCH.LookUpDistrictOffice, AIRBRANCH.APBHeaderData,  " & _
                    "  AIRBRANCH.EPDUSerProfiles, AIRBRANCH.LookUpEPDUnits,  " & _
                    " AIRBRANCH.SSPPSubpartData " & _
                    "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpAPplicationTypes.strApplicationTypeCode (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strPermitType = AIRBRANCH.LookUpPermitTypes.strPermitTypeCode (+)      " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber (+)      " & _
                    "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode (+)  " & _
                    "and AIRBRANCH.LookUpCountyInformation.strCountyCode = AIRBRANCH.LookUpDistrictInformation.strDistrictCounty (+)   " & _
                    "and AIRBRANCH.LookUpDistrictInformation.strDistrictCode = AIRBRANCH.LookUPDistricts.strDistrictCode (+)   " & _
                    "and AIRBRANCH.LookUPDistricts.strDistrictCode = AIRBRANCH.LooKUPDistrictOffice.strDistrictCode (+)   " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and AIRBRANCH.SSPPApplicationMaster.APBUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+) " & _
                    "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPSubpartData.strApplicationNumber (+) "
            End If

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            Select Case FieldType1
                Case "AIRS No."
                    SQLSearch1 = " AIRBRANCH.SSPPApplicationMaster.strAIRSNumber like '%" & Replace(SearchText1, "'", "''") & "%' "
                Case "Applicable Rules"
                    Select Case SearchText1b
                        Case "Any Rule"
                            SQLSearch1 = " AIRBRANCH.SSPPApplicationData.strTrackedRules <> '0000000000' "
                        Case "PSD"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 1, 1) = '1' "
                        Case "NAA NSR"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 2, 1) = '1' "
                        Case "112(g)"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 3, 1) = '1' "
                        Case "Rule (tt) RACT"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 4, 1) = '1' "
                        Case "Rule (yy) RACT"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 5, 1) = '1' "
                        Case "Actuals PAL"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 6, 1) = '1' "
                        Case "Expedited Permit"
                            SQLSearch1 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 7, 1) = '1' "
                        Case Else
                            SQLSearch1 = " "
                    End Select
                Case "Application No."
                    SQLSearch1 = " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber like '%" & Replace(SearchText1, "'", "''") & "%' "
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
                            SQLSearch1 = " datPNExpires is Not Null and datPNExpires >= sysdate and datFinalizedDate is Null and datEPAEnds is Null "
                        Case "7 - Public Notice Expired"
                            SQLSearch1 = " datPNExpires is Not Null and datPNExpires < sysdate and datToDirector is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "8 - EPA 45-day Review"
                            SQLSearch1 = " datEPAEnds is not Null and datFinalizedDate is Null and datDraftIssued is Not Null "
                        Case "9 - Administrative Review"
                            'SQLSearch1 = " (datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) "
                            SQLSearch1 = " (datToBranchCheif is Not Null and datFinalizedDate is Null   and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) "
                            'Case "9 - Administrative Review"
                            '    SQLSearch1 = " (datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector)) "
                        Case "11 - Closed Out"
                            SQLSearch1 = " datFinalizedDate is Not Null "
                        Case Else
                            SQLSearch1 = " "
                    End Select
                Case "Application Type"
                    If SearchText1b = "Other" Then
                        SQLSearch1 = " (Upper(strApplicationTypeDesc) <>  Upper('Acid Rain') and Upper(strApplicationTypeDesc) <>  Upper('AA') " & _
                        " and Upper(strApplicationTypeDesc) <>  Upper('NC') and  Upper(strApplicationTypeDesc) <>  Upper('SAW') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('SAWO') and Upper(strApplicationTypeDesc) <>  Upper('MAW') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('MAWO') and Upper(strApplicationTypeDesc) <>  Upper('TV-Renewal') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('502(b)10') and Upper(strApplicationTypeDesc) <>  Upper('TV-Initial') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('SM') and Upper(strApplicationTypeDesc) <>  Upper('Closed') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('ERC') and Upper(strApplicationTypeDesc) <>  Upper('OFF PERMIT') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('PBR') and Upper(strApplicationTypeDesc) <>  Upper('SIP')) "
                    Else
                        SQLSearch1 = " Upper(strApplicationTypeDesc) like Upper('%" & SearchText1b & "%') "
                    End If
                Case "Application Unit"
                    SQLSearch1 = " Upper(AIRBRANCH.LookUpEPDUnits.strUnitDesc) like Upper('%" & Replace(SearchText1b, "'", "''") & "%') "
                Case "Applog Comments"
                    SQLSearch1 = " Upper(AIRBRANCH.SSPPApplicationData.strComments) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Date Acknowledged"
                    SQLSearch1 = " datAcknowledgementLetterSent between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date APL Completed"
                    SQLSearch1 = " datApplicationPackageComplete between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date APL Dated"
                    SQLSearch1 = " datSentByFacility between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date APL Received"
                    SQLSearch1 = " datReceivedDate between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Assigned"
                    SQLSearch1 = " datAssignedtoEngineer between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Draft Issued"
                    SQLSearch1 = " datDraftIssued between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date PA Expires"
                    SQLSearch1 = " datPAExpires between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Finalized"
                    SQLSearch1 = " datPermitIssued between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date PN Expires"
                    SQLSearch1 = " datPNExpires between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Reassigned"
                    SQLSearch1 = " datReassignedToEngineer between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Started Review"
                    SQLSearch1 = " datApplicationStarted between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date to BC"
                    SQLSearch1 = " datToBranchCheif between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date to DO"
                    SQLSearch1 = " datToDirector between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date to PM"
                    SQLSearch1 = " datToPMII between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date to UC"
                    SQLSearch1 = " datToPMI between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Date Withdrawn"
                    SQLSearch1 = " datWithdrawn between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Deadline"
                    SQLSearch1 = " datApplicationDeadLine between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Engineer Firstname"
                    SQLSearch1 = " Upper(strFirstName) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Engineer Lastname"
                    SQLSearch1 = " Upper(strLastName) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Engineer Unit Code"
                    SQLSearch1 = " Upper(APBUnit) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "EPA 45-day Waived"
                    SQLSearch1 = " datEPAWaived between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "EPA 45-day Ends"
                    SQLSearch1 = " datEPAEnds between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "Facilty City"
                    SQLSearch1 = " Upper(strFacilityCity) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Facility County"
                    SQLSearch1 = " Upper(strCountyName) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Facility Name"
                    SQLSearch1 = " Upper(strFacilityName) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Facility Street"
                    SQLSearch1 = " Upper(strFacilityStreet1) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "HAPs Major"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strStateProgramCodes like '_1___' "
                Case "NAA 1Hr-Yes"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_1___' "
                Case "NAA 1Hr-Contr."
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_2___' "
                Case "NAA 1Hr-No"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_0___' "
                Case "NAA 8Hr-Atlanta"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__1__' "
                Case "NAA 8Hr-Macon"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__2__' "
                Case "NAA 8Hr-No"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__0__' "
                Case "NAA PM-Atlanta"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___1_' "
                Case "NAA PM-Chattanooga"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___2_' "
                Case "NAA PM-Floyd"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___3_' "
                Case "NAA PM-Macon"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___4_' "
                Case "NAA PM-No"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___0_' "
                Case "NSR/PSD Major"
                    SQLSearch1 = " AIRBRANCH.APBHeaderData.strStateProgramCodes like '1____' "
                Case "PA Ready"
                    SQLSearch1 = " strPAReady is Not Null and strPAReady = 'True' "
                Case "Permit Number"
                    temp = Replace(SearchText1, "-", "")
                    SQLSearch1 = " Upper(strPermitNumber) like Upper('%" & Replace(temp, "'", "''") & "%') "
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
                    SQLSearch1 = " Upper(AIRBRANCH.SSPPApplicationData.strPlantDescription) like '%" & Replace(SearchText1.ToUpper, "'", "''") & "%' "
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
                    SQLSearch1 = " Upper(strApplicationNotes) like Upper('%" & Replace(SearchText1, "'", "''") & "%') "
                Case "Regional District"
                    SQLSearch1 = " Upper(strDistrictName) like Upper('%" & Replace(SearchText1b, "'", "''") & "%') "
                Case "Regional Office"
                    SQLSearch1 = " Upper(strOfficeName) like Upper('%" & Replace(SearchText1b, "'", "''") & "%') "
                Case "Status Date"
                    SQLSearch1 = " StatusDate between '" & SearchDate1 & "' and '" & SearchDate1b & "' "
                Case "SIC Code"
                    SQLSearch1 = " AIRBRANCH.SSPPApplicationData.strSICCode like '%" & Replace(SearchText1, "'", "''") & "%' "
                Case "Subpart - 0-SIP"
                    SQLSearch1 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartSIP1 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '0' ) "
                Case "Subpart - 8-NESHAP (Part 61)"
                    SQLSearch1 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartNESHAP1 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '8' ) "
                Case "Subpart - 9-NSPS (Part 60)"
                    SQLSearch1 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartNSPS1 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '9' ) "
                Case "Subpart - M-MACT (Part 63)"
                    SQLSearch1 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartMACT1 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = 'M' ) "
            End Select

            Select Case FieldType2
                Case "AIRS No."
                    SQLSearch2 = " AIRBRANCH.SSPPApplicationMaster.strAIRSNumber like '%" & Replace(SearchText2, "'", "''") & "%' "
                Case "Applicable Rules"
                    Select Case SearchText2b
                        Case "Any Rule"
                            SQLSearch2 = " AIRBRANCH.SSPPApplicationData.strTrackedRules <> '0000000000' "
                        Case "PSD"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 1, 1) = '1' "
                        Case "NAA NSR"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 2, 1) = '1' "
                        Case "112(g)"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 3, 1) = '1' "
                        Case "Rule (tt) RACT"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 4, 1) = '1' "
                        Case "Rule (yy) RACT"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 5, 1) = '1' "
                        Case "Actuals PAL"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 6, 1) = '1' "
                        Case "Expedited Permit"
                            SQLSearch2 = " substr(AIRBRANCH.SSPPApplicationData.strTrackedRules, 7, 1) = '1' "
                        Case Else
                            SQLSearch2 = " "
                    End Select
                Case "Application No."
                    SQLSearch2 = " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber like '%" & Replace(SearchText2, "'", "''") & "%' "
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
                            SQLSearch2 = " datPNExpires is Not Null and datPNExpires >= sysdate and datFinalizedDate is Null and datEPAEnds is Null "
                        Case "7 - Public Notice Expired"
                            SQLSearch2 = " datPNExpires is Not Null and datPNExpires < sysdate and datToDirector is Null and datFinalizedDate is Null and datToBranchCheif is Null and datToDirector is Null "
                        Case "8 - EPA 45-day Review"
                            SQLSearch2 = " datEPAEnds is not Null and datFinalizedDate is Null and datDraftIssued is Not Null "
                        Case "9 - Administrative Review"
                            SQLSearch2 = " (datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) "
                            'Case "9 - Administrative Review"
                            '    SQLSearch2 = " (datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector)) "
                        Case "11 - Closed Out"
                            SQLSearch2 = " datFinalizedDate is Not Null "
                        Case Else
                            SQLSearch2 = " "
                    End Select
                Case "Application Type"
                    If SearchText2b = "Other" Then
                        SQLSearch2 = " (Upper(strApplicationTypeDesc) <>  Upper('Acid Rain') and Upper(strApplicationTypeDesc) <>  Upper('AA') " & _
                        " and Upper(strApplicationTypeDesc) <>  Upper('NC') and  Upper(strApplicationTypeDesc) <>  Upper('SAW') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('SAWO') and Upper(strApplicationTypeDesc) <>  Upper('MAW') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('MAWO') and Upper(strApplicationTypeDesc) <>  Upper('TV-Renewal') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('502(b)10') and Upper(strApplicationTypeDesc) <>  Upper('TV-Initial') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('SM') and Upper(strApplicationTypeDesc) <>  Upper('Closed') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('ERC') and Upper(strApplicationTypeDesc) <>  Upper('OFF PERMIT') " & _
                        "and Upper(strApplicationTypeDesc) <>  Upper('PBR') and Upper(strApplicationTypeDesc) <>  Upper('SIP')) "
                    Else
                        SQLSearch2 = " Upper(strApplicationTypeDesc) like Upper('%" & SearchText2b & "%') "
                    End If
                Case "Application Unit"
                    SQLSearch2 = " Upper(AIRBRANCH.LookUpEPDUnits.strUnitDesc) like Upper('%" & Replace(SearchText2b, "'", "''") & "%') "
                Case "Applog Comments"
                    SQLSearch2 = " Upper(AIRBRANCH.SSPPApplicationData.strComments) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Date Acknowledged"
                    SQLSearch2 = " datAcknowledgementLetterSent between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date APL Completed"
                    SQLSearch2 = " datApplicationPackageComplete between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date APL Dated"
                    SQLSearch2 = " datSentByFacility between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date APL Received"
                    SQLSearch2 = " datReceivedDate between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Assigned"
                    SQLSearch2 = " datAssignedtoEngineer between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Draft Issued"
                    SQLSearch2 = " datDraftIssued between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date PA Expires"
                    SQLSearch2 = " datPAExpires between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Finalized"
                    SQLSearch2 = " datPermitIssued between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date PN Expires"
                    SQLSearch2 = " datPNExpires between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Reassigned"
                    SQLSearch2 = " datReassignedToEngineer between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Started Review"
                    SQLSearch2 = " datApplicationStarted between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date to BC"
                    SQLSearch2 = " datToBranchCheif between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date to DO"
                    SQLSearch2 = " datToDirector between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date to PM"
                    SQLSearch2 = " datToPMII between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date to UC"
                    SQLSearch2 = " datToPMI between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Date Withdrawn"
                    SQLSearch2 = " datWithdrawn between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Deadline"
                    SQLSearch2 = " datApplicationDeadLine between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Engineer Firstname"
                    SQLSearch2 = " Upper(strFirstName) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Engineer Lastname"
                    SQLSearch2 = " Upper(strLastName) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Engineer Unit Code"
                    SQLSearch2 = " Upper(APBUnit) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "EPA 45-day Waived"
                    SQLSearch2 = " datEPAWaived between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "EPA 45-day Ends"
                    SQLSearch2 = " datEPAEnds between '" & SearchDate2 & "' and '" & SearchDate2b & "'  "
                Case "Facilty City"
                    SQLSearch2 = " Upper(strFacilityCity) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Facility County"
                    SQLSearch2 = " Upper(strCountyName) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Facility Name"
                    SQLSearch2 = " Upper(strFacilityName) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Facility Street"
                    SQLSearch2 = " Upper(strFacilityStreet1) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "HAPs Major"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strStateProgramCodes like '1____' "
                Case "NAA 1Hr-Yes"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_1___' "
                Case "NAA 1Hr-Contr."
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_2___' "
                Case "NAA 1Hr-No"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '_0___' "
                Case "NAA 8Hr-Atlanta"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__1__' "
                Case "NAA 8Hr-Macon"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__2__' "
                Case "NAA 8Hr-No"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '__0__' "
                Case "NAA PM-Atlanta"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___1_' "
                Case "NAA PM-Chattanooga"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___2_' "
                Case "NAA PM-Floyd"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___3_' "
                Case "NAA PM-Macon"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___4_' "
                Case "NAA PM-No"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strAttainmentStatus like '___0_' "
                Case "NSR/PSD Major"
                    SQLSearch2 = " AIRBRANCH.APBHeaderData.strStateProgramCodes like '1____' "
                Case "PA Ready"
                    SQLSearch2 = " strPAReady is Not Null and strPAReady = 'True' "
                Case "Permit Number"
                    temp = Replace(SearchText2, "-", "")
                    SQLSearch2 = " Upper(strPermitNumber) like Upper('%" & Replace(temp, "'", "''") & "%') "
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
                    SQLSearch2 = " Upper(AIRBRANCH.SSPPApplicationData.strPlantDescription) like '%" & Replace(SearchText2.ToUpper, "'", "''") & "%' "
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
                    SQLSearch2 = " Upper(strApplicationNotes) like Upper('%" & Replace(SearchText2, "'", "''") & "%') "
                Case "Regional District"
                    SQLSearch2 = " Upper(strDistrictName) like Upper('%" & Replace(SearchText2b, "'", "''") & "%') "
                Case "Regional Office"
                    SQLSearch2 = " Upper(strOfficeName) like Upper('%" & Replace(SearchText2b, "'", "''") & "%') "
                Case "SIC Code"
                    SQLSearch2 = " AIRBRANCH.SSPPApplicationData.strSICCode like '%" & Replace(SearchText2, "'", "''") & "%' "
                Case "Status Date"
                    SQLSearch2 = " StatusDate between '" & SearchDate2 & "' and '" & SearchDate2b & "' "
                Case "Subpart - 0-SIP"
                    SQLSearch2 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartSIP2 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '0' ) "
                Case "Subpart - 8-NESHAP (Part 61)"
                    SQLSearch2 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartNESHAP2 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '8' ) "
                Case "Subpart - 9-NSPS (Part 60)"
                    SQLSearch2 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartNSPS2 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = '9' ) "
                Case "Subpart - M-MACT (Part 63)"
                    SQLSearch2 = " ( AIRBRANCH.SSPPSubpartData.strSubpart = '" & SubpartMACT2 & "' " & _
                    "and substr(AIRBRANCH.SSPPSubpartData.strSubpartKey, -1, 1) = 'M' ) "
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
                SQLLine = SQLLine & "and (Upper(AIRBRANCH.LookUpEPDUnits.strUnitDesc) = Upper('" & Replace(AppUnitText, "'", "''") & "') " & _
                "or (Upper(APBUnit) = Upper('" & Replace(AppUnit, "'", "''") & "'))) "
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
                SQLLine = SQLLine & "and Upper(numUserID) like Upper('" & Replace(Engineer, "'", "''") & "') "
            End If

            SQLOrder = " order by "

            If SortOrder1 = "Descending Order" Then
                temp = "Desc"
            Else
                temp = " "
            End If

            Select Case Sort1
                Case "AIRS No."
                    SQLOrder = SQLOrder & " strAIRSNumber " & temp
                Case "Application No."
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber " & temp
                Case "Application Status"
                    SQLOrder = SQLOrder & " AppStatus " & temp
                Case "Application Type"
                    SQLOrder = SQLOrder & " strApplicationType " & temp
                Case "Application Unit"
                    SQLOrder = SQLOrder & " strUnitDesc " & temp
                Case "Applog Comments"
                    SQLOrder = SQLOrder & " strComments " & temp
                Case "Date Acknowledged"
                    SQLOrder = SQLOrder & " datAcknowledgementLetterSent " & temp
                Case "Date APL Completed"
                    SQLOrder = SQLOrder & " datApplicationPackageComplete " & temp
                Case "Date APL Dated"
                    SQLOrder = SQLOrder & " datSentByFacility " & temp
                Case "Date APL Received"
                    SQLOrder = SQLOrder & " datReceivedDate " & temp
                Case "Date Assigned"
                    SQLOrder = SQLOrder & " datAssignedtoEngineer " & temp
                Case "Date Draft Issued"
                    SQLOrder = SQLOrder & " datDraftIssued " & temp
                Case "Date PA Expires"
                    SQLOrder = SQLOrder & " datPAExpires " & temp
                Case "Date Finalized"
                    SQLOrder = SQLOrder & " datPermitIssued " & temp
                Case "Date PN Expires"
                    SQLOrder = SQLOrder & " datPNExpires " & temp
                Case "Date Reassigned"
                    SQLOrder = SQLOrder & " datReassignedToEngineer " & temp
                Case "Date Started Review"
                    SQLOrder = SQLOrder & " datApplicationStarted " & temp
                Case "Date to BC"
                    SQLOrder = SQLOrder & " datToBranchCheif " & temp
                Case "Date to DO"
                    SQLOrder = SQLOrder & " datToDirector " & temp
                Case "Date to PM"
                    SQLOrder = SQLOrder & " datToPMII " & temp
                Case "Date to UC"
                    SQLOrder = SQLOrder & " datToPMI " & temp
                Case "Date Withdrawn"
                    SQLOrder = SQLOrder & " datWithdrawn " & temp
                Case "Deadline"
                    SQLOrder = SQLOrder & " datApplicationDeadLine " & temp
                Case "Engineer Firstname"
                    SQLOrder = SQLOrder & " strFirstName " & temp
                Case "Engineer Lastname"
                    SQLOrder = SQLOrder & " strLastName " & temp
                Case "Engineer Unit Code"
                    SQLOrder = SQLOrder & " APBUnit " & temp
                Case "EPA 45-day Waived"
                    SQLOrder = SQLOrder & " datEPAWaived " & temp
                Case "EPA 45-day Ends"
                    SQLOrder = SQLOrder & " datEPAEnds " & temp
                Case "Facilty City"
                    SQLOrder = SQLOrder & " strFacilityCity " & temp
                Case "Facility County"
                    SQLOrder = SQLOrder & " strCountyName " & temp
                Case "Facility Name"
                    SQLOrder = SQLOrder & " strFacilityName " & temp
                Case "Facility Street"
                    SQLOrder = SQLOrder & " strFacilityStreet1 " & temp
                Case "NAA 1Hr-Yes"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 1Hr-Contr."
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 1Hr-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-Atlanta"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-Macon"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Atlanta"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Chattanooga"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Floyd"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Macon"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "PA Ready"
                    SQLOrder = SQLOrder & " strPAReady " & temp
                Case "Permit Number"
                    SQLOrder = SQLOrder & " strPermitNumber " & temp
                Case "Permit Type"
                    SQLOrder = SQLOrder & " strPermitType " & temp
                Case "Plant Description"
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationData.strPlantDescription " & temp
                Case "PN Ready"
                    SQLOrder = SQLOrder & " strPNReady " & temp
                Case "Public Advisory"
                    SQLOrder = SQLOrder & " strPublicInvolvement " & temp
                Case "Reason APL Submitted"
                    SQLOrder = SQLOrder & " strApplicationNotes " & temp
                Case "Regional District"
                    SQLOrder = SQLOrder & " strDistrictName " & temp
                Case "Regional Office"
                    SQLOrder = SQLOrder & " strOfficeName " & temp
                Case "SIC Code"
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationData.strSICCode " & temp
            End Select

            If SQLOrder = " order by " Then
                SQLOrder = " order by "
            Else
                SQLOrder = SQLOrder & ", "
            End If

            If SortOrder2 = "Descending Order" Then
                temp = "Desc"
            Else
                temp = " "
            End If

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            Select Case Sort2
                Case "AIRS No."
                    SQLOrder = SQLOrder & " strAIRSNumber " & temp
                Case "Application No."
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber " & temp
                Case "Application Status"
                    SQLOrder = SQLOrder & " AppStatus " & temp
                Case "Application Type"
                    SQLOrder = SQLOrder & " strApplicationType " & temp
                Case "Application Unit"
                    SQLOrder = SQLOrder & " strUnitDesc " & temp
                Case "Applog Comments"
                    SQLOrder = SQLOrder & " strComments " & temp
                Case "Date Acknowledged"
                    SQLOrder = SQLOrder & " datAcknowledgementLetterSent " & temp
                Case "Date APL Completed"
                    SQLOrder = SQLOrder & " datApplicationPackageComplete " & temp
                Case "Date APL Dated"
                    SQLOrder = SQLOrder & " datSentByFacility " & temp
                Case "Date APL Received"
                    SQLOrder = SQLOrder & " datReceivedDate " & temp
                Case "Date Assigned"
                    SQLOrder = SQLOrder & " datAssignedtoEngineer " & temp
                Case "Date Draft Issued"
                    SQLOrder = SQLOrder & " datDraftIssued " & temp
                Case "Date PA Expires"
                    SQLOrder = SQLOrder & " datPAExpires " & temp
                Case "Date Finalized"
                    SQLOrder = SQLOrder & " datPermitIssued " & temp
                Case "Date PN Expires"
                    SQLOrder = SQLOrder & " datPNExpires " & temp
                Case "Date Reassigned"
                    SQLOrder = SQLOrder & " datReassignedToEngineer " & temp
                Case "Date Started Review"
                    SQLOrder = SQLOrder & " datApplicationStarted " & temp
                Case "Date to BC"
                    SQLOrder = SQLOrder & " datToBranchCheif " & temp
                Case "Date to DO"
                    SQLOrder = SQLOrder & " datToDirector " & temp
                Case "Date to PM"
                    SQLOrder = SQLOrder & " datToPMII " & temp
                Case "Date to UC"
                    SQLOrder = SQLOrder & " datToPMI " & temp
                Case "Date Withdrawn"
                    SQLOrder = SQLOrder & " datWithdrawn " & temp
                Case "Deadline"
                    SQLOrder = SQLOrder & " datApplicationDeadLine " & temp
                Case "Engineer Firstname"
                    SQLOrder = SQLOrder & " strFirstName " & temp
                Case "Engineer Lastname"
                    SQLOrder = SQLOrder & " strLastName " & temp
                Case "Engineer Unit Code"
                    SQLOrder = SQLOrder & " APBUnit " & temp
                Case "EPA 45-day Waived"
                    SQLOrder = SQLOrder & " datEPAWaived " & temp
                Case "EPA 45-day Ends"
                    SQLOrder = SQLOrder & " datEPAEnds " & temp
                Case "Facilty City"
                    SQLOrder = SQLOrder & " strFacilityCity " & temp
                Case "Facility County"
                    SQLOrder = SQLOrder & " strCountyName " & temp
                Case "Facility Name"
                    SQLOrder = SQLOrder & " strFacilityName " & temp
                Case "Facility Street"
                    SQLOrder = SQLOrder & " strFacilityStreet1 " & temp
                Case "NAA 1Hr-Yes"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 1Hr-Contr."
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 1Hr-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-Atlanta"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-Macon"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA 8Hr-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Atlanta"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Chattanooga"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Floyd"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-Macon"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "NAA PM-No"
                    SQLOrder = SQLOrder & " strAttainmentStatus " & temp
                Case "PA Ready"
                    SQLOrder = SQLOrder & " strPAReady " & temp
                Case "Permit Number"
                    SQLOrder = SQLOrder & " strPermitNumber " & temp
                Case "Permit Type"
                    SQLOrder = SQLOrder & " strPermitType " & temp
                Case "Plant Description"
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationData.strPlantDescription " & temp
                Case "PN Ready"
                    SQLOrder = SQLOrder & " strPNReady " & temp
                Case "Public Advisory"
                    SQLOrder = SQLOrder & " strPublicInvolvement " & temp
                Case "Reason APL Submitted"
                    SQLOrder = SQLOrder & " strApplicationNotes " & temp
                Case "Regional District"
                    SQLOrder = SQLOrder & " strDistrictName " & temp
                Case "Regional Office"
                    SQLOrder = SQLOrder & " strOfficeName " & temp
                Case "SIC Code"
                    SQLOrder = SQLOrder & " AIRBRANCH.SSPPApplicationData.strSICCode " & temp
            End Select

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            temp = Mid(SQLOrder, (Len(SQLOrder) - 1))
            If Mid(SQLOrder, (Len(SQLOrder) - 1)) = ", " Then
                SQLOrder = Mid(SQLOrder, 1, (Len(SQLOrder) - 2))
            End If

            SQLLine = SQLLine & SQLOrder

            SQL = SQL & SQLLine

            If bgwApplicationLog.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            Using connection As New OracleConnection(DB.CurrentConnectionString)
                Using dataAdapter As New OracleDataAdapter(SQL, CurrentConnection)
                    Try
                        connection.Open()

                        If bgwApplicationLog.CancellationPending Then
                            e.Cancel = True
                            Exit Sub
                        End If

                        dataAdapter.Fill(dsApplication, "ApplicationLog")
                    Catch ee As OracleException
                        MessageBox.Show("Could not connect to the database.")
                        e.Cancel = True
                    End Try
                End Using
            End Using

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If bgwApplicationLog.CancellationPending Then e.Cancel = True
        End Try

    End Sub
    Private Sub bgwApplicationLog_RunWorkerCompleted(ByVal sender As Object, _
        ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
        Handles bgwApplicationLog.RunWorkerCompleted

        btnFind.Enabled = True
        btnResetSearch.Enabled = True
        mmiResetSearch.Enabled = True
        btnOpen.Enabled = False
        mmiOpen.Enabled = False
        btnExport.Enabled = False
        mmiExport.Enabled = False

        If e.Cancelled Then
            dsApplication = New DataSet
            'dgvApplicationLog.DataSource = dsApplication

            dgvApplicationLog.Visible = False
            lblMessage.Text = "Search canceled"
            lblMessage.Visible = True

            Exit Sub
        End If

        dgvApplicationLog.DataSource = dsApplication
        dgvApplicationLog.DataMember = "ApplicationLog"

        dgvApplicationLog.RowHeadersVisible = False
        dgvApplicationLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvApplicationLog.AllowUserToResizeColumns = True
        dgvApplicationLog.AllowUserToAddRows = False
        dgvApplicationLog.AllowUserToDeleteRows = False
        dgvApplicationLog.AllowUserToOrderColumns = True
        dgvApplicationLog.AllowUserToResizeRows = True

        dgvApplicationLog.Columns("strApplicationNumber").HeaderText = "APL #"
        dgvApplicationLog.Columns("strApplicationNumber").DisplayIndex = 0
        dgvApplicationLog.Columns("strApplicationType").HeaderText = "APL Type"
        dgvApplicationLog.Columns("strApplicationType").DisplayIndex = 4
        dgvApplicationLog.Columns("datReceivedDate").HeaderText = "APL Rcvd"
        dgvApplicationLog.Columns("datReceivedDate").DisplayIndex = 5
        dgvApplicationLog.Columns("datPermitIssued").HeaderText = "Date Finalized"
        dgvApplicationLog.Columns("datPermitIssued").DisplayIndex = 6
        dgvApplicationLog.Columns("datPermitIssued").Visible = False
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
        dgvApplicationLog.Columns("strSICCode").HeaderText = "SIC Code"
        dgvApplicationLog.Columns("strSICCode").DisplayIndex = 11
        dgvApplicationLog.Columns("strPlantDescription").HeaderText = "Plant Description"
        dgvApplicationLog.Columns("strPlantDescription").DisplayIndex = 12

        dgvApplicationLog.MakeColumnsLookLikeLinks(0)

        If chbShowAll.Checked = True Then
            dgvApplicationLog.Columns("strFacilityStreet1").HeaderText = "Facility Address"
            dgvApplicationLog.Columns("strFacilityStreet1").DisplayIndex = 13
            dgvApplicationLog.Columns("strFacilityCity").HeaderText = "City"
            dgvApplicationLog.Columns("strFacilityCity").DisplayIndex = 14
            dgvApplicationLog.Columns("strCountyName").HeaderText = "County"
            dgvApplicationLog.Columns("strCountyName").DisplayIndex = 15
            dgvApplicationLog.Columns("OneHrOzone").HeaderText = "1-HR Ozone"
            dgvApplicationLog.Columns("OneHrOzone").DisplayIndex = 16
            dgvApplicationLog.Columns("EightHrOzone").HeaderText = "8-HR Ozone"
            dgvApplicationLog.Columns("EightHrOzone").DisplayIndex = 17
            dgvApplicationLog.Columns("PMFine").HeaderText = "PM Fine"
            dgvApplicationLog.Columns("PMFine").DisplayIndex = 18
            dgvApplicationLog.Columns("strDistrictName").HeaderText = "District"
            dgvApplicationLog.Columns("strDistrictName").DisplayIndex = 19
            dgvApplicationLog.Columns("strOfficeName").HeaderText = "Office"
            dgvApplicationLog.Columns("strOfficeName").DisplayIndex = 20
            dgvApplicationLog.Columns("APBUnit").HeaderText = "APL Unit"
            dgvApplicationLog.Columns("APBUnit").DisplayIndex = 21
            dgvApplicationLog.Columns("strLastname").HeaderText = "Last Name"
            dgvApplicationLog.Columns("strLastname").DisplayIndex = 22
            dgvApplicationLog.Columns("strFirstName").HeaderText = "First Name"
            dgvApplicationLog.Columns("strFirstName").DisplayIndex = 23
            dgvApplicationLog.Columns("strUserUnit").HeaderText = "Engineer Unit"
            dgvApplicationLog.Columns("strUserUnit").DisplayIndex = 24
            dgvApplicationLog.Columns("datSentByFacility").HeaderText = "APL Dated"
            dgvApplicationLog.Columns("datSentByFacility").DisplayIndex = 25
            dgvApplicationLog.Columns("datAssignedtoEngineer").HeaderText = "App Assigned"
            dgvApplicationLog.Columns("datAssignedtoEngineer").DisplayIndex = 26
            dgvApplicationLog.Columns("datReassignedToEngineer").HeaderText = "App Reassigned"
            dgvApplicationLog.Columns("datReassignedToEngineer").DisplayIndex = 27
            dgvApplicationLog.Columns("datAcknowledgementLetterSent").HeaderText = "Acknow Date"
            dgvApplicationLog.Columns("datAcknowledgementLetterSent").DisplayIndex = 28
            dgvApplicationLog.Columns("strPublicInvolvement").HeaderText = "Public Advisory"
            dgvApplicationLog.Columns("strPublicInvolvement").DisplayIndex = 29
            dgvApplicationLog.Columns("datPNExpires").HeaderText = "PN Expired"
            dgvApplicationLog.Columns("datPNExpires").DisplayIndex = 30
            dgvApplicationLog.Columns("datPAExpires").HeaderText = "PA Expired"
            dgvApplicationLog.Columns("datPAExpires").DisplayIndex = 31
            dgvApplicationLog.Columns("datToPMI").HeaderText = "Date to UC"
            dgvApplicationLog.Columns("datToPMI").DisplayIndex = 32
            dgvApplicationLog.Columns("datToPMII").HeaderText = "Date to PM"
            dgvApplicationLog.Columns("datToPMII").DisplayIndex = 33
            dgvApplicationLog.Columns("datDraftIssued").HeaderText = "Draft Issued"
            dgvApplicationLog.Columns("datDraftIssued").DisplayIndex = 34
            dgvApplicationLog.Columns("strComments").HeaderText = "Applog Comments"
            dgvApplicationLog.Columns("strComments").DisplayIndex = 35
            dgvApplicationLog.Columns("strApplicationNotes").HeaderText = "Reason APL Submitted"
            dgvApplicationLog.Columns("strApplicationNotes").DisplayIndex = 36
            dgvApplicationLog.Columns("datApplicationDeadLine").HeaderText = "Application Deadline"
            dgvApplicationLog.Columns("datApplicationDeadLine").DisplayIndex = 37
            dgvApplicationLog.Columns("numUserID").Visible = False
            dgvApplicationLog.Columns("strPAReady").HeaderText = "PA Ready"
            dgvApplicationLog.Columns("strPAReady").DisplayIndex = 38
            dgvApplicationLog.Columns("strPNReady").HeaderText = "PN Ready"
            dgvApplicationLog.Columns("strPNReady").DisplayIndex = 39
            dgvApplicationLog.Columns("datEPAWaived").HeaderText = "EPA 45-day Waived"
            dgvApplicationLog.Columns("datEPAWaived").DisplayIndex = 40
            dgvApplicationLog.Columns("datEPAEnds").HeaderText = "EPA 45-day Ends"
            dgvApplicationLog.Columns("datEPAEnds").DisplayIndex = 41
            dgvApplicationLog.Columns("datToBranchCheif").HeaderText = "Date To BC"
            dgvApplicationLog.Columns("datToBranchCheif").DisplayIndex = 42
            dgvApplicationLog.Columns("datToDirector").HeaderText = "Date to DO"
            dgvApplicationLog.Columns("datToDirector").DisplayIndex = 43
            dgvApplicationLog.Columns("NSRMajor").HeaderText = "NSR/PSD Major"
            dgvApplicationLog.Columns("NSRMajor").DisplayIndex = 44
            dgvApplicationLog.Columns("HAPSMajor").HeaderText = "HAPs Major"
            dgvApplicationLog.Columns("HAPSMajor").DisplayIndex = 45
            dgvApplicationLog.Columns("PSDRule").HeaderText = "PSD Rule"
            dgvApplicationLog.Columns("PSDRule").DisplayIndex = 46
            dgvApplicationLog.Columns("NAARule").HeaderText = "NAA Rule"
            dgvApplicationLog.Columns("NAARule").DisplayIndex = 47
            dgvApplicationLog.Columns("gRule").HeaderText = "112(g)"
            dgvApplicationLog.Columns("gRule").DisplayIndex = 48
            dgvApplicationLog.Columns("ttRACT").HeaderText = "Rule (tt) RACT"
            dgvApplicationLog.Columns("ttRACT").DisplayIndex = 49
            dgvApplicationLog.Columns("yyRACT").HeaderText = "Rule (yy) RACT"
            dgvApplicationLog.Columns("yyRACT").DisplayIndex = 50
            dgvApplicationLog.Columns("PALRule").HeaderText = "PAL Rule"
            dgvApplicationLog.Columns("PALRule").DisplayIndex = 51
            dgvApplicationLog.Columns("ExpeditedPermitRule").HeaderText = "Expedited Permit"
            dgvApplicationLog.Columns("ExpeditedPermitRule").DisplayIndex = 52
            dgvApplicationLog.Columns("datApplicationPackageComplete").Visible = False
            dgvApplicationLog.Columns("datFinalizedDate").Visible = False
            dgvApplicationLog.Columns("datWithdrawn").Visible = False
            dgvApplicationLog.Columns("datApplicationStarted").Visible = False
            dgvApplicationLog.Columns("strAFSGCode").HeaderText = "AFS G Code"
            dgvApplicationLog.Columns("strAFSGCode").DisplayIndex = 53
            dgvApplicationLog.Columns("strAFSGCode").Visible = False
            dgvApplicationLog.Columns("strSubpart").HeaderText = "Subpart"
            dgvApplicationLog.Columns("strSubpart").DisplayIndex = 54
        End If

        If dsApplication.Tables(0).Rows.Count = 0 Then
            dgvApplicationLog.Visible = False
            Panel1.Text = "No applications found"
            lblMessage.Text = "No applications found"
            lblMessage.Visible = True
        Else
            If dsApplication.Tables(0).Rows.Count = 1 Then
                Panel1.Text = dsApplication.Tables(0).Rows.Count & " application found"
            Else
                Panel1.Text = dsApplication.Tables(0).Rows.Count & " applications found"
            End If
            dgvApplicationLog.Visible = True
            dgvApplicationLog.SanelyResizeColumns()
            lblMessage.Visible = False

            btnExport.Enabled = True
            mmiExport.Enabled = True
        End If
    End Sub
#End Region

    Sub ExportToExcel()
        If dgvApplicationLog.RowCount > 0 Then
            dgvApplicationLog.ExportToExcel()
        End If
    End Sub

#Region "Other procedures"
    Private Sub StartNewApplication()
        Try
            If AccountFormAccess(3, 4) = "1" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.BringToFront()
            Else
                MessageBox.Show("You do not have sufficient permissions to start a new application.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub OpenApplication(ByVal app As String)
        Try
            If app <> "" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = New SSPPApplicationTrackingLog
                End If
                PermitTrackingLog.Show()
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = app
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

#Region "Events"
    Private Sub cboFieldType1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFieldType1.SelectedIndexChanged
        Try

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
                    'cboSearchText1.Items.Add("10 - To DO")
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
                Case "Facilty City"
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

                Case "Regional Office"
                    txtSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate1b.Visible = False
                    cboSearchText1.Visible = True
                    cboSIP1.Visible = False
                    cboNESHAP1.Visible = False
                    cboNSPS1.Visible = False
                    cboMACT1.Visible = False

                    cboSearchText1.Items.Clear()
                    cboSearchText1.Items.Add("Albany")
                    cboSearchText1.Items.Add("Athens")
                    cboSearchText1.Items.Add("Atlanta")
                    cboSearchText1.Items.Add("Augusta")
                    cboSearchText1.Items.Add("Brunswick")
                    cboSearchText1.Items.Add("Cartersville")
                    cboSearchText1.Items.Add("Macon")
                    cboSearchText1.Items.Add("Main Office")
                    cboSearchText1.Items.Add("Savannah")
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
                Case "Subpart - 0-SIP"
                    txtSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate1b.Visible = False
                    cboSearchText1.Visible = False
                    cboSIP1.Visible = True
                    cboNESHAP1.Visible = False
                    cboNSPS1.Visible = False
                    cboMACT1.Visible = False
                Case "Subpart - 8-NESHAP (Part 61)"
                    txtSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate1b.Visible = False
                    cboSearchText1.Visible = False
                    cboSIP1.Visible = False
                    cboNESHAP1.Visible = True
                    cboNSPS1.Visible = False
                    cboMACT1.Visible = False
                Case "Subpart - 9-NSPS (Part 60)"
                    txtSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate1b.Visible = False
                    cboSearchText1.Visible = False
                    cboSIP1.Visible = False
                    cboNESHAP1.Visible = False
                    cboNSPS1.Visible = True
                    cboMACT1.Visible = False
                Case "Subpart - M-MACT (Part 63)"
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboFieldType2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldType2.SelectedIndexChanged
        Try

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
                    'cboSearchText2.Items.Add("10 - To DO")
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
                Case "Facilty City"
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

                Case "Regional Office"
                    txtSearchText2.Visible = False
                    DTPSearchDate2.Visible = False
                    DTPSearchDate2b.Visible = False
                    cboSearchText2.Visible = True
                    cboSIP2.Visible = False
                    cboNESHAP2.Visible = False
                    cboNSPS2.Visible = False
                    cboMACT2.Visible = False

                    cboSearchText2.Items.Clear()
                    cboSearchText2.Items.Add("Albany")
                    cboSearchText2.Items.Add("Athens")
                    cboSearchText2.Items.Add("Atlanta")
                    cboSearchText2.Items.Add("Augusta")
                    cboSearchText2.Items.Add("Brunswick")
                    cboSearchText2.Items.Add("Cartersville")
                    cboSearchText2.Items.Add("Macon")
                    cboSearchText2.Items.Add("Main Office")
                    cboSearchText2.Items.Add("Savannah")
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

                Case "Subpart - 0-SIP"
                    txtSearchText2.Visible = False
                    DTPSearchDate2.Visible = False
                    DTPSearchDate2b.Visible = False
                    cboSearchText2.Visible = False
                    cboSIP2.Visible = True
                    cboNESHAP2.Visible = False
                    cboNSPS2.Visible = False
                    cboMACT2.Visible = False
                Case "Subpart - 8-NESHAP (Part 61)"
                    txtSearchText2.Visible = False
                    DTPSearchDate2.Visible = False
                    DTPSearchDate2b.Visible = False
                    cboSearchText2.Visible = False
                    cboSIP2.Visible = False
                    cboNESHAP2.Visible = True
                    cboNSPS2.Visible = False
                    cboMACT2.Visible = False
                Case "Subpart - 9-NSPS (Part 60)"
                    txtSearchText2.Visible = False
                    DTPSearchDate2.Visible = False
                    DTPSearchDate2b.Visible = False
                    cboSearchText2.Visible = False
                    cboSIP2.Visible = False
                    cboNESHAP2.Visible = False
                    cboNSPS2.Visible = True
                    cboMACT2.Visible = False
                Case "Subpart - M-MACT (Part 63)"
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

#Region " DataGridView Events "

    Private Sub dgvApplicationLog_CellMouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvApplicationLog.CellMouseEnter
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)

        If e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index And e.RowIndex <> -1 Then
            dgvApplicationLog.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellMouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvApplicationLog.CellMouseLeave
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index And e.RowIndex <> -1 Then
            dgvApplicationLog.MakeCellNotLookLikeHoveredLink(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvApplicationLog.CellClick

        ' Anywhere in any cell in any non-header row in grid
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount Then
            selectedApp = dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value
            'Panel1.Text = selectedApp & " – " & _
            '    dgvApplicationLog.Rows(e.RowIndex).Cells("strFacilityName").Value
            btnOpen.Enabled = True
            mmiOpen.Enabled = True
        End If

        ' Only within the cell content of first column (App #)
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount _
            And e.ColumnIndex = dgvApplicationLog.Columns("strApplicationNumber").Index Then
            OpenApplication(dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvApplicationLog.CellDoubleClick
        'Double-click within the cell content (exclude first column to avoid double-firing)
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount _
            And e.ColumnIndex <> dgvApplicationLog.Columns("strApplicationNumber").Index Then
            OpenApplication(dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value)
        End If
    End Sub

    Private Sub dgvApplicationLog_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvApplicationLog.CellEnter
        If e.RowIndex <> -1 And e.RowIndex < dgvApplicationLog.RowCount Then
            selectedApp = dgvApplicationLog.Rows(e.RowIndex).Cells("strApplicationNumber").Value
            'Panel1.Text = selectedApp & " – " & _
            '    dgvApplicationLog.Rows(e.RowIndex).Cells("strFacilityName").Value
            btnOpen.Enabled = True
            mmiOpen.Enabled = True
        End If
    End Sub

#End Region

#Region "Menu, buttons, and toolbar"
    Private Sub NewApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNewApplication.Click
        StartNewApplication()
    End Sub
    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub
    Private Sub mmiOpenHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub mmiOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOpen.Click, btnOpen.Click
        If selectedApp <> "" Then OpenApplication(selectedApp)
    End Sub
    Private Sub mmiReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetSearch.Click, btnResetSearch.Click
        LoadDefaults()
    End Sub
    Private Sub mmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExport.Click, btnExport.Click
        ExportToExcel()
    End Sub
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        RunSearch()
    End Sub
#End Region

End Class