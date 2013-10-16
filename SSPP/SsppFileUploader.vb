Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports JohnGaltProject.Apb.SSPP
Imports JohnGaltProject.DAL.Documents
Imports JohnGaltProject.DAL.SSPP
Imports Oracle.DataAccess.Types


Public Class SsppFileUploader

#Region "Properties"
    Private CurrentFiles As List(Of PermitDocument)
    Private NewFile As String
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
        DocumentTypes = DAL.GetPermitDocumentTypes()
        With ddlNewDocumentType
            .DataSource = New BindingSource(DocumentTypes, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With

        AddHandler ddlNewDocumentType.SelectedIndexChanged, AddressOf ddlDocumentType_SelectedIndexChanged

        With ddlUpdateDocumentType
            .DataSource = New BindingSource(DocumentTypes, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
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
    End Enum

    Private Function GetMessageList() As Specialized.ListDictionary
        Dim messageList As New Specialized.ListDictionary
        messageList.Add(MessageType.InvalidApplicationNumber, "Error: The application number is not in the system.")
        messageList.Add(MessageType.FileNotFound, "Error: The file cannot be found.")
        messageList.Add(MessageType.DocumentTypeAlreadyExists, "A ""{0}"" has already been uploaded for this application.")
        messageList.Add(MessageType.UploadSuccess, "Success: The file ""{0}""" & vbNewLine & "has been uploaded.")
        messageList.Add(MessageType.UploadFailure, "Error: There was an error uploading the file. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.FileTooLarge, "The selected file is too large. " & vbNewLine & "Maximum file size is 2GB.")
        messageList.Add(MessageType.FileEmpty, "The selected file is empty.")
        messageList.Add(MessageType.DeleteFailure, "Error: The selected file was not deleted. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.DeleteSuccess, "Success: The file ""{0}"" was deleted.")
        messageList.Add(MessageType.ConfirmDelete, "Are you sure you want to delete the file ""{0}""?")
        messageList.Add(MessageType.DownloadFailure, "Error: There was an error saving the file. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.UpdateFailure, "Error: The selected file was not updated. " & vbNewLine & "Please try again.")
        messageList.Add(MessageType.UpdateSuccess, "Success: The file ""{0}"" was updated.")
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
        ShowCurrentFiles()
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

    Private Sub ShowCurrentFiles()
        DisableFileProperties()
        CurrentFiles = GetPermitDocuments(AppInfo.ApplicationNumber)
        If CurrentFiles.Count > 0 Then
            With dgvFileList
                .DataSource = New BindingSource(CurrentFiles, Nothing)
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
                .DefaultCellStyle.Format = "N0"
                .DisplayIndex = 3
            End With
            With .Columns("UploadDate")
                .HeaderText = "Uploaded On"
                .DefaultCellStyle.Format = DateFormat
                .DisplayIndex = 2
            End With
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub EnableFileUploader()
        ddlNewDocumentType.Enabled = True
        btnChooseNewFile.Enabled = True
    End Sub

#End Region

#Region "Clear forms"
    Private Sub ClearForm()
        ClearAppInfo()
        ClearMessage(lblMessage, EP)
        ClearFileList()
        ClearFileUploader()
        DisableFileUploader()
    End Sub

    Private Sub DisableFileUploader()
        ddlNewDocumentType.Enabled = False
        btnChooseNewFile.Enabled = False
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
        DisableFileProperties()
    End Sub

#End Region

#Region "File uploader"

    Private Sub btnChooseNewFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseNewFile.Click
        Dim openFileDialog As New OpenFileDialog With { _
            .InitialDirectory = GetSetting(UserSetting.PermitUploadLocation), _
            .Filter = String.Join("|", OpenFileFilters.ToArray) _
        }

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK _
        AndAlso openFileDialog.FileName <> "" Then
            NewFile = Nothing
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
                        NewFile = openFileDialog.FileName
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
        Dim fileInfo As New FileInfo(NewFile)

        ' Check if file exists
        If Not fileInfo.Exists Then
            DisplayMessage(lblMessage, GetMessage(MessageType.FileNotFound), True, EP, lblMessage)
            Exit Sub
        End If

        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            Dim m As String = String.Format(GetMessage(MessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text)
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

        Dim result As Boolean = UploadPermitDocument(NewPermitDocument, fileInfo.FullName, Me)

        If result Then
            Dim m As String = String.Format(GetMessage(MessageType.UploadSuccess), NewPermitDocument.FileName)
            DisplayMessage(lblMessage, m)
            SaveSetting(UserSetting.PermitUploadLocation, fileInfo.DirectoryName)
        Else
            DisplayMessage(lblMessage, GetMessage(MessageType.UploadFailure), True, EP, lblMessage)
        End If

        ClearFileUploader()
        ShowCurrentFiles()

    End Sub

    Private Function DocumentTypeAlreadyExists() As Boolean
        Dim index As Integer = CurrentFiles.FindIndex( _
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

#Region "Accept Button"

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtApplicationNumber.Leave, txtNewDescription.Leave, txtFileDescription.Leave, ddlUpdateDocumentType.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtApplicationNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApplicationNumber.Enter
        Me.AcceptButton = btnFindApplication
    End Sub

    Private Sub txtNewDescription_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNewDescription.Enter
        Me.AcceptButton = btnNewFileUpload
    End Sub

    Private Sub FileProperties_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtFileDescription.Enter, ddlUpdateDocumentType.Enter
        Me.AcceptButton = btnUpdateFileDescription
    End Sub

#End Region

#Region "Existing file properties"

    Private Sub dgvFileList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvFileList.SelectionChanged
        If dgvFileList.SelectedRows.Count > 0 Then
            'Dim SelectedFileID As Integer = Convert.ToInt32(dgvFileList.CurrentRow.Cells("DocumentId").Value)
            EnableFileProperties()
        Else
            DisableFileProperties()
        End If
    End Sub

    Private Sub EnableFileProperties()
        btnDeleteFile.Enabled = True
        btnDownloadFile.Enabled = True
        With txtFileDescription
            .Visible = True
            .Text = dgvFileList.CurrentRow.Cells("Comment").Value
        End With
        With ddlUpdateDocumentType
            .Visible = True
            .SelectedValue = dgvFileList.CurrentRow.Cells("DocumentId").Value
        End With
        With btnUpdateFileDescription
            .Enabled = True
            .Visible = True
        End With
        With lblSelectedFileName
            .Visible = True
            .Text = dgvFileList.CurrentRow.Cells("FileName").Value
        End With
    End Sub

    Private Sub DisableFileProperties()
        btnDeleteFile.Enabled = False
        btnDownloadFile.Enabled = False
        With txtFileDescription
            .Visible = False
            .Text = ""
        End With
        ddlUpdateDocumentType.Visible = False
        With btnUpdateFileDescription
            .Enabled = False
            .Visible = False
        End With
        With lblSelectedFileName
            .Visible = False
            .Text = ""
        End With
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
                ShowCurrentFiles()
            Else
                DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DeleteFailure), lblSelectedFileName), True, EP)
            End If
        End If
    End Sub

    Private Sub btnDownloadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadFile.Click
        Dim doc As PermitDocument = PermitDocumentFromFileListRow(dgvFileList.CurrentRow)
        Dim downloaded As Boolean = DownloadDocument(doc, Me)
        If Not downloaded Then
            DisplayMessage(lblMessage, String.Format(GetMessage(MessageType.DownloadFailure), lblSelectedFileName), True, EP)
        End If
    End Sub

    Private Sub btnUpdateFileDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFileDescription.Click
        Dim doc As PermitDocument = PermitDocumentFromFileListRow(dgvFileList.CurrentRow)
        Dim updated As Boolean = UpdatePermitDocument(doc, Me)
        If updated Then
            DisplayMessage(lblMessage, GetMessage(MessageType.UpdateSuccess))
            ShowCurrentFiles()
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

    Private Sub ddlNewDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlNewDocumentType.SelectedIndexChanged

    End Sub
End Class
