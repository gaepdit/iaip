Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sscp
Imports System.Collections.Generic

Namespace DAL.Sscp

    Module FceData

        ''' <summary>
        ''' Tests whether an SSCP FCE tracking number exists.
        ''' </summary>
        ''' <param name="id">The FCE tracking number to test.</param>
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
        ''' <param name="id">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM SSCPFCEMASTER WHERE STRFCENUMBER = :fceNumber"
            Dim parameter As New OracleParameter("fceNumber", fceNumber)

            Return New Apb.ApbFacilityId(DB.GetSingleValue(Of String)(query, parameter))
        End Function

        ''' <summary>
        ''' Returns a List of FCE's for a given facility.
        ''' </summary>
        ''' <param name="airs">The ID of the facility of interest.</param>
        ''' <returns>A List of FCE's.</returns>
        Public Function GetFceList(airs As Apb.ApbFacilityId) As List(Of Fce)
            Dim fceList As New List(Of Fce)
            Dim fceTable As DataTable = GetFceDataTable(airs)

            If fceTable IsNot Nothing And fceTable.Rows.Count > 0 Then
                For Each row As DataRow In fceTable.Rows
                    fceList.Add(GetFceFromDataRow(row))
                Next
            End If

            Return fceList
        End Function

        ''' <summary>
        ''' Returns an FCE using the data in a DataRow
        ''' </summary>
        ''' <param name="row">The DataRow to parse</param>
        ''' <returns>An FCE parsed from the DataRow</returns>
        Private Function GetFceFromDataRow(row As DataRow) As Fce
            If IsNothing(row) Then Return Nothing

            Dim fce As New Fce
            With fce
                .FceNumber = DB.GetNullable(Of String)(row("STRFCENUMBER"))
                .Facility = GetFacility(New Apb.ApbFacilityId(row("STRAIRSNUMBER")))
                .StaffResponsible = GetStaff(row("STRREVIEWER"))
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
        ''' <param name="airs">The ID of the facility of interest.</param>
        ''' <returns>A DataTable of FCE data</returns>
        Private Function GetFceDataTable(airs As Apb.ApbFacilityId) As DataTable
            Dim query As String =
                "SELECT fm.STRFCENUMBER , fm.STRAIRSNUMBER , fce.STRREVIEWER , " &
                "  fce.DATFCECOMPLETED , fce.STRFCECOMMENTS , " &
                "  fce.STRSITEINSPECTION , fce.STRFCEYEAR " &
                "FROM SSCPFCEMASTER fm " &
                "INNER JOIN SSCPFCE fce " &
                "ON fm.STRFCENUMBER = fce.STRFCENUMBER " &
                "WHERE fm.STRAIRSNUMBER = :airs " &
                "ORDER BY fce.STRFCEYEAR"
            Dim parameter As New OracleParameter("airs", airs.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        ''' <summary>
        ''' Returns an FCE for a given FCE number.
        ''' </summary>
        ''' <param name="fceNumber">The ID of the FCE to return.</param>
        ''' <returns>An FCE.</returns>
        Public Function GetFce(fceNumber As String) As Fce
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing
            Dim query As String =
                "SELECT fm.STRFCENUMBER , fm.STRAIRSNUMBER , fce.STRREVIEWER , " &
                "  fce.DATFCECOMPLETED , fce.STRFCECOMMENTS , " &
                "  fce.STRSITEINSPECTION , fce.STRFCEYEAR " &
                "FROM SSCPFCEMASTER fm " &
                "INNER JOIN SSCPFCE fce " &
                "ON fm.STRFCENUMBER = fce.STRFCENUMBER " &
                "WHERE fm.STRFCENUMBER = :fceNumber"
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
            If year = "" OrElse Not Integer.TryParse(year, Nothing) Then Return Nothing
            Dim query As String =
                "SELECT fm.STRFCENUMBER , fm.STRAIRSNUMBER , fce.STRREVIEWER , " &
                "  fce.DATFCECOMPLETED , fce.STRFCECOMMENTS , " &
                "  fce.STRSITEINSPECTION , fce.STRFCEYEAR " &
                "FROM SSCPFCEMASTER fm " &
                "INNER JOIN SSCPFCE fce " &
                "ON fm.STRFCENUMBER = fce.STRFCENUMBER " &
                "WHERE fm.STRAIRSNUMBER = :airs AND fce.STRFCEYEAR = :year"
            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("year", year)
            }
            Return GetFceFromDataRow(DB.GetDataRow(query, parameters))
        End Function

    End Module

End Namespace
