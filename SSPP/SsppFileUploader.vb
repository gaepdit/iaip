Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports JohnGaltProject.Apb.SSPP
Imports JohnGaltProject.DAL.Documents
Imports JohnGaltProject.DAL.SSPP
Imports Oracle.DataAccess.Types


Public Class SsppFileUploader

#Region "Properties"
    Private ExistingFiles As List(Of PermitDocument)
    Private NewFileToUpload As String
    Private DocumentTypes As Dictionary(Of Integer, String)

    Public Property AppInfo() As ApplicationInfo
        Get
            Return _appInfo
        End Get
        Set(ByVal value As ApplicationInfo)
            _appInfo = value
        End Set
    End Property
    Private _appInfo As ApplicationInfo
#End Region

#Region "Page Load"

    Private Sub SsppFileUploader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDocumentTypes()
        ClearForm()
        If AppInfo IsNot Nothing Then ShowApplication()
    End Sub

    Private Sub LoadDocumentTypes()
        ' Get list of various document types and bind that list to the comboboxes
        DocumentTypes = DAL.GetPermitDocumentTypes

        If DocumentTypes.Count > 0 Then
            With ddlNewDocumentType
                .DataSource = New BindingSource(DocumentTypes, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
            With ddlUpdateDocumentType
                .DataSource = New BindingSource(DocumentTypes, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With

            ' When a doc type is selected, display whether it already exists
            ' This has to be added after the list is bound (above) or it will trigger
            '   as each new list item is added to the list.
            ' (Only do this with the "Add New" panel, but not the "Update" panel)
            AddHandler ddlNewDocumentType.SelectedIndexChanged, AddressOf ddlDocumentType_SelectedIndexChanged
        Else
            btnFindApplication.Enabled = False
            DisableFileUpdate()
            DisableFileUploader()
        End If
    End Sub

#End Region

#Region "Messages"

    Private Enum MessageType As Byte
        InvalidApplicationNumber
        FileNotFound
        DocumentTypeAlreadyExists
        UploadSuccess
        UploadFailure
        FileTooLarge
        FileEmpty
        DeleteSuccess
        DeleteFailure
        ConfirmDelete
        DownloadFailure
        UpdateSuccess
        UpdateFailure
        DownloadingFile
        UploadingFile
    End Enum

    Private Function GetMessageList() As Specialized.ListDictionary
        Dim messageList As New Specialized.ListDictionary
        messageList.Add(MessageType.InvalidApplicationNumber, "Error: The application number is not in the system.")
        messageList.Add(MessageType.FileNotFound, "Error: The file cannot be found.")
        messageList.Add(MessageType.DocumentTypeAlreadyExists, "A ""{0}"" has already been uploaded for this application.")
        messageList.Add(MessageType.UploadSuccess, "Success: The file ""{0}""" & vbNewLine & "has been uploaded.")
        messageList.Add(MessageType.UploadFailure, "Error: There was an error uploading the file. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.FileTooLarge, "The selected file is too large. " & vbNewLine & "Maximum file size is " & Math.Round(OracleBlob.MaxSize / (1024 ^ 3), 2) & "GB.")
        messageList.Add(MessageType.FileEmpty, "The selected file is empty.")
        messageList.Add(MessageType.DeleteFailure, "Error: The selected file was not deleted. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.DeleteSuccess, "Success: The file ""{0}"" was deleted.")
        messageList.Add(MessageType.ConfirmDelete, "Are you sure you want to delete the file ""{0}""?")
        messageList.Add(MessageType.DownloadFailure, "Error: There was an error saving the file. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.UpdateFailure, "Error: The selected file was not updated. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.UpdateSuccess, "Success: The file ""{0}"" was updated.")
        messageList.Add(MessageType.DownloadingFile, "Downloading {0}. Please wait.")
        messageList.Add(MessageType.UploadingFile, "Uploading {0}. Please stand by.")

        Return messageList
    End Function
    Private MessageList As Specialized.ListDictionary = GetMessageList()
    Private Function GetMessage(ByVal key As MessageType) As String
        Return MessageList(key)
    End Function

#End Region

#Region "Application loader"

    Private Sub btnFindApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindApplication.Click
        ClearForm()
        FindApplication()
    End Sub

    Private Sub FindApplication()
        AppInfo = Nothing
        If DAL.SSPP.ApplicationExists(txtApplicationNumber.Text) Then
            AppInfo = GetApplicationInfo(txtApplicationNumber.Text)
            ShowApplication()
        Else
            DisplayMessage(lblMessage, GetMessage(MessageType.InvalidApplicationNumber), True, EP, lblMessage)
        End If
    End Sub

    Private Sub ShowApplication()
        DisplayApplicationInfo()
        LoadExistingDocuments()
        EnableFileUploader()
    End Sub

    Private Sub DisplayApplicationInfo()
        If AppInfo IsNot Nothing Then
            Dim infoDisplay As New StringBuilder

            Dim a As Integer = CInt(AppInfo.Facility.AirsNumber)
            infoDisplay.AppendFormat("AIRS # {0:000-00000}", a).AppendLine()
            infoDisplay.AppendLine(AppInfo.Facility.Name)
            infoDisplay.AppendLine(AppInfo.Facility.FacilityLocation.Address.ToString)
            lblAppInfo.Text = infoDisplay.ToString

            infoDisplay.Length = 0
            infoDisplay.AppendFormat("Responsible staff: {0}", AppInfo.StaffResponsible).AppendLine()
            infoDisplay.AppendFormat("{0}; {1}", AppInfo.ApplicationType, AppInfo.PermitType).AppendLine()
            infoDisplay.AppendFormat("Issued: {0:dd-MMM-yyyy}", AppInfo.DateIssued)

            lblAppInfo2.Text = infoDisplay.ToString
        Else
            ClearAppInfo()
        End If
    End Sub

#End Region

#Region "Display files"

    Private Sub LoadExistingDocuments()
        DisableFileUpdate()
        dgvFileList.DataSource = Nothing
        ExistingFiles = GetPermitDocuments(AppInfo.ApplicationNumber)
        If ExistingFiles.Count > 0 Then
            With dgvFileList
                .DataSource = New BindingSource(ExistingFiles, Nothing)
                .Enabled = True
                .ClearSelection()
            End With
            FormatCurrentFileList()
        End If
    End Sub

    Private Sub FormatCurrentFileList()
        With dgvFileList
            .Columns("ApplicationNumber").Visible = False
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
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub dataGridView_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvFileList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

#End Region

#Region "Enable/Disable Form Areas"

    Private Sub EnableFileUpdate()
        EnableOrDisableFileUpdate(True)
    End Sub

    Private Sub DisableFileUpdate()
        EnableOrDisableFileUpdate(False)
    End Sub

    Private Sub EnableOrDisableFileUpdate(ByVal enable As Boolean)
        btnDeleteFile.Enabled = enable
        btnDownloadFile.Enabled = enable
        With txtUpdateDescription
            .Visible = enable
            .Text = If(enable, dgvFileList.CurrentRow.Cells("Comment").Value, "")
        End With
        With lblUpdateDescription
            .Visible = enable
        End With
        With ddlUpdateDocumentType
            .Enabled = enable
            .Visible = enable
            If enable Then .SelectedValue = dgvFileList.CurrentRow.Cells("DocumentTypeId").Value
        End With
        With btnUpdateFileDescription
            .Enabled = enable
            .Visible = enable
        End With
        With lblSelectedFileName
            .Visible = enable
            .Text = If(enable, dgvFileList.CurrentRow.Cells("FileName").Value, "")
        End With
    End Sub

    Private Sub EnableFileUploader()
        EnableOrDisableFileUploader(True)
    End Sub

    Private Sub DisableFileUploader()
        EnableOrDisableFileUploader(False)
    End Sub

    Private Sub EnableOrDisableFileUploader(ByVal enable As Boolean)
        ddlNewDocumentType.Enabled = enable
        btnChooseNewFile.Enabled = enable
    End Sub

#End Region

#Region "Clear form sections"

    Private Sub ClearForm()
        ClearAppInfo()
        ClearMessage(lblMessage, EP)
        ClearFileList()
        ClearFileUploader()
        DisableFileUploader()
    End Sub

    Private Sub ClearAppInfo()
        lblAppInfo.Text = ""
        lblAppInfo2.Text = ""
    End Sub

    Private Sub ClearFileUploader()
        With lblNewFileName
            .Visible = False
            .Text = ""
        End With
        lblNewDescription.Visible = False
        With txtNewDescription
            .Visible = False
            .Text = ""
        End With
        With btnNewFileCancel
            .Enabled = False
            .Visible = False
        End With
        With btnNewFileUpload
            .Enabled = False
            .Visible = False
        End With
    End Sub

    Private Sub ClearFileList()
        With dgvFileList
            .DataSource = Nothing
            .Enabled = False
        End With
        DisableFileUpdate()
    End Sub

#End Region

#Region "New file uploader"

    Private Sub btnChooseNewFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseNewFile.Click
        Dim openFileDialog As New OpenFileDialog With { _
            .InitialDirectory = GetUserSetting(UserSetting.PermitUploadLocation), _
            .Filter = String.Join("|", FileOpenFilters.ToArray) _
        }

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK _
        AndAlso openFileDialog.FileName <> "" Then
            NewFileToUpload = Nothing
            lblNewDescription.Visible = False
            txtNewDescription.Visible = False
            With btnNewFileCancel
                .Visible = False
                .Enabled = False
            End With
            With btnNewFileUpload
                .Visible = False
                .Enabled = False
            End With

            Dim fileInfo As New FileInfo(openFileDialog.FileName)
            If Not fileInfo.Exists Then
                DisplayMessage(lblMessage, GetMessage(MessageType.FileNotFound), True, EP, lblMessage)
            Else
                With lblNewFileName
                    .Text = openFileDialog.SafeFileName
                    .Visible = True
                End With

                If fileInfo.Length >= OracleBlob.MaxSize Then
                    DisplayMessage(lblMessage, GetMessage(MessageType.FileTooLarge), True, EP, lblMessage)
                Else
                    If fileInfo.Length = 0 Then
                        DisplayMessage(lblMessage, GetMessage(MessageType.FileEmpty), True, EP, lblMessage)
                    Else
                        NewFileToUpload = openFileDialog.FileName
                        lblNewDescription.Visible = True
                        txtNewDescription.Visible = True
                        With btnNewFileCancel
                            .Visible = True
                            .Enabled = True
                        End With
                        With btnNewFileUpload
                            .Visible = True
                            .Enabled = True
                        End With
                        txtNewDescription.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnNewFileCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFileCancel.Click
        ClearFileUploader()
    End Sub

    Private Sub btnNewFileUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFileUpload.Click
        ClearMessage(lblMessage, EP)

        Dim fileInfo As New FileInfo(NewFileToUpload)
        ' Check if file exists
        If Not fileInfo.Exists Then
            DisplayMessage(lblMessage, GetMessage(MessageType.FileNotFound), True, EP, lblMessage)
            Exit Sub
        End If

        Dim m As String

        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            m = String.Format(GetMessage(MessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text)
            m &= vbNewLine & "Would you like to continue? (Both files will be kept.)"
            Dim response As Windows.Forms.DialogResult = MessageBox.Show(m, "Replace File?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            If response = Windows.Forms.DialogResult.Cancel Then
                DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text))
                Exit Sub
            End If
        End If

        ' Create Document object
        Dim NewPermitDocument As New PermitDocument
        With NewPermitDocument
            .ApplicationNumber = AppInfo.ApplicationNumber
            .Comment = txtNewDescription.Text
            .DocumentTypeId = ddlNewDocumentType.SelectedValue
            .DocumentType = ddlNewDocumentType.Text
            .FileName = fileInfo.Name
            .FileSize = fileInfo.Length
            .UploadDate = Today
        End With

        m = String.Format(GetMessage(MessageType.UploadingFile), NewPermitDocument.FileName)
        DisplayMessage(lblMessage, m)

        Dim result As Boolean = UploadPermitDocument(NewPermitDocument, fileInfo.FullName, Me)

        If result Then
            m = String.Format(GetMessage(MessageType.UploadSuccess), NewPermitDocument.FileName)
            DisplayMessage(lblMessage, m)
            SaveUserSetting(UserSetting.PermitUploadLocation, fileInfo.DirectoryName)
        Else
            DisplayMessage(lblMessage, GetMessage(MessageType.UploadFailure), True, EP, lblMessage)
        End If

        ClearFileUploader()
        LoadExistingDocuments()

    End Sub

    Private Function DocumentTypeAlreadyExists() As Boolean
        Dim index As Integer = ExistingFiles.FindIndex( _
            Function(doc) _
                doc.DocumentTypeId = ddlNewDocumentType.SelectedValue _
        )
        Return If(index = -1, False, True)
    End Function

    Private Sub ddlDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearMessage(lblMessage)
        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text))
        End If
    End Sub

#End Region

#Region "Existing file Update/Download/Delete"

    Private Sub dgvFileList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvFileList.SelectionChanged
        If dgvFileList.SelectedRows.Count > 0 Then
            EnableFileUpdate()
        Else
            DisableFileUpdate()
        End If
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim m As String = String.Format(GetMessage(MessageType.ConfirmDelete), lblSelectedFileName.Text)
        Dim response As Windows.Forms.DialogResult = _
            MessageBox.Show(m, "Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)

        If response = Windows.Forms.DialogResult.Yes Then
            Dim deleted As Boolean = DeleteDocument(dgvFileList.CurrentRow.Cells("BinaryFileId").Value)

            If deleted Then
                m = String.Format(GetMessage(MessageType.DeleteSuccess), lblSelectedFileName.Text)
                DisplayMessage(lblMessage, m)
                LoadExistingDocuments()
            Else
                DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DeleteFailure), lblSelectedFileName), True, EP)
            End If
        End If
    End Sub

    Private Sub btnDownloadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadFile.Click
        ClearMessage(lblMessage, EP)

        Dim doc As PermitDocument = PermitDocumentFromFileListRow(dgvFileList.CurrentRow)
        DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DownloadingFile), doc.FileName))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded Or canceled Then
            ClearMessage(lblMessage, EP)
        Else
            DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DownloadFailure), lblSelectedFileName), True, EP, lblMessage)
        End If
    End Sub

    Private Sub btnUpdateFileDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFileDescription.Click
        Dim doc As PermitDocument = PermitDocumentFromFileListRow(dgvFileList.CurrentRow)
        doc.Comment = txtUpdateDescription.Text
        doc.DocumentTypeId = ddlUpdateDocumentType.SelectedValue
        Dim updated As Boolean = UpdatePermitDocument(doc, Me)
        If updated Then
            DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.UpdateSuccess), doc.FileName))
            LoadExistingDocuments()
        Else
            DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.UpdateFailure), lblSelectedFileName), True, EP)
        End If
    End Sub

    Private Function PermitDocumentFromFileListRow(ByVal row As DataGridViewRow) As PermitDocument
        Dim doc As New PermitDocument
        With doc
            .ApplicationNumber = row.Cells("ApplicationNumber").Value
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

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtApplicationNumber.Leave, txtNewDescription.Leave, txtUpdateDescription.Leave, ddlUpdateDocumentType.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtApplicationNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApplicationNumber.Enter
        Me.AcceptButton = btnFindApplication
    End Sub

    Private Sub txtNewDescription_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNewDescription.Enter
        Me.AcceptButton = btnNewFileUpload
    End Sub

    Private Sub FileProperties_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtUpdateDescription.Enter, ddlUpdateDocumentType.Enter
        Me.AcceptButton = btnUpdateFileDescription
    End Sub

#End Region

End Class
