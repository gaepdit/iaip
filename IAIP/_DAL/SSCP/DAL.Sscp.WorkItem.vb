Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sscp

Namespace DAL.Sscp

    Module WorkItemData

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number exists.
        ''' </summary>
        ''' <param name="id">The SSCP work item tracking number to test.</param>
        ''' <returns>Returns True if the work item exists; otherwise, returns False.</returns>
        Public Function WorkItemExists(ByVal id As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCPITEMMASTER " & _
                " WHERE RowNum = 1 " & _
                " AND STRTRACKINGNUMBER = :id "
            Dim parameter As New OracleParameter("id", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Tests whether an SSCP work item tracking number refers to a stack test. 
        ''' Optionally returns the ISMP reference number associated with the SSCP work item reference number.
        ''' </summary>
        ''' <param name="id">The SSCP work item reference number to test.</param>
        ''' <param name="refNum">When this function returns, contains the ISMP reference number associated with the SSCP work 
        ''' item reference number if one exists. Otherwise, contains an empty string.</param>
        ''' <returns>Returns True if the SSCP work item reference number refers to a stack test; otherwise, returns False.</returns>
        Public Function WorkItemIsAStackTest(ByVal id As String, Optional ByRef refNum As String = "") As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT STRREFERENCENUMBER " & _
                " FROM AIRBRANCH.SSCPTESTREPORTS " & _
                " WHERE RowNum = 1 " & _
                " AND STRTRACKINGNUMBER = :id "
            Dim parameter As New OracleParameter("id", id)

            refNum = DB.GetSingleValue(Of String)(query, parameter)

            If refNum = "" Then
                Return False
            Else
                Return True
            End If
        End Function

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

        ''' <summary>
        ''' Retrieves the facility ID associated with an FCE
        ''' </summary>
        ''' <param name="id">The FCE tracking number.</param>
        ''' <returns>A facility ID</returns>
        Public Function GetFacilityIdByFceId(fceNumber As String) As Apb.ApbFacilityId
            If fceNumber = "" OrElse Not Integer.TryParse(fceNumber, Nothing) Then Return Nothing

            Dim query As String = "SELECT STRAIRSNUMBER FROM SSCPFCEMASTER WHERE STRFCENUMBER = :fceNumber"
            Dim parameter As New OracleParameter("fceNumber", fceNumber)

            Return New Apb.ApbFacilityId(DB.GetSingleValue(Of String)(query, parameter))
        End Function

    End Module

End Namespace
