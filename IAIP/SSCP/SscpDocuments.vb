Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports Iaip.Apb.Sscp
Imports Iaip.DAL.Sscp
Imports Iaip.DAL.DocumentData

Public Class SscpDocuments

#Region "Properties"
    Private Documents As List(Of EnforcementDocument)
    Private NewDocumentPath As String
    Private enforcementInfo As EnforcementInfo
    Private enforcementNumber As String
    Private _message As IaipMessage
    Private Property Message As IaipMessage
        Get
            Return _message
        End Get
        Set(value As IaipMessage)
            If value Is Nothing AndAlso Message IsNot Nothing Then
                Message.Clear()
            End If
            _message = value
            If value IsNot Nothing Then
                If Message.WarningLevel = IaipMessage.WarningLevels.ErrorReport Then
                    Message.Display(lblMessage, EP)
                Else
                    Message.Display(lblMessage)
                End If
            End If
        End Set
    End Property

#End Region

#Region "Page Load"

    Private Sub SscpDocuments_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadDocumentTypes()
        ClearEverything()
        If enforcementInfo IsNot Nothing Then ShowEnforcement()
    End Sub

    Private Sub LoadDocumentTypes()
        ' Get list of various document types and bind that list to the comboboxes
        Dim documentTypes As Dictionary(Of Integer, String) = GetEnforcementDocumentTypesDict()

        If documentTypes.Count > 0 Then
            ddlNewDocumentType.BindToDictionary(documentTypes)
            ddlDocumentType.BindToDictionary(documentTypes)

            ' When a doc type is selected, display whether it already exists
            ' This has to be added after the list is bound (above) or it will trigger
            '   as each new list item is added to the list.
            ' (Only do this with the "Add New" panel, but not the "Update" panel)
            AddHandler ddlNewDocumentType.SelectedIndexChanged, AddressOf ddlDocumentType_SelectedIndexChanged
        Else
            Me.Enabled = False
            MsgBox("No document types have been added. Please contact an administrator.", MsgBoxStyle.OkOnly, "Error")
            Me.Dispose()
        End If
    End Sub

#End Region

#Region "Enforcement fetcher"

    Private Sub btnFindEnforcement_Click(sender As Object, e As EventArgs) Handles btnFindEnforcement.Click
        ClearEverything()
        FindEnforcement()
    End Sub

    Private Sub FindEnforcement()
        If txtFindEnforcement.Text = "" Then Return

        enforcementInfo = Nothing
        enforcementNumber = txtFindEnforcement.Text
        If Integer.TryParse(enforcementNumber, Nothing) Then
            If EnforcementExists(enforcementNumber) Then
                enforcementInfo = GetEnforcementInfo(enforcementNumber)
                ShowEnforcement()
            Else
                Me.Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.InvalidEnforcementNumber), IaipMessage.WarningLevels.ErrorReport)
            End If
        End If
    End Sub

    Private Sub ShowEnforcement()
        DisplayEnforcementInfo()
        LoadDocuments()
        EnableNewDocument()
    End Sub

    Private Sub DisplayEnforcementInfo()
        If enforcementInfo IsNot Nothing Then
            Dim infoDisplay As New StringBuilder

            If enforcementInfo.IsDeleted Then
                infoDisplay.Append("DELETED ")
            End If

            infoDisplay.AppendFormat("Enforcement #{0}", enforcementInfo.EnforcementNumber.ToString()).AppendLine()
            infoDisplay.AppendFormat("AIRS # {0}: {1}", enforcementInfo.Facility.AirsNumber.FormattedString, enforcementInfo.Facility.FacilityName).AppendLine()

            lblEnforcementInfo.Text = infoDisplay.ToString

            If enforcementInfo.IsDeleted Then
                lblEnforcementInfo.BackColor = IaipColors.WarningBackColor
                lblEnforcementInfo.ForeColor = IaipColors.WarningForeColor
            End If

            infoDisplay.Length = 0
            infoDisplay.AppendLine(enforcementInfo.Facility.FacilityLocation.Address.ToString)
            infoDisplay.AppendFormat("Responsible staff: {0}", enforcementInfo.StaffResponsible).AppendLine()

            If enforcementInfo.DiscoveryDate IsNot Nothing Then
                infoDisplay.AppendFormat("{0}; Discovery Date: {1:dd-MMM-yyyy}", enforcementInfo.EnforcementTypeCode, enforcementInfo.DiscoveryDate).AppendLine()
            Else
                infoDisplay.AppendFormat("{0}", enforcementInfo.EnforcementTypeCode).AppendLine()
            End If

            lblEnforcementInfo2.Text = infoDisplay.ToString
        Else
            ClearEnforcementInfo()
        End If
    End Sub

