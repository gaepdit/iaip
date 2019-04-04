Imports System.Collections.Generic

''' <summary>
''' Allows storage of objects that change periodically and must be retrieved based on effective date.
''' </summary>
''' <typeparam name="T">The Type of the values returned.</typeparam>
''' <remarks>See https://www.martinfowler.com/eaaDev/TemporalProperty.html </remarks>
Public Class TemporalCollection(Of T)

    Public ReadOnly Property Contents As SortedList(Of Date, T)

    Public Sub New()
        Contents = New SortedList(Of Date, T)(New ReverseComparer(Of Date)(Comparer(Of Date).Default))
    End Sub

    Public Sub New(datedValues As IDictionary(Of Date, T))
        ' The ReverseComparer causes the SortedList to be sorted in reverse order (descending)
        Contents = New SortedList(Of Date, T)(
            datedValues,
            New ReverseComparer(Of Date)(Comparer(Of Date).Default)
        )
    End Sub

    Public Sub AddValue(effectiveDate As Date, value As T)
        Contents.Add(effectiveDate, value)
    End Sub

    ''' <summary>
    ''' Returns the current value in a Temporal Collection.
    ''' </summary>
    ''' <returns>The current value as T.</returns>
    Public Function GetCurrentValue() As T
        Return GetValueAt(Today)
    End Function

    ''' <summary>
    ''' Returns a value in a Temporal Collection as it existed on the specified effective date.
    ''' </summary>
    ''' <param name="effectiveDate">The effective date at which the value is needed.</param>
    ''' <returns></returns>
    Public Function GetValueAt(effectiveDate As Date) As T
        If Contents.Count = 0 Then
            Throw New ArgumentOutOfRangeException("Temporal collection contains no records.")
        End If

        For Each keyDate As Date In Contents.Keys
            If keyDate <= effectiveDate Then
                Return Contents(keyDate)
            End If
        Next

        Throw New ArgumentOutOfRangeException("No records that early.")
    End Function

    Public Function EarliestDate() As Date
        If Contents.Count = 0 Then
            Throw New ArgumentOutOfRangeException("Temporal collection contains no records.")
        End If

        Return Contents.Keys.Item(Contents.Count - 1)
    End Function

    Public Function LatestDate() As Date
        If Contents.Count = 0 Then
            Throw New ArgumentOutOfRangeException("Temporal collection contains no records.")
        End If

        Return Contents.Keys.Item(0)
    End Function

    Public ReadOnly Property Count() As Integer
        Get
            Return Contents.Count
        End Get
    End Property

End Class
