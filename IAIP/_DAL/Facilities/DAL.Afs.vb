Imports System.Collections.Generic
Imports Iaip.Apb.Sscp
Imports Iaip.Apb.Facilities
Imports System.Data.SqlClient

Namespace DAL

    Module AfsData

        Public Function GetNextAfsActionNumber(airsNumber As Apb.ApbFacilityId) As Integer
            Dim query As String = "SELECT STRAFSACTIONNUMBER " &
                "FROM APBSUPPLAMENTALDATA " &
                "WHERE STRAIRSNUMBER = @airsNumber"
            Dim parameter As New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            Return DB.GetSingleValue(Of Integer)(query, parameter)
        End Function

        Public Function SaveNextAfsActionNumber(airsNumber As Apb.ApbFacilityId, key As Integer) As Boolean
            Dim query As String = "UPDATE APBSUPPLAMENTALDATA SET " &
                "STRAFSACTIONNUMBER = @key " &
                "WHERE STRAIRSNUMBER = @airsNumber"
            Dim parameters() As SqlParameter = {
                New SqlParameter("@key", key),
                New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            }
            Return DB.RunCommand(query, parameters)
        End Function

    End Module

End Namespace
