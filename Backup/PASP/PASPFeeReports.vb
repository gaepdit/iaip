Imports System.Data.OracleClient

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class PASPFeeReports
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim progress1 As ProgressStatus
    Dim sb As StatusBar = New StatusBar
    Dim pnl2 As StatusBarPanel = New StatusBarPanel
    Dim pnl3 As StatusBarPanel = New StatusBarPanel
    Dim rpt As ReportDocument
    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    Dim crParameterFieldDefinition As ParameterFieldDefinition
    Dim crParameterValues As New ParameterValues
    Dim crParameterDiscreteValue As New ParameterDiscreteValue

    Private Sub PASPFeeReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            AddProgressBar()
            progress1.progress = -1

            LoadComboBoxesF()


            lblFacilityBalanceReportTag.Visible = False
            mtbFacilityBalanceYear.Visible = False
            btnRunBalanceReport.Visible = False

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Enabled = False
            chbFacilityBalance.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
            progress1.progress = 0
        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub

#Region "Load Functions"
    Sub AddProgressBar()
        Try

            sb.Parent = Me
            sb.ShowPanels = True

            progress1 = New ProgressStatus(sb)

            With progress1
                .Style = StatusBarPanelStyle.OwnerDraw
                .AutoSize = StatusBarPanelAutoSize.Spring
                .MinWidth = 300
                .BorderStyle = StatusBarPanelBorderStyle.Sunken
                .Alignment = HorizontalAlignment.Right
            End With

            sb.Panels.Add(progress1)
            progress1.progress = 0

            'Dim pnl2 As StatusBarPanel = New StatusBarPanel
            pnl2 = New StatusBarPanel
            pnl2.AutoSize = StatusBarPanelAutoSize.Contents
            pnl2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            pnl2.Alignment = HorizontalAlignment.Center
            pnl2.Text = UserName
            sb.Panels.Add(pnl2)

            'Dim pnl3 As StatusBarPanel = New StatusBarPanel
            pnl3 = New StatusBarPanel
            pnl3.AutoSize = StatusBarPanelAutoSize.Contents
            pnl3.BorderStyle = StatusBarPanelBorderStyle.Sunken
            pnl3.Alignment = HorizontalAlignment.Center
            pnl3.Text = OracleDate
            sb.Panels.Add(pnl3)


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        End Try

    End Sub
    Sub LoadComboBoxesF()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            SQL = "Select DISTINCT substr(" & connNameSpace & ".FSCalculations.strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from " & connNameSpace & ".FSCalculations, " & connNameSpace & ".APBFacilityInformation " _
            + "where " & connNameSpace & ".FSCalculations.strairsnumber = " & connNameSpace & ".APBFacilityInformation.strairsnumber " _
            + "Order by strfacilityname "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            da.Fill(ds, "facilityInfo")

            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtAIRS.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next
            Dim temp As String

            temp = dtAIRS.Rows.Count

            With cboAirsNo
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        End Try
    End Sub
    Sub LoadComboBoxesD()

        Try

            ' SQL = "Select distinct strdepositno from " & connNameSpace & ".FSAddPaid " _
            '+ "order by strdepositno"

            SQL = "Select distinct strdepositno from " & connNameSpace & ".FS_Transactions " _
          + "order by strdepositno"

            Dim cmd As New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader
            dr.Read()
            cboDepositNo.Items.Add("")
            Do
                cboDepositNo.Items.Add(dr("strdepositno"))
            Loop While dr.Read
            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), "PASPFeeReports.LoadComboBoxesD(Sub1)")
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        End Try

        Try

            SQL = "Select distinct substr(strairsnumber,5) as strairsnumber " _
            + "from " & connNameSpace & ".FSAddPaid order by strairsnumber"

            Dim cmd As New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader
            dr.Read()
            cboAirs.Items.Add("")
            Do
                cboAirs.Items.Add(dr("strairsnumber"))
            Loop While dr.Read
            If dr.IsClosed = False Then dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), "PASPFeeReports.LoadComboBoxesD(Sub2)")
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        End Try
    End Sub

#End Region

    Private Sub tabReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabReport.SelectedIndexChanged
        Try

            Select Case tabReport.SelectedTab.Name

                Case "TPfacSpecific"
                Case "TPYear"
                Case "TPFinancial"
                Case "TPCompliance"
                Case "TPDeposits"
                    If cboDepositNo.Items.Count = 0 Then
                        LoadComboBoxesD()
                    End If

                Case "TPGeneral"

            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Facility Specific"

    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked

        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityFee10
            SQL = "Select * from " & connNameSpace & ".VW_Facility_Fee " & _
            "where strAIRSNumber = '0413" & cboAirsNo.SelectedValue & "' "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Fee")
            rpt.SetDataSource(ds)

            crParameterDiscreteValue.Value = "0413" & cboAirsNo.Text
            crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("AirsNo")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterValues.Clear()
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, cboAirsNo.Text)
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
        progress1.progress = 0

    End Sub

