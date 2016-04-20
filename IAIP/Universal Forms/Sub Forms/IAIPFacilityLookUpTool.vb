'Imports System.DateTime
Imports Oracle.ManagedDataAccess.Client


Public Class IAIPFacilityLookUpTool

    Private Enum SearchByType
        AirsNumber
        FacilityName
        HistoricalName
        City
        Inspector
        County
        ZipCode
        SicCode
        Subpart
    End Enum

#Region " Accessible properties "

    Public ReadOnly Property SelectedAirsNumber() As String
        Get
            Return txtAIRSNumber.Text
        End Get
    End Property

    Public ReadOnly Property SelectedFacilityName() As String
        Get
            Return txtFacilityName.Text
        End Get
    End Property

#End Region

#Region " Form events "

    Private Sub IAIPFacilityLookUpTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

        If Not Me.Modal Then Me.Close()

    End Sub

    Private Sub IAIPFacilityLookUpTool_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtFacilityNameSearch.Focus()
    End Sub

#End Region

#Region " Search Procedure "

    Private Sub SearchBy(ByVal SearchType As SearchByType)
        monitor.TrackFeature("FacilitySearch." & SearchType.ToString)
        ApplicationInsights.TrackEvent("FacilitySearch." & SearchType.ToString)

        Dim query As String = ""
        Dim parameter As OracleParameter = Nothing

        Select Case SearchType
            Case SearchByType.AirsNumber
                query = "Select " & _
                "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                "strFacilityCity, " & _
                "strFacilityStreet1 " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where strAirsNumber Like :SearchString"

                parameter = New OracleParameter("SearchString", "%" & txtAIRSNumberSearch.Text & "%")

            Case SearchByType.City
                query = "Select " & _
                "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                "strFacilityCity, " & _
                "strFacilityStreet1 " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where Upper(strFacilityCity) Like Upper(:SearchString)"

                parameter = New OracleParameter("SearchString", "%" & txtCityNameSearch.Text & "%")

            Case SearchByType.County
                query = "Select " & _
                 "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                 "strFacilityCity, " & _
                 "strFacilityStreet1, strCountyName " & _
                 "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.LookUpCountyInformation " & _
                 "where substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
                 "and upper(strCountyName) like Upper(:SearchString) "

                parameter = New OracleParameter("SearchString", "%" & txtCountyNameSearch.Text & "%")

            Case SearchByType.FacilityName
                query = "Select " & _
                "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                "strFacilityCity, " & _
                "strFacilityStreet1 " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where Upper(strFacilityName) Like Upper(:SearchString)"

                parameter = New OracleParameter("SearchString", "%" & txtFacilityNameSearch.Text & "%")

            Case SearchByType.HistoricalName
                query = "Select " & _
                "strFacilityName, " & _
                "substr(strAIRSNumber, 5) as shortAIRS, " & _
                "strFacilityCity, " & _
                "strFacilityStreet1 " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where Upper(strFacilityName) Like Upper(:SearchString)" & _
                "Union " & _
                "Select " & _
                "distinct(strFacilityName) as strFacilityName, " & _
                "substr(strAIRSNumber, 5) as shortAIRS, " & _
                "strFacilityCity, strFacilityStreet1 " & _
                "from AIRBRANCH.HB_APBFacilityInformation " & _
                "where Upper(strFacilityName) Like Upper(:SearchString)" & _
                "Union " & _
                "select " & _
                "Distinct(strFacilityname) as strFacilityname,  " & _
                "substr(strAIRSNumber, 5) as shortAIRS,  " & _
                "strFacilityCity, strFacilityStreet1  " & _
                "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster   " & _
                "where AIRBRANCH.SSPPApplicationData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber " & _
                "and upper(strFacilityname) like Upper(:SearchString) "

                parameter = New OracleParameter("SearchString", "%" & txtFacilityNameSearch.Text & "%")

            Case SearchByType.SicCode
                query = "Select " & _
                "strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS, " & _
                "strSICCode, " & _
                "strFacilityCity, strFacilityStreet1 " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBHeaderData " & _
                "where Upper(AIRBRANCH.APBHeaderData.strSICCode) Like Upper(:SearchString) " & _
                "and AIRBRANCH.APBFacilityInformation.strairsnumber = AIRBRANCH.APBHeaderData.strAIRSNumber"

                parameter = New OracleParameter("SearchString", txtSICCodeSearch.Text & "%")

            Case SearchByType.Subpart
                If rdbPart60.Checked Then
                    query = "select " & _
                    "strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity,  " & _
                    "strFacilityStreet1,  " & _
                    "(AIRBRANCH.LookUpsubPart60.strSubPart|| ' - '||AIRBRANCH.LookUpSubpart60.strDescription) as SubPartData " & _
                    "from  " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBSubpartData,  " & _
                    "AIRBRANCH.LookUPSubPart60  " & _
                    "where  " & _
                    "AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSubPartData.strAIRSNumber  " & _
                    "and AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPart60.strSubpart  " & _
                    "and substr(strSubpartKey, 13) = '9'  " & _
                    "and (AIRBRANCH.APBSubpartData.strSubpart) like :SearchString   "

                ElseIf rdbPart61.Checked Then
                    query = "select " & _
                    "strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity,  " & _
                    "strFacilityStreet1,  " & _
                    "(AIRBRANCH.LookUpsubPart61.strSubPart|| ' - '||AIRBRANCH.LookUpSubpart61.strDescription) as SubPartData " & _
                    "from  " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBSubpartData,  " & _
                    "AIRBRANCH.LookUPSubPart61  " & _
                    "where  " & _
                    "AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSubPartData.strAIRSNumber  " & _
                    "and AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPart61.strSubpart  " & _
                    "and substr(strSubpartKey, 13) = '8'  " & _
                    "and (AIRBRANCH.APBSubpartData.strSubpart) like :SearchString   "

                ElseIf rdbPart63.Checked Then
                    query = "select " & _
                    "strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity,  " & _
                    "strFacilityStreet1,  " & _
                    "(AIRBRANCH.LookUpsubPart63.strSubPart|| ' - '||AIRBRANCH.LookUpSubpart63.strDescription) as SubPartData " & _
                    "from  " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBSubpartData,  " & _
                    "AIRBRANCH.LookUPSubPart63  " & _
                    "where  " & _
                    "AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSubPartData.strAIRSNumber  " & _
                    "and AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPart63.strSubpart  " & _
                    "and substr(strSubpartKey, 13) = 'M'  " & _
                    "and (AIRBRANCH.APBSubpartData.strSubpart) like :SearchString   "

                ElseIf rdbGASIP.Checked Then
                    query = "select " & _
                    "strFacilityName, substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity,  " & _
                    "strFacilityStreet1,  " & _
                    "(AIRBRANCH.LookUpSubPartSIP.strSubPart|| ' - '||AIRBRANCH.LookUpSubpartSIP.strDescription) as SubPartData " & _
                    "from  " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBSubpartData,  " & _
                    "AIRBRANCH.LookUPSubPartSIP " & _
                    "where  " & _
                    "AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSubPartData.strAIRSNumber  " & _
                    "and AIRBRANCH.APBSubpartData.strSubPart = AIRBRANCH.LookUpSubPartSIP.strSubpart  " & _
                    "and substr(strSubpartKey, 13) = '0'  " & _
                    "and (AIRBRANCH.APBSubpartData.strSubpart) like :SearchString   "
                End If

                parameter = New OracleParameter("SearchString", "%" & txtSubpartSearch.Text & "%")

            Case SearchByType.ZipCode
                query = "Select " & _
                "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                "strFacilityCity, " & _
                "strFacilityStreet1, strFacilityZipCode " & _
                "from AIRBRANCH.APBFacilityInformation " & _
                "where Upper(strFacilityZipCode) Like Upper(:SearchString)"

                parameter = New OracleParameter("SearchString", "%" & txtZipCodeSearch.Text & "%")

            Case SearchByType.Inspector
                query = "Select  " & _
                " AIRBRANCH.APBFacilityInformation.strFacilityName, " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,   " & _
                " AIRBRANCH.APBFacilityInformation.strFacilityCity,   " & _
                " AIRBRANCH.APBFacilityInformation.strFacilityStreet1, " & _
                "(strLastName||', '||strFirstname) as Inspector    " & _
                "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.VW_SSCPInspection_List, " & _
                "AIRBRANCH.EPDUserProfiles   " & _
                "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = '0413'||AIRBRANCH.VW_SSCPInspection_List.AIRSNumber   " & _
                "and AIRBRANCH.VW_SSCPInspection_List.numSSCPEngineer = AIRBRANCH.EPDUserProfiles.numUserID   " & _
                "and Upper(strLastName||', '||strFirstName) like Upper(:SearchString)  "

                parameter = New OracleParameter("SearchString", "%" & txtComplianceEngineer.Text & "%")

            Case Else
                query = ""

        End Select

        If query <> "" Then
            dgvResults.DataSource = DB.GetDataTable(query, parameter)

            dgvResults.Columns("shortAIRS").HeaderText = "AIRS #"
            dgvResults.Columns("shortAIRS").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("shortAIRS").DisplayIndex = 0
            dgvResults.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvResults.Columns("strFacilityName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("strFacilityName").DisplayIndex = 1
            dgvResults.Columns("strFacilityCity").HeaderText = "City"
            dgvResults.Columns("strFacilityCity").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("strFacilityCity").DisplayIndex = 2
            dgvResults.Columns("strFacilityStreet1").HeaderText = "Facility Address"
            dgvResults.Columns("strFacilityStreet1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("strFacilityStreet1").DisplayIndex = 3

            Select Case SearchType
                Case SearchByType.County
                    dgvResults.Columns("strCountyName").HeaderText = "SIC Code"
                    dgvResults.Columns("strCountyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvResults.Columns("strCountyName").DisplayIndex = 4

                Case SearchByType.SicCode
                    dgvResults.Columns("strSICCode").HeaderText = "SIC Code"
                    dgvResults.Columns("strSICCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvResults.Columns("strSICCode").DisplayIndex = 4

                Case SearchByType.Subpart
                    dgvResults.Columns("SubPartData").HeaderText = "Subpart 'Code - Description' "
                    dgvResults.Columns("SubPartData").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvResults.Columns("SubPartData").DisplayIndex = 4

                Case SearchByType.ZipCode
                    dgvResults.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                    dgvResults.Columns("strFacilityZipCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvResults.Columns("strFacilityZipCode").DisplayIndex = 4

                Case SearchByType.Inspector
                    dgvResults.Columns("Inspector").HeaderText = "Compliance Inspector"
                    dgvResults.Columns("Inspector").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvResults.Columns("Inspector").DisplayIndex = 4

            End Select

        End If
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
        btnUseAIRSNumber.Enabled = False
        dgvResults.DataSource = Nothing
    End Sub

#End Region

#Region " Search-by buttons "

    Private Sub btnAIRSNumberSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAIRSNumberSearch.Click
        SearchBy(SearchByType.AirsNumber)
    End Sub
    Private Sub btnFacilityNameSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacilityNameSearch.Click
        If chbHistoricalNames.Checked Then
            SearchBy(SearchByType.HistoricalName)
        Else
            SearchBy(SearchByType.FacilityName)
        End If
    End Sub
    Private Sub btnCitySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCitySearch.Click
        SearchBy(SearchByType.City)
    End Sub
    Private Sub btnComplianceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComplianceSearch.Click
        SearchBy(SearchByType.Inspector)
    End Sub
    Private Sub btnCountySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountySearch.Click
        SearchBy(SearchByType.County)
    End Sub
    Private Sub btnZipCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZipCodeSearch.Click
        SearchBy(SearchByType.ZipCode)
    End Sub
    Private Sub btnSICCodeSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSICCodeSearch.Click
        SearchBy(SearchByType.SicCode)
    End Sub
    Private Sub btnSubpartSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubpartSearch.Click
        SearchBy(SearchByType.Subpart)
    End Sub

#End Region

#Region " Results DataGridView "

    Private Sub dgvPossibleMatches_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResults.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvResults.RowCount Then
            txtAIRSNumber.Text = dgvResults(1, e.RowIndex).FormattedValue
            txtFacilityName.Text = dgvResults(0, e.RowIndex).FormattedValue
            btnUseAIRSNumber.Enabled = True
        End If
    End Sub

#End Region

#Region " Toolbar "

    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        ClearPage()
    End Sub

#End Region

#Region " Accept button "

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