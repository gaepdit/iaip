'Imports System.DateTime
Imports Oracle.DataAccess.Client


Public Class IAIPFacilityLookUpTool
    Dim dsSearch As New DataSet
    Dim daSearch As OracleDataAdapter
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader

    Public ReadOnly Property SelectedAirsNumber() As String
        Get
            Return txtAIRSNumber.Text
        End Get
    End Property

    Private Sub IAIPFacilityLookUpTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)

        If Me.Modal Then
            btnCancel.Enabled = True
            btnCancel.Visible = True
            btnUseAIRSNumber.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

#Region "Procedures"

    Private Sub SearchBy(ByVal SearchItem As String)

        Try
            Select Case SearchItem
                Case "Airs Number"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where strAirsNumber Like '%" & txtAIRSNumberSearch.Text & "%'"
                Case "City"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityCity) Like Upper('%" & Replace(txtCityNameSearch.Text, "'", "''") & "%')"
                Case "County"
                    SQL = "Select " & _
                     "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                     "strFacilityCity, " & _
                     "strFacilityStreet1, strCountyName " & _
                     "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".LookUpCountyInformation " & _
                     "where substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                     "and upper(strCountyName) like Upper('%" & txtCountyNameSearch.Text & "%') "
                Case "Facility Name"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')"
                Case "Historical Name"
                    SQL = "Select " & _
                    "strFacilityName, " & _
                    "substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')" & _
                    "Union " & _
                    "Select " & _
                    "distinct(strFacilityName) as strFacilityName, " & _
                    "substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".HB_APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')" & _
                    "Union " & _
                    "select " & _
                    "Distinct(strFacilityname) as strFacilityname,  " & _
                    "substr(strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity, strFacilityStreet1  " & _
                    "from " & DBNameSpace & ".SSPPApplicationData, " & DBNameSpace & ".SSPPApplicationMaster   " & _
                    "where " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
                    "and upper(strFacilityname) like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%') "
                Case "SIC Code"
                    SQL = "Select " & _
                    "strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS, " & _
                    "strSICCode, " & _
                    "strFacilityCity, strFacilityStreet1 " & _
                    "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBHeaderData " & _
                    "where Upper(" & DBNameSpace & ".APBHeaderData.strSICCode) Like Upper('%" & Replace(txtSICCodeSearch.Text, "'", "''") & "%') " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber"
                Case "Subpart"
                    If rdbPart60.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & DBNameSpace & ".LookUpsubPart60.strSubPart|| ' - '||" & DBNameSpace & ".LookUpSubpart60.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBSubpartData,  " & _
                        "" & DBNameSpace & ".LookUPSubPart60  " & _
                        "where  " & _
                        "" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".APBSubpartData.strSubPart = " & DBNameSpace & ".LookUpSubPart60.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '9'  " & _
                        "and (" & DBNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbPart61.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & DBNameSpace & ".LookUpsubPart61.strSubPart|| ' - '||" & DBNameSpace & ".LookUpSubpart61.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBSubpartData,  " & _
                        "" & DBNameSpace & ".LookUPSubPart61  " & _
                        "where  " & _
                        "" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".APBSubpartData.strSubPart = " & DBNameSpace & ".LookUpSubPart61.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '8'  " & _
                        "and (" & DBNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbPart63.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & DBNameSpace & ".LookUpsubPart63.strSubPart|| ' - '||" & DBNameSpace & ".LookUpSubpart63.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBSubpartData,  " & _
                        "" & DBNameSpace & ".LookUPSubPart63  " & _
                        "where  " & _
                        "" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".APBSubpartData.strSubPart = " & DBNameSpace & ".LookUpSubPart63.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = 'M'  " & _
                        "and (" & DBNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbGASIP.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & DBNameSpace & ".LookUpSubPartSIP.strSubPart|| ' - '||" & DBNameSpace & ".LookUpSubpartSIP.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBSubpartData,  " & _
                        "" & DBNameSpace & ".LookUPSubPartSIP " & _
                        "where  " & _
                        "" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".APBSubpartData.strSubPart = " & DBNameSpace & ".LookUpSubPartSIP.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '0'  " & _
                        "and (" & DBNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                Case "Zip Code"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1, strFacilityZipCode " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityZipCode) Like Upper('%" & Replace(txtZipCodeSearch.Text, "'", "''") & "%')"
                Case "Address"

                Case "Compliance"
                    ' SQL = "Select  " & _
                    '"strFacilityName, substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,   " & _
                    '"strFacilityCity,   " & _
                    '"strFacilityStreet1, (strLastName||', '||strFirstname) as Engineer    " & _
                    '"from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".SSCPFacilityAssignment, " & DBNameSpace & ".EPDUserProfiles   " & _
                    '"where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber   " & _
                    '"and " & DBNameSpace & ".SSCPFacilityAssignment.strSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID   " & _
                    '"and Upper(strLastName||', '||strFirstName) like Upper('%" & Replace(txtComplianceEngineer.Text, "'", "''") & "%')  "

                    SQL = "Select  " & _
                    " " & DBNameSpace & ".APBFacilityInformation.strFacilityName, " & _
                    "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,   " & _
                    " " & DBNameSpace & ".APBFacilityInformation.strFacilityCity,   " & _
                    " " & DBNameSpace & ".APBFacilityInformation.strFacilityStreet1, " & _
                    "(strLastName||', '||strFirstname) as Engineer    " & _
                    "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".VW_SSCPInspection_List, " & _
                    "" & DBNameSpace & ".EPDUserProfiles   " & _
                    "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413'||" & DBNameSpace & ".VW_SSCPInspection_List.AIRSNumber   " & _
                    "and " & DBNameSpace & ".VW_SSCPInspection_List.numSSCPEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID   " & _
                    "and Upper(strLastName||', '||strFirstName) like Upper('%" & Replace(txtComplianceEngineer.Text, "'", "''") & "%')  "

                Case Else

            End Select
            If SQL <> "" Then
                dsSearch = New DataSet
                daSearch = New OracleDataAdapter(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                daSearch.Fill(dsSearch, "FacSearch")

                dgvPossibleMatches.DataSource = dsSearch
                dgvPossibleMatches.DataMember = "FacSearch"

                dgvPossibleMatches.RowHeadersVisible = False
                dgvPossibleMatches.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvPossibleMatches.AllowUserToResizeColumns = True
                dgvPossibleMatches.AllowUserToAddRows = False
                dgvPossibleMatches.AllowUserToDeleteRows = False
                dgvPossibleMatches.AllowUserToOrderColumns = True
                dgvPossibleMatches.AllowUserToResizeRows = True
                dgvPossibleMatches.Columns("shortAIRS").HeaderText = "AIRS #"
                dgvPossibleMatches.Columns("shortAIRS").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvPossibleMatches.Columns("shortAIRS").DisplayIndex = 0
                dgvPossibleMatches.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvPossibleMatches.Columns("strFacilityName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvPossibleMatches.Columns("strFacilityName").DisplayIndex = 1
                dgvPossibleMatches.Columns("strFacilityCity").HeaderText = "City"
                dgvPossibleMatches.Columns("strFacilityCity").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvPossibleMatches.Columns("strFacilityCity").DisplayIndex = 2
                dgvPossibleMatches.Columns("strFacilityStreet1").HeaderText = "Facility Address"
                dgvPossibleMatches.Columns("strFacilityStreet1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvPossibleMatches.Columns("strFacilityStreet1").DisplayIndex = 3
                Select Case SearchItem
                    Case "Airs Number"

                    Case "City"

                    Case "County"
                        dgvPossibleMatches.Columns("strCountyName").HeaderText = "SIC Code"
                        dgvPossibleMatches.Columns("strCountyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvPossibleMatches.Columns("strCountyName").DisplayIndex = 4
                    Case "Facility Name"

                    Case "Historical Name"

                    Case "SIC Code"
                        dgvPossibleMatches.Columns("strSICCode").HeaderText = "SIC Code"
                        dgvPossibleMatches.Columns("strSICCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvPossibleMatches.Columns("strSICCode").DisplayIndex = 4
                    Case "Subpart"
                        dgvPossibleMatches.Columns("SubPartData").HeaderText = "Subpart 'Code - Description' "
                        dgvPossibleMatches.Columns("SubPartData").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvPossibleMatches.Columns("SubPartData").DisplayIndex = 4
                    Case "Zip Code"
                        dgvPossibleMatches.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                        dgvPossibleMatches.Columns("strFacilityZipCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvPossibleMatches.Columns("strFacilityZipCode").DisplayIndex = 4
                    Case "Address"

                    Case "Compliance"
                        dgvPossibleMatches.Columns("Engineer").HeaderText = "Compliance Engineer"
                        dgvPossibleMatches.Columns("Engineer").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvPossibleMatches.Columns("Engineer").DisplayIndex = 4
                    Case Else

                End Select

            End If


        Catch ex As Exception
            ErrorReport(SQL.ToString & vbCrLf & ex.ToString(), "FacilityLookUpTool.SearchBy")
        Finally

        End Try
    End Sub

    Private Sub ClearPage()
        txtAIRSNumber.Clear()
        txtAIRSNumberSearch.Clear()
        txtCityNameSearch.Clear()
        txtComplianceEngineer.Clear()
        txtCountyNameSearch.Clear()
        txtFacilityName.Clear()
        txtFacilityNameSearch.Clear()
        txtSICCodeSearch.Clear()
        txtZipCodeSearch.Clear()
    End Sub

#End Region

#Region "Search-by buttons"

    Private Sub btnAIRSNumberSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAIRSNumberSearch.Click
        SearchBy("Airs Number")
    End Sub
    Private Sub btnFacilityNameSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacilityNameSearch.Click
        If chbHistoricalNames.Checked = True Then
            SearchBy("Historical Name")
        Else
            SearchBy("Facility Name")
        End If
    End Sub
    Private Sub btnCitySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCitySearch.Click
        SearchBy("City")
    End Sub
    Private Sub btnComplianceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComplianceSearch.Click
        SearchBy("Compliance")
    End Sub
    Private Sub btnCountySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountySearch.Click
        SearchBy("County")
    End Sub
    Private Sub btnZipCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZipCodeSearch.Click
        SearchBy("Zip Code")
    End Sub
    Private Sub btnSICCodeSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSICCodeSearch.Click
        SearchBy("SIC Code")
    End Sub
    Private Sub btnSubpartSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubpartSearch.Click
        SearchBy("Subpart")
    End Sub

#End Region

#Region "Results"

    Private Sub btnUseAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUseAIRSNumber.Click
        Dim temp As String = ""

        Try

            temp = 8

            If Not ISMPFacility Is Nothing Then
                temp = 2
                ISMPFacility.ValueFromFacilityLookUp = txtAIRSNumber.Text
            End If
            If Not ISMPManagers Is Nothing Then
                temp = 3
                ISMPManagers.ValueFromFacilityLookUp = txtAIRSNumber.Text
                ISMPManagers.ValueFromFacilityLookUp2 = txtFacilityName.Text
            End If
            'If Not FacilitySummary Is Nothing Then
            '    temp = 4
            '    FacilitySummary.ValueFromFacilityLookUp = txtAIRSNumber.Text
            '    FacilitySummary.LoadInitialData()
            'End If
            If Not SSCPFCESelector Is Nothing Then
                temp = 7
                SSCPFCESelector.ValueFromFacilityLookUp = txtAIRSNumber.Text
                SSCPFCESelector.OpenFCETool()
            End If
            If Not ISMPFacility Is Nothing Then
                temp = 8
                ISMPFacility.ValueFromFacilityLookUp = txtAIRSNumber.Text
            End If
            If Not SSCP_Work Is Nothing Then
                temp = 9
                SSCP_Work.ValueFromFacilityLookUp = txtAIRSNumber.Text
                SSCP_Work.ValueFromFacilityLookUp2 = txtFacilityName.Text
            End If
            If Not TestFirmComments Is Nothing Then
                temp = 10
                TestFirmComments.ValueFromFacilityLookUp = txtAIRSNumber.Text
                TestFirmComments.ValueFromFacilityLookUp2 = txtFacilityName.Text
            End If
            If Not ISMPReportViewer Is Nothing Then
                temp = 11
                ISMPReportViewer.ValueFromFacilityLookUp = txtAIRSNumber.Text
            End If
        Catch ex As Exception
            ErrorReport(temp & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub dgvPossibleMatches_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPossibleMatches.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvPossibleMatches.HitTest(e.X, e.Y)

        Try
            If dgvPossibleMatches.RowCount > 0 And hti.RowIndex <> -1 Then
                txtAIRSNumber.Text = dgvPossibleMatches(1, hti.RowIndex).Value
                txtFacilityName.Text = dgvPossibleMatches(0, hti.RowIndex).Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Toolbar"

    Private Sub FacilityLookupToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles FacilityLookupToolBar.ButtonClick
        Select Case FacilityLookupToolBar.Buttons.IndexOf(e.Button)
            Case 0
                ClearPage()
        End Select
    End Sub

#End Region

#Region "Accept button"

    Private Sub tpFacilityName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpFacilityName.Enter
        Me.AcceptButton = btnFacilityNameSearch
    End Sub

    Private Sub tpAIRSNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpAIRSNumber.Enter
        Me.AcceptButton = btnAIRSNumberSearch
    End Sub

    Private Sub tpComplianceSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpComplianceSearch.Enter
        Me.AcceptButton = btnComplianceSearch
    End Sub

    Private Sub tpCity_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpCity.Enter
        Me.AcceptButton = btnCitySearch
    End Sub

    Private Sub tpZipCode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpZipCode.Enter
        Me.AcceptButton = btnZipCodeSearch
    End Sub

    Private Sub tpSIC_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpSIC.Enter
        Me.AcceptButton = btnSICCodeSearch
    End Sub

    Private Sub tpCounty_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpCounty.Enter
        Me.AcceptButton = btnCountySearch
    End Sub

    Private Sub tpSubpart_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpSubpart.Enter
        Me.AcceptButton = btnSubpartSearch
    End Sub

    Private Sub tabPages_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles tpFacilityName.Leave, tpAIRSNumber.Leave, tpComplianceSearch.Leave, tpCity.Leave, _
    tpZipCode.Leave, tpSIC.Leave, tpCounty.Leave, tpSubpart.Leave
        Me.AcceptButton = btnUseAIRSNumber
    End Sub

#End Region

End Class