#End Region

#Region "Display documents"

    Private Sub LoadDocuments()
        DisableDocument()
        dgvDocumentList.DataSource = Nothing
        Documents = GetEnforcementDocumentsAsList(enforcementInfo.EnforcementNumber)
        If Documents.Count > 0 Then
            With dgvDocumentList
                .DataSource = New BindingSource(Documents, Nothing)
                .Enabled = True
                .ClearSelection()
            End With
        End If
    End Sub

    Private Sub FormatDocumentList()
        With dgvDocumentList
            .Columns("EnforcementNumber").Visible = False
            .Columns("BinaryFileId").Visible = False
            With .Columns("Comment")
                .HeaderText = "Description"
                .DisplayIndex = 4
            End With
            .Columns("DocumentId").Visible = False
            With .Columns("DocumentType")
                .HeaderText = "Document Type"
                .DisplayIndex = 0
            End With
            .Columns("DocumentTypeId").Visible = False
            .Columns("FileExtension").Visible = False
            With .Columns("FileName")
                .HeaderText = "File Name"
                .DisplayIndex = 1
            End With
            With .Columns("FileSize")
                .HeaderText = "File Size"
                .DefaultCellStyle.Format = "fs:1"
                .DisplayIndex = 3
                .DefaultCellStyle.FormatProvider = New FileSizeFormatProvider
            End With
            With .Columns("UploadDate")
                .HeaderText = "Uploaded On"
                .DefaultCellStyle.Format = DateFormat
                .DisplayIndex = 2
            End With
        End With
    End Sub

    Private Sub dataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDocumentList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvDocumentList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDocumentList.DataBindingComplete
        FormatDocumentList()
        CType(sender, DataGridView).SanelyResizeColumns()
        CType(sender, DataGridView).ClearSelection()
    End Sub

#End Region

#Region "Enable/Disable Form Regions"

#Region "Document (update/delete/download)"

    Private Sub EnableDocument()
        EnableOrDisableDocument(EnableOrDisable.Enable)
    End Sub

    Private Sub DisableDocument()
        EnableOrDisableDocument(EnableOrDisable.Disable)
    End Sub

    Private Sub EnableOrDisableDocument(enable As EnableOrDisable)
        With pnlDocument
            .Enabled = enable
            .Visible = enable
        End With
        If enable Then
            txtDocumentDescription.Text = dgvDocumentList.CurrentRow.Cells("Comment").Value
            ddlDocumentType.SelectedValue = dgvDocumentList.CurrentRow.Cells("DocumentTypeId").Value
            lblDocumentName.Text = dgvDocumentList.CurrentRow.Cells("FileName").Value
        End If
    End Sub

#End Region

#Region "New Document"

    Private Sub EnableNewDocument()
        EnableOrDisableNewDocument(EnableOrDisable.Enable)
    End Sub

    Private Sub DisableNewDocument()
        EnableOrDisableNewDocument(EnableOrDisable.Disable)
    End Sub

    Private Sub EnableOrDisableNewDocument(enable As EnableOrDisable)
        With pnlNewDocument
            .Enabled = enable
        End With
        If Not enable Then
            With pnlNewDocumentDetails
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

#End Region

#Region "New Document Details"

    Private Sub EnableNewDocumentDetails()
        EnableOrDisableNewDocumentDetails(EnableOrDisable.Enable)
    End Sub

    Private Sub DisableNewDocumentDetails()
        EnableOrDisableNewDocumentDetails(EnableOrDisable.Disable)
    End Sub

    Private Sub EnableOrDisableNewDocumentDetails(enable As EnableOrDisable)
        With pnlNewDocumentDetails
            .Enabled = enable
            .Visible = enable
        End With
        If Not enable Then
            txtNewDocumentDescription.Text = ""
        End If
    End Sub

