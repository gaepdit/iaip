Imports Microsoft.Data.SqlClient

Namespace DAL.Sspp

    Module ApplicationData

        Public Function ApplicationExists(appNumber As String) As Boolean
            Dim appNumberInt As Integer
            If String.IsNullOrWhiteSpace(appNumber) OrElse Not Integer.TryParse(appNumber, appNumberInt) Then Return False
            Return ApplicationExists(appNumberInt)
        End Function

        Public Function ApplicationExists(appNumber As Integer) As Boolean
            If appNumber = 0 Then Return False
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM SSPPAPPLICATIONMASTER WHERE STRAPPLICATIONNUMBER = @appNumber"
            Return DB.GetBoolean(query, New SqlParameter("@appNumber", appNumber))
        End Function

        Public Function GetWhenLastModified(appNumber As Integer) As DateTimeOffset
            Dim query As String = "select DATMODIFINGDATE from SSPPAPPLICATIONMASTER where STRAPPLICATIONNUMBER = @appNumber"
            Dim param As New SqlParameter("@appNumber", appNumber)
            Return DB.GetSingleValue(Of DateTimeOffset)(query, param)
        End Function

        Public Sub DeleteProgramSubparts(appnum As String, programkey As String)
            Dim query As String = "Delete from SSPPSubpartData " &
            "where strSubpartKey = @pKey "
            Dim parameter As New SqlParameter("@pKey", appnum & programkey)
            DB.RunCommand(query, parameter)
        End Sub

        Public Sub SaveProgramSubpartData(appnum As String, programKey As String, subpart As String, activity As String)
            Dim query As String = "INSERT " &
            "INTO SSPPSUBPARTDATA " &
            "  ( " &
            "    STRAPPLICATIONNUMBER, STRSUBPARTKEY, STRSUBPART, " &
            "    STRAPPLICATIONACTIVITY, UPDATEUSER, UPDATEDATETIME, " &
            "    CREATEDATETIME " &
            "  ) " &
            "  VALUES " &
            "  ( " &
            "    @appnum, @pKey, @subpart, @activity, @pUser, GETDATE(), GETDATE() " &
            "  ) "

            Dim parameters As SqlParameter() = {
            New SqlParameter("@appnum", appnum),
            New SqlParameter("@pKey", appnum & programKey),
            New SqlParameter("@subpart", subpart),
            New SqlParameter("@activity", activity),
            New SqlParameter("@pUser", CurrentUser.UserID)
        }
            DB.RunCommand(query, parameters)
        End Sub

    End Module

End Namespace
