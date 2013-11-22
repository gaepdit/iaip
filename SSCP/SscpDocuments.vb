Imports System.Collections.Generic
Imports System.Text
Imports System.IO

Imports JohnGaltProject.Apb.SSCP
Imports JohnGaltProject.DAL.SSCP
Imports JohnGaltProject.DAL.Documents

Imports Oracle.DataAccess.Types

Public Class SscpDocuments

#Region "Properties"
    Private ExistingFiles As List(Of EnforcementDocument)
    Private NewDocument As String = Nothing

    Public Property EnforcementInfo() As EnforcementInfo
        Get
            Return _enforcementInfo
        End Get
        Set(ByVal value As EnforcementInfo)
            _enforcementInfo = value
        End Set
    End Property
    Private _enforcementInfo As EnforcementInfo
#End Region

#Region "Page Load"

    Private Sub SscpDocuments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDocumentTypes()
        ClearEverything()
        If EnforcementInfo IsNot Nothing Then ShowEnforcement()
    End Sub

    Private Sub LoadDocumentTypes()
        ' Get list of various document types and bind that list to the comboboxes
        Dim DocumentTypes As Dictionary(Of Integer, String) = DAL.GetEnforcementDocumentTypesDict

        If DocumentTypes.Count > 0 Then
            With ddlNewDocumentType
                .DataSource = New BindingSource(DocumentTypes, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
            With ddlDocumentType
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
            Me.Enabled = False
        End If
    End Sub

#End Region

#Region "Enforcement fetcher"

    Private Sub btnFindEnforcement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindEnforcement.Click
        ClearEverything()
        FindEnforcement()
    End Sub

    Private Sub FindEnforcement()
        If txtFindEnforcement.Text = "" Then Exit Sub

        EnforcementInfo = Nothing
        Dim enfNum As String = txtFindEnforcement.Text
        If Integer.TryParse(enfNum, Nothing) Then
            If EnforcementExists(enfNum) Then
                EnforcementInfo = GetEnforcementInfo(enfNum)
                ShowEnforcement()
            Else
                DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.InvalidApplicationNumber), True, EP, lblMessage)
            End If
        End If
    End Sub

    Private Sub ShowEnforcement()
        DisplayEnforcementInfo()
        LoadDocuments()
        EnableNewDocument()
    End Sub

    Private Sub DisplayEnforcementInfo()
        If EnforcementInfo IsNot Nothing Then
            Dim infoDisplay As New StringBuilder

            Dim airsNum As Integer = CInt(EnforcementInfo.Facility.AirsNumber)
            infoDisplay.AppendFormat("AIRS # {0:000-00000}: {1}", airsNum, EnforcementInfo.Facility.Name).AppendLine()
            infoDisplay.AppendLine(EnforcementInfo.Facility.FacilityLocation.Address.ToString)
            infoDisplay.AppendFormat("Responsible staff: {0}", EnforcementInfo.StaffResponsible).AppendLine()
            If Not EnforcementInfo.DiscoveryDate Is Nothing Then
                infoDisplay.AppendFormat("{0}; Discovery Date: {1:dd-MMM-yyyy}", EnforcementInfo.EnforcementTypeCode, EnforcementInfo.DiscoveryDate).AppendLine()
            Else
                infoDisplay.AppendFormat("{0}", EnforcementInfo.EnforcementTypeCode).AppendLine()
            End If

            lblEnforcementInfo.Text = infoDisplay.ToString
        Else
            ClearEnforcementInfo()
        End If
    End Sub

#End Region

#Region "Display documents"

    Private Sub LoadDocuments()
        DisableDocument()
        dgvDocumentList.DataSource = Nothing
        ExistingFiles = GetEnforcementDocuments(EnforcementInfo.EnforcementNumber)
        If ExistingFiles.Count > 0 Then
            With dgvDocumentList
                .DataSource = New BindingSource(ExistingFiles, Nothing)
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

    Private Sub dataGridView_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvDocumentList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvDocumentList_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvDocumentList.DataBindingComplete
        FormatDocumentList()
        CType(sender, DataGridView).SanelyResizeColumns()
        CType(sender, DataGridView).ClearSelection()
    End Sub

#End Region

#Region "Enable/Disable Form Regions"

#Region "Document (update/delete/download)"
    Private Sub EnableDocument()
        EnableOrDisableDocument(True)
    End Sub
    Private Sub DisableDocument()
        EnableOrDisableDocument(False)
    End Sub
    Private Sub EnableOrDisableDocument(ByVal enable As Boolean)
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
        EnableOrDisableNewDocument(True)
    End Sub
    Private Sub DisableNewDocument()
        EnableOrDisableNewDocument(False)
    End Sub
    Private Sub EnableOrDisableNewDocument(ByVal enable As Boolean)
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
        EnableOrDisableNewDocumentDetails(True)
    End Sub
    Private Sub DisableNewDocumentDetails()
        EnableOrDisableNewDocumentDetails(False)
    End Sub
    Private Sub EnableOrDisableNewDocumentDetails(ByVal enable As Boolean)
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
        ClearMessage(lblMessage, EP)
        ClearDocumentList()
        ClearNewDocument()
        DisableNewDocument()
    End Sub

    Private Sub ClearEnforcementInfo()
        lblEnforcementInfo.Text = ""
    End Sub

    Private Sub ClearNewDocument()
        NewDocument = Nothing
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

    Private Sub btnNewDocumentSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDocumentSelect.Click

        Dim openFileDialog As New OpenFileDialog With { _
            .InitialDirectory = GetUserSetting(UserSetting.PermitUploadLocation), _
            .Filter = String.Join("|", FileOpenFilters.ToArray) _
        }

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK _
        AndAlso openFileDialog.FileName <> "" Then

            ClearNewDocument()

            Dim fileInfo As New FileInfo(openFileDialog.FileName)

            If Not fileInfo.Exists Then
                DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.FileNotFound), True, EP, lblMessage)
            Else
                If fileInfo.Length >= Document.MaxFileSize Then
                    DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.FileTooLarge), True, EP, lblMessage)
                Else
                    If fileInfo.Length = 0 Then
                        DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.FileEmpty), True, EP, lblMessage)
                    Else
                        NewDocument = openFileDialog.FileName
                        EnableNewDocumentDetails()
                        lblNewDocumentName.Text = openFileDialog.SafeFileName
                        txtNewDocumentDescription.Focus()
                    End If
                End If
            End If

        End If
    End Sub

