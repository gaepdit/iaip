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
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.PermitRuleHistory
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.PermitRules
                    query = _
                    ""
                Case IAIPFacilitySummary.FacilityDataTable.Permits
                    query = _
                    ""
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
                    "WHERE STRAIRSNUMBER = :AirsNumber " & _
                    "ORDER BY STRREFERENCENUMBER DESC "
            End Select

            Return query
        End Function

    End Module
End Namespace
