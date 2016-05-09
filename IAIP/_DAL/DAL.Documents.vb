Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports Iaip.Apb.Sscp
Imports EpdItDbHelper

Namespace DAL
    Module DocumentData

#Region "File Filters"
        ' File filters for open/save dialog boxes
        ' Document extensions from http://en.wikipedia.org/wiki/List_of_file_formats#Document

        Public Function FileSaveFilters(ByVal key As String) As String
            Dim fileFilters As New Dictionary(Of String, String)
            With fileFilters
                ' Text
                .Add(".txt", "Text file (*.txt)|*.txt")
                .Add(".rtf", "Rich text file (*.rtf)|*.rtf")

                ' Documents, Microsoft
                .Add(".doc", "Word Document (*.doc)|*.doc")
                .Add(".docx", "Word Document (*.docx)|*.docx")
                .Add(".docm", "Word Document (*.docm)|*.docm")
                .Add(".wri", "Microsoft Write Document (*.wri)|*.wri")

                ' Documents, Other
                .Add(".pdf", "PDF Document (*.pdf)|*.pdf")
                .Add(".epub", "Ebook (*.epub)|*.epub")
                .Add(".gdoc", "Google Drive Document (*.gdoc)|*.gdoc")
                .Add(".odt", "OpenDocument (*.odt)|*.odt")
                .Add(".pages", "Apple Pages Document (*.pages)|*.pages")
                .Add(".wpd", "WordPerfect Document (*.wpd)|*.wpd")

                ' HTML
                .Add(".html", "HTML File (*.html, *.htm)|*.html;*.htm")
                .Add(".htm", "HTML File (*.html, *.htm)|*.html;*.htm")
                .Add(".xhtml", "HTML File (*.html, *.htm, *.xhtml)|*.html;*.htm;*.xhtml")
                .Add(".xht", "HTML File (*.html, *.htm, *.xhtml, *.xht)|*.html;*.htm;*.xhtml;*.xht")

                ' Spreadsheets, Microsoft
                .Add(".xls", "Excel Spreadsheet (*.xls)|*.xls")
                .Add(".xlsx", "Excel Spreadsheet (*.xlsx)|*.xlsx")
                .Add(".xlsm", "Excel Spreadsheet (*.xlsm)|*.xlsm")
                .Add(".xlsb", "Excel Spreadsheet (*.xlsb)|*.xlsb")

                ' Spreadsheets, Other
                .Add(".gsheet", "Google Drive Spreadsheet (*.gsheet)|*.gsheet")
                .Add(".ods", "Open Document Spreadsheet (*.ods)|*.ods")

                ' Data
                .Add(".csv", "CSV File (*.csv)|*.csv")
                .Add(".xml", "XML File (*.xml)|*.xml")

                ' Presentations
                .Add(".ppt", "PowerPoint Presentation (*.ppt)|*.ppt")
                .Add(".pptx", "PowerPoint Presentation (*.pptx)|*.pptx")
                .Add(".pptm", "PowerPoint Presentation (*.pptm)|*.pptm")

                ' Archive (zip) files
                .Add(".zip", "Archive File (*.zip)|*.zip")
                .Add(".7z", "Archive File (*.7z)|*.7z")
                .Add(".rar", "Archive File (*.rar)|*.rar")

                ' Images
                .Add(".bmp", "Bitmap Image (*.bmp)|*.bmp")
                .Add(".png", "PNG Image (*.png)|*.png")
                .Add(".jpg", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe")
                .Add(".jpeg", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe")
                .Add(".jpe", "JPEG Image (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;.jpe")
                .Add(".gif", "GIF Image (*.gif)|*.gif")
                .Add(".tif", "TIFF Image (*.tif, *.tiff)|*.tif;*.tiff")
                .Add(".tiff", "TIFF Image (*.tif, *.tiff)|*.tif;*.tiff")
            End With

            If fileFilters.ContainsKey(key) Then
                Return fileFilters(key)
            Else
                Return "*.*|*.*"
            End If

        End Function

        Public Function FileOpenFilters() As List(Of String)
            Dim fileFilters As New List(Of String)
            With fileFilters
                .Add("Common Document Formats (*.docx, *.doc, *.pdf, *.xlsx, *.xls)|*.pdf;*.doc;*.docx;*.xlsx;*.xls")
                .Add("PDF Documents (*.pdf)|*.pdf")
                .Add("Word Documents (*.doc, *.docx, *.docm)|*.doc;*.docx;*.docm")
                .Add("Excel Spreadsheets (*.xls, *.xlsx, *.xlsm, *.xlsb, *.csv)|*.xls;*.xlsx;*.xlsm;*.xlsb; *.csv")
                .Add("PowerPoint Presentations (*.ppt, *.pptx, *.pptm)|*.ppt;*.pptx;*.pptm")
                .Add("Text Files (*.txt)|*.txt")
                .Add("Rich Text Files (*.rtf)|*.rtf")
                .Add("Images (*.png, *.jpg, *.gif, *.tif, *.bmp, etc.)|*.png;*.jpg;*.jpeg;*.jpe;*.gif;*.tif;*.tiff;*.bmp")
                .Add("Archive Files(*.zip, *.7z, *.rar)|*.zip;*.7z;*.rar")
                .Add("Other Documents (*.epub, *.gdoc, *.odt, *.wpd, *.wri)|*.epub;*.gdoc;*.odt;*.wpd;*.wri")
                .Add("Other Spreadsheets (*.csv, *.gsheet, *.ods)|*.csv,*.gsheet,*.ods")
                .Add("HTML Files (*.html, *.htm, *.xhtml, *.xht)|*.html;*.htm;*.xhtml;*.xht")
                .Add("Data Files (*.xml, *.csv)|*.xml;*.csv")
                .Add("All Files (*.*)|*.*")
            End With
            Return fileFilters
        End Function

#End Region

#Region "Retrieve Enforcement Documents"

        Public Function GetEnforcementDocumentsAsList(ByVal enfNum As String) As List(Of EnforcementDocument)
            Dim docsList As New List(Of EnforcementDocument)
            Dim doc As New EnforcementDocument

            Dim dataTable As DataTable = GetEnforcementDocumentsAsTable(enfNum)

            For Each row As DataRow In dataTable.Rows
                doc = GetEnforcementDocumentFromDataRow(row)
                docsList.Add(doc)
            Next

            Return docsList
        End Function

        Public Function GetEnforcementDocumentsAsTable(ByVal enfNum As String) As DataTable
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
                " FROM AIRBRANCH.IAIP_BINARYFILES " &
                " INNER JOIN AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " &
                " ON IAIP_BINARYFILES.BINARYFILEID = IAIP_SSCP_ENFORCEMENTDOCS.NUMBINARYFILE " &
                " INNER JOIN AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " &
                " ON IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE = IAIP_LK_SSCPDOCUMENTTYPE.DOCUMENTTYPEID " &
                " WHERE IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER = :pId "
            Dim parameter As New SqlParameter("pId", enfNum)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region "Read Documents from DataRow"

        Private Function GetEnforcementDocumentFromDataRow(ByVal row As DataRow) As EnforcementDocument
            Dim doc As New EnforcementDocument

            FillDocumentFromDataRow(row, CType(doc, EnforcementDocument))

            With doc
                .DocumentId = CInt(row("ENFORCEMENTDOCSID"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
            End With

            Return doc
        End Function

        Private Sub FillDocumentFromDataRow(ByVal row As DataRow, ByRef doc As Document)
            With doc
                .BinaryFileId = Convert.ToInt32(row("BINARYFILEID"))
                .FileName = row("STRFILENAME")
                .FileSize = DBUtilities.GetNullable(Of Decimal?)(row("NUMFILESIZE"))
                .DocumentTypeId = row("NUMDOCUMENTTYPE")
                .Comment = DBUtilities.GetNullable(Of String)(row("STRCOMMENT"))
                .UploadDate = NormalizeDate(DBUtilities.GetNullable(Of Date)(row("CREATEDATE")))
                .DocumentType = row("STRDOCUMENTTYPE")
            End With
        End Sub

#End Region

#Region "Download files"

        Public Function DownloadDocument(ByVal doc As Document, <Out()> Optional ByRef canceled As Boolean = False, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing OrElse doc.BinaryFileId = 0 Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim result As Boolean = False

            Dim dialog As New SaveFileDialog()
            With dialog
                .Filter = FileSaveFilters(doc.FileExtension.ToLower)
                .DefaultExt = doc.FileExtension.ToLower
                .FileName = doc.FileName
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
            End With

            Dim dialogAction As DialogResult = dialog.ShowDialog()

            If dialogAction = DialogResult.OK Then
                result = DownloadFile(doc.BinaryFileId, dialog.FileName)
                If result Then
                    If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                        SaveUserSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(dialog.FileName))
                    End If
                    System.Diagnostics.Process.Start("explorer.exe", "/select,""" & dialog.FileName.ToString & """")
                End If
            ElseIf dialogAction = DialogResult.Cancel Then
                canceled = True
            End If

            dialog.Dispose()

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

        Public Function DownloadFile(ByVal id As Integer, ByVal filePath As String) As Boolean
            Dim query As String = " SELECT IAIP_BINARYFILES.BLOBDOCUMENT " &
                                " FROM AIRBRANCH.IAIP_BINARYFILES " &
                                " WHERE IAIP_BINARYFILES.BINARYFILEID = :pBinId "
            Dim parameter As SqlParameter = New SqlParameter("pBinId", id)
            Return DB.SaveBinaryFileFromDB(filePath, query, parameter)
        End Function

#End Region

#Region "Upload files"

        Private Function UploadDocument(ByVal doc As Document, ByVal pathToFile As String, ByVal metaDataQuery As String, ByVal metaDataId As String, Optional ByVal sender As Object = Nothing) As Boolean
            Throw New NotImplementedException()

            ' TODO: SQL Server migration

            'If String.IsNullOrEmpty(pathToFile) Then Return False

            'If sender IsNot Nothing Then
            '    sender.Cursor = Cursors.AppStarting
            'End If

            '' -- Transaction
            '' 1. Get seq value
            '' 2. Upload the binary file, use seq as id
            '' 3. Upload file metadata, including binary file id
            '' -- Commit transaction

            'Dim queryList As New List(Of String)
            'Dim parametersList As New List(Of SqlParameter())
            'Dim binarySeqId As Integer = GetNextBinaryFileSequenceValue()
            'Dim parameters As SqlParameter()

            'queryList.Add(
            '    " INSERT INTO AIRBRANCH.IAIP_BINARYFILES " &
            '    " (BINARYFILEID,STRFILENAME,STRFILEEXTENSION,NUMFILESIZE,BLOBDOCUMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " &
            '    " VALUES (:pBinId,:pFileName,:pFileExt,:pFileSize,:pBinFile,:pUser,:pUpdateDate,:pCreateDate) "
            ')
            'parameters = New SqlParameter() {
            '    New SqlParameter("pBinId", binarySeqId),
            '    New SqlParameter("pFileName", doc.FileName),
            '    New SqlParameter("pFileExt", doc.FileExtension),
            '    New SqlParameter("pFileSize", doc.FileSize),
            '    New SqlParameter("pBinFile", SqlDbType.VarBinary, DB.ReadByteArrayFromFile(pathToFile), ParameterDirection.Input),
            '    New SqlParameter("pUser", CurrentUser.UserID),
            '    New SqlParameter("pUpdateDate", Date.Now),
            '    New SqlParameter("pCreateDate", Date.Now)
            '}
            'parametersList.Add(parameters)

            'queryList.Add(metaDataQuery)
            'parameters = New SqlParameter() {
            '    New SqlParameter("pBinId", binarySeqId),
            '    New SqlParameter("pMetaDataId", metaDataId),
            '    New SqlParameter("pDocTypeId", doc.DocumentTypeId),
            '    New SqlParameter("pComment", doc.Comment),
            '    New SqlParameter("pUser", CurrentUser.UserID),
            '    New SqlParameter("pUpdateDate", Date.Now),
            '    New SqlParameter("pCreateDate", Date.Now)
            '}
            'parametersList.Add(parameters)

            'Dim result As Boolean = DB.RunCommand(queryList, parametersList)

            'If sender IsNot Nothing Then
            '    sender.Cursor = Nothing
            'End If

            'Return result
        End Function

        Public Function UploadEnforcementDocument(ByVal doc As EnforcementDocument, ByVal pathToFile As String, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            Dim metaDataQuery As String =
                            " INSERT INTO AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " &
                            " (NUMBINARYFILE,STRENFORCEMENTNUMBER,NUMDOCUMENTTYPE,STRCOMMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " &
                            " VALUES (:pBinId,:pMetaDataId,:pDocTypeId,:pComment,:pUser,:pUpdateDate,:pCreateDate) "
            Dim metaDataId As String = doc.EnforcementNumber
            Return UploadDocument(doc, pathToFile, metaDataQuery, metaDataId, sender)
        End Function

        Private Function GetNextBinaryFileSequenceValue() As Integer
            Dim query As String = " SELECT AIRBRANCH.IAIP_BINARYFILES_SEQ.NEXTVAL FROM DUAL "
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

#End Region

#Region "Delete files"

        Public Function DeleteDocument(ByVal id As Integer, Optional ByVal sender As Object = Nothing) As Boolean
            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String = " DELETE FROM AIRBRANCH.IAIP_BINARYFILES WHERE BINARYFILEID = :pBinId "
            Dim parameter As SqlParameter = New SqlParameter("pBinId", id)

            Dim result As Boolean = DB.RunCommand(query, parameter)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Update file description"
        Public Function UpdateEnforcementDocument(ByVal doc As EnforcementDocument, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            Dim query As String =
                " UPDATE AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " &
                " SET NUMDOCUMENTTYPE = :pDocTypeId, " &
                " STRCOMMENT = :pComment, " &
                " UPDATEUSER = :pUser, " &
                " UPDATEDATE = :pUpdateDate " &
                " WHERE ENFORCEMENTDOCSID = :pDocId "
            Return UpdateDocument(doc, query, sender)
        End Function

        Public Function UpdateDocument(ByVal doc As Document, ByVal query As String, Optional ByVal sender As Object = Nothing) As Boolean
            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim parameters As SqlParameter() = {
                New SqlParameter("pDocTypeId", doc.DocumentTypeId),
                New SqlParameter("pComment", doc.Comment),
                New SqlParameter("pUser", CurrentUser.UserID),
                New SqlParameter("pUpdateDate", Date.Now),
                New SqlParameter("pDocId", doc.DocumentId)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Document Types"

        Public Function GetEnforcementDocumentTypesDict() As Dictionary(Of Integer, String)
            Dim query As String = "SELECT DOCUMENTTYPEID, " &
                " STRDOCUMENTTYPE " &
                " FROM AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " &
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
                " FROM AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " &
                " ORDER BY NUMORDINAL, STRDOCUMENTTYPE "

            Dim dataTable As DataTable = DB.GetDataTable(query)

            For Each row As DataRow In dataTable.Rows
                FillEnforcementDocumentTypeFromDataRow(row, docType)
                docTypesList.Add(docType)
            Next

            Return docTypesList
        End Function

        Private Sub FillEnforcementDocumentTypeFromDataRow(ByVal row As DataRow, ByRef d As DocumentType)
            d = New DocumentType
            With d
                .Active = Convert.ToBoolean(row("FACTIVE"))
                .DocumentType = DBUtilities.GetNullable(Of String)(row("STRDOCUMENTTYPE"))
                .DocumentTypeId = row("DOCUMENTTYPEID")
                Try
                    .Ordinal = DBUtilities.GetNullable(Of Short?)(row("NUMORDINAL"))

                Catch ex As Exception

                End Try
            End With
        End Sub

        Public Function UpdateEnforcementDocumentType(ByVal d As DocumentType, Optional ByVal sender As Object = Nothing) As Boolean
            If d Is Nothing Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String =
                " UPDATE AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " &
                " SET STRDOCUMENTTYPE  = :pDocType, " &
                "   FACTIVE            = :pActive, " &
                "   NUMORDINAL         = :pPosition " &
                " WHERE DOCUMENTTYPEID = :pId "

            Dim parameters As SqlParameter() = {
                New SqlParameter("pDocType", d.DocumentType),
                New SqlParameter("pActive", d.Active.ToString),
                New SqlParameter("pPosition", d.Ordinal),
                New SqlParameter("pId", d.DocumentTypeId)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

        Public Function SaveEnforcementDocumentType(ByVal d As DocumentType, Optional ByVal sender As Object = Nothing) As Boolean
            If d Is Nothing Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String =
                " INSERT INTO AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " &
                " (STRDOCUMENTTYPE, FACTIVE, NUMORDINAL ) " &
                " VALUES (:pName, :pActive, :pOrdinal) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("pName", d.DocumentType),
                New SqlParameter("pActive", d.Active.ToString),
                New SqlParameter("pOrdinal", d.Ordinal)
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

#End Region

    End Module
End Namespace
