﻿Imports Microsoft.Data.SqlClient
Imports System.Text.RegularExpressions
Imports GaEpd
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.Apb.Facilities

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
            Return DB IsNot Nothing AndAlso DB.SPGetBoolean(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns whether an AIRS number already exists in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to test.</param>
        ''' <returns>True if the AIRS number exists; otherwise false.</returns>
        ''' <remarks>Looks for value in APBMASTERAIRS table. Does not make any judgments about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(airsNumber As String) As Boolean
            If Not IsValidAirsNumberFormat(airsNumber) Then Return False
            Return AirsNumberExists(New ApbFacilityId(airsNumber))
        End Function

        ''' <summary>
        ''' Returns the facility name for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>The facility name, or an empty string if facility AIRS number does not exist.</returns>
        Public Function GetFacilityName(airsNumber As ApbFacilityId) As String
            Return DBUtilities.GetNullableString(DB.SPGetString("iaip_facility.GetFacilityName", New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)))
        End Function

        ''' <summary>
        ''' Returns a Facility with basic information for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>A Facility with basic information, or Nothing if AIRS number does not exist.</returns>
        ''' <remarks></remarks>
        Public Function GetFacility(airsNumber As ApbFacilityId) As Facility
            Dim row As DataRow
            Dim spName As String = "iaip_facility.GetFacilityBasicInfo"
            Dim parameter As New SqlParameter("@AirsNumber", airsNumber.DbFormattedString)

            Try
                row = DB.SPGetDataRow(spName, parameter)
            Catch ex As Exception
                Return Nothing
            End Try

            If row Is Nothing Then Return Nothing

            Dim facility As New Facility(airsNumber)

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
                .DeactivatedByApb = DBUtilities.GetNullable(Of Boolean)(row("ApbDeactivated"))
                .DistrictOfficeLocation = DBUtilities.GetNullable(Of String)(row("STRDISTRICTNAME"))
                .DistrictResponsible = DBUtilities.GetNullable(Of Boolean)(row("STRDISTRICTRESPONSIBLE"))
            End With

            Return facility
        End Function

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

        Public Function CanFacilityBeDeactivated(airs As ApbFacilityId) As Boolean
            Dim spName As String = "select iaip_facility.CanFacilityBeDeactivated(@airs)"
            Dim parameter As New SqlParameter("@airs", airs.DbFormattedString)
            Return DB.GetBoolean(spName, parameter)
        End Function

        Public Function CanFacilityBeDeleted(airs As ApbFacilityId) As Boolean
            Dim spName As String = "select iaip_facility.CanFacilityBeDeleted(@airs)"
            Dim parameter As New SqlParameter("@airs", airs.DbFormattedString)
            Return DB.GetBoolean(spName, parameter)
        End Function

        ''' <summary>
        ''' Deactivate a facility (AIRS number) in the database. This might be needed, for example,
        ''' if a facility submits a permit application and pays permit fees, but eventually no
        ''' permit is issued and no other data is expected for the facility. The facility can't
        ''' be deleted because of the fees data, but otherwise, deactivating the facility removes
        ''' it from use.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to delete</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function DeactivateFacility(airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.DeactivateFacility"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@airs", airsNumber.DbFormattedString),
                New SqlParameter("@userId", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Completely remove a facility (AIRS number) from the database. All records related to the
        ''' facility are deleted. This can be done only if limited facility information has been 
        ''' entered.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to delete</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function DeleteFacility(airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.DeleteFacility"
            Dim parameter As New SqlParameter("@airs", airsNumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

        ''' <summary>
        ''' Marks all data related to a facility as "updated" to trigger processing by the ICIS-Air procedures.
        ''' </summary>
        ''' <param name="airsnumber">The AIRS number of the facility to update.</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function TriggerDataUpdateAtEPA(airsnumber As ApbFacilityId) As Boolean
            Dim spName As String = "iaip_facility.TriggerDataUpdateAtEPA"
            Dim parameter As New SqlParameter("@AirsNumber", airsnumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

#End Region

#Region " AIRS Number Validation "
        'Region added to validate that a number entered as an Airs Number, not only isn't null, but also adheres to the correct Airs Number Format
        'Used when you don't know whether an Airs Number entry by the user is in a valid format or exists already in the database 

        'Added to make sure AirsNumber entered is Not Null and is of the correct Pattern and can be used separately from ValidateAirsNumber
        Public Function ValidateAirsPattern(airsInput As String) As AirsNumberValidationResult
            If airsInput = "" Then
                Return AirsNumberValidationResult.Empty
            End If

            If Not IsValidAirsNumberFormat(airsInput) Then
                Return AirsNumberValidationResult.InvalidFormat
            End If

            Return AirsNumberValidationResult.Valid
        End Function

        'Added to make sure that an AirsNumber entered by user actually exists in the database
        Public Function ValidateAirsFacility(airsInput As String) As AirsNumberValidationResult
            If airsInput = "" Then
                Return AirsNumberValidationResult.Empty
            End If

            If Not IsValidAirsNumberFormat(airsInput) Then
                Return AirsNumberValidationResult.InvalidFormat
            End If

            If Not AirsNumberExists(airsInput) Then
                Return AirsNumberValidationResult.NonExistent
            End If

            Return AirsNumberValidationResult.Valid
        End Function

        Public Function GetAirsValidationMsg(result As AirsNumberValidationResult) As String
            Select Case result
                Case AirsNumberValidationResult.InvalidFormat
                    Return "The AIRS Number is not formatted correctly. " &
                        "Please try again."

                Case AirsNumberValidationResult.NonExistent
                    Return "No facility with that AIRS Number exists. " &
                        "Please try again."

                Case AirsNumberValidationResult.Empty
                    Return "Please enter an AIRS Number."

                Case Else
                    Return Nothing
            End Select
        End Function

#End Region

    End Module

    Public Enum AirsNumberValidationResult
        Empty
        InvalidFormat
        Valid
        NonExistent
    End Enum

End Namespace