Namespace DAL
    Module State

        Public Function GetCountiesAsDataTable() As DataTable
            Dim query As String = "SELECT STRCOUNTYCODE AS CountyCode, 
                STRCOUNTYNAME AS County
                FROM LOOKUPCOUNTYINFORMATION
                ORDER BY STRCOUNTYNAME"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetFacilityCitiesAsDataTable() As DataTable
            Dim query As String = "SELECT CITY FROM VW_CITIES ORDER BY CITY"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetDistrictOffices() As DataTable
            Dim query As String = "SELECT STRDISTRICTCODE AS DistrictCode, 
                STRDISTRICTNAME AS DistrictName, STRDISTRICTMANAGER AS Manager
                FROM LOOKUPDISTRICTS ORDER BY DistrictName"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetDistrictCountyAssignments() As DataTable
            Dim query As String = "SELECT STRDISTRICTCOUNTY AS CountyCode, 
                STRDISTRICTCODE AS DistrictCode
                FROM LOOKUPDISTRICTINFORMATION"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