#End Region

#End Region

#Region "Clear form sections"

    Private Sub ClearEverything()
        ClearEnforcementInfo()
        If Message IsNot Nothing Then Message.Clear()
        ClearDocumentList()
        ClearNewDocument()
        DisableNewDocument()
    End Sub

    Private Sub ClearEnforcementInfo()
        lblEnforcementInfo.Text = ""
        lblEnforcementInfo2.Text = ""
    End Sub

    Private Sub ClearNewDocument()
        NewDocumentPath = Nothing
        DisableNewDocumentDetails()
    End Sub

    Private Sub ClearDocumentList()
        With dgvDocumentList
            .DataSource = Nothing
            .Enabled = False
        End With
        DisableDocument()
    End Sub

#End Region

#Region "New document"

#Region "Select document"

    Private Sub btnNewDocumentSelect_Click(sender As Object, e As EventArgs) Handles btnNewDocumentSelect.Click
        Using openFileDialog As New OpenFileDialog With {
            .InitialDirectory = GetUserSetting(UserSetting.FileUploadLocation),
            .Filter = String.Join("|", FileOpenFilters.ToArray)
        }
            If openFileDialog.ShowDialog() <> DialogResult.OK OrElse openFileDialog.FileName = "" Then
                Return
            End If

            ClearNewDocument()

            Dim fileInfo As New FileInfo(openFileDialog.FileName)

            If Not fileInfo.Exists Then
                Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.FileNotFound), IaipMessage.WarningLevels.ErrorReport)
                Return
            End If

            If fileInfo.Length >= Document.MaxFileSize Then
                Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.FileTooLarge), IaipMessage.WarningLevels.ErrorReport)
                Return
            End If

            If fileInfo.Length = 0 Then
                Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.FileEmpty), IaipMessage.WarningLevels.ErrorReport)
                Return
            End If

            NewDocumentPath = openFileDialog.FileName
            EnableNewDocumentDetails()
            lblNewDocumentName.Text = openFileDialog.SafeFileName
            txtNewDocumentDescription.Focus()
        End Using
    End Sub

#End Region

#Region "New document actions"

    Private Sub btnNewDocumentCancel_Click(sender As Object, e As EventArgs) Handles btnNewDocumentCancel.Click
        ClearNewDocument()
    End Sub

    Private Sub btnNewDocumentUpload_Click(sender As Object, e As EventArgs) Handles btnNewDocumentUpload.Click
        If Message IsNot Nothing Then Message.Clear()
        Dim fileInfo As New FileInfo(NewDocumentPath)

        ' Check if file exists
        If Not fileInfo.Exists Then
            Me.Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.FileNotFound), IaipMessage.WarningLevels.ErrorReport)
            Return
        End If

        Dim m As String

        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            m = String.Format(GetDocumentMessage(DocumentMessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text)
            MessageBox.Show(m, "Document Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Create Document object
        Dim documentToUpload As New EnforcementDocument
        With documentToUpload
            .EnforcementNumber = enforcementInfo.EnforcementNumber
            .Comment = txtNewDocumentDescription.Text
            .DocumentTypeId = ddlNewDocumentType.SelectedValue
            .DocumentType = ddlNewDocumentType.Text
            .FileName = fileInfo.Name
            .FileSize = fileInfo.Length
            .UploadDate = Date.Now
        End With

        m = String.Format(GetDocumentMessage(DocumentMessageType.UploadingFile), documentToUpload.FileName)
        Me.Message = New IaipMessage(m)

        Dim result As Boolean = UploadEnforcementDocument(documentToUpload, fileInfo.FullName, Me)

        If result Then
            m = String.Format(GetDocumentMessage(DocumentMessageType.UploadSuccess), documentToUpload.FileName)
            Me.Message = New IaipMessage(m)
            SaveUserSetting(UserSetting.FileUploadLocation, fileInfo.DirectoryName)
        Else
            Me.Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.UploadFailure), IaipMessage.WarningLevels.ErrorReport)
        End If

        ClearNewDocument()
        LoadDocuments()
    End Sub

