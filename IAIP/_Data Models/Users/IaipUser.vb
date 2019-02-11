Partial Public Class IaipUser
    Inherits Person

#Region " Properties "

    Public Property UserID As Integer
    Public Property Username As String
    Public Property ActiveEmployee As ActiveOrInactive
    Public Property BranchID As Integer
    Public Property BranchName As String
    Public Property ProgramID As Integer
    Public Property ProgramName As String
    Public Property UnitId As Integer
    Public Property UnitName As String
    Public Property OfficeNumber As String
    Public Property RequirePasswordChange As Boolean
    Public Property RequestProfileUpdate As Boolean
    Public Property IaipRoles As IaipRoles

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

    Private Sub New(another As IaipUser)
        FirstName = another.FirstName
        LastName = another.LastName
        EmailAddress = another.EmailAddress
        PhoneNumber = another.PhoneNumber
        Prefix = another.Prefix
        Suffix = another.Suffix
        Title = another.Title

        UserID = another.UserID
        Username = another.Username
        ActiveEmployee = another.ActiveEmployee
        BranchID = another.BranchID
        BranchName = another.BranchName
        ProgramID = another.ProgramID
        ProgramName = another.ProgramName
        UnitId = another.UnitId
        UnitName = another.UnitName
        OfficeNumber = another.OfficeNumber

        RequirePasswordChange = another.RequirePasswordChange
        RequestProfileUpdate = another.RequestProfileUpdate
        IaipRoles = another.IaipRoles
    End Sub

    Public Overloads Function Clone() As IaipUser
        Return New IaipUser(Me)
    End Function

#End Region

#Region " Methods "

    Public Function HasRole(permissionCode As Integer) As Boolean
        Return IaipRoles.HasRole(permissionCode)
    End Function

    Public Function HasRole(permissionCodes As Integer()) As Boolean
        Return IaipRoles.HasRole(permissionCodes)
    End Function

    Public Function HasRoleType(roleType As RoleType) As Boolean
        Select Case roleType
            Case RoleType.BranchAdmin
                Return HasRole(42)

            Case RoleType.BranchChief
                Return HasRole(102)

            Case RoleType.ProgramManager
                Return HasRole({2, 11, 19, 28, 45, 57, 104, 143})

            Case RoleType.UnitManager
                Return HasRole({47, 63, 106, 114, 115, 121, 128})

            Case RoleType.DistrictManager
                Return HasRole({134, 136, 133, 135, 137, 138, 140})

        End Select

        Return False
    End Function

    Public Function HasPermission(capability As UserCan) As Boolean
        If HasRole(118) Then Return True ' DMU Management

        Select Case capability

            ' === Compliance caps
            Case UserCan.SaveEnforcement
                ' District offices, SSCP, or Branch Chief
                Return (BranchID = 5) Or (ProgramID = 4) Or HasRole(102)

            Case UserCan.ResolveEnforcement
                ' SSCP Program Manager, SSCP Unit Manager, or Branch Chief
                Return HasRole({19, 114, 102})

            ' === Facility caps
            Case UserCan.AddPollutantsToFacility
                ' Air Branch
                Return (BranchID = 1 OrElse BranchID = 5)

            Case UserCan.EditFacilityHeaderData
                ' Branch Chief; SSCP Unit Manager; or SSCP, ISMP, or SSPP Program Manager
                Return HasRole({102, 114, 2, 19, 28})

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liasion, SSPP Program Manager
                Return HasRole({114, 19, 102, 27, 28})

            ' === User management caps
            Case UserCan.EditAllUsers
                ' Branch Chief, APB Admin, all APB program managers
                Return HasRoleType(RoleType.BranchChief) Or
                    HasRoleType(RoleType.BranchAdmin) Or
                    HasRoleType(RoleType.ProgramManager)

            Case UserCan.EditDirectReports
                ' APB unit managers, District managers
                Return HasPermission(UserCan.EditAllUsers) Or
                    HasRoleType(RoleType.UnitManager) Or
                    HasRoleType(RoleType.DistrictManager)

            ' === Finance caps
            Case UserCan.OverrideFeeAmount
                ' Branch Chief, APB Admin, all APB program managers
                Return HasRoleType(RoleType.BranchChief) Or
                    HasRoleType(RoleType.BranchAdmin) Or
                    HasRoleType(RoleType.ProgramManager)

            Case UserCan.EditFinancialData
                ' All Finance staff
                Return HasRole({123, 124, 125})

            Case Else
                Return False
        End Select
    End Function

#End Region

End Class

#Region " Enums "

Public Enum UserCan
    SaveEnforcement
    ResolveEnforcement
    AddPollutantsToFacility
    EditFacilityHeaderData
    ShutDownFacility
    EditAllUsers
    EditDirectReports
    OverrideFeeAmount
    EditFinancialData
End Enum

Public Enum RoleType
    BranchChief
    ProgramManager
    UnitManager
    BranchAdmin
    DistrictManager
End Enum

#End Region