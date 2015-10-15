Imports System.Linq

Public Class IaipUser
    Inherits Staff

    Public Property UserName() As String
    Public ReadOnly Property UserID() As Integer
        Get
            Return Me.StaffId
        End Get
    End Property
    Public Property IaipAccountCodes As Generic.List(Of Integer)
    Public WriteOnly Property PermissionsString() As String
        Set(value As String)
            IaipAccountCodes = New Generic.List(Of Integer)
            IaipAccountCodes.AddRange(Array.ConvertAll(Of String, Integer)(value.Split(New Char() {"("c, ")"c}, StringSplitOptions.RemoveEmptyEntries), Function(s) Integer.Parse(s)))
            If Not IaipAccountCodes.Any() Then IaipAccountCodes.Add(0)
        End Set
    End Property

    Public Function HasPermissionCode(permissionCode As Integer) As Boolean
        Return IaipAccountCodes.Contains(permissionCode)
    End Function

    Public Function HasPermissionCode(permissionCodes As Integer()) As Boolean
        Return IaipAccountCodes.ContainsAny(permissionCodes.ToList)
    End Function

End Class
