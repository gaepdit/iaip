﻿Imports Oracle.DataAccess.Client
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
        ''' <remarks>Does not make any judgements about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(ByVal airsNumber As ApbFacilityId) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.APBMasterAIRS " & _
                " WHERE RowNum = 1 " & _
                " AND strAIRSnumber = :pId "
            Dim parameter As New OracleParameter("pId", airsNumber.DbFormattedString)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

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
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>The facility name, or an empty string if facility AIRS number does not exist.</returns>
        Public Function GetFacilityName(ByVal airsNumber As ApbFacilityId) As String
            Dim query As String = "SELECT STRFACILITYNAME " & _
                " FROM AIRBRANCH.APBFACILITYINFORMATION " & _
                " WHERE STRAIRSNUMBER = :pId"
            Dim parameter As New OracleParameter("pId", airsNumber.DbFormattedString)

            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a Facility with basic information for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>A Facility with basic information, or Nothing if AIRS number does not exist.</returns>
        ''' <remarks></remarks>
        Public Function GetFacility(ByVal airsNumber As ApbFacilityId) As Apb.Facility
            Dim row As DataRow = GetFacilityAsDataRow(airsNumber)
            Dim facility As New Apb.Facility(airsNumber)

            FillFacilityFromDataRow(row, facility)
            Return facility
        End Function

        Private Function GetFacilityAsDataRow(ByVal airsNumber As ApbFacilityId) As DataRow
            Dim query As String = "SELECT APBFACILITYINFORMATION.STRFACILITYNAME, " & _
                "   APBFACILITYINFORMATION.STRFACILITYCITY, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTATE, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET2, " & _
                "   APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLONGITUDE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLATITUDE, " & _
                "   LOOKUPCOUNTYINFORMATION.STRCOUNTYNAME " & _
                " FROM AIRBRANCH.APBFACILITYINFORMATION " & _
                " LEFT JOIN AIRBRANCH.LOOKUPCOUNTYINFORMATION " & _
                " ON SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5, 3) = LOOKUPCOUNTYINFORMATION.STRCOUNTYCODE " & _
                " WHERE APBFACILITYINFORMATION.STRAIRSNUMBER = :pId "

            Dim parameter As New OracleParameter("pId", airsNumber.DbFormattedString)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
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
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
            " FROM AIRBRANCH.AFSFacilityData " & _
            " WHERE RowNum = 1 " & _
            " AND strAIRSnumber = :airsnumber " & _
            " and strUpdateStatus <> 'H' "

            Dim parameter As New OracleParameter("airsnumber", airsNumber.DbFormattedString)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
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
            ' -- Commit transaction

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            ' 1. Update APBHeaderData
            queryList.Add( _
                " UPDATE AIRBRANCH.APBHEADERDATA " & _
                " SET STROPERATIONALSTATUS = :operationalStatus, " & _
                "  DATSHUTDOWNDATE        = :shutdownDate, " & _
                "  STRCOMMENTS            = :comments, " & _
                "  STRMODIFINGLOCATION    = :fromLocation, " & _
                "  STRMODIFINGPERSON      = :modifiedBy, " & _
                "  DATMODIFINGDATE        = SYSDATE " & _
                " WHERE STRAIRSNUMBER      = :airsNumber " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("operationalStatus", "X"), _
                New OracleParameter("shutdownDate", shutdownDate), _
                New OracleParameter("comments", comments), _
                New OracleParameter("fromLocation", Convert.ToInt32(fromLocation)), _
                New OracleParameter("modifiedBy", UserGCode), _
                New OracleParameter("airsNumber", airsNumber.DbFormattedString) _
            })

            ' 2. Update APBAirProgramPollutants
            queryList.Add( _
                " UPDATE AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " & _
                " SET STRCOMPLIANCESTATUS = :complianceStatus, " & _
                "  STRMODIFINGPERSON     = :modifiedBy, " & _
                "  DATMODIFINGDATE       = SYSDATE, " & _
                "  STROPERATIONALSTATUS  = :operationalStatus " & _
                " WHERE STRAIRSNUMBER     = :airsNumber " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("complianceStatus", "9"), _
                New OracleParameter("modifiedBy", UserGCode), _
                New OracleParameter("operationalStatus", "X"), _
                New OracleParameter("airsNumber", airsNumber.DbFormattedString) _
            })

            ' 3. Update EIS_FacilitySite
            queryList.Add( _
                " UPDATE AIRBRANCH.EIS_FACILITYSITE " & _
                " SET STRFACILITYSITESTATUSCODE = :statusCode, " & _
                "  STRFACILITYSITECOMMENT      = :comments, " & _
                "  UPDATEUSER                  = :modifiedBy, " & _
                "  UPDATEDATETIME              = SYSDATE " & _
                " WHERE FACILITYSITEID          = :airsNumber " _
            )
            parametersList.Add(New OracleParameter() { _
                New OracleParameter("statusCode", "PS"), _
                New OracleParameter("comments", "Facility shut down by permitting action."), _
                New OracleParameter("modifiedBy", UserGCode), _
                New OracleParameter("airsNumber", airsNumber.DbFormattedString) _
            })

            ' 4. Revoke all open permits
            queryList.Add( _
                " UPDATE AIRBRANCH.APBISSUEDPERMIT " & _
                " SET DATREVOKED         = :shutdownDate, " & _
                "   UPDATEDATE         = SYSDATE, " & _
                "   UPDATEDBY          = :modifiedBy, " & _
                "   ACTIVE             = :active " & _
                " WHERE STRAIRSNUMBER = :airsnumber " & _
                " AND ACTIVE = 1 " _
            )

            parametersList.Add(New OracleParameter() { _
                New OracleParameter("shutdownDate", shutdownDate), _
                New OracleParameter("modifiedBy", UserGCode), _
                New OracleParameter("active", 0), _
                New OracleParameter("airsnumber", airsNumber.ShortString) _
            })

            Return DB.RunCommand(queryList, parametersList)
        End Function

        ''' <summary>
        ''' Completely remove a facility (AIRS number) from the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to delete</param>
        ''' <returns>True if successful; otherwise false</returns>
        Public Function DeleteFacility(ByVal airsNumber As ApbFacilityId) As Boolean
            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            Dim parameter As OracleParameter() = {New OracleParameter("airsnumber", airsNumber.DbFormattedString)}

            Dim query As String = "Delete AIRBRANCH.SSCPInspectionsRequired " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.SSCPDistrictResponsible " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBAirProgramPollutants " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBContactInformation " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBSupplamentalData " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBHeaderData " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBFacilityInformation " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.AFSFacilityData " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.AFSAIRPollutantData " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            query = "Delete AIRBRANCH.APBMasterAIRS " & _
            " where strAIRSNumber = :airsnumber"
            queryList.Add(query)
            parametersList.Add(parameter)

            Return DB.RunCommand(queryList, parametersList)
        End Function

        Public Function UpdateDataAtEPA(ByVal airsnumber As ApbFacilityId) As Boolean
            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameter As OracleParameter() = {New OracleParameter("airsnumber", airsnumber.DbFormattedString)}

            Dim query As String = " UPDATE AIRBRANCH.APBFACILITYINFORMATION " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            query = " UPDATE AIRBRANCH.APBHEADERDATA " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            query = " UPDATE AIRBRANCH.SSCPITEMMASTER " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            query = " UPDATE AIRBRANCH.SSCP_AUDITEDENFORCEMENT " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            query = " UPDATE AIRBRANCH.SSCPFCEMASTER " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            query = " UPDATE AIRBRANCH.APBSUPPLAMENTALDATA " & _
            " SET ICIS_STATUSIND = 'U' " & _
            " WHERE STRAIRSNUMBER = :airsnumber "
            queryList.Add(query)
            parametersList.Add(parameter)

            Return DB.RunCommand(queryList, parametersList)
        End Function

#End Region

    End Module
End Namespace