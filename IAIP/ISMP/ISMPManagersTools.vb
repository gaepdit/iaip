Imports System.Data.SqlClient

Public Class ISMPManagersTools
    Dim query As String
    Dim dtEngineer As DataTable
    Dim dtTestReportAssignments As DataTable
    Dim dtEngineerGrid As DataTable
    Dim dtSummaryReport As DataTable
    Dim dtUnitStats As DataTable
    Dim dtMethods As DataTable

#Region "Page Load"

    Private Sub ISMPManagersTools_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            ShowCorrectTabs()
            LoadComboBoxDataSets()
            LoadComboBoxes()
            LoadTestReportAssignmentDataSet()
            LoadLVTestReportAssignment()
            FormatEngineerTestReportGrid()
            FormatTestSummaryGrid()

            DTPUnitStatsStartDate.Value = Today.AddDays(-30)
            DTPUnitStatsEndDate.Value = Today
            DTPMonthlyStart.Value = Today.AddDays(-30)
            DTPMonthlyEnd.Value = Today
            DTPEngineerTestReportStart.Text = Today.AddDays(-30)
            DTPEngineerTestReportEnd.Value = Today

            LoadMethods()
            dtpAddTestReportDateReceived.Value = Today
            DTPAddTestReportDateCompleted.Value = Today
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ShowCorrectTabs()
        Try

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
                TCManagersTools.TabPages.Add(TPMiscTools)
                TCMiscTools.TabPages.Add(TPMethods)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub LoadComboBoxDataSets()
        Try

            query = "select " &
            "concat(strLastName, ', ' ,strFirstName) as UserName,  " &
            "numUserID, numUnit  " &
            "from EPDUSerProfiles, LookUpEPDUnits  " &
            "where EPDUSerProfiles.numUnit = LookUpEPDUnits.numUnitCode  " &
            "and numProgram = '3'  " &
            "and numUnit <> '14'  " &
            "and numEmployeeStatus = '1' " &
            "and numUserID <> '0' " &
            "order by strlastname"

            dtEngineer = DB.GetDataTable(query)
            dtEngineer.TableName = "Engineers"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadComboBoxes()
        Try

            cboEngineer.Items.Add(" ")

            For Each row As DataRow In dtEngineer.Rows
                cboEngineer.Items.Add(row("UserName"))
                clbEngineersList2.Items.Add(row("UserName"))
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,   " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart,  " &
                "strEmissionSource,   " &
                "(Select strPollutantDescription   " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant   " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,   " &
                "(select concat(strLastName, ', ' ,strFirstName) as ReviewingEngineer   " &
                "from EPDUserProfiles, ISMPReportInformation   " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer   " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer   " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation   " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ( strclosed = 'False' or strClosed is null ) " &
                "and ISMPReportInformation.strReviewingEngineer = '0'  " &
                "and ISMPReportInformation.strDelete is NULL "

                dtTestReportAssignments = DB.GetDataTable(query)
                dtTestReportAssignments.TableName = "TestReportAssignment"
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadLVTestReportAssignment()
        Try

            LVTestReportAssignment.View = View.Details
            LVTestReportAssignment.AllowColumnReorder = True
            LVTestReportAssignment.CheckBoxes = True
            LVTestReportAssignment.GridLines = True
            LVTestReportAssignment.FullRowSelect = True

            Dim drtestReportAssignment As DataRow()

            Dim row As DataRow

            drtestReportAssignment = dtTestReportAssignments.Select()

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
    Private Sub FormatEngineerTestReportGrid()
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
    Private Sub FormatTestSummaryGrid()
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
    Private Sub LoadMethods()
        Try
            query = "Select " &
            "strMethodCode, strMethodDesc " &
            "From LookUpISMPMethods " &
            "order by strMethodCode "

            dtMethods = DB.GetDataTable(query)
            dtMethods.TableName = "Methods"

            dgvMethods.DataSource = dtMethods

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

    Private Sub LoadAllNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' ,strFirstName) as ReviewingEngineer  " &
                "from EPDUSerProfiles, ISMPReportInformation  " &
                "where EPDUSerProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                  "and ISMPReportInformation.strDelete is NULL"

                dtTestReportAssignments = DB.GetDataTable(query)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadUnassignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strReviewingEngineer = '0' " &
                  "and ISMPReportInformation.strDelete is NULL"

                dtTestReportAssignments = DB.GetDataTable(query)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadAssignedNoUnitTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                  "and ISMPReportInformation.strDelete is NULL"

                dtTestReportAssignments = DB.GetDataTable(query)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadDeletedTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strDelete is not NULL"

                dtTestReportAssignments = DB.GetDataTable(query)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadByTestReportTestReportAssignmentDataSet(ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                 "and ISMPReportInformation.strReviewingEngineer = '0' " &
                 "and ISMPReportInformation.strDocumentType = @ReportType " &
                "and ISMPReportInformation.strDelete is NULL"

                Dim p As New SqlParameter("@ReportType", ReportType)

                dtTestReportAssignments = DB.GetDataTable(query, p)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadByTestReportAllTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPReportInformation.strDocumentType = @ReportType " &
                "and ISMPReportInformation.strReviewingEngineer <> '0' " &
                "and ISMPReportInformation.strDelete is NULL"

                Dim p As New SqlParameter("@ReportType", ReportType)

                dtTestReportAssignments = DB.GetDataTable(query, p)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
    Private Sub LoadByTestReportAssignedTestReportAssignmentDataSet(ByRef ReportType As String)
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                    "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                    "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                    "strEmissionSource,  " &
                    "(Select strPollutantDescription  " &
                    "from LookUPPollutants, ISMPReportInformation  " &
                    "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                    "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                    "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                    "from EPDUserProfiles, ISMPReportInformation  " &
                    "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                    "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                    "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                    "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                    "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                    "and ISMPReportInformation.strDocumentType = @ReportType " &
                    "and ISMPReportInformation.strDelete is NULL"

                Dim p As New SqlParameter("@ReportType", ReportType)

                dtTestReportAssignments = DB.GetDataTable(query, p)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LoadByAIRSNumberTestReportAssignmentDataSet()
        Try

            If AccountFormAccess(17, 3) = "1" Then
                query = "Select " &
                "ISMPMaster.strReferenceNumber, ISMPMaster.strAIRSNumber, strFacilityName,  " &
                "format(DATTestDateStart, 'MMMM d, yyyy') as ForTestDateStart, " &
                "strEmissionSource,  " &
                "(Select strPollutantDescription  " &
                "from LookUPPollutants, ISMPReportInformation  " &
                "where LookUPPollutants.strPollutantCode = ISMPReportInformation.strPOllutant  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.StrReferenceNumber) as Pollutant,  " &
                "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
                "from EPDUserProfiles, ISMPReportInformation  " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer  " &
                "from ISMPMaster, APBFacilityInformation, ISMPReportInformation  " &
                "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and ISMPMaster.strAIRSNumber = @airs " &
                "and ISMPReportInformation.strDelete is NULL"

                Dim p As New SqlParameter("@airs", "0413" & txtAIRSNumber.Text)

                dtTestReportAssignments = DB.GetDataTable(query, p)
                dtTestReportAssignments.TableName = "TestReportAssignment"

            Else
                dtTestReportAssignments = Nothing
            End If

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

#Region "Saves"

    Private Sub SaveTestReportsAssignments()
        Dim strObject As String
        Dim EngineerGCode As String = ""
        Dim AssignDate As String = ""
        Dim PreCompliance As String = ""
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try


            drEngineers = dtEngineer.Select("UserName = '" & cboEngineer.Text & "'")
            For Each row In drEngineers
                EngineerGCode = row("numUserID")
            Next
            If chbNonComplianceTestReport.Checked = True Then
                PreCompliance = "True"
            Else
                PreCompliance = "False"
            End If

            If EngineerGCode <> "" Then

                For Each strObject In lblTestReportAssignment.Items
                    query = "select format(datReviewedBYUnitManager, 'dd-MMM-yyyy') as ReviewedByUnitManager " &
                          "from ISMPReportInformation " &
                          "where strReferenceNumber = @ref "

                    Dim p As New SqlParameter("@ref", strObject.ToString())

                    AssignDate = DB.GetString(query, p)

                    If AssignDate = "04-Jul-1776" Then
                        AssignDate = TodayFormatted
                    End If

                    query = "Update ISMPReportInformation set " &
                    "strReviewingEngineer = @strReviewingEngineer, " &
                    "datReviewedBYUnitManager = @datReviewedBYUnitManager, " &
                    "strReviewingUnit = @strReviewingUnit, " &
                    "numReviewingManager = @numReviewingManager, " &
                    "strPreComplianceStatus = @strPreComplianceStatus " &
                    "where ISMPReportInformation.strReferenceNumber = @strReferenceNumber "

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@strReviewingEngineer", EngineerGCode),
                        New SqlParameter("@datReviewedBYUnitManager", AssignDate),
                        New SqlParameter("@strReviewingUnit", CurrentUser.UnitId.ToString),
                        New SqlParameter("@numReviewingManager", CurrentUser.UserID),
                        New SqlParameter("@strPreComplianceStatus", PreCompliance),
                        New SqlParameter("@strReferenceNumber", strObject.ToString())
                    }

                    DB.RunCommand(query, p2)

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

