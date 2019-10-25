Imports System.Collections.Generic
Imports System.Linq

Public Class IaipRoles

    Public ReadOnly Property RoleCodes As HashSet(Of Integer)

    Public ReadOnly Property DbString As String
        Get
            If RoleCodes Is Nothing Then
                Return ""
            Else
                Return String.Join("", RoleCodes.ToList.ConvertAll(Function(i As Integer) "(" & i.ToString & ")"))
            End If
        End Get
    End Property

    Public Sub New(dbString As String)
        If String.IsNullOrEmpty(dbString) Then
            RoleCodes = New HashSet(Of Integer)({0})
        Else
            Dim roles As HashSet(Of Integer) = New HashSet(Of Integer)(Array.ConvertAll(dbString.Split(New Char() {"("c, ")"c}, StringSplitOptions.RemoveEmptyEntries), Function(s) Integer.Parse(s)))
            roles.Remove(0)

            If roles Is Nothing OrElse roles.Count = 0 Then
                RoleCodes = New HashSet(Of Integer)({0})
            Else
                RoleCodes = roles
            End If
        End If
    End Sub

    ' Role verification

    Public Function HasRole(roleCode As Integer) As Boolean
        Return Me.RoleCodes.Contains(roleCode)
    End Function

    Public Function HasRole(roleCodes As Integer()) As Boolean
        Return Me.RoleCodes.ContainsAny(roleCodes)
    End Function

End Class