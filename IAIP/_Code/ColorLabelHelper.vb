Imports System.Runtime.CompilerServices

Public Module ColorLabelHelper

    Public Enum ErrorLevel
        None
        Success
        Info
        Warning
        [Error]
    End Enum

    <Extension>
    Public Sub ShowMessage(label As Label, message As String, errorLevel As ErrorLevel)
        label.Visible = True
        label.Text = message

        If String.IsNullOrEmpty(message) Then
            errorLevel = ErrorLevel.None
        End If

        Select Case errorLevel

            Case ErrorLevel.None
                label.BackColor = New Color()
                label.ForeColor = New Color()

            Case ErrorLevel.Error
                label.BackColor = IaipColors.ErrorBackColor
                label.ForeColor = IaipColors.ErrorForeColor

            Case ErrorLevel.Info
                label.BackColor = IaipColors.InfoBackColor
                label.ForeColor = IaipColors.InfoForeColor

            Case ErrorLevel.Success
                label.BackColor = IaipColors.SuccessBackColor
                label.ForeColor = IaipColors.SuccessForeColor

            Case ErrorLevel.Warning
                label.BackColor = IaipColors.WarningBackColor
                label.ForeColor = IaipColors.WarningForeColor

        End Select
    End Sub

    <Extension>
    Public Sub ClearMessage(label As Label)
        label.ShowMessage(String.Empty, ErrorLevel.None)
    End Sub

End Module