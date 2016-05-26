Imports System.Data.SqlClient

Public Class SBEAPClientSearchTool
    Dim SQL As String
    Dim dsSearch As DataSet
    Dim daSearch As SqlDataAdapter

#Region " Form events "

    Private Sub SBEAPClientSearchTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Me.Modal Then Me.Close()
    End Sub

    Private Sub SBEAPClientSearchTool_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtSearchCompanyName.Focus()
    End Sub

#End Region

#Region " Form properties "

    Public ReadOnly Property SelectedClientID() As String
        Get
            Return txtClientID.Text
        End Get
    End Property

#End Region

#Region " Search procedures "

    Sub ClientSearch(ByVal Source As String)
        Try

            Select Case Source
                Case "CompanyName"
                    SQL = "Select " &
                    "SBEAPClients.ClientID, " &
                    "SBEAPClients.strCompanyName, " &
                    "SBEAPClients.strCompanyAddress, " &
                    "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where Upper(strCompanyName) like '%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%' "
                Case "HistoricalCompanyName"
                    SQL = "select " &
                    "strCompanyName, " &
                    "ClientID, " &
                    "SBEAPClients.strCompanyAddress, " &
                    "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where Upper(strCompanyName) like Upper('%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%') " &
                    "union " &
                    "select " &
                    "distinct(strCompanyName), " &
                    "ClientID, " &
                    "strCompanyAddress, " &
                    "strCompanyCity " &
                    "from HB_SBEAPClients " &
                    "where Upper(strCompanyname) like Upper('%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%') "
                Case "StreetAddress"
                    SQL = "Select " &
                    "ClientID, " &
                    "strCompanyName, strCompanyAddress, " &
                     "SBEAPClients.strCompanyCity " &
                    "from SBEAPClients " &
                    "where upper(strCompanyAddress) like ('%" & Replace(txtSearchStreet.Text.ToUpper, "'", "''") & "%') "
                Case "City"
                    SQL = "Select " &
                    "clientId, " &
                    "strCompanyName, strCompanyCity, " &
                    "strCompanyAddress " &
                    "from SBEAPClients " &
                    "where upper(strCompanyCity) like ('%" & Replace(txtSearchCity.Text.ToUpper, "'", "''") & "%') "
                Case "ZipCode"
                    SQL = "Select " &
                   "clientId, " &
                   "strCompanyName, strCompanyZipCode, " &
                   "strCompanyAddress, strCompanyCity " &
                   "from SBEAPClients " &
                   "where upper(strCompanyZipCode) like ('%" & Replace(txtSearchZipCode.Text.ToUpper, "'", "''") & "%') "
                Case "County"
                    SQL = "Select " &
                    "ClientId, " &
                    "strCompanyName, strCountyName, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, LookUpCountyInformation " &
                    "where SBEAPClients.strCompanyCounty = LookUpCountyInformation.strCountyCode " &
                    "and Upper(strCountyName) like ('%" & Replace(txtSearchCounty.Text.ToUpper, "'", "''") & "%') "
                Case "SIC"
                    SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientSIC, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and upper(strClientSIC) like ('%" & Replace(txtSearchSIC.Text.ToUpper, "'", "''") & "%') "
                Case "NAICS"
                    SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientNAICS, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and upper(strClientNAICS) like ('%" & Replace(txtSearchNAICS.Text.ToUpper, "'", "''") & "%') "
                Case "AIRSNumber"
                    SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strAIRSNumber, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and upper(strAIRSNumber) like ('%" & Replace(txtSearchAIRSNumber.Text.ToUpper, "'", "''") & "%') "
                Case "EmployeeLess"
                    SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientEmployees, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and strClientEmployees <= ('" & mtbSearchNumberOfEmployees.Text & "') "
                Case "EmployeeGreater"
                    SQL = "Select " &
                    "SBEAPClients.ClientId, " &
                    "strCompanyName, strClientEmployees, " &
                    "strCompanyAddress, strCompanyCity " &
                    "from SBEAPClients, SBEAPClientData " &
                    "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
                    "and strClientEmployees >= ('" & mtbSearchNumberOfEmployees.Text & "') "
                Case Else

            End Select

            dsSearch = New DataSet
            daSearch = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSearch.Fill(dsSearch, "SearchResult")
            dgvClientInformation.DataSource = dsSearch
            dgvClientInformation.DataMember = "SearchResult"

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
            dgvClientInformation.Columns("ClientID").Width = 60
            dgvClientInformation.Columns("strCompanyName").HeaderText = "Company Name"
            dgvClientInformation.Columns("strCompanyName").DisplayIndex = 1
            dgvClientInformation.Columns("strCompanyName").Width = 300
            Select Case Source
                Case "CompanyName"
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 2
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "HistoricalCompanyName"
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 2
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "StreetAddress"
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 2
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "City"
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 2
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                Case "ZipCode"
                    dgvClientInformation.Columns("strCompanyZipCode").HeaderText = "Zip Code"
                    dgvClientInformation.Columns("strCompanyZipCode").DisplayIndex = 2
                    dgvClientInformation.Columns("strCompanyZipCode").Width = 75
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "County"
                    dgvClientInformation.Columns("strCountyName").HeaderText = "County"
                    dgvClientInformation.Columns("strCountyName").DisplayIndex = 2
                    dgvClientInformation.Columns("strCountyName").Width = 300
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "SIC"
                    dgvClientInformation.Columns("strClientSIC").HeaderText = "SIC"
                    dgvClientInformation.Columns("strClientSIC").DisplayIndex = 2
                    dgvClientInformation.Columns("strClientSIC").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "NAICS"
                    dgvClientInformation.Columns("strClientNAICS").HeaderText = "NAICS"
                    dgvClientInformation.Columns("strClientNAICS").DisplayIndex = 2
                    dgvClientInformation.Columns("strClientNAICS").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "AIRSNumber"
                    dgvClientInformation.Columns("strAIRSNumber").HeaderText = "AIRS Number"
                    dgvClientInformation.Columns("strAIRSNumber").DisplayIndex = 2
                    dgvClientInformation.Columns("strAIRSNumber").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "EmployeeLess"
                    dgvClientInformation.Columns("strClientEmployees").HeaderText = "# of Employees"
                    dgvClientInformation.Columns("strClientEmployees").DisplayIndex = 2
                    dgvClientInformation.Columns("strClientEmployees").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case "EmployeeGreater"
                    dgvClientInformation.Columns("strClientEmployees").HeaderText = "# of Employees"
                    dgvClientInformation.Columns("strClientEmployees").DisplayIndex = 2
                    dgvClientInformation.Columns("strClientEmployees").Width = 100
                    dgvClientInformation.Columns("strCompanyAddress").HeaderText = "Street Address"
                    dgvClientInformation.Columns("strCompanyAddress").DisplayIndex = 3
                    dgvClientInformation.Columns("strCompanyAddress").Width = 150
                    dgvClientInformation.Columns("strCompanyCity").HeaderText = "Street City"
                    dgvClientInformation.Columns("strCompanyCity").DisplayIndex = 4
                    dgvClientInformation.Columns("strCompanyCity").Width = 100
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region " Search buttons "

    Private Sub btnSearchCompanyName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompanyName.Click
        Try
            If chbSearchHistoricalNames.Checked = False Then
                ClientSearch("CompanyName")
            Else
                ClientSearch("HistoricalCompanyName")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchStreet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchStreet.Click
        Try
            ClientSearch("StreetAddress")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        Try
            ClientSearch("City")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchZipCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchZipCode.Click
        Try
            ClientSearch("ZipCode")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchCounty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCounty.Click
        Try
            ClientSearch("County")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchSIC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSIC.Click
        Try
            ClientSearch("SIC")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchNAICS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNAICS.Click
        Try
            ClientSearch("NAICS")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAIRSNumber.Click
        Try
            ClientSearch("AIRSNumber")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchNumberOfEmployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNumberOfEmployees.Click
        Try
            If rdbEmployeeLessThan.Checked = True Then
                ClientSearch("EmployeeLess")
            Else
                ClientSearch("EmployeeGreater")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

#End Region

#Region " Results DataGridView "

    Private Sub dgvClientInformation_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvClientInformation.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvClientInformation.HitTest(e.X, e.Y)

        Try
            If dgvClientInformation.RowCount > 0 And hti.RowIndex <> -1 Then
                txtClientID.Text = dgvClientInformation(0, hti.RowIndex).FormattedValue
                txtClientCompanyName.Text = dgvClientInformation(1, hti.RowIndex).FormattedValue
                UseSelection.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Toolbar "

    Private Sub tsbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClear.Click
        txtSearchAIRSNumber.Clear()
        txtSearchCity.Clear()
        txtSearchCompanyName.Clear()
        txtSearchCounty.Clear()
        txtSearchNAICS.Clear()
        txtSearchSIC.Clear()
        txtSearchStreet.Clear()
        txtSearchZipCode.Clear()
        chbSearchHistoricalNames.Checked = False
        UseSelection.Enabled = False
        dgvClientInformation.DataSource = Nothing
    End Sub

#End Region

#Region " Accept Button "

    Private Sub TPClientCompanyName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPClientCompanyName.Enter
        Me.AcceptButton = btnSearchCompanyName
    End Sub

    Private Sub TPAddressSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPAddressSearch.Enter
        Me.AcceptButton = btnSearchStreet
    End Sub

    Private Sub TPCitySearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPCitySearch.Enter
        Me.AcceptButton = btnSearchCity
    End Sub

    Private Sub TPZipCodeSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPZipCodeSearch.Enter
        Me.AcceptButton = btnSearchZipCode
    End Sub

    Private Sub TPCountySearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPCountySearch.Enter
        Me.AcceptButton = btnSearchCounty
    End Sub

    Private Sub TPSICSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPSICSearch.Enter
        Me.AcceptButton = btnSearchSIC
    End Sub

    Private Sub TPNAICSSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPNAICSSearch.Enter
        Me.AcceptButton = btnSearchNAICS
    End Sub

    Private Sub TPAIRSNumberSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPAIRSNumberSearch.Enter
        Me.AcceptButton = btnSearchAIRSNumber
    End Sub

    Private Sub TPNumberOfEmployees_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPNumberOfEmployees.Enter
        Me.AcceptButton = btnSearchNumberOfEmployees
    End Sub

    Private Sub tabPages_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles TPClientCompanyName.Leave, TPAddressSearch.Leave, TPCitySearch.Leave, TPZipCodeSearch.Leave,
    TPCountySearch.Leave, TPSICSearch.Leave, TPNAICSSearch.Leave, TPAIRSNumberSearch.Leave, TPNumberOfEmployees.Leave
        Me.AcceptButton = UseSelection
    End Sub

#End Region

End Class