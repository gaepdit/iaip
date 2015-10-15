Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices

Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Imports Iaip.Apb.Sscp
Imports Iaip.Apb.Sspp

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
            Dim query As String = _
                " SELECT " & _
                "   IAIP_LK_SSCPDOCUMENTTYPE.STRDOCUMENTTYPE, " & _
                "   IAIP_BINARYFILES.STRFILENAME, " & _
                "   IAIP_SSCP_ENFORCEMENTDOCS.CREATEDATE, " & _
                "   IAIP_SSCP_ENFORCEMENTDOCS.STRCOMMENT, " & _
                "   IAIP_BINARYFILES.NUMFILESIZE, " & _
                "   IAIP_BINARYFILES.BINARYFILEID, " & _
                "   IAIP_BINARYFILES.STRFILEEXTENSION, " & _
                "   IAIP_SSCP_ENFORCEMENTDOCS.ENFORCEMENTDOCSID, " & _
                "   IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER, " & _
                "   IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE " & _
                " FROM AIRBRANCH.IAIP_BINARYFILES " & _
                " INNER JOIN AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " & _
                " ON IAIP_BINARYFILES.BINARYFILEID = IAIP_SSCP_ENFORCEMENTDOCS.NUMBINARYFILE " & _
                " INNER JOIN AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " & _
                " ON IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE = IAIP_LK_SSCPDOCUMENTTYPE.DOCUMENTTYPEID " & _
                " WHERE IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", enfNum)
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
                .FileSize = DB.GetNullable(Of Decimal?)(row("NUMFILESIZE"))
                .DocumentTypeId = row("NUMDOCUMENTTYPE")
                .Comment = DB.GetNullable(Of String)(row("STRCOMMENT"))
                .UploadDate = General.NormalizeDate(DB.GetNullable(Of Date)(row("CREATEDATE")))
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

        Public Function DownloadFile(ByVal id As Integer, ByVal path As String) As Boolean
            Dim query As String = " SELECT IAIP_BINARYFILES.BLOBDOCUMENT " & _
                                " FROM AIRBRANCH.IAIP_BINARYFILES " & _
                                " WHERE IAIP_BINARYFILES.BINARYFILEID = :pBinId "
            Dim parameter As OracleParameter = New OracleParameter("pBinId", id)

            Dim byteArray As Byte() = DB.GetByteArrayFromBlob(query, parameter)

            Try
                Using fs As New FileStream(path, FileMode.Create, FileAccess.Write)
                    Using bw As New BinaryWriter(fs)
                        bw.Write(byteArray)
                        bw.Close()
                    End Using ' bw
                    fs.Close()
                End Using ' fs

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

#End Region

