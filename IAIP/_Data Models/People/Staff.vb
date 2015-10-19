Public Class Staff
    Inherits Person

    Public Property StaffId() As Integer
    Public Property ActiveEmployee() As Boolean
    Public Property BranchID() As Integer
    Public Property BranchName() As String
    Public Property ProgramID() As Integer
    Public Property ProgramName() As String
    Public Property UnitId() As Integer
    Public Property UnitName() As String
    Public Property OfficeNumber() As String

#Region " Constructors "

    Public Sub New()
    End Sub

    Private Sub New(another As Staff)
        FirstName = another.FirstName
        LastName = another.LastName
        EmailAddress = another.EmailAddress
        PhoneNumber = another.PhoneNumber
        Prefix = another.Prefix
        Suffix = another.Suffix
        Title = another.Title

        StaffId = another.StaffId
        ActiveEmployee = another.ActiveEmployee
        BranchID = another.BranchID
        BranchName = another.BranchName
        ProgramID = another.ProgramID
        ProgramName = another.ProgramName
        UnitId = another.UnitId
        UnitName = another.UnitName
        OfficeNumber = another.OfficeNumber
    End Sub

    Public Overloads Function Clone() As Staff
        Return New Staff(Me)
    End Function

#End Region

End Class