#End Region

#Region "Year Specific"

    Private Sub btnFeesandEmissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeesandEmissions.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New TotalFee10

            SQL = "Select * from " & connNameSpace & ".VW_Total_fee "

            SQL = "SELECT  intYear, sum(intVOCTons) as intvoctons, " & _
            "sum(intPMTons) as intPMTons, " & _
            "sum(intSO2Tons) as intSO2Tons, " & _
            "sum(intNOXtons) as intNOXTons, " & _
            "sum(numSMFee) as numSMFee, " & _
            "sum(numNSPSFee) as numNSPSFee, " & _
            "sum(numTotalFee) as numTotalFee, " & _
            "round(avg(numFeeRate)) as numFeeRate, " & _
            "Round(avg(titlevminfee)) as titlevminfee, " & _
            "round(avg(titlevfee)) as titlevfee  " & _
            "from " & connNameSpace & ".vw_total_fee " & _
            "group by intyear "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_Fee")

            rpt.SetDataSource(ds)



            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Annual Emission and Fee")
            CRFeesReports.Refresh()

            'Dim crReportDocument As TotalFee

            ' 
            'progress1.progress = -1
            'crReportDocument = New TotalFee
            'crReportDocument.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))

            'CRFeesReports.ReportSource = crReportDocument
            'DisplayReport(CRFeesReports, "Annual Emission and Fee")
            'CRFeesReports.Refresh()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
        progress1.progress = 0

    End Sub

    Private Sub btnClassification_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassification.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityClassification10

            SQL = "Select * from " & connNameSpace & ".FSCalculations "
            SQL = "Select * from " & connNameSpace & ".VW_Facility_Class_Counts "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Class_Counts")

            rpt.SetDataSource(ds)


            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Facility Classification Totals")
            CRFeesReports.Refresh()

            'Dim crReportDocument As FacilityClassification

            ' 
            'progress1.progress = -1
            'crReportDocument = New FacilityClassification
            'crReportDocument.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))

            'CRFeesReports.ReportSource = crReportDocument
            'DisplayReport(CRFeesReports, "Facility Classification Totals")
            'CRFeesReports.Refresh()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub


    Private Sub btnRunBalanceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunBalanceReport.Click
        Try
            If mtbFacilityBalanceYear.Text <> "" Then
                If mtbFacilityBalanceYear.TextLength = 4 Then
                    mtbFacilityBalanceYear.Text = mtbFacilityBalanceYear.Text
                Else
                    mtbFacilityBalanceYear.Text = Date.Today.Year
                End If
            Else
                mtbFacilityBalanceYear.Text = Date.Today.Year
            End If

            FeeBalanceReport()

            lblFacilityBalanceReportTag.Visible = False
            mtbFacilityBalanceYear.Visible = False
            btnRunBalanceReport.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub FeeBalanceReport()
        Try
            progress1.progress = -1
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ds = New DataSet

            If chbFacilityBalance.Checked = False Then
                rpt = New FacilityBalance10
            Else
                rpt = New FacilityBalancewithZero10
            End If

            SQL = "SELECT " & _
            "strFacilityName, " & _
            "AIRBranch.FeeDetails.strAIRSNumber, " & _
            "AIRBranch.FeeDetails.intyear, " & _
            "totalDue, totalPaid, " & _
            "strContactFirstName, strContactLastName, " & _
            "strContactPhoneNumber1, strContactFaxNumber, " & _
            "strContactEmail, strContactAddress1, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strSICCode, " & _
            "strPaymentType, PaidYear   " & _
            "FROM AIRBranch.APBFacilityInformation, " & _
            "AIRBranch.FeeDetails, AIRBranch.FeesContact, " & _
            "AIRBranch.APBHeaderData, AIRBranch.FSPayAndSubmit  " & _
            "WHERE AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeeDetails.strAIRSNumber " & _
            "AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FeesContact.strAIRSnumber " & _
            "AND AIRBranch.APBFacilityInformation.strAIRSnumber = AIRBranch.APBHeaderData.strAIRSNumber " & _
            "AND AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.FSPayAndSubmit.strAIRSNumber " & _
            "and airbranch.feedetails.intyear = AIRBranch.fsPayAndSubmit.intYear " & _
            "and airbranch.feedetails.intyear = '" & mtbFacilityBalanceYear.Text & "' " & _
            "order by strairsnumber "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Balance")

            rpt.SetDataSource(ds)



            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Year"
            spValue.Value = mtbFacilityBalanceYear.Text
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRFeesReports.ParameterFieldInfo = ParameterFields

            CRFeesReports.ReportSource = rpt
            If chbFacilityBalance.Checked = False Then
                DisplayReport(CRFeesReports, "Facility Fee Balance")
            Else
                DisplayReport(CRFeesReports, "Facility Fee Balance with Zero Balance")
            End If

            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub btnFeeBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeBalance.Click
        Try
            lblFacilityBalanceReportTag.Visible = True
            mtbFacilityBalanceYear.Visible = True
            btnRunBalanceReport.Visible = True

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Checked = False
            chbFacilityBalance.Enabled = False
            mtbFacilityBalanceYear.Focus()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub

    Private Sub btnFeeBalanceZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeBalanceZero.Click

        Try
            lblFacilityBalanceReportTag.Visible = True
            mtbFacilityBalanceYear.Visible = True
            btnRunBalanceReport.Visible = True

            chbFacilityBalance.Visible = False
            chbFacilityBalance.Checked = True
            chbFacilityBalance.Enabled = False
            mtbFacilityBalanceYear.Focus()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "Financial"

    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New TotalPayment10
            SQL = "Select * from " & connNameSpace & ".VW_Total_PAYMENT "
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Total_PAYMENT")

            rpt.SetDataSource(ds)

            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Overall Fee Balance")
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnPayDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayDate.Click
        Try

            DateTimePicker1.Text = OracleDate
            DateTimePicker2.Text = OracleDate
            DateTimePicker1.Visible = True
            DateTimePicker2.Visible = True
            pnlDateRange.Visible = True
            Label3.Visible = True
            Label4.Visible = True
            Label5.Visible = True

            rdb2005Variance.Visible = False
            rdb2006Variance.Visible = False
            btnRunVarianceReport.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub btnDateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateReport.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterField
            Dim p3 As New ParameterDiscreteValue
            Dim p4 As New ParameterDiscreteValue

            rpt = New DepositDataQA10

            SQL = "Select * from " & connNameSpace & ".FSAddPaid " & _
            "where datPayDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' " & _
            "order by strBatchNo "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSAddPaid")

            rpt.SetDataSource(ds)

            p1.ParameterFieldName = "StartDate"
            p3.Value = DateTimePicker1.Value
            p1.CurrentValues.Add(p3)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            p2.ParameterFieldName = "EndDate"
            p4.Value = DateTimePicker2.Value
            p2.CurrentValues.Add(p4)
            p.Add(p2)
            CRFeesReports.ParameterFieldInfo = p

            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Payments by Date - " & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & " --> " & Format(DateTimePicker2.Value, "dd-MMM-yyyy"))
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub

    Private Sub btndeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeposit.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New DepositData10

            SQL = "Select * from " & connNameSpace & ".FSAddPaid " & _
            "where datPayDate between '" & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & "' and '" & Format(DateTimePicker2.Value, "dd-MMM-yyyy") & "' "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSAddPaid")
            rpt.SetDataSource(ds)

            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterField
            Dim p3 As New ParameterDiscreteValue
            Dim p4 As New ParameterDiscreteValue

            p1.ParameterFieldName = "StartDate"
            p3.Value = DateTimePicker1.Value
            p1.CurrentValues.Add(p3)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            p2.ParameterFieldName = "EndDate"
            p4.Value = DateTimePicker2.Value
            p2.CurrentValues.Add(p4)
            p.Add(p2)
            CRFeesReports.ParameterFieldInfo = p

            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Payments by Date - " & Format(DateTimePicker1.Value, "dd-MMM-yyyy") & " --> " & Format(DateTimePicker2.Value, "dd-MMM-yyyy"))
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnBankrupt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankrupt.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New Bankrupt10

            SQL = "select * from " & connNameSpace & ".VW_Bankrupt"
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Bankrupt")
            rpt.SetDataSource(ds)

            CRFeesReports.ReportSource = rpt
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnFeeByYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeByYear.Click
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New feeByYear10
            SQL = "Select * from " & connNameSpace & ".FeesDue "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FeesDue")
            rpt.SetDataSource(ds)



            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Total Fee by Year")
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Sub VarianceReport()
        Try
            pnlDateRange.Visible = False

            progress1.progress = -1
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ds = New DataSet
            rpt = New Variance10

            If rdb2005Variance.Checked = True Then
                SQL = "Select * from " & connNameSpace & ".FeeVariance " & _
                "where difference2005 <> '0' and vCheck2005 <> 'YES' "

            Else
                SQL = "Select * from " & connNameSpace & ".FeeVariance " & _
                "where difference2006 <> '0' and vCheck2006 <> 'YES' "

            End If

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FeeVariance")
            rpt.SetDataSource(ds)



            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Year"
            If rdb2005Variance.Checked = True Then
                spValue.Value = "2005"
            Else
                spValue.Value = "2006"
            End If
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRFeesReports.ParameterFieldInfo = ParameterFields

            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Variance Report")
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        progress1.progress = 0
    End Sub

    Private Sub btnRunVarianceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunVarianceReport.Click
        Try
            VarianceReport()
            pnlDateRange.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnvariance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvariance.Click
        Try
            rdb2005Variance.Visible = True
            rdb2006Variance.Visible = True
            btnRunVarianceReport.Visible = True
            pnlDateRange.Visible = True

            DateTimePicker1.Visible = False
            DateTimePicker2.Visible = False
            btndeposit.Visible = False
            btnDateReport.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

