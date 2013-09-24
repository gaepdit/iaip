Imports System
'Imports System.Security
Imports Oracle.DataAccess.Client
'Imports System.IO
'Imports System.Net.Mail

Public Class SSCPAdministrator
    Dim ds As DataSet
    Dim da As OracleDataAdapter


    Private Sub SSCPAdministrator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub
    Private Sub btnViewAllTV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAllTV.Click
        Try
            SQL = "select " & _
"substr(strAIRSNumber, 5) as AIRSNumber, " & _
"strFacilityName, strOperationalStatus, " & _
"strContactFirstName, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & _
"(select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '20'  " & _
"and strContactEmail is not null " & _
"union " & _
"Select * " & _
"from " & _
"(select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '40'  " & _
"and strContactEmail is not null) FeeContact  " & _
"where not exists " & _
"(select * from (select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '20'  " & _
"and strContactEmail is not null ) ComplianceContact where FeeContact.strAIRSNumber = ComplianceContact.strAIRSNumber) " & _
"union " & _
"select * " & _
"from (select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"'' as strContactFirstName, '' as strContactLastName, " & _
"'' asstrContactEmail, 'No Email' as strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber    " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X') WithoutContacts " & _
"where not exists " & _
"(select * from " & _
"(select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '20'  " & _
"and strContactEmail is not null " & _
"union " & _
"Select * " & _
"from " & _
"(select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '40'  " & _
"and strContactEmail is not null) FeeContact  " & _
"where not exists " & _
"(select * from (select " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber, " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
"strOperationalstatus, " & _
"strContactFirstname, strContactLastName, " & _
"strContactEmail, strContactDescription " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation, " & _
"" & DBNameSpace & ".APBContactInformation " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber   " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+) " & _
"and substr(strAIRProgramCodes, 13, 1) = '1' " & _
"and strOperationalstatus <> 'X' " & _
"and strkey = '20'  " & _
"and strContactEmail is not null ) ComplianceContact where FeeContact.strAIRSNumber = ComplianceContact.strAIRSNumber) " & _
"order by strAIRSNumber ) WithContact where WithoutContacts.strAIRsnumber = WithContact.strAIRSnumber) ) " & _
"order by strAIRSnumber "


            SQL = "select  " & _
"substr(strAIRSNumber, 5) as AIRSNumber,   " & _
"strFacilityName, strOperationalStatus,   " & _
"strContactTitle, strContactCompanyName, " & _
"case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstName, strContactLastName,  " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from   " & _
"(select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,   " & _
"strContactTitle,  strContactCompanyName,  " & _
"case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '20'    " & _
"and strContactEmail is not null   " & _
"union   " & _
"Select *   " & _
"from   " & _
"(select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  strContactTitle,  strContactCompanyName,  " & _
"case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '40'    " & _
"and strContactEmail is not null) FeeContact    " & _
"where not exists   " & _
"(select * from (select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  strContactTitle, strContactCompanyName,  " & _
 "case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '20'    " & _
"and strContactEmail is not null ) ComplianceContact where FeeContact.strAIRSNumber = ComplianceContact.strAIRSNumber)   " & _
"union   " & _
"select *   " & _
"from (select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  '' as strContactTitle, '' as strContactCompanyName,  " & _
"'' as strContactPrefix, " & _
"'' as strContactFirstName, '' as strContactLastName,   " & _
"'' as strContactAddress1, '' as ContactAddress2,  " & _
"'' as strContactCity, '' as strContactState,  " & _
"'' as strContactZipCode,    " & _
"'' asstrContactEmail, 'No Email' as strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber      " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X') WithoutContacts   " & _
"where not exists   " & _
"(select * from   " & _
"(select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  strContactTitle,  strContactCompanyName,  " & _
 "case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '20'    " & _
"and strContactEmail is not null   " & _
"union   " & _
"Select *   " & _
"from   " & _
"(select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  strcontacttitle, strContactCompanyName,  " & _
"case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '40'    " & _
"and strContactEmail is not null) FeeContact    " & _
"where not exists   " & _
"(select * from (select   " & _
"" & DBNameSpace & ".APBHeaderData.strAIRSnumber,   " & _
"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,    " & _
"strOperationalstatus,  strContactTitle, strContactCompanyName,  " & _
 "case " & _
