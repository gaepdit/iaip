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
            Dim parameter As New SqlParameter("@pid", scopeParameter)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetNavWorkListSQL(context As NavWorkListContext, scope As NavWorkListScope) As String
            Dim query As String = ""

            Select Case context

                Case NavWorkListContext.ComplianceWork
                    query = "SELECT item.STRTRACKINGNUMBER AS [Tracking #], item.STRAIRSNUMBER AS [AIRS #], fac.STRFACILITYNAME AS Facility, fac.STRFACILITYCITY AS City, act.STRACTIVITYNAME AS Type, item.DATRECEIVEDDATE AS [Date Received], prof.STRLASTNAME+', '+prof.STRFIRSTNAME AS [Staff Responsible]
                            FROM SSCPITEMMASTER AS item
                            INNER JOIN LOOKUPCOMPLIANCEACTIVITIES AS act ON act.STRACTIVITYTYPE = item.STREVENTTYPE
                            INNER JOIN EPDUSERPROFILES AS prof ON item.STRRESPONSIBLESTAFF = prof.NUMUSERID
                            INNER JOIN APBFACILITYINFORMATION AS fac ON fac.STRAIRSNUMBER = item.STRAIRSNUMBER
                            WHERE item.DATCOMPLETEDATE IS NULL AND item.STRDELETE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & " AND item.STRRESPONSIBLESTAFF = @pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = @pid "
                    End Select

                Case NavWorkListContext.LateFce
                    query = "SELECT fci.STRAIRSNUMBER AS [AIRS #], fci.STRFACILITYNAME AS Facility, fci.STRFACILITYCITY AS City, fci.LASTFCE AS [Most recent FCE], fci.STRCLASS AS Class, fci.STRCMSMEMBER AS [CMS Class], fci.STROPERATIONALSTATUS AS [Operating Status]
                            FROM (SELECT fm.STRAIRSNUMBER, fac.STRFACILITYNAME, fac.STRFACILITYCITY, MAX(fce.DATFCECOMPLETED) AS LASTFCE, hd.STRCLASS, sup.STRCMSMEMBER, hd.STROPERATIONALSTATUS
                            FROM SSCPFCE AS fce
                            INNER JOIN SSCPFCEMASTER AS fm ON fce.STRFCENUMBER = fm.STRFCENUMBER
                            INNER JOIN APBFACILITYINFORMATION AS fac ON fm.STRAIRSNUMBER = fac.STRAIRSNUMBER
                            INNER JOIN APBSUPPLAMENTALDATA AS sup ON fm.STRAIRSNUMBER = sup.STRAIRSNUMBER
                            INNER JOIN APBHEADERDATA AS hd ON fm.STRAIRSNUMBER = hd.STRAIRSNUMBER
                            GROUP BY fm.STRAIRSNUMBER, fac.STRFACILITYNAME, fac.STRFACILITYCITY, sup.STRCMSMEMBER, hd.STRCLASS, hd.STROPERATIONALSTATUS) AS fci
                            WHERE (fci.STRCMSMEMBER = 'A' AND fci.LASTFCE < DATEADD(yy, -" & MIN_FCE_SPAN_CLASS_A & ", GETDATE()))
                            OR (fci.STRCMSMEMBER = 'S' AND fci.LASTFCE < DATEADD(yy, -" & MIN_FCE_SPAN_CLASS_SM & ", GETDATE()))
                            OR (fci.STRCMSMEMBER = 'M' AND fci.LASTFCE < DATEADD(yy, -" & MIN_FCE_SPAN_CLASS_M & ", GETDATE()))"

                Case NavWorkListContext.Enforcement
                    query = "SELECT enf.STRENFORCEMENTNUMBER AS [Enforcement #], enf.STRAIRSNUMBER AS [AIRS #], fac.STRFACILITYNAME AS Facility, fac.STRFACILITYCITY AS City, enf.DATDISCOVERYDATE AS [Discovery Date],
                            CASE WHEN enf.STRAFSKEYACTIONNUMBER IS NOT NULL THEN 'Submitted to EPA' WHEN enf.STRSTATUS = 'UC' THEN 'Submitted to UC' ELSE 'At staff' END AS Status,
                            CASE WHEN enf.STRACTIONTYPE = 'CASEFILE' THEN 'Case File' ELSE enf.STRACTIONTYPE END AS Type, prof.STRLASTNAME+', '+prof.STRFIRSTNAME AS [Staff Responsible]
                            FROM SSCP_AUDITEDENFORCEMENT AS enf
                            INNER JOIN APBFACILITYINFORMATION AS fac ON enf.STRAIRSNUMBER = fac.STRAIRSNUMBER
                            INNER JOIN EPDUSERPROFILES AS prof ON enf.NUMSTAFFRESPONSIBLE = prof.NUMUSERID
                            WHERE enf.STRENFORCEMENTFINALIZED = 'False' "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & " AND enf.NUMSTAFFRESPONSIBLE = @pid " &
                                " ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = @pid " &
                                " ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                        Case Else
                            query = query & " ORDER BY enf.STRENFORCEMENTNUMBER DESC"
                    End Select

                Case NavWorkListContext.FacilitiesMissingSubparts
                    query = "SELECT hd.STRAIRSNUMBER AS [AIRS #], fac.STRFACILITYNAME AS Facility, fac.STRFACILITYCITY AS City,
                            CASE WHEN hd.APC = '8' THEN 'NESHAP' WHEN hd.APC = '9' THEN 'NSPS' WHEN hd.APC = 'M' THEN 'MACT' ELSE 'Error' END AS [Subparts missing]
                            FROM (
                            SELECT hd.STRAIRSNUMBER, hd.STROPERATIONALSTATUS, '8' AS APC
                            FROM APBHEADERDATA AS hd
                            WHERE SUBSTRING(hd.STRAIRPROGRAMCODES, 7, 1) = '1'
                            UNION
                            SELECT hd.STRAIRSNUMBER, hd.STROPERATIONALSTATUS, '9' AS APC
                            FROM APBHEADERDATA AS hd
                            WHERE SUBSTRING(hd.STRAIRPROGRAMCODES, 8, 1) = '1'
                            UNION
                            SELECT hd.STRAIRSNUMBER, hd.STROPERATIONALSTATUS, 'M' AS APC
                            FROM APBHEADERDATA AS hd
                            WHERE SUBSTRING(hd.STRAIRPROGRAMCODES, 12, 1) = '1') AS hd
                            LEFT JOIN (SELECT DISTINCT
                            sp.STRAIRSNUMBER, SUBSTRING(sp.STRSUBPARTKEY, 13, 1) AS APC
                            FROM APBSUBPARTDATA AS sp
                            WHERE SUBSTRING(sp.STRSUBPARTKEY, 13, 1) <> '0') AS sp ON sp.APC = hd.APC AND sp.STRAIRSNUMBER = hd.STRAIRSNUMBER
                            INNER JOIN APBFACILITYINFORMATION AS fac ON fac.STRAIRSNUMBER = hd.STRAIRSNUMBER
                            WHERE hd.STROPERATIONALSTATUS <> 'X' AND sp.apc IS NULL "

                Case NavWorkListContext.MonitoringTestNotifications
                    query = "SELECT notif.STRTESTLOGNUMBER AS [Test Log #], lnk.STRREFERENCENUMBER AS [Reference #],
                            CASE WHEN notif.STRAIRSNUMBER IS NULL THEN NULL ELSE fac.STRFACILITYNAME END AS Facility, notif.STRAIRSNUMBER AS [AIRS #], notif.STREMISSIONUNIT AS [Emission Unit], notif.DATPROPOSEDSTARTDATE AS [Start Date],
                            CASE WHEN prof.STRLASTNAME IS NULL THEN NULL ELSE prof.STRLASTNAME+', '+prof.STRFIRSTNAME END AS [Staff Responsible]
                            FROM ISMPTESTNOTIFICATION AS notif
                            LEFT JOIN ISMPTESTLOGLINK AS lnk ON notif.STRTESTLOGNUMBER = lnk.STRTESTLOGNUMBER
                            LEFT JOIN EPDUSERPROFILES AS prof ON notif.STRSTAFFRESPONSIBLE = prof.NUMUSERID
                            LEFT JOIN APBFACILITYINFORMATION AS fac ON notif.STRAIRSNUMBER = fac.STRAIRSNUMBER
                            WHERE lnk.STRREFERENCENUMBER IS NULL AND notif.DATPROPOSEDSTARTDATE > DATEADD(day, -180, GETDATE())"

                Case NavWorkListContext.MonitoringTestReports
                    query = "SELECT rep.STRREFERENCENUMBER AS [Reference #], ismp.STRAIRSNUMBER AS [AIRS #], fac.STRFACILITYNAME AS Facility, fac.STRFACILITYCITY AS City, rep.STREMISSIONSOURCE AS [Emission Source], lkp.STRPOLLUTANTDESCRIPTION AS Pollutant, dt.STRDOCUMENTTYPE AS [Document Type], prof.STRLASTNAME+', '+prof.STRFIRSTNAME AS [Reviewing Engineer], rep.DATTESTDATESTART AS [Test Date],
                            rep.DATRECEIVEDDATE AS [Date Received],
                            CASE WHEN rep.DATCOMPLETEDATE = '04-JUL-1776' THEN NULL ELSE rep.DATCOMPLETEDATE END AS [Complete Date], lks.STRCOMPLIANCESTATUS AS [Compliance Status], rep.STRPRECOMPLIANCESTATUS AS [Precompliance Status]
                            FROM ISMPREPORTINFORMATION AS rep
                            INNER JOIN ISMPMASTER AS ismp ON rep.STRREFERENCENUMBER = ismp.STRREFERENCENUMBER
                            INNER JOIN APBFACILITYINFORMATION AS fac ON ismp.STRAIRSNUMBER = fac.STRAIRSNUMBER
                            INNER JOIN LOOKUPPOLLUTANTS AS lkp ON rep.STRPOLLUTANT = lkp.STRPOLLUTANTCODE
                            INNER JOIN ISMPDOCUMENTTYPE AS dt ON rep.STRDOCUMENTTYPE = dt.STRKEY
                            INNER JOIN EPDUSERPROFILES AS prof ON rep.STRREVIEWINGENGINEER = prof.NUMUSERID
                            INNER JOIN LOOKUPISMPCOMPLIANCESTATUS AS lks ON rep.STRCOMPLIANCESTATUS = lks.STRCOMPLIANCEKEY
                            WHERE rep.STRCLOSED = 'False' AND rep.STRDELETE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & "AND rep.STRREVIEWINGENGINEER = @pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = @pid "
                    End Select

                Case NavWorkListContext.PermitApplications
                    query = "SELECT convert(int, app.STRAPPLICATIONNUMBER) AS [App #],
                            CASE WHEN app.STRAIRSNUMBER = '0413' THEN NULL ELSE app.STRAIRSNUMBER END AS [AIRS #], dat.STRFACILITYNAME AS Facility, dat.STRFACILITYCITY AS City, lka.STRAPPLICATIONTYPEDESC AS [App Type], trk.DATRECEIVEDDATE AS [Date Received], lkp.STRPERMITTYPEDESCRIPTION AS [Action Type], ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 1, 4), '')+'-'+ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 5, 3), '')+'-'+ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 8, 4), '')+'-'+ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 12, 1), '')+'-'+ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 13, 2), '')+'-'+ISNULL(SUBSTRING(dat.STRPERMITNUMBER, 15, 1), '') AS [Permit Number],
                            CASE WHEN app.STRSTAFFRESPONSIBLE = '0' THEN NULL WHEN app.STRSTAFFRESPONSIBLE IS NULL THEN NULL ELSE prof.STRLASTNAME+', '+prof.STRFIRSTNAME END AS [Staff Responsible],
                            CASE WHEN trk.DATPERMITISSUED IS NOT NULL THEN '11 - Permit Issued' WHEN trk.DATTODIRECTOR IS NOT NULL AND (trk.DATDRAFTISSUED IS NULL OR trk.DATDRAFTISSUED < trk.DATTODIRECTOR) THEN '10 - To Director''s Office' WHEN trk.DATTOBRANCHCHEIF IS NOT NULL AND trk.DATTODIRECTOR IS NULL AND (trk.DATDRAFTISSUED IS NULL OR trk.DATDRAFTISSUED < trk.DATTOBRANCHCHEIF) THEN '09 - To Branch Chief' WHEN trk.DATEPAENDS IS NOT NULL THEN '08 - EPA 45-day Review' WHEN trk.DATPNEXPIRES IS NOT NULL AND trk.DATPNEXPIRES < GETDATE() THEN '07 - Public Notice Expired' WHEN trk.DATPNEXPIRES IS NOT NULL AND trk.DATPNEXPIRES >= GETDATE() THEN '06 - Public Notice' WHEN trk.DATDRAFTISSUED IS NOT NULL AND trk.DATPNEXPIRES IS NULL THEN '05 - Draft Issued' WHEN trk.DATTOPMII IS NOT NULL THEN '04 - To Program Manager' WHEN trk.DATTOPMI IS NOT NULL THEN '03 - To Unit Manager' WHEN trk.DATREVIEWSUBMITTED IS NOT NULL AND (dat.STRSSCPUNIT <> '0' OR dat.STRISMPUNIT <> '0') THEN '02 - Internal Review' WHEN app.STRSTAFFRESPONSIBLE IS NULL OR app.STRSTAFFRESPONSIBLE = '0' THEN '00 - Received' ELSE '01 - At Staff' END AS [Application Status],
                            CASE WHEN trk.DATPERMITISSUED IS NOT NULL THEN trk.DATPERMITISSUED WHEN trk.DATTODIRECTOR IS NOT NULL AND (trk.DATDRAFTISSUED IS NULL OR trk.DATDRAFTISSUED < trk.DATTODIRECTOR) THEN trk.DATTODIRECTOR WHEN trk.DATTOBRANCHCHEIF IS NOT NULL AND trk.DATTODIRECTOR IS NULL AND (trk.DATDRAFTISSUED IS NULL OR trk.DATDRAFTISSUED < trk.DATTOBRANCHCHEIF) THEN trk.DATTOBRANCHCHEIF WHEN trk.DATEPAENDS IS NOT NULL THEN trk.DATEPAENDS WHEN trk.DATPNEXPIRES IS NOT NULL AND trk.DATPNEXPIRES < GETDATE() THEN trk.DATPNEXPIRES WHEN trk.DATPNEXPIRES IS NOT NULL AND trk.DATPNEXPIRES >= GETDATE() THEN trk.DATPNEXPIRES WHEN trk.DATDRAFTISSUED IS NOT NULL AND trk.DATPNEXPIRES IS NULL THEN trk.DATDRAFTISSUED WHEN trk.DATTOPMII IS NOT NULL THEN trk.DATTOPMII WHEN trk.DATTOPMI IS NOT NULL THEN trk.DATTOPMI WHEN trk.DATREVIEWSUBMITTED IS NOT NULL AND (dat.STRSSCPUNIT <> '0' OR dat.STRISMPUNIT <> '0') THEN trk.DATREVIEWSUBMITTED WHEN app.STRSTAFFRESPONSIBLE IS NULL OR app.STRSTAFFRESPONSIBLE = '0' THEN trk.DATRECEIVEDDATE ELSE trk.DATASSIGNEDTOENGINEER END AS [Status Date]
                            FROM SSPPAPPLICATIONMASTER AS app
                            LEFT JOIN SSPPAPPLICATIONTRACKING AS trk ON app.STRAPPLICATIONNUMBER = trk.STRAPPLICATIONNUMBER
                            LEFT JOIN EPDUSERPROFILES AS prof ON app.STRSTAFFRESPONSIBLE = prof.NUMUSERID
                            LEFT JOIN LOOKUPAPPLICATIONTYPES AS lka ON app.STRAPPLICATIONTYPE = lka.STRAPPLICATIONTYPECODE
                            LEFT JOIN LOOKUPPERMITTYPES AS lkp ON app.STRPERMITTYPE = lkp.STRPERMITTYPECODE
                            LEFT JOIN SSPPAPPLICATIONDATA AS dat ON app.STRAPPLICATIONNUMBER = dat.STRAPPLICATIONNUMBER
                            WHERE app.DATFINALIZEDDATE IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & " AND app.STRSTAFFRESPONSIBLE = @pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND (prof.NUMUNIT = @pid or app.APBUNIT = @pid) "
                    End Select

                Case NavWorkListContext.SbeapCases
                    query = "SELECT cl.NUMCASEID AS [Case ID], lnk.CLIENTID AS [Customer ID], cli.STRCOMPANYNAME AS Customer, cl.DATCASEOPENED AS [Case Opened], cl.STRCASESUMMARY AS Description, cl.DATMODIFINGDATE AS [Last Updated],
                            CASE WHEN cl.NUMSTAFFRESPONSIBLE IS NULL THEN NULL ELSE prof.STRLASTNAME+', '+prof.STRFIRSTNAME END AS [Staff Responsible]
                            FROM SBEAPCASELOG AS cl
                            LEFT JOIN EPDUSERPROFILES AS prof ON cl.NUMSTAFFRESPONSIBLE = prof.NUMUSERID
                            LEFT JOIN SBEAPCASELOGLINK AS lnk ON cl.NUMCASEID = lnk.NUMCASEID
                            LEFT JOIN SBEAPCLIENTS AS cli ON lnk.CLIENTID = cli.CLIENTID
                            WHERE cl.DATCASECLOSED IS NULL "

                    Select Case scope
                        Case NavWorkListScope.StaffView
                            query = query & " AND cl.NUMSTAFFRESPONSIBLE = @pid "
                        Case NavWorkListScope.UnitView
                            query = query & " AND prof.NUMUNIT = @pid "
                    End Select

            End Select

            Return query
        End Function

    End Module
End Namespace