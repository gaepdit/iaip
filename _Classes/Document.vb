Imports System.IO
Imports Oracle.DataAccess.Types

Public Class Document

    Public Shared MaxFileSize As Integer = Math.Min(OracleBlob.MaxSize, 500000000)

    Private _documentId As Integer
    Public Property DocumentId() As Integer
        Get
            Return _documentId
        End Get
        Set(ByVal value As Integer)
            _documentId = value
        End Set
    End Property

    Private _binaryFileId As Integer
    Public Property BinaryFileId() As Integer
        Get
            Return _binaryFileId
        End Get
        Set(ByVal value As Integer)
            _binaryFileId = value
        End Set
    End Property

    Private _fileName As String
    Public Property FileName() As String
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property

    Public ReadOnly Property FileExtension() As String
        Get
            Return Path.GetExtension(_fileName)
        End Get
    End Property

    Private _fileSize As Integer
    Public Property FileSize() As Integer
        Get
            Return _fileSize
        End Get
        Set(ByVal value As Integer)
            _fileSize = value
        End Set
    End Property

    Private _documentType As String
    Public Property DocumentType() As String
        Get
            Return _documentType
        End Get
        Set(ByVal value As String)
            _documentType = value
        End Set
    End Property

    Private _documentTypeId As Integer
    Public Property DocumentTypeId() As Integer
        Get
            Return _documentTypeId
        End Get
        Set(ByVal value As Integer)
            _documentTypeId = value
        End Set
    End Property

    Private _comment As String
    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
        End Set
    End Property

    Private _uploadDate As Date?
    Public Property UploadDate() As Date?
        Get
            Return _uploadDate
        End Get
        Set(ByVal value As Date?)
            _uploadDate = value
        End Set
    End Property

End Class

Public Class PermitDocument
    Inherits Document

    Private _applicationNumber As String
    Public Property ApplicationNumber() As String
        Get
            Return _applicationNumber
        End Get
        Set(ByVal value As String)
            _applicationNumber = value
        End Set
    End Property

End Class

Public Class EnforcementDocument
    Inherits Document

    Private _enforcementNumber As String
    Public Property EnforcementNumber() As String
        Get
            Return _enforcementNumber
        End Get
        Set(ByVal value As String)
            _enforcementNumber = value
        End Set
    End Property

End Class

Public Class EnforcementDocumentType

    Private _documentType As String
    Public Property DocumentType() As String
        Get
            Return _documentType
        End Get
        Set(ByVal value As String)
            _documentType = value
        End Set
    End Property

    Private _documentTypeId As Integer
    Public Property DocumentTypeId() As Integer
        Get
            Return _documentTypeId
        End Get
        Set(ByVal value As Integer)
            _documentTypeId = value
        End Set
    End Property

    Private _active As Boolean
    Public Property Active() As Boolean
        Get
            Return _active
        End Get
        Set(ByVal value As Boolean)
            _active = value
        End Set
    End Property

    'Public ReadOnly Property ActiveString() As String
    '    Get
    '        Return If(_active, "Yes", "No")
    '    End Get
    'End Property

    Private _ordinal As Short
    Public Property Ordinal() As Short
        Get
            Return _ordinal
        End Get
        Set(ByVal value As Short)
            _ordinal = value
        End Set
    End Property

End Class