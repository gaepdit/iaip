Imports System.Data.SqlClient

Public Class SBEAPClientSummary

    Private dtCounty As DataTable
    Private query As String

#Region " Page Load "

    Private Sub SBEAPClientMaintenance_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ClientSummary = Nothing
    End Sub

    Private Sub SBEAPClientMaintenance_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadDataSets()
        LoadComboBoxes()
    End Sub

    Private Sub LoadDataSets()
        Try
            Dim SQL As String = "Select " &
                "' ' as CountyName, ' ' as strCountyCode, '' as strDistrictCode, '' as strDistrictName " &
                "union select " &
            "Distinct(LookUpCountyInformation.strCountyName) as CountyName, " &
            "LookUpCountyInformation.strCountyCode, " &
            "LookUpDistrictinformation.strDistrictCode, " &
            "strDistrictName  " &
            "from LookUpCountyInformation " &
            "inner join LookUpDistrictInformation " &
            "on LookUpCountyInformation.strCountyCode = LookUpDistrictinformation.strDistrictCounty " &
            "inner join LookUpDistricts " &
            "on LookUpDistrictInformation.strDistrictCode = LookUpDistricts.strDistrictCode " &
            "order by CountyName "

            dtCounty = DB.GetDataTable(SQL)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComboBoxes()
        Try
            With cboCounty
                .DataSource = dtCounty
                .DisplayMember = "CountyName"
                .ValueMember = "strCountyCode"
                .SelectedValue = 0
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Public Sub LoadClientData()
        Try
            Dim CompanyName As String = ""
            Dim StartDate As String = ""
            Dim Address As String = ""
            Dim Address2 As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""
            Dim County As String = ""
            Dim Latitude As String = ""
            Dim Longitude As String = ""
            Dim MailingAddress As String = ""
            Dim MailingAddress2 As String = ""
            Dim MailingCity As String = ""
            Dim MailingState As String = ""
            Dim MailingZipCode As String = ""
            Dim Creator As String = ""
            Dim CreatedDate As String = ""
            Dim ModifingStaff As String = ""
            Dim ModifingDate As String = ""

            Dim Description As String = ""
            Dim WebSite As String = ""
            Dim SIC As String = ""
            Dim SICDesc As String = ""
            Dim NAICS As String = ""
            Dim Employees As String = ""
            Dim AIRSNumber As String = ""
            Dim AirProgramCode As String = ""
            Dim StateProgram As String = ""
            Dim AirPermit As String = ""
            Dim SSCPEngineer As String = ""
            Dim SSCPUnit As String = ""
            Dim SSPPEngineer As String = ""
            Dim SSPPUnit As String = ""
            Dim ISMPEngineer As String = ""
            Dim ISMPUnit As String = ""
            Dim AirDescription As String = ""

            If DAL.Sbeap.ClientExists(txtClientID.Text) Then
                query = "select  " &
                "strCompanyName, datStartDate,  " &
                "strCompanyAddress, strCompanyAddress2,  " &
                "strCompanyCity, strCompanyState,  " &
                "strCompanyZipCode, strcompanyCounty,  " &
                "strCompanyLatitude, strCompanyLongitude,  " &
                "strMailingAddress, strMailingAddress2,  " &
                "strMailingCity, strMailingState,  " &
                "strMailingZipCode,  " &
                "(select concat(strLastName,', ',strFirstName) as Creator  " &
                "from SBEAPClients inner join EPDUserProfiles  " &
                "on SBEAPClients.strCompanyCreator = EPDUserProfiles.numUserID  " &
                "where clientid = @clientid) as Creator, " &
                "format(datCompanyCreated, 'MMMM d, yyyy') as datCompanyCreated,  " &
                "(select concat(strLastName,', ',strFirstName) as Modifier " &
                "from SBEAPClients inner join EPDUserProfiles  " &
                "on SBEAPClients.strModifingPerson = EPDUserProfiles.numUserID  " &
                "where clientid = @clientid ) as Modifier,  " &
                "format(datModifingDate , 'MMMM d, yyyy') as datModifingDate " &
                "from SBEAPClients  " &
                "where ClientID = @clientid "

                Dim p As New SqlParameter("@clientid", txtClientID.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strCompanyname")) Then
                        CompanyName = ""
                    Else
                        CompanyName = dr.Item("strCompanyName")
                    End If
                    If IsDBNull(dr.Item("datStartDate")) Then
                        StartDate = ""
                    Else
                        StartDate = dr.Item("datStartDate")
                    End If
                    If IsDBNull(dr.Item("strCompanyAddress")) Then
                        Address = ""
                    Else
                        Address = dr.Item("strCompanyAddress")
                    End If
                    If IsDBNull(dr.Item("strCompanyAddress2")) Then
                        Address2 = ""
                    Else
                        Address2 = dr.Item("strCompanyAddress2")
                    End If
                    If IsDBNull(dr.Item("strCompanyCity")) Then
                        City = ""
                    Else
                        City = dr.Item("strCompanyCity")
                    End If
                    If IsDBNull(dr.Item("strCompanyState")) Then
                        State = ""
                    Else
                        State = dr.Item("strCompanyState")
                    End If
                    If IsDBNull(dr.Item("strCompanyZipCode")) Then
                        ZipCode = ""
                    Else
                        ZipCode = dr.Item("strCompanyZipCode")
                    End If
                    If IsDBNull(dr.Item("strcompanyCounty")) Then
                        County = ""
                    Else
                        County = dr.Item("strcompanyCounty")
                    End If
                    If IsDBNull(dr.Item("strCompanyLatitude")) Then
                        Latitude = ""
                    Else
                        Latitude = dr.Item("strCompanyLatitude")
                    End If
                    If IsDBNull(dr.Item("strCompanyLongitude")) Then
                        Longitude = ""
                    Else
                        Longitude = dr.Item("strCompanyLongitude")
                    End If
                    If IsDBNull(dr.Item("strMailingAddress")) Then
                        MailingAddress = ""
                    Else
                        MailingAddress = dr.Item("strMailingAddress")
                    End If
                    If IsDBNull(dr.Item("strMailingAddress2")) Then
                        MailingAddress2 = ""
                    Else
                        MailingAddress2 = dr.Item("strMailingAddress2")
                    End If
                    If IsDBNull(dr.Item("strMailingCity")) Then
                        MailingCity = ""
                    Else
                        MailingCity = dr.Item("strMailingCity")
                    End If
                    If IsDBNull(dr.Item("strMailingState")) Then
                        MailingState = ""
                    Else
                        MailingState = dr.Item("strMailingState")
                    End If
                    If IsDBNull(dr.Item("strMailingZipCode")) Then
                        MailingZipCode = ""
                    Else
                        MailingZipCode = dr.Item("strMailingZipCode")
                    End If
                    If IsDBNull(dr.Item("Creator")) Then
                        Creator = ""
                    Else
                        Creator = dr.Item("Creator")
                    End If
                    If IsDBNull(dr.Item("datCompanyCreated")) Then
                        CreatedDate = ""
                    Else
                        CreatedDate = dr.Item("datCompanyCreated")
                    End If
                    If IsDBNull(dr.Item("Modifier")) Then
                        ModifingStaff = ""
                    Else
                        ModifingStaff = dr.Item("Modifier")
                    End If
                    If IsDBNull(dr.Item("datModifingDate")) Then
                        ModifingDate = ""
                    Else
                        ModifingDate = dr.Item("datModifingDate")
                    End If
                End If

                query = "Select " &
                "strClientDescription, strClientWEbSite, " &
                "strClientSIC, SIC_DESC as strSICDesc, " &
                "strClientNAICS, " &
                "strClientEmployees, strAIRSNumber, " &
                "strAIRProgramCodes, strStateProgramCodes, " &
                "strAirPermitNumber, strSSCPEngineer, " &
                "strSSCPUnit, strSSPPEngineer, " &
                "strSSPPUnit, strISMPEngineer, " &
                "strISMPUnit, strAirDescription " &
                "from SBEAPClientData left join LK_SIC " &
                "on SBEAPClientData.strClientSIC = LK_SIC.SIC_CODE " &
                "where ClientID = @clientid "

                Dim dr2 As DataRow = DB.GetDataRow(query, p)

                If dr2 IsNot Nothing Then
                    If IsDBNull(dr2.Item("strClientDescription")) Then
                        Description = ""
                    Else
                        Description = dr2.Item("strClientDescription")
                    End If
                    If IsDBNull(dr2.Item("strClientWEbSite")) Then
                        WebSite = ""
                    Else
                        WebSite = dr2.Item("strClientWEbSite")
                    End If
                    If IsDBNull(dr2.Item("strClientSIC")) Then
                        SIC = ""
                    Else
                        SIC = dr2.Item("strClientSIC")
                    End If
                    If IsDBNull(dr2.Item("strSICDesc")) Then
                        SICDesc = ""
                    Else
                        SICDesc = "Description: " & dr2.Item("strSICDesc")
                    End If
                    If IsDBNull(dr2.Item("strClientNAICS")) Then
                        NAICS = ""
                    Else
                        NAICS = dr2.Item("strClientNAICS")
                    End If
                    If IsDBNull(dr2.Item("strClientEmployees")) Then
                        Employees = ""
                    Else
                        Employees = dr2.Item("strClientEmployees")
                    End If
                    If IsDBNull(dr2.Item("strAIRSNumber")) Then
                        AIRSNumber = ""
                    Else
                        AIRSNumber = dr2.Item("strAIRSNumber")
                    End If
                    If IsDBNull(dr2.Item("strAIRProgramCodes")) Then
                        AirProgramCode = ""
                    Else
                        AirProgramCode = dr2.Item("strAIRProgramCodes")
                    End If
                    If IsDBNull(dr2.Item("strStateProgramCodes")) Then
                        StateProgram = ""
                    Else
                        StateProgram = dr2.Item("strStateProgramCodes")
                    End If
                    If IsDBNull(dr2.Item("strAirPermitNumber")) Then
                        AirPermit = ""
                    Else
                        AirPermit = dr2.Item("strAirPermitNumber")
                    End If
                    If IsDBNull(dr2.Item("strSSCPEngineer")) Then
                        SSCPEngineer = ""
                    Else
                        SSCPEngineer = dr2.Item("strSSCPEngineer")
                    End If
                    If IsDBNull(dr2.Item("strSSCPUnit")) Then
                        SSCPUnit = ""
                    Else
                        SSCPUnit = dr2.Item("strSSCPUnit")
                    End If
                    If IsDBNull(dr2.Item("strSSPPEngineer")) Then
                        SSPPEngineer = ""
                    Else
                        SSPPEngineer = dr2.Item("strSSPPEngineer")
                    End If
                    If IsDBNull(dr2.Item("strSSPPUnit")) Then
                        SSPPUnit = ""
                    Else
                        SSPPUnit = dr2.Item("strSSPPUnit")
                    End If
                    If IsDBNull(dr2.Item("strISMPEngineer")) Then
                        ISMPEngineer = ""
                    Else
                        ISMPEngineer = dr2.Item("strISMPEngineer")
                    End If
                    If IsDBNull(dr2.Item("strISMPUnit")) Then
                        ISMPUnit = ""
                    Else
                        ISMPUnit = dr2.Item("strISMPUnit")
                    End If
                    If IsDBNull(dr2.Item("strAirDescription")) Then
                        AirDescription = ""
                    Else
                        AirDescription = dr2.Item("strAirDescription")
                    End If
                End If
            Else

            End If

            txtCompanyName.Text = CompanyName
            If StartDate = "" Then
                dtpStartDate.Value = Today
            Else
                Me.dtpStartDate.Text = StartDate
            End If
            txtStreetAddress.Text = Address
            txtStreetAddress2.Text = Address2
            txtCity.Text = City
            txtState.Text = State
            mtbZipCode.Text = ZipCode
            cboCounty.SelectedValue = County
            mtbLatitude.Text = Latitude
            mtbLongitude.Text = Longitude
            If MailingAddress <> "" Then
                txtMailingAddress.Text = MailingAddress
            Else
                txtMailingAddress.Text = "<Mailing Address 1>"
            End If
            If MailingAddress2 <> "" Then
                txtMailingAddress2.Text = MailingAddress2
            Else
                txtMailingAddress2.Text = "<Mailing Address 2>"
            End If
            If MailingCity <> "" Then
                txtMailingCity.Text = MailingCity
            Else
                txtMailingCity.Text = "<Mailing City>"
            End If
            If MailingState <> "" Then
                txtMailingState.Text = MailingState
            Else
                txtMailingState.Text = "GA"
            End If
            mtbMailingZipCode.Text = MailingZipCode

            txtClientCreator.Text = Creator & " - " & CreatedDate
            txtClientModifier.Text = ModifingStaff & " - " & ModifingDate

            txtDescription.Text = Description
            txtWebSite.Text = WebSite
            mtbSIC.Text = SIC
            lblSIC.Text = SICDesc
            mtbNAICS.Text = NAICS
            mtbNumberOfEmployees.Text = Employees
            mtbAIRSNumber.Text = AIRSNumber
            txtSSCPContact.Text = SSCPEngineer
            txtSSCPUnit.Text = SSCPUnit
            txtSSPPContact.Text = SSPPEngineer
            txtSSPPUnit.Text = SSPPUnit
            txtISMPContact.Text = ISMPEngineer
            txtISMPUnit.Text = ISMPUnit
            txtAirDescription.Text = AirDescription
            txtAIRPermitNumber.Text = AirPermit

            If Mid(AirProgramCode, 1, 1) = "1" Then
                chbSIP.Checked = True
            Else
                chbSIP.Checked = False
            End If
            If Mid(AirProgramCode, 2, 1) = "1" Then
                chbFederalSIP.Checked = True
            Else
                chbFederalSIP.Checked = False
            End If
            If Mid(AirProgramCode, 3, 1) = "1" Then
                chbNonFedSIP.Checked = True
            Else
                chbNonFedSIP.Checked = False
            End If
            If Mid(AirProgramCode, 4, 1) = "1" Then
                chbCFCTracking.Checked = True
            Else
                chbCFCTracking.Checked = False
            End If

            If Mid(AirProgramCode, 5, 1) = "1" Then
                chbPSD.Checked = True
            Else
                chbPSD.Checked = False
            End If
            If Mid(AirProgramCode, 6, 1) = "1" Then
                chbNSR.Checked = True
            Else
                chbNSR.Checked = False
            End If
            If Mid(AirProgramCode, 7, 1) = "1" Then
                chbNESHAP.Checked = True
            Else
                chbNESHAP.Checked = False
            End If
            If Mid(AirProgramCode, 8, 1) = "1" Then
                chbNSPS.Checked = True
            Else
                chbNSPS.Checked = False
            End If
            If Mid(AirProgramCode, 9, 1) = "1" Then
                chbAcidPrecip.Checked = True
            Else
                chbAcidPrecip.Checked = False
            End If
            If Mid(AirProgramCode, 10, 1) = "1" Then
                chbFESOP.Checked = True
            Else
                chbFESOP.Checked = False
            End If
            If Mid(AirProgramCode, 11, 1) = "1" Then
                chbNativeAmer.Checked = True
            Else
                chbNativeAmer.Checked = False
            End If
            If Mid(AirProgramCode, 12, 1) = "1" Then
                chbMACT.Checked = True
            Else
                chbMACT.Checked = False
            End If
            If Mid(AirProgramCode, 13, 1) = "1" Then
                chbTitleV.Checked = True
            Else
                chbTitleV.Checked = False
            End If
            If Mid(StateProgram, 1, 1) = "1" Then
                chbNSRPSD.Checked = True
            Else
                chbNSRPSD.Checked = False
            End If
            If Mid(StateProgram, 2, 1) = "1" Then
                chbHAPs.Checked = True
            Else
                chbHAPs.Checked = False
            End If

            LoadContactData()
            LoadClientWork()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadContactData()
        Try
            query = "select " &
            "SBEAPClientContacts.ClientContactID, " &
            "strClientFirstName, strClientLastName, " &
            "strclientSalutation, strClientCredentials, " &
            "strClientTitle, strClientPhoneNumber, " &
            "strClientCellPhone, strClientFax, " &
            "strClientEmail, " &
            "strClientAddress, strClientCity, " &
            "strClientState, strClientZipCode, " &
            "strContactNotes " &
            "from SBEAPClientContacts inner join " &
            "SBEAPClientLink " &
            "on SBEAPClientContacts.ClientContactID = SBEAPClientLink.ClientContactID " &
            "where ClientID = @clientid "

            Dim p As New SqlParameter("@clientid", txtClientID.Text)

            dgvContactInformation.DataSource = DB.GetDataTable(query, p)

            dgvContactInformation.RowHeadersVisible = False
            dgvContactInformation.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvContactInformation.AllowUserToResizeColumns = True
            dgvContactInformation.AllowUserToAddRows = False
            dgvContactInformation.AllowUserToDeleteRows = False
            dgvContactInformation.AllowUserToOrderColumns = True
            dgvContactInformation.AllowUserToResizeRows = True
            dgvContactInformation.ColumnHeadersHeight = "35"
            dgvContactInformation.Columns("ClientContactID").HeaderText = "Client ID"
            dgvContactInformation.Columns("ClientContactID").DisplayIndex = 0
            dgvContactInformation.Columns("ClientContactID").Visible = False
            dgvContactInformation.Columns("strClientFirstName").HeaderText = "First Name"
            dgvContactInformation.Columns("strClientFirstName").DisplayIndex = 1
            dgvContactInformation.Columns("strClientFirstName").Width = 125
            dgvContactInformation.Columns("strClientLastName").HeaderText = "Last Name"
            dgvContactInformation.Columns("strClientLastName").DisplayIndex = 2
            dgvContactInformation.Columns("strClientLastName").Width = 125
            dgvContactInformation.Columns("strclientSalutation").HeaderText = "Salutation"
            dgvContactInformation.Columns("strclientSalutation").DisplayIndex = 3
            dgvContactInformation.Columns("strClientCredentials").HeaderText = "Credentials"
            dgvContactInformation.Columns("strClientCredentials").DisplayIndex = 4
            dgvContactInformation.Columns("strClientTitle").HeaderText = "Title"
            dgvContactInformation.Columns("strClientTitle").DisplayIndex = 5
            dgvContactInformation.Columns("strClientPhoneNumber").HeaderText = "Phone Number"
            dgvContactInformation.Columns("strClientPhoneNumber").DisplayIndex = 6
            dgvContactInformation.Columns("strClientCellPhone").HeaderText = "Cell Phone"
            dgvContactInformation.Columns("strClientCellPhone").DisplayIndex = 7
            dgvContactInformation.Columns("strClientFax").HeaderText = "Fax"
            dgvContactInformation.Columns("strClientFax").DisplayIndex = 8
            dgvContactInformation.Columns("strClientEmail").HeaderText = "Email"
            dgvContactInformation.Columns("strClientEmail").DisplayIndex = 9
            dgvContactInformation.Columns("strClientAddress").HeaderText = "Address"
            dgvContactInformation.Columns("strClientAddress").DisplayIndex = 10
            dgvContactInformation.Columns("strClientCity").HeaderText = "City"
            dgvContactInformation.Columns("strClientCity").DisplayIndex = 11
            dgvContactInformation.Columns("strClientState").HeaderText = "State"
            dgvContactInformation.Columns("strClientState").DisplayIndex = 12
            dgvContactInformation.Columns("strClientZipCode").HeaderText = "Zip Code"
            dgvContactInformation.Columns("strClientZipCode").DisplayIndex = 13
            dgvContactInformation.Columns("strContactNotes").HeaderText = "Notes"
            dgvContactInformation.Columns("strContactNotes").DisplayIndex = 14

            dgvContactInformation.SanelyResizeColumns
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AddNewContactData()
        Try
            Dim Firstname As String = ""
            Dim LastName As String = ""
            Dim Salutation As String = ""
            Dim Credentials As String = ""
            Dim Title As String = ""
            Dim PhoneNumber As String = ""
            Dim CellPhone As String = ""
            Dim Fax As String = ""
            Dim email As String = ""
            Dim Address As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""
            Dim MainContact As String = ""
            Dim ContactNotes As String = ""

            If txtFirstName.Text <> "" Then
                Firstname = txtFirstName.Text
            Else
                Firstname = ""
            End If
            If txtLastName.Text <> "" Then
                LastName = txtLastName.Text
            Else
                LastName = ""
            End If
            If txtSalutation.Text <> "" Then
                Salutation = txtSalutation.Text
            Else
                Salutation = ""
            End If
            If txtCredentials.Text <> "" Then
                Credentials = txtCredentials.Text
            Else
                Credentials = ""
            End If
            If txtTitle.Text <> "" Then
                Title = txtTitle.Text
            Else
                Title = ""
            End If
            If mtbPhoneNumber.Text <> "" Then
                PhoneNumber = mtbPhoneNumber.Text
            Else
                PhoneNumber = ""
            End If
            If mtbCellPhone.Text <> "" Then
                CellPhone = mtbCellPhone.Text
            Else
                CellPhone = ""
            End If
            If mtbFaxNumber.Text <> "" Then
                Fax = mtbFaxNumber.Text
            Else
                Fax = ""
            End If
            If txtEmail.Text <> "" Then
                email = txtEmail.Text
            Else
                email = ""
            End If
            If txtContactAddress.Text <> "" Then
                Address = txtContactAddress.Text
            Else
                Address = ""
            End If
            If txtContactCity.Text <> "" Then
                City = txtContactCity.Text
            Else
                City = ""
            End If
            If txtContactState.Text <> "" Then
                State = txtContactState.Text
            Else
                State = ""
            End If
            If mtbContactZipCode.Text <> "" Then
                ZipCode = mtbContactZipCode.Text
            Else
                ZipCode = ""
            End If
            If Me.chbMainClientContact.Checked = True Then
                MainContact = "True"
            Else
                MainContact = "False"
            End If
            If txtContactNotes.Text <> "" Then
                ContactNotes = txtContactNotes.Text
            Else
                ContactNotes = ""
            End If

            query = "Insert into SBEAPClientContacts
                    (CLIENTCONTACTID, STRCLIENTFIRSTNAME, STRCLIENTLASTNAME, STRCLIENTSALUTATION, 
                    STRCLIENTCREDENTIALS, STRCLIENTTITLE, STRCLIENTPHONENUMBER, STRCLIENTCELLPHONE, 
                    STRCLIENTFAX, STRCLIENTEMAIL, STRCLIENTADDRESS, STRCLIENTCITY, STRCLIENTSTATE, 
                    STRCLIENTZIPCODE, STRCLIENTCREATOR, DATCLIENTCREATED, STRMODIFINGPERSON, 
                    DATMODIFINGDATE, STRCONTACTNOTES)
                    Select 
                    (Select (max(ClientContactID)+1) from SBEAPClientContacts), 
                    @STRCLIENTFIRSTNAME, @STRCLIENTLASTNAME, @STRCLIENTSALUTATION, 
                    @STRCLIENTCREDENTIALS, @STRCLIENTTITLE, @STRCLIENTPHONENUMBER, @STRCLIENTCELLPHONE, 
                    @STRCLIENTFAX, @STRCLIENTEMAIL, @STRCLIENTADDRESS, @STRCLIENTCITY, @STRCLIENTSTATE, 
                    @STRCLIENTZIPCODE, @STRCLIENTCREATOR, getdate(), @STRMODIFINGPERSON, 
                    getdate(), @STRCONTACTNOTES "

            Dim p As SqlParameter() = {
                    New SqlParameter("@STRCLIENTFIRSTNAME", Firstname),
                    New SqlParameter("@STRCLIENTLASTNAME", LastName),
                    New SqlParameter("@STRCLIENTSALUTATION", Salutation),
                    New SqlParameter("@STRCLIENTCREDENTIALS", Credentials),
                    New SqlParameter("@STRCLIENTTITLE", Title),
                    New SqlParameter("@STRCLIENTPHONENUMBER", PhoneNumber),
                    New SqlParameter("@STRCLIENTCELLPHONE", CellPhone),
                    New SqlParameter("@STRCLIENTFAX", Fax),
                    New SqlParameter("@STRCLIENTEMAIL", email),
                    New SqlParameter("@STRCLIENTADDRESS", Address),
                    New SqlParameter("@STRCLIENTCITY", City),
                    New SqlParameter("@STRCLIENTSTATE", State),
                    New SqlParameter("@STRCLIENTZIPCODE", ZipCode),
                    New SqlParameter("@STRCLIENTCREATOR", CurrentUser.UserID),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                    New SqlParameter("@STRCONTACTNOTES", ContactNotes)
                }

            DB.RunCommand(query, p)

            query = "Select max(ClientContactID) as ClientID " &
                    "from SBEAPClientContacts "
            txtContactID.Text = DB.GetString(query)

            txtContactCreationInfo.Text = CurrentUser.AlphaName & " - " & TodayFormatted
            txtContactLastModified.Text = CurrentUser.AlphaName & " - " & TodayFormatted

            query = "Select " &
            "ClientID " &
            "from SBEAPClientLink " &
            "where clientID = @clientid " &
            "and ClientContactID = @contactid "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@clientid", txtClientID.Text),
                New SqlParameter("@contactid", txtContactID.Text)
            }

            If Not DB.GetBoolean(query, p2) Then
                query = "Insert into SBEAPClientLink " &
                    "(CLIENTID, CLIENTCONTACTID, STRMAINCONTACT) " &
                    "values " &
                    "(@CLIENTID, @CLIENTCONTACTID, @STRMAINCONTACT) "
            Else
                query = "Update SBEAPClientLink set " &
                    "strMainContact = @STRMAINCONTACT " &
                    "where ClientID = @CLIENTID " &
                    "and ClientContactID = @CLIENTCONTACTID "
            End If

            Dim p3 As SqlParameter() = {
                New SqlParameter("@CLIENTID", txtClientID.Text),
                New SqlParameter("@CLIENTCONTACTID", txtContactID.Text),
                New SqlParameter("@STRMAINCONTACT", MainContact)
            }

            DB.RunCommand(query, p3)

            LoadContactData()
            ClearContactData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UpdateContactData()
        Try
            Dim Firstname As String = ""
            Dim LastName As String = ""
            Dim Salutation As String = ""
            Dim Credentials As String = ""
            Dim Title As String = ""
            Dim PhoneNumber As String = ""
            Dim CellPhone As String = ""
            Dim Fax As String = ""
            Dim email As String = ""
            Dim Address As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""
            Dim MainContact As String = ""
            Dim ContactNotes As String = ""

            If txtFirstName.Text <> "" Then
                Firstname = txtFirstName.Text
            Else
                Firstname = ""
            End If
            If txtLastName.Text <> "" Then
                LastName = txtLastName.Text
            Else
                LastName = ""
            End If
            If txtSalutation.Text <> "" Then
                Salutation = txtSalutation.Text
            Else
                Salutation = ""
            End If
            If txtCredentials.Text <> "" Then
                Credentials = txtCredentials.Text
            Else
                Credentials = ""
            End If
            If txtTitle.Text <> "" Then
                Title = txtTitle.Text
            Else
                Title = ""
            End If
            If mtbPhoneNumber.Text <> "" Then
                PhoneNumber = mtbPhoneNumber.Text
            Else
                PhoneNumber = ""
            End If
            If mtbCellPhone.Text <> "" Then
                CellPhone = mtbCellPhone.Text
            Else
                CellPhone = ""
            End If
            If mtbFaxNumber.Text <> "" Then
                Fax = mtbFaxNumber.Text
            Else
                Fax = ""
            End If
            If txtEmail.Text <> "" Then
                email = txtEmail.Text
            Else
                email = ""
            End If
            If txtContactAddress.Text <> "" Then
                Address = txtContactAddress.Text
            Else
                Address = ""
            End If
            If txtContactCity.Text <> "" Then
                City = txtContactCity.Text
            Else
                City = ""
            End If
            If txtContactState.Text <> "" Then
                State = txtContactState.Text
            Else
                State = ""
            End If
            If mtbContactZipCode.Text <> "" Then
                ZipCode = mtbContactZipCode.Text
            Else
                ZipCode = ""
            End If
            If Me.chbMainClientContact.Checked = True Then
                MainContact = "True"
            Else
                MainContact = "False"
            End If
            If txtContactNotes.Text <> "" Then
                ContactNotes = txtContactNotes.Text
            Else
                ContactNotes = ""
            End If

            If txtContactID.Text = "" Then
                MsgBox("Either click 'Add New Contact' or select the contact from the table below before editing the Contact Information.", MsgBoxStyle.Information,
                       "Edit Contact Data")
            Else
                query = "Update SBEAPClientContacts set " &
                "strClientFirstName = @strClientFirstName, " &
                "strClientLastName = @strClientLastName, " &
                "strClientSalutation = @strClientSalutation, " &
                "strClientCredentials = @strClientCredentials, " &
                "strClientTitle = @strClientTitle, " &
                "strClientPhoneNumber = @strClientPhoneNumber, " &
                "strClientCellPhone = @strClientCellPhone, " &
                "strClientFax = @strClientFax, " &
                "strClientEmail = @strClientEmail, " &
                "strClientAddress = @strClientAddress, " &
                "strClientCity = @strClientCity, " &
                "strClientState = @strClientState, " &
                "strClientZipCode = @strClientZipCode, " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingDate = GETDATE(), " &
                "strContactNotes = @strContactNotes " &
                "where ClientContactID = @ClientContactID "

                Dim p As SqlParameter() = {
                    New SqlParameter("@strClientFirstName", Firstname),
                    New SqlParameter("@strClientLastName", LastName),
                    New SqlParameter("@strClientSalutation", Salutation),
                    New SqlParameter("@strClientCredentials", Credentials),
                    New SqlParameter("@strClientTitle", Title),
                    New SqlParameter("@strClientPhoneNumber", PhoneNumber),
                    New SqlParameter("@strClientCellPhone", CellPhone),
                    New SqlParameter("@strClientFax", Fax),
                    New SqlParameter("@strClientEmail", email),
                    New SqlParameter("@strClientAddress", Address),
                    New SqlParameter("@strClientCity", City),
                    New SqlParameter("@strClientState", State),
                    New SqlParameter("@strClientZipCode", ZipCode),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@strContactNotes", ContactNotes),
                    New SqlParameter("@ClientContactID", txtContactID.Text)
                }

                DB.RunCommand(query, p)

                txtContactLastModified.Text = CurrentUser.AlphaName & " - " & TodayFormatted

                query = "Select " &
                "ClientID " &
                "from SBEAPClientLink " &
                "where clientID = @clientID " &
                "and ClientContactID = @ClientContactID "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@clientID", txtClientID.Text),
                    New SqlParameter("@ClientContactID", txtContactID.Text)
                }


                If Not DB.GetBoolean(query, p2) Then
                    query = "Insert into SBEAPClientLink " &
                    "(CLIENTID, CLIENTCONTACTID, STRMAINCONTACT) " &
                    "values " &
                    "(@CLIENTID, @CLIENTCONTACTID, @STRMAINCONTACT) "
                Else
                    query = "Update SBEAPClientLink set " &
                    "strMainContact = @STRMAINCONTACT " &
                    "where ClientID = @CLIENTID " &
                    "and ClientContactID = @CLIENTCONTACTID "
                End If

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@CLIENTID", txtClientID.Text),
                    New SqlParameter("@CLIENTCONTACTID", txtContactID.Text),
                    New SqlParameter("@STRMAINCONTACT", MainContact)
                }

                DB.RunCommand(query, p3)

                LoadContactData()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearContactData()
        Try
            txtContactID.Clear()
            txtFirstName.Clear()
            txtLastName.Clear()
            txtSalutation.Clear()
            txtCredentials.Clear()
            txtTitle.Clear()
            mtbPhoneNumber.Clear()
            mtbCellPhone.Clear()
            mtbFaxNumber.Clear()
            txtEmail.Clear()
            txtContactAddress.Clear()
            txtContactCity.Clear()
            txtContactState.Clear()
            mtbContactZipCode.Clear()
            txtContactCreationInfo.Clear()
            txtContactLastModified.Clear()
            chbMainClientContact.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadContact()
        Try

            query = "select " &
            "strClientFirstName, strClientLastName, " &
            "strClientSalutation, strClientCredentials, " &
            "strClientTitle, strClientPhoneNumber, " &
            "strClientCellPhone, strClientFax, " &
            "strClientEmail, strClientAddress, " &
            "strClientCity, strClientState, " &
            "strClientZipCode, " &
            "(select concat(strLastName,', ',strFirstName) as Creator " &
            "from SBEAPClientContacts, EPDUserProfiles " &
            "where SBEAPClientContacts.strClientCreator = EPDUserProfiles.numUserID " &
            "and ClientContactID = @contactid ) as Creator, " &
            "format(datClientCreated , 'MMMM d, yyyy') as datClientCreated, " &
            "(select concat(strLastName,', ',strFirstName) as Modifier " &
            "from SBEAPClientContacts, EPDUserProfiles " &
            "where SBEAPClientContacts.strClientCreator = EPDUserProfiles.numUserID " &
            "and ClientContactID = @contactid) as Modifier, " &
            "format(datModifingDate , 'MMMM d, yyyy') as datModifingDate,  " &
            "strMainContact, strContactNotes " &
            "from SBEAPClientContacts, SBEAPClientLink " &
            "where SBEAPClientContacts.ClientContactID = SBEAPClientLink.ClientContactID " &
            "and SBEAPClientContacts.ClientContactID = @contactid "

            Dim p As New SqlParameter("@contactid", txtContactID.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strClientFirstName")) Then
                    txtFirstName.Text = ""
                Else
                    txtFirstName.Text = dr.Item("strClientFirstName")
                End If
                If IsDBNull(dr.Item("strClientLastName")) Then
                    txtLastName.Text = ""
                Else
                    txtLastName.Text = dr.Item("strClientLastName")
                End If
                If IsDBNull(dr.Item("strClientSalutation")) Then
                    txtSalutation.Text = ""
                Else
                    txtSalutation.Text = dr.Item("strClientSalutation")
                End If
                If IsDBNull(dr.Item("strClientCredentials")) Then
                    txtCredentials.Text = ""
                Else
                    txtCredentials.Text = dr.Item("strClientCredentials")
                End If
                If IsDBNull(dr.Item("strClientTitle")) Then
                    txtTitle.Text = ""
                Else
                    txtTitle.Text = dr.Item("strClientTitle")
                End If
                If IsDBNull(dr.Item("strClientPhoneNumber")) Then
                    mtbPhoneNumber.Text = ""
                Else
                    mtbPhoneNumber.Text = dr.Item("strClientPhoneNumber")
                End If
                If IsDBNull(dr.Item("strClientCellPhone")) Then
                    mtbCellPhone.Text = ""
                Else
                    mtbCellPhone.Text = dr.Item("strClientCellPhone")
                End If
                If IsDBNull(dr.Item("strClientFax")) Then
                    mtbFaxNumber.Text = ""
                Else
                    mtbFaxNumber.Text = dr.Item("strClientFax")
                End If
                If IsDBNull(dr.Item("strClientEmail")) Then
                    txtEmail.Text = ""
                Else
                    txtEmail.Text = dr.Item("strClientEmail")
                End If
                If IsDBNull(dr.Item("strClientAddress")) Then
                    txtContactAddress.Text = ""
                Else
                    txtContactAddress.Text = dr.Item("strClientAddress")
                End If
                If IsDBNull(dr.Item("strClientCity")) Then
                    txtContactCity.Text = ""
                Else
                    txtContactCity.Text = dr.Item("strClientCity")
                End If
                If IsDBNull(dr.Item("strClientState")) Then
                    txtContactState.Text = ""
                Else
                    txtContactState.Text = dr.Item("strClientState")
                End If
                If IsDBNull(dr.Item("strClientZipCode")) Then
                    mtbContactZipCode.Text = ""
                Else
                    mtbContactZipCode.Text = dr.Item("strClientZipCode")
                End If
                If IsDBNull(dr.Item("Creator")) Then
                    txtContactCreationInfo.Text = ""
                Else
                    txtContactCreationInfo.Text = dr.Item("Creator")
                End If
                If IsDBNull(dr.Item("datClientCreated")) Then
                    txtContactCreationInfo.Text = txtContactCreationInfo.Text & " - "
                Else
                    txtContactCreationInfo.Text = txtContactCreationInfo.Text & " - " & dr.Item("datClientCreated")
                End If
                If IsDBNull(dr.Item("Modifier")) Then
                    txtContactLastModified.Text = ""
                Else
                    txtContactLastModified.Text = dr.Item("Modifier")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    txtContactLastModified.Text = txtContactLastModified.Text & " - "
                Else
                    txtContactLastModified.Text = txtContactLastModified.Text & " - " & dr.Item("datModifingDate")
                End If
                If IsDBNull(dr.Item("strMainContact")) Then
                    chbMainClientContact.Checked = False
                Else
                    If dr.Item("strMainContact") = "True" Then
                        chbMainClientContact.Checked = True
                    Else
                        chbMainClientContact.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strContactNotes")) Then
                    txtContactNotes.Clear()
                Else
                    txtContactNotes.Text = dr.Item("strContactNotes")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub GetNextClientID()
        Try

            Dim CurrYear As String = ""
            CurrYear = Now.Year.ToString

            query = "Select max(ClientID) as MaxClientID " &
            "from SBEAPClients " &
            "where CONVERT(int, ClientID) like @year "
            Dim p As New SqlParameter("@year", CurrYear & "%")

            Dim dr As DataRow = DB.GetDataRow(query, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("MaxClientID")) Then
                    txtClientID.Text = CurrYear & "00001"
                Else
                    txtClientID.Text = dr.Item("MaxClientID") + 1
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveClientData()
        Try
            Dim Status As String = ""
            Dim ClientName As String = ""
            Dim StartDate As String = ""
            Dim ClientAddress As String = ""
            Dim ClientAddress2 As String = ""
            Dim ClientCity As String = ""
            Dim ClientState As String = ""
            Dim ClientZipCode As String = ""
            Dim ClientCounty As String = ""
            Dim ClientLatitude As String = ""
            Dim ClientLongitude As String = ""
            Dim MailingAddress As String = ""
            Dim MailingAddress2 As String = ""
            Dim MailingCity As String = ""
            Dim MailingState As String = ""
            Dim MailingZipCode As String = ""
            Dim ClientCreator As String = ""

            Dim ClientDescription As String = ""
            Dim WebSite As String = ""
            Dim SIC As String = ""
            Dim NAICS As String = ""
            Dim Employees As String = ""
            Dim AIRSNumber As String = ""
            Dim AirCodes As String = ""
            Dim StateCodes As String = ""
            Dim SSCPEngineer As String = ""
            Dim SSCPUnit As String = ""
            Dim SSPPEngineer As String = ""
            Dim SSPPUnit As String = ""
            Dim ISMPEngineer As String = ""
            Dim ISMPUnit As String = ""
            Dim AIRPermit As String = ""
            Dim AirDescription As String = ""

            If txtCompanyName.Text <> "" Then
                ClientName = txtCompanyName.Text
            Else
                ClientName = ""
            End If
            StartDate = dtpStartDate.Text
            If txtStreetAddress.Text <> "" Then
                ClientAddress = txtStreetAddress.Text
            Else
                ClientAddress = ""
            End If
            If txtStreetAddress2.Text <> "" Then
                ClientAddress2 = txtStreetAddress2.Text
            Else
                ClientAddress2 = ""
            End If
            If Me.txtCity.Text <> "" Then
                ClientCity = txtCity.Text
            Else
                ClientCity = ""
            End If
            If txtState.Text <> "" Then
                ClientState = txtState.Text
            Else
                ClientState = ""
            End If
            If Me.mtbZipCode.Text <> "" Then
                ClientZipCode = mtbZipCode.Text
            Else
                ClientZipCode = ""
            End If
            If cboCounty.Text <> "" Then
                ClientCounty = cboCounty.SelectedValue
            Else
                ClientCounty = ""
            End If
            If mtbLatitude.Text <> "" Then
                ClientLatitude = mtbLatitude.Text
            Else
                ClientLatitude = ""
            End If
            If mtbLongitude.Text <> "" Then
                ClientLongitude = mtbLongitude.Text
            Else
                ClientLongitude = ""
            End If
            If txtMailingAddress.Text <> "" And txtMailingAddress.Text <> "<Mailing Address 1>" Then
                MailingAddress = txtMailingAddress.Text
            Else
                MailingAddress = ""
            End If
            If txtMailingAddress2.Text <> "" And txtMailingAddress2.Text <> "<Mailing Address 2>" Then
                MailingAddress2 = txtMailingAddress2.Text
            Else
                MailingAddress2 = ""
            End If
            If txtMailingCity.Text <> "" And txtMailingCity.Text <> "<Mailing City>" Then
                MailingCity = txtMailingCity.Text
            Else
                MailingCity = ""
            End If
            If txtMailingState.Text <> "" Then
                MailingState = txtMailingState.Text
            Else
                MailingState = ""
            End If
            If mtbMailingZipCode.Text <> "" Then
                MailingZipCode = mtbMailingZipCode.Text
            Else
                MailingZipCode = ""
            End If

            If txtDescription.Text <> "" Then
                ClientDescription = txtDescription.Text
            Else
                ClientDescription = ""
            End If
            If txtWebSite.Text <> "" Then
                WebSite = txtWebSite.Text
            Else
                WebSite = ""
            End If
            If mtbSIC.Text <> "" Then
                SIC = mtbSIC.Text
            Else
                SIC = ""
            End If
            If mtbNAICS.Text <> "" Then
                NAICS = mtbNAICS.Text
            Else
                NAICS = ""
            End If
            If mtbNumberOfEmployees.Text <> "" Then
                Employees = mtbNumberOfEmployees.Text
            Else
                Employees = ""
            End If
            If mtbAIRSNumber.Text <> "" Then
                AIRSNumber = mtbAIRSNumber.Text
            Else
                AIRSNumber = ""
            End If

            If chbSIP.Checked = True Then
                AirCodes = "1"
            Else
                AirCodes = "0"
            End If
            If chbFederalSIP.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNonFedSIP.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbCFCTracking.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbPSD.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNSR.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNESHAP.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNSPS.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbAcidPrecip.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbFESOP.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNativeAmer.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbMACT.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbTitleV.Checked = True Then
                AirCodes = AirCodes & "1"
            Else
                AirCodes = AirCodes & "0"
            End If
            If chbNSRPSD.Checked = True Then
                StateCodes = "1"
            Else
                StateCodes = "0"
            End If
            If chbHAPs.Checked = True Then
                StateCodes = StateCodes & "1"
            Else
                StateCodes = StateCodes & "0"
            End If
            If txtAIRPermitNumber.Text <> "" Then
                AIRPermit = txtAIRPermitNumber.Text
            Else
                AIRPermit = ""
            End If
            If txtSSCPContact.Text <> "" Then
                SSCPEngineer = txtSSCPContact.Text
            Else
                SSCPEngineer = ""
            End If
            If txtSSCPUnit.Text <> "" Then
                SSCPUnit = txtSSCPUnit.Text
            Else
                SSCPUnit = ""
            End If
            If txtSSPPContact.Text <> "" Then
                SSPPEngineer = txtSSPPContact.Text
            Else
                SSPPEngineer = ""
            End If
            If txtSSPPUnit.Text <> "" Then
                SSPPUnit = txtSSPPUnit.Text
            Else
                SSPPUnit = ""
            End If
            If txtISMPContact.Text <> "" Then
                ISMPEngineer = txtISMPContact.Text
            Else
                ISMPEngineer = ""
            End If
            If txtISMPUnit.Text <> "" Then
                ISMPUnit = txtISMPUnit.Text
            Else
                ISMPUnit = ""
            End If
            If txtAirDescription.Text <> "" Then
                AirDescription = txtAirDescription.Text
            Else
                AirDescription = ""
            End If


            If txtClientID.Text = "" Then
                Status = "Insert"

                Dim Result As DialogResult

                If DAL.Sbeap.ClientNameExists(txtCompanyName.Text) Then
                    Result = MessageBox.Show("There is a client with this name in the system." & vbCrLf & "Do you still want to create a new client?",
                      "Client Summary New Client", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                    Select Case Result
                        Case DialogResult.Yes
                        Case Else
                            Exit Sub
                    End Select
                End If
            Else
                If DAL.Sbeap.ClientExists(txtClientID.Text) Then
                    Status = "Update"
                Else
                    Status = "Insert"
                End If
            End If

            If Status = "Insert" Then
                If txtClientID.Text = "" Then
                    ClientCreator = CurrentUser.UserID
                    GetNextClientID()

                    query = "INSERT INTO SBEAPCLIENTS
                        ( CLIENTID
                        , STRCOMPANYNAME
                        , DATSTARTDATE
                        , STRCOMPANYADDRESS
                        , STRCOMPANYADDRESS2
                        , STRCOMPANYCITY
                        , STRCOMPANYSTATE
                        , STRCOMPANYZIPCODE
                        , STRCOMPANYCOUNTY
                        , STRCOMPANYLATITUDE
                        , STRCOMPANYLONGITUDE
                        , STRMAILINGADDRESS
                        , STRMAILINGADDRESS2
                        , STRMAILINGCITY
                        , STRMAILINGSTATE
                        , STRMAILINGZIPCODE
                        , STRCOMPANYCREATOR
                        , DATCOMPANYCREATED
                        , STRMODIFINGPERSON
                        , DATMODIFINGDATE
                        )
                        VALUES
                        ( @CLIENTID
                        , @STRCOMPANYNAME
                        , @DATSTARTDATE
                        , @STRCOMPANYADDRESS
                        , @STRCOMPANYADDRESS2
                        , @STRCOMPANYCITY
                        , @STRCOMPANYSTATE
                        , @STRCOMPANYZIPCODE
                        , @STRCOMPANYCOUNTY
                        , @STRCOMPANYLATITUDE
                        , @STRCOMPANYLONGITUDE
                        , @STRMAILINGADDRESS
                        , @STRMAILINGADDRESS2
                        , @STRMAILINGCITY
                        , @STRMAILINGSTATE
                        , @STRMAILINGZIPCODE
                        , @STRCOMPANYCREATOR
                        , getdate()
                        , @STRMODIFINGPERSON
                        , getdate()
                        )"

                    Dim p As SqlParameter() = {
                        New SqlParameter("@CLIENTID", txtClientID.Text),
                        New SqlParameter("@STRCOMPANYNAME", ClientName),
                        New SqlParameter("@DATSTARTDATE", StartDate),
                        New SqlParameter("@STRCOMPANYADDRESS", ClientAddress),
                        New SqlParameter("@STRCOMPANYADDRESS2", ClientAddress2),
                        New SqlParameter("@STRCOMPANYCITY", ClientCity),
                        New SqlParameter("@STRCOMPANYSTATE", ClientState),
                        New SqlParameter("@STRCOMPANYZIPCODE", ClientZipCode),
                        New SqlParameter("@STRCOMPANYCOUNTY", ClientCounty),
                        New SqlParameter("@STRCOMPANYLATITUDE", ClientLatitude),
                        New SqlParameter("@STRCOMPANYLONGITUDE", ClientLongitude),
                        New SqlParameter("@STRMAILINGADDRESS", MailingAddress),
                        New SqlParameter("@STRMAILINGADDRESS2", MailingAddress2),
                        New SqlParameter("@STRMAILINGCITY", MailingCity),
                        New SqlParameter("@STRMAILINGSTATE", MailingState),
                        New SqlParameter("@STRMAILINGZIPCODE", MailingZipCode),
                        New SqlParameter("@STRCOMPANYCREATOR", ClientCreator),
                        New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID)
                    }

                    DB.RunCommand(query, p)

                    query = "Select max(clientID) as MaxID " &
                    "from SBEAPClients "

                    txtClientID.Text = DB.GetInteger(query)

                    query = "INSERT INTO SBEAPCLIENTDATA
                        ( CLIENTID
                        , STRCLIENTDESCRIPTION
                        , STRCLIENTWEBSITE
                        , STRCLIENTSIC
                        , STRCLIENTNAICS
                        , STRCLIENTEMPLOYEES
                        , STRAIRSNUMBER
                        , STRAIRPROGRAMCODES
                        , STRSTATEPROGRAMCODES
                        , STRMODIFINGPERSON
                        , DATMODIFINGDATE
                        , STRAIRPERMITNUMBER
                        , STRSSCPENGINEER
                        , STRSSCPUNIT
                        , STRSSPPENGINEER
                        , STRSSPPUNIT
                        , STRISMPENGINEER
                        , STRISMPUNIT
                        , STRAIRDESCRIPTION
                        )
                        VALUES
                        ( @CLIENTID
                        , @STRCLIENTDESCRIPTION
                        , @STRCLIENTWEBSITE
                        , @STRCLIENTSIC
                        , @STRCLIENTNAICS
                        , @STRCLIENTEMPLOYEES
                        , @STRAIRSNUMBER
                        , @STRAIRPROGRAMCODES
                        , @STRSTATEPROGRAMCODES
                        , @STRMODIFINGPERSON
                        , getdate()
                        , @STRAIRPERMITNUMBER
                        , @STRSSCPENGINEER
                        , @STRSSCPUNIT
                        , @STRSSPPENGINEER
                        , @STRSSPPUNIT
                        , @STRISMPENGINEER
                        , @STRISMPUNIT
                        , @STRAIRDESCRIPTION
                        )"

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@CLIENTID", txtClientID.Text),
                        New SqlParameter("@STRCLIENTDESCRIPTION", ClientDescription),
                        New SqlParameter("@STRCLIENTWEBSITE", WebSite),
                        New SqlParameter("@STRCLIENTSIC", SIC),
                        New SqlParameter("@STRCLIENTNAICS", NAICS),
                        New SqlParameter("@STRCLIENTEMPLOYEES", Employees),
                        New SqlParameter("@STRAIRSNUMBER", AIRSNumber),
                        New SqlParameter("@STRAIRPROGRAMCODES", AirCodes),
                        New SqlParameter("@STRSTATEPROGRAMCODES", StateCodes),
                        New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                        New SqlParameter("@STRAIRPERMITNUMBER", AIRPermit),
                        New SqlParameter("@STRSSCPENGINEER", SSCPEngineer),
                        New SqlParameter("@STRSSCPUNIT", SSCPUnit),
                        New SqlParameter("@STRSSPPENGINEER", SSPPEngineer),
                        New SqlParameter("@STRSSPPUNIT", SSPPUnit),
                        New SqlParameter("@STRISMPENGINEER", ISMPEngineer),
                        New SqlParameter("@STRISMPUNIT", ISMPUnit),
                        New SqlParameter("@STRAIRDESCRIPTION", AirDescription)
                    }

                    DB.RunCommand(query, p2)
                Else
                    MsgBox("The Client ID box currnetly has a value.", MsgBoxStyle.Information, "Client Maintenance")
                End If
            Else
                query = "Update SBEAPClients set " &
                    "strCompanyName = @STRCOMPANYNAME, " &
                    "datStartDate = @DATSTARTDATE, " &
                    "strCompanyaddress = @STRCOMPANYADDRESS, " &
                    "strCompanyAddress2 = @STRCOMPANYADDRESS2, " &
                    "strCompanyCity = @STRCOMPANYCITY, " &
                    "strCompanyState = @STRCOMPANYSTATE, " &
                    "strCompanyZipCode = @STRCOMPANYZIPCODE, " &
                    "strCompanyCounty = @STRCOMPANYCOUNTY, " &
                    "strCompanyLatitude = @STRCOMPANYLATITUDE, " &
                    "strCompanyLongitude = @STRCOMPANYLONGITUDE, " &
                    "strMailingAddress = @STRMAILINGADDRESS, " &
                    "strMailingAddress2 = @STRMAILINGADDRESS2, " &
                    "strMailingCity = @STRMAILINGCITY, " &
                    "strMailingState = @STRMAILINGSTATE, " &
                    "strMailingZipCode = @STRMAILINGZIPCODE, " &
                    "strModifingPerson = @STRMODIFINGPERSON, " &
                    "datModifingDate = GETDATE() " &
                    "where ClientID = @CLIENTID "

                Dim p As SqlParameter() = {
                    New SqlParameter("@CLIENTID", txtClientID.Text),
                    New SqlParameter("@STRCOMPANYNAME", ClientName),
                    New SqlParameter("@DATSTARTDATE", StartDate),
                    New SqlParameter("@STRCOMPANYADDRESS", ClientAddress),
                    New SqlParameter("@STRCOMPANYADDRESS2", ClientAddress2),
                    New SqlParameter("@STRCOMPANYCITY", ClientCity),
                    New SqlParameter("@STRCOMPANYSTATE", ClientState),
                    New SqlParameter("@STRCOMPANYZIPCODE", ClientZipCode),
                    New SqlParameter("@STRCOMPANYCOUNTY", ClientCounty),
                    New SqlParameter("@STRCOMPANYLATITUDE", ClientLatitude),
                    New SqlParameter("@STRCOMPANYLONGITUDE", ClientLongitude),
                    New SqlParameter("@STRMAILINGADDRESS", MailingAddress),
                    New SqlParameter("@STRMAILINGADDRESS2", MailingAddress2),
                    New SqlParameter("@STRMAILINGCITY", MailingCity),
                    New SqlParameter("@STRMAILINGSTATE", MailingState),
                    New SqlParameter("@STRMAILINGZIPCODE", MailingZipCode),
                    New SqlParameter("@STRCOMPANYCREATOR", ClientCreator),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID)
                }

                DB.RunCommand(query, p)

                query = "Update SBEAPClientData set " &
                    "strClientDescription = @STRCLIENTDESCRIPTION, " &
                    "strClientWebSite = @STRCLIENTWEBSITE, " &
                    "strClientSIC = @STRCLIENTSIC, " &
                    "strClientNAICS = @STRCLIENTNAICS, " &
                    "strClientEmployees = @STRCLIENTEMPLOYEES, " &
                    "strAIRSNumber = @STRAIRSNUMBER, " &
                    "strAirProgramCodes = @STRAIRPROGRAMCODES, " &
                    "strStateProgramCodes = @STRSTATEPROGRAMCODES, " &
                    "strModifingperson = @STRMODIFINGPERSON, " &
                    "strAIRPermitNumber = @STRAIRPERMITNUMBER, " &
                    "strSSCPEngineer = @STRSSCPENGINEER, " &
                    "strSSCPUnit = @STRSSCPUNIT, " &
                    "strSSPPEngineer = @STRSSPPENGINEER, " &
                    "strSSPPUnit = @STRSSPPUNIT, " &
                    "strISMPEngineer = @STRISMPENGINEER, " &
                    "strISMPUnit = @STRISMPUNIT, " &
                    "strAirDescription = @STRAIRDESCRIPTION, " &
                    "datModifingDate = GETDATE() " &
                    "where ClientID = @CLIENTID "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@CLIENTID", txtClientID.Text),
                    New SqlParameter("@STRCLIENTDESCRIPTION", ClientDescription),
                    New SqlParameter("@STRCLIENTWEBSITE", WebSite),
                    New SqlParameter("@STRCLIENTSIC", SIC),
                    New SqlParameter("@STRCLIENTNAICS", NAICS),
                    New SqlParameter("@STRCLIENTEMPLOYEES", Employees),
                    New SqlParameter("@STRAIRSNUMBER", AIRSNumber),
                    New SqlParameter("@STRAIRPROGRAMCODES", AirCodes),
                    New SqlParameter("@STRSTATEPROGRAMCODES", StateCodes),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                    New SqlParameter("@STRAIRPERMITNUMBER", AIRPermit),
                    New SqlParameter("@STRSSCPENGINEER", SSCPEngineer),
                    New SqlParameter("@STRSSCPUNIT", SSCPUnit),
                    New SqlParameter("@STRSSPPENGINEER", SSPPEngineer),
                    New SqlParameter("@STRSSPPUNIT", SSPPUnit),
                    New SqlParameter("@STRISMPENGINEER", ISMPEngineer),
                    New SqlParameter("@STRISMPUNIT", ISMPUnit),
                    New SqlParameter("@STRAIRDESCRIPTION", AirDescription)
                }

                DB.RunCommand(query, p2)
            End If

            MsgBox("Done", MsgBoxStyle.Information, "SBEAP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RefreshAIRSData()
        Try
            Dim AirProgramCode As String = "000000000000000"
            Dim StateProgram As String = "00"

            query = "select " &
            "strSICcode, strNAICSCode, " &
            "strPlantDescription, " &
            "strAIRProgramCodes, " &
            "strStateProgramCodes " &
            "from APBHeaderData " &
            "where strAIRSNumber = @airs "

            Dim p As New SqlParameter("@airs", "0413" & mtbAIRSNumber.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strSICCode")) Then
                    mtbSIC.Clear()
                Else
                    mtbSIC.Text = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    mtbNAICS.Clear()
                Else
                    mtbNAICS.Text = dr.Item("strNAICSCode")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    txtAirDescription.Clear()
                Else
                    txtAirDescription.Text = dr.Item("strPlantDescription")
                End If
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AirProgramCode = "00000 00000 00000"
                Else
                    AirProgramCode = dr.Item("strAIRProgramCodes")
                End If
                If IsDBNull(dr.Item("strStateProgramCodes")) Then
                    StateProgram = "00"
                Else
                    StateProgram = dr.Item("strStateProgramCodes")
                End If
            End If

            If Mid(AirProgramCode, 1, 1) = "1" Then
                chbSIP.Checked = True
            Else
                chbSIP.Checked = False
            End If
            If Mid(AirProgramCode, 2, 1) = "1" Then
                chbFederalSIP.Checked = True
            Else
                chbFederalSIP.Checked = False
            End If
            If Mid(AirProgramCode, 3, 1) = "1" Then
                chbNonFedSIP.Checked = True
            Else
                chbNonFedSIP.Checked = False
            End If
            If Mid(AirProgramCode, 4, 1) = "1" Then
                chbCFCTracking.Checked = True
            Else
                chbCFCTracking.Checked = False
            End If

            If Mid(AirProgramCode, 5, 1) = "1" Then
                chbPSD.Checked = True
            Else
                chbPSD.Checked = False
            End If
            If Mid(AirProgramCode, 6, 1) = "1" Then
                chbNSR.Checked = True
            Else
                chbNSR.Checked = False
            End If
            If Mid(AirProgramCode, 7, 1) = "1" Then
                chbNESHAP.Checked = True
            Else
                chbNESHAP.Checked = False
            End If
            If Mid(AirProgramCode, 8, 1) = "1" Then
                chbNSPS.Checked = True
            Else
                chbNSPS.Checked = False
            End If
            If Mid(AirProgramCode, 9, 1) = "1" Then
                chbAcidPrecip.Checked = True
            Else
                chbAcidPrecip.Checked = False
            End If
            If Mid(AirProgramCode, 10, 1) = "1" Then
                chbFESOP.Checked = True
            Else
                chbFESOP.Checked = False
            End If
            If Mid(AirProgramCode, 11, 1) = "1" Then
                chbNativeAmer.Checked = True
            Else
                chbNativeAmer.Checked = False
            End If
            If Mid(AirProgramCode, 12, 1) = "1" Then
                chbMACT.Checked = True
            Else
                chbMACT.Checked = False
            End If
            If Mid(AirProgramCode, 13, 1) = "1" Then
                chbTitleV.Checked = True
            Else
                chbTitleV.Checked = False
            End If
            If Mid(StateProgram, 1, 1) = "1" Then
                chbNSRPSD.Checked = True
            Else
                chbNSRPSD.Checked = False
            End If
            If Mid(StateProgram, 2, 1) = "1" Then
                chbHAPs.Checked = True
            Else
                chbHAPs.Checked = False
            End If

            If txtStreetAddress.Text = "" Then
                query = "select " &
                "strFacilityStreet1, strFacilityStreet2, " &
                "strFacilityCity, strFacilityState, " &
                "strFacilityZipCode, numFacilityLongitude, " &
                "numFacilityLatitude " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @airs "

                Dim dr2 As DataRow = DB.GetDataRow(query, p)

                If dr2 IsNot Nothing Then
                    If IsDBNull(dr2.Item("strFacilityStreet1")) Then
                    Else
                        txtStreetAddress.Text = dr2.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr2.Item("strFacilityStreet2")) Then
                    Else
                        txtStreetAddress2.Text = dr2.Item("strFacilityStreet2")
                    End If
                    If IsDBNull(dr2.Item("strFacilityCity")) Then

                    Else
                        txtCity.Text = dr2.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr2.Item("strFacilityState")) Then

                    Else
                        txtState.Text = dr2.Item("strFacilityState")
                    End If
                    If IsDBNull(dr2.Item("strFacilityZipCode")) Then

                    Else
                        mtbZipCode.Text = dr2.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr2.Item("numFacilityLongitude")) Then

                    Else
                        mtbLongitude.Text = dr2.Item("numFacilityLongitude")
                    End If
                    If IsDBNull(dr2.Item("numFacilityLatitude")) Then

                    Else
                        mtbLatitude.Text = dr2.Item("numFacilityLatitude")
                    End If
                    cboCounty.SelectedValue = Mid(mtbAIRSNumber.Text, 1, 3)
                End If
            End If

            query = "SELECT DISTINCT
                       CONCAT(p.STRLASTNAME, ', ', p.STRFIRSTNAME) AS ISMPEngineer
                     , u.STRUNITDESC
                FROM   EPDUSERPROFILES AS p
                LEFT JOIN LOOKUPEPDUNITS AS u
                  ON p.NUMUNIT = u.NUMUNITCODE
                INNER JOIN ISMPREPORTINFORMATION AS i
                  ON p.NUMUSERID = i.STRREVIEWINGENGINEER
                INNER JOIN ISMPMASTER AS m
                  ON i.STRREFERENCENUMBER = m.STRREFERENCENUMBER
                WHERE  i.STRCLOSED = 'True'
                       AND m.STRAIRSNUMBER = @airs
                       AND i.datCompleteDate =
                (
                    SELECT DISTINCT
                           MAX(i.DATCOMPLETEDATE) AS CompleteDate
                    FROM   ISMPREPORTINFORMATION AS i
                    INNER JOIN ISMPMASTER AS m
                      ON i.STRREFERENCENUMBER = m.STRREFERENCENUMBER
                    WHERE  m.STRAIRSNUMBER = @airs
                           AND i.STRCLOSED = 'True'
                )"

            Dim dr3 As DataRow = DB.GetDataRow(query, p)

            If dr3 IsNot Nothing Then
                If IsDBNull(dr3.Item("ISMPEngineer")) Then
                    txtISMPContact.Clear()
                Else
                    txtISMPContact.Text = dr3.Item("ISMPEngineer")
                End If
                If IsDBNull(dr3.Item("strUnitDesc")) Then
                    txtISMPUnit.Clear()
                Else
                    txtISMPUnit.Text = dr3.Item("strUnitDesc")
                End If
            Else
                txtISMPContact.Clear()
                txtISMPUnit.Clear()
            End If

            query = "SELECT DISTINCT
                       CONCAT(p.STRLASTNAME, ', ', p.STRFIRSTNAME) AS SSPPStaffResponsible
                     , u.STRUNITDESC
                FROM   EPDUSERPROFILES AS p
                INNER JOIN SSPPAPPLICATIONMASTER AS m
                  ON p.NUMUSERID = m.STRSTAFFRESPONSIBLE
                INNER JOIN LOOKUPEPDUNITS AS u
                  ON p.NUMUNIT = u.NUMUNITCODE
                WHERE  m.STRAPPLICATIONNUMBER =
                (
                    SELECT DISTINCT
                           MAX(CONVERT( int, STRAPPLICATIONNUMBER)) AS GreatestApplication
                    FROM   SSPPAPPLICATIONMASTER
                    WHERE  STRAIRSNUMBER = @airs
                )
                       AND m.STRAIRSNUMBER = @airs"

            Dim dr4 As DataRow = DB.GetDataRow(query, p)

            If dr4 IsNot Nothing Then
                If IsDBNull(dr4.Item("SSPPStaffResponsible")) Then
                    txtSSPPContact.Clear()
                Else
                    txtSSPPContact.Text = dr4.Item("SSPPStaffResponsible")
                End If
                If IsDBNull(dr4.Item("strUnitDesc")) Then
                    txtSSPPUnit.Clear()
                Else
                    txtSSPPUnit.Text = dr4.Item("strUnitDesc")
                End If
            Else
                txtSSPPContact.Clear()
                txtSSPPUnit.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboCounty_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCounty.SelectedValueChanged
        Try
            Dim drDistrict As DataRow()

            If cboCounty.Text <> " " Then
                drDistrict = dtCounty.Select("CountyName = '" & cboCounty.Text & "'")
                For Each row As DataRow In drDistrict
                    txtDistrictInformation.Text = row("strDistrictName")
                Next
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewClientSummary_Click(sender As Object, e As EventArgs) Handles btnViewClientSummary.Click
        Try
            If txtClientID.Text <> "" Then
                LoadClientData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtStreetAddress_TextChanged(sender As Object, e As EventArgs) Handles txtStreetAddress.TextChanged
        Try
            If txtStreetAddress.Text <> "" Then
                txtMailingAddress.Text = txtStreetAddress.Text
            Else
                txtMailingAddress.Text = "<Mailing Address 1>"
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtStreetAddress2_TextChanged(sender As Object, e As EventArgs) Handles txtStreetAddress2.TextChanged
        Try
            If txtStreetAddress2.Text <> "" Then
                txtMailingAddress2.Text = txtStreetAddress2.Text
            Else
                txtMailingAddress2.Text = "<Mailing Address 2>"
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtCity_TextChanged(sender As Object, e As EventArgs) Handles txtCity.TextChanged
        Try
            If txtCity.Text <> "" Then
                txtMailingCity.Text = txtCity.Text
            Else
                txtMailingCity.Text = "<Mailing City>"
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtState_TextChanged(sender As Object, e As EventArgs) Handles txtState.TextChanged
        Try
            If txtState.Text <> "" Then
                txtMailingState.Text = txtState.Text
            Else
                txtMailingState.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mtbZipCode_TextChanged(sender As Object, e As EventArgs) Handles mtbZipCode.TextChanged
        Try
            If mtbZipCode.Text <> "" Then
                mtbMailingZipCode.Text = mtbZipCode.Text
            Else
                mtbMailingZipCode.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddContact_Click(sender As Object, e As EventArgs) Handles btnAddContact.Click
        UpdateContactData()
    End Sub

    Private Sub dgvContactInformation_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvContactInformation.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvContactInformation.HitTest(e.X, e.Y)

        Try
            If dgvContactInformation.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvContactInformation.Columns(0).HeaderText = "Client ID" Then
                    txtContactID.Text = dgvContactInformation(0, hti.RowIndex).Value
                End If
            End If

            If txtContactID.Text <> "" Then
                LoadContact()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchForContact_Click(sender As Object, e As EventArgs) Handles btnSearchForContact.Click
        If txtContactID.Text <> "" Then
            LoadContact()
        End If
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        SaveClientData()
    End Sub

    Private Sub tsbSearchTool_Click(sender As Object, e As EventArgs) Handles tsbSearchTool.Click
        Try
            Dim clientSearchDialog As New SBEAPClientSearchTool
            clientSearchDialog.ShowDialog()
            If clientSearchDialog.DialogResult = DialogResult.OK Then
                txtClientID.Text = clientSearchDialog.SelectedClientID
                LoadClientData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshAIRSData_Click(sender As Object, e As EventArgs) Handles btnRefreshAIRSData.Click
        RefreshAIRSData()
    End Sub

    Private Sub btnClearContact_Click(sender As Object, e As EventArgs) Handles btnClearContact.Click
        ClearContactData()
    End Sub

    Private Sub btnAddNewContact_Click(sender As Object, e As EventArgs) Handles btnAddNewContact.Click
        AddNewContactData()
    End Sub

    Private Sub btnDeleteContact_Click(sender As Object, e As EventArgs) Handles btnDeleteContact.Click
        Try
            Dim Result As DialogResult
            If txtContactID.Text <> "" Then
                Result = MessageBox.Show("Are you certain that you want to delete this Contact?",
                  "Contact Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Dim p As New SqlParameter("@contact", txtContactID.Text)

                        query = "Delete SBEAPClientLink " &
                            "where ClientContactID = @contact "

                        DB.RunCommand(query, p)

                        query = "Delete SBEAPClientContacts " &
                            "where ClientContactID = @contact "

                        DB.RunCommand(query, p)

                        ClearContactData()
                        LoadContactData()
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGetSiteAddress_Click(sender As Object, e As EventArgs) Handles btnGetSiteAddress.Click
        Try
            txtContactAddress.Text = txtStreetAddress.Text
            txtContactCity.Text = txtCity.Text
            txtContactState.Text = txtState.Text
            mtbContactZipCode.Text = mtbZipCode.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiDeleteClient_Click(sender As Object, e As EventArgs) Handles mmiDeleteClient.Click
        Try
            Dim Result As DialogResult
            Dim ContactID As String = ""
            Dim CaseID As String = ""
            Dim ActionID As String = ""
            Dim ActionType As String = ""

            If txtClientID.Text <> "" Then
                Result = MessageBox.Show("Are you sure you want to delete this client?",
                  "Client Summary Delete Client", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Do While CaseID <> "Done"
                            CaseID = "Done"

                            query = "Select " &
                            "numCaseID " &
                            "From SBEAPCaseLogLink " &
                            "where ClientID = @client "

                            Dim dr2 As DataRow = DB.GetDataRow(query, New SqlParameter("@client", txtClientID.Text))

                            If dr2 IsNot Nothing Then
                                If Not IsDBNull(dr2.Item("numCaseID")) Then
                                    CaseID = dr2.Item("numCaseID")
                                End If
                            End If

                            If CaseID <> "Done" Then
                                Do While ActionID <> "Done"
                                    ActionID = "Done"

                                    query = "Select " &
                                    "numActionID, numActionType " &
                                    "from SBEAPActionLog " &
                                    "where numCaseID = @caseid "

                                    Dim dr As DataRow = DB.GetDataRow(query, New SqlParameter("@caseid", CaseID))

                                    If dr IsNot Nothing Then
                                        If IsDBNull(dr.Item("numActionID")) Then
                                            ActionID = "Done"
                                        Else
                                            ActionID = dr.Item("numActionID")
                                        End If
                                        If IsDBNull(dr.Item("numActionType")) Then
                                            ActionType = "None"
                                        Else
                                            ActionType = dr.Item("numActionType")
                                        End If
                                    End If

                                    If ActionID <> "" And ActionID <> "Done" Then
                                        Select Case ActionType
                                            Case "4"
                                                query = "Delete SBEAPConferenceLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPActionLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                            Case "6"
                                                query = "Delete SBEAPPhoneLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPActionLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                            Case "10"
                                                query = "Delete SBEAPTechnicalAssist " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPActionLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                            Case "1" Or "2" Or "3" Or "5" Or "7" Or "8" Or "9" Or "11" Or "12"
                                                query = "Delete SBEAPOtherLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPActionLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                            Case Else
                                                query = "Delete SBEAPConferenceLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPPhoneLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPTechnicalAssist " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPOtherLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                                query = "Delete SBEAPActionLog " &
                                                "where numActionID = @actionid "

                                                DB.RunCommand(query, New SqlParameter("@actionid", ActionID))

                                        End Select
                                    End If
                                Loop

                                query = "Delete SBEAPCaseLog " &
                                "where numCaseID = @CaseID "

                                DB.RunCommand(query, New SqlParameter("@CaseID", CaseID))

                                query = "Delete SBEAPCaseLogLink " &
                                "where numCaseID = @CaseID "

                                DB.RunCommand(query, New SqlParameter("@CaseID", CaseID))
                            End If
                        Loop

                        ContactID = ""
                        Do While ContactID <> "Done"
                            ContactID = "Done"

                            query = "Select " &
                            "ClientID, ClientContactID " &
                            "from SBEAPClientLink " &
                            "where ClientID = @client "

                            Dim dr3 As DataRow = DB.GetDataRow(query, New SqlParameter("@client", txtClientID.Text))

                            If dr3 IsNot Nothing Then
                                If Not IsDBNull(dr3.Item("ClientContactID")) Then
                                    ContactID = dr3.Item("ClientContactID")
                                End If
                            End If

                            If ContactID <> "" And ContactID <> "Done" Then
                                query = "Delete SBEAPClientContacts " &
                                "where ClientContactID = @ContactID "

                                DB.RunCommand(query, New SqlParameter("@ContactID", ContactID))

                                query = "Delete SBEAPClientLink " &
                                "where ClientContactID = @ContactID "

                                DB.RunCommand(query, New SqlParameter("@ContactID", ContactID))
                            End If
                        Loop

                        query = "Delete SBEAPClientData " &
                        "where ClientID = @ClientID "

                        DB.RunCommand(query, New SqlParameter("@ClientID", txtClientID.Text))

                        query = "Delete SBEAPClients " &
                        "where ClientID = @ClientID "

                        DB.RunCommand(query, New SqlParameter("@ClientID", txtClientID.Text))

                        ClearClientSummary()
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbClear_Click(sender As Object, e As EventArgs) Handles tsbClear.Click
        ClearClientSummary()
    End Sub

    Private Sub ClearClientSummary()
        Try
            txtCompanyName.Clear()
            txtClientID.Clear()
            dtpStartDate.Value = Today
            txtStreetAddress.Clear()
            txtStreetAddress2.Clear()
            txtCity.Clear()
            txtState.Clear()
            mtbZipCode.Clear()
            cboCounty.SelectedValue = 0
            txtDistrictInformation.Clear()
            mtbLatitude.Clear()
            mtbLongitude.Clear()

            txtMailingAddress.Clear()
            txtMailingAddress2.Clear()
            txtMailingCity.Clear()
            txtMailingState.Clear()
            mtbMailingZipCode.Clear()
            txtClientCreator.Clear()
            txtClientModifier.Clear()

            mtbNumberOfEmployees.Clear()
            txtWebSite.Clear()
            txtDescription.Clear()
            mtbAIRSNumber.Clear()
            mtbSIC.Clear()
            mtbNAICS.Clear()
            txtAirDescription.Clear()
            txtAIRPermitNumber.Clear()
            chbSIP.Checked = False
            chbFederalSIP.Checked = False
            chbNonFedSIP.Checked = False
            chbCFCTracking.Checked = False
            chbPSD.Checked = False
            chbNSR.Checked = False
            chbNESHAP.Checked = False
            chbNSPS.Checked = False
            chbAcidPrecip.Checked = False
            chbFESOP.Checked = False
            chbNativeAmer.Checked = False
            chbMACT.Checked = False
            chbTitleV.Checked = False
            chbNSRPSD.Checked = False
            chbHAPs.Checked = False

            txtSSCPContact.Clear()
            txtSSCPUnit.Clear()
            txtSSPPContact.Clear()
            txtSSPPUnit.Clear()
            txtISMPContact.Clear()
            txtISMPUnit.Clear()

            ClearContactData()

            If dgvContactInformation.RowCount > 0 Then
                If dgvContactInformation.Columns(0).HeaderText = "Client ID" Then
                    dgvContactInformation.DataSource = Nothing
                Else

                End If
            End If

            If dgvCaseLog.RowCount > 0 Then
                If dgvCaseLog.Columns(0).HeaderText = "Case ID" Then
                    dgvCaseLog.DataSource = Nothing
                Else

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadClientWork()
        Try
            query = "Select " &
            "SBEAPCaseLog.numCaseID, " &
            "numStaffResponsible, " &
            "case " &
            "when numStaffResponsible is Null then '' " &
            "Else concat(strLastName,', ',strFirstName) " &
            "END StaffResponsible, " &
            "datCaseOpened as CaseOpened, " &
            "datCaseClosed as CaseClosed, " &
            "strCompanyName, strCaseSummary, " &
            "SBEAPCaseLogLink.ClientID " &
            "from SBEAPCaseLog left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "inner join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "where SBEAPClients.ClientID = @ClientID "

            dgvCaseLog.DataSource = DB.GetDataTable(query, New SqlParameter("@ClientID", txtClientID.Text))

            dgvCaseLog.RowHeadersVisible = False
            dgvCaseLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseLog.AllowUserToResizeColumns = True
            dgvCaseLog.AllowUserToAddRows = False
            dgvCaseLog.AllowUserToDeleteRows = False
            dgvCaseLog.AllowUserToOrderColumns = True
            dgvCaseLog.AllowUserToResizeRows = True
            dgvCaseLog.ColumnHeadersHeight = "35"
            dgvCaseLog.Columns("numCaseID").HeaderText = "Case ID"
            dgvCaseLog.Columns("numCaseID").DisplayIndex = 0
            dgvCaseLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseLog.Columns("StaffResponsible").DisplayIndex = 3
            dgvCaseLog.Columns("CaseOpened").HeaderText = "Date Case Opened"
            dgvCaseLog.Columns("CaseOpened").DisplayIndex = 2
            dgvCaseLog.Columns("CaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseLog.Columns("CaseClosed").HeaderText = "Date Case Closed"
            dgvCaseLog.Columns("CaseClosed").DisplayIndex = 5
            dgvCaseLog.Columns("CaseClosed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseLog.Columns("strCompanyName").HeaderText = "Client Name"
            dgvCaseLog.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseLog.Columns("strCompanyName").Width = "200"
            dgvCaseLog.Columns("ClientID").HeaderText = "Client ID"
            dgvCaseLog.Columns("ClientID").DisplayIndex = 6
            dgvCaseLog.Columns("numStaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseLog.Columns("numStaffResponsible").DisplayIndex = 7
            dgvCaseLog.Columns("numStaffResponsible").Visible = False
            dgvCaseLog.Columns("strCaseSummary").HeaderText = "Case Description"
            dgvCaseLog.Columns("strCaseSummary").DisplayIndex = 4
            dgvCaseLog.SanelyResizeColumns

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenCase_Click(sender As Object, e As EventArgs) Handles btnOpenCase.Click
        Try
            If txtCaseID.Text <> "" Then
                If CaseWork IsNot Nothing Then
                    CaseWork.Dispose()
                End If

                CaseWork = New SBEAPCaseWork

                If CaseWork IsNot Nothing AndAlso Not CaseWork.IsDisposed Then
                    CaseWork.txtCaseID.Text = txtCaseID.Text
                    CaseWork.Show()
                    CaseWork.LoadCaseLogData()
                Else
                    MessageBox.Show("There was an error opening the Case.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvCaseLog_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCaseLog.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCaseLog.HitTest(e.X, e.Y)
            If dgvCaseLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvCaseLog.Columns(0).HeaderText = "Case ID" Then
                    If IsDBNull(dgvCaseLog(0, hti.RowIndex).Value) Then
                        txtCaseID.Text = ""
                    Else
                        txtCaseID.Text = dgvCaseLog(0, hti.RowIndex).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub btnAddNewCase_Click(sender As Object, e As EventArgs) Handles btnAddNewCase.Click
        Try
            If CaseWork IsNot Nothing Then
                CaseWork.Dispose()
            End If

            CaseWork = New SBEAPCaseWork

            If CaseWork IsNot Nothing AndAlso Not CaseWork.IsDisposed Then
                CaseWork.Show()
                CaseWork.LoadCaseLogData()

                If txtClientID.Text <> "" Then
                    CaseWork.txtClientID.Text = txtClientID.Text
                    CaseWork.LoadClientInfo()
                    CaseWork.txtClientInformation.BackColor = Color.White
                End If
            Else
                MessageBox.Show("There was an error opening the Case.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSICSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSICSearch.LinkClicked
        Try

            Process.Start("http://www.osha.gov/pls/imis/sicsearch.html")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbNAICSSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbNAICSSearch.LinkClicked
        Try

            Process.Start("http://www.naics.com/search.htm")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class