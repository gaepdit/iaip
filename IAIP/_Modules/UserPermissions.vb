Class UserPermissions

    Public Shared Function CheckAuth(capability As UserCan) As Boolean
        If CurrentUser.HasPermissionCode(118) Then Return True ' DMU Management

        Select Case capability

            Case UserCan.SaveEnforcement Or UserCan.ChangeComplianceStatus
                Return (CurrentUser.BranchID = 5) OrElse              ' District offices or
                    (CurrentUser.ProgramID = 3 Or CurrentUser.ProgramID = 4)  ' DMU or SSCP

            Case UserCan.ResolveEnforcement
                Return CurrentUser.HasPermissionCode(19) OrElse ' SSCP Program Manager
                    CurrentUser.HasPermissionCode(114)          ' SSCP Unit Manager

            Case UserCan.AddPollutantsToFacility
                Return (CurrentUser.BranchID = 1) ' Air Branch

            Case UserCan.EditHeaderData
                Return (AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1")

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liasion, SSPP Program Manager
                Return CurrentUser.HasPermissionCode(New Integer() {114, 19, 102, 27, 28})

        End Select
    End Function

End Class

Public Enum UserCan
    SaveEnforcement
    ResolveEnforcement
    ChangeComplianceStatus
    AddPollutantsToFacility
    EditHeaderData
    ShutDownFacility
End Enum
