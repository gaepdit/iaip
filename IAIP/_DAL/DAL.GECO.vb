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

    End Module
End Namespace
