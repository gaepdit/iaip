Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports Iaip.Apb
Imports Iaip.Apb.Facilities

Public Class IAIPEditHeaderData

#Region " Form properties and variables "

    Public Property AirsNumber As ApbFacilityId
    Public Property FacilityName As String
    Public Property SomethingWasSaved As Boolean
    Private Property FacilityHeaderDataHistory As DataTable
    Private Property CurrentFacilityHeaderData As FacilityHeaderData

#End Region

#Region " Form Load "

    Private Sub IAIPEditHeaderData_Load(sender As Object, e As EventArgs) Handles Me.Load
        AirsNumberDisplay.Text = AirsNumber.FormattedString
        FacilityNameDisplay.Text = FacilityName

        DisableEditing()
        PreloadComboBoxes()
        LoadFacilityData()

        CheckEditingPermissions()

        FacilityHistoryDataGridView.Select()
    End Sub

    Private Sub PreloadComboBoxes()
        ClassificationDropDown.BindToEnum(Of FacilityClassification)()
        OperationalDropDown.BindToEnum(Of FacilityOperationalStatus)()
        EightHourOzoneDropDown.BindToEnum(Of EightHourOzoneNonattainmentStatus)()
        OneHourOzoneDropDown.BindToEnum(Of OneHourOzoneNonattainmentStatus)()
        PmFineDropDown.BindToEnum(Of PMFineNonattainmentStatus)()
    End Sub

    Private Sub LoadFacilityData()
        FacilityHeaderDataHistory = DAL.GetFacilityHeaderDataHistoryAsDataTable(AirsNumber)
        FacilityHeaderDataHistory.Columns("STRKEY").AllowDBNull = True

        Dim currentData As DataRow = DAL.GetFacilityHeaderDataAsDataRow(AirsNumber)
        currentData.Table.Columns("STRKEY").ReadOnly = False

        If FacilityHeaderDataHistory Is Nothing OrElse FacilityHeaderDataHistory.Rows.Count = 0 Then
            currentData.Item("STRKEY") = 1
        Else
            currentData.Item("STRKEY") = Convert.ToInt32(FacilityHeaderDataHistory.AsEnumerable().Max(Function(row) Convert.ToInt32(row("STRKEY")))) + 1
        End If

        FacilityHeaderDataHistory.ImportRow(currentData)

        BindFacilityHistoryDisplay(FacilityHeaderDataHistory)

        CurrentFacilityHeaderData = New FacilityHeaderData(AirsNumber)
        DAL.FillFacilityHeaderDataFromDataRow(currentData, CurrentFacilityHeaderData)
        DisplayFacilityData(CurrentFacilityHeaderData)

        FacilityHistoryDataGridView.Rows(0).Selected = True
    End Sub

    Private Sub BindFacilityHistoryDisplay(dt As DataTable)
        FacilityHistoryDataGridView.DataSource = dt

        FacilityHistoryDataGridView.Columns("STRKEY").Visible = False
        FacilityHistoryDataGridView.Columns("STRKEY").DisplayIndex = 0

        FacilityHistoryDataGridView.Columns("WhoModified").HeaderText = "Modified By"
        FacilityHistoryDataGridView.Columns("WhoModified").DisplayIndex = 1
        FacilityHistoryDataGridView.Columns("DATMODIFINGDATE").HeaderText = "Date Modified"
        FacilityHistoryDataGridView.Columns("DATMODIFINGDATE").DisplayIndex = 2
        FacilityHistoryDataGridView.Columns("DATMODIFINGDATE").DefaultCellStyle.Format = DateFormat
        FacilityHistoryDataGridView.Columns("STRMODIFINGLOCATION").HeaderText = "Modified From"
        FacilityHistoryDataGridView.Columns("STRMODIFINGLOCATION").DisplayIndex = 3
        FacilityHistoryDataGridView.Columns("STROPERATIONALSTATUS").HeaderText = "Operating Status"
        FacilityHistoryDataGridView.Columns("STRCLASS").HeaderText = "Classification"
        FacilityHistoryDataGridView.Columns("STRCOMMENTS").HeaderText = "Comments"
        FacilityHistoryDataGridView.Columns("DATSTARTUPDATE").HeaderText = "Start Up Date"
        FacilityHistoryDataGridView.Columns("DATSTARTUPDATE").DefaultCellStyle.Format = DateFormat
        FacilityHistoryDataGridView.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
        FacilityHistoryDataGridView.Columns("DATSHUTDOWNDATE").DefaultCellStyle.Format = DateFormat
        FacilityHistoryDataGridView.Columns("STRPLANTDESCRIPTION").HeaderText = "Plant Description"
        FacilityHistoryDataGridView.Columns("STRSICCODE").HeaderText = "SIC Code"
        FacilityHistoryDataGridView.Columns("STRNAICSCODE").HeaderText = "NAICS Code"

        FacilityHistoryDataGridView.Columns("STRRMPID").Visible = False
        FacilityHistoryDataGridView.Columns("FacilityOwnershipTypeCode").Visible = False
        FacilityHistoryDataGridView.Columns("STRCMSMEMBER").Visible = False
        FacilityHistoryDataGridView.Columns("STRAIRSNUMBER").Visible = False
        FacilityHistoryDataGridView.Columns("STRAIRPROGRAMCODES").Visible = False
        FacilityHistoryDataGridView.Columns("STRSTATEPROGRAMCODES").Visible = False
        FacilityHistoryDataGridView.Columns("STRATTAINMENTSTATUS").Visible = False
        FacilityHistoryDataGridView.Columns("STRDISTRICTOFFICE").Visible = False
        FacilityHistoryDataGridView.Columns("STRLASTNAME").Visible = False
        FacilityHistoryDataGridView.Columns("STRFIRSTNAME").Visible = False
        FacilityHistoryDataGridView.Columns("STRMODIFINGPERSON").Visible = False

        FacilityHistoryDataGridView.Sort(FacilityHistoryDataGridView.Columns("STRKEY"), ListSortDirection.Descending)
        FacilityHistoryDataGridView.SanelyResizeColumns()

        AddHandler FacilityHistoryDataGridView.CurrentCellChanged, AddressOf FacilityHistoryDataGridView_CurrentCellChanged
    End Sub

    Private Sub FacilityHistoryDataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
    Handles FacilityHistoryDataGridView.CellFormatting
        If e.ColumnIndex = 16 Then
            e.Value = DirectCast(CType(e.Value, Integer), HeaderDataModificationLocation).GetDescription
        End If
    End Sub

    Private Sub CheckEditingPermissions()
        If Not CurrentUser.HasPermission(UserCan.EditFacilityHeaderData) Then
            HideControls({EditData, SaveChangesButton, CancelEditButton})
        End If
    End Sub

