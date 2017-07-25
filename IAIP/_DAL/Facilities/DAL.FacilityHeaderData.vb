Imports System.Data.SqlClient
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports EpdIt

Namespace DAL
    Module FacilityHeaderDataData

#Region " Validate SIC/NAICS "

        ''' <summary>
        ''' Returns whether an SIC Code exists in the database lookup table
        ''' </summary>
        ''' <param name="sicCode">The SIC Code to test.</param>
        ''' <returns>True if the SIC Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgments about appropriateness of SIC Code otherwise.</remarks>
        Public Function SicCodeIsValid(sicCode As String) As Boolean
            If sicCode Is Nothing OrElse String.IsNullOrEmpty(sicCode) Then Return False
            If Not Regex.IsMatch(sicCode, SicCodePattern) Then Return False

            Dim spName As String = "iaip_facility.IsSicValid"
            Dim parameter As New SqlParameter("@sic_code", sicCode)

            Return DB.SPGetSingleValue(Of String)(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns whether an NAICS Code exists in the database lookup table
        ''' </summary>
        ''' <param name="naicsCode">The NAICS Code to test.</param>
        ''' <param name="allowEmpty">A flag to indicate whether an empty NAICS code is valid.</param>
        ''' <returns>True if the NAICS Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgments about appropriateness of NAICS Code otherwise.</remarks>
        Public Function NaicsCodeIsValid(naicsCode As String, Optional allowEmpty As Boolean = True) As Boolean
            If naicsCode Is Nothing OrElse String.IsNullOrEmpty(naicsCode) Then Return allowEmpty
            If Not Regex.IsMatch(naicsCode, NaicsCodePattern) Then Return False

            Dim spName As String = "iaip_facility.IsNaicsValid"
            Dim parameter As New SqlParameter("@naics_code", naicsCode)

            Return DB.SPGetSingleValue(Of String)(spName, parameter)
        End Function

#End Region

#Region " Read "

        ''' <summary>
        ''' Returns header data for a specified facility as a DataRow object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>DataRow containing header data for the specified facility</returns>
        ''' <remarks>Data retrieved from VW_FACILITY_HEADERDATA view.</remarks>
        Public Function GetFacilityHeaderDataAsDataRow(airsNumber As ApbFacilityId) As DataRow
            Dim spName As String = "iaip_facility.GetFacilityHeaderData"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataRow(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns header data for a specified facility as a FacilityHeaderData object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>A FacilityHeaderData object containing header data for the specified facility</returns>
        ''' <remarks></remarks>
        Public Function GetFacilityHeaderData(airsNumber As ApbFacilityId) As FacilityHeaderData
            Dim row As DataRow = GetFacilityHeaderDataAsDataRow(airsNumber)
            Dim facilityHeaderData As New FacilityHeaderData(airsNumber)

            FillFacilityHeaderDataFromDataRow(row, facilityHeaderData)
            Return facilityHeaderData
        End Function

        ''' <summary>
        ''' Fill out the header data properties of a FacilityHeaderData object from a DataRow containing data from the database
        ''' </summary>
        ''' <param name="row">A DataRow containing data from the database</param>
        ''' <param name="facilityHeaderData">A FacilityHeaderData object to complete</param>
        ''' <remarks></remarks>
        Public Sub FillFacilityHeaderDataFromDataRow(row As DataRow, ByRef facilityHeaderData As FacilityHeaderData)
            With facilityHeaderData
                .OperationalStatusCode = DBUtilities.GetNullable(Of String)(row("STROPERATIONALSTATUS"))
                .ClassificationCode = DBUtilities.GetNullable(Of String)(row("STRCLASS"))
                .SicCode = DBUtilities.GetNullable(Of String)(row("STRSICCODE"))
                .ShutdownDate = DBUtilities.GetNullableDateTime(row("DATSHUTDOWNDATE"))
                .StartupDate = DBUtilities.GetNullableDateTime(row("DATSTARTUPDATE"))
                .Naics = DBUtilities.GetNullable(Of String)(row("STRNAICSCODE"))
                .RmpId = DBUtilities.GetNullable(Of String)(row("STRRMPID"))
                .FacilityDescription = DBUtilities.GetNullable(Of String)(row("STRPLANTDESCRIPTION"))
                .AirProgramsCode = DBUtilities.GetNullable(Of String)(row("STRAIRPROGRAMCODES"))
                .AirProgramClassificationsCode = DBUtilities.GetNullable(Of String)(row("STRSTATEPROGRAMCODES"))
                .NonattainmentStatusesCode = DBUtilities.GetNullable(Of String)(row("STRATTAINMENTSTATUS"))
                .HeaderUpdateComment = DBUtilities.GetNullable(Of String)(row("STRCOMMENTS"))
                .DateDataModified = DBUtilities.GetNullableDateTime(row("DATMODIFINGDATE"))
                .WhoModified = DBUtilities.GetNullable(Of String)(row("WhoModified"))
                .WhereModifiedCode = DBUtilities.GetNullable(Of Integer)(row("STRMODIFINGLOCATION"))
                .CmsMemberCode = DBUtilities.GetNullable(Of String)(row("STRCMSMEMBER"))
            End With
        End Sub

        ''' <summary>
        ''' Returns a DataTable of historical header data for a specified facility
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>A DataTable of historical header data for the specified facility</returns>
        ''' <remarks>Data retrieved from VW_HB_APBHEADERDATA view.</remarks>
        Public Function GetFacilityHeaderDataHistoryAsDataTable(airsNumber As ApbFacilityId) As DataTable
            Dim spName As String = "iaip_facility.GetFacilityHeaderDataHistory"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        Public Function GetFacilityOperationalStatus(airsNumber As ApbFacilityId) As FacilityOperationalStatus
            Dim query As String = "SELECT STROPERATIONALSTATUS FROM APBHEADERDATA WHERE STRAIRSNUMBER = @airsNumber"
            Dim parameter As New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            Return [Enum].Parse(GetType(FacilityOperationalStatus), DB.GetString(query, parameter))
        End Function

#End Region

#Region " Write "

        ''' <summary>
        ''' Saves header data for a facility to the database
        ''' </summary>
        ''' <param name="headerData">A FacilityHeaderData object containing the data to save to the database</param>
        ''' <param name="fromLocation">A ModificationLocation Enum representing the user interface location where a change in facility header data was initiated</param>
        ''' <returns>True if the data was successfully saved to the database; otherwise, False</returns>
        ''' <remarks></remarks>
        Public Function SaveFacilityHeaderData(headerData As FacilityHeaderData, fromLocation As HeaderDataModificationLocation) As Boolean
            If Not AirsNumberExists(headerData.AirsNumber) Then Return False

            ' Stored Procedure accomplishes the following:
            '  1. Update ApbHeaderData
            '  2. Update ApbSupplamentalData (sic)
            '  3. Any active APC must have at least one key in ApbAirProgramPollutants:
            '     * Update all existing keys with new operating status
            '     * If none exist, add one with the new operating status, pollutant = OT & compliance status = C
            '  4. For any inactive APC, change any existing subparts in ApbSubpartData to inactive
            '  5. Change update status in AfsAirPollutantData to 'C' where currently 'N'

            Dim spName As String = "iaip_facility.UpdateFacilityHeaderData"

            ' Build active and inactive apc lists...
            Dim activeApcList As New List(Of String)
            Dim inactiveApcList As New List(Of String)
            Dim apcArray As Array = [Enum].GetValues(GetType(AirProgram))
            For Each apc As AirProgram In apcArray
                If apc = AirProgram.None Then
                    Continue For
                End If

                If (headerData.AirPrograms.HasFlag(apc)) Then
                    activeApcList.Add(FacilityHeaderData.GetAirProgramDbKey(apc))
                Else
                    inactiveApcList.Add(FacilityHeaderData.GetAirProgramDbKey(apc))
                End If
            Next

            Dim params As SqlParameter() = {
                New SqlParameter("@airsNumber", headerData.AirsNumber.DbFormattedString),
                New SqlParameter("@operationalStatusCode", headerData.OperationalStatusCode),
                New SqlParameter("@classification", headerData.Classification.ToString),
                New SqlParameter("@airProgramsCode", headerData.AirProgramsCode),
                New SqlParameter("@sic", headerData.SicCode),
                New SqlParameter("@naics", headerData.Naics),
                New SqlParameter("@startupDate", headerData.StartupDate),
                New SqlParameter("@shutdownDate", headerData.ShutdownDate),
                New SqlParameter("@headerUpdateComment", headerData.HeaderUpdateComment),
                New SqlParameter("@facilityDescription", headerData.FacilityDescription),
                New SqlParameter("@nonattainmentStatusesCode", headerData.NonattainmentStatusesCode),
                New SqlParameter("@airProgramClassificationsCode", headerData.AirProgramClassificationsCode),
                New SqlParameter("@fromLocation", Convert.ToInt32(fromLocation)),
                New SqlParameter("@rmpId", headerData.RmpId),
                New SqlParameter("@modifiedBy", CurrentUser.UserID),
                activeApcList.AsTvpSqlParameter("@activeApcList"),
                inactiveApcList.AsTvpSqlParameter("@inactiveApcList")
            }

            Return DB.SPRunCommand(spName, params)
        End Function

#End Region

    End Module
End Namespace