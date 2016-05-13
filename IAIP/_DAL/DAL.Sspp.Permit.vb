Imports System.Collections.Generic
Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Sspp

Namespace DAL.Sspp
    Module PermitData

#Region " Read "

        Public Function PermitExists(ByVal permitNumber As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.APBISSUEDPERMIT " &
                " WHERE RowNum = 1 " &
                " AND STRPERMITNUMBER = :permitnumber "
            Dim parameter As New OracleParameter("permitnumber", permitNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetPermit(ByVal permitNumber As String) As Permit
            Dim query As String =
                " SELECT ISSUEDPERMITID, " &
                "   STRAIRSNUMBER, " &
                "   STRPERMITNUMBER, " &
                "   DATISSUED, " &
                "   DATREVOKED, " &
                "   ACTIVE, " &
                "   PERMITTYPECODE " &
                " FROM AIRBRANCH.APBISSUEDPERMIT " &
                " WHERE STRPERMITNUMBER = :permitnumber " &
                " ORDER BY DATISSUED DESC"

            Dim parameter As New OracleParameter("permitnumber", permitNumber)
            Dim dr As DataRow = DB.GetDataRow(query, parameter)

            Return GetPermitFromDataRow(dr)
        End Function

        Public Function GetActivePermitsAsList(ByVal airsNumber As String) As List(Of Permit)

            Dim permitList As New List(Of Permit)
            Dim permit As New Permit

            Dim dataTable As DataTable = GetActivePermitsAsTable(airsNumber)

            For Each row As DataRow In dataTable.Rows
                permit = GetPermitFromDataRow(row)
                permitList.Add(permit)
            Next

            Return permitList
        End Function

        Public Function GetActivePermitsAsTable(ByVal airsNumber As String) As DataTable
            Dim query As String =
                " SELECT ISSUEDPERMITID, " &
                "   STRAIRSNUMBER, " &
                "   STRPERMITNUMBER, " &
                "   DATISSUED, " &
                "   DATREVOKED, " &
                "   UPDATEDBY, " &
                "   ACTIVE, " &
                "   PERMITTYPECODE " &
                " FROM AIRBRANCH.APBISSUEDPERMIT " &
                " WHERE STRAIRSNUMBER = :airsnumber " &
                " AND ACTIVE = '1' " &
                " ORDER BY DATISSUED Nulls FIRST "

            Dim parameter As New OracleParameter("airsnumber", airsNumber)
            Return DB.GetDataTable(query, parameter)
        End Function

        Private Function GetPermitFromDataRow(ByVal row As DataRow) As Permit
            Dim permit As New Permit
            With permit
                .Active = Convert.ToBoolean(Convert.ToInt32(DB.GetNullable(Of String)(row("ACTIVE"))))
                .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .ID = DB.GetNullable(Of String)(row("ISSUEDPERMITID"))
                .IssuedDate = DB.GetNullable(Of Date)(row("DATISSUED"))
                .PermitNumber = DB.GetNullable(Of String)(row("STRPERMITNUMBER"))
                .PermitTypeCode = DB.GetNullable(Of String)(row("PERMITTYPECODE"))
                .RevokedDate = DB.GetNullable(Of Date?)(row("DATREVOKED"))
            End With
            Return permit
        End Function

#End Region

#Region " Write "

        Public Function UpdatePermits(ByVal permits As List(Of Permit)) As Boolean
            If permits Is Nothing OrElse permits.Count = 0 Then Return False

            Dim query As String =
                " UPDATE AIRBRANCH.APBISSUEDPERMIT " &
                " SET STRAIRSNUMBER    = :AirsNumber, " &
                "   STRPERMITNUMBER    = :PermitNumber, " &
                "   DATISSUED          = :IssuedDate, " &
                "   DATREVOKED         = :RevokedDate, " &
                "   UPDATEDATE         = :UpdateDate, " &
                "   UPDATEDBY          = :UpdatedBy, " &
                "   ACTIVE             = :Active, " &
                "   PERMITTYPECODE     = :PermitTypeCode " &
                " WHERE ISSUEDPERMITID = :ID "

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameters As OracleParameter()

            For Each permit As Permit In permits
                queryList.Add(query)
                parameters = New OracleParameter() {
                    New OracleParameter("AirsNumber", permit.AirsNumber),
                    New OracleParameter("PermitNumber", permit.PermitNumber),
                    New OracleParameter("IssuedDate", permit.IssuedDate),
                    New OracleParameter("RevokedDate", permit.RevokedDate),
                    New OracleParameter("UpdateDate", Date.Now),
                    New OracleParameter("UpdatedBy", CurrentUser.UserID),
                    New OracleParameter("Active", Convert.ToInt32(permit.Active)),
                    New OracleParameter("PermitTypeCode", permit.PermitTypeCode),
                    New OracleParameter("ID", permit.ID)
                }
                parametersList.Add(parameters)
            Next

            Return DB.RunCommand(queryList, parametersList)
        End Function

        Public Function UpdatePermit(ByVal permit As Permit) As Boolean
            If permit Is Nothing Then Return False
            Dim permitList As New List(Of Permit)
            permitList.Add(permit)
            Return UpdatePermits(permitList)
        End Function

        Public Function AddPermit(ByVal permit As Permit) As Boolean
            If permit Is Nothing Then Return False

            Dim query As String =
                " INSERT " &
                " INTO AIRBRANCH.APBISSUEDPERMIT " &
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
                "   VALUES " &
                "   ( " &
                "     AIRBRANCH.PERMITID_SEQ.NEXTVAL, " &
                "     :AirsNumber, " &
                "     :PermitNumber, " &
                "     :IssuedDate, " &
                "     :RevokedDate, " &
                "     :CreateDate, " &
                "     :CreatedBy, " &
                "     :UpdateDate, " &
                "     :UpdatedBy, " &
                "     :Active, " &
                "     :PermitTypeCode " &
                "   ) "

            Dim parameters As OracleParameter() = {
                New OracleParameter("AirsNumber", permit.AirsNumber),
                New OracleParameter("PermitNumber", permit.PermitNumber),
                New OracleParameter("IssuedDate", permit.IssuedDate),
                New OracleParameter("RevokedDate", permit.RevokedDate),
                New OracleParameter("CreateDate", Date.Now),
                New OracleParameter("CreatedBy", CurrentUser.UserID),
                New OracleParameter("UpdateDate", Date.Now),
                New OracleParameter("UpdatedBy", CurrentUser.UserID),
                New OracleParameter("Active", Convert.ToInt32(permit.Active)),
                New OracleParameter("PermitTypeCode", permit.PermitTypeCode)
            }

            Return DB.RunCommand(query, parameters)
        End Function

#End Region

    End Module
End Namespace