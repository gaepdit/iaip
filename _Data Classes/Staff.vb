Namespace Apb

    Public Class Staff

        Public Property StaffId() As Integer
            Get
                Return _staffId
            End Get
            Set(ByVal value As Integer)
                _staffId = value
            End Set
        End Property
        Private _staffId As Integer

        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property
        Private _firstName As String

        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property
        Private _lastName As String

        'Public Property Email() As String
        '    Get
        '        Return _email
        '    End Get
        '    Set(ByVal value As String)
        '        _email = value
        '    End Set
        'End Property
        'Private _email As String

        'Public Property Phone() As String
        '    Get
        '        Return _phone
        '    End Get
        '    Set(ByVal value As String)
        '        _phone = value
        '    End Set
        'End Property
        'Private _phone As String

        Public Overrides Function ToString() As String
            Return Convert.ToString(FirstName & Convert.ToString(" ")) & LastName
        End Function

    End Class

End Namespace