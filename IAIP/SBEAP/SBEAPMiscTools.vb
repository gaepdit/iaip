'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
Imports System.Data.OracleClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class SBEAPMiscTools
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim dsMiscTools As DataSet
    Dim daMiscTools As OracleDataAdapter
    Dim rpt As ReportDocument

    Private Sub SBEAPMiscTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            TCMiscTools.TabPages.Remove(TPCaseWork)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            MiscTools = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnGetContactData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetContactData.Click
        Try
            dsMiscTools = New DataSet

            SQL = "select " & _
            "" & DBNameSpace & ".SBEAPClients.ClientID, " & _
            "strCompanyName, " & _
            "strClientFirstName, strClientLastName, " & _
            "strClientSalutation, strClientCredentials, " & _
            "strClientTitle, strClientPhoneNumber, " & _
            "strClientCellPhone, strClientFax, " & _
            "strClientEmail, strCompanyAddress, " & _
            "strCompanyCity, strCompanyState, " & _
            "strCompanyZipCode, strContactNotes  " & _
            "from " & DBNameSpace & ".SBEAPClientContacts, " & _
            "" & DBNameSpace & ".SBEAPClientLink, " & _
            "" & DBNameSpace & ".SBEAPClients  " & _
            "where " & DBNameSpace & ".SBEAPClientContacts.ClientContactID = " & DBNameSpace & ".SBEAPClientLink.ClientContactID  (+) " & _
            "and " & DBNameSpace & ".SBEAPClientLink.ClientID = " & DBNameSpace & ".SBEAPClients.ClientID (+) "


            SQL = "select " & _
           "" & DBNameSpace & ".SBEAPClients.ClientID, " & _
           "strCompanyName, " & _
           "strClientFirstName, strClientLastName, " & _
           "strClientSalutation, strClientCredentials, " & _
           "strClientTitle, strClientPhoneNumber, " & _
           "strClientCellPhone, strClientFax, " & _
           "strClientEmail, strCompanyAddress, " & _
           "strCompanyCity, strCompanyState, " & _
           "strCompanyZipCode, strContactNotes,  " & _
           "strClientSIC, strClientNAICS, " & _
           "strClientDescription " & _
           "from " & DBNameSpace & ".SBEAPClientContacts, " & _
           "" & DBNameSpace & ".SBEAPClientLink, " & _
           "" & DBNameSpace & ".SBEAPClients, " & _
           "" & DBNameSpace & ".SBEAPClientData " & _
           "where " & DBNameSpace & ".SBEAPClientContacts.ClientContactID = " & DBNameSpace & ".SBEAPClientLink.ClientContactID  (+) " & _
           "and " & DBNameSpace & ".SBEAPClientLink.ClientID = " & DBNameSpace & ".SBEAPClientData.ClientID (+) " & _
           "and " & DBNameSpace & ".SBEAPClientLink.ClientID = " & DBNameSpace & ".SBEAPClients.ClientID (+) "

            daMiscTools = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daMiscTools.Fill(dsMiscTools, "MiscTools")

            dgvMiscTools.DataSource = dsMiscTools
            dgvMiscTools.DataMember = "MiscTools"

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

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvMiscTools.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvMiscTools.ColumnCount - 1
                        .Cells(1, i + 1) = dgvMiscTools.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvMiscTools.ColumnCount - 1
                        For j = 0 To dgvMiscTools.RowCount - 1
                            .Cells(j + 2, i + 1).value = dgvMiscTools.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGetCaseLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCaseLog.Click
        Try
            ds = New DataSet
            rpt = New crCaseWorkLog

            If chbAllDates.Checked = True Then
                SQL = "Select * " & _
                "from AIRBranch.VW_SBEAP_Case_Log " & _
                "Order by datCaseOpened desc "
            Else
                SQL = "Select numcaseID, " & _
                "strCompanyName, strCompanyAddress, " & _
                "strCaseSummary, " & _
                "to_char(to_date(datCaseOpened, 'dd-Mon-yyyy')) as datCaseOpened, " & _
                "to_char(to_date(datCaseClosed, 'dd-Mon-yyyy')) as datCaseClosed, " & _
                "StaffResponsible " & _
                "from AIRBranch.VW_SBEAP_Case_Log " & _
                "where datCaseOpened between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' " & _
                "Order by datCaseOpened desc "
            End If

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_SBEAP_Case_Log")
            rpt.SetDataSource(ds)


            CRVCaseWork.ReportSource = rpt
            DisplayReport(CRVCaseWork, "Case Log")
            CRVCaseWork.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbAllDates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllDates.CheckedChanged
        Try
            If chbAllDates.Checked = True Then
                DTPStartDate.Enabled = False
                DTPEndDate.Enabled = False
            Else
                DTPStartDate.Enabled = True
                DTPEndDate.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class