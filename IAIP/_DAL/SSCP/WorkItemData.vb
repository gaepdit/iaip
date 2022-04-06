Imports System.Data.SqlClient
Imports System.Runtime.InteropServices

Namespace DAL.Sscp

    Module WorkItemData

        ' Generic Work Items

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number exists.
        ''' </summary>
        ''' <param name="trackingNumber">The SSCP work item tracking number to test.</param>
        ''' <returns>Returns True if the work item exists; otherwise, returns False.</returns>
        Public Function WorkItemExists(trackingNumber As Integer) As Boolean
            If trackingNumber = 0 Then Return False

            Dim query As String = "SELECT 1 " &
                " FROM SSCPITEMMASTER " &
                " WHERE STRTRACKINGNUMBER = @trackingNumber "

            Dim parameter As New SqlParameter("@trackingNumber", trackingNumber)

            Return DB.ValueExists(query, parameter)
        End Function

        Public Function GetWorkItemBasics(trackingNumber As Integer) As DataRow
            Dim query As String = "SELECT
                    im.STRTRACKINGNUMBER AS [Tracking #],
                    lk.STRACTIVITYNAME   AS [Type],
                    im.DATRECEIVEDDATE   AS [Event Date],
                    im.STRAIRSNUMBER     AS [AIRS #]
                FROM SSCPITEMMASTER im
                    INNER JOIN LOOKUPCOMPLIANCEACTIVITIES AS lk
                        ON im.STREVENTTYPE = lk.STRACTIVITYTYPE
                WHERE im.STRTRACKINGNUMBER = @trackingNumber"

            Dim parameter As New SqlParameter("@trackingNumber", trackingNumber)

            Return DB.GetDataRow(query, parameter)
        End Function

        ' Stack Tests

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number refers to a stack test. If it is, the ISMP reference number associated 
        ''' with the SSCP work item reference number is stored in the refNum output parameter.
        ''' </summary>
        ''' <param name="trackingNumber">The SSCP work item reference number to test.</param>
        ''' <param name="refNum">When this function returns, contains the ISMP reference number associated with the SSCP work 
        ''' item reference number if one exists. Otherwise, contains an empty string.</param>
        ''' <returns>Returns True if the SSCP work item reference number refers to a stack test; otherwise, returns False.</returns>
        Public Function TryGetRefNumForWorkItem(trackingNumber As Integer, <Out> Optional ByRef refNum As String = "") As Boolean
            If trackingNumber = 0 Then Return False

            Dim query As String = "SELECT STRREFERENCENUMBER " &
                " FROM SSCPTESTREPORTS " &
                " WHERE STRTRACKINGNUMBER = @id " &
                " AND STRREFERENCENUMBER <> 'N/A' "

            Dim parameter As New SqlParameter("@id", trackingNumber)

            refNum = DB.GetString(query, parameter)

            If refNum = "" Then
                Return False
            Else
                Return True
            End If
        End Function

        ' Notification

        ''' <summary>
        ''' Returns a DataTable of a SSCP notification types
        ''' </summary>
        ''' <returns></returns>
        Public Function GetSscpNotificationTypes() As DataTable
            Dim query As String = "SELECT STRNOTIFICATIONKEY, STRNOTIFICATIONDESC FROM LOOKUPSSCPNOTIFICATIONS"

            Dim dt As DataTable = DB.GetDataTable(query)

            dt.PrimaryKey = {dt.Columns("STRNOTIFICATIONKEY")}

            Return dt
        End Function

    End Module

End Namespace
