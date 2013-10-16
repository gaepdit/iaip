Imports Oracle.DataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module Lookups

        Public Function GetPermitDocumentTypes() As Dictionary(Of Integer, String)
            Dim query As String = "SELECT DOCUMENTTYPEID, " & _
                "STRDOCUMENTTYPE " & _
                "FROM AIRBRANCH.IAIP_LK_SSPPDOCUMENTTYPE " & _
                "WHERE FACTIVE = 'Y'"
            Return GetLookupDictionary(query)
        End Function

        Private Function GetLookupDictionary(ByVal query As String, Optional ByVal addBlank As Boolean = False) _
        As Dictionary(Of Integer, String)
            Dim d As New Dictionary(Of Integer, String)
            If addBlank Then d.Add(0, "")

            Dim dataTable As DataTable = DB.GetDataTable(query)
            For Each row As DataRow In dataTable.Rows
                d.Add(row.Item(0), row(1))
            Next
            Return d
        End Function

    End Module
End Namespace
