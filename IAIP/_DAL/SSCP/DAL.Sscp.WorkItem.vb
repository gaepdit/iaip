Imports System.Data.SqlClient
Imports Iaip.Apb.Sscp
Imports Iaip.Apb.Sscp.WorkItem
Imports System.Runtime.InteropServices
Imports System.Collections.Generic

Namespace DAL.Sscp

    Module WorkItemData

#Region " Generic Work Items "

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number exists.
        ''' </summary>
        ''' <param name="trackingNumber">The SSCP work item tracking number to test.</param>
        ''' <returns>Returns True if the work item exists; otherwise, returns False.</returns>
        Public Function WorkItemExists(ByVal trackingNumber As String) As Boolean
            If trackingNumber = "" OrElse Not Integer.TryParse(trackingNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SSCPITEMMASTER " &
                " WHERE RowNum = 1 " &
                " AND STRTRACKINGNUMBER = @id "
            Dim parameter As New SqlParameter("@id", trackingNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

#End Region

#Region " Utilities "

        Private Function QueryFilter(
                airs As Apb.ApbFacilityId,
                staffId As String,
                complete As WorkItemComplete,
                deleted As WorkItemDeleted
                ) As String
            Dim clause As String = ""

            If airs IsNot Nothing Then clause &= " AND STRAIRSNUMBER = @airs "

            If Not String.IsNullOrEmpty(staffId) Then clause &= " AND STRRESPONSIBLESTAFF = @staffId "

            If complete = WorkItemComplete.Complete Then
                clause &= " AND DATCOMPLETEDATE IS NOT NULL "
            ElseIf complete = WorkItemComplete.NotComplete Then
                clause &= " AND DATCOMPLETEDATE IS NULL "
            End If

            If deleted = WorkItemDeleted.Deleted Then
                clause &= " AND STRDELETE IS NOT NULL "
            ElseIf deleted = WorkItemDeleted.NotDeleted Then
                clause &= " AND STRDELETE IS NULL "
            End If

            Return clause
        End Function

#End Region

#Region " Stack Tests "

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number refers to a stack test. If it is, the ISMP reference number associated 
        ''' with the SSCP work item reference number is stored in the refNum output parameter.
        ''' </summary>
        ''' <param name="trackingNumber">The SSCP work item reference number to test.</param>
        ''' <param name="refNum">When this function returns, contains the ISMP reference number associated with the SSCP work 
        ''' item reference number if one exists. Otherwise, contains an empty string.</param>
        ''' <returns>Returns True if the SSCP work item reference number refers to a stack test; otherwise, returns False.</returns>
        Public Function TryGetRefNumForWorkItem(ByVal trackingNumber As String, <OutAttribute> Optional ByRef refNum As String = "") As Boolean
            If trackingNumber = "" OrElse Not Integer.TryParse(trackingNumber, Nothing) Then Return False

            Dim query As String = "SELECT STRREFERENCENUMBER " &
                " FROM SSCPTESTREPORTS " &
                " WHERE RowNum = 1 " &
                " AND STRTRACKINGNUMBER = @id "
            Dim parameter As New SqlParameter("@id", trackingNumber)

            refNum = DB.GetSingleValue(Of String)(query, parameter)

            If refNum = "" Then
                Return False
            Else
                Return True
            End If
        End Function

        ''' <summary>
        ''' Returns a DataTable of a SSCP inspections for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of SSCP inspections.</returns>
        ''' <remarks></remarks>
        Public Function GetCompStackTestDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_STACKTESTS " &
                " WHERE TRUNC(DATRECEIVEDFROMFACILITY) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Inspections "

        ''' <summary>
        ''' Returns the GEOS ID for an inspection if one exists; otherwise, returns an empty string
        ''' </summary>
        ''' <param name="id">The IAIP tracking number for the inspection</param>
        ''' <returns>GEOS Inspection ID</returns>
        Public Function GetGeosInspectionId(ByVal id As String) As String
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return ""

            Dim query As String = "SELECT INSPECTION_ID " &
            "  FROM GEOS_INSPECTIONS_XREF " &
            "  WHERE STRTRACKINGNUMBER = @id "
            Dim parameter As New SqlParameter("@id", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return result
        End Function

        ''' <summary>
        ''' Returns a DataTable of a SSCP inspections for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of SSCP inspections.</returns>
        ''' <remarks></remarks>
        Public Function GetInspectionDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_INSPECTIONS " &
                " WHERE TRUNC(DATINSPECTIONDATESTART) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " ACCs "

        ''' <summary>
        ''' Returns a DataTable of ACCs for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of ACCs.</returns>
        ''' <remarks></remarks>
        Public Function GetAccDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_ACCS " &
                " WHERE TRUNC(DATRECEIVEDDATE) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Notification "

        ''' <summary>
        ''' Returns a DataTable of a SSCP notifications for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of SSCP notifications.</returns>
        ''' <remarks></remarks>
        Public Function GetCompNotificationsDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_NOTIFICATIONS " &
                " WHERE TRUNC(DATRECEIVEDDATE) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Reports "

        ''' <summary>
        ''' Returns a DataTable of a SSCP reports for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of SSCP reports.</returns>
        ''' <remarks></remarks>
        Public Function GetCompReportsDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_REPORTS " &
                " WHERE TRUNC(DATRECEIVEDDATE) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " RMP Inspections "

        ''' <summary>
        ''' Returns a DataTable of a RMP inspections for a given facility.
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <param name="complete">Whether to filter for complete items or not. Defaults to all items.</param>
        ''' <param name="deleted">Whether to filter for deleted items or not. Defaults to items that are not deleted.</param>
        ''' <returns>A DataTable of RMP inspections.</returns>
        ''' <remarks></remarks>
        Public Function GetRmpInspectionDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing,
                Optional complete As WorkItemComplete = WorkItemComplete.All,
                Optional deleted As WorkItemDeleted = WorkItemDeleted.NotDeleted
                                                      ) As DataTable

            Dim query As String =
                "SELECT * FROM VW_SSCP_RMPINSPECTIONS " &
                " WHERE TRUNC(DATINSPECTIONDATESTART) BETWEEN @datestart AND @dateend "
            query &= QueryFilter(airs, staffId, complete, deleted)
            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Enums "

        Public Enum WorkItemComplete
            All
            Complete
            NotComplete
        End Enum

        Public Enum WorkItemDeleted
            All
            Deleted
            NotDeleted
        End Enum

#End Region

    End Module

End Namespace
