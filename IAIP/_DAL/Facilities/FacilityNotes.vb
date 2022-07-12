Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Module FacilityNotes

    Public Function GetFacilityNotes(airs As Apb.ApbFacilityId) As List(Of FacilityNote)
        Dim query As String = "select n.Id,
                   n.FacilityId,
                   n.Note,
                   n.DateEntered,
                   n.AuthorId,
                   n.Archived,
                   u.STRLASTNAME  as LastName,
                   u.STRFIRSTNAME as FirstName
            from FacilityNotes n
                inner join EPDUSERPROFILES u
                on n.AuthorId = u.NUMUSERID
            where FacilityId = @airs"

        Dim param As New SqlParameter("@airs", airs.DbFormattedString)

        Dim dt As DataTable = DB.GetDataTable(query, param)

        Dim result As New List(Of FacilityNote)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim author As New Staff With {
                    .FirstName = row.Item("FirstName"),
                    .LastName = row.Item("LastName"),
                    .UserId = row.Item("AuthorId")
                }

                result.Add(New FacilityNote() With {
                    .Archived = row.Item("Archived"),
                    .By = author,
                    .Dated = row.Item("DateEntered"),
                    .FacilityId = New Apb.ApbFacilityId(row.Item("FacilityId")),
                    .Id = row.Item("Id"),
                    .Note = row.Item("Note")
                })
            Next
        End If

        Return result
    End Function

    Public Function SaveFacilityNote(airs As Apb.ApbFacilityId, note As String) As Boolean
        Dim query As String = "insert into FacilityNotes (FacilityId, Note, AuthorId) values (@airs, @note, @authorId)"

        Dim params As SqlParameter() = {
            New SqlParameter("@airs", airs.DbFormattedString),
            New SqlParameter("@note", note),
            New SqlParameter("@authorId", CurrentUser.UserID)
        }

        Return DB.RunCommand(query, params)
    End Function

    Public Function ArchiveFacilityNote(id As Guid, action As String) As Boolean
        Dim toArchive As Boolean = action = "Archive"

        Dim query As String = "update FacilityNotes set Archived = @archive, DateModified = sysdatetimeoffset(), 
            ModifiedBy = @authorId where Id = @id"

        Dim params As SqlParameter() = {
            New SqlParameter("@archive", toArchive),
            New SqlParameter("@authorId", CurrentUser.UserID),
            New SqlParameter("@id", id)
        }

        Return DB.RunCommand(query, params)
    End Function

    Public Function DeleteFacilityNote(id As Guid) As Boolean
        Dim query As String = "delete from FacilityNotes where Id = @id"
        Dim param As New SqlParameter("@id", id)
        Return DB.RunCommand(query, param)
    End Function

End Module
