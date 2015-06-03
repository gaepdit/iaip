Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.SSCP

Namespace DAL.ISMP

    Module StackTest

        Public Function StackTestExists(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRREFERENCENUMBER = :ReferenceNumber "
            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function TestNotificationExists(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPTESTNOTIFICATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRTESTLOGNUMBER = :ReferenceNumber "
            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function StackTestIsClosedOut(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return False

            Dim query As String = "SELECT STRCLOSED " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE STRREFERENCENUMBER = :ReferenceNumber "
            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetStackTestDocumentType(ByVal referenceNumber As String) As String
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return Nothing

            Dim query As String = _
            "SELECT dt.STRDOCUMENTTYPE " & _
            "FROM AIRBRANCH.ISMPDocumentType dt " & _
            "INNER JOIN AIRBRANCH.ISMPReportInformation ri " & _
            "ON ri.STRDOCUMENTTYPE = dt.STRKEY " & _
            "WHERE ri.STRREFERENCENUMBER = :ReferenceNumber"

            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

    End Module

End Namespace
