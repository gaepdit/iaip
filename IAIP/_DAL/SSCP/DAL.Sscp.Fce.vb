Imports System.Collections.Generic
Imports System.Data.SqlClient

Namespace DAL.Sscp

    Module FceData

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="fceNumber">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM SSCPFCEMASTER WHERE STRFCENUMBER = @fceNumber"
            Dim parameter As New SqlParameter("@fceNumber", fceNumber)

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
                "SELECT * FROM VW_SSCP_FCES " &
                " WHERE 1=1 "
            Dim params As New List(Of SqlParameter)

            If airs IsNot Nothing Then
                query &= " AND STRAIRSNUMBER = @airs "
                params.Add(New SqlParameter("@airs", airs.DbFormattedString))
            End If

            If Not String.IsNullOrEmpty(staffId) Then
                query &= " AND STRREVIEWER = @staffId "
                params.Add(New SqlParameter("@staffId", staffId))
            End If

            If Not String.IsNullOrEmpty(year) Then
                query &= " AND STRFCEYEAR = @year "
                params.Add(New SqlParameter("@year", year))
            End If

            Dim parameters As SqlParameter() = params.ToArray
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
                airs As Apb.ApbFacilityId,
                Optional staffId As String = Nothing,
                Optional year As String = Nothing) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_FCES " &
                " WHERE DATFCECOMPLETED BETWEEN @datestart AND @dateend "

            Dim params As New List(Of SqlParameter) From {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd)
            }

            If airs IsNot Nothing Then
                query &= " AND STRAIRSNUMBER = @airs "
                params.Add(New SqlParameter("@airs", airs.DbFormattedString))
            End If

            If Not String.IsNullOrEmpty(staffId) Then
                query &= " AND STRREVIEWER = @staffId "
                params.Add(New SqlParameter("@staffId", staffId))
            End If

            If Not String.IsNullOrEmpty(year) Then
                query &= " AND STRFCEYEAR = @year "
                params.Add(New SqlParameter("@year", year))
            End If

            Dim parameters As SqlParameter() = params.ToArray
            Return DB.GetDataTable(query, parameters, True)
        End Function

    End Module

End Namespace
