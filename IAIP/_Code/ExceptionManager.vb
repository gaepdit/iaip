Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Reflection
Imports Daramee.TaskDialogSharp

Friend Module ExceptionManager

    Public Sub ApplicationThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        ' Application.ThreadException Handler added in StartupShutdown.Init
        e.Exception.Data.AddAsUniqueIfExists("Sender", sender.ToString)
        HandleUnhandledException(e.Exception, NameOf(ApplicationThreadException))
    End Sub

    Public Sub HandleUnhandledException(ex As Exception, context As String)
        ' Comes from Application.ThreadException Handler (above)
        ' or MyApplication.UnhandledException (ApplicationEvent.vb)
        ex.Data.AddAsUniqueIfExists("Unrecoverable", True)
        ErrorReport(ex, Nothing, context, True, True)
    End Sub

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="ex">The exception to be handled.</param>
    ''' <param name="contextMessage">A string representing the context of the error.</param>
    Public Sub ErrorReport(ex As Exception, contextMessage As String, Optional displayErrorToUser As Boolean = True)
        ErrorReport(ex, Nothing, contextMessage, displayErrorToUser)
    End Sub

    ''' <summary>
    ''' Handles logging and reporting of errors.
    ''' </summary>
    ''' <param name="ex">The exception to be handled.</param>
    ''' <param name="supplementalMessage">A string containing supplementary information to be logged.</param>
    ''' <param name="contextMessage">A string representing the calling function.</param>
    Public Sub ErrorReport(
            ex As Exception,
            supplementalMessage As String,
            contextMessage As String,
            Optional displayErrorToUser As Boolean = True,
            Optional unrecoverable As Boolean = False)

        If SeemsLikeACrystalReportsIssue(ex) Then
            ShowCrystalReportsSupportMessage()
            Return
        End If

        If SeemsLikeANetworkIssue(ex) Then
            ShowNetworkDownSupportMessage()
            Return
        End If

        ' First, log the exception.
        Dim logged As Boolean = LogException(ex, contextMessage, supplementalMessage)

        ' Second, display a dialog to the user describing the error and next steps.
        If displayErrorToUser Then
            Dim errorMessage As String = ex.Message

            If Not String.IsNullOrEmpty(supplementalMessage) Then
                errorMessage = errorMessage & Environment.NewLine & Environment.NewLine & supplementalMessage
            End If

            Dim whatHappened As String
            Dim whatUserCanDo As String = ""

            If errorMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
                whatHappened = "The IAIP is running multiple processing threads and needs time to complete them. Please allow time for the process to run."
                whatUserCanDo = "• Wait for the process to finish before continuing." & Environment.NewLine & Environment.NewLine
            ElseIf errorMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
                whatHappened = "This computer has run out of memory."
                whatUserCanDo = "• Try freeing up memory by closing other open computer applications." & Environment.NewLine & Environment.NewLine
            ElseIf unrecoverable Then
                whatHappened = "An unrecoverable error has occurred." & Environment.NewLine & Environment.NewLine &
                    "The action you requested was not performed. When you click Exit, the IAIP will close."
                whatUserCanDo = "• Restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
                    "• If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email." &
                    Environment.NewLine & Environment.NewLine
            Else
                whatHappened = "An error has occurred."
            End If

            whatUserCanDo = whatUserCanDo &
                "• Close and restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
                "• If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email."

            ShowErrorDialog(ex, whatHappened, whatUserCanDo, unrecoverable, logged)
        End If

    End Sub

    Public Function LogException(ex As Exception, contextMessage As String, supplementalMessage As String) As Boolean
        ' Only log if UAT or Prod
#If DEBUG Then
        Return False
#End If

        If Not String.IsNullOrEmpty(contextMessage) Then
            ExceptionLogger.Tags.AddAsUniqueIfExists("context", contextMessage)
        End If

        If Not String.IsNullOrEmpty(supplementalMessage) Then
            ex.Data.AddAsUniqueIfExists(NameOf(supplementalMessage), supplementalMessage)
        End If

        Try
            ExceptionLogger.Capture(New SharpRaven.Data.SentryEvent(ex))
        Catch ee As Exception
            Return False
        Finally
            ExceptionLogger.Tags.Remove("context")
        End Try

        Return True
    End Function

    Private Function SeemsLikeANetworkIssue(ex As Exception) As Boolean
        If ex.GetType() = GetType(SqlException) AndAlso
            (ex.Message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server.") OrElse
            ex.Message.Contains("A transport-level error has occurred when receiving results from the server.") OrElse
            ex.Message.Contains("The timeout period elapsed prior to completion of the operation or the server is not responding.")) Then
            Return True
        End If

        Return False
    End Function

    Private Function SeemsLikeACrystalReportsIssue(ex As Exception) As Boolean
        If (ex.GetType() = GetType(IO.FileNotFoundException) AndAlso
            ex.Message.Contains("Could not load file or assembly 'CrystalDecisions.CrystalReports.Engine,")) OrElse
            (ex.GetType() = GetType(CrystalDecisions.CrystalReports.Engine.LoadSaveReportException) AndAlso
            ex.Message.Contains("Could not load file or assembly 'CrystalDecisions.CrystalReports.Engine,")) OrElse
            (ex.GetType() = GetType(TypeInitializationException) AndAlso
            ex.Message.Contains("The type initializer for 'CrystalDecisions.CrystalReports.Engine.ReportDocument' threw an exception.")) Then
            Return True
        End If

        Return False
    End Function

    Private Sub ShowNetworkDownSupportMessage()
        Dim nav As IAIPNavigation = CType(SingleForm(IAIPNavigation.Name), IAIPNavigation)
        nav.CheckNetworkConnection()

        Using bgw As New BackgroundWorker
            AddHandler bgw.DoWork,
            Sub()
                Dim td As New TaskDialog With {
                    .Title = "Can't connect",
                    .MainIcon = TaskDialogIcon.Warning,
                    .MainInstruction = "The network appears to be down.",
                    .Content = "Please check your internet connection and try again. " &
                        "If you are working remotely, also check your VPN connection." & Environment.NewLine & Environment.NewLine &
                        "If you continue to receive this message, contact EPD-IT for support.",
                    .CommonButtons = TaskDialogCommonButtonFlags.OK,
                    .Buttons = {},
                    .UseCommandLinks = False
                }

                ' when the task dialog is closed, it throws an unhandled exception that ends 
                ' the background worker, but it still works as long as .Buttons is defined above
                td.Show()
            End Sub

            bgw.RunWorkerAsync()
        End Using
    End Sub

    '-- 
    '--  method to show error dialog
    '--
    Private Function ShowErrorDialog(ex As Exception,
                                     whatHappened As String,
                                     whatUserCanDo As String,
                                     unrecoverable As Boolean,
                                     logged As Boolean) As DialogResult

        If String.IsNullOrEmpty(whatHappened) Then
            whatHappened = "An unexpected error has occurred. The action you requested was not performed."
        End If

        If String.IsNullOrEmpty(whatUserCanDo) Then
            whatUserCanDo = "Restart the IAIP and try repeating your last action." & Environment.NewLine & Environment.NewLine &
                "If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email."
        End If

        Using ed As New ExceptionDialog
            With ed
                .ErrorMessage.Text = whatHappened
                .ActionMessage.Text = whatUserCanDo
                .ErrorDetails.Text = FormatExceptionForUser(ex)
                .Unrecoverable = unrecoverable
                .Logged = logged
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
            '-- see https://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
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
            Return Environment.UserDomainName & "\" & Environment.UserName
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
            Dim objParameters As ParameterInfo() = sf.GetMethod.GetParameters()
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
