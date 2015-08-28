Public Class Person

    Public Property FirstName() As String
    Public Property LastName() As String
    Public Property EmailAddress() As String
    Public Property PhoneNumber() As String

    Public Overrides Function ToString() As String
        Return AlphaName
    End Function

    Public ReadOnly Property FullName() As String
        Get
            Return Convert.ToString(FirstName & " " & LastName)
        End Get
    End Property

    Public ReadOnly Property AlphaName() As String
        Get
            Return Convert.ToString(LastName & ", " & FirstName)
        End Get
    End Property

    Public Property Prefix() As String
    Public Property Suffix() As String
    Public Property Title() As String

End Class
