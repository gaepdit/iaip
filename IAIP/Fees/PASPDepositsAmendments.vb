Imports Oracle.ManagedDataAccess.Client


Public Class PASPDepositsAmendments
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim feeyear As String = Now.Year
    Dim feeTon, feeSM, feePart70, feeNSPS As Double
    Dim totalfee, part70fee, smfee, nspsfee, calculatedfee As Double
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim dsWorkEnTry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Dim ValidatingState As String
    Dim daInvoice As OracleDataAdapter
    Dim dsInvoice As DataSet

    Private Sub PASPDepositsAmendments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            pnl1.Text = " "
            pnl2.Text = UserName
            pnl3.Text = OracleDate
            feeyear = Now.AddYears(-1).Year
            dtpBatchDepositDate.Text = OracleDate
            LoadComboBoxes()





            TCDepositsAmendments.TabPages.Remove(TPDeposits)
            TCDepositsAmendments.TabPages.Remove(TPAmendments)
            TCDepositsAmendments.TabPages.Remove(TPVariances)
            TCDepositsAmendments.TabPages.Remove(TPModifications)

            'txtSubmittal.Text = "1"
            'DTPAmendmentSubmitted.Text = OracleDate
            'txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")
            'If AccountArray(18, 0) = "18" Then
            '    If AccountArray(18, 3) = "0" Then
            '        TCDepositsAmendments.TabPages.Remove(TPAmendments)
            '        TCDepositsAmendments.TabPages.Remove(TPVariances)
            '        TCDepositsAmendments.TabPages.Remove(TPModifications)

            '    End If
            'End If
            'FillComboBoxes()
            'LoadComboBoxes2()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadComboBoxes()
        Try
            Dim dtAIRS As New DataTable
            Dim dtPayType As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "PayID, PayType " & _
            "from AIRBRANCH.FSPayType " & _
            "order by paytype"

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "PayType")

            dtPayType.Columns.Add("PayID", GetType(System.String))
            dtPayType.Columns.Add("PayType", GetType(System.String))

            drNewRow = dtPayType.NewRow()
            drNewRow("PayType") = " "
            drNewRow("PayID") = " "
            dtPayType.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("PayType").Rows()
                drNewRow = dtPayType.NewRow()
                drNewRow("PayID") = drDSRow("PayID")
                drNewRow("PayType") = drDSRow("PayType")
                dtPayType.Rows.Add(drNewRow)
            Next

            With cboDepositPayType
                .DataSource = dtPayType
                .DisplayMember = "PayType"
                .ValueMember = "PayID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Amendment Functions"


#Region "Other Functions"
    Sub LoadFeeRates()
        Dim SQL As String
        Try
            SQL = "Select smfee, pertonrate, nspsfee, part70fee " & _
            " from AIRBRANCH.FSFeeRates " & _
            " where intyear = '" & feeyear & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then

                If dr.IsDBNull(0) Then
                    feeSM = 0.0
                Else
                    feeSM = dr.Item("smfee")
                End If

                If dr.IsDBNull(1) Then
                    feeTon = 0.0
                Else
                    feeTon = dr.Item("pertonrate")
                End If

                If dr.IsDBNull(2) Then
                    feeNSPS = 0.0
                Else
                    feeNSPS = dr.Item("nspsfee")
                End If

                If dr.IsDBNull(3) Then
                    feePart70 = 0.0
                Else
                    feePart70 = dr.Item("part70fee")
                End If
            Else
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub DidNotOperate()

        Try

            'If the facility has checked did not operate do thefollowing:

            btnCalculate.Enabled = False
            chkPart70SM.Enabled = False
            chkNSPSExempt.Enabled = False
            cblNSPSExempt.Enabled = False
            txtvoctons.Text = 0
            txtnoxtons.Text = 0
            txtso2tons.Text = 0
            txtpmtons.Text = 0
            part70fee = 0.0
            smfee = 0.0
            totalfee = 0.0
            nspsfee = 0.0
            calculatedfee = 0.0
            lblpart70fee.Text = String.Format("{0:C}", 0.0)
            lblTotalFee.Text = String.Format("{0:C}", 0.0)
            lblNSPSFee.Text = String.Format("{0:C}", 0.0)
            lblPart70SMFee.Text = String.Format("{0:C}", 0.0)
            lblPart70.Text = String.Format("{0:C}", 0.0)
            lblSM.Text = String.Format("{0:C}", 0.0)
            lblcalculated.Text = String.Format("{0:C}", 0.0)
            lblvocfee.Text = String.Format("{0:C}", 0.0)
            lblnoxfee.Text = String.Format("{0:C}", 0.0)
            lblso2fee.Text = String.Format("{0:C}", 0.0)
            lblpmfee.Text = String.Format("{0:C}", 0.0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    '' Removed during Code Analysis review of CA1811
    'Private Sub GetCalculationInfo()
    '    Dim SQL, nspsReason As String
    '    Dim i As Integer
    '    Dim fee As Double
    '    Dim exception As String

    '    Try

    '        nspsReason = "0"

    '        SQL = "Select intvoctons, intpmtons, intso2tons, intnoxtons, " _
    '            + "numpart70fee, numsmfee, numnspsfee, numtotalfee, " _
    '            + "strnspsexempt, strnspsreason, stroperate, numfeerate, " _
    '            + "strclass1, strnsps1, strpart70, strsyntheticminor, numcalculatedfee " _
    '            + "from AIRBRANCH.FSCalculations " _
    '            + "where strairsnumber = '0413" & airsnumber & "' " _
    '            + "and intyear = '" & feeyear & "' "

    '        Dim cmd As New OracleCommand(SQL, CurrentConnection)
    '        cmd.CommandType = CommandType.Text

    '        If CurrentConnection.State = ConnectionState.Open Then
    '        Else
    '            CurrentConnection.Open()
    '        End If

    '        Dim dr As OracleDataReader = cmd.ExecuteReader()
    '        Dim recExist As Boolean = dr.Read

    '        If recExist = True Then

    '            'Getting emission details from FSCalculations. 
    '            'This table has all the information that goes into panel fee calculations.

    '            If dr.IsDBNull(12) Then
    '            Else
    '                ddlClass.Text = dr.Item("strclass1")
    '            End If

    '            If dr.IsDBNull(0) Then
    '                txtvoctons.Text = ""
    '                lblvocfee.Text = ""
    '            Else
    '                txtvoctons.Text = dr.Item("intvoctons")
    '                fee = PollutantVOCNOx(CInt(txtvoctons.Text))
    '                lblvocfee.Text = String.Format("{0:C}", fee)
    '            End If

    '            If dr.IsDBNull(1) Then
    '                txtpmtons.Text = ""
    '                lblpmfee.Text = ""
    '            Else
    '                txtpmtons.Text = dr.Item("intpmtons")
    '                fee = PollutantPMSO2(CInt(txtpmtons.Text))
    '                lblpmfee.Text = String.Format("{0:C}", fee)

    '            End If

    '            If dr.IsDBNull(2) Then
    '                txtso2tons.Text = ""
    '                lblso2fee.Text = ""
    '            Else
    '                txtso2tons.Text = dr.Item("intso2tons")
    '                fee = PollutantPMSO2(CInt(txtso2tons.Text))
    '                lblso2fee.Text = String.Format("{0:C}", fee)

    '            End If

    '            If dr.IsDBNull(3) Then
    '                txtnoxtons.Text = ""
    '                lblnoxfee.Text = ""
    '            Else
    '                txtnoxtons.Text = dr.Item("intnoxtons")
    '                fee = PollutantVOCNOx(CInt(txtnoxtons.Text))
    '                lblnoxfee.Text = String.Format("{0:C}", fee)
    '            End If

    '            If dr.IsDBNull(4) Then
    '                part70fee = 0
    '            Else
    '                part70fee = dr.Item("numpart70fee")
    '                'lblPart70Fee.Text = String.Format("{0:C}", dr.Item("numtotalpart70fee"))
    '            End If

    '            If dr.IsDBNull(5) Then
    '                smfee = 0
    '            Else
    '                smfee = dr.Item("numsmfee")
    '                'lblpart70SMFee.Text = String.Format("{0:C}", dr.Item("numpart70smfee"))
    '            End If

    '            If dr.IsDBNull(6) Then
    '                nspsfee = 0
    '            Else
    '                nspsfee = dr.Item("numnspsfee")
    '                'lblNSPSFee.Text = String.Format("{0:C}", dr.Item("numnspsfee"))
    '            End If

    '            If dr.IsDBNull(7) Then
    '                totalfee = 0
    '            Else
    '                totalfee = dr.Item("numtotalfee")
    '                'lblTotalFee.Text = String.Format("{0:C}", totalfee)
    '            End If

    '            If dr.IsDBNull(16) Then
    '                calculatedfee = 0
    '            Else
    '                calculatedfee = dr.Item("numcalculatedfee")
    '            End If

    '            If IsDBNull(dr.Item("strnsps1")) Then
    '                chkNSPS1.Checked = False
    '            Else
    '                If dr.Item("strnsps1") = "YES" Then
    '                    chkNSPS1.Checked = True
    '                Else
    '                    chkNSPS1.Checked = False
    '                End If
    '            End If

    '            If IsDBNull(dr.Item("strnspsexempt")) Then
    '                chkNSPSExempt.Checked = False
    '            Else
    '                If dr.Item("strnspsexempt") = "YES" Then
    '                    chkNSPSExempt.Checked = True
    '                    cblNSPSExempt.Enabled = True
    '                    'nspsfee = 0
    '                Else
    '                    chkNSPSExempt.Checked = False
    '                End If
    '            End If

    '            If dr.IsDBNull(9) Then
    '                nspsReason = "0"
    '            Else
    '                nspsReason = dr.Item("strnspsreason")
    '            End If

    '            If dr.Item("stroperate") = "NO" Then
    '                chkDidNotOperate.Checked = True
    '                DidNotOperate()
    '            Else
    '                chkDidNotOperate.Checked = False
    '            End If

    '            If dr.IsDBNull(14) Then
    '            Else
    '                If dr.Item("strpart70") = "YES" Then
    '                    chkPart70SM.SelectedIndex = 0
    '                End If
    '            End If

    '            If dr.IsDBNull(15) Then
    '            Else
    '                If dr.Item("strsyntheticminor") = "YES" Then
    '                    chkPart70SM.SelectedIndex = 1
    '                End If
    '            End If

    '        Else
    '            MsgBox("No record exist for this AIRS Number and selected Fee Year", MsgBoxStyle.Information, Me.Text)
    '            Exit Sub

    '        End If

    '        If nspsReason = "0" Then
    '            For i = 1 To cblNSPSExempt.Items.Count
    '                cblNSPSExempt.SetItemCheckState(i - 1, CheckState.Unchecked)
    '            Next
    '        Else
    '            Do While nspsReason.Length <> 0
    '                If nspsReason.Contains(",") Then
    '                    exception = Mid(nspsReason, 1, nspsReason.IndexOf(","))
    '                    nspsReason = Replace(nspsReason, exception & ",", "")
    '                Else
    '                    exception = nspsReason
    '                    nspsReason = ""
    '                End If
    '                For i = 0 To cblNSPSExempt.Items.Count - 1
    '                    If cblNSPSExempt.GetItemCheckState(i) = CheckState.Unchecked Then
    '                        cblNSPSExempt.SetSelected(i, True)
    '                        If cblNSPSExempt.SelectedValue = exception Then
    '                            cblNSPSExempt.SetItemChecked(i, True)
    '                        End If
    '                    End If
    '                Next
    '            Loop
    '        End If

    '        'old technic
    '        'For i = 1 To nspsReason.Length
    '        '    If i = 8 Or i = 9 Then
    '        '    Else
    '        '        If Mid(nspsReason, i, 1) = 1 Then
    '        '            cblNSPSExempt.SetItemCheckState(i - 1, CheckState.Checked)
    '        '        Else
    '        '            cblNSPSExempt.SetItemCheckState(i - 1, CheckState.Unchecked)
    '        '        End If
    '        '    End If
    '        'Next

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub

    Private Sub ClassCalculate()
        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
            Else
                Select Case ddlClass.Text

                    Case "A"
                        pnlEmissions.Enabled = True
                        btnCalculate.Enabled = True
                        chkPart70SM.SetItemCheckState(0, CheckState.Checked)
                        If part70fee > feePart70 Then
                        Else
                            part70fee = feePart70
                        End If
                        ResetFees()

                        'lblpart70SMFee.Text = String.Format("{0:C}", part70smfee)

                    Case "B"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        ResetFees()

                    Case "SM"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        chkPart70SM.SetItemCheckState(1, CheckState.Checked)
                        If smfee > feeSM Then
                        Else
                            smfee = feeSM
                        End If
                        ResetFees()

                    Case "PR"
                        pnlEmissions.Enabled = False
                        btnCalculate.Enabled = True
                        ResetFees()

                    Case Else
                        'MsgBox("There is no information for this facility", MsgBoxStyle.Information, me.text)

                End Select

            End If

            CalculateFees()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GetFSPayAndSubmitInfo()
        Try

            SQL = "Select " & _
            "strPaymentType, intSubmittal, " & _
            "strOfficialName, strOfficialTitle, " & _
            "dateSubmit, strComments " & _
            "from AIRBRANCH.FSPayAndSubmit " & _
            "where strAIRSnumber = '0413" & cboAirsNo2.Text & "' " & _
            "and intYear = '" & cboFeeYear2.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strPaymentType")) Then
                    cboAmendmentsPayType.Text = ""
                Else
                    cboAmendmentsPayType.Text = dr.Item("strPaymentType")
                End If
                txtSubmittal.Text = "1"
                'If IsDBNull(dr.Item("intSubmittal")) Then
                '    txtSubmittal.Text = "1"
                'Else
                '    txtSubmittal.Text = "1"
                'End If
                If IsDBNull(dr.Item("strOfficialName")) Then
                    txtOfficalName.Clear()
                Else
                    txtOfficalName.Text = dr.Item("strOfficialName")
                End If
                If IsDBNull(dr.Item("strOfficialTitle")) Then
                    txtOfficalTitle.Clear()
                Else
                    txtOfficalTitle.Text = dr.Item("strOfficialTitle")
                End If
                If IsDBNull(dr.Item("dateSubmit")) Then
                    DTPAmendmentSubmitted.Text = OracleDate
                Else
                    DTPAmendmentSubmitted.Text = dr.Item("dateSubmit")
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    txtAmendmentComments.Clear()
                Else
                    txtAmendmentComments.Text = dr.Item("strComments")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ResetFees()

        Try

            If ddlClass.Text = "A" Then
                If txtvoctons.Text = "" Then
                    txtvoctons.Text = 0
                End If
                If txtpmtons.Text = "" Then
                    txtpmtons.Text = 0
                End If
                If txtnoxtons.Text = "" Then
                    txtnoxtons.Text = 0
                End If
                If txtso2tons.Text = "" Then
                    txtso2tons.Text = 0
                End If

            Else
                txtvoctons.Text = 0
                txtnoxtons.Text = 0
                txtso2tons.Text = 0
                txtpmtons.Text = 0
                lblvocfee.Text = 0
                lblnoxfee.Text = 0
                lblso2fee.Text = 0
                lblpmfee.Text = 0
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Function PollutantVOCNOx(ByVal tons As Integer) As Double
        Dim fee As Double

        Try


            'For 1-hour zone non-attainment counties, the VOC/NOx emissions
            'threshold is 25 tons

            If chkNonAttainment.Checked = True Then
                If tons <= 25 Then
                    fee = 0.0
                Else
                    fee = tons * feeTon
                End If
            Else
                If tons <= 100 Then
                    fee = 0.0
                Else
                    fee = tons * feeTon
                End If

            End If

            Return fee

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Function
    Function PollutantPMSO2(ByVal tons As Integer) As Double

        Dim fee As Double

        Try


            If tons <= 100 Then
                fee = 0.0
            Else
                fee = tons * feeTon
            End If

            Return fee

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Function
    Sub PerformCalculations()

        Dim fee As Double

        Try


            If IsNumeric(txtvoctons.Text) Then
                fee = PollutantVOCNOx(CInt(txtvoctons.Text))
                lblvocfee.Text = String.Format("{0:C}", fee)
            Else
                txtvoctons.Text = ""
                lblvocfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtnoxtons.Text) Then
                fee = PollutantVOCNOx(CInt(txtnoxtons.Text))
                lblnoxfee.Text = String.Format("{0:C}", fee)
            Else
                txtnoxtons.Text = ""
                lblnoxfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtpmtons.Text) Then
                fee = PollutantPMSO2(CInt(txtpmtons.Text))
                lblpmfee.Text = String.Format("{0:C}", fee)
            Else
                txtpmtons.Text = ""
                lblpmfee.Text = String.Format("{0:C}", 0.0)
            End If

            If IsNumeric(txtso2tons.Text) Then
                fee = PollutantPMSO2(CInt(txtso2tons.Text))
                lblso2fee.Text = String.Format("{0:C}", fee)
            Else
                txtso2tons.Text = ""
                lblso2fee.Text = String.Format("{0:C}", 0.0)
            End If

            ClassCalculate()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub CalculateFees()

        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
                Exit Sub
            End If

            If lblvocfee.Text = "" Then
                lblvocfee.Text = 0
            End If
            If lblpmfee.Text = "" Then
                lblpmfee.Text = 0
            End If
            If lblnoxfee.Text = "" Then
                lblnoxfee.Text = 0
            End If
            If lblso2fee.Text = "" Then
                lblso2fee.Text = 0
            End If

            calculatedfee = CDbl(lblvocfee.Text) + CDbl(lblso2fee.Text) + _
            CDbl(lblpmfee.Text) + CDbl(lblnoxfee.Text)

            lblpart70fee.Text = String.Format("{0:C}", calculatedfee)

            If chkPart70SM.CheckedIndices.Contains(0) = True Then
                If calculatedfee < feePart70 Then
                    'lblpart70SMFee.Text = String.Format("{0:C}", 2500.0)
                    part70fee = feePart70
                Else
                    part70fee = calculatedfee
                    'lblpart70SMFee.Text = String.Format("{0:C}", part70fee)
                End If
            Else
                If ddlClass.Text <> "A" Then part70fee = 0
            End If

            If chkPart70SM.CheckedIndices.Contains(1) = True Then
                If part70fee < feeSM Then
                    smfee = feeSM
                Else
                    smfee = 0.0
                End If
            Else
                smfee = 0
            End If

            If chkPart70SM.CheckedIndices.Contains(0) = True And _
                chkPart70SM.CheckedIndices.Contains(1) = True Then
                If calculatedfee < feePart70 Then
                    part70fee = feePart70
                    'lblPart70SMFee.Text = String.Format("{0:C}", feePart70)
                Else
                    part70fee = calculatedfee
                    'lblPart70SMFee.Text = String.Format("{0:C}", part70fee)
                End If
            End If

            If part70fee < feeSM Then
                lblPart70SMFee.Text = String.Format("{0:C}", smfee)
            Else
                lblPart70SMFee.Text = String.Format("{0:C}", part70fee)
            End If

            If chkNSPS1.Checked = True Then
                chkNSPSExempt.Enabled = True
                nspsfee = feeNSPS
            Else
                chkNSPSExempt.Checked = False
                chkNSPSExempt.Enabled = False
                nspsfee = 0.0
            End If

            If chkNSPSExempt.Checked = True Then
                'chkNSPS1.Checked = True
                cblNSPSExempt.Enabled = True
                nspsfee = 0.0
            Else
                cblNSPSExempt.Enabled = False
            End If

            lblNSPSFee.Text = String.Format("{0:C}", nspsfee)

            totalfee = part70fee + nspsfee + smfee

            'The following three lines are just for writing to the datapase purpose
            'All the three labels never come into picture otherwise
            lblcalculated.Text = String.Format("{0:C}", calculatedfee)
            lblPart70.Text = String.Format("{0:C}", part70fee)
            lblSM.Text = String.Format("{0:C}", smfee)
            lblcalculated.Visible = False
            lblPart70.Visible = False
            lblSM.Visible = False

            lblTotalFee.Text = String.Format("{0:C}", totalfee)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    '' Removed during Code Analysis review of CA1811
    'Private Sub ClearPage2()

    '    Try

    '        'cboFacilityName.Text = ""
    '        ' cboAirsNo2.Text = ""
    '        cboFeeYear2.Text = ""
    '        ddlClass.Text = ""
    '        chkNSPS1.Checked = False
    '        chkDidNotOperate.Checked = False
    '        chkNonAttainment.Checked = False
    '        chkNSPSExempt.Checked = False
    '        txtvoctons.Text = 0
    '        txtnoxtons.Text = 0
    '        txtso2tons.Text = 0
    '        txtpmtons.Text = 0
    '        part70fee = 0.0
    '        smfee = 0.0
    '        totalfee = 0.0
    '        nspsfee = 0.0
    '        calculatedfee = 0.0
    '        lblpart70fee.Text = String.Format("{0:C}", 0.0)
    '        lblTotalFee.Text = String.Format("{0:C}", 0.0)
    '        lblNSPSFee.Text = String.Format("{0:C}", 0.0)
    '        lblPart70SMFee.Text = String.Format("{0:C}", 0.0)
    '        lblPart70.Text = String.Format("{0:C}", 0.0)
    '        lblSM.Text = String.Format("{0:C}", 0.0)
    '        lblcalculated.Text = String.Format("{0:C}", 0.0)
    '        lblvocfee.Text = String.Format("{0:C}", 0.0)
    '        lblnoxfee.Text = String.Format("{0:C}", 0.0)
    '        lblso2fee.Text = String.Format("{0:C}", 0.0)
    '        lblpmfee.Text = String.Format("{0:C}", 0.0)

    '        chkPart70SM.SetItemCheckState(1, CheckState.Unchecked)
    '        chkPart70SM.SetItemCheckState(0, CheckState.Unchecked)

    '        Dim i As Integer
    '        For i = 0 To (cblNSPSExempt.Items.Count - 1)
    '            cblNSPSExempt.SetItemCheckState(i, CheckState.Unchecked)
    '        Next

    '        txtOfficalName.Clear()
    '        txtOfficalTitle.Clear()
    '        txtAmendmentComments.Clear()
    '        DTPAmendmentSubmitted.Text = OracleDate
    '        cboAmendmentsPayType.Text = ""

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub

