Imports Oracle.DataAccess.Client
Imports System.IO

Module ErrorReporting

    Public Sub ErrorReport(ByVal exc As System.Exception, ByVal ErrorLocation As String)
        monitor.TrackException(exc, ErrorLocation)
        ErrorReport(exc.ToString, ErrorLocation)
    End Sub

    Public Sub ErrorReport(ByVal ErrorMessage As String, ByVal ErrorLocation As String, Optional ByVal exc As System.Exception = Nothing)
        Dim SQL As String
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader
        Dim ErrorMess As String

        If UserGCode = "" Then
            UserGCode = "0"
        End If

        If ErrorMessage.Contains("ORA-12592") Then
            MsgBox("There was a connectivity error with the database." & vbCrLf & "Please run the task that caused this error again to verify the data is correct." & vbCrLf & "If the error presists, try waiting until the internet connection improves or contact the Data Management Unit." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If

        If ErrorMessage.Contains("ORA-12545") Or ErrorMessage.Contains("ORA-604") Or ErrorMessage.Contains("ORA-257") Then
            MsgBox("There is no connection to the database at this time." & vbCrLf & "Verify that you currently have an internet connection." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-03114") Then
            MsgBox("There is no connection to the database at this time." & vbCrLf & "Verify that you currently have an internet connection." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-1033") Or ErrorMessage.Contains("ORA-01033") Then
            MsgBox("The database is currently undergoing a restart procedure." & vbCrLf & "Please wait 15 minutes and try again." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-12154") Then
            MsgBox("This PC has a connection error." & vbCrLf & "Please contact the Data Management Unit for assistance." & vbCrLf & _
             "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Could not load file or assembly 'CrystalDecisions.") Or _
                      ErrorMessage.Contains("Integrated Air Information Platf") Then
            MsgBox("This machine needs to run the Crystal Report Patch." & vbCrLf & _
                   "Please contact the Data Management Unit for assistance.", _
                    MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
            MsgBox("The platform is running multiple processing threads and needs time to complete them." & vbCrLf & _
                   "Please allow time for the process to run.", MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-03113") Or ErrorMessage.Contains("ORA-12637") Then
            MsgBox("The platform experienced a connection error." & vbCrLf & "Try reloading the form" & vbCrLf & _
                   "If the problem persists please contact the Data Management Unit.", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-12571") Or ErrorMessage.Contains("ORA-01033") Or ErrorMessage.Contains("ORA-12545") Then
            MsgBox("The platform experienced a connection error." & vbCrLf & "Try reloading the form" & vbCrLf & _
                   "If the problem persists please contact the Data Management Unit.", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("System.ComponentModel.Win32Exception: The system cannot find the file specified") Then
            MsgBox("The platform is attempting to contact an external server." & vbCrLf & _
                   "The server maybe unavailable at thit time." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("System.InvalidOperationException: Invalid operation. The connection is closed") Then
            MsgBox("The connection to the database was temporarily lost." & vbCrLf & "Please try to reload the page." & vbCrLf & _
                   "If the problem persists contact the Data Management Unit.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
            MsgBox("It appears that this computer has thrown a System.OutOfMemoryException. " & vbCrLf & _
                   "Try freeing up memory by closing any un-needed open computer applications.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Attempted to read or write protected memory") Then
            MsgBox("There was a unique error." & vbCrLf & _
                   "Try to perform your action again. If the error returns close out the IAIP application and try again." & vbCrLf & _
                   "If this error persists contact the Data Management Unit", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        Try
            ErrorMessage = GetCurrentVersion.ToString & vbCrLf & ErrorMessage
            ErrorMess = Mid(ErrorMessage, 1, 4000)

            SQL = "Insert into " & DBNameSpace & ".IAIPErrorLog " & _
            "(strErrorNumber, strUser, " & _
            "strErrorLocation, strErrorMessage, " & _
            "datErrorDate) " & _
            "values " & _
            "(" & DBNameSpace & ".IAIPErrornumber.nextval, '" & UserGCode & "', " & _
            "'" & Replace(ErrorLocation, "'", "''") & "', '" & Replace(ErrorMess, "'", "''") & "', " & _
            "sysdate) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Read()
            dr.Close()


            MsgBox("An Error has occurred." & vbCrLf & "The error has been logged and sent to the developers." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
        Catch ex As Exception
            MsgBox("There was an error in logging this problem." & vbCrLf & "Please contact the Data Management Unit with this problem." & vbCrLf & _
                   ErrorMessage, MsgBoxStyle.Exclamation, "Integrated Air Information Platform - ERROR MESSAGE")
        End Try

    End Sub

End Module
