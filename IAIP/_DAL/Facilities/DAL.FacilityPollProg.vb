Imports Iaip.Apb.Facilities
Imports Oracle.ManagedDataAccess.Client

Namespace DAL

    Module PollutantsPrograms

        Public Function GetFacilityPollutants(airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = "SELECT DISTINCT(poll.STRPOLLUTANTKEY), " &
                "  lkpoll.STRPOLLUTANTDESCRIPTION " &
                "FROM AIRBRANCH.APBAIRPROGRAMPOLLUTANTS poll " &
                "INNER JOIN AIRBRANCH.LOOKUPPOLLUTANTS lkpoll ON " &
                "  poll.STRPOLLUTANTKEY = lkpoll.STRPOLLUTANTCODE " &
                "WHERE poll.STRAIRSNUMBER = :airsNumber"
            Dim parameter As New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("STRPOLLUTANTKEY")}
            Return dt
        End Function

        Public Function GetFacilityAirPrograms(airsNumber As Apb.ApbFacilityId) As AirProgram
            Dim query As String = "SELECT STRAIRPROGRAMCODES FROM AIRBRANCH.APBHEADERDATA WHERE STRAIRSNUMBER = :airsNumber"
            Dim parameter As New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            Dim apc As String = DB.GetSingleValue(Of String)(query, parameter)

            If apc Is Nothing Then
                Return AirProgram.None
            Else
                Return ConvertBitFieldToEnum(Of AirProgram)(apc)
            End If
        End Function

        Public Function GetFacilityAirProgramsAsDataTable(airsNumber As Apb.ApbFacilityId) As DataTable
            Dim ap As AirProgram = GetFacilityAirPrograms(airsNumber)

            If Convert.ToInt32(ap) = 0 Then
                Return Nothing
            End If

            Dim dt As New DataTable
            dt.Columns.Add("AirProgramKey")
            dt.Columns.Add("AirProgramDesc")
            Dim dr As DataRow
            For Each f As AirProgram In ap.GetUniqueFlags
                dr = dt.NewRow()
                dr("AirProgramKey") = f.ToString
                dr("AirProgramDesc") = f.GetDescription
                dt.Rows.Add(dr)
            Next
            Return dt
        End Function

    End Module

End Namespace
