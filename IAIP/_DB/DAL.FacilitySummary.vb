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

    End Module
End Namespace
