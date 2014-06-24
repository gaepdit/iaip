Imports System.Collections.Generic

Namespace DAL
    Module Fees

        Public Function GetAllFeeYears() As List(Of String)
            Dim list As New List(Of String)
            Dim dataTable As DataTable = GetAllFeeYearsAsDataTable()
            For Each row As DataRow In dataTable.Rows
                list.Add(row("FEEYEAR"))
            Next
            Return list
        End Function

        Private Function GetAllFeeYearsAsDataTable() As DataTable
            Dim query As String = "SELECT DISTINCT(NUMFEEYEAR) AS FEEYEAR " & _
                "FROM " & DBNameSpace & ".FSLK_NSPSREASONYEAR " & _
                "ORDER BY FEEYEAR DESC"
            Return DB.GetDataTable(query)
        End Function

        Public Function UpsertFSMailout() As Boolean

            Return False
        End Function

    End Module
End Namespace