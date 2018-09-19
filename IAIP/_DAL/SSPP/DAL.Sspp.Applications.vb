Imports System.Data.SqlClient

Namespace DAL.Sspp

    Module ApplicationData

        Public Function ApplicationExists(appNumber As String) As Boolean
            Dim appNumberInt As Integer
            If String.IsNullOrWhiteSpace(appNumber) OrElse Not Integer.TryParse(appNumber, appNumberInt) Then Return False
            Return ApplicationExists(appNumberInt)
        End Function

        Public Function ApplicationExists(appNumber As Integer) As Boolean
            If appNumber = 0 Then Return False
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM SSPPAPPLICATIONMASTER WHERE STRAPPLICATIONNUMBER = @id "
            Return DB.GetBoolean(query, New SqlParameter("@id", appNumber))
        End Function

        Public Function GetWhenLastModified(appNumber As Integer) As DateTimeOffset
            Dim query As String = "select DATMODIFINGDATE from SSPPAPPLICATIONMASTER where STRAPPLICATIONNUMBER = @appNumber"
            Dim param As New SqlParameter("@appNumber", appNumber)
            Return DB.GetSingleValue(Of DateTimeOffset)(query, param)
        End Function

    End Module

End Namespace
