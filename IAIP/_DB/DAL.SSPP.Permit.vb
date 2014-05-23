Imports System.Collections.Generic
Imports Oracle.DataAccess.Client
Imports Iaip.Apb.SSPP

Namespace DAL.SSPP
    Module Permits

#Region " Read "

        Public Function PermitExists(ByVal permitNumber As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".APBISSUEDPERMIT " & _
                " WHERE RowNum = 1 " & _
                " AND STRPERMITNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", permitNumber)

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
                " FROM " & DBNameSpace & ".APBISSUEDPERMIT " & _
                " WHERE STRPERMITNUMBER = :pID "

            Dim parameter As New OracleParameter("pId", permitNumber)
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
                " FROM " & DBNameSpace & ".APBISSUEDPERMIT " & _
                " WHERE STRAIRSNUMBER = :pID " & _
                " ORDER BY DATISSUED Nulls FIRST "

            Dim parameter As New OracleParameter("pId", airsNumber)
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
                " FROM " & DBNameSpace & ".APBISSUEDPERMIT " & _
                " WHERE STRAIRSNUMBER = :pID " & _
                " AND ACTIVE = '1' " & _
                " ORDER BY DATISSUED Nulls FIRST "

            Dim parameter As New OracleParameter("pId", airsNumber)
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
                " UPDATE " & DBNameSpace & ".APBISSUEDPERMIT " & _
                " SET STRAIRSNUMBER    = :pAirsNumber, " & _
                "   STRPERMITNUMBER    = :pPermitNumber, " & _
                "   DATISSUED          = :pIssuedDate, " & _
                "   DATREVOKED         = :pRevokedDate, " & _
                "   UPDATEDATE         = :pUpdateDate, " & _
                "   UPDATEDBY          = :pUpdatedBy, " & _
                "   ACTIVE             = :pActive, " & _
                "   PERMITTYPECODE     = :pPermitTypeCode " & _
                " WHERE ISSUEDPERMITID = :pId "

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameters As OracleParameter()

            For Each permit As Permit In permits
                queryList.Add(query)
                parameters = New OracleParameter() { _
                    New OracleParameter("pAirsNumber", permit.AirsNumber), _
                    New OracleParameter("pPermitNumber", permit.PermitNumber), _
                    New OracleParameter("pIssuedDate", permit.IssuedDate), _
                    New OracleParameter("pRevokedDate", permit.RevokedDate), _
                    New OracleParameter("pUpdateDate", Date.Now), _
                    New OracleParameter("pUpdatedBy", UserGCode), _
                    New OracleParameter("pActive", Convert.ToInt32(permit.Active)), _
                    New OracleParameter("pPermitTypeCode", permit.PermitTypeCode), _
                    New OracleParameter("pId", permit.ID) _
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
                " INTO " & DBNameSpace & ".APBISSUEDPERMIT " & _
                "   ( " & _
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
                "     :pAirsNumber, " & _
                "     :pPermitNumber, " & _
                "     :pIssuedDate, " & _
                "     :pRevokedDate, " & _
                "     :pCreateDate, " & _
                "     :pCreatedBy, " & _
                "     :pUpdateDate, " & _
                "     :pUpdatedBy, " & _
                "     :pActive, " & _
                "     :pPermitTypeCode " & _
                "   ) "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("pAirsNumber", permit.AirsNumber), _
                New OracleParameter("pPermitNumber", permit.PermitNumber), _
                New OracleParameter("pIssuedDate", permit.IssuedDate), _
                New OracleParameter("pRevokedDate", permit.RevokedDate), _
                New OracleParameter("pCreateDate", Date.Now), _
                New OracleParameter("pCreatedBy", UserGCode), _
                New OracleParameter("pUpdateDate", Date.Now), _
                New OracleParameter("pUpdatedBy", UserGCode), _
                New OracleParameter("pActive", Convert.ToInt32(permit.Active)), _
                New OracleParameter("pPermitTypeCode", permit.PermitTypeCode) _
            }

            Return DB.RunCommand(query, parameters)
        End Function

#End Region

    End Module
End Namespace