#End Region

#Region "Document type validation"

    Private Function DocumentTypeAlreadyExists() As Boolean
        Dim index As Integer = Documents.FindIndex(
            Function(doc) _
                doc.DocumentTypeId = ddlNewDocumentType.SelectedValue
        )
        Return index <> -1
    End Function

    Private Sub ddlDocumentType_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Message IsNot Nothing Then Message.Clear()
        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text), IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

#End Region

#End Region

#Region "Document update/download/delete"

    Private Sub dgvDocumentList_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDocumentList.SelectionChanged
        If dgvDocumentList.SelectedRows.Count = 1 Then
            EnableDocument()
        Else
            DisableDocument()
        End If
    End Sub

    Private Sub btnDocumentDelete_Click(sender As Object, e As EventArgs) Handles btnDocumentDelete.Click
        Dim m As String = String.Format(GetDocumentMessage(DocumentMessageType.ConfirmDelete), lblDocumentName.Text)
        Dim response As DialogResult =
            MessageBox.Show(m, "Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)

        If response = DialogResult.Yes Then
            Dim deleted As Boolean = DeleteDocument(dgvDocumentList.CurrentRow.Cells("BinaryFileId").Value)

            If deleted Then
                m = String.Format(GetDocumentMessage(DocumentMessageType.DeleteSuccess), lblDocumentName.Text)
                Me.Message = New IaipMessage(m)
                LoadDocuments()
            Else
                Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.DeleteFailure), lblDocumentName), IaipMessage.WarningLevels.ErrorReport)
            End If
        End If
    End Sub

    Private Sub btnDocumentDownload_Click(sender As Object, e As EventArgs) Handles btnDocumentDownload.Click
        If Message IsNot Nothing Then Message.Clear()

        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        Me.Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.DownloadingFile))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded OrElse canceled Then
            If Message IsNot Nothing Then Message.Clear()
        Else
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.DownloadFailure), lblDocumentName), IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Sub btnDocumentUpdate_Click(sender As Object, e As EventArgs) Handles btnDocumentUpdate.Click
        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        doc.Comment = txtDocumentDescription.Text
        doc.DocumentTypeId = ddlDocumentType.SelectedValue
        Dim updated As Boolean = UpdateEnforcementDocument(doc, Me)
        If updated Then
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.UpdateSuccess), doc.FileName))
            LoadDocuments()
        Else
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.UpdateFailure), lblDocumentName), IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Function EnforcementDocumentFromFileListRow(row As DataGridViewRow) As EnforcementDocument
        Dim doc As New EnforcementDocument
        With doc
            .EnforcementNumber = row.Cells("EnforcementNumber").Value
            .BinaryFileId = row.Cells("BinaryFileId").Value
            .Comment = row.Cells("Comment").Value
            .DocumentId = row.Cells("DocumentId").Value
            .DocumentType = row.Cells("DocumentType").Value
            .DocumentTypeId = row.Cells("DocumentTypeId").Value
            .FileName = row.Cells("FileName").Value
            .FileSize = row.Cells("FileSize").Value
            .UploadDate = DateTime.Parse(row.Cells("UploadDate").Value)
        End With
        Return doc
    End Function

#End Region

#Region "Change Accept Button"

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
    Handles txtFindEnforcement.Leave, txtDocumentDescription.Leave, txtNewDocumentDescription.Leave, ddlNewDocumentType.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtFindEnforcement_Enter(sender As Object, e As EventArgs) _
    Handles txtFindEnforcement.Enter
        Me.AcceptButton = btnFindEnforcement
    End Sub

    Private Sub txtNewDocument_Enter(sender As Object, e As EventArgs) _
    Handles txtNewDocumentDescription.Enter
        Me.AcceptButton = btnNewDocumentUpload
    End Sub

    Private Sub FileProperties_Enter(sender As Object, e As EventArgs) _
    Handles txtDocumentDescription.Enter, ddlDocumentType.Enter
        Me.AcceptButton = btnDocumentUpdate
    End Sub

#End Region

End Class