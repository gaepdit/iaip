Imports System.Collections.Generic

''' <summary>
''' Allows storage of objects that change periodically and must be retrieved based on effective date.
''' </summary>
''' <typeparam name="T">The Type of the values returned.</typeparam>
''' <remarks>See https://www.martinfowler.com/eaaDev/TemporalProperty.html </remarks>
Public Class TemporalCollection(Of T)

    Private Property contents As SortedList(Of Date, T)

    Public Sub New()
        contents = New SortedList(Of Date, T)(New ReverseComparer(Of Date)(Comparer(Of Date).Default))
    End Sub

    Public Sub New(datedValues As IDictionary(Of Date, T))
        ' The ReverseComparer causes the SortedList to be sorted in reverse order (descending)
        contents = New SortedList(Of Date, T)(
            datedValues,
            New ReverseComparer(Of Date)(Comparer(Of Date).Default)
        )
    End Sub

    Public Sub AddValue(effectiveDate As Date, value As T)
        contents.Add(effectiveDate, value)
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
        If contents.Count = 0 Then
            Throw New ArgumentOutOfRangeException("Temporal collection contains no records.")
        End If

        For Each keyDate As Date In contents.Keys
            If keyDate <= effectiveDate Then
                Return contents(keyDate)
            End If
        Next

        Throw New ArgumentOutOfRangeException("No records that early.")
    End Function

    Public ReadOnly Property EarliestDate() As Date
        Get
            If contents.Count = 0 Then
                Throw New ArgumentOutOfRangeException("Temporal collection contains no records.")
            End If

            Return contents.Keys.Item(contents.Count - 1)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return contents.Count
        End Get
    End Property


End Class
