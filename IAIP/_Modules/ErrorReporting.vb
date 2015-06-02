Imports Oracle.DataAccess.Client

Module ErrorReporting

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="exc">The exception to be handled.</param>
    ''' <param name="contextMessage">A string representing the context of the error.</param>
    Public Sub ErrorReport(ByVal exc As System.Exception, ByVal contextMessage As String)
        ErrorReport(exc, "", contextMessage)
    End Sub

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="exc">The exception to be handled.</param>
    ''' <param name="supplementalMessage">A string containing supplementary information to be logged.</param>
    ''' <param name="contextMessage">A string representing the calling function.</param>
    Public Sub ErrorReport(ByVal exc As System.Exception, ByVal supplementalMessage As String, ByVal contextMessage As String)

        ' First, track the exception using our analytics program. This is more reliable.
        monitor.TrackException(exc, contextMessage)

        ' Second, try logging the error message to the IAIP database. This requires a connection so will sometimes fail.
        Dim errorMessage As String = exc.Message
        If Not String.IsNullOrEmpty(supplementalMessage) Then
            errorMessage = errorMessage & Environment.NewLine & Environment.NewLine & supplementalMessage
        End If
        LogError(errorMessage, contextMessage)

        ' Third display a dialog to the user describing the error and next steps.
        Dim WhatHappened As String = ""
        Dim WhatUserCanDo As String = ""

        If errorMessage.Contains("Could not load file or assembly 'CrystalDecisions.") Then
            App.ShowCrystalReportsSupportMessage()
            Exit Sub
        End If

        If errorMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
            WhatHappened = "The IAIP is running multiple processing threads and needs time to complete them. Please allow time for the process to run."
            WhatUserCanDo = "• Wait for the process to finish before continuing." & Environment.NewLine & Environment.NewLine
        ElseIf errorMessage.Contains("ORA-12571") Or errorMessage.Contains("ORA-01033") Or errorMessage.Contains("ORA-12545") Then
            WhatHappened = "The IAIP experienced a connection error."
            WhatUserCanDo = "• Try closing and reloading the form. " & Environment.NewLine & Environment.NewLine
        ElseIf errorMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
            WhatHappened = "This computer has run out of memory."
            WhatUserCanDo = "• Try freeing up memory by closing other open computer applications." & Environment.NewLine & Environment.NewLine
        Else
            WhatHappened = "An error has occurred."
        End If

        WhatUserCanDo = WhatUserCanDo & "• Close and restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine & _
        "• If you continue to see this error, please email the DMU. Describe what you were doing and paste the error details below into your email."

        IaipExceptionManager.ShowErrorDialog(exc, WhatHappened, WhatUserCanDo)
    End Sub

    Private Function LogError(ByVal errorMessage As String, ByVal errorLocation As String) As Boolean
        If UserGCode = "" Then UserGCode = "0"

        Dim query As String = "INSERT INTO AIRBRANCH.IAIPERRORLOG " & _
            " (STRERRORNUMBER, STRUSER, STRERRORLOCATION, STRERRORMESSAGE, DATERRORDATE) " & _
            " values (AIRBRANCH.IAIPERRORNUMBER.NEXTVAL, :UserID, :ErrorLocation, :ErrorMessage, SYSDATE) "
        Dim parameters As OracleParameter() = New OracleParameter() { _
            New OracleParameter("UserID", UserGCode), _
            New OracleParameter("ErrorLocation", errorLocation), _
            New OracleParameter("ErrorMessage", errorMessage) _
        }

        Try
            Return DB.RunCommand(query, parameters)
        Catch ex As Exception
            Return False
        End Try
    End Function

End Module
