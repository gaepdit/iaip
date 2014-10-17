Imports Oracle.DataAccess.Client
Imports Iaip.Apb
Imports Iaip.Apb.Facility
Imports System.Collections.Generic

Namespace DAL
    Module FacilityHeaderData

#Region " Validate SIC/NAICS "

        ''' <summary>
        ''' Returns whether an SIC Code exists in the database lookup table
        ''' </summary>
        ''' <param name="sicCode">The SIC Code to test.</param>
        ''' <returns>True if the SIC Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgements about appropriateness of SIC Code otherwise.</remarks>
        Public Function SicCodeExists(ByVal sicCode As String) As Boolean
            If sicCode Is Nothing OrElse String.IsNullOrEmpty(sicCode) Then Return False

            ' Valid SIC Codes are one to four digits
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{1,4}$")
            If Not rgx.IsMatch(sicCode) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".LOOKUPSICCODES " & _
                " WHERE RowNum = 1 " & _
                " AND STRSICCODE = :pId "
            Dim parameter As New OracleParameter("pId", sicCode)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Returns whether an NAICS Code exists in the database lookup table
        ''' </summary>
        ''' <param name="naicsCode">The NAICS Code to test.</param>
        ''' <returns>True if the NAICS Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgements about appropriateness of NAICS Code otherwise.</remarks>
        Public Function NaicsCodeExists(ByVal naicsCode As String) As Boolean
            If naicsCode Is Nothing OrElse String.IsNullOrEmpty(naicsCode) Then Return False

            ' Valid NAICS Codes are two to six digits
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{2,6}$")
            If Not rgx.IsMatch(naicsCode) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".EILOOKUPNAICS " & _
                " WHERE RowNum = 1 " & _
                " AND STRNAICSCODE = :pId "
            Dim parameter As New OracleParameter("pId", naicsCode)

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
        ''' <remarks></remarks>
        Public Function GetFacilityHeaderDataAsDataRow(ByVal airsNumber As String) As DataRow
            If Not NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = " SELECT " & _
                "  Null AS STRKEY, " & _
                "  USERNAME, " & _
                "  MODIFINGDATE, " & _
                "  STRMODIFINGLOCATION, " & _
                "  STROPERATIONALSTATUS, " & _
                "  STRCLASS, " & _
                "  STRCOMMENTS, " & _
                "  DATSTARTUPDATE, " & _
                "  DATSHUTDOWNDATE, " & _
                "  STRPLANTDESCRIPTION, " & _
                "  STRSICCODE, " & _
                "  STRNAICSCODE, " & _
                "  STRRMPID, " & _
                "  STRAIRPROGRAMCODES, " & _
                "  STRSTATEPROGRAMCODES, " & _
                "  STRATTAINMENTSTATUS " & _
                " FROM " & DBNameSpace & ".VW_APBFACILITYHEADER " & _
                " WHERE STRAIRSNUMBER = :pId "

            Dim parameter As New OracleParameter("pId", airsNumber)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        ''' <summary>
        ''' Returns header data for a specified facility as a FacilityHeaderData object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>A FacilityHeaderData object containing header data for the specified facility</returns>
        ''' <remarks></remarks>
        Public Function GetFacilityHeaderData(ByVal airsNumber As String) As Apb.FacilityHeaderData
            Dim row As DataRow = GetFacilityHeaderDataAsDataRow(airsNumber)
            Dim facilityHeaderData As New Apb.FacilityHeaderData(airsNumber)

            FillFacilityHeaderDataFromDataRow(row, facilityHeaderData)
            Return facilityHeaderData
        End Function

        ''' <summary>
        ''' Fill out the header data properties of a FacilityHeaderData object from a DataRow containing data from the database
        ''' </summary>
        ''' <param name="row">A DataRow containing data from the database</param>
        ''' <param name="facilityHeaderData">A FacilityHeaderData object to complete</param>
        ''' <remarks></remarks>
        Public Sub FillFacilityHeaderDataFromDataRow(ByVal row As DataRow, ByRef facilityHeaderData As Apb.FacilityHeaderData)
            With facilityHeaderData
                .OperationalStatusCode = DB.GetNullable(Of String)(row("STROPERATIONALSTATUS"))
                .ClassificationCode = DB.GetNullable(Of String)(row("STRCLASS"))
                .SicCode = DB.GetNullable(Of String)(row("STRSICCODE"))
                .ShutdownDate = DB.GetNullableDateTimeFromString(row("DATSHUTDOWNDATE"))
                .StartupDate = DB.GetNullableDateTimeFromString(row("DATSTARTUPDATE"))
                .Naics = DB.GetNullable(Of String)(row("STRNAICSCODE"))
                .RmpId = DB.GetNullable(Of String)(row("STRRMPID"))
                .FacilityDescription = DB.GetNullable(Of String)(row("STRPLANTDESCRIPTION"))
                .AirProgramsCode = DB.GetNullable(Of String)(row("STRAIRPROGRAMCODES"))
                .AirProgramClassificationsCode = DB.GetNullable(Of String)(row("STRSTATEPROGRAMCODES"))
                .NonattainmentStatusesCode = DB.GetNullable(Of String)(row("STRATTAINMENTSTATUS"))
                .HeaderUpdateComment = DB.GetNullable(Of String)(row("STRCOMMENTS"))
                .DateDataModified = DB.GetNullableDateTimeFromString(row("MODIFINGDATE"))
                .WhoModified = DB.GetNullable(Of String)(row("USERNAME"))
                .WhereModified = DB.GetNullable(Of String)(row("STRMODIFINGLOCATION"))
            End With
        End Sub

        ''' <summary>
        ''' Returns a DataTable of historical header data for a specified facility
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>A DataTable of historical header data for the specified facility</returns>
        ''' <remarks></remarks>
        Public Function GetFacilityHeaderDataHistoryAsDataTable(ByVal airsNumber As String) As DataTable
            If Not NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = " SELECT " & _
                "  STRKEY, " & _
                "  USERNAME, " & _
                "  MODIFINGDATE, " & _
                "  STRMODIFINGLOCATION, " & _
                "  STROPERATIONALSTATUS, " & _
                "  STRCLASS, " & _
                "  STRCOMMENTS, " & _
                "  DATSTARTUPDATE, " & _
                "  DATSHUTDOWNDATE, " & _
                "  STRPLANTDESCRIPTION, " & _
                "  STRSICCODE, " & _
                "  STRNAICSCODE, " & _
                "  STRRMPID, " & _
                "  STRAIRPROGRAMCODES, " & _
                "  STRSTATEPROGRAMCODES, " & _
                "  STRATTAINMENTSTATUS " & _
                " FROM " & DBNameSpace & ".VW_HB_APBHEADERDATA " & _
                " WHERE STRAIRSNUMBER = :pId " & _
                " ORDER BY STRKEY DESC "

            Dim parameter As New OracleParameter("pId", airsNumber)

            Return DB.GetDataTable(query, parameter)
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
        Public Function SaveFacilityHeaderData(ByVal headerData As Apb.FacilityHeaderData, ByVal fromLocation As Apb.FacilityHeaderData.ModificationLocation) As Boolean
            If Not AirsNumberExists(headerData.AirsNumber) Then Return False
            Dim ExpandedAirsNumber As String = GetNormalizedAirsNumber(headerData.AirsNumber, True)

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
            Dim parametersList As New List(Of OracleParameter())


            ' 1. Update ApbHeaderData
            queryList.Add( _
                "UPDATE " & DBNameSpace & ".APBHEADERDATA " & _
                "  SET STROPERATIONALSTATUS = :OperationalStatusCode, " & _
                "    STRCLASS               = :Classification, " & _
                "    STRAIRPROGRAMCODES     = :AirProgramsCode, " & _
                "    STRSICCODE             = :SicCode, " & _
                "    DATSTARTUPDATE         = :StartupDate, " & _
                "    DATSHUTDOWNDATE        = :ShutdownDate, " & _
                "    STRCOMMENTS            = :HeaderUpdateComment, " & _
                "    STRPLANTDESCRIPTION    = :FacilityDescription, " & _
                "    STRATTAINMENTSTATUS    = :NonattainmentStatusesCode, " & _
                "    STRSTATEPROGRAMCODES   = :AirProgramClassifications, " & _
                "    STRMODIFINGLOCATION    = :fromLocation, " & _
                "    STRNAICSCODE           = :Naics, " & _
                "    STRMODIFINGPERSON      = :modifiedby, " & _
                "    DATMODIFINGDATE        = sysdate " & _
                "  WHERE STRAIRSNUMBER      = :airsnumber " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("OperationalStatusCode", headerData.OperationalStatusCode), _
                New OracleParameter("Classification", headerData.Classification.ToString), _
                New OracleParameter("AirProgramsCode", headerData.AirProgramsCode), _
                New OracleParameter("SicCode", headerData.SicCode), _
                New OracleParameter("StartupDate", headerData.StartupDate), _
                New OracleParameter("ShutdownDate", headerData.ShutdownDate), _
                New OracleParameter("HeaderUpdateComment", headerData.HeaderUpdateComment), _
                New OracleParameter("FacilityDescription", headerData.FacilityDescription), _
                New OracleParameter("NonattainmentStatusesCode", headerData.NonattainmentStatusesCode), _
                New OracleParameter("AirProgramClassifications", headerData.AirProgramClassificationsCode), _
                New OracleParameter("fromLocation", Convert.ToInt32(fromLocation)), _
                New OracleParameter("Naics", headerData.Naics), _
                New OracleParameter("modifiedby", UserGCode), _
                New OracleParameter("airsnumber", ExpandedAirsNumber) _
            })

            ' 2. Update ApbSupplamentalData (sic)
            queryList.Add( _
                " UPDATE " & DBNameSpace & ".APBSUPPLAMENTALDATA " & _
                "  SET STRMODIFINGPERSON = :modifiedby, " & _
                "    DATMODIFINGDATE     = sysdate, " & _
                "    STRRMPID            = :rmp " & _
                "  WHERE STRAIRSNUMBER   = :airsnumber " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("modifiedby", UserGCode), _
                New OracleParameter("rmp", headerData.RmpId), _
                New OracleParameter("airsnumber", ExpandedAirsNumber) _
            })

            ' Check for existance of each possible AirProgram
            Dim apcArray As Array = System.[Enum].GetValues(GetType(AirProgram))
            For Each apc As AirProgram In apcArray
                If apc = AirProgram.None Then
                    Continue For
                End If

                ' TODO: Change to HasFlag after converting to .NET 4.0
                If (headerData.AirPrograms And apc) <> 0 Then

                    ' 3a. For each active APC, update all existing keys in 
                    '     ApbAirProgramPollutants with new operating status
                    queryList.Add( _
                        " UPDATE " & DBNameSpace & ".APBAIRPROGRAMPOLLUTANTS " & _
                        "  SET STROPERATIONALSTATUS = :operatingstatus " & _
                        "  WHERE STRAIRSNUMBER      = :airsnumber " _
                    )
                    parametersList.Add(New OracleParameter() { _
                        New OracleParameter("operatingstatus", headerData.OperationalStatus.ToString), _
                        New OracleParameter("airsnumber", ExpandedAirsNumber) _
                    })

                    ' 3b. Any active APC must have at least one key in ApbAirProgramPollutants;
                    '     if none exist, add one with the new operating status, pollutant = OT 
                    '     & compliance status = C
                    queryList.Add( _
                        " INSERT " & _
                        " INTO " & DBNameSpace & ".APBAIRPROGRAMPOLLUTANTS " & _
                        "  ( STRAIRSNUMBER, " & _
                        "    STRAIRPOLLUTANTKEY, " & _
                        "    STRPOLLUTANTKEY, " & _
                        "    STRCOMPLIANCESTATUS, " & _
                        "    STRMODIFINGPERSON, " & _
                        "    DATMODIFINGDATE, " & _
                        "    STROPERATIONALSTATUS ) " & _
                        " SELECT :airsnumber, " & _
                        "    :airpollkey, " & _
                        "    :pollkey, " & _
                        "    :compliancestatus, " & _
                        "    :modifiedby, " & _
                        "    SYSDATE, " & _
                        "    :operatingstatus " & _
                        " FROM DUAL " & _
                        " WHERE NOT EXISTS " & _
                        "  (SELECT NULL " & _
                        "  FROM " & DBNameSpace & ".APBAIRPROGRAMPOLLUTANTS " & _
                        "  WHERE STRAIRPOLLUTANTKEY = :airpollkey " & _
                        "  ) " _
                    )
                    parametersList.Add(New OracleParameter() { _
                        New OracleParameter("airsnumber", ExpandedAirsNumber), _
                        New OracleParameter("airpollkey", ExpandedAirsNumber & GetAirProgramDbKey(apc)), _
                        New OracleParameter("pollkey", "OT"), _
                        New OracleParameter("compliancestatus", "C"), _
                        New OracleParameter("modifiedby", UserGCode), _
                        New OracleParameter("operatingstatus", headerData.OperationalStatus.ToString) _
                    })

                Else

                    ' 4. For any inactive APC, change any existing subparts in ApbSubpartData to inactive
                    queryList.Add( _
                        " UPDATE " & DBNameSpace & ".APBSUBPARTDATA " & _
                        "  SET ACTIVE          = :active, " & _
                        "    UPDATEUSER        = :modifiedby, " & _
                        "    UPDATEDATETIME    = sysdate " & _
                        "  WHERE STRSUBPARTKEY = :airpollkey " _
                    )
                    parametersList.Add(New OracleParameter() { _
                        New OracleParameter("active", "0"), _
                        New OracleParameter("modifiedby", UserGCode), _
                        New OracleParameter("airpollkey", ExpandedAirsNumber & GetAirProgramDbKey(apc)) _
                    })

                End If
            Next

            ' 5. Change update status in AfsAirPollutantData to 'C' where currently 'N'
            queryList.Add( _
                " UPDATE " & DBNameSpace & ".AFSAIRPOLLUTANTDATA " & _
                "  SET STRUPDATESTATUS   = 'C', " & _
                "    STRMODIFINGPERSON   = :modifiedby, " & _
                "    DATMODIFINGDATE     = sysdate " & _
                "  WHERE STRAIRSNUMBER   = :airsnumber " & _
                "    AND STRUPDATESTATUS = 'N' " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("modifiedby", UserGCode), _
                New OracleParameter("airsnumber", ExpandedAirsNumber) _
            })

            Return DB.RunCommand(queryList, parametersList)
        End Function

#End Region

    End Module
End Namespace