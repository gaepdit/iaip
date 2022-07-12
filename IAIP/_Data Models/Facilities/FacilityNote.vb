Imports System.ComponentModel

Public Class FacilityNote
    Public Property Id As Guid
    <DisplayName("AIRS #")>
    Public Property FacilityId As Apb.ApbFacilityId
    Public Property Dated As Date
    Public Property By As Staff
    Public Property Note As String
    Public Property Archived As Boolean
End Class
