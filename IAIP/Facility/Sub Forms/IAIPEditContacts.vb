Imports System.Data.SqlClient
Imports Iaip.DAL
Imports Iaip.Apb
Imports EpdIt.DBUtilities
Imports System.Collections.Generic

Public Class IAIPEditContacts

    Public Property AirsNumber As ApbFacilityId
    Friend Property Key As ContactKey

    Protected Overrides Sub OnLoad(e As EventArgs)
        ParseParameters()
        LoadContactsDataset()
    End Sub

    Private Sub ParseParameters()
        If Parameters Is Nothing Then
            MessageBox.Show("Bad data.")
            Close()
        End If

        If Parameters.ContainsKey(FormParameter.AirsNumber) Then
            AirsNumber = ApbFacilityId.TryCastApbFacilityId(Parameters(FormParameter.AirsNumber))

            If AirsNumber Is Nothing Then
                MessageBox.Show("Invalid AIRS Number.")
                Close()
            End If

            lblAirsNumber.Text = AirsNumber.FormattedString
        End If

        If Parameters.ContainsKey(FormParameter.FacilityName) Then
            lblFacilityName.Text = Parameters(FormParameter.FacilityName)
        End If
    End Sub

    Private Sub LoadContactsDataset()
        Dim query As String = "select case
                   when strKey = '10' then 'Current Monitoring Contact'
                   when strKey = '20' then 'Current Compliance Contact'
                   when strKey = '30' then 'Current Permitting Contact'
                   when strKey = '40' then 'Current Fee Contact'
                   when strkey = '41' then 'Current EIS Contact'
                   when strKey = '42' then 'Current ES Contact'
                   when left(strKey, 1) = '1' then 'Past Monitoring Contact ' + right(STRKEY, 1)
                   when left(strKey, 1) = '2' then 'Past Compliance Contact ' + right(STRKEY, 1)
                   when left(strKey, 1) = '3' then 'Past Permitting Contact ' + right(STRKEY, 1)
                   else 'Unknown'
               end as ContactType,
               convert(date, DATMODIFINGDATE)
                   as [Updated],
               strContactDescription,
               strKey,
               strContactFirstName,
               strContactLastname,
               strContactPrefix,
               strContactSuffix,
               strContactTitle,
               strContactCompanyName,
               strContactPhoneNumber1,
               strContactPhoneNumber2,
               strContactFaxNumber,
               strContactEmail,
               strContactAddress1,
               strContactAddress2,
               strContactCity,
               strContactState,
               strContactZipCode
        from APBContactInformation
        where strAIRSnumber = @airs
          and convert(int, STRKEY) < 50
        order by substring(strKey, 1, 1), strKey"

        Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)

        ContactsDataGrid.DataSource = DB.GetDataTable(query, p)

        ContactsDataGrid.Columns("ContactType").HeaderText = "Contact Type"
        ContactsDataGrid.Columns("strKey").Visible = False
        ContactsDataGrid.Columns("strContactPrefix").HeaderText = "Social Title"
        ContactsDataGrid.Columns("strContactFirstName").HeaderText = "First Name"
        ContactsDataGrid.Columns("strContactLastName").HeaderText = "Last Name"
        ContactsDataGrid.Columns("strContactSuffix").HeaderText = "Suffix"
        ContactsDataGrid.Columns("strContactTitle").HeaderText = "Title"
        ContactsDataGrid.Columns("strContactCompanyName").HeaderText = "Company Name"
        ContactsDataGrid.Columns("strContactPhoneNumber1").HeaderText = "Phone Number 1"
        ContactsDataGrid.Columns("strContactPhoneNumber2").HeaderText = "Phone Number 2"
        ContactsDataGrid.Columns("strContactFaxNumber").HeaderText = "Fax Number"
        ContactsDataGrid.Columns("strContactEmail").HeaderText = "Email Address"
        ContactsDataGrid.Columns("strContactAddress1").HeaderText = "Address Line 1"
        ContactsDataGrid.Columns("strContactAddress2").HeaderText = "Address Line 2"
        ContactsDataGrid.Columns("strContactCity").HeaderText = "City"
        ContactsDataGrid.Columns("strContactState").HeaderText = "State"
        ContactsDataGrid.Columns("strContactZipCode").HeaderText = "Zip Code"
        ContactsDataGrid.Columns("strContactDescription").HeaderText = "Description"
    End Sub

    Private Sub LoadSelectedContact()
        Dim row As DataGridViewRow = ContactsDataGrid.SelectedRows.Item(0)

        If row Is Nothing Then
            ClearForm()
            Return
        End If

        txtNewFirstName.Text = GetNullableString(row.Cells("strContactFirstName").Value)
        txtNewLastName.Text = GetNullableString(row.Cells("strContactLastName").Value)
        txtNewPrefix.Text = GetNullableString(row.Cells("strContactPrefix").Value)
        txtNewSuffix.Text = GetNullableString(row.Cells("strContactSuffix").Value)
        txtNewTitle.Text = GetNullableString(row.Cells("STRCONTACTTITLE").Value)
        txtNewCompany.Text = GetNullableString(row.Cells("STRCONTACTCOMPANYNAME").Value)
        txtNewPhoneNumber.Text = GetNullableString(row.Cells("STRCONTACTPHONENUMBER1").Value)
        mtbNewPhoneNumber2.Text = GetNullableString(row.Cells("STRCONTACTPHONENUMBER2").Value)
        mtbNewFaxNumber.Text = GetNullableString(row.Cells("STRCONTACTFAXNUMBER").Value)
        txtNewEmail.Text = GetNullableString(row.Cells("STRCONTACTEMAIL").Value)
        txtNewAddress.Text = GetNullableString(row.Cells("STRCONTACTADDRESS1").Value)
        txtNewCity.Text = GetNullableString(row.Cells("STRCONTACTCITY").Value)
        txtNewState.Text = GetNullableString(row.Cells("STRCONTACTSTATE").Value)
        mtbNewZipCode.Text = GetNullableString(row.Cells("STRCONTACTZIPCODE").Value)
        txtNewDescrption.Text = GetNullableString(row.Cells("STRCONTACTDESCRIPTION").Value)

        rdbNewMonitoringContact.Checked = False
        rdbNewComplianceContact.Checked = False
        rdbNewPermittingContact.Checked = False
        rdbNewFeeContact.Checked = False
        rdbNewEISContact.Checked = False
        rdbNewESContact.Checked = False

        Select Case Key
            Case ContactKey.IndustrialSourceMonitoring
                rdbNewMonitoringContact.Checked = True
            Case ContactKey.StationarySourceCompliance
                rdbNewComplianceContact.Checked = True
            Case ContactKey.StationarySourcePermitting
                rdbNewPermittingContact.Checked = True
            Case ContactKey.Fees
                rdbNewFeeContact.Checked = True
            Case ContactKey.EmissionInventory
                rdbNewEISContact.Checked = True
            Case ContactKey.EmissionStatement
                rdbNewESContact.Checked = True
        End Select

        ContactKeyPanel.Enabled = False
        btnSaveNewContact.Enabled = False
        btnUpdateContact.Enabled = True

        If row.Cells("ContactType").Value.ToString.Substring(0, 4) = "Past" Then
            btnUpdateContact.Enabled = False
        End If
    End Sub

    Private Sub ContactsDataGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles ContactsDataGrid.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < ContactsDataGrid.RowCount AndAlso ContactsDataGrid.SelectedRows.Count = 1 Then
            Key = ContactsDataGrid("strKey", e.RowIndex).Value
            LoadSelectedContact()
        End If
    End Sub

    Private Sub btnClearForm_Click(sender As Object, e As EventArgs) Handles btnClearForm.Click
        ClearForm()
    End Sub

    Private Sub ClearForm()
        txtNewAddress.Clear()
        txtNewCity.Clear()
        txtNewCompany.Clear()
        txtNewDescrption.Clear()
        txtNewEmail.Clear()
        txtNewFirstName.Clear()
        txtNewLastName.Clear()
        txtNewPrefix.Clear()
        txtNewState.Clear()
        txtNewSuffix.Clear()
        txtNewTitle.Clear()
        mtbNewFaxNumber.Clear()
        txtNewPhoneNumber.Clear()
        mtbNewPhoneNumber2.Clear()
        mtbNewZipCode.Clear()
        rdbNewMonitoringContact.Checked = False
        rdbNewComplianceContact.Checked = False
        rdbNewPermittingContact.Checked = False
        rdbNewFeeContact.Checked = False
        rdbNewEISContact.Checked = False
        rdbNewESContact.Checked = False

        Key = ContactKey.None
        ContactKeyPanel.Enabled = True

        btnSaveNewContact.Enabled = True
        btnUpdateContact.Enabled = False
        ContactsDataGrid.SelectNone()
    End Sub

    Private Sub btnUpdateContact_Click(sender As Object, e As EventArgs) Handles btnUpdateContact.Click
        Dim query As String = "UPDATE APBCONTACTINFORMATION " &
            "SET STRCONTACTFIRSTNAME = @STRCONTACTFIRSTNAME " &
            ", STRCONTACTLASTNAME = @STRCONTACTLASTNAME " &
            ", STRCONTACTPREFIX = @STRCONTACTPREFIX " &
            ", STRCONTACTSUFFIX = @STRCONTACTSUFFIX " &
            ", STRCONTACTTITLE = @STRCONTACTTITLE " &
            ", STRCONTACTCOMPANYNAME = @STRCONTACTCOMPANYNAME " &
            ", STRCONTACTPHONENUMBER1 = @STRCONTACTPHONENUMBER1 " &
            ", STRCONTACTPHONENUMBER2 = @STRCONTACTPHONENUMBER2 " &
            ", STRCONTACTFAXNUMBER = @STRCONTACTFAXNUMBER " &
            ", STRCONTACTEMAIL = @STRCONTACTEMAIL " &
            ", STRCONTACTADDRESS1 = @STRCONTACTADDRESS1 " &
            ", STRCONTACTCITY = @STRCONTACTCITY " &
            ", STRCONTACTSTATE = @STRCONTACTSTATE " &
            ", STRCONTACTZIPCODE = @STRCONTACTZIPCODE " &
            ", STRMODIFINGPERSON = @STRMODIFINGPERSON " &
            ", DATMODIFINGDATE = GETDATE() " &
            ", STRCONTACTDESCRIPTION = @STRCONTACTDESCRIPTION " &
            "WHERE  STRAIRSNUMBER = @STRAIRSNUMBER " &
            "AND STRKEY = @STRKEY "

        Dim p As SqlParameter() = {
            New SqlParameter("@STRCONTACTFIRSTNAME", txtNewFirstName.Text),
            New SqlParameter("@STRCONTACTLASTNAME", txtNewLastName.Text),
            New SqlParameter("@STRCONTACTPREFIX", txtNewPrefix.Text),
            New SqlParameter("@STRCONTACTSUFFIX", txtNewSuffix.Text),
            New SqlParameter("@STRCONTACTTITLE", txtNewTitle.Text),
            New SqlParameter("@STRCONTACTCOMPANYNAME", txtNewCompany.Text),
            New SqlParameter("@STRCONTACTPHONENUMBER1", txtNewPhoneNumber.Text),
            New SqlParameter("@STRCONTACTPHONENUMBER2", mtbNewPhoneNumber2.Text),
            New SqlParameter("@STRCONTACTFAXNUMBER", mtbNewFaxNumber.Text),
            New SqlParameter("@STRCONTACTEMAIL", txtNewEmail.Text),
            New SqlParameter("@STRCONTACTADDRESS1", txtNewAddress.Text),
            New SqlParameter("@STRCONTACTCITY", txtNewCity.Text),
            New SqlParameter("@STRCONTACTSTATE", txtNewState.Text),
            New SqlParameter("@STRCONTACTZIPCODE", mtbNewZipCode.Text),
            New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
            New SqlParameter("@STRCONTACTDESCRIPTION", txtNewDescrption.Text),
            New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
            New SqlParameter("@STRKEY", Key.ToString("D"))
        }

        If DB.RunCommand(query, p) Then
            MsgBox("Contact updated.", MsgBoxStyle.Information, Text)
        Else
            MsgBox("An error occurred.")
        End If

        LoadContactsDataset()
    End Sub

    Private Sub btnSaveNewContact_Click(sender As Object, e As EventArgs) Handles btnSaveNewContact.Click
        Dim newKey As String

        If rdbNewMonitoringContact.Checked Then
            newKey = "10"
        ElseIf rdbNewComplianceContact.Checked Then
            newKey = "20"
        ElseIf rdbNewPermittingContact.Checked Then
            newKey = "30"
        ElseIf rdbNewFeeContact.Checked Then
            newKey = "40"
        ElseIf rdbNewEISContact.Checked Then
            newKey = "41"
        ElseIf rdbNewESContact.Checked Then
            newKey = "42"
        Else
            MsgBox("Select a Contact Type first." & vbCrLf & "No data saved.", MsgBoxStyle.Information, Text)
            Return
        End If

        Dim spName As String = "iaip_facility.SaveApbContact"

        Dim p As SqlParameter() = {
            New SqlParameter("@key", newKey),
            New SqlParameter("@facilityId", AirsNumber.DbFormattedString),
            New SqlParameter("@firstName", txtNewFirstName.Text),
            New SqlParameter("@lastName", txtNewLastName.Text),
            New SqlParameter("@prefix", txtNewPrefix.Text),
            New SqlParameter("@suffix", txtNewSuffix.Text),
            New SqlParameter("@title", txtNewTitle.Text),
            New SqlParameter("@organization", txtNewCompany.Text),
            New SqlParameter("@telephone", txtNewPhoneNumber.Text),
            New SqlParameter("@telephone2", mtbNewPhoneNumber2.Text),
            New SqlParameter("@fax", mtbNewFaxNumber.Text),
            New SqlParameter("@email", txtNewEmail.Text),
            New SqlParameter("@address1", txtNewAddress.Text),
            New SqlParameter("@address2", Nothing),
            New SqlParameter("@city", txtNewCity.Text),
            New SqlParameter("@state", txtNewState.Text),
            New SqlParameter("@postalCode", mtbNewZipCode.Text),
            New SqlParameter("@userId", CurrentUser.UserID),
            New SqlParameter("@description", txtNewDescrption.Text)
        }

        Dim returnValue As Integer
        DB.SPRunCommand(spName, p, returnValue:=returnValue)

        If returnValue = 0 Then
            MsgBox("Contact added.", MsgBoxStyle.Information, Text)
        Else
            MsgBox("An error occurred.")
        End If

        LoadContactsDataset()
    End Sub

End Class