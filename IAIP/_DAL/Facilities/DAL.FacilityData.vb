Imports System.Data.SqlClient
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports EpdIt

Namespace DAL
    Module FacilityData

#Region " Read "

        ''' <summary>
        ''' Returns whether an AIRS number already exists in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to test.</param>
        ''' <returns>True if the AIRS number exists; otherwise false.</returns>
        ''' <remarks>Looks for value in APBMASTERAIRS table. Does not make any judgments about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.AirsNumberExists"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns the facility name for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>The facility name, or an empty string if facility AIRS number does not exist.</returns>
        Public Function GetFacilityName(airsNumber As ApbFacilityId) As String
            Dim fac As Facility = GetFacility(airsNumber)
            Return fac.FacilityName
        End Function

        ''' <summary>
        ''' Returns a Facility with basic information for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>A Facility with basic information, or Nothing if AIRS number does not exist.</returns>
        ''' <remarks></remarks>
        Public Function GetFacility(airsNumber As ApbFacilityId) As Facility
            Dim row As DataRow = GetFacilityAsDataRow(airsNumber)
            If row IsNot Nothing Then
                Dim facility As New Facility(airsNumber)
                FillFacilityFromDataRow(row, facility)
                Return facility
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns basic info for a specified facility as a DataRow object
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the specified facility</param>
        ''' <returns>DataRow containing basic info for the specified facility</returns>
        ''' <remarks>Data retrieved from VW_FACILITY_BASICINFO view.</remarks>
        Private Function GetFacilityAsDataRow(airsNumber As ApbFacilityId) As DataRow
            Dim spName As String = "iaip_facility.GetFacilityBasicInfo"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Try

                Return DB.SPGetDataRow(spName, parameter)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Sub FillFacilityFromDataRow(row As DataRow, ByRef facility As Facility)
            Dim address As New Address
            With address
                .City = DBUtilities.GetNullable(Of String)(row("STRFACILITYCITY"))
                .Country = "United States of America"
                .PostalCode = DBUtilities.GetNullable(Of String)(row("STRFACILITYZIPCODE"))
                .State = DBUtilities.GetNullable(Of String)(row("STRFACILITYSTATE"))
                .Street = DBUtilities.GetNullable(Of String)(row("STRFACILITYSTREET1"))
                .Street2 = DBUtilities.GetNullable(Of String)(row("STRFACILITYSTREET2"))
                If .Street2 = "N/A" Then
                    .Street2 = ""
                End If
            End With

            Dim location As New Location
            With location
                .Address = address
                .County = DBUtilities.GetNullable(Of String)(row("STRCOUNTYNAME"))
                .Latitude = DBUtilities.GetNullable(Of Decimal)(row("NUMFACILITYLATITUDE"))
                .Longitude = DBUtilities.GetNullable(Of Decimal)(row("NUMFACILITYLONGITUDE"))
            End With

            With facility
                .FacilityLocation = location
                .FacilityName = DBUtilities.GetNullable(Of String)(row("STRFACILITYNAME"))
                .ApprovedByApb = DBUtilities.GetNullable(Of Boolean)(row("ApbApproved"))
                .DistrictOfficeLocation = DBUtilities.GetNullable(Of String)(row("STRDISTRICTNAME"))
                .DistrictResponsible = DBUtilities.GetNullable(Of Boolean)(row("STRDISTRICTRESPONSIBLE"))
            End With
        End Sub

        ''' <summary>
        ''' Determines if a facility has been approved (by the APB) in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to check</param>
        ''' <returns>True if facility has been approved; otherwise, false</returns>
        ''' <remarks>Looks at STRUPDATESTATUS in AFSFACILITYDATA table.</remarks>
        Public Function FacilityHasBeenApproved(airsNumber As Apb.ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.HasFacilityBeenApproved"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        ''' <summary>
        '''  Returns a set of important dates for display purposes
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the facility to query</param>
        ''' <returns>A Dictionary with string keys and date values.</returns>
        ''' <remarks>Data retrieved from VW_FACILITY_DATADATES view.</remarks>
        Public Function GetDataExchangeDates(airsNumber As ApbFacilityId) As DataRow
            Dim spName As String = "iaip_facility.GetDataDates"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetDataRow(spName, parameter)
        End Function

#End Region

#Region " Write "

        ''' <summary>
        ''' Marks a facility as shut down in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the facility to shut down</param>
        ''' <param name="shutdownDate">The actual date the facility shut down</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function ShutDownFacility(airsNumber As Apb.ApbFacilityId,
                                          shutdownDate As Date,
                                          comments As String,
                                          fromLocation As HeaderDataModificationLocation
                                         ) As Boolean
            ' -- Transaction (handled in database procedure):
            '    1. Update APBHeaderData
            '    2. Update APBAirProgramPollutants
            '    3. Update EIS_FacilitySite
            '    4. Revoke all open permits
            Dim spName As String = "iaip_facility.ShutDownFacility"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@AirsNumber", airsNumber.DbFormattedString),
                New SqlParameter("@ShutDownDate", shutdownDate),
                New SqlParameter("@Comments", comments),
                New SqlParameter("@FromUiLocation", Convert.ToInt32(fromLocation)),
                New SqlParameter("@UserId", CurrentUser.UserID)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Completely remove a facility (AIRS number) from the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to delete</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function DeleteFacility(airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.DeleteFacility"
            Dim parameter As SqlParameter = New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

        ''' <summary>
        ''' Marks all data related to a facility as "updated" to trigger processing by the ICIS-Air procedures.
        ''' </summary>
        ''' <param name="airsnumber">The AIRS number of the facility to update.</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function TriggerDataUpdateAtEPA(airsnumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.TriggerDataUpdateAtEPA"
            Dim parameter As SqlParameter = New SqlParameter("@AirsNumber", airsnumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

#End Region

    End Module
End Namespace