#End Region

#Region "Run Statistics"

    Private Sub EngineerTestReport()
        Dim strObject As Object
        Dim DateBias As String = ""
        Dim Engineer As String = "and ("
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try

            If rdbEngineerTestReportTestDate.Checked = True Then
                DateBias = "datTestDateStart between @startdate " &
                "and  @enddate "
            End If
            If rdbEngineerTestReportReceived.Checked = True Then
                DateBias = "datReceivedDate between @startdate " &
                "and  @enddate "
            End If
            If rdbEngineerTestReportCompleted.Checked = True Then
                DateBias = "datCompleteDate between @startdate " &
                "and  @enddate "
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
                drEngineers = dtEngineer.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    Engineer += "strReviewingEngineer = '" & row("numUserID") & "' or "
                Next
            Next

            If Engineer = "and (" Then
                Engineer = "and strReviewingEngineer = '0' "
            Else
                Engineer = Mid(Engineer, 1, (Len(Engineer) - 3)) & ") "
            End If

            query = "Select ISMPReportInformation.strReferenceNumber, strFacilityName, " &
            "SUBSTRING(ISMPMaster.strAIRSNumber, 5,8) as AIRSNumber, strClosed, " &
            "format(datTestDateStart, 'dd-MMM-yyyy') as ForDatTestDateStart, " &
            "format(datReceivedDate, 'dd-MMM-yyyy') as ForDatReceivedDate, " &
            "format(datCompleteDate, 'dd-MMM-yyyy') as ForDatCompleteDate, " &
            "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer  " &
            "from EPDUserProfiles, ISMPReportInformation  " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as ReviewingEngineer, " &
            "(Select concat(strLastName, ', ' , strFirstName) as WitnessingEngineer " &
            "from EPDUserProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
            "and ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber) as WitnessingEngineer " &
            "from ISMPReportInformation, ISMPMaster, APBFacilityInformation " &
            "where ISMPReportInformation.strReferenceNumber = ISMPMaster.strReferenceNumber " &
            "and ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
            "and " & DateBias & " " & Engineer & " "

            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPEngineerTestReportStart.Value),
                New SqlParameter("@enddate", DTPEngineerTestReportEnd.Value)
            }

            dtEngineerGrid = DB.GetDataTable(query, p)
            dtEngineerGrid.TableName = "EngineerGrid"

            dgrEngineersFacilityList.DataSource = dtEngineerGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub EngineerOpenTestReports()
        Dim strObject As Object
        Dim Engineer As String = "and ("
        Dim drEngineers As DataRow()
        Dim row As DataRow

        Try

            For Each strObject In clbEngineersList2.CheckedItems
                drEngineers = dtEngineer.Select("UserName = '" & strObject.ToString() & "'")
                For Each row In drEngineers
                    Engineer += "strReviewingEngineer = '" & row("numUserID") & "' or "
                Next
            Next

            If Engineer = "and (" Then
                Engineer = "and strReviewingEngineer = '0' "
            Else
                Engineer = Mid(Engineer, 1, (Len(Engineer) - 3)) & ") "
            End If

            query = "Select " &
            "(select concat(strLastName, ', ' , strFirstName) as ReviewingEngineer " &
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

            lsbEngineers.Items.Clear()

            Dim dt As DataTable = DB.GetDataTable(query)

            For Each dr As DataRow In dt.Rows
                lsbEngineers.Items.Add(dr.Item("ReviewingEngineer") & vbTab & " \ " & vbTab & dr.Item("strReferenceNumber") _
                & vbTab & " \ " & vbTab & dr.Item("strFacilityName") & vbTab & " \ " & vbTab & dr.Item("ForDatReceivedDate") _
                & vbTab & " \ " & vbTab & "(" & dr.Item("Days") & ")")
            Next

        Catch ex As Exception
            ErrorReport(ex, query, "ISMPManagersTools.EngineerOpenTestReports")
        Finally

        End Try


    End Sub

