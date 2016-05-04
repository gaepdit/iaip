Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sscp
Imports System.Collections.Generic

Namespace DAL.Sscp

    Module FceData

        ''' <summary>
        ''' Tests whether an SSCP FCE tracking number exists.
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number to test.</param>
        ''' <returns>Returns True if the FCE exists; otherwise, returns False.</returns>
        Public Function FceExists(ByVal fceNumber As String) As Boolean
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCPFCEMASTER " & _
                " WHERE RowNum = 1 " & _
                " AND STRFCENUMBER = :fceNumber "
            Dim parameter As New OracleParameter("fceNumber", fceNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM AIRBRANCH.SSCPFCEMASTER WHERE STRFCENUMBER = :fceNumber"
            Dim parameter As New OracleParameter("fceNumber", fceNumber)

            Return New Apb.ApbFacilityId(DB.GetSingleValue(Of String)(query, parameter))
        End Function

        ''' <summary>
        ''' Returns a List of FCE's for a given facility.
        ''' </summary>
        ''' <param name="airs">The ID of the facility of interest.</param>
        ''' <returns>A List of FCE's.</returns>
        Public Function GetFceList(airs As Apb.ApbFacilityId) As List(Of Fce)
            Dim list As New List(Of Fce)
            Dim dt As DataTable = GetFceDataTable(airs)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    list.Add(GetFceFromDataRow(row))
                Next
            End If

            Return list
        End Function

        ''' <summary>
        ''' Parses an FCE using the data in a DataRow
        ''' </summary>
        ''' <param name="row">The DataRow to parse.</param>
        ''' <returns>An FCE parsed from the DataRow.</returns>
        Private Function GetFceFromDataRow(row As DataRow) As Fce
            If IsNothing(row) Then Return Nothing

            Dim fce As New Fce
            With fce
                .FceNumber = DB.GetNullable(Of String)(row("STRFCENUMBER"))
                .Facility = GetFacility(New Apb.ApbFacilityId(row("STRAIRSNUMBER")))
                .StaffResponsible = GetIaipUserByUserId(row("STRREVIEWER"))
                .DateComplete = DB.GetNullable(Of Date)(row("DATFCECOMPLETED"))
                .Comments = DB.GetNullable(Of String)(row("STRFCECOMMENTS"))
                .SiteVisitTypeDbCode = Convert.ToBoolean(DB.GetNullable(Of String)(row("STRSITEINSPECTION")))
                .FceYear = DB.GetNullable(Of String)(row("STRFCEYEAR"))
            End With
            Return fce
        End Function

        ''' <summary>
        ''' Returns a DataTable of FCE data for a given facility
        ''' </summary>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="year">An optional FCE year to filter for.</param>
        ''' <returns>A DataTable of FCE data</returns>
        Public Function GetFceDataTable(
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional year As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_FCES " &
                " WHERE 1=1 "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND STRREVIEWER = :staffId "
            If Not String.IsNullOrEmpty(year) Then query &= " AND STRFCEYEAR = :year "

            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId),
                New OracleParameter("year", year)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        ''' <summary>
        ''' Returns a DataTable of FCE data for a given facility
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="year">An optional FCE year to filter for.</param>
        ''' <returns>A DataTable of FCE data</returns>
        Public Function GetFceDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional year As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_FCES " &
                " WHERE TRUNC(DATFCECOMPLETED) BETWEEN :datestart AND :dateend "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND STRREVIEWER = :staffId "
            If Not String.IsNullOrEmpty(year) Then query &= " AND STRFCEYEAR = :year "

            Dim parameters As OracleParameter() = {
                New OracleParameter("datestart", dateRangeStart),
                New OracleParameter("dateend", dateRangeEnd),
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId),
                New OracleParameter("year", year)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        ''' <summary>
        ''' Returns an FCE for a given FCE number.
        ''' </summary>
        ''' <param name="fceNumber">The ID of the FCE to return.</param>
        ''' <returns>An FCE.</returns>
        Public Function GetFce(fceNumber As String) As Fce
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing
            Dim query As String =
                "SELECT * " &
                "FROM AIRBRANCH.VW_SSCP_FCES " &
                "WHERE STRFCENUMBER = :fceNumber"
            Dim parameter As New OracleParameter("fceNumber", fceNumber)
            Return GetFceFromDataRow(DB.GetDataRow(query, parameter))
        End Function


        ''' <summary>
        ''' Returns an FCE for a given facility for a given year.
        ''' </summary>
        ''' <param name="airs">The ID of the facility of interest.</param>
        ''' <param name="year">The FCE year desired.</param>
        ''' <returns>An FCE.</returns>
        Public Function GetFce(airs As Apb.ApbFacilityId, year As String) As Fce

            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_FCES " &
                " WHERE STRAIRSNUMBER = :airs " &
                " AND STRFCEYEAR = :year "

            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("year", year)
            }

            Return GetFceFromDataRow(DB.GetDataRow(query, parameters))
        End Function

    End Module

End Namespace
