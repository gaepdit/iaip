Imports Oracle.DataAccess.Client
Imports Iaip.Apb

Namespace DAL
    Module Facilities

        ''' <summary>
        ''' Returns whether an AIRS number already exists in the database
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to test.</param>
        ''' <returns>True if the AIRS number exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgements about state of facility otherwise.</remarks>
        Public Function AirsNumberExists(ByVal airsNumber As String) As Boolean
            If Not Facility.NormalizeAirsNumber(airsNumber, True) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".APBMasterAIRS " & _
                " WHERE RowNum = 1 " & _
                " AND strAIRSnumber = :pId "
            Dim parameter As New OracleParameter("pId", airsNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Returns the facility name for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>The facility name, or an empty string if facility AIRS number does not exist.</returns>
        Public Function GetFacilityName(ByVal airsNumber As String) As String
            If Not Facility.NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = "SELECT STRFACILITYNAME " & _
                " FROM AIRBRANCH.APBFACILITYINFORMATION " & _
                " WHERE STRAIRSNUMBER = :pId"
            Dim parameter As New OracleParameter("pId", airsNumber)
            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a Facility with basic information for a given AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to search for.</param>
        ''' <returns>A Facility with basic information, or Nothing if AIRS number does not exist.</returns>
        ''' <remarks></remarks>
        Public Function GetFacility(ByVal airsNumber As String) As Facility
            Dim row As DataRow = GetFacilityAsDataRow(airsNumber)
            Dim facility As New Facility(airsNumber)

            FillFacilityFromDataRow(row, facility)
            Return facility
        End Function

        Private Function GetFacilityAsDataRow(ByVal airsNumber As String) As DataRow
            If Not Facility.NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = "SELECT APBFACILITYINFORMATION.STRFACILITYNAME, " & _
                "   APBFACILITYINFORMATION.STRFACILITYCITY, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTATE, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET2, " & _
                "   APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLONGITUDE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLATITUDE, " & _
                "   LOOKUPCOUNTYINFORMATION.STRCOUNTYNAME " & _
                " FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                " LEFT JOIN " & DBNameSpace & ".LOOKUPCOUNTYINFORMATION " & _
                " ON SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5, 3) = LOOKUPCOUNTYINFORMATION.STRCOUNTYCODE " & _
                " WHERE APBFACILITYINFORMATION.STRAIRSNUMBER = :pId "

            Dim parameter As New OracleParameter("pId", airsNumber)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        Private Sub FillFacilityFromDataRow(ByVal row As DataRow, ByRef facility As Facility)
            Dim address As New Address
            With address
                .City = DB.GetNullable(Of String)(row("STRFACILITYCITY"))
                .Country = "United States of America"
                .PostalCode = DB.GetNullable(Of String)(row("STRFACILITYZIPCODE"))
                .State = DB.GetNullable(Of String)(row("STRFACILITYSTATE"))
                .Street = DB.GetNullable(Of String)(row("STRFACILITYSTREET1"))
                .Street2 = DB.GetNullable(Of String)(row("STRFACILITYSTREET2"))
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

    End Module
End Namespace