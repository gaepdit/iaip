Imports System.Data.SqlClient
Imports Oracle.ManagedDataAccess.Types

Namespace DAL
    Module FacilitySummaryData

        Public Function GetFSDataTable(ByVal whichTable As IAIPFacilitySummary.FacilityDataTable, ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = GetQueryString(whichTable)
            Dim parameter As SqlParameter = New SqlParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetQueryString(ByVal whichTable As IAIPFacilitySummary.FacilityDataTable) As String
            Dim query As String = ""
            Select Case whichTable

                Case IAIPFacilitySummary.FacilityDataTable.ComplianceEnforcement
                    query =
                    "SELECT STRENFORCEMENTNUMBER , DATDISCOVERYDATE AS ViolationDate " &
                    "  , STRACTIONTYPE AS HPVStatus , CASE WHEN " &
                    "      DATENFORCEMENTFINALIZED IS NOT NULL THEN 'Closed' ELSE " &
                    "      'Open' END AS Status " &
                    "FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                    "WHERE STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY STRENFORCEMENTNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ComplianceFCE
                    query =
                    "SELECT fce.STRFCEYEAR AS FCEYear, fm.STRFCENUMBER, (eup.STRLASTNAME || ', ' || " &
                    "  eup.STRFIRSTNAME) AS ReviewingEngineer, TO_CHAR( " &
                    "  fce.DATFCECOMPLETED, 'dd-Mon-yyyy') AS FCECompleted, " &
                    "  fce.STRFCECOMMENTS " &
                    "FROM AIRBRANCH.SSCPFCE fce " &
                    "INNER JOIN AIRBRANCH.SSCPFCEMASTER fm " &
                    "ON fce.STRFCENUMBER = fm.STRFCENUMBER " &
                    "LEFT JOIN AIRBRANCH.EPDUSERPROFILES eup " &
                    "ON fce.STRREVIEWER = eup.NUMUSERID " &
                    "WHERE fm.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY fce.DATFCECOMPLETED DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ComplianceWork
                    query =
                    "SELECT vw.STRTRACKINGNUMBER, vw.STRACTIVITYNAME, vw.RECEIVEDDATE " &
                    "FROM AIRBRANCH.VW_SSCPWORKDATAGRID vw " &
                    "WHERE vw.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY vw.STRTRACKINGNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsCompliance
                    query =
                    "SELECT strContactKey , strContactPrefix ||' ' || " &
                    "  strContactFirstName ||' ' ||strContactLastName ||' ' || " &
                    "  strContactSuffix AS ContactName , strContactTitle , " &
                    "  strContactCompanyName , strContactPhoneNumber1 , " &
                    "  strContactFaxNumber , strContactEmail , strContactAddress1 || " &
                    "  ', ' || strContactAddress2 || ', ' || strContactCity || ', ' " &
                    "  || strContactState || ' ' || strContactZipCode AS Address , " &
                    "  strContactDescription " &
                    "FROM AIRBRANCH.APBContactInformation " &
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '2%' " &
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsGeco
                    query =
                    "SELECT DISTINCT( oua.NUMUSERID ) , oup.STRUSERTYPE ,( " &
                    "  oup.STRSALUTATION || ' ' || oup.STRFIRSTNAME || ' ' || " &
                    "  oup.STRLASTNAME ) AS GECOContact , oup.STRTITLE , " &
                    "  oul.STRUSEREMAIL , oup.STRPHONENUMBER , oup.STRFAXNUMBER , " &
                    "  oup.STRCOMPANYNAME , oup.STRADDRESS || ', ' || oup.STRCITY || " &
                    "  ', ' || oup.STRSTATE || ' ' || oup.STRZIP AS Address " &
                    "FROM AIRBRANCH.OLAPUserAccess oua " &
                    "INNER JOIN AIRBRANCH.OLAPUserProfile oup " &
                    "ON oua.NUMUSERID = oup.NUMUSERID " &
                    "LEFT JOIN AIRBRANCH.OLAPUserLogIN oul " &
                    "ON oua.NUMUSERID = oul.NUMUSERID " &
                    "WHERE oua.STRAIRSNUMBER = :AirsNumber"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsPermitting
                    query =
                    "SELECT strContactKey, strContactPrefix||' '|| " &
                    "  strContactFirstName||' '||strContactLastName||' '|| " &
                    "  strContactSuffix AS ContactName, strContactTitle, " &
                    "  strContactCompanyName, strContactPhoneNumber1, " &
                    "  strContactFaxNumber, strContactEmail, " &
                    "  strContactAddress1 || ', '|| strContactAddress2|| ', '|| " &
                    "  strContactCity|| ', '|| strContactState|| ' '|| " &
                    "  strContactZipCode AS Address, strContactDescription " &
                    "FROM AIRBRANCH.APBContactInformation " &
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '3%' " &
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsState
                    query =
                    "SELECT vw.AirProgram, vw.Staff, vw.Unit  " &
                    "FROM AIRBRANCH.VW_FACILITY_STATECONTACTS vw " &
                    "WHERE vw.AIRSNUMBER = :AirsNumber "
                Case IAIPFacilitySummary.FacilityDataTable.ContactsTesting
                    query =
                    "SELECT strContactKey, strContactPrefix ||' ' ||strContactFirstName ||' ' || " &
                    "  strContactLastName ||' ' ||strContactSuffix AS ContactName , " &
                    "  strContactTitle , strContactCompanyName , " &
                    "  strContactPhoneNumber1 , strContactFaxNumber , " &
                    "  strContactEmail , strContactAddress1 || ', ' || " &
                    "  strContactAddress2 || ', ' || strContactCity || ', ' || " &
                    "  strContactState || ' ' || strContactZipCode AS Address , " &
                    "  strContactDescription " &
                    "FROM AIRBRANCH.APBContactInformation " &
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '1%' " &
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.ContactsWebSite
                    query =
                    "SELECT strContactKey, strContactPrefix||' '|| " &
                    "  strContactFirstName||' '||strContactLastName||' '|| " &
                    "  strContactSuffix AS ContactName, strContactTitle , " &
                    "  strContactCompanyName, strContactPhoneNumber1, " &
                    "  strContactFaxNumber, strContactEmail, " &
                    "  strContactAddress1|| ', '|| strContactAddress2|| ', '|| " &
                    "  strContactCity|| ', '|| strContactState|| ' '|| " &
                    "  strContactZipCode AS address, strContactDescription " &
                    "FROM AIRBRANCH.APBContactInformation " &
                    "WHERE strAIRSNumber = :AirsNumber AND strKey LIKE '4%' " &
                    "ORDER BY strContactKey"
                Case IAIPFacilitySummary.FacilityDataTable.EiPost2009
                    query =
                    "SELECT INTINVENTORYYEAR , LEADEMISSIONS , " &
                    "  COEMISSIONS , NH3EMISSIONS , NOXEMISSIONS , PMCONEMISSIONS , " &
                    "  PM10FILEMISSIONS , PM10PRIEMISSIONS , PM25FILEMISSIONS , " &
                    "  PM25PRIEMISSIONS , SO2EMISSIONS , VOCEMISSIONS " &
                    "FROM AIRBRANCH.VW_EIS_EMISSIONSUMMARY " &
                    "WHERE FACILITYSITEID =SUBSTR(:AirsNumber,5,8) " &
                    "ORDER BY INTINVENTORYYEAR DESC"
                Case IAIPFacilitySummary.FacilityDataTable.EiPre2009
                    query =
                    "SELECT DISTINCT( em.STRINVENTORYYEAR ) , CASE WHEN " &
                    "      COTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      COTable.TotalEmissions END CO , CASE WHEN " &
                    "      LeadTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      LeadTable.TotalEmissions END Lead , CASE WHEN " &
                    "      NH3Table.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      NH3Table.TotalEmissions END NH3 , CASE WHEN " &
                    "      NOXTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      NOXTable.TotalEmissions END NOX , CASE WHEN " &
                    "      PMTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      PMTable.TotalEmissions END PM , CASE WHEN " &
                    "      PM10Table.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      PM10Table.TotalEmissions END PM10 , CASE WHEN " &
                    "      PM25Table.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      PM25Table.TotalEmissions END PM25 , CASE WHEN " &
                    "      SO2Table.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      SO2Table.TotalEmissions END SO2 , CASE WHEN " &
                    "      VOCTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      VOCTable.TotalEmissions END VOC , CASE WHEN " &
                    "      PMFILTable.TotalEmissions IS NULL THEN 0 ELSE " &
                    "      PMFILTable.TotalEmissions END PMFIL " &
                    "FROM AIRBRANCH.EIEM em " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = '7439921' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) LeadTable " &
                    "ON em.STRINVENTORYYEAR = LeadTable.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'NOX' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) NOXTable " &
                    "ON em.STRINVENTORYYEAR = NOXTable.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'CO' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) COTable " &
                    "ON em.STRINVENTORYYEAR = COTable.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'SO2' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) SO2Table " &
                    "ON em.STRINVENTORYYEAR = SO2Table.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'NH3' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) NH3Table " &
                    "ON em.STRINVENTORYYEAR = NH3Table.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'PM-PRI' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) PMTable " &
                    "ON em.STRINVENTORYYEAR = PMTable.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'PM10-PMI' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) PM10Table " &
                    "ON em.STRINVENTORYYEAR = PM10Table.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'PM25-PMI' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) PM25Table " &
                    "ON em.STRINVENTORYYEAR = PM25Table.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'PM-FIL' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) PMFILTable " &
                    "ON em.STRINVENTORYYEAR = PMFILTable.STRINVENTORYYEAR " &
                    "LEFT JOIN " &
                    "  (SELECT em.STRPOLLUTANTCODE AS PollutantCode , SUM( " &
                    "    em.DBLEMISSIONNUMERICVALUE ) AS TotalEmissions , " &
                    "    em.STRINVENTORYYEAR , em.STRSTATEFACILITYIDENTIFIER " &
                    "  FROM AIRBRANCH.EIEM em " &
                    "  WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, " &
                    "    8 ) AND em.STRPOLLUTANTCODE = 'VOC' " &
                    "  GROUP BY em.STRPOLLUTANTCODE , em.STRINVENTORYYEAR , " &
                    "    em.STRSTATEFACILITYIDENTIFIER " &
                    "  ) VOCTable " &
                    "ON em.STRINVENTORYYEAR = VOCTable.STRINVENTORYYEAR " &
                    "WHERE em.STRSTATEFACILITYIDENTIFIER = SUBSTR( :AirsNumber, 5, 8 " &
                    "  ) " &
                    "ORDER BY em.STRINVENTORYYEAR DESC"
                Case IAIPFacilitySummary.FacilityDataTable.Fees
                    query =
                    "SELECT vw.* ,( NUMTOTALFEE - TOTALPAID ) AS Balance " &
                    "FROM AIRBRANCH.VW_APBFACILITYFEES vw " &
                    "WHERE vw.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY vw.INTYEAR DESC"
                Case IAIPFacilitySummary.FacilityDataTable.FinancialDeposits
                    query =
                    "SELECT fst.NUMFEEYEAR , fst.NUMPAYMENT , fst.DATTRANSACTIONDATE " &
                    "  , fst.INVOICEID , fst.STRCHECKNO , fst.STRDEPOSITNO , CASE " &
                    "    WHEN fst.TRANSACTIONTYPECODE = '1' THEN 'Deposit' WHEN " &
                    "      fst.TRANSACTIONTYPECODE = '2'    THEN 'Refund' ELSE 'N/A' " &
                    "  END TRANSACTIONTYPECODE , fst.STRBATCHNO , CASE WHEN " &
                    "      fst.STRENTRYPERSON IS NULL THEN '' ELSE( eup.STRLASTNAME " &
                    "      || ', ' || eup.STRFIRSTNAME ) END strEntryPerson , " &
                    "  fst.STRCOMMENT , fst.TRANSACTIONID " &
                    "FROM AIRBRANCH.FS_Transactions fst " &
                    "LEFT JOIN AIRBRANCH.EPDUserProfiles eup " &
                    "ON fst.STRENTRYPERSON = eup.NUMUSERID " &
                    "WHERE fst.STRAIRSNUMBER = :AirsNumber AND fst.ACTIVE = '1' " &
                    "ORDER BY fst.NUMFEEYEAR DESC , fst.DATTRANSACTIONDATE DESC"
                Case IAIPFacilitySummary.FacilityDataTable.FinancialFees
                    query =
                    "SELECT fad.NUMFEEYEAR , fad.INTVOCTONS , fad.INTPMTONS , " &
                    "  fad.INTSO2TONS , fad.INTNOXTONS , fad.NUMPART70FEE , " &
                    "  fad.NUMSMFEE , fad.NUMTOTALFEE , CASE WHEN fad.STRNSPSEXEMPT " &
                    "      = '1'                      THEN 'YES'                  WHEN fad.STRNSPSEXEMPT " &
                    "      = '0'                      THEN 'NO' END strNSPSExempt , '' AS strNSPSReason , " &
                    "  CASE WHEN fad.STROPERATE = '1' THEN 'YES' WHEN fad.STROPERATE " &
                    "      = '0'                      THEN 'NO' END strOperate , " &
                    "  fad.NUMFEERATE , fad.STRNSPSEXEMPTREASON , CASE        WHEN " &
                    "      fad.STRPART70 = '1'         THEN 'YES'                     WHEN " &
                    "      fad.STRPART70 = '0'         THEN 'NO' END strPart70 , CASE WHEN " &
                    "      fad.STRSYNTHETICMINOR = '1' THEN 'YES'                     WHEN " &
                    "      fad.STRSYNTHETICMINOR = '0' THEN 'NO' END " &
                    "  strSyntheticMinor , fad.NUMCALCULATEDFEE , fad.STRCLASS , " &
                    "  CASE WHEN fad.STRNSPS = '1' THEN 'YES' WHEN fad.STRNSPS = '0' " &
                    "                              THEN 'NO' END strNSPS , " &
                    "  fad.DATSHUTDOWN , fad.STRPAYMENTPLAN , fad.STROFFICIALNAME , " &
                    "  fad.STROFFICIALTITLE , CASE WHEN fsa.INTSUBMITTAL = '1' THEN " &
                    "      'YES'                   WHEN fsa.INTSUBMITTAL = '0' THEN " &
                    "      'NO' END intSubmittal , fsa.DATSUBMITTAL " &
                    "FROM AIRBRANCH.FS_FeeAuditedData fad " &
                    "INNER JOIN AIRBRANCH.FS_Admin fsa " &
                    "ON fad.STRAIRSNUMBER = fsa.STRAIRSNUMBER AND fsa.NUMFEEYEAR = " &
                    "  fad.NUMFEEYEAR " &
                    "WHERE fad.STRAIRSNUMBER = :AirsNumber AND fsa.ACTIVE = '1' AND " &
                    "  fsa.STRENROLLED IS NOT NULL AND fsa.STRENROLLED = '1' " &
                    "ORDER BY fad.NUMFEEYEAR DESC"
                Case IAIPFacilitySummary.FacilityDataTable.FinancialInvoices
                    query =
                    "SELECT DISTINCT ffi.NUMFEEYEAR , ffi.INVOICEID , ffi.NUMAMOUNT " &
                    "  , ffi.DATINVOICEDATE , CASE                        WHEN ffi.ACTIVE = '1' THEN " &
                    "      'Active'                                       WHEN ffi.ACTIVE = '0' THEN 'VOID' " &
                    "  END InvoiceStatus , lpt.STRPAYTYPEDESC , CASE      WHEN " &
                    "      ffi.STRINVOICESTATUS = '1' THEN 'Paid in Full' WHEN " &
                    "      ffi.STRINVOICESTATUS = '0' AND( fst.NUMPAYMENT <> '0' AND " &
                    "      fst.NUMPAYMENT IS NOT NULL AND fst.ACTIVE = '1' ) THEN " &
                    "      'Partial Payment' WHEN ffi.STRINVOICESTATUS = '0' THEN " &
                    "      'Unpaid' END PayStatus , ffi.STRCOMMENT " &
                    "FROM AIRBRANCH.FS_FeeInvoice ffi " &
                    "INNER JOIN AIRBRANCH.FSLK_PayType lpt " &
                    "ON ffi.STRPAYTYPE = lpt.NUMPAYTYPEID " &
                    "LEFT JOIN AIRBRANCH.FS_Transactions fst " &
                    "ON ffi.INVOICEID = fst.INVOICEID " &
                    "WHERE ffi.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY ffi.NUMFEEYEAR DESC , ffi.DATINVOICEDATE DESC"
                Case IAIPFacilitySummary.FacilityDataTable.PermitApplications
                    query =
                    "SELECT TO_NUMBER( appm.STRAPPLICATIONNUMBER ) AS " &
                    "  ApplicationNumber , appd.STRFACILITYNAME , " &
                    "  lat.STRAPPLICATIONTYPEDESC , appt.DATRECEIVEDDATE , CASE WHEN " &
                    "      appd.STRPERMITNUMBER IS NULL THEN ' ' ELSE SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 1, 4 ) || '-' || SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 5, 3 ) || '-' || SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 8, 4 ) || '-' || SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 12, 1 ) || '-' || SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 13, 2 ) || '-' || SUBSTR( " &
                    "      appd.STRPERMITNUMBER, 15, 1 ) END AS PermitNumber , " &
                    "  appt.DATPERMITISSUED , CASE WHEN appm.STRSTAFFRESPONSIBLE = " &
                    "      '0'  THEN 'Unassigned'   WHEN appm.STRSTAFFRESPONSIBLE IS " &
                    "      NULL THEN 'Unassigned' ELSE( eup.STRLASTNAME || ', ' || " &
                    "      eup.STRFIRSTNAME ) END AS StaffResponsible , " &
                    "  lpt.STRPERMITTYPEDESCRIPTION , CASE WHEN appt.DATPERMITISSUED " &
                    "      IS NOT NULL OR appm.DATFINALIZEDDATE IS NOT NULL THEN " &
                    "      '11 – Closed Out' WHEN appt.DATTODIRECTOR IS NOT NULL AND " &
                    "      appm.DATFINALIZEDDATE IS NULL AND( appt.DATDRAFTISSUED IS " &
                    "      NULL OR appt.DATDRAFTISSUED < appt.DATTODIRECTOR ) THEN " &
                    "      '09 – Administrative Review' WHEN appt.DATTOBRANCHCHEIF " &
                    "      IS NOT NULL AND appm.DATFINALIZEDDATE IS NULL AND " &
                    "      appt.DATTODIRECTOR IS NULL AND( appt.DATDRAFTISSUED IS " &
                    "      NULL OR appt.DATDRAFTISSUED < appt.DATTOBRANCHCHEIF ) " &
                    "               THEN '09 – Administrative Review' WHEN appt.DATEPAENDS IS " &
                    "      NOT NULL THEN '08 – EPA 45-day Review'     WHEN " &
                    "      appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES < " &
                    "      SysDate THEN '07 – Public Notice Expired' WHEN " &
                    "      appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES >= " &
                    "      SysDate THEN '06 – Public Notice' WHEN " &
                    "      appt.DATDRAFTISSUED IS NOT NULL AND appt.DATPNEXPIRES IS " &
                    "      NULL THEN '05 – Draft Issued' WHEN appt.DATTOPMII IS NOT " &
                    "      NULL THEN '04 – AT PM'        WHEN appt.DATTOPMI IS NOT " &
                    "      NULL THEN '03 – At UC'        WHEN " &
                    "      appt.DATREVIEWSUBMITTED IS NOT NULL AND( appd.STRSSCPUNIT " &
                    "      <> '0' OR appd.STRISMPUNIT <> '0' ) THEN " &
                    "      '02 – Internal Review' WHEN appm.STRSTAFFRESPONSIBLE IS " &
                    "      NULL OR appm.STRSTAFFRESPONSIBLE = '0' THEN " &
                    "      '0 – Unassigned' ELSE '01 – At Engineer' END AS AppStatus " &
                    "  , CASE                              WHEN appt.DATPERMITISSUED IS NOT NULL THEN " &
                    "      appt.DATPERMITISSUED            WHEN appm.DATFINALIZEDDATE IS NOT " &
                    "      NULL THEN appm.DATFINALIZEDDATE WHEN appt.DATTODIRECTOR " &
                    "      IS NOT NULL AND appm.DATFINALIZEDDATE IS NULL AND( " &
                    "      appt.DATDRAFTISSUED IS NULL OR appt.DATDRAFTISSUED < " &
                    "      appt.DATTODIRECTOR ) THEN appt.DATTODIRECTOR WHEN " &
                    "      appt.DATTOBRANCHCHEIF IS NOT NULL AND " &
                    "      appm.DATFINALIZEDDATE IS NULL AND appt.DATTODIRECTOR IS " &
                    "      NULL AND( appt.DATDRAFTISSUED IS NULL OR " &
                    "      appt.DATDRAFTISSUED < appt.DATTOBRANCHCHEIF ) THEN " &
                    "      appt.DATTOBRANCHCHEIF WHEN appt.DATEPAENDS IS NOT NULL " &
                    "                                      THEN appt.DATEPAENDS    WHEN appt.DATPNEXPIRES IS NOT NULL " &
                    "      AND appt.DATPNEXPIRES < SysDate THEN appt.DATPNEXPIRES " &
                    "                                     WHEN appt.DATPNEXPIRES IS NOT NULL AND appt.DATPNEXPIRES >= " &
                    "      SysDate                                             THEN appt.DATPNEXPIRES WHEN appt.DATDRAFTISSUED " &
                    "      IS NOT NULL AND appt.DATPNEXPIRES IS NULL           THEN " &
                    "      appt.DATDRAFTISSUED WHEN appt.DATTOPMII IS NOT NULL THEN " &
                    "      appt.DATTOPMII      WHEN appt.DATTOPMI IS NOT NULL  THEN " &
                    "      appt.DATTOPMI       WHEN appt.DATREVIEWSUBMITTED IS NOT " &
                    "      NULL AND( appd.STRSSCPUNIT <> '0' OR appd.STRISMPUNIT <> " &
                    "      '0' ) THEN appt.DATREVIEWSUBMITTED WHEN " &
                    "      appm.STRSTAFFRESPONSIBLE IS NULL OR " &
                    "      appm.STRSTAFFRESPONSIBLE = '0' THEN NULL ELSE " &
                    "      appt.DATASSIGNEDTOENGINEER END AS StatusDate " &
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " &
                    "LEFT JOIN AIRBRANCH.SSPPApplicationTracking appt " &
                    "ON appm.STRAPPLICATIONNUMBER = appt.STRAPPLICATIONNUMBER " &
                    "LEFT JOIN AIRBRANCH.SSPPApplicationData appd " &
                    "ON appm.STRAPPLICATIONNUMBER = appd.STRAPPLICATIONNUMBER " &
                    "LEFT JOIN AIRBRANCH.LookUpApplicationTypes lat " &
                    "ON appm.STRAPPLICATIONTYPE = lat.STRAPPLICATIONTYPECODE " &
                    "LEFT JOIN AIRBRANCH.LookUPPermitTypes lpt " &
                    "ON appm.STRPERMITTYPE = lpt.STRPERMITTYPECODE " &
                    "LEFT JOIN AIRBRANCH.EPDUserProfiles eup " &
                    "ON appm.STRSTAFFRESPONSIBLE = eup.NUMUSERID " &
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY APPLICATIONNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.PermitRuleHistory
                    query =
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " &
                    "  AppActivity , 'SIP' AS Part , lsip.STRSUBPART , " &
                    "  sd.CREATEDATETIME , lsip.STRDESCRIPTION " &
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " &
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " &
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " &
                    "INNER JOIN AIRBRANCH.LookUpSubpartSIP lsip " &
                    "ON sd.STRSUBPART = lsip.STRSUBPART " &
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " &
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '0' " &
                    "UNION " &
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " &
                    "  AppActivity , 'NSPS (Part 60)' AS Part , l60.STRSUBPART , " &
                    "  sd.CREATEDATETIME , l60.STRDESCRIPTION " &
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " &
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " &
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " &
                    "INNER JOIN AIRBRANCH.LookUpSubpart60 l60 " &
                    "ON sd.STRSUBPART = l60.STRSUBPART " &
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " &
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '9' " &
                    "UNION " &
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " &
                    "  AppActivity , 'NESHAP (Part 61)' AS Part , l61.STRSUBPART , " &
                    "  sd.CREATEDATETIME , l61.STRDESCRIPTION " &
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " &
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " &
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " &
                    "INNER JOIN AIRBRANCH.LookUpSubpart61 l61 " &
                    "ON sd.STRSUBPART = l61.STRSUBPART " &
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " &
                    "  sd.STRSUBPARTKEY, 6, 1 ) = '8' " &
                    "UNION " &
                    "SELECT sd.STRAPPLICATIONNUMBER , CASE                WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '0' THEN 'Removed' WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '1' THEN 'Added'   WHEN " &
                    "      sd.STRAPPLICATIONACTIVITY = '2' THEN 'Modified' END " &
                    "  AppActivity , 'MACT (Part 63)' AS Subpart , l63.STRSUBPART , " &
                    "  sd.CREATEDATETIME , l63.STRDESCRIPTION " &
                    "FROM AIRBRANCH.SSPPApplicationMaster appm " &
                    "INNER JOIN AIRBRANCH.SSPPSubpartData sd " &
                    "ON appm.STRAPPLICATIONNUMBER = sd.STRAPPLICATIONNUMBER " &
                    "INNER JOIN AIRBRANCH.LookUpSubpart63 l63 " &
                    "ON sd.STRSUBPART = l63.STRSUBPART " &
                    "WHERE appm.STRAIRSNUMBER = :AirsNumber AND SUBSTR( " &
                    "  sd.STRSUBPARTKEY, 6, 1 ) = 'M'"
                Case IAIPFacilitySummary.FacilityDataTable.PermitRules
                    query =
                    "SELECT 'SIP' AS Part , lsip.STRSUBPART , lsip.STRDESCRIPTION , " &
                    "  sd.CREATEDATETIME " &
                    "FROM AIRBRANCH.APBSubpartData sd " &
                    "INNER JOIN AIRBRANCH.LookUpSubPartSIP lsip " &
                    "ON sd.STRSUBPART = lsip.STRSUBPART " &
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " &
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '0' " &
                    "UNION " &
                    "SELECT 'NSPS (Part 60)' AS Part , l60.STRSUBPART , " &
                    "  l60.STRDESCRIPTION , sd.CREATEDATETIME " &
                    "FROM AIRBRANCH.APBSubpartData sd " &
                    "INNER JOIN AIRBRANCH.LookUpSubPart60 l60 " &
                    "ON sd.STRSUBPART = l60.STRSUBPART " &
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " &
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '9' " &
                    "UNION " &
                    "SELECT 'NESHAP (Part 61)' AS Part , l61.STRSUBPART , " &
                    "  l61.STRDESCRIPTION , sd.CREATEDATETIME " &
                    "FROM AIRBRANCH.APBSubpartData sd " &
                    "INNER JOIN AIRBRANCH.LookUpSubPart61 l61 " &
                    "ON sd.STRSUBPART = l61.STRSUBPART " &
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " &
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = '8' " &
                    "UNION " &
                    "SELECT 'MACT (Part 63)' AS Part , l63.STRSUBPART , " &
                    "  l63.STRDESCRIPTION , sd.CREATEDATETIME " &
                    "FROM AIRBRANCH.APBSubpartData sd " &
                    "INNER JOIN AIRBRANCH.LookUpSubPart63 l63 " &
                    "ON sd.STRSUBPART = l63.STRSUBPART " &
                    "WHERE sd.ACTIVE <> '0' AND sd.STRAIRSNUMBER = :AirsNumber AND " &
                    "  SUBSTR( sd.STRSUBPARTKEY, 13, 1 ) = 'M' " &
                    "ORDER BY Part , STRSUBPART"
                Case IAIPFacilitySummary.FacilityDataTable.Permits
                    query =
                    "SELECT STRPERMITNUMBER , DATISSUED , DATREVOKED , ACTIVE " &
                    "FROM AIRBRANCH.APBISSUEDPERMIT " &
                    "WHERE STRAIRSNUMBER = SUBSTR( :AirsNumber, 5, 8 ) " &
                    "ORDER BY DATISSUED DESC NULLS FIRST"
                Case IAIPFacilitySummary.FacilityDataTable.TestMemos
                    query =
                    "SELECT tm.STRREFERENCENUMBER , SUBSTR( tm.STRMEMORANDUMFIELD, 0 " &
                    "  , 150 ) AS MemoField " &
                    "FROM AIRBRANCH.ISMPTestREportMemo tm " &
                    "INNER JOIN AIRBRANCH.ISMPMaster im " &
                    "ON tm.STRREFERENCENUMBER = im.STRREFERENCENUMBER " &
                    "WHERE im.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY tm.STRREFERENCENUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.TestNotifications
                    query =
                    "SELECT tn.STRTESTLOGNUMBER ,( up.STRLASTNAME || ', ' || " &
                    "  up.STRFIRSTNAME ) AS Staff , tn.STREMISSIONUNIT , " &
                    "  eu.STRUNITDESC , tn.DATTESTNOTIFICATION , " &
                    "  tn.DATPROPOSEDSTARTDATE , tn.DATPROPOSEDENDDATE " &
                    "FROM AIRBRANCH.ISMPTestNotification tn " &
                    "LEFT JOIN AIRBRANCH.EPDUserProfiles up " &
                    "ON tn.STRSTAFFRESPONSIBLE = up.NUMUSERID " &
                    "LEFT JOIN AIRBRANCH.LookUpEPDUnits eu " &
                    "ON up.NUMUNIT = eu.NUMUNITCODE " &
                    "WHERE tn.STRAIRSNUMBER = :AirsNumber " &
                    "ORDER BY tn.STRTESTLOGNUMBER DESC"
                Case IAIPFacilitySummary.FacilityDataTable.TestReports
                    query =
                    "SELECT STRREFERENCENUMBER , STATUS , STREMISSIONSOURCE , " &
                    "  STRPOLLUTANTDESCRIPTION , STRREPORTTYPE , REVIEWINGENGINEER , " &
                    "  TESTDATESTART , RECEIVEDDATE , COMPLETEDATE , " &
                    "  STRCOMPLIANCESTATUS , STRPRECOMPLIANCESTATUS " &
                    "FROM AIRBRANCH.VW_ISMPWORKDATAGRID " &
                    "WHERE STRAIRSNUMBER = :AirsNumber "
            End Select

            Return query
        End Function

    End Module
End Namespace
