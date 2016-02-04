Imports System.Collections.Generic
Imports System.Linq

Public Class IaipRoles

#Region " Properties "

    Public Property RoleCodes As HashSet(Of Integer)

    Public Property DbString As String
        Get
            If RoleCodes Is Nothing Then
                Return ""
            Else
                Return String.Join("", RoleCodes.ToList.ConvertAll(Function(i As Integer) "(" & i.ToString & ")"))
            End If
        End Get
        Set(value As String)
            If value Is Nothing Then
                RoleCodes = New HashSet(Of Integer)
            Else
                RoleCodes = New HashSet(Of Integer)(Array.ConvertAll(value.Split(New Char() {"("c, ")"c}, StringSplitOptions.RemoveEmptyEntries), Function(s) Integer.Parse(s)))
            End If
            If RoleCodes.Count = 0 Then
                RoleCodes.Add(0)
            End If
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
        RoleCodes = New HashSet(Of Integer)
    End Sub

    Public Sub New(dbRolesString As String)
        Me.DbString = dbRolesString
    End Sub

    Public Sub New(iaipRoles As HashSet(Of Integer))
        Me.RoleCodes = iaipRoles
    End Sub

    Public Shared Narrowing Operator CType(p As String) As IaipRoles
        Return New IaipRoles(p)
    End Operator

    Public Shared Narrowing Operator CType(i As HashSet(Of Integer)) As IaipRoles
        Return New IaipRoles(i)
    End Operator

#End Region

#Region " Methods "

    Public Function HasRoles(roleCode As Integer) As Boolean
        Return RoleCodes.Contains(roleCode)
    End Function

    Public Function HasRoles(roleCodes As Integer()) As Boolean
        Return roleCodes.ContainsAny(roleCodes)
    End Function

#End Region

End Class

Public Enum UserCan
    SaveEnforcement
    ResolveEnforcement
    ChangeComplianceStatus
    AddPollutantsToFacility
    EditHeaderData
    ShutDownFacility
    CreateUsers
    EditUsers
End Enum