#End Region

    Private Sub RunMonthlyReport()
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

            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPMonthlyStart.Value),
                New SqlParameter("@enddate", DTPMonthlyEnd.Value)
            }

            'Tests Received in Date Range
            query = "Select count(*) as Count from ISMPReportInformation " &
            "where datReceivedDate between @startdate and @enddate " &
            "and strDelete is NULL"
            TestReceived = DB.GetInteger(query, p)

            'Tests Completed in Date Range 
            query = "Select count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between @startdate and @enddate " &
            "and strClosed = 'True' and strDelete is NULL "
            TestCompleted = DB.GetInteger(query, p)

            'Tests Witnessed in Date Range
            query = "Select Count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between @startdate and @enddate " &
            "and strDelete is NULL and (strWitnessingEngineer <> '0' or strWitnessingEngineer2 <> '0') "
            TestWitnessed = DB.GetInteger(query, p)

            'Tests out of compliance 
            query = "Select Count(*) as Count from ISMPReportInformation " &
            "where datCompleteDate between @startdate and @enddate " &
            "and strDelete is NULL and strComplianceStatus = '05' "
            OutofCompliance = DB.GetInteger(query, p)

            'Test Median 
            query = "Select datediff(day, datReceivedDate, datCompleteDate) as diff from ISMPReportInformation " &
            "where datCompleteDate between @startdate and @enddate " &
            "and strDelete is NULL " &
            "and strClosed = 'True' order by diff desc"
            For Each dr As DataRow In DB.GetDataTable(query, p).Rows
                ReDim Preserve MedianArray(n)
                MedianArray(n) = dr.Item("Diff")
                n = n + 1
            Next

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
    Private Sub GetOutOfComplianceReport()
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

            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPMonthlyStart.Value),
                New SqlParameter("@enddate", DTPMonthlyEnd.Value)
            }

            query = "Select ISMPReportInformation.strReferenceNumber, strEmissionSource, strPollutantDescription, " &
            "format(datTestDateStart, 'dd-MMM-yyyy') as fordatTestDateStart, format(datTestDateEnd, 'dd-MMM-yyyy') as fordatTestDateEnd, " &
            "SUBSTRING(ISMPMaster.strAIRSNumber, 5,8) as AIRSNumber, strFacilityName, strFacilityCity, strFacilityState, " &
            "ISMPReportType.strReportType " &
            "from ISMPReportInformation, LookUPPollutants, ISMPMaster, APBFacilityInformation, " &
            "ISMPReportType " &
            "where strDelete is NULL and strComplianceStatus = '05' " &
            "and datCompleteDate between @startdate and @enddate " &
            "and strPollutantCode = strPOllutant " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and APBFacilityInformation.strAIRSNumber = ISMPMaster.strAIRSNumber " &
            "and ISMPReportInformation.strReportType = ISMPReportType.strKey "

            Report = ""

            For Each dr As DataRow In DB.GetDataTable(query, p).Rows
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

            Next

            txtOutOfComplianceReport.SelectionTabs = New Integer() {30, 260}
            txtOutOfComplianceReport.Text = Report

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ClearTestReportAssignmentTab()
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

    Private Sub RunSummaryReport()
        Try

            query = "Select " &
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
             "from EPDUserProfiles " &
             "left join (SELECT EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenReports " &
             "    FROM EPDUserProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'False' ) " &
             "    and EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "Group by strfirstname) OpenReport " &
             "on epduserprofiles.strFirstname = OpenReport.Engineer " &
             "left join (SELECT EPDUserProfiles.STRFIRSTNAME as Engineer, Count(*) as ClosedReports " &
             "    FROM EPDUserProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'True' ) " &
             "    and EPDUSerProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "    and datCompleteDate Between DATEADD(day, -60, GETDATE()) and GETDATE() " &
             "Group by strfirstname) ClosedReport " &
             "on epduserprofiles.strFirstName = ClosedReport.Engineer " &
             "left join (SELECT EPDUSerProfiles.STRFIRSTNAME as Engineer, Count(*) as OpenFiftys " &
             "    FROM EPDUSerProfiles, ISMPReportInformation " &
             "    WHERE (ISMPReportInformation.STRCLOSED = 'False' ) " &
             "    and EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer " &
             "    and datReceivedDate <= DATEADD(day, -50, GETDATE() ) " &
             "Group by strfirstname) OLdOpen " &
             "on epduserprofiles.strFirstName = OldOpen.Engineer " &
             " where (OpenReports > '0' or ClosedReports > '0'  or OpenFiftys > '0') " &
             "Order by Staff "

            dtSummaryReport = DB.GetDataTable(query)
            dtSummaryReport.TableName = "Test Summary"
            dgrTestSummary.DataSource = dtSummaryReport

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub RunUnitStatistics2()
        Try
            query = "SELECT DISTINCT
                CONCAT(strLastName, ', ', strFirstName) AS Engineer, strUnitDesc, totalreceived, ReceivedCount, ROUND(CONVERT(float, ReceivedCount) / CONVERT(float, TotalReceived) * 100, 2) AS ProgramPercent, MedDays, PercentDays, Witness1.witcount + witness2.witcount + Witness3.witcount AS Witnessed
                FROM ISMPReportInformation
                INNER JOIN EPDUserProfiles ON ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID
                INNER JOIN LookUpEPDUnits ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode
                INNER JOIN (SELECT COUNT(*) AS TotalReceived
                FROM ISMPReportInformation
                WHERE datCompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND strReviewingEngineer <> '0' AND strClosed = 'True') AS TotalReviewed ON ISMPReportInformation.strReviewingEngineer IS NOT NULL
                LEFT JOIN (SELECT strReviewingEngineer, COUNT(*) AS ReceivedCount
                FROM ISMPReportInformation
                WHERE datcompleteDate BETWEEN @startdate AND @enddate AND (strDelete IS NULL OR strDelete <> 'True') AND strReviewingEngineer <> '0' AND strClosed = 'True'
                GROUP BY strReviewingEngineer) AS TotalRec ON ISMPReportInformation.strReviewingEngineer = TotalRec.strReviewingEngineer
                LEFT JOIN (SELECT DISTINCT
                t.strReviewingEngineer, PERCENTILE_CONT(0.8) WITHIN GROUP(ORDER BY t.DaysIn) OVER(PARTITION BY t.strReviewingEngineer) AS percentDays, PERCENTILE_CONT(0.5) WITHIN GROUP(ORDER BY t.DaysIn) OVER(PARTITION BY t.strReviewingEngineer) AS MedDays
                FROM (SELECT strReviewingEngineer,
                CASE WHEN strClosed = 'True' THEN DATEDIFF(day, datReceivedDate, datCompleteDate) WHEN strClosed = 'False' THEN DATEDIFF(day, datReceivedDate, GETDATE()) END AS DaysIn
                FROM ISMPReportInformation
                WHERE datCompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND strReviewingEngineer <> '0' AND strClosed = 'True') AS t) AS PercentDays ON ISMPREportINformation.strReviewingEngineer = PercentDays.strReviewingEngineer
                LEFT JOIN (SELECT ISMPReportInformation.strWitnessingEngineer, COUNT(*) AS WitCount
                FROM ISMPReportInformation
                WHERE datcompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND ISMPReportInformation.strWitnessingEngineer <> '0' AND strClosed = 'True'
                GROUP BY ISMPReportInformation.strWitnessingEngineer) AS Witness1 ON ISMPREportINformation.strReviewingEngineer = Witness1.strWitnessingEngineer
                LEFT JOIN (SELECT ISMPReportInformation.strWitnessingEngineer2, COUNT(*) AS WitCount
                FROM ISMPReportInformation
                WHERE datcompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND ISMPReportInformation.strWitnessingEngineer2 <> '0' AND strClosed = 'True'
                GROUP BY ISMPReportInformation.strWitnessingEngineer2) AS Witness2 ON ISMPREportINformation.strReviewingEngineer = Witness2.strWitnessingEngineer2
                LEFT JOIN (SELECT ISMPWitnessingEng.strWitnessingEngineer, COUNT(*) AS WitCount
                FROM ISMPReportInformation, ISMPWitnessingEng
                WHERE datcompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND ISMPReportInformation.strReferenceNumber = ISMPWitnessingEng.strReferenceNumber AND strClosed = 'True'
                GROUP BY ISMPWitnessingEng.strWitnessingEngineer) AS Witness3 ON ISMPREportINformation.strReviewingEngineer = Witness3.strWitnessingEngineer
                WHERE datCompleteDate BETWEEN @startdate AND @enddate AND (strDelete <> 'True' OR strDelete IS NULL) AND ISMPReportInformation.strReviewingEngineer <> '0'
                ORDER BY strUnitDesc, Engineer"

            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPUnitStatsStartDate.Value),
                New SqlParameter("@enddate", DTPUnitStatsEndDate.Value)
            }

            dtUnitStats = DB.GetDataTable(query, p)
            dtUnitStats.TableName = "UnitStats"
            dgvUnitStats.DataSource = dtUnitStats

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

    Private Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click
        If TPReportAssignment.Focus = True Then
            SaveTestReportsAssignments()
        End If
    End Sub

    Private Sub bClear_Click(sender As Object, e As EventArgs) Handles bClear.Click
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
            OpenFormTestReportEntry(txtReferenceNumber.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExportToExcel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExportToExcel.LinkClicked
        dtEngineerGrid.ExportToExcel(Me)
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
            query = "select strReferenceNumber, " &
            "concat(strLastName, ', ' , strFirstName) as Engineer,  " &
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
            "and datCompleteDate between @startdate and @enddate  " &
            "and (strDelete <> 'True' or strDelete is Null)  " &
            "and strReviewingEngineer <> '0'  " &
            "and strClosed = 'True' "

            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPUnitStatsStartDate.Value),
                New SqlParameter("@enddate", DTPUnitStatsEndDate.Value)
            }

            dtUnitStats = DB.GetDataTable(query, p)
            dtUnitStats.TableName = "UnitStats"
            dgvUnitStats.DataSource = dtUnitStats

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
            OpenFormTestReportEntry(txtUnitStatReferenceNumber.Text)
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
                query = "Select " &
                "strMethodDesc " &
                "from LookUpISMPMethods " &
                "where strMethodCode = @code "

                Dim p As New SqlParameter("@code", txtMethodCode.Text)

                temp = DB.GetString(query, p)

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

                query = "Select " &
                "strMethodCode " &
                "From LookUpISMPMethods " &
                "where SUBSTRING(strMethodDesc, 1, charindex('-',strMethodDesc) - 2)  = @method "

                Dim p As New SqlParameter("@method", "Method " & txtMethodNumber.Text)

                If DB.ValueExists(query, p) Then
                    temp = DB.GetString(query, p)

                    If temp = txtMethodCode.Text Then
                        query = "Update LookUpISMPMethods set " &
                        "strMethodDesc = @desc " &
                        "where strMethodCode = @code "

                        Dim p2 As SqlParameter() = {
                            New SqlParameter("@desc", "Method " & txtMethodNumber.Text & " - " & txtMethodDescription.Text),
                            New SqlParameter("@code", txtMethodCode.Text)
                        }

                        DB.RunCommand(query, p2)
                    Else
                        query = "Select (max(strMethodCode) + 1) as MethodCode " &
                        "from LookUpISMPMethods "
                        Dim c As Integer = DB.GetInteger(query)
                        temp = c.ToString
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

                        query = "Insert into LookUpISMPMethods " &
                            "(STRMETHODCODE, STRMETHODDESC) " &
                        "values " &
                            "(@STRMETHODCODE, @STRMETHODDESC) "

                        Dim p3 As SqlParameter() = {
                            New SqlParameter("@STRMETHODCODE", temp),
                            New SqlParameter("@STRMETHODDESC", "Method " & txtMethodNumber.Text & " - " & txtMethodDescription.Text)
                        }

                        DB.RunCommand(query, p3)
                    End If
                Else
                    query = "Select (max(strMethodCode) + 1) as MethodCode " &
                    "from LookUpISMPMethods "
                    Dim c As Integer = DB.GetInteger(query)
                    temp = c.ToString
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

                    query = "Insert into LookUpISMPMethods " &
                            "(STRMETHODCODE, STRMETHODDESC) " &
                        "values " &
                            "(@STRMETHODCODE, @STRMETHODDESC) "

                    Dim p3 As SqlParameter() = {
                            New SqlParameter("@STRMETHODCODE", temp),
                            New SqlParameter("@STRMETHODDESC", "Method " & txtMethodNumber.Text & " - " & txtMethodDescription.Text)
                        }

                    DB.RunCommand(query, p3)
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

                query = "Select " &
                "strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", RefNum)

                If DB.ValueExists(query, p) Then
                    MsgBox("This Refernece Number already exists in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                query = "Select " &
                "strAIRSNumber " &
                "from APBMasterAIRS " &
                "where strAIRSNumber = @airs "

                Dim p2 As New SqlParameter("@airs", "0413" & AIRSNumber)

                If Not DB.ValueExists(query, p2) Then
                    MsgBox("This AIRS Number does not exist in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                query = "Insert into ISMPMaster " &
                "(STRREFERENCENUMBER, STRAIRSNUMBER, STRMODIFINGPERSON, DATMODIFINGDATE) " &
                "values " &
                "(@STRREFERENCENUMBER, @STRAIRSNUMBER, @STRMODIFINGPERSON, getdate()) "

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@STRREFERENCENUMBER", RefNum),
                    New SqlParameter("@STRAIRSNUMBER", "0413" & AIRSNumber),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID)
                }

                DB.RunCommand(query, p3)

                query = "Insert into ISMPReportInformation " &
                    "(STRREFERENCENUMBER, STRPOLLUTANT, STREMISSIONSOURCE, STRREPORTTYPE, " &
                    "STRDOCUMENTTYPE, STRAPPLICABLEREQUIREMENT, STRTESTINGFIRM, STRREVIEWINGENGINEER, " &
                    "STRWITNESSINGENGINEER, STRWITNESSINGENGINEER2, STRREVIEWINGUNIT, DATREVIEWEDBYUNITMANAGER, " &
                    "STRCOMPLIANCEMANAGER, DATTESTDATESTART, DATTESTDATEEND, DATRECEIVEDDATE, " &
                    "DATCOMPLETEDATE, MMOCOMMENTAREA, STRCLOSED, STRCOMMISSIONER, " &
                    "STRDIRECTOR, STRPROGRAMMANAGER, STRCOMPLIANCESTATUS, STRCC, " &
                    "STRMODIFINGPERSON, DATMODIFINGDATE, STRCONTROLEQUIPMENTDATA, STRDELETE, " &
                    "STRDETERMINATIONMETHOD, STROTHERWITNESSINGENG, STRCONFIDENTIALDATA, NUMREVIEWINGMANAGER, " &
                    "STRPRECOMPLIANCESTATUS) " &
                "values " &
                    "(@STRREFERENCENUMBER, @STRPOLLUTANT, @STREMISSIONSOURCE, @STRREPORTTYPE, " &
                    "@STRDOCUMENTTYPE, @STRAPPLICABLEREQUIREMENT, @STRTESTINGFIRM, @STRREVIEWINGENGINEER, " &
                    "@STRWITNESSINGENGINEER, @STRWITNESSINGENGINEER2, @STRREVIEWINGUNIT, @DATREVIEWEDBYUNITMANAGER, " &
                    "@STRCOMPLIANCEMANAGER, @DATTESTDATESTART, @DATTESTDATEEND, @DATRECEIVEDDATE, " &
                    "@DATCOMPLETEDATE, @MMOCOMMENTAREA, @STRCLOSED, @STRCOMMISSIONER, " &
                    "@STRDIRECTOR, @STRPROGRAMMANAGER, @STRCOMPLIANCESTATUS, @STRCC, " &
                    "@STRMODIFINGPERSON, getdate(), @STRCONTROLEQUIPMENTDATA, null, " &
                    "null, null, null, null, " &
                    "null) "

                Dim p4 As SqlParameter() = {
                    New SqlParameter("@STRREFERENCENUMBER", RefNum),
                    New SqlParameter("@STRPOLLUTANT", "00001"),
                    New SqlParameter("@STREMISSIONSOURCE", "N/A"),
                    New SqlParameter("@STRREPORTTYPE", "001"),
                    New SqlParameter("@STRDOCUMENTTYPE", "001"),
                    New SqlParameter("@STRAPPLICABLEREQUIREMENT", "N/A"),
                    New SqlParameter("@STRTESTINGFIRM", "00001"),
                    New SqlParameter("@STRREVIEWINGENGINEER", "0"),
                    New SqlParameter("@STRWITNESSINGENGINEER", "0"),
                    New SqlParameter("@STRWITNESSINGENGINEER2", "0"),
                    New SqlParameter("@STRREVIEWINGUNIT", "0"),
                    New SqlParameter("@DATREVIEWEDBYUNITMANAGER", dtpAddTestReportDateReceived.Value),
                    New SqlParameter("@STRCOMPLIANCEMANAGER", "0"),
                    New SqlParameter("@DATTESTDATESTART", "04-Jul-1776"),
                    New SqlParameter("@DATTESTDATEEND", "04-Jul-1776"),
                    New SqlParameter("@DATRECEIVEDDATE", dtpAddTestReportDateReceived.Value),
                    New SqlParameter("@DATCOMPLETEDATE", DTPAddTestReportDateCompleted.Value),
                    New SqlParameter("@MMOCOMMENTAREA", "N/A"),
                    New SqlParameter("@STRCLOSED", "False"),
                    New SqlParameter("@STRCOMMISSIONER", Commissioner),
                    New SqlParameter("@STRDIRECTOR", Director),
                    New SqlParameter("@STRPROGRAMMANAGER", ProgramManager),
                    New SqlParameter("@STRCOMPLIANCESTATUS", "01"),
                    New SqlParameter("@STRCC", "0"),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                    New SqlParameter("@STRCONTROLEQUIPMENTDATA", "N/A")
                }

                DB.RunCommand(query, p4)

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
                query = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtCloseTestReportRefNum.Text)

                If DB.ValueExists(query, p) Then
                    query = "Update ISMPReportInformation set " &
                    "strClosed = 'True' " &
                    "where strReferenceNumber = @ref "
                    DB.RunCommand(query, p)
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
                query = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtCloseTestReportRefNum.Text)

                If DB.ValueExists(query, p) Then
                    query = "Update ISMPReportInformation set " &
                    "strClosed = 'False' " &
                    "where strReferenceNumber = @ref "
                    DB.RunCommand(query, p)
                    MsgBox("Test Report Re-Opened", MsgBoxStyle.Information, "Historical Test Report")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class
