Public Class Person

#Region " Properties "

    Public Property FirstName() As String
    Public Property LastName() As String
    Public Property EmailAddress() As String
    Public Property PhoneNumber() As String
    Public Property Prefix() As String
    Public Property Suffix() As String
    Public Property Title() As String

#End Region

#Region " ReadOnly Properties and Functions "

    Public ReadOnly Property FullName() As String
        Get
            Return {FirstName, LastName}.ConcatNonEmptyStrings(" ")
        End Get
    End Property

    Public ReadOnly Property AlphaName() As String
        Get
            Dim a As String = {LastName, FirstName}.ConcatNonEmptyStrings(", ")
            Return If(a, " ")
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return AlphaName
    End Function

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

    Private Sub New(another As Person)
        FirstName = another.FirstName
        LastName = another.LastName
        EmailAddress = another.EmailAddress
        PhoneNumber = another.PhoneNumber
        Prefix = another.Prefix
        Suffix = another.Suffix
        Title = another.Title
    End Sub

    Public Function Clone() As Person
        Return New Person(Me)
    End Function

#End Region

End Class
