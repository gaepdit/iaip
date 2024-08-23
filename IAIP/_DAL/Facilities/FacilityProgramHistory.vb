Imports Microsoft.Data.SqlClient
Imports Iaip.Apb.Facilities

Namespace DAL
    Module FacilityProgramHistory

        Public Function SearchHistoricalAirProgramStatus(beginSearchDate As Date, endSearchDate As Date, program As AirPrograms) As DataTable
            Dim airProgramNumber As Integer = AirProgramBitPosition(program)

            Dim params As SqlParameter() = {
                New SqlParameter("@beginSearchDate", beginSearchDate),
                New SqlParameter("@endSearchDate", endSearchDate),
                New SqlParameter("@airProgramNumber", airProgramNumber)
            }

            Return DB.SPGetDataTable("iaip_facility.SearchHistoryAirProgram", params)
        End Function

        Public Function SearchHistoricalFacilityClassificationStatus(beginSearchDate As Date, endSearchDate As Date, [class] As FacilityClassification) As DataTable
            Dim params As SqlParameter() = {
                New SqlParameter("@beginSearchDate", beginSearchDate),
                New SqlParameter("@endSearchDate", endSearchDate),
                New SqlParameter("@class", [class].ToString)
            }

            Return DB.SPGetDataTable("iaip_facility.SearchHistoryClass", params)
        End Function

    End Module
End Namespace
