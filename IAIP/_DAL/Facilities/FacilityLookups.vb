Namespace DAL
    Module FacilityLookups

        Public Function GetFacilityOwnershipTypes() As DataTable
            Return DB.GetDataTable("select Code, Description, ICIS_STATUS_FLAG from LK_ICIS_FacilityOwnershipType")
        End Function

    End Module
End Namespace
