Namespace DAL
    Module CommonData

        Public Function GetPollutantsTable() As DataTable
            Dim query As String = "SELECT STRPOLLUTANTCODE AS ""Pollutant Code"", " &
                "  STRPOLLUTANTDESCRIPTION AS ""Pollutant"" " &
                "FROM AIRBRANCH.LOOKUPPOLLUTANTS " &
                "WHERE STRAFSCODE = 'True' " &
                "ORDER BY STRPOLLUTANTDESCRIPTION"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
