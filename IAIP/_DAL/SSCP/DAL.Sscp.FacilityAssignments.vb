Imports System.Data.SqlClient
Imports EpdIt

Namespace DAL.Sscp

    Module FacilityAssignments

        Public Function FacilityAssignmentYearExists(airsNumber As String, targetYear As Integer) As Boolean
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
                Return False
            Else
                Return FacilityAssignmentYearExists(CType(airsNumber, Apb.ApbFacilityId), targetYear)
            End If
        End Function

        Public Function FacilityAssignmentYearExists(airsNumber As Apb.ApbFacilityId, targetYear As Integer) As Boolean
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE INTYEAR = @year " &
                " AND STRAIRSNUMBER = @airs "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@year", targetYear),
                New SqlParameter("@airs", airsNumber.DbFormattedString)
            }

            Return DB.GetBoolean(query, parameters)
        End Function

        Public Function SetFacilityStaffAssignment(airsNumber As Apb.ApbFacilityId, targetYear As Integer, staffAssignee As Integer, mgrAssignor As Integer) As Boolean
            Dim SQL As String

            If FacilityAssignmentYearExists(airsNumber, targetYear) Then
                SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPEngineer = @user, " &
                    "strAssigningManager = @mgr , " &
                    "DATASSIGNINGDATE =  GETDATE()  " &
                    "where strAIRSNumber = @airs " &
                    "and intYear = @year "
            Else
                SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    " numSSCPEngineer, strAssigningManager, " &
                    " DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    " @airs, @year, " &
                    " @user, @mgr, " &
                    " GETDATE() ) "
            End If

            Dim parameters As SqlParameter() = {
                New SqlParameter("@user", staffAssignee),
                New SqlParameter("@mgr", mgrAssignor),
                New SqlParameter("@airs", airsNumber.DbFormattedString),
                New SqlParameter("@year", targetYear)
            }

            Return DB.RunCommand(SQL, parameters, forceAddNullableParameters:=True)
        End Function

        Public Function SetFacilityUnitAssignment(airsNumber As Apb.ApbFacilityId, targetYear As Integer, unitAssignee As Integer, mgrAssignor As Integer) As Boolean
            Dim SQL As String

            If FacilityAssignmentYearExists(airsNumber, targetYear) Then
                SQL = "Update SSCPInspectionsRequired set " &
                    "numSSCPUnit = @unit, " &
                    "strAssigningManager = @mgr , " &
                    "DATASSIGNINGDATE =  GETDATE()  " &
                    "where strAIRSNumber = @airs " &
                    "and intYear = @year "
            Else
                SQL = "Insert into SSCPInspectionsRequired " &
                    "(numKey, strAIRSNumber, intYear, " &
                    " numSSCPUnit, strAssigningManager, " &
                    " DATASSIGNINGDATE) " &
                    "values " &
                    "((select max(numKey) + 1 from SSCPInspectionsRequired), " &
                    " @airs, @year, " &
                    " @unit, @mgr, " &
                    " GETDATE() ) "
            End If

            Dim parameters As SqlParameter() = {
                New SqlParameter("@unit", unitAssignee),
                New SqlParameter("@mgr", mgrAssignor),
                New SqlParameter("@airs", airsNumber.DbFormattedString),
                New SqlParameter("@year", targetYear)
            }

            Return DB.RunCommand(SQL, parameters, forceAddNullableParameters:=True)
        End Function

        Public Function AssignmentYearExists(targetYear As Integer) As Boolean
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE INTYEAR = @year "

            Dim parameter As New SqlParameter("@year", targetYear)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function DeleteAssignmentYear(targetYear As Integer) As Boolean
            Dim query As String = " DELETE FROM SSCPINSPECTIONSREQUIRED " &
                " WHERE INTYEAR = @year "
            Dim parameter As New SqlParameter("@year", targetYear)

            Return DB.RunCommand(query, parameter)
        End Function

        Public Function CopyAssignmentYear(oldYear As Integer, targetYear As Integer) As Integer
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
                    Not FacilityAssignmentYearExists(airsNumberString, targetYear) Then

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