#End Region
    Private Sub btnAmend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmend.Click
        Try

            If cboAirsNo2.Text = "" Or cboAirsNo2.Text = " " Then
                MessageBox.Show("Please enter a valid AIRS Number")
                Exit Sub
            End If

            If txtOfficalName.Text = "" Or txtOfficalTitle.Text = "" Or txtAmendmentComments.Text = "" Then
                MessageBox.Show("Please enter information in all the fields 'Offical Name/Title/GECO Comments'")
                Exit Sub
            End If
            If chkNSPSExempt.Checked = True Then
                Dim chkNSPSReason As Boolean
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        chkNSPSReason = True
                        Exit For
                    End If
                Next
                If chkNSPSReason = False Then
                    MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                End If
            End If

            SaveOldData()
            PerformCalculations()
            SaveFeeCalcInfo()

            SaveFSPayAndSubmit()

            MsgBox("The emission information has been updated.", MsgBoxStyle.Information, Me.Text)
            ClearPage()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Try

            PerformCalculations()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Text Box, Check Boxes Changed Events"
    Private Sub chkDidNotOperate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDidNotOperate.CheckedChanged
        Try

            If chkDidNotOperate.Checked = True Then
                DidNotOperate()
            Else
                btnCalculate.Enabled = True
                chkPart70SM.Enabled = True
                chkNSPSExempt.Enabled = True
                If chkNSPSExempt.Checked = True Then
                    cblNSPSExempt.Enabled = True
                End If
                ClassCalculate()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chkNSPSExempt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNSPSExempt.CheckedChanged
        Try

            If chkNSPSExempt.Checked = True Then
                cblNSPSExempt.Enabled = True
            Else
                cblNSPSExempt.Enabled = False
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    cblNSPSExempt.SetItemCheckState(i, CheckState.Unchecked)
                Next
            End If

            CalculateFees()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub ddlClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        Try

            chkDidNotOperate.Checked = False
            chkPart70SM.Enabled = True
            chkNSPSExempt.Enabled = True
            If chkNSPSExempt.Checked = True Then
                cblNSPSExempt.Enabled = True
            End If
            ClassCalculate()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Update and Insert Functions"
    Sub SaveFSPayAndSubmit()
        Try
            If cboAmendmentsPayType.Text = "" Then
                cboAmendmentsPayType.Text = "N/A"
            End If

            SQL = "select " & _
            "intSubmittal " & _
            "from AIRBRANCH.FSPayAndSubmit " & _
            "where strAIRSNumber = '0413" & cboAirsNo2.Text & "' " & _
            "and intYear = '" & cboFeeYear2.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into AIRBRANCH.FSPayAndSubmit " & _
                "values " & _
                "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                "'" & cboAmendmentsPayType.Text & "', '" & txtSubmittal.Text & "', " & _
                "'" & Replace(txtOfficalName.Text, "'", "''") & "', '" & Replace(txtOfficalTitle.Text, "'", "''") & "', " & _
                "'" & DTPAmendmentSubmitted.Text & "', '" & Replace(txtAmendmentComments.Text, "'", "''") & "') "
            Else
                SQL = "Update AIRBRANCH.FSPayAndSubmit set " & _
                "strPaymentType = '" & cboAmendmentsPayType.Text & "', " & _
                "intSubmittal = '" & Replace(txtSubmittal.Text, "'", "''") & "', " & _
                "strOfficialName = '" & Replace(txtOfficalName.Text, "'", "''") & "', " & _
                "strOfficialTitle = '" & Replace(txtOfficalTitle.Text, "'", "''") & "', " & _
                "dateSubmit = '" & DTPAmendmentSubmitted.Text & "', " & _
                "strComments = '" & Replace(txtAmendmentComments.Text, "'", "''") & "' " & _
                "where strAIRSNumber = '0413" & cboAirsNo2.Text & "' " & _
                "and intYear = '" & cboFeeYear2.Text & "' "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub SaveOldData()
        Try


            SQL = "INSERT INTO AIRBRANCH.FSAmendment " _
            + "Select STRAIRSNUMBER, INTYEAR, INTVOCTONS, INTPMTONS, " _
            + "INTSO2TONS, INTNOXTONS, STRNSPSEXEMPT, STRNSPSREASON, " _
            + "STROPERATE, STRNSPSEXEMPTREASON, STRPART70, STRSYNTHETICMINOR, " _
            + "NUMCALCULATEDFEE, STRCLASS1, STRNSPS1, " _
            + "to_date('" & Format$(Now, "dd-MMM-yyyy hh:mm:ss") & "', " _
            + "'dd-mon-yyyy hh:mi:ss'), " _
            + "'" & UserGCode & "' from AIRBRANCH.FSCalculations " _
            + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
            + "and intyear = '" & cboFeeYear2.Text & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            cmd.ExecuteReader()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub UpdateFeeCalculations()

        Dim SQL, didnotoperate, exemptnsps, nspsreason, nsps1, exemptreasontext As String
        Dim part70, syntheticminor As String
        Dim i As Integer
        Dim cmd As OracleCommand

        Try

            nspsreason = "0"
            exemptreasontext = ""

            If chkNSPS1.Checked = True Then
                nsps1 = "YES"
            Else
                nsps1 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 0 Then
                part70 = "YES"
            Else
                part70 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 1 Then
                syntheticminor = "YES"
            Else
                syntheticminor = "NO"
            End If

            If chkNSPSExempt.Checked = True Then
                exemptnsps = "YES"
                nspsreason = ""
                For i = 0 To cblNSPSExempt.Items.Count - 1
                    If cblNSPSExempt.GetItemChecked(i) = True Then
                        cblNSPSExempt.SelectedIndex = i
                        nspsreason = nspsreason & cblNSPSExempt.SelectedValue & ","
                        exemptreasontext = exemptreasontext & cblNSPSExempt.Text & "; "
                    End If
                Next
            Else
                exemptnsps = "NO"
            End If
            If nspsreason.Length > 1 Then
                nspsreason = Mid(nspsreason, 1, Len(nspsreason) - 1)
            End If

            If nspsreason = "" Then
                nspsreason = "0"
            End If

            If chkDidNotOperate.Checked = True Then
                didnotoperate = "NO"
            Else
                didnotoperate = "YES"
            End If

            SQL = "Update AIRBRANCH.FSCalculations set " _
            + "intvoctons = '" & CInt(txtvoctons.Text) & "', " _
            + "intnoxtons = '" & CInt(txtnoxtons.Text) & "', " _
            + "intpmtons = '" & CInt(txtpmtons.Text) & "', " _
            + "intso2tons = '" & CInt(txtso2tons.Text) & "', " _
            + "numpart70fee = '" & CDbl(lblPart70.Text) & "', " _
            + "numsmfee = '" & CDbl(lblSM.Text) & "', " _
            + "numnspsfee = '" & CDbl(lblNSPSFee.Text) & "', " _
            + "numtotalfee = '" & CDbl(lblTotalFee.Text) & "', " _
            + "strnspsexempt = '" & exemptnsps & "', " _
            + "strnspsreason = '" & nspsreason & "', " _
            + "stroperate = '" & didnotoperate & "', " _
            + "strclass1 = '" & ddlClass.Text & "', " _
            + "strnsps1 = '" & nsps1 & "', " _
            + "strnspsexemptreason = '" & Replace(exemptreasontext, "'", "''") & "', " _
            + "strpart70 = '" & part70 & "', " _
            + "strsyntheticminor = '" & syntheticminor & "', " _
            + "numcalculatedfee = '" & CDbl(lblcalculated.Text) & "' " _
            + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
            + "and intyear = '" & cboFeeYear2.Text & "' "


            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub SaveFeeCalcInfo()
        Try


            If chkNSPSExempt.Checked = True Then
                Dim chkNSPSReason As Boolean
                Dim i As Integer
                For i = 0 To (cblNSPSExempt.Items.Count - 1)
                    If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
                        chkNSPSReason = True
                        Exit For
                    End If
                Next
                If chkNSPSReason = False Then
                    MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                End If
            End If

            'This sub will first check if a record for this facility exists in 
            'the table FSCalculations for the fee year If the record exists it
            'will update the record or else it will insert a new record for the
            'facility in the table

            Dim SQL As String

            SQL = "Select strairsnumber " _
         + "from AIRBRANCH.FSCalculations " _
         + "where strairsnumber = '0413" & cboAirsNo2.Text & "' " _
         + "and intyear = '" & cboFeeYear2.Text & "' "

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then
                UpdateFeeCalculations()
            Else
                InsertFeeCalculations()
            End If

            SQL = "Select strAIRSNUmber " & _
            "From AIRBRANCH.FSPayAndSubmit " & _
            "where strAIRSNumber = '0413" & cboAirsNo2.Text & "' " & _
            "and intYear = '" & cboFeeYear2.Text & "'  "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = False Then
                SQL = "Insert into AIRBRANCH.FSPayAndSubmit " & _
                "values " & _
                "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                "'Entire Annual Year', 1, " & _
                "'" & Replace(UserName, "'", "''") & "', 'GA APB Employee', " & _
                "sysdate, '') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub InsertFeeCalculations()
        Dim SQL, didnotoperate, exemptnsps, exemptreasontext As String
        Dim part70, syntheticminor As String
        Dim nsps1, nspsreason As String
        Dim i As Integer

        Try


            nspsreason = "0"
            exemptreasontext = ""
            If chkNSPS1.Checked = True Then
                nsps1 = "YES"
            Else
                nsps1 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 0 Then
                part70 = "YES"
            Else
                part70 = "NO"
            End If

            If chkPart70SM.SelectedIndex = 1 Then
                syntheticminor = "YES"
            Else
                syntheticminor = "NO"
            End If

            If chkNSPSExempt.Checked = True Then
                exemptnsps = "YES"
                nspsreason = ""
                For i = 0 To cblNSPSExempt.Items.Count - 1
                    If cblNSPSExempt.GetItemChecked(i) = True Then
                        cblNSPSExempt.SelectedIndex = i
                        nspsreason = nspsreason & cblNSPSExempt.SelectedValue & ","
                        exemptreasontext = exemptreasontext & cblNSPSExempt.Text & "; "
                    End If
                Next
            Else
                exemptnsps = "NO"
            End If

            If nspsreason.Length > 1 Then
                nspsreason = Mid(nspsreason, 1, Len(nspsreason) - 1)
            End If
            If nspsreason = "" Then
                nspsreason = "0"
            End If

            If chkDidNotOperate.Checked = True Then
                didnotoperate = "NO"
            Else
                didnotoperate = "YES"
            End If

            SQL = "Insert into AIRBRANCH.FSCalculations " _
            + "(strairsnumber, intyear, " _
            + "intvoctons, intpmtons, intso2tons, intnoxtons, " _
            + "numpart70fee, numsmfee, numnspsfee, " _
            + "numtotalfee, strnspsexempt, strnspsreason, stroperate, " _
            + "strclass1, strnsps1, strnspsexemptreason, strpart70, " _
            + "strsyntheticminor, numcalculatedfee, numAdminFee) " _
            + "values('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " _
            + "'" & CInt(txtvoctons.Text) & "', '" & CInt(txtpmtons.Text) & "', " _
            + "'" & CInt(txtso2tons.Text) & "', '" & CInt(txtnoxtons.Text) & "', " _
            + "'" & CDbl(lblPart70.Text) & "', '" & CDbl(lblSM.Text) & "', " _
            + "'" & CDbl(lblNSPSFee.Text) & "', " _
            + "'" & CDbl(lblTotalFee.Text) & "', '" & exemptnsps & "', " _
            + "'" & nspsreason & "', '" & didnotoperate & "', " _
            + "'" & ddlClass.Text & "', '" & nsps1 & "', " _
            + "'" & Replace(exemptreasontext, "'", "''") & "', " _
            + "'" & part70 & "', '" & syntheticminor & "', " _
            + "'" & CDbl(lblcalculated.Text) & "', '0') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select " & _
            "strInvoiceNo " & _
            "from AIRBRANCH.FSAddPaid " & _
            "where strAIRSNumber = '0413" & cboAirsNo2.Text & "' " & _
            "and intYear = '" & cboFeeYear2.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                If cboAmendmentsPayType.Text = "Entire Annual Year" Then
                    SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                    "values " & _
                    "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                    "'0', '', '', '', " & _
                    "'ANNUAL', '', '" & UserGCode & "', " & _
                    "'" & Replace(txtAmendmentComments.Text, "'", "''") & "', '', " & _
                    "AIRBRANCH.SeqFSDeposit.nextval, " & _
                    "'" & cboAirsNo2.Text & "-A1-" & cboFeeYear2.Text & "') "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                    "values " & _
                    "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                    "'0', '', '', '', " & _
                    "'QUARTER ONE', '', '" & UserGCode & "', " & _
                    "'" & Replace(txtAmendmentComments.Text, "'", "''") & "', '', " & _
                    "AIRBRANCH.SeqFSDeposit.nextval, " & _
                    "'" & cboAirsNo2.Text & "-Q1-" & cboFeeYear2.Text & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                   "values " & _
                   "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                   "'0', '', '', '', " & _
                   "'QUARTER TWO', '', '" & UserGCode & "', " & _
                   "'" & Replace(txtAmendmentComments.Text, "'", "''") & "', '', " & _
                   "AIRBRANCH.SeqFSDeposit.nextval, " & _
                   "'" & cboAirsNo2.Text & "-Q2-" & cboFeeYear2.Text & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                   "values " & _
                   "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                   "'0', '', '', '', " & _
                   "'QUARTER THREE', '', '" & UserGCode & "', " & _
                   "'" & Replace(txtAmendmentComments.Text, "'", "''") & "', '', " & _
                   "AIRBRANCH.SeqFSDeposit.nextval, " & _
                   "'" & cboAirsNo2.Text & "-Q3-" & cboFeeYear2.Text & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                   "values " & _
                   "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                   "'0', '', '', '', " & _
                   "'QUARTER FOUR', '', '" & UserGCode & "', " & _
                   "'" & Replace(txtAmendmentComments.Text, "'", "''") & "', '', " & _
                   "AIRBRANCH.SeqFSDeposit.nextval, " & _
                   "'" & cboAirsNo2.Text & "-Q4-" & cboFeeYear2.Text & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    'Function validatePnlFeeCalc()

    '    Dim validate As Boolean

    '    validate = True

    '    If chkDidNotOperate.Checked = False And ddlClass.Text = "A" Then

    '        For Each ctrl As Control In pnlFeeCalculation.Controls

    '            If TypeOf ctrl Is TextBox Then
    '                If CType(ctrl, TextBox).Text = "" Or _
    '                Not IsNumeric(CType(ctrl, TextBox).Text) Or _
    '                CType(ctrl, TextBox).Text > 4000 Or _
    '                CType(ctrl, TextBox).Text < 0 Then
    '                    validate = False
    '                    'ErrorMessage()
    '                    Return validate
    '                End If
    '            Else
    '                validate = True
    '            End If
    '        Next
    '    End If

    '    If chkNSPSExempt.Checked = True Then
    '        Dim i As Integer
    '        For i = 0 To (cblNSPSExempt.Items.Count - 1)
    '            If (cblNSPSExempt.CheckedIndices.Contains(i) = True) Then
    '                validate = True
    '                Exit For
    '            End If
    '            validate = False
    '        Next
    '        If validate = False Then
    '            MsgBox("Please select at least one checkbox for NSPS exempt", MsgBoxStyle.OKOnly, me.text )
    '            'ErrorMessage()
    '            Return validate
    '        Else
    '        End If
    '    End If

    '    Return validate

    'End Function
