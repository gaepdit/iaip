Imports Iaip.Apb.ApbFacilityId
Imports Microsoft.Data.SqlClient
Imports Iaip.Apb

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

    Public ReadOnly Property SelectedAirsNumberAsText() As String
        Get
            Return txtAIRSNumber.Text
        End Get
    End Property

    Public ReadOnly Property SelectedAirsNumber As ApbFacilityId
        Get
            If txtAIRSNumber.Text Is Nothing OrElse Not IsValidAirsNumberFormat(txtAIRSNumber.Text) Then
                Return Nothing
            End If

            Return New ApbFacilityId(txtAIRSNumber.Text)
        End Get
    End Property

    Public ReadOnly Property SelectedFacilityName() As String
        Get
            Return txtFacilityName.Text
        End Get
    End Property

#End Region

#Region " Form events "

    Private Sub IAIPFacilityLookUpTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Me.Modal Then Me.Close()
    End Sub

    Private Sub IAIPFacilityLookUpTool_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtFacilityNameSearch.Focus()
    End Sub

#End Region

#Region " Search Procedure "

    Private Sub SearchBy(SearchType As SearchByType)
        Dim query As String = ""
        Dim parameter As SqlParameter = Nothing

        Select Case SearchType
            Case SearchByType.AirsNumber
                query = "Select " &
                "strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                "strFacilityCity " &
                "from APBFacilityInformation f " &
                "where strAirsNumber Like @SearchString"

                parameter = New SqlParameter("@SearchString", "%" & txtAIRSNumberSearch.Text & "%")

            Case SearchByType.City
                query = "Select " &
                "strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                "strFacilityCity " &
                "from APBFacilityInformation f " &
                "where strFacilityCity Like @SearchString"

                parameter = New SqlParameter("@SearchString", "%" & txtCityNameSearch.Text & "%")

            Case SearchByType.County
                query = "Select " &
                 "strFacilityName, " &
                 "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                 "strFacilityCity, strCountyName " &
                 "from APBFacilityInformation f, LookUpCountyInformation " &
                 "where substring(f.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode " &
                 "and strCountyName like @SearchString "

                parameter = New SqlParameter("@SearchString", "%" & txtCountyNameSearch.Text & "%")

            Case SearchByType.FacilityName
                query = "Select " &
                "strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                "strFacilityCity " &
                "from APBFacilityInformation f " &
                "where strFacilityName Like @SearchString"

                parameter = New SqlParameter("@SearchString", "%" & txtFacilityNameSearch.Text & "%")

            Case SearchByType.HistoricalName
                query = "Select
                    STRFACILITYNAME,
                    concat(substring(STRAIRSNUMBER, 5, 3), '-', right(STRAIRSNUMBER, 5)) as shortAIRS,
                    STRFACILITYCITY
                from APBFACILITYINFORMATION
                where STRFACILITYNAME Like @SearchString
                Union
                Select
                    distinct
                    STRFACILITYNAME,
                    concat(substring(STRAIRSNUMBER, 5, 3), '-', right(STRAIRSNUMBER, 5)),
                    STRFACILITYCITY
                from HB_APBFACILITYINFORMATION
                where STRFACILITYNAME Like @SearchString
                Union
                select
                    distinct
                    STRFACILITYNAME,
                    concat(substring(STRAIRSNUMBER, 5, 3), '-', right(STRAIRSNUMBER, 5)),
                    STRFACILITYCITY
                from SSPPAPPLICATIONDATA a
                    inner join SSPPAPPLICATIONMASTER m
                        on a.STRAPPLICATIONNUMBER = m.STRAPPLICATIONNUMBER
                where STRFACILITYNAME like @SearchString "

                parameter = New SqlParameter("@SearchString", "%" & txtFacilityNameSearch.Text & "%")

            Case SearchByType.SicCode
                query = "Select " &
                "strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                "strSICCode, " &
                "strFacilityCity " &
                "from APBFacilityInformation f, APBHeaderData " &
                "where APBHeaderData.strSICCode Like @SearchString " &
                "and f.strairsnumber = APBHeaderData.strAIRSNumber"

                parameter = New SqlParameter("@SearchString", txtSICCodeSearch.Text & "%")

            Case SearchByType.Subpart
                If rdbPart60.Checked Then
                    query = "select
                        STRFACILITYNAME,
                        concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS,
                        STRFACILITYCITY,
                        i.ICIS_PROGRAM_SUBPART_DESC as SubPartData
                    from APBFACILITYINFORMATION f
                        inner join APBSUBPARTDATA s
                            on s.STRAIRSNUMBER = f.STRAIRSNUMBER
                        inner join LK_ICIS_PROGRAM_SUBPART i
                            on i.LK_SUBPART_CODE = s.STRSUBPART
                               and i.LGCY_PROGRAM_CODE = right(s.STRSUBPARTKEY, 1)
                               and s.ACTIVE = 1
                               and i.ICIS_STATUS_FLAG = 'A'
                    where i.LGCY_PROGRAM_CODE = '9'
                          and s.STRSUBPART like @SearchString collate SQL_Latin1_General_CP1_CI_AS"

                ElseIf rdbPart61.Checked Then
                    query = "select
                        STRFACILITYNAME,
                        concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS,
                        STRFACILITYCITY,
                        i.ICIS_PROGRAM_SUBPART_DESC as SubPartData
                    from APBFACILITYINFORMATION f
                        inner join APBSUBPARTDATA s
                            on s.STRAIRSNUMBER = f.STRAIRSNUMBER
                        inner join LK_ICIS_PROGRAM_SUBPART i
                            on i.LK_SUBPART_CODE = s.STRSUBPART
                               and i.LGCY_PROGRAM_CODE = right(s.STRSUBPARTKEY, 1)
                               and s.ACTIVE = 1
                               and i.ICIS_STATUS_FLAG = 'A'
                    where i.LGCY_PROGRAM_CODE = '8'
                          and s.STRSUBPART like @SearchString collate SQL_Latin1_General_CP1_CI_AS"

                ElseIf rdbPart63.Checked Then
                    query = "select
                        STRFACILITYNAME,
                        concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS,
                        STRFACILITYCITY,
                        i.ICIS_PROGRAM_SUBPART_DESC as SubPartData
                    from APBFACILITYINFORMATION f
                        inner join APBSUBPARTDATA s
                            on s.STRAIRSNUMBER = f.STRAIRSNUMBER
                        inner join LK_ICIS_PROGRAM_SUBPART i
                            on i.LK_SUBPART_CODE = s.STRSUBPART
                               and i.LGCY_PROGRAM_CODE = right(s.STRSUBPARTKEY, 1)
                               and s.ACTIVE = 1
                               and i.ICIS_STATUS_FLAG = 'A'
                    where i.LGCY_PROGRAM_CODE = 'M'
                          and s.STRSUBPART like @SearchString collate SQL_Latin1_General_CP1_CI_AS"

                ElseIf rdbGASIP.Checked Then
                    query = "select f.STRFACILITYNAME,
                           concat(substring(f.STRAIRSNUMBER, 5, 3),
                                  '-', right(f.STRAIRSNUMBER, 5))    as shortAIRS,
                           f.STRFACILITYCITY,
                           (l.STRSUBPART + ' - ' + l.STRDESCRIPTION) as SubPartData
                    from dbo.APBFACILITYINFORMATION f
                         inner join dbo.APBSUBPARTDATA s
                                    on f.strAIRSNumber = s.strAIRSNumber
                         inner join dbo.LOOKUPSUBPARTSIP l
                                    on s.strSubPart = l.strSubpart
                    where right(strSubpartKey, 1) = '0'
                      and s.strSubpart like @SearchString collate SQL_Latin1_General_CP1_CI_AS"
                End If

                parameter = New SqlParameter("@SearchString", "%" & txtSubpartSearch.Text & "%")

            Case SearchByType.ZipCode
                query = "Select " &
                "strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                "strFacilityCity, " &
                "strFacilityZipCode " &
                "from APBFacilityInformation f " &
                "where strFacilityZipCode Like @SearchString"

                parameter = New SqlParameter("@SearchString", "%" & txtZipCodeSearch.Text & "%")

            Case SearchByType.Inspector
                query = "Select " &
                " f.strFacilityName, " &
                "concat(substring(f.STRAIRSNUMBER, 5, 3), '-', right(f.STRAIRSNUMBER, 5)) as shortAIRS, " &
                " f.strFacilityCity, " &
                "(strLastName + ', ' + strFirstname) as Inspector " &
                "from APBFacilityInformation f , VW_SSCPInspection_List, " &
                "EPDUserProfiles " &
                "where f.strAIRSNumber = '0413' + VW_SSCPInspection_List.AIRSNumber " &
                "and VW_SSCPInspection_List.numSSCPEngineer = EPDUserProfiles.numUserID " &
                "and strLastName + ', ' + strFirstName like @SearchString  "

                parameter = New SqlParameter("@SearchString", "%" & txtComplianceEngineer.Text & "%")

            Case Else
                query = ""

        End Select

        If query <> "" Then
            dgvResults.DataSource = DB.GetDataTable(query, parameter)

            dgvResults.Columns("shortAIRS").HeaderText = "AIRS #"
            dgvResults.Columns("shortAIRS").DisplayIndex = 0
            dgvResults.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvResults.Columns("strFacilityName").DisplayIndex = 1
            dgvResults.Columns("strFacilityCity").HeaderText = "City"
            dgvResults.Columns("strFacilityCity").DisplayIndex = 2

            Select Case SearchType
                Case SearchByType.County
                    dgvResults.Columns("strCountyName").HeaderText = "County"
                    dgvResults.Columns("strCountyName").DisplayIndex = 3

                Case SearchByType.SicCode
                    dgvResults.Columns("strSICCode").HeaderText = "SIC Code"
                    dgvResults.Columns("strSICCode").DisplayIndex = 3

                Case SearchByType.Subpart
                    dgvResults.Columns("SubPartData").HeaderText = "Rule Subpart"
                    dgvResults.Columns("SubPartData").DisplayIndex = 3

                Case SearchByType.ZipCode
                    dgvResults.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                    dgvResults.Columns("strFacilityZipCode").DisplayIndex = 3

                Case SearchByType.Inspector
                    dgvResults.Columns("Inspector").HeaderText = "Compliance Inspector"
                    dgvResults.Columns("Inspector").DisplayIndex = 3

            End Select

            dgvResults.SanelyResizeColumns

            lblSearchResults.Text = "Search results: " & dgvResults.Rows.Count
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

    Private Sub btnAIRSNumberSearch_Click(sender As Object, e As EventArgs) Handles btnAIRSNumberSearch.Click
        SearchBy(SearchByType.AirsNumber)
    End Sub
    Private Sub btnFacilityNameSearch_Click(sender As Object, e As EventArgs) Handles btnFacilityNameSearch.Click
        If chbHistoricalNames.Checked Then
            SearchBy(SearchByType.HistoricalName)
        Else
            SearchBy(SearchByType.FacilityName)
        End If
    End Sub
    Private Sub btnCitySearch_Click(sender As Object, e As EventArgs) Handles btnCitySearch.Click
        SearchBy(SearchByType.City)
    End Sub
    Private Sub btnComplianceSearch_Click(sender As Object, e As EventArgs) Handles btnComplianceSearch.Click
        SearchBy(SearchByType.Inspector)
    End Sub
    Private Sub btnCountySearch_Click(sender As Object, e As EventArgs) Handles btnCountySearch.Click
        SearchBy(SearchByType.County)
    End Sub
    Private Sub btnZipCodeSearch_Click(sender As Object, e As EventArgs) Handles btnZipCodeSearch.Click
        SearchBy(SearchByType.ZipCode)
    End Sub
    Private Sub btnSICCodeSearch_Click(sender As Object, e As EventArgs) Handles btnSICCodeSearch.Click
        SearchBy(SearchByType.SicCode)
    End Sub
    Private Sub btnSubpartSearch_Click(sender As Object, e As EventArgs) Handles btnSubpartSearch.Click
        SearchBy(SearchByType.Subpart)
    End Sub