#Region "Deposits"

    Private Sub lblDepositData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblDepositData.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            Dim depositno, airsno, header As String

            SQL = ""
            If cboDepositNo.Text <> "" Then
                'SQL = "Select * from " & connNameSpace & ".FSAddPaid " & _
                '"where strDepositNo like '%" & cboDepositNo.Text & "%' " & _
                '"order by intyear desc  "

                SQL = "Select * from " & connNameSpace & ".FS_Transactions " & _
                "where strDepositNo like '%" & cboDepositNo.Text & "%' " & _
                "order by nuMFeeYear desc  "
            Else
                If cboAirs.Text <> "" Then
                    'SQL = "Select * from " & connNameSpace & ".FSAddPaid " & _
                    '"where strAIRSNumber like '0413%" & cboAirs.Text & "%' " & _
                    '"order by intyear desc  "

                    SQL = "Select * from " & connNameSpace & ".FS_Transactions " & _
                    "where strAIRSNumber like '0413%" & cboAirs.Text & "%' " & _
                    "order by nuMFeeYear desc  "
                End If
            End If
            If SQL = "" Then
                'SQL = "Select * from " & connNameSpace & ".FSAddPaid " & _
                '"order by intyear desc  "

                SQL = "Select * from " & connNameSpace & ".FS_Transactions " & _
                "order by nuMFeeYear desc  "
            End If

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSAddPaid")
            rpt = New DepositQA10
            rpt.SetDataSource(ds)


            If cboDepositNo.Text <> "" Then
                depositno = cboDepositNo.Text
                header = depositno
                airsno = ""
            Else
                If cboAirs.Text <> "" Then
                    airsno = cboAirs.Text
                    header = airsno
                    depositno = ""
                Else
                    MsgBox("Please select at least one value.", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If

            Dim p As New ParameterFields
            Dim p1 As New ParameterField
            Dim p2 As New ParameterField
            Dim p3 As New ParameterDiscreteValue
            Dim p4 As New ParameterDiscreteValue

            p1.ParameterFieldName = "DepositNo"
            p3.Value = depositno
            p1.CurrentValues.Add(p3)
            p.Add(p1)
            CRFeesReports.ParameterFieldInfo = p

            p2.ParameterFieldName = "AirsNo"
            p4.Value = "0413" & airsno
            p2.CurrentValues.Add(p4)
            p.Add(p2)
            CRFeesReports.ParameterFieldInfo = p

            rpt.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))
            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, header)
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

