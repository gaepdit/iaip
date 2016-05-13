Public Module DateFunctions

    ''' <summary>
    ''' Converts a Date object to Nothing if date is equal to #7/4/1776#. 
    ''' </summary>
    ''' <param name="d">The Date to normalize.</param>
    ''' <returns>A nullable Date that has been normalized.</returns>
    ''' <remarks></remarks>
    Public Function NormalizeDate(ByVal d As Date?) As Date?
        ' Converts a date to Nothing if date is equal to #7/4/1776#

        If d.Equals(CType(Nothing, Date)) Then Return d
        If Not IsDate(d) Then Return Nothing
        If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
        Return d
    End Function

    ''' <summary>
    ''' Determines whether any of a list of nullable dates contains a value
    ''' </summary>
    ''' <param name="dates">An array of nullable dates</param>
    ''' <returns>Returns True if any of the nullable dates has a value; otherwise returns False.</returns>
    Public Function AnyOfTheseDatesHasValue(dates() As Date?) As Boolean
        For Each d As Date? In dates
            If d.HasValue Then
                Return True
            End If
        Next
        Return False
    End Function

End Module
