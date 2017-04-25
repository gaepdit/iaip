Imports System.Text.RegularExpressions

Namespace Apb.Sspp

    Public Class Permit
        Implements IEquatable(Of Permit)

#Region " Constructors "

        Public Sub New()
        End Sub

        Public Sub New(permitNumber As String)
            Me.PermitNumber = permitNumber
        End Sub

        Public Sub New(airsNumber As String, _
                       permitNumber As String, _
                       issuedDate As Date, _
                       active As Boolean, _
                       permitTypeCode As Integer)
            Me.AirsNumber = airsNumber
            Me.PermitNumber = permitNumber
            Me.IssuedDate = issuedDate
            Me.Active = active
            Me.PermitTypeCode = permitTypeCode
        End Sub

#End Region

#Region " Properties "

        Public Property ID() As Integer
        Public Property AirsNumber() As String
        Public Property PermitNumber() As String
        Public Property IssuedDate() As Date?
        Public Property RevokedDate() As Date?
        Public Property Active() As Boolean
        Public Property PermitTypeCode() As String

#End Region

#Region " Overrides "

        Public Overrides Function ToString() As String
            Return PermitNumber
        End Function

#End Region

#Region " IEquatable Interface implementation "

        Public Overloads Function Equals(other As Permit) As Boolean _
            Implements IEquatable(Of Permit).Equals
            If other Is Nothing Then Return False
            Return Me.PermitNumber.Equals(other.PermitNumber)
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then Return MyBase.Equals(obj)
            If TypeOf obj Is Permit Then Return Equals(DirectCast(obj, Permit))
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.PermitNumber.GetHashCode()
        End Function

#End Region

#Region " Static functions "

        Public Shared Function IsValidPermitNumber(permitNumber As String) As Boolean
            ' Valid permit numbers are in the form 0000-000-0000-A-00-?
            ' (with the dashes)
            If permitNumber Is Nothing Then Return False
            Return Regex.IsMatch(permitNumber, PermitNumberPattern)
        End Function

#End Region

    End Class

End Namespace