#End Region

#Region "New document actions"

    Private Sub btnNewDocumentCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDocumentCancel.Click
        ClearNewDocument()
    End Sub

    Private Sub btnNewDocumentUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewDocumentUpload.Click
        ClearMessage(lblMessage, EP)
        Dim fileInfo As New FileInfo(NewDocument)

        ' Check if file exists
        If Not fileInfo.Exists Then
            DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.FileNotFound), True, EP, lblMessage)
            Exit Sub
        End If

        Dim m As String

        ' Check if similar document has already been uploaded
        If DocumentTypeAlreadyExists() Then
            m = String.Format(GetDocumentMessage(DocumentMessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text)
            MessageBox.Show(m, "Document Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Create Document object
        Dim documentToUpload As New EnforcementDocument
        With documentToUpload
            .EnforcementNumber = EnforcementInfo.EnforcementNumber
            .Comment = txtNewDocumentDescription.Text
            .DocumentTypeId = ddlNewDocumentType.SelectedValue
            .DocumentType = ddlNewDocumentType.Text
            .FileName = fileInfo.Name
            .FileSize = fileInfo.Length
            .UploadDate = Today
        End With

        m = String.Format(GetDocumentMessage(DocumentMessageType.UploadingFile), documentToUpload.FileName)
        DisplayMessage(lblMessage, m)

        Dim result As Boolean = UploadEnforcementDocument(documentToUpload, fileInfo.FullName, Me)

        If result Then
            m = String.Format(GetDocumentMessage(DocumentMessageType.UploadSuccess), documentToUpload.FileName)
            DisplayMessage(lblMessage, m)
            SaveUserSetting(UserSetting.EnforcementUploadLocation, fileInfo.DirectoryName)
        Else
            DisplayMessage(lblMessage, GetDocumentMessage(DocumentMessageType.UploadFailure), True, EP, lblMessage)
        End If

        ClearNewDocument()
        LoadDocuments()
    End Sub

#End Region

#Region "Document type validation"

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
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DocumentTypeAlreadyExists), ddlNewDocumentType.Text))
        End If
    End Sub

#End Region

#End Region

#Region "Document update/download/delete"

    Private Sub dgvDocumentList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvDocumentList.SelectionChanged
        If dgvDocumentList.SelectedRows.Count = 1 Then
            EnableDocument()
        Else
            DisableDocument()
        End If
    End Sub

    Private Sub btnDocumentDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentDelete.Click
        Dim m As String = String.Format(GetDocumentMessage(DocumentMessageType.ConfirmDelete), lblDocumentName.Text)
        Dim response As Windows.Forms.DialogResult = _
            MessageBox.Show(m, "Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)

        If response = Windows.Forms.DialogResult.Yes Then
            Dim deleted As Boolean = DeleteDocument(dgvDocumentList.CurrentRow.Cells("BinaryFileId").Value)

            If deleted Then
                m = String.Format(GetDocumentMessage(DocumentMessageType.DeleteSuccess), lblDocumentName.Text)
                DisplayMessage(lblMessage, m)
                LoadDocuments()
            Else
                DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DeleteFailure), lblDocumentName), True, EP)
            End If
        End If
    End Sub

    Private Sub btnDocumentDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentDownload.Click
        ClearMessage(lblMessage, EP)

        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadingFile), doc.FileName))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded Or canceled Then
            ClearMessage(lblMessage, EP)
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadFailure), lblDocumentName), True, EP, lblMessage)
        End If
    End Sub

    Private Sub btnDocumentUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentUpdate.Click
        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        doc.Comment = txtDocumentDescription.Text
        doc.DocumentTypeId = ddlDocumentType.SelectedValue
        Dim updated As Boolean = UpdateEnforcementDocument(doc, Me)
        If updated Then
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateSuccess), doc.FileName))
            LoadDocuments()
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateFailure), lblDocumentName), True, EP)
        End If
    End Sub

    Private Function EnforcementDocumentFromFileListRow(ByVal row As DataGridViewRow) As EnforcementDocument
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

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtFindEnforcement.Leave, txtDocumentDescription.Leave, txtNewDocumentDescription.Leave, ddlNewDocumentType.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtApplicationNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtFindEnforcement.Enter
        Me.AcceptButton = btnFindEnforcement
    End Sub

    Private Sub txtNewDocument_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtNewDocumentDescription.Enter
        Me.AcceptButton = btnNewDocumentUpload
    End Sub

    Private Sub FileProperties_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtDocumentDescription.Enter, ddlDocumentType.Enter
        Me.AcceptButton = btnDocumentUpdate
    End Sub

#End Region


End Class