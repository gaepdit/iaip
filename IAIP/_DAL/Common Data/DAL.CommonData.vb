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

        Public Function GetIaipAccountRoles() As DataTable
            Dim query As String = "SELECT NUMACCOUNTCODE AS ""AccountCode"", STRACCOUNTDESC AS " &
                "  ""AccountDescription"", NUMBRANCHCODE AS ""BranchCode"", " &
            "  NUMPROGRAMCODE AS ""ProgramCode"", NUMUNITCODE AS ""UnitCode"" " &
            "FROM AIRBRANCH.LOOKUPIAIPACCOUNTS " &
            "ORDER BY STRACCOUNTDESC"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
