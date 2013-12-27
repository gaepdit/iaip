Imports Oracle.DataAccess.Client
Imports JohnGaltProject.Apb

Namespace DAL
    Module FacilityInfo

        Public Function GetFacilityNameByAirs(ByVal id As String) As String
            If Not Apb.Facility.NormalizeAirsNumber(id, True) Then Return Nothing

            Dim query As String = "SELECT STRFACILITYNAME " & _
                " FROM AIRBRANCH.APBFACILITYINFORMATION " & _
                " WHERE STRAIRSNUMBER = :pId"
            Dim parameter As New OracleParameter("pId", id)
            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        Public Function GetFacilityInfoByAirsAsDataRow(ByVal id As String) As DataRow
            If Not Apb.Facility.NormalizeAirsNumber(id, True) Then Return Nothing

            Dim query As String = "SELECT APBFACILITYINFORMATION.STRAIRSNUMBER, " & _
                "   APBFACILITYINFORMATION.STRFACILITYNAME, " & _
                "   APBFACILITYINFORMATION.STRFACILITYCITY, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTATE, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
                "   APBFACILITYINFORMATION.STRFACILITYSTREET2, " & _
                "   APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLONGITUDE, " & _
                "   APBFACILITYINFORMATION.NUMFACILITYLATITUDE, " & _
                "   APBHEADERDATA.STROPERATIONALSTATUS, " & _
                "   APBHEADERDATA.STRCLASS, " & _
                "   APBHEADERDATA.STRSICCODE, " & _
                "   APBHEADERDATA.STRFEINUMBER, " & _
                "   APBHEADERDATA.STRPLANTDESCRIPTION, " & _
                "   APBHEADERDATA.STRNAICSCODE, " & _
                "   LOOKUPCOUNTYINFORMATION.STRCOUNTYNAME " & _
                " FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                " LEFT JOIN " & DBNameSpace & ".APBHEADERDATA " & _
                " ON APBFACILITYINFORMATION.STRAIRSNUMBER    = APBHEADERDATA.STRAIRSNUMBER " & _
                " LEFT JOIN " & DBNameSpace & ".LOOKUPCOUNTYINFORMATION " & _
                " ON SUBSTR(APBFACILITYINFORMATION.STRAIRSNUMBER, 5, 3) = LOOKUPCOUNTYINFORMATION.STRCOUNTYCODE " & _
                " WHERE APBFACILITYINFORMATION.STRAIRSNUMBER = :pId "

            Dim parameter As New OracleParameter("pId", id)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        Public Function GetFacilityInfoByAirs(ByVal id As String) As Facility
            Dim dataRow As DataRow = GetFacilityInfoByAirsAsDataRow(id)
            Dim facility As New Facility

            FillFacilityInfoFromDataRow(dataRow, facility)
            Return facility
        End Function


        Private Sub FillFacilityInfoFromDataRow(ByVal row As DataRow, ByRef facility As Facility)
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
                .AirsNumber = row("STRAIRSNUMBER")
                .Classification = DB.GetNullable(Of String)(row("STRCLASS"))
                .Description = DB.GetNullable(Of String)(row("STRPLANTDESCRIPTION"))
                .FacilityLocation = location
                .Fein = DB.GetNullable(Of String)(row("STRFEINUMBER"))
                .Naics = DB.GetNullable(Of String)(row("STRNAICSCODE"))
                .Name = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
                .OperationalStatus = DB.GetNullable(Of String)(row("STROPERATIONALSTATUS"))
                .Sic = DB.GetNullable(Of String)(row("STRSICCODE"))
            End With
        End Sub

    End Module
End Namespace