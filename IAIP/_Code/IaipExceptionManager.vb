Imports System.Reflection

Public Module IaipExceptionManager

    Public Sub ApplicationThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        ' Application.ThreadException Handler added in StartupShutdown.Init

        e.Exception.Data.Add("Sender", sender.ToString)
        e.Exception.Data.Add("Unrecoverable", True)
        HandleException(e.Exception, NameOf(ApplicationThreadException))

    End Sub

    Public Sub HandleException(ex As Exception, Optional context As String = Nothing)
        ' Log exception
#If Not DEBUG Then
        If context IsNot Nothing Then
            ExceptionLogger.Tags.Add("context", context)
        End If
        ExceptionLogger.Capture(New SharpRaven.Data.SentryEvent(ex))
        ExceptionLogger.Tags.Remove("context")
#End If

        Dim WhatHappened As String = "An unrecoverable error has occurred." & Environment.NewLine & Environment.NewLine &
            "The action you requested was not performed. When you click Exit, the IAIP will close."

        Dim WhatUserCanDo As String = "• Restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
            "• If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email."

        ShowErrorDialog(ex, WhatHappened, WhatUserCanDo, True)

        CloseIaip()
    End Sub

    '-- 
    '--  method to show error dialog
    '--
    Public Function ShowErrorDialog(exc As Exception,
                                           Optional WhatHappened As String = "",
                                           Optional WhatUserCanDo As String = "",
                                           Optional showExitButton As Boolean = False
                                           ) As DialogResult

        If WhatHappened = "" Then
            WhatHappened = "An unexpected error has occurred. The action you requested was not performed."
        End If

        If WhatUserCanDo = "" Then
            WhatUserCanDo = "Restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
            "If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email."
        End If

        Using ed As New ExceptionDialog
            With ed
                .ErrorMessage.Text = WhatHappened
                .ActionMessage.Text = WhatUserCanDo
                .ErrorDetails.Text = FormatExceptionForUser(exc)
                If showExitButton Then .Unrecoverable = True
            End With

            Return ed.ShowDialog()
        End Using
    End Function

    '--
    '-- turns exception into something an average user can hopefully
    '-- understand; still very technical
    '--
    Private Function FormatExceptionForUser(exc As Exception) As String
        Dim sb As New Text.StringBuilder
        With sb
            .Append("Detailed error information follows:")
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append(ExceptionToString(exc))
        End With
        Return sb.ToString
    End Function

    '--
    '-- translate exception object to string, with additional system info
    '--
    Private Function ExceptionToString(exc As Exception) As String
        Dim sb As New Text.StringBuilder

        If Not (exc.InnerException Is Nothing) Then
            '-- sometimes the original exception is wrapped in a more relevant outer exception
            '-- the detail exception is the "inner" exception
            '-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
            With sb
                .Append("(Inner Exception)")
                .Append(Environment.NewLine)
                .Append(ExceptionToString(exc.InnerException))
                .Append(Environment.NewLine)
                .Append("(Outer Exception)")
                .Append(Environment.NewLine)
            End With
        End If

        With sb
            '-- get general system and app information
            .Append(SysInfoToString)

            '-- get exception-specific information
            .Append("Exception Source:      ")
            Try
                .Append(exc.Source)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Type:        ")
            Try
                .Append(exc.GetType.FullName)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Message:     ")
            Try
                .Append(exc.Message)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Target Site: ")
            Try
                .Append(exc.TargetSite.Name)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            Try
                Dim x As String = EnhancedStackTrace(exc)
                .Append(x)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)
        End With

        Return sb.ToString
    End Function

    '--
    '-- gather some system information that is helpful to diagnosing
    '-- exception
    '--
    Private Function SysInfoToString(Optional blnIncludeStackTrace As Boolean = False) As String
        Dim objStringBuilder As New System.Text.StringBuilder

        With objStringBuilder

            .Append("Date and Time:         ")
            .Append(DateTime.Now)
            .Append(Environment.NewLine)

            .Append("Machine Name:          ")
            Try
                .Append(Environment.MachineName)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Current User:          ")
            .Append(UserIdentity)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)

            .Append("Application Domain:    ")
            Try
                .Append(System.AppDomain.CurrentDomain.FriendlyName())
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Assembly Version:      ")
            Try
                .Append(GetCurrentVersion.ToString)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)

            If blnIncludeStackTrace Then
                .Append(EnhancedStackTrace())
            End If

        End With

        Return objStringBuilder.ToString
    End Function

    '--
    '-- enhanced stack trace generator
    '--
    Private Function EnhancedStackTrace(objStackTrace As StackTrace,
        Optional strSkipClassName As String = "") As String
        Dim intFrame As Integer

        Dim sb As New System.Text.StringBuilder

        sb.Append(Environment.NewLine)
        sb.Append("---- Stack Trace ----")
        sb.Append(Environment.NewLine)

        For intFrame = 0 To objStackTrace.FrameCount - 1
            Dim sf As StackFrame = objStackTrace.GetFrame(intFrame)
            Dim mi As MemberInfo = sf.GetMethod

            If strSkipClassName <> "" AndAlso mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1 Then
                '-- don't include frames with this name
            Else
                sb.Append(StackFrameToString(sf))
            End If
        Next
        sb.Append(Environment.NewLine)

        Return sb.ToString
    End Function


    '--
    '-- enhanced stack trace generator (exception)
    '--
    Private Function EnhancedStackTrace(objException As Exception) As String
        Dim objStackTrace As New StackTrace(objException, True)
        Return EnhancedStackTrace(objStackTrace)
    End Function

    '--
    '-- enhanced stack trace generator (no params)
    '--
    Private Function EnhancedStackTrace() As String
        Dim objStackTrace As New StackTrace(True)
        Return EnhancedStackTrace(objStackTrace, "ExceptionManager")
    End Function

    '--
    '-- exception-safe WindowsIdentity.GetCurrent retrieval returns "domain\username"
    '-- per MS, this sometimes randomly fails with "Access Denied" particularly on NT4
    '--
    Private Function CurrentWindowsIdentity() As String
        Try
            Return System.Security.Principal.WindowsIdentity.GetCurrent.Name()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    '--
    '-- exception-safe "domain\username" retrieval from Environment
    '--
    Private Function CurrentEnvironmentIdentity() As String
        Try
            Return System.Environment.UserDomainName + "\" + System.Environment.UserName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    '--
    '-- retrieve identity with fallback on error to safer method
    '--
    Private Function UserIdentity() As String
        Dim strTemp As String
        strTemp = CurrentWindowsIdentity()
        If strTemp = "" Then
            strTemp = CurrentEnvironmentIdentity()
        End If
        Return strTemp
    End Function

    '--
    '-- turns a single stack frame object into an informative string
    '--
    Private Function StackFrameToString(sf As StackFrame) As String
        Dim sb As New System.Text.StringBuilder
        Dim intParam As Integer
        Dim mi As MemberInfo = sf.GetMethod

        With sb
            '-- build method name
            .Append("   ")
            .Append(mi.DeclaringType.Namespace)
            .Append(".")
            .Append(mi.DeclaringType.Name)
            .Append(".")
            .Append(mi.Name)

            '-- build method params
            Dim objParameters() As ParameterInfo = sf.GetMethod.GetParameters()
            Dim objParameter As ParameterInfo
            .Append("(")
            intParam = 0
            For Each objParameter In objParameters
                intParam += 1
                If intParam > 1 Then .Append(", ")
                .Append(objParameter.Name)
                .Append(" As ")
                .Append(objParameter.ParameterType.Name)
            Next
            .Append(")")
            .Append(Environment.NewLine)

            '-- if source code is available, append location info
            .Append("       ")
            .Append(System.IO.Path.GetFileName(sf.GetFileName))
            .Append(": line ")
            .Append(String.Format("{0:#0000}", sf.GetFileLineNumber))
            .Append(", col ")
            .Append(String.Format("{0:#00}", sf.GetFileColumnNumber))
            '-- if IL is available, append IL location info
            If sf.GetILOffset <> StackFrame.OFFSET_UNKNOWN Then
                .Append(", IL ")
                .Append(String.Format("{0:#0000}", sf.GetILOffset))
            End If
            .Append(Environment.NewLine)
        End With
        Return sb.ToString
    End Function

End Module
