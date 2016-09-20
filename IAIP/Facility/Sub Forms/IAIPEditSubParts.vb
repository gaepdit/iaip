Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Iaip.SharedData
Imports Iaip.Apb.Facilities

Public Class IAIPEditSubParts

#Region " Properties "

    Dim _AirsNumber As Apb.ApbFacilityId
    Property AirsNumber As Apb.ApbFacilityId
        Get
            Return _AirsNumber
        End Get
        Set(value As Apb.ApbFacilityId)
            _AirsNumber = value
            AirsNumberDisplay.Text = value.FormattedString
            LoadFacilityInformation()
        End Set
    End Property

    Private Structure SubpartItem
        Property Subpart As String
        Property Description As String
        ReadOnly Property LongDescription As String
            Get
                Return Subpart & " – " & Description
            End Get
        End Property

        Public Sub New(subpart As String, description As String)
            Me.Subpart = subpart
            Me.Description = description
        End Sub
    End Structure

#End Region

#Region " Page Load "

    Private Sub IAIPEditSubParts_Load(sender As Object, e As EventArgs) Handles Me.Load
        AirsNumberDisplay.Text = ""
        txtFacilityName.Text = ""
        SetPermissions()
        SetUpDataGridViews()
        SetUpComboBoxes()
        SetUpCheckListBoxes()
    End Sub

    Private Sub SetUpDataGridViews()
        With dgvNSPS
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts)
            .DataMember = RulePart.NSPS.ToString
            .Columns("Long Description").Visible = False
            .SanelyResizeColumns()
        End With
        With dgvNESHAP
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts)
            .DataMember = RulePart.NESHAP.ToString
            .Columns("Long Description").Visible = False
            .SanelyResizeColumns()
        End With
        With dgvMACT
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts)
            .DataMember = RulePart.MACT.ToString
            .Columns("Long Description").Visible = False
            .SanelyResizeColumns()
        End With
        With dgvSIP
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts)
            .DataMember = RulePart.SIP.ToString
            .Columns("Long Description").Visible = False
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub SetUpComboBoxes()
        With cboSIPSubpart
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.SIP.ToString)
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = -1
        End With

        With cboNSPSSubpart
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NSPS.ToString)
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = -1
        End With

        With cboNESHAPSubpart
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.NESHAP.ToString)
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = -1
        End With

        With cboMACTSubPart
            .DataSource = GetSharedData(SharedDataSet.RuleSubparts).Tables(RulePart.MACT.ToString)
            .DisplayMember = "Long Description"
            .ValueMember = "Subpart"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub SetUpCheckListBoxes()
        clbSIP.DisplayMember = "LongDescription"
        clbSIP.ValueMember = "Subpart"

        clbNSPS.DisplayMember = "LongDescription"
        clbNSPS.ValueMember = "Subpart"

        clbNESHAP.DisplayMember = "LongDescription"
        clbNESHAP.ValueMember = "Subpart"

        clbMACT.DisplayMember = "LongDescription"
        clbMACT.ValueMember = "Subpart"
    End Sub

    Private Sub SetPermissions()
        If Not (AccountFormAccess(26, 1) = "1" Or
            AccountFormAccess(26, 2) = "1" Or
            AccountFormAccess(26, 3) = "1" Or
            AccountFormAccess(26, 4) = "1" Or
            CurrentUser.HasRole({19, 113, 114, 141})) Then

            DisableSaving()

        End If
    End Sub

    Private Sub DisableSaving()
        DisableControls({btnSaveSIPSubpart, btnRemoveSIPSubpart, btnSaveNSPSSubpart, btnRemoveNSPSSubpart,
                        btnSaveNESHAPSubpart, btnRemoveNESHAPSubpart, btnAddMACTSubpart, btnRemoveMACTSubPart,
                        btnEditSIP, btnEditNSPS, btnEditNESHAP, btnEditMACT})
    End Sub

