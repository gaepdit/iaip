Imports Oracle.DataAccess.Client
Imports Iaip.Apb

Public Class IAIPEditHeaderData

#Region " Form properties and variables "

    Private _AirsNumber As String
    Public Property AirsNumber() As String
        Get
            Return _AirsNumber
        End Get
        Set(ByVal value As String)
            _AirsNumber = value
        End Set
    End Property

    Private _FacilityName As String
    Public Property FacilityName() As String
        Get
            Return _FacilityName
        End Get
        Set(ByVal value As String)
            _FacilityName = value
        End Set
    End Property

    Private FacilityHistory As DataTable

    Private CurrentFacility As FacilityHeaderData

    Private _somethingSaved As Boolean = False
    Public Property SomethingWasSaved() As Boolean
        Get
            Return _somethingSaved
        End Get
        Set(ByVal value As Boolean)
            _somethingSaved = value
        End Set
    End Property

#End Region

#Region " Form Load "

    Private Sub IAIPEditHeaderData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)

        AirsNumberDisplay.Text = Facility.FormatAirsNumber(Me.AirsNumber)
        FacilityNameDisplay.Text = Me.FacilityName

        DisableEditing()
        PreloadComboBoxes()
        LoadFacilityData()

        CheckEditingPermissions()
    End Sub

    Private Sub PreloadComboBoxes()
        Classification.BindToEnum(Of Facility.Classification)()
        OperationalStatus.BindToEnum(Of Facility.OperationalStatus)()
        EightHourOzone.BindToEnum(Of Facility.EightHourNonattainmentStatus)()
        OneHourOzone.BindToEnum(Of Facility.OneHourNonattainmentStatus)()
        PmFine.BindToEnum(Of Facility.PMFineNonattainmentStatus)()
    End Sub

    Private Sub LoadFacilityData()
        FacilityHistory = DAL.FacilityHeaderData.GetFacilityHeaderDataHistoryAsDataTable(AirsNumber)

        Dim currentData As DataRow = DAL.FacilityHeaderData.GetFacilityHeaderDataAsDataRow(AirsNumber)
        currentData(0) = FacilityHistory.Compute("Max(STRKEY)", String.Empty) + 1
        FacilityHistory.ImportRow(currentData)

        BindFacilityHistoryDisplay(FacilityHistory)

        CurrentFacility = New FacilityHeaderData(AirsNumber)
        DAL.FillFacilityHeaderDataFromDataRow(currentData, CurrentFacility)
        DisplayFacilityData(CurrentFacility)

        FacilityHistoryDisplay.Rows(0).Selected = True
    End Sub

    Private Sub BindFacilityHistoryDisplay(ByVal dt As DataTable)
        FacilityHistoryDisplay.DataSource = dt

        FacilityHistoryDisplay.Columns("STRKEY").Visible = False

        FacilityHistoryDisplay.Columns("USERNAME").HeaderText = "Modified By"
        FacilityHistoryDisplay.Columns("MODIFINGDATE").HeaderText = "Date Modified"
        FacilityHistoryDisplay.Columns("STRMODIFINGLOCATION").HeaderText = "Modified From"
        FacilityHistoryDisplay.Columns("STROPERATIONALSTATUS").HeaderText = "Operating Status"
        FacilityHistoryDisplay.Columns("STRCLASS").HeaderText = "Classification"
        FacilityHistoryDisplay.Columns("STRCOMMENTS").HeaderText = "Comments"
        FacilityHistoryDisplay.Columns("DATSTARTUPDATE").HeaderText = "Start Up Date"
        FacilityHistoryDisplay.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
        FacilityHistoryDisplay.Columns("STRPLANTDESCRIPTION").HeaderText = "Plant Description"
        FacilityHistoryDisplay.Columns("STRSICCODE").HeaderText = "SIC Code"
        FacilityHistoryDisplay.Columns("STRNAICSCODE").HeaderText = "NAICS Code"
        FacilityHistoryDisplay.Columns("STRRMPID").HeaderText = "RMP ID"

        FacilityHistoryDisplay.Columns("STRAIRPROGRAMCODES").Visible = False
        FacilityHistoryDisplay.Columns("STRSTATEPROGRAMCODES").Visible = False
        FacilityHistoryDisplay.Columns("STRATTAINMENTSTATUS").Visible = False

        FacilityHistoryDisplay.Sort(FacilityHistoryDisplay.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        FacilityHistoryDisplay.SanelyResizeColumns()

        AddHandler FacilityHistoryDisplay.CurrentCellChanged, AddressOf HeaderDataHistory_CurrentCellChanged
    End Sub

    Private Function EditingIsAllowed() As Boolean
        If AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CheckEditingPermissions()
        If Not EditingIsAllowed() Then
            DisableAndHide(New Control() {EditableButton, SaveChangesButton, CancelEditButton})
        Else
            EditableButton.Focus()
        End If
    End Sub

#End Region

#Region " Editing Buttons "

    Private Sub EnableEditing()
        Comments.Clear()

        FacilityHistoryDisplay.Enabled = False

        Dim EditableControls As Control() = { _
            Classification, _
            OperationalStatus, _
            SicCode, _
            StartUpDate, _
            ShutdownDate, _
            NaicsCode, _
            AirProgramCodes, _
            NonattainmentStatuses, _
            FacilityDescription, _
            RmpId, _
            Comments, _
            SaveChangesButton, _
            CancelEditButton _
        }
        EnableControls(EditableControls)
    End Sub

    Private Sub DisableEditing()
        FacilityHistoryDisplay.Enabled = True

        Dim EditableControls As Control() = { _
            Classification, _
            OperationalStatus, _
            SicCode, _
            StartUpDate, _
            ShutdownDate, _
            NaicsCode, _
            AirProgramCodes, _
            NonattainmentStatuses, _
            FacilityDescription, _
            RmpId, _
            Comments, _
            SaveChangesButton, _
            CancelEditButton _
        }
        DisableControls(EditableControls)
    End Sub

    Private Sub EditableButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditableButton.CheckedChanged
        If EditableButton.Checked Then
            DisplayFacilityData(CurrentFacility)
            EnableEditing()
            Comments.Focus()
        Else
            DisableEditing()
        End If
    End Sub

    Private Sub CancelEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelEditButton.Click
        If EditableButton.Checked Then
            EditableButton.Checked = False
        End If
    End Sub

    Private Sub SaveChangesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveChangesButton.Click
        SaveData()
    End Sub

#End Region

#Region " Item selection and display "

    Private Sub HeaderDataHistory_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If FacilityHistoryDisplay.CurrentRow IsNot Nothing Then
            Dim currentDataRowView As DataRowView = CType(FacilityHistoryDisplay.CurrentRow.DataBoundItem, DataRowView)
            Dim row As DataRow = currentDataRowView.Row
            DisplayFacilityData(row)
        End If
    End Sub

    Private Sub DisplayFacilityData(ByVal row As DataRow)
        Dim displayedFacility As FacilityHeaderData = New FacilityHeaderData(AirsNumber)
        DAL.FacilityHeaderData.FillFacilityHeaderDataFromDataRow(row, displayedFacility)

        DisplayFacilityData(displayedFacility)
    End Sub

    Private Sub DisplayFacilityData(ByVal displayedFacility As FacilityHeaderData)
        With displayedFacility

            Classification.SelectedValue = .Classification
            OperationalStatus.SelectedValue = .OperationalStatus
            SicCode.Text = .SicCode
            If .StartupDate Is Nothing Then
                StartUpDate.Checked = False
            Else
                StartUpDate.Checked = True
                StartUpDate.Value = .StartupDate
            End If
            If .ShutdownDate Is Nothing Then
                ShutdownDate.Checked = False
            Else
                ShutdownDate.Checked = True
                ShutdownDate.Value = .ShutdownDate
            End If
            NaicsCode.Text = .Naics

            ApcAcid.Checked = Convert.ToBoolean(.AirPrograms And Facility.AirPrograms.AcidPrecipitation)
            ApcCfc.Checked = (.AirPrograms And Facility.AirPrograms.CfcTracking)
            ApcFederalSip.Checked = (.AirPrograms And Facility.AirPrograms.FederalSIP)
            ApcFesop.Checked = (.AirPrograms And Facility.AirPrograms.FESOP)
            ApcMact.Checked = (.AirPrograms And Facility.AirPrograms.MACT)
            ApcNativeAmerican.Checked = (.AirPrograms And Facility.AirPrograms.NativeAmerican)
            ApcNeshap.Checked = (.AirPrograms And Facility.AirPrograms.NESHAP)
            ApcNonfederalSip.Checked = (.AirPrograms And Facility.AirPrograms.NonFederalSIP)
            ApcNsps.Checked = (.AirPrograms And Facility.AirPrograms.NSPS)
            ApcNsr.Checked = (.AirPrograms And Facility.AirPrograms.NSR)
            ApcPsd.Checked = (.AirPrograms And Facility.AirPrograms.PSD)
            ApcRmp.Checked = (.AirPrograms And Facility.AirPrograms.RMP)
            ApcSip.Checked = (.AirPrograms And Facility.AirPrograms.SIP)
            ApcTitleV.Checked = (.AirPrograms And Facility.AirPrograms.TitleV)

            NsrMajor.Checked = .AirProgramClassifications And Facility.AirProgramClassifications.NsrMajor
            HapMajor.Checked = .AirProgramClassifications And Facility.AirProgramClassifications.HapMajor

            OneHourOzone.SelectedValue = .OneHourNonAttainmentState
            EightHourOzone.SelectedValue = .EightHourNonAttainmentState
            PmFine.SelectedValue = .PMFineNonAttainmentState

            FacilityDescription.Text = .FacilityDescription
            RmpId.Text = .RmpId
            Comments.Text = .HeaderUpdateComment

            If EditableButton.Checked Then
                ModifiedDescDisplay.Text = "Current data as modified by " & .WhoModified & " on " & .DateDataModified & " from " & .WhereModified
            Else
                ModifiedDescDisplay.Text = "Modification by " & .WhoModified & " on " & .DateDataModified & " from " & .WhereModified
            End If

        End With
    End Sub

#End Region

#Region " Save Data "

    Private Function BoxUpFacilityFromForm() As FacilityHeaderData
        Dim facilityHeaderData As New FacilityHeaderData(AirsNumber)

        With facilityHeaderData
            .Classification = Classification.SelectedValue
            .OperationalStatus = OperationalStatus.SelectedValue
            .SicCode = SicCode.Text
            If StartUpDate.Checked Then
                .StartupDate = StartUpDate.Value
            Else
                .StartupDate = Nothing
            End If
            If ShutdownDate.Checked Then
                .ShutdownDate = ShutdownDate.Value
            Else
                .ShutdownDate = Nothing
            End If
            .Naics = NaicsCode.Text

            .AirPrograms = Facility.AirPrograms.None
            If ApcAcid.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.AcidPrecipitation
            If ApcCfc.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.CfcTracking
            If ApcFederalSip.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.FederalSIP
            If ApcFesop.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.FESOP
            If ApcMact.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.MACT
            If ApcNativeAmerican.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.NativeAmerican
            If ApcNeshap.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.NESHAP
            If ApcNonfederalSip.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.NonFederalSIP
            If ApcNsps.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.NSPS
            If ApcNsr.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.NSR
            If ApcPsd.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.PSD
            If ApcRmp.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.RMP
            If ApcSip.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.SIP
            If ApcTitleV.Checked Then .AirPrograms = .AirPrograms Or Facility.AirPrograms.TitleV

            .AirProgramClassifications = Facility.AirProgramClassifications.None
            If NsrMajor.Checked Then .AirProgramClassifications = .AirProgramClassifications Or Facility.AirProgramClassifications.NsrMajor
            If HapMajor.Checked Then .AirProgramClassifications = .AirProgramClassifications Or Facility.AirProgramClassifications.HapMajor

            .OneHourNonAttainmentState = OneHourOzone.SelectedValue
            .EightHourNonAttainmentState = EightHourOzone.SelectedValue
            .PMFineNonAttainmentState = PmFine.SelectedValue

            .FacilityDescription = FacilityDescription.Text
            .RmpId = RmpId.Text
            .HeaderUpdateComment = Comments.Text
        End With

        Return facilityHeaderData
    End Function

    Private Sub SaveData()
        Dim editedFacility As FacilityHeaderData = BoxUpFacilityFromForm()

        ' Compare edited data to current data
        If ComparableFacility(editedFacility).Equals(ComparableFacility(CurrentFacility)) Then
            MessageBox.Show("No data has been changed. Nothing saved.", "Nothing Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Validate fields and require comment ... See Save()
        'If invalid Then
        '    MessageBox.Show("Some data is not valid. Nothing saved.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        ' Save edited data
        'Dim result As Boolean = DAL.FacilityHeaderData.SaveFacilityHeaderData(editedFacility)

        ' If successful, report back to Facility Summary and close
        'If result Then
        '    Me.SomethingWasSaved = True
        '    Me.Close()
        'Else
        '    MessageBox.Show("There was an error saving the new data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If

    End Sub

    Private Function ComparableFacility(ByVal hd As FacilityHeaderData) As FacilityHeaderData
        Dim hdwc As FacilityHeaderData = hd
        With hdwc
            .HeaderUpdateComment = Nothing
            .DateDataModified = Nothing
            .WhoModified = Nothing
            .WhereModified = Nothing
        End With
        Return hdwc
    End Function

#End Region



#Region "old stuff"

    'Sub LoadFacilityHeaderData()
    '    Dim temp As String = ""
    '    Dim ModifingPerson As String
    '    Dim ModifingDate As String
    '    Dim ModifingLocation As String

    '    Try
    '        SQL = "Select * " & _
    '        "From " & DBNameSpace & ".VW_APBFacilityHeader " & _
    '        "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

    '        SQL2 = "Select * " & _
    '        "from " & DBNameSpace & ".VW_HB_APBHeaderData " & _
    '        "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' " & _
    '        "Order by strKey DESC "

    '        dsHeaderData = New DataSet
    '        daHeaderData = New OracleDataAdapter(SQL, CurrentConnection)
    '        daHeaderData2 = New OracleDataAdapter(SQL2, CurrentConnection)

    '        If CurrentConnection.State = ConnectionState.Closed Then
    '            CurrentConnection.Open()
    '        End If

    '        daHeaderData.Fill(dsHeaderData, "Current")
    '        daHeaderData2.Fill(dsHeaderData, "Historical")

    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(1).ToString
    '        Select Case temp
    '            Case "O"
    '                OperationalStatus.Text = "O - Operational"
    '            Case "P"
    '                OperationalStatus.Text = "P - Planned"
    '            Case "C"
    '                OperationalStatus.Text = "C - Under Construction"
    '            Case "T"
    '                OperationalStatus.Text = "T - Temporarily Closed"
    '            Case "X"
    '                OperationalStatus.Text = "X - Closed/Dismantled"
    '            Case "I"
    '                OperationalStatus.Text = "I - Seasonal Operation"
    '            Case Else
    '                OperationalStatus.Text = "Unknown - Please Fix"
    '        End Select
    '        Classification.Text = dsHeaderData.Tables("Current").Rows(0).Item(2).ToString
    '        SicCode.Text = dsHeaderData.Tables("Current").Rows(0).Item(4).ToString
    '        Comments.Text = dsHeaderData.Tables("Current").Rows(0).Item(13).ToString
    '        ModifingPerson = dsHeaderData.Tables("Current").Rows(0).Item(7).ToString
    '        ModifingDate = dsHeaderData.Tables("Current").Rows(0).Item(12).ToString
    '        ModifingLocation = dsHeaderData.Tables("Current").Rows(0).Item(16).ToString
    '        NaicsCode.Text = dsHeaderData.Tables("Current").Rows(0).Item(17).ToString

    '        txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(9).ToString
    '        If temp = "" Then
    '            StartUpDate.Checked = False
    '            StartUpDate.Text = OracleDate
    '        Else
    '            StartUpDate.Checked = True
    '            StartUpDate.Text = temp
    '        End If

    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(10).ToString
    '        If temp = "" Then
    '            ShutdownDate.Checked = False
    '            ShutdownDate.Text = OracleDate
    '        Else
    '            ShutdownDate.Checked = True
    '            ShutdownDate.Text = temp
    '        End If

    '        FacilityDescription.Text = dsHeaderData.Tables("Current").Rows(0).Item(14).ToString
    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(15).ToString

    '        If temp = "" Then
    '            NsrMajor.Checked = False
    '            HapMajor.Checked = False
    '        Else
    '            If Mid(temp, 1, 1) = "1" Then
    '                NsrMajor.Checked = True
    '            Else
    '                NsrMajor.Checked = False
    '            End If
    '            If Mid(temp, 2, 1) = "1" Then
    '                HapMajor.Checked = True
    '            Else
    '                HapMajor.Checked = False
    '            End If
    '        End If

    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(6).ToString
    '        If temp = "" Then
    '            OneHourOzone.Text = "No"
    '            EightHourOzone.Text = "No"
    '            PmFine.Text = "No"
    '        Else
    '            Select Case (Mid(temp, 2, 1))
    '                Case 1
    '                    OneHourOzone.Text = "Yes"
    '                Case 2
    '                    OneHourOzone.Text = "Contribute"
    '                Case Else
    '                    OneHourOzone.Text = "No"
    '            End Select
    '            Select Case (Mid(temp, 3, 1))
    '                Case 1
    '                    EightHourOzone.Text = "Atlanta"
    '                Case 2
    '                    EightHourOzone.Text = "Macon"
    '                Case Else
    '                    EightHourOzone.Text = "No"
    '            End Select
    '            Select Case (Mid(temp, 4, 1))
    '                Case 1
    '                    PmFine.Text = "Atlanta"
    '                Case 2
    '                    PmFine.Text = "Chattanooga"
    '                Case 3
    '                    PmFine.Text = "Floyd"
    '                Case 4
    '                    PmFine.Text = "Macon"
    '                Case Else
    '                    PmFine.Text = "No"
    '            End Select
    '        End If
    '        temp = dsHeaderData.Tables("Current").Rows(0).Item(3).ToString
    '        AddAirProgramCodes(temp)

    '        HeaderDataHistory.DataSource = dsHeaderData
    '        HeaderDataHistory.DataMember = "Historical"
    '        HeaderDataHistory.RowHeadersVisible = False
    '        HeaderDataHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    '        HeaderDataHistory.AllowUserToResizeColumns = True
    '        HeaderDataHistory.AllowUserToAddRows = False
    '        HeaderDataHistory.AllowUserToDeleteRows = False
    '        HeaderDataHistory.AllowUserToOrderColumns = True
    '        HeaderDataHistory.AllowUserToResizeRows = True
    '        HeaderDataHistory.Columns("strKey").HeaderText = "Key"
    '        HeaderDataHistory.Columns("strKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
    '        HeaderDataHistory.Columns("strKey").DisplayIndex = 0
    '        HeaderDataHistory.Columns("strKey").Visible = False
    '        HeaderDataHistory.Columns("UserName").HeaderText = "Modifing Person"
    '        HeaderDataHistory.Columns("UserName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("UserName").DisplayIndex = 1
    '        HeaderDataHistory.Columns("ModifingDate").HeaderText = "Date Modified"
    '        HeaderDataHistory.Columns("ModifingDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("ModifingDate").DisplayIndex = 2
    '        HeaderDataHistory.Columns("strModifingLocation").HeaderText = "Modifing Location"
    '        HeaderDataHistory.Columns("strModifingLocation").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strModifingLocation").DisplayIndex = 3
    '        HeaderDataHistory.Columns("strOperationalStatus").HeaderText = "Operating Status"
    '        HeaderDataHistory.Columns("strOperationalStatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strOperationalStatus").DisplayIndex = 4
    '        HeaderDataHistory.Columns("strClass").HeaderText = "Classification"
    '        HeaderDataHistory.Columns("strClass").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strClass").DisplayIndex = 5
    '        HeaderDataHistory.Columns("strAIRProgramCodes").HeaderText = "Air Program Codes"
    '        HeaderDataHistory.Columns("strAIRProgramCodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strAIRProgramCodes").DisplayIndex = 6
    '        HeaderDataHistory.Columns("strAIRProgramCodes").Visible = False
    '        HeaderDataHistory.Columns("strSICCode").HeaderText = "SIC Code"
    '        HeaderDataHistory.Columns("strSICCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strSICCode").DisplayIndex = 7
    '        HeaderDataHistory.Columns("strComments").HeaderText = "Comments"
    '        HeaderDataHistory.Columns("strComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strComments").DisplayIndex = 8
    '        HeaderDataHistory.Columns("datStartUpDate").HeaderText = "Start Up Date"
    '        HeaderDataHistory.Columns("datStartUpDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("datStartUpDate").DisplayIndex = 9
    '        HeaderDataHistory.Columns("datShutDownDate").HeaderText = "Shut Down Date"
    '        HeaderDataHistory.Columns("datShutDownDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("datShutDownDate").DisplayIndex = 10
    '        HeaderDataHistory.Columns("strPlantDescription").HeaderText = "Plant Description"
    '        HeaderDataHistory.Columns("strPlantDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strPlantDescription").DisplayIndex = 11
    '        HeaderDataHistory.Columns("strAIRSNumber").HeaderText = "AIRS Number"
    '        HeaderDataHistory.Columns("strAIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strAIRSNumber").DisplayIndex = 12
    '        HeaderDataHistory.Columns("strAIRSNumber").Visible = False
    '        HeaderDataHistory.Columns("strAttainmentStatus").Visible = False
    '        HeaderDataHistory.Columns("strStateProgramCodes").Visible = False
    '        HeaderDataHistory.Columns("strNAICSCode").HeaderText = "NAICS Code"
    '        HeaderDataHistory.Columns("strNAICSCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '        HeaderDataHistory.Columns("strNAICSCode").DisplayIndex = 13

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try

    'End Sub

    'Sub AddAirProgramCodes(ByRef AirProgramCode As String)
    '    Try

    '        ApcSip.Checked = False
    '        ApcFederalSip.Checked = False
    '        ApcNonfederalSip.Checked = False
    '        ApcCfc.Checked = False
    '        ApcPsd.Checked = False
    '        ApcNsr.Checked = False
    '        ApcNeshap.Checked = False
    '        ApcNsps.Checked = False
    '        ApcFesop.Checked = False
    '        ApcAcid.Checked = False
    '        ApcNativeAmerican.Checked = False
    '        ApcMact.Checked = False
    '        ApcTitleV.Checked = False
    '        ApcRmp.Checked = False

    '        If Mid(AirProgramCode, 1, 1) = 1 Then
    '            ApcSip.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 2, 1) = 1 Then
    '            ApcFederalSip.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 3, 1) = 1 Then
    '            ApcNonfederalSip.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 4, 1) = 1 Then
    '            ApcCfc.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 5, 1) = 1 Then
    '            ApcPsd.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 6, 1) = 1 Then
    '            ApcNsr.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 7, 1) = 1 Then
    '            ApcNeshap.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 8, 1) = 1 Then
    '            ApcNsps.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 9, 1) = 1 Then
    '            ApcFesop.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 10, 1) = 1 Then
    '            ApcAcid.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 11, 1) = 1 Then
    '            ApcNativeAmerican.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 12, 1) = 1 Then
    '            ApcMact.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 13, 1) = 1 Then
    '            ApcTitleV.Checked = True
    '        End If
    '        If Mid(AirProgramCode, 14, 1) = 1 Then
    '            ApcRmp.Checked = True
    '        End If

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try


    'End Sub

    'Sub Save()
    '    Dim ErrorCheck As Boolean = False
    '    Dim Classification As String = ""
    '    Dim OperationalStatus As String = ""
    '    Dim SICCode As String = ""
    '    Dim NAICSCode As String = ""
    '    Dim AirProgramCode As String = ""
    '    Dim temp As String = ""
    '    Dim StartUpDate As String = ""
    '    Dim ShutDownDate As String = ""
    '    Dim Comments As String = ""
    '    Dim PlantDescription As String = ""
    '    Dim NSRMajor As String = ""
    '    Dim StateProgramCodes As String = "00000"
    '    Dim AttainmentStatus As String = "00000"
    '    Dim RMPNumber As String = ""

    '    Try
    '        temp = txtAirsNumber.Text

    '        OperationalStatus.BackColor = Color.White
    '        Classification.BackColor = Color.White
    '        SICCode.BackColor = Color.White
    '        NAICSCode.BackColor = Color.White

    '        If AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1" Then
    '            'End If
    '            'If UserProgram = "5" Or (UserBranch = "1" And UserUnit = "---") _
    '            '    Or (UserProgram = "3" And AccountArray(68, 3) = "1") Then
    '            If Classification.Text <> "" And Classification.Text <> " " Then
    '                If Classification.Text <> dsHeaderData.Tables("Current").Rows(0).Item(2).ToString Then
    '                    Classification = Classification.Text
    '                Else
    '                    Classification = ""
    '                End If
    '            Else
    '                ErrorCheck = True
    '                Classification.BackColor = Color.Yellow
    '            End If
    '            If OperationalStatus.Text <> "" And OperationalStatus.Text <> " " Then
    '                If Mid(OperationalStatus.Text, 1, 1) <> dsHeaderData.Tables("Current").Rows(0).Item(1).ToString Then
    '                    OperationalStatus = Mid(OperationalStatus.Text, 1, 1)
    '                Else
    '                    OperationalStatus = ""
    '                End If
    '            Else
    '                ErrorCheck = True
    '                OperationalStatus.BackColor = Color.Yellow
    '            End If
    '            If SICCode.Text <> dsHeaderData.Tables("Current").Rows(0).Item(4).ToString Then
    '                If IsNumeric(SICCode.Text) Then
    '                    If SICCode.Text <> "" Then
    '                        SICCode = SICCode.Text
    '                    Else
    '                        SICCode = ""
    '                    End If
    '                Else
    '                    ErrorCheck = True
    '                    SICCode.BackColor = Color.Yellow
    '                End If
    '            Else
    '                SICCode = ""
    '            End If
    '            If NAICSCode.Text <> dsHeaderData.Tables("Current").Rows(0).Item(5).ToString Then
    '                If IsNumeric(NAICSCode.Text) Then
    '                    If NAICSCode.Text <> "" Then
    '                        If ValidateNAICS(NAICSCode.Text) = False Then
    '                            ErrorCheck = True
    '                            NAICSCode.BackColor = Color.Yellow
    '                        End If
    '                        NAICSCode = NAICSCode.Text
    '                    Else
    '                        NAICSCode = ""
    '                    End If
    '                Else
    '                    ErrorCheck = True
    '                    NAICSCode.BackColor = Color.Yellow
    '                End If
    '            End If
    '            If NSRMajor.Checked = True Then
    '                StateProgramCodes = "10000"
    '            Else
    '                StateProgramCodes = "00000"
    '            End If
    '            If HapMajor.Checked = True Then
    '                StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "1" & Mid(StateProgramCodes, 3)
    '            Else
    '                StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "0" & Mid(StateProgramCodes, 3)
    '            End If
    '            If StateProgramCodes <> dsHeaderData.Tables("Current").Rows(0).Item(15).ToString Then
    '                'StateProgramCodes = StateProgramCodes
    '            Else
    '                StateProgramCodes = ""
    '            End If

    '            Select Case OneHourOzone.Text
    '                Case "Yes"
    '                    AttainmentStatus = "01000"
    '                Case "Contribute"
    '                    AttainmentStatus = "02000"
    '                Case Else
    '                    AttainmentStatus = "00000"
    '            End Select
    '            Select Case EightHourOzone.Text
    '                Case "Atlanta"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
    '                Case "Macon"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
    '                Case Else
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
    '            End Select
    '            Select Case PmFine.Text
    '                Case "Atlanta"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
    '                Case "Chattanooga"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
    '                Case "Floyd"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
    '                Case "Macon"
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
    '                Case Else
    '                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
    '            End Select
    '            If AttainmentStatus <> dsHeaderData.Tables("Current").Rows(0).Item(6).ToString Then
    '                'AttainmentStatus = AttainmentStatus
    '            Else
    '                AttainmentStatus = ""
    '            End If

    '            AirProgramCode = ""

    '            If ApcSip.Checked = True Then
    '                AirProgramCode = "1"
    '                SQL = "Select strPollutantKey " & _
    '                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "0' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "0', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "0' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "0' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "0'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = "0"
    '                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
    '                   "active = '0', " & _
    '                   "UpdateUser = '" & UserGCode & "', " & _
    '                   "updateDateTime = '" & OracleDate & "' " & _
    '                   "where strSubPartKey = '0413" & txtAirsNumber.Text & "0' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                dr.Close()
    '            End If
    '            If ApcFederalSip.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "1' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "1', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "1' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "1' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "1'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcNonfederalSip.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "3' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "3', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "3' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "3' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "3'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcCfc.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "4' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "4', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "4' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "4' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "4'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcPsd.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "6' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "6', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "6' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "6' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "6'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcNsr.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "7' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "7', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "7' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "7' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "7'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcNeshap.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "8' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "8', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "8' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "8' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "8'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
    '               "active = '0', " & _
    '               "UpdateUser = '" & UserGCode & "', " & _
    '               "updateDateTime = '" & OracleDate & "' " & _
    '               "where strSubPartKey = '0413" & txtAirsNumber.Text & "8' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                dr.Close()
    '            End If
    '            If ApcNsps.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "9' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "9', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "9' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "9' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "9'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
    '               "active = '0', " & _
    '               "UpdateUser = '" & UserGCode & "', " & _
    '               "updateDateTime = '" & OracleDate & "' " & _
    '               "where strSubPartKey = '0413" & txtAirsNumber.Text & "9' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                dr.Close()
    '            End If
    '            If ApcFesop.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "F' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "F', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "F' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "F' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "F'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcAcid.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "A' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "A', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "A' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "A' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "A'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcNativeAmerican.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "I' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "I', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "I' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "I' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "I'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcMact.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "M' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "M', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "M' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "M' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "M'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
    '                "active = '0', " & _
    '                "UpdateUser = '" & UserGCode & "', " & _
    '                "updateDateTime = '" & OracleDate & "' " & _
    '                "where strSubPartKey = '0413" & txtAirsNumber.Text & "M' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                dr.Close()
    '            End If
    '            If ApcTitleV.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '                SQL = "Select strPollutantKey " & _
    '               "from " & DBNameSpace & ".APBAirProgramPollutants " & _
    '               "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "V' "
    '                cmd = New OracleCommand(SQL, CurrentConnection)
    '                If CurrentConnection.State = ConnectionState.Closed Then
    '                    CurrentConnection.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                recExist = dr.Read
    '                dr.Close()
    '                If recExist = False Then
    '                    SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
    '                     "(strAirsNumber, strAirPollutantKey, " & _
    '                     "strPollutantKey, strComplianceStatus, " & _
    '                     "strModifingPerson, datModifingDate, " & _
    '                     "strOperationalStatus) " & _
    '                     "values " & _
    '                     "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "V', " & _
    '                     "'OT', 'C', " & _
    '                     "'" & UserGCode & "', '" & OracleDate & "', " & _
    '                     "'O')"
    '                    cmd = New OracleCommand(SQL, CurrentConnection)
    '                    If CurrentConnection.State = ConnectionState.Closed Then
    '                        CurrentConnection.Open()
    '                    End If
    '                    dr = cmd.ExecuteReader
    '                    dr.Read()
    '                    dr.Close()
    '                Else
    '                    If OperationalStatus <> "" Then
    '                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
    '                        "strOperationalStatus = '" & OperationalStatus & "' " & _
    '                        "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "V' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        dr.Read()
    '                        dr.Close()
    '                    End If
    '                    If Classification <> "" Then
    '                        SQL = "Select strUpdateStatus " & _
    '                        "from " & DBNameSpace & ".AFSAirPollutantData " & _
    '                        "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "V' "
    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        dr = cmd.ExecuteReader
    '                        While dr.Read
    '                            temp = dr.Item("strUpdateStatus")
    '                        End While
    '                        dr.Close()
    '                        If temp = "N" Then
    '                            SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
    '                            "strUpdateStatus = 'C' " & _
    '                            "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "V'"
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            dr = cmd.ExecuteReader
    '                            dr.Read()
    '                            dr.Close()
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If
    '            If ApcRmp.Checked = True Then
    '                AirProgramCode = AirProgramCode & "1"
    '            Else
    '                AirProgramCode = AirProgramCode & "0"
    '            End If

    '            AirProgramCode = AirProgramCode & "0"

    '            If AirProgramCode <> dsHeaderData.Tables("Current").Rows(0).Item(3).ToString Then
    '                'AirProgramCode = AirProgramCode
    '            Else
    '                AirProgramCode = ""
    '            End If

    '            If StartUpDate.Text <> dsHeaderData.Tables("Current").Rows(0).Item(9).ToString Then
    '                If StartUpDate.Checked = True Then
    '                    StartUpDate = StartUpDate.Text
    '                Else
    '                    If dsHeaderData.Tables("Current").Rows(0).Item(9).ToString <> "" Then
    '                        StartUpDate = ""
    '                    Else
    '                        StartUpDate = "Ignore"
    '                    End If
    '                End If
    '            Else
    '                StartUpDate = "Ignore"
    '            End If

    '            If ShutDownDate.Text <> dsHeaderData.Tables("Current").Rows(0).Item(10).ToString Then
    '                If ShutDownDate.Checked = True Then
    '                    ShutDownDate = ShutDownDate.Text
    '                Else
    '                    If dsHeaderData.Tables("Current").Rows(0).Item(10).ToString <> "" Then
    '                        ShutDownDate = ""
    '                    Else
    '                        ShutDownDate = "Ignore"
    '                    End If
    '                End If
    '            Else
    '                ShutDownDate = "Ignore"
    '            End If

    '            If PlantDescription.Text <> dsHeaderData.Tables("Current").Rows(0).Item(14).ToString Then
    '                If PlantDescription.Text <> "" Then
    '                    PlantDescription = Replace(PlantDescription.Text, "'", "''")
    '                Else
    '                    PlantDescription = ""
    '                End If
    '            Else
    '                PlantDescription = "Ignore"
    '            End If

    '            If Comments.Text <> "" Then
    '                If Comments.Text <> dsHeaderData.Tables("Current").Rows(0).Item(13).ToString Then
    '                    Comments = Replace(Comments.Text, "'", "''")
    '                Else
    '                    MsgBox("Since this is a direct change to the data, " & vbCrLf & _
    '                    "please make a unique comment. " & vbCrLf & _
    '                    "So future users know why the data was changed." & vbCrLf & _
    '                    "No data will be saved at this time.", _
    '                     MsgBoxStyle.Information, "Edit Header Data")
    '                    Comments = "Error"
    '                End If
    '            Else
    '                MsgBox("Since this is a direct change to the data, " & vbCrLf & _
    '                "please make a unique comment. " & vbCrLf & _
    '                "So future users know why the data was changed." & vbCrLf & _
    '                "No data will be saved at this time.", _
    '                 MsgBoxStyle.Information, "Edit Header Data")
    '                Comments = "Error"
    '            End If

    '            If ErrorCheck <> True Then
    '                If Comments <> "Error" Then
    '                    If Classification <> "" Or AirProgramCode <> "" Or SICCode <> "" Or _
    '                      OperationalStatus <> "" Or StartUpDate <> "Ignore" Or _
    '                      ShutDownDate <> "Ignore" Or PlantDescription <> "Ignore" Or _
    '                      Comments <> "" Or AttainmentStatus <> "" Or _
    '                      StateProgramCodes <> "" Or NAICSCode <> "" Then

    '                        SQL = "Update " & DBNameSpace & ".APBHeaderData set "
    '                        If Classification <> "" Then
    '                            SQL = SQL & "strClass = '" & Classification & "', "
    '                        End If
    '                        If OperationalStatus <> "" Then
    '                            SQL = SQL & "strOperationalstatus = '" & OperationalStatus & "', "
    '                        End If
    '                        If SICCode <> "" Then
    '                            SQL = SQL & "strSICCode = '" & SICCode & "', "
    '                        End If
    '                        If NAICSCode <> "" Then
    '                            SQL = SQL & "strNAICSCode = '" & NAICSCode & "', "
    '                        End If
    '                        If StateProgramCodes <> "" Then
    '                            SQL = SQL & "strStateProgramCodes = '" & StateProgramCodes & "', "
    '                        End If
    '                        If AttainmentStatus <> "" Then
    '                            SQL = SQL & "strAttainmentStatus = '" & AttainmentStatus & "', "
    '                        End If
    '                        If AirProgramCode <> "" Then
    '                            SQL = SQL & "strAIRProgramCodes = '" & AirProgramCode & "', "
    '                        End If
    '                        If StartUpDate <> "Ignore" Then
    '                            SQL = SQL & "datStartUpDate = '" & StartUpDate & "', "
    '                        End If
    '                        If ShutDownDate <> "Ignore" Then
    '                            SQL = SQL & "datShutDownDate = '" & ShutDownDate & "', "
    '                        End If
    '                        If PlantDescription <> "Ignore" Then
    '                            SQL = SQL & "strPlantDescription = '" & Replace(PlantDescription, "'", "''") & "', "
    '                        End If
    '                        If Comments <> "Ignore" Then
    '                            SQL = SQL & "strComments = '" & Replace(Comments, "'", "''") & "', "
    '                        End If
    '                        SQL = SQL & "strModifingPerson = '" & UserGCode & "', " & _
    '                        "datModifingDate = '" & OracleDate & "', " & _
    '                        "strModifingLocation = '2' " & _
    '                        "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

    '                        cmd = New OracleCommand(SQL, CurrentConnection)

    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If


    '                        dr = cmd.ExecuteReader
    '                        dr.Close()

    '                        If RmpId.Text <> "" Then
    '                            RMPNumber = RmpId.Text
    '                        Else
    '                            RMPNumber = ""
    '                        End If

    '                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
    '                        "strRMPID = '" & Replace(RMPNumber, "'", "''") & "' " & _
    '                        "where strAIRSnumber = '0413" & txtAirsNumber.Text & "' "

    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If

    '                        dr = cmd.ExecuteReader
    '                        dr.Close()

    '                        If OperationalStatus = "X" Then
    '                            SQL = "Update airbranch.EIS_FacilitySite set " & _
    '                            "strFacilitySiteStatusCode = 'PS', " & _
    '                            "strFacilitySiteComment = 'Facility Shutdown by permitting action.', " & _
    '                            "UpdateUSer = '" & UserName & "', " & _
    '                            "updateDateTime = sysdate " & _
    '                            "where facilitySiteID = '" & txtAirsNumber.Text & "' "
    '                            cmd = New OracleCommand(SQL, CurrentConnection)
    '                            If CurrentConnection.State = ConnectionState.Closed Then
    '                                CurrentConnection.Open()
    '                            End If
    '                            cmd.ExecuteReader()
    '                        End If

    '                        SQL = "Update AIRBranch.EIS_FacilitySite set " & _
    '                        "strFacilitySiteDescription = '" & Replace(txtPlantDescription.Text, "'", "''") & "' " & _
    '                        "where facilitySiteID = '" & txtAirsNumber.Text & "' "

    '                        cmd = New OracleCommand(SQL, CurrentConnection)
    '                        If CurrentConnection.State = ConnectionState.Closed Then
    '                            CurrentConnection.Open()
    '                        End If
    '                        cmd.ExecuteReader()


    '                        LoadFacilityHeaderData()
    '                        MsgBox("Data Updated", MsgBoxStyle.Information, "Edit Header Data")
    '                        Me.DialogResult = System.Windows.Forms.DialogResult.OK

    '                    Else
    '                        MsgBox("No data was changed", MsgBoxStyle.Information, "Edit Header Data")
    '                    End If
    '                End If
    '            Else
    '                MsgBox("The data was not save due to bad data.", _
    '                                   MsgBoxStyle.Information, "Edit Header Data")
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ErrorReport(temp & vbCrLf & ex.ToString(), "DevEditHeaderData.Save")
    '    Finally

    '    End Try

    'End Sub

    'Private Sub txtKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim temp As String = ""
    '        Dim ModifingPerson As String
    '        Dim ModifingDate As String
    '        Dim ModifingLocation As String



    '        If txtKey.Text <> "" Then
    '            SQL = "select * " & _
    '            "from " & DBNameSpace & ".VW_HB_APBHeaderData " & _
    '            "where strKEy = '" & txtKey.Text & "' "
    '            cmd = New OracleCommand(SQL, CurrentConnection)
    '            If CurrentConnection.State = ConnectionState.Closed Then
    '                CurrentConnection.Open()
    '            End If
    '            dr = cmd.ExecuteReader
    '            recExist = dr.Read
    '            If recExist = True Then

    '                If IsDBNull(dr.Item("strOperationalStatus")) Then
    '                    OperationalStatus.Text = "Unknown - Please Fix"
    '                Else
    '                    temp = dr.Item("strOperationalStatus")
    '                    Select Case temp
    '                        Case "O"
    '                            OperationalStatus.Text = "O - Operational"
    '                        Case "P"
    '                            OperationalStatus.Text = "P - Planned"
    '                        Case "C"
    '                            OperationalStatus.Text = "C - Under Construction"
    '                        Case "T"
    '                            OperationalStatus.Text = "T - Temporarily Closed"
    '                        Case "X"
    '                            OperationalStatus.Text = "X - Closed/Dismantled"
    '                        Case "I"
    '                            OperationalStatus.Text = "I - Seasonal Operation"
    '                        Case Else
    '                            OperationalStatus.Text = "Unknown - Please Fix"
    '                    End Select
    '                End If
    '                If IsDBNull(dr.Item("strClass")) Then
    '                    Classification.Text = " "
    '                Else
    '                    temp = dr.Item("strClass")
    '                    Classification.Text = dr.Item("strClass")
    '                End If

    '                If IsDBNull(dr.Item("strSICCode")) Then
    '                    SicCode.Text = ""
    '                Else
    '                    SicCode.Text = dr.Item("strSICCode")
    '                End If
    '                If IsDBNull(dr.Item("strNAICSCode")) Then
    '                    NaicsCode.Clear()
    '                Else
    '                    NaicsCode.Text = dr.Item("strNAICSCode")
    '                End If
    '                If IsDBNull(dr.Item("UserName")) Then
    '                    ModifingPerson = "Unknown"
    '                Else
    '                    ModifingPerson = dr.Item("USERName")
    '                End If
    '                If IsDBNull(dr.Item("ModifingDate")) Then
    '                    ModifingDate = "Unknown Date"
    '                Else
    '                    ModifingDate = dr.Item("ModifingDate")
    '                End If
    '                If IsDBNull(dr.Item("strModifingLocation")) Then
    '                    ModifingLocation = "Unknown Location"
    '                Else
    '                    ModifingLocation = dr.Item("strModifingLocation")
    '                End If
    '                txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

    '                If IsDBNull(dr.Item("strComments")) Then
    '                    Comments.Text = ""
    '                Else
    '                    Comments.Text = dr.Item("strComments")
    '                End If
    '                If IsDBNull(dr.Item("datStartUpDate")) Then
    '                    StartUpDate.Text = OracleDate
    '                    StartUpDate.Checked = False
    '                Else
    '                    StartUpDate.Checked = True
    '                    StartUpDate.Text = dr.Item("datStartUpDate")
    '                End If
    '                If IsDBNull(dr.Item("datShutDownDate")) Then
    '                    ShutdownDate.Text = OracleDate
    '                    ShutdownDate.Checked = False
    '                Else
    '                    ShutdownDate.Checked = True
    '                    ShutdownDate.Text = dr.Item("datShutDownDate")
    '                End If
    '                If IsDBNull(dr.Item("strPlantDescription")) Then
    '                    FacilityDescription.Text = ""
    '                Else
    '                    FacilityDescription.Text = dr.Item("strPlantDescription")
    '                End If
    '                If IsDBNull(dr.Item("strStateProgramCodes")) Then
    '                    NsrMajor.Checked = False
    '                    HapMajor.Checked = False
    '                Else
    '                    If Mid(dr.Item("strStateProgramCodes"), 1, 1) = "1" Then
    '                        NsrMajor.Checked = True
    '                    Else
    '                        NsrMajor.Checked = False
    '                    End If
    '                    If Mid(dr.Item("strStateProgramCodes"), 2, 1) = "1" Then
    '                        HapMajor.Checked = True
    '                    Else
    '                        HapMajor.Checked = False
    '                    End If
    '                End If
    '                If IsDBNull(dr.Item("strAttainmentStatus")) Then
    '                    OneHourOzone.Text = "No"
    '                    EightHourOzone.Text = "No"
    '                    PmFine.Text = "No"
    '                Else
    '                    Select Case (Mid(dr.Item("strAttainmentStatus"), 2, 1))
    '                        Case 1
    '                            OneHourOzone.Text = "Yes"
    '                        Case 2
    '                            OneHourOzone.Text = "Contribute"
    '                        Case Else
    '                            OneHourOzone.Text = "No"
    '                    End Select
    '                    Select Case (Mid(dr.Item("strAttainmentStatus"), 3, 1))
    '                        Case 1
    '                            EightHourOzone.Text = "Atlanta"
    '                        Case 2
    '                            EightHourOzone.Text = "Macon"
    '                        Case Else
    '                            EightHourOzone.Text = "No"
    '                    End Select
    '                    Select Case (Mid(dr.Item("strAttainmentStatus"), 4, 1))
    '                        Case 1
    '                            PmFine.Text = "Atlanta"
    '                        Case 2
    '                            PmFine.Text = "Chattanooga"
    '                        Case 3
    '                            PmFine.Text = "Floyd"
    '                        Case 4
    '                            PmFine.Text = "Macon"
    '                        Case Else
    '                            PmFine.Text = "No"
    '                    End Select
    '                End If
    '                If IsDBNull(dr.Item("strAirProgramCodes")) Then
    '                    temp = "000000000000000"
    '                Else
    '                    temp = dr.Item("strAirProgramCodes")
    '                End If
    '            End If

    '            AddAirProgramCodes(temp)

    '        End If

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try

    'End Sub

#End Region

End Class