Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports Iaip.Apb.Sscp
Imports EpdIt

Namespace DAL
    Module DocumentData

#Region "File Filters"
        ' File filters for open/save dialog boxes
        ' Document extensions from https://en.wikipedia.org/wiki/List_of_file_formats#Document

        Public FileSaveFilters As New Dictionary(Of String, String) From {
            {".txt", "Text file (*.txt)|*.txt"}, ' Text
            {".rtf", "Rich text file (*.rtf)|*.rtf"},
            {".doc", "Word Document (*.doc)|*.doc"}, ' Documents, Microsoft
            {".docx", "Word Document (*.docx)|*.docx"},
            {".docm", "Word Document (*.docm)|*.docm"},
            {".wri", "Microsoft Write Document (*.wri)|*.wri"},
            {".pdf", "PDF Document (*.pdf)|*.pdf"}, ' Documents, Other
            {".epub", "Ebook (*.epub)|*.epub"},
            {".gdoc", "Google Drive Document (*.gdoc)|*.gdoc"},
            {".odt", "OpenDocument (*.odt)|*.odt"},
            {".pages", "Apple Pages Document (*.pages)|*.pages"},
            {".wpd", "WordPerfect Document (*.wpd)|*.wpd"},
            {".html", "HTML File (*.html, *.htm)|*.html;*.htm"}, ' HTML
            {".htm", "HTML File (*.html, *.htm)|*.html;*.htm"},
            {".xhtml", "HTML File (*.html, *.htm, *.xhtml)|*.html;*.htm;*.xhtml"},
            {".xht", "HTML File (*.html, *.htm, *.xhtml, *.xht)|*.html;*.htm;*.xhtml;*.xht"},
            {".xls", "Excel Spreadsheet (*.xls)|*.xls"}, ' Spreadsheets, Microsoft
            {".xlsx", "Excel Spreadsheet (*.xlsx)|*.xlsx"},
            {".xlsm", "Excel Spreadsheet (*.xlsm)|*.xlsm"},
            {".xlsb", "Excel Spreadsheet (*.xlsb)|*.xlsb"},
            {".gsheet", "Google Drive Spreadsheet (*.gsheet)|*.gsheet"}, ' Spreadsheets, Other
            {".ods", "Open Document Spreadsheet (*.ods)|*.ods"},
            {".csv", "CSV File (*.csv)|*.csv"}, ' Data
            {".xml", "XML File (*.xml)|*.xml"},
            {".ppt", "PowerPoint Presentation (*.ppt)|*.ppt"}, ' Presentations
            {".pptx", "PowerPoint Presentation (*.pptx)|*.pptx"},
            {".pptm", "PowerPoint Presentation (*.pptm)|*.pptm"},
            {".zip", "Archive File (*.zip)|*.zip"}, ' Archive (zip) files
            {".7z", "Archive File (*.7z)|*.7z"},
            {".rar", "Archive File (*.rar)|*.rar"},
            {".bmp", "Bitmap Image (*.bmp)|*.bmp"}, ' Images
            {".png", "PNG Image (*.png)|*.png"},
            {".jpg", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe"},
            {".jpeg", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe"},
            {".jpe", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe"},
            {".gif", "GIF Image (*.gif)|*.gif"},
            {".tif", "TIFF Image (*.tif, *.tiff)|*.tif;*.tiff"},
            {".tiff", "TIFF Image (*.tif, *.tiff)|*.tif;*.tiff"}
        }

        Public Function GetFileSaveFilters(key As String) As String
            If FileSaveFilters.ContainsKey(key) Then
                Return FileSaveFilters(key)
            Else
                Return "*.*|*.*"
            End If
        End Function

        Public FileOpenFilters As New List(Of String) From {
            {"Common Document Formats (*.docx, *.doc, *.pdf, *.xlsx, *.xls)|*.pdf;*.doc;*.docx;*.xlsx;*.xls"},
            {"PDF Documents (*.pdf)|*.pdf"},
            {"Word Documents (*.doc, *.docx, *.docm)|*.doc;*.docx;*.docm"},
            {"Excel Spreadsheets (*.xls, *.xlsx, *.xlsm, *.xlsb, *.csv)|*.xls;*.xlsx;*.xlsm;*.xlsb; *.csv"},
            {"PowerPoint Presentations (*.ppt, *.pptx, *.pptm)|*.ppt;*.pptx;*.pptm"},
            {"Text Files (*.txt)|*.txt"},
            {"Rich Text Files (*.rtf)|*.rtf"},
            {"Images (*.png, *.jpg, *.gif, *.tif, *.bmp, etc.)|*.png;*.jpg;*.jpeg;*.jpe;*.gif;*.tif;*.tiff;*.bmp"},
            {"Archive Files(*.zip, *.7z, *.rar)|*.zip;*.7z;*.rar"},
            {"Other Documents (*.epub, *.gdoc, *.odt, *.wpd, *.wri)|*.epub;*.gdoc;*.odt;*.wpd;*.wri"},
            {"Other Spreadsheets (*.csv, *.gsheet, *.ods)|*.csv,*.gsheet,*.ods"},
            {"HTML Files (*.html, *.htm, *.xhtml, *.xht)|*.html;*.htm;*.xhtml;*.xht"},
            {"Data Files (*.xml, *.csv)|*.xml;*.csv"},
            {"All Files (*.*)|*.*"}
        }

#End Region

#Region "Retrieve Enforcement Documents"

        Public Function GetEnforcementDocumentsAsList(enfNum As Integer) As List(Of EnforcementDocument)
            Dim docsList As New List(Of EnforcementDocument)
            Dim doc As EnforcementDocument

            Dim dataTable As DataTable = GetEnforcementDocumentsAsTable(enfNum)

            For Each row As DataRow In dataTable.Rows
                doc = GetEnforcementDocumentFromDataRow(row)
                docsList.Add(doc)
            Next

            Return docsList
        End Function

        Public Function GetEnforcementDocumentsAsTable(enfNum As Integer) As DataTable
            Dim query As String =
                " SELECT " &
                "   IAIP_LK_SSCPDOCUMENTTYPE.STRDOCUMENTTYPE, " &
                "   IAIP_BINARYFILES.STRFILENAME, " &
                "   IAIP_SSCP_ENFORCEMENTDOCS.CREATEDATE, " &
                "   IAIP_SSCP_ENFORCEMENTDOCS.STRCOMMENT, " &
                "   IAIP_BINARYFILES.NUMFILESIZE, " &
                "   IAIP_BINARYFILES.BINARYFILEID, " &
                "   IAIP_BINARYFILES.STRFILEEXTENSION, " &
                "   IAIP_SSCP_ENFORCEMENTDOCS.ENFORCEMENTDOCSID, " &
                "   IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER, " &
                "   IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE " &
                " FROM IAIP_BINARYFILES " &
                " INNER JOIN IAIP_SSCP_ENFORCEMENTDOCS " &
                " ON IAIP_BINARYFILES.BINARYFILEID = IAIP_SSCP_ENFORCEMENTDOCS.NUMBINARYFILE " &
                " INNER JOIN IAIP_LK_SSCPDOCUMENTTYPE " &
                " ON IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE = IAIP_LK_SSCPDOCUMENTTYPE.DOCUMENTTYPEID " &
                " WHERE IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER = @Id "
            Dim parameter As New SqlParameter("@Id", enfNum)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region "Read Documents from DataRow"

        Private Function GetEnforcementDocumentFromDataRow(row As DataRow) As EnforcementDocument
            Dim doc As New EnforcementDocument

            FillDocumentFromDataRow(row, CType(doc, EnforcementDocument))

            With doc
                .DocumentId = CInt(row("ENFORCEMENTDOCSID"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
            End With

            Return doc
        End Function

        Private Sub FillDocumentFromDataRow(row As DataRow, ByRef doc As Document)
            With doc
                .BinaryFileId = Convert.ToInt32(row("BINARYFILEID"))
                .FileName = row("STRFILENAME")
                .FileSize = DBUtilities.GetNullable(Of Integer?)(row("NUMFILESIZE"))
                .DocumentTypeId = row("NUMDOCUMENTTYPE")
                .Comment = DBUtilities.GetNullable(Of String)(row("STRCOMMENT"))
                .UploadDate = NormalizeDate(DBUtilities.GetNullable(Of Date)(row("CREATEDATE")))
                .DocumentType = row("STRDOCUMENTTYPE")
            End With
        End Sub

#End Region

#Region "Download files"

        Public Function DownloadDocument(doc As Document, <Out()> Optional ByRef canceled As Boolean = False, Optional sender As Object = Nothing) As Boolean
            If doc Is Nothing OrElse doc.BinaryFileId = 0 Then Return False

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            Dim result As Boolean = False

            Dim dialog As New SaveFileDialog()
            With dialog
                .Filter = GetFileSaveFilters(doc.FileExtension.ToLower)
                .DefaultExt = doc.FileExtension.ToLower
                .FileName = doc.FileName
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
            End With

            Dim dialogAction As DialogResult = dialog.ShowDialog()

            If dialogAction = DialogResult.OK Then
                result = DownloadFile(doc.BinaryFileId, dialog.FileName)
                If result Then
                    If Path.GetDirectoryName(dialog.FileName) <> dialog.InitialDirectory Then
                        SaveUserSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(dialog.FileName))
                    End If

                    Process.Start("explorer.exe", "/select,""" & dialog.FileName.ToString & """")
                End If
            ElseIf dialogAction = DialogResult.Cancel Then
                canceled = True
            End If

            dialog.Dispose()

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

        Public Function DownloadFile(id As Integer, filePath As String) As Boolean
            Dim query As String = " SELECT IAIP_BINARYFILES.BLOBDOCUMENT " &
                " FROM IAIP_BINARYFILES " &
                " WHERE IAIP_BINARYFILES.BINARYFILEID = @FileID "
            Dim parameter As SqlParameter = New SqlParameter("@FileID", id)
            Return SaveBinaryFileFromDB(filePath, query, parameter)
        End Function

#End Region

#Region "Upload files"

        Private Function UploadDocument(doc As Document, pathToFile As String, metaDataQuery As String, metaDataId As String, Optional sender As Object = Nothing) As Boolean
            If String.IsNullOrEmpty(pathToFile) Then Return False

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            ' 1. Get seq value
            ' -- Start Transaction
            ' 2. Upload the binary file; use seq as id
            ' 3. Upload file metadata; include binary file id
            ' -- Commit Transaction

            Dim binarySeqId As Integer = GetNextBinaryFileSequenceValue()

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())

            queryList.Add(
                " INSERT INTO IAIP_BINARYFILES " &
                " (BINARYFILEID,STRFILENAME,STRFILEEXTENSION,NUMFILESIZE,BLOBDOCUMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " &
                " VALUES (@FileID,@FileName,@FileExt,@FileSize,@BinFile,@User,@UpdateDate,@CreateDate) "
            )
            parametersList.Add({
                New SqlParameter("@FileID", binarySeqId),
                New SqlParameter("@FileName", doc.FileName),
                New SqlParameter("@FileExt", doc.FileExtension),
                New SqlParameter("@FileSize", doc.FileSize),
                New SqlParameter("@BinFile", ReadByteArrayFromFile(pathToFile)),
                New SqlParameter("@User", CurrentUser.UserID),
                New SqlParameter("@UpdateDate", Now),
                New SqlParameter("@CreateDate", Now)
            })

            queryList.Add(metaDataQuery)
            parametersList.Add({
                New SqlParameter("@FileID", binarySeqId),
                New SqlParameter("@MetaDataId", metaDataId),
                New SqlParameter("@DocTypeId", doc.DocumentTypeId),
                New SqlParameter("@Comment", doc.Comment),
                New SqlParameter("@User", CurrentUser.UserID),
                New SqlParameter("@UpdateDate", Now),
                New SqlParameter("@CreateDate", Now)
            })

            Dim result As Boolean = DB.RunCommand(queryList, parametersList)

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

        Public Function UploadEnforcementDocument(doc As EnforcementDocument, pathToFile As String, Optional sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            Dim metaDataQuery As String =
                            " INSERT INTO IAIP_SSCP_ENFORCEMENTDOCS " &
                            " (ENFORCEMENTDOCSID,NUMBINARYFILE,STRENFORCEMENTNUMBER,NUMDOCUMENTTYPE,STRCOMMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " &
                            " VALUES (NEXT VALUE FOR IAIP_SSCP_ENFORCEMENTDOCS_SEQ,@FileID,@MetaDataId,@DocTypeId,@Comment,@User,@UpdateDate,@CreateDate) "
            Dim metaDataId As String = doc.EnforcementNumber
            Return UploadDocument(doc, pathToFile, metaDataQuery, metaDataId, sender)
        End Function

        Private Function GetNextBinaryFileSequenceValue() As Integer
            Dim query As String = "SELECT NEXT VALUE FOR IAIP_BINARYFILES_SEQ"
            Return DB.GetInteger(query)
        End Function

#End Region

#Region "Delete files"

        Public Function DeleteDocument(id As Integer, Optional sender As Object = Nothing) As Boolean
            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            Dim query As String = " DELETE FROM IAIP_BINARYFILES WHERE BINARYFILEID = @FileID "
            Dim parameter As SqlParameter = New SqlParameter("@FileID", id)

            Dim result As Boolean = DB.RunCommand(query, parameter)

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Update file description"
        Public Function UpdateEnforcementDocument(doc As EnforcementDocument, Optional sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            Dim query As String =
                " UPDATE IAIP_SSCP_ENFORCEMENTDOCS " &
                " SET NUMDOCUMENTTYPE = @DocTypeId, " &
                " STRCOMMENT = @Comment, " &
                " UPDATEUSER = @User, " &
                " UPDATEDATE = @UpdateDate " &
                " WHERE ENFORCEMENTDOCSID = @DocId "
            Return UpdateDocument(doc, query, sender)
        End Function

        Public Function UpdateDocument(doc As Document, query As String, Optional sender As Object = Nothing) As Boolean
            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            Dim parameters As SqlParameter() = {
                New SqlParameter("@DocTypeId", doc.DocumentTypeId),
                New SqlParameter("@Comment", doc.Comment),
                New SqlParameter("@User", CurrentUser.UserID),
                New SqlParameter("@UpdateDate", Date.Now),
                New SqlParameter("@DocId", doc.DocumentId)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Document Types"

        Public Function GetEnforcementDocumentTypesDict() As Dictionary(Of Integer, String)
            Dim query As String = "SELECT DOCUMENTTYPEID, " &
                " STRDOCUMENTTYPE " &
                " FROM IAIP_LK_SSCPDOCUMENTTYPE " &
                " WHERE FACTIVE = '" & Boolean.TrueString & "' " &
                " ORDER BY NUMORDINAL, STRDOCUMENTTYPE "
            Return DB.GetLookupDictionary(query)
        End Function

        Public Function GetEnforcementDocumentTypes() As List(Of DocumentType)
            Dim docTypesList As New List(Of DocumentType)
            Dim docType As New DocumentType

            Dim query As String = "SELECT DOCUMENTTYPEID, " &
                " STRDOCUMENTTYPE, " &
                " FACTIVE, " &
                " NUMORDINAL " &
                " FROM IAIP_LK_SSCPDOCUMENTTYPE " &
                " ORDER BY NUMORDINAL, STRDOCUMENTTYPE "

            Dim dataTable As DataTable = DB.GetDataTable(query)

            For Each row As DataRow In dataTable.Rows
                FillEnforcementDocumentTypeFromDataRow(row, docType)
                docTypesList.Add(docType)
            Next

            Return docTypesList
        End Function

        Private Sub FillEnforcementDocumentTypeFromDataRow(row As DataRow, ByRef d As DocumentType)
            d = New DocumentType
            With d
                .Active = Convert.ToBoolean(row("FACTIVE"))
                .DocumentType = DBUtilities.GetNullable(Of String)(row("STRDOCUMENTTYPE"))
                .DocumentTypeId = row("DOCUMENTTYPEID")
                .Ordinal = DBUtilities.GetNullable(Of Integer)(row("NUMORDINAL"))
            End With
        End Sub

        Public Function UpdateEnforcementDocumentType(d As DocumentType, Optional sender As Object = Nothing) As Boolean
            If d Is Nothing Then Return False

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            Dim query As String =
                " UPDATE IAIP_LK_SSCPDOCUMENTTYPE " &
                " SET STRDOCUMENTTYPE  = @DocType, " &
                "   FACTIVE            = @Active, " &
                "   NUMORDINAL         = @Position " &
                " WHERE DOCUMENTTYPEID = @Id "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@DocType", d.DocumentType),
                New SqlParameter("@Active", d.Active.ToString),
                New SqlParameter("@Position", d.Ordinal),
                New SqlParameter("@Id", d.DocumentTypeId)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

        Public Function SaveEnforcementDocumentType(d As DocumentType, Optional sender As Object = Nothing) As Boolean
            If d Is Nothing Then Return False

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Cursors.AppStarting
            End If

            Dim query As String =
                " INSERT INTO IAIP_LK_SSCPDOCUMENTTYPE " &
                " (DOCUMENTTYPEID, STRDOCUMENTTYPE, FACTIVE, NUMORDINAL ) " &
                " VALUES ((SELECT MAX(DOCUMENTTYPEID) + 1 FROM IAIP_LK_SSCPDOCUMENTTYPE), @Name, @Active, @Ordinal) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@Name", d.DocumentType),
                New SqlParameter("@Active", d.Active.ToString),
                New SqlParameter("@Ordinal", d.Ordinal)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If TypeOf sender Is Form Then
                CType(sender, Form).Cursor = Nothing
            End If

            Return result
        End Function

#End Region

    End Module
End Namespace
