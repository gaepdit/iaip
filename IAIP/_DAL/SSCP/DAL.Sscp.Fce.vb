Imports System.Data.SqlClient
Imports Iaip.Apb.Sscp
Imports System.Collections.Generic

Namespace DAL.Sscp

    Module FceData

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM AIRBRANCH.SSCPFCEMASTER WHERE STRFCENUMBER = :fceNumber"
            Dim parameter As New SqlParameter("fceNumber", fceNumber)

            Return New Apb.ApbFacilityId(DB.GetSingleValue(Of String)(query, parameter))
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

            Dim parameters As SqlParameter() = {
                New SqlParameter("airs", airs.DbFormattedString),
                New SqlParameter("staffId", staffId),
                New SqlParameter("year", year)
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

            Dim parameters As SqlParameter() = {
                New SqlParameter("datestart", dateRangeStart),
                New SqlParameter("dateend", dateRangeEnd),
                New SqlParameter("airs", airs.DbFormattedString),
                New SqlParameter("staffId", staffId),
                New SqlParameter("year", year)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

    End Module

End Namespace
