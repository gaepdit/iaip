Imports System.Data.SqlClient
Imports EpdIt

Namespace DAL.Sscp

    Module FacilityAssignments

        Public Function FacilityAssignmentExists(ByVal airsNumber As String, ByVal targetYear As Integer) As Boolean
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
                Return False
            Else
                Return FacilityAssignmentExists(CType(airsNumber, Apb.ApbFacilityId), targetYear)
            End If
        End Function

        Public Function FacilityAssignmentExists(ByVal airsNumber As Apb.ApbFacilityId, ByVal targetYear As Integer) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE RowNum = 1 " &
                " AND INTYEAR = @year " &
                " AND STRAIRSNUMBER = @airs "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@year", targetYear),
                New SqlParameter("@airs", airsNumber.DbFormattedString)
            }

            Dim result As String = DB.GetSingleValue(Of String)(query, parameters)
            Return Convert.ToBoolean(result)
        End Function

        Public Function AssignmentYearExists(ByVal targetYear As Integer) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE RowNum = 1 " &
                " AND INTYEAR = @year "
            Dim parameter As New SqlParameter("@year", targetYear)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function DeleteAssignmentYear(ByVal targetYear As Integer) As Boolean
            Dim query As String = " DELETE FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE INTYEAR = @year "
            Dim parameter As New SqlParameter("@year", targetYear)

            Return DB.RunCommand(query, parameter)
        End Function

        Public Function CopyAssignmentYear(ByVal oldYear As Integer, ByVal targetYear As Integer) As Integer
            ' I realize this Function is horrible and should be moved into a Stored Procedure,
            ' but the expected number of times this Function will be called in the life of this 
            ' application is approximately one (plus or minus one). It is not worth the extra
            ' effort to do it right at this time. At least it will be relatively easy to redo 
            ' this in the future. -- Doug

            Dim recordsInserted As Integer = 0

            Dim query1 As String = " SELECT   STRAIRSNUMBER " &
                "  FROM SSCPINSPECTIONSREQUIRED " &
                "  WHERE INTYEAR = @oldYear " &
                "  ORDER BY STRAIRSNUMBER "
            Dim parameter1 As New SqlParameter("@oldYear", oldYear)
            Dim dataTable As DataTable = DB.GetDataTable(query1, parameter1)

            If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
                For Each row As DataRow In dataTable.Rows
                    Dim airsNumberString As String = DBUtilities.GetNullable(Of String)(row("STRAIRSNUMBER"))
                    If Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumberString) AndAlso
                    Not FacilityAssignmentExists(airsNumberString, targetYear) Then

                        Dim query2 As String = "INSERT " &
                            "  INTO SSCPINSPECTIONSREQUIRED " &
                            "    ( " &
                            "      NUMKEY " &
                            "    , STRAIRSNUMBER " &
                            "    , INTYEAR " &
                            "    , NUMSSCPENGINEER " &
                            "    , NUMSSCPUNIT " &
                            "    , STRINSPECTIONREQUIRED " &
                            "    , STRFCEREQUIRED " &
                            "    , STRASSIGNINGMANAGER " &
                            "    , DATASSIGNINGDATE " &
                            "    ) " &
                            " SELECT " &
                            "    (SELECT MAX(NUMKEY) + 1 FROM SSCPINSPECTIONSREQUIRED " &
                            "    ) AS NEWKEY " &
                            "  , STRAIRSNUMBER " &
                            "  , @targetyear " &
                            "  , NUMSSCPENGINEER " &
                            "  , NUMSSCPUNIT " &
                            "  , STRINSPECTIONREQUIRED " &
                            "  , STRFCEREQUIRED " &
                            "  , STRASSIGNINGMANAGER " &
                            "  , DATASSIGNINGDATE " &
                            "  FROM SSCPINSPECTIONSREQUIRED " &
                            "  WHERE INTYEAR       = @oldyear " &
                            "    AND STRAIRSNUMBER = @airsnumber "

                        Dim parameters2 As SqlParameter() = New SqlParameter() {
                            New SqlParameter("@targetyear", targetYear) _
                            , New SqlParameter("@oldyear", oldYear) _
                            , New SqlParameter("@airsnumber", airsNumberString)
                        }

                        Dim recordInserted As Integer = 0
                        DB.RunCommand(query2, parameters2, recordInserted)
                        recordsInserted = recordsInserted + recordInserted

                    End If
                Next ' row in dataTable
            End If

            Return recordsInserted
        End Function

    End Module

End Namespace
