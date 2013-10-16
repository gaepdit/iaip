Namespace Apb
    Module FacilityUtility

        Public Function ValidAirsNumber(ByVal airsNumber As String) As Boolean
            ' Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000
            ' (with or without the dashes)

            ' Remove dashes and spaces (the only non-numeral characters allowed)
            Dim a As String = airsNumber.Replace("-", "").Replace(" ", "")

            ' Test to see if remaining string can be parsed as an integer
            ' (i.e., only numerals remain)
            If Not (Int64.TryParse(a, Nothing)) Then _
                Return False
            If Not (a.Length = 8 Or a.Length = 12) Then _
                Return False
            If (a.Length = 12 And Mid(a, 1, 4) <> "0413") Then _
                Return False

            ' No red flags? Give a green light (to mix metaphors)
            Return True
        End Function

        Public Function NormalizeAirsNumber(ByRef airsNumber As String, Optional ByVal expand As Boolean = False) As Boolean
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").
            '
            ' Return value indicates whether the conversion succeeded.

            ' First, validate the raw AIRS number
            If Not ValidAirsNumber(airsNumber) Then Return False

            ' If okay, then remove spaces and dashes
            Dim a As String = airsNumber.Replace("-", "").Replace(" ", "")

            If expand Then
                ' Expand the short form to the long form
                a = If(a.Length = 8, "0413", "") & a
            Else
                ' Contract the long form to the short form
                If a.Length = 12 Then a = a.Remove(0, 4)
            End If

            ' Revalidate just in case (shouldn't ever fail?)
            If Not ValidAirsNumber(a) Then Return False

            ' Replace referenced variable with converted result and return
            airsNumber = a
            Return True
        End Function

        Public Function NormalizeDate(ByVal d As Date?) As Date?
            ' Converts a date to Nothing if date is equal to #7/4/1776#

            If d.Equals(CType(Nothing, Date)) Then Return d
            If Not IsDate(d) Then Return Nothing
            If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
            Return d
        End Function

    End Module
End Namespace