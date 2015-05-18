Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace DAL
    Module FacilitySummary

#Region " Compliance "

        Public Function GetComplianceWorkData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
            "SELECT vw.STRTRACKINGNUMBER, vw.STRACTIVITYNAME, vw.RECEIVEDDATE " & _
            "FROM AIRBRANCH.VW_SSCPWORKDATAGRID vw " & _
            "WHERE vw.STRAIRSNUMBER = :AirsNumber " & _
            "ORDER BY vw.STRTRACKINGNUMBER DESC"

            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetComplianceFceData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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

            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetComplianceEnforcementData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
            "SELECT STRENFORCEMENTNUMBER , DATDISCOVERYDATE AS ViolationDate " & _
            "  , STRACTIONTYPE AS HPVStatus , CASE WHEN " & _
            "      DATENFORCEMENTFINALIZED IS NOT NULL THEN 'Closed' ELSE " & _
            "      'Open' END AS Status " & _
            "FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
            "ORDER BY STRENFORCEMENTNUMBER DESC"

            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Fees "

        Public Function GetFeesData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
            "SELECT vw.* ,( NUMTOTALFEE - TOTALPAID ) AS Balance " & _
            "FROM AIRBRANCH.VW_APBFACILITYFEES vw " & _
            "WHERE vw.STRAIRSNUMBER = :AirsNumber " & _
            "ORDER BY vw.INTYEAR DESC"

            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Contacts "

        Public Function GetContactsStateData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
            "SELECT vw.AirProgram, vw.Staff, vw.Unit  " & _
            "FROM AIRBRANCH.VW_FACILITY_STATECONTACTS vw " & _
            "WHERE vw.AIRSNUMBER = :AirsNumber "

            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetContactsWebSiteData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetContactsPermittingData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetContactsTestingData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetContactsComplianceData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetContactsGecoData(ByVal airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = _
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
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

    End Module
End Namespace