"when strContactPrefix = 'N/A' then '' " & _
"else strContactPrefix " & _
"end strContactPrefix, " & _
"strContactFirstname, strContactLastName,   " & _
"strContactAddress1,  " & _
"case  " & _
"when strContactAddress2 <> 'N/A' and strContactAddress2 <> '' then strContactAddress2  " & _
"else ''  " & _
"end strContactAddress2,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode,    " & _
"strContactEmail, strContactDescription   " & _
"from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation,   " & _
"" & DBNameSpace & ".APBContactInformation   " & _
"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber     " & _
"and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber (+)   " & _
"and substr(strAIRProgramCodes, 13, 1) = '1'   " & _
"and strOperationalstatus <> 'X'   " & _
"and strkey = '20'    " & _
"and strContactEmail is not null ) ComplianceContact where FeeContact.strAIRSNumber = ComplianceContact.strAIRSNumber)   " & _
"order by strAIRSNumber ) WithContact where WithoutContacts.strAIRsnumber = WithContact.strAIRSnumber) )   " & _
"order by strAIRSnumber   "



            ds = New DataSet

            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "ComplianceContacts")
            dgvSSCPContacts.DataSource = ds
            dgvSSCPContacts.DataMember = "ComplianceContacts"


            dgvSSCPContacts.RowHeadersVisible = False
            dgvSSCPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSCPContacts.AllowUserToResizeColumns = True
            dgvSSCPContacts.AllowUserToAddRows = False
            dgvSSCPContacts.AllowUserToDeleteRows = False
            dgvSSCPContacts.AllowUserToOrderColumns = True
            dgvSSCPContacts.AllowUserToResizeRows = True
            dgvSSCPContacts.ColumnHeadersHeight = "35"
            dgvSSCPContacts.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvSSCPContacts.Columns("AIRSNumber").DisplayIndex = 0
            dgvSSCPContacts.Columns("AIRSNumber").Width = 65
            dgvSSCPContacts.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvSSCPContacts.Columns("strFacilityName").DisplayIndex = 1
            dgvSSCPContacts.Columns("strFacilityName").Width = 200

            dgvSSCPContacts.Columns("strOperationalStatus").HeaderText = "Op. Status"
            dgvSSCPContacts.Columns("strOperationalStatus").DisplayIndex = 2
            dgvSSCPContacts.Columns("strOperationalStatus").Width = 50

            dgvSSCPContacts.Columns("strContactPrefix").HeaderText = "Contact Title"
            dgvSSCPContacts.Columns("strContactPrefix").DisplayIndex = 3
            dgvSSCPContacts.Columns("strContactPrefix").Width = 100

            dgvSSCPContacts.Columns("strContactFirstName").HeaderText = "Contact First Name"
            dgvSSCPContacts.Columns("strContactFirstName").DisplayIndex = 4
            dgvSSCPContacts.Columns("strContactFirstName").Width = 100

            dgvSSCPContacts.Columns("strContactLastName").HeaderText = "Contact Last Name"
            dgvSSCPContacts.Columns("strContactLastName").DisplayIndex = 5
            dgvSSCPContacts.Columns("strContactLastName").Width = 100

            dgvSSCPContacts.Columns("strContactTitle").HeaderText = "Contact Job Title"
            dgvSSCPContacts.Columns("strContactTitle").DisplayIndex = 6
            dgvSSCPContacts.Columns("strContactTitle").Width = 100


            dgvSSCPContacts.Columns("strContactEmail").HeaderText = "Contact Email"
            dgvSSCPContacts.Columns("strContactEmail").DisplayIndex = 7
            dgvSSCPContacts.Columns("strContactEmail").Width = 150

            dgvSSCPContacts.Columns("strContactDescription").HeaderText = "Contact Description"
            dgvSSCPContacts.Columns("strContactDescription").DisplayIndex = 8
            dgvSSCPContacts.Columns("strContactDescription").Width = 200

            dgvSSCPContacts.Columns("strContactAddress1").HeaderText = "Address"
            dgvSSCPContacts.Columns("strContactAddress1").DisplayIndex = 9
            dgvSSCPContacts.Columns("strContactAddress1").Width = 100

            dgvSSCPContacts.Columns("strContactAddress2").HeaderText = "Address 2"
            dgvSSCPContacts.Columns("strContactAddress2").DisplayIndex = 10
            dgvSSCPContacts.Columns("strContactAddress2").Width = 100

            dgvSSCPContacts.Columns("strContactCity").HeaderText = "City"
            dgvSSCPContacts.Columns("strContactCity").DisplayIndex = 11
            dgvSSCPContacts.Columns("strContactCity").Width = 100

            dgvSSCPContacts.Columns("strContactState").HeaderText = "State"
            dgvSSCPContacts.Columns("strContactState").DisplayIndex = 12
            dgvSSCPContacts.Columns("strContactState").Width = 100

            dgvSSCPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvSSCPContacts.Columns("strContactZipCode").DisplayIndex = 13
            dgvSSCPContacts.Columns("strContactZipCode").Width = 100

            dgvSSCPContacts.Columns("strContactCompanyName").HeaderText = "Contact Co. Name"
            dgvSSCPContacts.Columns("strContactCompanyName").DisplayIndex = 14
            dgvSSCPContacts.Columns("strContactCompanyName").Width = 100
            txtCount.Text = dgvSSCPContacts.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel()
    End Sub
    Sub ExportToExcel()
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As New Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvSSCPContacts.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvSSCPContacts.ColumnCount - 1
                        .Cells(1, i + 1) = dgvSSCPContacts.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvSSCPContacts.ColumnCount - 1
                        For j = 0 To dgvSSCPContacts.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvSSCPContacts.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Private Sub btnGenerateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateEmail.Click
        Try
            Dim EmailAddress As String = ""
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim StaffPhone As String = ""
            Dim StaffEmail As String = ""
            Dim j As Integer
            Dim BCAddress As String = ""

            For j = 0 To dgvSSCPContacts.RowCount - 1
                EmailAddress = txtBccAddresses.Text & dgvSSCPContacts.Item(13, j).Value.ToString & ", "
                txtBccAddresses.Text = txtBccAddresses.Text & dgvSSCPContacts.Item(13, j).Value.ToString & ", "
            Next
            'Subject = txtEventTitle.Text & " - " & DTPEventDate.Text
            'Body = txtEventTitle.Text & "%0D%0A" & txtEventDescription.Text & "%0D%0A" & _
            'DTPEventDate.Value & " - " & txtEventTime.Text & "%0D%0A" & _
            'vbCrLf & txtEventVenue.Text & "%0D%0A" & _
            'txtEventAddress.Text & "%0D%0A" & txtEventCity.Text & ", " & mtbEventState.Text & " " & mtbEventZipCode.Text

            MsgBox("Emails will be saved to the clipboard." & vbCrLf & "Paste them into the bbc box by hitting ctrl -V", _
                                               MsgBoxStyle.Information, Me.Text)

            Clipboard.SetDataObject(EmailAddress, True)

            System.Diagnostics.Process.Start("mailto: ?subject=" & Subject & "&body=" & _
                                              Body)



            Exit Sub
      
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


End Class