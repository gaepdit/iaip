Public Class SBEAPMiscTools
    Private Sub SBEAPMiscTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub btnGetContactData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactData.Click
        Dim query As String = "select " &
                "AIRBRANCH.SBEAPClients.ClientID, " &
                "strCompanyName, " &
                "strClientFirstName, strClientLastName, " &
                "strClientSalutation, strClientCredentials, " &
                "strClientTitle, strClientPhoneNumber, " &
                "strClientCellPhone, strClientFax, " &
                "strClientEmail, strCompanyAddress, " &
                "strCompanyCity, strCompanyState, " &
                "strCompanyZipCode, strContactNotes,  " &
                "strClientSIC, strClientNAICS, " &
                "strClientDescription " &
                "from AIRBRANCH.SBEAPClientContacts, " &
                "AIRBRANCH.SBEAPClientLink, " &
                "AIRBRANCH.SBEAPClients, " &
                "AIRBRANCH.SBEAPClientData " &
                "where AIRBRANCH.SBEAPClientContacts.ClientContactID = AIRBRANCH.SBEAPClientLink.ClientContactID  (+) " &
                "and AIRBRANCH.SBEAPClientLink.ClientID = AIRBRANCH.SBEAPClientData.ClientID (+) " &
                "and AIRBRANCH.SBEAPClientLink.ClientID = AIRBRANCH.SBEAPClients.ClientID (+) "

        Dim dtMiscTools As DataTable = DB.GetDataTable(query)

        dgvMiscTools.DataSource = dtMiscTools

        dgvMiscTools.RowHeadersVisible = False
        dgvMiscTools.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvMiscTools.AllowUserToResizeColumns = True
        dgvMiscTools.AllowUserToAddRows = False
        dgvMiscTools.AllowUserToDeleteRows = False
        dgvMiscTools.AllowUserToOrderColumns = True
        dgvMiscTools.AllowUserToResizeRows = True
        dgvMiscTools.ColumnHeadersHeight = "35"
        dgvMiscTools.Columns("ClientID").HeaderText = "Customer ID"
        dgvMiscTools.Columns("ClientID").DisplayIndex = 0
        dgvMiscTools.Columns("strCompanyName").HeaderText = "Company Name"
        dgvMiscTools.Columns("strCompanyName").DisplayIndex = 1
        dgvMiscTools.Columns("strClientFirstName").HeaderText = "First Name"
        dgvMiscTools.Columns("strClientFirstName").DisplayIndex = 2
        dgvMiscTools.Columns("strClientLastName").HeaderText = "Last Name"
        dgvMiscTools.Columns("strClientLastName").DisplayIndex = 3
        dgvMiscTools.Columns("strClientSalutation").HeaderText = "Salutation"
        dgvMiscTools.Columns("strClientSalutation").DisplayIndex = 4
        dgvMiscTools.Columns("strClientCredentials").HeaderText = "Credentials"
        dgvMiscTools.Columns("strClientCredentials").DisplayIndex = 5
        dgvMiscTools.Columns("strClientTitle").HeaderText = "Customer Title"
        dgvMiscTools.Columns("strClientTitle").DisplayIndex = 6
        dgvMiscTools.Columns("strClientPhoneNumber").HeaderText = "Phone Number"
        dgvMiscTools.Columns("strClientPhoneNumber").DisplayIndex = 7
        dgvMiscTools.Columns("strClientCellPhone").HeaderText = "Cell Phone"
        dgvMiscTools.Columns("strClientCellPhone").DisplayIndex = 8
        dgvMiscTools.Columns("strClientFax").HeaderText = "Fax Number"
        dgvMiscTools.Columns("strClientFax").DisplayIndex = 9
        dgvMiscTools.Columns("strClientEmail").HeaderText = "Email"
        dgvMiscTools.Columns("strClientEmail").DisplayIndex = 10
        dgvMiscTools.Columns("strCompanyAddress").HeaderText = "Customer Address"
        dgvMiscTools.Columns("strCompanyAddress").DisplayIndex = 11
        dgvMiscTools.Columns("strCompanyCity").HeaderText = "Customer City"
        dgvMiscTools.Columns("strCompanyCity").DisplayIndex = 12
        dgvMiscTools.Columns("strCompanyState").HeaderText = "Customer State"
        dgvMiscTools.Columns("strCompanyState").DisplayIndex = 13
        dgvMiscTools.Columns("strCompanyZipCode").HeaderText = "Customer Zip Code"
        dgvMiscTools.Columns("strCompanyZipCode").DisplayIndex = 14
        dgvMiscTools.Columns("strContactNotes").HeaderText = "Notes"
        dgvMiscTools.Columns("strContactNotes").DisplayIndex = 15
        dgvMiscTools.Columns("strClientSIC").HeaderText = "SIC"
        dgvMiscTools.Columns("strClientSIC").DisplayIndex = 16
        dgvMiscTools.Columns("strClientNAICS").HeaderText = "NAICS"
        dgvMiscTools.Columns("strClientNAICS").DisplayIndex = 17
        dgvMiscTools.Columns("strClientDescription").HeaderText = "Client Description"
        dgvMiscTools.Columns("strClientDescription").DisplayIndex = 18

        txtCount.Text = dgvMiscTools.RowCount.ToString
    End Sub

    Private Sub ExportToExcel_Click(sender As Object, e As EventArgs) Handles ExportToExcel.Click
        dgvMiscTools.ExportToExcel(Me)
    End Sub
End Class