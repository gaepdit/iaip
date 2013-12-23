Namespace Apb

    Module FacilityUtility

        Public Function IsAirsNumberValid(ByVal airsNumber As String) As Boolean
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
            If Not IsAirsNumberValid(airsNumber) Then Return False

            ' If okay, then remove spaces and dashes
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")

            If expand Then
                ' Expand the short form to the long form
                If airsNumber.Length = 8 Then airsNumber = "0413" & airsNumber
            Else
                ' Contract the long form to the short form
                If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)
            End If

            Return True
        End Function

        Public Function FormatAirsNumber(ByVal airsNumber As String, Optional ByVal expand As Boolean = False) As String
            ' Converts a string representation of an AIRS number to the "000-00000" form 
            ' (eight numerals, one dash).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "04-13-000-00000"
            ' form (12 numerals, dashes added, beginning with "04-13").

            If Not NormalizeAirsNumber(airsNumber, expand) Then Return Nothing
            If expand Then
                Return Mid(airsNumber, 1, 2) & "-" & Mid(airsNumber, 3, 2) & "-" & _
                    Mid(airsNumber, 5, 3) & "-" & Mid(airsNumber, 8, 5)
            Else
                Return Mid(airsNumber, 1, 3) & "-" & Mid(airsNumber, 4, 5)
            End If
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
