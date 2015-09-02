'Imports System.DateTime
Imports Oracle.ManagedDataAccess.Client
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
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim cmd4, cmd5, cmd6 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim dr4, dr5, dr6 As OracleDataReader
    Dim recExist As Boolean
    Dim dsEngineer As DataSet
    Dim dsCounty As DataSet
    Dim dsCity As DataSet
    Dim dsFacilityList As DataSet
    Dim daEngineer As OracleDataAdapter
    Dim daCounty As OracleDataAdapter
    Dim daCity As OracleDataAdapter
    Dim dsTestReportAssignments As DataSet
    Dim daTestreportAssignments As OracleDataAdapter
    Dim daFacilityList As OracleDataAdapter
    Dim dsEngineerGrid As DataSet
    Dim daEngineerGrid As OracleDataAdapter
    Dim dsSummaryReport As DataSet
    Dim daSummaryReport As OracleDataAdapter
    Dim dsExcelFiles As DataSet
    Dim daUnitStats As OracleDataAdapter
    Dim daExcelFiles As OracleDataAdapter
    Dim dsUnitStats As DataSet
    Dim dsMethods As DataSet
    Dim daMethods As OracleDataAdapter


    Private Sub ISMPManagersTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
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
            panel2.Text = UserName
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

            If UserAccounts <> "" Then
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
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ShowCorrectPanels()
        Try

            Select Case UserUnit
                Case ""
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = False
                    PanelChemVOC.Visible = True
                    txtDaysOpen2.ReadOnly = True
                    PanelAll.Visible = True

                Case "12"
                    PanelCombustMineral.Visible = False
                    txtDaysOpen.ReadOnly = False
                    txtDaysOpen2.ReadOnly = True
                    PanelChemVOC.Visible = True
                    PanelAll.Visible = True
                Case "13"
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = True
                    txtDaysOpen2.ReadOnly = False
                    PanelChemVOC.Visible = False
                    PanelAll.Visible = True
                Case "14"
                    PanelCombustMineral.Visible = True
                    txtDaysOpen.ReadOnly = False
                    txtDaysOpen2.ReadOnly = True
                    PanelChemVOC.Visible = True
                    PanelAll.Visible = True
                    'Case Else
                    '    PanelCombustMineral.Visible = False
                    '    PanelChemVOC.Visible = False
                    '    PanelAll.Visible = True
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

            SQL = "select " & _
            "(strLastName|| ', ' ||strFirstName) as UserName,  " & _
            "numUserID, numUnit  " & _
            "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.LookUpEPDUnits  " & _
            "where AIRBRANCH.EPDUSerProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode  " & _
            "and numProgram = '3'  " & _
            "and numUnit <> '14'  " & _
            "and numEmployeeStatus = '1' " & _
            "and numUserID <> '0' " & _
            "order by strlastname"

            SQL2 = "select strCountyCode, strCountyName from AIRBRANCH.LookUpCountyInformation " & _
            "order by strCountyName"

            SQL3 = "select distinct(strFacilityCity) as City from AIRBRANCH.APBFacilityInformation " & _
            "order by strFacilityCity"

            dsEngineer = New DataSet
            dsCounty = New DataSet
            dsCity = New DataSet

            daEngineer = New OracleDataAdapter(SQL, CurrentConnection)
            daCounty = New OracleDataAdapter(SQL2, CurrentConnection)
            daCity = New OracleDataAdapter(SQL3, CurrentConnection)

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

            If UserUnit = "" Then
                drEngineers = dtEngineers2.Select()
            Else
                drEngineers = dtEngineers2.Select("numUnit is Null")
            End If

            Select Case UserUnit
                Case "12"
                    drEngineers = dtEngineers2.Select("numUnit = '12'")
                    For Each row In drEngineers
                        clbEngineers1.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                Case "13"
                    drEngineers = dtEngineers2.Select("numUnit = '13'")
                    For Each row In drEngineers
                        clbEngineers2.Items.Add(row("UserName"))
                        clbEngineersList2.Items.Add(row("UserName"))
                    Next
                Case "14"
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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,   " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
                "strEmissionSource,   " & _
                "(Select strPollutantDescription   " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant   " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,   " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer   " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer   " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer   " & _
                "from AIRBRANCH.ISMPMaster, AIRBranch.APBFacilityInformation, AIRBRANCH.ISMPReportInformation   " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and ( strclosed = 'False' or strClosed is null ) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0'  " & _
                "and AIRBRANCH.ISMPReportInformation.strDelete is NULL "
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                      "and ( strclosed = 'False' or strClosed is null ) " & _
                    "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"

                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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

            SQL = "Select FileID, FileTitle " & _
            "From AIRBRANCH.ISMPTestReportAids"

            dsExcelFiles = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)

            daExcelFiles = New OracleDataAdapter(cmd)

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

            SQL = "Select * from AIRBRANCH.ISMPDocumentType"
            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "Select " & _
            "strMethodCode, strMethodDesc " & _
            "From AIRBRANCH.LookUpISMPMethods " & _
            "order by strMethodCode "

            dsMethods = New DataSet
            daMethods = New OracleDataAdapter(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                  "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                      "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                  "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                      "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0' " & _
                  "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0' " & _
                      "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                  "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                     "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                       "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                  "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                      "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPReportInformation.strDelete is not NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is not NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                 "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0' " & _
                 "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '0' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDocumentType = '" & ReportType & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                SQL = "Select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                "strEmissionSource,  " & _
                "(Select strPollutantDescription  " & _
                "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                "and AIRBRANCH.ISMPMaster.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
            Else
                If AccountFormAccess(17, 2) = "1" Then
                    SQL = "Select " & _
                    "AIRBRANCH.ISMPMaster.strReferenceNumber, AIRBRANCH.ISMPMaster.strAIRSNumber, strFacilityName,  " & _
                    "to_Char(DATTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart, " & _
                    "strEmissionSource,  " & _
                    "(Select strPollutantDescription  " & _
                    "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.LookUPPollutants.strPollutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.StrReferenceNumber) as Pollutant,  " & _
                    "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
                    "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                    "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer  " & _
                    "from AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPReportInformation  " & _
                    "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
                    "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
                    "and strReviewingUnit = '" & UserGCode & "' " & _
                      "and AIRBRANCH.ISMPMaster.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                    "and AIRBRANCH.ISMPReportInformation.strDelete is NULL"
                Else
                    SQL = ""
                End If
            End If

            dsTestReportAssignments = New DataSet
            daTestreportAssignments = New OracleDataAdapter

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

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
                    SQL = "select to_char(datReviewedBYUnitManager, 'dd-Mon-yyyy') as ReviewedByUnitManager " & _
                          "from AIRBRANCH.ISMPReportInformation " & _
                          "where strReferenceNumber = '" & strObject.ToString() & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AssignDate = dr.Item("ReviewedByUnitManager")
                    End While
                    If AssignDate = "04-Jul-1776" Then
                        AssignDate = OracleDate
                    Else
                        'AssignDate = AssignDate
                    End If

                    Dim tempUnit As String
                    If UserUnit = "---" Then
                        tempUnit = "0"
                    Else
                        tempUnit = UserUnit
                    End If

                    SQL = "Update AIRBRANCH.ISMPReportInformation set " & _
                    "strReviewingEngineer = '" & EngineerGCode & "', " & _
                    "datReviewedBYUnitManager = '" & AssignDate & "', " & _
                    "strReviewingUnit = '" & tempUnit & "', " & _
                    "numReviewingManager = '" & UserGCode & "', " & _
                    "strPreComplianceStatus = '" & PreCompliance & "' " & _
                    "where AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & strObject.ToString() & "'"

                    cmd = New OracleCommand(SQL, CurrentConnection)

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
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & OneStack2 & "' " & _
            "where strKEy = '002'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbOneStack3Runs.Checked = True Then
                OneStack3 = False
            Else
                OneStack3 = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & OneStack3 & "' " & _
            "where strKEy = '003'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbOneStack4Runs.Checked = True Then
                OneStack4 = False
            Else
                OneStack4 = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & OneStack4 & "' " & _
            "where strKEy = '004'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            If chbTwoStack.Checked = True Then
                TwoStackStandard = False
            Else
                TwoStackStandard = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & TwoStackStandard & "' " & _
            "where strKEy = '005'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If Me.chbTwoStackDRE.Checked = True Then
                TwoStackDRE = False
            Else
                TwoStackDRE = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & TwoStackDRE & "' " & _
            "where strKEy = '006'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbLoadingRack.Checked = True Then
                LoadingRack = False
            Else
                LoadingRack = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & LoadingRack & "' " & _
            "where strKEy = '007'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbTreatmentPonds.Checked = True Then
                PondTreatment = False
            Else
                PondTreatment = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & PondTreatment & "' " & _
            "where strKEy = '008'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbGasTests.Checked = True Then
                GasConc = False
            Else
                GasConc = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & GasConc & "' " & _
            "where strKEy = '009'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbFlare.Checked = True Then
                Flare = False
            Else
                Flare = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & Flare & "' " & _
            "where strKEy = '010'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbRATA.Checked = True Then
                Rata = False
            Else
                Rata = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & Rata & "' " & _
            "where strKEy = '011'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMemorandum.Checked = True Then
                MemoStandard = False
            Else
                MemoStandard = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & MemoStandard & "' " & _
            "where strKEy = '012'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMemorandumToFile.Checked = True Then
                MemoFile = False
            Else
                MemoFile = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & MemoFile & "' " & _
            "where strKEy = '013'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod9Multi.Checked = True Then
                Method9Multi = False
            Else
                Method9Multi = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & Method9Multi & "' " & _
            "where strKEy = '014'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod22.Checked = True Then
                Method22 = False
            Else
                Method22 = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & Method22 & "' " & _
            "where strKEy = '015'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbMethod9Single.Checked = True Then
                Method9Single = False
            Else
                Method9Single = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & Method9Single & "' " & _
            "where strKEy = '016'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbPEMS.Checked = True Then
                PEMS = False
            Else
                PEMS = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & PEMS & "' " & _
            "where strKEy = '017'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader


            If chbPTE.Checked = True Then
                PTE = False
            Else
                PTE = True
            End If
            SQL = "Update AIRBRANCH.ISMPDocumentType set " & _
            "strAFSPrint = '" & PTE & "' " & _
            "where strKEy = '018'"

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Update AIRBRANCH.ISMPFacilityAssignment set " & _
                    "strISMPUnit = 'H' "
                Else
                    SQL = "Update AIRBRANCH.ISMPFacilityAssignment set " & _
                    "strChemicalVOC = 'I' "
                End If
                Try


                    For Each strObject In lsbFacilities.Items
                        SQL2 = SQL & "where strAIRSNumber = '0413" & strObject.ToCharArray() & "' "
                        cmd = New OracleCommand(SQL2, CurrentConnection)
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
                DateBias = "datTestDateStart between '" & DTPStartDateFacility.Text & "' " & _
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbFacilityDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPStartDateFacility.Text & "' " & _
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbFacilityDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPStartDateFacility.Text & "' " & _
                "and '" & DTPEndDateFacility.Text & "'"
            End If
            If rdbStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
            End If

            'txtOpenFacility
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityClosed = dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays = dr.Item("Count")
            End While

            'Compliance Status Open
            'File Open 
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '01' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '02' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '03' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '04' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '05' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpen5 = dr.Item("Count")
            End While

            'Compliance Status Closed
            'File Open 
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '01' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '02' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '03' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '04' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strComplianceStatus = '05' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityClosed5 = dr.Item("Count")
            End While

            'Compliance Status for Days Open
            'File Open 
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and strComplianceStatus = '01' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays1 = dr.Item("Count")
            End While

            'For Information Purposes Only
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and strComplianceStatus = '02' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays2 = dr.Item("Count")
            End While

            'In Compliance
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and strComplianceStatus = '03' " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
            "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays3 = dr.Item("Count")
            End While

            'Indeterminate
            SQL = "Select count(*) as Count " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
           "where " & _
           "strDelete is NULL " & _
           "and strComplianceStatus = '04' " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
           "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
           "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
           "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
           "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
           "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
           "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                CSFacilityOpenDays4 = dr.Item("Count")
            End While

            'Not In Compliance
            SQL = "Select count(*) as Count " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
           "where " & _
           "strDelete is NULL " & _
           "and strComplianceStatus = '05' " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
           "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
           "and Upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) Like Upper('" & CityBias & "') " & _
           "and AIRBRANCH.ISMPMaster.strAIRSNumber Like '0413" & FacilityBias & "' " & _
           "and subStr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) Like '" & CountyBias & "' " & _
           "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpenFacility.Text & "' " & _
           "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

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
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
            End If

            If PanelChemVOC.Visible = True Then
                For Each strObject In clbEngineers1.CheckedItems
                    drEngineers = dtEngineers.Select("UserName = '" & strObject.ToString() & "'")
                    For Each row In drEngineers
                        EngineerGCode = row("numUserID")
                    Next

                    'txtOpenFacility
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where strClosed = 'False' " & _
                    "and strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpen += dr.Item("Count")
                    End While

                    'txtFacilityOpenDays
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where " & _
                    "strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen.Text & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpenDays += dr.Item("Count")
                    End While

                    'txtWitnessedTests
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where " & _
                    "strDelete is NULL " & _
                    "and (strWitnessingEngineer = '" & EngineerGCode & "' " & _
                    "or strWitnessingEngineer2 = '" & EngineerGCode & "') " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityWitnessed += dr.Item("count")
                    End While

                    'txtClosedFacility
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where strClosed = 'True' " & _
                    "and strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)

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
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where strClosed = 'False' " & _
                    "and strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpen2 += dr.Item("Count")
                    End While

                    'txtFacilityOpenDays
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where " & _
                    "strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen2.Text & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityOpenDays2 += dr.Item("Count")
                    End While

                    'txtWitnessedTests
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where " & _
                    "strDelete is NULL " & _
                    "and (strWitnessingEngineer = '" & EngineerGCode & "' " & _
                    "or strWitnessingEngineer2 = '" & EngineerGCode & "') " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityWitnessed2 += dr.Item("count")
                    End While

                    'txtClosedFacility
                    SQL = "Select count(*) as Count " & _
                    "from AIRBRANCH.ISMPReportInformation " & _
                    "where strClosed = 'True' " & _
                    "and strDelete is NULL " & _
                    "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                    "and " & DateBias & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FacilityClosed2 += dr.Item("Count")
                    End While
                Next
            End If

            'txtOpenFacility
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and strReviewingEngineer = '0' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpen3 += dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and strReviewingEngineer = '0' " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen3.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays3 += dr.Item("Count")
            End While

            'txtWitnessedTests
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and (strWitnessingEngineer <> '0' " & _
            "or strWitnessingEngineer2 <> '0') " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityWitnessed3 += dr.Item("count")
            End While

            'txtClosedFacility
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and strReviewingEngineer = '0' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityClosed3 += dr.Item("Count")
            End While

            'txtOpenFacility
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where strClosed = 'False' " & _
            "and strDelete is NULL " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpen4 += dr.Item("Count")
            End While

            'txtFacilityOpenDays
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and (to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) >= '" & txtDaysOpen4.Text & "' " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

            dr = cmd.ExecuteReader
            While dr.Read
                FacilityOpenDays4 += dr.Item("Count")
            End While

            'txtWitnessedTests
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where " & _
            "strDelete is NULL " & _
            "and (strWitnessingEngineer <> '0' " & _
            "or strWitnessingEngineer2 <> '0') " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityWitnessed4 += dr.Item("count")
            End While

            'txtClosedFacility
            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where strClosed = 'True' " & _
            "and strDelete is NULL " & _
            "and " & DateBias & " "

            cmd = New OracleCommand(SQL, CurrentConnection)

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
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Completed between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were: "
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were:"
            End If

            If EngineerGCode = "" Then

            Else
                ' Work to get other witnessed staff
                '                SQL = "select  count(*) as OtherWitnessed, ISMPWitnessingEng.strreferencenumber     " & _
                '"from AIRBranch.ISMPWitnessingEng,  Airbranch.ISMPReportInformation     " & _
                '"where AIRBranch.ISMPWitnessingEng.strreferencenumber  = Airbranch.ISMPReportInformation.strreferencenumber " & _
                '"and strDelete is null " & _
                '"and datCompleteDate between '01-Jan-10' and '01-May-12'  " & _
                '"and AIRBranch.ISMPWitnessingEng.strWitnessingEngineer = '329' " & _
                '"group by ISMPWitnessingEng.strreferencenumber "

                SQL = "select " & _
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                "case " & _
                "	when ReceivedByDate is NULL then 0  " & _
                "	Else ReceivedByDate " & _
                "End as ReceivedByDate,  " & _
                "Case  " & _
                "	when OpenByDate is Null then 0  " & _
                "	Else OpenByDate  " & _
                "End as OpenByDate,  " & _
                "Case  " & _
                "	WHEN CloseByDate is Null then 0  " & _
                "	Else CloseByDate " & _
                "End as CloseByDate,  " & _
                "Case  " & _
                "	when WitnessedByDate is Null then 0  " & _
                "	Else WitnessedByDate  " & _
                "End as WitnessedByDate, " & _
                "case  " & _
                "	when OpenWitnessedByDate is NULL then 0  " & _
                "	Else OpenWitnessedByDate  " & _
                "End as OpenWitnessedByDate,  " & _
                "case  " & _
                "	when CloseWitnessedByDate is NULL then 0  " & _
                "	Else CloseWitnessedByDate  " & _
                "End as CloseWitnessedByDate,  " & _
                "Case " & _
                "   when GreaterByDate is NUll then 0 " & _
                "   Else GreaterByDate " & _
                "End as GreaterByDate, " & _
                "case  " & _
                "	when OpenGreaterByDate is NULL then 0  " & _
                "	Else OpenGreaterByDate " & _
                "end as OpenGreaterByDate,    " & _
                "case  " & _
                "	When CloseGreaterByDate is NULL then 0  " & _
                "	Else CloseGreaterByDate  " & _
                "End as CloseGreaterByDate,  " & _
                "Case " & _
                "   when ComplianceByDate is NULL then 0 " & _
                "   Else ComplianceByDate " & _
                "End as ComplianceByDate, " & _
                "Case  " & _
                "	when OpenComplianceByDate is NULL then 0  " & _
                "	Else OpenComplianceByDate " & _
                "End as OpenComplianceByDate,  " & _
                "Case  " & _
                "	When CloseComplianceByDate is NULL then 0  " & _
                "	Else CloseComplianceByDate " & _
                "End as CloseComplianceByDate,  " & _
                "OtherWitnessed " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation,  " & _
                "(Select strReviewingEngineer,  count(*) as ReceivedByDate   " & _
                "from AIRBRANCH.ISMPReportInformation   " & _
                "where strDelete is NULL " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) ReceivedByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenByDate  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseByDate  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and StrDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as WitnessedByDate  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strWitnessingEngineer) WitnessedByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedByDate   " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                 "and " & DateBias & " " & _
                "group by strWitnessingEngineer) OpenWitnessedByDates,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as CloseWitnessedByDate   " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True' " & _
                "and " & DateBias & " " & _
                "group by strwitnessingEngineer) CloseWitnessedByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as GreaterByDate " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and datReceivedDate < Decode(strClosed, 'False', (trunc(sysdate) - 50), " & _
                "                                        'True', (-50 + datCompleteDate)) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) GreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenGreaterByDate " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenGreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseGreaterByDate " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseGreaterByDates,  " & _
                "(select strReviewingEngineer, " & _
                "count(*) as ComplianceByDate " & _
                "from AIRBRANCH.ISMPReportInformation " & _
                "where strComplianceStatus = '05' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) ComplianceByDates, " & _
                "(select strReviewingEngineer,   " & _
                "count(*) as OpenComplianceByDate  " & _
                "from AIRBRANCH.ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) OpenComplianceByDates,   " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseComplianceByDate  " & _
                "from AIRBRANCH.ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'True'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) CloseComplianceByDates,   " & _
                "(select  count(*) as OtherWitnessed " & _
                "from AIRBRANCH.ISMPWitnessingEng,  AIRBRANCH.ISMPReportInformation " & _
                "where AIRBRANCH.ISMPWitnessingEng.strreferencenumber  = AIRBRANCH.ISMPReportInformation.strreferencenumber  " & _
                "and strDelete is null  " & _
                "and " & DateBias & "  " & _
                "and AIRBranch.ISMPWitnessingEng.strWitnessingEngineer = '" & EngineerGCode & "')  OtherWitnesses  " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL2 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpenByDate " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysOpenByDate ASC "

                SQL3 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(datCompleteDate - datReceivedDate) as DaysCloseByDate " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysCloseByDate ASC "

                SQL4 = "Select " & _
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                "case  " & _
                "	when ReceivedTotal is NULL then 0  " & _
                "	Else ReceivedTotal  " & _
                "end as ReceivedTotal,  " & _
                "case  " & _
                "	when OpenTotal is NULL then 0  " & _
                "	Else OpenTotal  " & _
                "End as OpenTotal,  " & _
                "Case  " & _
                "	when OpenWitnessedTotal is NULL then 0  " & _
                "	Else OpenWitnessedTotal  " & _
                "End as OpenWitnessedTotal,  " & _
                "Case  " & _
                "	When OpenComplianceTotal is NULL then 0  " & _
                "	Else OpenComplianceTotal  " & _
                "End as OpenComplianceTotal,  " & _
                "Case  " & _
                "	when CloseTotal is NULL then 0  " & _
                "	else CloseTotal  " & _
                "End as CloseTotal,  " & _
                "Case  " & _
                "	when ClosedWitnessedTotal is NULL then 0  " & _
                "	Else ClosedWitnessedTotal  " & _
                "End as ClosedWitnessedTotal,  " & _
                "Case  " & _
                "	when ClosedComplianceTotal is NULL then 0  " & _
                "	Else ClosedComplianceTotal " & _
                "End as ClosedComplianceTotal,  " & _
                "Case  " & _
                "when OpenGreaterTotal is NULL then 0   " & _
                "Else OpenGreaterTotal   " & _
                "End as OpenGreaterTotal, " & _
                "Case  " & _
                "when ClosedGreaterTotal is NULL then 0   " & _
                "Else ClosedGreaterTotal   " & _
                "End as ClosedGreaterTotal   " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation, " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as ReceivedTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "Group by strReviewingEngineer) ReceivedTotals,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenTotal " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is NULL  " & _
                "Group by strReviewingEngineer) OpenTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is Null " & _
                "group by strWitnessingEngineer) OpenWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenComplianceTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) OpenComplianceTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and strDelete is NULL " & _
                "Group by strReviewingEngineer) CloseTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as ClosedWitnessedTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strClosed = 'True' " & _
                "and strDelete is NULL  " & _
                "group by strWitnessingEngineer) ClosedWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as ClosedComplianceTotal  " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) ClosedComplianceTotals, " & _
                "(select strReviewingEngineer, count(*) as OpenGreaterTotal " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "Group by strReviewingEngineer) OpenGreaterTotals, " & _
                "(select strReviewingEngineer, count(*) as ClosedGreaterTotal " & _
                "from AIRBRANCH.ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate)  " & _
                "Group by strReviewingEngineer) ClosedGreaterTotals " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer (+) " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer (+)   " & _
                "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL5 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpen " & _
                "from AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysOpen ASC "

                SQL6 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(datCompleteDate -datReceivedDate) as DaysClosed " & _
                "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
                "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysClosed ASC "

                cmd = New OracleCommand(SQL, CurrentConnection)
                cmd2 = New OracleCommand(SQL2, CurrentConnection)
                cmd3 = New OracleCommand(SQL3, CurrentConnection)
                cmd4 = New OracleCommand(SQL4, CurrentConnection)
                cmd5 = New OracleCommand(SQL5, CurrentConnection)
                cmd6 = New OracleCommand(SQL6, CurrentConnection)

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

            Statement = Statement & _
            "For the Staff member: " & Staff & vbCrLf & _
            vbTab & DateStatement & vbCrLf & vbCrLf & _
            "1. " & ReceivedByDate & " Test Reports Received " & vbCrLf & _
            "2. " & OpenByDate & " of these " & ReceivedByDate & " Test Reports are currently open" & vbCrLf & _
            "3. " & ClosedByDate & " of these " & ReceivedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "4. " & WitnessedByDate & " of these " & ReceivedByDate & " Test Reports were witnessed by " & Staff & vbCrLf & _
            "5. " & OpenWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are still open " & vbCrLf & _
            "6. " & CloseWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "7. " & GreaterByDate & " of these " & ReceivedByDate & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "8. " & OpenGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more than 50-days are still open " & vbCrLf & _
            "9. " & CloseGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more then 50-days are currently closed " & vbCrLf & vbCrLf & _
            "10. " & ComplianceByDate & " of these " & ReceivedByDate & " Test Reports were out of compliance" & vbCrLf & _
            "11. " & OpenComplianceByDate & " of these " & ComplianceByDate & " Test Reports are still open " & vbCrLf & _
            "12. " & CloseComplianceByDate & " of these " & ComplianceByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "13. The median time taken to complete those " & ClosedByDate & " Closed Test Reports was " & CloseMedianByDate & "-days" & vbCrLf & _
            "14. The 80% Percentile Time taken to complete those " & ClosedByDate & " Closed Test Reports was " & ClosePercentileByDate & "-days" & vbCrLf & _
            "15. The median time of the " & OpenByDate & " Open Test Reports is " & OpenMedianByDate & "-days" & vbCrLf & _
            "16. The 80% Percentile Time of the " & OpenByDate & " Open Test Reports is " & OpenPercentileByDate & "-days" & vbCrLf & vbCrLf & _
            "17. Overall " & Staff & " has received " & ReceivedTotal & " Test Reports" & vbCrLf & vbCrLf & _
            "18. " & OpenTotal & " of " & ReceivedTotal & " Test Reports are currently open" & vbCrLf & _
            "19. " & OpenWitnessedTotal & " of these " & OpenTotal & " Test Reports have been witnessed" & vbCrLf & _
            "20. " & OpenComplianceTotal & " of these " & OpenTotal & " Test Reports are currently out of compliance " & vbCrLf & _
            "21. " & OpenGreaterTotal & " of these " & OpenTotal & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "22. The median time of the " & OpenTotal & " Open Test Reports is " & OpenMedianTotal & "-days" & vbCrLf & _
            "23. The 80% Percentile Time of the " & OpenTotal & " Open Test Reports is " & PercentileOpenTotalDay & "-days" & vbCrLf & vbCrLf & _
            "24. " & ClosedTotal & " of " & ReceivedTotal & " Test Reports are currently closed " & vbCrLf & _
            "25. " & ClosedWitnessedTotal & " of these " & ClosedTotal & " Test Reports have been witnessed" & vbCrLf & _
            "26. " & ClosedComplianceTotal & " of these " & ClosedTotal & " Test Reports are out of compliance " & vbCrLf & _
            "27. " & ClosedGreaterTotal & " of these " & ClosedTotal & " Test Reports were open for more than 50-days" & vbCrLf & _
            "28. The median time of the " & ClosedTotal & " Closed Test Reports was " & ClosedMedianTotal & "-days" & vbCrLf & _
            "29. The 80% Percentile Time of the " & ClosedTotal & " Closed Test Reports was " & PercentileClosedTotalDay & "-days" & _
            vbCrLf & vbCrLf & _
            "30. Additionally " & OtherWitnessed & " Test were witnessed but reviewed by another staff member. " & vbCrLf & vbCrLf & _
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
    'Sub AddFacilityName()
    '    Try

    '        If txtAIRSNumber.Text <> "" Then
    '            SQL = "Select strFacilityName from AIRBRANCH.APBFacilityInformation " & _
    '            "where strAirsNumber = '0413" & txtAIRSNumber.Text & "'"

    '            cmd = New OracleCommand(SQL, Conn)
    '            If Conn.State = ConnectionState.Closed Then
    '                Conn.Open()
    '            End If
    '            dr = cmd.ExecuteReader
    '            While dr.Read
    '                txtFacility.Text = dr.Item("strFacilityName")
    '            End While
    '            If FacilityLookUpTool Is Nothing Then
    '            Else
    '                FacilityLookUpTool.Focus()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try

    'End Sub
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
                DateBias = "datTestDateStart between '" & DTPEngineerTestReportStart.Text & "' " & _
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPEngineerTestReportStart.Text & "' " & _
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPEngineerTestReportStart.Text & "' " & _
                "and '" & DTPEngineerTestReportEnd.Text & "'"
            End If
            If rdbEngineerTestReportAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
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

            SQL = "Select AIRBRANCH.ISMPReportInformation.strReferenceNumber, strFacilityName, " & _
            "substr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5) as AIRSNumber, strClosed, " & _
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as ForDatTestDateStart, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as ForDatReceivedDate, " & _
            "to_char(datCompleteDate, 'dd-Mon-yyyy') as ForDatCompleteDate, " & _
            "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer  " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
            "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
            "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer, " & _
            "(Select (strLastName|| ', ' ||strFirstName) as WitnessingEngineer " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
            "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer " & _
            "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as WitnessingEngineer " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and " & DateBias & " " & Engineer & " "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dsEngineerGrid = New DataSet

            daEngineerGrid = New OracleDataAdapter(SQL, CurrentConnection)

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

            SQL = "Select " & _
            "(select (strLastName|| ', ' ||strFirstName) as ReviewingEngineer " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
            "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer " & _
            "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber) as ReviewingEngineer, " & _
            "AIRBRANCH.ISMPReportInformation.strReferenceNumber, strFacilityName, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as ForDatReceivedDate, " & _
            "(to_date('" & OracleDate & "', 'dd-Mon-yyyy') - datReceivedDate) as Days " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation " & _
            "where " & _
            "AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber " & _
            "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and strClosed = 'False' " & _
            Engineer & _
            "Order by strReviewingEngineer "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "Select count(*) as Count from AIRBRANCH.ISMPReportInformation " & _
            "where datReceivedDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strDelete is NULL"
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestReceived = dr.Item("Count")
            End While

            'Tests Completed in Date Range 
            SQL = "Select count(*) as Count from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strClosed = 'True' and strDelete is NULL "
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestCompleted = dr.Item("Count")
            End While

            'Tests Witnessed in Date Range
            SQL = "Select Count(*) as Count from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strDelete is NULL and (strWitnessingEngineer <> '0' or strWitnessingEngineer2 <> '0') "
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                TestWitnessed = dr.Item("Count")
            End While

            'Tests out of compliance 
            SQL = "Select Count(*) as Count from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strDelete is NULL and strComplianceStatus = '05' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                OutofCompliance = dr.Item("count")
            End While

            'Test Median 
            SQL = "Select (datCompleteDate - datReceivedDate) as diff from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strDelete is NULL " & _
            "and strClosed = 'True' order by diff desc"
            cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "Select AIRBRANCH.ISMPReportInformation.strReferenceNumber, strEmissionSource, strPollutantDescription, " & _
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as fordatTestDateStart, to_char(datTestDateEnd, 'dd-Mon-yyyy') as fordatTestDateEnd, " & _
            "substr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5) as AIRSNumber, strFacilityName, strFacilityCity, strFacilityState, " & _
            "AIRBRANCH.ISMPReportType.strReportType " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation, " & _
            "AIRBRANCH.ISMPReportType " & _
            "where strDelete is NULL and strComplianceStatus = '05' " & _
            "and datCompleteDate between '" & DTPMonthlyStart.Text & "' and '" & DTPMonthlyEnd.Text & "' " & _
            "and strPollutantCode = strPOllutant " & _
            "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.ISMPMaster.strAIRSNumber " & _
            "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey "

            cmd = New OracleCommand(SQL, CurrentConnection)
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

                Report = Report & CompanyName & Environment.NewLine & CompanyLocation & Environment.NewLine & _
                SourceTested & Environment.NewLine & " " & Environment.NewLine & ReportType & Environment.NewLine & _
                PollutantDetermined & Environment.NewLine & TestDate & Environment.NewLine & Refnum & Environment.NewLine & _
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

                SQL = "select max(FileId) as ID " & _
                "from AIRBRANCH.ISMPTestReportAids "
                cmd = New OracleCommand(SQL, CurrentConnection)
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

                Dim da As OracleDataAdapter
                Dim ds As DataSet
                Dim Fs As FileStream = New FileStream(PathName, FileMode.Open, FileAccess.Read)
                Dim DocData As Byte()
                ReDim DocData(Fs.Length)
                Fs.Read(DocData, 0, System.Convert.ToInt32(Fs.Length))
                Fs.Close()

                SQL = "Select * " & _
                "From AIRBRANCH.ISMPTestReportAIDS " & _
                "where FileID = '" & IDnumber & "' "

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da = New OracleDataAdapter(SQL, CurrentConnection)
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

                SQL = "Delete AIRBRANCH.ISMPTestReportAids " & _
                "where FileID = '" & FileID & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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

            SQL = "Select " & _
             "distinct(Case " & _
             "when strFirstName = ' ' then '-Unassigned' " & _
             "Else strFirstName " & _
             "End) as Staff, " & _
             "Case " & _
             "    When OpenReports is Null then 0 " & _
             "    Else OpenReports " & _
             "End as OpenReports, " & _
             "Case " & _
             "	 When ClosedReports is Null then 0 " & _
             "    Else ClosedReports " & _
             "End as ClosedReports, " & _
             "Case " & _
             "    When OpenFiftys is Null then 0 " & _
             "    Else OpenFiftys " & _
             "End as OpenFiftys " & _
             "From (SELECT AIRBRANCH.EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenReports " & _
             "    FROM AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "    WHERE (AIRBRANCH.ISMPReportInformation.STRCLOSED = 'False' ) " & _
             "    and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer " & _
             "Group by strfirstname) OpenReport, " & _
             "(SELECT AIRBRANCH.EPDUserProfiles.STRFIRSTNAME as Engineer, Count(*) as ClosedReports " & _
             "    FROM AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "    WHERE (AIRBRANCH.ISMPReportInformation.STRCLOSED = 'True' ) " & _
             "    and AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer " & _
             "    and datCompleteDate Between Trunc(sysdate) - 60 and Trunc(sysdate) " & _
             "Group by strfirstname) ClosedReport, " & _
             "(SELECT AIRBRANCH.EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenFiftys " & _
             "    FROM AIRBRANCH.EPDUSerProfiles, AIRBRANCH.ISMPReportInformation " & _
             "    WHERE (AIRBRANCH.ISMPReportInformation.STRCLOSED = 'False' ) " & _
             "    and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer " & _
             "    and datReceivedDate <= (Trunc(SysDate) - 50) " & _
             "Group by strfirstname) OLdOpen, " & _
             "AIRBRANCH.EPDUserProfiles " & _
             "where strFirstname = OpenReport.Engineer (+) " & _
             "and strFirstName = ClosedReport.Engineer (+) " & _
             "and strFirstName = OldOpen.Engineer (+) " & _
             "and (OpenReports > '0' or ClosedReports > '0'  or OpenFiftys > '0') " & _
             "Order by Staff "

            dsSummaryReport = New DataSet

            daSummaryReport = New OracleDataAdapter(SQL, CurrentConnection)

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

                WordText = vbTab & vbTab & vbTab & vbTab & vbTab & "ISMP" & _
                 vbCrLf & line & vbCrLf & "Source Test Summary" & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Print Date: " & OracleDate & _
                  vbCrLf & line & vbCrLf & "Staff" & vbTab & vbTab & "# of Open" & vbTab & vbTab & "Reports Open" & vbTab & vbTab & "Reports Close" & _
                  vbCrLf & vbTab & vbTab & "Reports" & vbTab & vbTab & ">50 days" & vbTab & vbTab & "Last 60 days" & _
                  vbCrLf & line & _
                  vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|" & vbTab & "|"

                For j = 0 To i - 1
                    If dgrTestSummary.Item(j, 0).length > 6 Then
                        WordText = WordText & _
                        vbCrLf & dgrTestSummary.Item(j, 0) & vbTab & dgrTestSummary.Item(j, 1) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 2) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 3) & _
                        vbCrLf & line
                    Else
                        WordText = WordText & _
                        vbCrLf & dgrTestSummary.Item(j, 0) & vbTab & vbTab & dgrTestSummary.Item(j, 1) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 2) & vbTab & vbTab & vbTab & dgrTestSummary.Item(j, 3) & _
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

                    SQL = "Select " & _
                    "FileId, FileTitle, ISMPBlob " & _
                    "from AIRBRANCH.ISMPTestReportAids " & _
                    "Where FileID = '" & FileID & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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


            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "strISMPUnit, strISMPReviewer, datISMPReviewDate,  " & _
            "strISMPComments,  " & _
            "substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5) as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityinformation.strFacilityName " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.SSPPApplicationMaster,  " & _
            "AIRBRANCH.SSPPApplicationTracking, AIRBRANCH.SSPPApplicationData " & _
            "where datISMPReviewDate between '" & DTPAppStartDate.Text & "' and '" & DTPAppEndDate.Text & "'  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber " & _
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSPPApplicationMaster.strAIRSNumber "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            SQL = "select " & _
            "distinct(strLastName|| ', ' ||strFirstName) as Engineer,  " & _
            "strUnitDesc, totalreceived,  " & _
            "ReceivedCount,  " & _
            "round((ReceivedCount/TotalReceived)*100, 2) as ProgramPercent,   " & _
            "case when numUnit = '13' then round((ReceivedCount/ComUnitTotal)*100, 2)  " & _
            " when numUnit = '12' then round((ReceivedCount/ChemUnitTotal)*100, 2)  " & _
            "End UnitPercent,  " & _
            "ComUnitTotal, ChemUnitTotal, " & _
            "MedDays,  " & _
            "PercentDays,  " & _
            "(Witness1.witcount + witness2.witcount + Witness3.witcount) as Witnessed " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles,  " & _
            "AIRBRANCH.LookUpEPDUnits, " & _
            "(select count(*) as TotalReceived " & _
            "from AIRBRANCH.ISMPReportInformation  " & _
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null) " & _
            "and strReviewingEngineer <> '0' " & _
            "and strClosed = 'True') TotalReviewed,  " & _
            "(select strReviewingEngineer, Count(*) as ReceivedCount " & _
            "from AIRBRANCH.ISMPReportInformation   " & _
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete is Null or strDelete <> 'True') " & _
            "and strReviewingEngineer <> '0'  " & _
            "and strClosed = 'True'  " & _
            "group by strReviewingEngineer) TotalRec,  " & _
            "(select count(*) as ComUnitTotal  " & _
            "from AIRBRANCH.ISMPReportInformation,   " & _
            "(select numUserID   " & _
            "from AIRBRANCH.EPDUserProfiles   " & _
            "where numProgram = '3'  " & _
            "and numUnit = '13') ComUnit  " & _
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)   " & _
            "and strClosed = 'True'  " & _
            "and strReviewingEngineer  = ComUnit.numUserID) ComTotal,  " & _
            "(select count(*) as ChemUnitTotal  " & _
            "from AIRBRANCH.ISMPReportInformation,   " & _
            "(select numUserID   " & _
            "from AIRBRANCH.EPDUserProfiles   " & _
            "where numProgram = '3'   " & _
            "and numUnit = '12') ChemUnit  " & _
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)   " & _
            "and strClosed = 'True'  " & _
            "and strReviewingEngineer  = ChemUnit.numUserID) ChemTotal,  " & _
            "(select strReviewingEngineer,  " & _
            "Median(dayin) as MedDays    " & _
            "from  " & _
            "(select  " & _
            "strReviewingEngineer,  " & _
            "case  " & _
            "when strClosed = 'True' then (datCompleteDate - datReceivedDate)  " & _
            "when strClosed = 'False' then (round(sysdate, 'DDD') - datReceivedDate) " & _
            "END DayIn " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null) " & _
            "and strClosed = 'True'  " & _
            "and strReviewingEngineer <> '0') SubTable  " & _
            "group by strReviewingEngineer) MedianTotal,  " & _
            "(select strReviewingEngineer,  " & _
            "Percentile_cont(0.8) within Group(Order by DaysIn) as percentDays  " & _
            "from  " & _
            "(select  " & _
            "strReviewingEngineer,  " & _
            "case  " & _
            "when strClosed = 'True' then (datCompleteDate - datReceivedDate)  " & _
            "when strClosed = 'False' then (round(sysdate, 'DDD') - datReceivedDate) " & _
            "END DaysIn " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null) " & _
            "and strReviewingEngineer <> '0'  " & _
            "and strClosed = 'True')  " & _
            "group by strReviewingEngineer) PercentDays,  " & _
            "(select AIRBRANCH.ISMPReportInformation.strWitnessingEngineer,  " & _
            "count(*) as WitCount " & _
            "from AIRBRANCH.ISMPReportInformation   " & _
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)  " & _
            "and AIRBRANCH.ISMPReportInformation.strWitnessingEngineer <> '0' " & _
            "and strClosed = 'True'  " & _
            "group by AIRBRANCH.ISMPReportInformation.strWitnessingEngineer) Witness1,  " & _
            "(select AIRBRANCH.ISMPReportInformation.strWitnessingEngineer2,  " & _
            "count(*) as WitCount " & _
            "from AIRBRANCH.ISMPReportInformation   " & _
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)  " & _
            "and AIRBRANCH.ISMPReportInformation.strWitnessingEngineer2 <> '0' " & _
            "and strClosed = 'True'  " & _
            "group by AIRBRANCH.ISMPReportInformation.strWitnessingEngineer2) Witness2,  " & _
            "(select  AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer,  " & _
            "count(*) as WitCount " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.ISMPWitnessingEng    " & _
            "where datcompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)  " & _
            "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = AIRBRANCH.ISMPWitnessingEng.strReferenceNumber   " & _
            "and strClosed = 'True'  " & _
            "group by AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer) Witness3  " & _
            "where AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
            "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode " & _
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "' " & _
            "and (strDelete <> 'True' or strDelete is Null)  " & _
            "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer <> '0'  " & _
            "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = TotalRec.strReviewingEngineer (+)  " & _
            "and AIRBRANCH.ISMPREportINformation.strReviewingEngineer = MedianTotal.strReviewingEngineer (+) " & _
            "and AIRBRANCH.ISMPREportINformation.strReviewingEngineer = PercentDays.strReviewingEngineer (+) " & _
            "and AIRBRANCH.ISMPREportINformation.strReviewingEngineer = Witness1.strWitnessingEngineer (+)  " & _
            "and AIRBRANCH.ISMPREportINformation.strReviewingEngineer = Witness2.strWitnessingEngineer2 (+) " & _
            "and AIRBRANCH.ISMPREportINformation.strReviewingEngineer = Witness3.strWitnessingEngineer (+)  " & _
            "order by strUnitDesc, Engineer "

            dsUnitStats = New DataSet
            daUnitStats = New OracleDataAdapter(SQL, CurrentConnection)
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

            SQL = "Select strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as StrAIRSNumber, " & _
            "CASE " & _
            "when strISMPUnit = 'H' then 'Chemical and VOC' " & _
            "when strISMPUnit = 'I' then 'Combustion and Mineral' " & _
            "ELSE 'Unassigned' " & _
            "END as UnitAssigned " & _
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.ISMPFacilityAssignment " & _
            "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.ISMPFacilityAssignment.strAIRSNumber " & _
            "order by strAIRSNumber "

            cmd = New OracleCommand(SQL, CurrentConnection)

            daFacilityList = New OracleDataAdapter(SQL, CurrentConnection)

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
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm("ISMPTestReports", id)
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
            SQL = "select strReferenceNumber, " & _
            "(strLastName|| ', ' ||strFirstName) as Engineer,  " & _
            "case  " & _
            "when datTestDateStart = '04-Jul-1776' then  null  " & _
            "else to_char(datTestDateStart, 'dd-Mon-yyyy') " & _
            "End datTestDateStart,  " & _
            "case  " & _
            "when datReceivedDate = '04-Jul-1776' then Null  " & _
            "else to_char(datReceivedDate, 'dd-Mon-yyyy')  " & _
            "End datReceiveddate,  " & _
            "Case  " & _
            "when datCompleteDate = '04-Jul-1776' then Null  " & _
            "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " & _
            "end datCompleteDate  " & _
            "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles    " & _
            "where AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)  " & _
            "and strReviewingEngineer <> '0'  " & _
            "and strClosed = 'True' "

            dsUnitStats = New DataSet
            daUnitStats = New OracleDataAdapter(SQL, CurrentConnection)
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
            SQL = "select strReferenceNumber, " & _
            "(strLastName|| ', ' ||strFirstName) as Engineer,  " & _
            "case  " & _
            "when datTestDateStart = '04-Jul-1776' then Null  " & _
            "else to_char(datTestDateStart, 'dd-Mon-yyyy')  " & _
            "end datTestDateStart,  " & _
            "case  " & _
            "when datReceivedDate = '04-Jul-1776' then Null  " & _
            "else to_Char(datReceivedDate, 'dd-Mon-yyyy')  " & _
            "End datReceivedDate,  " & _
            "case  " & _
            "when datCompleteDate = '04-Jul-1776' then Null  " & _
            "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " & _
            "End datCompleteDate  " & _
            "from AIRBRANCH.ISMPReportInformation,  AIRBRANCH.EPDUserProfiles,  " & _
            "(select numUserID    " & _
            "from AIRBRANCH.EPDUserProfiles " & _
            "where numProgram = '3'  " & _
            "and numUnit = '12') ChemUnit   " & _
            "where AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUSerProfiles.numUSerID   " & _
            "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
            "and (strDelete <> 'True' or strDelete is Null)    " & _
            "and strClosed = 'True'   " & _
            "and strReviewingEngineer  = ChemUnit.numUserID "

            dsUnitStats = New DataSet
            daUnitStats = New OracleDataAdapter(SQL, CurrentConnection)
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
            SQL = "select strReferenceNumber, " & _
           "(strLastName|| ', ' ||strFirstName) as Engineer,  " & _
           "case  " & _
           "when datTestDateStart = '04-Jul-1776' then Null  " & _
           "else to_char(datTestDateStart, 'dd-Mon-yyyy')  " & _
           "end datTestDateStart,  " & _
           "case  " & _
           "when datReceivedDate = '04-Jul-1776' then Null  " & _
           "else to_Char(datReceivedDate, 'dd-Mon-yyyy')  " & _
           "End datReceivedDate,  " & _
           "case  " & _
           "when datCompleteDate = '04-Jul-1776' then Null  " & _
           "else to_char(datCompleteDate, 'dd-Mon-yyyy')  " & _
           "End datCompleteDate  " & _
           "from AIRBRANCH.ISMPReportInformation,  AIRBRANCH.EPDUserProfiles,  " & _
           "(select numUserID " & _
           "from AIRBRANCH.EPDUserProfiles     " & _
           "where numProgram = '3' " & _
           "and numUnit = '13') ComUnit   " & _
           "where AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID   " & _
           "and datCompleteDate between '" & DTPUnitStatsStartDate.Text & "' and '" & DTPUnitStatsEndDate.Text & "'  " & _
           "and (strDelete <> 'True' or strDelete is Null)    " & _
           "and strClosed = 'True'   " & _
           "and strReviewingEngineer  = ComUnit.numUserID "

            dsUnitStats = New DataSet
            daUnitStats = New OracleDataAdapter(SQL, CurrentConnection)
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
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm("ISMPTestReports", id)
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
                SQL = "Select " & _
                "strMethodDesc " & _
                "from AIRBRANCH.LookUpISMPMethods " & _
                "where strMethodCode = '" & txtMethodCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "Select " & _
                "strMethodCode " & _
                "From AIRBRANCH.LookUpISMPMethods " & _
                "where substr(strMethodDesc, 1, instr(strMethodDesc,'-')-2)  = 'Method " & Replace(txtMethodNumber.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist = True Then
                    temp = dr.Item("strMethodCode")
                    dr.Close()
                    If temp = txtMethodCode.Text Then
                        SQL = "Update AIRBRANCH.LookUpISMPMethods set " & _
                        "strMethodDesc = 'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "' " & _
                        "where strMethodCode = '" & Replace(txtMethodCode.Text, "'", "''") & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        SQL = "Select (max(strMethodCode) + 1) as MethodCode " & _
                        "from AIRBRANCH.LookUpISMPMethods "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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

                        SQL = "Insert into AIRBRANCH.LookUpISMPMethods " & _
                        "values " & _
                        "('" & temp & "', " & _
                        "'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "') "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                Else
                    dr.Close()
                    SQL = "Select (max(strMethodCode) + 1) as MethodCode " & _
                    "from AIRBRANCH.LookUpISMPMethods "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Insert into AIRBRANCH.LookUpISMPMethods " & _
                    "values " & _
                    "('" & temp & "', " & _
                    "'Method " & Replace(txtMethodNumber.Text, "'", "''") & " - " & Replace(txtMethodDescription.Text, "'", "''") & "') "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "Select " & _
                "strReferenceNumber " & _
                "from AIRBRANCH.ISMPMaster " & _
                "where strReferenceNumber = '" & RefNum & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "Select " & _
                "strAIRSNumber " & _
                "from AIRBRANCH.APBMasterAIRS " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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

                SQL = "Insert into AIRBRANCH.ISMPMaster " & _
                "values " & _
                "('" & RefNum & "', '0413" & AIRSNumber & "', " & _
                "'" & UserGCode & "', '" & OracleDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Insert into AIRBRANCH.ISMPReportInformation " & _
                "values " & _
                "('" & RefNum & "', '00001', " & _
                "'N/A', '001', " & _
                "'001', 'N/A', " & _
                "'00001', '0', " & _
                "'0', '0', " & _
                "'0', '" & DateReceived & "', " & _
                "'0', '04-Jul-1776', " & _
                "'04-Jul-1776', '" & DateReceived & "', " & _
                "'" & DateCompleted & "', 'N/A', " & _
                "'False', '" & Replace(Commissioner, "'", "''") & "', " & _
                "'" & Replace(Director, "'", "''") & "', '" & Replace(ProgramManager, "'", "''") & "', " & _
                "'01', '0', " & _
                "'" & UserGCode & "', '" & OracleDate & "', " & _
                "'N/A', '', " & _
                "'', '', " & _
                "'', '') "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "Select " & _
                "strReferenceNumber " & _
                "from AIRBRANCH.ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.ISMPReportInformation set " & _
                    "strClosed = 'True' " & _
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "Select " & _
                "strReferenceNumber " & _
                "from AIRBRANCH.ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update AIRBRANCH.ISMPReportInformation set " & _
                    "strClosed = 'False' " & _
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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
