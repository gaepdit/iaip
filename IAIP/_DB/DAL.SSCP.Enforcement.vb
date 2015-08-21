Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.SSCP

Namespace DAL.SSCP

    Module Enforcement

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
