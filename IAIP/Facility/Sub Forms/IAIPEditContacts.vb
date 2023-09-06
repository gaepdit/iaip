Imports System.Data.SqlClient
Imports Iaip.DAL
Imports Iaip.Apb
Imports EpdIt.DBUtilities
Imports System.Text

Public Class IAIPEditContacts

    Public Property AirsNumber As ApbFacilityId
    Friend Property Key As ContactKey

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        ParseParameters()
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
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
        Dim query As New StringBuilder("select STRKEY as [Key],
               case
                   when STRKEY = '10' then 'Current Monitoring Contact'
                   when STRKEY = '20' then 'Current Compliance Contact'
                   when STRKEY = '30' then 'Current Permitting Contact'
                   when STRKEY = '40' then 'Current Fee Contact'
                   when STRKEY = '41' then 'Current EIS Contact'
                   when left(STRKEY, 1) = '1'
                       then 'Past Monitoring Contact ' + right(STRKEY, 1)
                   when left(STRKEY, 1) = '2'
                       then 'Past Compliance Contact ' + right(STRKEY, 1)
                   when left(STRKEY, 1) = '3'
                       then 'Past Permitting Contact ' + right(STRKEY, 1)
                   else 'Unknown'
               end                            as [Contact type],
               convert(date, DATMODIFINGDATE) as [Updated],
               STRCONTACTFIRSTNAME            as [First name],
               STRCONTACTLASTNAME             as [Last name],
               STRCONTACTPREFIX               as Honorific,
               STRCONTACTSUFFIX               as Suffix,
               STRCONTACTTITLE                as Title,
               STRCONTACTCOMPANYNAME          as [Company name],
               STRCONTACTPHONENUMBER1         as [Phone number 1],
               STRCONTACTPHONENUMBER2         as [Phone number 2],
               STRCONTACTFAXNUMBER            as [Fax number],
               STRCONTACTEMAIL                as [Email address],
               STRCONTACTADDRESS1             as [Address line 1],
               STRCONTACTCITY                 as City,
               STRCONTACTSTATE                as State,
               STRCONTACTZIPCODE              as [Postal code],
               STRCONTACTDESCRIPTION          as Comments
        from APBCONTACTINFORMATION
        where STRAIRSNUMBER = @airs ")

        If chkShowHistory.Checked Then
            query.Append(" and convert(int, STRKEY) <= 41 ")
        Else
            query.Append(" and STRKEY in ('10', '20', '30', '40', '41') ")
        End If

        query.Append(" order by [Contact type]")

        Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)

        ContactsDataGrid.DataSource = DB.GetDataTable(query.ToString, p)
        ContactsDataGrid.Columns("Key").Visible = False

        ClearForm()
    End Sub

    Private Sub LoadSelectedContact()
        Dim row As DataGridViewRow = ContactsDataGrid.SelectedRows.Item(0)

        If row Is Nothing Then
            ClearForm()
            Return
        End If

        txtNewFirstName.Text = GetNullableString(row.Cells("First name").Value)
        txtNewLastName.Text = GetNullableString(row.Cells("Last name").Value)
        txtNewPrefix.Text = GetNullableString(row.Cells("Honorific").Value)
        txtNewSuffix.Text = GetNullableString(row.Cells("Suffix").Value)
        txtNewTitle.Text = GetNullableString(row.Cells("Title").Value)
        txtNewCompany.Text = GetNullableString(row.Cells("Company name").Value)
        txtNewPhoneNumber.Text = GetNullableString(row.Cells("Phone number 1").Value)
        mtbNewPhoneNumber2.Text = GetNullableString(row.Cells("Phone number 2").Value)
        mtbNewFaxNumber.Text = GetNullableString(row.Cells("Fax number").Value)
        txtNewEmail.Text = GetNullableString(row.Cells("Email address").Value)
        txtNewAddress.Text = GetNullableString(row.Cells("Address line 1").Value)
        txtNewCity.Text = GetNullableString(row.Cells("City").Value)
        txtNewState.Text = GetNullableString(row.Cells("State").Value)
        mtbNewZipCode.Text = GetNullableString(row.Cells("Postal code").Value)
        txtNewDescrption.Text = GetNullableString(row.Cells("Comments").Value)

        rdbNewMonitoringContact.Checked = False
        rdbNewComplianceContact.Checked = False
        rdbNewPermittingContact.Checked = False
        rdbNewFeeContact.Checked = False
        rdbNewEISContact.Checked = False

        Select Case CInt(row.Cells("Key").Value)
            Case 10 To 19
                Key = ContactKey.Monitoring
                rdbNewMonitoringContact.Checked = True
            Case 20 To 29
                Key = ContactKey.Compliance
                rdbNewComplianceContact.Checked = True
            Case 30 To 39
                Key = ContactKey.Permitting
                rdbNewPermittingContact.Checked = True
            Case 40
                Key = ContactKey.Fees
                rdbNewFeeContact.Checked = True
            Case 41
                Key = ContactKey.EmissionInventory
                rdbNewEISContact.Checked = True
        End Select
        btnSaveNewContact.Visible = False
        btnSaveNewContact.Enabled = False
        btnUpdateContact.Visible = True
        btnUpdateContact.Enabled = True
        btnUpdateContact.Text = "Update selected contact"

        If row.Cells("Contact type").Value.ToString.Substring(0, 4) = "Past" Then
            btnUpdateContact.Text = "Save as new contact"
            Key = ContactKey.None
        End If
    End Sub

    Private Sub ContactsDataGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles ContactsDataGrid.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < ContactsDataGrid.RowCount AndAlso ContactsDataGrid.SelectedRows.Count = 1 Then
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

        Key = ContactKey.None

        btnSaveNewContact.Enabled = True
        btnUpdateContact.Enabled = False
        btnSaveNewContact.Visible = True
        btnUpdateContact.Visible = False

        ContactsDataGrid.SelectNone()
    End Sub

    Private Sub btnUpdateContact_Click(sender As Object, e As EventArgs) Handles btnUpdateContact.Click
        Dim reKey As ContactKey

        If rdbNewMonitoringContact.Checked Then
            reKey = ContactKey.Monitoring
        ElseIf rdbNewComplianceContact.Checked Then
            reKey = ContactKey.Compliance
        ElseIf rdbNewPermittingContact.Checked Then
            reKey = ContactKey.Permitting
        ElseIf rdbNewFeeContact.Checked Then
            reKey = ContactKey.Fees
        ElseIf rdbNewEISContact.Checked Then
            reKey = ContactKey.EmissionInventory
        Else
            MsgBox("Select a Contact Type first." & vbCrLf & "No data saved.", MsgBoxStyle.Information, Text)
            Return
        End If

        If reKey = Key Then
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
            Return
        End If

        SaveNewContact(reKey)
    End Sub

    Private Sub btnSaveNewContact_Click(sender As Object, e As EventArgs) Handles btnSaveNewContact.Click
        Dim reKey As ContactKey

        If rdbNewMonitoringContact.Checked Then
            reKey = ContactKey.Monitoring
        ElseIf rdbNewComplianceContact.Checked Then
            reKey = ContactKey.Compliance
        ElseIf rdbNewPermittingContact.Checked Then
            reKey = ContactKey.Permitting
        ElseIf rdbNewFeeContact.Checked Then
            reKey = ContactKey.Fees
        ElseIf rdbNewEISContact.Checked Then
            reKey = ContactKey.EmissionInventory
        Else
            MsgBox("Select a Contact Type first." & vbCrLf & "No data saved.", MsgBoxStyle.Information, Text)
            Return
        End If

        SaveNewContact(reKey)
    End Sub

    Private Sub SaveNewContact(reKey As ContactKey)
        If ContactKeyExists(reKey, AirsNumber) Then
            Dim response As DialogResult = MessageBox.Show("A contact of that type already exists. Do you want to replace it?",
                                                           "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                                           MessageBoxDefaultButton.Button2)
            If response = DialogResult.No Then Return
        End If

        Dim spName As String = "iaip_facility.SaveApbContact"

        Dim p As SqlParameter() = {
            New SqlParameter("@key", reKey.ToString("D")),
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

    Private Sub chkShowHistory_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowHistory.CheckedChanged
        LoadContactsDataset()
    End Sub

    Private Sub ContactsDataGrid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles ContactsDataGrid.RowPrePaint
        If ContactsDataGrid.Rows(e.RowIndex).Cells("Contact type").Value.ToString.StartsWith("Past ") Then
            ContactsDataGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(&HFFDDBBBB)
        End If
    End Sub

    Private Sub rdbNewMonitoringContact_CheckedChanged(sender As Object, e As EventArgs) Handles _
            rdbNewMonitoringContact.CheckedChanged, rdbNewComplianceContact.CheckedChanged,
            rdbNewPermittingContact.CheckedChanged, rdbNewFeeContact.CheckedChanged,
            rdbNewEISContact.CheckedChanged

        If Not btnUpdateContact.Visible Then Return

        Dim reKey As ContactKey

        If rdbNewMonitoringContact.Checked Then
            reKey = ContactKey.Monitoring
        ElseIf rdbNewComplianceContact.Checked Then
            reKey = ContactKey.Compliance
        ElseIf rdbNewPermittingContact.Checked Then
            reKey = ContactKey.Permitting
        ElseIf rdbNewFeeContact.Checked Then
            reKey = ContactKey.Fees
        ElseIf rdbNewEISContact.Checked Then
            reKey = ContactKey.EmissionInventory
        Else
            Return
        End If

        If reKey = Key Then
            btnUpdateContact.Text = "Update selected contact"
        Else
            btnUpdateContact.Text = "Save as new contact"
        End If
    End Sub

End Class