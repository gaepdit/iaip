Imports Oracle.DataAccess.Client
Imports Iaip.Apb

Namespace DAL
    Module FacilityHeaderData

#Region " Validate SIC/NAICS "

        ''' <summary>
        ''' Returns whether an SIC Code exists in the database lookup table
        ''' </summary>
        ''' <param name="sicCode">The SIC Code to test.</param>
        ''' <returns>True if the SIC Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgements about appropriateness of SIC Code otherwise.</remarks>
        Public Function SicCodeExists(ByVal sicCode As String) As Boolean
            If sicCode Is Nothing OrElse String.IsNullOrEmpty(sicCode) Then Return False

            ' Valid SIC Codes are one to four digits
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{1,4}$")
            If Not rgx.IsMatch(sicCode) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".LOOKUPSICCODES " & _
                " WHERE RowNum = 1 " & _
                " AND STRSICCODE = :pId "
            Dim parameter As New OracleParameter("pId", sicCode)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        ''' <summary>
        ''' Returns whether an NAICS Code exists in the database lookup table
        ''' </summary>
        ''' <param name="naicsCode">The NAICS Code to test.</param>
        ''' <returns>True if the NAICS Code exists; otherwise false.</returns>
        ''' <remarks>Does not make any judgements about appropriateness of NAICS Code otherwise.</remarks>
        Public Function NaicsCodeExists(ByVal naicsCode As String) As Boolean
            If naicsCode Is Nothing OrElse String.IsNullOrEmpty(naicsCode) Then Return False

            ' Valid NAICS Codes are two to six digits
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{2,6}$")
            If Not rgx.IsMatch(naicsCode) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".EILOOKUPNAICS " & _
                " WHERE RowNum = 1 " & _
                " AND STRNAICSCODE = :pId "
            Dim parameter As New OracleParameter("pId", naicsCode)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

#End Region

#Region " Read "

        Public Function GetFacilityHeaderDataAsDataRow(ByVal airsNumber As String) As DataRow
            If Not Facility.NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = " SELECT " & _
                "  Null AS STRKEY, " & _
                "  USERNAME, " & _
                "  MODIFINGDATE, " & _
                "  STRMODIFINGLOCATION, " & _
                "  STROPERATIONALSTATUS, " & _
                "  STRCLASS, " & _
                "  STRCOMMENTS, " & _
                "  DATSTARTUPDATE, " & _
                "  DATSHUTDOWNDATE, " & _
                "  STRPLANTDESCRIPTION, " & _
                "  STRSICCODE, " & _
                "  STRNAICSCODE, " & _
                "  STRRMPID, " & _
                "  STRAIRPROGRAMCODES, " & _
                "  STRSTATEPROGRAMCODES, " & _
                "  STRATTAINMENTSTATUS " & _
                " FROM AIRBRANCH.VW_APBFACILITYHEADER " & _
                " WHERE STRAIRSNUMBER = :pId "

            Dim parameter As New OracleParameter("pId", airsNumber)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        Public Function GetFacilityHeaderData(ByVal airsNumber As String) As Apb.FacilityHeaderData
            Dim row As DataRow = GetFacilityHeaderDataAsDataRow(airsNumber)
            Dim facilityHeaderData As New Apb.FacilityHeaderData(airsNumber)

            FillFacilityHeaderDataFromDataRow(row, facilityHeaderData)
            Return facilityHeaderData
        End Function

        Public Sub FillFacilityHeaderDataFromDataRow(ByVal row As DataRow, ByRef facilityHeaderData As Apb.FacilityHeaderData)
            With facilityHeaderData
                .OperationalStatusCode = DB.GetNullable(Of String)(row("STROPERATIONALSTATUS"))
                .ClassificationCode = DB.GetNullable(Of String)(row("STRCLASS"))
                .SicCode = DB.GetNullable(Of String)(row("STRSICCODE"))
                .ShutdownDate = DB.GetNullableDateTimeFromString(row("DATSTARTUPDATE"))
                .StartupDate = DB.GetNullableDateTimeFromString(row("DATSHUTDOWNDATE"))
                .Naics = DB.GetNullable(Of String)(row("STRNAICSCODE"))
                .RmpId = DB.GetNullable(Of String)(row("STRRMPID"))
                .FacilityDescription = DB.GetNullable(Of String)(row("STRPLANTDESCRIPTION"))
                .AirProgramsCode = DB.GetNullable(Of String)(row("STRAIRPROGRAMCODES"))
                .AirProgramClassificationsCode = DB.GetNullable(Of String)(row("STRSTATEPROGRAMCODES"))
                .NonattainmentStatusesCode = DB.GetNullable(Of String)(row("STRATTAINMENTSTATUS"))
                .HeaderUpdateComment = DB.GetNullable(Of String)(row("STRCOMMENTS"))
                .DateDataModified = DB.GetNullableDateTimeFromString(row("MODIFINGDATE"))
                .WhoModified = DB.GetNullable(Of String)(row("USERNAME"))
                .WhereModified = DB.GetNullable(Of String)(row("STRMODIFINGLOCATION"))
            End With
        End Sub

        Public Function GetFacilityHeaderDataHistoryAsDataTable(ByVal airsNumber As String) As DataTable
            If Not Facility.NormalizeAirsNumber(airsNumber, True) Then Return Nothing

            Dim query As String = " SELECT " & _
                "  STRKEY, " & _
                "  USERNAME, " & _
                "  MODIFINGDATE, " & _
                "  STRMODIFINGLOCATION, " & _
                "  STROPERATIONALSTATUS, " & _
                "  STRCLASS, " & _
                "  STRCOMMENTS, " & _
                "  DATSTARTUPDATE, " & _
                "  DATSHUTDOWNDATE, " & _
                "  STRPLANTDESCRIPTION, " & _
                "  STRSICCODE, " & _
                "  STRNAICSCODE, " & _
                "  STRRMPID, " & _
                "  STRAIRPROGRAMCODES, " & _
                "  STRSTATEPROGRAMCODES, " & _
                "  STRATTAINMENTSTATUS " & _
                " FROM AIRBRANCH.VW_HB_APBHEADERDATA " & _
                " WHERE STRAIRSNUMBER = :pId " & _
                " ORDER BY STRKEY DESC "

            Dim parameter As New OracleParameter("pId", airsNumber)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Write "

        Public Function SaveFacilityHeaderData(ByVal headerData As Apb.FacilityHeaderData) As Boolean

            Return False
        End Function

#End Region

    End Module
End Namespace