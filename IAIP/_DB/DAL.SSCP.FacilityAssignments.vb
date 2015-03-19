Imports Oracle.DataAccess.Client
Imports Iaip.Apb.SSCP

Namespace DAL.SSCP

    Module FacilityAssignments

        Public Function FacilityAssignmentExists(ByVal airsNumber As String, ByVal targetYear As Integer) As Boolean
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
                Return False
            Else
                Return FacilityAssignmentExists(CType(airsNumber, Apb.ApbFacilityId), targetYear)
            End If
        End Function

        Public Function FacilityAssignmentExists(ByVal airsNumber As Apb.ApbFacilityId, ByVal targetYear As Integer) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                " WHERE RowNum = 1 " & _
                " AND INTYEAR = :year " & _
                " AND STRAIRSNUMBER = :airs "

            Dim parameters As OracleParameter() = New OracleParameter() { _
                New OracleParameter("year", targetYear), _
                New OracleParameter("airs", airsNumber.DbFormattedString) _
            }

            Dim result As String = DB.GetSingleValue(Of String)(query, parameters)
            Return Convert.ToBoolean(result)
        End Function

        Public Function AssignmentYearExists(ByVal targetYear As Integer) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                " WHERE RowNum = 1 " & _
                " AND INTYEAR = :year "
            Dim parameter As New OracleParameter("year", targetYear)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function DeleteAssignmentYear(ByVal targetYear As Integer) As Boolean
            Dim query As String = " DELETE FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                " WHERE INTYEAR = :year "
            Dim parameter As New OracleParameter("year", targetYear)

            Return DB.RunCommand(query, parameter)
        End Function

        Public Function CopyAssignmentYear(ByVal oldYear As Integer, ByVal targetYear As Integer) As Integer
            ' I realize this Function is horrible and should be moved into a Stored Procedure,
            ' but the expected number of times this Function will be called in the life of this 
            ' application is approximately one (plus or minus one). It is not worth the extra
            ' effort to do it right at this time. At least it will be relatively easy to redo 
            ' this in the future. -- Doug

            Dim recordsInserted As Integer = 0

            Dim query1 As String = " SELECT   STRAIRSNUMBER " & _
                "  FROM SSCPINSPECTIONSREQUIRED " & _
                "  WHERE INTYEAR = :oldYear " & _
                "  ORDER BY STRAIRSNUMBER "
            Dim parameter1 As New OracleParameter("oldYear", oldYear)
            Dim dataTable As DataTable = DB.GetDataTable(query1, parameter1)

            If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
                For Each row As DataRow In dataTable.Rows
                    Dim airsNumberString As String = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                    If Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumberString) AndAlso _
                    Not FacilityAssignmentExists(airsNumberString, targetYear) Then

                        Dim query2 As String = "INSERT " & _
                            "  INTO AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                            "    ( " & _
                            "      NUMKEY " & _
                            "    , STRAIRSNUMBER " & _
                            "    , INTYEAR " & _
                            "    , NUMSSCPENGINEER " & _
                            "    , NUMSSCPUNIT " & _
                            "    , STRINSPECTIONREQUIRED " & _
                            "    , STRFCEREQUIRED " & _
                            "    , STRASSIGNINGMANAGER " & _
                            "    , DATASSIGNINGDATE " & _
                            "    ) " & _
                            " SELECT " & _
                            "    (SELECT MAX(NUMKEY) + 1 FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                            "    ) AS NEWKEY " & _
                            "  , STRAIRSNUMBER " & _
                            "  , :targetyear " & _
                            "  , NUMSSCPENGINEER " & _
                            "  , NUMSSCPUNIT " & _
                            "  , STRINSPECTIONREQUIRED " & _
                            "  , STRFCEREQUIRED " & _
                            "  , STRASSIGNINGMANAGER " & _
                            "  , DATASSIGNINGDATE " & _
                            "  FROM AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
                            "  WHERE INTYEAR       = :oldyear " & _
                            "    AND STRAIRSNUMBER = :airsnumber "

                        Dim parameters2 As OracleParameter() = New OracleParameter() { _
                            New OracleParameter("targetyear", targetYear) _
                            , New OracleParameter("oldyear", oldYear) _
                            , New OracleParameter("airsnumber", airsNumberString) _
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
