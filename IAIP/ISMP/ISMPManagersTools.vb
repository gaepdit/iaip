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


    Private Sub ISMPManagersTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            CreateStatusBar()
            ShowCorrectTabs()
            LoadComboBoxDataSets()
            LoadComboBoxes()
            LoadTestReportAssignmentDataSet()
            LoadLVTestReportAssignment()
            FormatEngineerTestReportGrid()
            FormatTestSummaryGrid()
            FormatExcelDataGrid()
            LoadExcelDataSet()

            DTPUnitStatsStartDate.Value = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPUnitStatsEndDate.Value = OracleDate
            DTPMonthlyStart.Value = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPMonthlyEnd.Value = OracleDate
            DTPUnitStart.Value = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPUnitEnd.Value = OracleDate
            DTPEngineerTestReportStart.Text = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPEngineerTestReportEnd.Text = OracleDate
            DTPAppStartDate.Text = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPAppEndDate.Text = OracleDate
            DTPStartDateFacility.Value = OracleDate
            DTPEndDateFacility.Value = OracleDate

            LoadMethods()
            dtpAddTestReportDateReceived.Text = OracleDate
            DTPAddTestReportDateCompleted.Text = OracleDate
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = CurrentUser.AlphaName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ShowCorrectTabs()
        Try

            TCManagersTools.TabPages.Remove(TPAIRSReportsPrinted)
            TCManagersTools.TabPages.Remove(TPMonthlyReport)
            TCManagersTools.TabPages.Remove(TPReportAssignment)
            TCManagersTools.TabPages.Remove(TPTestReportStatistics)
            TCManagersTools.TabPages.Remove(TPUnitStatistics)
            TCManagersTools.TabPages.Remove(TPUnitAssignment)
            TCManagersTools.TabPages.Remove(TPUnitStatistics2)
            TCManagersTools.TabPages.Remove(TPExcelFiles)
            TCManagersTools.TabPages.Remove(TPMiscTools)
            TCManagersTools.TabPages.Remove(TPApplicationsReviewed)

            TCMiscTools.TabPages.Remove(TPMethods)

            'Program Manager 
            If AccountFormAccess(17, 3) = "1" Then
                TCManagersTools.TabPages.Add(TPReportAssignment)
                TCManagersTools.TabPages.Add(TPMonthlyReport)
                TCManagersTools.TabPages.Add(TPUnitStatistics2)
                TCManagersTools.TabPages.Add(TPUnitStatistics)
                TCManagersTools.TabPages.Add(TPAIRSReportsPrinted)
                'TCManagersTools.TabPages.Add(Me.TPExcelFiles)
                TCManagersTools.TabPages.Add(TPMiscTools)
                TCMiscTools.TabPages.Add(TPMethods)

                ShowCorrectPanels()
                LoadAFSPrintList()
            Else
                'Unit Manager 
                If AccountFormAccess(17, 2) = "1" Then
                    TCManagersTools.TabPages.Add(TPUnitAssignment)
                    TCManagersTools.TabPages.Add(TPReportAssignment)
                    TCManagersTools.TabPages.Add(TPUnitStatistics2)
                    TCManagersTools.TabPages.Add(TPUnitStatistics)
                    'TCManagersTools.TabPages.Add(Me.TPExcelFiles)
                    TCManagersTools.TabPages.Add(TPMiscTools)
                    TCMiscTools.TabPages.Add(TPMethods)
                    ShowCorrectPanels()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ShowCorrectPanels()
        Try

            Select Case CurrentUser.UnitId
                Case 0
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = False
                    PanelChemVOC.Visible = True
                    txtDaysOpen2.ReadOnly = True
                    PanelAll.Visible = True

                Case 12
                    PanelCombustMineral.Visible = False
                    txtDaysOpen.ReadOnly = False
                    txtDaysOpen2.ReadOnly = True
                    PanelChemVOC.Visible = True
                    PanelAll.Visible = True
                Case 13
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = True
                    txtDaysOpen2.ReadOnly = False
                    PanelChemVOC.Visible = False
                    PanelAll.Visible = True
                Case 14
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = False
                    txtDaysOpen2.ReadOnly = True
                    PanelChemVOC.Visible = True
                    PanelAll.Visible = True
                Case Else
                    PanelAll.Visible = True
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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

            SQL2 = "select strCountyCode, strCountyName from LookUpCountyInformation " &
            "order by strCountyName"

            SQL3 = "select distinct(strFacilityCity) as City from APBFacilityInformation " &
            "order by strFacilityCity"

            dsEngineer = New DataSet
            dsCounty = New DataSet
            dsCity = New DataSet

            daEngineer = New SqlDataAdapter(SQL, CurrentConnection)
            daCounty = New SqlDataAdapter(SQL2, CurrentConnection)
            daCity = New SqlDataAdapter(SQL3, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEngineer.Fill(dsEngineer, "Engineers")
            daCounty.Fill(dsCounty, "County")
            daCity.Fill(dsCity, "City")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadComboBoxes()
        Dim dtEngineers2 As DataTable
        Dim dtCounty As New DataTable
        Dim dtCity As New DataTable

        Try


            dtEngineers2 = dsEngineer.Tables("Engineers")
            dtCounty = dsCounty.Tables("County")
            dtCity = dsCity.Tables("City")

            Dim drEngineers As DataRow()
            Dim drCounty As DataRow()
            Dim drCity As DataRow()

            Dim row As DataRow

            cboEngineer.Items.Add(" ")

            drEngineers = dtEngineers2.Select()
            For Each row In drEngineers
                cboEngineer.Items.Add(row("UserName"))
                MmiByEngineer.MenuItems.Add(row("UserName"))
            Next

            If CurrentUser.UnitId = 0 Then
                drEngineers = dtEngineers2.Select()
            Else
                drEngineers = dtEngineers2.Select("numUnit is Null")
            End If

            Select Case CurrentUser.UnitId
                Case 12
                    drEngineers = dtEngineers2.Select("numUnit = '12'")
                    For Each row In drEngineers
                        clbEngineers1.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                Case 13
                    drEngineers = dtEngineers2.Select("numUnit = '13'")
                    For Each row In drEngineers
                        clbEngineers2.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                Case 14
                    drEngineers = dtEngineers2.Select("numUnit = '12'")
                    For Each row In drEngineers
                        clbEngineers1.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                    drEngineers = dtEngineers2.Select("numUnit = '13'")
                    For Each row In drEngineers
                        clbEngineers2.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                Case Else
                    drEngineers = dtEngineers2.Select("numUnit = '12'")
                    For Each row In drEngineers
                        clbEngineers1.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                    drEngineers = dtEngineers2.Select("numUnit = '13'")
                    For Each row In drEngineers
                        clbEngineers2.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
            End Select

            cboCounty.Items.Add(" ")

            drCounty = dtCounty.Select()
            For Each row In drCounty
                cboCounty.Items.Add(row("strCountyName"))
            Next

            cboCity.Items.Add(" ")
            drCity = dtCity.Select()
            For Each row In drCity
                cboCity.Items.Add(row("City"))
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,   " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                      "and ( strclosed = 'False' or strClosed is null ) " &
                    "and ISMPReportInformation.strReviewingEngineer = '0' " &
                    "and ISMPReportInformation.strDelete is NULL"

                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadExcelDataSet()
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            SQL = "Select FileID, FileTitle " &
            "From ISMPTestReportAids"

            dsExcelFiles = New DataSet

            cmd = New SqlCommand(SQL, CurrentConnection)

            daExcelFiles = New SqlDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daExcelFiles.Fill(dsExcelFiles, "ExcelFiles")
            dgrExcelFiles.DataSource = dsExcelFiles
            dgrExcelFiles.DataMember = "ExcelFiles"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatExcelDataGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ExcelFiles"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "FileID"
            objtextcol.HeaderText = "ID Number"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "FileTitle"
            objtextcol.HeaderText = "Name of File"
            objtextcol.Width = 300
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrExcelFiles.TableStyles.Clear()
            dgrExcelFiles.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrExcelFiles.CaptionText = "Excel Files Currently Saved"
            dgrExcelFiles.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Different Test Report Assignment Data Sets"
    Sub LoadAllByUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                  "and ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                      "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadAllNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadUnassignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadAssignedByUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                  "and ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                     "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                       "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadAssignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadDeletedTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                    "and ISMPReportInformation.strDelete is not NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadByTestReportTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                    "and ISMPReportInformation.strReviewingEngineer = '0' " &
                    "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                    "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadByTestReportAllTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                    "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                    "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                    "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
    Sub LoadByTestReportAssignedTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                    "and ISMPReportInformation.strDocumentType = '" & ReportType & "' " &
                    "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadByAIRSNumberTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                SQL = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " &
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
                    "and strReviewingUnit = '" & CurrentUser.UserID & "' " &
                      "and ISMPMaster.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " &
                    "and ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
    Private Sub LVTestReportAssignment_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles LVTestReportAssignment.ColumnClick
        Try

            LVTestReportAssignment.ListViewItemSorter = New ListViewItemComparer(e.Column)
            LVTestReportAssignment.Sort()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub ISMPManagersTools_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                    SQL = "select to_char(datReviewedBYUnitManager, 'dd-Mon-yyyy') as ReviewedByUnitManager " &
                          "from ISMPReportInformation " &
                          "where strReferenceNumber = '" & strObject.ToString() & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AssignDate = dr.Item("ReviewedByUnitManager")
                    End While
                    If AssignDate = "04-Jul-1776" Then
                        AssignDate = OracleDate
                    Else
                        'AssignDate = AssignDate
                    End If

                    Dim tempUnit As String = CurrentUser.UnitId.ToString

                    SQL = "Update ISMPReportInformation set " &
                    "strReviewingEngineer = '" & EngineerGCode & "', " &
                    "datReviewedBYUnitManager = '" & AssignDate & "', " &
                    "strReviewingUnit = '" & tempUnit & "', " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveUnitAssignments()
        Dim strObject As String

        Try

            If rdbChemVOC.Checked <> False Or rdbCombusMineral.Checked <> False Then

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                If rdbChemVOC.Checked = True Then
                    SQL = "Update ISMPFacilityAssignment set " &
                    "strISMPUnit = 'H' "
                Else
                    SQL = "Update ISMPFacilityAssignment set " &
                    "strChemicalVOC = 'I' "
                End If
                Try


                    For Each strObject In lsbFacilities.Items
                        SQL2 = SQL & "where strAIRSNumber = '0413" & strObject.ToCharArray() & "' "
                        cmd = New SqlCommand(SQL2, CurrentConnection)
                        dr = cmd.ExecuteReader
                        SQL2 = ""
                    Next

                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try
                '  

                LVFacilities.Clear()
                FillFacilitiesDataGrid()
                lsbFacilities.Items.Clear()
            Else
                MsgBox("Select a unit to assign these facilities to first.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Run Statistics"
    Sub RunFacilityStatistics()
        Dim DateBias As String = ""
        Dim FacilityBias As String = "%"
        Dim CityBias As String = "%"
        Dim CountyBias As String = "%%%"
        Dim dtTable As New DataTable
        Dim drRow As DataRow()
        Dim row As DataRow

        Dim FacilityOpen As Integer = "0"
        Dim CSFacilityOpen1 As Integer = "0"
        Dim CSFacilityOpen2 As Integer = "0"
        Dim CSFacilityOpen3 As Integer = "0"
        Dim CSFacilityOpen4 As Integer = "0"
        Dim CSFacilityOpen5 As Integer = "0"

        Dim FacilityClosed As Integer = "0"
        Dim CSFacilityClosed1 As Integer = "0"
        Dim CSFacilityClosed2 As Integer = "0"
        Dim CSFacilityClosed3 As Integer = "0"
        Dim CSFacilityClosed4 As Integer = "0"
        Dim CSFacilityClosed5 As Integer = "0"

        Dim FacilityOpenDays As Integer = "0"
        Dim CSFacilityOpenDays1 As Integer = "0"
        Dim CSFacilityOpenDays2 As Integer = "0"
        Dim CSFacilityOpenDays3 As Integer = "0"
        Dim CSFacilityOpenDays4 As Integer = "0"
        Dim CSFacilityOpenDays5 As Integer = "0"

        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            If txtFacility.Text <> "" Then
                FacilityBias = txtAIRSNumber2.Text
            End If
            If cboCounty.Text <> "" And cboCounty.Text <> " " Then
                dtTable = dsCounty.Tables("County")
                drRow = dtTable.Select("strCountyName = '" & cboCounty.Text & "'")
                For Each row In drRow
                    CountyBias = row("strCountyCode")
                Next
            Else
                CountyBias = "%%%"
            End If
            If cboCity.Text <> "" And cboCity.Text <> " " Then
                dtTable = dsCity.Tables("City")
                drRow = dtTable.Select("City = '" & cboCity.Text & "'")
                For Each row In drRow
                    CityBias = row("City")
                Next
            Else
                CityBias = "%"
            End If
            If rdbFacilityDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPStartDateFacility.Text & "' " &
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbFacilityDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPStartDateFacility.Text & "' " &
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbFacilityDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPStartDateFacility.Text & "' " &
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If

            'txtOpenFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)
            Try



                dr = cmd.ExecuteReader
                While dr.Read
                    FacilityOpen = dr.Item("Count")
                End While
            Catch ex As Exception
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            Finally

            End Try
            ' 

            'txtClosedFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityClosed = dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where " &
            "strDelete is NULL " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays = dr.Item("Count")
            End While

            'Compliance Status Open
            'File Open 
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '01' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '02' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '03' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '04' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '05' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen5 = dr.Item("Count")
            End While

            'Compliance Status Closed
            'File Open 
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '01' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '02' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '03' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '04' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strComplianceStatus = '05' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed5 = dr.Item("Count")
            End While

            'Compliance Status for Days Open
            'File Open 
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where " &
            "strDelete is NULL " &
            "and strComplianceStatus = '01' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where " &
            "strDelete is NULL " &
            "and strComplianceStatus = '02' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where " &
            "strDelete is NULL " &
            "and strComplianceStatus = '03' " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
            "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
            "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " &
           "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
           "where " &
           "strDelete is NULL " &
           "and strComplianceStatus = '04' " &
           "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
           "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
           "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
           "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
           "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
           "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
           "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " &
           "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
           "where " &
           "strDelete is NULL " &
           "and strComplianceStatus = '05' " &
           "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
           "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
           "and Upper(APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " &
           "and ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " &
           "and subStr(ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " &
           "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " &
           "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays5 = dr.Item("Count")
            End While

            txtOpenFacility.Text = FacilityOpen
            txtClosedFacility.Text = FacilityClosed
            txtCSFileOpenOpen.Text = CSFacilityOpen1
            txtCSInfoOnlyOpen.Text = CSFacilityOpen2
            txtCSInComplianceOpen.Text = CSFacilityOpen3
            txtIndeterminateOpen.Text = CSFacilityOpen4
            txtCSNotInComplianceOpen.Text = CSFacilityOpen5
            txtCSFileOpenClosed.Text = CSFacilityClosed1
            txtCSInfoOnlyClosed.Text = CSFacilityClosed2
            txtCSInComplianceClosed.Text = CSFacilityClosed3
            txtIndeterminateClosed.Text = CSFacilityClosed4
            txtCSNotInComplianceClosed.Text = CSFacilityClosed5
            txtFacilityOpenDays.Text = FacilityOpenDays
            txtCSFileOpenOpenDays.Text = CSFacilityOpenDays1
            txtCSInfoOnlyOpenDays.Text = CSFacilityOpenDays2
            txtCSInComplianceOpenDays.Text = CSFacilityOpenDays3
            txtIndeterminateOpenDays.Text = CSFacilityOpenDays4
            txtCSNotInComplianceOpenDays.Text = CSFacilityOpenDays5

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub RunUnitStatistics()
        Dim DateBias As String = ""
        Dim strObject As Object
        Dim dtEngineers As New DataTable
        dtEngineers = dsEngineer.Tables("Engineers")
        Dim drEngineers As DataRow()
        Dim row As DataRow
        Dim EngineerGCode As String = "0"

        Dim FacilityOpen As Integer = "0"
        Dim FacilityOpen2 As Integer = "0"
        Dim FacilityOpen3 As Integer = "0"
        Dim FacilityOpen4 As Integer = "0"
        Dim FacilityOpenDays As Integer = "0"
        Dim FacilityOpenDays2 As Integer = "0"
        Dim FacilityOpenDays3 As Integer = "0"
        Dim FacilityOpenDays4 As Integer = "0"
        Dim FacilityWitnessed As Integer = "0"
        Dim FacilityWitnessed2 As Integer = "0"
        Dim FacilityWitnessed3 As Integer = "0"
        Dim FacilityWitnessed4 As Integer = "0"
        Dim FacilityClosed As Integer = "0"
        Dim FacilityClosed2 As Integer = "0"
        Dim FacilityClosed3 As Integer = "0"
        Dim FacilityClosed4 As Integer = "0"

        Try


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            If rdbUnitDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
            End If

            If PanelChemVOC.Visible = True Then
                For Each strObject In clbEngineers1.CheckedItems
                    drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                    For Each row In drEngineers
                        EngineerGCode = row("numUserID")
                    Next

                    'txtOpenFacility
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where strClosed = 'False' " &
                    "and strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpen += dr.Item("Count")
                    End While

                    'txtFacilityOpenDays
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where " &
                    "strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen.Text & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpenDays += dr.Item("Count")
                    End While

                    'txtWitnessedTests
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where " &
                    "strDelete is NULL " &
                    "and (strWitnessingEngineer = '" & EngineerGCode & "' " &
                    "or strWitnessingEngineer2 = '" & EngineerGCode & "') " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityWitnessed += dr.Item("count")
                    End While

                    'txtClosedFacility
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where strClosed = 'True' " &
                    "and strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityClosed += dr.Item("Count")
                    End While
                Next
            End If

            If PanelCombustMineral.Visible = True Then
                For Each strObject In clbEngineers2.CheckedItems
                    drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                    For Each row In drEngineers
                        EngineerGCode = row("numUserID")
                    Next

                    'txtOpenFacility
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where strClosed = 'False' " &
                    "and strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpen2 += dr.Item("Count")
                    End While

                    'txtFacilityOpenDays
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where " &
                    "strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen2.Text & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpenDays2 += dr.Item("Count")
                    End While

                    'txtWitnessedTests
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where " &
                    "strDelete is NULL " &
                    "and (strWitnessingEngineer = '" & EngineerGCode & "' " &
                    "or strWitnessingEngineer2 = '" & EngineerGCode & "') " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityWitnessed2 += dr.Item("count")
                    End While

                    'txtClosedFacility
                    SQL = "Select count(*) as Count " &
                    "from ISMPReportInformation " &
                    "where strClosed = 'True' " &
                    "and strDelete is NULL " &
                    "and strReviewingEngineer = '" & EngineerGCode & "' " &
                    "and " & DateBias & " "

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityClosed2 += dr.Item("Count")
                    End While
                Next
            End If

            'txtOpenFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strReviewingEngineer = '0' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpen3 += dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where " &
            "strDelete is NULL " &
            "and strReviewingEngineer = '0' " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen3.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays3 += dr.Item("Count")
            End While

            'txtWitnessedTests
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where " &
            "strDelete is NULL " &
            "and (strWitnessingEngineer <> '0' " &
            "or strWitnessingEngineer2 <> '0') " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityWitnessed3 += dr.Item("count")
            End While

            'txtClosedFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strReviewingEngineer = '0' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityClosed3 += dr.Item("Count")
            End While

            'txtOpenFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where strClosed = 'False' " &
            "and strDelete is NULL " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpen4 += dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where " &
            "strDelete is NULL " &
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen4.Text & "' " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays4 += dr.Item("Count")
            End While

            'txtWitnessedTests
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where " &
            "strDelete is NULL " &
            "and (strWitnessingEngineer <> '0' " &
            "or strWitnessingEngineer2 <> '0') " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityWitnessed4 += dr.Item("count")
            End While

            'txtClosedFacility
            SQL = "Select count(*) as Count " &
            "from ISMPReportInformation " &
            "where strClosed = 'True' " &
            "and strDelete is NULL " &
            "and " & DateBias & " "

            cmd = New SqlCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityClosed4 += dr.Item("Count")
            End While



            txtOpenFiles1.Text = FacilityOpen
            txtFilesOpen1.Text = FacilityOpenDays
            txtWitnessed1.Text = FacilityWitnessed
            txtClosed1.Text = FacilityClosed

            txtOpenFiles2.Text = FacilityOpen2
            txtFilesOpen2.Text = FacilityOpenDays2
            txtWitnessed2.Text = FacilityWitnessed2
            txtClosed2.Text = FacilityClosed2

            txtOpenFiles3.Text = FacilityOpen3
            txtFilesOpen3.Text = FacilityOpenDays3
            txtWitnessed3.Text = FacilityWitnessed3
            txtClosed3.Text = FacilityClosed3

            txtOpenFilesTotal.Text = FacilityOpen4
            txtFilesOpenTotal.Text = FacilityOpenDays4
            txtWitnessedTotal.Text = FacilityWitnessed4
            txtClosedTotal.Text = FacilityClosed4

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RunUnitEngineerStatistics(ByVal EngineerGCode As String)
        Dim DateBias As String = ""

        Dim Staff As String = ""
        Dim DateStatement As String = ""
        Dim ReceivedByDate As String = "X"
        Dim OpenByDate As String = "X"
        Dim ClosedByDate As String = "X"
        Dim WitnessedByDate As String = "X"
        Dim OpenWitnessedByDate As String = "X"
        Dim CloseWitnessedByDate As String = "X"
        Dim GreaterByDate As String = "X"
        Dim OpenGreaterByDate As String = "X"
        Dim CloseGreaterByDate As String = "X"
        Dim ComplianceByDate As String = "X"
        Dim OpenComplianceByDate As String = "X"
        Dim CloseComplianceByDate As String = "X"
        Dim OpenMedianByDate As String = "X"
        Dim CloseMedianByDate As String = "X"
        Dim OpenPercentileByDate As String = "X"
        Dim ClosePercentileByDate As String = "X"
        Dim OtherWitnessed As String = "X"

        Dim ReceivedTotal As String = "X"
        Dim OpenTotal As String = "X"
        Dim OpenWitnessedTotal As String = "X"
        Dim OpenComplianceTotal As String = "X"
        Dim OpenGreaterTotal As String = "X"
        Dim OpenMedianTotal As String = "X"
        Dim PercentileOpenTotalDay As String = "X"
        Dim ClosedTotal As String = "X"
        Dim ClosedWitnessedTotal As String = "X"
        Dim ClosedComplianceTotal As String = "X"
        Dim ClosedGreaterTotal As String = "X"
        Dim ClosedMedianTotal As String = "X"
        Dim PercentileClosedTotalDay As String = "X"
        Dim Statement As String = ""

        Dim i As Integer = 0
        Dim MedianArrayByDateOpen(i) As Decimal
        Dim j As Integer = 0
        Dim MedianArrayByDateClose(j) As Decimal
        Dim n As Integer = 0
        Dim MedianArrayOpen(n) As Decimal
        Dim o As Integer = 0
        Dim MedianArrayClosed(o) As Decimal

        Try
            If rdbUnitDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Completed between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were: "
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were:"
            End If

            If EngineerGCode = "" Then

            Else

                SQL = "select " &
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " &
                "case " &
                "	when ReceivedByDate is NULL then 0  " &
                "	Else ReceivedByDate " &
                "End as ReceivedByDate,  " &
                "Case  " &
                "	when OpenByDate is Null then 0  " &
                "	Else OpenByDate  " &
                "End as OpenByDate,  " &
                "Case  " &
                "	WHEN CloseByDate is Null then 0  " &
                "	Else CloseByDate " &
                "End as CloseByDate,  " &
                "Case  " &
                "	when WitnessedByDate is Null then 0  " &
                "	Else WitnessedByDate  " &
                "End as WitnessedByDate, " &
                "case  " &
                "	when OpenWitnessedByDate is NULL then 0  " &
                "	Else OpenWitnessedByDate  " &
                "End as OpenWitnessedByDate,  " &
                "case  " &
                "	when CloseWitnessedByDate is NULL then 0  " &
                "	Else CloseWitnessedByDate  " &
                "End as CloseWitnessedByDate,  " &
                "Case " &
                "   when GreaterByDate is NUll then 0 " &
                "   Else GreaterByDate " &
                "End as GreaterByDate, " &
                "case  " &
                "	when OpenGreaterByDate is NULL then 0  " &
                "	Else OpenGreaterByDate " &
                "end as OpenGreaterByDate,    " &
                "case  " &
                "	When CloseGreaterByDate is NULL then 0  " &
                "	Else CloseGreaterByDate  " &
                "End as CloseGreaterByDate,  " &
                "Case " &
                "   when ComplianceByDate is NULL then 0 " &
                "   Else ComplianceByDate " &
                "End as ComplianceByDate, " &
                "Case  " &
                "	when OpenComplianceByDate is NULL then 0  " &
                "	Else OpenComplianceByDate " &
                "End as OpenComplianceByDate,  " &
                "Case  " &
                "	When CloseComplianceByDate is NULL then 0  " &
                "	Else CloseComplianceByDate " &
                "End as CloseComplianceByDate,  " &
                "OtherWitnessed " &
                "from EPDUserProfiles, ISMPReportInformation,  " &
                "(Select strReviewingEngineer,  count(*) as ReceivedByDate   " &
                "from ISMPReportInformation   " &
                "where strDelete is NULL " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) ReceivedByDates,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as OpenByDate  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) OpenByDates,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as CloseByDate  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True'  " &
                "and StrDelete is NULL  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) CloseByDates,  " &
                "(Select strWitnessingEngineer,  " &
                "count(*) as WitnessedByDate  " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strWitnessingEngineer) WitnessedByDates,  " &
                "(Select strWitnessingEngineer,  " &
                "count(*) as OpenWitnessedByDate   " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                 "and " & DateBias & " " &
                "group by strWitnessingEngineer) OpenWitnessedByDates,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as CloseWitnessedByDate   " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True' " &
                "and " & DateBias & " " &
                "group by strwitnessingEngineer) CloseWitnessedByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as GreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and datReceivedDate < Decode(strClosed, 'False', (trunc(sysdate) - 50), " &
                "                                        'True', (-50 + datCompleteDate)) " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) GreaterByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as OpenGreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                "and datReceivedDate < (trunc(sysdate) - 50)  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) OpenGreaterByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as CloseGreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True'  " &
                "and datReceivedDate < (-50 + datCompleteDate) " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) CloseGreaterByDates,  " &
                "(select strReviewingEngineer, " &
                "count(*) as ComplianceByDate " &
                "from ISMPReportInformation " &
                "where strComplianceStatus = '05' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) ComplianceByDates, " &
                "(select strReviewingEngineer,   " &
                "count(*) as OpenComplianceByDate  " &
                "from ISMPReportInformation   " &
                "where strComplianceStatus = '05'  " &
                "and strClosed = 'False'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) OpenComplianceByDates,   " &
                "(Select strReviewingEngineer,  " &
                "count(*) as CloseComplianceByDate  " &
                "from ISMPReportInformation   " &
                "where strComplianceStatus = '05'  " &
                "and strClosed = 'True'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) CloseComplianceByDates,   " &
                "(select  count(*) as OtherWitnessed " &
                "from ISMPWitnessingEng,  ISMPReportInformation " &
                "where ISMPWitnessingEng.strreferencenumber  = ISMPReportInformation.strreferencenumber  " &
                "and strDelete is null  " &
                "and " & DateBias & "  " &
                "and ISMPWitnessingEng.strWitnessingEngineer = '" & EngineerGCode & "')  OtherWitnesses  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL2 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(trunc(sysdate) - datReceivedDate) as DaysOpenByDate " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysOpenByDate ASC "

                SQL3 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(datCompleteDate - datReceivedDate) as DaysCloseByDate " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysCloseByDate ASC "

                SQL4 = "Select " &
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " &
                "case  " &
                "	when ReceivedTotal is NULL then 0  " &
                "	Else ReceivedTotal  " &
                "end as ReceivedTotal,  " &
                "case  " &
                "	when OpenTotal is NULL then 0  " &
                "	Else OpenTotal  " &
                "End as OpenTotal,  " &
                "Case  " &
                "	when OpenWitnessedTotal is NULL then 0  " &
                "	Else OpenWitnessedTotal  " &
                "End as OpenWitnessedTotal,  " &
                "Case  " &
                "	When OpenComplianceTotal is NULL then 0  " &
                "	Else OpenComplianceTotal  " &
                "End as OpenComplianceTotal,  " &
                "Case  " &
                "	when CloseTotal is NULL then 0  " &
                "	else CloseTotal  " &
                "End as CloseTotal,  " &
                "Case  " &
                "	when ClosedWitnessedTotal is NULL then 0  " &
                "	Else ClosedWitnessedTotal  " &
                "End as ClosedWitnessedTotal,  " &
                "Case  " &
                "	when ClosedComplianceTotal is NULL then 0  " &
                "	Else ClosedComplianceTotal " &
                "End as ClosedComplianceTotal,  " &
                "Case  " &
                "when OpenGreaterTotal is NULL then 0   " &
                "Else OpenGreaterTotal   " &
                "End as OpenGreaterTotal, " &
                "Case  " &
                "when ClosedGreaterTotal is NULL then 0   " &
                "Else ClosedGreaterTotal   " &
                "End as ClosedGreaterTotal   " &
                "from EPDUserProfiles, ISMPReportInformation, " &
                "(Select strReviewingEngineer,  " &
                "count(*) as ReceivedTotal  " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "Group by strReviewingEngineer) ReceivedTotals,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as OpenTotal " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False' " &
                "and strDelete is NULL  " &
                "Group by strReviewingEngineer) OpenTotals,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as OpenWitnessedTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False' " &
                "and strDelete is Null " &
                "group by strWitnessingEngineer) OpenWitnessedTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as OpenComplianceTotal  " &
                "from ISMPReportInformation  " &
                "where strComplianceStatus = '05' " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "group by strReviewingEngineer) OpenComplianceTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as CloseTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True'  " &
                "and strDelete is NULL " &
                "Group by strReviewingEngineer) CloseTotals,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as ClosedWitnessedTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True' " &
                "and strDelete is NULL  " &
                "group by strWitnessingEngineer) ClosedWitnessedTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as ClosedComplianceTotal  " &
                "from ISMPReportInformation  " &
                "where strComplianceStatus = '05' " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "group by strReviewingEngineer) ClosedComplianceTotals, " &
                "(select strReviewingEngineer, count(*) as OpenGreaterTotal " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                "and datReceivedDate < (trunc(sysdate) - 50)  " &
                "Group by strReviewingEngineer) OpenGreaterTotals, " &
                "(select strReviewingEngineer, count(*) as ClosedGreaterTotal " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True'  " &
                "and datReceivedDate < (-50 + datCompleteDate)  " &
                "Group by strReviewingEngineer) ClosedGreaterTotals " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer (+)   " &
                "and ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL5 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(trunc(sysdate) - datReceivedDate) as DaysOpen " &
                "from EPDUSerProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysOpen ASC "

                SQL6 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(datCompleteDate -datReceivedDate) as DaysClosed " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysClosed ASC "

                cmd = New SqlCommand(SQL, CurrentConnection)
                cmd2 = New SqlCommand(SQL2, CurrentConnection)
                cmd3 = New SqlCommand(SQL3, CurrentConnection)
                cmd4 = New SqlCommand(SQL4, CurrentConnection)
                cmd5 = New SqlCommand(SQL5, CurrentConnection)
                cmd6 = New SqlCommand(SQL6, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                Try

                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("Staff")) Then
                            Staff = "X"
                        Else
                            Staff = dr.Item("Staff")
                        End If
                        If IsDBNull(dr.Item("ReceivedByDate")) Then
                            ReceivedByDate = "X"
                        Else
                            ReceivedByDate = dr.Item("ReceivedByDate")
                        End If
                        If IsDBNull(dr.Item("OpenbyDate")) Then
                            OpenByDate = "X"
                        Else
                            OpenByDate = dr.Item("OpenbyDate")
                        End If
                        If IsDBNull(dr.Item("CLoseByDate")) Then
                            ClosedByDate = "X"
                        Else
                            ClosedByDate = dr.Item("CLoseByDate")
                        End If
                        If IsDBNull(dr.Item("WitnessedByDate")) Then
                            WitnessedByDate = "X"
                        Else
                            WitnessedByDate = dr.Item("WitnessedByDate")
                        End If
                        If IsDBNull(dr.Item("OpenWitnessedByDate")) Then
                            OpenWitnessedByDate = "X"
                        Else
                            OpenWitnessedByDate = dr.Item("OpenWitnessedByDate")
                        End If
                        If IsDBNull(dr.Item("Closewitnessedbydate")) Then
                            CloseWitnessedByDate = "X"
                        Else
                            CloseWitnessedByDate = dr.Item("Closewitnessedbydate")
                        End If
                        If IsDBNull(dr.Item("GreaterByDate")) Then
                            GreaterByDate = "X"
                        Else
                            GreaterByDate = dr.Item("GreaterByDate")
                        End If
                        If IsDBNull(dr.Item("OpenGreaterByDate")) Then
                            OpenGreaterByDate = "X"
                        Else
                            OpenGreaterByDate = dr.Item("OpenGreaterByDate")
                        End If
                        If IsDBNull(dr.Item("CloseGreaterByDate")) Then
                            CloseGreaterByDate = "X"
                        Else
                            CloseGreaterByDate = dr.Item("CloseGreaterByDate")
                        End If
                        If IsDBNull(dr.Item("ComplianceByDate")) Then
                            ComplianceByDate = "X"
                        Else
                            ComplianceByDate = dr.Item("ComplianceByDate")
                        End If
                        If IsDBNull(dr.Item("OpenComplianceByDate")) Then
                            OpenComplianceByDate = "X"
                        Else
                            OpenComplianceByDate = dr.Item("OpenComplianceByDate")
                        End If
                        If IsDBNull(dr.Item("CloseComplianceByDate")) Then
                            CloseComplianceByDate = "X"
                        Else
                            CloseComplianceByDate = dr.Item("CloseComplianceByDate")
                        End If
                        If IsDBNull(dr.Item("OtherWitnessed")) Then
                            OtherWitnessed = ""
                        Else
                            OtherWitnessed = dr.Item("OtherWitnessed")
                        End If
                    End While

                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        ReDim Preserve MedianArrayByDateOpen(i)
                        MedianArrayByDateOpen(i) = CInt(dr2.Item("DaysOpenByDate"))
                        i += 1
                    End While


                    dr3 = cmd3.ExecuteReader
                    While dr3.Read
                        ReDim Preserve MedianArrayByDateClose(j)
                        MedianArrayByDateClose(j) = CInt(dr3.Item("DaysCloseByDate"))
                        j += 1
                    End While

                    dr4 = cmd4.ExecuteReader
                    While dr4.Read
                        ReceivedTotal = dr4.Item("ReceivedTotal")
                        OpenTotal = dr4.Item("OpenTotal")
                        OpenWitnessedTotal = dr4.Item("OpenWitnessedTotal")
                        OpenComplianceTotal = dr4.Item("OpenComplianceTotal")
                        OpenGreaterTotal = dr4.Item("OpenGreaterTotal")
                        ClosedTotal = dr4.Item("CloseTotal")
                        ClosedWitnessedTotal = dr4.Item("ClosedWitnessedTotal")
                        ClosedComplianceTotal = dr4.Item("ClosedComplianceTotal")
                        ClosedGreaterTotal = dr4.Item("ClosedGreaterTotal")
                    End While

                    dr5 = cmd5.ExecuteReader
                    While dr5.Read
                        ReDim Preserve MedianArrayOpen(n)
                        MedianArrayOpen(n) = CInt(dr5.Item("DaysOpen"))
                        n += 1
                    End While

                    dr6 = cmd6.ExecuteReader
                    While dr6.Read
                        ReDim Preserve MedianArrayClosed(o)
                        MedianArrayClosed(o) = CInt(dr6.Item("DaysClosed"))
                        o += 1
                    End While

                Catch ex As Exception
                    ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
                Finally

                End Try
                ' 



                If MedianArrayByDateOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2))) / 2
                    If MedianArrayByDateOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianByDate = MedianArrayByDateOpen(MedianArrayByDateOpen.GetLength(0) \ 2)
                    If MedianArrayByDateOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = MedianArrayByDateOpen(MedianArrayByDateOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayByDateClose.GetLength(0) Mod 2 = 0 Then
                    CloseMedianByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2))) / 2
                    If MedianArrayByDateClose.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    CloseMedianByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) \ 2)
                    If MedianArrayByDateClose.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianTotal = (MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = (MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianTotal = MedianArrayOpen(MedianArrayOpen.GetLength(0) \ 2)
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = MedianArrayOpen(MedianArrayOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayClosed.GetLength(0) Mod 2 = 0 Then
                    ClosedMedianTotal = (MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2))) / 2
                    If MedianArrayClosed.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = (MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    ClosedMedianTotal = MedianArrayClosed(MedianArrayClosed.GetLength(0) \ 2)
                    If MedianArrayClosed.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = MedianArrayClosed(MedianArrayClosed.GetLength(0) * 0.8)
                    End If
                End If

            End If

            Statement = Statement &
            "For the Staff member: " & Staff & vbCrLf &
            vbTab & DateStatement & vbCrLf & vbCrLf &
            "1. " & ReceivedByDate & " Test Reports Received " & vbCrLf &
            "2. " & OpenByDate & " of these " & ReceivedByDate & " Test Reports are currently open" & vbCrLf &
            "3. " & ClosedByDate & " of these " & ReceivedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
            "4. " & WitnessedByDate & " of these " & ReceivedByDate & " Test Reports were witnessed by " & Staff & vbCrLf &
            "5. " & OpenWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are still open " & vbCrLf &
            "6. " & CloseWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
            "7. " & GreaterByDate & " of these " & ReceivedByDate & " Test Reports have been open for more than 50-days" & vbCrLf &
            "8. " & OpenGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more than 50-days are still open " & vbCrLf &
            "9. " & CloseGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more then 50-days are currently closed " & vbCrLf & vbCrLf &
            "10. " & ComplianceByDate & " of these " & ReceivedByDate & " Test Reports were out of compliance" & vbCrLf &
            "11. " & OpenComplianceByDate & " of these " & ComplianceByDate & " Test Reports are still open " & vbCrLf &
            "12. " & CloseComplianceByDate & " of these " & ComplianceByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
            "13. The median time taken to complete those " & ClosedByDate & " Closed Test Reports was " & CloseMedianByDate & "-days" & vbCrLf &
            "14. The 80% Percentile Time taken to complete those " & ClosedByDate & " Closed Test Reports was " & ClosePercentileByDate & "-days" & vbCrLf &
            "15. The median time of the " & OpenByDate & " Open Test Reports is " & OpenMedianByDate & "-days" & vbCrLf &
            "16. The 80% Percentile Time of the " & OpenByDate & " Open Test Reports is " & OpenPercentileByDate & "-days" & vbCrLf & vbCrLf &
            "17. Overall " & Staff & " has received " & ReceivedTotal & " Test Reports" & vbCrLf & vbCrLf &
            "18. " & OpenTotal & " of " & ReceivedTotal & " Test Reports are currently open" & vbCrLf &
            "19. " & OpenWitnessedTotal & " of these " & OpenTotal & " Test Reports have been witnessed" & vbCrLf &
            "20. " & OpenComplianceTotal & " of these " & OpenTotal & " Test Reports are currently out of compliance " & vbCrLf &
            "21. " & OpenGreaterTotal & " of these " & OpenTotal & " Test Reports have been open for more than 50-days" & vbCrLf &
            "22. The median time of the " & OpenTotal & " Open Test Reports is " & OpenMedianTotal & "-days" & vbCrLf &
            "23. The 80% Percentile Time of the " & OpenTotal & " Open Test Reports is " & PercentileOpenTotalDay & "-days" & vbCrLf & vbCrLf &
            "24. " & ClosedTotal & " of " & ReceivedTotal & " Test Reports are currently closed " & vbCrLf &
            "25. " & ClosedWitnessedTotal & " of these " & ClosedTotal & " Test Reports have been witnessed" & vbCrLf &
            "26. " & ClosedComplianceTotal & " of these " & ClosedTotal & " Test Reports are out of compliance " & vbCrLf &
            "27. " & ClosedGreaterTotal & " of these " & ClosedTotal & " Test Reports were open for more than 50-days" & vbCrLf &
            "28. The median time of the " & ClosedTotal & " Closed Test Reports was " & ClosedMedianTotal & "-days" & vbCrLf &
            "29. The 80% Percentile Time of the " & ClosedTotal & " Closed Test Reports was " & PercentileClosedTotalDay & "-days" &
            vbCrLf & vbCrLf &
            "30. Additionally " & OtherWitnessed & " Test were witnessed but reviewed by another staff member. " & vbCrLf & vbCrLf &
            vbCrLf

            txtEngineerStatistics.Text = txtEngineerStatistics.Text & Statement

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub EngineerUnitStats()
        Dim strObject As Object
        Dim dtEngineers As New DataTable
        dtEngineers = dsEngineer.Tables("Engineers")
        Dim drEngineers As DataRow()
        Dim row As DataRow
        Dim EngineerGCode As String = "0"

        Try

            txtEngineerStatistics.Clear()

            For Each strObject In clbEngineers1.CheckedItems
                drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    EngineerGCode = row("numUserID")
                Next
                RunUnitEngineerStatistics(EngineerGCode)
            Next

            For Each strObject In clbEngineers2.CheckedItems
                drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    EngineerGCode = row("numUserID")
                Next
                RunUnitEngineerStatistics(EngineerGCode)
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub PrintEngineerUnitStats()
        Dim WordText As String
        'Dim WordApp As New Word.ApplicationClass
        'Dim wordDoc As Word.DocumentClass
        Dim wordDoc As Microsoft.Office.Interop.Word.Document
        Dim WordApp As New Microsoft.Office.Interop.Word.Application


        Try
            WordText = txtEngineerStatistics.Text
            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(WordText)
            WordApp.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

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
            "substr(ISMPMaster.strAIRSNumber, 5) as AIRSNumber, strClosed, " &
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as ForDatTestDateStart, " &
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as ForDatReceivedDate, " &
            "to_char(datCompleteDate, 'dd-Mon-yyyy') as ForDatCompleteDate, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as ForDatReceivedDate, " &
            "(to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) as Days " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as fordatTestDateStart, to_char(datTestDateEnd, 'dd-Mon-yyyy') as fordatTestDateEnd, " &
            "substr(ISMPMaster.strAIRSNumber, 5) as AIRSNumber, strFacilityName, strFacilityCity, strFacilityState, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearPage()
        Try

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId:="cmdCB")>
    Sub AddExcelFile()
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            Dim myStream As Stream
            Dim path As New OpenFileDialog
            Dim PathName As String = "N/A"
            Dim FileName As String = ""
            Dim IDnumber As String = ""

            path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            path.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            path.FilterIndex = 2
            path.RestoreDirectory = True

            If path.ShowDialog() = DialogResult.OK Then
                myStream = path.OpenFile()
                If Not (myStream Is Nothing) Then
                    If path.ValidateNames() Then
                        PathName = path.FileName.ToString
                        FileName = Mid(PathName, PathName.LastIndexOf("\") + 2, (Len(PathName) - PathName.LastIndexOf(".") + 5))
                    Else
                        PathName = "N/A"
                        FileName = "N/A"
                    End If

                    ' Insert code to read the stream here.
                    myStream.Close()
                End If
            End If

            If PathName <> "N/A" Then

                SQL = "select max(FileId) as ID " &
                "from ISMPTestReportAids "
                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If dr.IsDBNull(0) Then
                        IDnumber = "0"
                    Else
                        IDnumber = dr.Item("ID")
                    End If
                Else
                    IDnumber = "0"
                End If

                IDnumber += 1

                If txtFileName.Text <> "" Then
                    FileName = txtFileName.Text
                End If
                FileName = Mid(FileName, 1, 50)

                Dim da As SqlDataAdapter
                Dim ds As DataSet
                Dim Fs As FileStream = New FileStream(PathName, FileMode.Open, FileAccess.Read)
                Dim DocData As Byte()
                ReDim DocData(Fs.Length)
                Fs.Read(DocData, 0, System.Convert.ToInt32(Fs.Length))
                Fs.Close()

                SQL = "Select * " &
                "From ISMPTestReportAIDS " &
                "where FileID = '" & IDnumber & "' "

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da = New SqlDataAdapter(SQL, CurrentConnection)
                Dim cmdCB As SqlCommandBuilder = New SqlCommandBuilder(da)
                ds = New DataSet("IAIPData")
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                da.Fill(ds, "IAIPData")
                Dim row As DataRow = ds.Tables("IAIPData").NewRow()
                row("FileID") = IDnumber
                row("FileTitle") = FileName
                row("ISMPBLOB") = DocData
                ds.Tables("IAIPData").Rows.Add(row)
                da.Update(ds, "IAIPData")

                LoadExcelDataSet()
                txtNewFileName.Clear()

                MsgBox("File Added")

            Else
                MsgBox("Bad Path")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RemoveExcelFile()
        Dim FileID As String

        Try

            If txtFileName.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                FileID = txtFileName.Text
                FileID = Mid(FileID, 1, FileID.IndexOf(" - "))

                SQL = "Delete ISMPTestReportAids " &
                "where FileID = '" & FileID & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                LoadExcelDataSet()
                MsgBox("File Removed")
                txtFileName.Clear()

            Else
                MsgBox("First Select a file from the Datagrid")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
             "    and datCompleteDate Between Trunc(sysdate) - 60 and Trunc(sysdate) " &
             "Group by strfirstname) ClosedReport, " &
             "(SELECT EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenFiftys " &
             "    FROM EPDUSerProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'False' ) " &
             "    and EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "    and datReceivedDate <= (Trunc(SysDate) - 50) " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                 vbCrLf & line & vbCrLf & "Source Test Summary" & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Print Date: " & OracleDate &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DownloadExcelFiles()
        Dim FileID As String
        Dim FileName As String
        Dim path As New SaveFileDialog
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook

        Dim DestFilePath As String = "N/A"


        Try


            If txtFileName.Text <> "" Then
                FileID = txtFileName.Text
                FileID = Mid(FileID, 1, FileID.IndexOf(" - "))

                FileName = txtFileName.Text
                FileName = Mid(FileName, FileName.IndexOf(" - ") + 4)

                path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                path.FileName = FileName
                path.Filter = "Microsoft Office Excel Workbook (.xls)|.xls"
                path.FilterIndex = 1
                path.DefaultExt = ".xls"

                If path.ShowDialog = DialogResult.OK Then
                    DestFilePath = path.FileName.ToString
                Else
                    DestFilePath = "N/A"
                End If

                If DestFilePath <> "N/A" Then
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    SQL = "Select " &
                    "FileId, FileTitle, ISMPBlob " &
                    "from ISMPTestReportAids " &
                    "Where FileID = '" & FileID & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader

                    dr.Read()
                    Dim b(dr.GetBytes(2, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                    dr.GetBytes(2, 0, b, 0, b.Length)
                    dr.Close()

                    Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Write(b, 0, b.Length)
                    fs.Close()

                    ExcelDoc = ExcelApp.Workbooks.Open(DestFilePath)
                    ExcelDoc.Activate()
                    If ExcelApp.Visible = False Then
                        ExcelApp.Visible = True
                    End If



                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try

    End Sub
    Sub RunApplicationReport()
        Try


            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strISMPUnit, strISMPReviewer, datISMPReviewDate,  " &
            "strISMPComments,  " &
            "substr(SSPPApplicationMaster.strAIRSNumber, 5) as AIRSNumber, " &
            "APBFacilityinformation.strFacilityName " &
            "from APBFacilityInformation, SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData " &
            "where datISMPReviewDate between '" & DTPAppStartDate.Text & "' and '" & DTPAppEndDate.Text & "'  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and APBFacilityInformation.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub PrintApplicationReport()
        Try
            Dim wordDoc As Microsoft.Office.Interop.Word.Document
            Dim WordApp As New Microsoft.Office.Interop.Word.Application
            'Dim WordApp As New Word.ApplicationClass
            'Dim wordDoc As Word.DocumentClass

            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(txtISMPApplicationReport.Text)
            WordApp.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "case when numUnit = '13' then round((ReceivedCount/ComUnitTotal)*100, 2)  " &
            " when numUnit = '12' then round((ReceivedCount/ChemUnitTotal)*100, 2)  " &
            "End UnitPercent,  " &
            "ComUnitTotal, ChemUnitTotal, " &
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
            "(select count(*) as ComUnitTotal  " &
            "from ISMPReportInformation,   " &
            "(select numUserID   " &
            "from EPDUserProfiles   " &
            "where numProgram = '3'  " &
            "and numUnit = '13') ComUnit  " &
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)   " &
            "and strClosed = 'True'  " &
            "and strReviewingEngineer  = ComUnit.numUserID) ComTotal,  " &
            "(select count(*) as ChemUnitTotal  " &
            "from ISMPReportInformation,   " &
            "(select numUserID   " &
            "from EPDUserProfiles   " &
            "where numProgram = '3'   " &
            "and numUnit = '12') ChemUnit  " &
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)   " &
            "and strClosed = 'True'  " &
            "and strReviewingEngineer  = ChemUnit.numUserID) ChemTotal,  " &
            "(select strReviewingEngineer,  " &
            "Median(dayin) as MedDays    " &
            "from  " &
            "(select  " &
            "strReviewingEngineer,  " &
            "case  " &
            "when strClosed = 'True' then (datCompleteDate - datReceivedDate)  " &
            "when strClosed = 'False' then (round(sysdate, 'DDD') - datReceivedDate) " &
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
            "when strClosed = 'True' then (datCompleteDate - datReceivedDate)  " &
            "when strClosed = 'False' then (round(sysdate, 'DDD') - datReceivedDate) " &
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
            dgvUnitStats.Columns("Engineer").HeaderText = "Engineer"
            dgvUnitStats.Columns("Engineer").DisplayIndex = 0
            dgvUnitStats.Columns("strUnitDesc").HeaderText = "Engineer Unit"
            dgvUnitStats.Columns("strUnitDesc").DisplayIndex = 1
            dgvUnitStats.Columns("TotalReceived").HeaderText = "Total Reviewed"
            dgvUnitStats.Columns("TotalReceived").DisplayIndex = 2
            dgvUnitStats.Columns("TotalReceived").Visible = False
            dgvUnitStats.Columns("ReceivedCount").HeaderText = "Engineer Reviewed"
            dgvUnitStats.Columns("ReceivedCount").DisplayIndex = 3
            dgvUnitStats.Columns("ProgramPercent").HeaderText = "% Program "
            dgvUnitStats.Columns("ProgramPercent").DisplayIndex = 5
            dgvUnitStats.Columns("UnitPercent").HeaderText = "% Unit"
            dgvUnitStats.Columns("UnitPercent").DisplayIndex = 4
            dgvUnitStats.Columns("MedDays").HeaderText = "Median Days"
            dgvUnitStats.Columns("MedDays").DisplayIndex = 6
            dgvUnitStats.Columns("PercentDays").HeaderText = "80% Days"
            dgvUnitStats.Columns("PercentDays").DisplayIndex = 7
            dgvUnitStats.Columns("Witnessed").HeaderText = "Witnessed"
            dgvUnitStats.Columns("Witnessed").DisplayIndex = 8
            dgvUnitStats.Columns("ComUnitTotal").HeaderText = "Combustion Unit Total"
            dgvUnitStats.Columns("ComUnitTotal").DisplayIndex = 9
            dgvUnitStats.Columns("ComUnitTotal").Visible = False
            dgvUnitStats.Columns("ChemUnitTotal").HeaderText = "Chemicals Unit Total"
            dgvUnitStats.Columns("ChemUnitTotal").DisplayIndex = 10
            dgvUnitStats.Columns("ChemUnitTotal").Visible = False


            Try
                txtTotalReviewed.Text = dgvUnitStats(2, 0).Value
            Catch ex As Exception
                txtTotalReviewed.Text = "0"
            End Try

            Try
                txtChemicalTotal.Text = dgvUnitStats(7, 0).Value
            Catch ex As Exception
                txtChemicalTotal.Text = "0"
            End Try


            Try
                txtCombustionTotal.Text = dgvUnitStats(6, 0).Value
            Catch ex As Exception
                txtCombustionTotal.Text = "0"
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
                If IsDBNull(dgvUnitStats(8, x).Value) Then
                    MedianAvg = MedianAvg + 0
                Else
                    MedianAvg = MedianAvg + (dgvUnitStats(8, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
                If IsDBNull(dgvUnitStats(9, x).Value) Then
                    PercentialAvg = PercentialAvg + 0
                Else
                    PercentialAvg = PercentialAvg + (dgvUnitStats(9, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
                If IsDBNull(dgvUnitStats(10, x).Value) Then
                    WitnessAvg = WitnessAvg + 0
                Else
                    WitnessAvg = WitnessAvg + (dgvUnitStats(10, x).Value * dgvUnitStats(4, x).Value / 100)
                End If
            Next

            txtAverageofTotalReviewed.Text = TotalAvg
            txtAverageMedianDays.Text = MedianAvg
            txtPercentialAverage.Text = PercentialAvg
            txtAverageWitnessed.Text = WitnessAvg

            txtUnitStatsCount.Clear()
            txtUnitStatReferenceNumber.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
    'Private Sub txtAIRSNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        If txtAIRSNumber.Text <> "" Then
    '            If TPReportAssignment.Focus = True Then
    '                LVTestReportAssignment.Clear()
    '                lblTestReportAssignment.Items.Clear()
    '                txtTestReportCount.Text = "0"
    '                LoadByAIRSNumberTestReportAssignmentDataSet()
    '                LoadLVTestReportAssignment()
    '            End If
    '            If TPTestReportStatistics.Focus = True Then
    '                AddFacilityName()

    '            End If

    '        End If

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try

    'End Sub
    Private Sub TBManagersTools_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBManagersTools.ButtonClick
        Try

            Select Case TBManagersTools.Buttons.IndexOf(e.Button)
                Case 0
                    If TPReportAssignment.Focus = True Then
                        SaveTestReportsAssignments()
                    End If
                    If TPAIRSReportsPrinted.Focus = True Then
                        SaveAIRSPrinting()
                    End If
                    If TPUnitAssignment.Focus = True Then
                        SaveUnitAssignments()
                    End If
                Case 1

                Case 2

                Case 3
                    If TPReportAssignment.Focus = True Then
                        cboEngineer.Text = ""
                        lblTestReportAssignment.Items.Clear()
                        txtTestReportCount.Text = 0
                        LVTestReportAssignment.Clear()
                        LoadLVTestReportAssignment()
                    End If
                    If TPMethods.Focus = True Then
                        txtMethodCode.Clear()
                        txtMethodDescription.Clear()
                        txtMethodNumber.Clear()
                    End If
                Case 4
                    Me.Close()
                Case 5
                    Me.Close()
                Case Else
                    MsgBox("try clicking again")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Menu Items"
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            If TPReportAssignment.Focus = True Then
                SaveTestReportsAssignments()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClear.Click
        Try

            ClearTestReportAssignmentTab()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiClearTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClearTab.Click
        Try

            If TPReportAssignment.Focus = True Then
                ClearTestReportAssignmentTab()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
#Region "Viewing Options"
    Private Sub MmiViewTestReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiViewTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAllNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiUnassignedTestReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiUnassignedTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadUnassignedNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTestReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedTestReports.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAssignedNoUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiShowDeletedRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiShowDeletedRecords.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadDeletedTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
                Me.ValueFromFacilityLookUp2 = facilityLookupDialog.SelectedFacilityName
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub MmiViewByFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiViewByFacility.Click
        If TPReportAssignment.Focus = True Then
            OpenFacilityLookupTool()
        End If
    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumber.Text = Value
            txtAIRSNumber2.Text = Value
        End Set
    End Property
    Public WriteOnly Property ValueFromFacilityLookUp2() As String
        Set(ByVal Value2 As String)
            txtFacility.Text = Value2
        End Set
    End Property
    Private Sub MmiByEngineer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiByEngineer.Click
        Try

            'Must have all engineers loaded some how
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub MmiAllByUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllByUnit.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAllByUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiUnassignedByUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiUnassignedByUnit.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedByUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedByUnit.Click
        Try

            If TPReportAssignment.Focus = True Then
                LVTestReportAssignment.Clear()
                lblTestReportAssignment.Items.Clear()
                txtTestReportCount.Text = "0"
                LoadAssignedByUnitTestReportAssignmentDataSet()
                LoadLVTestReportAssignment()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "By Test Report-Unassigned"
    Private Sub MmiUnassigned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiUnassigned.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackTwoRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackTwoRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackThreeRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackThreeRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiOneStackFourRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackFourRun.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiTwoStackStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiTwoStackDRE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiLoadingRack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiFlare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPondTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiGasConcentration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiRata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPEMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMemoPTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod9Single_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod9Multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiMethod22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "By Test Report-Assigned"
    Private Sub MmiAssignedNoDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedNoDocument.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackTwoRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedOneStackTwoRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackThreeRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedOneStackThreeRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedOneStackFourRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedOneStackFourRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTwoStackStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedTwoStackDRE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedLoadingRack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedFlare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedPondTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedGasConcentration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedRata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedPEMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMemoPTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod9Single_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod9Multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAssignedMethod22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAssignedMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "By Test Report-All"
    Private Sub MmiAllNoDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllNoDoc.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "001"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackTwoRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllOneStackTwoRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "002"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackThreeRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllOneStackThreeRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "003"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllOneStackFourRuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllOneStackFourRuns.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "004"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllTwoStackStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllTwoStackStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "005"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllTwoStackDRE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllTwoStackDRE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "006"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllLoadingRack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllLoadingRack.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "007"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllFlare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllFlare.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "010"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllPondTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllPondTreatment.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "008"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllGasConcentration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllGasConcentration.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "009"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllRata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllRata.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "011"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllPEMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllPEMS.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "017"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMemoStandard.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "012"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMemoToFile.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "013"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMemoPTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMemoPTE.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "018"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod9Single_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMethod9Single.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "016"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod9Multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMethod9Multi.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "014"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiAllMethod22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllMethod22.Click
        Try

            Dim ReportType As String
            LVTestReportAssignment.Clear()
            lblTestReportAssignment.Items.Clear()
            txtTestReportCount.Text = "0"
            ReportType = "015"
            LoadByTestReportAssignedTestReportAssignmentDataSet(ReportType)
            LoadLVTestReportAssignment()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#End Region


#End Region
    Private Sub LVTestReportAssignment_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles LVTestReportAssignment.ItemCheck
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LLRunFacilityReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLRunFacilityReport.LinkClicked
        Try

            RunFacilityStatistics()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LLFaciltiySearch_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLFaciltiySearch.LinkClicked
        If TPTestReportStatistics.Focus = True Then
            OpenFacilityLookupTool()
        End If
    End Sub
    Private Sub MmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCut.Click
        Try

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FillFacilitiesDataGrid()
        Try

            dsFacilityList = New DataSet

            SQL = "Select strFacilityName, substr(APBFacilityInformation.strAIRSnumber, 5) as StrAIRSNumber, " &
            "CASE " &
            "when strISMPUnit = 'H' then 'Chemical and VOC' " &
            "when strISMPUnit = 'I' then 'Combustion and Mineral' " &
            "ELSE 'Unassigned' " &
            "END as UnitAssigned " &
            "from APBFacilityInformation, ISMPFacilityAssignment " &
            "where APBFacilityInformation.strAIRSNumber = ISMPFacilityAssignment.strAIRSNumber " &
            "order by strAIRSNumber "

            cmd = New SqlCommand(SQL, CurrentConnection)

            daFacilityList = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFacilityList.Fill(dsFacilityList, "FacilityList")



            LVFacilities.View = View.Details
            LVFacilities.AllowColumnReorder = True
            LVFacilities.CheckBoxes = True
            LVFacilities.GridLines = True
            LVFacilities.FullRowSelect = True

            Dim dtFacilitiesAssignment As New DataTable
            dtFacilitiesAssignment = dsFacilityList.Tables("FacilityList")

            Dim drFacilitiesAssignment As DataRow()

            Dim row As DataRow

            drFacilitiesAssignment = dtFacilitiesAssignment.Select()

            LVFacilities.Columns.Add("AIRS Number", 100, HorizontalAlignment.Left)
            LVFacilities.Columns.Add("Facility Name", 300, HorizontalAlignment.Left)
            LVFacilities.Columns.Add("Currently Assigned Unit", 200, HorizontalAlignment.Left)

            For Each row In drFacilitiesAssignment

                Dim item1 As New ListViewItem(row("StrAIRSNumber").ToString())

                item1.Checked = False
                item1.SubItems.Add(row("strFacilityName").ToString())
                item1.SubItems.Add(row("UnitAssigned").ToString())

                LVFacilities.Items.AddRange(New ListViewItem() {item1})

            Next row

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LLBAllFacilities_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLBAllFacilities.LinkClicked
        Try

            FillFacilitiesDataGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LVFacilities_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles LVFacilities.ColumnClick
        Try

            ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
            LVFacilities.ListViewItemSorter = New ListViewItemComparer(e.Column)
            ' Call the sort method to manually sort the column based on the ListViewItemComparer implementation.
            LVFacilities.Sort()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LVFacilities_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles LVFacilities.ItemCheck
        Try

            If LVFacilities.Items.Item(e.Index).Checked = True Then
                lsbFacilities.Items.Remove(LVFacilities.Items.Item(e.Index).Text)
            Else
                lsbFacilities.Items.Add(LVFacilities.Items.Item(e.Index).Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbRunMonthlyReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbRunMonthlyReport.LinkClicked
        Try

            RunMonthlyReport()
            GetOutOfComplianceReport()
            RunSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbPrintMonthlyReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPrintMonthlyReport.LinkClicked
        Try

            ExportToWord()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LlbUnitStatistics_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LlbUnitStatistics.LinkClicked
        Try


            RunUnitStatistics()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub txtDaysOpen_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDaysOpen.TextChanged
        Try

            If txtDaysOpen2.ReadOnly = True Then
                txtDaysOpen2.Text = txtDaysOpen.Text
                txtDaysOpen3.Text = txtDaysOpen.Text
                txtDaysOpen4.Text = txtDaysOpen.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtDaysOpen2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDaysOpen2.TextChanged
        Try

            If txtDaysOpen.ReadOnly = True Then
                txtDaysOpen.Text = txtDaysOpen2.Text
                txtDaysOpen3.Text = txtDaysOpen2.Text
                txtDaysOpen4.Text = txtDaysOpen2.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbEngineerTestReports_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEngineerTestReports.LinkClicked
        Try

            EngineerTestReport()
            EngineerOpenTestReports()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrEngineersFacilityList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrEngineersFacilityList.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewReport.LinkClicked
        Try
            Dim id As String = txtReferenceNumber.Text
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm(ISMPTestReports, id)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExportToExcel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportToExcel.LinkClicked
        dsEngineerGrid.Tables(0).ExportToExcel(Me)
    End Sub
    Private Sub llbRunSummaryReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try

            RunSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbPrintSummaryReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try

            PrintSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbAddExcelFile_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbAddExcelFile.LinkClicked
        Try

            AddExcelFile()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbRemoveFile_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbRemoveFile.LinkClicked
        Try

            RemoveExcelFile()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrExcelFiles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrExcelFiles.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrExcelFiles.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrExcelFiles(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrExcelFiles(hti.Row, 1)) Then
                    Else
                        txtFileName.Text = dgrExcelFiles(hti.Row, 0) & " - " & dgrExcelFiles(hti.Row, 1)
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbPrintSummaryReport_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPrintSummaryReport.LinkClicked
        Try

            PrintSummaryReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbRunEngineerStatReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbRunEngineerStatReport.LinkClicked
        Try


            EngineerUnitStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbExportStatsToWord_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportStatsToWord.LinkClicked
        Try

            PrintEngineerUnitStats()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbDownloadExcelFiles_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbDownloadExcelFiles.LinkClicked
        Try

            DownloadExcelFiles()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunApplicationReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunApplicationReport.Click
        Try

            RunApplicationReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnApplicationReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplicationReport.Click
        Try

            PrintApplicationReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunUnitStatsReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunUnitStatsReport.Click
        Try

            RunUnitStatistics2()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblTotalTests_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblTotalTests.LinkClicked
        Try
            SQL = "select strReferenceNumber, " &
            "(strLastName|| ', ' ||strFirstName) as Engineer,  " &
            "case  " &
            "when datTestDateStart = '04-Jul-1776' then  null  " &
            "else to_char(datTestDateStart, 'dd-Mon-yyyy') " &
            "End datTestDateStart,  " &
            "case  " &
            "when datReceivedDate = '04-Jul-1776' then Null  " &
            "else to_char(datReceivedDate, 'dd-Mon-yyyy')  " &
            "End datReceiveddate,  " &
            "Case  " &
            "when datCompleteDate = '04-Jul-1776' then Null  " &
            "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblChemTests_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblChemTests.LinkClicked
        Try
            SQL = "select strReferenceNumber, " &
            "(strLastName|| ', ' ||strFirstName) as Engineer,  " &
            "case  " &
            "when datTestDateStart = '04-Jul-1776' then Null  " &
            "else to_char(datTestDateStart, 'dd-Mon-yyyy')  " &
            "end datTestDateStart,  " &
            "case  " &
            "when datReceivedDate = '04-Jul-1776' then Null  " &
            "else to_Char(datReceivedDate, 'dd-Mon-yyyy')  " &
            "End datReceivedDate,  " &
            "case  " &
            "when datCompleteDate = '04-Jul-1776' then Null  " &
            "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " &
            "End datCompleteDate  " &
            "from ISMPReportInformation,  EPDUserProfiles,  " &
            "(select numUserID    " &
            "from EPDUserProfiles " &
            "where numProgram = '3'  " &
            "and numUnit = '12') ChemUnit   " &
            "where ISMPReportInformation.strReviewingEngineer = EPDUSerProfiles.numUSerID   " &
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
            "and (strDelete <> 'True' or strDelete is Null)    " &
            "and strClosed = 'True'   " &
            "and strReviewingEngineer  = ChemUnit.numUserID "

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblComTests_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblComTests.LinkClicked
        Try
            SQL = "select strReferenceNumber, " &
           "(strLastName|| ', ' ||strFirstName) as Engineer,  " &
           "case  " &
           "when datTestDateStart = '04-Jul-1776' then Null  " &
           "else to_char(datTestDateStart, 'dd-Mon-yyyy')  " &
           "end datTestDateStart,  " &
           "case  " &
           "when datReceivedDate = '04-Jul-1776' then Null  " &
           "else to_Char(datReceivedDate, 'dd-Mon-yyyy')  " &
           "End datReceivedDate,  " &
           "case  " &
           "when datCompleteDate = '04-Jul-1776' then Null  " &
           "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " &
           "End datCompleteDate  " &
           "from ISMPReportInformation,  EPDUserProfiles,  " &
           "(select numUserID " &
           "from EPDUserProfiles     " &
           "where numProgram = '3' " &
           "and numUnit = '13') ComUnit   " &
           "where ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID   " &
           "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " &
           "and (strDelete <> 'True' or strDelete is Null)    " &
           "and strClosed = 'True'   " &
           "and strReviewingEngineer  = ComUnit.numUserID "

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvUnitStats_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvUnitStats.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvUnitStats.HitTest(e.X, e.Y)

        Try
            If dgvUnitStats.Columns(0).HeaderText = "Reference #" Then
                If dgvUnitStats.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtUnitStatReferenceNumber.Text = dgvUnitStats(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnViewTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTestReport.Click
        Try
            Dim id As String = txtUnitStatReferenceNumber.Text
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm(ISMPTestReports, id)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvMethods_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvMethods.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvMethods.HitTest(e.X, e.Y)

        Try
            If dgvMethods.RowCount > 0 And hti.RowIndex <> -1 Then
                txtMethodCode.Text = dgvMethods(0, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub txtMethodCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMethodCode.TextChanged
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnUpdateMethods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMethods.Click
        Try
            Dim temp As String = ""

            If txtMethodNumber.Text <> "" And txtMethodDescription.Text <> "" Then
                txtMethodNumber.BackColor = Color.White
                txtMethodDescription.BackColor = Color.White
                temp = "Method " & txtMethodNumber.Text.ToUpper & " - " & txtMethodDescription.Text

                SQL = "Select " &
                "strMethodCode " &
                "From LookUpISMPMethods " &
                "where substr(strMethodDesc, 1, instr(strMethodDesc,'-')-2)  = 'Method " & Replace(txtMethodNumber.Text.ToUpper, "'", "''") & "' "
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub


    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub btnAddTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTestReport.Click
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
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
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
                "'" & CurrentUser.UserID & "', '" & OracleDate & "', " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddTestReport.Click
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
            dtpAddTestReportDateReceived.Text = OracleDate
            dtpAddTestReportDateReceived.BackColor = Color.White
            DTPAddTestReportDateCompleted.Text = OracleDate
            DTPAddTestReportDateCompleted.BackColor = Color.White


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseHistoricTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseHistoricTestReport.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReOpenHistoricTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReOpenHistoricTestReport.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class
