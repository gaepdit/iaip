Public Class Document

    Public Shared MaxFileSize As Integer = Math.Min(Oracle.ManagedDataAccess.Types.OracleBlob.MaxSize, 500000000)

    Public Property DocumentId() As Integer
    Public Property BinaryFileId() As Integer
    Public Property FileName() As String

    Public ReadOnly Property FileExtension() As String
        Get
            Return IO.Path.GetExtension(FileName)
        End Get
    End Property

    Public Property FileSize() As Integer
    Public Property DocumentType() As String
    Public Property DocumentTypeId() As Integer
    Public Property Comment() As String
    Public Property UploadDate() As Date?

End Class
