Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Reflection
Imports System.Deployment.Application

Module App

#Region " URL handling "

    Public Sub OpenDocumentationUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenHelp")
        OpenUri(DocumentationUrl, objectSender)
    End Sub

    Public Sub OpenSupportUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenSupport")
        OpenUri(SupportUrl, objectSender)
    End Sub

    Public Sub OpenChangelogUrl(Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenChangelog")
        OpenUri(ChangelogUrl, objectSender)
    End Sub

    Public Function OpenUri(ByVal uriString As String, Optional ByVal objectSender As Object = Nothing) As Boolean
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        Try
            If objectSender IsNot Nothing Then objectSender.Cursor = Cursors.AppStarting
            If uriString Is Nothing OrElse uriString = "" Then Return False

            Process.Start(uriString)
            Return True
        Catch ee As Exception When _
        TypeOf ee Is System.ComponentModel.Win32Exception OrElse _
        TypeOf ee Is System.ObjectDisposedException OrElse _
        TypeOf ee Is System.IO.FileNotFoundException
            Return False
        Finally
            If objectSender IsNot Nothing Then objectSender.Cursor = Nothing
        End Try
    End Function

    Public Function OpenUri(ByVal uri As Uri, Optional ByVal objectSender As Object = Nothing) As Boolean
        Return OpenUri(uri.ToString, objectSender)
    End Function

#End Region

#Region " Versioning Info "
    Friend CurrentVersion As Version = Nothing

    Public Function GetCurrentVersion() As Version
        ' This is the currently installed (running) version

        If CurrentVersion Is Nothing Then
            If ApplicationDeployment.IsNetworkDeployed Then
                CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion
            Else
                Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
                Dim fileVersionInfo As FileVersionInfo = fileVersionInfo.GetVersionInfo(thisAssembly.Location)
                CurrentVersion = New Version(fileVersionInfo.FileVersion)
            End If
        End If

        Return CurrentVersion
    End Function

    Public Function GetCurrentVersionAsMajorMinorBuild() As Version
        Return GetVersionAsMajorMinorBuild(GetCurrentVersion)
    End Function

    Private Function GetVersionAsMajorMinorBuild(ByVal v As Version) As Version
        ' This converts a Version from four components to three
        If v.Revision = -1 Then Return v ' (A version with fewer than four components gets returned as-is)
        Return New Version(v.Major, v.Minor, v.Build)
    End Function

    Private Function GetVersionAsMajorMinor(ByVal v As Version) As Version
        ' This converts a Version from four components to three
        If v.Build = -1 Then Return v ' (A version with fewer than three components gets returned as-is)
        Return New Version(v.Major, v.Minor)
    End Function

#End Region

#Region " String functions "

    Public Function ConcatNonEmptyStrings(ByVal separator As String, ByVal value() As String) As String
        Return String.Join(separator, Array.FindAll(value, Function(s) Not String.IsNullOrEmpty(s)))
    End Function

    Public Function FormatStringAsPhoneNumber(ByVal p As String) As String
        If p Is Nothing Then Return p
        If Not System.Text.RegularExpressions.Regex.IsMatch(p, "^[0-9 ]+$") Then Return p
        If Not (p.Length = 7 Or p.Length >= 10) Then Return p

        If p.Length = 7 Then
            Return p.Substring(0, 3) & "-" & p.Substring(4, 4)
        ElseIf p.Length = 10 Then
            Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4)
        Else
            Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4) & " Ext. " & p.Substring(10, p.Length - 10)
        End If
    End Function

#End Region

#Region " Date functions "

    Public Function NormalizeDate(ByVal d As Date?) As Date?
        ' Converts a date to Nothing if date is equal to #7/4/1776#

        If d.Equals(CType(Nothing, Date)) Then Return d
        If Not IsDate(d) Then Return Nothing
        If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
        Return d
    End Function

#End Region

#Region " Control procedures "

    ''' <summary>
    ''' Disables and hides a Control by setting its .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="control">The Control to disable and hide</param>
    Public Sub DisableAndHide(ByVal control As Control)
        If control IsNot Nothing Then
            With control
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

    ''' <summary>
    ''' Disables and hides all Controls in an array by setting their .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="controls">An array of Controls to disable and hide</param>
    Public Sub DisableAndHide(ByVal controls As Control())
        For Each control As Control In controls
            DisableAndHide(control)
        Next
    End Sub

    ''' <summary>
    ''' Enables and shows a Control by setting its .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="control">The Control to enable and show</param>
    Public Sub EnableAndShow(ByVal control As Control)
        If control IsNot Nothing Then
            With control
                .Enabled = True
                .Visible = True
            End With
        End If
    End Sub

    ''' <summary>
    ''' Enables and shows all Controls in an array by setting their .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="controls">An array of Controls to enable and show</param>
    Public Sub EnableAndShow(ByVal controls As Control())
        For Each control As Control In controls
            EnableAndShow(control)
        Next
    End Sub

#End Region

#Region " MenuItem procedures "

    ''' <summary>
    ''' Disables and hides a MenuItem by setting its .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="menuItem">The MenuItem to disable and hide</param>
    Public Sub DisableAndHide(ByVal menuItem As MenuItem)
        If menuItem IsNot Nothing Then
            With menuItem
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

    ''' <summary>
    ''' Disables and hides all Controls in an array by setting their .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="menuItems">An array of controls to disable and hide</param>
    Public Sub DisableAndHide(ByVal menuItems As MenuItem())
        For Each menuItem As MenuItem In menuItems
            DisableAndHide(menuItem)
        Next
    End Sub

    ''' <summary>
    ''' Enables and shows a MenuItem by setting its .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="menuItem">The menuItem to enable and show</param>
    Public Sub EnableAndShow(ByVal menuItem As MenuItem)
        If menuItem IsNot Nothing Then
            With menuItem
                .Enabled = True
                .Visible = True
            End With
        End If
    End Sub

    ''' <summary>
    ''' Enables and shows all Controls in an array by setting their .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="menuItems">An array of controls to enable and show</param>
    Public Sub EnableAndShow(ByVal menuItems As MenuItem())
        For Each menuItem As MenuItem In menuItems
            EnableAndShow(menuItem)
        Next
    End Sub

#End Region

#Region " App updater "

    Public Sub CheckForUpdate()
        Dim info As UpdateCheckInfo = Nothing

        If (ApplicationDeployment.IsNetworkDeployed) Then
            Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

            Try
                info = AD.CheckForDetailedUpdate()
            Catch dde As DeploymentDownloadException
                MessageBox.Show("The IAIP cannot be updated right now. " & vbNewLine & vbNewLine _
                                & "Please check your network connection or try again later. " & _
                                vbNewLine & vbNewLine & "Error: " + dde.Message, _
                                "Error")
                Return
            Catch ioe As InvalidOperationException
                MessageBox.Show("This application cannot be updated. Please contact support for " & _
                                "more information. " & vbNewLine & vbNewLine & _
                                "Error: " & ioe.Message, _
                                "Error")
                Return
            End Try

            If (info.UpdateAvailable) Then
                Dim doUpdate As Boolean = True

                If (Not info.IsUpdateRequired) Then
                    Dim dr As DialogResult
                    dr = MessageBox.Show("An update is available (" & _
                                         info.AvailableVersion.ToString & "). Would you like to install it now?", _
                                         "Update Available", MessageBoxButtons.YesNo)
                    If (Not System.Windows.Forms.DialogResult.Yes = dr) Then doUpdate = False
                Else
                    ' Display a message that the app MUST reboot. Display the minimum required version.
                    MessageBox.Show("A mandatory update will now be installed (" & info.AvailableVersion.ToString & "). ", _
                                    "Update Available", MessageBoxButtons.OK, _
                                    MessageBoxIcon.Information)
                End If

                If (doUpdate) Then
                    Try
                        AD.Update()
                        MessageBox.Show("The IAIP has been updated and will now restart.")
                        Application.Restart()
                    Catch dde As DeploymentDownloadException
                        MessageBox.Show("The IAIP cannot be updated right now. " & vbNewLine & vbNewLine & _
                                        "Please check your network connection or try again later.")
                        Return
                    End Try
                End If
            Else
                MessageBox.Show("You have the latest version. No updates are available. :)", _
                                "No Update Available")
            End If
        Else
            MessageBox.Show("Not running as a Network Deployed Application.", _
                            "Error")
        End If
    End Sub

#End Region

#Region " App timers "

#Region " Database ping timer "

    Private DbPingTimer As Timers.Timer

    Private Sub DbPingTimerElapsed()
        Dim result As Boolean = DB.PingDBConnection(CurrentConnection)
        If Not result Then
            MessageBox.Show("The database connection has been lost. " & vbNewLine & _
                            "Please close and restart the IAIP.", _
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

#End Region

#Region " App duration timer "

    Private AppDurationTimer As Timers.Timer
    Private ShutdownWarningTimer As Timers.Timer

    Private Sub AppDurationTimerElapsed()
        StartTimer(ShutdownWarningTimer, 1000 * 60 * 5, AddressOf ShutdownWarningTimerElapsed, False) ' 5 minutes 

        Dim result As DialogResult
        result = MessageBox.Show("The IAIP has been open for three hours. " & vbNewLine & _
                                 "Do you want to continue to use it?" & vbNewLine & _
                                    vbNewLine & _
                                 "(The IAIP will be automatically terminated " & vbNewLine & _
                                 "in five minutes.)", _
                                 "Are you still there?", MessageBoxButtons.YesNo, _
                                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

        Select Case result
            Case DialogResult.Yes
                StopTimer(ShutdownWarningTimer)
                AppDurationTimer.Start()
            Case DialogResult.No
                StartupShutdown.CloseIaip()
        End Select
    End Sub

    Private Sub ShutdownWarningTimerElapsed()
        StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " NADC cutover timer "

    Private NadcCutoverTimer As Timers.Timer
    Private Enum NadcCutoverTimerState
        Init
        FirstWarning
        FinalWarning
    End Enum
    Private NadcCutoverTimerStatus As NadcCutoverTimerState = NadcCutoverTimerState.Init

    Private Sub StartNadcCutoverTimer()
        If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
            ' First time span ends 30 minutes before NADC_CUTOVER_DATETIME
            Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now).Subtract(TimeSpan.FromMinutes(30))

            StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
            NadcCutoverTimerStatus = NadcCutoverTimerState.Init
        End If
    End Sub

    Private Sub NadcCutoverTimerElapsed()
        Select Case NadcCutoverTimerStatus

            Case NadcCutoverTimerState.Init ' Occurs 30 minutes before NADC_CUTOVER_DATETIME
                If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
                    ' Second time span ends 5 minutes before NADC_CUTOVER_DATETIME
                    Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now).Subtract(TimeSpan.FromMinutes(5))

                    StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
                    NadcCutoverTimerStatus = NadcCutoverTimerState.FirstWarning

                    MessageBox.Show("The IAIP will shut down at 5pm EDT on Friday, " & vbNewLine & _
                                    "May 2, 2014. Service will resume on Monday " & vbNewLine & _
                                    "morning (May 5). Please finish your work. " & vbNewLine & _
                                    vbNewLine & _
                                    "Thank you for your patience and understanding. ", _
                                    "Service interruption: 30-minute warning", MessageBoxButtons.OK, _
                                             MessageBoxIcon.Warning)
                End If

            Case NadcCutoverTimerState.FirstWarning ' Occurs 5 minutes before NADC_CUTOVER_DATETIME
                If DateTime.Now < DB.NADC_CUTOVER_DATETIME Then
                    ' Third time span ends at NADC_CUTOVER_DATETIME
                    Dim span As TimeSpan = (DB.NADC_CUTOVER_DATETIME - DateTime.Now)

                    StartTimer(NadcCutoverTimer, span.TotalMilliseconds, AddressOf NadcCutoverTimerElapsed, False)
                    NadcCutoverTimerStatus = NadcCutoverTimerState.FinalWarning

                    MessageBox.Show("The IAIP will shut down at 5pm EDT on Friday, " & vbNewLine & _
                                    "May 2, 2014. Service will resume on Monday " & vbNewLine & _
                                    "morning (May 5). Please finish your work and " & vbNewLine & _
                                    "close the IAIP. " & vbNewLine & _
                                    vbNewLine & _
                                    "This is your final notice. " & vbNewLine & _
                                    vbNewLine & _
                                    "Thank you for your patience and understanding. ", _
                                    "Service interruption: 5-MINUTE WARNING", MessageBoxButtons.OK, _
                                             MessageBoxIcon.Warning)
                End If

            Case NadcCutoverTimerState.FinalWarning ' Occurs at NADC_CUTOVER_DATETIME
                StartupShutdown.CloseIaip()

        End Select
    End Sub

#End Region

#Region " Start and stop timers "

    Public Sub StartAppTimers()
        StartTimer(AppDurationTimer, 1000 * 60 * 60 * 3, AddressOf AppDurationTimerElapsed, False) ' 3 hours 
        StartTimer(DbPingTimer, 1000 * 60 * 2, AddressOf DbPingTimerElapsed) ' 2 minutes (for testing purposes)
        'StartTimer(DbPingTimer, 1000 * 60 * 45, AddressOf DbPingTimerElapsed) ' 45 minutes 
        StartNadcCutoverTimer()
    End Sub

    Public Sub StopAppTimers()
        StopTimer(AppDurationTimer)
        StopTimer(DbPingTimer)
        StopTimer(ShutdownWarningTimer)
        StopTimer(NadcCutoverTimer)
    End Sub

    ''' <summary>
    ''' Sets up and starts a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to start.</param>
    ''' <param name="interval">The time in milliseconds between events.</param>
    ''' <param name="timerElapsedEventHandler">The procedure to handle the Timer Elapsed event.</param>
    ''' <param name="autoReset">A value indicating whether the Timer Elapsed event should recur or not.</param>
    ''' <remarks></remarks>
    Private Sub StartTimer(ByRef timer As Timers.Timer, ByVal interval As Double, _
                           ByVal timerElapsedEventHandler As Timers.ElapsedEventHandler, _
                           Optional ByVal autoReset As Boolean = True)
        timer = New Timers.Timer(interval)
        AddHandler timer.Elapsed, timerElapsedEventHandler
        timer.Enabled = True
        timer.AutoReset = autoReset
    End Sub

    ''' <summary>
    ''' Stops and disposes of a Timer.
    ''' </summary>
    ''' <param name="timer">The Timer to stop</param>
    Private Sub StopTimer(ByVal timer As Timers.Timer)
        If timer IsNot Nothing Then
            timer.Enabled = False
            timer.Dispose()
        End If
    End Sub

#End Region

#End Region

End Module