#End Region
#End Region

#Region "Deposit Functions"
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try

            LoadDataGrid()
            txtCount.Text = dgvDeposit.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadDataGrid()

        Dim SQL As String = ""
        Try

            dsWorkEnTry = New DataSet

            If cboDepositNo.Text <> "" Then
                SQL = "Select " & _
                "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                "strDepositNo, datPayDate, " & _
                "numPayment, intYear, " & _
                "strCheckNo, strBatchNo, " & _
                "strPayType, intPayId, " & _
                "strComments, strInvoiceNo " & _
                "from AIRBRANCH.FSAddPaid " & _
                "where strDepositNo = '" & cboDepositNo.Text & "' " & _
                "order by strBatchNo "
            Else
                If cboAirsNo.Text <> "" Then
                    SQL = "Select " & _
                   "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                   "strDepositNo, datPayDate, " & _
                   "numPayment, intYear, " & _
                   "strCheckNo, strBatchNo, " & _
                   "strPayType, intPayId, " & _
                   "strComments, strInvoiceNo " & _
                   "from AIRBRANCH.FSAddPaid " & _
                   "where strairsnumber = '0413" & cboAirsNo.Text & "' " & _
                   "order by strBatchNo "
                Else
                    If cboFeeYear.Text <> "" Then
                        If rdbDeposited.Checked = True Then
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from AIRBRANCH.FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "and strDepositNo is Not Null " & _
                           "order by strBatchNo "
                        Else
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from AIRBRANCH.FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "order by strBatchNo "
                        End If
                    Else
                        MsgBox("Please select a Deposit Number or AIRS Number or Fee Year first", MsgBoxStyle.OkOnly, Me.Text)
                    End If
                End If
            End If

            If SQL <> "" Then
                daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
                dgvDeposit.DataSource = dsWorkEnTry
                dgvDeposit.DataMember = "tblWorkEntry"

                dgvDeposit.RowHeadersVisible = False
                dgvDeposit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvDeposit.AllowUserToResizeColumns = True
                dgvDeposit.AllowUserToAddRows = False
                dgvDeposit.AllowUserToDeleteRows = False
                dgvDeposit.AllowUserToOrderColumns = True
                dgvDeposit.AllowUserToResizeRows = True
                dgvDeposit.ColumnHeadersHeight = "35"
                dgvDeposit.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvDeposit.Columns("strairsnumber").DisplayIndex = 0
                dgvDeposit.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvDeposit.Columns("strdepositno").DisplayIndex = 1
                dgvDeposit.Columns("datpaydate").HeaderText = "Deposit Date"
                dgvDeposit.Columns("datpaydate").DisplayIndex = 2
                dgvDeposit.Columns("datpaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvDeposit.Columns("numpayment").HeaderText = "Payment"
                dgvDeposit.Columns("numpayment").DisplayIndex = 3
                dgvDeposit.Columns("intyear").HeaderText = "Year"
                dgvDeposit.Columns("intyear").DisplayIndex = 4
                dgvDeposit.Columns("strcheckno").HeaderText = "Check Number"
                dgvDeposit.Columns("strcheckno").DisplayIndex = 5
                dgvDeposit.Columns("strbatchno").HeaderText = "Batch Number"
                dgvDeposit.Columns("strbatchno").DisplayIndex = 6
                dgvDeposit.Columns("strpaytype").HeaderText = "Payment Type"
                dgvDeposit.Columns("strpaytype").DisplayIndex = 7
                dgvDeposit.Columns("intpayid").HeaderText = "Pay ID"
                dgvDeposit.Columns("intpayid").DisplayIndex = 8
                dgvDeposit.Columns("intpayid").Visible = False
                dgvDeposit.Columns("strcomments").HeaderText = "Comments"
                dgvDeposit.Columns("strcomments").DisplayIndex = 9
                dgvDeposit.Columns("strcomments").Visible = False
                dgvDeposit.Columns("strinvoiceno").HeaderText = "Invoice Number"
                dgvDeposit.Columns("strinvoiceno").DisplayIndex = 10
                dgvDeposit.Columns("strinvoiceno").Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub dgvDeposit_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposit.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDeposit.HitTest(e.X, e.Y)
        Dim temp As String

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposit.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvDeposit.Columns(1).HeaderText

                    If dgvDeposit.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAirsNo.Text = dgvDeposit(0, hti.RowIndex).Value
                        txtPayId.Text = dgvDeposit(8, hti.RowIndex).Value
                        If IsDBNull(dgvDeposit(1, hti.RowIndex).Value) Then
                            txtDepositNo.Clear()
                        Else
                            txtDepositNo.Text = dgvDeposit(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(2, hti.RowIndex).Value) Then
                            txtDepositdate.Clear()
                        Else
                            txtDepositdate.Text = dgvDeposit(2, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposit(3, hti.RowIndex).Value) Then
                            txtPayment.Clear()
                        Else
                            txtPayment.Text = dgvDeposit(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(4, hti.RowIndex).Value) Then
                            txtYear.Clear()
                        Else
                            txtYear.Text = dgvDeposit(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(5, hti.RowIndex).Value) Then
                            txtCheckNo.Clear()
                        Else
                            txtCheckNo.Text = dgvDeposit(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(6, hti.RowIndex).Value) Then
                            txtbatchNo.Clear()
                        Else
                            txtbatchNo.Text = dgvDeposit(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(7, hti.RowIndex).Value) Then
                            cboPayType.Text = ""
                        Else
                            cboPayType.Text = ""
                            cboPayType.Text = dgvDeposit(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(9, hti.RowIndex).Value) Then
                            txtComments.Clear()
                        Else
                            txtComments.Text = dgvDeposit(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(10, hti.RowIndex).Value) Then
                            txtInvoiceNo.Clear()
                        Else
                            txtInvoiceNo.Text = dgvDeposit(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try


    End Sub

    Sub ValidateEntry()
        Try

            If cboAirsNo.Items.Contains(mtbAirsNo.Text) = False Then
                MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                       "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, Me.Text)
                ValidatingState = False
                Exit Sub
            Else
                'MsgBox("Valid AIRS #")
                ' ValidatingState = False
                ' Exit Sub
            End If
            If mtbAirsNo.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS Number.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                ValidatingState = False
                Exit Sub
            End If
            If txtYear.Text = "" Then
                If IsNumeric(txtYear.Text) Then
                Else
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    ValidatingState = False
                    Exit Sub
                End If
            Else
                If txtYear.Text > feeyear + 1 Then
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    ValidatingState = False
                    Exit Sub
                End If
            End If

            If txtDepositdate.Text = "" Then
                MsgBox("Please enter a Deposit Date", MsgBoxStyle.OkOnly, "Incorrect Date")
                ValidatingState = False
                Exit Sub
            End If

            If txtPayment.Text = "" Then
                MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
                ValidatingState = False
                Exit Sub
            End If

            If txtDepositNo.Text = "" Then
                MsgBox("Please enter a Deposit Number", MsgBoxStyle.OkOnly, "Incorrect Deposit No.")
                ValidatingState = False
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnUpdateEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEnTry.Click
        Try
            Dim InvoiceNumber As String = ""


            ValidatingState = True
            ValidateEntry()

            If ValidatingState = True Then
                Select Case cboPayType.Text
                    Case "ANNUAL"
                        InvoiceNumber = mtbAirsNo.Text & "-A-" & txtYear.Text
                    Case "QUARTER ONE"
                        InvoiceNumber = mtbAirsNo.Text & "-Q1-" & txtYear.Text
                    Case "QUARTER TWO"
                        InvoiceNumber = mtbAirsNo.Text & "-Q2-" & txtYear.Text
                    Case "QUARTER THREE"
                        InvoiceNumber = mtbAirsNo.Text & "-Q3-" & txtYear.Text
                    Case "QUARTER FOUR"
                        InvoiceNumber = mtbAirsNo.Text & "-Q4-" & txtYear.Text
                    Case Else
                        InvoiceNumber = ""
                End Select

                SQL = "Update AIRBRANCH.FSAddPaid set strairsnumber = '0413" & mtbAirsNo.Text & "', " _
                + "datPaydate = '" & txtDepositdate.Text & "', " _
                + "numPayment = '" & CDec(txtPayment.Text) & "', " _
                + "strCheckno = '" & txtCheckNo.Text & "', " _
                + "strBatchno = '" & txtbatchNo.Text & "', " _
                + "strPaytype = '" & cboPayType.Text & "', " _
                + "strDepositno = '" & txtDepositNo.Text & "', " _
                + "intYear = '" & CInt(txtYear.Text) & "', " _
                + "strComments = '" & Replace(txtComments.Text, "'", "''") & "', " _
                + "strInvoiceNo = '" & Replace(InvoiceNumber, "'", "''") & "', " _
                + "strEntryPerson = '" & UserGCode & "' " _
                + "where intpayid = '" & txtPayId.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                cmd.CommandType = CommandType.Text

                If CurrentConnection.State = ConnectionState.Open Then
                Else
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader

                MsgBox("The record has been updated successfully", MsgBoxStyle.Information, "Update Success!")
                ClearPage()
                LoadDataGrid()
            Else
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnDeleteEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEnTry.Click

        Try

            SQL = "Delete from AIRBRANCH.FSAddPaid " _
            + "where intpayid = '" & txtPayId.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, "Delete Success!")
            ClearPage()
            LoadDataGrid()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ClearPage()
        Try

            txtPayId.Text = ""
            txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")
            cboDepositNo.Text = txtDepositNo.Text
            txtDepositNo.Text = ""
            txtPayment.Text = ""
            txtCheckNo.Text = ""
            txtbatchNo.Text = ""
            txtComments.Text = ""
            txtYear.Text = ""
            cboPayType.Text = ""
            mtbAirsNo.Text = ""
            txtInvoiceNo.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Variances"
    Sub LoadDataGridNotVerified()
        Dim SQL As String = ""
        Try

            dsWorkEnTry = New DataSet
            If Not dsWorkEnTry.Tables("tblworkentry") Is Nothing Then
                dsWorkEnTry.Tables("tblworkentry").Clear()
                dsWorkEnTry.Tables.Remove("tblworkentry")
                dsWorkEnTry.AcceptChanges()
            End If

            Select Case cboFeeYear3.Text
                Case 2005
                    SQL = "Select strairsnumber, Difference2005, Fee2004, Fee2005, " _
                        + "Vcheck2005, comments2005 from airbranch.feevariance " _
                        + "where Difference2005 <> 0 and vcheck2005 = 'NO'"

                Case 2006
                    SQL = "Select strairsnumber, Difference2006, Fee2005, Fee2006, " _
                        + "Vcheck2006, comments2006 from airbranch.feevariance " _
                        + "where Difference2006 <> 0 and vcheck2006 = 'NO'"
            End Select

            daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
            dgvVariance.DataSource = dsWorkEnTry
            dgvVariance.DataMember = "tblWorkEntry"
            txtVarianceCount.Text = dgvVariance.RowCount.ToString

            Select Case cboFeeYear3.Text
                Case 2005
                    dgvVariance.RowHeadersVisible = False
                    dgvVariance.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvVariance.AllowUserToResizeColumns = True
                    dgvVariance.AllowUserToAddRows = False
                    dgvVariance.AllowUserToDeleteRows = False
                    dgvVariance.AllowUserToOrderColumns = True
                    dgvVariance.AllowUserToResizeRows = True
                    dgvVariance.ColumnHeadersHeight = "35"
                    dgvVariance.Columns("strairsnumber").HeaderText = "AIRS #"
                    dgvVariance.Columns("strairsnumber").DisplayIndex = 0
                    dgvVariance.Columns("Difference2005").HeaderText = "Difference"
                    dgvVariance.Columns("Difference2005").DisplayIndex = 1
                    dgvVariance.Columns("Fee2004").HeaderText = "2004 Fee"
                    dgvVariance.Columns("Fee2004").DisplayIndex = 2
                    dgvVariance.Columns("Fee2005").HeaderText = "2005 Fee"
                    dgvVariance.Columns("Fee2005").DisplayIndex = 3
                    dgvVariance.Columns("Vcheck2005").HeaderText = "Verified?"
                    dgvVariance.Columns("Vcheck2005").DisplayIndex = 4
                    dgvVariance.Columns("comments2005").HeaderText = "Comments"
                    dgvVariance.Columns("comments2005").DisplayIndex = 5
                Case 2006
                    dgvVariance.RowHeadersVisible = False
                    dgvVariance.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvVariance.AllowUserToResizeColumns = True
                    dgvVariance.AllowUserToAddRows = False
                    dgvVariance.AllowUserToDeleteRows = False
                    dgvVariance.AllowUserToOrderColumns = True
                    dgvVariance.AllowUserToResizeRows = True
                    dgvVariance.ColumnHeadersHeight = "35"
                    dgvVariance.Columns("strairsnumber").HeaderText = "AIRS #"
                    dgvVariance.Columns("strairsnumber").DisplayIndex = 0
                    dgvVariance.Columns("Difference2006").HeaderText = "Difference"
                    dgvVariance.Columns("Difference2006").DisplayIndex = 1
                    dgvVariance.Columns("Fee2005").HeaderText = "2005 Fee"
                    dgvVariance.Columns("Fee2005").DisplayIndex = 2
                    dgvVariance.Columns("Fee2006").HeaderText = "2006 Fee"
                    dgvVariance.Columns("Fee2006").DisplayIndex = 3
                    dgvVariance.Columns("Vcheck2006").HeaderText = "Verified?"
                    dgvVariance.Columns("Vcheck2006").DisplayIndex = 4
                    dgvVariance.Columns("comments2006").HeaderText = "Comments"
                    dgvVariance.Columns("comments2006").DisplayIndex = 5
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub
    Sub LoadDataGridVerified()
        Dim SQL As String = ""
        Try

            dsWorkEnTry = New DataSet

            If Not dsWorkEnTry.Tables("tblworkentry") Is Nothing Then
                dsWorkEnTry.Tables("tblworkentry").Clear()
                dsWorkEnTry.Tables.Remove("tblworkentry")
                dsWorkEnTry.AcceptChanges()
            End If

            Select Case cboFeeYear3.Text
                Case 2005
                    SQL = "Select strairsnumber, Difference2005, Fee2004, Fee2005, " _
                        + "Vcheck2005, comments2005 from airbranch.feevariance " _
                        + "where Difference2005 <> 0 and vcheck2005 = 'YES'"

                Case 2006
                    SQL = "Select strairsnumber, Difference2006, Fee2005, Fee2006, " _
                        + "Vcheck2006, comments2006 from airbranch.feevariance " _
                        + "where Difference2006 <> 0 and vcheck2006 = 'YES'"
            End Select

            daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
            dgvVariance.DataSource = dsWorkEnTry
            dgvVariance.DataMember = "tblWorkEntry"
            txtVarianceCount.Text = dgvVariance.RowCount.ToString

            Select Case cboFeeYear3.Text
                Case 2005
                    dgvVariance.RowHeadersVisible = False
                    dgvVariance.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvVariance.AllowUserToResizeColumns = True
                    dgvVariance.AllowUserToAddRows = False
                    dgvVariance.AllowUserToDeleteRows = False
                    dgvVariance.AllowUserToOrderColumns = True
                    dgvVariance.AllowUserToResizeRows = True
                    dgvVariance.ColumnHeadersHeight = "35"
                    dgvVariance.Columns("strairsnumber").HeaderText = "AIRS #"
                    dgvVariance.Columns("strairsnumber").DisplayIndex = 0
                    dgvVariance.Columns("Difference2005").HeaderText = "Difference"
                    dgvVariance.Columns("Difference2005").DisplayIndex = 1
                    dgvVariance.Columns("Fee2004").HeaderText = "2004 Fee"
                    dgvVariance.Columns("Fee2004").DisplayIndex = 2
                    dgvVariance.Columns("Fee2005").HeaderText = "2005 Fee"
                    dgvVariance.Columns("Fee2005").DisplayIndex = 3
                    dgvVariance.Columns("Vcheck2005").HeaderText = "Verified?"
                    dgvVariance.Columns("Vcheck2005").DisplayIndex = 4
                    dgvVariance.Columns("comments2005").HeaderText = "Comments"
                    dgvVariance.Columns("comments2005").DisplayIndex = 5
                Case 2006
                    dgvVariance.RowHeadersVisible = False
                    dgvVariance.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvVariance.AllowUserToResizeColumns = True
                    dgvVariance.AllowUserToAddRows = False
                    dgvVariance.AllowUserToDeleteRows = False
                    dgvVariance.AllowUserToOrderColumns = True
                    dgvVariance.AllowUserToResizeRows = True
                    dgvVariance.ColumnHeadersHeight = "35"
                    dgvVariance.Columns("strairsnumber").HeaderText = "AIRS #"
                    dgvVariance.Columns("strairsnumber").DisplayIndex = 0
                    dgvVariance.Columns("Difference2006").HeaderText = "Difference"
                    dgvVariance.Columns("Difference2006").DisplayIndex = 1
                    dgvVariance.Columns("Fee2005").HeaderText = "2005 Fee"
                    dgvVariance.Columns("Fee2005").DisplayIndex = 2
                    dgvVariance.Columns("Fee2006").HeaderText = "2006 Fee"
                    dgvVariance.Columns("Fee2006").DisplayIndex = 3
                    dgvVariance.Columns("Vcheck2006").HeaderText = "Verified?"
                    dgvVariance.Columns("Vcheck2006").DisplayIndex = 4
                    dgvVariance.Columns("comments2006").HeaderText = "Comments"
                    dgvVariance.Columns("comments2006").DisplayIndex = 5
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub PASPFeeVarianceCheck_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Dispose()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim dt As DataTable
            Dim airsno As String
            Dim varcheck As String
            Dim comments As String
            dt = dsWorkEnTry.Tables("tblWorkEntry").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 4 To 5
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        airsno = "0413" & Row(0, DataRowVersion.Original).ToString
                                        varcheck = Row(4, DataRowVersion.Current).ToString
                                        comments = Row(5, DataRowVersion.Current).ToString
                                        UpdateRecords(airsno, varcheck, comments)
                                        'Exit For
                                    End If
                                End If
                            Next
                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            dgvVariance.DataSource = Nothing
            dgvVariance.Refresh()
            txtVarianceCount.Text = "0"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub UpdateRecords(ByVal airsno As String, ByVal varcheck As String, ByVal comments As String)

        Try
            SQL = "Update AIRBRANCH.FSCalculations set " _
            + "variancecheck = '" & UCase(varcheck) & "', " _
            + "variancecomments = '" & Replace(comments, "'", "''") & "' " _
            + "where strairsnumber = '" & airsno & "' and " _
            + "intyear = '" & CInt(cboFeeYear3.Text) & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub lblVerified_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblVerified.LinkClicked
        Try

            LoadDataGridVerified()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblNotVerified_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNotVerified.LinkClicked
        Try

            LoadDataGridNotVerified()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Final Submissions and Bankrupcies"
    Private Sub ClearPage3()
        Try

            cboFacilityName3.Text = ""
            cboAirsNo3.Text = ""
            chkBankrupt.Checked = False
            chkFinal.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadComboBoxes2()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            SQL = "Select DISTINCT substr(AIRBRANCH.FSAddPaid.strairsnumber, 5) as strairsnumber, " & _
            "AIRBRANCH.APBFacilityInformation.strfacilityname " & _
            "from AIRBRANCH.FSAddPaid, AIRBRANCH.APBFacilityInformation " & _
            "where AIRBRANCH.FSAddPaid.strairsnumber = AIRBRANCH.APBFacilityInformation.strairsnumber " & _
            "order by AIRBRANCH.APBFacilityInformation.strFacilityName "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "facilityInfo")

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

            With cboAirsNo3
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName3
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewAll3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll3.LinkClicked
        Try


            SQL = "Select isbankrupt from AIRBRANCH.APBSupplamentalData " _
                + "where strairsnumber = '0413" & cboAirsNo3.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                If dr.IsDBNull(0) Then
                    chkBankrupt.Checked = False
                Else
                    If dr.Item("isbankrupt") = "NO" Then
                        chkBankrupt.Checked = False
                    Else
                        chkBankrupt.Checked = True
                    End If
                End If
            End If
            dr.Close()

            SQL = "Select intsubmittal " & _
            "from AIRBRANCH.FSPayAndSubmit " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
            "and intyear = '" & CInt(txtYear3.Text) & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                If dr.Item("intsubmittal") = 0 Then
                    chkFinal.Checked = False
                Else
                    chkFinal.Checked = True
                End If
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnSave3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave3.Click
        Dim bankrupt As String

        Try


            If chkBankrupt.Checked = True Then
                bankrupt = "YES"
            Else
                bankrupt = "NO"
            End If

            SQL = "Update AIRBRANCH.APBSupplamentalData set " _
                + "isbankrupt = '" & bankrupt & "' " _
                + "where strairsnumber = '0413" & cboAirsNo3.Text & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            cmd.ExecuteNonQuery()

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            If chkFinal.Checked = True Then
                FinalSubmitAdd()
            Else
                FinalSubmitRemove()
            End If

            MsgBox("The facility information has been updated.", MsgBoxStyle.Information, "Update Success")
            ClearPage3()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub FinalSubmitAdd()
        Try

            Dim confirmation As String
            confirmation = cboAirsNo3.Text & "-" & Now
            Dim SQL As String = "Update AIRBRANCH.FSPayAndSubmit set " & _
            "intsubmittal = '1' " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
            "and intyear = '" & CInt(txtYear3.Text) & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            SQL = "Insert into AIRBRANCH.FSConfirmation (" & _
            "strairsnumber, intyear, strconfirmation, numuserid, datconfirmation) values(" & _
            "'0413" & cboAirsNo3.Text & "', '" & CInt(txtYear3.Text) & "', " & _
            "'" & confirmation & "', '" & UserGCode & "', " & _
            "to_date('" & Format$(Now, "dd-MMM-yyyy hh:mm:ss") & "', 'dd-mon-yyyy hh:mi:ss'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub FinalSubmitRemove()
        Try


            SQL = "Update AIRBRANCH.FSPayAndSubmit set " & _
             "intsubmittal = '0' " & _
             "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
             "and intyear = '" & CInt(txtYear3.Text) & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete from AIRBRANCH.FSConfirmation " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
             "and intyear = '" & CInt(txtYear3.Text) & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        dgvDeposit.ExportToExcel(Me)
    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try
            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try
            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try
            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try
            ClearPage()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Fee Deposits"
    Sub LoadFacilityData(ByVal AIRSNumber As String)
        Try
            SQL = "Select " & _
            "strAIRSNumber, strFacilityName " & _
            "from AIRBRANCH.APBFacilityInformation " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    lblFacilityName.Text = "Facility Name"
                Else
                    lblFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Sub ValidateData()
        Try
            If mtbAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strAIRSNumber " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                    "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                    ValidatingState = False
                    Exit Sub
                End If
            Else
                MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                 "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                ValidatingState = False
                Exit Sub
            End If
            If mtbFeeYear2.Text = "" Then
                If IsNumeric(mtbFeeYear.Text) Then
                Else
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    ValidatingState = False
                    Exit Sub
                End If
            End If
            If txtDepositAmount.Text = "" Then
                MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
                ValidatingState = False
                Exit Sub
            End If
            If txtDepositNumberField.Text = "" Then
                MsgBox("Please enter a Deposit Number", MsgBoxStyle.OkOnly, "Incorrect Deposit No.")
                ValidatingState = False
                Exit Sub
            End If
            If txtBatchNoField.Text = "" Then
                MsgBox("Please enter a Batch Number", MsgBoxStyle.OkOnly, "Incorrect Batch No.")
                ValidatingState = False
                Exit Sub
            End If
            If txtCheckNumberField.Text = "" Then
                MsgBox("Please enter a Check Number", MsgBoxStyle.OkOnly, "Incorrect Check No.")
                ValidatingState = False
                Exit Sub
            End If
            If txtInvoiceForDeposit.Text = "" Then
                MsgBox("Please select an Invoice Number", MsgBoxStyle.OkOnly, "No Invoice No.")
                ValidatingState = False
                Exit Sub
            Else
                SQL = "Select InvoiceID from AIRBranch.FS_FeeInvoice " & _
                "where invoiceID = '" & txtInvoiceForDeposit.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("Please select a valid Invoice Number", MsgBoxStyle.OkOnly, "No Valid Invoice No.")
                    ValidatingState = False
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DepositSearch()
        Try

            ds = New DataSet

            SQL = "select " & _
            "substr(AIRBRANCH.FS_Transactions.strAIRSNumber, 5) as strAIRSNumber, " & _
            "strDepositNO, strBatchNo, " & _
            "transactionID, datTransactionDate, " & _
            "numPayment, AIRBRANCH.FS_Transactions.numFeeYear, " & _
            "strCheckNo, strCreditCardNo, " & _
            "AIRBRANCH.FS_Transactions.InvoiceID, strPaytypeDesc as strPayType, " & _
            "AIRBRANCH.FS_Transactions.strComment " & _
            "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice, " & _
            "AIRBRANCH.FSLK_PayType " & _
            "where AIRBRANCH.FS_Transactions.InvoiceID = AIRBRANCH.FS_FeeInvoice.INvoiceID " & _
            "and AIRBRANCH.FS_FeeInvoice.strPayType = AIRBRANCH.FSLK_PayType.numPaytypeID  " & _
            "and strDepositNo = '" & txtDepositNumber.Text & "' " & _
            "and AIRBRANCH.FS_Transactions.Active = '1' " & _
            "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
            "order by strBatchNo "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "Deposit")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DeleteInvoice()
        Try

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you sure you want to remove " & lblInvoiceNumber.Text & " for AIRS # - " & mtbAIRSNumber.Text & "?", _
              "PASP Fee Tool", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case DialogResult.Yes

                Case Else
                    Exit Sub
            End Select

            SQL = "Delete AIRBRANCH.FSAddPaid " & _
            "where intPayID = '" & txtTransactionID.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()

            txtTransactionID.Clear()
            txtDepositComments.Clear()
            lblInvoiceNumber.Text = "Invoice #"
            txtDepositAmount.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = OracleDate
            cboDepositPayType.Text = ""

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewInvoices()
        Try

            dsInvoice = New DataSet
            If txtCheckNumber.Text <> "" Then
                'Old code from old fee tables 
                'SQL = "Select  " & _
                '  "substr( AIRBRANCH.FSAddPaid.strAIRSNumber, 5) as strAIRSnumber,  " & _
                '  "strDepositNo, datPayDate,  " & _
                '  "numPayment,  AIRBRANCH.FSAddPaid.intYear,  " & _
                '  "strCheckNo, strBatchNo,  " & _
                '  "strPayType, intPayId,  " & _
                '  "strComments, strInvoiceNo, " & _
                '  "case " & _
                '  "when numtotalfee is null then 0 " & _
                '  "when strInvoiceNo like '%Q%' then numtotalfee/4 " & _
                '  "else numtotalfee " & _
                '  "end FeeDue " & _
                '  "from  AIRBRANCH.FSAddPaid, AIRBRANCH.FSCalculations   " & _
                '  "where  AIRBRANCH.fsaddpaid.strairsnumber = AIRBRANCH.FSCalculations.strairsnumber (+) " & _
                '  "and AIRBRANCH.fsaddpaid.intyear = AIRBRANCH.FSCalculations.intyear (+) " & _
                '  "and AIRBRANCH.FSAddPaid.strCheckNo like '%" & Replace(txtCheckNumber.Text, "'", "''") & "%'  " & _
                '  "order by strBatchNo  "

                SQL = "select " & _
                "substr(AIRBRANCH.FS_FeeInvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                "strDepositNo, datTransactionDate, " & _
                "numPayment, AIRBRANCH.FS_FeeInvoice.numFeeYear, " & _
                "strCheckNo, strBatchNo, " & _
                "Description, TransactionID, " & _
                "AIRBRANCH.FS_Transactions.strComment, AIRBRANCH.FS_FeeInvoice.InvoiceID, " & _
                "case " & _
                "when AIRBRANCH.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
                "when AIRBRANCH.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 " & _
                "else numAmount " & _
                "end FeeDue " & _
                "from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice, " & _
                "AIRBRANCH.FSLK_TransactionType  " & _
                "where AIRBRANCH.FS_FeeInvoice.InvoiceID = AIRBRANCH.FS_Transactions.INvoiceID (+) " & _
                "and AIRBRANCH.FS_Transactions.transactionTypeCode = AIRBRANCH.FSLK_TransactionType.TransactionTypeCode (+) " & _
                "and strCheckNo like '%" & Replace(txtCheckNumber.Text, "'", "''") & "%'  " & _
                "and AIRBRANCH.FS_Transactions.Active = '1' " & _
                "and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                "order by strBatchNo  "
            Else

                'SQL = "select " & _
                '"substr(AIRBRANCH.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                '"strDepositNo, datTransactionDate, " & _
                '"numPayment, AIRBRANCH.FS_FeeINvoice.numFeeYear, " & _
                '"strCheckNo, strBatchNo, " & _
                '"Description, TransactionID, " & _
                '"AIRBRANCH.FS_Transactions.strComment, AIRBRANCH.FS_FeeINvoice.InvoiceID, " & _
                '"case " & _
                '"when AIRBRANCH.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
                '"when AIRBRANCH.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 " & _
                '"else numAmount " & _
                '"end FeeDue " & _
                '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice, " & _
                '"AIRBRANCH.FSLK_TransactionType  " & _
                '"where AIRBRANCH.FS_FeeINvoice.InvoiceID = AIRBRANCH.FS_Transactions.INvoiceID (+) " & _
                '"and AIRBRANCH.FS_Transactions.transactionTypeCode = AIRBRANCH.FSLK_TransactionType.TransactionTypeCode (+) " & _
                '"and AIRBRANCH.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
                '"and AIRBRANCH.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' " & _
                '"and AIRBRANCH.FS_Transactions.Active = '1' " & _
                '"and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                '"order by strBatchNo  "

                SQL = "select " & _
 "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " & _
 "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " & _
 " strComment,  ALLInvoices.InvoiceID,  " & _
 "FeeDue  " & _
 "from  " & _
 "(select substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
 "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
 "from  AIRBranch.FS_FeeInvoice " & _
 "where AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
 "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' " & _
 "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
 "union " & _
 "select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
 "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
 "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
 "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
 "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
 "and AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
 "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' and AIRBranch.FS_FeeInvoice.Active = '1' " & _
 "and AIRBranch.FS_Transactions.Active = '1'  ) ALLInvoices,  " & _
 "(select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, strDepositNo, datTransactionDate, " & _
 "numPayment, AIRBranch.FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " & _
 "AIRBranch.FS_Transactions.strComment, AIRBranch.FS_FeeINvoice.InvoiceID, " & _
 "case when AIRBranch.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
 "when AIRBranch.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " & _
 "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
 "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
 "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
 "and AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
 "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' " & _
 "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
 "and AIRBranch.FS_Transactions.Active = '1' order by strBatchNo) Transactions " & _
 "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "

            End If
            daInvoice = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daInvoice.Fill(dsInvoice, "Deposit")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchDeposits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDeposits.Click
        Try
            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub lblViewInvoices_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewInvoices.LinkClicked
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cboDepositPayType.Text = ""
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            lblInvoiceNumber.Text = "Invoice #"
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = OracleDate
            txtCheckNumber.Clear()

            If mtbAIRSNumber.Text <> "" Then
                If mtbFeeYear.Text <> "" Then
                    lblViewInvoices.Enabled = False

                    bgwInvoices.WorkerReportsProgress = True
                    bgwInvoices.WorkerSupportsCancellation = True
                    bgwInvoices.RunWorkerAsync()
                Else
                    MsgBox("Select a year to check for invoices.", MsgBoxStyle.Information, "PASP Deposit Amendments")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvDeposits_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposits.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvDeposits.HitTest(e.X, e.Y)
            Dim temp As String

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposits.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvDeposits.Columns(1).HeaderText

                    If dgvDeposits.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvDeposits(0, hti.RowIndex).Value
                        LoadFacilityData(dgvDeposits(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvDeposits(0, hti.RowIndex).Value
                        If IsDBNull(dgvDeposits(3, hti.RowIndex).Value) Then
                        Else
                            txtTransactionID.Text = dgvDeposits(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(2, hti.RowIndex).Value) Then
                            txtBatchNumber.Clear()
                        Else
                            txtBatchNumber.Text = dgvDeposits(2, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(2, hti.RowIndex).Value) Then
                            txtBatchNoField.Clear()
                            If txtBatchNumber.Text <> "" Then
                                txtBatchNoField.Text = txtBatchNumber.Text
                            End If
                        Else
                            txtBatchNoField.Text = dgvDeposits(2, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(4, hti.RowIndex).Value) Then
                            dtpBatchDepositDate.Text = OracleDate
                            DTPBatchDepositDateField.Text = OracleDate
                        Else
                            dtpBatchDepositDate.Text = dgvDeposits(4, hti.RowIndex).FormattedValue
                            DTPBatchDepositDateField.Text = dgvDeposits(4, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposits(5, hti.RowIndex).Value) Then
                            txtDepositAmount.Clear()
                        Else
                            txtDepositAmount.Text = dgvDeposits(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(6, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            mtbFeeYear2.Clear()
                        Else
                            mtbFeeYear.Text = dgvDeposits(6, hti.RowIndex).Value
                            mtbFeeYear2.Text = dgvDeposits(6, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(11, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvDeposits(11, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(10, hti.RowIndex).Value) Then
                            cboDepositPayType.Text = ""
                        Else
                            cboDepositPayType.Text = ""
                            cboDepositPayType.Text = dgvDeposits(10, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvDeposits(9, hti.RowIndex).Value) Then
                            lblInvoiceNumber.Text = "Invoice #"
                            txtSearchInvoice.Text = ""
                            txtInvoiceForDeposit.Clear()
                        Else
                            lblInvoiceNumber.Text = "Invoice #: " & dgvDeposits(9, hti.RowIndex).Value
                            txtSearchInvoice.Text = dgvDeposits(9, hti.RowIndex).Value
                            txtInvoiceForDeposit.Text = dgvDeposits(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(7, hti.RowIndex).Value) Then
                            txtCheckNumber.Clear()
                        Else
                            txtCheckNumber.Text = dgvDeposits(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(7, hti.RowIndex).Value) Then
                            txtCheckNumberField.Clear()
                            If txtCheckNumber.Text <> "" Then
                                txtCheckNumberField.Text = txtCheckNumber.Text
                            End If
                        Else
                            txtCheckNumberField.Text = dgvDeposits(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposits(1, hti.RowIndex).Value) Then
                            txtDepositNumberField.Clear()
                            If txtDepositNumber.Text <> "" Then
                                txtDepositNumberField.Text = txtDepositNumber.Text
                            End If
                        Else
                            txtDepositNumberField.Text = dgvDeposits(1, hti.RowIndex).Value
                        End If


                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvInvoices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInvoices.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvInvoices.HitTest(e.X, e.Y)
            Dim temp As String

            If hti.Type = DataGrid.HitTestType.Cell Then
                If dgvInvoices.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvInvoices.Columns(1).HeaderText

                    If dgvInvoices.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAIRSNumber.Text = dgvInvoices(0, hti.RowIndex).Value
                        LoadFacilityData(dgvInvoices(0, hti.RowIndex).Value)
                        lblAIRSNumber.Text = "AIRS #: " & dgvInvoices(0, hti.RowIndex).Value
                        If IsDBNull(dgvInvoices(8, hti.RowIndex).Value) Then
                            txtTransactionID.Text = ""
                        Else
                            txtTransactionID.Text = dgvInvoices(8, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(1, hti.RowIndex).Value) Then
                            txtDepositNumberField.Clear()
                            If txtDepositNumber.Text <> "" Then
                                txtDepositNumberField.Text = txtDepositNumber.Text
                            End If
                        Else
                            txtDepositNumberField.Text = dgvInvoices(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(11, hti.RowIndex).Value) Then
                            If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                                txtDepositAmount.Clear()
                            Else
                                txtDepositAmount.Text = dgvInvoices(3, hti.RowIndex).Value
                            End If
                        Else
                            If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                                txtDepositAmount.Text = dgvInvoices(11, hti.RowIndex).Value
                            Else
                                If dgvInvoices(3, hti.RowIndex).Value = 0 Then
                                    txtDepositAmount.Text = dgvInvoices(11, hti.RowIndex).Value
                                Else
                                    txtDepositAmount.Text = dgvInvoices(3, hti.RowIndex).Value
                                End If
                            End If
                        End If

                        If IsDBNull(dgvInvoices(4, hti.RowIndex).Value) Then
                            mtbFeeYear.Clear()
                            mtbFeeYear2.Clear()
                        Else
                            mtbFeeYear.Text = dgvInvoices(4, hti.RowIndex).Value
                            mtbFeeYear2.Text = dgvInvoices(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(5, hti.RowIndex).Value) Then
                            txtCheckNumberField.Clear()
                            If txtCheckNumber.Text <> "" Then
                                txtCheckNumberField.Text = txtCheckNumber.Text
                            End If
                        Else
                            txtCheckNumberField.Text = dgvInvoices(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(6, hti.RowIndex).Value) Then
                            txtBatchNoField.Clear()
                            If txtBatchNumber.Text <> "" Then
                                txtBatchNoField.Text = txtBatchNumber.Text
                            End If
                        Else
                            txtBatchNoField.Text = dgvInvoices(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(7, hti.RowIndex).Value) Then
                            cboDepositPayType.Text = ""
                        Else
                            cboDepositPayType.Text = ""
                            cboDepositPayType.Text = dgvInvoices(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(9, hti.RowIndex).Value) Then
                            txtDepositComments.Clear()
                        Else
                            txtDepositComments.Text = dgvInvoices(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(10, hti.RowIndex).Value) Then
                            lblInvoiceNumber.Text = "Invoice #"
                            txtInvoiceForDeposit.Clear()
                        Else
                            lblInvoiceNumber.Text = "Invoice #: " & dgvInvoices(10, hti.RowIndex).Value
                            txtInvoiceForDeposit.Text = dgvInvoices(10, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInvoices(2, hti.RowIndex).Value) Then
                            DTPBatchDepositDateField.Text = dtpBatchDepositDate.Text
                        Else
                            DTPBatchDepositDateField.Text = dgvInvoices(2, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewCheckDeposit.Click
        Try
            '  Dim InvoiceNumber As String = ""
            Dim PayId As String = ""
            Dim DepositNo As String = ""
            Dim Submittal As String = ""

            ValidatingState = True
            ValidateData()
            If ValidatingState = False Then
                Exit Sub
            End If
            If ValidatingState = True Then
                If txtTransactionID.Text = "" Then
                    SQL = "Insert into AIRBRANCH.FS_Transactions " & _
                    "values " & _
                    "((AIRBRANCH.seq_fs_transactions.nextVal), " & _
                    "'" & Replace(txtInvoiceForDeposit.Text, "'", "''") & "', " & _
                    "'1', '" & DTPBatchDepositDateField.Text & "', " & _
                    "'" & Replace(Replace(Replace(txtDepositAmount.Text, "'", "''"), ",", ""), "$", "") & "', " & _
                    "'" & Replace(txtCheckNumberField.Text, "'", "''") & "', " & _
                    "'" & Replace(txtDepositNumberField.Text, "'", "''") & "', '" & Replace(txtBatchNoField.Text, "'", "''") & "', " & _
                    "'" & UserGCode & "', '" & Replace(txtDepositComments.Text, "'", "''") & "', " & _
                    "'1', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '" & OracleDate & "', " & _
                    "'0413" & mtbAIRSNumber.Text & "', " & _
                    "'" & mtbFeeYear2.Text & "', '" & Replace(txtCreditCardNo.Text, "'", "''") & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    InvoiceStatusCheck(txtInvoiceForDeposit.Text)

                Else
                    MsgBox("Use the Update Existing Check Deposit instead.", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                If Not DAL.Update_FS_Admin_Status(mtbFeeYear2.Text, mtbAIRSNumber.Text) Then
                    MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                btnSearchDeposits.Enabled = False

                bgwDeposits.WorkerReportsProgress = True
                bgwDeposits.WorkerSupportsCancellation = True
                bgwDeposits.RunWorkerAsync()

                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()

                ClearForm()
                MsgBox("The record was added successfully", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub InvoiceStatusCheck(ByVal invoiceID As String)
        Try
            SQL = "select " & _
            "(invoiceTotal - PaymentTotal) as Balance " & _
            "from (select " & _
            "sum(numAmount) as InvoiceTotal " & _
            "from airbranch.FS_Feeinvoice " & _
            "where invoiceid = '" & invoiceID & "' " & _
            "and Active = '1' ) INVOICED, " & _
            "(select " & _
            "sum(numPayment) as PaymentTotal " & _
            "from airbranch.FS_TRANSACTIONS " & _
            "where invoiceid = '" & invoiceID & "' " & _
            "and Active = '1' ) Payments "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("Balance")) Then
                    temp = "1"
                Else
                    temp = dr.Item("Balance")
                End If
            End While
            dr.Close()

            If temp <> "0" Then
                SQL = "Update AIRBRANCH.FS_FeeInvoice set " & _
                "strInvoicestatus = '0' " & _
                "where invoiceId = '" & invoiceID & "' "
            Else
                SQL = "Update AIRBRANCH.FS_FeeInvoice set " & _
                "strInvoicestatus = '1' " & _
                "where invoiceId = '" & invoiceID & "' "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateExistingDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateExistingDeposit.Click
        Try
            Dim InvoiceNumber As String = ""
            If txtTransactionID.Text <> "" Then
                ValidatingState = True
                ValidateData()
                If ValidatingState = True Then
                    If ValidatingState = True Then

                        If txtTransactionID.Text <> "" Then
                            SQL = "Update AIRBRANCH.FS_Transactions set " & _
                            "invoiceid = '" & txtInvoiceForDeposit.Text & "', " & _
                            "TransactionTypecode = '1', " & _
                            "datTransactionDate = '" & DTPBatchDepositDateField.Text & "', " & _
                            "numPayment = '" & Replace(Replace(Replace(txtDepositAmount.Text, "'", "''"), ",", ""), "$", "") & "', " & _
                            "strCheckNo = '" & txtCheckNumberField.Text & "', " & _
                            "strDepositNo = '" & txtDepositNumberField.Text & "', " & _
                            "strBatchNo = '" & txtBatchNoField.Text & "', " & _
                            "strComment = '" & Replace(txtDepositComments.Text, "'", "''") & "', " & _
                            "active = '1', " & _
                            "updateUser = '" & UserGCode & "', " & _
                            "updateDateTime = sysdate, " & _
                            "strCreditCardNo = '" & txtCreditCardNo.Text & "' " & _
                            "where TransactionID = '" & txtTransactionID.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        Else
                            MsgBox("Use the Add New Check Deposit.", MsgBoxStyle.Information, Me.Text)
                            Exit Sub
                        End If

                        If Not DAL.Update_FS_Admin_Status(mtbFeeYear2.Text, mtbAIRSNumber.Text) Then
                            MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        InvoiceStatusCheck(txtInvoiceForDeposit.Text)

                        btnSearchDeposits.Enabled = False

                        bgwDeposits.WorkerReportsProgress = True
                        bgwDeposits.WorkerSupportsCancellation = True
                        bgwDeposits.RunWorkerAsync()

                        lblViewInvoices.Enabled = False

                        bgwInvoices.WorkerReportsProgress = True
                        bgwInvoices.WorkerSupportsCancellation = True
                        bgwInvoices.RunWorkerAsync()

                        ClearForm()
                        MsgBox("The record has been updated successfully", MsgBoxStyle.Information, Me.Text)
                    Else
                    End If
                End If

            Else
                MsgBox("Please select an existing record from either of the two list.", MsgBoxStyle.Information, "PASP Deposits and Amendments")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteCheckDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCheckDeposit.Click
        Try
            Dim Result As DialogResult
            If txtTransactionID.Text = "" Then
                MsgBox("Select a transaction first.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Result = MessageBox.Show("Are you sure you want to remove " & txtCheckNumberField.Text & _
                                     " for AIRS # - " & mtbAIRSNumber.Text & "?", _
              "PASP Fee Tool", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case DialogResult.Yes
                    SQL = "Update AIRBRANCH.FS_Transactions set " & _
                    "active = '0' " & _
                    "where TransactionId = '" & txtTransactionID.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    cmd.CommandType = CommandType.Text
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader

                    btnSearchDeposits.Enabled = False

                    bgwDeposits.WorkerReportsProgress = True
                    bgwDeposits.WorkerSupportsCancellation = True
                    bgwDeposits.RunWorkerAsync()

                    lblViewInvoices.Enabled = False

                    bgwInvoices.WorkerReportsProgress = True
                    bgwInvoices.WorkerSupportsCancellation = True
                    bgwInvoices.RunWorkerAsync()

                    txtCheckNumber.Clear()
                    lblAIRSNumber.Text = "AIRS #"
                    lblFacilityName.Text = "Facility Name"
                    cboDepositPayType.Text = ""
                    mtbFeeYear2.Clear()
                    txtDepositAmount.Clear()
                    lblInvoiceNumber.Text = "Invoice #"
                    txtTransactionID.Clear()
                    txtDepositComments.Clear()
                    txtDepositNumberField.Clear()
                    txtBatchNoField.Clear()
                    txtCheckNumberField.Clear()
                    DTPBatchDepositDateField.Text = OracleDate

                    MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, Me.Text)

                Case Else
                    Exit Sub
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteInventoryRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteInventoryRecords.Click
        Try
            If txtTransactionID.Text <> "" Then
                DeleteInvoice()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwDeposits_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwDeposits.DoWork
        Try

            DepositSearch()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwDeposits_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDeposits.RunWorkerCompleted
        Try
            dgvDeposits.DataSource = ds
            dgvDeposits.DataMember = "Deposit"

            dgvDeposits.RowHeadersVisible = False
            dgvDeposits.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDeposits.AllowUserToResizeColumns = True
            dgvDeposits.AllowUserToAddRows = False
            dgvDeposits.AllowUserToDeleteRows = False
            dgvDeposits.AllowUserToOrderColumns = True
            dgvDeposits.AllowUserToResizeRows = True
            dgvDeposits.ColumnHeadersHeight = "35"
            dgvDeposits.Columns("strairsnumber").HeaderText = "AIRS Number"
            dgvDeposits.Columns("strairsnumber").DisplayIndex = 0
            dgvDeposits.Columns("strdepositno").HeaderText = "Deposit Number"
            dgvDeposits.Columns("strdepositno").DisplayIndex = 1
            dgvDeposits.Columns("datTransactionDate").HeaderText = "Transaction Date"
            dgvDeposits.Columns("datTransactionDate").DisplayIndex = 2
            dgvDeposits.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDeposits.Columns("numpayment").HeaderText = "Payment"
            dgvDeposits.Columns("numpayment").DisplayIndex = 3
            dgvDeposits.Columns("numpayment").DefaultCellStyle.Format = "c"
            dgvDeposits.Columns("nuMFeeyear").HeaderText = "Year"
            dgvDeposits.Columns("numFeeyear").DisplayIndex = 4
            dgvDeposits.Columns("strcheckno").HeaderText = "Check Number"
            dgvDeposits.Columns("strcheckno").DisplayIndex = 5
            dgvDeposits.Columns("strCreditCardNo").HeaderText = "Credit Card Conf. #"
            dgvDeposits.Columns("strCreditCardNo").DisplayIndex = 6

            dgvDeposits.Columns("strbatchno").HeaderText = "Batch Number"
            dgvDeposits.Columns("strbatchno").DisplayIndex = 7
            dgvDeposits.Columns("strpaytype").HeaderText = "Payment Type"
            dgvDeposits.Columns("strpaytype").DisplayIndex = 8
            dgvDeposits.Columns("TransactionID").HeaderText = "Transaction ID"
            dgvDeposits.Columns("TransactionID").DisplayIndex = 9
            dgvDeposits.Columns("TransactionID").Visible = True
            dgvDeposits.Columns("strcomment").HeaderText = "Comments"
            dgvDeposits.Columns("strcomment").DisplayIndex = 10
            dgvDeposits.Columns("strcomment").Visible = False
            dgvDeposits.Columns("invoiceID").HeaderText = "Invoice Number"
            dgvDeposits.Columns("invoiceID").DisplayIndex = 11
            dgvDeposits.Columns("invoiceID").Visible = True

            txtDepositCount.Text = dgvDeposits.RowCount.ToString
            btnSearchDeposits.Enabled = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwInvoices_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwInvoices.DoWork
        Try

            ViewInvoices()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwInvoices_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInvoices.RunWorkerCompleted
        Try
            dgvInvoices.DataSource = dsInvoice
            dgvInvoices.DataMember = "Deposit"

            dgvInvoices.RowHeadersVisible = False
            dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInvoices.AllowUserToResizeColumns = True
            dgvInvoices.AllowUserToAddRows = False
            dgvInvoices.AllowUserToDeleteRows = False
            dgvInvoices.AllowUserToOrderColumns = True
            dgvInvoices.AllowUserToResizeRows = True
            dgvInvoices.ColumnHeadersHeight = "35"
            dgvInvoices.Columns("strairsnumber").HeaderText = "AIRS Number"
            dgvInvoices.Columns("strairsnumber").DisplayIndex = 0
            dgvInvoices.Columns("strdepositno").HeaderText = "Deposit Number"
            dgvInvoices.Columns("strdepositno").DisplayIndex = 1
            dgvInvoices.Columns("datTransactionDate").HeaderText = "Deposit Date"
            dgvInvoices.Columns("datTransactionDate").DisplayIndex = 2
            dgvInvoices.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInvoices.Columns("numpayment").HeaderText = "Payment"
            dgvInvoices.Columns("numpayment").DisplayIndex = 3
            dgvInvoices.Columns("numpayment").DefaultCellStyle.Format = "c"
            dgvInvoices.Columns("numFeeyear").HeaderText = "Year"
            dgvInvoices.Columns("numFeeyear").DisplayIndex = 4
            dgvInvoices.Columns("strcheckno").HeaderText = "Check Number"
            dgvInvoices.Columns("strcheckno").DisplayIndex = 5
            dgvInvoices.Columns("strbatchno").HeaderText = "Batch Number"
            dgvInvoices.Columns("strbatchno").DisplayIndex = 6
            dgvInvoices.Columns("Description").HeaderText = "Payment Type"
            dgvInvoices.Columns("Description").DisplayIndex = 7
            dgvInvoices.Columns("TransactionID").HeaderText = "Transaction ID"
            dgvInvoices.Columns("TransactionID").DisplayIndex = 8
            dgvInvoices.Columns("TransactionID").Visible = False
            dgvInvoices.Columns("strcomment").HeaderText = "Comments"
            dgvInvoices.Columns("strcomment").DisplayIndex = 9
            dgvInvoices.Columns("strcomment").Visible = False
            dgvInvoices.Columns("InvoiceID").HeaderText = "Invoice Number"
            dgvInvoices.Columns("InvoiceID").DisplayIndex = 10
            dgvInvoices.Columns("InvoiceID").Visible = True
            dgvInvoices.Columns("FeeDue").HeaderText = "Fees Due"
            dgvInvoices.Columns("FeeDue").DisplayIndex = 11
            dgvInvoices.Columns("FeeDue").Visible = True
            txtCountInvoices.Text = dgvInvoices.RowCount.ToString
            lblViewInvoices.Enabled = True

            If mtbAIRSNumber.Text <> "" And dgvInvoices.RowCount = 0 Then
                SQL = "Select " & _
                "strFacilityName " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblAIRSNumber.Text = "AIRS #: "
                        lblFacilityName.Text = "Facility Name: "
                    Else
                        lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                        lblFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                    End If
                End While
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearForm()
        Try
            txtCheckNumber.Clear()
            mtbAIRSNumber.Clear()
            mtbFeeYear.Clear()
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cboDepositPayType.Text = ""
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            lblInvoiceNumber.Text = "Invoice #"
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = OracleDate
            txtCreditCardNo.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearEntryInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearEntryInformation.Click
        Try
            ClearForm()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


#End Region
    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        Try
            ClearForm()
            txtDepositNumber.Clear()
            txtBatchNumber.Clear()
            dtpBatchDepositDate.Text = OracleDate

            btnSearchDeposits.Enabled = False

            bgwDeposits.WorkerReportsProgress = True
            bgwDeposits.WorkerSupportsCancellation = True
            bgwDeposits.RunWorkerAsync()

            lblViewInvoices.Enabled = False

            bgwInvoices.WorkerReportsProgress = True
            bgwInvoices.WorkerSupportsCancellation = True
            bgwInvoices.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiRefreshDropDowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshDropDowns.Click
        Try
            LoadComboBoxes()
            ' FillComboBoxes()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboDepositPayType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDepositPayType.TextChanged
        Try
            If txtDepositNumberField.Text = "" Then
                txtDepositNumberField.Text = txtDepositNumber.Text
            End If
            If txtBatchNoField.Text = "" Then
                txtBatchNoField.Text = txtBatchNumber.Text
            End If
            If txtCheckNumberField.Text = "" Then
                txtCheckNumberField.Text = txtCheckNumber.Text
            End If
            If mtbFeeYear2.Text = "" Then
                mtbFeeYear2.Text = mtbFeeYear.Text
            End If

            If cboDepositPayType.Text = "ANNUAL" Or cboDepositPayType.Text.Contains("QUARTER") Then
                Select Case cboDepositPayType.Text
                    Case "ANNUAL"
                        lblInvoiceNumber.Text = "Invoice #: " & mtbAIRSNumber.Text & "-A1-" & mtbFeeYear.Text
                    Case "QUARTER ONE"
                        lblInvoiceNumber.Text = "Invoice #: " & mtbAIRSNumber.Text & "-Q1-" & mtbFeeYear.Text
                    Case "QUARTER TWO"
                        lblInvoiceNumber.Text = "Invoice #: " & mtbAIRSNumber.Text & "-Q2-" & mtbFeeYear.Text
                    Case "QUARTER THREE"
                        lblInvoiceNumber.Text = "Invoice #: " & mtbAIRSNumber.Text & "-Q3-" & mtbFeeYear.Text
                    Case "QUARTER FOUR"
                        lblInvoiceNumber.Text = "Invoice #: " & mtbAIRSNumber.Text & "-Q4-" & mtbFeeYear.Text
                    Case Else
                        lblInvoiceNumber.Text = "Invoice #: "
                End Select
            Else
                lblInvoiceNumber.Text = "Invoice #: "
            End If
            If DTPBatchDepositDateField.Text = OracleDate Then
                DTPBatchDepositDateField.Text = dtpBatchDepositDate.Text
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddInvoice.Click
        Try
            SQL = "select intPayID " & _
            "from AIRBRANCH.FSAddPaid " & _
            "where strAIRSNumber = '0413" & cboAirsNo2.Text & "' " & _
            "and intYear = '" & cboFeeYear2.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = False Then
                Select Case cboAmendmentsPayType.Text
                    Case "Entire Annual Year"
                        SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                        "values " & _
                        "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                        "'0', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', AIRBRANCH.seqFSDeposit.nextval, " & _
                        "'" & cboAirsNo2.Text & "-A1-" & cboFeeYear2.Text & "')"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Case "Four Quarterly Payments"
                        SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                        "values " & _
                        "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                        "'0', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', AIRBRANCH.seqFSDeposit.nextval, " & _
                        "'" & cboAirsNo2.Text & "-Q1-" & cboFeeYear2.Text & "')"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                        "values " & _
                        "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                        "'0', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', AIRBRANCH.seqFSDeposit.nextval, " & _
                        "'" & cboAirsNo2.Text & "-Q2-" & cboFeeYear2.Text & "')"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                        "values " & _
                        "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                        "'0', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', AIRBRANCH.seqFSDeposit.nextval, " & _
                        "'" & cboAirsNo2.Text & "-Q3-" & cboFeeYear2.Text & "')"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBRANCH.FSAddPaid " & _
                        "values " & _
                        "('0413" & cboAirsNo2.Text & "', '" & cboFeeYear2.Text & "', " & _
                        "'0', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', AIRBRANCH.seqFSDeposit.nextval, " & _
                        "'" & cboAirsNo2.Text & "-Q4-" & cboFeeYear2.Text & "')"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Case Else
                        MessageBox.Show("Please select a pay type from the dropdown.")
                        Exit Sub
                End Select
                MessageBox.Show("Invoice created")
            Else
                MessageBox.Show("There already exists an invoice for this AIRS number and Year.")
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbSearchForCheck_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForCheck.LinkClicked
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cboDepositPayType.Text = ""
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            lblInvoiceNumber.Text = "Invoice #"
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = OracleDate

            If txtCheckNumber.Text <> "" Then
                lblViewInvoices.Enabled = False

                bgwInvoices.WorkerReportsProgress = True
                bgwInvoices.WorkerSupportsCancellation = True
                bgwInvoices.RunWorkerAsync()
            Else
                MsgBox("You must enter a check # (Partial or complete).", MsgBoxStyle.Information, "PASP Deposit Amendments")

            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbSearchForInvoice_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForInvoice.LinkClicked
        Try
            lblAIRSNumber.Text = "AIRS #"
            lblFacilityName.Text = "Facility Name"
            cboDepositPayType.Text = ""
            mtbFeeYear2.Clear()
            txtDepositAmount.Clear()
            lblInvoiceNumber.Text = "Invoice #"
            txtTransactionID.Clear()
            txtDepositComments.Clear()
            txtDepositNumberField.Clear()
            txtBatchNoField.Clear()
            txtCheckNumberField.Clear()
            DTPBatchDepositDateField.Text = OracleDate

            If txtSearchInvoice.Text <> "" Then
                lblViewInvoices.Enabled = False

                dsInvoice = New DataSet
                If txtSearchInvoice.Text <> "" Then
                    'SQL = "select " & _
                    '"substr(AIRBRANCH.FS_FeeInvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                    '"strDepositNo, datTransactionDate, " & _
                    '"numPayment, AIRBRANCH.FS_FeeInvoice.numFeeYear, " & _
                    '"strCheckNo, strBatchNo, " & _
                    '"Description, TransactionID, " & _
                    '"AIRBRANCH.FS_Transactions.strComment, AIRBRANCH.FS_FeeInvoice.InvoiceID, " & _
                    '"case " & _
                    '"when AIRBRANCH.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
                    '"when AIRBRANCH.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 " & _
                    '"else numAmount " & _
                    '"end FeeDue " & _
                    '"from AIRBRANCH.FS_Transactions, AIRBRANCH.FS_FeeInvoice, " & _
                    '"AIRBRANCH.FSLK_TransactionType  " & _
                    '"where AIRBRANCH.FS_FeeInvoice.InvoiceID = AIRBRANCH.FS_Transactions.INvoiceID (+) " & _
                    '"and AIRBRANCH.FS_Transactions.transactionTypeCode = AIRBRANCH.FSLK_TransactionType.TransactionTypeCode (+) " & _
                    '"and AIRBRANCH.FS_FeeInvoice.InvoiceID like '%" & Replace(txtSearchInvoice.Text, "'", "''") & "%'  " & _
                    '"and AIRBRANCH.FS_Transactions.Active = '1'  " & _
                    '"and AIRBRANCH.FS_FeeInvoice.Active = '1' " & _
                    '"order by InvoiceID desc   "


                    SQL = "select " & _
                    "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " & _
                    "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " & _
                    " strComment,  ALLInvoices.InvoiceID,  " & _
                    "FeeDue  " & _
                    "from  " & _
                    "(select substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                    "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
                    "from  AIRBranch.FS_FeeInvoice " & _
                    "where AIRBRANCH.FS_FeeInvoice.InvoiceID like '%" & Replace(txtSearchInvoice.Text, "'", "''") & "%'  " & _
                    "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
                    "union " & _
                    "select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                    "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
                    "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
                    "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
                    "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
                    "and AIRBRANCH.FS_FeeInvoice.InvoiceID like '%" & Replace(txtSearchInvoice.Text, "'", "''") & "%'  " & _
                    "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBranch.FS_Transactions.Active = '1'  ) ALLInvoices,  " & _
                    "(select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
                    "strDepositNo, datTransactionDate, " & _
                    "numPayment, AIRBranch.FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " & _
                    "AIRBranch.FS_Transactions.strComment, AIRBranch.FS_FeeINvoice.InvoiceID, " & _
                    "case when AIRBranch.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
                    "when AIRBranch.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " & _
                    "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
                    "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
                    "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
                    "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
                    "and AIRBranch.FS_Transactions.Active = '1' order by strBatchNo) Transactions " & _
                    "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "





                Else
                    SQL = "select " & _
     "distinct  ALLInvoices.strAIRSNumber, strDepositNo, datTransactionDate,  " & _
     "numPayment,  ALLInvoices.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID,  " & _
     " strComment,  ALLInvoices.InvoiceID,  " & _
     "FeeDue  " & _
     "from  " & _
     "(select substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
     "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
     "from  AIRBranch.FS_FeeInvoice " & _
     "where AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
     "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' " & _
     "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
     "union " & _
     "select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, " & _
     "AIRBranch.FS_FeeINvoice.numFeeYear, AIRBranch.FS_FeeINvoice.InvoiceID " & _
     "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
     "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
     "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
     "and AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
     "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' and AIRBranch.FS_FeeInvoice.Active = '1' " & _
     "and AIRBranch.FS_Transactions.Active = '1'  ) ALLInvoices,  " & _
     "(select distinct substr(AIRBranch.FS_FeeINvoice.strAIRSnumber, 5) as strAIRSNumber, strDepositNo, datTransactionDate, " & _
     "numPayment, AIRBranch.FS_FeeINvoice.numFeeYear, strCheckNo, strBatchNo, Description, TransactionID, " & _
     "AIRBranch.FS_Transactions.strComment, AIRBranch.FS_FeeINvoice.InvoiceID, " & _
     "case when AIRBranch.FS_Transactions.transactionTypeCode = '1' then numAmount " & _
     "when AIRBranch.FS_Transactions.TransactionTypeCode = '2' then numAmount/4 else numAmount end FeeDue " & _
     "from AIRBranch.FS_Transactions, AIRBranch.FS_FeeInvoice, AIRBranch.FSLK_TransactionType  " & _
     "where AIRBranch.FS_FeeINvoice.InvoiceID = AIRBranch.FS_Transactions.INvoiceID (+) " & _
     "and AIRBranch.FS_Transactions.transactionTypeCode = AIRBranch.FSLK_TransactionType.TransactionTypeCode (+) " & _
     "and AIRBranch.FS_FeeInvoice.strAIRSnumber like '0413%" & Replace(mtbAIRSNumber.Text, "'", "''") & "%'  " & _
     "and AIRBranch.FS_FeeInvoice.numFeeYear = '" & mtbFeeYear.Text & "' " & _
     "and AIRBranch.FS_FeeInvoice.Active = '1' " & _
     "and AIRBranch.FS_Transactions.Active = '1' order by InvoiceID desc) Transactions " & _
     "where Allinvoices.InvoiceID = Transactions.InvoiceID  (+) "
                End If
                daInvoice = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daInvoice.Fill(dsInvoice, "Deposit")

                dgvInvoices.DataSource = dsInvoice
                dgvInvoices.DataMember = "Deposit"

                dgvInvoices.RowHeadersVisible = False
                dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvInvoices.AllowUserToResizeColumns = True
                dgvInvoices.AllowUserToAddRows = False
                dgvInvoices.AllowUserToDeleteRows = False
                dgvInvoices.AllowUserToOrderColumns = True
                dgvInvoices.AllowUserToResizeRows = True
                dgvInvoices.ColumnHeadersHeight = "35"
                dgvInvoices.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvInvoices.Columns("strairsnumber").DisplayIndex = 0
                dgvInvoices.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvInvoices.Columns("strdepositno").DisplayIndex = 3
                dgvInvoices.Columns("datTransactionDate").HeaderText = "Deposit Date"
                dgvInvoices.Columns("datTransactionDate").DisplayIndex = 7
                dgvInvoices.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvInvoices.Columns("numpayment").HeaderText = "Payment"
                dgvInvoices.Columns("numpayment").DisplayIndex = 6
                dgvInvoices.Columns("numpayment").DefaultCellStyle.Format = "c"
                dgvInvoices.Columns("numFeeyear").HeaderText = "Year"
                dgvInvoices.Columns("numFeeyear").DisplayIndex = 1
                dgvInvoices.Columns("strcheckno").HeaderText = "Check Number"
                dgvInvoices.Columns("strcheckno").DisplayIndex = 5
                dgvInvoices.Columns("strbatchno").HeaderText = "Batch Number"
                dgvInvoices.Columns("strbatchno").DisplayIndex = 4
                dgvInvoices.Columns("Description").HeaderText = "Payment Type"
                dgvInvoices.Columns("Description").DisplayIndex = 8
                dgvInvoices.Columns("TransactionID").HeaderText = "Transaction ID"
                dgvInvoices.Columns("TransactionID").DisplayIndex = 10
                dgvInvoices.Columns("TransactionID").Visible = False
                dgvInvoices.Columns("strcomment").HeaderText = "Comments"
                dgvInvoices.Columns("strcomment").DisplayIndex = 11
                dgvInvoices.Columns("strcomment").Visible = False
                dgvInvoices.Columns("InvoiceID").HeaderText = "Invoice Number"
                dgvInvoices.Columns("InvoiceID").DisplayIndex = 2
                dgvInvoices.Columns("InvoiceID").Visible = True
                dgvInvoices.Columns("FeeDue").HeaderText = "Fees Due"
                dgvInvoices.Columns("FeeDue").DisplayIndex = 9
                dgvInvoices.Columns("FeeDue").Visible = True
                txtCountInvoices.Text = dgvInvoices.RowCount.ToString
                lblViewInvoices.Enabled = True

                If mtbAIRSNumber.Text <> "" And dgvInvoices.RowCount = 0 Then
                    SQL = "Select " & _
                    "strFacilityName " & _
                    "from AIRBRANCH.APBFacilityInformation " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            lblAIRSNumber.Text = "AIRS #: "
                            lblFacilityName.Text = "Facility Name: "
                        Else
                            lblAIRSNumber.Text = "AIRS #: " & mtbAIRSNumber.Text
                            lblFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                        End If
                    End While
                End If
            Else
                MsgBox("You must enter an invoice # (Partial or complete).", MsgBoxStyle.Information, "PASP Deposit Amendments")

            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
 
End Class