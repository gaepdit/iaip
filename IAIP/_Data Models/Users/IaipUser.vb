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
    Private Shared ReadOnly ProgramManagerCodes As Integer() = {2, 11, 19, 28, 45, 57, 104}
    Private Shared ReadOnly UnitManagerCodes As Integer() = {47, 63, 106, 114, 115, 121, 128}
    Private Shared ReadOnly DistrictManagerCodes As Integer() = {133, 134, 135, 136, 137, 138, 140}
    Private Shared ReadOnly SaveEnforcementRoles As Integer() = {19, 20, 113, 114, 102}
    Private Shared ReadOnly ResolveEnforcementRoles As Integer() = {19, 114, 102}
    Private Shared ReadOnly EditFacilityHeaderDataRoles As Integer() = {102, 114, 2, 19, 28}
    Private Shared ReadOnly EditFacilityAddressRoles As Integer() = {2, 19, 27, 28, 29, 102, 114, 121}
    Private Shared ReadOnly ShutDownFacilityRoles As Integer() = {114, 19, 102, 27, 28}
    Private Shared ReadOnly CreateFacility As Integer() = {28, 29, 121, 19}
    Private Shared ReadOnly DeleteFacilityNoteRoles As Integer() = {2, 19, 27, 28, 29, 102, 114, 121}
    Private Shared ReadOnly CreatePermitAppRoles As Integer() = {28, 29, 102}
    Private Shared ReadOnly EditPermitAppRoles As Integer() = {28, 29, 121, 122, 102}
    Private Shared ReadOnly UploadPermitFileRoles As Integer() = {28, 121, 29, 102}
    Private Shared ReadOnly DeletePermitFileRoles As Integer() = {28, 29, 120}
    Private Shared ReadOnly EditFinancialDataRoles As Integer() = {123, 124, 125}
    Private Shared ReadOnly VoidUnpaidApplicationFeeInvoicesRoles As Integer() = {123, 124, 125, 28, 121}
    Private Shared ReadOnly ManageAnnualFeesRoles As Integer() = {11, 123, 124}
    Private Shared ReadOnly EditAnnualFeesRoles As Integer() = {11, 19, 119, 123, 124}
    Private Shared ReadOnly EditAnnualFeesDepositsRoles As Integer() = {2, 11, 123, 124, 125, 127}
    Private Shared ReadOnly AccessEmissionsInventoryRoles As Integer() = {144, 145}
    Private Shared ReadOnly AccessGecoUserManagementRoles As Integer() = {123, 124, 144, 145}
    Private Shared ReadOnly AccessEdtToolsRoles As Integer() = {19, 28}
    Private Shared ReadOnly ResetEpaIcisAirDataRoles As Integer() = {19}
    Public Function HasRoleType(roleType As RoleType) As Boolean
        Select Case roleType
            Case RoleType.BranchAdmin
                Return HasRole(42)

            Case RoleType.BranchChief
                Return HasRole(102)

            Case RoleType.ProgramManager
                Return HasRole(ProgramManagerCodes)

            Case RoleType.UnitManager
                Return HasRole(UnitManagerCodes)

            Case RoleType.DistrictManager
                Return HasRole(DistrictManagerCodes)

            Case RoleType.EpdIt
                Return HasRole(118)

            Case Else
                Return False
        End Select
    End Function

    Public Function HasPermission(capability As UserCan) As Boolean
        If HasRoleType(RoleType.EpdIt) Then Return True ' EPD-IT

        Select Case capability

            ' === Compliance caps
            Case UserCan.SaveEnforcement
                ' District offices, SSCP, or Branch Chief
                Return BranchID = 5 OrElse ProgramID = 4 OrElse HasRole(SaveEnforcementRoles)

            Case UserCan.ResolveEnforcement
                ' SSCP Program Manager, SSCP Unit Manager, or Branch Chief
                Return HasRole(ResolveEnforcementRoles)

            ' === Facility caps
            Case UserCan.AddPollutantsToFacility
                ' Air Branch or District Offices
                Return BranchID = 1 OrElse BranchID = 5

            Case UserCan.EditFacilityHeaderData
                ' Branch Chief; SSCP Unit Manager; SSCP, ISMP, or SSPP Program Manager; District Manager
                Return HasRole(EditFacilityHeaderDataRoles) OrElse HasRoleType(RoleType.DistrictManager)

            Case UserCan.EditFacilityAddress
                ' ISMP Program Manager, SSCP Program Manager, District Liaison, SSPP Program Manager,
                ' SSPP Administrative, Branch Chief, SSCP Unit Manager, SSPP Unit Manager
                Return HasRole(EditFacilityAddressRoles)

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liaison, SSPP Program Manager
                Return HasRole(ShutDownFacilityRoles)

            Case UserCan.CreateFacility
                ' SSPP Program Manager, SSPP Administrative, SSPP Unit Manager, SSCP Program Manager
                Return HasRole(CreateFacility) OrElse
                    (HasRoleType(RoleType.UnitManager) AndAlso UnitId = 30) ' SSCP Air Toxics Unit Manager

            Case UserCan.DeleteFacilityNote
                ' ISMP Program Manager, SSCP Program Manager, District Liaison, SSPP Program Manager,
                ' SSPP Administrative, Branch Chief, SSCP Unit Manager, SSPP Unit Manager
                Return HasRole(DeleteFacilityNoteRoles)

            Case UserCan.EditFacilityColocationGroups
                Return HasPermission(UserCan.EditFacilityHeaderData)

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

            ' === SSPP caps
            Case UserCan.CreatePermitApp
                ' SSPP Program Manager, SSPP Administrative, Branch Chief
                Return HasRole(CreatePermitAppRoles)

            Case UserCan.EditPermitApp
                ' SSPP users, Branch Chief 
                Return ProgramID = 5 OrElse HasRole(EditPermitAppRoles)

            Case UserCan.UploadPermitFile
                ' SSPP Program Manager, SSPP Unit Manager, SSPP Administrative, Branch Chief
                Return HasRole(UploadPermitFileRoles)

            Case UserCan.DeletePermitFile
                ' SSPP Program Manager, SSPP Administrative, Web Publisher
                Return HasRole(DeletePermitFileRoles)

            ' === Application Fees
            Case UserCan.OverrideFeeAmount
                ' Branch Chief, APB Admin, all APB program managers
                Return HasRoleType(RoleType.BranchChief) OrElse
                    HasRoleType(RoleType.BranchAdmin) OrElse
                    HasRoleType(RoleType.ProgramManager)

            Case UserCan.EditFinancialData
                ' All Finance staff
                Return HasRole(EditFinancialDataRoles)

            Case UserCan.VoidUnpaidApplicationFeeInvoices
                ' All Finance staff, SSPP Program Manager, SSPP Unit Manager
                Return HasRole(VoidUnpaidApplicationFeeInvoicesRoles)

            ' === Annual Fees
            Case UserCan.ManageAnnualFees
                'Planning & Support Manager, Financial Administrative, Financial Manager
                Return HasRole(ManageAnnualFeesRoles)

            Case UserCan.EditAnnualFees
                ' Planning & Support Manager, SSCP Program Manager, EPD-IT,
                ' Financial Administrative, Financial Manager
                Return HasRole(EditAnnualFeesRoles)

            Case UserCan.EditAnnualFeesDeposits
                ' ISMP Program Manager, Planning & Support Manager, Financial Administrative,
                ' Financial Manager, Financial Staff, Planning & Support Hourly Worker
                Return HasRole(EditAnnualFeesDepositsRoles)

            ' === ECSU
            Case UserCan.AccessEmissionsInventory
                ' 144 - ECSU Staff
                ' 145 - ECSU Manager
                Return HasRole(AccessEmissionsInventoryRoles)

            Case UserCan.AccessGecoUserManagement
                ' 123 - Financial Administrative
                ' 124 - Financial Manager
                ' 144 - ECSU Staff
                ' 145 - ECSU Manager
                Return HasRole(AccessGecoUserManagementRoles)

            ' === EPA ICIS-Air EDT caps
            Case UserCan.AccessEdtTools
                Return HasRole(AccessEdtToolsRoles)

            Case UserCan.ResetEpaIcisAirData
                Return HasRole(ResetEpaIcisAirDataRoles)

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
    AccessEdtTools
    ResetEpaIcisAirData
    EditFacilityColocationGroups
End Enum

Public Enum RoleType
    BranchChief
    ProgramManager
    UnitManager
    BranchAdmin
    DistrictManager
    EpdIt
End Enum
