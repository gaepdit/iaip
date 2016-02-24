Class UserPermissions

    Public Shared Function CheckAuth(capability As UserCan) As Boolean
        If UserAccounts.Contains("(118)") Then Return True ' DMU Management

        Select Case capability

            Case UserCan.SaveEnforcement, UserCan.ChangeComplianceStatus
                Return (UserBranch = "5") OrElse              ' District offices or
                    (UserProgram = "3" Or UserProgram = "4")  ' DMU or SSCP

            Case UserCan.ResolveEnforcement
                Return UserAccounts.Contains("(19)") OrElse ' SSCP Program Manager
                    UserAccounts.Contains("(114)")          ' SSCP Unit Manager

            Case UserCan.AddPollutantsToFacility
                Return (UserBranch = "1") ' Air Branch

            Case UserCan.EditHeaderData
                Return (AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1")

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liasion, SSPP Program Manager
                Return UserHasPermission(New String() {"(114)", "(19)", "(102)", "(27)", "(28)"})

        End Select
    End Function

    Private Shared Function UserHasPermission(ByVal permissionCode As String) As Boolean
        If UserAccounts.Contains(permissionCode) Then Return True
        Return False
    End Function

    Private Shared Function UserHasPermission(ByVal permissionsAllowed As String()) As Boolean
        For Each permissionCode As String In permissionsAllowed
            If UserHasPermission(permissionCode) Then Return True
        Next
        Return False
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
