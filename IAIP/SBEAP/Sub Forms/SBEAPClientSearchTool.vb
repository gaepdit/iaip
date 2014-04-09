'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
Imports System.Data.OracleClient

Public Class SBEAPClientSearchTool
    Dim dsSearch As DataSet
    Dim daSearch As OracleDataAdapter


    Private Sub SBEAPClientSearchTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lbl1.Text = "Search for a Client..."
            lbl2.Text = UserName
            lbl3.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub ClientSearch(ByVal Source As String)
        Try

            Select Case Source
                Case "CompanyName"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientID, " & _
                    "" & DBNameSpace & ".SBEAPClients.strCompanyName, " & _
                    "" & DBNameSpace & ".SBEAPClients.strCompanyAddress, " & _
                    "" & DBNameSpace & ".SBEAPClients.strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients " & _
                    "where Upper(strCompanyName) like '%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%' "
                Case "HistoricalCompanyName"
                    SQL = "select " & _
                    "strCompanyName, " & _
                    "ClientID, " & _
                    "" & DBNameSpace & ".SBEAPClients.strCompanyAddress, " & _
                    "" & DBNameSpace & ".SBEAPClients.strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients " & _
                    "where Upper(strCompanyName) like Upper('%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%') " & _
                    "union " & _
                    "select " & _
                    "distinct(strCompanyName), " & _
                    "ClientID, " & _
                    "strCompanyAddress, " & _
                    "strCompanyCity " & _
                    "from HB_SBEAPClients " & _
                    "where Upper(strCompanyname) like Upper('%" & Replace(txtSearchCompanyName.Text.ToUpper, "'", "''") & "%') "
                Case "StreetAddress"
                    SQL = "Select " & _
                    "ClientID, " & _
                    "strCompanyName, strCompanyAddress, " & _
                     "" & DBNameSpace & ".SBEAPClients.strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients " & _
                    "where upper(strCompanyAddress) like ('%" & Replace(txtSearchStreet.Text.ToUpper, "'", "''") & "%') "
                Case "City"
                    SQL = "Select " & _
                    "clientId, " & _
                    "strCompanyName, strCompanyCity, " & _
                    "strCompanyAddress " & _
                    "from " & DBNameSpace & ".SBEAPClients " & _
                    "where upper(strCompanyCity) like ('%" & Replace(txtSearchCity.Text.ToUpper, "'", "''") & "%') "
                Case "ZipCode"
                    SQL = "Select " & _
                   "clientId, " & _
                   "strCompanyName, strCompanyZipCode, " & _
                   "strCompanyAddress, strCompanyCity " & _
                   "from " & DBNameSpace & ".SBEAPClients " & _
                   "where upper(strCompanyZipCode) like ('%" & Replace(txtSearchZipCode.Text.ToUpper, "'", "''") & "%') "
                Case "County"
                    SQL = "Select " & _
                    "ClientId, " & _
                    "strCompanyName, strCountyName, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".LookUpCountyInformation " & _
                    "where " & DBNameSpace & ".SBEAPClients.strCompanyCounty = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                    "and Upper(strCountyName) like ('%" & Replace(txtSearchCounty.Text.ToUpper, "'", "''") & "%') "
                Case "SIC"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientId, " & _
                    "strCompanyName, strClientSIC, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".SBEAPClientData " & _
                    "where " & DBNameSpace & ".SBEAPClients.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID " & _
                    "and upper(strClientSIC) like ('%" & Replace(txtSearchSIC.Text.ToUpper, "'", "''") & "%') "
                Case "NAICS"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientId, " & _
                    "strCompanyName, strClientNAICS, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".SBEAPClientData " & _
                    "where " & DBNameSpace & ".SBEAPClients.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID " & _
                    "and upper(strClientNAICS) like ('%" & Replace(txtSearchNAICS.Text.ToUpper, "'", "''") & "%') "
                Case "AIRSNumber"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientId, " & _
                    "strCompanyName, strAIRSNumber, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".SBEAPClientData " & _
                    "where " & DBNameSpace & ".SBEAPClients.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID " & _
                    "and upper(strAIRSNumber) like ('%" & Replace(txtSearchAIRSNumber.Text.ToUpper, "'", "''") & "%') "
                Case "EmployeeLess"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientId, " & _
                    "strCompanyName, strClientEmployees, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".SBEAPClientData " & _
                    "where " & DBNameSpace & ".SBEAPClients.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID " & _
                    "and strClientEmployees <= ('" & mtbSearchNumberOfEmployees.Text & "') "
                Case "EmployeeGreater"
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SBEAPClients.ClientId, " & _
                    "strCompanyName, strClientEmployees, " & _
                    "strCompanyAddress, strCompanyCity " & _
                    "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".SBEAPClientData " & _
                    "where " & DBNameSpace & ".SBEAPClients.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID " & _
                    "and strClientEmployees >= ('" & mtbSearchNumberOfEmployees.Text & "') "
                Case Else

            End Select

            dsSearch = New DataSet
            daSearch = New OracleDataAdapter(SQL, CurrentConnection)

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
            txtCount.Text = dgvClientInformation.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchCompanyName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompanyName.Click
        Try
            If chbSearchHistoricalNames.Checked = False Then
                ClientSearch("CompanyName")
            Else
                ClientSearch("HistoricalCompanyName")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchStreet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchStreet.Click
        Try
            ClientSearch("StreetAddress")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        Try
            ClientSearch("City")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchZipCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchZipCode.Click
        Try
            ClientSearch("ZipCode")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchCounty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCounty.Click
        Try
            ClientSearch("County")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchSIC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSIC.Click
        Try
            ClientSearch("SIC")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchNAICS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNAICS.Click
        Try
            ClientSearch("NAICS")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSearchAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAIRSNumber.Click
        Try
            ClientSearch("AIRSNumber")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvClientInformation_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvClientInformation.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvClientInformation.HitTest(e.X, e.Y)
            If dgvClientInformation.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvClientInformation.Columns(0).HeaderText = "Customer ID" Then
                    If IsDBNull(dgvClientInformation(0, hti.RowIndex).Value) Then
                        txtClientID.Text = ""
                        txtClientCompanyName.Text = ""
                    Else
                        txtClientID.Text = dgvClientInformation(0, hti.RowIndex).Value
                        txtClientCompanyName.Text = dgvClientInformation(1, hti.RowIndex).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUseClientID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUseClientID.Click
        Try
            If ClientSummary Is Nothing Then
            Else
                ClientSummary.txtClientID.Text = txtClientID.Text
                If ClientSummary.txtClientID.Text <> "" Then
                    ClientSummary.LoadClientData()
                End If
            End If
            If CaseWork Is Nothing Then
            Else
                CaseWork.txtClientID.Text = txtClientID.Text
                If CaseWork.txtClientID.Text <> "" Then
                    CaseWork.LoadClientInfo()
                End If
            End If
            If PhoneLog Is Nothing Then
            Else
                PhoneLog.txtClientID.Text = txtClientID.Text
                If PhoneLog.txtClientID.Text <> "" Then
                    PhoneLog.LoadClientInfo()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

End Class