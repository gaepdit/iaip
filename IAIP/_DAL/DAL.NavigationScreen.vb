Imports System.Data.SqlClient
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
        Public Function GetNavWorkList(context As NavWorkListContext, scope As NavWorkListScope, Optional scopeParameter As String = "") As DataTable
            Dim query As String = GetNavWorkListSQL(context, scope)
            If query Is Nothing Or query = "" Then Return Nothing
            Dim parameter As New SqlParameter("pid", scopeParameter)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetNavWorkListSQL(context As NavWorkListContext, scope As NavWorkListScope) As String
            Dim query As String = ""

            Select Case context

                Case NavWorkListContext.ComplianceWork
                    query = "SELECT item.STRTRACKINGNUMBER AS ""Tracking #"", " &
                        "  item.STRAIRSNUMBER AS ""Airs #"", fac.STRFACILITYNAME AS " &
                        "  ""Facility"", fac.STRFACILITYCITY AS ""City"", " &
                        "  act.STRACTIVITYNAME AS ""Type"", item.DATRECEIVEDDATE AS " &
                        "  ""Date Received"",(prof.STRLASTNAME || ', ' || " &
                        "  prof.STRFIRSTNAME) AS ""Staff Responsible"" " &
                        "FROM SSCPITEMMASTER item " &
                        "INNER JOIN LOOKUPCOMPLIANCEACTIVITIES act ON " &
                        "  act.STRACTIVITYTYPE = item.STREVENTTYPE " &
                        "INNER JOIN EPDUSERPROFILES prof ON item.STRRESPONSIBLESTAFF = " &
                        "  prof.NUMUSERID " &
                        "INNER JOIN APBFACILITYINFORMATION fac ON fac.STRAIRSNUMBER = " &
                        "  item.STRAIRSNUMBER " &
                        "WHERE item.DATCOMPLETEDATE IS NULL AND item.STRDELETE IS NULL"

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & " AND item.STRRESPONSIBLESTAFF = :pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = :pid "
                    End Select

                Case NavWorkListContext.LateFce
                    query = "SELECT STRAIRSNUMBER   AS ""AIRS #"" " &
                        ", STRFACILITYNAME      AS ""Facility"" " &
                        ", STRFACILITYCITY      AS ""City"" " &
                        ", LASTFCE              AS ""Most recent FCE"" " &
                        ", STRCLASS             AS ""Class"" " &
                        ", STRCMSMEMBER         AS ""CMS Class"" " &
                        ", STROPERATIONALSTATUS AS ""Operating Status"" " &
                        "FROM " &
                        "  (SELECT fm.STRAIRSNUMBER " &
                        "  , fac.STRFACILITYNAME " &
                        "  , fac.STRFACILITYCITY " &
                        "  , MAX (fce.DATFCECOMPLETED) AS LASTFCE " &
                        "  , hd.STRCLASS " &
                        "  , sup.STRCMSMEMBER " &
                        "  , hd.STROPERATIONALSTATUS " &
                        "  FROM SSCPFCE fce " &
                        "  INNER JOIN SSCPFCEMASTER fm " &
                        "  ON fce.STRFCENUMBER = fm.STRFCENUMBER " &
                        "  INNER JOIN APBFACILITYINFORMATION fac " &
                        "  ON fm.STRAIRSNUMBER = fac.STRAIRSNUMBER " &
                        "  INNER JOIN APBSUPPLAMENTALDATA sup " &
                        "  ON fm.STRAIRSNUMBER = sup.STRAIRSNUMBER " &
                        "  INNER JOIN APBHEADERDATA hd " &
                        "  ON fm.STRAIRSNUMBER = hd.STRAIRSNUMBER " &
                        "  GROUP BY fm.STRAIRSNUMBER " &
                        "  , fac.STRFACILITYNAME " &
                        "  , fac.STRFACILITYCITY " &
                        "  , sup.STRCMSMEMBER " &
                        "  , hd.STRCLASS " &
                        "  , hd.STROPERATIONALSTATUS " &
                        "  ) " &
                        "WHERE (STRCMSMEMBER = 'A' " &
                        "AND LASTFCE         < ADD_MONTHS (SYSDATE, - " & MIN_FCE_SPAN_CLASS_A & " * 12)) " &
                        "OR (STRCMSMEMBER    = 'S' " &
                        "AND LASTFCE         < ADD_MONTHS (SYSDATE, - " & MIN_FCE_SPAN_CLASS_SM & " * 12))"

                Case NavWorkListContext.Enforcement
                    query = "SELECT enf.STRENFORCEMENTNUMBER AS ""Enforcement #"" " &
                        ", enf.STRAIRSNUMBER             AS ""AIRS #"" " &
                        ", fac.STRFACILITYNAME           AS ""Facility"" " &
                        ", fac.STRFACILITYCITY           AS ""City"" " &
                        ", enf.DATDISCOVERYDATE          AS ""Discovery Date"" " &
                        ", CASE " &
                        "    WHEN enf.STRAFSKEYACTIONNUMBER IS NOT NULL " &
                        "    THEN 'Submitted to EPA' " &
                        "    WHEN enf.STRSTATUS = 'UC' " &
                        "    THEN 'Submitted to UC' " &
                        "    ELSE 'At staff' " &
                        "  END AS ""Status"" " &
                        ", CASE " &
                        "    WHEN enf.STRACTIONTYPE = 'CASEFILE' " &
                        "    THEN 'Case File' " &
                        "    ELSE enf.STRACTIONTYPE " &
                        "  END AS ""Type"" " &
                        ", prof.STRLASTNAME " &
                        "  || ', ' " &
                        "  || prof.STRFIRSTNAME AS ""Staff Responsible"" " &
                        "FROM SSCP_AUDITEDENFORCEMENT enf " &
                        "INNER JOIN APBFACILITYINFORMATION fac " &
                        "ON enf.STRAIRSNUMBER = fac.STRAIRSNUMBER " &
                        "INNER JOIN EPDUSERPROFILES prof " &
                        "ON enf.NUMSTAFFRESPONSIBLE        = prof.NUMUSERID " &
                        "WHERE enf.STRENFORCEMENTFINALIZED = 'False' "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & "AND enf.NUMSTAFFRESPONSIBLE       = :pid " &
                                "ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = :pid " &
                                "ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                        Case Else
                            query = query & "ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                    End Select

                Case NavWorkListContext.FacilitiesMissingSubparts
                    query = "SELECT hd.STRAIRSNUMBER AS ""AIRS #"" " &
                        ", fac.STRFACILITYNAME   AS ""Facility"" " &
                        ", fac.STRFACILITYCITY   AS ""City"" " &
                        ", CASE " &
                        "    WHEN hd.APC = '8' " &
                        "    THEN 'NESHAP' " &
                        "    WHEN hd.APC = '9' " &
                        "    THEN 'NSPS' " &
                        "    WHEN hd.APC = 'M' " &
                        "    THEN 'MACT' " &
                        "    ELSE 'Error' " &
                        "  END AS ""Subparts missing"" " &
                        "FROM " &
                        "  (SELECT hd.STRAIRSNUMBER " &
                        "  , hd.STROPERATIONALSTATUS " &
                        "  , '8' AS APC " &
                        "  FROM APBHEADERDATA hd " &
                        "  WHERE SUBSTR (hd.STRAIRPROGRAMCODES, 7, 1) = '1' " &
                        "  UNION " &
                        "  SELECT hd.STRAIRSNUMBER " &
                        "  , hd.STROPERATIONALSTATUS " &
                        "  , '9' AS airprogramcode " &
                        "  FROM APBHEADERDATA hd " &
                        "  WHERE SUBSTR (hd.STRAIRPROGRAMCODES, 8, 1) = '1' " &
                        "  UNION " &
                        "  SELECT hd.STRAIRSNUMBER " &
                        "  , hd.STROPERATIONALSTATUS " &
                        "  , 'M' AS airprogramcode " &
                        "  FROM APBHEADERDATA hd " &
                        "  WHERE SUBSTR (hd.STRAIRPROGRAMCODES, 12, 1) = '1' " &
                        "  ) hd " &
                        "LEFT JOIN " &
                        "  (SELECT DISTINCT sp.STRAIRSNUMBER " &
                        "  , SUBSTR (sp.STRSUBPARTKEY, 13, 1) AS APC " &
                        "  FROM APBSUBPARTDATA sp " &
                        "  WHERE SUBSTR (sp.STRSUBPARTKEY, 13, 1) <> '0' " &
                        "  ) sp " &
                        "ON sp.APC            = hd.APC " &
                        "AND sp.STRAIRSNUMBER = hd.STRAIRSNUMBER " &
                        "INNER JOIN APBFACILITYINFORMATION fac " &
                        "ON fac.STRAIRSNUMBER           = hd.STRAIRSNUMBER " &
                        "WHERE hd.stroperationalstatus <> 'X' " &
                        "AND sp.apc                    IS NULL"

                Case NavWorkListContext.MonitoringTestNotifications
                    query = "SELECT notif.STRTESTLOGNUMBER AS ""Test Log #"" " &
                        ", lnk.STRREFERENCENUMBER      AS ""Reference #"" " &
                        ", CASE " &
                        "    WHEN notif.STRAIRSNUMBER IS NULL " &
                        "    THEN NULL " &
                        "    ELSE fac.STRFACILITYNAME " &
                        "  END ""Facility"" " &
                        ", notif.STRAIRSNUMBER        AS ""AIRS #"" " &
                        ", notif.STREMISSIONUNIT      AS ""Emission Unit"" " &
                        ", notif.DATPROPOSEDSTARTDATE AS ""Start Date"" " &
                        ", CASE " &
                        "    WHEN prof.STRLASTNAME IS NULL " &
                        "    THEN NULL " &
                        "    ELSE (prof.STRLASTNAME " &
                        "      || ', ' " &
                        "      || prof.STRFIRSTNAME) " &
                        "  END ""Staff Responsible"" " &
                        "FROM ISMPTESTNOTIFICATION notif " &
                        "LEFT JOIN ISMPTESTLOGLINK lnk " &
                        "ON notif.STRTESTLOGNUMBER = lnk.STRTESTLOGNUMBER " &
                        "LEFT JOIN EPDUSERPROFILES prof " &
                        "ON notif.STRSTAFFRESPONSIBLE = prof.NUMUSERID " &
                        "LEFT JOIN APBFACILITYINFORMATION fac " &
                        "ON notif.STRAIRSNUMBER         = fac.STRAIRSNUMBER " &
                        "WHERE lnk.STRREFERENCENUMBER  IS NULL " &
                        "AND notif.DATPROPOSEDSTARTDATE > (SYSDATE - 180)"

                Case NavWorkListContext.MonitoringTestReports
                    query = "SELECT rep.STRREFERENCENUMBER AS ""Reference #"" " &
                        ", ismp.STRAIRSNUMBER          AS ""AIRS #"" " &
                        ", fac.STRFACILITYNAME         AS ""Facility"" " &
                        ", fac.STRFACILITYCITY         AS ""City"" " &
                        ", rep.STREMISSIONSOURCE       AS ""Emission Source"" " &
                        ", lkp.STRPOLLUTANTDESCRIPTION AS ""Pollutant"" " &
                        ", dt.STRDOCUMENTTYPE          AS ""Document Type"" " &
                        ", prof.STRLASTNAME " &
                        "  || ', ' " &
                        "  || prof.STRFIRSTNAME AS ""Reviewing Engineer"" " &
                        ", rep.DATTESTDATESTART AS ""Test Date"" " &
                        ", CASE " &
                        "    WHEN rep.DATCOMPLETEDATE = '04-JUL-1776' " &
                        "    THEN NULL " &
                        "    ELSE rep.DATCOMPLETEDATE " &
                        "  END                        AS ""Complete Date"" " &
                        ", lks.STRCOMPLIANCESTATUS    AS ""Compliance Status"" " &
                        ", rep.STRPRECOMPLIANCESTATUS AS ""Precompliance Status"" " &
                        "FROM ISMPREPORTINFORMATION rep " &
                        "INNER JOIN ISMPMASTER ismp " &
                        "ON rep.STRREFERENCENUMBER = ismp.STRREFERENCENUMBER " &
                        "INNER JOIN APBFACILITYINFORMATION fac " &
                        "ON ismp.STRAIRSNUMBER = fac.STRAIRSNUMBER " &
                        "INNER JOIN LOOKUPPOLLUTANTS lkp " &
                        "ON rep.STRPOLLUTANT = lkp.STRPOLLUTANTCODE " &
                        "INNER JOIN ISMPDOCUMENTTYPE dt " &
                        "ON rep.STRDOCUMENTTYPE = dt.STRKEY " &
                        "INNER JOIN EPDUSERPROFILES prof " &
                        "ON rep.STRREVIEWINGENGINEER = prof.NUMUSERID " &
                        "INNER JOIN LOOKUPISMPCOMPLIANCESTATUS lks " &
                        "ON rep.STRCOMPLIANCESTATUS     = lks.STRCOMPLIANCEKEY " &
                        "WHERE rep.STRCLOSED            = 'False' " &
                        "AND rep.STRDELETE             IS NULL"

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & "AND rep.STRREVIEWINGENGINEER = :pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = :pid "
                    End Select

                Case NavWorkListContext.PermitApplications
                    query = "SELECT app.STRAPPLICATIONNUMBER AS ""App #"" " &
                        ", CASE " &
                        "    WHEN app.STRAIRSNUMBER = '0413' " &
                        "    THEN NULL " &
                        "    ELSE app.STRAIRSNUMBER " &
                        "  END                          AS ""AIRS #"" " &
                        ", dat.STRFACILITYNAME          AS ""Facility"" " &
                        ", dat.STRFACILITYCITY          AS ""City"" " &
                        ", lka.STRAPPLICATIONTYPEDESC   AS ""App Type"" " &
                        ", trk.DATRECEIVEDDATE          AS ""Date Received"" " &
                        ", lkp.STRPERMITTYPEDESCRIPTION AS ""Action Type"" " &
                        ", SUBSTR (dat.STRPERMITNUMBER, 1, 4) " &
                        "  || '-' " &
                        "  || SUBSTR (dat.STRPERMITNUMBER, 5, 3) " &
                        "  || '-' " &
                        "  || SUBSTR (dat.STRPERMITNUMBER, 8, 4) " &
                        "  || '-' " &
                        "  || SUBSTR (dat.STRPERMITNUMBER, 12, 1) " &
                        "  || '-' " &
                        "  || SUBSTR (dat.STRPERMITNUMBER, 13, 2) " &
                        "  || '-' " &
                        "  || SUBSTR (dat.STRPERMITNUMBER, 15, 1) AS ""Permit Number"" " &
                        ", CASE " &
                        "    WHEN app.STRSTAFFRESPONSIBLE = '0' " &
                        "    THEN NULL " &
                        "    WHEN app.STRSTAFFRESPONSIBLE IS NULL " &
                        "    THEN NULL " &
                        "    ELSE prof.STRLASTNAME " &
                        "      || ', ' " &
                        "      || prof.STRFIRSTNAME " &
                        "  END AS ""Staff Responsible"" " &
                        ", CASE " &
                        "    WHEN trk.DATPERMITISSUED IS NOT NULL " &
                        "    THEN 'Permit Issued' " &
                        "    WHEN trk.DATTODIRECTOR  IS NOT NULL " &
                        "    AND (trk.DATDRAFTISSUED IS NULL " &
                        "    OR trk.DATDRAFTISSUED    < trk.DATTODIRECTOR) " &
                        "    THEN 'To Director''s Office' " &
                        "    WHEN trk.DATTOBRANCHCHEIF IS NOT NULL " &
                        "    AND trk.DATTODIRECTOR     IS NULL " &
                        "    AND (trk.DATDRAFTISSUED   IS NULL " &
                        "    OR trk.DATDRAFTISSUED      < trk.DATTOBRANCHCHEIF) " &
                        "    THEN 'To Branch Chief' " &
                        "    WHEN trk.DATEPAENDS IS NOT NULL " &
                        "    THEN 'EPA 45-day Review' " &
                        "    WHEN trk.DATPNEXPIRES IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES   < SysDate " &
                        "    THEN 'Public Notice Expired' " &
                        "    WHEN trk.DATPNEXPIRES IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES  >= SysDate " &
                        "    THEN 'Public Notice' " &
                        "    WHEN trk.DATDRAFTISSUED IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES    IS NULL " &
                        "    THEN 'Draft Issued' " &
                        "    WHEN trk.DATTOPMII IS NOT NULL " &
                        "    THEN 'To Program Manager' " &
                        "    WHEN trk.DATTOPMI IS NOT NULL " &
                        "    THEN 'To Unit Manager' " &
                        "    WHEN trk.DATREVIEWSUBMITTED IS NOT NULL " &
                        "    AND (dat.STRSSCPUNIT        <> '0' " &
                        "    OR dat.STRISMPUNIT          <> '0') " &
                        "    THEN 'Internal Review' " &
                        "    WHEN app.STRSTAFFRESPONSIBLE IS NULL " &
                        "    OR app.STRSTAFFRESPONSIBLE    = '0' " &
                        "    THEN 'Received' " &
                        "    ELSE 'At Staff' " &
                        "  END AS ""Application Status"" " &
                        ", CASE " &
                        "    WHEN trk.DATPERMITISSUED IS NOT NULL " &
                        "    THEN trk.DATPERMITISSUED " &
                        "    WHEN trk.DATTODIRECTOR  IS NOT NULL " &
                        "    AND (trk.DATDRAFTISSUED IS NULL " &
                        "    OR trk.DATDRAFTISSUED    < trk.DATTODIRECTOR) " &
                        "    THEN trk.DATTODIRECTOR " &
                        "    WHEN trk.DATTOBRANCHCHEIF IS NOT NULL " &
                        "    AND trk.DATTODIRECTOR     IS NULL " &
                        "    AND (trk.DATDRAFTISSUED   IS NULL " &
                        "    OR trk.DATDRAFTISSUED      < trk.DATTOBRANCHCHEIF) " &
                        "    THEN trk.DATTOBRANCHCHEIF " &
                        "    WHEN trk.DATEPAENDS IS NOT NULL " &
                        "    THEN trk.DATEPAENDS " &
                        "    WHEN trk.DATPNEXPIRES IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES   < SysDate " &
                        "    THEN trk.DATPNEXPIRES " &
                        "    WHEN trk.DATPNEXPIRES IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES  >= SysDate " &
                        "    THEN trk.DATPNEXPIRES " &
                        "    WHEN trk.DATDRAFTISSUED IS NOT NULL " &
                        "    AND trk.DATPNEXPIRES    IS NULL " &
                        "    THEN trk.DATDRAFTISSUED " &
                        "    WHEN trk.DATTOPMII IS NOT NULL " &
                        "    THEN trk.DATTOPMII " &
                        "    WHEN trk.DATTOPMI IS NOT NULL " &
                        "    THEN trk.DATTOPMI " &
                        "    WHEN trk.DATREVIEWSUBMITTED IS NOT NULL " &
                        "    AND (dat.STRSSCPUNIT        <> '0' " &
                        "    OR dat.STRISMPUNIT          <> '0') " &
                        "    THEN trk.DATREVIEWSUBMITTED " &
                        "    WHEN app.STRSTAFFRESPONSIBLE IS NULL " &
                        "    OR app.STRSTAFFRESPONSIBLE    = '0' " &
                        "    THEN trk.DATRECEIVEDDATE " &
                        "    ELSE trk.DATASSIGNEDTOENGINEER " &
                        "  END AS ""Status Date"" " &
                        "FROM SSPPAPPLICATIONMASTER app " &
                        "LEFT JOIN SSPPAPPLICATIONTRACKING trk " &
                        "ON app.STRAPPLICATIONNUMBER = trk.STRAPPLICATIONNUMBER " &
                        "LEFT JOIN EPDUSERPROFILES prof " &
                        "ON app.STRSTAFFRESPONSIBLE = prof.NUMUSERID " &
                        "LEFT JOIN LOOKUPAPPLICATIONTYPES lka " &
                        "ON app.STRAPPLICATIONTYPE = lka.STRAPPLICATIONTYPECODE " &
                        "LEFT JOIN LOOKUPPERMITTYPES lkp " &
                        "ON app.STRPERMITTYPE = lkp.STRPERMITTYPECODE " &
                        "LEFT JOIN SSPPAPPLICATIONDATA dat " &
                        "ON app.STRAPPLICATIONNUMBER = dat.STRAPPLICATIONNUMBER " &
                        "WHERE app.DATFINALIZEDDATE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & "AND app.STRSTAFFRESPONSIBLE = :pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = :pid "
                    End Select

                Case NavWorkListContext.SbeapCases
                    query = "SELECT cl.NUMCASEID  AS ""Case ID"" " &
                        ", lnk.CLIENTID       AS ""Customer ID"" " &
                        ", cli.STRCOMPANYNAME AS ""Customer"" " &
                        ", cl.DATCASEOPENED   AS ""Case Opened"" " &
                        ", cl.STRCASESUMMARY  AS ""Description"" " &
                        ", cl.DATMODIFINGDATE AS ""Last Updated"" " &
                        ", CASE " &
                        "    WHEN cl.NUMSTAFFRESPONSIBLE IS NULL " &
                        "    THEN NULL " &
                        "    ELSE (prof.STRLASTNAME " &
                        "      || ', ' " &
                        "      || prof.STRFIRSTNAME) " &
                        "  END ""Staff Responsible"" " &
                        "FROM SBEAPCASELOG cl " &
                        "LEFT JOIN EPDUSERPROFILES prof " &
                        "ON cl.NUMSTAFFRESPONSIBLE = prof.NUMUSERID " &
                        "LEFT JOIN SBEAPCASELOGLINK lnk " &
                        "ON cl.NUMCASEID = lnk.NUMCASEID " &
                        "LEFT JOIN SBEAPCLIENTS cli " &
                        "ON lnk.CLIENTID            = cli.CLIENTID " &
                        "WHERE cl.DATCASECLOSED    IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & "AND cl.NUMSTAFFRESPONSIBLE = :pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = :pid "
                    End Select

            End Select

            Return query
        End Function

    End Module
End Namespace