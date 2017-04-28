Imports Iaip.Apb.Facilities

Namespace DAL
    Module Rules

        Public Function GetRuleSubpartsAsDataTable(part As RulePart) As DataTable
            Dim query As String = ""

            Select Case part
                Case RulePart.SIP
                    query = "SELECT STRSUBPART AS Subpart, STRDESCRIPTION AS Description, CONCAT(STRSUBPART, ' – ', STRDESCRIPTION) AS [Long Description] FROM LOOKUPSUBPARTSIP ORDER BY STRSUBPART"

                Case RulePart.NSPS
                    query = "SELECT STRSUBPART AS Subpart, STRDESCRIPTION AS Description, CONCAT(STRSUBPART, ' – ', STRDESCRIPTION) AS [Long Description] FROM LOOKUPSUBPART60 ORDER BY STRSUBPART"

                Case RulePart.NESHAP
                    query = "SELECT STRSUBPART AS Subpart, STRDESCRIPTION AS Description, CONCAT(STRSUBPART, ' – ', STRDESCRIPTION) AS [Long Description] FROM LOOKUPSUBPART61 ORDER BY STRSUBPART"

                Case RulePart.MACT
                    query = "SELECT STRSUBPART AS Subpart, STRDESCRIPTION AS Description, CONCAT(STRSUBPART, ' – ', STRDESCRIPTION) AS [Long Description] FROM LOOKUPSUBPART63 ORDER BY STRSUBPART"

            End Select

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.CaseSensitive = True
            dt.PrimaryKey = New DataColumn() {dt.Columns("Subpart")}
            Return dt
        End Function

    End Module
End Namespace
