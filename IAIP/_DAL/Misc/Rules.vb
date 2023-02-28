Imports Iaip.Apb.Facilities

Namespace DAL
    Module Rules

        Public Function GetRuleSubpartsAsDataTable(part As RulePart) As DataTable
            Dim query As String = ""

            Select Case part
                Case RulePart.SIP
                    query = "SELECT STRSUBPART AS Subpart,
                        STRDESCRIPTION AS Description,
                        CONCAT(STRSUBPART, N' – ', STRDESCRIPTION) AS [Long Description]
                    FROM LOOKUPSUBPARTSIP
                    where STRDESCRIPTION not in ('[reserved]', 'Reserved')
                    ORDER BY STRSUBPART"

                Case RulePart.NSPS
                    query = "select
                        LK_SUBPART_CODE AS Subpart,
                        ICIS_PROGRAM_SUBPART_DESC AS Description,
                        CONCAT(LK_SUBPART_CODE, N' – ', ICIS_PROGRAM_SUBPART_DESC) AS [Long Description]
                    from LK_ICIS_PROGRAM_SUBPART
                    where ICIS_PROGRAM_CODE in ('CAANSPS', 'CAANSPSM')
                          and ICIS_STATUS_FLAG = 'A'
                    order by LK_SUBPART_CODE"

                Case RulePart.NESHAP
                    query = "select
                        LK_SUBPART_CODE AS Subpart,
                        ICIS_PROGRAM_SUBPART_DESC AS Description,
                        CONCAT(LK_SUBPART_CODE, N' – ', ICIS_PROGRAM_SUBPART_DESC) AS [Long Description]
                    from LK_ICIS_PROGRAM_SUBPART
                    where ICIS_PROGRAM_CODE in ('CAANESH')
                          and ICIS_STATUS_FLAG = 'A'
                    order by LK_SUBPART_CODE"

                Case RulePart.MACT
                    query = "select
                        LK_SUBPART_CODE AS Subpart,
                        ICIS_PROGRAM_SUBPART_DESC AS Description,
                        CONCAT(LK_SUBPART_CODE, N' – ', ICIS_PROGRAM_SUBPART_DESC) AS [Long Description]
                    from LK_ICIS_PROGRAM_SUBPART
                    where ICIS_PROGRAM_CODE in ('CAAMACT', 'CAAGACTM')
                          and ICIS_STATUS_FLAG = 'A'
                    order by LK_SUBPART_CODE"

            End Select

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.CaseSensitive = True
            dt.PrimaryKey = {dt.Columns("Subpart")}
            Return dt
        End Function

    End Module
End Namespace
