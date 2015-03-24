Namespace IAIP
    Public Class EdtError

        Public Property ID() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property
        Private _id As String

        Public Property ErrorCode() As String
            Get
                Return _errorCode
            End Get
            Set(ByVal value As String)
                _errorCode = value
            End Set
        End Property
        Private _errorCode As String

    End Class
End Namespace
