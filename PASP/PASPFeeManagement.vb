Imports System.Data.OracleClient

Public Class PASPFeeManagement
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim dtairs As New DataTable
    Dim FeeYear As String
    Dim AIRSNumber As String

    Private Sub PASPFeeManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadFeeRates("1")
            LoadNSPSExemptions("1")
            LoadNSPSExemptions2("1")
            LoadNSPSExemptionYear()
            LoadSelectedNSPSExemptions()

            FormatWebUsers()
            TabControl3.TabPages.Remove(TPActivate)
            TabControl3.TabPages.Remove(TPFeeFacility)

            btnFirstEnrollment.Enabled = False
            btnUnenrollFeeYear.Enabled = False
            btnUpdateContactData.Enabled = False
            btnSetMailoutDate.Enabled = False
            btnSaveAddition.Enabled = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


#Region "Subs and Functions"
  
    Sub LoadFeeRates(ByVal ActiveStatus As String)
        Try
            SQL = "Select " & _
            "numFeeRateID, " & _
            "numFeeYear, " & _
            "datFeePeriodStart, datFeePeriodEnd, " & _
            "numPart70Fee, numSMFee, " & _
            "numPerTonRate, numNSPSFee, " & _
            "datFeeDueDate, " & _
            "numAdminFeeRate, datAdminApplicable, " & _
            "datFirstQrtDue, datSecondQrtDue, " & _
            "datThirdQrtDue, datFourthQrtDue, " & _
            "strComments, " & _
            "numAAThres, numNAThres " & _
            "from " & connNameSpace & ".FS_FeeRate " & _
            "where Active = '" & ActiveStatus & "' " & _
            "order by numFeeYear desc "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "FeeRates")
            dgvFeeRates.DataSource = ds
            dgvFeeRates.DataMember = "FeeRates"

            dgvFeeRates.RowHeadersVisible = False
            dgvFeeRates.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeRates.AllowUserToResizeColumns = True
            dgvFeeRates.AllowUserToAddRows = False
            dgvFeeRates.AllowUserToDeleteRows = False
            dgvFeeRates.AllowUserToOrderColumns = True
            dgvFeeRates.AllowUserToResizeRows = True

            dgvFeeRates.Columns("numFeeRateID").HeaderText = "ID"
            dgvFeeRates.Columns("numFeeRateID").DisplayIndex = 0
            dgvFeeRates.Columns("numFeeRateID").Visible = False
            dgvFeeRates.Columns("numFeeYear").HeaderText = "Fee Year"
            dgvFeeRates.Columns("numFeeYear").DisplayIndex = 1
            dgvFeeRates.Columns("numFeeYear").Width = 40
            dgvFeeRates.Columns("datFeePeriodStart").HeaderText = "Start Date"
            dgvFeeRates.Columns("datFeePeriodStart").DisplayIndex = 2
            dgvFeeRates.Columns("datFeePeriodStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datFeePeriodStart").Width = 75
            dgvFeeRates.Columns("datFeePeriodEnd").HeaderText = "End Date"
            dgvFeeRates.Columns("datFeePeriodEnd").DisplayIndex = 3
            dgvFeeRates.Columns("datFeePeriodEnd").Width = 75
            dgvFeeRates.Columns("datFeePeriodEnd").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvFeeRates.Columns("numPart70Fee").DisplayIndex = 4
            dgvFeeRates.Columns("numPart70Fee").Width = 75
            dgvFeeRates.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numSMFee").HeaderText = "SM Fee"
            dgvFeeRates.Columns("numSMFee").DisplayIndex = 5
            dgvFeeRates.Columns("numSMFee").Width = 75
            dgvFeeRates.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numPerTonRate").HeaderText = "Per Ton Rate"
            dgvFeeRates.Columns("numPerTonRate").DisplayIndex = 6
            dgvFeeRates.Columns("numPerTonRate").Width = 75
            dgvFeeRates.Columns("numPerTonRate").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvFeeRates.Columns("numNSPSFee").DisplayIndex = 7
            dgvFeeRates.Columns("numNSPSFee").Width = 75
            dgvFeeRates.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numAAThres").HeaderText = "Attainment Threshold"
            dgvFeeRates.Columns("numAAThres").DisplayIndex = 8
            dgvFeeRates.Columns("numAAThres").Width = 75
            dgvFeeRates.Columns("numNAThres").HeaderText = "NonAttainment Threshold"
            dgvFeeRates.Columns("numNAThres").DisplayIndex = 9
            dgvFeeRates.Columns("numNAThres").Width = 75



            dgvFeeRates.Columns("datFeeDueDate").HeaderText = "Due Date"
            dgvFeeRates.Columns("datFeeDueDate").DisplayIndex = 10
            dgvFeeRates.Columns("datFeeDueDate").Width = 75
            dgvFeeRates.Columns("datFeeDueDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("numAdminFeeRate").HeaderText = "Admin Fee"
            dgvFeeRates.Columns("numAdminFeeRate").DisplayIndex = 11
            dgvFeeRates.Columns("numAdminFeeRate").Width = 75
            '  dgvFeeRates.Columns("numAdminFeeRate").DefaultCellStyle.Format = "p2"
            dgvFeeRates.Columns("datAdminApplicable").HeaderText = "Admin. Fee Applicable"
            dgvFeeRates.Columns("datAdminApplicable").DisplayIndex = 12
            dgvFeeRates.Columns("datAdminApplicable").Width = 75
            dgvFeeRates.Columns("datAdminApplicable").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeRates.Columns("datFirstQrtDue").HeaderText = "1st Qrt Due Date"
            dgvFeeRates.Columns("datFirstQrtDue").DisplayIndex = 13
            dgvFeeRates.Columns("datFirstQrtDue").Width = 125
            dgvFeeRates.Columns("datFirstQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datSecondQrtDue").HeaderText = "2nd Qrt Due Date"
            dgvFeeRates.Columns("datSecondQrtDue").DisplayIndex = 14
            dgvFeeRates.Columns("datSecondQrtDue").Width = 125
            dgvFeeRates.Columns("datSecondQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datThirdQrtDue").HeaderText = "3rd Qrt Due Date"
            dgvFeeRates.Columns("datThirdQrtDue").DisplayIndex = 15
            dgvFeeRates.Columns("datThirdQrtDue").Width = 125
            dgvFeeRates.Columns("datThirdQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datFourthQrtDue").HeaderText = "4th Qrt Due Date"
            dgvFeeRates.Columns("datFourthQrtDue").DisplayIndex = 16
            dgvFeeRates.Columns("datFourthQrtDue").Width = 125
            dgvFeeRates.Columns("datFourthQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeRates.Columns("strComments").HeaderText = "Notes"
            dgvFeeRates.Columns("strComments").DisplayIndex = 17
            dgvFeeRates.Columns("strComments").Width = 200

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearFeeData()
        Try
            txtFeeID.Clear()
            txtFeeYear.Clear()
            dtpFeePeriodStart.Text = OracleDate
            dtpFeePeriodEnd.Text = OracleDate
            txtTitleVfee.Clear()
            txtAnnualSMFee.Clear()
            txtAnnualNSPSFee.Clear()
            txtperTonRate.Clear()
            dtpFeeDueDate.Text = OracleDate
            txtAdminFeePercent.Clear()
            dtpAdminApplicable.Text = OracleDate
            txtFeeNotes.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNSPSExemptions(ByVal ActiveStatus As String)
        Try
            SQL = "Select " & _
            "NSPSReasonCode, Description, " & _
            "(strLastName||', '||strFirstName) as UpdatingUser, " & _
            "UpdateDateTime, CreateDateTime, " & _
            "case " & _
            "when Active = '0' then 'Flagged as deleted' " & _
            "else 'Active' " & _
            "end ActiveStatus " & _
            "from " & connNameSpace & ".FSLK_NSPSReason, " & connNameSpace & ".EPDUserProfiles " & _
            "where " & connNameSpace & ".FSLK_NSPSReason.UpdateUser = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
            "and Active = '" & ActiveStatus & "' " & _
            "order by NSPSReasonCode "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "NSPSExemptions")
            dgvNSPSExemptions.DataSource = ds
            dgvNSPSExemptions.DataMember = "NSPSExemptions"

            dgvNSPSExemptions.RowHeadersVisible = False
            dgvNSPSExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptions.AllowUserToResizeColumns = True
            dgvNSPSExemptions.AllowUserToResizeRows = True
            dgvNSPSExemptions.AllowUserToAddRows = False
            dgvNSPSExemptions.AllowUserToDeleteRows = False
            dgvNSPSExemptions.AllowUserToOrderColumns = True
            dgvNSPSExemptions.Columns("NSPSReasonCode").HeaderText = "ID"
            dgvNSPSExemptions.Columns("NSPSReasonCode").DisplayIndex = 0
            '  dgvNSPSExemptions.Columns("NSPSReasonCode").Visible = False
            dgvNSPSExemptions.Columns("Description").HeaderText = "NSPS Exemption Reason"
            dgvNSPSExemptions.Columns("Description").DisplayIndex = 1
            dgvNSPSExemptions.Columns("UpdatingUser").HeaderText = "Updating User"
            dgvNSPSExemptions.Columns("UpdatingUser").DisplayIndex = 2
            dgvNSPSExemptions.Columns("UpdateDateTime").HeaderText = "Updated Date"
            dgvNSPSExemptions.Columns("UpdateDateTime").DisplayIndex = 3
            dgvNSPSExemptions.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvNSPSExemptions.Columns("CreateDateTime").HeaderText = "Created Date"
            dgvNSPSExemptions.Columns("CreateDateTime").DisplayIndex = 4
            dgvNSPSExemptions.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvNSPSExemptions.Columns("ActiveStatus").HeaderText = "Active Status"
            dgvNSPSExemptions.Columns("ActiveStatus").DisplayIndex = 5

            dgvNSPSExemptions.AutoResizeColumns()
            dgvNSPSExemptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNSPSExemptions2(ByVal ActiveStatus As String)
        Try
            SQL = "Select " & _
            "NSPSReasonCode, Description, " & _
            "(strLastName||', '||strFirstName) as UpdatingUser, " & _
            "UpdateDateTime, CreateDateTime, " & _
            "case " & _
            "when Active = '0' then 'Flagged as deleted' " & _
            "else 'Active' " & _
            "end ActiveStatus " & _
            "from " & connNameSpace & ".FSLK_NSPSReason, " & connNameSpace & ".EPDUserProfiles " & _
            "where " & connNameSpace & ".FSLK_NSPSReason.UpdateUser = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
            "and Active = '" & ActiveStatus & "' " & _
            "order by NSPSReasonCode "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "NSPSExemptions")

            dgvExistingExemptions.DataSource = ds
            dgvExistingExemptions.DataMember = "NSPSExemptions"

            dgvExistingExemptions.RowHeadersVisible = False
            dgvExistingExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvExistingExemptions.AllowUserToResizeColumns = True
            dgvExistingExemptions.AllowUserToResizeRows = True
            dgvExistingExemptions.AllowUserToAddRows = False
            dgvExistingExemptions.AllowUserToDeleteRows = False
            dgvExistingExemptions.AllowUserToOrderColumns = True
            dgvExistingExemptions.Columns("NSPSReasonCode").HeaderText = "ID"
            dgvExistingExemptions.Columns("NSPSReasonCode").DisplayIndex = 0
            dgvExistingExemptions.Columns("Description").HeaderText = "NSPS Exemption Reason"
            dgvExistingExemptions.Columns("Description").DisplayIndex = 1
            dgvExistingExemptions.Columns("UpdatingUser").HeaderText = "Updating User"
            dgvExistingExemptions.Columns("UpdatingUser").DisplayIndex = 2
            dgvExistingExemptions.Columns("UpdateDateTime").HeaderText = "Updated Date"
            dgvExistingExemptions.Columns("UpdateDateTime").DisplayIndex = 3
            dgvExistingExemptions.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvExistingExemptions.Columns("CreateDateTime").HeaderText = "Created Date"
            dgvExistingExemptions.Columns("CreateDateTime").DisplayIndex = 4
            dgvExistingExemptions.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvExistingExemptions.Columns("ActiveStatus").HeaderText = "Active Status"
            dgvExistingExemptions.Columns("ActiveStatus").DisplayIndex = 5

            dgvExistingExemptions.AutoResizeColumns()
            dgvExistingExemptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadNSPSExemptionYear()
        Try
            Dim NSPSYear As String = ""
            cboNSPSExemptionYear.Items.Clear()
            'cboNSPSExemptionYear.Items.Add(Today.Year + 1)
            cboAvailableFeeYears.Items.Clear()
            'cboAvailableFeeYears.Items.Add(Today.Year + 1)

            SQL = "Select " & _
            "distinct(numFeeYear) as NSPSYear " & _
            "from " & connNameSpace & ".FSLK_NSPSReasonYear " & _
            "order by numFeeyear desc "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NSPSYear")) Then
                Else
                    NSPSYear = dr.Item("NSPSYear")

                    If cboNSPSExemptionYear.Items.Contains(NSPSYear) Then
                    Else
                        cboNSPSExemptionYear.Items.Add(NSPSYear)
                    End If
                    If cboAvailableFeeYears.Items.Contains(NSPSYear) Then
                    Else
                        cboAvailableFeeYears.Items.Add(NSPSYear)
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSelectedNSPSExemptions()
        Try
            dgvNSPSExemptionsByYear.RowHeadersVisible = False
            dgvNSPSExemptionsByYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptionsByYear.AllowUserToResizeColumns = True
            dgvNSPSExemptionsByYear.AllowUserToAddRows = False
            dgvNSPSExemptionsByYear.AllowUserToDeleteRows = False
            dgvNSPSExemptionsByYear.AllowUserToOrderColumns = True
            dgvNSPSExemptionsByYear.AllowUserToResizeRows = True
            dgvNSPSExemptionsByYear.ColumnHeadersHeight = "35"

            dgvNSPSExemptionsByYear.Columns.Add("numFeeYear", "Year")
            dgvNSPSExemptionsByYear.Columns("numFeeYear").DisplayIndex = 0
            dgvNSPSExemptionsByYear.Columns("numFeeYear").Width = (dgvNSPSExemptionsByYear.Width * 0.1)
            dgvNSPSExemptionsByYear.Columns("numFeeYear").Visible = True

            dgvNSPSExemptionsByYear.Columns.Add("NSPSReasonCode", "NSPS ID")
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").DisplayIndex = 1
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").Width = (dgvNSPSExemptionsByYear.Width * 0.15)
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").ReadOnly = True

            dgvNSPSExemptionsByYear.Columns.Add("displayOrder", "Display Order")
            dgvNSPSExemptionsByYear.Columns("displayOrder").DisplayIndex = 2
            dgvNSPSExemptionsByYear.Columns("displayOrder").Width = (dgvNSPSExemptionsByYear.Width * 0.15)
            dgvNSPSExemptionsByYear.Columns("displayOrder").ReadOnly = False

            dgvNSPSExemptionsByYear.Columns.Add("Description", "NSPS Exemption Reason")
            dgvNSPSExemptionsByYear.Columns("Description").DisplayIndex = 3
            dgvNSPSExemptionsByYear.Columns("Description").Width = (dgvNSPSExemptionsByYear.Width * 0.6)
            dgvNSPSExemptionsByYear.Columns("Description").ReadOnly = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub btnClearFeeData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFeeData.Click
        Try
            ClearFeeData()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvFeeRates_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFeeRates.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvFeeRates.HitTest(e.X, e.Y)

            If dgvFeeRates.RowCount > 0 And hti.RowIndex <> -1 Then

                ClearFeeData()
                If IsDBNull(dgvFeeRates(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtFeeID.Text = dgvFeeRates(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(1, hti.RowIndex).Value) Then
                    txtFeeYear.Clear()
                Else
                    txtFeeYear.Text = dgvFeeRates(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(2, hti.RowIndex).Value) Then
                    dtpFeePeriodStart.Text = OracleDate
                Else
                    dtpFeePeriodStart.Text = dgvFeeRates(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(3, hti.RowIndex).Value) Then
                    dtpFeePeriodEnd.Text = OracleDate
                Else
                    dtpFeePeriodEnd.Text = dgvFeeRates(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(4, hti.RowIndex).Value) Then
                    txtTitleVfee.Clear()
                Else
                    txtTitleVfee.Text = dgvFeeRates(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(5, hti.RowIndex).Value) Then
                    txtAnnualSMFee.Clear() 'this was missing for some reason on 3/19/2012 -Mfloyd
                Else
                    txtAnnualSMFee.Text = dgvFeeRates(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(7, hti.RowIndex).Value) Then
                    txtAnnualNSPSFee.Clear()
                Else
                    txtAnnualNSPSFee.Text = dgvFeeRates(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(6, hti.RowIndex).Value) Then
                    txtperTonRate.Clear()
                Else
                    txtperTonRate.Text = dgvFeeRates(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(16, hti.RowIndex).Value) Then
                    txtAttainmentThreshold.Clear()
                Else
                    txtAttainmentThreshold.Text = dgvFeeRates(16, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(17, hti.RowIndex).Value) Then
                    txtNonAttainmentThreshold.Clear()
                Else
                    txtNonAttainmentThreshold.Text = dgvFeeRates(17, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(8, hti.RowIndex).Value) Then
                    dtpFeeDueDate.Text = OracleDate
                Else
                    dtpFeeDueDate.Text = dgvFeeRates(8, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(9, hti.RowIndex).Value) Then
                    txtAdminFeePercent.Clear()
                Else
                    txtAdminFeePercent.Text = dgvFeeRates(9, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(10, hti.RowIndex).Value) Then
                    dtpAdminApplicable.Text = OracleDate
                Else
                    dtpAdminApplicable.Text = dgvFeeRates(10, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(11, hti.RowIndex).Value) Then
                    dtpFirstQrtDue.Text = OracleDate
                Else
                    dtpFirstQrtDue.Text = dgvFeeRates(11, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(12, hti.RowIndex).Value) Then
                    dtpSecondQrtDue.Text = OracleDate
                Else
                    dtpSecondQrtDue.Text = dgvFeeRates(12, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(13, hti.RowIndex).Value) Then
                    dtpThirdQrtDue.Text = OracleDate
                Else
                    dtpThirdQrtDue.Text = dgvFeeRates(13, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(14, hti.RowIndex).Value) Then
                    dtpFourthQrtDue.Text = OracleDate
                Else
                    dtpFourthQrtDue.Text = dgvFeeRates(14, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(15, hti.RowIndex).Value) Then
                    txtFeeNotes.Clear()
                Else
                    txtFeeNotes.Text = dgvFeeRates(15, hti.RowIndex).Value
                End If




            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnsaveRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveRate.Click
        Try
            If Insert_FS_FeeRate(txtFeeYear.Text, dtpFeePeriodStart.Text, dtpFeePeriodEnd.Text, _
                               txtTitleVfee.Text, txtAnnualSMFee.Text, txtperTonRate.Text, txtAnnualNSPSFee.Text, _
                               dtpFeeDueDate.Text, txtAdminFeePercent.Text, dtpAdminApplicable.Text, _
                               txtFeeNotes.Text, "1", dtpFirstQrtDue.Text, dtpSecondQrtDue.Text, _
                               dtpThirdQrtDue.Text, dtpFourthQrtDue.Text, txtAttainmentThreshold.Text, _
                               txtNonAttainmentThreshold.Text) = True Then

                LoadFeeRates("1")
                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateFeeData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFeeData.Click
        Try
            If txtFeeID.Text <> "" Then
                If Update_FS_FeeRate(txtFeeID.Text, txtFeeYear.Text, dtpFeePeriodStart.Text, dtpFeePeriodEnd.Text, _
                                         txtTitleVfee.Text, txtAnnualSMFee.Text, txtperTonRate.Text, txtAnnualNSPSFee.Text, _
                                         dtpFeeDueDate.Text, txtAdminFeePercent.Text, dtpAdminApplicable.Text, _
                                         txtFeeNotes.Text, "1", dtpFirstQrtDue.Text, dtpSecondQrtDue.Text, _
                                         dtpThirdQrtDue.Text, dtpFourthQrtDue.Text, txtAttainmentThreshold.Text, _
                                         txtNonAttainmentThreshold.Text) = True Then

                    LoadFeeRates("1")
                    ClearFeeData()
                    MsgBox("Update completed", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Did not update", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteFeeRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFeeRate.Click
        Try
            If txtFeeID.Text <> "" Then
                If Update_FS_FeeRate(txtFeeID.Text, txtFeeYear.Text, dtpFeePeriodStart.Text, dtpFeePeriodEnd.Text, _
                                         txtTitleVfee.Text, txtAnnualSMFee.Text, txtperTonRate.Text, txtAnnualNSPSFee.Text, _
                                         dtpFeeDueDate.Text, txtAdminFeePercent.Text, dtpAdminApplicable.Text, _
                                         txtFeeNotes.Text, "0", dtpFirstQrtDue.Text, dtpSecondQrtDue.Text, _
                                         dtpThirdQrtDue.Text, dtpFourthQrtDue.Text, txtAttainmentThreshold.Text, _
                                         txtNonAttainmentThreshold.Text) = True Then

                    LoadFeeRates("1")
                    MsgBox("Delete completed", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Did not Delete", MsgBoxStyle.Information, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnViewDeletedFeeRates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDeletedFeeRates.Click
        Try

            LoadFeeRates("0")

        Catch ex As Exception

        End Try
    End Sub
    Sub LoadNSPSExemptionByYear()
        Try
            Dim NSPStemp As String = ""
            Dim ReasonID As String = ""
            Dim DisplayOrder As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 1


            SQL = "Select " & _
            "NSPSReasonCode, DisplayOrder " & _
            "from " & connNameSpace & ".FSLK_NSPSReasonYear " & _
            "where numFeeYear = '" & cboNSPSExemptionYear.Text & "' " & _
            "order by NSPSReasonCode "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NSPSReasonCode")) Then
                    NSPStemp = NSPStemp
                Else
                    NSPStemp = NSPStemp & dr.Item("NSPSReasonCode")
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        NSPStemp = NSPStemp & "-" & i & ","
                        i += 1
                    Else
                        NSPStemp = NSPStemp & "-" & dr.Item("DisplayOrder") & ","
                        If dr.Item("DisplayOrder") >= i Then
                            i = dr.Item("DisplayOrder") + 1
                        End If
                    End If
                End If
            End While
            dr.Close()

            dgvNSPSExemptionsByYear.Rows.Clear()
            Do While NSPStemp <> ""
                ReasonID = Mid(NSPStemp, 1, InStr(NSPStemp, "-", CompareMethod.Text) - 1)

                If ReasonID.Length = 1 Then
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 3)
                Else
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 4)
                End If


                temp = ReasonID & "-" & DisplayOrder & ","
                NSPStemp = Replace(NSPStemp, temp, "")

                Dim x As Integer = 0
                While x < dgvNSPSExemptions.Rows.Count
                    Dim y As Integer = 0
                    While y < dgvNSPSExemptions.Rows(x).Cells.Count
                        Dim c As DataGridViewCell = dgvNSPSExemptions.Rows(x).Cells(y)
                        If Not c.Value Is DBNull.Value Or Nothing Then
                            If CType(c.Value, String) = ReasonID Then
                                dgvRow = New DataGridViewRow
                                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, x).Value
                                dgvRow.Cells(2).Value = DisplayOrder
                                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, x).Value
                                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                            End If
                        End If
                        System.Math.Min(System.Threading.Interlocked.Increment(y), y - 1)
                    End While
                    System.Math.Min(System.Threading.Interlocked.Increment(x), x - 1)
                End While
            Loop
            MessageBox.Show("Done", Me.Text, MessageBoxButtons.OK)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectForm.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvNSPSExemptionsByYear.Rows.Count

            If i > 0 Then
                temp = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                For i = 0 To dgvNSPSExemptionsByYear.Rows.Count - 1
                    If dgvNSPSExemptionsByYear(1, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                    dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                    dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                    dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                End If
            Else
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnViewNSPSExemptionsByYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewNSPSExemptionsByYear.Click
        Try
            If cboNSPSExemptionYear.Text <> "" Then
                LoadNSPSExemptionByYear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllForms.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            dgvNSPSExemptionsByYear.Rows.Clear()

            For i = 0 To dgvNSPSExemptions.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, i).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, i).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            Next

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUnselectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectForm.Click
        Try
            Dim ReasonID As String = ""

            If dgvNSPSExemptionsByYear.RowCount > 0 Then
                ReasonID = dgvNSPSExemptionsByYear(1, dgvNSPSExemptionsByYear.CurrentRow.Index).Value

                SQL = "Select " & _
                "strNSPSReason " & _
                "from " & connNameSpace & ".FSCalculations " & _
                "where intYear = '" & dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value & "' " & _
                "and (strNSPSReason like '%" & ReasonID & ",' or strNSPSReason = '" & ReasonID & "' or strNSPSReason like '%," & ReasonID & "') "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MessageBox.Show("Unable to Remove this exemption from this year because this exemption has been used.")
                    Exit Sub
                Else
                    dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAllForms.Click
        Try
            Dim ReasonID As String = ""
            Dim i As Integer = 0
            Dim x As Integer = 0

            If dgvNSPSExemptionsByYear.RowCount > 0 Then
                'dgvNSPSExemptionsByYear.Rows.Clear()

                i = 0
                While i < dgvNSPSExemptionsByYear.RowCount
                    ' For i = 0 To dgvNSPSExemptionsByYear.RowCount - 1

                    dgvNSPSExemptionsByYear.Rows(i).Selected = True
                    ReasonID = dgvNSPSExemptionsByYear(1, i).Value
                    SQL = "Select " & _
                    "strNSPSReason " & _
                    "from " & connNameSpace & ".FSCalculations " & _
                    "where intYear = '" & dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value & "' " & _
                    "and (strNSPSReason like '%" & ReasonID & ",' or strNSPSReason = '" & ReasonID & "' or strNSPSReason like '%," & ReasonID & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        i += 1
                    Else

                        dgvNSPSExemptionsByYear.Rows(i).Selected = True
                        dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                        dgvNSPSExemptionsByYear.Rows(i).Selected = False
                    End If
                End While
                'Next

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateNSPSbyYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateNSPSbyYear.Click
        Try
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim ReasonID As String
            Dim Order As String
            Dim temp As String = ""
            Dim ExistingID As String = ""

            SQL = "Select " & _
            "NSPSReasonCode " & _
            "from " & connNameSpace & ".FSLK_NSPSReasonYear " & _
            "where numFeeYear = '" & cboNSPSExemptionYear.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NSPSReasonCode")) Then
                    ExistingID = ExistingID
                Else
                    ExistingID = ExistingID & "(" & dr.Item("NSPSReasonCode") & ")"
                End If
            End While
            dr.Close()

            While x < dgvNSPSExemptionsByYear.Rows.Count
                ReasonID = dgvNSPSExemptionsByYear(1, x).Value
                Order = dgvNSPSExemptionsByYear(2, x).Value
                x += 1

                SQL = "Select " & _
                "DisplayOrder " & _
                "from " & connNameSpace & ".FSLK_NSPSReasonYear " & _
                "where numFeeYear = '" & cboNSPSExemptionYear.Text & "' " & _
                "and NSPSReasonCode = '" & ReasonID & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                temp = ""
                While dr.Read
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        temp = "NULL"
                    Else
                        temp = dr.Item("DisplayOrder")
                    End If
                End While
                dr.Close()

                If temp <> Order Then
                    Select Case temp
                        Case ""
                            If Insert_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order) = True Then

                            End If

                        Case "NULL"
                            If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order, "1") = True Then

                            End If

                        Case Else
                            If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order, "1") = True Then

                            End If
                    End Select
                End If
                ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
            End While

            If ExistingID <> "" Then
                Do While ExistingID <> ""
                    ReasonID = Mid(ExistingID, InStr(ExistingID, "(", CompareMethod.Text) + 1, InStr(ExistingID, ")", CompareMethod.Text) - 2)
                    ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
                    If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, "0", "0") = True Then

                    End If
                Loop
            End If
            LoadNSPSExemptionYear()
            MessageBox.Show("Update Complete", Me.Text, MessageBoxButtons.OK)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNSPSExemption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNSPSExemption.Click
        Try
            If Insert_FSLK_NSPSReason(txtNSPSExemption.Text) = True Then
                txtNSPSExemption.Clear()
                LoadNSPSExemptions("1")
                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)

                Dim maxRow As Integer
                maxRow = dgvNSPSExemptions.RowCount - 1
                If dgvNSPSExemptions.Rows.Count >= maxRow AndAlso maxRow >= 1 Then
                    dgvNSPSExemptions.FirstDisplayedScrollingRowIndex = maxRow
                    dgvNSPSExemptions.Rows(maxRow).Selected = True
                End If
                LoadNSPSExemptions("1")
                LoadNSPSExemptions2("1")
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteNSPSExemption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNSPSExemption.Click
        Try
            'Update_FSLK_NSPSReason
            If txtDeleteNSPSExemptions.Text <> "" Then
                If Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, "", "0") = True Then
                    txtDeleteNSPSExemptions.Clear()
                    txtNSPSExemption.Clear()
                    LoadNSPSExemptions("1")
                    LoadNSPSExemptions2("1")
                    MsgBox("Exemption Deleted", MsgBoxStyle.Information, Me.Text)
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewDeletedNSPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDeletedNSPS.Click
        Try
            LoadNSPSExemptions2("0")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvNSPSExemptions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNSPSExemptions.MouseUp
        Try
            'Dim hti As DataGridView.HitTestInfo = dgvNSPSExemptions.HitTest(e.X, e.Y)

            'If dgvNSPSExemptions.RowCount > 0 And hti.RowIndex <> -1 Then

            '    txtDeleteNSPSExemptions.Clear()
            '    txtNSPSExemption.Clear()
            '    If IsDBNull(dgvNSPSExemptions(0, hti.RowIndex).Value) Then
            '        Exit Sub
            '    Else
            '        txtDeleteNSPSExemptions.Text = dgvNSPSExemptions(0, hti.RowIndex).Value
            '    End If
            '    If IsDBNull(dgvNSPSExemptions(1, hti.RowIndex).Value) Then
            '        txtNSPSExemption.Clear()
            '    Else
            '        txtNSPSExemption.Text = dgvNSPSExemptions(1, hti.RowIndex).Value
            '    End If
            'End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateNSPSExemption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateNSPSExemption.Click
        Try
            If txtDeleteNSPSExemptions.Text <> "" Then
                If Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, txtNSPSExemption.Text, "1") = True Then
                    txtDeleteNSPSExemptions.Clear()
                    txtNSPSExemption.Clear()
                    LoadNSPSExemptions("1")
                    LoadNSPSExemptions2("1")

                    MsgBox("Exemption Updated", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnReloadFeeRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadFeeRate.Click
        Try

            LoadFeeRates("1")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewEnrolledFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEnrolledFacilities.Click
        Try

            ViewEnrolledFacilities()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFirstEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirstEnrollment.Click
        Try
            Dim EnrollCheck As String = ""

            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
            "count(*) as EnrollCheck " & _
            "from " & connNameSpace & ".FS_Admin " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
            "and strEnrolled = '1' " & _
            "and ACTIVE = '1' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("EnrollCheck")) Then
                    EnrollCheck = "0"
                Else
                    EnrollCheck = dr.Item("EnrollCheck")
                End If
            End While
            dr.Close()
            If EnrollCheck > 0 Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "There are already facilities enrolled for this fee year.", _
                        MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update " & connNameSpace & ".FS_Admin set " & _
            "strEnrolled = '1', " & _
            "datEnrollment = sysdate, " & _
            "updateUser = 'IAIP||" & UserName & "', " & _
            "UpdateDateTime = '" & OracleDate & "', " & _
             "numCurrentStatus = 3 " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
            "and ACTIVE = '1' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & connNameSpace & ".FS_Admin set " & _
            "datInitialEnrollment = datEnrollment " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
            "and datInitialEnrollment is null " & _
            "and ACTIVE = '1' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_DATA", conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = cboAvailableFeeYears.Text
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = ""

            cmd.ExecuteNonQuery()

            ViewEnrolledFacilities()

            MsgBox("Facilities Enrolled for the selected fee year.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnenrollFeeYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnenrollFeeYear.Click
        Try
            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If cboAvailableFeeYears.Text < (Today.Year - 1) Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Only Current and last Fee Years are elegible to be unenrolled.", _
                        MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you positive you wanted to reset enrollment for this year.", _
              Me.Text, MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case Windows.Forms.DialogResult.Yes

                Case Else
                    Exit Sub
            End Select

            SQL = "Update " & connNameSpace & ".FS_Admin set " & _
            "strEnrolled = '0', " & _
            "datEnrollment = '', " & _
            "datInitialEnrollment = '', " & _
            "updateUser = 'IAIP||" & UserName & "', " & _
            "UpdateDateTime = '" & OracleDate & "' " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
            "and ACTIVE = '1' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            ViewEnrolledFacilities()

            MsgBox("Facilities Unenrolled.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewEnrolledFacilities()
        Try
            SQL = "Select " & _
            "substr(fS_Admin.strAIRSnumber, 5) as AIRSNumber,  " & _
            "strFacilityname,  " & _
            "strFacilityAddress1, strFacilityCity,  " & _
            "strFacilityZipCode,  " & _
            "strFirstName, strLastName, " & _
            "strContactCoName, strContactAddress1,  " & _
            "strContactCity, strcontactState,  " & _
            "strContactZipCode, strGECOUserEmail,  " & _
            "case " & _
            "when strOperationalStatus = '1' then 'YES' " & _
            "when strOperationalStatus = '0' then 'NO' " & _
            "else 'NO' " & _
            "end strOperationalStatus, " & _
            "strClass,  " & _
            "case " & _
            "when strNSPS = '1' then 'YES' " & _
            "when strNSPS = '0' then 'NO' " & _
            "else 'NO' " & _
            "end strNSPS, " & _
            "case " & _
            "when strPart70 = '1' then 'YES' " & _
            "when strPart70 = '0' then 'NO' " & _
            "else 'NO' " & _
            "end strPart70, " & _
            "datShutdowndate  " & _
            "From " & connNameSpace & ".FS_Admin, " & connNameSpace & ".FS_MailOut  " & _
            "where " & connNameSpace & ".FS_Admin.strAIRSnumber = " & connNameSpace & ".FS_MailOut.strAIRSnumber  " & _
            "and " & connNameSpace & ".FS_Admin.numFeeYear = " & connNameSpace & ".FS_MailOut.numFeeYear  " & _
            "and  " & connNameSpace & ".FS_Admin.numFeeYear = '" & cboAvailableFeeYears.Text & "'  " & _
            "and (strEnrolled = '1')  " & _
            "AND " & connNameSpace & ".FS_Admin.Active = '1' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "MailOutList")
            dgvFeeManagmentLists.DataSource = ds
            dgvFeeManagmentLists.DataMember = "MailOutList"

            dgvFeeManagmentLists.RowHeadersVisible = False
            dgvFeeManagmentLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagmentLists.AllowUserToResizeColumns = True
            dgvFeeManagmentLists.AllowUserToAddRows = False
            dgvFeeManagmentLists.AllowUserToDeleteRows = False
            dgvFeeManagmentLists.AllowUserToOrderColumns = True
            dgvFeeManagmentLists.AllowUserToResizeRows = True

            dgvFeeManagmentLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagmentLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagmentLists.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeManagmentLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeManagmentLists.Columns("STRCLASS").HeaderText = "Class"
            dgvFeeManagmentLists.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeManagmentLists.Columns("strNSPS").HeaderText = "NSPS"
            dgvFeeManagmentLists.Columns("STRNSPS").DisplayIndex = 4
            dgvFeeManagmentLists.Columns("STRPART70").HeaderText = "TV Source"
            dgvFeeManagmentLists.Columns("STRPART70").DisplayIndex = 5
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
            dgvFeeManagmentLists.Columns("strFirstName").HeaderText = "Contact First Name"
            dgvFeeManagmentLists.Columns("strFirstName").DisplayIndex = 7
            dgvFeeManagmentLists.Columns("STRlASTNAME").HeaderText = "Contact Last Name"
            dgvFeeManagmentLists.Columns("STRLASTNAME").DisplayIndex = 8
            dgvFeeManagmentLists.Columns("strContactCoName").HeaderText = "Contact Company"
            dgvFeeManagmentLists.Columns("strContactCoName").DisplayIndex = 9
            dgvFeeManagmentLists.Columns("strContactAddress1").HeaderText = "Address"
            dgvFeeManagmentLists.Columns("strContactAddress1").DisplayIndex = 10
            dgvFeeManagmentLists.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeManagmentLists.Columns("STRCONTACTCITY").DisplayIndex = 11
            dgvFeeManagmentLists.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeManagmentLists.Columns("STRCONTACTSTATE").DisplayIndex = 12
            dgvFeeManagmentLists.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeManagmentLists.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
            dgvFeeManagmentLists.Columns("strFacilityAddress1").HeaderText = "Facility Street"
            dgvFeeManagmentLists.Columns("strFacilityAddress1").DisplayIndex = 14
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").DisplayIndex = 15
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode"
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
            dgvFeeManagmentLists.Columns("strGECOUserEmail").HeaderText = "Contact Email"
            dgvFeeManagmentLists.Columns("strGECOUserEmail").DisplayIndex = 17

            txtCount.Text = dgvFeeManagmentLists.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewMailout.Click
        Try
            ViewMailOut()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGenerateMailoutList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateMailoutList.Click
        Try
            If cboAvailableFeeYears.Text = "" Then
                MsgBox("Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If IsNumeric(cboAvailableFeeYears.Text) Then
            Else
                MsgBox("Select a valid fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "select count(*) as ContactTotals " & _
            "from AIRBranch.FS_MailOut " & _
            "where numfeeyear = '" & cboAvailableFeeYears.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("ContactTotals")) Then
                    temp = "0"
                Else
                    temp = dr.Item("ContactTotals")
                End If
            End While
            dr.Close()

            If temp < 1 Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_FEE_MAILOUT", conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = cboAvailableFeeYears.Text
                cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = ""

                cmd.ExecuteNonQuery()

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_FEE_DATA", conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = cboAvailableFeeYears.Text
                cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = ""

                cmd.ExecuteNonQuery()


                'SQL = "Update " & connNameSpace & ".FS_Admin set " & _
                '"strMailoutSent = '1', " & _
                '"datMailoutSent = sysdate " & _
                '"where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
                '"and strEnrolled = '1' " & _
                '"and strInitialMailout = '1' "

                'cmd = New OracleCommand(SQL, conn)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If
                'dr = cmd.ExecuteReader
                'dr.Close()

                'SQL = "Update " & connNameSpace & ".FS_Admin set " & _
                '"strInitialMailout = '1' " & _
                '"where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
                '"and strEnrolled = '1' " & _
                '"and strInitialMailout = '0' "

                'cmd = New OracleCommand(SQL, conn)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If
                'dr = cmd.ExecuteReader
                'dr.Close()

                SQL = "Update " & connNameSpace & ".FS_Admin set " & _
                "numCurrentStatus = 4, " & _
                "strInitialMailout = '1'  " & _
                "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
                "and strInitialMailout <> '0' " & _
                "and strMailoutSent <> '0' " & _
                "and numCurrentStatus < 5 "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If

            ViewMailOut()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewMailOut()
        Try
            SQL = "Select " & _
            "substr(fS_Admin.strAIRSnumber, 5) as AIRSNumber,  " & _
            "strFacilityname,  " & _
            "strFacilityAddress1, strFacilityCity,  " & _
            "strFacilityZipCode,  " & _
            "strFirstName, strLastName, " & _
            "strContactCoName, strContactAddress1,  " & _
            "strContactCity, strcontactState,  " & _
            "strContactZipCode, strGECOUserEmail,  " & _
            "case " & _
            "when strOperationalStatus = '1' then 'YES' " & _
            "when strOperationalStatus = 'X' then 'NO' " & _
            "else 'YES' " & _
            "end strOperationalStatus, " & _
            "strClass, " & _
            "case " & _
            "when strNSPS = '1' then 'YES' " & _
            "when strNSPS = '0' then 'NO' " & _
            "else 'NO' " & _
            "end strNSPS, " & _
            "case " & _
            "when strPart70 = '1' then 'YES'" & _
            "when strPart70 = '0' then 'NO' " & _
            "else 'NO' " & _
            "end strPart70, " & _
            "datShutdowndate  " & _
            "From " & connNameSpace & ".FS_Admin, " & connNameSpace & ".FS_MailOut   " & _
            "where " & connNameSpace & ".FS_Admin.strAIRSnumber = " & connNameSpace & ".FS_MailOut.strAIRSnumber  " & _
            "and " & connNameSpace & ".FS_Admin.numFeeYear = " & connNameSpace & ".FS_MailOut.numFeeYear  " & _
            "and  " & connNameSpace & ".FS_Admin.numFeeYear = '" & cboAvailableFeeYears.Text & "'  " & _
            "and (strInitialMailOut = '1' or strMailoutSent = '1' )  " & _
            "AND " & connNameSpace & ".FS_Admin.Active = '1' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "MailOutList")
            dgvFeeManagmentLists.DataSource = ds
            dgvFeeManagmentLists.DataMember = "MailOutList"

            dgvFeeManagmentLists.RowHeadersVisible = False
            dgvFeeManagmentLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagmentLists.AllowUserToResizeColumns = True
            dgvFeeManagmentLists.AllowUserToAddRows = False
            dgvFeeManagmentLists.AllowUserToDeleteRows = False
            dgvFeeManagmentLists.AllowUserToOrderColumns = True
            dgvFeeManagmentLists.AllowUserToResizeRows = True

            dgvFeeManagmentLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagmentLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagmentLists.Columns("strFacilityName").HeaderText = "Facility Name - Snapshot"
            dgvFeeManagmentLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status - Snapshot"
            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeManagmentLists.Columns("STRCLASS").HeaderText = "Class - Snapshot"
            dgvFeeManagmentLists.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeManagmentLists.Columns("strNSPS").HeaderText = "NSPS - Snapshot"
            dgvFeeManagmentLists.Columns("STRNSPS").DisplayIndex = 4
            dgvFeeManagmentLists.Columns("STRPART70").HeaderText = "TV Source - Snapshot"
            dgvFeeManagmentLists.Columns("STRPART70").DisplayIndex = 5
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date - Snapshot"
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
            dgvFeeManagmentLists.Columns("strFirstName").HeaderText = "Contact First Name "
            dgvFeeManagmentLists.Columns("strFirstName").DisplayIndex = 7
            dgvFeeManagmentLists.Columns("STRlASTNAME").HeaderText = "Contact Last Name"
            dgvFeeManagmentLists.Columns("STRLASTNAME").DisplayIndex = 8
            dgvFeeManagmentLists.Columns("strContactCoName").HeaderText = "Contact Company "
            dgvFeeManagmentLists.Columns("strContactCoName").DisplayIndex = 9
            dgvFeeManagmentLists.Columns("strContactAddress1").HeaderText = "Address"
            dgvFeeManagmentLists.Columns("strContactAddress1").DisplayIndex = 10
            dgvFeeManagmentLists.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeManagmentLists.Columns("STRCONTACTCITY").DisplayIndex = 11
            dgvFeeManagmentLists.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeManagmentLists.Columns("STRCONTACTSTATE").DisplayIndex = 12
            dgvFeeManagmentLists.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeManagmentLists.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
            dgvFeeManagmentLists.Columns("strFacilityAddress1").HeaderText = "Facility Street"
            dgvFeeManagmentLists.Columns("strFacilityAddress1").DisplayIndex = 14
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").DisplayIndex = 15
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode"
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
            dgvFeeManagmentLists.Columns("strGECOUserEmail").HeaderText = "Contact Email"
            dgvFeeManagmentLists.Columns("strGECOUserEmail").DisplayIndex = 17

            txtCount.Text = dgvFeeManagmentLists.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateContactData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContactData.Click
        Try
            Dim AIRSNumber As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactSuffix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactCompanyName As String = ""
            Dim ContactPhone As String = ""
            Dim ContactFax As String = ""
            Dim ContactEmail As String = ""
            Dim ContactAddress As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".FS_Admin " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Select * " & _
                    "from " & connNameSpace & ".APBContactInformation " & _
                    "where strAIRSNumber = '" & AIRSNumber & "' " & _
                    "and strKey = '40' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strContactFirstname")) Then
                            ContactFirstName = ""
                        Else
                            ContactFirstName = dr2.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr2.Item("strContactLastname")) Then
                            ContactLastName = ""
                        Else
                            ContactLastName = dr2.Item("strContactLastName")
                        End If
                        If IsDBNull(dr2.Item("strContactprefix")) Then
                            ContactPrefix = ""
                        Else
                            ContactPrefix = dr2.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr2.Item("strContactSuffix")) Then
                            ContactSuffix = ""
                        Else
                            ContactSuffix = dr2.Item("strContactSuffix")
                        End If
                        If IsDBNull(dr2.Item("strContactTitle")) Then
                            ContactTitle = ""
                        Else
                            ContactTitle = dr2.Item("strContactTitle")
                        End If
                        If IsDBNull(dr2.Item("strContactCompanyName")) Then
                            ContactCompanyName = ""
                        Else
                            ContactCompanyName = dr2.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr2.Item("strContactPhoneNumber1")) Then
                            ContactPhone = ""
                        Else
                            ContactPhone = dr2.Item("strContactPhoneNumber1")
                        End If
                        If IsDBNull(dr2.Item("strContactFaxNumber")) Then
                            ContactFax = ""
                        Else
                            ContactFax = dr2.Item("strContactFaxNumber")
                        End If
                        If IsDBNull(dr2.Item("strcontactEmail")) Then
                            ContactEmail = ""
                        Else
                            ContactEmail = dr2.Item("strContactEmail")
                        End If
                        If IsDBNull(dr2.Item("strContactAddress1")) Then
                            ContactAddress = ""
                        Else
                            ContactAddress = dr2.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr2.Item("strContactcity")) Then
                            ContactCity = ""
                        Else
                            ContactCity = dr2.Item("strcontactCity")
                        End If
                        If IsDBNull(dr2.Item("strContactState")) Then
                            ContactState = ""
                        Else
                            ContactState = dr2.Item("strContactState")
                        End If
                        If IsDBNull(dr2.Item("strContactZipCode")) Then
                            ContactZipCode = ""
                        Else
                            ContactZipCode = dr2.Item("strContactZipCode")
                        End If
                    End While
                    dr2.Close()

                    SQL = "Update " & connNameSpace & ".FS_MailOut set " & _
                    "strFirstName = '" & ContactFirstName & "', " & _
                    "strLastName = '" & ContactLastName & "', " & _
                    "strPrefix = '" & ContactPrefix & "',  " & _
                    "strTitle = '" & ContactSuffix & "', " & _
                    "strContactCoName = '" & ContactCompanyName & "', " & _
                    "strContactAddress1 = '" & ContactAddress & "', " & _
                    "strContactCity = '" & ContactCity & "', " & _
                    "strContactState = '" & ContactState & "', " & _
                    "strcontactZipCode = '" & ContactZipCode & "' " & _
                    "where strAIRSNumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '" & cboAvailableFeeYears.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ExportToExcel()
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvFeeManagmentLists.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvFeeManagmentLists.ColumnCount - 1
                        .Cells(1, i + 1) = dgvFeeManagmentLists.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvFeeManagmentLists.ColumnCount - 1
                        For j = 0 To dgvFeeManagmentLists.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvFeeManagmentLists.Item(i, j).Value.ToString
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
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            ExportToExcel()
            MsgBox("Done", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvExistingExemptions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvExistingExemptions.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvExistingExemptions.HitTest(e.X, e.Y)

            If dgvExistingExemptions.RowCount > 0 And hti.RowIndex <> -1 Then

                txtDeleteNSPSExemptions.Clear()
                txtNSPSExemption.Clear()
                If IsDBNull(dgvExistingExemptions(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtDeleteNSPSExemptions.Text = dgvExistingExemptions(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvExistingExemptions(1, hti.RowIndex).Value) Then
                    txtNSPSExemption.Clear()
                Else
                    txtNSPSExemption.Text = dgvExistingExemptions(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshNSPSExemptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshNSPSExemptions.Click
        Try
            LoadNSPSExemptions2("1")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearNSPSExemptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNSPSExemptions.Click
        Try
            txtDeleteNSPSExemptions.Clear()
            txtNSPSExemption.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewFacilitiesSubjectToFees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFacilitiesSubjectToFees.Click
        Try
            SQL = "Select " & _
          "substr(fS_Admin.strAIRSnumber, 5) as AIRSNumber,  " & _
          "strFacilityname,  " & _
          "strFacilityStreet1, strFacilityCity,  " & _
          "strFacilityZipCode,  " & _
          "strOperationalStatus, strClass,  " & _
          "case " & _
          "when substr(strAIRProgramCodes, 8, 1) = '1' then 'YES' " & _
          "else 'NO' " & _
          "end strNSPS, " & _
          "case " & _
          "when substr(strAIRProgramCodes, 13, 1) = '1' then 'YES' " & _
          "else 'NO' " & _
          "end strPArt70, " & _
          "case " & _
          "when strOperationalStatus = 'X' then datShutDownDate " & _
          "end datShutdowndate,  " & _
          "case " & _
          "when strEnrolled = '1' then 'YES' " & _
          "else 'NO' " & _
          "end strEnrolled, " & _
          "case " & _
          "when strInitialMailout = '1' then 'YES' " & _
          "else 'NO' " & _
          "end strInitialMailout, " & _
          "case " & _
          "when strMailoutSent = '1' then 'YES' " & _
          "else 'NO' " & _
          "end strMailoutSent " & _
          "From " & connNameSpace & ".FS_Admin, " & connNameSpace & ".APBFacilityInformation,  " & _
          "" & connNameSpace & ".APBHeaderData " & _
          "where " & connNameSpace & ".FS_Admin.strAIRSnumber = " & connNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
          "and " & connNameSpace & ".FS_admin.strAIRSNumber = " & connNameSpace & ".APBheaderData.strAIRSNumber " & _
          "and  " & connNameSpace & ".FS_Admin.numFeeYear = '" & cboAvailableFeeYears.Text & "'  " & _
          "AND " & connNameSpace & ".FS_Admin.Active = '1' "
 '"and (strEnrolled = '1' or strInitialMailout = '1' or strMailoutSent = '1')  " & _

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "MailOutList")
            dgvFeeManagmentLists.DataSource = ds
            dgvFeeManagmentLists.DataMember = "MailOutList"

            dgvFeeManagmentLists.RowHeadersVisible = False
            dgvFeeManagmentLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagmentLists.AllowUserToResizeColumns = True
            dgvFeeManagmentLists.AllowUserToAddRows = False
            dgvFeeManagmentLists.AllowUserToDeleteRows = False
            dgvFeeManagmentLists.AllowUserToOrderColumns = True
            dgvFeeManagmentLists.AllowUserToResizeRows = True

            dgvFeeManagmentLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagmentLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagmentLists.Columns("strFacilityName").HeaderText = "Facility Name - Current Data"
            dgvFeeManagmentLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagmentLists.Columns("strFacilityStreet1").HeaderText = "Facility Street - Current Data"
            dgvFeeManagmentLists.Columns("strFacilityStreet1").DisplayIndex = 2
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").HeaderText = "Facility City - Current Data"
            dgvFeeManagmentLists.Columns("STRFACILITYCITY").DisplayIndex = 3
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode - Current Data"
            dgvFeeManagmentLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 4

            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status - Current Data"
            dgvFeeManagmentLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 5
            dgvFeeManagmentLists.Columns("STRCLASS").HeaderText = "Class - Current Data"
            dgvFeeManagmentLists.Columns("STRCLASS").DisplayIndex = 6
            dgvFeeManagmentLists.Columns("strNSPS").HeaderText = "NSPS - Current Data"
            dgvFeeManagmentLists.Columns("STRNSPS").DisplayIndex = 7
            dgvFeeManagmentLists.Columns("STRPART70").HeaderText = "TV Source - Current Data"
            dgvFeeManagmentLists.Columns("STRPART70").DisplayIndex = 8
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date - Current Data"
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 9
            dgvFeeManagmentLists.Columns("DATSHUTDOWNDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeManagmentLists.Columns("strEnrolled").HeaderText = "Enrolled Facility"
            dgvFeeManagmentLists.Columns("strEnrolled").DisplayIndex = 10
            dgvFeeManagmentLists.Columns("strInitialMailout").HeaderText = "In Initial Mailout"
            dgvFeeManagmentLists.Columns("strInitialMailout").DisplayIndex = 11
            dgvFeeManagmentLists.Columns("strMailoutSent").HeaderText = "In Mailout"
            dgvFeeManagmentLists.Columns("strMailoutSent").DisplayIndex = 12
            txtCount.Text = dgvFeeManagmentLists.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSetMailoutDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMailoutDate.Click
        Try
            SQL = "Update " & connNameSpace & ".FS_Admin set " & _
            "datMailoutSent = '" & dtpDateMailoutSent.Text & "', " & _
            "strMailOutSent = '1' " & _
            "where numFeeYear = '" & cboAvailableFeeYears.Text & "' " & _
            "and datMailoutSent is null " & _
            "and strEnrolled = '1' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Mailout Sent date set.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub FormatWebUsers()
        Try
            dgvUsers.RowHeadersVisible = False
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUsers.AllowUserToResizeColumns = True
            dgvUsers.AllowUserToAddRows = False
            dgvUsers.AllowUserToDeleteRows = False
            dgvUsers.AllowUserToOrderColumns = True
            dgvUsers.AllowUserToResizeRows = True
            dgvUsers.ColumnHeadersHeight = "35"

            dgvUsers.Columns.Add("ID", "ID")
            dgvUsers.Columns("ID").DisplayIndex = 0
            dgvUsers.Columns("ID").Visible = False

            dgvUsers.Columns.Add("numuserid", "UserID")
            dgvUsers.Columns("numuserid").DisplayIndex = 1
            dgvUsers.Columns("numuserid").Visible = False

            dgvUsers.Columns.Add("Email", "Email Address")
            dgvUsers.Columns("Email").DisplayIndex = 2
            dgvUsers.Columns("Email").Width = 250
            dgvUsers.Columns("Email").ReadOnly = True

            Dim colReadOnly As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colReadOnly)
            dgvUsers.Columns(3).HeaderText = "Admin Access"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colWrite)
            dgvUsers.Columns(4).HeaderText = "Fee Access"

            Dim colFullAccess As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colFullAccess)
            dgvUsers.Columns(5).HeaderText = "EI Access"

            Dim colSpecialPermissions As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colSpecialPermissions)
            dgvUsers.Columns(6).HeaderText = "ES Access"


            dgvUserFacilities.RowHeadersVisible = False
            dgvUserFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUserFacilities.AllowUserToResizeColumns = True
            dgvUserFacilities.AllowUserToAddRows = False
            dgvUserFacilities.AllowUserToDeleteRows = False
            dgvUserFacilities.AllowUserToOrderColumns = True
            dgvUserFacilities.AllowUserToResizeRows = True
            dgvUserFacilities.ColumnHeadersHeight = "35"

            dgvUserFacilities.Columns.Add("strAIRSNumber", "AIRS Number")
            dgvUserFacilities.Columns("strAIRSNumber").DisplayIndex = 0
            dgvUserFacilities.Columns("strAIRSNumber").Visible = True

            dgvUserFacilities.Columns.Add("strFacilityName", "Facility Name")
            dgvUserFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvUserFacilities.Columns("strFacilityName").Width = 250

            Dim colReadOnly2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colReadOnly2)
            dgvUserFacilities.Columns(2).HeaderText = "Admin Access"

            Dim colWrite2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colWrite2)
            dgvUserFacilities.Columns(3).HeaderText = "Fee Access"

            Dim colFullAccess2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colFullAccess2)
            dgvUserFacilities.Columns(4).HeaderText = "EI Access"

            Dim colSpecialPermissions2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colSpecialPermissions2)
            dgvUserFacilities.Columns(5).HeaderText = "ES Access"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewUserData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        Try
            ViewFacilitySpecificUsers()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewFacilitySpecificUsers()
        Try
            Dim dgvRow As New DataGridViewRow

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "DMU Tools")
            Else
                txtEmail.Clear()

                SQL = "Select strFacilityName " & _
                "from " & connNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFaciltyName.Text = " - "
                    Else
                        lblFaciltyName.Text = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()

                SQL = "SELECT " & _
                "" & connNameSpace & ".OlapUserAccess.NumUserID as ID, " & connNameSpace & ".OlapUserLogin.numuserid, " & _
                "" & connNameSpace & ".OlapUserLogin.strUserEmail as Email, " & _
                "Case " & _
                "When intAdminAccess = 0 Then 'False' " & _
                "When intAdminAccess = 1 Then 'True' " & _
                "End as intAdminAccess, " & _
                "Case " & _
                "When intFeeAccess = 0 Then 'False' " & _
                "When intFeeAccess = 1 Then 'True' " & _
                "End as intFeeAccess, " & _
                "Case " & _
                "When intEIAccess = 0 Then 'False' " & _
                "When intEIAccess = 1 Then 'True' " & _
                "End as intEIAccess, " & _
                "Case " & _
                "When intESAccess = 0 Then 'False' " & _
                "When intESAccess = 1 Then 'True' " & _
                "End as intESAccess " & _
                "FROM " & connNameSpace & ".OlapUserAccess, " & connNameSpace & ".OlapUserLogin " & _
                "WHERE " & connNameSpace & ".OLAPUserAccess.NumUserId = " & connNameSpace & ".OlapUserLogin.NumUserID " & _
                "AND " & connNameSpace & ".OlapUserAccess.strAirsNumber = '0413" & mtbAIRSNumber.Text & "' order by email"

                dgvUsers.Rows.Clear()
                ds = New DataSet

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvUsers)
                    If IsDBNull(dr.Item("ID")) Then
                        dgvRow.Cells(0).Value = ""
                    Else
                        dgvRow.Cells(0).Value = dr.Item("ID")
                    End If
                    If IsDBNull(dr.Item("numuserid")) Then
                        dgvRow.Cells(1).Value = ""
                    Else
                        dgvRow.Cells(1).Value = dr.Item("numuserid")
                    End If
                    If IsDBNull(dr.Item("Email")) Then
                        dgvRow.Cells(2).Value = ""
                    Else
                        dgvRow.Cells(2).Value = dr.Item("Email")
                    End If
                    If IsDBNull(dr.Item("intAdminAccess")) Then
                        dgvRow.Cells(3).Value = False
                    Else
                        dgvRow.Cells(3).Value = dr.Item("intAdminAccess")
                    End If
                    If IsDBNull(dr.Item("intFeeAccess")) Then
                        dgvRow.Cells(4).Value = False
                    Else
                        dgvRow.Cells(4).Value = dr.Item("intFeeAccess")
                    End If

                    If IsDBNull(dr.Item("intEIAccess")) Then
                        dgvRow.Cells(5).Value = False
                    Else
                        dgvRow.Cells(5).Value = dr.Item("intEIAccess")
                    End If
                    If IsDBNull(dr.Item("intESAccess")) Then
                        dgvRow.Cells(6).Value = False
                    Else
                        dgvRow.Cells(6).Value = dr.Item("intESAccess")
                    End If
                    dgvUsers.Rows.Add(dgvRow)
                End While
                dr.Close()

                da = New OracleDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.Fill(ds, "FacilityUsers")

                cboUsers.DataSource = ds.Tables("FacilityUsers")
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "ID"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer

            SQL = "Select numUserId " & _
            "from " & connNameSpace & ".olapuserlogin " & _
            "where struseremail = '" & Replace(UCase(txtEmail.Text), "'", "''") & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then 'Email address is registered
                userID = dr.Item("numUserId")
                Dim InsertString As String = "Insert into " & connNameSpace & ".OlapUserAccess " & _
                "(numUserId, strAirsNumber, strFacilityName) values( " & _
                "'" & userID & "', '0413" & mtbAIRSNumber.Text & "', '" & Replace(lblFaciltyName.Text, "'", "''") & "') "

                Dim cmd1 As New OracleCommand(InsertString, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd1.ExecuteNonQuery()

                ViewFacilitySpecificUsers()

                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            SQL = "DELETE " & connNameSpace & ".OlapUserAccess " & _
            "WHERE numUserID = '" & cboUsers.SelectedValue & "' " & _
            "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

            ViewFacilitySpecificUsers()
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i = 0 To dgvUsers.Rows.Count - 1
                If dgvUsers(3, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUsers(4, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUsers(5, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUsers(6, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE " & connNameSpace & ".OlapUserAccess " & _
                "SET intadminaccess = '" & adminaccess & "', " & _
                "intFeeAccess = '" & feeaccess & "', " & _
                "intEIAccess = '" & eiaccess & "', " & _
                "intESAccess = '" & esaccess & "' " & _
                "WHERE numUserID = '" & dgvUsers(1, i).Value & "' " & _
                "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            ViewFacilitySpecificUsers()
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub lblViewEmailData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        Try
            LoadUserInfo(txtWebUserEmail.Text)
            LoadUserFacilityInfo(txtWebUserEmail.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadUserInfo(ByVal UserData As String)
        Try
            SQL = "Select " & _
            "" & connNameSpace & ".OLAPUserProfile.numUserID, " & _
            "strfirstname, strlastname, " & _
            "strtitle, strcompanyname, " & _
            "straddress, strcity, " & _
            "strstate, strzip, " & _
            "strphonenumber, strfaxnumber, " & _
            "datLastLogIn, strConfirm, " & _
            "strUserEmail " & _
            "from " & connNameSpace & ".OlapUserProfile, " & connNameSpace & ".OLAPUserLogIn " & _
            "where " & connNameSpace & ".OLAPUserProfile.numUserID = " & connNameSpace & ".OLAPUserLogIn.numuserid " & _
            "and strUserEmail = upper('" & UserData & "') "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    txtEditFirstName.Clear()
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                    txtEditFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtEditLastName.Clear()
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                    txtEditLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    txtEditTitle.Clear()
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                    txtEditTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    txtEditCompany.Clear()
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                    txtEditCompany.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    txtEditAddress.Clear()
                Else
                    lblAddress.Text = dr.Item("straddress")
                    txtEditAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    txtEditCity.Clear()
                    mtbEditState.Clear()
                    mtbEditZipCode.Clear()
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                    txtEditCity.Text = dr.Item("strCity")
                    mtbEditState.Text = dr.Item("strState")
                    mtbEditZipCode.Text = dr.Item("strZip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    mtbEditPhoneNumber.Clear()
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                    mtbEditPhoneNumber.Text = dr.Item("strPhoneNumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    mtbEditFaxNumber.Clear()
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                    mtbEditFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strConfirm")) Then
                    lblConfirmDate.Text = ""
                Else
                    lblConfirmDate.Text = "Date User Email Confirmed: " & dr.Item("strConfirm")
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date User Last Logged In: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("strUserEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("strUserEmail")
                End If
            Else
                txtWebUserID.Clear()
                txtEditFirstName.Clear()
                txtEditLastName.Clear()
                txtEditTitle.Clear()
                txtEditCompany.Clear()
                txtEditAddress.Clear()
                txtEditCity.Clear()
                mtbEditState.Clear()
                mtbEditZipCode.Clear()
                mtbEditPhoneNumber.Clear()
                mtbEditFaxNumber.Clear()
                lblLastLogIn.Text = ""
                lblConfirmDate.Text = ""
                txtEditEmail.Clear()
            End If

            txtEditUserPassword.Clear()
            txtEditFirstName.Visible = False
            txtEditLastName.Visible = False
            txtEditTitle.Visible = False
            txtEditCompany.Visible = False
            txtEditAddress.Visible = False
            txtEditCity.Visible = False
            mtbEditState.Visible = False
            mtbEditZipCode.Visible = False
            mtbEditPhoneNumber.Visible = False
            mtbEditFaxNumber.Visible = False
            btnSaveEditedData.Visible = False
            txtEditUserPassword.Visible = False
            btnChangeEmailAddress.Visible = False
            txtEditEmail.Visible = False
            btnUpdatePassword.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadUserFacilityInfo(ByVal EmailLoc As String)
        Try
            Dim dgvRow As New DataGridViewRow

            SQL = "SELECT substr(strairsnumber, 5) as strAIRSNumber, strfacilityname, " & _
             "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
             "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
             "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
             "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
             "FROM " & connNameSpace & ".OlapUserAccess, " & connNameSpace & ".OLAPUserLogIn  " & _
             "WHERE " & connNameSpace & ".OlapUserAccess.numUserId = " & connNameSpace & ".OLAPUserLogIn.numUserId " & _
             "and  strUserEmail = upper('" & EmailLoc & "') " & _
             "order by strfacilityname"

            dgvUserFacilities.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvUserFacilities)
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strFacilityName")
                End If
          
                If IsDBNull(dr.Item("intAdminAccess")) Then
                    dgvRow.Cells(2).Value = False
                Else
                    dgvRow.Cells(2).Value = dr.Item("intAdminAccess")
                End If
                If IsDBNull(dr.Item("intFeeAccess")) Then
                    dgvRow.Cells(3).Value = False
                Else
                    dgvRow.Cells(3).Value = dr.Item("intFeeAccess")
                End If

                If IsDBNull(dr.Item("intEIAccess")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("intEIAccess")
                End If
                If IsDBNull(dr.Item("intESAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("intESAccess")
                End If
                dgvUserFacilities.Rows.Add(dgvRow)
            End While
            dr.Close()

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "FacilityUsers")

            cboFacilityToDelete.DataSource = ds.Tables("FacilityUsers")
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditUserData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUserData.Click
        Try
            txtEditFirstName.Visible = True
            txtEditLastName.Visible = True
            txtEditTitle.Visible = True
            txtEditCompany.Visible = True
            txtEditAddress.Visible = True
            txtEditCity.Visible = True
            mtbEditState.Visible = True
            mtbEditZipCode.Visible = True
            mtbEditPhoneNumber.Visible = True
            mtbEditFaxNumber.Visible = True
            btnSaveEditedData.Visible = True
            txtEditUserPassword.Visible = True
            btnChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
            btnUpdatePassword.Visible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEditedData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEditedData.Click
        Try
            Dim FirstName As String = " "
            Dim LastName As String = " "
            Dim Title As String = " "
            Dim Company As String = " "
            Dim Address As String = " "
            Dim City As String = " "
            Dim State As String = " "
            Dim Zip As String = " "
            Dim PhoneNumber As String = " "
            Dim FaxNumber As String = " "

            If txtWebUserID.Text <> "" Then
                If txtEditFirstName.Text <> "" Then
                    FirstName = " strFirstName = '" & Replace(txtEditFirstName.Text, "'", "''") & "', "
                End If
                If txtEditLastName.Text <> "" Then
                    LastName = " strLastName = '" & Replace(txtEditLastName.Text, "'", "''") & "', "
                End If
                If txtEditTitle.Text <> "" Then
                    Title = " strTitle = '" & Replace(txtEditTitle.Text, "'", "''") & "', "
                End If
                If txtEditCompany.Text <> "" Then
                    Company = " strCompanyName = '" & Replace(txtEditCompany.Text, "'", "''") & "', "
                End If
                If txtEditAddress.Text <> "" Then
                    Address = " strAddress = '" & Replace(txtEditAddress.Text, "'", "''") & "', "
                End If
                If txtEditCity.Text <> "" Then
                    City = " strCity = '" & Replace(txtEditCity.Text, "'", "''") & "', "
                End If
                If mtbEditState.Text <> "" Then
                    State = " strState = '" & Replace(mtbEditState.Text, "'", "''") & "', "
                End If
                If mtbEditZipCode.Text <> "" Then
                    Zip = " strZip = '" & Replace(mtbEditZipCode.Text, "'", "''") & "', "
                End If
                If mtbEditPhoneNumber.Text <> "" Then
                    PhoneNumber = " strPhoneNumber = '" & Replace(mtbEditPhoneNumber.Text, "'", "''") & "', "
                End If
                If mtbEditFaxNumber.Text <> "" Then
                    FaxNumber = " strFaxNumber = '" & Replace(mtbEditFaxNumber.Text, "'", "''") & "', "
                End If

                SQL = "Update " & connNameSpace & ".OLAPUserProfile set " & _
                FirstName & LastName & Title & Company & Address & _
                City & State & Zip & PhoneNumber & FaxNumber & _
                "numUserID = '" & txtWebUserID.Text & "' " & _
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                lblFName.Text = "First Name: " & txtEditFirstName.Text
                lblLName.Text = "Last Name: " & txtEditLastName.Text
                lblTitle.Text = "Title: " & txtEditTitle.Text
                lblCoName.Text = "Company Name: " & txtEditCompany.Text
                lblAddress.Text = txtEditAddress.Text
                lblCityStateZip.Text = txtEditCity.Text & ", " & mtbEditState.Text & " " & mtbEditZipCode.Text
                lblPhoneNo.Text = "Phone Number: " & mtbEditPhoneNumber.Text
                lblFaxNo.Text = "Fax Number: " & mtbEditFaxNumber.Text

                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                'New password change code 6/30/2010
                SQL = "Update " & connNameSpace & ".OLAPUserLogIN set " & _
                "strUserPassword = '" & getMd5Hash(txtEditUserPassword.Text) & "' " & _
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtEditUserPassword.Clear()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnChangeEmailAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeEmailAddress.Click
        Try
            If txtWebUserID.Text <> "" Then
                If EmailAddressCheck(txtEditEmail.Text) = True Then
                    SQL = "Select " & _
                    "numUserID, strUserPassword " & _
                    "from " & connNameSpace & ".OLAPUserLogIN " & _
                    "where upper(strUserEmail) = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("numUserID")) Then
                            Else
                                If txtWebUserID.Text <> dr.Item("numUserID") Then
                                    MsgBox("Another user already has this email address and it would violate a unique constraint if you were " & _
                                           "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                                    Exit Sub
                                End If
                            End If
                        End While
                        dr.Close()
                    End If

                    SQL = "Update " & connNameSpace & ".OLAPUserLogIn set " & _
                    "strUserEmail = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' " & _
                    "where numUserID = '" & txtWebUserID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboUserEmail.Text = ""
                    txtWebUserEmail.Text = txtEditEmail.Text

                    '  LoadDataGridFacility(txtWebUserEmail.Text)
                    LoadUserInfo(txtWebUserEmail.Text)

                    If txtWebUserID.Text = "" Then
                        pnlUserInfo.Visible = False
                        pnlUserFacility.Visible = False
                    Else
                        pnlUserInfo.Visible = True
                        pnlUserFacility.Visible = True
                    End If

                Else
                    MsgBox("Invalid Email Address", MsgBoxStyle.Exclamation, "DMU Tools")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddFacilitytoUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                SQL = "Select " & _
                "numUserId " & _
                "from " & connNameSpace & ".OlapUserAccess " & _
                "where numUserId = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & mtbFacilityToAdd.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = False Then
                    SQL = "Insert into " & connNameSpace & ".OlapUserAccess " & _
                     "(numUserId, strAirsNumber, strFacilityName) " & _
                     "values " & _
                     "('" & txtWebUserID.Text & "', '0413" & mtbFacilityToAdd.Text & "', " & _
                     "(select strFacilityName " & _
                     "from " & connNameSpace & ".APBFacilityInformation " & _
                     "where strAIRSnumber = '0413" & mtbFacilityToAdd.Text & "')) "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    LoadUserFacilityInfo(txtWebUserEmail.Text)
                    MsgBox("The facility has beed added to this user", MsgBoxStyle.Information, "Insert Success!")
                Else
                    MsgBox("The facility already exists for this user." & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnDeleteFacilityUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                SQL = "DELETE " & connNameSpace & ".OlapUserAccess " & _
                "WHERE numUserID = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & cboFacilityToDelete.SelectedValue & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

                LoadUserFacilityInfo(txtWebUserEmail.Text)
                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i = 0 To dgvUserFacilities.Rows.Count - 1
                If dgvUserFacilities(2, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUserFacilities(3, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUserFacilities(4, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUserFacilities(5, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE " & connNameSpace & ".OlapUserAccess " & _
                "SET intadminaccess = '" & adminaccess & "', " & _
                "intFeeAccess = '" & feeaccess & "', " & _
                "intEIAccess = '" & eiaccess & "', " & _
                "intESAccess = '" & esaccess & "' " & _
                "WHERE numUserID = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & dgvUserFacilities(0, i).Value & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveAddition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAddition.Click
        Try
            If btnSetMailoutDate.Enabled = True Then

                SQL = "Insert into " & connNameSpace & ".FS_Admin " & _
                "(numFeeYear, strAIRSNumber, " & _
                "strInitialMailout, " & _
                "active, updateUser, " & _
                "updateDateTime, createDatetime) " & _
                "values " & _
                "('" & cboAvailableFeeYears.Text & "', '0413" & mtbCheckAIRSNumber.Text & "', " & _
                "'1', " & _
                "'1', '" & UserGCode & "', " & _
                "sysdate, sysdate ) "
            Else
                SQL = "Insert into " & connNameSpace & ".FS_Admin " & _
               "(numFeeYear, strAIRSNumber, " & _
               "strEnrolled, datInitialEnrollment, " & _
               "active, updateUser, " & _
               "updateDateTime, createDatetime) " & _
               "values " & _
               "('" & cboAvailableFeeYears.Text & "', '0413" & mtbCheckAIRSNumber.Text & "', " & _
               "'1', sysdate, " & _
               "'1', '" & UserGCode & "', " & _
               "sysdate, sysdate ) "
            End If

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'dr.Close()

            'Insert into FS_FeeData
            SQL = "Insert into " & connNameSpace & ".FS_FeeData " & _
            "("


            'Insert into FS_ContactInfo 
            SQL = ""


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbCheckFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCheckFacility.LinkClicked
        Try
            If cboAvailableFeeYears.Text = "" Or cboAvailableFeeYears.Text.Length <> "4" Then
                MsgBox("Please select a year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
            "strFacilityName " & _
            "from " & connNameSpace & ".APBFacilityInformation  " & _
           "where " & connNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & mtbCheckAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtCheckFacility.Clear()
                Else
                    txtCheckFacility.Text = dr.Item("strFacilityname")
                End If
            End While
            dr.Close()

            lblCheckState.Text = "N/A"

            SQL = "Select " & _
            "strEnrolled, " & _
            "strInitialMailout, strMailoutSent " & _
            "from " & connNameSpace & ".FS_Admin " & _
            "where numfeeyear = '" & cboAvailableFeeYears.Text & "'  " & _
            "and " & connNameSpace & ".FS_Admin.strAIRSNumber = '0413" & mtbCheckAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnrolled")) Then
                    lblCheckState.Text = "N/A"
                Else
                    lblCheckState.Text = "Enrolled"
                End If
                If lblCheckState.Text = "N/A" Then
                    If IsDBNull(dr.Item("strInitalMailOut")) Then
                        lblCheckState.Text = "N/A"
                    Else
                        lblCheckState.Text = "Mailout"
                    End If
                End If
                If lblCheckState.Text = "N/A" Then
                    If IsDBNull(dr.Item("strMailoutSent")) Then
                        lblCheckState.Text = "N/A"
                    Else
                        lblCheckState.Text = "Mailout Sent"
                    End If
                End If
            End While
            dr.Close()
            If lblCheckState.Text = "N/A" Then
                btnSaveAddition.Enabled = True
                lblCheckState.Text = ""
                MsgBox("Facility is not in the mailout or enrolled", MsgBoxStyle.Information, Me.Text)
            Else
                btnSaveAddition.Enabled = False
                lblCheckState.Text = ""
                MsgBox("Facility is already in the mailout or enrolled", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboAvailableFeeYears_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAvailableFeeYears.TextChanged
        Try
            If cboAvailableFeeYears.Text >= (Today.Year - 1) Then
                btnFirstEnrollment.Enabled = True
                btnUnenrollFeeYear.Enabled = True
                btnUpdateContactData.Enabled = True
                btnSetMailoutDate.Enabled = True
                btnSaveAddition.Enabled = True
            Else
                btnFirstEnrollment.Enabled = False
                btnUnenrollFeeYear.Enabled = False
                btnUpdateContactData.Enabled = False
                btnSetMailoutDate.Enabled = False
                btnSaveAddition.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFeesLog.Click
        Try
            If FeeStats Is Nothing Then
                If FeeStats Is Nothing Then FeeStats = New PASPFeeAuditLog
            Else
                FeeStats.Dispose()
                FeeStats = New PASPFeeAuditLog
            End If
            FeeStats.Show()
            FeeStats.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

            FeeStats.mtbFeeAdminAIRSNumber.Text = mtbCheckAIRSNumber.Text
            FeeStats.txtFeeAdminFacilityName.Text = txtCheckFacility.Text
            FeeStats.mtbFeeAdminExistingYear.Text = cboAvailableFeeYears.Text

            If FeeStats.mtbFeeAdminAIRSNumber.Text <> "" Then
                FeeStats.LoadAdminData()
                FeeStats.LoadAuditedData()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class