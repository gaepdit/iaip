Imports Iaip.DAL.EventRegistrationData
Namespace Apb.Res

    Public Class ResEvent

        Public Sub New(id As Integer)
            Me.EventId = id
            Dim dataRow As DataRow = GetResEventByIdAsDataRow(id)
            If dataRow IsNot Nothing Then
                FillResEventInfoFromDataRow(dataRow, Me)
            End If
        End Sub

        Public Property EventId() As Integer
        Public Property Active() As Boolean
        Public Property EventStatus() As String
        Public Property WebContact() As IaipUser
        Public Property Title() As String
        Public Property Description() As String
        Public Property StartDate() As Date?
        Public Property EndDate() As Date?
        Public Property Venue() As String
        Public Property Capacity() As Integer
        Public Property Notes() As String
        Public Property LoginRequired() As Boolean
        Public Property PassCode() As String
        Public Property Address() As Address
        Public Property Contact() As IaipUser
        Public Property StartTime() As String
        Public Property EndTime() As String

    End Class

End Namespace
