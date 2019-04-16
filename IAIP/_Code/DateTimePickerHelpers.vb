Imports System.Collections.Generic

Public Module DateTimePickerHelpers

    Public Sub SetDtpMaxDates(maxDate As Date, dateControls As List(Of DateTimePicker))
        For Each dateControl As DateTimePicker In dateControls
            dateControl.MaxDate = maxDate
            dateControl.Checked = False
        Next
    End Sub

    Public Function GetNullableDateFromDateTimePicker(dtp As DateTimePicker) As Date?
        If dtp.Checked Then
            Return dtp.Value
        Else
            Return Nothing
        End If
    End Function

End Module
