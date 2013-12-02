Imports Oracle.DataAccess.Client
Imports JohnGaltProject.Apb.SSCP

Namespace DAL
    Namespace SSCP

        Module Enforcement

            Public Function EnforcementExists(ByVal id As String) As Boolean
                Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                    " FROM SSCP_AUDITEDENFORCEMENT " & _
                    " WHERE RowNum = 1 " & _
                    " AND SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = :pId "
                Dim parameter As New OracleParameter("pId", id)

                Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
                Return Convert.ToBoolean(result)
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

                Dim facility As New Apb.Facility
                With facility
                    .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                    .Name = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
                    .FacilityLocation = location
                End With

                Dim staff As New Apb.Staff
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
                Dim query As String = <s><![CDATA[
                    SELECT SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER,
                      SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER,
                      APBFACILITYINFORMATION.STRFACILITYNAME,
                      APBFACILITYINFORMATION.STRFACILITYCITY,
                      APBFACILITYINFORMATION.STRFACILITYSTATE,
                      EPDUSERPROFILES.STRFIRSTNAME,
                      EPDUSERPROFILES.STRLASTNAME,
                      SSCP_AUDITEDENFORCEMENT.DATDISCOVERYDATE,
                      SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTFINALIZED,
                      SSCP_AUDITEDENFORCEMENT.DATENFORCEMENTFINALIZED,
                      SSCP_AUDITEDENFORCEMENT.STRACTIONTYPE
                    FROM SSCP_AUDITEDENFORCEMENT
                    LEFT JOIN APBFACILITYINFORMATION
                    ON APBFACILITYINFORMATION.STRAIRSNUMBER = SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER
                    LEFT JOIN EPDUSERPROFILES
                    ON EPDUSERPROFILES.NUMUSERID                       = SSCP_AUDITEDENFORCEMENT.NUMSTAFFRESPONSIBLE
                    WHERE SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = :pId
                ]]></s>.Value
                Dim parameter As New OracleParameter("pId", id)
                Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
                If dataTable Is Nothing Then Return Nothing

                Dim dataRow As DataRow = dataTable.Rows(0)

                Dim enfInfo As New EnforcementInfo
                FillEnforcementInfoFromDataRow(dataRow, enfInfo)
                Return enfInfo
            End Function

        End Module

    End Namespace
End Namespace
