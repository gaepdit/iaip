Namespace Apb

    Module FacilityUtility

        Public Function NormalizeDate(ByVal d As Date?) As Date?
            ' Converts a date to Nothing if date is equal to #7/4/1776#

            If d.Equals(CType(Nothing, Date)) Then Return d
            If Not IsDate(d) Then Return Nothing
            If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
            Return d
        End Function

    End Module

End Namespace
