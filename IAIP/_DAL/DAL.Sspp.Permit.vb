Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.Apb.Sspp
Imports EpdIt

Namespace DAL.Sspp
    Module PermitData

#Region " Read "

        Public Function PermitExists(permitNumber As String) As Boolean
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM APBISSUEDPERMIT " &
                " WHERE STRPERMITNUMBER = @permitnumber "

            Dim parameter As New SqlParameter("@permitnumber", permitNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetPermit(permitNumber As String) As Permit
            Dim query As String =
                " SELECT ISSUEDPERMITID, " &
                "   STRAIRSNUMBER, " &
                "   STRPERMITNUMBER, " &
                "   DATISSUED, " &
                "   DATREVOKED, " &
                "   ACTIVE, " &
                "   PERMITTYPECODE " &
                " FROM APBISSUEDPERMIT " &
                " WHERE STRPERMITNUMBER = @permitnumber " &
                " ORDER BY DATISSUED DESC"

            Dim parameter As New SqlParameter("@permitnumber", permitNumber)
            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            Return GetPermitFromDataRow(dr)
        End Function

        Public Function GetActivePermitsAsList(airsNumber As String) As List(Of Permit)

            Dim permitList As New List(Of Permit)
            Dim permit As New Permit

            Dim dataTable As DataTable = GetActivePermitsAsTable(airsNumber)

            For Each row As DataRow In dataTable.Rows
                permit = GetPermitFromDataRow(row)
                permitList.Add(permit)
            Next

            Return permitList
        End Function

        Public Function GetActivePermitsAsTable(airsNumber As String) As DataTable
            Dim query As String =
                " SELECT ISSUEDPERMITID, " &
                "   STRAIRSNUMBER, " &
                "   STRPERMITNUMBER, " &
                "   DATISSUED, " &
                "   DATREVOKED, " &
                "   UPDATEDBY, " &
                "   ACTIVE, " &
                "   PERMITTYPECODE " &
                " FROM APBISSUEDPERMIT " &
                " WHERE STRAIRSNUMBER = @airsnumber " &
                " AND ACTIVE = '1' " &
                " ORDER BY DATISSUED "

            Dim parameter As New SqlParameter("@airsnumber", airsNumber)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetPermitFromDataRow(row As DataRow) As Permit
            Dim permit As New Permit
            With permit
                .Active = ConvertDBValueToBoolean(DBUtilities.GetNullable(Of String)(row("ACTIVE")), BooleanDBConversionType.OneOrZero)
                .AirsNumber = DBUtilities.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .ID = DBUtilities.GetNullable(Of String)(row("ISSUEDPERMITID"))
                .IssuedDate = DBUtilities.GetNullable(Of Date)(row("DATISSUED"))
                .PermitNumber = DBUtilities.GetNullable(Of String)(row("STRPERMITNUMBER"))
                .PermitTypeCode = DBUtilities.GetNullable(Of String)(row("PERMITTYPECODE"))
                .RevokedDate = DBUtilities.GetNullable(Of Date?)(row("DATREVOKED"))
            End With
            Return permit
        End Function

#End Region

#Region " Write "

        Public Function UpdatePermits(permits As List(Of Permit)) As Boolean
            If permits Is Nothing OrElse permits.Count = 0 Then Return False

            Dim query As String =
                " UPDATE APBISSUEDPERMIT " &
                " SET STRAIRSNUMBER    = @AirsNumber, " &
                "   STRPERMITNUMBER    = @PermitNumber, " &
                "   DATISSUED          = @IssuedDate, " &
                "   DATREVOKED         = @RevokedDate, " &
                "   UPDATEDATE         = @UpdateDate, " &
                "   UPDATEDBY          = @UpdatedBy, " &
                "   ACTIVE             = @Active, " &
                "   PERMITTYPECODE     = @PermitTypeCode " &
                " WHERE ISSUEDPERMITID = @ID "

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())
            Dim parameters As SqlParameter()

            For Each permit As Permit In permits
                queryList.Add(query)
                parameters = New SqlParameter() {
                    New SqlParameter("@AirsNumber", permit.AirsNumber),
                    New SqlParameter("@PermitNumber", permit.PermitNumber),
                    New SqlParameter("@IssuedDate", permit.IssuedDate),
                    New SqlParameter("@RevokedDate", permit.RevokedDate),
                    New SqlParameter("@UpdateDate", Date.Now),
                    New SqlParameter("@UpdatedBy", CurrentUser.UserID),
                    New SqlParameter("@Active", ConvertBooleanToDBValue(permit.Active, BooleanDBConversionType.OneOrZero)),
                    New SqlParameter("@PermitTypeCode", permit.PermitTypeCode),
                    New SqlParameter("@ID", permit.ID)
                }
                parametersList.Add(parameters)
            Next

            Return DB.RunCommand(queryList, parametersList, forceAddNullableParameters:=True)
        End Function

        Public Function UpdatePermit(permit As Permit) As Boolean
            If permit Is Nothing Then Return False
            Dim permitList As New List(Of Permit)
            permitList.Add(permit)
            Return UpdatePermits(permitList)
        End Function

        Public Function AddPermit(permit As Permit) As Boolean
            If permit Is Nothing Then Return False

            Dim query As String =
                " INSERT " &
                " INTO APBISSUEDPERMIT " &
                "   ( " &
                "     ISSUEDPERMITID, " &
                "     STRAIRSNUMBER, " &
                "     STRPERMITNUMBER, " &
                "     DATISSUED, " &
                "     DATREVOKED, " &
                "     CREATEDATE, " &
                "     CREATEDBY, " &
                "     UPDATEDATE, " &
                "     UPDATEDBY, " &
                "     ACTIVE, " &
                "     PERMITTYPECODE " &
                "   ) " &
                "   select " &
                "     next value for PERMITID_SEQ, " &
                "     @AirsNumber, " &
                "     @PermitNumber, " &
                "     @IssuedDate, " &
                "     @RevokedDate, " &
                "     @CreateDate, " &
                "     @CreatedBy, " &
                "     @UpdateDate, " &
                "     @UpdatedBy, " &
                "     @Active, " &
                "     @PermitTypeCode "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@AirsNumber", permit.AirsNumber),
                New SqlParameter("@PermitNumber", permit.PermitNumber),
                New SqlParameter("@IssuedDate", permit.IssuedDate),
                New SqlParameter("@RevokedDate", permit.RevokedDate),
                New SqlParameter("@CreateDate", Date.Now),
                New SqlParameter("@CreatedBy", CurrentUser.UserID),
                New SqlParameter("@UpdateDate", Date.Now),
                New SqlParameter("@UpdatedBy", CurrentUser.UserID),
                New SqlParameter("@Active", ConvertBooleanToDBValue(permit.Active, BooleanDBConversionType.OneOrZero)),
                New SqlParameter("@PermitTypeCode", permit.PermitTypeCode)
            }

            Return DB.RunCommand(query, parameters, forceAddNullableParameters:=True)
        End Function

        Public Function RevokePermits(permits As List(Of Permit), revocationDate As Date) As Boolean
            If permits Is Nothing OrElse permits.Count = 0 Then Return False

            Dim permitIds As New List(Of Integer)

            For Each permit As Permit In permits
                permitIds.Add(permit.ID)
            Next

            Dim query As String =
                " UPDATE APBISSUEDPERMIT " &
                " SET DATREVOKED = @RevokedDate, " &
                "   UPDATEDATE = @UpdateDate, " &
                "   UPDATEDBY = @UpdatedBy, " &
                "   ACTIVE = @Active " &
                " WHERE ISSUEDPERMITID IN (SELECT * FROM @ids) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@RevokedDate", revocationDate),
                New SqlParameter("@UpdateDate", Date.Now),
                New SqlParameter("@UpdatedBy", CurrentUser.UserID),
                New SqlParameter("@Active", ConvertBooleanToDBValue(False, BooleanDBConversionType.OneOrZero)),
                permitIds.AsTvpSqlParameter("@ids")
            }

            Return DB.RunCommand(query, parameters)
        End Function

#End Region

    End Module
End Namespace