#End Region

#Region " Editing "

    Private Sub EnableEditing()
        FacilityHistoryDataGridView.Enabled = False
        FacilityHistoryDataGridView.ClearSelection()

        DisplayFacilityData(CurrentFacilityHeaderData)
        Comments.Clear()

        Dim editableControls As Control() = {
            ClassificationDropDown,
            OperationalDropDown,
            SicCode,
            StartUpDate,
            NaicsCode,
            AirProgramCodes,
            AirProgramClassificationsGroupbox,
            NonattainmentStatuses,
            FacilityDescription,
            RmpId,
            OwnershipGroupBox,
            Comments,
            SaveChangesButton,
            CancelEditButton
        }
        AllowControls(editableControls)

        If CurrentFacilityHeaderData.OperationalStatus = FacilityOperationalStatus.X Then
            OperationalDropDown.Enabled = False
        End If

        Comments.Focus()
    End Sub

    Private Sub DisableEditing()
        ResetControlHighlights()

        Dim editableControls As Control() = {
            ClassificationDropDown,
            OperationalDropDown,
            SicCode,
            StartUpDate,
            ShutdownDate,
            NaicsCode,
            AirProgramCodes,
            AirProgramClassificationsGroupbox,
            NonattainmentStatuses,
            FacilityDescription,
            RmpId,
            OwnershipGroupBox,
            Comments,
            SaveChangesButton,
            CancelEditButton
        }
        PreventControls(editableControls)

        FacilityHistoryDataGridView.Enabled = True
    End Sub

    Private Sub CancelEditing()
        DisableEditing()
        DisplayCurrentRow()
    End Sub

    Private Sub EditData_CheckedChanged(sender As Object, e As EventArgs) Handles EditData.CheckedChanged
        If EditData.Checked Then
            EnableEditing()
        Else
            CancelEditing()
        End If
    End Sub

    Private Sub CancelEditButton_Click(sender As Object, e As EventArgs) Handles CancelEditButton.Click
        If EditData.Checked Then
            EditData.Checked = False
        End If
    End Sub

    Private Sub SaveChangesButton_Click(sender As Object, e As EventArgs) Handles SaveChangesButton.Click
        SaveEditedData()
    End Sub

    Private Sub IAIPEditHeaderData_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape AndAlso EditData.Checked Then
            EditData.Checked = False
        End If
    End Sub

    Private Sub OperationalDropDown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OperationalDropDown.SelectedIndexChanged
        If EditData.Checked Then
            Dim nonShutdownControls As Control() = {
                ClassificationDropDown,
                SicCode,
                StartUpDate,
                NaicsCode,
                AirProgramCodes,
                AirProgramClassificationsGroupbox,
                NonattainmentStatuses,
                FacilityDescription,
                RmpId,
                OwnershipGroupBox
            }
            If UserIsTryingToCloseFacility() Then
                PreventControls(nonShutdownControls)
                ShutdownDate.Checked = True
                ShutdownDate.Enabled = True
                ModifiedDescDisplay.Text = "When changing operating status to Closed/Dismantled, " &
                "no other data can be modified. Make any other required changes first. " &
                "Please enter a final permit revocation date."
                PermitRevocationDateLabel.BackColor = Color.Yellow
                ModifiedDescDisplay.BackColor = Color.Yellow
            Else
                ResetControlHighlights()
                AllowControls(nonShutdownControls)
                ShutdownDate.Checked = False
                ShutdownDate.Enabled = False
                ModifiedDescDisplay.Text = "Editing current facility data."
            End If
        End If
    End Sub

    Private Function UserIsTryingToCloseFacility() As Boolean
        Return CType(OperationalDropDown.SelectedValue, FacilityOperationalStatus) = FacilityOperationalStatus.X AndAlso
            CurrentFacilityHeaderData.OperationalStatus <> FacilityOperationalStatus.X
    End Function

