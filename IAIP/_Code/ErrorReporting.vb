﻿Module ErrorReporting

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="exc">The exception to be handled.</param>
    ''' <param name="contextMessage">A string representing the context of the error.</param>
    Public Sub ErrorReport(exc As Exception, contextMessage As String, Optional displayErrorToUser As Boolean = True)
        ErrorReport(exc, Nothing, contextMessage, displayErrorToUser)
    End Sub

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="exc">The exception to be handled.</param>
    ''' <param name="supplementalMessage">A string containing supplementary information to be logged.</param>
    ''' <param name="contextMessage">A string representing the calling function.</param>
    Public Sub ErrorReport(exc As Exception, supplementalMessage As String, contextMessage As String, Optional displayErrorToUser As Boolean = True)

        ' First, log the exception.
#If Not DEBUG Then
        ExceptionLogger.Tags.Add("context", contextMessage)
        If Not String.IsNullOrEmpty(SupplementalMessage) Then
            exc.Data.Add(NameOf(SupplementalMessage), SupplementalMessage)
        End If
        ExceptionLogger.Capture(New SharpRaven.Data.SentryEvent(exc))
        ExceptionLogger.Tags.Remove("context")
#End If

        ' Second, display a dialog to the user describing the error and next steps.
        Dim errorMessage As String = exc.Message
        If Not String.IsNullOrEmpty(supplementalMessage) Then
            errorMessage = errorMessage & Environment.NewLine & Environment.NewLine & supplementalMessage
        End If

        If displayErrorToUser Then
            Dim WhatHappened As String = ""
            Dim WhatUserCanDo As String = ""

            If errorMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
                WhatHappened = "The IAIP is running multiple processing threads and needs time to complete them. Please allow time for the process to run."
                WhatUserCanDo = "• Wait for the process to finish before continuing." & Environment.NewLine & Environment.NewLine
            ElseIf errorMessage.Contains("ORA-") Then
                WhatHappened = "The IAIP experienced a database connection error."
                WhatUserCanDo = "• Check your Internet connection. " & Environment.NewLine & Environment.NewLine &
                        "• If operating from a remote location, check your VPN connection. " & Environment.NewLine & Environment.NewLine
            ElseIf errorMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
                WhatHappened = "This computer has run out of memory."
                WhatUserCanDo = "• Try freeing up memory by closing other open computer applications." & Environment.NewLine & Environment.NewLine
            Else
                WhatHappened = "An error has occurred."
            End If

            WhatUserCanDo = WhatUserCanDo & "• Close and restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
                    "• If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email."

            IaipExceptionManager.ShowErrorDialog(exc, WhatHappened, WhatUserCanDo)
        End If

    End Sub

End Module
