Imports System.Data.SqlClient

Public Class SBEAPClientSearchTool

#Region " Properties "

    Public ReadOnly Property SelectedClientID() As String
        Get
            Return txtClientID.Text
        End Get
    End Property

#End Region

#Region " Form events "

    Private Sub SBEAPClientSearchTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Me.Modal Then Me.Close()
    End Sub

    Private Sub SBEAPClientSearchTool_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtSearchCompanyName.Focus()
    End Sub

#End Region

#Region " Search procedure "

    Private Sub ClientSearch(Source As String)
        Dim SQL As String = ""
        Dim searchString As String = ""

        Select Case Source
            Case "CompanyName"
                SQL = "Select " &
                    "SBEAPClients.ClientID, " &
                    "SBEAPClients.strCompanyName, " &
                    "SBEAPClients.strCompanyAddress, " &
                    "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where strCompanyName like @searchstring "

                searchString = "%" & txtSearchCompanyName.Text & "%"

            Case "HistoricalCompanyName"
                SQL = "select " &
                    "ClientID, " &
                    "strCompanyName, " &
                    "SBEAPClients.strCompanyAddress, " &
                    "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where strCompanyName like @searchstring " &
                    "union " &
                    "select " &
                    "ClientID, " &
                    "strCompanyName, " &
                    "strCompanyAddress, " &
                    "strCompanyCity " &
                    "from HB_SBEAPClients " &
                    "where strCompanyname like @searchstring "

                searchString = "%" & txtSearchCompanyName.Text & "%"

            Case "StreetAddress"
                SQL = "Select " &
                    "ClientID, " &
                    "strCompanyName, strCompanyAddress, " &
                     "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where strCompanyAddress like @searchstring "

                searchString = "%" & txtSearchStreet.Text & "%"

            Case "City"
                SQL = "Select " &
                    "clientId, " &
                    "strCompanyName, strCompanyCity, " &
                    "strCompanyAddress " &
                    "from SBEAPClients " &
                    "where strCompanyCity like @searchstring "

                searchString = "%" & txtSearchCity.Text & "%"

            Case "ZipCode"
                SQL = "Select " &
                   "clientId, " &
                   "strCompanyName, strCompanyZipCode, " &
                   "strCompanyAddress, strCompanyCity " &
                   "from SBEAPClients " &
                   "where strCompanyZipCode like @searchstring "

                searchString = "%" & txtSearchZipCode.Text & "%"

            Case "County"
                SQL = "Select " &
                    "ClientId, " &
                    "strCompanyName, strCountyName, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients inner join LookUpCountyInformation " &
                    "on SBEAPClients.strCompanyCounty = LookUpCountyInformation.strCountyCode " &
                    "where strCountyName like @searchstring "

                searchString = "%" & txtSearchCounty.Text & "%"

            Case "SIC"
                SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientSIC, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients inner join SBEAPClientData " &
                    "on SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "where strClientSIC like @searchstring "

                searchString = "%" & txtSearchSIC.Text & "%"

            Case "NAICS"
                SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientNAICS, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients inner join SBEAPClientData " &
                    "on SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "where strClientNAICS like @searchstring "

                searchString = "%" & txtSearchNAICS.Text & "%"

            Case "AIRSNumber"
                SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strAIRSNumber, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients inner join SBEAPClientData " &
                    "on SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "where strAIRSNumber like @searchstring "

                searchString = "%" & txtSearchAIRSNumber.Text & "%"

            Case "EmployeeLess"
                SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientEmployees, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and strClientEmployees <= @searchstring "

                searchString = mtbSearchNumberOfEmployees.Text

            Case "EmployeeGreater"
                SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientEmployees, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients inner join SBEAPClientData " &
                    "on SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "where strClientEmployees >= @searchstring "

                searchString = mtbSearchNumberOfEmployees.Text

            Case Else

        End Select

        Dim p As New SqlParameter("@searchstring", searchString)

        If SQL <> "" Then
            dgvClientInformation.DataSource = DB.GetDataTable(SQL, p)

            dgvClientInformation.RowHeadersVisible = False
            dgvClientInformation.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvClientInformation.AllowUserToResizeColumns = True
            dgvClientInformation.AllowUserToAddRows = False
            dgvClientInformation.AllowUserToDeleteRows = False
            dgvClientInformation.AllowUserToOrderColumns = True
            dgvClientInformation.AllowUserToResizeRows = True
            dgvClientInformation.ColumnHeadersHeight = "35"
            dgvClientInformation.Columns("ClientID").HeaderText = "Customer ID"
            dgvClientInformation.Columns("ClientID").DisplayIndex = 0
            dgvClientInformation.Columns("strCompanyName").HeaderText = "Company Name"
            dgvClientInformation.Columns("strCompanyName").DisplayIndex = 1

            dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
            dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"

            Select Case Source
                Case "ZipCode"
                    dgvClientInformation.Columns("strCompanyZipCode").HeaderText = "Zip Code"
                Case "County"
                    dgvClientInformation.Columns("strCountyName").HeaderText = "County"
                Case "SIC"
                    dgvClientInformation.Columns("strClientSIC").HeaderText = "SIC"
                Case "NAICS"
                    dgvClientInformation.Columns("strClientNAICS").HeaderText = "NAICS"
                Case "AIRSNumber"
                    dgvClientInformation.Columns("strAIRSNumber").HeaderText = "AIRS Number"
                Case "EmployeeLess"
                    dgvClientInformation.Columns("strClientEmployees").HeaderText = "# of Employees"
                Case "EmployeeGreater"
                    dgvClientInformation.Columns("strClientEmployees").HeaderText = "# of Employees"
            End Select

            dgvClientInformation.SanelyResizeColumns
        End If
    End Sub

