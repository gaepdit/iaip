Imports Oracle.DataAccess.Client
Imports Iaip.Apb
Imports Iaip.Apb.Facility
Imports System.Collections.Generic

Namespace DAL
    Module Facility

#Region " Read "

        ''' <summary>
        ''' Returns whether an AIRS number already exists in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to test.</param>
        ''' <returns>True if the AIRS number exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgments about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(ByVal airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "IAIP_FACILITY.DoesAirsNumberExist"
            Dim parameter As New OracleParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns whether an AIRS number already exists in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to test as a string.</param>
        ''' <returns>True if the AIRS number exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgments about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(ByVal airsNumber As String) As Boolean
            If Not ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
                Return False
            Else
                Return AirsNumberExists(CType(airsNumber, ApbFacilityId))
            End If
        End Function

        ''' <summary>
        ''' Returns the facility name for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for as a string.</param>
        ''' <returns>The facility name, or an empty string if facility AIRS number does not exist.</returns>
        Public Function GetFacilityName(ByVal airsNumber As ApbFacilityId) As String
            Dim fac As Apb.Facility = GetFacility(airsNumber)
            Return fac.FacilityName
        End Function

        ''' <summary>
        ''' Returns a Facility with basic information for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>A Facility with basic information, or Nothing if AIRS number does not exist.</returns>
        ''' <remarks></remarks>
        Public Function GetFacility(ByVal airsNumber As ApbFacilityId) As Apb.Facility
            Dim row As DataRow = GetFacilityAsDataRow(airsNumber)
            If row IsNot Nothing Then
                Dim facility As New Apb.Facility(airsNumber)
                FillFacilityFromDataRow(row, facility)
                Return facility
            Else
                Return Nothing
            End If
        End Function

        Private Function GetFacilityAsDataRow(ByVal airsNumber As ApbFacilityId) As DataRow
            Dim spName As String = "IAIP_FACILITY.GetFacilityBasicInfo"
            Dim parameter As New OracleParameter("AirsNumber", airsNumber.DbFormattedString)

            Return DB.SPGetDataRow(spName, parameter)
        End Function

        Private Sub FillFacilityFromDataRow(ByVal row As DataRow, ByRef facility As Apb.Facility)
            Dim address As New Address
            With address
                .City = DB.GetNullable(Of String)(row("STRFACILITYCITY"))
                .Country = "United States of America"
                .PostalCode = DB.GetNullable(Of String)(row("STRFACILITYZIPCODE"))
                .State = DB.GetNullable(Of String)(row("STRFACILITYSTATE"))
                .Street = DB.GetNullable(Of String)(row("STRFACILITYSTREET1"))
                .Street2 = DB.GetNullable(Of String)(row("STRFACILITYSTREET2"))
                If .Street2 = "N/A" Then
                    .Street2 = ""
                End If
            End With

            Dim location As New Location
            With location
                .Address = address
                .County = DB.GetNullable(Of String)(row("STRCOUNTYNAME"))
                .Latitude = DB.GetNullable(Of Decimal)(row("NUMFACILITYLATITUDE"))
                .Longitude = DB.GetNullable(Of Decimal)(row("NUMFACILITYLONGITUDE"))
            End With

            With facility
                .FacilityLocation = location
                .FacilityName = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
            End With
        End Sub

        ''' <summary>
        ''' Determines if a facility has been approved (by the APB) in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to check</param>
        ''' <returns>True if facility has been approved; otherwise, false</returns>
        Public Function FacilityHasBeenApproved(ByVal airsNumber As Apb.ApbFacilityId) As Boolean
            Dim spName As String = "IAIP_FACILITY.HasFacilityBeenApproved"
            Dim parameter As New OracleParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

#End Region

#Region " Write "

        ''' <summary>
        ''' Marks a facility as shut down in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number of the facility to shut down</param>
        ''' <param name="shutdownDate">The actual date the facility shut down</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function ShutDownFacility(ByVal airsNumber As Apb.ApbFacilityId, _
                                         ByVal shutdownDate As Date, _
                                         ByVal comments As String, _
                                         ByVal fromLocation As Apb.FacilityHeaderData.ModificationLocation _
                                         ) As Boolean
            ' -- Transaction:
            '    1. Update APBHeaderData
            '    2. Update APBAirProgramPollutants
            '    3. Update EIS_FacilitySite
            '    4. Revoke all open permits
            Dim spName As String = "IAIP_FACILITY.ShutDownFacility"
            Dim parameters As OracleParameter() = { _
                New OracleParameter("AirsNumber", airsNumber.DbFormattedString), _
                New OracleParameter("ShutDownDate", shutdownDate), _
                New OracleParameter("Comments", comments), _
                New OracleParameter("FromUiLocation", fromLocation), _
                New OracleParameter("UserId", UserGCode) _
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Completely remove a facility (AIRS number) from the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to delete</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function DeleteFacility(ByVal airsNumber As ApbFacilityId) As Boolean
            Dim spName As String = "IAIP_FACILITY.ShutDownFacility"
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsNumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

        ''' <summary>
        ''' Marks all data related to a facility as "updated" to trigger processing by the ICIS-Air procedures.
        ''' </summary>
        ''' <param name="airsnumber">The AIRS number of the facility to update.</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function TriggerDataUpdateAtEPA(ByVal airsnumber As ApbFacilityId) As Boolean
            Dim spName As String = "IAIP_FACILITY.TriggerDataUpdateAtEPA"
            Dim parameter As OracleParameter = New OracleParameter("AirsNumber", airsnumber.DbFormattedString)
            Return DB.SPRunCommand(spName, parameter)
        End Function

#End Region

    End Module
End Namespace