#End Region

#Region " Item selection and display "

    Private Sub FacilityHistoryDataGridView_CurrentCellChanged(sender As Object, e As EventArgs)
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

    Private Sub DisplayFacilityData(row As DataRow)
        Dim displayedFacility As FacilityHeaderData = New FacilityHeaderData(AirsNumber)
        DAL.FillFacilityHeaderDataFromDataRow(row, displayedFacility)

        DisplayFacilityData(displayedFacility)
    End Sub

    Private Sub DisplayFacilityData(displayedFacility As FacilityHeaderData)
        With displayedFacility

            ClassificationDropDown.SelectedValue = .Classification
            OperationalDropDown.SelectedValue = .OperationalStatus
            SicCode.Text = .SicCode
            If .StartupDate Is Nothing Then
                StartUpDate.Checked = False
            Else
                StartUpDate.Checked = True
                StartUpDate.Value = .StartupDate.Value
            End If
            If .ShutdownDate Is Nothing Then
                ShutdownDate.Checked = False
            Else
                ShutdownDate.Checked = True
                ShutdownDate.Value = .ShutdownDate.Value
            End If
            NaicsCode.Text = .Naics

            ApcAcid.Checked = Convert.ToBoolean(.AirPrograms And AirPrograms.AcidPrecipitation)
            ApcCfc.Checked = CBool(.AirPrograms And AirPrograms.CfcTracking)
            ApcFederalSip.Checked = CBool(.AirPrograms And AirPrograms.FederalSIP)
            ApcFesop.Checked = CBool(.AirPrograms And AirPrograms.FESOP)
            ApcMact.Checked = CBool(.AirPrograms And AirPrograms.MACT)
            ApcNativeAmerican.Checked = CBool(.AirPrograms And AirPrograms.NativeAmerican)
            ApcNeshap.Checked = CBool(.AirPrograms And AirPrograms.NESHAP)
            ApcNonfederalSip.Checked = CBool(.AirPrograms And AirPrograms.NonFederalSIP)
            ApcNsps.Checked = CBool(.AirPrograms And AirPrograms.NSPS)
            ApcNsr.Checked = CBool(.AirPrograms And AirPrograms.NSR)
            ApcPsd.Checked = CBool(.AirPrograms And AirPrograms.PSD)
            ApcRmp.Checked = CBool(.AirPrograms And AirPrograms.RMP)
            ApcSip.Checked = CBool(.AirPrograms And AirPrograms.SIP)
            ApcTitleV.Checked = CBool(.AirPrograms And AirPrograms.TitleV)

            NsrMajor.Checked = CBool(.AirProgramClassifications And AirProgramClassifications.NsrMajor)
            HapMajor.Checked = CBool(.AirProgramClassifications And AirProgramClassifications.HapMajor)

            ' Currently we are only tracking federally-owned facilities. Eventually 
            ' this could be expanded to use a drop-down with all ownership types.
            FederallyOwned.Checked = CBool(.OwnershipTypeCode = FacilityHeaderData.FederallyOwnedTypeCode)

            OneHourOzoneDropDown.SelectedValue = .OneHourOzoneNonAttainment
            EightHourOzoneDropDown.SelectedValue = .EightHourOzoneNonAttainment
            PmFineDropDown.SelectedValue = .PMFineNonAttainmentState

            FacilityDescription.Text = .FacilityDescription
            RmpId.Text = .RmpId
            Comments.Text = .HeaderUpdateComment

            If EditData.Checked Then
                ModifiedDescDisplay.Text = "Editing current facility data"
            Else
                ModifiedDescDisplay.Text = "Modification by " & .WhoModified & " on " & String.Format(DateStringFormat, .DateDataModified)
                If .WhereModified <> HeaderDataModificationLocation.Unspecified Then
                    ModifiedDescDisplay.Text = ModifiedDescDisplay.Text & " from " & .WhereModified.GetDescription
                End If
                ModifiedDescDisplay.Text = ModifiedDescDisplay.Text & "."
            End If

        End With
    End Sub