#End Region

#Region " Load facility information "

    Private Sub LoadFacilityInformation()
        Try
            If AirsNumber Is Nothing Then
                TCSubparts.Enabled = False
                Exit Sub
            End If

            TCSubparts.Enabled = True

            Dim SQL As String = "Select " &
                "strFacilityName, strAirProgramCodes  " &
                "from APBFacilityInformation inner join APBHeaderData " &
                "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSnumber " &
                "where APBFacilityInformation.strAIRSnumber = @airs "

            Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            Dim AirProgramCodes As String = ""

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtFacilityName.Text = "ERROR"
                Else
                    txtFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    AirProgramCodes = "000000000000000"
                Else
                    AirProgramCodes = dr.Item("strAirProgramCodes")
                End If
            Else
                txtFacilityName.Text = "ERROR"
                AirProgramCodes = "000000000000000"
            End If

            SQL = "Select " &
                "strSubPartKey, strSubPart " &
                "from APBSubpartData " &
                "where APBSubpartData.strAIRSnumber = @airs " &
                "and Active = '1' " &
                "order by strSubPart "

            Dim dt As DataTable = DB.GetDataTable(SQL, p)

            Dim subpart As String

            For Each dr2 As DataRow In dt.Rows
                If Not IsDBNull(dr2.Item("strSubPartKey")) AndAlso Not IsDBNull(dr2.Item("strSubPart")) Then
                    subpart = dr2.Item("strSubPart")

                    Select Case Mid(dr2.Item("strSubPartKey"), 13)
                        Case "0"
                            LoadDescription(subpart, RulePart.SIP)
                        Case "9"
                            LoadDescription(subpart, RulePart.NSPS)
                        Case "8"
                            LoadDescription(subpart, RulePart.NESHAP)
                        Case "M"
                            LoadDescription(subpart, RulePart.MACT)
                    End Select
                End If
            Next

            If Mid(AirProgramCodes, 1, 1) = "0" Then
                TCSubparts.TabPages.Remove(TPSIP)
            End If

            If Mid(AirProgramCodes, 8, 1) = "0" Then
                TCSubparts.TabPages.Remove(TPPart60)
            End If

            If Mid(AirProgramCodes, 7, 1) = "0" Then
                TCSubparts.TabPages.Remove(TPPart61)
            End If

            If Mid(AirProgramCodes, 12, 1) = "0" Then
                TCSubparts.TabPages.Remove(TPPart63)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadDescription(subpart As String, rulePart As RulePart)
        Dim dr As DataRow = GetSharedData(SharedDataSet.RuleSubparts).Tables(rulePart.ToString).Rows.Find(subpart)
        If dr IsNot Nothing Then
            Dim spi As New SubpartItem(dr(0), dr(1))
            Select Case rulePart
                Case RulePart.SIP
                    If Not clbSIP.Items.Contains(spi) Then
                        clbSIP.Items.Add(spi)
                    End If
                Case RulePart.NSPS
                    If Not clbNSPS.Items.Contains(spi) Then
                        clbNSPS.Items.Add(spi)
                    End If
                Case RulePart.NESHAP
                    If Not clbNESHAP.Items.Contains(spi) Then
                        clbNESHAP.Items.Add(spi)
                    End If
                Case RulePart.MACT
                    If Not clbMACT.Items.Contains(spi) Then
                        clbMACT.Items.Add(spi)
                    End If
            End Select
        End If
    End Sub

#End Region

