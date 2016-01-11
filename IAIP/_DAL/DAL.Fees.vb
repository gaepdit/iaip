Imports System.Collections.Generic
Imports Oracle.ManagedDataAccess.Client

Namespace DAL
    Module FeesData

        ''' <summary>
        ''' Returns a DataTable of fees summary data for a given facility
        ''' </summary>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <returns>A DataTable of fees summary data for a facility</returns>
        Public Function GetFeesFacilitySummaryAsDataTable(
                Optional airs As Apb.ApbFacilityId = Nothing) As DataTable

            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_FEES_FACILITY_SUMMARY " &
                " WHERE 1=1 "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "

            Dim parameter As OracleParameter = New OracleParameter("airs", airs.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a DataTable of fees summary data for a given facility
        ''' </summary>
        ''' <param name="startFeeYear">Beginning year of a date range to filter for.</param>
        ''' <param name="endFeeYear">Ending year of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <returns>A DataTable of fees summary data</returns>
        Public Function GetFeesFacilitySummaryAsDataTable(
                startFeeYear As String, endFeeYear As String,
                Optional airs As Apb.ApbFacilityId = Nothing) As DataTable

            Dim startFeeYearDecimal, endFeeYearDecimal As Decimal
            If Not Decimal.TryParse(startFeeYear, startFeeYearDecimal) Then Return Nothing
            If Not Decimal.TryParse(endFeeYear, endFeeYearDecimal) Then Return Nothing

            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_FEES_FACILITY_SUMMARY " &
                " WHERE NUMFEEYEAR BETWEEN :startFeeYear AND :endFeeYear "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "

            Dim parameters As OracleParameter() = {
                New OracleParameter("startFeeYear", startFeeYearDecimal),
                New OracleParameter("endFeeYear", endFeeYearDecimal),
                New OracleParameter("airs", airs.DbFormattedString)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

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
                "SELECT * FROM AIRBRANCH.VW_FEES_FACILITY_SUMMARY " &
                " WHERE NUMFEEYEAR BETWEEN :startFeeYear AND :endFeeYear "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "

            Dim parameters As OracleParameter() = {
                New OracleParameter("startFeeYear", startFeeYear),
                New OracleParameter("endFeeYear", endFeeYear),
                New OracleParameter("airs", airs.DbFormattedString)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        Public Function Update_FS_Admin_Status(ByVal feeYear As String, ByVal airsNumber As String) As Boolean
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then Return False
            Dim aN As Apb.ApbFacilityId = airsNumber

            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim sp As String = "AIRBRANCH.PD_FEE_STATUS"

            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("FEEYEAR", OracleDbType.Decimal, feeYearDecimal, ParameterDirection.Input),
                New OracleParameter("AIRSNUMBER", aN.DbFormattedString)
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
                "FROM AIRBRANCH.FSLK_NSPSREASONYEAR " &
                "ORDER BY FEEYEAR DESC"
            Return DB.GetDataTable(query)
        End Function

        Public Function FeeMailoutEntryExists(ByVal airsNumber As Apb.ApbFacilityId, ByVal feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.FS_Mailout " &
                " WHERE RowNum = 1 " &
                " AND strAIRSnumber = :airsnumber " &
                " AND numFeeYear = :feeyear "

            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("airsnumber", airsNumber.DbFormattedString),
                New OracleParameter("feeyear", feeYearDecimal)
            }

            Dim result As String = DB.GetSingleValue(Of String)(query, parameters)
            Return Convert.ToBoolean(result)
        End Function

        Public Function UpdateFeeMailoutContact(ByVal contact As Contact, ByVal airsNumber As String, ByVal feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE AIRBRANCH.FS_MAILOUT " &
                " SET STRFIRSTNAME       = :v2, " &
                "  STRLASTNAME          = :v3, " &
                "  STRPREFIX            = :v4, " &
                "  STRCONTACTSUFFIX     = :v28, " &
                "  STRTITLE             = :v5, " &
                "  STRCONTACTCONAME     = :v6, " &
                "  STRCONTACTADDRESS1   = :v7, " &
                "  STRCONTACTADDRESS2   = :v8, " &
                "  STRCONTACTCITY       = :v9, " &
                "  STRCONTACTSTATE      = :v10, " &
                "  STRCONTACTZIPCODE    = :v11, " &
                "  STRGECOUSEREMAIL     = :v12, " &
                "  UPDATEUSER           = :v25, " &
                "  UPDATEDATETIME       = :v26 " &
                " WHERE STRAIRSNUMBER   = :airsnumber " &
                " AND NUMFEEYEAR        = :feeyear "

            Dim parameters As OracleParameter() = {
                New OracleParameter("v2", contact.FirstName),
                New OracleParameter("v3", contact.LastName),
                New OracleParameter("v4", contact.Prefix),
                New OracleParameter("v28", contact.Suffix),
                New OracleParameter("v5", contact.Title),
                New OracleParameter("v6", contact.CompanyName),
                New OracleParameter("v7", contact.MailingAddress.Street),
                New OracleParameter("v8", contact.MailingAddress.Street2),
                New OracleParameter("v9", contact.MailingAddress.City),
                New OracleParameter("v10", contact.MailingAddress.State),
                New OracleParameter("v11", contact.MailingAddress.PostalCode),
                New OracleParameter("v12", contact.EmailAddress),
                New OracleParameter("v25", UserGCode),
                New OracleParameter("v26", OracleDate),
                New OracleParameter("airsnumber", airsNumber),
                New OracleParameter("feeyear", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)

        End Function

        Public Function UpdateFeeMailoutFacility(ByVal facility As Apb.Facilities.Facility, ByVal airsNumber As String, ByVal feeYear As String) As Boolean
            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim query As String = "UPDATE AIRBRANCH.FS_MAILOUT " &
                " SET STROPERATIONALSTATUS = :v13, " &
                "  STRCLASS             = :v14, " &
                "  STRNSPS              = :v15, " &
                "  STRPART70            = :v16, " &
                "  DATSHUTDOWNDATE      = :v17, " &
                "  STRFACILITYNAME      = :v18, " &
                "  STRFACILITYADDRESS1  = :v19, " &
                "  STRFACILITYADDRESS2  = :v20, " &
                "  STRFACILITYCITY      = :v21, " &
                "  STRFACILITYZIPCODE   = :v22, " &
                "  STRCOMMENT           = :v23, " &
                "  UPDATEUSER           = :v25, " &
                "  UPDATEDATETIME       = :v26 " &
                " WHERE STRAIRSNUMBER   = :airsnumber " &
                " AND NUMFEEYEAR        = :feeyear "

            Dim parameters As OracleParameter() = {
                New OracleParameter("v13", facility.HeaderData.OperationalStatusCode),
                New OracleParameter("v14", facility.HeaderData.ClassificationCode),
                New OracleParameter("v15", If(facility.SubjectToNsps, "1", "0")),
                New OracleParameter("v16", If(facility.SubjectToPart70, "1", "0")),
                New OracleParameter("v17", facility.HeaderData.ShutdownDate),
                New OracleParameter("v18", facility.FacilityName),
                New OracleParameter("v19", facility.FacilityLocation.Address.Street),
                New OracleParameter("v20", facility.FacilityLocation.Address.Street2),
                New OracleParameter("v21", facility.FacilityLocation.Address.City),
                New OracleParameter("v22", facility.FacilityLocation.Address.PostalCode),
                New OracleParameter("v23", facility.Comment),
                New OracleParameter("v25", UserGCode),
                New OracleParameter("v26", OracleDate),
                New OracleParameter("airsnumber", airsNumber),
                New OracleParameter("feeyear", feeYearDecimal)
            }

            Return DB.RunCommand(query, parameters)

        End Function

        Public Function GetAllFeeFacilities() As DataTable
            Dim query As String = "SELECT DISTINCT SUBSTR(fa.STRAIRSNUMBER, 5) AS STRAIRSNUMBER, " &
                "  SUBSTR(fa.STRAIRSNUMBER, 5, 3) || '-' || SUBSTR( " &
                "  fa.STRAIRSNUMBER, 8) AS ""AIRS Number"", fi.STRFACILITYNAME " &
                "  AS ""Facility Name"" " &
                "FROM AIRBRANCH.FS_ADMIN fa " &
                "INNER JOIN AIRBRANCH.APBFACILITYINFORMATION fi ON " &
                "  fa.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                "ORDER BY fi.STRFACILITYNAME"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace