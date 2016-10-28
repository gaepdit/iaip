Imports System.Data.SqlClient

Namespace DAL.Sbeap

    Module SbeapData

        Public Function ClientExists(clientID As String) As Boolean
            If clientID = "" OrElse Not Integer.TryParse(clientID, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPCLIENTS " &
                " WHERE CLIENTID = @id "

            Dim parameter As New SqlParameter("@id", clientID)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function ClientNameExists(clientName As String) As Boolean
            If String.IsNullOrWhiteSpace(clientName) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPCLIENTS " &
                " WHERE strCompanyName = @id "

            Dim parameter As New SqlParameter("@id", clientName)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function CaseExists(caseNumber As String) As Boolean
            If caseNumber = "" OrElse Not Integer.TryParse(caseNumber, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPCASELOG " &
                " WHERE NUMCASEID = @id "

            Dim parameter As New SqlParameter("@id", caseNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function ActionExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPActionLog " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function ComplianceAssistExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPComplianceAssist " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function TechnicalAssistExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPTechnicalAssist " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function PhoneLogExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPPhoneLog " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function ConferenceLogExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPConferenceLog " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function OtherLogExists(actionId As String) As Boolean
            If actionId = "" OrElse Not Integer.TryParse(actionId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SBEAPOtherLog " &
                " WHERE numActionID = @id "

            Dim parameter As New SqlParameter("@id", actionId)

            Return DB.GetBoolean(query, parameter)
        End Function

    End Module

End Namespace
