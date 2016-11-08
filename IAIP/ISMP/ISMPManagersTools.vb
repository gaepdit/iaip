'Imports System.DateTime
Imports System.Data.SqlClient
Imports System.Windows.Forms
'Imports Microsoft.Office.Core
Imports System.IO
Imports System
Imports System.Data
'Imports System.Text
'Imports System.Data.SqlClient

Public Class ISMPManagersTools
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    'Dim Paneltemp1 As String
    Dim SQL, SQL2, SQL3 As String
    Dim SQL4, SQL5, SQL6 As String
    Dim cmd, cmd2, cmd3 As SqlCommand
    Dim cmd4, cmd5, cmd6 As SqlCommand
    Dim dr, dr2, dr3 As SqlDataReader
    Dim dr4, dr5, dr6 As SqlDataReader
    Dim recExist As Boolean
    Dim dsEngineer As DataSet
    Dim dsCounty As DataSet
    Dim dsCity As DataSet
    Dim dsFacilityList As DataSet
    Dim daEngineer As SqlDataAdapter
    Dim daCounty As SqlDataAdapter
    Dim daCity As SqlDataAdapter
    Dim dsTestReportAssignments As DataSet
    Dim daTestreportAssignments As SqlDataAdapter
    Dim daFacilityList As SqlDataAdapter
    Dim dsEngineerGrid As DataSet
    Dim daEngineerGrid As SqlDataAdapter
    Dim dsSummaryReport As DataSet
    Dim daSummaryReport As SqlDataAdapter
    Dim dsExcelFiles As DataSet
    Dim daUnitStats As SqlDataAdapter
    Dim daExcelFiles As SqlDataAdapter
    Dim dsUnitStats As DataSet
    Dim dsMethods As DataSet
    Dim daMethods As SqlDataAdapter


    Private Sub ISMPManagersTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ShowCorrectTabs()
            LoadComboBoxDataSets()
            LoadComboBoxes()
            LoadTestReportAssignmentDataSet()
            LoadLVTestReportAssignment()
            FormatEngineerTestReportGrid()
            FormatTestSummaryGrid()

            DTPUnitStatsStartDate.Value = Date.Today.AddDays(-30)
            DTPUnitStatsEndDate.Value = Today
            DTPMonthlyStart.Value = Date.Today.AddDays(-30)
            DTPMonthlyEnd.Value = Today
            DTPEngineerTestReportStart.Text = Date.Today.AddDays(-30)
            DTPEngineerTestReportEnd.Value = Today

            LoadMethods()
            dtpAddTestReportDateReceived.Value = Today
            DTPAddTestReportDateCompleted.Value = Today
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub ShowCorrectTabs()
        Try

            TCManagersTools.TabPages.Remove(TPAIRSReportsPrinted)
            TCManagersTools.TabPages.Remove(TPMonthlyReport)
            TCManagersTools.TabPages.Remove(TPReportAssignment)
            TCManagersTools.TabPages.Remove(TPUnitStatistics2)
            TCManagersTools.TabPages.Remove(TPMiscTools)

            TCMiscTools.TabPages.Remove(TPMethods)

            'Program & Unit Manager 
            If AccountFormAccess(17, 3) = "1" Then
                TCManagersTools.TabPages.Add(TPReportAssignment)
                TCManagersTools.TabPages.Add(TPMonthlyReport)
                TCManagersTools.TabPages.Add(TPUnitStatistics2)
                TCManagersTools.TabPages.Add(TPAIRSReportsPrinted)
                TCManagersTools.TabPages.Add(TPMiscTools)
                TCMiscTools.TabPages.Add(TPMethods)

                LoadAFSPrintList()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadComboBoxDataSets()
        Try

            SQL = "select " &
            "(strLastName|| ', ' ||strFirstName) as UserName,  " &
            "numUserID, numUnit  " &
            "from EPDUSerProfiles, LookUpEPDUnits  " &
            "where EPDUSerProfiles.numUnit = LookUpEPDUnits.numUnitCode  " &
            "and numProgram = '3'  " &
            "and numUnit <> '14'  " &
            "and numEmployeeStatus = '1' " &
            "and numUserID <> '0' " &
            "order by strlastname"

            dsEngineer = New DataSet

            daEngineer = New SqlDataAdapter(SQL, CurrentConnection)

            daEngineer.Fill(dsEngineer, "Engineers")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadComboBoxes()
        Try

            cboEngineer.Items.Add(" ")

            For Each row As DataRow In dsEngineer.Tables("Engineers").Rows
                cboEngineer.Items.Add(row("UserName"))
                clbEngineersList2.Items.Add(row("UserName"))
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,   " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart,  " &
                "strEmissionSource,   " &
                "(Select strPollutantDescription   " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant   " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,   " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer   " &
                "from EPDUserProfiles, ISMPReportInformation   " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer   " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer   " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation   " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ( strclosed = 'False' or strClosed is null ) " &
                "and ISMPReportInformation.strReviewingEngineer = '0'  " &
                "and ISMPReportInformation.strDelete is NULL "
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadLVTestReportAssignment()
        Try

            LVTestReportAssignment.View = View.Details
            LVTestReportAssignment.AllowColumnReorder = True
            LVTestReportAssignment.CheckBoxes = True
            LVTestReportAssignment.GridLines = True
            LVTestReportAssignment.FullRowSelect = True

            Dim dtTestReportAssignment As New DataTable
            dtTestReportAssignment = dsTestReportAssignments.Tables("TestReportAssignment")

            Dim drtestReportAssignment As DataRow()

            Dim row As DataRow

            drtestReportAssignment = dtTestReportAssignment.Select()

            LVTestReportAssignment.Columns.Add("Reference Number", 100, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("Facility Name", 200, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("AIRS Number", 100, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("Test Date", 100, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("Emission Source Tested", 200, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("Pollutant Tested", 100, HorizontalAlignment.Left)
            LVTestReportAssignment.Columns.Add("Reviewing Engineer", 100, HorizontalAlignment.Left)

            For Each row In drtestReportAssignment

                Dim item1 As New ListViewItem(row("strReferenceNumber").ToString())
                item1.Checked = False
                item1.SubItems.Add(row("strFacilityName").ToString())
                item1.SubItems.Add(Mid(row("strAIRSNumber").ToString(), 5))
                item1.SubItems.Add(row("ForTestDateStart").ToString())
                item1.SubItems.Add(row("strEmissionSource").ToString())
                item1.SubItems.Add(row("Pollutant").ToString())
                item1.SubItems.Add(row("ReviewingEngineer").ToString())

                LVTestReportAssignment.Items.AddRange(New ListViewItem() {item1})

            Next row

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatEngineerTestReportGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "EngineerGrid"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReferenceNumber"
            objtextcol.HeaderText = "Reference #"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFacilityName"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.Width = 300
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "AIRSNumber"
            objtextcol.HeaderText = "AIRS Number"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strClosed"
            objtextcol.HeaderText = "Record Status"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ForDatTestDateStart"
            objtextcol.HeaderText = "Date Started"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ForDatReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ForDatCompleteDate"
            objtextcol.HeaderText = "Date Completed"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReviewingEngineer"
            objtextcol.HeaderText = "Reviewing Engineer"
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "WitnessingEngineer"
            objtextcol.HeaderText = "Witnessing Engineer"
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrEngineersFacilityList.TableStyles.Clear()
            dgrEngineersFacilityList.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrEngineersFacilityList.CaptionText = "Engineer Test Reports"
            dgrEngineersFacilityList.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatTestSummaryGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Test Summary"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Staff"
            objtextcol.HeaderText = "Staff"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "OpenReports"
            objtextcol.HeaderText = "# of Open Reports"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "OpenFiftys"
            objtextcol.HeaderText = "Reports Open >50 days"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ClosedReports"
            objtextcol.HeaderText = "Reports Closed Last 60 days"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrTestSummary.TableStyles.Clear()
            dgrTestSummary.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrTestSummary.CaptionText = "Source Test Summary"
            dgrTestSummary.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadAFSPrintList()
        Dim temp As String = ""

        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            SQL = "Select * from ISMPDocumentType"
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            While dr.Read
                temp = dr.Item("strKey")
                Select Case temp
                    Case "002"
                        If dr.Item("strAFSPrint") = False Then
                            chbOneStack2Runs.Checked = True
                        Else
                            chbOneStack2Runs.Checked = False
                        End If
                    Case "003"
                        If dr.Item("strAFSPrint") = False Then
                            chbOneStack3Runs.Checked = True
                        Else
                            chbOneStack3Runs.Checked = False
                        End If
                    Case "004"
                        If dr.Item("strAFSPrint") = False Then
                            chbOneStack4Runs.Checked = True
                        Else
                            chbOneStack4Runs.Checked = False
                        End If
                    Case "005"
                        If dr.Item("strAFSPrint") = False Then
                            chbTwoStack.Checked = True
                        Else
                            chbTwoStack.Checked = False
                        End If
                    Case "006"
                        If dr.Item("strAFSPrint") = False Then
                            chbTwoStackDRE.Checked = True
                        Else
                            chbTwoStackDRE.Checked = False
                        End If
                    Case "007"
                        If dr.Item("strAFSPrint") = False Then
                            chbLoadingRack.Checked = True
                        Else
                            chbLoadingRack.Checked = False
                        End If
                    Case "008"
                        If dr.Item("strAFSPrint") = False Then
                            chbTreatmentPonds.Checked = True
                        Else
                            chbTreatmentPonds.Checked = False
                        End If
                    Case "009"
                        If dr.Item("strAFSPrint") = False Then
                            chbGasTests.Checked = True
                        Else
                            chbGasTests.Checked = False
                        End If
                    Case "010"
                        If dr.Item("strAFSPrint") = False Then
                            chbFlare.Checked = True
                        Else
                            chbFlare.Checked = False
                        End If
                    Case "011"
                        If dr.Item("strAFSPrint") = False Then
                            chbRATA.Checked = True
                        Else
                            chbRATA.Checked = False
                        End If
                    Case "012"
                        If dr.Item("strAFSPrint") = False Then
                            chbMemorandum.Checked = True
                        Else
                            chbMemorandum.Checked = False
                        End If
                    Case "013"
                        If dr.Item("strAFSPrint") = False Then
                            chbMemorandumToFile.Checked = True
                        Else
                            chbMemorandumToFile.Checked = False
                        End If
                    Case "014"
                        If dr.Item("strAFSPrint") = False Then
                            chbMethod9Multi.Checked = True
                        Else
                            chbMethod9Multi.Checked = False
                        End If
                    Case "015"
                        If dr.Item("strAFSPrint") = False Then
                            chbMethod22.Checked = True
                        Else
                            chbMethod22.Checked = False
                        End If
                    Case "016"
                        If dr.Item("strAFSPrint") = False Then
                            chbMethod9Single.Checked = True
                        Else
                            chbMethod9Single.Checked = False
                        End If
                    Case "017"
                        If dr.Item("strAFSPrint") = False Then
                            chbPEMS.Checked = True
                        Else
                            chbPEMS.Checked = False
                        End If
                    Case "018"
                        If dr.Item("strAFSPrint") = False Then
                            chbPTE.Checked = True
                        Else
                            chbPTE.Checked = False
                        End If
                End Select

            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadMethods()
        Try
            SQL = "Select " &
            "strMethodCode, strMethodDesc " &
            "From LookUpISMPMethods " &
            "order by strMethodCode "

            dsMethods = New DataSet
            daMethods = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daMethods.Fill(dsMethods, "Methods")
            dgvMethods.DataSource = dsMethods
            dgvMethods.DataMember = "Methods"



            dgvMethods.RowHeadersVisible = False
            dgvMethods.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMethods.AllowUserToResizeColumns = True
            dgvMethods.AllowUserToAddRows = False
            dgvMethods.AllowUserToDeleteRows = False
            dgvMethods.AllowUserToOrderColumns = True
            dgvMethods.AllowUserToResizeRows = True
            dgvMethods.Columns("strMethodCode").HeaderText = "Method Code"
            dgvMethods.Columns("strMethodCode").DisplayIndex = 0
            dgvMethods.Columns("strMethodCode").Visible = False
            dgvMethods.Columns("strMethodDesc").HeaderText = "Determination Method"
            dgvMethods.Columns("strMethodDesc").DisplayIndex = 1
            dgvMethods.Columns("strMethodDesc").Width = 500

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Different Test Report Assignment Data Sets"
    Sub LoadAllNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUSerProfiles, ISMPReportInformation  " &
                "where EPDUSerProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                  "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadUnassignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strReviewingEngineer = '0' " &
                  "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadAssignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                  "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadDeletedTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strDelete is not NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadByTestReportTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                 "and ISMPReportInformation.strReviewingEngineer = '0' " &
                 "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadByTestReportAllTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
    Sub LoadByTestReportAssignedTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                    "strEmissionSource,  " &
                    "(Select strPollutantDescription  " &
                    "from LookUPPollutants, ISMPReportInformation  " &
                    "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                    "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                    "from EPDUserProfiles, ISMPReportInformation  " &
                    "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                    "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                    "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                    "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                    "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                    "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                    "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadByAIRSNumberTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPMaster.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " &
                "and ISMPReportInformation.strDelete is NULL"
            Else
                SQL = ""
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New SqlDataAdapter

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daTestreportAssignments.SelectCommand = cmd

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daTestreportAssignments.Fill(dsTestReportAssignments, "TestReportAssignment")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
    Private Sub LVTestReportAssignment_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LVTestReportAssignment.ColumnClick
        Try

            LVTestReportAssignment.ListViewItemSorter = New ListViewItemComparer(e.Column)
            LVTestReportAssignment.Sort()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub ISMPManagersTools_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Functions"
#Region "Saves"
    Sub SaveTestReportsAssignments()
        Dim strObject As String
        Dim EngineerGCode As String = ""
        Dim AssignDate As String = ""
        Dim PreCompliance As String = ""
        Dim dtEngineers As New DataTable
        dtEngineers = dsEngineer.Tables("Engineers")
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try


            drEngineers = dtEngineers.Select("UserName = '" & cboEngineer.Text & "'")
            For Each row In drEngineers
                EngineerGCode = row("numUserID")
            Next
            If chbNonComplianceTestReport.Checked = True Then
                PreCompliance = "True"
            Else
                PreCompliance = "False"
            End If

            If EngineerGCode <> "" Then

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                For Each strObject In lblTestReportAssignment.Items
                    SQL = "select format(datReviewedBYUnitManager, 'dd-MMM-yyyy') as ReviewedByUnitManager " &
                          "from ISMPReportInformation " &
                          "where strReferenceNumber = '" & strObject.ToString() & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AssignDate = dr.Item("ReviewedByUnitManager")
                    End While
                    If AssignDate = "04-Jul-1776" Then
                        AssignDate = TodayFormatted
                    Else
                        'AssignDate = AssignDate
                    End If

                    SQL = "Update ISMPReportInformation set " &
                    "strReviewingEngineer = '" & EngineerGCode & "', " &
                    "datReviewedBYUnitManager = '" & AssignDate & "', " &
                    "strReviewingUnit = '" & CurrentUser.UnitId.ToString & "', " &
                    "numReviewingManager = '" & CurrentUser.UserID & "', " &
                    "strPreComplianceStatus = '" & PreCompliance & "' " &
                    "where ISMPReportInformation.strReferenceNumber = '" & strObject.ToString() & "'"

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader

                Next
                LoadTestReportAssignmentDataSet()
                LVTestReportAssignment.Clear()
                LoadLVTestReportAssignment()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
            Else
                MsgBox("Select an Engineer to Assign these Test Reports to first.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveAIRSPrinting()
        Dim OneStack2 As String
        Dim OneStack3 As String
        Dim OneStack4 As String
        Dim TwoStackStandard As String
        Dim TwoStackDRE As String
        Dim LoadingRack As String
        Dim PondTreatment As String
        Dim GasConc As String
        Dim Flare As String
        Dim Rata As String
        Dim MemoStandard As String
        Dim MemoFile As String
        Dim Method9Multi As String
        Dim Method22 As String
        Dim Method9Single As String
        Dim PEMS As String
        Dim PTE As String

        Try


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            If chbOneStack2Runs.Checked = True Then
                OneStack2 = False
            Else
                OneStack2 = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & OneStack2 & "' " &
            "where strKEy = '002'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbOneStack3Runs.Checked = True Then
                OneStack3 = False
            Else
                OneStack3 = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & OneStack3 & "' " &
            "where strKEy = '003'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbOneStack4Runs.Checked = True Then
                OneStack4 = False
            Else
                OneStack4 = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & OneStack4 & "' " &
            "where strKEy = '004'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbTwoStack.Checked = True Then
                TwoStackStandard = False
            Else
                TwoStackStandard = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & TwoStackStandard & "' " &
            "where strKEy = '005'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If Me.chbTwoStackDRE.Checked = True Then
                TwoStackDRE = False
            Else
                TwoStackDRE = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & TwoStackDRE & "' " &
            "where strKEy = '006'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbLoadingRack.Checked = True Then
                LoadingRack = False
            Else
                LoadingRack = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & LoadingRack & "' " &
            "where strKEy = '007'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbTreatmentPonds.Checked = True Then
                PondTreatment = False
            Else
                PondTreatment = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & PondTreatment & "' " &
            "where strKEy = '008'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbGasTests.Checked = True Then
                GasConc = False
            Else
                GasConc = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & GasConc & "' " &
            "where strKEy = '009'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbFlare.Checked = True Then
                Flare = False
            Else
                Flare = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & Flare & "' " &
            "where strKEy = '010'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbRATA.Checked = True Then
                Rata = False
            Else
                Rata = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & Rata & "' " &
            "where strKEy = '011'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMemorandum.Checked = True Then
                MemoStandard = False
            Else
                MemoStandard = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & MemoStandard & "' " &
            "where strKEy = '012'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMemorandumToFile.Checked = True Then
                MemoFile = False
            Else
                MemoFile = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & MemoFile & "' " &
            "where strKEy = '013'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod9Multi.Checked = True Then
                Method9Multi = False
            Else
                Method9Multi = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & Method9Multi & "' " &
            "where strKEy = '014'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod22.Checked = True Then
                Method22 = False
            Else
                Method22 = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & Method22 & "' " &
            "where strKEy = '015'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod9Single.Checked = True Then
                Method9Single = False
            Else
                Method9Single = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & Method9Single & "' " &
            "where strKEy = '016'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbPEMS.Checked = True Then
                PEMS = False
            Else
                PEMS = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & PEMS & "' " &
            "where strKEy = '017'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbPTE.Checked = True Then
                PTE = False
            Else
                PTE = True
            End If
            SQL = "Update ISMPDocumentType set " &
            "strAFSPrint = '" & PTE & "' " &
            "where strKEy = '018'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Run Statistics"
    Sub EngineerTestReport()
        Dim strObject As Object
        Dim DateBias As String = ""
        Dim Engineer As String = "and ("
        Dim dtEngineers As New DataTable
        dtEngineers = dsEngineer.Tables("Engineers")
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try

            If rdbEngineerTestReportTestDate.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPEngineerTestReportStart.Text & "' " &
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPEngineerTestReportStart.Text & "' " &
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPEngineerTestReportStart.Text & "' " &
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If

            For Each strObject In clbEngineersList2.CheckedItems
                drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    Engineer += "strReviewingEngineer = '" & row("numUserID") & "' or "
                Next
            Next

            If Engineer = "and (" Then
                Engineer = "and strReviewingEngineer = '0' "
            Else
                Engineer = Mid(Engineer, 1, (Len(Engineer) - 3)) & ") "
            End If

            SQL = "Select ISMPReportInformation.strReferenceNumber, strFacilityName, " &
            "SUBSTRING(ISMPMaster.strAIRSNumber, 5,8) as AIRSNumber, strClosed, " &
            "format(datTestDateStart, 'dd-MMM-yyyy') as ForDatTestDateStart, " &
            "format(datReceivedDate, 'dd-MMM-yyyy') as ForDatReceivedDate, " &
            "format(datCompleteDate, 'dd-MMM-yyyy') as ForDatCompleteDate, " &
            "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " &
            "from EPDUserProfiles, ISMPReportInformation  " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer, " &
            "(Select (strLastName|| ', ' ||strFirstName) as WitnessingEngineer " &
            "from EPDUserProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
            "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as WitnessingEngineer " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and " & DateBias & " " & Engineer & " "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dsEngineerGrid = New DataSet

            daEngineerGrid = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEngineerGrid.Fill(dsEngineerGrid, "EngineerGrid")
            dgrEngineersFacilityList.DataSource = dsEngineerGrid
            dgrEngineersFacilityList.DataMember = "EngineerGrid"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub EngineerOpenTestReports()
        Dim strObject As Object
        Dim Engineer As String = "and ("
        Dim dtEngineers As New DataTable
        dtEngineers = dsEngineer.Tables("Engineers")
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try

            For Each strObject In clbEngineersList2.CheckedItems
                drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    Engineer += "strReviewingEngineer = '" & row("numUserID") & "' or "
                Next
            Next

            If Engineer = "and (" Then
                Engineer = "and strReviewingEngineer = '0' "
            Else
                Engineer = Mid(Engineer, 1, (Len(Engineer) - 3)) & ") "
            End If

            SQL = "Select " &
            "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer " &
            "from EPDUserProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
            "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer, " &
            "ISMPReportInformation.strReferenceNumber, strFacilityName, " &
            "format(datReceivedDate, 'dd-MMM-yyyy') as ForDatReceivedDate, " &
            "DATEDIFF(day, datReceivedDate, GETDATE()) as Days " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where " &
            "ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and strClosed = 'False' " &
            Engineer &
            "Order by strReviewingEngineer "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            lsbEngineers.Items.Clear()

            While dr.Read
                lsbEngineers.Items.Add(dr.Item("ReviewingEngineer") & vbTab & " \ " & vbTab & dr.Item("strReferenceNumber") _
                & vbTab & " \ " & vbTab & dr.Item("strFacilityName") & vbTab & " \ " & vbTab & dr.Item("ForDatReceivedDate") _
                & vbTab & " \ " & vbTab & "(" & dr.Item("Days") & ")")
            End While

        Catch ex As Exception
            ErrorReport(ex, SQL, "ISMPManagersTools.EngineerOpenTestReports")
        Finally

        End Try


    End Sub
#End Region
    Sub RunMonthlyReport()
        Dim TestReceived As String = 0
        Dim TestCompleted As String = 0
        Dim TestWitnessed As String = 0
        Dim OutofCompliance As String = 0
        Dim MedianTime As String = 0
        Dim PercentileTime As String = 0
        Dim n As Integer = 0
        Dim MedianArray(n) As Decimal
        Dim Percential As Decimal

        Try


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            'Tests Received in Date Range
            SQL = "Select count(*) as Count from ISMPReportInformation " &
            "where datReceivedDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strDelete is NULL"
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestReceived = dr.Item("Count")
            End While

            'Tests Completed in Date Range 
            SQL = "Select count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strClosed = 'True' and strDelete is NULL "
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestCompleted = dr.Item("Count")
            End While

            'Tests Witnessed in Date Range
            SQL = "Select Count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strDelete is NULL and (strWitnessingEngineer <> '0' or strWitnessingEngineer2 <> '0') "
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestWitnessed = dr.Item("Count")
            End While

            'Tests out of compliance 
            SQL = "Select Count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strDelete is NULL and strComplianceStatus = '05' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                OutofCompliance = dr.Item("count")
            End While

            'Test Median 
            SQL = "Select (datCompleteDate - datReceivedDate) as diff from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strDelete is NULL " &
            "and strClosed = 'True' order by diff desc"
            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            While dr.Read
                ReDim Preserve MedianArray(n)
                MedianArray(n) = CInt(dr.Item("Diff"))
                n = n + 1
            End While

            If txtPercential.Text <> "" Then
                Percential = CDec(txtPercential.Text)
                If Percential > 0.1 And Percential < 99.999999 Then
                    Percential = Percential / 100
                Else
                    Percential = 0.8
                End If
            Else
                Percential = 0.8
            End If

            Array.Sort(MedianArray)

            If MedianArray.GetLength(0) Mod 2 = 0 Then
                MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                If MedianArray.GetLength(0) <= 2 Then
                    PercentileTime = "Unavailable"
                Else
                    PercentileTime = (MedianArray((MedianArray.GetLength(0) * Percential) - 1) + MedianArray((MedianArray.GetLength(0) * Percential))) / 2
                End If
            Else
                MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                If MedianArray.GetLength(0) <= 2 Then
                    PercentileTime = "Unavailable"
                Else
                    PercentileTime = MedianArray(MedianArray.GetLength(0) * Percential)
                End If
            End If

            txtReceived.Text = TestReceived
            txtTestCompleted.Text = TestCompleted
            txtTestWitnessed.Text = TestWitnessed
            txtTestOutOfCompliance.Text = OutofCompliance
            txtMedianTimeToComplete.Text = MedianTime
            txt80Percent.Text = PercentileTime

            Dim Text As String
            Dim Received As String
            Dim Completed As String
            Dim Compliance As String
            Dim Witnessed As String

            Select Case txtReceived.Text
                Case 0
                    Received = "There were no test reports received for this month. "
                Case 1
                    Received = "ISMP received " + txtReceived.Text + " test report this month. "
                Case Else
                    Received = "ISMP received " + txtReceived.Text + " test reports this month. "
            End Select

            Select Case txtTestCompleted.Text
                Case 0
                    Completed = "There were no completed reviews of test reports this month, "
                Case 1
                    Completed = "ISMP completed reviews of " + txtTestCompleted.Text + " test report this month, "
                Case Else
                    Completed = "ISMP completed reviews of " + txtTestCompleted.Text + " test reports this month, "
            End Select

            Select Case txtTestOutOfCompliance.Text
                Case 0
                    Compliance = " "
                Case 1
                    Compliance = "There was " + txtTestOutOfCompliance.Text + " reviewed report that showed noncompliance this month."
                Case Else
                    Compliance = "There were " + txtTestOutOfCompliance.Text + " reviewed reports that showed noncompliance this month."
            End Select

            Select Case txtTestWitnessed.Text
                Case 0
                    Witnessed = "and ISMP staff was not on-site during any of these tests. "
                Case 1
                    Witnessed = "and ISMP staff was on-site during 1 of these tests. "
                Case Else
                    Witnessed = "and ISMP staff was on-site during " + txtTestWitnessed.Text + " of these tests. "
            End Select

            Text = "Test Reports" + vbCrLf + Received + vbCr + Completed + vbCrLf + Witnessed + vbCrLf + Compliance

            txtReportText.Text = Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GetOutOfComplianceReport()
        Dim Report As String
        Dim CompanyName As String
        Dim CompanyLocation As String
        Dim SourceTested As String
        Dim PollutantDetermined As String
        Dim TestDate As String
        Dim temp1 As String
        Dim temp2 As String
        Dim Refnum As String
        Dim AIRSNumber As String
        Dim dash As String
        Dim ReportType As String

        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            SQL = "Select ISMPReportInformation.strReferenceNumber, strEmissionSource, strPollutantDescription, " &
            "format(datTestDateStart, 'dd-MMM-yyyy') as fordatTestDateStart, format(datTestDateEnd, 'dd-MMM-yyyy') as fordatTestDateEnd, " &
            "SUBSTRING(ISMPMaster.strAIRSNumber, 5,8) as AIRSNumber, strFacilityName, strFacilityCity, strFacilityState, " &
            "ISMPReportType.strReportType " &
            "from ISMPReportInformation, LookUPPollutants, ISMPMaster, APBFacilityInformation, " &
            "ISMPReportType " &
            "where strDelete is NULL and strComplianceStatus = '05' " &
            "and datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " &
            "and strPollutantCode = strPOllutant " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and APBFacilityInformation.strAIRSNumber = ISMPMaster.strAIRSNumber " &
            "and ISMPReportInformation.strReportType = ISMPReportType.strKey "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            Report = ""

            While dr.Read
                CompanyName = "Company Name -- " & vbTab & dr.Item("strFacilityName")
                CompanyLocation = "Company City -- " & vbTab & dr.Item("strFacilityCity") & ", " & dr.Item("strFacilityState")
                SourceTested = "Source Tested -- " & vbTab & dr.Item("strEmissionSource")
                ReportType = "Report Type -- " & vbTab & dr.Item("strReportType")
                PollutantDetermined = "Pollutant Tested -- " & vbTab & dr.Item("strPollutantDescription")
                temp1 = dr.Item("fordatTestDateStart")
                temp2 = dr.Item("fordatTestDateEnd")
                If temp1 = temp2 Then
                    TestDate = "Testing Date(s) -- " & vbTab & temp1
                Else
                    TestDate = "Testing Date(s) -- " & vbTab & temp1 & " - " & temp2
                End If
                Refnum = "Reference Number -- " & vbTab & dr.Item("strReferenceNumber")
                AIRSNumber = "AIRS Number -- " & vbTab & Mid(dr.Item("AIRSNumber"), 1, 3) & "-" & Mid(dr.Item("AIRSNumber"), 4)
                dash = "-------------------------------------------------------------------------------------------------"

                Report = Report & CompanyName & Environment.NewLine & CompanyLocation & Environment.NewLine &
                SourceTested & Environment.NewLine & " " & Environment.NewLine & ReportType & Environment.NewLine &
                PollutantDetermined & Environment.NewLine & TestDate & Environment.NewLine & Refnum & Environment.NewLine &
                AIRSNumber & Environment.NewLine & dash & Environment.NewLine

            End While

            txtOutOfComplianceReport.SelectionTabs = New Integer() {30, 260}
            txtOutOfComplianceReport.Text = Report

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearTestReportAssignmentTab()
        Try

            cboEngineer.Text = ""
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = 0
            LVTestReportAssignment.Clear()
            LoadTestReportAssignmentDataSet()
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ExportToWord()
        'Dim WordApp As New Word.ApplicationClass
        'Dim wordDoc As Word.DocumentClass
        Dim wordDoc As Microsoft.Office.Interop.Word.Document
        Dim WordApp As New Microsoft.Office.Interop.Word.Application
        Try

            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(txtReportText.Text & vbCrLf & vbCrLf & txtOutOfComplianceReport.Text)
            WordApp.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub RunSummaryReport()
        Try

            SQL = "Select " &
             "distinct(Case " &
             "when strFirstName = ' ' then '-Unassigned' " &
             "Else strFirstName " &
             "End) as Staff, " &
             "Case " &
             "    When OpenReports is Null then 0 " &
             "    Else OpenReports " &
             "End as OpenReports, " &
             "Case " &
             "	 When ClosedReports is Null then 0 " &
             "    Else ClosedReports " &
             "End as ClosedReports, " &
             "Case " &
             "    When OpenFiftys is Null then 0 " &
             "    Else OpenFiftys " &
             "End as OpenFiftys " &
             "From (SELECT EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenReports " &
             "    FROM EPDUserProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'False' ) " &
             "    and EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "Group by strfirstname) OpenReport, " &
             "(SELECT EPDUserProfiles.STRFIRSTNAME as Engineer, Count(*) as ClosedReports " &
             "    FROM EPDUserProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'True' ) " &
             "    and EPDUSerProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "    and datCompleteDate Between DATEADD(day, -60, GETDATE()) and GETDATE() " &
             "Group by strfirstname) ClosedReport, " &
             "(SELECT EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenFiftys " &
             "    FROM EPDUSerProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'False' ) " &
             "    and EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "    and datReceivedDate <= DATEADD(day, -50, GETDATE() ) " &
             "Group by strfirstname) OLdOpen, " &
             "EPDUserProfiles " &
             "where strFirstname = OpenReport.Engineer (+) " &
             "and strFirstName = ClosedReport.Engineer (+) " &
             "and strFirstName = OldOpen.Engineer (+) " &
             "and (OpenReports > '0' or ClosedReports > '0'  or OpenFiftys > '0') " &
             "Order by Staff "

            dsSummaryReport = New DataSet

            daSummaryReport = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSummaryReport.Fill(dsSummaryReport, "Test Summary")
            dgrTestSummary.DataSource = dsSummaryReport
            dgrTestSummary.DataMember = "Test Summary"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub PrintSummaryReport()
        Dim i As Integer = 0
        Dim j As Integer

        Try

            If dsSummaryReport Is Nothing Then
            Else
                i = dsSummaryReport.Tables(0).Rows.Count
            End If

            If i <> 0 Then
                Dim WordText As String
                'Dim WordApp As New Word.ApplicationClass
                'Dim wordDoc As Word.DocumentClass
                Dim wordDoc As Microsoft.Office.Interop.Word.Document
                Dim WordApp As New Microsoft.Office.Interop.Word.Application
                Dim line As String = "________________________________________________________________________"

                WordText = vbTab & vbTab & vbTab & vbTab & vbTab & "ISMP" &
                 vbCrLf & line & vbCrLf & "Source Test Summary" & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Print Date: " & TodayFormatted &
                  vbCrLf & line & vbCrLf & "Staff" & vbTab & vbTab & "# of Open" & vbTab & vbTab & "Reports Open" & vbTab & vbTab & "Reports Close" &
                  vbCrLf & vbTab & vbTab & "Reports" & vbTab & vbTab & ">50 days" & vbTab & vbTab & "Last 60 days" &
                  vbCrLf & line &
                  vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|"

                For j = 0 To i - 1
                    If dgrTestSummary.Item(j, 0).length > 6 Then
                        WordText = WordText &
                        vbCrLf & dgrTestSummary.Item(j, 0) & vbTab & dgrTestSummary.Item(j, 1) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 2) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 3) &
                        vbCrLf & line
                    Else
                        WordText = WordText &
                        vbCrLf & dgrTestSummary.Item(j, 0) & vbTab & vbTab & dgrTestSummary.Item(j, 1) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 2) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 3) &
                        vbCrLf & line
                    End If
                Next

                wordDoc = WordApp.Documents.Add()
                wordDoc.Activate()
                WordApp.Selection.TypeText(WordText)
                WordApp.Visible = True
            Else
                MsgBox("You must run the Report First", MsgBoxStyle.Information, "ISMP Managers Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RunUnitStatistics2()
        Try
            SQL = "select " &
            "distinct(strLastName|| ', ' ||strFirstName) as Engineer,  " &
            "strUnitDesc, totalreceived,  " &
            "ReceivedCount,  " &
            "round((ReceivedCount/TotalReceived)*100, 2) as ProgramPercent,   " &
            "MedDays,  " &
            "PercentDays,  " &
            "(Witness1.witcount + witness2.witcount + Witness3.witcount) as Witnessed " &
            "from ISMPReportInformation, EPDUserProfiles,  " &
            "LookUpEPDUnits, " &
            "(select count(*) as TotalReceived " &
            "from ISMPReportInformation  " &
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null) " &
            "and strReviewingEngineer <> '0' " &
            "and strClosed = 'True') TotalReviewed,  " &
            "(select strReviewingEngineer, Count(*) as ReceivedCount " &
            "from ISMPReportInformation   " &
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete is Null or strDelete <> 'True') " &
            "and strReviewingEngineer <> '0'  " &
            "and strClosed = 'True'  " &
            "group by strReviewingEngineer) TotalRec,  " &
            "(select strReviewingEngineer,  " &
            "Median(dayin) as MedDays    " &
            "from  " &
            "(select  " &
            "strReviewingEngineer,  " &
            "case  " &
            "when strClosed = 'True' then DATEDIFF(day, datReceivedDate, datCompleteDate)  " &
            "when strClosed = 'False' then DATEDIFF(day, datReceivedDate, GETDATE()) " &
            "END DayIn " &
            "from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null) " &
            "and strClosed = 'True'  " &
            "and strReviewingEngineer <> '0') SubTable  " &
            "group by strReviewingEngineer) MedianTotal,  " &
            "(select strReviewingEngineer,  " &
            "Percentile_cont(0.8) within Group(Order by DaysIn) as percentDays  " &
            "from  " &
            "(select  " &
            "strReviewingEngineer,  " &
            "case  " &
            "when strClosed = 'True' then DATEDIFF(day, datReceivedDate, datCompleteDate)  " &
            "when strClosed = 'False' then DATEDIFF(day, datReceivedDate, GETDATE()) " &
            "END DaysIn " &
            "from ISMPReportInformation " &
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null) " &
            "and strReviewingEngineer <> '0'  " &
            "and strClosed = 'True')  " &
            "group by strReviewingEngineer) PercentDays,  " &
            "(select ISMPReportInformation.strWitnessingEngineer,  " &
            "count(*) as WitCount " &
            "from ISMPReportInformation   " &
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and ISMPReportInformation.strWitnessingEngineer <> '0' " &
            "and strClosed = 'True'  " &
            "group by ISMPReportInformation.strWitnessingEngineer) Witness1,  " &
            "(select ISMPReportInformation.strWitnessingEngineer2,  " &
            "count(*) as WitCount " &
            "from ISMPReportInformation   " &
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and ISMPReportInformation.strWitnessingEngineer2 <> '0' " &
            "and strClosed = 'True'  " &
            "group by ISMPReportInformation.strWitnessingEngineer2) Witness2,  " &
            "(select  ISMPWitnessingEng.strWitnessingEngineer,  " &
            "count(*) as WitCount " &
            "from ISMPReportInformation, ISMPWitnessingEng    " &
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and ISMPReportInformation.strReferenceNumber = ISMPWitnessingEng.strReferenceNumber   " &
            "and strClosed = 'True'  " &
            "group by ISMPWitnessingEng.strWitnessingEngineer) Witness3  " &
            "where ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID  " &
            "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "' " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and ISMPReportInformation.strReviewingEngineer <> '0'  " &
            "and ISMPReportInformation.strReviewingEngineer = TotalRec.strReviewingEngineer (+)  " &
            "and ISMPREportINformation.strReviewingEngineer = MedianTotal.strReviewingEngineer (+) " &
            "and ISMPREportINformation.strReviewingEngineer = PercentDays.strReviewingEngineer (+) " &
            "and ISMPREportINformation.strReviewingEngineer = Witness1.strWitnessingEngineer (+)  " &
            "and ISMPREportINformation.strReviewingEngineer = Witness2.strWitnessingEngineer2 (+) " &
            "and ISMPREportINformation.strReviewingEngineer = Witness3.strWitnessingEngineer (+)  " &
            "order by strUnitDesc, Engineer "

            dsUnitStats = New DataSet
            daUnitStats = New SqlDataAdapter(SQL, CurrentConnection)
            daUnitStats.Fill(dsUnitStats, "UnitStats")
            dgvUnitStats.DataSource = dsUnitStats
            dgvUnitStats.DataMember = "UnitStats"

            dgvUnitStats.RowHeadersVisible = False
            dgvUnitStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUnitStats.AllowUserToResizeColumns = True
            dgvUnitStats.AllowUserToAddRows = False
            dgvUnitStats.AllowUserToDeleteRows = False
            dgvUnitStats.AllowUserToOrderColumns = True
            dgvUnitStats.AllowUserToResizeRows = True
            dgvUnitStats.ColumnHeadersHeight = "35"
            dgvUnitStats.Columns("Engineer").HeaderText = "Engineer"
            dgvUnitStats.Columns("strUnitDesc").HeaderText = "Engineer Unit"
            dgvUnitStats.Columns("TotalReceived").HeaderText = "Total Reviewed"
            dgvUnitStats.Columns("TotalReceived").Visible = False
            dgvUnitStats.Columns("ReceivedCount").HeaderText = "Engineer Reviewed"
            dgvUnitStats.Columns("ProgramPercent").HeaderText = "% Program "
            dgvUnitStats.Columns("MedDays").HeaderText = "Median Days"
            dgvUnitStats.Columns("PercentDays").HeaderText = "80% Days"
            dgvUnitStats.Columns("Witnessed").HeaderText = "Witnessed"

            Try
                txtTotalReviewed.Text = dgvUnitStats(2, 0).Value
            Catch ex As Exception
                txtTotalReviewed.Text = "0"
            End Try

            txtEngineerCount.Text = dgvUnitStats.RowCount.ToString

            Dim TotalAvg As Decimal = 0
            Dim MedianAvg As Decimal = 0
            Dim PercentialAvg As Decimal = 0
            Dim WitnessAvg As Decimal = 0
            Dim x As Integer = 0

            For x = 0 To dgvUnitStats.RowCount - 1
                If IsDBNull(dgvUnitStats(3, x).Value) Then
                    TotalAvg = TotalAvg + 0
                Else
                    TotalAvg = TotalAvg + (dgvUnitStats(3, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
                If IsDBNull(dgvUnitStats(5, x).Value) Then
                    MedianAvg = MedianAvg + 0
                Else
                    MedianAvg = MedianAvg + (dgvUnitStats(5, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
                If IsDBNull(dgvUnitStats(6, x).Value) Then
                    PercentialAvg = PercentialAvg + 0
                Else
                    PercentialAvg = PercentialAvg + (dgvUnitStats(6, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
                If IsDBNull(dgvUnitStats(7, x).Value) Then
                    WitnessAvg = WitnessAvg + 0
                Else
                    WitnessAvg = WitnessAvg + (dgvUnitStats(7, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
            Next

            txtAverageofTotalReviewed.Text = TotalAvg
            txtAverageMedianDays.Text = MedianAvg
            txtPercentialAverage.Text = PercentialAvg
            txtAverageWitnessed.Text = WitnessAvg

            txtUnitStatsCount.Clear()
            txtUnitStatReferenceNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
    Private Sub TBManagersTools_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBManagersTools.ButtonClick
        Try

            Select Case TBManagersTools.Buttons.IndexOf(e.Button)
                Case 0
                    If TPReportAssignment.Focus = True Then
                        SaveTestReportsAssignments()
                    ElseIf TPAIRSReportsPrinted.Focus = True Then
                        SaveAIRSPrinting()
                    End If
                Case 1
                    If TPReportAssignment.Focus = True Then
                        cboEngineer.Text = ""
                        lblTestReportAssignment.Items.Clear()
                        txtTestReportCount.Text = 0
                        LVTestReportAssignment.Clear()
                        LoadLVTestReportAssignment()
                    ElseIf TPMethods.Focus = True Then
                        txtMethodCode.Clear()
                        txtMethodDescription.Clear()
                        txtMethodNumber.Clear()
                    End If
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Menu Items"
    Private Sub MmiSave_Click(sender As Object, e As EventArgs) Handles MmiSave.Click
        Try

            If TPReportAssignment.Focus = True Then
                SaveTestReportsAssignments()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiClear_Click(sender As Object, e As EventArgs) Handles MmiClear.Click
        Try

            ClearTestReportAssignmentTab()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiClearTab_Click(sender As Object, e As EventArgs) Handles MmiClearTab.Click
        Try

            If TPReportAssignment.Focus = True Then
                ClearTestReportAssignmentTab()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
#Region "Viewing Options"
    Private Sub MmiViewTestReports_Click(sender As Object, e As EventArgs) Handles MmiViewTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAllNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiUnassignedTestReports_Click(sender As Object, e As EventArgs) Handles MmiUnassignedTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadUnassignedNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTestReports_Click(sender As Object, e As EventArgs) Handles MmiAssignedTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAssignedNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiShowDeletedRecords_Click(sender As Object, e As EventArgs) Handles MmiShowDeletedRecords.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadDeletedTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                txtAIRSNumber.Text = facilityLookupDialog.SelectedAirsNumber
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub MmiViewByFacility_Click(sender As Object, e As EventArgs) Handles MmiViewByFacility.Click
        If TPReportAssignment.Focus = True Then
            OpenFacilityLookupTool()
        End If
    End Sub

#Region "By Test Report-Unassigned"
    Private Sub MmiUnassigned_Click(sender As Object, e As EventArgs) Handles MmiUnassigned.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackTwoRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackTwoRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackThreeRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackThreeRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackFourRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackFourRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiTwoStackStandard_Click(sender As Object, e As EventArgs) Handles MmiTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiTwoStackDRE_Click(sender As Object, e As EventArgs) Handles MmiTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiLoadingRack_Click(sender As Object, e As EventArgs) Handles MmiLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiFlare_Click(sender As Object, e As EventArgs) Handles MmiFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPondTreatment_Click(sender As Object, e As EventArgs) Handles MmiPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiGasConcentration_Click(sender As Object, e As EventArgs) Handles MmiGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiRata_Click(sender As Object, e As EventArgs) Handles MmiRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPEMS_Click(sender As Object, e As EventArgs) Handles MmiPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoStandard_Click(sender As Object, e As EventArgs) Handles MmiMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoToFile_Click(sender As Object, e As EventArgs) Handles MmiMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoPTE_Click(sender As Object, e As EventArgs) Handles MmiMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod9Single_Click(sender As Object, e As EventArgs) Handles MmiMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod9Multi_Click(sender As Object, e As EventArgs) Handles MmiMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod22_Click(sender As Object, e As EventArgs) Handles MmiMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "By Test Report-Assigned"
    Private Sub MmiAssignedNoDocument_Click(sender As Object, e As EventArgs) Handles MmiAssignedNoDocument.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackTwoRuns_Click(sender As Object, e As EventArgs) Handles MmiAssignedOneStackTwoRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackThreeRuns_Click(sender As Object, e As EventArgs) Handles MmiAssignedOneStackThreeRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackFourRuns_Click(sender As Object, e As EventArgs) Handles MmiAssignedOneStackFourRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTwoStackStandard_Click(sender As Object, e As EventArgs) Handles MmiAssignedTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTwoStackDRE_Click(sender As Object, e As EventArgs) Handles MmiAssignedTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedLoadingRack_Click(sender As Object, e As EventArgs) Handles MmiAssignedLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedFlare_Click(sender As Object, e As EventArgs) Handles MmiAssignedFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedPondTreatment_Click(sender As Object, e As EventArgs) Handles MmiAssignedPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedGasConcentration_Click(sender As Object, e As EventArgs) Handles MmiAssignedGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedRata_Click(sender As Object, e As EventArgs) Handles MmiAssignedRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedPEMS_Click(sender As Object, e As EventArgs) Handles MmiAssignedPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoStandard_Click(sender As Object, e As EventArgs) Handles MmiAssignedMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoToFile_Click(sender As Object, e As EventArgs) Handles MmiAssignedMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoPTE_Click(sender As Object, e As EventArgs) Handles MmiAssignedMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod9Single_Click(sender As Object, e As EventArgs) Handles MmiAssignedMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod9Multi_Click(sender As Object, e As EventArgs) Handles MmiAssignedMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod22_Click(sender As Object, e As EventArgs) Handles MmiAssignedMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "By Test Report-All"
    Private Sub MmiAllNoDoc_Click(sender As Object, e As EventArgs) Handles MmiAllNoDoc.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackTwoRuns_Click(sender As Object, e As EventArgs) Handles MmiAllOneStackTwoRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackThreeRuns_Click(sender As Object, e As EventArgs) Handles MmiAllOneStackThreeRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackFourRuns_Click(sender As Object, e As EventArgs) Handles MmiAllOneStackFourRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllTwoStackStandard_Click(sender As Object, e As EventArgs) Handles MmiAllTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllTwoStackDRE_Click(sender As Object, e As EventArgs) Handles MmiAllTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllLoadingRack_Click(sender As Object, e As EventArgs) Handles MmiAllLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllFlare_Click(sender As Object, e As EventArgs) Handles MmiAllFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllPondTreatment_Click(sender As Object, e As EventArgs) Handles MmiAllPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllGasConcentration_Click(sender As Object, e As EventArgs) Handles MmiAllGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllRata_Click(sender As Object, e As EventArgs) Handles MmiAllRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllPEMS_Click(sender As Object, e As EventArgs) Handles MmiAllPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoStandard_Click(sender As Object, e As EventArgs) Handles MmiAllMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoToFile_Click(sender As Object, e As EventArgs) Handles MmiAllMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoPTE_Click(sender As Object, e As EventArgs) Handles MmiAllMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod9Single_Click(sender As Object, e As EventArgs) Handles MmiAllMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod9Multi_Click(sender As Object, e As EventArgs) Handles MmiAllMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod22_Click(sender As Object, e As EventArgs) Handles MmiAllMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#End Region


#End Region
    Private Sub LVTestReportAssignment_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles LVTestReportAssignment.ItemCheck
        Try

            Dim count As Integer = txtTestReportCount.Text

            If LVTestReportAssignment.Items.Item(e.Index).Checked = True Then
                lblTestReportAssignment.Items.Remove(LVTestReportAssignment.Items.Item(e.Index).Text)
                count -= 1
            Else
                lblTestReportAssignment.Items.Add(LVTestReportAssignment.Items.Item(e.Index).Text)
                count += 1
            End If
            txtTestReportCount.Text = count
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbRunMonthlyReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbRunMonthlyReport.LinkClicked
        Try

            RunMonthlyReport()
            GetOutOfComplianceReport()
            RunSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbPrintMonthlyReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbPrintMonthlyReport.LinkClicked
        Try

            ExportToWord()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbEngineerTestReports_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEngineerTestReports.LinkClicked
        Try

            EngineerTestReport()
            EngineerOpenTestReports()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrEngineersFacilityList_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrEngineersFacilityList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrEngineersFacilityList.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrEngineersFacilityList(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrEngineersFacilityList(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrEngineersFacilityList(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrEngineersFacilityList(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrEngineersFacilityList(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrEngineersFacilityList(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrEngineersFacilityList(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrEngineersFacilityList(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrEngineersFacilityList(hti.Row, 8)) Then
                                                Else
                                                    txtReferenceNumber.Text = dgrEngineersFacilityList(hti.Row, 0)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewReport.LinkClicked
        Try
            Dim id As String = txtReferenceNumber.Text
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm(ISMPTestReports, id)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExportToExcel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExportToExcel.LinkClicked
        dsEngineerGrid.Tables(0).ExportToExcel(Me)
    End Sub
    Private Sub llbPrintSummaryReport_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbPrintSummaryReport.LinkClicked
        Try

            PrintSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunUnitStatsReport_Click(sender As Object, e As EventArgs) Handles btnRunUnitStatsReport.Click
        Try

            RunUnitStatistics2()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblTotalTests_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTotalTests.LinkClicked
        Try
            SQL = "select strReferenceNumber, " &
            "(strLastName|| ', ' ||strFirstName) as Engineer,  " &
            "case  " &
            "when datTestDateStart = '04-Jul-1776' then  null  " &
            "else format(datTestDateStart, 'dd-MMM-yyyy') " &
            "End datTestDateStart,  " &
            "case  " &
            "when datReceivedDate = '04-Jul-1776' then Null  " &
            "else format(datReceivedDate, 'dd-MMM-yyyy')  " &
            "End datReceiveddate,  " &
            "Case  " &
            "when datCompleteDate = '04-Jul-1776' then Null  " &
            "else format(datCompleteDate, 'dd-MMM-yyyy')  " &
            "end datCompleteDate  " &
            "from ISMPReportInformation, EPDUserProfiles    " &
            "where ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID  " &
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and strReviewingEngineer <> '0'  " &
            "and strClosed = 'True' "

            dsUnitStats = New DataSet
            daUnitStats = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daUnitStats.Fill(dsUnitStats, "UnitStats")
            dgvUnitStats.DataSource = dsUnitStats
            dgvUnitStats.DataMember = "UnitStats"

            dgvUnitStats.RowHeadersVisible = False
            dgvUnitStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUnitStats.AllowUserToResizeColumns = True
            dgvUnitStats.AllowUserToAddRows = False
            dgvUnitStats.AllowUserToDeleteRows = False
            dgvUnitStats.AllowUserToOrderColumns = True
            dgvUnitStats.AllowUserToResizeRows = True
            dgvUnitStats.ColumnHeadersHeight = "35"
            dgvUnitStats.Columns("strReferenceNumber").HeaderText = "Reference #"
            dgvUnitStats.Columns("strReferenceNumber").DisplayIndex = 0
            dgvUnitStats.Columns("Engineer").HeaderText = "Engineer"
            dgvUnitStats.Columns("Engineer").DisplayIndex = 1
            dgvUnitStats.Columns("datTestDateStart").HeaderText = "Test Date"
            dgvUnitStats.Columns("datTestDateStart").DisplayIndex = 2
            dgvUnitStats.Columns("datReceivedDate").HeaderText = "Received Date"
            dgvUnitStats.Columns("datReceivedDate").DisplayIndex = 3
            dgvUnitStats.Columns("datCompleteDate").HeaderText = "Complete Date"
            dgvUnitStats.Columns("datCompleteDate").DisplayIndex = 4

            txtUnitStatsCount.Text = dgvUnitStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvUnitStats_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvUnitStats.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvUnitStats.HitTest(e.X, e.Y)

        Try
            If dgvUnitStats.Columns(0).HeaderText = "Reference #" Then
                If dgvUnitStats.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtUnitStatReferenceNumber.Text = dgvUnitStats(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnViewTestReport_Click(sender As Object, e As EventArgs) Handles btnViewTestReport.Click
        Try
            Dim id As String = txtUnitStatReferenceNumber.Text
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm(ISMPTestReports, id)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvMethods_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvMethods.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvMethods.HitTest(e.X, e.Y)

        Try
            If dgvMethods.RowCount > 0 And hti.RowIndex <> -1 Then
                txtMethodCode.Text = dgvMethods(0, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub txtMethodCode_TextChanged(sender As Object, e As EventArgs) Handles txtMethodCode.TextChanged
        Try
            Dim temp As String = ""

            If txtMethodCode.Text <> "" Then
                SQL = "Select " &
                "strMethodDesc " &
                "from LookUpISMPMethods " &
                "where strMethodCode = '" & txtMethodCode.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    temp = dr.Item("strMethodDesc")
                Else
                    temp = ""
                End If
                dr.Close()

                If temp <> "" And temp.Contains(" - ") Then
                    txtMethodNumber.Text = Replace(Mid(temp, 1, (temp.IndexOf(" - "))), "Method ", "")
                    txtMethodDescription.Text = Mid(temp, (temp.IndexOf(" - ") + 4))
                Else
                    txtMethodNumber.Clear()
                    txtMethodDescription.Clear()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnUpdateMethods_Click(sender As Object, e As EventArgs) Handles btnUpdateMethods.Click
        Try
            Dim temp As String = ""

            If txtMethodNumber.Text <> "" And txtMethodDescription.Text <> "" Then
                txtMethodNumber.BackColor = Color.White
                txtMethodDescription.BackColor = Color.White
                temp = "Method " & txtMethodNumber.Text.ToUpper & " - " & txtMethodDescription.Text

                SQL = "Select " &
                "strMethodCode " &
                "From LookUpISMPMethods " &
                "where SUBSTRING(strMethodDesc, 1, instr(strMethodDesc,'-')-2)  = 'Method " & Replace(txtMethodNumber.Text.ToUpper, "'", "''") & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist = True Then
                    temp = dr.Item("strMethodCode")
                    dr.Close()
                    If temp = txtMethodCode.Text Then
                        SQL = "Update LookUpISMPMethods set " &
                        "strMethodDesc = 'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "' " &
                        "where strMethodCode = '" & Replace(txtMethodCode.Text, "'", "''") & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        SQL = "Select (max(strMethodCode) + 1) as MethodCode " &
                        "from LookUpISMPMethods "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("MethodCode")) Then
                                temp = "00000"
                            Else
                                temp = dr.Item("MethodCode")
                                Select Case temp.Length
                                    Case 0
                                        temp = "00000"
                                    Case 1
                                        temp = "0000" & temp
                                    Case 2
                                        temp = "000" & temp
                                    Case 3
                                        temp = "00" & temp
                                    Case 4
                                        temp = "0" & temp
                                    Case 5
                                        'temp = temp
                                    Case Else
                                        temp = Mid(temp, 1, 5)
                                End Select
                            End If
                        End While

                        SQL = "Insert into LookUpISMPMethods " &
                        "values " &
                        "('" & temp & "', " &
                        "'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "') "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                Else
                    dr.Close()
                    SQL = "Select (max(strMethodCode) + 1) as MethodCode " &
                    "from LookUpISMPMethods "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("MethodCode")) Then
                            temp = "00000"
                        Else
                            temp = dr.Item("MethodCode")
                            Select Case temp.Length
                                Case 0
                                    temp = "00000"
                                Case 1
                                    temp = "0000" & temp
                                Case 2
                                    temp = "000" & temp
                                Case 3
                                    temp = "00" & temp
                                Case 4
                                    temp = "0" & temp
                                Case 5
                                    'temp = temp
                                Case Else
                                    temp = Mid(temp, 1, 5)
                            End Select
                        End If
                    End While

                    SQL = "Insert into LookUpISMPMethods " &
                    "values " &
                    "('" & temp & "', " &
                    "'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "') "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadMethods()

            Else
                If txtMethodNumber.Text = "" Then
                    txtMethodNumber.BackColor = Color.Tomato
                End If
                If txtMethodDescription.Text = "" Then
                    txtMethodDescription.BackColor = Color.Tomato
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnAddTestReport_Click(sender As Object, e As EventArgs) Handles btnAddTestReport.Click
        Try
            Dim RefNum As String
            Dim AIRSNumber As String
            Dim Commissioner As String
            Dim Director As String
            Dim ProgramManager As String
            Dim DateReceived As String
            Dim DateCompleted As String

            If txtAddTestReportRefNum.Text <> "" Then
                txtAddTestReportRefNum.BackColor = Color.White
                RefNum = txtAddTestReportRefNum.Text
                If mtbAddTestReportAIRSNumber.Text <> "" And Len(mtbAddTestReportAIRSNumber.Text) = 8 Then
                    AIRSNumber = mtbAddTestReportAIRSNumber.Text
                    mtbAddTestReportAIRSNumber.BackColor = Color.White
                Else
                    mtbAddTestReportAIRSNumber.BackColor = Color.Tomato
                    MsgBox("Please add a valid AIRS Number.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportCommissioner.Text <> "" Then
                    Commissioner = txtAddTestReportCommissioner.Text
                    txtAddTestReportCommissioner.BackColor = Color.White
                Else
                    txtAddTestReportCommissioner.BackColor = Color.Tomato
                    MsgBox("Please add a valid Commissioenr.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportDirector.Text <> "" Then
                    Director = txtAddTestReportDirector.Text
                    txtAddTestReportDirector.BackColor = Color.White
                Else
                    txtAddTestReportDirector.BackColor = Color.Tomato
                    MsgBox("Please add a valid Director.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportProgramManager.Text <> "" Then
                    ProgramManager = txtAddTestReportProgramManager.Text
                    txtAddTestReportProgramManager.BackColor = Color.White
                Else
                    txtAddTestReportProgramManager.BackColor = Color.Tomato
                    MsgBox("Please add a valid Program Manager.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                DateReceived = dtpAddTestReportDateReceived.Text
                DateCompleted = DTPAddTestReportDateCompleted.Text

                SQL = "Select " &
                "strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = '" & RefNum & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("This Refernece Number already exists in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                SQL = "Select " &
                "strAIRSNumber " &
                "from APBMasterAIRS " &
                "where strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("This AIRS Number does not exist in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                SQL = "Insert into ISMPMaster " &
                "values " &
                "('" & RefNum & "', '0413" & AIRSNumber & "', " &
                "'" & CurrentUser.UserID & "', GETDATE() ) "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Insert into ISMPReportInformation " &
                "values " &
                "('" & RefNum & "', '00001', " &
                "'N/A', '001', " &
                "'001', 'N/A', " &
                "'00001', '0', " &
                "'0', '0', " &
                "'0', '" & DateReceived & "', " &
                "'0', '04-Jul-1776', " &
                "'04-Jul-1776', '" & DateReceived & "', " &
                "'" & DateCompleted & "', 'N/A', " &
                "'False', '" & Replace(Commissioner, "'", "''") & "', " &
                "'" & Replace(Director, "'", "''") & "', '" & Replace(ProgramManager, "'", "''") & "', " &
                "'01', '0', " &
                "'" & CurrentUser.UserID & "', GETDATE(), " &
                "'N/A', '', " &
                "'', '', " &
                "'', '') "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                MsgBox("Record Added.", MsgBoxStyle.Information, "Add Test Report")

            Else
                txtAddTestReportRefNum.BackColor = Color.Tomato
                MsgBox("Please add a valid Reference Number.", MsgBoxStyle.Information, "Add Test Report")
                Exit Sub
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddTestReport_Click(sender As Object, e As EventArgs) Handles btnClearAddTestReport.Click
        Try
            txtAddTestReportRefNum.Clear()
            txtAddTestReportRefNum.BackColor = Color.White
            txtAddTestReportCommissioner.Clear()
            txtAddTestReportCommissioner.BackColor = Color.White
            txtAddTestReportDirector.Clear()
            txtAddTestReportDirector.BackColor = Color.White
            txtAddTestReportProgramManager.Clear()
            txtAddTestReportProgramManager.BackColor = Color.White
            mtbAddTestReportAIRSNumber.Clear()
            mtbAddTestReportAIRSNumber.BackColor = Color.White
            dtpAddTestReportDateReceived.Value = Today
            dtpAddTestReportDateReceived.BackColor = Color.White
            DTPAddTestReportDateCompleted.Value = Today
            DTPAddTestReportDateCompleted.BackColor = Color.White


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseHistoricTestReport_Click(sender As Object, e As EventArgs) Handles btnCloseHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                SQL = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update ISMPReportInformation set " &
                    "strClosed = 'True' " &
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("Test Report Closed", MsgBoxStyle.Information, "Historical Test Report")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReOpenHistoricTestReport_Click(sender As Object, e As EventArgs) Handles btnReOpenHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                SQL = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update ISMPReportInformation set " &
                    "strClosed = 'False' " &
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("Test Report Re-Opened", MsgBoxStyle.Information, "Historical Test Report")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class
