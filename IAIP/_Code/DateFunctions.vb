Public Module DateFunctions

    ''' <summary>
    ''' Converts a nullable date to Nothing if the date value is equal to #7/4/1776#. 
    ''' </summary>
    ''' <param name="d">The date to normalize.</param>
    ''' <returns>A nullable Date that has been normalized.</returns>
    Public Function NormalizeDate(d As Date?) As Date?
        If d Is Nothing OrElse d.Equals(New Date(1776, 7, 4)) Then Return Nothing
        Return d
    End Function

    ''' <summary>
    ''' Converts a database object to a nullable date.
    ''' Returns Nothing if the date is null or DBNull or equal to #7/4/1776#.
    ''' </summary>
    ''' <param name="d">The object to normalize.</param>
    ''' <returns>A nullable Date that has been normalized.</returns>
    Public Function NormalizeDbDate(d As Object) As Date?
        If IsDBNull(d) OrElse
            Not IsDate(d) OrElse
            d Is Nothing OrElse
            d.Equals(New Date(1776, 7, 4)) Then

            Return Nothing
        End If

        Return d
    End Function

    ''' <summary>
    ''' Evaluates a nullable date and returns the date value or today if the date is null.
    ''' </summary>
    ''' <param name="d">The date to evaluate.</param>
    ''' <returns>A Date.</returns>
    Public Function RealDateOrToday(d As Date?) As Date
        Return RealDateOr(d, Today)
    End Function

    ''' <summary>
    ''' Evaluates a nullable date and returns the date value or today if the date is null.
    ''' </summary>
    ''' <param name="d">The date to evaluate.</param>
    ''' <returns>A Date.</returns>
    Public Function RealDateOr(d As Date?, replacementDate As Date) As Date
        If d Is Nothing Then Return replacementDate
        Return d
    End Function

    ''' <summary>
    ''' Determines whether any of a list of nullable dates contains a value
    ''' </summary>
    ''' <param name="dates">An array of nullable dates</param>
    ''' <returns>Returns True if any of the nullable dates has a value; otherwise returns False.</returns>
    Public Function AnyOfTheseDatesHasValue(dates() As Date?) As Boolean
        NotNull(dates, NameOf(dates))
        For Each d As Date? In dates
            If d.HasValue Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function DatePriorToDate(month As Integer, day As Integer, priorToDate As Date) As Date
        Return New Date(priorToDate.Year - Convert.ToInt32(priorToDate <= New Date(priorToDate.Year, month, day)), month, day)
    End Function

End Module