#End Region

#Region " Search buttons "

    Private Sub btnSearchCompanyName_Click(sender As Object, e As EventArgs) Handles btnSearchCompanyName.Click
        If chbSearchHistoricalNames.Checked = False Then
            ClientSearch("CompanyName")
        Else
            ClientSearch("HistoricalCompanyName")
        End If
    End Sub

    Private Sub btnSearchStreet_Click(sender As Object, e As EventArgs) Handles btnSearchStreet.Click
        ClientSearch("StreetAddress")
    End Sub

    Private Sub btnSearchCity_Click(sender As Object, e As EventArgs) Handles btnSearchCity.Click
        ClientSearch("City")
    End Sub

    Private Sub btnSearchZipCode_Click(sender As Object, e As EventArgs) Handles btnSearchZipCode.Click
        ClientSearch("ZipCode")
    End Sub

    Private Sub btnSearchCounty_Click(sender As Object, e As EventArgs) Handles btnSearchCounty.Click
        ClientSearch("County")
    End Sub

    Private Sub btnSearchSIC_Click(sender As Object, e As EventArgs) Handles btnSearchSIC.Click
        ClientSearch("SIC")
    End Sub

    Private Sub btnSearchNAICS_Click(sender As Object, e As EventArgs) Handles btnSearchNAICS.Click
        ClientSearch("NAICS")
    End Sub

    Private Sub btnSearchAIRSNumber_Click(sender As Object, e As EventArgs) Handles btnSearchAIRSNumber.Click
        ClientSearch("AIRSNumber")
    End Sub

    Private Sub btnSearchNumberOfEmployees_Click(sender As Object, e As EventArgs) Handles btnSearchNumberOfEmployees.Click
        If IsNumeric(mtbSearchNumberOfEmployees.Text) Then
            If rdbEmployeeLessThan.Checked Then
                ClientSearch("EmployeeLess")
            Else
                ClientSearch("EmployeeGreater")
            End If
        End If
    End Sub

#End Region

#Region " Results DataGridView "

    Private Sub dgvClientInformation_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientInformation.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvClientInformation.RowCount Then
            txtClientID.Text = dgvClientInformation(0, e.RowIndex).FormattedValue
            txtClientCompanyName.Text = dgvClientInformation(1, e.RowIndex).FormattedValue
            UseSelection.Enabled = True
        End If
    End Sub

#End Region

#Region " Toolbar "

    Private Sub tsbClear_Click(sender As Object, e As EventArgs) Handles tsbClear.Click
        txtSearchAIRSNumber.Clear()
        txtSearchCity.Clear()
        txtSearchCompanyName.Clear()
        txtSearchCounty.Clear()
        txtSearchNAICS.Clear()
        txtSearchSIC.Clear()
        txtSearchStreet.Clear()
        txtSearchZipCode.Clear()
        mtbSearchNumberOfEmployees.Clear()
        chbSearchHistoricalNames.Checked = False
        UseSelection.Enabled = False
        dgvClientInformation.DataSource = Nothing
    End Sub

#End Region

#Region " Accept Button "

    Private Sub TPClientCompanyName_Enter(sender As Object, e As EventArgs) Handles TPClientCompanyName.Enter
        Me.AcceptButton = btnSearchCompanyName
    End Sub

    Private Sub TPAddressSearch_Enter(sender As Object, e As EventArgs) Handles TPAddressSearch.Enter
        Me.AcceptButton = btnSearchStreet
    End Sub

    Private Sub TPCitySearch_Enter(sender As Object, e As EventArgs) Handles TPCitySearch.Enter
        Me.AcceptButton = btnSearchCity
    End Sub

    Private Sub TPZipCodeSearch_Enter(sender As Object, e As EventArgs) Handles TPZipCodeSearch.Enter
        Me.AcceptButton = btnSearchZipCode
    End Sub

    Private Sub TPCountySearch_Enter(sender As Object, e As EventArgs) Handles TPCountySearch.Enter
        Me.AcceptButton = btnSearchCounty
    End Sub

    Private Sub TPSICSearch_Enter(sender As Object, e As EventArgs) Handles TPSICSearch.Enter
        Me.AcceptButton = btnSearchSIC
    End Sub

    Private Sub TPNAICSSearch_Enter(sender As Object, e As EventArgs) Handles TPNAICSSearch.Enter
        Me.AcceptButton = btnSearchNAICS
    End Sub

    Private Sub TPAIRSNumberSearch_Enter(sender As Object, e As EventArgs) Handles TPAIRSNumberSearch.Enter
        Me.AcceptButton = btnSearchAIRSNumber
    End Sub

    Private Sub TPNumberOfEmployees_Enter(sender As Object, e As EventArgs) Handles TPNumberOfEmployees.Enter
        Me.AcceptButton = btnSearchNumberOfEmployees
    End Sub

    Private Sub tabPages_Leave(sender As Object, e As EventArgs) _
    Handles TPClientCompanyName.Leave, TPAddressSearch.Leave, TPCitySearch.Leave, TPZipCodeSearch.Leave,
    TPCountySearch.Leave, TPSICSearch.Leave, TPNAICSSearch.Leave, TPAIRSNumberSearch.Leave, TPNumberOfEmployees.Leave
        Me.AcceptButton = UseSelection
    End Sub

#End Region

End Class