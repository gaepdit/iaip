Imports System.DateTime
Imports System.Data.OracleClient


Public Class IAIPFacilityLookUpTool
    Dim dsSearch As New DataSet
    Dim daSearch As OracleDataAdapter
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader

    Private Sub IAIPFacilityLookUpTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            Me.Size = New Drawing.Size(500, 500)


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Page Load Functions"

#End Region
#Region "Subs and Functions"
    Sub SearchBy(ByVal SearchItem As String)

        Try
            Select Case SearchItem
                Case "Airs Number"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where strAirsNumber Like '%" & txtAIRSNumberSearch.Text & "%'"
                Case "City"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityCity) Like Upper('%" & Replace(txtCityNameSearch.Text, "'", "''") & "%')"
                Case "County"
                    SQL = "Select " & _
                     "strFacilityName, substr(strAIRSNumber, 5) as ShortAIRS, " & _
                     "strFacilityCity, " & _
                     "strFacilityStreet1, strCountyName " & _
                     "from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".LookUpCountyInformation " & _
                     "where substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5, 3) = " & connNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                     "and upper(strCountyName) like Upper('%" & txtCountyNameSearch.Text & "%') "
                Case "Facility Name"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')"
                Case "Historical Name"
                    SQL = "Select " & _
                    "strFacilityName, " & _
                    "substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1 " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')" & _
                    "Union " & _
                    "Select " & _
                    "distinct(strFacilityName) as strFacilityName, " & _
                    "substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, strFacilityStreet1 " & _
                    "from " & connNameSpace & ".HB_APBFacilityInformation " & _
                    "where Upper(strFacilityName) Like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%')" & _
                    "Union " & _
                    "select " & _
                    "Distinct(strFacilityname) as strFacilityname,  " & _
                    "substr(strAIRSNumber, 5) as shortAIRS,  " & _
                    "strFacilityCity, strFacilityStreet1  " & _
                    "from " & connNameSpace & ".SSPPApplicationData, " & connNameSpace & ".SSPPApplicationMaster   " & _
                    "where " & connNameSpace & ".SSPPApplicationData.strApplicationNumber = " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
                    "and upper(strFacilityname) like Upper('%" & Replace(txtFacilityNameSearch.Text, "'", "''") & "%') "
                Case "SIC Code"
                    SQL = "Select " & _
                    "strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS, " & _
                    "strSICCode, " & _
                    "strFacilityCity, strFacilityStreet1 " & _
                    "from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".APBHeaderData " & _
                    "where Upper(" & connNameSpace & ".APBHeaderData.strSICCode) Like Upper('%" & Replace(txtSICCodeSearch.Text, "'", "''") & "%') " & _
                    "and " & connNameSpace & ".APBFacilityInformation.strairsnumber = " & connNameSpace & ".APBHeaderData.strAIRSNumber"
                Case "Subpart"
                    If rdbPart60.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & connNameSpace & ".LookUpsubPart60.strSubPart|| ' - '||" & connNameSpace & ".LookUpSubpart60.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".APBSubpartData,  " & _
                        "" & connNameSpace & ".LookUPSubPart60  " & _
                        "where  " & _
                        "" & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & connNameSpace & ".APBSubpartData.strSubPart = " & connNameSpace & ".LookUpSubPart60.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '9'  " & _
                        "and (" & connNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbPart61.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & connNameSpace & ".LookUpsubPart61.strSubPart|| ' - '||" & connNameSpace & ".LookUpSubpart61.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".APBSubpartData,  " & _
                        "" & connNameSpace & ".LookUPSubPart61  " & _
                        "where  " & _
                        "" & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & connNameSpace & ".APBSubpartData.strSubPart = " & connNameSpace & ".LookUpSubPart61.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '8'  " & _
                        "and (" & connNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbPart63.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & connNameSpace & ".LookUpsubPart63.strSubPart|| ' - '||" & connNameSpace & ".LookUpSubpart63.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".APBSubpartData,  " & _
                        "" & connNameSpace & ".LookUPSubPart63  " & _
                        "where  " & _
                        "" & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & connNameSpace & ".APBSubpartData.strSubPart = " & connNameSpace & ".LookUpSubPart63.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = 'M'  " & _
                        "and (" & connNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                    If rdbGASIP.Checked = True Then
                        SQL = "select " & _
                        "strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,  " & _
                        "strFacilityCity,  " & _
                        "strFacilityStreet1,  " & _
                        "(" & connNameSpace & ".LookUpSubPartSIP.strSubPart|| ' - '||" & connNameSpace & ".LookUpSubpartSIP.strDescription) as SubPartData " & _
                        "from  " & _
                        "" & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".APBSubpartData,  " & _
                        "" & connNameSpace & ".LookUPSubPartSIP " & _
                        "where  " & _
                        "" & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".APBSubPartData.strAIRSNumber  " & _
                        "and " & connNameSpace & ".APBSubpartData.strSubPart = " & connNameSpace & ".LookUpSubPartSIP.strSubpart  " & _
                        "and substr(strSubpartKey, 13) = '0'  " & _
                        "and (" & connNameSpace & ".APBSubpartData.strSubpart) like '%" & Replace(txtSubpartSearch.Text, "'", "''") & "%'   "
                    End If
                Case "Zip Code"
                    SQL = "Select " & _
                    "strFacilityName, substr(strAIRSNumber, 5) as shortAIRS, " & _
                    "strFacilityCity, " & _
                    "strFacilityStreet1, strFacilityZipCode " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where Upper(strFacilityZipCode) Like Upper('%" & Replace(txtZipCodeSearch.Text, "'", "''") & "%')"
                Case "Address"

                Case "Compliance"
                    ' SQL = "Select  " & _
                    '"strFacilityName, substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,   " & _
                    '"strFacilityCity,   " & _
                    '"strFacilityStreet1, (strLastName||', '||strFirstname) as Engineer    " & _
                    '"from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".SSCPFacilityAssignment, " & connNameSpace & ".EPDUserProfiles   " & _
                    '"where " & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".SSCPFacilityAssignment.strAIRSNumber   " & _
                    '"and " & connNameSpace & ".SSCPFacilityAssignment.strSSCPEngineer = " & connNameSpace & ".EPDUserProfiles.numUserID   " & _
                    '"and Upper(strLastName||', '||strFirstName) like Upper('%" & Replace(txtComplianceEngineer.Text, "'", "''") & "%')  "

                    SQL = "Select  " & _
                    " " & connNameSpace & ".APBFacilityInformation.strFacilityName, " & _
                    "substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as shortAIRS,   " & _
                    " " & connNameSpace & ".APBFacilityInformation.strFacilityCity,   " & _
                    " " & connNameSpace & ".APBFacilityInformation.strFacilityStreet1, " & _
                    "(strLastName||', '||strFirstname) as Engineer    " & _
                    "from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".VW_SSCPInspection_List, " & _
                    "" & connNameSpace & ".EPDUserProfiles   " & _
                    "where " & connNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413'||" & connNameSpace & ".VW_SSCPInspection_List.AIRSNumber   " & _
                    "and " & connNameSpace & ".VW_SSCPInspection_List.numSSCPEngineer = " & connNameSpace & ".EPDUserProfiles.numUserID   " & _
                    "and Upper(strLastName||', '||strFirstName) like Upper('%" & Replace(txtComplianceEngineer.Text, "'", "''") & "%')  "

                Case Else

            End Select
            If SQL <> "" Then
                dsSearch = New DataSet
                daSearch = New OracleDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
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
        Try

            txtAIRSNumber.Clear()
            txtAIRSNumberSearch.Clear()
            txtCityNameSearch.Clear()
            txtComplianceEngineer.Clear()
            txtCountyNameSearch.Clear()
            txtFacilityName.Clear()
            txtFacilityNameSearch.Clear()
            txtSICCodeSearch.Clear()
            txtZipCodeSearch.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
#Region "Declarations"
    Private Sub btnAIRSNumberSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAIRSNumberSearch.Click
        Try

            SearchBy("Airs Number")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnFacilityNameSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacilityNameSearch.Click
        Try

            If chbHistoricalNames.Checked = True Then
                SearchBy("Historical Name")
            Else
                SearchBy("Facility Name")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnCitySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCitySearch.Click
        Try

            SearchBy("City")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnComplianceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComplianceSearch.Click
        Try

            SearchBy("Compliance")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnCountySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountySearch.Click
        Try

            SearchBy("County")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnZipCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZipCodeSearch.Click
        Try

            SearchBy("Zip Code")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSICCodeSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSICCodeSearch.Click
        Try

            SearchBy("SIC Code")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSelectAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAIRSNumber.Click
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
            If Not FacilitySummary2 Is Nothing Then
                temp = 4
                FacilitySummary2.ValueFromFacilityLookUp = txtAIRSNumber.Text
                FacilitySummary2.LoadInitialData()
            End If
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
    Private Sub btnSubpartSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubpartSearch.Click
        Try


            SearchBy("Subpart")

        Catch ex As Exception
            ErrorReport(SQL.ToString & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Main Menu Items"
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            FacilityLookUpTool = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub mmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        conn.Dispose()
        End

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^X")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("^C")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("^V")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
    Private Sub TBWork_EnTry_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBWork_Entry.ButtonClick
        Try

            Select Case TBWork_Entry.Buttons.IndexOf(e.Button)
                Case 0
                    ClearPage()
                Case 1
                    FacilityLookUpTool = Nothing
                    Me.Close()
                Case 2
                    SendKeys.Send("^X")
                Case 3
                    SendKeys.Send("^C")
                Case 4
                    SendKeys.Send("^V")
            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtAIRSNumberSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAIRSNumberSearch.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("Airs Number")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtCityNameSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCityNameSearch.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("City")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtComplianceEngineer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComplianceEngineer.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("Compliance")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtCountyNameSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCountyNameSearch.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("County")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtFacilityNameSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFacilityNameSearch.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                If chbHistoricalNames.Checked = True Then
                    SearchBy("Historical Name")
                Else
                    SearchBy("Facility Name")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtSICCodeSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSICCodeSearch.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("SIC Code")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtZipCodeSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZipCodeSearch.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                SearchBy("Zip Code")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region


   
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try
            ClearPage()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
End Class