#End Region

#Region "Compliance"

    Private Sub btnClassChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassChange.Click
        progress1.progress = -1
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            ds = New DataSet
            rpt = New ClassChanged10

            SQL = "select * from " & connNameSpace & ".VW_Class_Changed"

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Class_Changed")
            rpt.SetDataSource(ds)

            CRFeesReports.ReportSource = rpt
            CRFeesReports.DisplayGroupTree = True
            CRFeesReports.ShowGroupTreeButton = True
            CRFeesReports.Refresh()


            'Dim crReportDocument As ClassChanged

            ' 
            'pnlNSPS.Visible = False
            'crReportDocument = New ClassChanged
            'crReportDocument.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))
            'CRFeesReports.ReportSource = crReportDocument
            'DisplayReport(CRFeesReports, "Change in Classification")
            'CRFeesReports.Refresh()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnNSPSChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSPSChange.Click
        Try

            pnlNSPS.Visible = True

            lblNSPS1.Visible = True
            lblNSPS2.Visible = True
            lblNSPS3.Visible = True

            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub lblNSPS1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS1.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet

            SQL = "Select * " & _
            "from " & connNameSpace & ".VW_NSPS_Status " & _
            "where strnsps = 'YES' " & _
            "and STRnspsexempt = 'YES'"

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")

            rpt = New NSPSStatus10
            rpt.SetDataSource(ds)


            pnlNSPS.Visible = False
            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "NSPS Exempt - Subject but exempt")
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0

    End Sub
    Private Sub lblNSPS2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS2.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New NSPSStatus1_10
            SQL = "Select * " & _
            "from " & connNameSpace & ".VW_NSPS_Status " & _
            "where Strnsps1 = 'YES' " & _
            "and strnsps = 'NO'"

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)


            pnlNSPS.Visible = False
            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "NSPS Subject - Not subject")
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub lblNSPS3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNSPS3.LinkClicked
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New NSPSStatus2_10
            SQL = "Select * " & _
            "from " & connNameSpace & ".VW_NSPS_Status " & _
            "where strnsps = 'YES' " & _
            "and STRoperate <> 'YES'"

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NSPS_Status")
            rpt.SetDataSource(ds)


            pnlNSPS.Visible = False
            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "NSPS, Did not Operate")
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub
    Private Sub btnRunNonRespondent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunNonRespondent.Click
        Try

            If mtbNonRespondentYear.Text.Length <> 4 Or mtbNonRespondentYear.Text = "" Then
                mtbNonRespondentYear.Text = Date.Today.Year.ToString
            End If

            NoResponse()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub NoResponse()
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New NonRespondent10

            SQL = "Select * from " & connNameSpace & ".VW_NonRespondent " & _
            "where intYear = '" & mtbNonRespondentYear.Text & "' " & _
            "and intSubmittal <> '1' "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_NonRespondent")
            rpt.SetDataSource(ds)



            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Failed to Submit Fee Data - " & mtbNonRespondentYear.Text)
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

        progress1.progress = 0

    End Sub
    Private Sub btnNoResponse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoResponse.Click
        Try

            pnlNSPS.Visible = True

            lblNSPS1.Visible = False
            lblNSPS2.Visible = False
            lblNSPS3.Visible = False

            mtbNonRespondentYear.Visible = True
            btnRunNonRespondent.Visible = True
            lblNonRespondant.Visible = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub btnNoOperate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoOperate.Click
        Try
            pnlNSPS.Visible = False
            mtbNonRespondentYear.Visible = False
            btnRunNonRespondent.Visible = False
            lblNonRespondant.Visible = False

            progress1.progress = -1
            ds = New DataSet
            rpt = New NoOperate10
            SQL = "Select * from " & connNameSpace & ".VW_No_Operate "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_No_Operate")
            rpt.SetDataSource(ds)



            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Did Not Operate")
            CRFeesReports.Refresh()

            'Dim crReportDocument As NoOperate

            ' 
            'pnlNSPS.Visible = False
            'crReportDocument = New NoOperate
            'crReportDocument.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))
            'CRFeesReports.ReportSource = crReportDocument
            'DisplayReport(CRFeesReports, "Did Not Operate")
            'CRFeesReports.Refresh()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

#End Region

#Region "General"

    Private Sub btnComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComments.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityComments10

            SQL = "Select * from " & connNameSpace & ".FSPAYANDSUBMIT " & _
            "where strComments is not Null "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FSPAYANDSUBMIT")
            rpt.SetDataSource(ds)


            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Facility Comments")
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
        'crReportDocument.Close()
    End Sub

    Private Sub btnFacInfoChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacInfoChange.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New FacilityInfo10

            SQL = "Select * from " & connNameSpace & ".VW_Facility_Info "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Facility_Info")
            rpt.SetDataSource(ds)


            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Facility Info")
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
    End Sub

    Private Sub btnTrainingReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrainingReg.Click
        Try
            progress1.progress = -1
            ds = New DataSet
            rpt = New TrainingReg10

            SQL = "Select * from AIRBranch.VW_Training_reg "
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Training_reg")
            rpt.SetDataSource(ds)


            CRFeesReports.ReportSource = rpt
            DisplayReport(CRFeesReports, "Training Registrants")
            CRFeesReports.DisplayGroupTree = False
            CRFeesReports.ShowGroupTreeButton = False
            CRFeesReports.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        progress1.progress = 0
    End Sub

#End Region

  
    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
        End Try
    End Sub

    
End Class