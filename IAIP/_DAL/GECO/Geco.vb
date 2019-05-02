Imports System.Data.SqlClient

Namespace DAL
    Module Geco

        Public Function UpdateGecoUserEmail(email As String, newEmail As String, ByRef token As String) As UpdateGecoUserEmailResult
            Dim params As SqlParameter() = {
                New SqlParameter("@OldEmail", Trim(email)),
                New SqlParameter("@NewEmail", Trim(newEmail))
            }

            Dim result As Integer
            token = DB.SPGetString("geco.UpdateUserEmail", params, result)

            Select Case result
                Case 0
                    Return UpdateGecoUserEmailResult.Success
                Case 1
                    Return UpdateGecoUserEmailResult.InvalidEmail
                Case 2
                    Return UpdateGecoUserEmailResult.NewEmailExists
            End Select

            Return UpdateGecoUserEmailResult.DbError
        End Function

        '  0 - Successfully added new email
        '  1 - Account does not exist
        '  2 - New email address already registered
        ' -1 - Error
        Public Enum UpdateGecoUserEmailResult
            Success
            InvalidEmail
            NewEmailExists
            DbError
        End Enum

        Public Function UpdateUserGecoAccess(adminAccess As Boolean, feeAccess As Boolean, eiAccess As Boolean, esAccess As Boolean, userID As Integer, airs As Apb.ApbFacilityId) As Boolean
            Dim query As String = "UPDATE OlapUserAccess SET " &
            " INTADMINACCESS = @admin, " &
            " intFeeAccess = @fee, " &
            " intEIAccess = @ei, " &
            " intESAccess = @es " &
            " WHERE numUserID = @userID " &
            " and strAirsNumber = @airs "

            Dim params As SqlParameter() = {
            New SqlParameter("@admin", adminAccess),
            New SqlParameter("@fee", feeAccess),
            New SqlParameter("@ei", eiAccess),
            New SqlParameter("@es", esAccess),
            New SqlParameter("@userID", userID),
            New SqlParameter("@airs", airs.DbFormattedString)
        }

            Return DB.RunCommand(query, params)
        End Function

        Public Function AddUserGecoAccess(userID As Integer, airs As Apb.ApbFacilityId) As Boolean
            Dim query As String = "Insert into OlapUserAccess " &
                " (numUserId, strAirsNumber) " &
                " values " &
                " (@numUserId, @strAirsNumber) "

            Dim params2 As SqlParameter() = {
                New SqlParameter("@numUserId", userID),
                New SqlParameter("@strAirsNumber", airs.DbFormattedString)
            }

            Return DB.RunCommand(query, params2)
        End Function

        Public Function UserGecoAccessExists(userID As Integer, airs As Apb.ApbFacilityId) As Boolean
            Dim query As String = "select convert(bit, count(*)) from OLAPUSERACCESS " &
                " where NUMUSERID = @NUMUSERID and STRAIRSNUMBER = @STRAIRSNUMBER "

            Dim params As SqlParameter() = {
                New SqlParameter("@NUMUSERID", userID),
                New SqlParameter("@STRAIRSNUMBER", airs.DbFormattedString)
            }

            Return DB.GetBoolean(query, params)
        End Function

        Public Function DeleteUserGecoAccess(userID As Integer, airs As Apb.ApbFacilityId) As Boolean
            Dim query As String = "DELETE from OlapUserAccess " &
            " WHERE numUserID = @numUserID " &
            " and strAirsNumber = @strAirsNumber "

            Dim params As SqlParameter() = {
                New SqlParameter("@numUserID", userID),
                New SqlParameter("@strAirsNumber", airs.DbFormattedString)
            }

            Return DB.RunCommand(query, params)
        End Function

        Public Function GetUserGecoAccessTable(userEmail As String) As DataTable
            Dim query As String = "SELECT concat(substring(a.STRAIRSNUMBER, 5, 3), '-', substring(a.STRAIRSNUMBER, 8, 5)) as strAIRSNumber,
                   f.STRFACILITYNAME,
                   INTADMINACCESS,
                   INTFEEACCESS,
                   INTEIACCESS,
                   INTESACCESS
            FROM OLAPUSERACCESS a
                 inner join OLAPUSERLOGIN l
                         on a.NUMUSERID = l.NUMUSERID
                 inner join APBFACILITYINFORMATION f
                         on f.STRAIRSNUMBER = a.STRAIRSNUMBER
            where STRUSEREMAIL = @strUserEmail
            order by f.STRFACILITYNAME"

            Dim param As New SqlParameter("@strUserEmail", userEmail)

            Return DB.GetDataTable(query, param)
        End Function

        Public Function GetGecoAccessForFacility(airs As Apb.ApbFacilityId) As DataTable
            Dim query As String = "SELECT l.NUMUSERID,
                l.STRUSEREMAIL as Email,
                INTADMINACCESS,
                INTFEEACCESS,
                INTEIACCESS,
                INTESACCESS
            FROM OLAPUSERACCESS a
                inner join OLAPUSERLOGIN l
                    on a.NUMUSERID = l.NUMUSERID
            where STRAIRSNUMBER = @STRAIRSNUMBER
            order by Email"

            Dim param As New SqlParameter("@STRAIRSNUMBER", airs.DbFormattedString)

            Return DB.GetDataTable(query, param)
        End Function

    End Module
End Namespace