#Region " Facility data editing "

    Private Sub AddSubpartToFacility(rulePart As RulePart)
        Dim key As String = ""
        Dim subpart As String = ""

        Select Case rulePart
            Case RulePart.MACT
                key = AirsNumber.DbFormattedString & "M"
                subpart = cboMACTSubPart.SelectedValue
            Case RulePart.NESHAP
                key = AirsNumber.DbFormattedString & "8"
                subpart = cboNESHAPSubpart.SelectedValue
            Case RulePart.NSPS
                key = AirsNumber.DbFormattedString & "9"
                subpart = cboNSPSSubpart.SelectedValue
            Case RulePart.SIP
                key = AirsNumber.DbFormattedString & "0"
                subpart = cboSIPSubpart.SelectedValue
        End Select

        Dim SqlList As New List(Of String)
        Dim ParamList As New List(Of SqlParameter())

        Dim SQL As String = "Select " &
            "1 " &
            "from APBSubpartData " &
            "where strSubpartKey = @key " &
            "and strSubpart = @subpart COLLATE Latin1_General_100_CS_AS "

        Dim p As SqlParameter() = {
            New SqlParameter("@airs", AirsNumber.DbFormattedString),
            New SqlParameter("@user", CurrentUser.UserID),
            New SqlParameter("@key", key),
            New SqlParameter("@subpart", subpart)
        }

        If DB.ValueExists(SQL, p) Then
            SqlList.Add("Update APBSubpartData set " &
                        "Active = '1', " &
                        "UpdateUser = @user, " &
                        "updateDateTime = getdate() " &
                        "where strSubpartKey = @key " &
                        "and strSubpart = @subpart COLLATE Latin1_General_100_CS_AS ")
            ParamList.Add(p)
        Else
            SqlList.Add("INSERT INTO APBSUBPARTDATA " &
                        "  ( STRAIRSNUMBER, STRSUBPARTKEY, " &
                        "    STRSUBPART, UPDATEUSER , " &
                        "    UPDATEDATETIME, ACTIVE, CREATEDATETIME " &
                        "  ) VALUES " &
                        "(@airs, @key, " &
                        " @subpart, @user, " &
                        " getdate(), '1', getdate()) ")
            ParamList.Add(p)
        End If

        SqlList.Add("Update AFSAirPollutantData set " &
                    "strUpdateStatus = 'C' " &
                    "where strAirPollutantKey = @key " &
                    "and strUpdateStatus = 'N' ")
        ParamList.Add(p)

        DB.RunCommand(SqlList, ParamList)

        LoadDescription(subpart, rulePart)
    End Sub

    Private Sub RemoveSubpartFromFacility(rulePart As RulePart)
        Dim SqlList As New List(Of String)
        Dim ParamList As New List(Of SqlParameter())

        Dim clb As New CheckedListBox
        Dim ruleKey As String = ""

        Select Case rulePart
            Case RulePart.MACT
                clb = clbMACT
                ruleKey = "M"
            Case RulePart.NESHAP
                clb = clbNESHAP
                ruleKey = "8"
            Case RulePart.NSPS
                clb = clbNSPS
                ruleKey = "9"
            Case RulePart.SIP
                clb = clbSIP
                ruleKey = "0"
        End Select

        For Each spi As SubpartItem In clb.CheckedItems
            SqlList.Add("Update APBSubpartData set " &
                        "active = '0', " &
                        "UpdateUser = @user, " &
                        "updateDateTime = getdate() " &
                        "where strSubPartKey = @key " &
                        "and strSubpart = @subpart COLLATE Latin1_General_100_CS_AS ")
            ParamList.Add({New SqlParameter("@user", CurrentUser.UserID),
                          New SqlParameter("@key", AirsNumber.DbFormattedString & ruleKey),
                          New SqlParameter("@subpart", spi.Subpart)})
        Next

        If DB.RunCommand(SqlList, ParamList) Then
            Dim toRemove As New List(Of SubpartItem)

            For Each spi As SubpartItem In clb.CheckedItems
                toRemove.Add(spi)
            Next

            For Each spi As SubpartItem In toRemove
                clb.Items.Remove(spi)
            Next
        End If
    End Sub

    Private Sub btnSaveSIPSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveSIPSubpart.Click
        If cboSIPSubpart.SelectedIndex <> -1 Then
            AddSubpartToFacility(RulePart.SIP)
        End If
    End Sub

    Private Sub btnSaveNSPSSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveNSPSSubpart.Click
        If cboNSPSSubpart.SelectedIndex <> -1 Then
            AddSubpartToFacility(RulePart.NSPS)
        End If
    End Sub

    Private Sub btnSaveNESHAPSubpart_Click(sender As Object, e As EventArgs) Handles btnSaveNESHAPSubpart.Click
        If cboNESHAPSubpart.SelectedIndex <> -1 Then
            AddSubpartToFacility(RulePart.NESHAP)
        End If
    End Sub

    Private Sub btnAddMACTSubpart_Click(sender As Object, e As EventArgs) Handles btnAddMACTSubpart.Click
        If cboMACTSubPart.SelectedIndex <> -1 Then
            AddSubpartToFacility(RulePart.MACT)
        End If
    End Sub

    Private Sub btnRemoveSIPSubpart_Click(sender As Object, e As EventArgs) Handles btnRemoveSIPSubpart.Click
        If clbSIP.CheckedItems.Count > 0 Then
            RemoveSubpartFromFacility(RulePart.SIP)
        End If
    End Sub

    Private Sub btnRemoveNSPSSubpart_Click(sender As Object, e As EventArgs) Handles btnRemoveNSPSSubpart.Click
        If clbNSPS.CheckedItems.Count > 0 Then
            RemoveSubpartFromFacility(RulePart.NSPS)
        End If
    End Sub

    Private Sub btnRemoveNESHAPSubpart_Click(sender As Object, e As EventArgs) Handles btnRemoveNESHAPSubpart.Click
        If clbNESHAP.CheckedItems.Count > 0 Then
            RemoveSubpartFromFacility(RulePart.NESHAP)
        End If
    End Sub

    Private Sub btnRemoveMACTSubPart_Click(sender As Object, e As EventArgs) Handles btnRemoveMACTSubPart.Click
        If clbMACT.CheckedItems.Count > 0 Then
            RemoveSubpartFromFacility(RulePart.MACT)
        End If
    End Sub

