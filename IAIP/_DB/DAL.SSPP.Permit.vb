Imports System.Collections.Generic
Imports Oracle.DataAccess.Client
Imports Iaip.Apb.SSPP

Namespace DAL.SSPP
    Module Permits

#Region " Read "

        Public Function PermitExists(ByVal permitNumber As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.APBISSUEDPERMIT " & _
                " WHERE RowNum = 1 " & _
                " AND STRPERMITNUMBER = :permitnumber "
            Dim parameter As New OracleParameter("permitnumber", permitNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function GetPermit(ByVal permitNumber As String) As Permit
            Dim query As String = _
                " SELECT ISSUEDPERMITID, " & _
                "   STRAIRSNUMBER, " & _
                "   STRPERMITNUMBER, " & _
                "   DATISSUED, " & _
                "   DATREVOKED, " & _
                "   ACTIVE, " & _
                "   PERMITTYPECODE " & _
                " FROM AIRBRANCH.APBISSUEDPERMIT " & _
                " WHERE STRPERMITNUMBER = :permitnumber " & _
                " ORDER BY DATISSUED DESC"

            Dim parameter As New OracleParameter("permitnumber", permitNumber)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)

            Return GetPermitFromDataRow(dt.Rows(0))
        End Function

        Public Function GetPermitsAsList(ByVal airsNumber As String) As List(Of Permit)

            Dim permitList As New List(Of Permit)
            Dim permit As New Permit

            Dim dataTable As DataTable = GetPermitsAsTable(airsNumber)

            For Each row As DataRow In dataTable.Rows
                permit = GetPermitFromDataRow(row)
                permitList.Add(permit)
            Next

            Return permitList
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

        Public Function GetPermitsAsTable(ByVal airsNumber As String) As DataTable
            Dim query As String = _
                " SELECT ISSUEDPERMITID, " & _
                "   STRAIRSNUMBER, " & _
                "   STRPERMITNUMBER, " & _
                "   DATISSUED, " & _
                "   DATREVOKED, " & _
                "   ACTIVE, " & _
                "   PERMITTYPECODE " & _
                " FROM AIRBRANCH.APBISSUEDPERMIT " & _
                " WHERE STRAIRSNUMBER = :airsnumber " & _
                " ORDER BY DATISSUED DESC Nulls FIRST "

            Dim parameter As New OracleParameter("airsnumber", airsNumber)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetActivePermitsAsTable(ByVal airsNumber As String) As DataTable
            Dim query As String = _
                " SELECT ISSUEDPERMITID, " & _
                "   STRAIRSNUMBER, " & _
                "   STRPERMITNUMBER, " & _
                "   DATISSUED, " & _
                "   DATREVOKED, " & _
                "   UPDATEDBY, " & _
                "   ACTIVE, " & _
                "   PERMITTYPECODE " & _
                " FROM AIRBRANCH.APBISSUEDPERMIT " & _
                " WHERE STRAIRSNUMBER = :airsnumber " & _
                " AND ACTIVE = '1' " & _
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

            Dim query As String = _
                " UPDATE AIRBRANCH.APBISSUEDPERMIT " & _
                " SET STRAIRSNUMBER    = :AirsNumber, " & _
                "   STRPERMITNUMBER    = :PermitNumber, " & _
                "   DATISSUED          = :IssuedDate, " & _
                "   DATREVOKED         = :RevokedDate, " & _
                "   UPDATEDATE         = :UpdateDate, " & _
                "   UPDATEDBY          = :UpdatedBy, " & _
                "   ACTIVE             = :Active, " & _
                "   PERMITTYPECODE     = :PermitTypeCode " & _
                " WHERE ISSUEDPERMITID = :ID "

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameters As OracleParameter()

            For Each permit As Permit In permits
                queryList.Add(query)
                parameters = New OracleParameter() { _
                    New OracleParameter("AirsNumber", permit.AirsNumber), _
                    New OracleParameter("PermitNumber", permit.PermitNumber), _
                    New OracleParameter("IssuedDate", permit.IssuedDate), _
                    New OracleParameter("RevokedDate", permit.RevokedDate), _
                    New OracleParameter("UpdateDate", Date.Now), _
                    New OracleParameter("UpdatedBy", UserGCode), _
                    New OracleParameter("Active", Convert.ToInt32(permit.Active)), _
                    New OracleParameter("PermitTypeCode", permit.PermitTypeCode), _
                    New OracleParameter("ID", permit.ID) _
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

            Dim query As String = _
                " INSERT " & _
                " INTO AIRBRANCH.APBISSUEDPERMIT " & _
                "   ( " & _
                "     ISSUEDPERMITID, " & _
                "     STRAIRSNUMBER, " & _
                "     STRPERMITNUMBER, " & _
                "     DATISSUED, " & _
                "     DATREVOKED, " & _
                "     CREATEDATE, " & _
                "     CREATEDBY, " & _
                "     UPDATEDATE, " & _
                "     UPDATEDBY, " & _
                "     ACTIVE, " & _
                "     PERMITTYPECODE " & _
                "   ) " & _
                "   VALUES " & _
                "   ( " & _
                "     AIRBRANCH.PERMITID_SEQ.NEXTVAL, " & _
                "     :AirsNumber, " & _
                "     :PermitNumber, " & _
                "     :IssuedDate, " & _
                "     :RevokedDate, " & _
                "     :CreateDate, " & _
                "     :CreatedBy, " & _
                "     :UpdateDate, " & _
                "     :UpdatedBy, " & _
                "     :Active, " & _
                "     :PermitTypeCode " & _
                "   ) "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("AirsNumber", permit.AirsNumber), _
                New OracleParameter("PermitNumber", permit.PermitNumber), _
                New OracleParameter("IssuedDate", permit.IssuedDate), _
                New OracleParameter("RevokedDate", permit.RevokedDate), _
                New OracleParameter("CreateDate", Date.Now), _
                New OracleParameter("CreatedBy", UserGCode), _
                New OracleParameter("UpdateDate", Date.Now), _
                New OracleParameter("UpdatedBy", UserGCode), _
                New OracleParameter("Active", Convert.ToInt32(permit.Active)), _
                New OracleParameter("PermitTypeCode", permit.PermitTypeCode) _
            }

            Return DB.RunCommand(query, parameters)
        End Function

#End Region

    End Module
End Namespace