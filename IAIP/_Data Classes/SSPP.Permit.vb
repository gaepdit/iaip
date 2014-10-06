Namespace Apb.SSPP

    Public Class Permit

        Public Sub New()
        End Sub

        Public Sub New(ByVal airsNumber As String, _
                       ByVal permitNumber As String, _
                       ByVal issuedDate As Date, _
                       ByVal active As Boolean, _
                       ByVal permitTypeCode As Integer)
            Me.AirsNumber = airsNumber
            Me.PermitNumber = permitNumber
            Me.IssuedDate = issuedDate
            Me.Active = active
            Me.PermitTypeCode = permitTypeCode
        End Sub

        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Private _ID As Integer

        Public Property AirsNumber() As String
            Get
                Return _AirsNumber
            End Get
            Set(ByVal value As String)
                _AirsNumber = value
            End Set
        End Property
        Private _AirsNumber As String

        Public Property PermitNumber() As String
            Get
                Return _PermitNumber
            End Get
            Set(ByVal value As String)
                _PermitNumber = value
            End Set
        End Property
        Private _PermitNumber As String

        Public Property IssuedDate() As Date?
            Get
                Return _IssuedDate
            End Get
            Set(ByVal value As Date?)
                _IssuedDate = value
            End Set
        End Property
        Private _IssuedDate As Date?

        Public Property RevokedDate() As Date?
            Get
                Return _RevokedDate
            End Get
            Set(ByVal value As Date?)
                _RevokedDate = value
            End Set
        End Property
        Private _RevokedDate As Date?

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property
        Private _Active As Boolean

        Public Property PermitTypeCode() As String
            Get
                Return _PermitTypeCode
            End Get
            Set(ByVal value As String)
                _PermitTypeCode = value
            End Set
        End Property
        Private _PermitTypeCode As String

        Public Overrides Function ToString() As String
            Return PermitNumber
        End Function


#Region " Shared functions "

        Public Shared Function ValidPermitNumber(ByVal permitNumber As String) As Boolean
            ' Valid permit numbers are in the form 0000-000-0000-A-00-?
            ' (with the dashes)
            If permitNumber Is Nothing Then Return False
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{4}-\d{3}-\d{4}-[A-Z]-\d{2}-[A-Z0-9]$")
            Return rgx.IsMatch(permitNumber)
        End Function

#End Region

    End Class

End Namespace