#End Region

#Region " Save Data "

    Private Function BoxUpFacilityFromForm() As FacilityHeaderData
        Dim facilityHeaderData As New FacilityHeaderData(AirsNumber)

        With facilityHeaderData
            .Classification = CType(ClassificationDropDown.SelectedValue, FacilityClassification)
            .OperationalStatus = CType(OperationalDropDown.SelectedValue, FacilityOperationalStatus)
            .SicCode = RealStringOrNothing(SicCode.Text)
            .Naics = RealStringOrNothing(NaicsCode.Text)

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

            .AirPrograms = AirPrograms.None
            If ApcAcid.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.AcidPrecipitation
            If ApcCfc.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.CfcTracking
            If ApcFederalSip.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.FederalSIP
            If ApcFesop.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.FESOP
            If ApcMact.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.MACT
            If ApcNativeAmerican.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.NativeAmerican
            If ApcNeshap.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.NESHAP
            If ApcNonfederalSip.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.NonFederalSIP
            If ApcNsps.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.NSPS
            If ApcNsr.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.NSR
            If ApcPsd.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.PSD
            If ApcRmp.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.RMP
            If ApcSip.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.SIP
            If ApcTitleV.Checked Then .AirPrograms = .AirPrograms Or AirPrograms.TitleV

            .AirProgramClassifications = AirProgramClassifications.None
            If NsrMajor.Checked Then .AirProgramClassifications = .AirProgramClassifications Or AirProgramClassifications.NsrMajor
            If HapMajor.Checked Then .AirProgramClassifications = .AirProgramClassifications Or AirProgramClassifications.HapMajor

            ' Currently we are only tracking federally-owned facilities, represented by this OwnershipTypeCode
            .OwnershipTypeCode = If(FederallyOwned.Checked, FacilityHeaderData.FederallyOwnedTypeCode, Nothing)

            .OneHourOzoneNonAttainment = CType(OneHourOzoneDropDown.SelectedValue, OneHourOzoneNonattainmentStatus)
            .EightHourOzoneNonAttainment = CType(EightHourOzoneDropDown.SelectedValue, EightHourOzoneNonattainmentStatus)
            .PMFineNonAttainmentState = CType(PmFineDropDown.SelectedValue, PMFineNonattainmentStatus)

            .FacilityDescription = FacilityDescription.Text
            .RmpId = RmpId.Text
            .HeaderUpdateComment = Comments.Text
        End With

        Return facilityHeaderData
    End Function

    Private Function PreSaveCheck(editedFacility As FacilityHeaderData) As Boolean

        ' See if facility is being shut down & check permissions
        If UserIsTryingToCloseFacility() Then
            If Not CurrentUser.HasPermission(UserCan.ShutDownFacility) Then
                MessageBox.Show("You do not have permissions to shut down a facility. Please contact your manager.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If Not ConfirmFacilityShutdown() Then
                Return False
            End If
        End If

        ' Compare edited data to current data
        If Not FacilityHeaderDataDiffers(editedFacility, CurrentFacilityHeaderData) Then
            MessageBox.Show("No data has been changed. Nothing saved.",
                                "Nothing Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Validate fields 
        Dim invalidControls As New List(Of Control)
        If Not ValidateAllFields(editedFacility, invalidControls) Then
            HighlightControls(invalidControls)
            MessageBox.Show("Some data is not valid. Double-check your entries." & vbNewLine & vbNewLine & "Nothing saved.",
                            "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Shared Function FacilityHeaderDataDiffers(facility1 As FacilityHeaderData, facility2 As FacilityHeaderData) As Boolean
        If facility1.AirProgramClassificationsCode <> facility2.AirProgramClassificationsCode Then Return True
        If facility1.AirProgramsCode <> facility2.AirProgramsCode Then Return True
        If facility1.ClassificationCode <> facility2.ClassificationCode Then Return True
        If facility1.FacilityDescription <> facility2.FacilityDescription Then Return True
        If facility1.Naics <> facility2.Naics Then Return True
        If facility1.NonattainmentStatusesCode <> facility2.NonattainmentStatusesCode Then Return True
        If facility1.OperationalStatusCode <> facility2.OperationalStatusCode Then Return True
        If facility1.RmpId <> facility2.RmpId Then Return True
        If facility1.OwnershipTypeCode <> facility2.OwnershipTypeCode Then Return True
        If Not Nullable.Equals(facility1.ShutdownDate, facility2.ShutdownDate) Then Return True
        If facility1.SicCode <> facility2.SicCode Then Return True
        If Not Nullable.Equals(facility1.StartupDate, facility2.StartupDate) Then Return True

        Return False
    End Function

    Private Shared Function ConfirmFacilityShutdown() As Boolean
        Return MessageBox.Show("Are you sure you want to mark this facility as closed/dismantled? " &
                               "This will revoke all existing permits.",
                               "Warning", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes
    End Function

    Private Function ValidateAllFields(editedFacility As FacilityHeaderData, <Runtime.InteropServices.Out()> ByRef invalidControls As List(Of Control)) As Boolean
        Dim valid As Boolean = True
        invalidControls = New List(Of Control)

        If String.IsNullOrEmpty(Comments.Text) Then
            MessageBox.Show("Since this is a direct change to the data, please add a useful comment so future users " &
                            "will know the reason for the change.",
                            "Missing Comment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(CommentsLabel)
        End If

        If editedFacility.OperationalStatus = FacilityOperationalStatus.X _
        AndAlso CurrentFacilityHeaderData.OperationalStatus <> FacilityOperationalStatus.X _
        AndAlso editedFacility.ShutdownDate Is Nothing Then
            MessageBox.Show("You have marked the facility as closed. Please enter the date the final permit was revoked.",
                            "Missing Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(PermitRevocationDateLabel)
        End If

        If editedFacility.ShutdownDate IsNot Nothing _
            AndAlso editedFacility.OperationalStatus <> FacilityOperationalStatus.X _
            AndAlso editedFacility.ShutdownDate Is Nothing Then
            MessageBox.Show("A permit revocation date is entered, but the facility is not marked as closed. Please reconcile this.",
                            "Missing Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(PermitRevocationDateLabel)
        End If

        If CType(ClassificationDropDown.SelectedValue, FacilityClassification) = FacilityClassification.Unspecified Then
            valid = False
            invalidControls.Add(ClassificationLabel)
        End If

        If CType(OperationalDropDown.SelectedValue, FacilityClassification) = FacilityOperationalStatus.U Then
            valid = False
            invalidControls.Add(OperationalStatusLabel)
        End If

        If Not DAL.SicCodeIsValid(SicCode.Text) Then
            MessageBox.Show("Please enter a valid SIC code.",
                            "Invalid SIC", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(SicCodeLabel)
        End If

        If Not DAL.NaicsCodeIsValid(NaicsCode.Text) Then
            MessageBox.Show("Please enter a valid NAICS code.",
                            "Invalid NAICS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            valid = False
            invalidControls.Add(NaicsCodeLabel)
        End If

        If String.IsNullOrEmpty(FacilityDescription.Text) Then
            valid = False
            invalidControls.Add(FacilityDescriptionLabel)
        End If

        If RmpId.Text <> "____-____-____" AndAlso Not FacilityHeaderData.IsValidRmpId(RmpId.Text) Then
            valid = False
            invalidControls.Add(RmpIdLabel)
        End If

        Return valid
    End Function

    Private Sub ResetControlHighlights()
        Dim controlsToReset As New List(Of Control)(New Control() {
          ClassificationLabel,
          OperationalStatusLabel,
          SicCodeLabel,
          NaicsCodeLabel,
          CommentsLabel,
          FacilityDescriptionLabel,
          PermitRevocationDateLabel,
          ModifiedDescDisplay,
          RmpIdLabel
        })

        For Each c As Control In controlsToReset
            c.BackColor = SystemColors.Control
        Next
    End Sub

    Private Sub SaveEditedData()
        ResetControlHighlights()

        Dim editedFacility As FacilityHeaderData = BoxUpFacilityFromForm()

        If PreSaveCheck(editedFacility) Then

            Dim result As Boolean

            ' Save edited data
            If UserIsTryingToCloseFacility() Then
                result = DAL.ShutDownFacility(editedFacility.AirsNumber,
                                              editedFacility.ShutdownDate.Value,
                                              editedFacility.HeaderUpdateComment,
                                              HeaderDataModificationLocation.HeaderDataEditor)
            Else
                result = DAL.SaveFacilityHeaderData(editedFacility,
                                                    HeaderDataModificationLocation.HeaderDataEditor)
            End If

            If result Then
                ' If successful, report back to Facility Summary 
                SomethingWasSaved = True

                ' replace local variable with current facility data
                CurrentFacilityHeaderData = editedFacility

                ' Add to gridview
                Dim currentData As DataRow = DAL.GetFacilityHeaderDataAsDataRow(AirsNumber)
                currentData.Table.Columns("STRKEY").ReadOnly = False
                currentData.Item("STRKEY") = Convert.ToInt32(FacilityHeaderDataHistory.AsEnumerable().Max(Function(row) Convert.ToInt32(row("STRKEY")))) + 1

                FacilityHeaderDataHistory.ImportRow(currentData)
                FacilityHistoryDataGridView.Sort(FacilityHistoryDataGridView.Columns("STRKEY"), ListSortDirection.Descending)
                FacilityHistoryDataGridView.CurrentCell = FacilityHistoryDataGridView.Rows(0).Cells(2)

                EditData.Checked = False
            Else
                MessageBox.Show("There was an error saving the new data. Please try again.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub

#End Region

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If FacilityHeaderDataHistory IsNot Nothing Then FacilityHeaderDataHistory.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class