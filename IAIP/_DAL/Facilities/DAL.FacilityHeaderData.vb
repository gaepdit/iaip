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
        Public Function SicCodeIsValid(ByVal sicCode As String) As Boolean
            If sicCode Is Nothing OrElse String.IsNullOrEmpty(sicCode) Then Return False
            If Not Regex.IsMatch(sicCode, SicCodePattern) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.LK_SIC " &
                " WHERE RowNum = 1 " &
                " AND ACTIVE = 1 " &
                " AND SIC_CODE = :pId "
            Dim parameter As New SqlParameter("pId", sicCode)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Returns whether an NAICS Code exists in the database lookup table
        ''' </summary>
        ''' <param name="naicsCode">The NAICS Code to test.</param>
        ''' <returns>True if the NAICS Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgments about appropriateness of NAICS Code otherwise.</remarks>
        Public Function NaicsCodeIsValid(ByVal naicsCode As String) As Boolean
            If naicsCode Is Nothing OrElse String.IsNullOrEmpty(naicsCode) Then Return False
            If Not Regex.IsMatch(naicsCode, NaicsCodePattern) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.LK_NAICS " &
                " WHERE RowNum = 1 " &
                " AND ACTIVE = 1 " &
                " AND NAICS_CODE = :pId "
            Dim parameter As New SqlParameter("pId", naicsCode)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

#End Region

#Region " Read "

        ''' <summary>
        ''' Returns header data for a specified facility as a DataRow object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>DataRow containing header data for the specified facility</returns>
        ''' <remarks>Data retrieved from VW_FACILITY_HEADERDATA view.</remarks>
        Public Function GetFacilityHeaderDataAsDataRow(ByVal airsNumber As ApbFacilityId) As DataRow
            Dim spName As String = "AIRBRANCH.IAIP_FACILITY.GetFacilityHeaderData"
            Dim parameter As New SqlParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataRow(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns header data for a specified facility as a FacilityHeaderData object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>A FacilityHeaderData object containing header data for the specified facility</returns>
        ''' <remarks></remarks>
        Public Function GetFacilityHeaderData(ByVal airsNumber As ApbFacilityId) As FacilityHeaderData
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
        Public Sub FillFacilityHeaderDataFromDataRow(ByVal row As DataRow, ByRef facilityHeaderData As FacilityHeaderData)
            With facilityHeaderData
                .OperationalStatusCode = DBUtilities.GetNullable(Of String)(row("STROPERATIONALSTATUS"))
                .ClassificationCode = DBUtilities.GetNullable(Of String)(row("STRCLASS"))
                .SicCode = DBUtilities.GetNullable(Of String)(row("STRSICCODE"))
                .ShutdownDate = DBUtilities.GetNullableDateTimeFromString(row("DATSHUTDOWNDATE"))
                .StartupDate = DBUtilities.GetNullableDateTimeFromString(row("DATSTARTUPDATE"))
                .Naics = DBUtilities.GetNullable(Of String)(row("STRNAICSCODE"))
                .RmpId = DBUtilities.GetNullable(Of String)(row("STRRMPID"))
                .FacilityDescription = DBUtilities.GetNullable(Of String)(row("STRPLANTDESCRIPTION"))
                .AirProgramsCode = DBUtilities.GetNullable(Of String)(row("STRAIRPROGRAMCODES"))
                .AirProgramClassificationsCode = DBUtilities.GetNullable(Of String)(row("STRSTATEPROGRAMCODES"))
                .NonattainmentStatusesCode = DBUtilities.GetNullable(Of String)(row("STRATTAINMENTSTATUS"))
                .HeaderUpdateComment = DBUtilities.GetNullable(Of String)(row("STRCOMMENTS"))
                .DateDataModified = DBUtilities.GetNullableDateTimeFromString(row("DATMODIFINGDATE"))
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
        Public Function GetFacilityHeaderDataHistoryAsDataTable(ByVal airsNumber As ApbFacilityId) As DataTable
            Dim spName As String = "AIRBRANCH.IAIP_FACILITY.GetFacilityHeaderDataHistory"
            Dim parameter As New SqlParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        Public Function GetFacilityOperationalStatus(airsNumber As ApbFacilityId) As FacilityOperationalStatus
            Dim query As String = "SELECT STROPERATIONALSTATUS FROM AIRBRANCH.APBHEADERDATA WHERE STRAIRSNUMBER = :airsNumber"
            Dim parameter As New SqlParameter("airsNumber", airsNumber.DbFormattedString)
            Return [Enum].Parse(GetType(FacilityOperationalStatus), DB.GetSingleValue(Of String)(query, parameter))
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
        Public Function SaveFacilityHeaderData(ByVal headerData As FacilityHeaderData, ByVal fromLocation As HeaderDataModificationLocation) As Boolean
            If Not AirsNumberExists(headerData.AirsNumber) Then Return False

            ' -- Transaction
            ' 1. Update ApbHeaderData
            ' 2. Update ApbSupplamentalData (sic)
            ' 3. Any active APC must have at least one key in ApbAirProgramPollutants:
            '    * Update all existing keys with new operating status
            '    * If none exist, add one with the new operating status, pollutant = OT & compliance status = C
            ' 4. For any inactive APC, change any existing subparts in ApbSubpartData to inactive
            ' 5. Change update status in AfsAirPollutantData to 'C' where currently 'N'
            ' -- Commit transaction

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())


            ' 1. Update ApbHeaderData
            queryList.Add(
                "UPDATE AIRBRANCH.APBHEADERDATA " &
                "  SET STROPERATIONALSTATUS = :OperationalStatusCode, " &
                "    STRCLASS               = :Classification, " &
                "    STRAIRPROGRAMCODES     = :AirProgramsCode, " &
                "    STRSICCODE             = :SicCode, " &
                "    DATSTARTUPDATE         = :StartupDate, " &
                "    DATSHUTDOWNDATE        = :ShutdownDate, " &
                "    STRCOMMENTS            = :HeaderUpdateComment, " &
                "    STRPLANTDESCRIPTION    = :FacilityDescription, " &
                "    STRATTAINMENTSTATUS    = :NonattainmentStatusesCode, " &
                "    STRSTATEPROGRAMCODES   = :AirProgramClassifications, " &
                "    STRMODIFINGLOCATION    = :fromLocation, " &
                "    STRNAICSCODE           = :Naics, " &
                "    STRMODIFINGPERSON      = :modifiedby, " &
                "    DATMODIFINGDATE        = sysdate " &
                "  WHERE STRAIRSNUMBER      = :airsnumber "
            )
            parametersList.Add(New SqlParameter() {
                New SqlParameter("OperationalStatusCode", headerData.OperationalStatusCode),
                New SqlParameter("Classification", headerData.Classification.ToString),
                New SqlParameter("AirProgramsCode", headerData.AirProgramsCode),
                New SqlParameter("SicCode", headerData.SicCode),
                New SqlParameter("StartupDate", headerData.StartupDate),
                New SqlParameter("ShutdownDate", headerData.ShutdownDate),
                New SqlParameter("HeaderUpdateComment", headerData.HeaderUpdateComment),
                New SqlParameter("FacilityDescription", headerData.FacilityDescription),
                New SqlParameter("NonattainmentStatusesCode", headerData.NonattainmentStatusesCode),
                New SqlParameter("AirProgramClassifications", headerData.AirProgramClassificationsCode),
                New SqlParameter("fromLocation", Convert.ToInt32(fromLocation)),
                New SqlParameter("Naics", headerData.Naics),
                New SqlParameter("modifiedby", CurrentUser.UserID),
                New SqlParameter("airsnumber", headerData.AirsNumber.DbFormattedString)
            })

            ' 2. Update ApbSupplamentalData (sic)
            queryList.Add(
                " UPDATE AIRBRANCH.APBSUPPLAMENTALDATA " &
                "  SET STRMODIFINGPERSON = :modifiedby, " &
                "    DATMODIFINGDATE     = sysdate, " &
                "    STRRMPID            = :rmp " &
                "  WHERE STRAIRSNUMBER   = :airsnumber "
            )
            parametersList.Add(New SqlParameter() {
                New SqlParameter("modifiedby", CurrentUser.UserID),
                New SqlParameter("rmp", headerData.RmpId),
                New SqlParameter("airsnumber", headerData.AirsNumber.DbFormattedString)
            })

            ' Check for existance of each possible AirProgram
            Dim apcArray As Array = System.[Enum].GetValues(GetType(AirProgram))
            For Each apc As AirProgram In apcArray
                If apc = AirProgram.None Then
                    Continue For
                End If

                If (headerData.AirPrograms.HasFlag(apc)) Then

                    ' 3a. For each active APC, update all existing keys in 
                    '     ApbAirProgramPollutants with new operating status
                    queryList.Add(
                        " UPDATE AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                        "  SET STROPERATIONALSTATUS = :operatingstatus " &
                        "  WHERE STRAIRSNUMBER      = :airsnumber " &
                        "  AND STROPERATIONALSTATUS <> 'X' "
                    )
                    parametersList.Add(New SqlParameter() {
                        New SqlParameter("operatingstatus", headerData.OperationalStatus.ToString),
                        New SqlParameter("airsnumber", headerData.AirsNumber.DbFormattedString)
                    })

                    ' 3b. Any active APC must have at least one key in ApbAirProgramPollutants;
                    '     if none exist, add one with the new operating status, pollutant = OT 
                    '     & compliance status = 0 (compliance status column is deprecated)
                    queryList.Add(
                        " INSERT " &
                        " INTO AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                        "  ( STRAIRSNUMBER, " &
                        "    STRAIRPOLLUTANTKEY, " &
                        "    STRPOLLUTANTKEY, " &
                        "    STRMODIFINGPERSON, " &
                        "    DATMODIFINGDATE, " &
                        "    STROPERATIONALSTATUS ) " &
                        " SELECT :airsnumber, " &
                        "    :airpollkey, " &
                        "    :pollkey, " &
                        "    :modifiedby, " &
                        "    SYSDATE, " &
                        "    :operatingstatus " &
                        " FROM DUAL " &
                        " WHERE NOT EXISTS " &
                        "  (SELECT NULL " &
                        "  FROM AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                        "  WHERE STRAIRPOLLUTANTKEY = :airpollkey " &
                        "  ) "
                    )
                    parametersList.Add(New SqlParameter() {
                        New SqlParameter("airsnumber", headerData.AirsNumber.DbFormattedString),
                        New SqlParameter("airpollkey", headerData.AirsNumber.DbFormattedString & FacilityHeaderData.GetAirProgramDbKey(apc)),
                        New SqlParameter("pollkey", "OT"),
                        New SqlParameter("modifiedby", CurrentUser.UserID),
                        New SqlParameter("operatingstatus", headerData.OperationalStatus.ToString)
                    })

                Else

                    ' 4. For any inactive APC, change any existing subparts in ApbSubpartData to inactive
                    queryList.Add(
                        " UPDATE AIRBRANCH.APBSUBPARTDATA " &
                        "  SET ACTIVE          = :active, " &
                        "    UPDATEUSER        = :modifiedby, " &
                        "    UPDATEDATETIME    = sysdate " &
                        "  WHERE STRSUBPARTKEY = :airpollkey "
                    )
                    parametersList.Add(New SqlParameter() {
                        New SqlParameter("active", "0"),
                        New SqlParameter("modifiedby", CurrentUser.UserID),
                        New SqlParameter("airpollkey", headerData.AirsNumber.DbFormattedString & FacilityHeaderData.GetAirProgramDbKey(apc))
                    })

                End If
            Next

            ' 5. Change update status in AfsAirPollutantData to 'C' where currently 'N'
            queryList.Add(
                " UPDATE AIRBRANCH.AFSAIRPOLLUTANTDATA " &
                "  SET STRUPDATESTATUS   = 'C', " &
                "    STRMODIFINGPERSON   = :modifiedby, " &
                "    DATMODIFINGDATE     = sysdate " &
                "  WHERE STRAIRSNUMBER   = :airsnumber " &
                "    AND STRUPDATESTATUS = 'N' "
            )
            parametersList.Add(New SqlParameter() {
                New SqlParameter("modifiedby", CurrentUser.UserID),
                New SqlParameter("airsnumber", headerData.AirsNumber.DbFormattedString)
            })

            Return DB.RunCommand(queryList, parametersList)
        End Function

#End Region

    End Module
End Namespace