Namespace Apb.SSPP

    Public Class Permit
        Implements IEquatable(Of Permit)

        Public Sub New()
        End Sub

        Public Sub New(ByVal permitNumber As String)
            Me.PermitNumber = permitNumber
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
        Public Property AirsNumber() As String
        Public Property PermitNumber() As String
        Public Property IssuedDate() As Date?
        Public Property RevokedDate() As Date?
        Public Property Active() As Boolean
        Public Property PermitTypeCode() As String

        Public Overrides Function ToString() As String
            Return PermitNumber
        End Function

#Region " IEquatable Interface implementation "

        Public Overloads Function Equals(ByVal other As Permit) As Boolean _
            Implements IEquatable(Of Permit).Equals
            If other Is Nothing Then Return False
            Return Me.PermitNumber.Equals(other.PermitNumber)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return MyBase.Equals(obj)
            If TypeOf obj Is Permit Then Return Equals(DirectCast(obj, Permit))
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.PermitNumber.GetHashCode()
        End Function

#End Region

#Region " Shared functions "

        Public Shared Function IsValidPermitNumber(ByVal permitNumber As String) As Boolean
            ' Valid permit numbers are in the form 0000-000-0000-A-00-?
            ' (with the dashes)
            If permitNumber Is Nothing Then Return False
            ' Test regex here: http://regexr.com/39l4d
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{4}-\d{3}-\d{4}-[A-Z]-\d{2}-[A-Z0-9]$")
            Return rgx.IsMatch(permitNumber)
        End Function

#End Region

    End Class

End Namespace