#End Region

#Region " Subpart editing "

    Private Sub btnEditSIP_Click(sender As Object, e As EventArgs) Handles btnEditSIP.Click
        UpdateSubpart(RulePart.SIP)
    End Sub

    Private Sub btnEditNSPS_Click(sender As Object, e As EventArgs) Handles btnEditNSPS.Click
        UpdateSubpart(RulePart.NSPS)
    End Sub

    Private Sub btnEditNESHAP_Click(sender As Object, e As EventArgs) Handles btnEditNESHAP.Click
        UpdateSubpart(RulePart.NESHAP)
    End Sub

    Private Sub btnEditMACT_Click(sender As Object, e As EventArgs) Handles btnEditMACT.Click
        UpdateSubpart(RulePart.MACT)
    End Sub

    Private Sub UpdateSubpart(rulePart As RulePart)
        Dim tableName As String = ""
        Dim spi As New SubpartItem

        Select Case rulePart
            Case RulePart.MACT
                spi = New SubpartItem(txtMACTCode.Text, txtMACTDescription.Text)
                tableName = "LOOKUPSUBPART63"
            Case RulePart.NESHAP
                spi = New SubpartItem(txtNESHAPCode.Text, txtNESHAPDescription.Text)
                tableName = "LOOKUPSUBPART61"
            Case RulePart.NSPS
                spi = New SubpartItem(txtNSPSCode.Text, txtNSPSDescription.Text)
                tableName = "LOOKUPSUBPART60"
            Case RulePart.SIP
                spi = New SubpartItem(txtSIPCode.Text, txtSIPDescription.Text)
                tableName = "LOOKUPSUBPARTSIP"
        End Select

        If Not (String.IsNullOrWhiteSpace(spi.Subpart) Or String.IsNullOrWhiteSpace(spi.Description)) Then

            Dim SQL As String = "Select 1 From " & tableName & " where strSubpart = @subpart COLLATE Latin1_General_100_CS_AS "

            Dim p As SqlParameter() = {
                New SqlParameter("@subpart", spi.Subpart),
                New SqlParameter("@desc", spi.Description)
            }

            If DB.ValueExists(SQL, p) Then
                SQL = "Update " & tableName & " set " &
                    "strDescription = @desc " &
                    "where strSubpart = @subpart COLLATE Latin1_General_100_CS_AS "
            Else
                SQL = "Insert into " & tableName &
                    "(strSubpart, strDescription) " &
                    "values " &
                    "(@subpart, @desc) "
            End If

            If DB.RunCommand(SQL, p) Then
                Dim dr As DataRow = GetSharedData(SharedDataSet.RuleSubparts).Tables(rulePart.ToString).Rows.Find(spi.Subpart)

                If dr Is Nothing Then
                    GetSharedData(SharedDataSet.RuleSubparts).Tables(rulePart.ToString).Rows.Add({spi.Subpart, spi.Description, spi.LongDescription})
                Else
                    dr.Item("Description") = spi.Description
                    dr.Item("Long Description") = spi.LongDescription
                End If

                SetUpDataGridViews()
            Else
                MessageBox.Show("There was an error updating the Subpart.")
            End If
        Else
            MessageBox.Show("Enter values for the subpart code and description first.")
        End If
    End Sub

    Private Sub btnClearSIP_Click(sender As Object, e As EventArgs) Handles btnClearSIP.Click
        txtSIPCode.Clear()
        txtSIPDescription.Clear()
    End Sub

    Private Sub btnClearNSPS_Click(sender As Object, e As EventArgs) Handles btnClearNSPS.Click
        txtNSPSCode.Clear()
        txtNSPSDescription.Clear()
    End Sub

    Private Sub btnClearNESHAP_Click(sender As Object, e As EventArgs) Handles btnClearNESHAP.Click
        txtNESHAPCode.Clear()
        txtNESHAPDescription.Clear()
    End Sub

    Private Sub btnClearMACT_Click(sender As Object, e As EventArgs) Handles btnClearMACT.Click
        txtMACTCode.Clear()
        txtMACTDescription.Clear()
    End Sub

