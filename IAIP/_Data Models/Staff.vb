Public Class Staff
    Inherits Person

    Private _staffId As Integer
    Public Property StaffId() As Integer
        Get
            Return _staffId
        End Get
        Set(ByVal value As Integer)
            _staffId = value
        End Set
    End Property

    Private _activeStatus As Boolean
    Public Property ActiveStatus() As Boolean
        Get
            Return _activeStatus
        End Get
        Set(ByVal value As Boolean)
            _activeStatus = value
        End Set
    End Property

    Private _branchId As Integer
    Public Property BranchID() As Integer
        Get
            Return _branchId
        End Get
        Set(ByVal value As Integer)
            _branchId = value
        End Set
    End Property

    Private _branchName As String
    Public Property BranchName() As String
        Get
            Return _branchName
        End Get
        Set(ByVal value As String)
            _branchName = value
        End Set
    End Property

    Private _programId As Integer
    Public Property ProgramID() As Integer
        Get
            Return _programId
        End Get
        Set(ByVal value As Integer)
            _programId = value
        End Set
    End Property

    Private _programName As String
    Public Property ProgramName() As String
        Get
            Return _programName
        End Get
        Set(ByVal value As String)
            _programName = value
        End Set
    End Property

    Private _unitId As Integer
    Public Property UnitId() As Integer
        Get
            Return _unitId
        End Get
        Set(ByVal value As Integer)
            _unitId = value
        End Set
    End Property

    Private _unitName As String
    Public Property UnitName() As String
        Get
            Return _unitName
        End Get
        Set(ByVal value As String)
            _unitName = value
        End Set
    End Property

End Class
