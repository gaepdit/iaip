Partial Public Class IaipUser
    Inherits Person

#Region " Properties "

    Public Property UserID As Integer
    Public Property Username As String
    Public Property ActiveEmployee As Boolean
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

    Public Function CheckIf(capability As UserCan) As Boolean
        If HasRoles(118) Then Return True ' DMU Management

        Select Case capability

            Case UserCan.SaveEnforcement Or UserCan.ChangeComplianceStatus
                Return (BranchID = 5) OrElse              ' District offices or
                    (ProgramID = 3 Or ProgramID = 4)  ' DMU or SSCP

            Case UserCan.ResolveEnforcement
                Return HasRoles(19) OrElse ' SSCP Program Manager
                    HasRoles(114)          ' SSCP Unit Manager

            Case UserCan.AddPollutantsToFacility
                Return (BranchID = 1) ' Air Branch

            Case UserCan.EditHeaderData
                Return (AccountFormAccess(29, 2) = "1" Or AccountFormAccess(29, 3) = "1" Or AccountFormAccess(29, 4) = "1")

            Case UserCan.ShutDownFacility
                ' SSCP Unit Manager, SSCP Program Manager, Branch Chief, District Liasion, SSPP Program Manager
                Return HasRoles(New Integer() {114, 19, 102, 27, 28})

            Case UserCan.CreateUsers
                Return False

            Case UserCan.EditUsers
                Return False

        End Select
    End Function

    Public Function HasRoles(permissionCode As Integer) As Boolean
        Return IaipRoles.HasRoles(permissionCode)
    End Function

    Public Function HasRoles(permissionCodes As Integer()) As Boolean
        Return IaipRoles.HasRoles(permissionCodes)
    End Function

#End Region

End Class