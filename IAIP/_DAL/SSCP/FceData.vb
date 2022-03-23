Imports System.Data.SqlClient

Namespace DAL.Sscp

    Module FceData

        ''' <summary>
        ''' Tests whether an FCE exists.
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number to test.</param>
        ''' <returns>Returns True if the FCE exists; otherwise, returns False.</returns>
        Public Function FceExists(fceNumber As Integer) As Boolean
            If fceNumber = 0 Then Return False

            Dim query As String = "SELECT 1 " &
                " FROM SSCPFCEMASTER " &
                " WHERE STRFCENUMBER = @fceNumber " &
                " and (IsDeleted is null or IsDeleted = 0) "

            Dim parameter As New SqlParameter("@fceNumber", fceNumber)

            Return DB.ValueExists(query, parameter)
        End Function

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As Integer) As Apb.ApbFacilityId
            Return GetFacilityIdByFceId(fceNumber.ToString)
        End Function

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM SSCPFCEMASTER WHERE STRFCENUMBER = @fceNumber"
            Dim parameter As New SqlParameter("@fceNumber", fceNumber)

            Return New Apb.ApbFacilityId(DB.GetString(query, parameter))
        End Function

    End Module

End Namespace