#End Region

#Region " Results DataGridView "

    Private Sub dgvPossibleMatches_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResults.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvResults.RowCount Then
            txtAIRSNumber.Text = dgvResults(1, e.RowIndex).FormattedValue
            txtFacilityName.Text = dgvResults(0, e.RowIndex).FormattedValue
            btnUseAIRSNumber.Enabled = True
        End If
    End Sub

#End Region

#Region " Toolbar "

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ClearPage()
    End Sub

#End Region

#Region " Accept button "

    Private Sub tpFacilityName_Enter(sender As Object, e As EventArgs) Handles tpFacilityName.Enter
        Me.AcceptButton = btnFacilityNameSearch
    End Sub

    Private Sub tpAIRSNumber_Enter(sender As Object, e As EventArgs) Handles tpAIRSNumber.Enter
        Me.AcceptButton = btnAIRSNumberSearch
    End Sub

    Private Sub tpComplianceSearch_Enter(sender As Object, e As EventArgs) Handles tpComplianceSearch.Enter
        Me.AcceptButton = btnComplianceSearch
    End Sub

    Private Sub tpCity_Enter(sender As Object, e As EventArgs) Handles tpCity.Enter
        Me.AcceptButton = btnCitySearch
    End Sub

    Private Sub tpZipCode_Enter(sender As Object, e As EventArgs) Handles tpZipCode.Enter
        Me.AcceptButton = btnZipCodeSearch
    End Sub

    Private Sub tpSIC_Enter(sender As Object, e As EventArgs) Handles tpSIC.Enter
        Me.AcceptButton = btnSICCodeSearch
    End Sub

    Private Sub tpCounty_Enter(sender As Object, e As EventArgs) Handles tpCounty.Enter
        Me.AcceptButton = btnCountySearch
    End Sub

    Private Sub tpSubpart_Enter(sender As Object, e As EventArgs) Handles tpSubpart.Enter
        Me.AcceptButton = btnSubpartSearch
    End Sub

    Private Sub tabPages_Leave(sender As Object, e As EventArgs) _
    Handles tpFacilityName.Leave, tpAIRSNumber.Leave, tpComplianceSearch.Leave, tpCity.Leave,
    tpZipCode.Leave, tpSIC.Leave, tpCounty.Leave, tpSubpart.Leave
        Me.AcceptButton = btnUseAIRSNumber
    End Sub

#End Region

End Class