#End Region

#Region " DGV selection events "

    Private Sub dgvMACT_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMACT.SelectionChanged
        If dgvMACT.SelectedRows.Count = 1 Then
            txtMACTCode.Text = dgvMACT.CurrentRow.Cells("Subpart").Value
            txtMACTDescription.Text = dgvMACT.CurrentRow.Cells("Description").Value
        End If
    End Sub

    Private Sub dgvNESHAP_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNESHAP.SelectionChanged
        If dgvNESHAP.SelectedRows.Count = 1 Then
            txtNESHAPCode.Text = dgvNESHAP.CurrentRow.Cells("Subpart").Value
            txtNESHAPDescription.Text = dgvNESHAP.CurrentRow.Cells("Description").Value
        End If
    End Sub

    Private Sub dgvNSPS_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNSPS.SelectionChanged
        If dgvNSPS.SelectedRows.Count = 1 Then
            txtNSPSCode.Text = dgvNSPS.CurrentRow.Cells("Subpart").Value
            txtNSPSDescription.Text = dgvNSPS.CurrentRow.Cells("Description").Value
        End If
    End Sub

    Private Sub dgvSIP_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSIP.SelectionChanged
        If dgvSIP.SelectedRows.Count = 1 Then
            txtSIPCode.Text = dgvSIP.CurrentRow.Cells("Subpart").Value
            txtSIPDescription.Text = dgvSIP.CurrentRow.Cells("Description").Value
        End If
    End Sub

    Private Sub TCSubparts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCSubparts.SelectedIndexChanged

    End Sub

#End Region

End Class