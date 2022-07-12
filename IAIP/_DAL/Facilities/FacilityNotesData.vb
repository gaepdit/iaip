Imports System.Collections.Generic

Public Module FacilityNotesData

    Public Function GetFacilityNotes(airs As Apb.ApbFacilityId) As List(Of FacilityNote)

        Return New List(Of FacilityNote) From {
            New FacilityNote() With {
                .FacilityId = airs,
                .Dated = Today.AddDays(-1),
                .By = Author2,
                .Note = ShortText,
                .Archived = True
            },
            New FacilityNote() With {
                .FacilityId = airs,
                .Dated = Today.AddDays(-3),
                .By = Author1,
                .Note = LongText
            }
        }

    End Function

    Private Property Author1 As New Staff() With {
        .FirstName = "Douglas",
        .LastName = "Waldron",
        .UserId = 345
    }

    Private Property Author2 As New Staff() With {
        .FirstName = "Demo",
        .LastName = "User",
        .UserId = 1
    }

    Private Const ShortText As String = "Short bit of text."
    Private Const LongText As String = "Long bit of text. This text will overflow the typical column width. 

Long bit of text. This text will overflow the typical column width. Long bit of text. This text will overflow the 

typical column width. Long bit of text. This text will overflow the typical column width. Long bit of text. This 

text will overflow the typical column width. Long bit of text. This text will overflow the typical column width."


End Module
