Imports System.Collections.Generic

Namespace DAL
    Module State

        Public Function GetCountiesAsDictionary() As Dictionary(Of Integer, String)
            Dim query As String = "SELECT CONVERT(int, STRCOUNTYCODE) AS CountyCode, 
                STRCOUNTYNAME AS County
                FROM LOOKUPCOUNTYINFORMATION
                ORDER BY STRCOUNTYNAME"

            Return DB.GetLookupDictionary(query)
        End Function

        Public Function GetCountiesAsDataTable() As DataTable
            Dim query As String = "SELECT STRCOUNTYCODE AS CountyCode, 
                STRCOUNTYNAME AS County
                FROM LOOKUPCOUNTYINFORMATION
                ORDER BY STRCOUNTYNAME"

            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
