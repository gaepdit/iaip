' TODO:
' [X] Fix Modified by statement when editing/canceling
' [ ] Fix RMP (don't require)
' [ ] Test 001-00001

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

        FacilityHistoryDataGridView.Select()
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

        AddHandler FacilityHistoryDataGridView.CurrentCellChanged, AddressOf FacilityHistoryDataGridView_CurrentCellChanged
    End Sub

    Private Sub CheckEditingPermissions()
        If Not EditingIsAllowed() Then
            DisableAndHide(New Control() {EditData, SaveChangesButton, CancelEditButton})
        End If
    End Sub

#End Region

#Region " Editing "

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
            AirProgramClassifications, _
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

        Dim EditableControls As Control() = { _
            ClassificationDropDown, _
            OperationalDropDown, _
            SicCode, _
            StartUpDate, _
            ShutdownDate, _
            NaicsCode, _
            AirProgramCodes, _
            AirProgramClassifications, _
            NonattainmentStatuses, _
            FacilityDescription, _
            RmpId, _
            Comments, _
            SaveChangesButton, _
            CancelEditButton _
        }
        DisableControls(EditableControls)

        FacilityHistoryDataGridView.Enabled = True
    End Sub

    Private Sub CancelEditing()
        DisableEditing()
        DisplayCurrentRow()
    End Sub

    Private Sub EditData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditData.CheckedChanged
        If EditData.Checked Then
            EnableEditing()
        Else
            CancelEditing()
        End If
    End Sub

    Private Sub CancelEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelEditButton.Click
        If EditData.Checked Then
            EditData.Checked = False
        End If
    End Sub

    Private Sub SaveChangesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveChangesButton.Click
        SaveEditedData()
    End Sub

    Private Sub IAIPEditHeaderData_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape AndAlso EditData.Checked Then
            EditData.Checked = False
        End If
    End Sub

    Private Sub OperationalDropDown_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OperationalDropDown.SelectedIndexChanged
        ResetHighlight()
        If EditData.Checked Then
            Dim NonShutdownControls As Control() = { _
                ClassificationDropDown, _
                SicCode, _
                StartUpDate, _
                NaicsCode, _
                AirProgramCodes, _
                AirProgramClassifications, _
                NonattainmentStatuses, _
                FacilityDescription, _
                RmpId _
            }
            If UserIsTryingToCloseFacility() Then
                DisableControls(NonShutdownControls)
                ModifiedDescDisplay.Text = "When changing operating status to Closed/Dismantled, no other data can be modified. " & _
                "Save any other required changes first. Please enter a final permit revocation date."
                PermitRevocationDateLabel.BackColor = Color.Yellow
            Else
                EnableControls(NonShutdownControls)
                ModifiedDescDisplay.Text = "Editing current facility data."
            End If
        End If
    End Sub

    Private Function UserIsTryingToCloseFacility() As Boolean
        If OperationalDropDown.SelectedValue = Facility.OperationalStatus.X _
        AndAlso CurrentFacilityHeaderData.OperationalStatus <> Facility.OperationalStatus.X Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region " Item selection and display "

    Private Sub FacilityHistoryDataGridView_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Handler added after initial load of data
        DisplayCurrentRow()
    End Sub

    Private Sub DisplayCurrentRow()
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

            If EditData.Checked Then
                ModifiedDescDisplay.Text = "Editing current facility data"
            Else
                ModifiedDescDisplay.Text = "Modification by " & .WhoModified & _
                    " on " & .DateDataModified & _
                    If(String.IsNullOrEmpty(.WhereModified), "", " from " & .WhereModified & ".")
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

        ' See if facility is being shut down & check permissions
        If UserIsTryingToCloseFacility() Then
            If Not UserCanShutDownFacility() Then
                MessageBox.Show("You do not have permissions to shut down a facility. Please contact your manager.", _
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                If Not ConfirmFacilityShutdown(editedFacility) Then
                    Return False
                End If
            End If
        End If

        ' Confirm facility shutdown

        ' Compare edited data to current data
        If ComparableHeaderData(editedFacility).Equals(ComparableHeaderData(CurrentFacilityHeaderData)) Then
            MessageBox.Show("No data has been changed. Nothing saved.", _
                            "Nothing Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Validate fields 
        Dim invalidControls As New List(Of Control)
        If Not ValidateAllFields(editedFacility, invalidControls) Then
            HighlightInvalidControls(invalidControls)
            MessageBox.Show("Some data is not valid. Double-check your entries." & vbNewLine & vbNewLine & "Nothing saved.", _
                            "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Function ConfirmFacilityShutdown(ByVal editedFacility As FacilityHeaderData) As Boolean
        Return MessageBox.Show("Are you sure you want to mark this facility as closed/dismantled? " & _
                               "This will revoke all existing permits.", _
                               "Warning", MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
    End Function

    Private Function ValidateAllFields(ByVal editedFacility As FacilityHeaderData, <Runtime.InteropServices.Out()> ByRef invalidControls As List(Of Control)) As Boolean
        Dim valid As Boolean = True
        invalidControls = New List(Of Control)

        If String.IsNullOrEmpty(Comments.Text) Then
            MessageBox.Show("Since this is a direct change to the data, please add a useful comment so future users " & _
                            "will know the reason for the change.", _
                            "Missing Comment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(CommentsLabel)
        End If

        If editedFacility.OperationalStatus = Facility.OperationalStatus.X _
        AndAlso CurrentFacilityHeaderData.OperationalStatus <> Facility.OperationalStatus.X _
        AndAlso editedFacility.ShutdownDate Is Nothing Then
            MessageBox.Show("You have marked the facility as closed. Please enter the date the final permit was revoked.", _
                            "Missing Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(PermitRevocationDateLabel)
        End If

        If Not editedFacility.ShutdownDate Is Nothing _
            AndAlso editedFacility.OperationalStatus <> Facility.OperationalStatus.X _
            AndAlso editedFacility.ShutdownDate = Nothing Then
            MessageBox.Show("A permit revocation date is entered, but the facility is not marked as closed. Please reconcile this.", _
                            "Missing Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(PermitRevocationDateLabel)
        End If

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

        If String.IsNullOrEmpty(FacilityDescription.Text) Then
            valid = False
            invalidControls.Add(FacilityDescriptionLabel)
        End If

        If Not (RmpId.Text = "____-____-____") AndAlso Not FacilityHeaderData.ValidRmpId(RmpId.Text) Then
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
          PermitRevocationDateLabel, _
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

            Dim result As Boolean

            ' Save edited data
            If UserIsTryingToCloseFacility() Then
                result = DAL.ShutDownFacility(editedFacility.AirsNumber, _
                                              editedFacility.ShutdownDate, _
                                              editedFacility.HeaderUpdateComment, _
                                              FacilityHeaderData.ModificationLocation.HeaderDataEditor)
            Else
                result = DAL.SaveFacilityHeaderData(editedFacility, _
                                                    FacilityHeaderData.ModificationLocation.HeaderDataEditor)
            End If

            If result Then
                ' If successful, report back to Facility Summary 
                Me.SomethingWasSaved = True

                ' replace local variable with current facility data
                CurrentFacilityHeaderData = editedFacility

                ' Add to datagridview
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

#Region " Permissions "

    Private Function UserHasPermission(ByVal permissionCode As String) As Boolean
        If UserAccounts.Contains(permissionCode) Then Return True
        Return False
    End Function

    Private Function UserHasPermission(ByVal permissionsAllowed As String()) As Boolean
        For Each permissionCode As String In permissionsAllowed
            If UserHasPermission(permissionCode) Then Return True
        Next
        Return False
    End Function

    Private Function EditingIsAllowed() As Boolean
        If AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function UserCanShutDownFacility() As Boolean
        ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, & District Liasion
        If UserHasPermission(New String() {"(114)", "(19)", "(102)", "(27)"}) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class