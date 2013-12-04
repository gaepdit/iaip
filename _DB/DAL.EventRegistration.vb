Imports Oracle.DataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module EventRegistration

        Public Function GetEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As Dictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " & _
                " FROM AIRBRANCH.RESLK_EVENTSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return d
        End Function

        Public Function GetRegistrationStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As Dictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " & _
                " FROM AIRBRANCH.RESLK_EVENTSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return d
        End Function

    End Module
End Namespace