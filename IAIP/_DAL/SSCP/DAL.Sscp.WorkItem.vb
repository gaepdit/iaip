Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sscp
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

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCPITEMMASTER " & _
                " WHERE RowNum = 1 " & _
                " AND STRTRACKINGNUMBER = :id "
            Dim parameter As New OracleParameter("id", trackingNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Returns an SSCP work item for a given tracking number.
        ''' </summary>
        ''' <param name="trackingNumber">The tracking number of the work item to return.</param>
        ''' <returns>An SSCP work item.</returns>
        Public Function GetWorkItem(trackingNumber As String) As WorkItem
            If trackingNumber = "" OrElse Not Integer.TryParse(trackingNumber, Nothing) Then Return Nothing
            Dim query As String =
                "SELECT STRTRACKINGNUMBER , STRAIRSNUMBER , DATRECEIVEDDATE , " &
                "  STREVENTTYPE , STRRESPONSIBLESTAFF , DATCOMPLETEDATE , " &
                "  STRDELETE , DATACKNOLEDGMENTLETTERSENT " &
                "FROM SSCPITEMMASTER " &
                "WHERE STRTRACKINGNUMBER = :trackingNumber"
            Dim parameter As New OracleParameter("trackingNumber", trackingNumber)
            Return ParseWorkItemFromDataRow(DB.GetDataRow(query, parameter))
        End Function

        ''' <summary>
        ''' Returns a list of SSCP work items for a given facility.
        ''' </summary>
        ''' <param name="airs">The Facility ID.</param>
        ''' <returns>A List of SSCP Work Items</returns>
        ''' <remarks></remarks>
        Public Function GetWorkItemList(airs As Apb.ApbFacilityId) As List(Of WorkItem)
            Dim list As New List(Of WorkItem)
            Dim dt As DataTable = GetWorkItemDataTable(airs)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    list.Add(ParseWorkItemFromDataRow(row))
                Next
            End If

            Return list
        End Function

        ''' <summary>
        ''' Returns a DataTable of SSCP work items for a given facility.
        ''' </summary>
        ''' <param name="airs">The Facility ID.</param>
        ''' <returns>A DataTable of SSCP Work Items</returns>
        ''' <remarks></remarks>
        Public Function GetWorkItemDataTable(airs As Apb.ApbFacilityId) As DataTable
            Dim query As String =
                "SELECT STRTRACKINGNUMBER , STRAIRSNUMBER , DATRECEIVEDDATE , " &
                "  STREVENTTYPE , STRRESPONSIBLESTAFF , DATCOMPLETEDATE , " &
                "  STRDELETE , DATACKNOLEDGMENTLETTERSENT " &
                "FROM SSCPITEMMASTER " &
                "WHERE STRAIRSNUMBER = :airs AND STRDELETE IS NULL"
            Dim parameter As New OracleParameter("airs", airs.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a DataTable of a specific SSCP work type for a given facility.
        ''' </summary>
        ''' <param name="airs">The Facility ID.</param>
        ''' <param name="eventType">The work type desired.</param>
        ''' <returns>A DataTable of SSCP Work Items of a specific work type.</returns>
        ''' <remarks></remarks>
        Public Function GetWorkItemDataTable(airs As Apb.ApbFacilityId, eventType As WorkItem.WorkItemEventType) As DataTable
            Dim query As String =
                "SELECT STRTRACKINGNUMBER , STRAIRSNUMBER , DATRECEIVEDDATE , " &
                "  STREVENTTYPE , STRRESPONSIBLESTAFF , DATCOMPLETEDATE , " &
                "  STRDELETE , DATACKNOLEDGMENTLETTERSENT " &
                "FROM SSCPITEMMASTER " &
                "WHERE STRAIRSNUMBER = :airs AND STRDELETE IS NULL AND " &
                "  STREVENTTYPE = :eventtype"
            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("eventtype", WorkItem.EventTypeDbCodes(eventType))
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        ''' <summary>
        ''' Parses an SSCP work item using the data in a DataRow
        ''' </summary>
        ''' <param name="row">The DataRow to parse.</param>
        ''' <returns>An SSCP work item parsed from the DataRow</returns>
        Public Function ParseWorkItemFromDataRow(row As DataRow) As WorkItem
            If IsNothing(row) Then Return Nothing
            Dim workItem As New WorkItem
            With workItem
                .SscpTrackingNumber = row("STRTRACKINGNUMBER")
                .Facility = GetFacility(row("STRAIRSNUMBER"))
                .DateReceived = row("DATRECEIVEDDATE")
                .StaffResponsible = GetStaff(row("STRRESPONSIBLESTAFF"))
                .DateComplete = DB.GetNullable(Of Date)(row("DATCOMPLETEDATE"))
                .DeletedDbCode = row("STRDELETE")
                .DateAcknowledgmentLetterSent = DB.GetNullable(Of Date)(row("DATACKNOLEDGMENTLETTERSENT"))
                .EventTypeDbCode = DB.GetNullable(Of String)(row("STREVENTTYPE"))
            End With
            Return workItem
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

            Dim query As String = "SELECT STRREFERENCENUMBER " & _
                " FROM AIRBRANCH.SSCPTESTREPORTS " & _
                " WHERE RowNum = 1 " & _
                " AND STRTRACKINGNUMBER = :id "
            Dim parameter As New OracleParameter("id", trackingNumber)

            refNum = DB.GetSingleValue(Of String)(query, parameter)

            If refNum = "" Then
                Return False
            Else
                Return True
            End If
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

            Dim query As String = "SELECT INSPECTION_ID " & _
            "  FROM AIRBRANCH.GEOS_INSPECTIONS_XREF " & _
            "  WHERE STRTRACKINGNUMBER = :id "
            Dim parameter As New OracleParameter("id", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return result
        End Function

#End Region

    End Module

End Namespace