#Region "Upload files"

        Private Function UploadDocument(ByVal doc As Document, ByVal pathToFile As String, ByVal metaDataQuery As String, ByVal metaDataId As String, Optional ByVal sender As Object = Nothing) As Boolean
            If String.IsNullOrEmpty(pathToFile) Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            ' -- Transaction
            ' 1. Get seq value
            ' 2. Upload the binary file, use seq as id
            ' 3. Upload file metadata, including binary file id
            ' -- Commit transaction

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim binarySeqId As Integer = GetNextBinaryFileSequenceValue()
            Dim parameters As OracleParameter()

            queryList.Add( _
                " INSERT INTO AIRBRANCH.IAIP_BINARYFILES " & _
                " (BINARYFILEID,STRFILENAME,STRFILEEXTENSION,NUMFILESIZE,BLOBDOCUMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " & _
                " VALUES (:pBinId,:pFileName,:pFileExt,:pFileSize,:pBinFile,:pUser,:pUpdateDate,:pCreateDate) " _
            )
            parameters = New OracleParameter() { _
                New OracleParameter("pBinId", binarySeqId), _
                New OracleParameter("pFileName", doc.FileName), _
                New OracleParameter("pFileExt", doc.FileExtension), _
                New OracleParameter("pFileSize", doc.FileSize), _
                New OracleParameter("pBinFile", OracleDbType.Blob, DB.ReadByteArrayFromFile(pathToFile), ParameterDirection.Input), _
                New OracleParameter("pUser", CurrentUser.UserID), _
                New OracleParameter("pUpdateDate", Date.Now), _
                New OracleParameter("pCreateDate", Date.Now) _
            }
            parametersList.Add(parameters)

            queryList.Add(metaDataQuery)
            parameters = New OracleParameter() { _
                New OracleParameter("pBinId", binarySeqId), _
                New OracleParameter("pMetaDataId", metaDataId), _
                New OracleParameter("pDocTypeId", doc.DocumentTypeId), _
                New OracleParameter("pComment", doc.Comment), _
                New OracleParameter("pUser", CurrentUser.UserID), _
                New OracleParameter("pUpdateDate", Date.Now), _
                New OracleParameter("pCreateDate", Date.Now) _
            }
            parametersList.Add(parameters)

            Dim result As Boolean = DB.RunCommand(queryList, parametersList)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

        Public Function UploadEnforcementDocument(ByVal doc As EnforcementDocument, ByVal pathToFile As String, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            Dim metaDataQuery As String = _
                            " INSERT INTO AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " & _
                            " (NUMBINARYFILE,STRENFORCEMENTNUMBER,NUMDOCUMENTTYPE,STRCOMMENT,UPDATEUSER,UPDATEDATE,CREATEDATE) " & _
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
            Dim parameter As OracleParameter = New OracleParameter("pBinId", id)

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
            Dim query As String = _
                " UPDATE AIRBRANCH.IAIP_SSCP_ENFORCEMENTDOCS " & _
                " SET NUMDOCUMENTTYPE = :pDocTypeId, " & _
                " STRCOMMENT = :pComment, " & _
                " UPDATEUSER = :pUser, " & _
                " UPDATEDATE = :pUpdateDate " & _
                " WHERE ENFORCEMENTDOCSID = :pDocId "
            Return UpdateDocument(doc, query, sender)
        End Function

        Public Function UpdateDocument(ByVal doc As Document, ByVal query As String, Optional ByVal sender As Object = Nothing) As Boolean
            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim parameters As OracleParameter() = { _
                New OracleParameter("pDocTypeId", doc.DocumentTypeId), _
                New OracleParameter("pComment", doc.Comment), _
                New OracleParameter("pUser", CurrentUser.UserID), _
                New OracleParameter("pUpdateDate", Date.Now), _
                New OracleParameter("pDocId", doc.DocumentId) _
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
            Dim query As String = "SELECT DOCUMENTTYPEID, " & _
                " STRDOCUMENTTYPE " & _
                " FROM AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " & _
                " WHERE FACTIVE = '" & Boolean.TrueString & "' " & _
                " ORDER BY NUMORDINAL, STRDOCUMENTTYPE "
            Return DB.GetLookupDictionary(query)
        End Function

        Public Function GetEnforcementDocumentTypes() As List(Of DocumentType)
            Dim docTypesList As New List(Of DocumentType)
            Dim docType As New DocumentType

            Dim query As String = "SELECT DOCUMENTTYPEID, " & _
                " STRDOCUMENTTYPE, " & _
                " FACTIVE, " & _
                " NUMORDINAL " & _
                " FROM AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " & _
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
                .DocumentType = DB.GetNullable(Of String)(row("STRDOCUMENTTYPE"))
                .DocumentTypeId = row("DOCUMENTTYPEID")
                Try
                    .Ordinal = DB.GetNullable(Of Short?)(row("NUMORDINAL"))

                Catch ex As Exception

                End Try
            End With
        End Sub

        Public Function UpdateEnforcementDocumentType(ByVal d As DocumentType, Optional ByVal sender As Object = Nothing) As Boolean
            If d Is Nothing Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String = _
                " UPDATE AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " & _
                " SET STRDOCUMENTTYPE  = :pDocType, " & _
                "   FACTIVE            = :pActive, " & _
                "   NUMORDINAL         = :pPosition " & _
                " WHERE DOCUMENTTYPEID = :pId "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("pDocType", d.DocumentType), _
                New OracleParameter("pActive", d.Active.ToString), _
                New OracleParameter("pPosition", d.Ordinal), _
                New OracleParameter("pId", d.DocumentTypeId) _
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

            Dim query As String = _
                " INSERT INTO AIRBRANCH.IAIP_LK_SSCPDOCUMENTTYPE " & _
                " (STRDOCUMENTTYPE, FACTIVE, NUMORDINAL ) " & _
                " VALUES (:pName, :pActive, :pOrdinal) "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("pName", d.DocumentType), _
                New OracleParameter("pActive", d.Active.ToString), _
                New OracleParameter("pOrdinal", d.Ordinal) _
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
