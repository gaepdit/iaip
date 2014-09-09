' TODO:
' Fix Modified by statement when editing/canceling
' Fix RMP (don't require)
' Test 001-00001

Imports Iaip.Apb
Imports System.Collections.Generic

Public Class IAIPEditHeaderData

#Region " Form properties and variables "

    Private _airsNumber As String
    Public Property AirsNumber() As String
        Get
            Return _airsNumber
        End Get
        Set(ByVal value As String)
            _airsNumber = value
        End Set
    End Property

    Private _facilityName As String
    Public Property FacilityName() As String
        Get
            Return _facilityName
        End Get
        Set(ByVal value As String)
            _facilityName = value
        End Set
    End Property

    Private FacilityHeaderDataHistory As DataTable

    Private CurrentFacilityHeaderData As FacilityHeaderData

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

        If Not Facility.ValidAirsNumber(Me.AirsNumber) Then
            MessageBox.Show("AIRS number is not valid. Try again.", _
                            "No AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If

        AirsNumberDisplay.Text = Facility.FormatAirsNumber(Me.AirsNumber)
        FacilityNameDisplay.Text = Me.FacilityName

        DisableEditing()
        PreloadComboBoxes()
        LoadFacilityData()

        CheckEditingPermissions()
    End Sub

    Private Sub PreloadComboBoxes()
        ClassificationDropDown.BindToEnum(Of Facility.Classification)()
        OperationalDropDown.BindToEnum(Of Facility.OperationalStatus)()
        EightHourOzoneDropDown.BindToEnum(Of Facility.EightHourNonattainmentStatus)()
        OneHourOzoneDropDown.BindToEnum(Of Facility.OneHourNonattainmentStatus)()
        PmFineDropDown.BindToEnum(Of Facility.PMFineNonattainmentStatus)()
    End Sub

    Private Sub LoadFacilityData()
        FacilityHeaderDataHistory = DAL.FacilityHeaderData.GetFacilityHeaderDataHistoryAsDataTable(AirsNumber)

        Dim currentData As DataRow = DAL.FacilityHeaderData.GetFacilityHeaderDataAsDataRow(AirsNumber)
        currentData(0) = FacilityHeaderDataHistory.Compute("Max(STRKEY)", String.Empty) + 1
        FacilityHeaderDataHistory.ImportRow(currentData)

        BindFacilityHistoryDisplay(FacilityHeaderDataHistory)

        CurrentFacilityHeaderData = New FacilityHeaderData(AirsNumber)
        DAL.FillFacilityHeaderDataFromDataRow(currentData, CurrentFacilityHeaderData)
        DisplayFacilityData(CurrentFacilityHeaderData)

        FacilityHistoryDataGridView.Rows(0).Selected = True
    End Sub

    Private Sub BindFacilityHistoryDisplay(ByVal dt As DataTable)
        FacilityHistoryDataGridView.DataSource = dt

        FacilityHistoryDataGridView.Columns("STRKEY").Visible = False

        FacilityHistoryDataGridView.Columns("USERNAME").HeaderText = "Modified By"
        FacilityHistoryDataGridView.Columns("MODIFINGDATE").HeaderText = "Date Modified"
        FacilityHistoryDataGridView.Columns("STRMODIFINGLOCATION").HeaderText = "Modified From"
        FacilityHistoryDataGridView.Columns("STROPERATIONALSTATUS").HeaderText = "Operating Status"
        FacilityHistoryDataGridView.Columns("STRCLASS").HeaderText = "Classification"
        FacilityHistoryDataGridView.Columns("STRCOMMENTS").HeaderText = "Comments"
        FacilityHistoryDataGridView.Columns("DATSTARTUPDATE").HeaderText = "Start Up Date"
        FacilityHistoryDataGridView.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
        FacilityHistoryDataGridView.Columns("STRPLANTDESCRIPTION").HeaderText = "Plant Description"
        FacilityHistoryDataGridView.Columns("STRSICCODE").HeaderText = "SIC Code"
        FacilityHistoryDataGridView.Columns("STRNAICSCODE").HeaderText = "NAICS Code"
        FacilityHistoryDataGridView.Columns("STRRMPID").HeaderText = "RMP ID"

        FacilityHistoryDataGridView.Columns("STRAIRPROGRAMCODES").Visible = False
        FacilityHistoryDataGridView.Columns("STRSTATEPROGRAMCODES").Visible = False
        FacilityHistoryDataGridView.Columns("STRATTAINMENTSTATUS").Visible = False

        FacilityHistoryDataGridView.Sort(FacilityHistoryDataGridView.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        FacilityHistoryDataGridView.SanelyResizeColumns()

        AddHandler FacilityHistoryDataGridView.CurrentCellChanged, AddressOf FacilityHistoryDisplay_CurrentCellChanged
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
        FacilityHistoryDataGridView.Enabled = False
        FacilityHistoryDataGridView.ClearSelection()

        DisplayFacilityData(CurrentFacilityHeaderData)
        Comments.Clear()

        Dim EditableControls As Control() = { _
            ClassificationDropDown, _
            OperationalDropDown, _
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

        Comments.Focus()
    End Sub

    Private Sub DisableEditing()
        ResetHighlight()

        FacilityHistoryDataGridView.Enabled = True
        If FacilityHistoryDataGridView.RowCount > 0 Then
            FacilityHistoryDataGridView.Rows(0).Selected = True
        End If

        Dim EditableControls As Control() = { _
            ClassificationDropDown, _
            OperationalDropDown, _
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
            EnableEditing()
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
        SaveEditedData()
    End Sub

#End Region

#Region " Item selection and display "

    Private Sub FacilityHistoryDisplay_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Handler added after initial load of data
        If FacilityHistoryDataGridView.CurrentRow IsNot Nothing Then
            Dim currentDataRowView As DataRowView = CType(FacilityHistoryDataGridView.CurrentRow.DataBoundItem, DataRowView)
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

            ClassificationDropDown.SelectedValue = .Classification
            OperationalDropDown.SelectedValue = .OperationalStatus
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

            OneHourOzoneDropDown.SelectedValue = .OneHourNonAttainmentState
            EightHourOzoneDropDown.SelectedValue = .EightHourNonAttainmentState
            PmFineDropDown.SelectedValue = .PMFineNonAttainmentState

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
            .Classification = ClassificationDropDown.SelectedValue
            .OperationalStatus = OperationalDropDown.SelectedValue
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

            .OneHourNonAttainmentState = OneHourOzoneDropDown.SelectedValue
            .EightHourNonAttainmentState = EightHourOzoneDropDown.SelectedValue
            .PMFineNonAttainmentState = PmFineDropDown.SelectedValue

            .FacilityDescription = FacilityDescription.Text
            .RmpId = RmpId.Text
            .HeaderUpdateComment = Comments.Text
        End With

        Return facilityHeaderData
    End Function

    Private Function PreSaveCheck(ByVal editedFacility As FacilityHeaderData) As Boolean
        ' Compare edited data to current data
        If ComparableHeaderData(editedFacility).Equals(ComparableHeaderData(CurrentFacilityHeaderData)) Then
            MessageBox.Show("No data has been changed. Nothing saved.", _
                            "Nothing Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Validate fields and require comment ... See Save()
        Dim invalidControls As New List(Of Control)
        If Not ValidateAllFields(invalidControls) Then
            HighlightInvalidControls(invalidControls)
            MessageBox.Show("Some data is not valid. Double-check your entries." & vbNewLine & vbNewLine & "Nothing saved.", _
                            "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Function ValidateAllFields(<Runtime.InteropServices.Out()> ByRef invalidControls As List(Of Control)) As Boolean
        Dim valid As Boolean = True
        invalidControls = New List(Of Control)

        If ClassificationDropDown.SelectedValue = Facility.Classification.Unspecified Then
            valid = False
            invalidControls.Add(ClassificationLabel)
        End If

        If OperationalDropDown.SelectedValue = Facility.OperationalStatus.Unspecified Then
            valid = False
            invalidControls.Add(OperationalStatusLabel)
        End If

        If Not DAL.FacilityHeaderData.SicCodeExists(SicCode.Text) Then
            valid = False
            invalidControls.Add(SicCodeLabel)
        End If

        If Not DAL.FacilityHeaderData.NaicsCodeExists(NaicsCode.Text) Then
            valid = False
            invalidControls.Add(NaicsCodeLabel)
        End If

        If String.IsNullOrEmpty(Comments.Text) Then
            MessageBox.Show("Since this is a direct change to the data, please add a useful comment so future users " & _
                            "will know the reason for the change." & vbNewLine & vbNewLine & "Nothing saved.", _
                            "Missing Comment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(CommentsLabel)
        End If

        If String.IsNullOrEmpty(FacilityDescription.Text) Then
            valid = False
            invalidControls.Add(FacilityDescriptionLabel)
        End If

        If Not (RmpId.Text = RmpId.Mask) AndAlso Not FacilityHeaderData.ValidRmpId(RmpId.Text) Then
            valid = False
            invalidControls.Add(RmpIdLabel)
        End If

        Return valid
    End Function

    Private Sub HighlightInvalidControls(ByVal invalidControls As List(Of Control))
        For Each c As Control In invalidControls
            c.BackColor = Color.Yellow
        Next
    End Sub

    Private Sub ResetHighlight()
        Dim resetableControls As New List(Of Control)(New Control() { _
          ClassificationLabel, _
          OperationalStatusLabel, _
          SicCodeLabel, _
          NaicsCodeLabel, _
          CommentsLabel, _
          FacilityDescriptionLabel, _
          RmpIdLabel _
        })

        For Each c As Control In resetableControls
            c.BackColor = System.Drawing.SystemColors.Control
        Next
    End Sub

    Private Function ComparableHeaderData(ByVal headerdata As FacilityHeaderData) As FacilityHeaderData
        Dim comparableHeaderDataReturn As FacilityHeaderData = headerdata
        With comparableHeaderDataReturn
            .HeaderUpdateComment = Nothing
            .DateDataModified = Nothing
            .WhoModified = Nothing
            .WhereModified = Nothing
        End With
        Return comparableHeaderDataReturn
    End Function

    Private Sub SaveEditedData()
        ResetHighlight()

        Dim editedFacility As FacilityHeaderData = BoxUpFacilityFromForm()

        If PreSaveCheck(editedFacility) Then

            ' Save edited data
            Dim result As Boolean = DAL.FacilityHeaderData.SaveFacilityHeaderData(editedFacility)

            If result Then
                ' If successful, report back to Facility Summary 
                Me.SomethingWasSaved = True

                ' + replace local variable with current facility data
                CurrentFacilityHeaderData = editedFacility

                ' + Add to datagridview
                Dim currentData As DataRow = DAL.FacilityHeaderData.GetFacilityHeaderDataAsDataRow(AirsNumber)
                currentData(0) = FacilityHeaderDataHistory.Compute("Max(STRKEY)", String.Empty) + 1
                FacilityHeaderDataHistory.ImportRow(currentData)

            Else
                MessageBox.Show("There was an error saving the new data. Please try again.", _
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub

#End Region



#Region "old stuff"

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