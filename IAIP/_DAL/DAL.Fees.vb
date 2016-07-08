Imports System.Collections.Generic
Imports System.Data.SqlClient

Namespace DAL
    Module FeesData

        ''' <summary>
        ''' Returns a DataTable of fees summary data for a given facility
        ''' </summary>
        ''' <param name="startFeeYear">Beginning year of a date range to filter for.</param>
        ''' <param name="endFeeYear">Ending year of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <returns>A DataTable of fees summary data</returns>
        Public Function GetFeesFacilitySummaryAsDataTable(
                startFeeYear As Integer, endFeeYear As Integer,
                Optional airs As Apb.ApbFacilityId = Nothing) As DataTable

            Dim query As String =
                "SELECT * FROM VW_FEES_FACILITY_SUMMARY " &
                " WHERE NUMFEEYEAR BETWEEN @startFeeYear AND @endFeeYear " &
                " AND (@airs IS NULL OR STRAIRSNUMBER = @airs) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@startFeeYear", startFeeYear),
                New SqlParameter("@endFeeYear", endFeeYear),
                New SqlParameter("@airs", airs.DbFormattedString)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        Public Function Update_FS_Admin_Status(feeYear As String, airsNumber As String) As Boolean
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then Return False
            Dim aN As Apb.ApbFacilityId = airsNumber

            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim sp As String = "PD_FEE_STATUS"

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@FEEYEAR", SqlDbType.Decimal, feeYearDecimal, ParameterDirection.Input),
                New SqlParameter("@AIRSNUMBER", aN.DbFormattedString)
            }

            Try
                Return DB.SPRunCommand(sp, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetAllFeeYears() As List(Of String)
            Dim list As New List(Of String)
            Dim dataTable As DataTable = GetAllFeeYearsAsDataTable()
            For Each row As DataRow In dataTable.Rows
                list.Add(row("FEEYEAR"))
            Next
            Return list
        End Function

        Private Function GetAllFeeYearsAsDataTable() As DataTable
            Dim query As String = "SELECT DISTINCT(NUMFEEYEAR) AS FEEYEAR " &
                "FROM FSLK_NSPSREASONYEAR " &
                "ORDER BY FEEYEAR DESC"
            Return DB.GetDataTable(query)
        End Function

        Public Function FeeMailoutEntryExists(airsNumber As Apb.ApbFacilityId, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM FS_Mailout " &
                " WHERE RowNum = 1 " &
                " AND strAIRSnumber = @airsnumber " &
                " AND numFeeYear = @feeyear "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@airsnumber", airsNumber.DbFormattedString),
                New SqlParameter("@feeyear", feeYearDecimal)
            }

            Dim result As String = DB.GetSingleValue(Of String)(query, parameters)
            Return Convert.ToBoolean(result)
        End Function

        Public Function UpdateFeeMailoutContact(contact As Contact, airsNumber As String, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE FS_MAILOUT " &
                " SET STRFIRSTNAME       = @v2, " &
                "  STRLASTNAME          = @v3, " &
                "  STRPREFIX            = @v4, " &
                "  STRCONTACTSUFFIX     = @v28, " &
                "  STRTITLE             = @v5, " &
                "  STRCONTACTCONAME     = @v6, " &
                "  STRCONTACTADDRESS1   = @v7, " &
                "  STRCONTACTADDRESS2   = @v8, " &
                "  STRCONTACTCITY       = @v9, " &
                "  STRCONTACTSTATE      = @v10, " &
                "  STRCONTACTZIPCODE    = @v11, " &
                "  STRGECOUSEREMAIL     = @v12, " &
                "  UPDATEUSER           = @v25, " &
                "  UPDATEDATETIME       = @v26 " &
                " WHERE STRAIRSNUMBER   = @airsnumber " &
                " AND NUMFEEYEAR        = @feeyear "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@v2", contact.FirstName),
                New SqlParameter("@v3", contact.LastName),
                New SqlParameter("@v4", contact.Prefix),
                New SqlParameter("@v28", contact.Suffix),
                New SqlParameter("@v5", contact.Title),
                New SqlParameter("@v6", contact.CompanyName),
                New SqlParameter("@v7", contact.MailingAddress.Street),
                New SqlParameter("@v8", contact.MailingAddress.Street2),
                New SqlParameter("@v9", contact.MailingAddress.City),
                New SqlParameter("@v10", contact.MailingAddress.State),
                New SqlParameter("@v11", contact.MailingAddress.PostalCode),
                New SqlParameter("@v12", contact.EmailAddress),
                New SqlParameter("@v25", CurrentUser.UserID),
                New SqlParameter("@v26", OracleDate),
                New SqlParameter("@airsnumber", airsNumber),
                New SqlParameter("@feeyear", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)

        End Function

        Public Function UpdateFeeMailoutFacility(facility As Apb.Facilities.Facility, airsNumber As String, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE FS_MAILOUT " &
                " SET STROPERATIONALSTATUS = @v13, " &
                "  STRCLASS             = @v14, " &
                "  STRNSPS              = @v15, " &
                "  STRPART70            = @v16, " &
                "  DATSHUTDOWNDATE      = @v17, " &
                "  STRFACILITYNAME      = @v18, " &
                "  STRFACILITYADDRESS1  = @v19, " &
                "  STRFACILITYADDRESS2  = @v20, " &
                "  STRFACILITYCITY      = @v21, " &
                "  STRFACILITYZIPCODE   = @v22, " &
                "  STRCOMMENT           = @v23, " &
                "  UPDATEUSER           = @v25, " &
                "  UPDATEDATETIME       = @v26 " &
                " WHERE STRAIRSNUMBER   = @airsnumber " &
                " AND NUMFEEYEAR        = @feeyear "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@v13", facility.HeaderData.OperationalStatusCode),
                New SqlParameter("@v14", facility.HeaderData.ClassificationCode),
                New SqlParameter("@v15", If(facility.SubjectToNsps, "1", "0")),
                New SqlParameter("@v16", If(facility.SubjectToPart70, "1", "0")),
                New SqlParameter("@v17", facility.HeaderData.ShutdownDate),
                New SqlParameter("@v18", facility.FacilityName),
                New SqlParameter("@v19", facility.FacilityLocation.Address.Street),
                New SqlParameter("@v20", facility.FacilityLocation.Address.Street2),
                New SqlParameter("@v21", facility.FacilityLocation.Address.City),
                New SqlParameter("@v22", facility.FacilityLocation.Address.PostalCode),
                New SqlParameter("@v23", facility.Comment),
                New SqlParameter("@v25", CurrentUser.UserID),
                New SqlParameter("@v26", OracleDate),
                New SqlParameter("@airsnumber", airsNumber),
                New SqlParameter("@feeyear", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)

        End Function

        Public Function GetAllFeeFacilities() As DataTable
            Dim query As String = "SELECT DISTINCT SUBSTR(fa.STRAIRSNUMBER, 5) AS STRAIRSNUMBER, " &
                "  SUBSTR(fa.STRAIRSNUMBER, 5, 3) || '-' || SUBSTR( " &
                "  fa.STRAIRSNUMBER, 8) AS ""AIRS Number"", fi.STRFACILITYNAME " &
                "  AS ""Facility Name"" " &
                "FROM FS_ADMIN fa " &
                "INNER JOIN APBFACILITYINFORMATION fi ON " &
                "  fa.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                "ORDER BY fi.STRFACILITYNAME"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace