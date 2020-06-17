'--
'-- Generic user error dialog
'--
'-- UI adapted from
'--
'-- Alan Cooper's "About Face: The Essentials of User Interface Design"
'-- Chapter VII, "The End of Errors", pages 423-440
'--
'-- Jeff Atwood
'-- https://www.codinghorror.com
'--
Imports System.Reflection

Friend Class ExceptionDialog
    Inherits Form

    Public Property MyException As Exception
    Public Property Unrecoverable As Boolean
    Public Property Logged As Boolean

    Private Const _intSpacing As Integer = 10
    Private Const _detailsHeight As Integer = 200

    Private Sub UserErrorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '-- make sure our window is on top
        TopMost = True
        TopMost = False

        '-- show whether unrecoverable 
        If Unrecoverable Then
            btnOK.Text = "Exit"
            Icon = My.Resources.ErrorIcon
        End If

        DisplayMessages()

        '-- size the labels' height to accommodate the amount of text in them
        ErrorDetails.Anchor = AnchorStyles.None
        SizeBox(ErrorDescription)
        SizeBox(ActionMessage)

        '-- now shift everything up
        ActionHeading.Top = ErrorDescription.Top + ErrorDescription.Height + _intSpacing
        ActionMessage.Top = ActionHeading.Top + ActionHeading.Height + _intSpacing
        btnCopy.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing + _intSpacing - 3
        btnOK.Top = ActionMessage.Top + ActionMessage.Height + _intSpacing + _intSpacing - 3

        '-- now shift bottom of dialog up to just below the ErrorDetails box
        ClientSize = New Size(ClientSize.Width, btnOK.Top + btnOK.Height + _intSpacing + _detailsHeight)

        With ErrorDetails
            .Location = New Point(btnOK.Left, btnOK.Top + btnOK.Height + _intSpacing)
            .Height = ClientSize.Height - ErrorDetails.Top - _intSpacing
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        End With

        CenterToScreen()
    End Sub

    Private Sub DisplayMessages()
        '-- show whether logged
        If Not Logged Then
            IntroMessage.Text = "A copy of this error message has NOT been sent to EPD-IT."
        End If

        Dim exMessage As String = MyException.Message

        Dim whatHappened As String
        Dim whatUserCanDo As String

        If exMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
            whatHappened = "The IAIP is running multiple processing threads and needs time to complete them. Please allow time for the process to run."
            whatUserCanDo = "• Wait for the process to finish before continuing." &
                Environment.NewLine & Environment.NewLine
        ElseIf exMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
            whatHappened = "This computer has run out of memory."
            whatUserCanDo = "• Try freeing up memory by closing other open computer applications." & Environment.NewLine & Environment.NewLine
        ElseIf Unrecoverable Then
            whatHappened = "An unrecoverable error has occurred." &
                Environment.NewLine & Environment.NewLine &
                "The action you requested was not performed. When you click Exit, the IAIP will close."
            whatUserCanDo = "• Restart the IAIP and try repeating your last action." &
                Environment.NewLine & Environment.NewLine &
                "• If you continue to see this error, please email EPD IT. Describe what you were doing and paste the error details below into your email." &
                Environment.NewLine & Environment.NewLine
        Else
            whatHappened = "An unexpected error has occurred. The action you requested was not performed."
            whatUserCanDo = ""
        End If

        whatUserCanDo &= "• Close and restart the IAIP and try repeating your last action." &
            Environment.NewLine & Environment.NewLine &
            "• If you continue to see this error, please contact EPD IT. Describe what you were doing and paste the error details below into your email."

        ErrorDescription.Text = whatHappened
        ActionMessage.Text = whatUserCanDo
        ErrorDetails.Text = FormatExceptionForUser(MyException)
    End Sub

    Private Shared Sub SizeBox(ctl As TextBox)
        Try
            '-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
            Using g As Graphics = Graphics.FromHwnd(ctl.Handle)
                Dim objSizeF As SizeF = g.MeasureString(ctl.Text, ctl.Font, New SizeF(ctl.Width, ctl.Height))
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5
            End Using
        Catch ex As Security.SecurityException
            '-- do nothing; we can't set control sizes without full trust
        End Try
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Clipboard.SetText(ErrorDetails.Text)
        btnCopy.Text = "Copied!"
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Unrecoverable Then
            CloseIaip()
        Else
            Close()
        End If
    End Sub

#Region " Exception formatting "
    '--
    '-- turns exception into something an average user can hopefully
    '-- understand; still very technical
    '--
    Private Shared Function FormatExceptionForUser(exc As Exception) As String
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
    Private Shared Function ExceptionToString(exc As Exception) As String
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
    Private Shared Function SysInfoToString(Optional blnIncludeStackTrace As Boolean = False) As String
        Dim objStringBuilder As New Text.StringBuilder

        With objStringBuilder

            .Append("Date and Time:         ")
            .Append(Now)
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
                .Append(AppDomain.CurrentDomain.FriendlyName())
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
    Private Shared Function EnhancedStackTrace(objStackTrace As StackTrace,
        Optional strSkipClassName As String = "") As String
        Dim intFrame As Integer

        Dim sb As New Text.StringBuilder

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
    Private Shared Function EnhancedStackTrace(objException As Exception) As String
        Dim objStackTrace As New StackTrace(objException, True)
        Return EnhancedStackTrace(objStackTrace)
    End Function

    '--
    '-- enhanced stack trace generator (no params)
    '--
    Private Shared Function EnhancedStackTrace() As String
        Dim objStackTrace As New StackTrace(True)
        Return EnhancedStackTrace(objStackTrace, "ExceptionManager")
    End Function

    '--
    '-- exception-safe WindowsIdentity.GetCurrent retrieval returns "domain\username"
    '-- per MS, this sometimes randomly fails with "Access Denied" particularly on NT4
    '--
    Private Shared Function CurrentWindowsIdentity() As String
        Try
            Return Security.Principal.WindowsIdentity.GetCurrent.Name()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    '--
    '-- exception-safe "domain\username" retrieval from Environment
    '--
    Private Shared Function CurrentEnvironmentIdentity() As String
        Try
            Return Environment.UserDomainName & "\" & Environment.UserName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    '--
    '-- retrieve identity with fallback on error to safer method
    '--
    Private Shared Function UserIdentity() As String
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
    Private Shared Function StackFrameToString(sf As StackFrame) As String
        Dim sb As New Text.StringBuilder
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
            .Append(IO.Path.GetFileName(sf.GetFileName))
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

#End Region

End Class