Imports Microsoft.Data.SqlClient
Imports Iaip.IAIPNavigation

Namespace DAL
    Module NavigationScreenData

        ''' <summary>
        ''' Returns a table of current work items of various types based on context and scope
        ''' </summary>
        ''' <param name="context">The context of the Nav Work List to return</param>
        ''' <param name="scope">The scope of the Nav Work List to return</param>
        ''' <param name="scopeParameter">An optional ID, e.g., User ID, Branch ID or Unit ID</param>
        ''' <returns></returns>
        Public Function GetNavWorkList(context As NavWorkListContext, scope As NavWorkListScope, Optional scopeParameter As Integer? = Nothing) As DataTable
            Dim query As String = GetNavWorkListSQL(context, scope)
            If query Is Nothing OrElse query = "" Then Return Nothing
            Dim parameter As New SqlParameter("@pid", scopeParameter)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetNavWorkListSQL(context As NavWorkListContext, scope As NavWorkListScope) As String
            Dim query As String = ""

            Select Case context

                Case NavWorkListContext.FacilitiesMissingSubparts
                    query = "SELECT convert(varchar(max), h.STRAIRSNUMBER) as [AIRS #],
                               f.STRFACILITYNAME                      as Facility,
                               f.STRFACILITYCITY                      as City,
                               CASE
                                   WHEN p.ICIS_PROGRAM_CODE = 'CAANESH'
                                       THEN 'NESHAP'
                                   WHEN p.ICIS_PROGRAM_CODE = 'CAANSPS'
                                       THEN 'NSPS'
                                   WHEN p.ICIS_PROGRAM_CODE = 'CAAMACT'
                                       THEN 'MACT'
                                   ELSE 'Error'
                               END                                    AS [Subparts missing]
                        FROM APBHEADERDATA h
                             inner join APBFACILITYINFORMATION f
                                        on f.STRAIRSNUMBER = h.STRAIRSNUMBER
                             inner join ICIS_PROGRAM_CODES p
                                        on p.STRAIRSNUMBER = h.STRAIRSNUMBER
                             inner join LK_ICIS_PROGRAM i
                                        on i.ICIS_PROGRAM_CODE = p.ICIS_PROGRAM_CODE
                             left join APBSUBPARTDATA s
                                       on s.STRAIRSNUMBER = p.STRAIRSNUMBER
                                           and right(s.STRSUBPARTKEY, 1) = i.LGCY_PROGRAM_CODE
                                           and s.ACTIVE = '1'
                             left join LK_ICIS_PROGRAM_SUBPART l
                                       on l.LGCY_PROGRAM_CODE = i.LGCY_PROGRAM_CODE
                                           and l.LK_SUBPART_CODE = s.STRSUBPART
                                           and l.ICIS_STATUS_FLAG = 'A'
                        where h.STROPERATIONALSTATUS <> 'X'
                          and p.OperatingStatusCode <> 'CLS'
                          and p.ICIS_PROGRAM_CODE in ('CAANSPS', 'CAAMACT', 'CAANESH')
                          and i.ICIS_STATUS_FLAG = 'A'
                          and s.STRAIRSNUMBER is null "

                Case NavWorkListContext.MonitoringTestNotifications
                    query = "SELECT n.STRTESTLOGNUMBER     AS [Test Log #],
                           l.STRREFERENCENUMBER   AS [Reference #],
                           CASE
                               WHEN n.STRAIRSNUMBER IS NULL
                                   THEN NULL
                               ELSE f.STRFACILITYNAME
                           END                    AS Facility,
                           n.STRAIRSNUMBER        AS [AIRS #],
                           n.STREMISSIONUNIT      AS [Emission Unit],
                           n.DATPROPOSEDSTARTDATE AS [Start Date],
                           CASE
                               WHEN u.STRLASTNAME IS NULL THEN NULL
                               ELSE u.STRLASTNAME + ', ' + u.STRFIRSTNAME
                           END                    AS [Staff Responsible]
                    FROM ISMPTESTNOTIFICATION AS n
                         LEFT JOIN ISMPTESTLOGLINK AS l
                                   ON n.STRTESTLOGNUMBER = l.STRTESTLOGNUMBER
                         LEFT JOIN EPDUSERPROFILES AS u
                                   ON n.STRSTAFFRESPONSIBLE = u.NUMUSERID
                         LEFT JOIN APBFACILITYINFORMATION AS f
                                   ON n.STRAIRSNUMBER = f.STRAIRSNUMBER
                    WHERE l.STRREFERENCENUMBER IS NULL
                      AND n.DATPROPOSEDSTARTDATE > DATEADD(day, -180, GETDATE()) "

                Case NavWorkListContext.MonitoringTestReports
                    query = "SELECT r.STRREFERENCENUMBER                  AS [Reference #],
                               m.STRAIRSNUMBER                       AS [AIRS #],
                               f.STRFACILITYNAME                     AS Facility,
                               f.STRFACILITYCITY                     AS City,
                               r.STREMISSIONSOURCE                   AS [Emission Source],
                               lp.STRPOLLUTANTDESCRIPTION            AS Pollutant,
                               d.STRDOCUMENTTYPE                     AS [Document Type],
                               u.STRLASTNAME + ', ' + u.STRFIRSTNAME AS [Reviewing Engineer],
                               r.DATTESTDATESTART                    AS [Test Date],
                               r.DATRECEIVEDDATE                     AS [Date Received],
                               CASE
                                   WHEN r.DATCOMPLETEDATE = '04-JUL-1776' THEN NULL
                                   ELSE r.DATCOMPLETEDATE
                               END                                   AS [Complete Date],
                               lc.STRCOMPLIANCESTATUS                AS [Compliance Status],
                               r.STRPRECOMPLIANCESTATUS              AS [Precompliance Status]
                        FROM ISMPREPORTINFORMATION AS r
                             INNER JOIN ISMPMASTER AS m
                                        ON r.STRREFERENCENUMBER = m.STRREFERENCENUMBER
                             INNER JOIN APBFACILITYINFORMATION AS f
                                        ON m.STRAIRSNUMBER = f.STRAIRSNUMBER
                             INNER JOIN LOOKUPPOLLUTANTS AS lp
                                        ON r.STRPOLLUTANT = lp.STRPOLLUTANTCODE
                             INNER JOIN ISMPDOCUMENTTYPE AS d
                                        ON r.STRDOCUMENTTYPE = d.STRKEY
                             INNER JOIN EPDUSERPROFILES AS u
                                        ON r.STRREVIEWINGENGINEER = u.NUMUSERID
                             INNER JOIN LOOKUPISMPCOMPLIANCESTATUS AS lc
                                        ON r.STRCOMPLIANCESTATUS = lc.STRCOMPLIANCEKEY
                        WHERE r.STRCLOSED = 'False'
                          AND r.STRDELETE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query &= "AND r.STRREVIEWINGENGINEER = @pid "
                        Case NavWorkListScope.UnitView
                            query &= " AND u.NUMUNIT = @pid "
                    End Select

                Case NavWorkListContext.PermitApplications
                    query = "SELECT convert(int, a.STRAPPLICATIONNUMBER)                  AS [App #],
                               CASE
                                   WHEN a.STRAIRSNUMBER = '0413' THEN NULL
                                   when a.STRAIRSNUMBER like '%APL%' then replace(a.STRAIRSNUMBER, '0413APL', 'APL')
                                   ELSE a.STRAIRSNUMBER
                               END                                                   AS [AIRS #],
                               d.STRFACILITYNAME                                     AS Facility,
                               d.STRFACILITYCITY                                     AS City,
                               lt.STRAPPLICATIONTYPEDESC                             AS [App Type],
                               t.DATRECEIVEDDATE                                     AS [Date Received],
                               lp.STRPERMITTYPEDESCRIPTION                           AS [Action Type],
                               ISNULL(SUBSTRING(d.STRPERMITNUMBER, 1, 4), '') + '-'
                                   + ISNULL(SUBSTRING(d.STRPERMITNUMBER, 5, 3), '') + '-'
                                   + ISNULL(SUBSTRING(d.STRPERMITNUMBER, 8, 4), '') + '-'
                                   + ISNULL(SUBSTRING(d.STRPERMITNUMBER, 12, 1), '') + '-'
                                   + ISNULL(SUBSTRING(d.STRPERMITNUMBER, 13, 2), '') + '-'
                                   + ISNULL(SUBSTRING(d.STRPERMITNUMBER, 15, 1), '') AS [Permit Number],
                               CASE
                                   WHEN a.STRSTAFFRESPONSIBLE = '0' THEN NULL
                                   WHEN a.STRSTAFFRESPONSIBLE IS NULL THEN NULL
                                   ELSE u.STRLASTNAME + ', ' + u.STRFIRSTNAME
                               END                                                   AS [Staff Responsible],
                               CASE
                                   WHEN t.DATPERMITISSUED IS NOT NULL THEN '11 - Permit Issued'
                                   WHEN t.DATTODIRECTOR IS NOT NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR)
                                       THEN '10 - To Director''s Office'
                                   WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND t.DATTODIRECTOR IS NULL AND
                                        (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN '09 - To Branch Chief'
                                   WHEN t.DATEPAENDS IS NOT NULL THEN '08 - EPA 45-day Review'
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN '07 - Public Notice Expired'
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN '06 - Public Notice'
                                   WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN '05 - Draft Issued'
                                   WHEN t.DATTOPMII IS NOT NULL THEN '04 - To Program Manager'
                                   WHEN t.DATTOPMI IS NOT NULL THEN '03 - To Unit Manager'
                                   WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0')
                                       THEN '02 - Internal Review'
                                   WHEN a.STRSTAFFRESPONSIBLE IS NULL OR a.STRSTAFFRESPONSIBLE = '0' THEN '00 - Received'
                                   ELSE '01 - At Staff'
                               END                                                   AS [Application Status],
                               CASE
                                   WHEN t.DATPERMITISSUED IS NOT NULL THEN t.DATPERMITISSUED
                                   WHEN t.DATTODIRECTOR IS NOT NULL AND (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTODIRECTOR)
                                       THEN t.DATTODIRECTOR
                                   WHEN t.DATTOBRANCHCHEIF IS NOT NULL AND t.DATTODIRECTOR IS NULL AND
                                        (t.DATDRAFTISSUED IS NULL OR t.DATDRAFTISSUED < t.DATTOBRANCHCHEIF) THEN t.DATTOBRANCHCHEIF
                                   WHEN t.DATEPAENDS IS NOT NULL THEN t.DATEPAENDS
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES < GETDATE() THEN t.DATPNEXPIRES
                                   WHEN t.DATPNEXPIRES IS NOT NULL AND t.DATPNEXPIRES >= GETDATE() THEN t.DATPNEXPIRES
                                   WHEN t.DATDRAFTISSUED IS NOT NULL AND t.DATPNEXPIRES IS NULL THEN t.DATDRAFTISSUED
                                   WHEN t.DATTOPMII IS NOT NULL THEN t.DATTOPMII
                                   WHEN t.DATTOPMI IS NOT NULL THEN t.DATTOPMI
                                   WHEN t.DATREVIEWSUBMITTED IS NOT NULL AND (d.STRSSCPUNIT <> '0' OR d.STRISMPUNIT <> '0')
                                       THEN t.DATREVIEWSUBMITTED
                                   WHEN a.STRSTAFFRESPONSIBLE IS NULL OR a.STRSTAFFRESPONSIBLE = '0' THEN t.DATRECEIVEDDATE
                                   ELSE t.DATASSIGNEDTOENGINEER
                               END                                                   AS [Status Date],
                               IIF(substring(d.STRTRACKEDRULES, 7, 1) = '1', 'Expedited Permit', 'No') AS [Expedited Permit]
                        FROM SSPPAPPLICATIONMASTER AS a
                             LEFT JOIN SSPPAPPLICATIONTRACKING AS t
                                       ON a.STRAPPLICATIONNUMBER = t.STRAPPLICATIONNUMBER
                             LEFT JOIN EPDUSERPROFILES AS u
                                       ON a.STRSTAFFRESPONSIBLE = u.NUMUSERID
                             LEFT JOIN LOOKUPAPPLICATIONTYPES AS lt
                                       ON a.STRAPPLICATIONTYPE = lt.STRAPPLICATIONTYPECODE
                             LEFT JOIN LOOKUPPERMITTYPES AS lp
                                       ON a.STRPERMITTYPE = lp.STRPERMITTYPECODE
                             LEFT JOIN SSPPAPPLICATIONDATA AS d
                                       ON a.STRAPPLICATIONNUMBER = d.STRAPPLICATIONNUMBER
                        WHERE a.DATFINALIZEDDATE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query &= " AND a.STRSTAFFRESPONSIBLE = @pid "
                        Case NavWorkListScope.UnitView
                            query &= " AND (u.NUMUNIT = @pid or a.APBUNIT = @pid) "
                    End Select

            End Select

            query &= " ORDER BY 1, 2 "
            Return query
        End Function

    End Module
End Namespace