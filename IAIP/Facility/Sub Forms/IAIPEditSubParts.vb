Imports System.Collections.Generic
Imports System.Data.SqlClient
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
        SetUpComboBoxes()
        SetUpCheckListBoxes()
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
        If Not (AccountFormAccess(26, 1) = "1" OrElse
            AccountFormAccess(26, 2) = "1" OrElse
            AccountFormAccess(26, 3) = "1" OrElse
            AccountFormAccess(26, 4) = "1" OrElse
            CurrentUser.HasRole({19, 113, 114, 141})) Then

            DisableSaving()

        End If
    End Sub

    Private Sub DisableSaving()
        DisableControls({btnSaveSIPSubpart, btnRemoveSIPSubpart, btnSaveNSPSSubpart, btnRemoveNSPSSubpart,
                        btnSaveNESHAPSubpart, btnRemoveNESHAPSubpart, btnAddMACTSubpart, btnRemoveMACTSubPart})
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
                    txtFacilityName.Text = dr.Item("strFacilityName").ToString
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    AirProgramCodes = "000000000000000"
                Else
                    AirProgramCodes = dr.Item("strAirProgramCodes").ToString
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
                    subpart = dr2.Item("strSubPart").ToString

                    Select Case Mid(dr2.Item("strSubPartKey").ToString, 13).ToString
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
            Dim spi As New SubpartItem(dr(0).ToString, dr(1).ToString)
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
                subpart = cboMACTSubPart.SelectedValue.ToString
            Case RulePart.NESHAP
                key = AirsNumber.DbFormattedString & "8"
                subpart = cboNESHAPSubpart.SelectedValue.ToString
            Case RulePart.NSPS
                key = AirsNumber.DbFormattedString & "9"
                subpart = cboNSPSSubpart.SelectedValue.ToString
            Case RulePart.SIP
                key = AirsNumber.DbFormattedString & "0"
                subpart = cboSIPSubpart.SelectedValue.ToString
        End Select

        Dim SqlList As New List(Of String)
        Dim ParamList As New List(Of SqlParameter())

        Dim SQL As String = "Select " &
            "1 " &
            "from APBSubpartData " &
            "where strSubpartKey = @key " &
            "and strSubpart = @subpart "

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
                        "and strSubpart = @subpart ")
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

        Dim clb As CheckedListBox
        Dim ruleKey As String

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
            Case Else
                Exit Sub
        End Select

        For Each spi As SubpartItem In clb.CheckedItems
            SqlList.Add("Update APBSubpartData set " &
                        "active = '0', " &
                        "UpdateUser = @user, " &
                        "updateDateTime = getdate() " &
                        "where strSubPartKey = @key " &
                        "and strSubpart = @subpart ")
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

End Class