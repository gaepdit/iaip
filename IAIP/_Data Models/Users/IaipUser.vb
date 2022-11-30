Partial Public Class IaipUser
    Inherits Person

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

    Public Function HasRole(permissionCode As Integer) As Boolean
        Return IaipRoles.HasRole(permissionCode)
    End Function

    Public Function HasRole(permissionCodes As Integer()) As Boolean
        Return IaipRoles.HasRole(permissionCodes)
    End Function

    ' Numbered roles are described in AIRBRANCH.dbo.LOOKUPIAIPACCOUNTS
    ' and stored per user in AIRBRANCH.dbo.IAIPPERMISSIONS
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
                Return HasRole({133, 134, 135, 136, 137, 138, 140})

            Case Else
                Return False
        End Select
    End Function

    Public Function HasPermission(capability As UserCan) As Boolean
        If HasRole(118) Then Return True ' EPD-IT

        Select Case capability

            ' === Compliance caps
            Case UserCan.SaveEnforcement
                ' District offices, SSCP, or Branch Chief
                Return BranchID = 5 OrElse
                    ProgramID = 4 OrElse
                    HasRole({19, 20, 113, 114, 102})

            Case UserCan.ResolveEnforcement
                ' SSCP Program Manager, SSCP Unit Manager, or Branch Chief
                Return HasRole({19, 114, 102})

            ' === Facility caps
            Case UserCan.AddPollutantsToFacility
                ' Air Branch
                Return BranchID = 1 OrElse ' Air Branch
                    BranchID = 5 ' District Offices

            Case UserCan.EditFacilityHeaderData
                ' Branch Chief; SSCP Unit Manager; SSCP, ISMP, or SSPP Program Manager; District Manager
                Return HasRole({102, 114, 2, 19, 28}) OrElse
                    HasRoleType(RoleType.DistrictManager)

            Case UserCan.EditFacilityAddress
                ' ISMP Program Manager, SSCP Program Manager, District Liaison, SSPP Program Manager,
                ' SSPP Administrative, Branch Chief, SSCP Unit Manager, SSPP Unit Manager
                Return HasRole({2, 19, 27, 28, 29, 102, 114, 121})

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liaison, SSPP Program Manager
                Return HasRole({114, 19, 102, 27, 28})
                
            Case UserCan.CreateFacility
                ' SSPP Program Manager, SSPP Administrative, SSPP Unit Manager, SSCP Program Manager
                Return HasRole({28, 29, 121, 19}) OrElse
                    (HasRoleType(RoleType.UnitManager) AndAlso UnitId = 30) ' SSCP Air Toxics Unit Manager

            Case UserCan.DeleteFacilityNote
                ' ISMP Program Manager, SSCP Program Manager, District Liaison, SSPP Program Manager,
                ' SSPP Administrative, Branch Chief, SSCP Unit Manager, SSPP Unit Manager
                Return HasRole({2, 19, 27, 28, 29, 102, 114, 121})

            ' === User management caps
            Case UserCan.EditAllUsers
                ' Branch Chief, APB Admin, all APB program managers
                Return HasRoleType(RoleType.BranchChief) OrElse
                    HasRoleType(RoleType.BranchAdmin) OrElse
                    HasRoleType(RoleType.ProgramManager)

            Case UserCan.EditDirectReports
                ' APB unit managers, District managers
                Return HasPermission(UserCan.EditAllUsers) OrElse
                    HasRoleType(RoleType.UnitManager) OrElse
                    HasRoleType(RoleType.DistrictManager)

            ' === Finance caps
            Case UserCan.OverrideFeeAmount
                ' Branch Chief, APB Admin, all APB program managers
                Return HasRoleType(RoleType.BranchChief) OrElse
                    HasRoleType(RoleType.BranchAdmin) OrElse
                    HasRoleType(RoleType.ProgramManager)

            Case UserCan.EditFinancialData
                ' All Finance staff
                Return HasRole({123, 124, 125})

            ' === SSPP caps
            Case UserCan.CreatePermitApp
                ' SSPP Program Manager, SSPP Administrative, Branch Chief
                Return HasRole({28, 29, 102})

            Case UserCan.EditPermitApp
                ' SSPP users, Branch Chief 
                Return ProgramID = 5 OrElse
                    HasRole({28, 29, 121, 122, 102})

            Case UserCan.UploadPermitFile
                ' SSPP Program Manager, SSPP Unit Manager, SSPP Administrative, Branch Chief
                Return HasRole({28, 121, 29, 102})

            Case UserCan.DeletePermitFile
                ' SSPP Program Manager, SSPP Administrative, Web Publisher
                Return HasRole({28, 29, 120})

            ' === Application Fees
            Case UserCan.VoidUnpaidApplicationFeeInvoices
                ' All Finance staff, SSPP Program Manager, SSPP Unit Manager
                Return HasRole({123, 124, 125, 28, 121})

            ' === Annual Fees
            Case UserCan.ManageAnnualFees
                'Planning & Support Manager, Financial Administrative, Financial Manager
                Return HasRole({11, 123, 124})

            Case UserCan.EditAnnualFees
                ' Planning & Support Manager, SSCP Program Manager, EPD-IT,
                ' Financial Administrative, Financial Manager, SBEAP Assistance Provider
                Return HasRole({11, 19, 119, 123, 124, 142})

            Case UserCan.EditAnnualFeesDeposits
                ' ISMP Program Manager, Planning & Support Manager, Financial Administrative,
                ' Financial Manager, Financial Staff, Planning & Support Hourly Worker
                Return HasRole({2, 11, 123, 124, 125, 127})

            ' === ECSU
            Case UserCan.AccessEmissionsInventory
                ' 144 - ECSU Staff
                ' 145 - ECSU Manager
                Return HasRole({144, 145})

            Case UserCan.AccessGecoUserManagement
                ' 123 - Financial Administrative
                ' 124 - Financial Manager
                ' 144 - ECSU Staff
                ' 145 - ECSU Manager
                Return HasRole({123, 124, 144, 145})

            Case Else
                Return False
        End Select
    End Function

End Class

Public Enum UserCan
    SaveEnforcement
    ResolveEnforcement
    AddPollutantsToFacility
    EditFacilityHeaderData
    EditFacilityAddress
    ShutDownFacility
    CreateFacility
    EditAllUsers
    EditDirectReports
    OverrideFeeAmount
    EditFinancialData
    CreatePermitApp
    EditPermitApp
    UploadPermitFile
    DeletePermitFile
    ManageAnnualFees
    EditAnnualFees
    EditAnnualFeesDeposits
    AccessEmissionsInventory
    AccessGecoUserManagement
    DeleteFacilityNote
    VoidUnpaidApplicationFeeInvoices
End Enum

Public Enum RoleType
    BranchChief
    ProgramManager
    UnitManager
    BranchAdmin
    DistrictManager
End Enum
