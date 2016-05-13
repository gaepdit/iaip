Imports System.Collections.Generic
Imports System.Linq

Public Class IaipRoles

#Region " Properties "
    Private _roles As HashSet(Of Integer)

    Public Property RoleCodes As HashSet(Of Integer)
        Get
            Return _roles
        End Get
        Set(value As HashSet(Of Integer))
            If value Is Nothing Then
                _roles = New HashSet(Of Integer)
            Else
                _roles = value
            End If
            _roles.Remove(0)
            If _roles.Count = 0 Then _roles.Add(0)
        End Set
    End Property

    Public Property DbString As String
        Get
            If _roles Is Nothing Then
                Return ""
            Else
                Return String.Join("", _roles.ToList.ConvertAll(Function(i As Integer) "(" & i.ToString & ")"))
            End If
        End Get
        Set(value As String)
            If String.IsNullOrEmpty(value) Then
                _roles = New HashSet(Of Integer)
            Else
                _roles = New HashSet(Of Integer)(Array.ConvertAll(value.Split(New Char() {"("c, ")"c}, StringSplitOptions.RemoveEmptyEntries), Function(s) Integer.Parse(s)))
            End If
            _roles.Remove(0)
            If _roles.Count = 0 Then _roles.Add(0)
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
        RoleCodes = New HashSet(Of Integer)({0})
    End Sub

    Public Sub New(dbString As String)
        Me.DbString = dbString
    End Sub

    Public Sub New(roles As HashSet(Of Integer))
        Me.RoleCodes = roles
    End Sub

    Public Shared Narrowing Operator CType(s As String) As IaipRoles
        Return New IaipRoles(s)
    End Operator

    Public Shared Narrowing Operator CType(i As HashSet(Of Integer)) As IaipRoles
        Return New IaipRoles(i)
    End Operator

#End Region

#Region " Methods "

    Public Function HasRole(roleCode As Integer) As Boolean
        Return Me.RoleCodes.Contains(roleCode)
    End Function

    Public Function HasRole(roleCodes As Integer()) As Boolean
        Return Me.RoleCodes.ContainsAny(roleCodes)
    End Function

#End Region

End Class