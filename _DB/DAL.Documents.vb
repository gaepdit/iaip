Imports Oracle.DataAccess.Client
Imports System.Collections.Generic
Imports System.IO
Imports Oracle.DataAccess.Types

Namespace DAL
    Module Documents

#Region "File Filters"
        ' File filters for open/save dialog boxes
        ' Document extensions from http://en.wikipedia.org/wiki/List_of_file_formats#Document

        Public Function SaveFileFilters(ByVal key As String) As String
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

        Public Function OpenFileFilters() As List(Of String)
            Dim fileFilters As New List(Of String)
            With fileFilters
                .Add("Common Document Formats (*.docx, *.pdf, *.doc)|*.pdf;*.doc;*.docx")
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

#Region "Read Enforcement Documents"
        ' Work in progress. Working on SSPP right now. Will need to use that as an example.

        Public Function GetEnforcementDocuments(ByVal enforcementId As String) As Dictionary(Of Integer, EnforcementDocument)
            Dim enforcementDocuments As Dictionary(Of Integer, EnforcementDocument) = Nothing
            Dim enforcementDocument As EnforcementDocument = Nothing

            Dim query As String = <s><![CDATA[
                SELECT IAIP_BINARYFILES.STRFILENAME,
                  IAIP_BINARYFILES.STRFILEEXTENSION,
                  IAIP_BINARYFILES.NUMFILESIZE,
                  IAIP_SSCP_ENFORCEMENTDOCS.ENFORCEMENTDOCSID,
                  IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER,
                  IAIP_SSCP_ENFORCEMENTDOCS.NUMDOCUMENTTYPE,
                  IAIP_SSCP_ENFORCEMENTDOCS.STRCOMMENT,
                  IAIP_SSCP_ENFORCEMENTDOCS.UPDATEDATE,
                  IAIP_BINARYFILES.BINARYFILEID
                FROM IAIP_BINARYFILES
                INNER JOIN IAIP_SSCP_ENFORCEMENTDOCS
                ON IAIP_BINARYFILES.BINARYFILEID = IAIP_SSCP_ENFORCEMENTDOCS.NUMBINARYFILE
                WHERE IAIP_SSCP_ENFORCEMENTDOCS.STRENFORCEMENTNUMBER = :pId
            ]]></s>.Value
            Dim parameter As New OracleParameter("pId", enforcementId)
            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)

            For Each row As DataRow In dataTable.Rows
                FillEnforcementDocumentFromDataRow(row, enforcementDocument)
                enforcementDocuments.Add(row.Item(0), enforcementDocument)
            Next

            Return enforcementDocuments
        End Function

        Private Sub FillEnforcementDocumentFromDataRow(ByVal row As DataRow, ByRef enfDoc As EnforcementDocument)
            enfDoc = New EnforcementDocument
            FillDocumentFromDataRow(row, CType(enfDoc, EnforcementDocument))

            With enfDoc
                .DocumentId = CInt(row("ENFORCEMENTDOCSID"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
            End With
        End Sub

#End Region

#Region "Read Permit Documents"

        Public Function GetPermitDocuments(ByVal applicationNumber As String) As List(Of PermitDocument)
            Dim permitDocumentsList As New List(Of PermitDocument)
            Dim permitDocument As New PermitDocument

            Dim query As String = <s><![CDATA[
                SELECT 
                  IAIP_LK_SSPPDOCUMENTTYPE.STRDOCUMENTTYPE,
                  IAIP_BINARYFILES.STRFILENAME,
                  IAIP_SSPP_PERMITDOCS.UPDATEDATE,
                  IAIP_SSPP_PERMITDOCS.STRCOMMENT,
                  IAIP_BINARYFILES.NUMFILESIZE,
                  IAIP_BINARYFILES.BINARYFILEID,
                  IAIP_BINARYFILES.STRFILEEXTENSION,
                  IAIP_SSPP_PERMITDOCS.PERMITDOCSID,
                  IAIP_SSPP_PERMITDOCS.STRAPPLICATIONNUMBER,
                  IAIP_SSPP_PERMITDOCS.NUMDOCUMENTTYPE
                FROM IAIP_BINARYFILES
                INNER JOIN IAIP_SSPP_PERMITDOCS
                ON IAIP_BINARYFILES.BINARYFILEID = IAIP_SSPP_PERMITDOCS.NUMBINARYFILE
                INNER JOIN IAIP_LK_SSPPDOCUMENTTYPE
                ON IAIP_SSPP_PERMITDOCS.NUMDOCUMENTTYPE = IAIP_LK_SSPPDOCUMENTTYPE.DOCUMENTTYPEID
                WHERE IAIP_SSPP_PERMITDOCS.STRAPPLICATIONNUMBER = :pId
            ]]></s>.Value
            Dim parameter As New OracleParameter("pId", applicationNumber)
            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)

            For Each row As DataRow In dataTable.Rows
                FillPermitDocumentFromDataRow(row, permitDocument)
                permitDocumentsList.Add(permitDocument)
            Next

            Return permitDocumentsList
        End Function

        Private Sub FillPermitDocumentFromDataRow(ByVal row As DataRow, ByRef permitDoc As PermitDocument)
            permitDoc = New PermitDocument
            FillDocumentFromDataRow(row, CType(permitDoc, PermitDocument))

            With permitDoc
                .DocumentId = CInt(row("PERMITDOCSID"))
                .ApplicationNumber = row("STRAPPLICATIONNUMBER")
            End With
        End Sub

#End Region

#Region "Read Generic Documents"

        Private Sub FillDocumentFromDataRow(ByVal row As DataRow, ByRef doc As Document)
            With doc
                .BinaryFileId = Convert.ToInt32(row("BINARYFILEID"))
                .FileName = row("STRFILENAME")
                .FileSize = DB.GetNullable(Of Decimal?)(row("NUMFILESIZE"))
                .DocumentTypeId = row("NUMDOCUMENTTYPE")
                .Comment = DB.GetNullable(Of String)(row("STRCOMMENT"))
                .UploadDate = Apb.NormalizeDate(DB.GetNullable(Of Date)(row("UPDATEDATE")))
                .DocumentType = row("STRDOCUMENTTYPE")
            End With
        End Sub

#End Region

#Region "Download files"

        Public Function DownloadDocument(ByVal doc As Document, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing OrElse doc.BinaryFileId = 0 Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim result As Boolean = False

            Dim dialog As New SaveFileDialog()
            With dialog
                .Filter = SaveFileFilters(doc.FileExtension.ToLower)
                .DefaultExt = doc.FileExtension.ToLower
                .FileName = doc.FileName
                .InitialDirectory = GetSetting(UserSetting.FileDownloadLocation)
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                result = DownloadFile(doc.BinaryFileId, dialog.FileName)

                If result Then
                    If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                        SaveSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(dialog.FileName))
                    End If
                    System.Diagnostics.Process.Start("explorer.exe", "/select,""" & dialog.FileName.ToString & """")
                End If
            End If

            dialog.Dispose()

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

        Public Function DownloadFile(ByVal id As Integer, ByVal path As String) As Boolean
            Dim query As String = "SELECT IAIP_BINARYFILES.DOCUMENT " & _
                                " FROM IAIP_BINARYFILES " & _
                                " WHERE IAIP_BINARYFILES.BINARYFILEID = :pId "
            Dim parameter As OracleParameter = New OracleParameter("pId", id)

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

        Public Function UploadPermitDocument(ByVal doc As PermitDocument, ByVal pathToFile As String, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False
            If String.IsNullOrEmpty(pathToFile) Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            ' -- Transaction
            ' 1. Get seq value
            ' 2. Upload the binary file, use seq as id
            ' 3. Upload file metadata, including binary file id
            ' - Commit transaction

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim binarySeqId As Integer = GetNextPermitSequenceValue()
            Dim parameters As OracleParameter()

            queryList.Add( _
                "INSERT INTO IAIP_BINARYFILES " & _
                " (BINARYFILEID,STRFILENAME,STRFILEEXTENSION,NUMFILESIZE,DOCUMENT,UPDATEUSER) " & _
                " VALUES (:pBinId,:pFileName,:pFileExt,:pFileSize,:pBinFile,:pUser) " _
            )
            parameters = New OracleParameter() { _
                New OracleParameter("pBinId", binarySeqId), _
                New OracleParameter("pFileName", doc.FileName), _
                New OracleParameter("pFileExt", doc.FileExtension), _
                New OracleParameter("pFileSize", doc.FileSize), _
                New OracleParameter("pBinFile", OracleDbType.Blob, ReadByteArrayFromFile(pathToFile), ParameterDirection.Input), _
                New OracleParameter("pUser", UserGCode) _
            }
            parametersList.Add(parameters)

            queryList.Add( _
                "INSERT INTO IAIP_SSPP_PERMITDOCS " & _
                " (NUMBINARYFILE,STRAPPLICATIONNUMBER,NUMDOCUMENTTYPE,STRCOMMENT,UPDATEUSER) " & _
                " VALUES (:pBinId,:pAppNumber,:pDocTypeId,:pComment,:pUser) " _
            )
            parameters = New OracleParameter() { _
                New OracleParameter("pBinId", binarySeqId), _
                New OracleParameter("pAppNumber", doc.ApplicationNumber), _
                New OracleParameter("pDocTypeId", doc.DocumentTypeId), _
                New OracleParameter("pComment", doc.Comment), _
                New OracleParameter("pUser", UserGCode) _
            }
            parametersList.Add(parameters)

            Dim result As Boolean = DB.RunCommandList(queryList, parametersList)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

        Private Function GetNextPermitSequenceValue() As Integer
            Dim query As String = "SELECT AIRBRANCH.IAIP_BINARYFILES_ID_SEQ.NEXTVAL FROM DUAL"
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

#End Region

#Region "Delete files"

        Public Function DeleteDocument(ByVal id As Integer, Optional ByVal sender As Object = Nothing) As Boolean
            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String = "DELETE FROM AIRBRANCH.IAIP_BINARYFILES WHERE BINARYFILEID = :pId"
            Dim parameter As OracleParameter = New OracleParameter("pBinId", id)

            Dim result As Boolean = DB.RunCommand(query, parameter)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Update file description"

        Public Function UpdatePermitDocument(ByVal doc As PermitDocument, Optional ByVal sender As Object = Nothing) As Boolean
            If doc Is Nothing Then Return False

            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If

            Dim query As String = _
                "UPDATE IAIP_SSPP_PERMITDOCS " & _
                " SET NUMDOCUMENTTYPE = :pDocTypeId, " & _
                " STRCOMMENT = :pComment, " & _
                " UPDATEUSER = :pUser " & _
                " WHERE PERMITDOCSID = :pDocId "
            Dim parameters As OracleParameter() = { _
                New OracleParameter("pDocTypeId", doc.DocumentTypeId), _
                New OracleParameter("pComment", doc.Comment), _
                New OracleParameter("pUser", UserGCode), _
                New OracleParameter("pDocId", doc.DocumentId) _
            }

            Dim result As Boolean = DB.RunCommand(query, parameters)

            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If

            Return result
        End Function

#End Region

#Region "Utilities"
        Private Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Dim fs As New FileStream(pathToFile, FileMode.Open, FileAccess.Read)

            Dim byteArray As Byte() = File.ReadAllBytes(pathToFile)

            fs.Close()
            fs.Dispose()

            Return byteArray
        End Function

#End Region

    End Module
End Namespace
