Imports System.Data.SqlClient

Public Class SBEAPClientSummary
    Dim SQL As String
    Dim dsCounty As DataSet
    Dim daCounty As SqlDataAdapter
    Dim dsContact As DataSet
    Dim daContact As SqlDataAdapter
    Dim dsCaseLogGrid As DataSet
    Dim daCaseLogGrid As SqlDataAdapter

    Public WriteOnly Property ValueFromClientLookUp() As String
        Set(Value As String)
            txtClientID.Text = Value
        End Set
    End Property

    Private Sub SBEAPClientMaintenance_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            ClientSummary = Nothing

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SBEAPClientMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            pnl1.Text = "Client Summary"
            pnl2.Text = CurrentUser.AlphaName
            pnl3.Text = OracleDate

            LoadDataSets()
            LoadComboBoxes()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub LoadDataSets()
        Try
            SQL = "Select " &
            "Distinct(LookUpCountyInformation.strCountyName) as CountyName, " &
            "LookUpCountyInformation.strCountyCode, " &
            "LookUpDistrictinformation.strDistrictCode, " &
            "strDistrictName, strOfficeName  " &
            "from LookUpCountyInformation, LookUpDistrictInformation, " &
            "LookUpDistricts, LookUpDistrictOffice  " &
            "where LookUpCountyInformation.strCountyCode = LookUpDistrictinformation.strDistrictCounty " &
            "and LookUpDistrictInformation.strDistrictCode = LookUpDistricts.strDistrictCode " &
            "and LookUpDistrictInformation.strDistrictCode = LookUpDistrictOffice.strDistrictCode " &
            "order by CountyName "

            dsCounty = New DataSet
            daCounty = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daCounty.Fill(dsCounty, "CountyData")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComboBoxes()
        Try
            Dim dtCounty As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dtCounty.Columns.Add("CountyName", GetType(System.String))
            dtCounty.Columns.Add("strCountyCode", GetType(System.String))

            drNewRow = dtCounty.NewRow()
            drNewRow("CountyName") = " "
            drNewRow("strCountyCode") = " "
            dtCounty.Rows.Add(drNewRow)

            For Each drDSRow In dsCounty.Tables("CountyData").Rows()
                drNewRow = dtCounty.NewRow
                drNewRow("CountyName") = drDSRow("CountyName")
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                dtCounty.Rows.Add(drNewRow)
            Next

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
#Region "Subs and Functions"
    Sub LoadClientData()
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

            SQL = "Select ClientId " &
            "from SBEAPClients " &
            "where ClientID = '" & txtClientID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "select  " &
                "strCompanyName, datStartDate,  " &
                "strCompanyAddress, strCompanyAddress2,  " &
                "strCompanyCity, strCompanyState,  " &
                "strCompanyZipCode, strcompanyCounty,  " &
                "strCompanyLatitude, strCompanyLongitude,  " &
                "strMailingAddress, strMailingAddress2,  " &
                "strMailingCity, strMailingState,  " &
                "strMailingZipCode,  " &
                "(select (strLastName||', '||strFirstName) as Creator  " &
                "from SBEAPClients, EPDUserProfiles  " &
                "where SBEAPClients.strCompanyCreator = EPDUserProfiles.numUserID  " &
                "and clientid = '" & txtClientID.Text & "') as Creator, " &
                "to_char(datCompanyCreated, 'dd-Mon-yyyy') as datCompanyCreated,  " &
                "(select (strLastName||', '||strFirstName) as Modifier " &
                "from SBEAPClients, EPDUserProfiles  " &
                "where SBEAPClients.strModifingPerson = EPDUserProfiles.numUserID  " &
                "and clientid = '" & txtClientID.Text & "') as Modifier,  " &
                "to_char(datModifingDate, 'dd-Mon-yyyy') as datModifingDate " &
                "from SBEAPClients  " &
                "where ClientID = '" & txtClientID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

                SQL = "Select " &
                "strClientDescription, strClientWEbSite, " &
                "strClientSIC, SIC_DESC as strSICDesc, " &
                "strClientNAICS, " &
                "strClientEmployees, strAIRSNumber, " &
                "strAIRProgramCodes, strStateProgramCodes, " &
                "strAirPermitNumber, strSSCPEngineer, " &
                "strSSCPUnit, strSSPPEngineer, " &
                "strSSPPUnit, strISMPEngineer, " &
                "strISMPUnit, strAirDescription " &
                "from SBEAPClientData, LK_SIC " &
                "where SBEAPClientData.strClientSIC = LK_SIC.SIC_CODE (+) " &
                "and ClientID = '" & txtClientID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strClientDescription")) Then
                        Description = ""
                    Else
                        Description = dr.Item("strClientDescription")
                    End If
                    If IsDBNull(dr.Item("strClientWEbSite")) Then
                        WebSite = ""
                    Else
                        WebSite = dr.Item("strClientWEbSite")
                    End If
                    If IsDBNull(dr.Item("strClientSIC")) Then
                        SIC = ""
                    Else
                        SIC = dr.Item("strClientSIC")
                    End If
                    If IsDBNull(dr.Item("strSICDesc")) Then
                        SICDesc = ""
                    Else
                        SICDesc = "Description: " & dr.Item("strSICDesc")
                    End If
                    If IsDBNull(dr.Item("strClientNAICS")) Then
                        NAICS = ""
                    Else
                        NAICS = dr.Item("strClientNAICS")
                    End If
                    If IsDBNull(dr.Item("strClientEmployees")) Then
                        Employees = ""
                    Else
                        Employees = dr.Item("strClientEmployees")
                    End If
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        AIRSNumber = ""
                    Else
                        AIRSNumber = dr.Item("strAIRSNumber")
                    End If
                    If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                        AirProgramCode = ""
                    Else
                        AirProgramCode = dr.Item("strAIRProgramCodes")
                    End If
                    If IsDBNull(dr.Item("strStateProgramCodes")) Then
                        StateProgram = ""
                    Else
                        StateProgram = dr.Item("strStateProgramCodes")
                    End If
                    If IsDBNull(dr.Item("strAirPermitNumber")) Then
                        AirPermit = ""
                    Else
                        AirPermit = dr.Item("strAirPermitNumber")
                    End If
                    If IsDBNull(dr.Item("strSSCPEngineer")) Then
                        SSCPEngineer = ""
                    Else
                        SSCPEngineer = dr.Item("strSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("strSSCPUnit")) Then
                        SSCPUnit = ""
                    Else
                        SSCPUnit = dr.Item("strSSCPUnit")
                    End If
                    If IsDBNull(dr.Item("strSSPPEngineer")) Then
                        SSPPEngineer = ""
                    Else
                        SSPPEngineer = dr.Item("strSSPPEngineer")
                    End If
                    If IsDBNull(dr.Item("strSSPPUnit")) Then
                        SSPPUnit = ""
                    Else
                        SSPPUnit = dr.Item("strSSPPUnit")
                    End If
                    If IsDBNull(dr.Item("strISMPEngineer")) Then
                        ISMPEngineer = ""
                    Else
                        ISMPEngineer = dr.Item("strISMPEngineer")
                    End If
                    If IsDBNull(dr.Item("strISMPUnit")) Then
                        ISMPUnit = ""
                    Else
                        ISMPUnit = dr.Item("strISMPUnit")
                    End If
                    If IsDBNull(dr.Item("strAirDescription")) Then
                        AirDescription = ""
                    Else
                        AirDescription = dr.Item("strAirDescription")
                    End If
                End While
                dr.Close()
            Else

            End If

            txtCompanyName.Text = CompanyName
            If StartDate = "" Then
                dtpStartDate.Text = OracleDate
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
    Sub LoadContactData()
        Try
            dsContact = New DataSet

            SQL = "select " &
            "SBEAPClientContacts.ClientContactID, " &
            "strClientFirstName, strClientLastName, " &
            "strclientSalutation, strClientCredentials, " &
            "strClientTitle, strClientPhoneNumber, " &
            "strClientCellPhone, strClientFax, " &
            "strClientEmail, " &
            "strClientAddress, strClientCity, " &
            "strClientState, strClientZipCode, " &
            "strContactNotes " &
            "from SBEAPClientContacts, " &
            "SBEAPClientLink " &
            "where SBEAPClientContacts.ClientContactID = SBEAPClientLink.ClientContactID " &
            "and ClientID = '" & txtClientID.Text & "' "

            daContact = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daContact.Fill(dsContact, "ContactList")
            dgvContactInformation.DataSource = dsContact
            dgvContactInformation.DataMember = "ContactList"

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveContactData()
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

            If txtContactID.Text = "" Then
                SQL = "Insert into SBEAPClientContacts " &
                "values " &
                "((select (max(ClientContactID)+1) from SBEAPClientContacts), " &
                "'" & Replace(Firstname, "'", "''") & "', " &
                "'" & Replace(LastName, "'", "''") & "', " &
                "'" & Replace(Salutation, "'", "''") & "', " &
                "'" & Replace(Credentials, "'", "''") & "', " &
                "'" & Replace(Title, "'", "''") & "', " &
                "'" & Replace(PhoneNumber, "'", "''") & "', " &
                "'" & Replace(CellPhone, "'", "''") & "', " &
                "'" & Replace(Fax, "'", "''") & "', " &
                "'" & Replace(email, "'", "''") & "', " &
                "'" & Replace(Address, "'", "''") & "', " &
                "'" & Replace(City, "'", "''") & "', " &
                "'" & Replace(State, "'", "''") & "', " &
                "'" & Replace(ZipCode, "'", "''") & "', " &
                "'" & CurrentUser.UserID & "', " &
                "GETDATE(), '" & CurrentUser.UserID & "', " &
                "GETDATE()) "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select max(ClientContactID) as ClientID " &
                "from SBEAPClientContacts "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    txtContactID.Text = dr.Item("ClientID")
                End While
                dr.Close()

                txtContactCreationInfo.Text = CurrentUser.AlphaName & " - " & OracleDate
            Else
                SQL = "Update SBEAPClientContacts set " &
                "strClientFirstName = '" & Replace(Firstname, "'", "''") & "', " &
                "strClientLastName = '" & Replace(LastName, "'", "''") & "', " &
                "strClientSalutation = '" & Replace(Salutation, "'", "''") & "', " &
                "strClientCredentials = '" & Replace(Credentials, "'", "''") & "', " &
                "strClientTitle = '" & Replace(Title, "'", "''") & "', " &
                "strClientPhoneNumber = '" & Replace(PhoneNumber, "'", "''") & "', " &
                "strClientCellPhone = '" & Replace(CellPhone, "'", "''") & "', " &
                "strClientFax = '" & Replace(Fax, "'", "''") & "', " &
                "strClientEmail = '" & Replace(email, "'", "''") & "', " &
                "strClientAddress = '" & Replace(Address, "'", "''") & "', " &
                "strClientCity = '" & Replace(City, "'", "''") & "', " &
                "strClientState = '" & Replace(State, "'", "''") & "', " &
                "strClientZipCode = '" & Replace(ZipCode, "'", "''") & "', " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = GETDATE() " &
                "where ClientContactID = '" & txtContactID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            txtContactLastModified.Text = CurrentUser.AlphaName & " - " & OracleDate

            SQL = "Select " &
            "ClientID " &
            "from SBEAPClientLink " &
            "where clientID = '" & txtClientID.Text & "' " &
            "and ClientContactID = '" & txtContactID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPClientLink " &
                "values " &
                "('" & txtClientID.Text & "', '" & txtContactID.Text & "', " &
                "'" & MainContact & "') "
            Else
                SQL = "Update SBEAPClientLink set " &
                "strMainContact = 'False' " &
                "where ClientID = '" & txtClientID.Text & "' " &
                "and ClientContactID = '" & txtContactID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update SBEAPClientLink set " &
                "ClientID = '" & txtClientID.Text & "', " &
                "ClientContactID = '" & txtContactID.Text & "', " &
                "strMainContact  = '" & MainContact & "' " &
                "where ClientID = '" & txtClientID.Text & "' " &
                "and ClientContactID = '" & txtContactID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadContactData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub AddNewContactData()
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

            SQL = "Insert into SBEAPClientContacts " &
                "values " &
                "((select (max(ClientContactID)+1) from SBEAPClientContacts), " &
                "'" & Replace(Firstname, "'", "''") & "', " &
                "'" & Replace(LastName, "'", "''") & "', " &
                "'" & Replace(Salutation, "'", "''") & "', " &
                "'" & Replace(Credentials, "'", "''") & "', " &
                "'" & Replace(Title, "'", "''") & "', " &
                "'" & Replace(PhoneNumber, "'", "''") & "', " &
                "'" & Replace(CellPhone, "'", "''") & "', " &
                "'" & Replace(Fax, "'", "''") & "', " &
                "'" & Replace(email, "'", "''") & "', " &
                "'" & Replace(Address, "'", "''") & "', " &
                "'" & Replace(City, "'", "''") & "', " &
                "'" & Replace(State, "'", "''") & "', " &
                "'" & Replace(ZipCode, "'", "''") & "', " &
                "'" & CurrentUser.UserID & "', " &
                "GETDATE(), '" & CurrentUser.UserID & "', " &
                "GETDATE(), '" & Replace(ContactNotes, "'", "''") & "') "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(ClientContactID) as ClientID " &
            "from SBEAPClientContacts "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtContactID.Text = dr.Item("ClientID")
            End While
            dr.Close()

            txtContactCreationInfo.Text = CurrentUser.AlphaName & " - " & OracleDate
            txtContactLastModified.Text = CurrentUser.AlphaName & " - " & OracleDate

            SQL = "Select " &
            "ClientID " &
            "from SBEAPClientLink " &
            "where clientID = '" & txtClientID.Text & "' " &
            "and ClientContactID = '" & txtContactID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPClientLink " &
                "values " &
                "('" & txtClientID.Text & "', '" & txtContactID.Text & "', " &
                "'" & MainContact & "') "
            Else
                SQL = "Update SBEAPClientLink set " &
                "strMainContact = 'False' " &
                "where ClientID = '" & txtClientID.Text & "' " &
                "and ClientContactID = '" & txtContactID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update SBEAPClientLink set " &
                "ClientID = '" & txtClientID.Text & "', " &
                "ClientContactID = '" & txtContactID.Text & "', " &
                "strMainContact  = '" & MainContact & "' " &
                "where ClientID = '" & txtClientID.Text & "' " &
                "and ClientContactID = '" & txtContactID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadContactData()
            ClearContactData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub UpdateContactData()
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

            SQL = "select " &
            "SBEAPClientContacts.clientContactID " &
            "from SBEAPClientContacts, SBEAPClientLink " &
            "where SBEAPClientContacts.clientContactID = SBEAPClientLink.ClientContactID  " &
            "and Upper(strClientFirstName) = upper('" & txtFirstName.Text & "') " &
            "and upper(strClientLastName) = Upper('" & txtLastName.Text & "') " &
            "and ClientID = '" & txtClientID.Text & "' "

            If txtContactID.Text = "" Then
                MsgBox("Either click 'Add New Contact' or select the contact from the table below before editing the Contact Information.", MsgBoxStyle.Information,
                       "Edit Contact Data")
            Else
                SQL = "Update SBEAPClientContacts set " &
                "strClientFirstName = '" & Replace(Firstname, "'", "''") & "', " &
                "strClientLastName = '" & Replace(LastName, "'", "''") & "', " &
                "strClientSalutation = '" & Replace(Salutation, "'", "''") & "', " &
                "strClientCredentials = '" & Replace(Credentials, "'", "''") & "', " &
                "strClientTitle = '" & Replace(Title, "'", "''") & "', " &
                "strClientPhoneNumber = '" & Replace(PhoneNumber, "'", "''") & "', " &
                "strClientCellPhone = '" & Replace(CellPhone, "'", "''") & "', " &
                "strClientFax = '" & Replace(Fax, "'", "''") & "', " &
                "strClientEmail = '" & Replace(email, "'", "''") & "', " &
                "strClientAddress = '" & Replace(Address, "'", "''") & "', " &
                "strClientCity = '" & Replace(City, "'", "''") & "', " &
                "strClientState = '" & Replace(State, "'", "''") & "', " &
                "strClientZipCode = '" & Replace(ZipCode, "'", "''") & "', " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = GETDATE(), " &
                "strContactNotes = '" & Replace(ContactNotes, "'", "''") & "' " &
                "where ClientContactID = '" & txtContactID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtContactLastModified.Text = CurrentUser.AlphaName & " - " & OracleDate

                SQL = "Select " &
                "ClientID " &
                "from SBEAPClientLink " &
                "where clientID = '" & txtClientID.Text & "' " &
                "and ClientContactID = '" & txtContactID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into SBEAPClientLink " &
                    "values " &
                    "('" & txtClientID.Text & "', '" & txtContactID.Text & "', " &
                    "'" & MainContact & "') "
                Else
                    SQL = "Update SBEAPClientLink set " &
                    "strMainContact = 'False' " &
                    "where ClientID = '" & txtClientID.Text & "' " &
                    "and ClientContactID = '" & txtContactID.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Update SBEAPClientLink set " &
                    "ClientID = '" & txtClientID.Text & "', " &
                    "ClientContactID = '" & txtContactID.Text & "', " &
                    "strMainContact  = '" & MainContact & "' " &
                    "where ClientID = '" & txtClientID.Text & "' " &
                    "and ClientContactID = '" & txtContactID.Text & "' "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadContactData()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearContactData()
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
    Sub LoadContact()
        Try

            SQL = "select " &
            "strClientFirstName, strClientLastName, " &
            "strClientSalutation, strClientCredentials, " &
            "strClientTitle, strClientPhoneNumber, " &
            "strClientCellPhone, strClientFax, " &
            "strClientEmail, strClientAddress, " &
            "strClientCity, strClientState, " &
            "strClientZipCode, " &
            "(select (strLastName||', '||strFirstName) as Creator " &
            "from SBEAPClientContacts, EPDUserProfiles " &
            "where SBEAPClientContacts.strClientCreator = EPDUserProfiles.numUserID " &
            "and ClientContactID = '" & txtContactID.Text & "') as Creator, " &
            "to_char(datClientCreated, 'dd-Mon-yyyy') as datClientCreated, " &
            "(select (strLastName||', '||strFirstName) as Modifier " &
            "from SBEAPClientContacts, EPDUserProfiles " &
            "where SBEAPClientContacts.strClientCreator = EPDUserProfiles.numUserID " &
            "and ClientContactID = '" & txtContactID.Text & "') as Modifier, " &
            "to_char(datModifingDate, 'dd-Mon-yyyy') as datModifingDate,  " &
            "strMainContact, strContactNotes " &
            "from SBEAPClientContacts, SBEAPClientLink " &
            "where SBEAPClientContacts.ClientContactID = SBEAPClientLink.ClientContactID " &
            "and SBEAPClientContacts.ClientContactID = '" & txtContactID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub GetNextClientID()
        Try

            Dim CurrYear As String = ""
            CurrYear = Now.Year.ToString

            SQL = "Select max(ClientID) as MaxClientID " &
            "from SBEAPClients " &
            "where ClientID like '" & CurrYear & "%'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("MaxClientID")) Then
                    txtClientID.Text = CurrYear & "00001"
                Else
                    txtClientID.Text = dr.Item("MaxClientID") + 1
                End If
            End While
            dr.Close()

            'txtClientID.Text = CurrYear

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveClientData()
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

                SQL = "Select " &
                "strCompanyName " &
                "from SBEAPClients " &
                "where upper(strCompanyName) = '" & Replace(txtCompanyName.Text.ToUpper, "'", "''") & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                Dim Result As DialogResult

                If recExist = True Then
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
                SQL = "Select ClientID " &
                "from SBEAPClientData " &
                "where ClientID = '" & txtClientID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    Status = "Update"
                Else
                    Status = "Insert"
                End If
            End If

            If Status = "Insert" Then
                If txtClientID.Text = "" Then
                    ClientCreator = CurrentUser.UserID
                    GetNextClientID()

                    SQL = "Insert into SBEAPClients " &
                    "values " &
                    "('" & txtClientID.Text & "', " &
                    "'" & Replace(ClientName, "'", "''") & "', " &
                    "'" & StartDate & "', " &
                    "'" & Replace(ClientAddress, "'", "''") & "', " &
                    "'" & Replace(ClientAddress2, "'", "''") & "', " &
                    "'" & Replace(ClientCity, "'", "''") & "', " &
                    "'" & Replace(ClientState, "'", "''") & "', " &
                    "'" & Replace(ClientZipCode, "'", "''") & "', " &
                    "'" & Replace(ClientCounty, "'", "''") & "', " &
                    "'" & Replace(ClientLatitude, "'", "''") & "', " &
                    "'" & Replace(ClientLongitude, "'", "''") & "', " &
                    "'" & Replace(MailingAddress, "'", "''") & "', " &
                    "'" & Replace(MailingAddress2, "'", "''") & "', " &
                    "'" & Replace(MailingCity, "'", "''") & "', " &
                    "'" & Replace(MailingState, "'", "''") & "', " &
                    "'" & Replace(MailingZipCode, "'", "''") & "', " &
                    "'" & Replace(ClientCreator, "'", "''") & "', " &
                    "GETDATE(), '" & CurrentUser.UserID & "', GETDATE(), '') "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select max(clientID) as MaxID " &
                    "from SBEAPClients "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        txtClientID.Text = dr.Item("MaxID")
                    End While
                    dr.Close()

                    SQL = "Insert into SBEAPClientData " &
                    "values " &
                    "('" & txtClientID.Text & "', '" & Replace(ClientDescription, "'", "''") & "', " &
                    "'" & Replace(WebSite, "'", "''") & "', '" & SIC & "', " &
                    "'" & NAICS & "', '" & Employees & "', " &
                    "'" & AIRSNumber & "', '" & AirCodes & "', " &
                    "'" & StateCodes & "', " &
                    "'" & CurrentUser.UserID & "', GETDATE(), '', " &
                    "'" & Replace(AIRPermit, "'", "''") & "', '" & Replace(SSCPEngineer, "'", "''") & "', " &
                    "'" & Replace(SSCPUnit, "'", "''") & "', '" & Replace(SSPPEngineer, "'", "''") & "', " &
                    "'" & Replace(SSPPUnit, "'", "''") & "', '" & Replace(ISMPEngineer, "'", "''") & "', " &
                    "'" & Replace(ISMPUnit, "'", "''") & "', '" & Replace(AirDescription, "'", "''") & "') "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    MsgBox("The Client ID box currnetly has a value.", MsgBoxStyle.Information, "Client Maintenance")
                End If
            Else
                SQL = "Update SBEAPClients set " &
                "strCompanyName = '" & Replace(ClientName, "'", "''") & "', " &
                "datStartDate = '" & Replace(StartDate, "'", "''") & "', " &
                "strCompanyaddress = '" & Replace(ClientAddress, "'", "''") & "', " &
                "strCompanyAddress2 = '" & Replace(ClientAddress2, "'", "''") & "', " &
                "strCompanyCity = '" & Replace(ClientCity, "'", "''") & "', " &
                "strCompanyState = '" & Replace(ClientState, "'", "''") & "', " &
                "strCompanyZipCode = '" & Replace(ClientZipCode, "'", "''") & "', " &
                "strCompanyCounty = '" & ClientCounty & "', " &
                "strCompanyLatitude = '" & Replace(ClientLatitude, "'", "''") & "', " &
                "strCompanyLongitude = '" & Replace(ClientLongitude, "'", "''") & "', " &
                "strMailingAddress = '" & Replace(MailingAddress, "'", "''") & "', " &
                "strMailingAddress2 = '" & Replace(MailingAddress2, "'", "''") & "', " &
                "strMailingCity = '" & Replace(MailingCity, "'", "''") & "', " &
                "strMailingState = '" & Replace(MailingState, "'", "''") & "', " &
                "strMailingZipCode = '" & Replace(MailingZipCode, "'", "''") & "', " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = GETDATE() " &
                "where ClientID = '" & txtClientID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update SBEAPClientData set " &
                "strClientDescription = '" & ClientDescription & "', " &
                "strClientWebSite = '" & WebSite & "', " &
                "strClientSIC = '" & SIC & "', " &
                "strClientNAICS = '" & NAICS & "', " &
                "strClientEmployees = '" & Employees & "', " &
                "strAIRSNumber = '" & AIRSNumber & "', " &
                "strAirProgramCodes = '" & AirCodes & "', " &
                "strStateProgramCodes = '" & StateCodes & "', " &
                "strModifingperson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = GETDATE(), " &
                "strAIRPermitNumber = '" & Replace(AIRPermit, "'", "''") & "', " &
                "strSSCPEngineer = '" & Replace(SSCPEngineer, "'", "''") & "', " &
                "strSSCPUnit = '" & Replace(SSCPUnit, "'", "''") & "', " &
                "strSSPPEngineer = '" & Replace(SSPPEngineer, "'", "''") & "', " &
                "strSSPPUnit = '" & Replace(SSPPUnit, "'", "''") & "', " &
                "strISMPEngineer = '" & Replace(ISMPEngineer, "'", "''") & "', " &
                "strISMPUnit = '" & Replace(ISMPUnit, "'", "''") & "', " &
                "strAirDescription = '" & Replace(AirDescription, "'", "''") & "' " &
                "where ClientID = '" & txtClientID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If

            MsgBox("Done", MsgBoxStyle.Information, "SBEAP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RefreshAIRSData()
        Try
            Dim AirProgramCode As String = "000000000000000"
            Dim StateProgram As String = "00"

            SQL = "select " &
            "strSICcode, strNAICSCode, " &
            "strPlantDescription, " &
            "strAIRProgramCodes, " &
            "strStateProgramCodes " &
            "from APBHeaderData " &
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()


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
                SQL = "select " &
                "strFacilityStreet1, strFacilityStreet2, " &
                "strFacilityCity, strFacilityState, " &
                "strFacilityZipCode, numFacilityLongitude, " &
                "numFacilityLatitude " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        ' txtStreetAddress.Clear()
                    Else
                        txtStreetAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet2")) Then
                        ' txtStreetAddress2.Clear()
                    Else
                        txtStreetAddress2.Text = dr.Item("strFacilityStreet2")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then

                    Else
                        txtCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then

                    Else
                        txtState.Text = dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then

                    Else
                        mtbZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr.Item("numFacilityLongitude")) Then

                    Else
                        mtbLongitude.Text = dr.Item("numFacilityLongitude")
                    End If
                    If IsDBNull(dr.Item("numFacilityLatitude")) Then

                    Else
                        mtbLatitude.Text = dr.Item("numFacilityLatitude")
                    End If
                    cboCounty.SelectedValue = Mid(mtbAIRSNumber.Text, 1, 3)
                End While
                dr.Close()
            End If

            SQL = "Select " &
            "Distinct((strLastName||', '||strFirstName)) as ISMPEngineer, strUnitDesc   " &
            "from EPDUserProfiles, ISMPReportInformation,   " &
            "ISMPMaster, LookUpEPDUnits    " &
            "where EPDUserProfiles.numUnit = LookUpEPDunits.numunitCode (+) " &
            "and numUserID = strReviewingEngineer   " &
            "AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber   " &
            "and strClosed = 'True'  " &
            "and datCompleteDate = (Select Distinct(Max(datCompleteDate)) as CompleteDate  " &
            "from ISMPReportInformation, ISMPMaster  " &
            "where ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber   " &
            "and ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " &
            "and strClosed = 'True')  " &
            "and ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("ISMPEngineer")) Then
                    txtISMPContact.Clear()
                Else
                    txtISMPContact.Text = dr.Item("ISMPEngineer")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    txtISMPUnit.Clear()
                Else
                    txtISMPUnit.Text = dr.Item("strUnitDesc")
                End If
            Else
                txtISMPContact.Clear()
                txtISMPUnit.Clear()
            End If
            dr.Close()

            SQL = "select  " &
            "Distinct((strLastName||', '||strFirstName)) as SSPPStaffResponsible, strUnitDesc   " &
            "from EPDUserProfiles, SSPPApplicationMaster, " &
            "LookUpEPDUnits " &
            "where EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and numUserID = strStaffResponsible  " &
            "and SSPPApplicationMaster.strApplicationNumber =  " &
            "(select distinct(max(CONVERT(int, strApplicationNumber))) as GreatestApplication  " &
            "from SSPPApplicationMaster   " &
            "where SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "')  " &
            "and SSPPApplicationMaster.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "'  "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("SSPPStaffResponsible")) Then
                    txtSSPPContact.Clear()
                Else
                    txtSSPPContact.Text = dr.Item("SSPPStaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    txtSSPPUnit.Clear()
                Else
                    txtSSPPUnit.Text = dr.Item("strUnitDesc")
                End If
            Else
                txtSSPPContact.Clear()
                txtSSPPUnit.Clear()
            End If
            dr.Close()

            If txtClientID.Text <> "" Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Do you want to import contacts from this AIRS #?",
                  "Client Summary Contact Import", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes


                End Select

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Declarations"
    Private Sub cboCounty_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCounty.SelectedValueChanged
        Try
            Dim dtDistrictInfo As DataTable
            Dim drDistrict As DataRow()
            Dim row As DataRow

            If cboCounty.Text <> " " Then
                dtDistrictInfo = dsCounty.Tables("CountyData")
                drDistrict = dtDistrictInfo.Select("CountyName = '" & cboCounty.Text & "'")
                For Each row In drDistrict
                    txtDistrictInformation.Text = row("strDistrictName").ToString() & " District - " & row("strOfficeName").ToString()
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

#End Region
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
        Try

            UpdateContactData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
        Finally

        End Try
    End Sub
    Private Sub btnSearchForContact_Click(sender As Object, e As EventArgs) Handles btnSearchForContact.Click
        Try
            If txtContactID.Text <> "" Then
                LoadContact()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Try
            SaveClientData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbSearchTool_Click(sender As Object, e As EventArgs) Handles tsbSearchTool.Click
        Try
            Dim clientSearchDialog As New SBEAPClientSearchTool
            clientSearchDialog.ShowDialog()
            If clientSearchDialog.DialogResult = DialogResult.OK Then
                Me.ValueFromClientLookUp = clientSearchDialog.SelectedClientID
                LoadClientData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbBack_Click(sender As Object, e As EventArgs) Handles tsbBack.Click
        Try
            ClientSummary = Nothing
            Me.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshAIRSData_Click(sender As Object, e As EventArgs) Handles btnRefreshAIRSData.Click
        Try
            RefreshAIRSData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnClearContact_Click(sender As Object, e As EventArgs) Handles btnClearContact.Click
        Try
            ClearContactData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewContact_Click(sender As Object, e As EventArgs) Handles btnAddNewContact.Click
        Try
            AddNewContactData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
                        SQL = "Delete SBEAPClientLink " &
                        "where ClientContactID = '" & txtContactID.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete SBEAPClientContacts " &
                        "where ClientContactID = '" & txtContactID.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

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

                            SQL = "Select " &
                            "numCaseID " &
                            "From SBEAPCaseLogLink " &
                            "where ClientID = '" & txtClientID.Text & "' "
                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("numCaseID")) Then
                                    CaseID = "Done"
                                Else
                                    CaseID = dr.Item("numCaseID")
                                End If
                            End While
                            dr.Close()
                            If CaseID <> "Done" Then
                                Do While ActionID <> "Done"
                                    ActionID = "Done"

                                    SQL = "Select " &
                                    "numActionID, numActionType " &
                                    "from SBEAPActionLog " &
                                    "where numCaseID = '" & CaseID & "' "
                                    cmd = New SqlCommand(SQL, CurrentConnection)
                                    If CurrentConnection.State = ConnectionState.Closed Then
                                        CurrentConnection.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
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
                                    End While
                                    dr.Close()

                                    If ActionID <> "" And ActionID <> "Done" Then
                                        Select Case ActionType
                                            Case "4"
                                                SQL = "Delete SBEAPConferenceLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPActionLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()
                                            Case "6"
                                                SQL = "Delete SBEAPPhoneLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPActionLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()
                                            Case "10"
                                                SQL = "Delete SBEAPTechnicalAssist " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPActionLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()
                                            Case "1" Or "2" Or "3" Or "5" Or "7" Or "8" Or "9" Or "11" Or "12"
                                                SQL = "Delete SBEAPOtherLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPActionLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()
                                            Case Else
                                                SQL = "Delete SBEAPConferenceLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPPhoneLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPTechnicalAssist " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPOtherLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()

                                                SQL = "Delete SBEAPActionLog " &
                                                "where numActionID = '" & ActionID & "'  "
                                                cmd = New SqlCommand(SQL, CurrentConnection)
                                                If CurrentConnection.State = ConnectionState.Closed Then
                                                    CurrentConnection.Open()
                                                End If
                                                dr = cmd.ExecuteReader
                                                dr.Close()
                                        End Select
                                    End If
                                Loop
                                SQL = "Delete SBEAPCaseLog " &
                                "where numCaseID = '" & CaseID & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SBEAPCaseLogLink " &
                                "where numCaseID = '" & CaseID & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            End If
                        Loop

                        ContactID = ""
                        Do While ContactID <> "Done"
                            ContactID = "Done"

                            SQL = "Select " &
                            "ClientID, ClientContactID " &
                            "from SBEAPClientLink " &
                            "where ClientID = '" & txtClientID.Text & "' "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("ClientContactID")) Then
                                    ContactID = "Done"
                                Else
                                    ContactID = dr.Item("ClientContactID")
                                End If
                            End While
                            dr.Close()

                            If ContactID <> "" And ContactID <> "Done" Then
                                SQL = "Delete SBEAPClientContacts " &
                                "where ClientContactID = '" & ContactID & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Delete SBEAPClientLink " &
                                "where ClientContactID = '" & ContactID & "' "
                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            End If
                        Loop

                        SQL = "Delete SBEAPClientData " &
                        "where ClientID = '" & txtClientID.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete SBEAPClients " &
                        "where ClientID = '" & txtClientID.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ClearClientSummary()
                    Case Else
                        Exit Sub
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbClear_Click(sender As Object, e As EventArgs) Handles tsbClear.Click
        Try

            ClearClientSummary()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearClientSummary()
        Try
            txtCompanyName.Clear()
            txtClientID.Clear()
            dtpStartDate.Text = OracleDate
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
                    dsContact = New DataSet
                    dgvContactInformation.DataSource = dsContact
                Else

                End If
            End If

            If dgvCaseLog.RowCount > 0 Then
                If dgvCaseLog.Columns(0).HeaderText = "Case ID" Then
                    dsCaseLogGrid = New DataSet
                    dgvCaseLog.DataSource = dsCaseLogGrid
                Else

                End If
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadClientWork()
        Try
            SQL = "Select " &
            "SBEAPCaseLog.numCaseID, " &
            "numStaffResponsible, " &
            "case " &
            "when numStaffResponsible is Null then '' " &
            "Else (strLastName||', '||strFirstName) " &
            "END StaffResponsible, " &
            "datCaseOpened as CaseOpened, " &
            "datCaseClosed as CaseClosed, " &
            "strCompanyName, strCaseSummary, " &
            "SBEAPCaseLogLink.ClientID " &
            "from SBEAPCaseLog, EPDUserProfiles, " &
            "SBEAPClients, SBEAPCaseLogLink " &
            "where SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID (+)  " &
            "and SBEAPClients.ClientID = '" & txtClientID.Text & "'  "

            dsCaseLogGrid = New DataSet
            daCaseLogGrid = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daCaseLogGrid.Fill(dsCaseLogGrid, "NavScreen")

            dgvCaseLog.DataSource = dsCaseLogGrid
            dgvCaseLog.DataMember = "NavScreen"

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
            'dgvCaseLog.Columns("ClientID").Visible = False
            dgvCaseLog.Columns("numStaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseLog.Columns("numStaffResponsible").DisplayIndex = 7
            dgvCaseLog.Columns("numStaffResponsible").Visible = False
            dgvCaseLog.Columns("strCaseSummary").HeaderText = "Case Description"
            dgvCaseLog.Columns("strCaseSummary").DisplayIndex = 4

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenCase_Click(sender As Object, e As EventArgs) Handles btnOpenCase.Click
        Try
            If txtCaseID.Text <> "" Then
                If CaseWork Is Nothing Then

                Else
                    CaseWork.Dispose()
                End If
                CaseWork = New SBEAPCaseWork
                CaseWork.txtCaseID.Text = txtCaseID.Text
                CaseWork.Show()
                CaseWork.LoadCaseLogData()
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
            If CaseWork Is Nothing Then

            Else
                CaseWork.Dispose()
            End If
            CaseWork = New SBEAPCaseWork
            CaseWork.Show()
            CaseWork.LoadCaseLogData()
            If txtClientID.Text <> "" Then
                CaseWork.txtClientID.Text = txtClientID.Text
                CaseWork.LoadClientInfo()
                CaseWork.txtClientInformation.BackColor = Color.White
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSICSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSICSearch.LinkClicked
        Try

            System.Diagnostics.Process.Start("http://www.osha.gov/pls/imis/sicsearch.html")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbNAICSSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbNAICSSearch.LinkClicked
        Try

            System.Diagnostics.Process.Start("http://www.naics.com/search.htm")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class