Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sscp

Namespace DAL.Sscp

    Module EnforcementData

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data for a given facility
        ''' </summary>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <returns>A DataTable of enforcement summary data</returns>
        Public Function GetEnforcementSummaryDataTable(
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_ENFORCEMENT_SUMMARY " &
                " WHERE 1=1 "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND NUMSTAFFRESPONSIBLE = :staffId "

            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data  for a given facility
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <returns>A DataTable of enforcement summary data</returns>
        Public Function GetEnforcementSummaryDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_ENFORCEMENT_SUMMARY " &
                " WHERE TRUNC(EnforcementDate) BETWEEN :datestart AND :dateend "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND NUMSTAFFRESPONSIBLE = :staffId "

            Dim parameters As OracleParameter() = {
                New OracleParameter("datestart", dateRangeStart),
                New OracleParameter("dateend", dateRangeEnd),
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        Public Function EnforcementExists(ByVal id As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
                " WHERE RowNum = 1 " & _
                " AND STRENFORCEMENTNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function EnforcementExistsForTrackingNumber(ByVal id As String, ByRef enfNumber As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = " SELECT STRENFORCEMENTNUMBER " & _
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
                " WHERE STRTRACKINGNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)

            If result Is Nothing Then
                Return False
            Else
                enfNumber = result
                Return True
            End If
        End Function

        Private Sub FillEnforcementInfoFromDataRow(ByVal row As DataRow, ByRef enfInfo As EnforcementInfo)
            Dim address As New Address
            With address
                .City = DB.GetNullable(Of String)(row("STRFACILITYCITY"))
                .State = DB.GetNullable(Of String)(row("STRFACILITYSTATE"))
            End With

            Dim location As New Location
            With location
                .Address = address
            End With

            Dim facility As New Apb.Facilities.Facility
            With facility
                .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .FacilityName = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
                .FacilityLocation = location
            End With

            Dim staff As New Staff
            With staff
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
            End With

            With enfInfo
                .DiscoveryDate = DB.GetNullable(Of Date?)(row("DATDISCOVERYDATE"))
                .DateFinalized = DB.GetNullable(Of Date?)(row("DATENFORCEMENTFINALIZED"))
                .Open = Not Convert.ToBoolean(row("STRENFORCEMENTFINALIZED"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
                .EnforcementTypeCode = DB.GetNullable(Of String)(row("STRACTIONTYPE"))
                .Facility = facility
                .StaffResponsible = staff
            End With
        End Sub

        Public Function GetEnforcementInfo(ByVal id As String) As EnforcementInfo
            Dim query As String = _
                " SELECT SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER, " & _
                "   SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER, " & _
                "   APBFACILITYINFORMATION.STRFACILITYNAME, " & _
                "   APBFACILITYINFORMATION.STRFACILITYCITY, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTATE, " & _
                "   EPDUSERPROFILES.STRFIRSTNAME, " & _
                "   EPDUSERPROFILES.STRLASTNAME, " & _
                "   SSCP_AUDITEDENFORCEMENT.DATDISCOVERYDATE, " & _
                "   SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTFINALIZED, " & _
                "   SSCP_AUDITEDENFORCEMENT.DATENFORCEMENTFINALIZED, " & _
                "   SSCP_AUDITEDENFORCEMENT.STRACTIONTYPE " & _
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
                " LEFT JOIN AIRBRANCH.APBFACILITYINFORMATION " & _
                " ON APBFACILITYINFORMATION.STRAIRSNUMBER = SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER " & _
                " LEFT JOIN AIRBRANCH.EPDUSERPROFILES " & _
                " ON EPDUSERPROFILES.NUMUSERID = SSCP_AUDITEDENFORCEMENT.NUMSTAFFRESPONSIBLE " & _
                " WHERE SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)
            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)

            Dim enfInfo As New EnforcementInfo
            FillEnforcementInfoFromDataRow(dataRow, enfInfo)
            Return enfInfo
        End Function

        Public Function GetViolationTypes() As DataTable
            Dim query As String =
                "SELECT AIRVIOLATIONTYPECODE, VIOLATIONTYPEDESC, SEVERITYCODE " &
                " , POLLUTANTREQUIRED, DEPRECATED " &
                "FROM AIRBRANCH.LK_VIOLATION_TYPE " &
                "WHERE STATUS = 'A' "
            Return DB.GetDataTable(query)
        End Function

    End Module

End Namespace
