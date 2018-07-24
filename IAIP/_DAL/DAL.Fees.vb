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
                " AND (@airs IS NULL OR STRAIRSNUMBER = @airs) " &
                " ORDER BY STRAIRSNUMBER, NUMFEEYEAR DESC "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@startFeeYear", startFeeYear),
                New SqlParameter("@endFeeYear", endFeeYear),
                New SqlParameter("@airs", airs.DbFormattedString)
            }

            Return DB.GetDataTable(query, parameters)
        End Function

        Public Function Update_FS_Admin_Status(feeYear As Integer, airsNumber As Apb.ApbFacilityId) As Boolean
            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@FEEYEAR", SqlDbType.SmallInt) With {.Value = feeYear},
                New SqlParameter("@AIRSNUMBER", SqlDbType.VarChar) With {.Value = airsNumber.DbFormattedString}
            }

            Try
                Return DB.SPRunCommand("dbo.PD_FEE_STATUS", parameters)
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
            Dim query As String = "select distinct (convert(int, NUMFEEYEAR)) as FeeYear
                from FSLK_NSPSREASONYEAR
                ORDER BY FeeYear DESC "
            Return DB.GetDataTable(query)
        End Function

        Public Function GetFeePaymentTypes() As DataTable
            Dim query As String = "select
                    NUMPAYTYPEID,
                    STRPAYTYPEDESC
                from FSLK_PAYTYPE
                where ACTIVE = 1"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetFeePaymentTypesAsList() As List(Of String)
            Dim list As New List(Of String)
            Dim dt As DataTable = GetFeePaymentTypes()
            For Each row As DataRow In dt.Rows
                list.Add(row("STRPAYTYPEDESC").ToString)
            Next
            Return list
        End Function

        Public Function FeeMailoutEntryExists(airsNumber As Apb.ApbFacilityId, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "SELECT 1 " &
                " FROM FS_Mailout " &
                " WHERE strAIRSnumber = @airsnumber " &
                " AND numFeeYear = @feeyear "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@airsnumber", airsNumber.DbFormattedString),
                New SqlParameter("@feeyear", feeYearDecimal)
            }

            Return DB.ValueExists(query, parameters)
        End Function

        Public Function UpdateFeeMailoutContact(contact As Contact, airsNumber As String, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE FS_MAILOUT " &
                " SET STRFIRSTNAME      = @STRFIRSTNAME, " &
                "  STRLASTNAME          = @STRLASTNAME, " &
                "  STRPREFIX            = @STRPREFIX, " &
                "  STRCONTACTSUFFIX     = @STRCONTACTSUFFIX, " &
                "  STRTITLE             = @STRTITLE, " &
                "  STRCONTACTCONAME     = @STRCONTACTCONAME, " &
                "  STRCONTACTADDRESS1   = @STRCONTACTADDRESS1, " &
                "  STRCONTACTADDRESS2   = @STRCONTACTADDRESS2, " &
                "  STRCONTACTCITY       = @STRCONTACTCITY, " &
                "  STRCONTACTSTATE      = @STRCONTACTSTATE, " &
                "  STRCONTACTZIPCODE    = @STRCONTACTZIPCODE, " &
                "  STRGECOUSEREMAIL     = @STRGECOUSEREMAIL, " &
                "  UPDATEUSER           = @UPDATEUSER, " &
                "  UPDATEDATETIME       = getdate() " &
                " WHERE STRAIRSNUMBER   = @STRAIRSNUMBER " &
                " AND NUMFEEYEAR        = @NUMFEEYEAR "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@STRFIRSTNAME", contact.FirstName),
                New SqlParameter("@STRLASTNAME", contact.LastName),
                New SqlParameter("@STRPREFIX", contact.Prefix),
                New SqlParameter("@STRCONTACTSUFFIX", contact.Suffix),
                New SqlParameter("@STRTITLE", contact.Title),
                New SqlParameter("@STRCONTACTCONAME", contact.CompanyName),
                New SqlParameter("@STRCONTACTADDRESS1", contact.MailingAddress.Street),
                New SqlParameter("@STRCONTACTADDRESS2", contact.MailingAddress.Street2),
                New SqlParameter("@STRCONTACTCITY", contact.MailingAddress.City),
                New SqlParameter("@STRCONTACTSTATE", contact.MailingAddress.State),
                New SqlParameter("@STRCONTACTZIPCODE", contact.MailingAddress.PostalCode),
                New SqlParameter("@STRGECOUSEREMAIL", contact.EmailAddress),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                New SqlParameter("@STRAIRSNUMBER", airsNumber),
                New SqlParameter("@NUMFEEYEAR", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Public Function UpdateFeeMailoutFacility(facility As Apb.Facilities.Facility, airsNumber As String, feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE FS_MAILOUT " &
                " SET STROPERATIONALSTATUS = @STROPERATIONALSTATUS, " &
                "  STRCLASS             = @STRCLASS, " &
                "  STRNSPS              = @STRNSPS, " &
                "  STRPART70            = @STRPART70, " &
                "  DATSHUTDOWNDATE      = @DATSHUTDOWNDATE, " &
                "  STRFACILITYNAME      = @STRFACILITYNAME, " &
                "  STRFACILITYADDRESS1  = @STRFACILITYADDRESS1, " &
                "  STRFACILITYADDRESS2  = @STRFACILITYADDRESS2, " &
                "  STRFACILITYCITY      = @STRFACILITYCITY, " &
                "  STRFACILITYZIPCODE   = @STRFACILITYZIPCODE, " &
                "  STRCOMMENT           = @STRCOMMENT, " &
                "  UPDATEUSER           = @UPDATEUSER, " &
                "  UPDATEDATETIME       = getdate() " &
                " WHERE STRAIRSNUMBER   = @STRAIRSNUMBER " &
                " AND NUMFEEYEAR        = @NUMFEEYEAR "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@STROPERATIONALSTATUS", facility.HeaderData.OperationalStatusCode),
                New SqlParameter("@STRCLASS", facility.HeaderData.ClassificationCode),
                New SqlParameter("@STRNSPS", Convert.ToInt16(facility.SubjectToNsps)),
                New SqlParameter("@STRPART70", Convert.ToInt16(facility.SubjectToPart70)),
                New SqlParameter("@DATSHUTDOWNDATE", facility.HeaderData.ShutdownDate),
                New SqlParameter("@STRFACILITYNAME", facility.FacilityName),
                New SqlParameter("@STRFACILITYADDRESS1", facility.FacilityLocation.Address.Street),
                New SqlParameter("@STRFACILITYADDRESS2", facility.FacilityLocation.Address.Street2),
                New SqlParameter("@STRFACILITYCITY", facility.FacilityLocation.Address.City),
                New SqlParameter("@STRFACILITYZIPCODE", facility.FacilityLocation.Address.PostalCode),
                New SqlParameter("@STRCOMMENT", facility.Comment),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                New SqlParameter("@STRAIRSNUMBER", airsNumber),
                New SqlParameter("@NUMFEEYEAR", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Public Function GetAllFeeFacilities() As DataTable
            Dim query As String = "SELECT DISTINCT substring(fa.STRAIRSNUMBER, 5, 8) AS STRAIRSNUMBER, " &
                "  substring(fa.STRAIRSNUMBER, 5, 3) + '-' + substring(fa.STRAIRSNUMBER, 8, 5) AS [AIRS Number], " &
                "  fi.STRFACILITYNAME AS [Facility Name] " &
                "FROM FS_ADMIN fa " &
                "INNER JOIN APBFACILITYINFORMATION fi ON " &
                "  fa.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                "ORDER BY fi.STRFACILITYNAME"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("STRAIRSNUMBER")}
            Return dt
        End Function

    End Module
End Namespace