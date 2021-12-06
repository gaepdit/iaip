Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports EpdIt.DBUtilities
Imports Iaip.Apb.Facilities

Public Class EisTool

    Private Sub EisTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not CurrentUser.HasPermission(UserCan.AccessEmissionsInventory) Then
            Close()
        End If

        LoadcboEISstatusCodes()
        LoadEISLog()
        LoadStats()
        LoadEISYear()
        LoadOperStatus()
        LoadHistoryComboBoxes()
    End Sub

    Private Sub LoadOperStatus()
        cbIaipOperStatus.BindToDictionary(FacilityOperationalStatusDescriptions)
        cbEisModifyOperStatus.BindToDictionary(EisSiteStatusCodeDescriptions)
    End Sub

    Private Sub LoadEISLog()
        Dim SQL As String = "Select distinct(inventoryYear) as InvYear " &
            "from EIS_Admin " &
            "order by invYear desc "
        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboEILogYear.Items.Add(dr.Item("InvYear"))
            cboEISStatisticsYear.Items.Add(dr.Item("InvYear"))
            cboAllContacts.Items.Add(dr.Item("InvYear"))
        Next

        cboEILogYear.SelectedIndex = 0
        cboEISStatisticsYear.SelectedIndex = 0
        cboAllContacts.SelectedIndex = 0

        SQL = "select distinct strDMUResponsibleStaff as DMUStafff " &
            "from EIS_QAAdmin " &
            "union " &
            "select distinct (strLastName +', '+ strFirstName) as DMUStafff " &
            "from EPDUserProfiles " &
            "where numBranch = '1' " &
            "and numProgram = '3' " &
            "and numunit = '14' " &
            "and numEmployeeStatus = 1 "
        dt = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboEISQAStaff.Items.Add(dr.Item("DMUStafff"))
        Next

        SQL = "Select " &
            " '' as QAStatusCode, '' as strDesc " &
            " union Select " &
            "QAStatusCode, strDesc " &
            "From EISLK_QAStatus " &
            "Where active = '1' "
        Dim dtQAStatus As DataTable = DB.GetDataTable(SQL)

        With cboEISQAStatus
            .DataSource = dtQAStatus
            .DisplayMember = "strDesc"
            .ValueMember = "QAStatusCode"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadStats()
        dgvEISStats.RowHeadersVisible = False
        dgvEISStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvEISStats.AllowUserToResizeColumns = True
        dgvEISStats.AllowUserToAddRows = False
        dgvEISStats.AllowUserToDeleteRows = False
        dgvEISStats.AllowUserToOrderColumns = False
        dgvEISStats.AllowUserToResizeRows = False
        dgvEISStats.ColumnHeadersHeight = 35

        Dim colSelect As New DataGridViewCheckBoxColumn With {
            .ThreeState = False,
            .TrueValue = True,
            .FalseValue = False,
            .HeaderText = "Select",
            .Name = "Select"
        }

        dgvEISStats.Columns.Add(colSelect)
        dgvEISStats.Columns(0).Width = 50

        dgvEISStats.Columns.Add("FacilitySiteID", "AIRS No.")
        dgvEISStats.Columns("FacilitySiteID").DisplayIndex = 1
        dgvEISStats.Columns("FacilitySiteID").Visible = True

        dgvEISStats.Columns.Add("strFacilityName", "Facility Name")
        dgvEISStats.Columns("strFacilityName").DisplayIndex = 2
        dgvEISStats.Columns("strFacilityName").Width = 250

        dgvEISStats.Columns.Add("InventoryYear", "EIS Year")
        dgvEISStats.Columns("InventoryYear").DisplayIndex = 3
        dgvEISStats.Columns("InventoryYear").Visible = True

        dgvEISStats.Columns.Add("EISStatus", "EIS Status")
        dgvEISStats.Columns("EISStatus").DisplayIndex = 4
        dgvEISStats.Columns("EISStatus").Visible = True

        dgvEISStats.Columns.Add("EISAccess", "EIS Access")
        dgvEISStats.Columns("EISAccess").DisplayIndex = 5
        dgvEISStats.Columns("EISAccess").Visible = True

        dgvEISStats.Columns.Add("OptOut", "Opt Out")
        dgvEISStats.Columns("OptOut").DisplayIndex = 6
        dgvEISStats.Columns("OptOut").Visible = True

        dgvEISStats.Columns.Add("MailOut", "Mailout")
        dgvEISStats.Columns("MailOut").DisplayIndex = 7
        dgvEISStats.Columns("MailOut").Visible = True

        dgvEISStats.Columns.Add("MailoutEmail", "Mailout Email")
        dgvEISStats.Columns("MailoutEmail").DisplayIndex = 8
        dgvEISStats.Columns("MailoutEmail").Visible = True

        dgvEISStats.Columns.Add("strDMUResponsibleStaff", "QA Reviewer")
        dgvEISStats.Columns("strDMUResponsibleStaff").DisplayIndex = 9
        dgvEISStats.Columns("strDMUResponsibleStaff").Visible = True

        dgvEISStats.Columns.Add("Enrollment", "Enrollment")
        dgvEISStats.Columns("Enrollment").DisplayIndex = 10
        dgvEISStats.Columns("Enrollment").Visible = True

        dgvEISStats.Columns.Add("QASTATUS", "QA Status")
        dgvEISStats.Columns("QASTATUS").DisplayIndex = 11
        dgvEISStats.Columns("QASTATUS").Visible = True

        dgvEISStats.Columns.Add("datQAStatus", "QA Status Date")
        dgvEISStats.Columns("datQAStatus").DisplayIndex = 12
        dgvEISStats.Columns("datQAStatus").Visible = True
        dgvEISStats.Columns("datQAStatus").DefaultCellStyle.Format = "dd-MMM-yyyy"


        dgvEISStats.Columns.Add("IAIPPrefix", "IAIP Prefix")
        dgvEISStats.Columns("IAIPPrefix").DisplayIndex = 13
        dgvEISStats.Columns("IAIPPrefix").Visible = True

        dgvEISStats.Columns.Add("IAIPFIRSTNAME", "IAIP First Name")
        dgvEISStats.Columns("IAIPFIRSTNAME").DisplayIndex = 14
        dgvEISStats.Columns("IAIPFIRSTNAME").Visible = True


        dgvEISStats.Columns.Add("IAIPLASTNAME", "IAIP Last Name")
        dgvEISStats.Columns("IAIPLASTNAME").DisplayIndex = 15
        dgvEISStats.Columns("IAIPLASTNAME").Visible = True

        dgvEISStats.Columns.Add("IAIPEMAIL", "IAIP Email")
        dgvEISStats.Columns("IAIPEMAIL").DisplayIndex = 16
        dgvEISStats.Columns("IAIPEMAIL").Visible = True

        dgvEISStats.Columns.Add("EISCOMPANYNAME", "Contact Co. Name")
        dgvEISStats.Columns("EISCOMPANYNAME").DisplayIndex = 17
        dgvEISStats.Columns("EISCOMPANYNAME").Visible = True

        dgvEISStats.Columns.Add("EISADDRESS", "Contact Address")
        dgvEISStats.Columns("EISADDRESS").DisplayIndex = 18
        dgvEISStats.Columns("EISADDRESS").Visible = True

        dgvEISStats.Columns.Add("EISADDRESS2", "Contact Address 2")
        dgvEISStats.Columns("EISADDRESS2").DisplayIndex = 19
        dgvEISStats.Columns("EISADDRESS2").Visible = True

        dgvEISStats.Columns.Add("EISCITY", "Contact City")
        dgvEISStats.Columns("EISCITY").DisplayIndex = 20
        dgvEISStats.Columns("EISCITY").Visible = True

        dgvEISStats.Columns.Add("EISState", "Contact State")
        dgvEISStats.Columns("EISState").DisplayIndex = 21
        dgvEISStats.Columns("EISState").Visible = True

        dgvEISStats.Columns.Add("EISZipCode", "Contact Zip Code")
        dgvEISStats.Columns("EISZipCode").DisplayIndex = 22
        dgvEISStats.Columns("EISZipCode").Visible = True

        dgvEISStats.Columns.Add("EISPrefix", "Contact Prefix")
        dgvEISStats.Columns("EISPrefix").DisplayIndex = 23
        dgvEISStats.Columns("EISPrefix").Visible = True

        dgvEISStats.Columns.Add("EISFirstname", "Contact First Name")
        dgvEISStats.Columns("EISFirstname").DisplayIndex = 24
        dgvEISStats.Columns("EISFirstname").Visible = True

        dgvEISStats.Columns.Add("EISLastName", "Contact Last Name")
        dgvEISStats.Columns("EISLastName").DisplayIndex = 25
        dgvEISStats.Columns("EISLastName").Visible = True

        dgvEISStats.Columns.Add("DATFINALIZE", "Date Submitted")
        dgvEISStats.Columns("DATFINALIZE").DisplayIndex = 26
        dgvEISStats.Columns("DATFINALIZE").Visible = True

        dgvEISStats.Columns.Add("FITrackingNumber", "FI Tracking Number")
        dgvEISStats.Columns("FITrackingNumber").DisplayIndex = 27
        dgvEISStats.Columns("FITrackingNumber").Visible = True

        dgvEISStats.Columns.Add("PointTrackingNumber", "Point Tracking Number")
        dgvEISStats.Columns("PointTrackingNumber").DisplayIndex = 28
        dgvEISStats.Columns("PointTrackingNumber").Visible = True

        dgvEISStats.Columns.Add("Comments", "Comments")
        dgvEISStats.Columns("Comments").DisplayIndex = 29
        dgvEISStats.Columns("Comments").Visible = True
    End Sub

    Private Sub LoadcboEISstatusCodes()
        Dim SQL As String = "Select '' as EISSTATUSCODE, '- Select EIS Status Code -' as STRDESC " &
            " union select distinct  EISSTATUSCODE, STRDESC " &
            " from EISLK_EISSTATUSCODE "

        With cboEILogStatusCode
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "STRDESC"
            .ValueMember = "EISSTATUSCODE"
            .SelectedIndex = 0
        End With

        SQL = "select '' as EISAccessCode, '- Select EIS Access Code -' as STRDESC " &
            " union select EISAccessCode, strDesc " &
            " from EISLK_EISAccesscode " &
            " order by strDesc"

        With cboEILogAccessCode
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "STRDESC"
            .ValueMember = "EISAccessCode"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub btnReloadFSData_Click(sender As Object, e As EventArgs) Handles btnReloadFSData.Click
        LoadFSData()
    End Sub

    Private Sub LoadFSData()
        Try
            If cboEILogYear.Text = "" OrElse cboEILogYear.Text.Length <> 4 Then
                MsgBox("Please select a valid year from the EIS Year dropdown.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If Not mtbEILogAIRSNumber.IsValid Then
                MsgBox("Please enter a valid AIRS #.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            txtEILogSelectedYear.Text = cboEILogYear.Text
            txtEILogSelectedAIRSNumber.AirsNumber = mtbEILogAIRSNumber.AirsNumber

            If Not LoadAdminData() Then
                MsgBox("AIRS # does not exist for the selected EIS year.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "select  " &
            "strFacilitySiteName, STRFACILITYSITESTATUSCODE " &
            "from EIS_FacilitySite " &
            "where FacilitySiteId = @FacilitySiteId "

            Dim param As New SqlParameter("@FacilitySiteId", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)

            Dim dr As DataRow = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilitySiteName")) Then
                    txtEIModifyFacilityName.Clear()
                    txtEILogFacilityName.Clear()
                Else
                    txtEIModifyFacilityName.Text = dr.Item("strFacilitySiteName")
                    txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                End If

                If IsDBNull(dr.Item("STRFACILITYSITESTATUSCODE")) Then
                    cbEisModifyOperStatus.SelectedValue = EisSiteStatus.UNK
                Else
                    cbEisModifyOperStatus.SelectedValue = [Enum].Parse(GetType(EisSiteStatus), dr.Item("STRFACILITYSITESTATUSCODE"))
                End If
            End If

            SQL = "select * " &
            "from EIS_FacilitySiteAddress " &
            "where FacilitySiteId = @FacilitySiteId "

            dr = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strLocationAddressText")) Then
                    txtEIModifyLocation.Clear()
                Else
                    txtEIModifyLocation.Text = dr.Item("strLocationAddressText")
                End If
                If IsDBNull(dr.Item("strLocalityName")) Then
                    txtEIModifyCity.Clear()
                Else
                    txtEIModifyCity.Text = dr.Item("strLocalityName")
                End If
                If IsDBNull(dr.Item("strLocationAddressPostalCode")) Then
                    mtbEIModifyZipCode.Clear()
                Else
                    mtbEIModifyZipCode.Text = dr.Item("strLocationAddressPostalCode")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSTEXT")) Then
                    txtEIModifyMLocation.Clear()
                Else
                    txtEIModifyMLocation.Text = dr.Item("STRMAILINGADDRESSTEXT")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSCITYNAME")) Then
                    txtEIModifyMCity.Clear()
                Else
                    txtEIModifyMCity.Text = dr.Item("STRMAILINGADDRESSCITYNAME")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSPOSTALCODE")) Then
                    mtbEIModifyMZipCode.Clear()
                Else
                    mtbEIModifyMZipCode.Text = dr.Item("STRMAILINGADDRESSPOSTALCODE")
                End If
            End If

            SQL = "select numLatitudeMeasure, numLongitudeMeasure " &
            "from EIS_FacilityGeoCoord " &
            "where FacilitySiteId = @FacilitySiteId "

            dr = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numLatitudeMeasure")) Then
                    mtbEIModifyLatitude.Clear()
                Else
                    mtbEIModifyLatitude.Text = dr.Item("numLatitudeMeasure")
                End If
                If IsDBNull(dr.Item("numLongitudeMeasure")) Then
                    mtbEIModifyLongitude.Clear()
                Else
                    mtbEIModifyLongitude.Text = dr.Item("numLongitudeMeasure")
                End If
            End If

            SQL = "SELECT fi.STRFACILITYNAME, fi.STRFACILITYSTREET1, " &
                "  fi.STRFACILITYCITY, fi.STRFACILITYSTATE, " &
                "  fi.STRFACILITYZIPCODE, fi.NUMFACILITYLONGITUDE, " &
                "  fi.NUMFACILITYLATITUDE, hd.STROPERATIONALSTATUS " &
                "FROM APBFACILITYINFORMATION fi " &
                "INNER JOIN APBHEADERDATA hd ON fi.STRAIRSNUMBER = " &
                "  hd.STRAIRSNUMBER " &
                "WHERE fi.STRAIRSNUMBER = @airs "

            Dim param2 As New SqlParameter("@airs", txtEILogSelectedAIRSNumber.AirsNumber.DbFormattedString)

            dr = DB.GetDataRow(SQL, param2)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEIModifyIAIPFacilityName.Clear()
                Else
                    txtEIModifyIAIPFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtEIModifyIAIPLocation.Clear()
                Else
                    txtEIModifyIAIPLocation.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtEIModifyIAIPCity.Clear()
                Else
                    txtEIModifyIAIPCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbEIModifyIAIPZipCode.Clear()
                Else
                    mtbEIModifyIAIPZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbEIModifyIAIPLongitude.Clear()
                Else
                    mtbEIModifyIAIPLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbEIModifyIAIPLatitude.Clear()
                Else
                    mtbEIModifyIAIPLatitude.Text = dr.Item("numFacilityLatitude")
                End If
                If IsDBNull(dr.Item("STROPERATIONALSTATUS")) Then
                    cbIaipOperStatus.SelectedValue = FacilityOperationalStatus.U
                Else
                    cbIaipOperStatus.SelectedValue = [Enum].Parse(GetType(FacilityOperationalStatus), dr.Item("STROPERATIONALSTATUS"))
                End If
            End If

            SQL = "Select * " &
            "from EIS_Mailout " &
            "where intInventoryYear = @intInventoryYear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@intInventoryYear", txtEILogSelectedYear.Text),
                New SqlParameter("@FacilitySiteID", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            dr = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISMailoutFacilityName.Clear()
                Else
                    txtEISMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISMailoutCompanyName.Clear()
                Else
                    txtEISMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISMailoutAddress.Clear()
                Else
                    txtEISMailoutAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISMailoutAddress2.Clear()
                Else
                    txtEISMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISMailoutCity.Clear()
                Else
                    txtEISMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISMailoutState.Clear()
                Else
                    txtEISMailoutState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISMailoutZipCode.Clear()
                Else
                    txtEISMailoutZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISMailoutFirstName.Clear()
                Else
                    txtEISMailoutFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISMailoutLastName.Clear()
                Else
                    txtEISMailoutLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISMailoutPrefix.Clear()
                Else
                    txtEISMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISMailoutEmail.Clear()
                Else
                    txtEISMailoutEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEISMailoutComments.Clear()
                Else
                    txtEISMailoutComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISMailoutUpdateUser.Clear()
                Else
                    txtEISMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("UpdateDateTime")) Then
                    txtEISMailoutUpdateDateTime.Clear()
                Else
                    txtEISMailoutUpdateDateTime.Text = dr.Item("UpdateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISMailoutCreateDateTime.Clear()
                Else
                    txtEISMailoutCreateDateTime.Text = dr.Item("CreateDateTime")
                End If
            End If

            SQL = "select strContactFirstName, strContactLastName,
                       strContactPrefix, strContactSuffix,
                       strContactTitle, strContactPhoneNumber1,
                       strContactPhoneNumber2, strContactFaxNumber,
                       strContactEmail, strContactCompanyName,
                       strContactAddress1, strContactAddress2,
                       strContactCity, strContactState,
                       strContactZipCode, strContactDescription,
                       datModifingDate, (strLastName + ', ' + strFirstName) as ModifingPerson
                from APBContactInformation
                    inner join EPDUserProfiles
                    on APBContactInformation.strModifingPerson = EPDUserProfiles.numUserID
                where strContactKey = @key "

            Dim param3 As New SqlParameter("@key", txtEILogSelectedAIRSNumber.AirsNumber.DbFormattedString & "41")

            dr = DB.GetDataRow(SQL, param3)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISContactFirstName.Clear()
                Else
                    txtEISContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISContactLastName.Clear()
                Else
                    txtEISContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISContactPrefix.Clear()
                Else
                    txtEISContactPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtEISContactSuffix.Clear()
                Else
                    txtEISContactSuffix.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtEISContactTitle.Clear()
                Else
                    txtEISContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    txtEISContactPhone.Clear()
                Else
                    txtEISContactPhone.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    txtEISContactPhone2.Clear()
                Else
                    txtEISContactPhone2.Text = dr.Item("strContactPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    txtEISContactFax.Clear()
                Else
                    txtEISContactFax.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISContactEmail.Clear()
                Else
                    txtEISContactEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISContactCompanyName.Clear()
                Else
                    txtEISContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISContactAddress.Clear()
                Else
                    txtEISContactAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISContactAddress2.Clear()
                Else
                    txtEISContactAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISContactCity.Clear()
                Else
                    txtEISContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISContactState.Clear()
                Else
                    txtEISContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISContactZipCode.Clear()
                Else
                    txtEISContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtEISContactDescription.Clear()
                Else
                    txtEISContactDescription.Text = dr.Item("strContactDescription")
                End If
                If IsDBNull(dr.Item("ModifingPerson")) Then
                    txtEISContactUpdateUser.Clear()
                Else
                    txtEISContactUpdateUser.Text = dr.Item("ModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    txtEISContactUpdateDateTime.Clear()
                Else
                    txtEISContactUpdateDateTime.Text = dr.Item("datModifingDate")
                End If
            End If

            LoadQASpecificData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function LoadAdminData() As Boolean
        Try
            dtpDeadlineEIS.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            Dim SQL As String = "Select * " &
                "From EIS_Admin " &
                "where inventoryYear = @inventoryYear " &
                "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryYear", txtEILogSelectedYear.Text),
                New SqlParameter("@FacilitySiteID", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("EISStatusCode")) Then
                    cboEILogStatusCode.SelectedText = ""
                Else
                    cboEILogStatusCode.SelectedValue = dr.Item("EISStatusCode")
                    txtEILogStatusCode.Text = cboEILogStatusCode.Text
                End If
                If IsDBNull(dr.Item("datEISStatus")) Then
                    dtpEILogStatusDateSubmit.Value = Today
                Else
                    dtpEILogStatusDateSubmit.Text = dr.Item("datEISStatus")
                End If
                If IsDBNull(dr.Item("EISAccessCode")) Then
                    cboEILogAccessCode.Text = ""
                Else
                    cboEILogAccessCode.SelectedValue = dr.Item("EISAccessCode")
                End If

                gbColocate.Visible = False
                rdbEILogOpOutYes.Checked = False
                rdbEILogOpOutNo.Checked = False
                lblColocated.Text = ""
                txtColocatedWith.Text = ""
                lblOptOutReason.Text = ""

                If Not IsDBNull(dr.Item("strOptOut")) Then
                    If dr.Item("strOptOut").ToString = "1" Then
                        gbColocate.Visible = True
                        rdbEILogOpOutYes.Checked = True
                        txtColocatedWith.Text = dr("ColocatedWith").ToString

                        If Not IsDBNull(dr.Item("IsColocated")) Then
                            lblColocated.Text = If(CBool(dr.Item("IsColocated")), "Yes", "No")
                        End If

                        If Not IsDBNull(dr.Item("STROPTOUTREASON")) Then
                            If dr.Item("STROPTOUTREASON").ToString = "1" Then
                                lblOptOutReason.Text = "Did not operate"
                            ElseIf dr.Item("STROPTOUTREASON").ToString = "2" Then
                                lblOptOutReason.Text = "Emissions below thresholds"
                            End If
                        End If
                    Else
                        rdbEILogOpOutNo.Checked = True
                    End If
                End If

                If IsDBNull(dr.Item("strIncorrectOptOut")) Then
                    chbOptedOutIncorrectly.Checked = False
                Else
                    If dr.Item("strIncorrectOptOut") = "1" Then
                        chbOptedOutIncorrectly.Checked = True
                    Else
                        chbOptedOutIncorrectly.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    rdbEILogMailoutYes.Checked = False
                    rdbEILogMailoutNo.Checked = False
                Else
                    If dr.Item("strMailout") = "1" Then
                        rdbEILogMailoutYes.Checked = True
                    Else
                        rdbEILogMailoutNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strEnrollment")) Then
                    rdbEILogEnrolledYes.Checked = False
                    rdbEILogEnrolledNo.Checked = False
                Else
                    If dr.Item("strEnrollment") = "1" Then
                        rdbEILogEnrolledYes.Checked = True
                    Else
                        rdbEILogEnrolledNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datEnrollment")) Then
                    dtpEILogDateEnrolled.Value = Today
                Else
                    dtpEILogDateEnrolled.Text = dr.Item("datEnrollment")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEILogComments.Clear()
                Else
                    txtEILogComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    rdbEILogActiveYes.Checked = False
                    rdbEILogActiveNo.Checked = False
                Else
                    If dr.Item("Active") = "1" Then
                        rdbEILogActiveYes.Checked = True
                    Else
                        rdbEILogActiveNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("updateUser")) Then
                    txtEILogUpdatedBy.Clear()
                Else
                    txtEILogUpdatedBy.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDatetime")) Then
                    txtEILogUpdatedTime.Clear()
                Else
                    txtEILogUpdatedTime.Text = dr.Item("updateDatetime")
                End If
                If IsDBNull(dr.Item("intPrepopYear")) Then
                    txtEILogPrePopYear.Clear()
                Else
                    txtEILogPrePopYear.Text = dr.Item("intPrepopYear")
                End If
                If IsDBNull(dr.Item("datEISDeadline")) Then

                Else
                    dtpDeadlineEIS.Text = dr.Item("datEISDeadline")
                    dtpDeadlineEIS.Checked = False
                End If
                If IsDBNull(dr.Item("strEISDeadlineComment")) Then

                Else
                    txtAllEISDeadlineComment.Text = dr.Item("strEISDeadlineComment")
                End If

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub LoadQASpecificData()
        Try
            dtpQAStarted.Value = Today
            dtpQAPassed.Value = Today
            dtpQAPassed.Checked = False
            cboEISQAStatus.Text = ""
            cboEISQAStaff.Text = ""
            dtpQAStatus.Value = Today
            dtpQACompleted.Value = Today
            dtpQACompleted.Checked = False
            txtQAComments.Clear()
            txtFITrackingNumber.Text = ""
            txtAllFITrackingNumbers.Clear()
            txtPointTrackingNumber.Text = ""
            txtAllPointTrackingNumbers.Clear()
            chbFIErrors.Checked = False
            chbPointErrors.Checked = False
            dtpEISDeadline.Value = Today
            dtpEISDeadline.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            Dim SQL As String = "Select * " &
            "from EIS_QAAdmin " &
            "where inventoryYear = @inventoryYear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryYear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("datDateQAStart")) Then
                    dtpQAStarted.Value = Today
                Else
                    dtpQAStarted.Text = dr.Item("datDateQAStart")
                End If
                If IsDBNull(dr.Item("datDateQAPass")) Then
                    dtpQAPassed.Value = Today
                    dtpQAPassed.Checked = False
                Else
                    dtpQAPassed.Text = dr.Item("datDateQAPass")
                    dtpQAPassed.Checked = True
                End If
                If IsDBNull(dr.Item("QAStatusCode")) Then
                    cboEISQAStatus.Text = ""
                Else
                    cboEISQAStatus.SelectedValue = dr.Item("QAStatusCode")
                End If
                If IsDBNull(dr.Item("datQAStatus")) Then
                    dtpQAStatus.Value = Today
                Else
                    dtpQAStatus.Text = dr.Item("datQAStatus")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    cboEISQAStaff.Text = ""
                Else
                    cboEISQAStaff.Text = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("datQAComplete")) Then
                    dtpQACompleted.Value = Today
                    dtpQACompleted.Checked = False
                Else
                    dtpQACompleted.Text = dr.Item("datQAComplete")
                    dtpQACompleted.Checked = True
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtQAComments.Clear()
                    txtAllQAComments.Clear()
                Else
                    txtAllQAComments.Clear()
                    txtAllQAComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("strFITrackingNumber")) Then
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Clear()
                Else
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Text = dr.Item("strFITrackingNumber")
                End If
                If IsDBNull(dr.Item("strPointTrackingNumber")) Then
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Clear()
                Else
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Text = dr.Item("strPointTrackingNumber")
                End If
                If IsDBNull(dr.Item("strFIError")) Then
                    chbFIErrors.Checked = False
                Else
                    If dr.Item("strFIError") = "True" Then
                        chbFIErrors.Checked = True
                    Else
                        chbFIErrors.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strPointError")) Then
                    chbPointErrors.Checked = False
                Else
                    If dr.Item("strpointError") = "True" Then
                        chbPointErrors.Checked = True
                    Else
                        chbPointErrors.Checked = False
                    End If
                End If
            End If

            If cboEILogStatusCode.SelectedText <> "" AndAlso cboEILogStatusCode.SelectedValue >= 4 Then
                pnlQAProcess.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISStats_Click(sender As Object, e As EventArgs) Handles btnViewEISStats.Click
        ViewEISStats()
        TCEISStats.SelectTab(TPEISStatSummary)
    End Sub

    Private Sub ViewEISStats()
        Try

            If cboEISStatisticsYear.Text = "" Then
                MessageBox.Show("Please select a valid year first.")
                Return
            End If

            txtSelectedEISStatYear.Text = cboEISStatisticsYear.Text
            txtSelectedEISMailout.Text = cboEISStatisticsYear.Text
            txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

            Dim query As String = "SELECT * FROM (SELECT COUNT(*) AS EISUniverse FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear) AS t1, 
                (SELECT COUNT(*) AS EISMailout FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1') AS t2, 
                (SELECT COUNT(*) AS EISEnrollment FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1') AS t3, 
                (SELECT COUNT(*) AS EISUNEnrollment FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '0') AS t4, 
                (SELECT COUNT(*) AS EISNoActivity FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strOptOut IS NULL AND strEnrollment = '1') AS t5, 
                (SELECT COUNT(*) AS EISOptsIn FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strOptOut = '0' AND strEnrollment = '1') AS t6, 
                (SELECT COUNT(*) AS EISOptsOut FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND strOptOut = '1' AND strEnrollment = '1') AS t7, 
                (SELECT COUNT(*) AS EISSubmittal FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= '3' AND strOptOut = '0') AS t8, 
                (SELECT COUNT(*) AS EISInProgress FROM EIS_Admin WHERE active = '1' AND inventoryYear = @inventoryyear AND strEnrollment = '1' AND eisStatuscode = '2' AND strEnrollment = '1' AND strOptOut = '0') AS t9, 
                (SELECT COUNT(*) AS EISQABegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND EISAccesscode = '2' AND eisstatuscode = '4' AND strOptOut = '0') AS t10, 
                (SELECT COUNT(*) AS EISEPASubmitted FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND EISAccesscode = '0' AND eisstatuscode = '5' AND strOptOut = '0') AS t11, 
                (SELECT COUNT(*) AS EISFinalized FROM EIS_Admin WHERE active = '1' AND inventoryYear = @inventoryyear AND strEnrollment = '1' AND (EISStatusCode = '3' OR EISStatusCode = '4' OR EISStatusCode = '5') ) AS t12, 
                (SELECT COUNT(*) AS QASubmittedToDo FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= 3 AND strOptOut = '0' AND NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) ) AS t13, 
                (SELECT COUNT(*) AS QAOptOutToDo FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND (eisstatuscode = 3 OR eisstatuscode = 4) AND (strOptOut = '1' OR strOptout IS NULL) AND NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) ) AS t14, 
                (SELECT COUNT(*) AS QASubmittedBegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= 3 AND strOptOut = '0' AND EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) ) AS t15, 
                (SELECT COUNT(*) AS QAOptOutBegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND (eisstatuscode = '3' OR eisstatuscode = '4') AND (strOptOut = '1' OR strOptout IS NULL) AND (NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) OR EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) ) ) AS t16, 
                (SELECT COUNT(*) AS QASubmittedToEPA FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= '3' AND strOptOut = '0' AND EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NOT NULL) ) AS t17, 
                (SELECT COUNT(*) AS QAOptOutToEPA FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode = '5' AND (strOptOut = '1' OR strOptout IS NULL) AND (NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) OR EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NOT NULL) ) ) AS t18, 
                (SELECT COUNT(*) AS FIPassed FROM EIS_Admin, EIS_QAAdmin WHERE EIS_Admin.InventoryYear = EIS_QAAdmin.inventoryYEar AND EIS_Admin.facilitysiteID = EIS_QAAdmin.facilitysiteID AND eis_qaAdmin.qaStatusCode = '2' AND eis_admin.inventoryyear = @inventoryyear) AS t19"

            Dim param As New SqlParameter("@inventoryyear", cboEISStatisticsYear.Text)

            Dim dr As DataRow = DB.GetDataRow(query, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("EISUniverse")) Then
                    txtEISActiveEIUniverse.Clear()
                Else
                    txtEISActiveEIUniverse.Text = dr.Item("EISUniverse")
                End If
                If IsDBNull(dr.Item("EISMailout")) Then
                    txtEISMailout.Clear()
                Else
                    txtEISMailout.Text = dr.Item("EISMailout")
                End If
                If IsDBNull(dr.Item("EISEnrollment")) Then
                    txtEISEnrolled.Clear()
                Else
                    txtEISEnrolled.Text = dr.Item("EISEnrollment")
                End If
                If IsDBNull(dr.Item("EISUnenrollment")) Then
                    txtEISUnenrolled.Clear()
                Else
                    txtEISUnenrolled.Text = dr.Item("EISUnenrollment")
                End If
                If IsDBNull(dr.Item("EISNoActivity")) Then
                    txtEISNoActivity.Clear()
                Else
                    txtEISNoActivity.Text = dr.Item("EISNoActivity")
                End If
                If IsDBNull(dr.Item("EISOptsIn")) Then
                    txtEISOptedIn.Clear()
                Else
                    txtEISOptedIn.Text = dr.Item("EISOptsIn")
                End If
                If IsDBNull(dr.Item("EISOptsOut")) Then
                    txtEISOptedOut.Clear()
                Else
                    txtEISOptedOut.Text = dr.Item("EISOptsOut")
                End If
                If IsDBNull(dr.Item("EISInProgress")) Then
                    txtEISInProgress.Clear()
                Else
                    txtEISInProgress.Text = dr.Item("EISInProgress")
                End If
                If IsDBNull(dr.Item("EISSubmittal")) Then
                    txtEISSubmitted.Clear()
                Else
                    txtEISSubmitted.Text = dr.Item("EISSubmittal")
                End If
                If IsDBNull(dr.Item("EISQABegan")) Then
                    txtEISQABegan.Clear()
                Else
                    txtEISQABegan.Text = dr.Item("EISQABegan")
                End If

                If IsDBNull(dr.Item("EISFinalized")) Then
                    txtEISFinalized.Clear()
                Else
                    txtEISFinalized.Text = dr.Item("EISFinalized")
                End If

                If IsDBNull(dr.Item("QASubmittedToDo")) Then
                    txtEISSubmittedToDo.Clear()
                Else
                    txtEISSubmittedToDo.Text = dr.Item("QASubmittedToDO")
                End If
                If IsDBNull(dr.Item("QASubmittedBegan")) Then
                    txtEISSubmittedBegan.Clear()
                Else
                    txtEISSubmittedBegan.Text = dr.Item("QASubmittedBegan")
                End If
                If IsDBNull(dr.Item("QASubmittedToEPA")) Then
                    txtEISSubmittedToEPA.Clear()
                Else
                    txtEISSubmittedToEPA.Text = dr.Item("QASubmittedToEPA")
                End If

                If IsDBNull(dr.Item("QAOptOutToDo")) Then
                    txtEISOpOutToDo.Clear()
                Else
                    txtEISOpOutToDo.Text = dr.Item("QAOptOutToDo")
                End If
                If IsDBNull(dr.Item("QAOptOutBegan")) Then
                    txtEISOpOutBegan.Clear()
                Else
                    txtEISOpOutBegan.Text = dr.Item("QAOptOutBegan")
                End If
                If IsDBNull(dr.Item("QAOptOutToEPA")) Then
                    txtEISOpOutToEPA.Clear()
                Else
                    txtEISOpOutToEPA.Text = dr.Item("QAOptOutToEPA")
                End If
                If IsDBNull(dr.Item("FIPassed")) Then
                    txtEISFIPassed.Clear()
                Else
                    txtEISFIPassed.Text = dr.Item("FIPassed")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEIUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISEIUniverse.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "", "1", "", "", "", "")

            lblEISCount.Text = "Active EIS Universe Count: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISMailOutTotal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISMailOutTotal.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "", "1", "", "", "", "")

            lblEISCount.Text = "Mailout Total Count: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISEnrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "", "", "", "")

            lblEISCount.Text = "Enrolled Count: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISNoActivity_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISNoActivity.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "Null", "", "", "")

            lblEISCount.Text = "No Activity Count: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISUnenrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISUnenrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "0", "1", "", "", "", "")

            lblEISCount.Text = "Unenrolled Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISInProgress.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode = 2 ", "", "")

            lblEISCount.Text = "In Progress Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISOptedIn.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", "", "", "")

            lblEISCount.Text = "Opted-In Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISOptedOut.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "1", "", "", "")

            lblEISCount.Text = "Opted-Out Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmitted_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISSubmitted.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", "")

            lblEISCount.Text = "Submitted Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISFinalized.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "",
                     " and (EISStatusCode = '3' or EISStatusCode = '4' or EISStatusCode = '5') ", "", "")

            lblEISCount.Text = "Finalized Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISQABegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISQABegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "1", "1", " and (strOptOut is null or strOptout = '0') ", "4", "2", "")

            lblEISCount.Text = "In Progress Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmittedToEPA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and datQAComplete is not null ")

            lblEISCount.Text = "QA Submitted, EPA Submitted Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvEISStats_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvEISStats.MouseUp
        Try
            Dim CurrentTabPage As TabPage = TCEISStats.SelectedTab
            Dim hti As DataGridView.HitTestInfo = dgvEISStats.HitTest(e.X, e.Y)
            Dim i As Integer = 0

            If hti.RowIndex = -1 AndAlso hti.ColumnIndex <> -1 Then
                If dgvEISStats.Columns(hti.ColumnIndex).HeaderText = " " Then
                    If dgvEISStats(0, 0).Value Then
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = True
                        Next
                    End If
                End If
            Else
                If hti.RowIndex <> -1 Then
                    mtbEISLogAIRSNumber.AirsNumber = New Apb.ApbFacilityId(dgvEISStats(1, hti.RowIndex).Value.ToString)
                End If
            End If

            If CurrentTabPage.Name.ToString = "TPEISStatMailout" AndAlso
                (dgvEISStats.RowCount > 0 AndAlso hti.RowIndex <> -1) Then

                dgvEISStats.Enabled = False

                txtEISStatsMailoutFacilityName.Clear()
                txtEISStatsMailoutPrefix.Clear()
                txtEISStatsMailoutFirstName.Clear()
                txtEISStatsMailoutLastName.Clear()
                txtEISStatsMailoutCompanyName.Clear()
                txtEISStatsMailoutAddress1.Clear()
                txtEISStatsMailoutAddress2.Clear()
                txtEISStatsMailoutCity.Clear()
                txtEISStatsMailoutState.Clear()
                txtEISStatsMailoutZipCode.Clear()
                txtEISStatsMailoutEmailAddress.Clear()
                txtEISStatsMailoutComments.Clear()
                txtEISStatsMailoutUpdateUser.Clear()
                txtEISStatsMailoutUpdateDate.Clear()
                txtEISStatsMailoutCreateDate.Clear()

                If IsDBNull(dgvEISStats(1, hti.RowIndex).Value) Then
                    txtEISStatsMailoutAIRSNumber.Clear()
                Else
                    txtEISStatsMailoutAIRSNumber.Text = dgvEISStats(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvEISStats(3, hti.RowIndex).Value) Then
                    txtSelectedEISMailout.Clear()
                Else
                    txtSelectedEISMailout.Text = dgvEISStats(3, hti.RowIndex).Value
                End If

                Dim SQL As String = "Select " &
                "strFacilityName, " &
                "strContactCompanyName, strContactAddress1, " &
                "strContactAddress2, strContactCity, " &
                "strcontactstate, strcontactzipCode, " &
                "strcontactFirstName, strcontactLastName, " &
                "strContactPrefix, strContactEmail, " &
                "stroperationalStatus, strClass, " &
                "strcomment, UpdateUser, " &
                "updateDateTime, CreateDateTime " &
                 "from EIS_Mailout " &
                 "where intInventoryyear = @year " &
                 "and FacilitySiteID = @airs "

                Dim params As SqlParameter() = {
                    New SqlParameter("@year", txtSelectedEISMailout.Text),
                    New SqlParameter("@airs", txtEISStatsMailoutAIRSNumber.Text)
                }

                Dim dr As DataRow = DB.GetDataRow(SQL, params)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEISStatsMailoutFacilityName.Clear()
                    Else
                        txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISStatsMailoutCompanyName.Clear()
                    Else
                        txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISStatsMailoutAddress1.Clear()
                    Else
                        txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactAddress2")) Then
                        txtEISStatsMailoutAddress2.Clear()
                    Else
                        txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISStatsMailoutCity.Clear()
                    Else
                        txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strcontactstate")) Then
                        txtEISStatsMailoutState.Clear()
                    Else
                        txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                    End If
                    If IsDBNull(dr.Item("strcontactzipCode")) Then
                        txtEISStatsMailoutZipCode.Clear()
                    Else
                        txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                    End If
                    If IsDBNull(dr.Item("strcontactFirstName")) Then
                        txtEISStatsMailoutFirstName.Clear()
                    Else
                        txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                    End If
                    If IsDBNull(dr.Item("strcontactLastName")) Then
                        txtEISStatsMailoutLastName.Clear()
                    Else
                        txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISStatsMailoutPrefix.Clear()
                    Else
                        txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISStatsMailoutEmailAddress.Clear()
                    Else
                        txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strcomment")) Then
                        txtEISStatsMailoutComments.Clear()
                    Else
                        txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                    End If
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtEISStatsMailoutUpdateUser.Clear()
                    Else
                        txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("updateDateTime")) Then
                        txtEISStatsMailoutUpdateDate.Clear()
                    Else
                        txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        txtEISStatsMailoutCreateDate.Clear()
                    Else
                        txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                    End If
                End If
            End If
            dgvEISStats.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEISStatMailout_Click(sender As Object, e As EventArgs) Handles btnSaveEISStatMailout.Click
        Try
            If txtSelectedEISMailout.Text <> "" AndAlso txtEISStatsMailoutAIRSNumber.Text <> "" Then
                Dim SQL As String = "UPdate EIS_Mailout set " &
                    "strFacilityName = @strFacilityName, " &
                    "strContactCompanyName = @strContactCompanyName, " &
                    "strContactAddress1 = @strContactAddress1, " &
                    "strContactAddress2 = @strContactAddress2, " &
                    "strContactCity = @strContactCity, " &
                    "strContactState = @strContactState, " &
                    "strContactZipCode = @strContactZipCode, " &
                    "strContactFirstName = @strContactFirstName, " &
                    "strContactLastName = @strContactLastName, " &
                    "strContactPrefix = @strContactPrefix, " &
                    "strContactEmail = @strContactEmail, " &
                    "strComment = @strComment " &
                    "where intInventoryYear = @intInventoryYear " &
                    "and FacilitySiteID = @FacilitySiteID "

                Dim params As SqlParameter() = {
                    New SqlParameter("@strFacilityName", txtEISStatsMailoutFacilityName.Text),
                    New SqlParameter("@strContactCompanyName", txtEISStatsMailoutCompanyName.Text),
                    New SqlParameter("@strContactAddress1", txtEISStatsMailoutAddress1.Text),
                    New SqlParameter("@strContactAddress2", txtEISStatsMailoutAddress2.Text),
                    New SqlParameter("@strContactCity", txtEISStatsMailoutCity.Text),
                    New SqlParameter("@strContactState", txtEISStatsMailoutState.Text),
                    New SqlParameter("@strContactZipCode", txtEISStatsMailoutZipCode.Text),
                    New SqlParameter("@strContactFirstName", txtEISStatsMailoutFirstName.Text),
                    New SqlParameter("@strContactLastName", txtEISStatsMailoutLastName.Text),
                    New SqlParameter("@strContactPrefix", txtEISStatsMailoutPrefix.Text),
                    New SqlParameter("@strContactEmail", txtEISStatsMailoutEmailAddress.Text),
                    New SqlParameter("@strComment", txtEISStatsMailoutComments.Text),
                    New SqlParameter("@intInventoryYear", txtSelectedEISStatYear.Text),
                    New SqlParameter("@FacilitySiteID", txtEISStatsMailoutAIRSNumber.Text)
                }

                DB.RunCommand(SQL, params)

                MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select a valid year from the dropdown and a valid contact from the resulting list." & vbCrLf &
                       "NO DATA UPDATED", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsEnrollment_Click(sender As Object, e As EventArgs) Handles btnEISStatsEnrollment.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                Dim temp As String = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    Dim SQL As String = "Update EIS_Admin set " &
                    "strEnrollment = '1', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = getdate() " &
                    "where inventoryyear = @inventoryyear " &
                    "and strEnrollment = '0' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode in ('0', '1') " &
                    "and strMailout = '1' " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsRemoveEnrollment_Click(sender As Object, e As EventArgs) Handles btnEISStatsRemoveEnrollment.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                Dim temp As String = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    Dim SQL As String = "Update EIS_Admin set " &
                    "strEnrollment = '0', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = getdate() " &
                    "where inventoryyear = @inventoryyear " &
                    "and strEnrollment = '1' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode = '0' " &
                    "and strMailout = '1' " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseOutEIS_Click(sender As Object, e As EventArgs) Handles btnCloseOutEIS.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to close out.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim query As String = "Update EIS_Admin set " &
                " EISAccessCode = '2' " &
                " where inventoryYear = @inventoryYear " &
                " and FacilitySiteID in ({0}) "

                Dim paramNameList As New List(Of String)
                Dim paramList As New List(Of SqlParameter) From {
                    New SqlParameter("@inventoryYear", EISConfirm)
                }

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                    paramNameList.Add(paramName)
                    paramList.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                Next
                Dim inClause As String = String.Join(",", paramNameList)

                If paramNameList.Count > 0 Then
                    DB.RunCommand(String.Format(query, inClause), paramList.ToArray)
                    ViewEISStats()
                    MsgBox(EISConfirm & " Emission Inventory Year closed out.", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("No facilities displayed.")
                End If
            Else
                MsgBox("Year does not match selected EIS year.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISBeginQA_Click(sender As Object, e As EventArgs) Handles btnEISBeginQA.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then

                Dim selection As Boolean = False
                For Each row As DataGridViewRow In dgvEISStats.Rows
                    If row.Cells(0).Value Then selection = True
                Next
                If Not selection Then
                    MsgBox("No facilities selected.")
                    Return
                End If

                Dim queryList As New List(Of String)
                Dim paramsList As New List(Of SqlParameter())

                ' Update EIS_Admin for non-opted-out facilities
                Dim query1 As String = "Update EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '4', " &
                    "datEISstatus = GETDATE(), " &
                    "UpdateUser = @updateuser, " &
                    "updatedatetime = getdate() " &
                    "where strOptOut = '0' " &
                    "and inventoryYear = @inventoryYear " &
                    "and FacilitySiteID in ({0}) "

                Dim paramNameList1 As New List(Of String)
                Dim paramList1 As New List(Of SqlParameter) From {
                    New SqlParameter("@updateuser", CurrentUser.AlphaName),
                    New SqlParameter("@inventoryYear", EISConfirm)
                }

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value AndAlso dgvEISStats(6, i).Value = "No" Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList1.Add(paramName)
                        paramList1.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next

                If paramNameList1.Count > 0 Then
                    queryList.Add(String.Format(query1, String.Join(",", paramNameList1)))
                    paramsList.Add(paramList1.ToArray)
                End If

                ' Update EIS_Admin for opted-out facilities
                Dim query2 As String = "Update EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '5', " &
                    "datEISstatus = getdate(), " &
                    "UpdateUser = @UpdateUser, " &
                    "updatedatetime = getdate() " &
                    "where strOptOut = '1' " &
                    "and inventoryYear = @inventoryYear " &
                    "and FacilitySiteID in ({0}) "

                Dim paramNameList2 As New List(Of String)
                Dim paramList2 As New List(Of SqlParameter) From {
                    New SqlParameter("@updateuser", CurrentUser.AlphaName),
                    New SqlParameter("@inventoryYear", EISConfirm)
                }

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value AndAlso dgvEISStats(6, i).Value = "Yes" Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList2.Add(paramName)
                        paramList2.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next

                If paramNameList2.Count > 0 Then
                    queryList.Add(String.Format(query2, String.Join(",", paramNameList2)))
                    paramsList.Add(paramList2.ToArray)
                End If

                ' Insert EIS_QAAdmin with new facilities
                Dim query3 As String = "INSERT INTO EIS_QAAdmin 
                    (INVENTORYYEAR, FACILITYSITEID, DATDATEQASTART, QASTATUSCODE, DATQASTATUS, STRDMURESPONSIBLESTAFF, ACTIVE, 
                    UPDATEUSER, UPDATEDATETIME, CREATEDATETIME)
                    SELECT @INVENTORYYEAR, @FACILITYSITEID, GETDATE(), '1', GETDATE(), @UPDATEUSER, '1', 
                    @UPDATEUSER, GETDATE(), GETDATE()
                    WHERE NOT EXISTS (SELECT * FROM EIS_QAAdmin
                    WHERE inventoryYear = @INVENTORYYEAR AND FacilitySiteID = @FACILITYSITEID) 
                    AND EXISTS (SELECT * FROM EIS_Admin
                    WHERE inventoryYear = @INVENTORYYEAR AND FacilitySiteID = @FACILITYSITEID AND strOptOut = '0')"

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value Then
                        queryList.Add(query3)
                        paramsList.Add({
                            New SqlParameter("@INVENTORYYEAR", EISConfirm),
                            New SqlParameter("@FACILITYSITEID", dgvEISStats(1, i).Value),
                            New SqlParameter("@UPDATEUSER", CurrentUser.AlphaName)
                        })
                    End If
                Next

                DB.RunCommand(queryList, paramsList)

                Dim spName As String = "dbo.PD_EIS_QASTART"
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value Then
                        Dim param As SqlParameter() = {
                            New SqlParameter("@AIRSNUMBER_IN", dgvEISStats(1, i).Value),
                            New SqlParameter("@INTYEAR_IN", EISConfirm)
                        }
                        DB.SPRunCommand(spName, param)
                    End If
                Next

                ViewEISStats()
                MsgBox(EISConfirm & " QA process begun.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnEILogUpdate_Click(sender As Object, e As EventArgs) Handles btnEILogUpdate.Click
        Try
            If cboEILogAccessCode.SelectedValue.ToString = "" OrElse cboEILogStatusCode.SelectedValue.ToString = "" Then
                MessageBox.Show("Select a valid access code and status code before saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim EISAccess As String = ""
            Dim OptOut As String = Nothing
            Dim EISStatus As String = ""
            Dim Enrollment As String = ""
            Dim Mailout As String = ""
            Dim ActiveStatus As String = ""
            Dim IncorrectlyOptedOut As String = ""

            If rdbEILogMailoutYes.Checked Then
                Mailout = "1"
            Else
                If rdbEILogMailoutNo.Checked Then
                    Mailout = "0"
                Else
                    Mailout = ""
                End If
            End If
            If rdbEILogEnrolledYes.Checked Then
                Enrollment = "1"
            Else
                Enrollment = "0"
            End If
            If rdbEILogOpOutYes.Checked Then
                OptOut = "1"
            ElseIf rdbEILogOpOutNo.Checked Then
                OptOut = "0"
            End If
            If chbOptedOutIncorrectly.Checked Then
                IncorrectlyOptedOut = "1"
            Else
                IncorrectlyOptedOut = "0"
            End If
            EISStatus = cboEILogStatusCode.SelectedValue.ToString
            EISAccess = cboEILogAccessCode.SelectedValue.ToString
            If rdbEILogActiveYes.Checked Then
                ActiveStatus = "1"
            Else
                ActiveStatus = "0"
            End If

            Dim SQL As String = "Select FacilitySiteID from EIS_Admin " &
            "where inventoryyear = @inventoryyear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
            }

            If Not DB.ValueExists(SQL, params) Then
                MsgBox("The facility is not currently in the EIS universe for the selected year." & vbCrLf &
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)

                Return
            End If

            SQL = "Update EIS_Admin set " &
            "EISStatusCode = @EISStatusCode, " &
            "DatEISStatus = @DatEISStatus, " &
            "EISAccessCode = @EISAccessCode, " &
            "strOptOut = @strOptOut, " &
            "strIncorrectOptOut = @strIncorrectOptOut, " &
            "strMailout = @strMailout, " &
            "strEnrollment = @strEnrollment, " &
            "datEnrollment = @datEnrollment, " &
            "strComment = @strComment, " &
            "active = @active, " &
            "updateUser = @updateUser, " &
            "updateDateTime = getdate() " &
            "where inventoryyear = @inventoryyear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params2 As SqlParameter() = {
                New SqlParameter("@EISStatusCode", EISStatus),
                New SqlParameter("@DatEISStatus", dtpEILogStatusDateSubmit.Value),
                New SqlParameter("@EISAccessCode", EISAccess),
                New SqlParameter("@strOptOut", OptOut),
                New SqlParameter("@strIncorrectOptOut", IncorrectlyOptedOut),
                New SqlParameter("@strMailout", Mailout),
                New SqlParameter("@strEnrollment", Enrollment),
                New SqlParameter("@datEnrollment", dtpEILogDateEnrolled.Value),
                New SqlParameter("@strComment", txtEILogComments.Text),
                New SqlParameter("@active", ActiveStatus),
                New SqlParameter("@updateUser", CurrentUser.AlphaName),
                New SqlParameter("@inventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
            }

            DB.RunCommand(SQL, params2)

            If dtpDeadlineEIS.Checked Then
                Dim DeadLineComments As String = ""
                If txtAllEISDeadlineComment.Text.Contains(dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf &
                txtEISDeadlineComment.Text) Then
                Else
                    DeadLineComments = dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf &
                    txtEISDeadlineComment.Text &
                    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

                    SQL = "update EIS_Admin set " &
                    "datEISDeadline = @datEISDeadline,  " &
                    "strEISDeadlineComment = @strEISDeadlineComment  " &
                    "where INventoryyear = @INventoryyear " &
                    "and FacilitySiteID = @FacilitySiteID  "

                    Dim params3 As SqlParameter() = {
                        New SqlParameter("@datEISDeadline", dtpDeadlineEIS.Text),
                        New SqlParameter("@strEISDeadlineComment", DeadLineComments),
                        New SqlParameter("@INventoryyear", cboEILogYear.Text),
                        New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
                    }

                    DB.RunCommand(SQL, params3)
                End If
            End If

            If Not rdbEILogOpOutYes.Checked Then
                Dim QAStart As String = ""
                Dim QAPass As String = ""
                Dim QAStatusCode As String = ""
                Dim QAStatusDate As String = ""
                Dim StaffResponsible As String = ""
                Dim QAComplete As String = ""
                Dim QAComments As String = ""
                Dim FITracking As String = ""
                Dim pointTracking As String = ""
                Dim FIError As String = ""
                Dim pointError As String = ""

                QAStart = dtpQAStarted.Text
                If dtpQAPassed.Checked Then
                    QAPass = dtpQAPassed.Text
                Else
                    QAPass = ""
                End If
                If dtpQACompleted.Checked Then
                    QAComplete = dtpQACompleted.Text
                Else
                    QAComplete = ""
                End If
                QAStatusCode = cboEISQAStatus.SelectedValue
                QAStatusDate = TodayFormatted
                StaffResponsible = cboEISQAStaff.Text
                If txtQAComments.Text = "" Then
                    If txtAllQAComments.Text = "" Then
                        QAComments = ""
                    Else
                        QAComments = txtAllQAComments.Text
                    End If
                Else
                    If txtAllQAComments.Text = "" Then
                        QAComments = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtQAComments.Text
                    Else
                        QAComments = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
                             txtAllQAComments.Text
                    End If
                End If
                If txtFITrackingNumber.Text = "" Then
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = ""
                    Else
                        FITracking = txtAllFITrackingNumbers.Text
                    End If
                Else
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtFITrackingNumber.Text
                    Else
                        FITracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
                                    txtAllFITrackingNumbers.Text
                    End If
                End If
                If chbFIErrors.Checked Then
                    FIError = "True"
                Else
                    FIError = "False"
                End If
                If txtPointTrackingNumber.Text = "" Then
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = ""
                    Else
                        pointTracking = txtAllPointTrackingNumbers.Text
                    End If
                Else
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtPointTrackingNumber.Text
                    Else
                        pointTracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                                txtAllPointTrackingNumbers.Text
                    End If
                End If
                If chbPointErrors.Checked Then
                    pointError = "True"
                Else
                    pointError = "False"
                End If

                SQL = "Update eis_QAAdmin set " &
               "datDateQAStart = @datDateQAStart, " &
               "datDateQAPass = @datDateQAPass, " &
               "QAStatusCode = @QAStatusCode, " &
               "datQAStatus = @datQAStatus, " &
               "strDMUResponsibleStaff = @strDMUResponsibleStaff, " &
               "datQAComplete = @datQAComplete, " &
               "strComment = @strComment, " &
               "active = '1', " &
               "updateuser = @updateuser, " &
               "updateDateTime = getdate(), " &
               "strFITrackingnumber = @strFITrackingnumber, " &
               "strFIError = @strFIError, " &
               "STRPOINTTRACKINGNUMBER = @STRPOINTTRACKINGNUMBER, " &
               "strpointerror = @strpointerror " &
               "where INventoryyear = @INventoryyear " &
               "and FacilitySiteID = @FacilitySiteID "

                Dim params4 As SqlParameter() = {
                    New SqlParameter("@datDateQAStart", If(QAStart = "", SqlString.Null, QAStart)),
                    New SqlParameter("@datDateQAPass", If(QAPass = "", SqlString.Null, QAPass)),
                    New SqlParameter("@QAStatusCode", If(QAStatusCode = "", SqlString.Null, QAStatusCode)),
                    New SqlParameter("@datQAStatus", If(QAStatusDate = "", SqlString.Null, QAStatusDate)),
                    New SqlParameter("@strDMUResponsibleStaff", StaffResponsible),
                    New SqlParameter("@datQAComplete", If(QAComplete = "", SqlString.Null, QAComplete)),
                    New SqlParameter("@strComment", QAComments),
                    New SqlParameter("@updateuser", CurrentUser.AlphaName),
                    New SqlParameter("@strFITrackingnumber", If(FITracking = "", SqlString.Null, FITracking)),
                    New SqlParameter("@strFIError", If(FIError = "", SqlString.Null, FIError)),
                    New SqlParameter("@STRPOINTTRACKINGNUMBER", pointTracking),
                    New SqlParameter("@strpointerror", If(pointError = "", SqlString.Null, pointError)),
                    New SqlParameter("@INventoryyear", cboEILogYear.Text),
                    New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
                }

                DB.RunCommand(SQL, params4)

                LoadQASpecificData()
            End If

            LoadAdminData()
            MsgBox("Admin Data updated.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEILogAddNewFacility_Click(sender As Object, e As EventArgs) Handles btnEILogAddNewFacility.Click
        Try
            Dim spname As String = "dbo.PD_EIS_Data"
            Dim params As SqlParameter() = {
                New SqlParameter("@AIRSNUM", txtEILogSelectedAIRSNumber.AirsNumber.ShortString),
                New SqlParameter("@INTYEAR", txtEILogSelectedYear.Text)
            }
            DB.SPRunCommand(spname, params)

            LoadAdminData()
            MsgBox("New Facility Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateQAData_Click(sender As Object, e As EventArgs) Handles btnUpdateQAData.Click
        Try
            Dim QAStart As String = ""
            Dim QAPass As String = ""
            Dim QAStatusCode As String = ""
            Dim QAStatusDate As String = ""
            Dim StaffResponsible As String = ""
            Dim QAComplete As String = ""
            Dim QAComments As String = ""
            Dim FITracking As String = ""
            Dim PointTracking As String = ""
            Dim FIError As String = ""
            Dim PointError As String = ""

            QAStart = dtpQAStarted.Text
            If dtpQAPassed.Checked Then
                QAPass = dtpQAPassed.Text
            Else
                QAPass = ""
            End If
            If dtpQACompleted.Checked Then
                QAComplete = dtpQACompleted.Text
            Else
                QAComplete = ""
            End If
            QAStatusCode = cboEISQAStatus.SelectedValue
            QAStatusDate = Format(Today, DateFormat)
            StaffResponsible = cboEISQAStaff.Text
            If txtQAComments.Text = "" Then
                If txtAllQAComments.Text = "" Then
                    QAComments = ""
                Else
                    QAComments = txtAllQAComments.Text
                End If
            Else
                If txtAllQAComments.Text = "" Then
                    QAComments = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtQAComments.Text
                Else
                    QAComments = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
                         txtAllQAComments.Text
                End If
            End If
            If txtFITrackingNumber.Text = "" Then
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = ""
                Else
                    FITracking = txtAllFITrackingNumbers.Text
                End If
            Else
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtFITrackingNumber.Text
                Else
                    FITracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
                                txtAllFITrackingNumbers.Text
                End If
            End If
            If chbFIErrors.Checked Then
                FIError = "True"
            Else
                FIError = "False"
            End If

            If txtPointTrackingNumber.Text = "" Then
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = ""
                Else
                    PointTracking = txtAllPointTrackingNumbers.Text
                End If
            Else
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtPointTrackingNumber.Text
                Else
                    PointTracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                            txtAllPointTrackingNumbers.Text
                End If
            End If
            If chbPointErrors.Checked Then
                PointError = "True"
            Else
                PointError = "False"
            End If

            Dim SQL As String = "Update eis_QAAdmin set " &
            "datDateQAStart = @datDateQAStart, " &
            "datDateQAPass = @datDateQAPass, " &
            "QAStatusCode = @QAStatusCode, " &
            "datQAStatus = @datQAStatus, " &
            "strDMUResponsibleStaff = @strDMUResponsibleStaff, " &
            "datQAComplete = @datQAComplete, " &
            "strComment = @strComment, " &
            "active = '1', " &
            "updateuser = @updateuser, " &
            "updateDateTime = getdate(), " &
            "strFITrackingnumber = @strFITrackingnumber, " &
            "strFIError = @strFIError, " &
            "STRPOINTTRACKINGNUMBER = @STRPOINTTRACKINGNUMBER, " &
            "strpointerror = @strpointerror " &
            "where INventoryyear = @INventoryyear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@datDateQAStart", If(QAStart = "", SqlString.Null, QAStart)),
                New SqlParameter("@datDateQAPass", If(QAPass = "", SqlString.Null, QAPass)),
                New SqlParameter("@QAStatusCode", If(QAStatusCode = "", SqlString.Null, QAStatusCode)),
                New SqlParameter("@datQAStatus", If(QAStatusDate = "", SqlString.Null, QAStatusDate)),
                New SqlParameter("@strDMUResponsibleStaff", StaffResponsible),
                New SqlParameter("@datQAComplete", If(QAComplete = "", SqlString.Null, QAComplete)),
                New SqlParameter("@strComment", QAComments),
                New SqlParameter("@updateuser", CurrentUser.AlphaName),
                New SqlParameter("@strFITrackingnumber", If(FITracking = "", SqlString.Null, FITracking)),
                New SqlParameter("@strFIError", If(FIError = "", SqlString.Null, FIError)),
                New SqlParameter("@STRPOINTTRACKINGNUMBER", PointTracking),
                New SqlParameter("@strpointerror", If(PointError = "", SqlString.Null, PointError)),
                New SqlParameter("@INventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.AirsNumber.ShortString)
            }

            DB.RunCommand(SQL, params)

            LoadQASpecificData()

            If dtpQACompleted.Checked Then
                Dim spname As String = "dbo.PD_EIS_QA_Done"
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@AIRSNUM", txtEILogSelectedAIRSNumber.AirsNumber.ShortString),
                    New SqlParameter("@INTYEAR", txtEILogSelectedYear.Text),
                    New SqlParameter("@DATLASTSUBMIT", dtpQACompleted.Value)
                }
                DB.SPRunCommand(spname, params2)
            End If

            MsgBox("QA data saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEIModifyUpdateLocation_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateLocation.Click

        If Not txtEILogSelectedAIRSNumber.IsValid Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If

        Dim Address As String = txtEIModifyLocation.Text
        Dim City As String = txtEIModifyCity.Text
        Dim PostalCode As String = mtbEIModifyZipCode.Text

        If Address <> "" AndAlso City <> "" Then
            Dim query As String = "Update EIS_FacilitySiteAddress set " &
            " STRLOCATIONADDRESSTEXT = @Address, " &
            " STRLOCALITYNAME = @City, " &
            " STRLOCATIONADDRESSPOSTALCODE = @PostalCode " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@Address", Address),
                New SqlParameter("@City", City),
                New SqlParameter("@PostalCode", PostalCode),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH LOCATION ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateMailing_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateMailing.Click

        If Not txtEILogSelectedAIRSNumber.IsValid Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If

        Dim Address As String = txtEIModifyMLocation.Text
        Dim City As String = txtEIModifyMCity.Text
        Dim PostalCode As String = mtbEIModifyMZipCode.Text

        If Address <> "" AndAlso City <> "" Then
            Dim query As String = "Update EIS_FacilitySiteAddress set " &
            " strMailingAddressText = @Address, " &
            " strMailingAddresscityname = @City, " &
            " strMailingAddressPostalCode = @PostalCode " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@Address", Address),
                New SqlParameter("@City", City),
                New SqlParameter("@PostalCode", PostalCode),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH MAILING ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateName_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateName.Click
        If Not txtEILogSelectedAIRSNumber.IsValid Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If

        If txtEIModifyFacilityName.Text <> "" Then
            txtEIModifyFacilityName.Text = Facility.SanitizeFacilityNameForDb(txtEIModifyFacilityName.Text)
        End If

        Dim FacilityName As String = txtEIModifyFacilityName.Text

        If FacilityName <> "" Then
            Dim query As String = "Update EIS_FacilitySite set " &
            " strFacilitySiteName = @FacilityName " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@FacilityName", FacilityName),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Sub UpdateFacilityGEOCoord()
        Try
            If Not txtEILogSelectedAIRSNumber.IsValid Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If mtbEIModifyLatitude.Text <> "" AndAlso mtbEIModifyLongitude.Text <> "" Then
                Dim SQL As String = "Update EIS_FacilityGEOCoord set " &
                "numLatitudeMeasure = @numLatitudeMeasure, " &
                "numLongitudeMeasure = @numLongitudeMeasure " &
                "where facilitySiteID = @facilitySiteID "

                Dim params As SqlParameter() = {
                    New SqlParameter("@numLatitudeMeasure", CDec(mtbEIModifyLatitude.Text)),
                    New SqlParameter("@numLongitudeMeasure", -CDec(mtbEIModifyLongitude.Text)),
                    New SqlParameter("@facilitySiteID", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
                }

                DB.RunCommand(SQL, params)

                SQL = "Update APBFacilityInformation set " &
                    "numFacilityLongitude = @numFacilityLongitude, " &
                    "numFacilityLatitude = @numFacilityLatitude, " &
                    "strComments = @strComments, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingDate = getdate() " &
                    "where strAIRSNumber = @strAIRSNumber "

                Dim params2 As SqlParameter() = {
                    New SqlParameter("@numFacilityLongitude", -CDec(mtbEIModifyLongitude.Text)),
                    New SqlParameter("@numFacilityLatitude", mtbEIModifyLatitude.Text),
                    New SqlParameter("@strComments", "Updated by " & CurrentUser.AlphaName & " through DMU Staff Tools - Emissions Inventory Log. "),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@strAIRSNumber", txtEILogSelectedAIRSNumber.AirsNumber.DbFormattedString)
                }

                DB.RunCommand(SQL, params2)

                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Latitude & Longitude data not saved." & vbCrLf & "Add both values to update.",
                         MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateLatLong_Click(sender As Object, e As EventArgs) Handles btnUpdateLatLong.Click
        UpdateFacilityGEOCoord()
    End Sub

    Private Sub btnUpdateEisOperStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateEisOperStatus.Click
        If Not txtEILogSelectedAIRSNumber.IsValid Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim query As String = "UPDATE EIS_FACILITYSITE " &
                " SET STRFACILITYSITESTATUSCODE = @statuscode " &
                " , STRFACILITYSITECOMMENT = @sitecomment " &
                " , UPDATEUSER = @updateuser " &
                " , UPDATEDATETIME = GETDATE() " &
                " WHERE FACILITYSITEID = @siteid "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@statuscode", cbEisModifyOperStatus.SelectedValue.ToString),
                New SqlParameter("@sitecomment", "Site status updated from IAIP"),
                New SqlParameter("@updateuser", CurrentUser.UserID & "-" & CurrentUser.AlphaName),
                New SqlParameter("@siteid", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
            }

            If DB.RunCommand(query, parameters) Then
                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("There was an error updating the data.", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub

    Private Sub btnEIModifyCopy_Click(sender As Object, e As EventArgs) Handles btnEIModifyCopy.Click
        txtEIModifyFacilityName.Text = txtEIModifyIAIPFacilityName.Text
        txtEIModifyLocation.Text = txtEIModifyIAIPLocation.Text
        txtEIModifyCity.Text = txtEIModifyIAIPCity.Text
        mtbEIModifyMZipCode.Text = mtbEIModifyIAIPZipCode.Text
        txtEIModifyMLocation.Text = txtEIModifyIAIPLocation.Text
        txtEIModifyMCity.Text = txtEIModifyIAIPCity.Text
        mtbEIModifyZipCode.Text = mtbEIModifyIAIPZipCode.Text
        mtbEIModifyLatitude.Text = mtbEIModifyIAIPLatitude.Text
        mtbEIModifyLongitude.Text = mtbEIModifyIAIPLongitude.Text
        Select Case CType(cbIaipOperStatus.SelectedValue, FacilityOperationalStatus)
            Case FacilityOperationalStatus.C, FacilityOperationalStatus.I, FacilityOperationalStatus.P, FacilityOperationalStatus.U
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.UNK
            Case FacilityOperationalStatus.O
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.OP
            Case FacilityOperationalStatus.T
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.TS
            Case FacilityOperationalStatus.X
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.PS
        End Select
    End Sub

    Private Sub btnEISMailoutUpdate_Click(sender As Object, e As EventArgs) Handles btnEISMailoutUpdate.Click
        Try

            If Not txtEILogSelectedAIRSNumber.IsValid Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Update EIS_Mailout Set " &
            "strFacilityName= @strFacilityName, " &
            "strContactCompanyName = @strContactCompanyName, " &
            "strContactAddress1 = @strContactAddress1, " &
            "strContactAddress2 = @strContactAddress2, " &
            "strContactCity = @strContactCity, " &
            "strContactState = @strContactState, " &
            "strContactZipCode = @strContactZipCode, " &
            "strContactFirstName = @strContactFirstName, " &
            "strContactLastName = @strContactLastName, " &
            "strContactPrefix = @strContactPrefix, " &
            "strContactEmail = @strContactEmail, " &
            "strComment = @strComment " &
            "where FacilitySiteid = @FacilitySiteid " &
            "and intInventoryYear = @intInventoryYear "

            Dim params As SqlParameter() = {
                New SqlParameter("@strFacilityName", txtEISMailoutEditFacilityName.Text),
                New SqlParameter("@strContactCompanyName", txtEISMailoutEditCompanyName.Text),
                New SqlParameter("@strContactAddress1", txtEISMailoutEditAdress.Text),
                New SqlParameter("@strContactAddress2", txtEISMailoutEditAddress2.Text),
                New SqlParameter("@strContactCity", txtEISMailoutEditCity.Text),
                New SqlParameter("@strContactState", txtEISMailoutEditState.Text),
                New SqlParameter("@strContactZipCode", txtEISMailoutEditZipCode.Text),
                New SqlParameter("@strContactFirstName", txtEISMailoutEditFirstName.Text),
                New SqlParameter("@strContactLastName", txtEISMailoutEditLastName.Text),
                New SqlParameter("@strContactPrefix", txtEISMailoutEditPrefix.Text),
                New SqlParameter("@strContactEmail", txtEISMailoutEditEmailAddress.Text),
                New SqlParameter("@strComment", txtEISMailoutEditComments.Text),
                New SqlParameter("@FacilitySiteid", txtEILogSelectedAIRSNumber.AirsNumber.ShortString),
                New SqlParameter("@intInventoryYear", txtEILogSelectedYear.Text)
            }

            DB.RunCommand(SQL, params)

            MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedToDo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            lblEISCount.Text = "QA Submitted, To-Do Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                      " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            lblEISCount.Text = "QA Submitted, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwFIErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwFIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
             " and EISStatusCode >= 3 ", "",
             " and datQAComplete is null and strFIError = 'True' and (strPointError = 'False' or strPointError is null) ")

            lblEISCount.Text = "QA Submitted, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwithEIErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithEIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
             " and EISStatusCode >= 3 ", "",
             " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and strPointError = 'True'  ")

            lblEISCount.Text = "QA Submitted, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithBothErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithBothErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
          " and EISStatusCode >= 3 ", "",
          " and datQAComplete is null and (strFIError = 'True' ) and (strPointError = 'True' ) ")

            lblEISCount.Text = "QA Submitted, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithoutErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithoutErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
          " and EISStatusCode >= 3 ", "",
          " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and " &
          "(strPointError = 'False' or strPointError is null) " &
          "and QAStatusCode is not null")

            lblEISCount.Text = "QA Submitted, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutToDo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or stroptout is null )",
             " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            lblEISCount.Text = "QA Opted-Out, To-do Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutBegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null) ",
                     " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            lblEISCount.Text = "QA Opted-Out, Started Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutSubmittedToEPA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null )  ",
               " and EISStatusCode >= 5 ", "", " and datQAComplete is not null ")

            lblEISCount.Text = "QA Submitted, EPA Submitted Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForFacility_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSearchForFacility.LinkClicked
        Try
            If cboEISStatisticsYear.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Select " &
                  "strFacilityName, " &
                  "strContactCompanyName, strContactAddress1, " &
                  "strContactAddress2, strContactCity, " &
                  "strcontactstate, strcontactzipCode, " &
                  "strcontactFirstName, strcontactLastName, " &
                  "strContactPrefix, strContactEmail, " &
                  "stroperationalStatus, strClass, " &
                  "strcomment, UpdateUser, " &
                  "updateDateTime, CreateDateTime " &
                   "from EIS_Mailout " &
                   "where intInventoryyear = @intInventoryyear " &
                   "and FacilitySiteID = @FacilitySiteID "
            Dim params As SqlParameter() = {
                New SqlParameter("@intInventoryyear", cboEISStatisticsYear.Text),
                New SqlParameter("@FacilitySiteID", txtEISStatsMailoutAIRSNumber.Text)
            }
            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISStatsMailoutFacilityName.Clear()
                Else
                    txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISStatsMailoutCompanyName.Clear()
                Else
                    txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISStatsMailoutAddress1.Clear()
                Else
                    txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISStatsMailoutAddress2.Clear()
                Else
                    txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISStatsMailoutCity.Clear()
                Else
                    txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strcontactstate")) Then
                    txtEISStatsMailoutState.Clear()
                Else
                    txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                End If
                If IsDBNull(dr.Item("strcontactzipCode")) Then
                    txtEISStatsMailoutZipCode.Clear()
                Else
                    txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                End If
                If IsDBNull(dr.Item("strcontactFirstName")) Then
                    txtEISStatsMailoutFirstName.Clear()
                Else
                    txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                End If
                If IsDBNull(dr.Item("strcontactLastName")) Then
                    txtEISStatsMailoutLastName.Clear()
                Else
                    txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISStatsMailoutPrefix.Clear()
                Else
                    txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISStatsMailoutEmailAddress.Clear()
                Else
                    txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strcomment")) Then
                    txtEISStatsMailoutComments.Clear()
                Else
                    txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISStatsMailoutUpdateUser.Clear()
                Else
                    txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDateTime")) Then
                    txtEISStatsMailoutUpdateDate.Clear()
                Else
                    txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISStatsMailoutCreateDate.Clear()
                Else
                    txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                End If
            End If

            If txtEISStatsMailoutFacilityName.Text = "" Then
                SQL = "SELECT * " &
                    "FROM " &
                    "  (SELECT dt_EIContact.STRAIRSNUMBER, fi.STRFACILITYNAME, " &
                    "    hd.STROPERATIONALSTATUS, hd.STRCLASS,( " &
                    "    CASE                                WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTLASTNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTLASTNAME ELSE '' " &
                    "    END) STRContactLastName,( " &
                    "    CASE                                 WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTFIRSTNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTFIRSTNAME ELSE '' " &
                    "    END) STRContactfirstName,( " &
                    "    CASE                                   WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTCOMPANYNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTCOMPANYNAME " &
                    "    END) STRContactCompanyName,( " &
                    "    CASE                             WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTEMAIL WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTEMAIL " &
                    "    END) STRContactEmail,( " &
                    "    CASE                              WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTPREFIX WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTPREFIX " &
                    "    END) strCONTACTPREFIX,( " &
                    "    CASE                                WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTADDRESS1 WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTADDRESS1 " &
                    "    END) STRCONTACTADDRESS1,( " &
                    "    CASE                            WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTCITY WHEN dt_EIContact.STRKEY IS " &
                    "        NULL THEN dt_PermitContact.STRCONTACTCITY " &
                    "    END) STRCONTACTCITY,( " &
                    "    CASE                             WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTSTATE WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTSTATE " &
                    "    END) STRCONTACTSTATE,( " &
                    "    CASE                               WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTZIPCODE WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTZIPCODE " &
                    "    END) STRCONTACTZIPCODE " &
                    "  FROM " &
                    "    (SELECT DISTINCT dt_EIList.STRAIRSNUMBER, dt_Contact.STRKEY " &
                    "      , dt_Contact.STRCONTACTLASTNAME, " &
                    "      dt_Contact.STRCONTACTFIRSTNAME, " &
                    "      dt_Contact.STRCONTACTCOMPANYNAME, " &
                    "      dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, " &
                    "      dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, " &
                    "      dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                    "    FROM " &
                    "      (SELECT * " &
                    "      FROM APBHEADERDATA hd " &
                    "      WHERE(hd.STROPERATIONALSTATUS = 'O' OR " &
                    "        hd.STROPERATIONALSTATUS = 'P' OR " &
                    "        hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' " &
                    "      ) dt_EIList " &
                    "    LEFT JOIN " &
                    "      (SELECT * FROM APBCONTACTINFORMATION ci WHERE ci.STRKEY = " &
                    "        41 " &
                    "      ) dt_Contact ON dt_EIList.STRAIRSNUMBER = " &
                    "      dt_Contact.STRAIRSNUMBER " &
                    "    ) dt_EIContact " &
                    "  LEFT JOIN " &
                    "    (SELECT DISTINCT dt_EIList.STRAIRSNUMBER, dt_Contact.STRKEY " &
                    "      , dt_Contact.STRCONTACTLASTNAME, " &
                    "      dt_Contact.STRCONTACTFIRSTNAME, " &
                    "      dt_Contact.STRCONTACTCOMPANYNAME, " &
                    "      dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, " &
                    "      dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, " &
                    "      dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                    "    FROM " &
                    "      (SELECT * FROM APBHEADERDATA hd WHERE( " &
                    "        hd.STROPERATIONALSTATUS = 'O' OR " &
                    "        hd.STROPERATIONALSTATUS = 'P' OR " &
                    "        hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' " &
                    "      ) dt_EIList " &
                    "    LEFT JOIN " &
                    "      (SELECT * FROM APBCONTACTINFORMATION ci WHERE ci.STRKEY = " &
                    "        30 " &
                    "      ) dt_Contact ON dt_EIList.STRAIRSNUMBER = " &
                    "      dt_Contact.STRAIRSNUMBER " &
                    "    ) dt_PermitContact ON dt_EIContact.STRAIRSNUMBER = " &
                    "    dt_PermitContact.STRAIRSNUMBER " &
                    "  INNER JOIN APBHEADERDATA hd ON dt_EIContact.STRAIRSNUMBER = " &
                    "    hd.STRAIRSNUMBER " &
                    "  INNER JOIN APBFACILITYINFORMATION fi ON " &
                    "    dt_EIContact.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                    "  ) t1 " &
                    " WHERE STRAIRSNUMBER = @STRAIRSNUMBER "
                Dim param As New SqlParameter("@strAIRSnumber", "0413" & txtEISStatsMailoutAIRSNumber.Text)

                Dim dr2 As DataRow = DB.GetDataRow(SQL, param)

                If dr2 IsNot Nothing Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEISStatsMailoutFacilityName.Clear()
                    Else
                        txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISStatsMailoutCompanyName.Clear()
                    Else
                        txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISStatsMailoutAddress1.Clear()
                    Else
                        txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                    End If
                    txtEISStatsMailoutAddress2.Clear()
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISStatsMailoutCity.Clear()
                    Else
                        txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strcontactstate")) Then
                        txtEISStatsMailoutState.Clear()
                    Else
                        txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                    End If
                    If IsDBNull(dr.Item("strcontactzipCode")) Then
                        txtEISStatsMailoutZipCode.Clear()
                    Else
                        txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                    End If
                    If IsDBNull(dr.Item("strcontactFirstName")) Then
                        txtEISStatsMailoutFirstName.Clear()
                    Else
                        txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                    End If
                    If IsDBNull(dr.Item("strcontactLastName")) Then
                        txtEISStatsMailoutLastName.Clear()
                    Else
                        txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISStatsMailoutPrefix.Clear()
                    Else
                        txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISStatsMailoutEmailAddress.Clear()
                    Else
                        txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    txtEISStatsMailoutComments.Clear()
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtEISStatsMailoutUpdateUser.Clear()
                    Else
                        txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("updateDateTime")) Then
                        txtEISStatsMailoutUpdateDate.Clear()
                    Else
                        txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        txtEISStatsMailoutCreateDate.Clear()
                    Else
                        txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                    End If
                End If

                btnAddtoEISMailout.Visible = True

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddtoEISMailout_Click(sender As Object, e As EventArgs) Handles btnAddtoEISMailout.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to add facilities into Mailout.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim temp As String = ""

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value AndAlso dgvEISStats(7, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next

                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    Dim SQL As String = "Update EIS_Admin set " &
                    "strMailOut = '1' " &
                    "where inventoryYear = @inventoryyear " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)

                    MsgBox(EISConfirm & " Emission Inventory Facilities in Mailout.", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Year does not match selected EIS year")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISComplete_Click(sender As Object, e As EventArgs) Handles btnEISComplete.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to mark Facilities as complete.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim query As String = "Update EIS_Admin set " &
                "EISAccessCode = '0', " &
                "EISStatusCode = '5', " &
                "datEISstatus = getdate(), " &
                "UpdateUser = @UpdateUser, " &
                "updatedatetime = getdate() " &
                "where inventoryYear = @inventoryYear " &
                " and FacilitySiteID in ({0}) "

                Dim paramNameList As New List(Of String)
                Dim paramList As New List(Of SqlParameter) From {
                    New SqlParameter("@inventoryYear", EISConfirm),
                    New SqlParameter("@UpdateUser", CurrentUser.AlphaName)
                }

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList.Add(paramName)
                        paramList.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next
                Dim inClause As String = String.Join(",", paramNameList)

                If paramNameList.Count > 0 Then
                    DB.RunCommand(String.Format(query, inClause), paramList.ToArray)
                    MsgBox(EISConfirm & " EIS process completed.", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("No facilities selected.")
                End If
            Else
                MsgBox("Year does not match selected EIS year")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewPollutantThresholds()
        Try
            Dim SQL As String
            If rdbThreeYearPollutants.Checked Then
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from EIThresholds " &
                "where strType = '3YEAR' " &
                "order by strPollutant "
            Else
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from EIThresholds " &
                "where strType = 'ANNUAL' " &
                "order by strPollutant "
            End If

            Dim dt As DataTable = DB.GetDataTable(SQL)

            dgvThresholdPollutants.DataSource = dt

            dgvThresholdPollutants.RowHeadersVisible = False
            dgvThresholdPollutants.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvThresholdPollutants.AllowUserToResizeColumns = True
            dgvThresholdPollutants.AllowUserToAddRows = False
            dgvThresholdPollutants.AllowUserToDeleteRows = False
            dgvThresholdPollutants.AllowUserToOrderColumns = True
            dgvThresholdPollutants.AllowUserToResizeRows = True

            dgvThresholdPollutants.Columns("strPollutant").HeaderText = "Pollutant"
            dgvThresholdPollutants.Columns("strPollutant").DisplayIndex = 0
            dgvThresholdPollutants.Columns("numThreshold").HeaderText = "Threshold"
            dgvThresholdPollutants.Columns("numThreshold").DisplayIndex = 1
            dgvThresholdPollutants.Columns("numThresholdNAA").HeaderText = "Nonattainment Area Threshold"
            dgvThresholdPollutants.Columns("numThresholdNAA").DisplayIndex = 2

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewThresholdPollutants_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewThresholdPollutants.LinkClicked
        ViewPollutantThresholds()
    End Sub

    Private Sub dgvThresholdPollutants_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvThresholdPollutants.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvThresholdPollutants.HitTest(e.X, e.Y)

        Try
            If dgvThresholdPollutants.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
                If IsDBNull(dgvThresholdPollutants(0, hti.RowIndex).Value) Then
                    txtPollutant.Clear()
                Else
                    txtPollutant.Text = dgvThresholdPollutants(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(1, hti.RowIndex).Value) Then
                    txtThreshold.Clear()
                Else
                    txtThreshold.Text = dgvThresholdPollutants(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(2, hti.RowIndex).Value) Then
                    txtNonAttainmentThreshold.Clear()
                Else
                    txtNonAttainmentThreshold.Text = dgvThresholdPollutants(2, hti.RowIndex).Value
                End If
            Else
                txtPollutant.Clear()
                txtThreshold.Clear()
                txtNonAttainmentThreshold.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewPollutant_Click(sender As Object, e As EventArgs) Handles btnAddNewPollutant.Click
        Try
            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Return
            End If
            If txtPollutant.Text = "" Then
                Return
            End If

            Dim SQL As String = "Select * from " &
            "EIThresholds " &
            "where strPollutant = @strPollutant " &
            "and strType = @strType "

            Dim params As SqlParameter() = {
                New SqlParameter("@strPollutant", txtPollutant.Text),
                New SqlParameter("@strType", ThresholdType)
            }

            If DB.ValueExists(SQL, params) Then
                MsgBox("Pollutant currently exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                Dim SQL2 As String = "INSERT INTO EITHRESHOLDS " &
                    " (STRPOLLUTANT, NUMTHRESHOLD, NUMTHRESHOLDNAA, STRTYPE) " &
                    "VALUES " &
                    " (@STRPOLLUTANT, @NUMTHRESHOLD, @NUMTHRESHOLDNAA, @STRTYPE) "
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@STRPOLLUTANT", txtPollutant.Text),
                    New SqlParameter("@NUMTHRESHOLD", txtThreshold.Text),
                    New SqlParameter("@NUMTHRESHOLDNAA", txtNonAttainmentThreshold.Text),
                    New SqlParameter("@STRTYPE", ThresholdType)
                }
                DB.RunCommand(SQL2, params2)

                ViewPollutantThresholds()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePollutant_Click(sender As Object, e As EventArgs) Handles btnUpdatePollutant.Click
        Try
            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Return
            End If
            If txtPollutant.Text = "" Then
                Return
            End If

            Dim SQL As String = "Select * from " &
            "EIThresholds " &
            "where strPollutant = @strPollutant " &
            "and strType = @strType "

            Dim params As SqlParameter() = {
                New SqlParameter("@strPollutant", txtPollutant.Text),
                New SqlParameter("@strType", ThresholdType)
            }

            If DB.ValueExists(SQL, params) Then
                Dim SQL2 As String = "UPDATE EITHRESHOLDS " &
                    "SET NUMTHRESHOLD   = @NUMTHRESHOLD " &
                    ", NUMTHRESHOLDNAA  = @NUMTHRESHOLDNAA " &
                    "WHERE STRPOLLUTANT = @STRPOLLUTANT " &
                    "AND STRTYPE        = @STRTYPE"
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@NUMTHRESHOLD", txtThreshold.Text),
                    New SqlParameter("@NUMTHRESHOLDNAA", txtNonAttainmentThreshold.Text),
                    New SqlParameter("@STRPOLLUTANT", txtPollutant.Text),
                    New SqlParameter("@STRTYPE", ThresholdType)
                }
                DB.RunCommand(SQL2, params2)

                ViewPollutantThresholds()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Pollutant currently does not exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadEISYear()
        Dim SQL As String = "Select " &
        "strYear, " &
        "strEIType, datDeadLine " &
        "from EIThresholdYears " &
        "order by strYear desc "
        Dim dt As DataTable = DB.GetDataTable(SQL)

        dgvEISYear.DataSource = dt

        dgvEISYear.RowHeadersVisible = False
        dgvEISYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvEISYear.AllowUserToResizeColumns = True
        dgvEISYear.AllowUserToAddRows = False
        dgvEISYear.AllowUserToDeleteRows = False
        dgvEISYear.AllowUserToOrderColumns = True
        dgvEISYear.AllowUserToResizeRows = True

        dgvEISYear.Columns("strYear").HeaderText = "EIS Year"
        dgvEISYear.Columns("strYear").DisplayIndex = 0
        dgvEISYear.Columns("strEIType").HeaderText = "Type"
        dgvEISYear.Columns("strEIType").DisplayIndex = 1
        dgvEISYear.Columns("datDeadLine").HeaderText = "EIS Date Deadline"
        dgvEISYear.Columns("datDeadLine").DisplayIndex = 2
        dgvEISYear.Columns("datDeadLine").DefaultCellStyle.Format = "dd-MMM-yyyy"
    End Sub

    Private Sub dgvEISYear_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvEISYear.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvEISYear.HitTest(e.X, e.Y)

        Try
            If dgvEISYear.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
                If IsDBNull(dgvEISYear(0, hti.RowIndex).Value) Then
                    mtbThresholdYear.Clear()
                Else
                    mtbThresholdYear.Text = dgvEISYear(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvEISYear(1, hti.RowIndex).Value) Then
                    rdbEISAnnual.Checked = False
                    rdbEISThreeYear.Checked = False
                Else
                    If dgvEISYear(1, hti.RowIndex).Value = "3YEAR" Then
                        rdbEISAnnual.Checked = False
                        rdbEISThreeYear.Checked = True
                    Else
                        rdbEISAnnual.Checked = True
                        rdbEISThreeYear.Checked = False
                    End If
                End If
                If IsDBNull(dgvEISYear(2, hti.RowIndex).Value) Then
                    dtpEISDeadline.Value = Today
                Else
                    dtpEISDeadline.Text = dgvEISYear(2, hti.RowIndex).Value
                End If
            Else
                mtbThresholdYear.Clear()
                rdbEISAnnual.Checked = False
                rdbEISThreeYear.Checked = False
                dtpEISDeadline.Value = Today
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClearEISYear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbClearEISYear.LinkClicked
        Try

            mtbThresholdYear.Clear()
            rdbEISAnnual.Checked = False
            rdbEISThreeYear.Checked = False
            dtpEISDeadline.Value = Today

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddEISYear_Click(sender As Object, e As EventArgs) Handles btnAddEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If rdbEISThreeYear.Checked Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            Dim SQL As String = "Select " &
            "strYear " &
            "from EIThresholdYears " &
            "where strYEar = @strYEar "

            Dim param As New SqlParameter("@strYEar", mtbThresholdYear.Text)

            If DB.ValueExists(SQL, param) Then
                MsgBox("EIS Year currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "INSERT INTO EITHRESHOLDYEARS " &
                    " (STRYEAR, STREITYPE, DATDEADLINE) " &
                    " VALUES " &
                    " (@STRYEAR, @STREITYPE, @DATDEADLINE) "
                Dim params As SqlParameter() = {
                    New SqlParameter("@STRYEAR", mtbThresholdYear.Text),
                    New SqlParameter("@STREITYPE", EISYearType),
                    New SqlParameter("@DATDEADLINE", dtpEISDeadline.Text)
                }
                DB.RunCommand(SQL, params)

                LoadEISYear()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateEISYear_Click(sender As Object, e As EventArgs) Handles btnUpdateEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If rdbEISThreeYear.Checked Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            Dim SQL As String = "Select " &
                "strYear " &
                "from EIThresholdYears " &
                "where strYEar = @strYEar "
            Dim param As New SqlParameter("@strYEar", mtbThresholdYear.Text)

            If DB.ValueExists(SQL, param) Then
                SQL = "UPDATE EITHRESHOLDYEARS " &
                    " SET STREITYPE = @STREITYPE, " &
                    " DATDEADLINE = @DATDEADLINE " &
                    " WHERE STRYEAR = @STRYEAR "
                Dim params As SqlParameter() = {
                    New SqlParameter("@STREITYPE", EISYearType),
                    New SqlParameter("@DATDEADLINE", dtpEISDeadline.Text),
                    New SqlParameter("@STRYEAR", mtbThresholdYear.Text)
                }
                DB.RunCommand(SQL, params)

                LoadEISYear()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("EIS Year does not currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadEISLog_Click(sender As Object, e As EventArgs) Handles btnLoadEISLog.Click
        If Not mtbEISLogAIRSNumber.IsValid Then
            MsgBox("Enter a valid AIRS number.")
            Return
        End If

        If cboEISStatisticsYear.Text.Length <> 4 Then
            MsgBox("Select a year.")
            Return
        End If

        mtbEILogAIRSNumber.AirsNumber = mtbEISLogAIRSNumber.AirsNumber
        cboEILogYear.Text = cboEISStatisticsYear.Text

        LoadFSData()

        TCDMUTools.SelectedIndex = 0
    End Sub

    Private Sub llbEISStatsFipassed_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsFipassed.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Return
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and QAStatusCode = '2' ")

            lblEISCount.Text = "QA Submitted, EPA Submitted Count: " & dgvEISStats.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFromQA_Click(sender As Object, e As EventArgs) Handles btnRemoveFromQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete all current QA data.", Me.Text)

            If EISConfirm = txtEILogSelectedYear.Text Then
                Dim SQL1 As String = "delete EIS_QAAdmin " &
                "where inventoryyear = @inventoryyear " &
                "and facilitysiteid = @facilitysiteid "
                Dim params1 As SqlParameter() = {
                    New SqlParameter("@inventoryyear", EISConfirm),
                    New SqlParameter("@facilitysiteid", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
                }

                Dim SQL2 As String = "Update EIS_Admin set " &
                  "EISAccessCode = '2', " &
                  "EISStatusCode = '3', " &
                  "datEISstatus = GETDATE(), " &
                  "UpdateUser = @UpdateUser, " &
                  "updatedatetime = getdate() " &
                  "where inventoryYear = @inventoryYear " &
                  "and facilitysiteid = @facilitysiteid "
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@UpdateUser", CurrentUser.AlphaName),
                    New SqlParameter("@inventoryYear", EISConfirm),
                    New SqlParameter("@facilitysiteid", txtEILogSelectedAIRSNumber.AirsNumber.ShortString)
                }

                Dim querylist As New List(Of String) From {SQL1, SQL2}
                Dim paramlist As New List(Of SqlParameter()) From {params1, params2}

                DB.RunCommand(querylist, paramlist)

                MsgBox("Done", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCleanUp_Click(sender As Object, e As EventArgs) Handles btnCleanUp.Click
        Try
            Dim spName As String = "dbo.PD_EIS_QASTART"

            Dim selection As Boolean = False

            For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                If dgvEISStats(0, i).Value Then
                    Dim params As SqlParameter() = {
                        New SqlParameter("@AIRSNUMBER_IN", dgvEISStats(1, i).Value),
                        New SqlParameter("@INTYEAR_IN", cboEISStatisticsYear.Text)
                    }
                    DB.SPRunCommand(spName, params)
                    selection = True
                End If
            Next

            If selection Then
                MsgBox("Complete", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("No facilities selected.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewMailoutData_Click(sender As Object, e As EventArgs) Handles btnViewMailoutData.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            lblEISCount.Text = "EIS Mailout Count: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenerateMailout_Click(sender As Object, e As EventArgs) Handles btnGenerateMailout.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strMailout = '1' " &
            "where inventoryYear = @inventoryYear " &
            "and Active = '1' "
            Dim param As New SqlParameter("@inventoryYear", txtSelectedEISMailout.Text)

            DB.RunCommand(SQL, param)

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            lblEISCount.Text = "EIS Mailout Count (Generated): " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveAllMailout_Click(sender As Object, e As EventArgs) Handles btnRemoveAllMailout.Click
        Try

            Dim SQL As String = "Update EIS_Admin set " &
          "strMailout = null " &
          "where inventoryYear = @inventoryYear " &
          "and strMailout = '1' " &
          "and Active = '1' "
            Dim param As New SqlParameter("@inventoryYear", txtSelectedEISMailout.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            lblEISCount.Text = "EIS Mailout Count (Removed): " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISEnrolled_Click(sender As Object, e As EventArgs) Handles btnViewEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            lblEISCount.Text = "EIS Enrolled: " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEISEnrollMailoutList_Click(sender As Object, e As EventArgs) Handles btnEISEnrollMailoutList.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strEnrollment = '1' , " &
            "EISSTATUSCODE= '1', " &
            "EISAccessCode = '1', " &
            "DatEISStatus = getdate() " &
            "where active = '1' " &
            "and InventoryYear = @InventoryYear " &
            "and EISStatusCode in ('0', '1') " &
            "and strMailout = '1' "

            Dim param As New SqlParameter("@InventoryYear", txtEISStatsEnrollmentYear.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            lblEISCount.Text = "EIS Enrolled (Generated): " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveEISEnrolled_Click(sender As Object, e As EventArgs) Handles btnRemoveEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strEnrollment = '0' " &
            "where active = '1' " &
            "and InventoryYear = @InventoryYear " &
            "and strEnrollment = '1' "

            Dim param As New SqlParameter("@InventoryYear", txtEISStatsEnrollmentYear.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            lblEISCount.Text = "EIS Enrolled (Removed): " & dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub EIS_VIEW(EISYear As String, EISMailout As String, EISEnrollment As String,
                    EISActive As String, Optout As String, EISStatus As String,
                    EISAccess As String, QAStatus As String)

        'EISYear = value
        'EISMailout = value: 0,1, or null
        'EISEnrollment = value: 0, 1, or null 
        'EISActive = value: 0, 1 or null 
        'Optout = value: 0, 1, Null, or text
        'EISStatus = text
        'EISAccess = text
        'QAStatus = text 

        Try
            If EISYear = "" Then
                Return
            End If

            If EISActive = "" Then
                EISActive = "1"
            End If

            Dim SQL As String = "Select " &
            "'False' as ID, " &
            "FACILITYSITEID, " &
            "STRFACILITYNAME, INVENTORYYEAR, " &
            "EISSTAtuS, EISACCESS, OPTOUT, " &
            "MAILOUT, MAILOUTEMAIL, " &
            "STRDMURESPONSIBLESTAFF, ENROLLMENT, " &
            "QASTATUS, DATQASTATUS, " &
            "IAIPPREFIX, IAIPFIRSTNAME, " &
            "IAIPLASTNAME, IAIPEMAIL, " &
            "EISCOMPANYNAME, EISADDRESS, " &
            "EISADDRESS2, EISCITY, " &
            "EISSTATE, EISZIPCODE, " &
            "EISPREFIX, EISFIRSTNAME, " &
            "EISLASTNAME, DATFINALIZE, " &
            "strComment as Comments, " &
            "STRFITRACKINGNUMBER as FITrackingNumber, " &
            "STRPOINTTRACKINGNUMBER as PointTrackingNumber " &
            " from VW_EIS_Stats " &
            "where inventoryyear = @inventoryyear " &
            "and Active = @Active "

            If EISMailout <> "" Then
                SQL &= " and strMailout = @strMailout "
            End If
            If EISEnrollment <> "" Then
                SQL &= " and strEnrollment = @strEnrollment "
            End If
            If Optout <> "" Then
                Select Case Optout
                    Case "Null"
                        SQL &= " and strOptOut is null "
                    Case "0"
                        SQL &= " and strOptOut = '0' "
                    Case "1"
                        SQL &= " and strOptOut = '1'  "
                    Case Else
                        SQL &= Optout
                End Select
            End If
            If EISStatus <> "" Then
                SQL &= EISStatus
            End If
            If EISAccess <> "" Then
                SQL &= EISAccess
            End If
            If QAStatus <> "" Then
                SQL &= QAStatus
            End If

            SQL &= " ORDER BY INVENTORYYEAR DESC, FACILITYSITEID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryyear", EISYear),
                New SqlParameter("@Active", EISActive),
                New SqlParameter("@strMailout", EISMailout),
                New SqlParameter("@strEnrollment", EISEnrollment)
            }

            dgvEISStats.Rows.Clear()

            Dim dt As DataTable = DB.GetDataTable(SQL, params)

            For Each dr As DataRow In dt.Rows
                Using dgvRow As New DataGridViewRow
                    dgvRow.CreateCells(dgvEISStats)

                    If IsDBNull(dr.Item("ID")) Then
                        dgvRow.Cells(0).Value = ""
                    Else
                        dgvRow.Cells(0).Value = dr.Item("ID")
                    End If
                    If IsDBNull(dr.Item("FacilitySiteID")) Then
                        dgvRow.Cells(1).Value = ""
                    Else
                        dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        dgvRow.Cells(2).Value = ""
                    Else
                        dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("INVENTORYYEAR")) Then
                        dgvRow.Cells(3).Value = ""
                    Else
                        dgvRow.Cells(3).Value = dr.Item("INVENTORYYEAR")
                    End If
                    If IsDBNull(dr.Item("EISStatus")) Then
                        dgvRow.Cells(4).Value = ""
                    Else
                        dgvRow.Cells(4).Value = dr.Item("EISStatus")
                    End If
                    If IsDBNull(dr.Item("EISAccess")) Then
                        dgvRow.Cells(5).Value = ""
                    Else
                        dgvRow.Cells(5).Value = dr.Item("EISAccess")
                    End If
                    If IsDBNull(dr.Item("OptOut")) Then
                        dgvRow.Cells(6).Value = ""
                    Else
                        dgvRow.Cells(6).Value = dr.Item("OptOut")
                    End If

                    If IsDBNull(dr.Item("MailOut")) Then
                        dgvRow.Cells(7).Value = ""
                    Else
                        dgvRow.Cells(7).Value = dr.Item("Mailout")
                    End If
                    If IsDBNull(dr.Item("MailoutEmail")) Then
                        dgvRow.Cells(8).Value = ""
                    Else
                        dgvRow.Cells(8).Value = dr.Item("MailoutEmail")
                    End If
                    If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                        dgvRow.Cells(9).Value = ""
                    Else
                        dgvRow.Cells(9).Value = dr.Item("strDMUResponsibleStaff")
                    End If
                    If IsDBNull(dr.Item("Enrollment")) Then
                        dgvRow.Cells(10).Value = ""
                    Else
                        dgvRow.Cells(10).Value = dr.Item("Enrollment")
                    End If
                    If IsDBNull(dr.Item("QASTATUS")) Then
                        dgvRow.Cells(11).Value = ""
                    Else
                        dgvRow.Cells(11).Value = dr.Item("QASTATUS")
                    End If

                    dgvRow.Cells(12).Value = GetNullableDateTime(dr.Item("DATQASTATUS"))

                    If IsDBNull(dr.Item("IAIPPrefix")) Then
                        dgvRow.Cells(13).Value = ""
                    Else
                        dgvRow.Cells(13).Value = dr.Item("IAIPPrefix")
                    End If
                    If IsDBNull(dr.Item("IAIPFIRSTNAME")) Then
                        dgvRow.Cells(14).Value = ""
                    Else
                        dgvRow.Cells(14).Value = dr.Item("IAIPFIRSTNAME")
                    End If
                    If IsDBNull(dr.Item("IAIPLASTNAME")) Then
                        dgvRow.Cells(15).Value = ""
                    Else
                        dgvRow.Cells(15).Value = dr.Item("IAIPLASTNAME")
                    End If
                    If IsDBNull(dr.Item("IAIPEMAIL")) Then
                        dgvRow.Cells(16).Value = ""
                    Else
                        dgvRow.Cells(16).Value = dr.Item("IAIPEMAIL")
                    End If
                    If IsDBNull(dr.Item("EISCOMPANYNAME")) Then
                        dgvRow.Cells(17).Value = ""
                    Else
                        dgvRow.Cells(17).Value = dr.Item("EISCOMPANYNAME")
                    End If
                    If IsDBNull(dr.Item("EISADDRESS")) Then
                        dgvRow.Cells(18).Value = ""
                    Else
                        dgvRow.Cells(18).Value = dr.Item("EISADDRESS")
                    End If
                    If IsDBNull(dr.Item("EISADDRESS2")) Then
                        dgvRow.Cells(19).Value = ""
                    Else
                        dgvRow.Cells(19).Value = dr.Item("EISADDRESS2")
                    End If
                    If IsDBNull(dr.Item("EISCITY")) Then
                        dgvRow.Cells(20).Value = ""
                    Else
                        dgvRow.Cells(20).Value = dr.Item("EISCITY")
                    End If
                    If IsDBNull(dr.Item("EISState")) Then
                        dgvRow.Cells(21).Value = ""
                    Else
                        dgvRow.Cells(21).Value = dr.Item("EISState")
                    End If
                    If IsDBNull(dr.Item("EISZipCode")) Then
                        dgvRow.Cells(22).Value = ""
                    Else
                        dgvRow.Cells(22).Value = dr.Item("EISZipCode")
                    End If
                    If IsDBNull(dr.Item("EISPrefix")) Then
                        dgvRow.Cells(23).Value = ""
                    Else
                        dgvRow.Cells(23).Value = dr.Item("EISPrefix")
                    End If
                    If IsDBNull(dr.Item("EISFirstname")) Then
                        dgvRow.Cells(24).Value = ""
                    Else
                        dgvRow.Cells(24).Value = dr.Item("EISFirstname")
                    End If

                    If IsDBNull(dr.Item("EISLASTNAME")) Then
                        dgvRow.Cells(25).Value = ""
                    Else
                        dgvRow.Cells(25).Value = dr.Item("EISLASTNAME")
                    End If

                    dgvRow.Cells(26).Value = GetNullableDateTime(dr.Item("DATFINALIZE"))
                    dgvRow.Cells(27).Value = GetNullable(Of String)(dr.Item("FITrackingNumber"))
                    dgvRow.Cells(28).Value = GetNullable(Of String)(dr.Item("PointTrackingNumber"))
                    dgvRow.Cells(29).Value = GetNullable(Of String)(dr.Item("Comments"))

                    dgvEISStats.Rows.Add(dgvRow)
                End Using
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISSummaryToExcel_Click(sender As Object, e As EventArgs) Handles btnEISSummaryToExcel.Click
        dgvEISStats.ExportToExcel(Me)
    End Sub

    Private Sub AcceptButton_Leave(sender As Object, e As EventArgs) _
    Handles mtbEILogAIRSNumber.Leave,
    txtEIModifyFacilityName.Leave,
    txtEIModifyLocation.Leave, txtEIModifyCity.Leave, mtbEIModifyZipCode.Leave,
    txtEIModifyMLocation.Leave, txtEIModifyMCity.Leave, mtbEIModifyMZipCode.Leave,
    mtbEIModifyLatitude.Leave, mtbEIModifyLongitude.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub mtbEILogAIRSNumber_Enter(sender As Object, e As EventArgs) _
    Handles mtbEILogAIRSNumber.Enter
        Me.AcceptButton = btnReloadFSData
    End Sub

    Private Sub txtEIModifyFacilityName_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyFacilityName.Enter
        Me.AcceptButton = btnEIModifyUpdateName
    End Sub

    Private Sub EIModifyLocation_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyLocation.Enter, txtEIModifyCity.Enter, mtbEIModifyZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateLocation
    End Sub

    Private Sub EIModifyMailing_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyMLocation.Enter, txtEIModifyMCity.Enter, mtbEIModifyMZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateMailing
    End Sub

    Private Sub EIModifyLatitudeLongitude_Enter(sender As Object, e As EventArgs) _
    Handles mtbEIModifyLatitude.Enter, mtbEIModifyLongitude.Enter
        Me.AcceptButton = btnUpdateLatLong
    End Sub

    Private Sub btnMismatchedStatus_Click(sender As Object, e As EventArgs) Handles btnMismatchedStatus.Click
        ShowMismatchedOperatingStatus()
    End Sub

    Private Sub ShowMismatchedOperatingStatus()
        Dim query As String = "SELECT ef.FACILITYSITEID AS ""AIRS Number"", " &
            "  ef.STRFACILITYSITENAME AS ""Facility Name"", " &
            "  ef.STRFACILITYSITESTATUSCODE AS ""EIS Site Status"", " &
            "  hd.STROPERATIONALSTATUS AS ""IAIP Site Status"" " &
            "FROM EIS_FACILITYSITE ef " &
            "INNER JOIN APBHEADERDATA hd ON ef.FACILITYSITEID = RIGHT( " &
            "  hd.STRAIRSNUMBER, 8) " &
            "WHERE(ef.STRFACILITYSITESTATUSCODE = 'OP' AND " &
            "  hd.STROPERATIONALSTATUS <> 'O') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE = 'PS' AND " &
            "  hd.STROPERATIONALSTATUS <> 'X') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE = 'TS' AND " &
            "  hd.STROPERATIONALSTATUS <> 'T') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'OP' AND " &
            "  hd.STROPERATIONALSTATUS = 'O') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'PS' AND " &
            "  hd.STROPERATIONALSTATUS = 'X') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'TS' AND " &
            "  hd.STROPERATIONALSTATUS = 'T') OR " &
            "  ef.STRFACILITYSITESTATUSCODE IS NULL OR " &
            "  hd.STROPERATIONALSTATUS IS NULL " &
            "ORDER BY ef.FACILITYSITEID"
        dgvOperStatusMismatch.DataSource = DB.GetDataTable(query)
        dgvOperStatusMismatch.SanelyResizeColumns
    End Sub

#Region " EIS Stats selection tools "

    Private Sub dgvEISStats_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEISStats.CellClick
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvEISStats.RowCount Then
            If dgvEISStats.Rows(e.RowIndex).Cells("Select").Value Then
                dgvEISStats.Rows(e.RowIndex).Cells("Select").Value = False
            Else
                dgvEISStats.Rows(e.RowIndex).Cells("Select").Value = True
            End If
        End If

        DisplayEisStageCount(CountSelectedEisStageFacilities())
    End Sub

    Private Sub DisplayEisStageCount(count As Integer)
        lblEisStageSelectedCount.Text = String.Format("Selected: {0}", count.ToString)
    End Sub

    Private Function CountSelectedEisStageFacilities() As Integer
        Dim count As Integer = 0

        For Each row As DataGridViewRow In dgvEISStats.Rows
            If CBool(row.Cells("Select").Value) Then
                count += 1
            End If
        Next

        Return count
    End Function

    Private Sub btnEisStageSelectAll_Click(sender As Object, e As EventArgs) Handles btnEisStageSelectAll.Click
        For Each row As DataGridViewRow In dgvEISStats.Rows
            row.Cells("Select").Value = True
        Next

        DisplayEisStageCount(CountSelectedEisStageFacilities)
    End Sub

    Private Sub btnSelectHighlighted_Click(sender As Object, e As EventArgs) Handles btnSelectHighlighted.Click
        For Each row As DataGridViewRow In dgvEISStats.SelectedRows
            row.Cells("Select").Value = True
        Next

        DisplayEisStageCount(CountSelectedEisStageFacilities)
    End Sub

    Private Sub btnEisStageSelectNone_Click(sender As Object, e As EventArgs) Handles btnEisStageSelectNone.Click
        For Each row As DataGridViewRow In dgvEISStats.Rows
            row.Cells("Select").Value = False
        Next

        DisplayEisStageCount(0)
    End Sub

#End Region

#Region " History "

    Private Sub LoadHistoryComboBoxes()
        Dim sql As String = "SELECT INVENTORYYEAR AS EIYear
            FROM EIS_ADMIN
            where INVENTORYYEAR < 2019
            UNION
            SELECT STRINVENTORYYEAR
            FROM EISI
            ORDER BY EIYear DESC"

        With cboEIYear
            .DataSource = DB.GetDataTable(sql)
            .DisplayMember = "EIYear"
            .ValueMember = "EIYear"
            .SelectedIndex = 0
        End With

        sql = "select e.STRPOLLUTANTCODE As Pollutant,
                   p.STRDESC          as Description
            from EIEM e
                inner JOIN dbo.EISLK_POLLUTANTCODE p
                ON e.STRPOLLUTANTCODE = p.POLLUTANTCODE
            union
            select e.POLLUTANTCODE,
                   p.STRDESC
            FROM dbo.EIS_REPORTINGPERIODEMISSIONS e
                inner JOIN dbo.EISLK_POLLUTANTCODE p
                ON e.POLLUTANTCODE = p.POLLUTANTCODE
            order by Description "

        With cboEIPollutants
            .DataSource = DB.GetDataTable(sql)
            .DisplayMember = "Description"
            .ValueMember = "Pollutant"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub btnEISummary_Click(sender As Object, e As EventArgs) Handles btnEISummary.Click
        Dim sql As String
        Dim year As Integer

        If Integer.TryParse(cboEIYear.Text, year) Then
            If CInt(cboEIYear.Text) < 2010 Then
                sql = "SELECT AIRSNumber, FacilityName, SO2, NOX, VOC, CO, NH3, Lead, PMFIL, PMPRI, PM10PRI, PM25PRI
                    FROM (SELECT SUBSTRING(strairsnumber, 5, 8) AS AIRSNumber,
                                 strfacilityname                AS FacilityName,
                                 SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, CO, NH3, Lead
                          FROM (SELECT dt.strairsnumber, dt.strfacilityname,
                                       SUM(IIF(dt.strpollutantcode = 'SO2', pollutanttotal, NULL))      AS SO2,
                                       SUM(IIF(dt.strpollutantcode = 'NOX', pollutanttotal, NULL))      AS NOx,
                                       SUM(IIF(dt.strpollutantcode = 'PM-PRI', pollutanttotal, NULL))   AS PMPRI,
                                       SUM(IIF(dt.strpollutantcode = 'PM-FIL', pollutanttotal, NULL))   AS PMFIL,
                                       SUM(IIF(dt.strpollutantcode = 'PM10-PRI', pollutanttotal, NULL)) AS PM10PRI,
                                       SUM(IIF(dt.strpollutantcode = 'PM25-PRI', pollutanttotal, NULL)) AS PM25PRI,
                                       SUM(IIF(dt.strpollutantcode = 'VOC', pollutanttotal, NULL))      AS VOC,
                                       SUM(IIF(dt.strpollutantcode = 'CO', pollutanttotal, NULL))       AS CO,
                                       SUM(IIF(dt.strpollutantcode = 'NH3', pollutanttotal, NULL))      AS NH3,
                                       SUM(IIF(dt.strpollutantcode = '7439921', pollutanttotal, NULL))  AS Lead
                                FROM (SELECT dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode,
                                             dtSumPollutant.PollutantTotal
                                      FROM eisi
                                          inner join
                                      (SELECT strairsnumber, strpollutantcode,
                                              SUM(dblemissionnumericvalue) AS PollutantTotal, strinventoryyear
                                       FROM eiem
                                       WHERE strinventoryyear = @year
                                       GROUP BY strairsnumber, strpollutantcode, strinventoryyear) AS dtSumPollutant
                                          on eisi.strairsnumber = dtSumPollutant.strairsnumber
                                              AND eisi.strinventoryyear = dtSumPollutant.strinventoryyear) AS dt
                                GROUP BY dt.strairsnumber, dt.strfacilityname) AS t1) AS t2
                    order by AIRSNumber"

                Dim param As New SqlParameter("@year", cboEIYear.Text)

                dgvEIResults.DataSource = DB.GetDataTable(sql, param)

                dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                dgvEIResults.Columns("AIRSNumber").Width = 75
                dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                dgvEIResults.Columns("FacilityName").Width = 225
                dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                dgvEIResults.Columns("Lead").HeaderText = "Lead"
                dgvEIResults.Columns("PMPRI").HeaderText = "PM Primary - old EI"
                dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
            Else
                sql = "select AIRSNumber, FacilityName, SO2, NOX, VOC, CO, NH3, Lead, PMCON, PM10PRI, PM10FIL, PM25PRI, PMFIL
                    from VW_EIS_EMISSIONSUMMARY
                    WHERE INTINVENTORYYEAR = @year
                    order by AIRSNumber"

                Dim param As New SqlParameter("@year", cboEIYear.Text)

                dgvEIResults.DataSource = DB.GetDataTable(sql, param)

                dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                dgvEIResults.Columns("AIRSNumber").Width = 75
                dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                dgvEIResults.Columns("FacilityName").Width = 225
                dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                dgvEIResults.Columns("Lead").HeaderText = "Lead"
                dgvEIResults.Columns("PMCON").HeaderText = "Condensible PM"
                dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                dgvEIResults.Columns("PM10FIL").HeaderText = "Filterable PM10"
                dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
            End If
        End If
    End Sub

    Private Sub btnViewEISummaryByPollutant_Click(sender As Object, e As EventArgs) Handles btnViewEISummaryByPollutant.Click
        Dim sql As String
        Dim year As Integer

        If Integer.TryParse(cboEIYear.Text, year) Then
            If CInt(cboEIYear.Text) < 2010 Then
                sql = "SELECT right(m.STRAIRSNUMBER, 8)      as AIRSNumber,
                           i.STRFACILITYNAME              AS FacilityName,
                           SUM(m.DBLEMISSIONNUMERICVALUE) AS Pollutant
                    FROM eiem m
                        inner join eisi i
                        on m.STRAIRSNUMBER = i.STRAIRSNUMBER
                            AND m.STRINVENTORYYEAR = i.STRINVENTORYYEAR
                    WHERE m.STRINVENTORYYEAR = @year
                      AND m.STRPOLLUTANTCODE = @poll
                    GROUP BY m.STRAIRSNUMBER, i.STRFACILITYNAME
                    order by m.STRAIRSNUMBER"
            Else
                sql = "SELECT FACILITYSITEID as AIRSNumber, f.STRFACILITYNAME AS FacilityName, SUM(FLTTOTALEMISSIONS) AS Pollutant
                    FROM VW_EIS_RPEMISSIONS e
                        inner join APBFACILITYINFORMATION f
                        on right(f.STRAIRSNUMBER, 8) = e.FACILITYSITEID
                    WHERE INTINVENTORYYEAR = @year
                      AND POLLUTANTCODE = @poll
                      and RPTPERIODTYPECODE = 'A'
                    GROUP BY FACILITYSITEID, f.STRFACILITYNAME, POLLUTANTCODE
                    order by FACILITYSITEID "
            End If

            Dim params As SqlParameter() = {
                New SqlParameter("@year", cboEIYear.Text),
                New SqlParameter("@poll", cboEIPollutants.SelectedValue)
            }

            dgvEIResults.DataSource = DB.GetDataTable(sql, params)

            dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvEIResults.Columns("AIRSNumber").Width = 75
            dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
            dgvEIResults.Columns("FacilityName").Width = 225
            dgvEIResults.Columns("Pollutant").HeaderText = cboEIPollutants.Text
        End If
    End Sub

#End Region

#Region " CAERS Users "

    Private Sub btnCaersView_Click(sender As Object, e As EventArgs) Handles btnCaersView.Click
        Dim param As SqlParameter = New SqlParameter("@includeDeleted", chkCaersShowDeleted.Checked)
        dgvCaersUsers.DataSource = DB.SPGetDataTable("geco.Caer_GetAllContacts", param)
        dgvCaersUsers.Columns("Deleted").Visible = chkCaersShowDeleted.Checked
    End Sub

    Private Sub dgvCaersUsers_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCaersUsers.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            If dgvCaersUsers.Columns(e.ColumnIndex).HeaderText.ToUpper = "AIRS #" AndAlso Apb.ApbFacilityId.IsValidAirsNumberFormat(e.Value.ToString()) Then
                e.Value = New Apb.ApbFacilityId(e.Value.ToString).FormattedString
            ElseIf TypeOf e.Value Is Date Then
                e.CellStyle.Format = DateFormat
            End If
        End If
    End Sub

    Private Sub btnViewAllContacts_Click(sender As Object, e As EventArgs) Handles btnViewAllContacts.Click
        Dim param As SqlParameter = New SqlParameter("@InventoryYear", cboAllContacts.Text)
        Dim dt As DataTable = DB.SPGetDataTable("dbo.PD_EI_Status_And_Contacts", param)
        dt.PrimaryKey = Nothing
        dgvAllContacts.DataSource = dt
    End Sub

#End Region

End Class