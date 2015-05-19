Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace DAL
    Module FacilitySummary

        Public Function GetFSDataTable(ByVal whichTable As IAIPFacilitySummary.FacilityDataTable, ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = GetQueryString(whichTable)
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetQueryString(ByVal whichTable As IAIPFacilitySummary.FacilityDataTable) As String
            Dim query As String = ""
            Select Case whichTable

                Case IAIPFacilitySummary.FacilityDataTable.ComplianceEnforcement
                    query = _
                    "SELECT STRENFORCEMENTNUMBER , DATDISCOVERYDATE AS ViolationDate " & _
                    "  , STRACTIONTYPE AS HPVStatus , CASE WHEN " & _
                    "      DATENFORCEMENTFINALIZED IS NOT NULL THEN 'Closed' ELSE " & _
                    "      'Open' END AS Status " & _
                    "FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
                    "ORDER BY STRENFORCEMENTNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ComplianceFCE
                    query = _
                    "SELECT fce.STRFCEYEAR AS FCEYear, fm.STRFCENUMBER, (eup.STRLASTNAME || ', ' || " & _
                    "  eup.STRFIRSTNAME) AS ReviewingEngineer, TO_CHAR( " & _
                    "  fce.DATFCECOMPLETED, 'dd-Mon-yyyy') AS FCECompleted, " & _
                    "  fce.STRFCECOMMENTS " & _
                    "FROM AIRBRANCH.SSCPFCE fce " & _
                    "INNER JOIN AIRBRANCH.SSCPFCEMASTER fm " & _
                    "ON fce.STRFCENUMBER = fm.STRFCENUMBER " & _
                    "LEFT JOIN AIRBRANCH.EPDUSERPROFILES eup " & _
                    "ON fce.STRREVIEWER = eup.NUMUSERID " & _
                    "WHERE fm.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY fce.DATFCECOMPLETED DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ComplianceWork
                    query = _
                    "SELECT vw.STRTRACKINGNUMBER, vw.STRACTIVITYNAME, vw.RECEIVEDDATE " & _
                    "FROM AIRBRANCH.VW_SSCPWORKDATAGRID vw " & _
                    "WHERE vw.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY vw.STRTRACKINGNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsCompliance
                    query = _
                    "SELECT strContactKey , strContactPrefix ||' ' || " & _
                    "  strContactFirstName ||' ' ||strContactLastName ||' ' || " & _
                    "  strContactSuffix AS ContactName , strContactTitle , " & _
                    "  strContactCompanyName , strContactPhoneNumber1 , " & _
                    "  strContactFaxNumber , strContactEmail , strContactAddress1 || " & _
                    "  ', ' || strContactAddress2 || ', ' || strContactCity || ', ' " & _
                    "  || strContactState || ' ' || strContactZipCode AS Address , " & _
                    "  strContactDescription " & _
                    "FROM AIRBRANCH.APBContactInformation " & _
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '2%' " & _
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsGeco
                    query = _
                    "SELECT DISTINCT( oua.NUMUSERID ) , oup.STRUSERTYPE ,( " & _
                    "  oup.STRSALUTATION || ' ' || oup.STRFIRSTNAME || ' ' || " & _
                    "  oup.STRLASTNAME ) AS GECOContact , oup.STRTITLE , " & _
                    "  oul.STRUSEREMAIL , oup.STRPHONENUMBER , oup.STRFAXNUMBER , " & _
                    "  oup.STRCOMPANYNAME , oup.STRADDRESS || ', ' || oup.STRCITY || " & _
                    "  ', ' || oup.STRSTATE || ' ' || oup.STRZIP AS Address " & _
                    "FROM AIRBRANCH.OLAPUserAccess oua " & _
                    "INNER JOIN AIRBRANCH.OLAPUserProfile oup " & _
                    "ON oua.NUMUSERID = oup.NUMUSERID " & _
                    "LEFT JOIN AIRBRANCH.OLAPUserLogIN oul " & _
                    "ON oua.NUMUSERID = oul.NUMUSERID " & _
                    "WHERE oua.STRAIRSNUMBER = :AirsNumber"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsPermitting
                    query = _
                    "SELECT strContactKey, strContactPrefix||' '|| " & _
                    "  strContactFirstName||' '||strContactLastName||' '|| " & _
                    "  strContactSuffix AS ContactName, strContactTitle, " & _
                    "  strContactCompanyName, strContactPhoneNumber1, " & _
                    "  strContactFaxNumber, strContactEmail, " & _
                    "  strContactAddress1 || ', '|| strContactAddress2|| ', '|| " & _
                    "  strContactCity|| ', '|| strContactState|| ' '|| " & _
                    "  strContactZipCode AS Address, strContactDescription " & _
                    "FROM AIRBRANCH.APBContactInformation " & _
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '3%' " & _
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsState
                    query = _
                    "SELECT vw.AirProgram, vw.Staff, vw.Unit  " & _
                    "FROM AIRBRANCH.VW_FACILITY_STATECONTACTS vw " & _
                    "WHERE vw.AIRSNUMBER = :AirsNumber "
                Case IAIPFacilitySummary.FacilityDataTable.ContactsTesting
                    query = _
                    "SELECT strContactKey, strContactPrefix ||' ' ||strContactFirstName ||' ' || " & _
                    "  strContactLastName ||' ' ||strContactSuffix AS ContactName , " & _
                    "  strContactTitle , strContactCompanyName , " & _
                    "  strContactPhoneNumber1 , strContactFaxNumber , " & _
                    "  strContactEmail , strContactAddress1 || ', ' || " & _
                    "  strContactAddress2 || ', ' || strContactCity || ', ' || " & _
                    "  strContactState || ' ' || strContactZipCode AS Address , " & _
                    "  strContactDescription " & _
                    "FROM AIRBRANCH.APBContactInformation " & _
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '1%' " & _
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsWebSite
                    query = _
                    "SELECT strContactKey, strContactPrefix||' '|| " & _
                    "  strContactFirstName||' '||strContactLastName||' '|| " & _
                    "  strContactSuffix AS ContactName, strContactTitle , " & _
                    "  strContactCompanyName, strContactPhoneNumber1, " & _
                    "  strContactFaxNumber, strContactEmail, " & _
                    "  strContactAddress1|| ', '|| strContactAddress2|| ', '|| " & _
                    "  strContactCity|| ', '|| strContactState|| ' '|| " & _
                    "  strContactZipCode AS address, strContactDescription " & _
                    "FROM AIRBRANCH.APBContactInformation " & _
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '4%' " & _
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.EIPost2009
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.EIPre2009
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.Fees
                    query = _
                    "SELECT vw.* ,( NUMTOTALFEE - TOTALPAID ) AS Balance " & _
                    "FROM AIRBRANCH.VW_APBFACILITYFEES vw " & _
                    "WHERE vw.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY vw.INTYEAR DESC"
                Case IAIPFacilitySummary.FacilityDataTable.FinancialDeposits
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.FinancialFees
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.FinancialInvoices
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.PermitApplications
                    query = _
                    "SELECT TO_NUMBER( appm.STRAPPLICATIONNUMBER ) AS " & _
                    "  ApplicationNumber , appd.STRFACILITYNAME , " & _
                    "  lat.STRAPPLICATIONTYPEDESC , appt.DATRECEIVEDDATE , CASE WHEN " & _
                    "      appd.STRPERMITNUMBER IS NULL THEN ' ' ELSE SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 1, 4 ) || '-' || SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 5, 3 ) || '-' || SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 8, 4 ) || '-' || SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 12, 1 ) || '-' || SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 13, 2 ) || '-' || SUBSTR( " & _
                    "      appd.STRPERMITNUMBER, 15, 1 ) END AS PermitNumber , " & _
                    "  appt.DATPERMITISSUED , CASE WHEN appm.STRSTAFFRESPONSIBLE = " & _
                    "      '0'  THEN 'Unassigned'   WHEN appm.STRSTAFFRESPONSIBLE IS " & _
                    "      NULL THEN 'Unassigned' ELSE( eup.STRLASTNAME || ', ' || " & _
                    "      eup.STRFIRSTNAME ) END AS StaffResponsible , " & _
                    "  lpt.STRPERMITTYPEDESCRIPTION , CASE WHEN appt.DATPERMITISSUED " & _
                    "      IS NOT NULL OR appm.DATFINALIZEDDATE IS NOT NULL THEN " & _
                    "      '11 – Closed Out' WHEN appt.DATTODIRECTOR IS NOT NULL AND " & _
                    "      appm.DATFINALIZEDDATE IS NULL AND( appt.DATDRAFTISSUED IS " & _
                    "      NULL OR appt.DATDRAFTISSUED < appt.DATTODIRECTOR ) THEN " & _
                    "      '09 – Administrative Review' WHEN appt.DATTOBRANCHCHEIF " & _
                    "      IS NOT NULL AND appm.DATFINALIZEDDATE IS NULL AND " & _
                    "      appt.DATTODIRECTOR IS NULL AND( appt.DATDRAFTISSUED IS " & _
                    "      NULL OR appt.DATDRAFTISSUED < appt.DATTOBRANCHCHEIF ) " & _
                    "               THEN '09 – Administrative Review' WHEN appt.DATEPAENDS IS " & _
                    "      NOT NULL THEN '08 – EPA 45-day Review'     WHEN " & _
                    "      appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES < " & _
                    "      SysDate THEN '07 – Public Notice Expired' WHEN " & _
                    "      appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES >= " & _
                    "      SysDate THEN '06 – Public Notice' WHEN " & _
                    "      appt.DATDRAFTISSUED IS NOT NULL AND appt.DATPNEXPIRES IS " & _
                    "      NULL THEN '05 – Draft Issued' WHEN appt.DATTOPMII IS NOT " & _
                    "      NULL THEN '04 – AT PM'        WHEN appt.DATTOPMI IS NOT " & _
                    "      NULL THEN '03 – At UC'        WHEN " & _
                    "      appt.DATREVIEWSUBMITTED IS NOT NULL AND( appd.STRSSCPUNIT " & _
                    "      <> '0' OR appd.STRISMPUNIT <> '0' ) THEN " & _
                    "      '02 – Internal Review' WHEN appm.STRSTAFFRESPONSIBLE IS " & _
                    "      NULL OR appm.STRSTAFFRESPONSIBLE = '0' THEN " & _
                    "      '0 – Unassigned' ELSE '01 – At Engineer' END AS AppStatus " & _
                    "  , CASE                              WHEN appt.DATPERMITISSUED IS NOT NULL THEN " & _
                    "      appt.DATPERMITISSUED            WHEN appm.DATFINALIZEDDATE IS NOT " & _
                    "      NULL THEN appm.DATFINALIZEDDATE WHEN appt.DATTODIRECTOR " & _
                    "      IS NOT NULL AND appm.DATFINALIZEDDATE IS NULL AND( " & _
                    "      appt.DATDRAFTISSUED IS NULL OR appt.DATDRAFTISSUED < " & _
                    "      appt.DATTODIRECTOR ) THEN appt.DATTODIRECTOR WHEN " & _
                    "      appt.DATTOBRANCHCHEIF IS NOT NULL AND " & _
                    "      appm.DATFINALIZEDDATE IS NULL AND appt.DATTODIRECTOR IS " & _
                    "      NULL AND( appt.DATDRAFTISSUED IS NULL OR " & _
                    "      appt.DATDRAFTISSUED < appt.DATTOBRANCHCHEIF ) THEN " & _
                    "      appt.DATTOBRANCHCHEIF WHEN appt.DATEPAENDS IS NOT NULL " & _
                    "                                      THEN appt.DATEPAENDS    WHEN appt.DATPNEXPIRES IS NOT NULL " & _
                    "      AND appt.DATPNEXPIRES < SysDate THEN appt.DATPNEXPIRES " & _
                    "                                     WHEN appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES >= " & _
                    "      SysDate                                             THEN appt.DATPNEXPIRES WHEN appt.DATDRAFTISSUED " & _
                    "      IS NOT NULL AND appt.DATPNEXPIRES IS NULL           THEN " & _
                    "      appt.DATDRAFTISSUED WHEN appt.DATTOPMII IS NOT NULL THEN " & _
                    "      appt.DATTOPMII      WHEN appt.DATTOPMI IS NOT NULL  THEN " & _
                    "      appt.DATTOPMI       WHEN appt.DATREVIEWSUBMITTED IS NOT " & _
                    "      NULL AND( appd.STRSSCPUNIT <> '0' OR appd.STRISMPUNIT <> " & _
                    "      '0' ) THEN appt.DATREVIEWSUBMITTED WHEN " & _
                    "      appm.STRSTAFFRESPONSIBLE IS NULL OR " & _
                    "      appm.STRSTAFFRESPONSIBLE = '0' THEN NULL ELSE " & _
                    "      appt.DATASSIGNEDTOENGINEER END AS StatusDate " & _
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " & _
                    "LEFT JOIN AIRBRANCH.SSPPApplicationTracking appt " & _
                    "ON appm.STRAPPLICATIONNUMBER = appt.STRAPPLICATIONNUMBER " & _
                    "LEFT JOIN AIRBRANCH.SSPPApplicationData appd " & _
                    "ON appm.STRAPPLICATIONNUMBER = appd.STRAPPLICATIONNUMBER " & _
                    "LEFT JOIN AIRBRANCH.LookUpApplicationTypes lat " & _
                    "ON appm.STRAPPLICATIONTYPE = lat.STRAPPLICATIONTYPECODE " & _
                    "LEFT JOIN AIRBRANCH.LookUPPermitTypes lpt " & _
                    "ON appm.STRPERMITTYPE = lpt.STRPERMITTYPECODE " & _
                    "LEFT JOIN AIRBRANCH.EPDUserProfiles eup " & _
                    "ON appm.STRSTAFFRESPONSIBLE = eup.NUMUSERID " & _
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY APPLICATIONNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.PermitRuleHistory
                    query = _
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " & _
                    "  AppActivity , 'SIP' AS Part , lsip.STRSUBPART , " & _
                    "  sd.CREATEDATETIME , lsip.STRDESCRIPTION " & _
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " & _
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " & _
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " & _
                    "INNER JOIN AIRBRANCH.LookUpSubpartSIP lsip " & _
                    "ON sd.STRSUBPART = lsip.STRSUBPART " & _
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " & _
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '0' " & _
                    "UNION " & _
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " & _
                    "  AppActivity , 'NSPS (Part 60)' AS Part , l60.STRSUBPART , " & _
                    "  sd.CREATEDATETIME , l60.STRDESCRIPTION " & _
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " & _
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " & _
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " & _
                    "INNER JOIN AIRBRANCH.LookUpSubpart60 l60 " & _
                    "ON sd.STRSUBPART = l60.STRSUBPART " & _
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " & _
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '9' " & _
                    "UNION " & _
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " & _
                    "  AppActivity , 'NESHAP (Part 61)' AS Part , l61.STRSUBPART , " & _
                    "  sd.CREATEDATETIME , l61.STRDESCRIPTION " & _
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " & _
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " & _
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " & _
                    "INNER JOIN AIRBRANCH.LookUpSubpart61 l61 " & _
                    "ON sd.STRSUBPART = l61.STRSUBPART " & _
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " & _
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '8' " & _
                    "UNION " & _
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " & _
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " & _
                    "  AppActivity , 'MACT (Part 63)' AS Subpart , l63.STRSUBPART , " & _
                    "  sd.CREATEDATETIME , l63.STRDESCRIPTION " & _
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " & _
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " & _
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " & _
                    "INNER JOIN AIRBRANCH.LookUpSubpart63 l63 " & _
                    "ON sd.STRSUBPART = l63.STRSUBPART " & _
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " & _
                    "  sd.STRSUBPARTKEY, 6, 1 ) = 'M'"
                Case IAIPFacilitySummary.FacilityDataTable.PermitRules
                    query = _
                    "SELECT 'SIP' AS Part , lsip.STRSUBPART , lsip.STRDESCRIPTION , " & _
                    "  sd.CREATEDATETIME " & _
                    "FROM AIRBRANCH.APBSubpartData sd " & _
                    "INNER JOIN AIRBRANCH.LookUpSubPartSIP lsip " & _
                    "ON sd.STRSUBPART = lsip.STRSUBPART " & _
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " & _
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '0' " & _
                    "UNION " & _
                    "SELECT 'NSPS (Part 60)' AS Part , l60.STRSUBPART , " & _
                    "  l60.STRDESCRIPTION , sd.CREATEDATETIME " & _
                    "FROM AIRBRANCH.APBSubpartData sd " & _
                    "INNER JOIN AIRBRANCH.LookUpSubPart60 l60 " & _
                    "ON sd.STRSUBPART = l60.STRSUBPART " & _
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " & _
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '9' " & _
                    "UNION " & _
                    "SELECT 'NESHAP (Part 61)' AS Part , l61.STRSUBPART , " & _
                    "  l61.STRDESCRIPTION , sd.CREATEDATETIME " & _
                    "FROM AIRBRANCH.APBSubpartData sd " & _
                    "INNER JOIN AIRBRANCH.LookUpSubPart61 l61 " & _
                    "ON sd.STRSUBPART = l61.STRSUBPART " & _
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " & _
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '8' " & _
                    "UNION " & _
                    "SELECT 'MACT (Part 63)' AS Part , l63.STRSUBPART , " & _
                    "  l63.STRDESCRIPTION , sd.CREATEDATETIME " & _
                    "FROM AIRBRANCH.APBSubpartData sd " & _
                    "INNER JOIN AIRBRANCH.LookUpSubPart63 l63 " & _
                    "ON sd.STRSUBPART = l63.STRSUBPART " & _
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " & _
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = 'M' " & _
                    "ORDER BY Part , STRSUBPART"
                Case IAIPFacilitySummary.FacilityDataTable.Permits
                    query = _
                    "SELECT STRPERMITNUMBER , DATISSUED , DATREVOKED , ACTIVE " & _
                    "FROM AIRBRANCH.APBISSUEDPERMIT " & _
                    "WHERE STRAIRSNUMBER = SUBSTR( :AirsNumber, 5, 8 ) " & _
                    "ORDER BY DATISSUED DESC NULLS FIRST"
                Case IAIPFacilitySummary.FacilityDataTable.TestMemos
                    query = _
                    "SELECT tm.STRREFERENCENUMBER , SUBSTR( tm.STRMEMORANDUMFIELD, 0 " & _
                    "  , 150 ) AS MemoField " & _
                    "FROM AIRBRANCH.ISMPTestREportMemo tm " & _
                    "INNER JOIN AIRBRANCH.ISMPMaster im " & _
                    "ON tm.STRREFERENCENUMBER = im.STRREFERENCENUMBER " & _
                    "WHERE im.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY tm.STRREFERENCENUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.TestNotifications
                    query = _
                    "SELECT tn.STRTESTLOGNUMBER ,( up.STRLASTNAME || ', ' || " & _
                    "  up.STRFIRSTNAME ) AS Staff , tn.STREMISSIONUNIT , " & _
                    "  eu.STRUNITDESC , tn.DATTESTNOTIFICATION , " & _
                    "  tn.DATPROPOSEDSTARTDATE , tn.DATPROPOSEDENDDATE " & _
                    "FROM AIRBRANCH.ISMPTestNotification tn " & _
                    "LEFT JOIN AIRBRANCH.EPDUserProfiles up " & _
                    "ON tn.STRSTAFFRESPONSIBLE = up.NUMUSERID " & _
                    "LEFT JOIN AIRBRANCH.LookUpEPDUnits eu " & _
                    "ON up.NUMUNIT = eu.NUMUNITCODE " & _
                    "WHERE tn.STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY tn.STRTESTLOGNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.TestReports
                    query = _
                    "SELECT STRREFERENCENUMBER , STATUS , STREMISSIONSOURCE , " & _
                    "  STRPOLLUTANTDESCRIPTION , STRREPORTTYPE , REVIEWINGENGINEER , " & _
                    "  TESTDATESTART , RECEIVEDDATE , COMPLETEDATE , " & _
                    "  STRCOMPLIANCESTATUS " & _
                    "FROM AIRBRANCH.VW_ISMPWORKDATAGRID " & _
                    "WHERE STRAIRSNUMBER = :AirsNumber " 
            End Select

            Return query
        End Function

    End Module
End Namespace
