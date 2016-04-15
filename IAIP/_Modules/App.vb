Imports System.IO
Imports System.Reflection
Imports System.Deployment.Application

Public Module App

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

    Public Sub OpenMapUrl(ByVal addressString As String, Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenMap")
        OpenUri(New Uri(MapUrlFragment & addressString), objectSender)
    End Sub

    Public Sub OpenPermitSearchUrl(ByVal airsNumber As Apb.ApbFacilityId, Optional ByVal objectSender As Form = Nothing)
        monitor.TrackFeature("Url.OpenPermitSearch")
        OpenUri(New Uri(PermitSearchUrlFragment & airsNumber.ToString), objectSender)
    End Sub

    Private Function OpenUri(ByVal uriString As String, Optional ByVal objectSender As Object = Nothing) As Boolean
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        Try
            If objectSender IsNot Nothing Then objectSender.Cursor = Cursors.AppStarting
            If uriString Is Nothing OrElse uriString = "" Then Return False

            Process.Start(uriString)
            Return True
        Catch ee As Exception When _
        TypeOf ee Is System.ComponentModel.Win32Exception OrElse
        TypeOf ee Is System.ObjectDisposedException OrElse
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
                Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(thisAssembly.Location)
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

    '' Not currently used, but may be useful in the future
    'Private Function GetVersionAsMajorMinor(ByVal v As Version) As Version
    '    ' This converts a Version from four components to three
    '    If v.Build = -1 Then Return v ' (A version with fewer than three components gets returned as-is)
    '    Return New Version(v.Major, v.Minor)
    'End Function

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
                                & "Please check your network connection or try again later. " &
                                vbNewLine & vbNewLine & "Error: " + dde.Message,
                                "Error")
                Return
            Catch ioe As InvalidOperationException
                MessageBox.Show("This application cannot be updated. Please contact support for " &
                                "more information. " & vbNewLine & vbNewLine &
                                "Error: " & ioe.Message,
                                "Error")
                Return
            End Try

            If (info.UpdateAvailable) Then
                Dim doUpdate As Boolean = True

                If (Not info.IsUpdateRequired) Then
                    Dim dr As DialogResult
                    dr = MessageBox.Show("An update is available (" &
                                         GetVersionAsMajorMinorBuild(info.AvailableVersion).ToString &
                                         "). Would you like to install it now?",
                                         "Update Available", MessageBoxButtons.YesNo)
                    If (Not DialogResult.Yes = dr) Then doUpdate = False
                Else
                    ' Display a message that the app MUST reboot. Display the minimum required version.
                    MessageBox.Show("A mandatory update will now be installed (" &
                                    GetVersionAsMajorMinorBuild(info.AvailableVersion).ToString & "). ",
                                    "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                If (doUpdate) Then
                    Try
                        AD.Update()
                        'MessageBox.Show("The IAIP has been updated and will now restart.")
                        Application.Restart()
                    Catch dde As DeploymentDownloadException
                        MessageBox.Show("The IAIP cannot be updated right now. " & vbNewLine & vbNewLine &
                                        "Please check your network connection or try again later.")
                        Return
                    End Try
                End If
            Else
                MessageBox.Show("You have the latest version. No updates are available. :)",
                                "No Update Available")
            End If
        Else
            MessageBox.Show("Not running as a Network Deployed Application.",
                            "Error")
        End If
    End Sub

    Public ReadOnly Property JustUpdated As Boolean
        Get
            If (ApplicationDeployment.IsNetworkDeployed) Then
                Return ApplicationDeployment.CurrentDeployment.IsFirstRun
            Else
                Return False
            End If
        End Get
    End Property

#End Region

#Region " Crystal Reports "

    Public Sub TestCrystalReportsInstallation()
        Try
            System.Reflection.Assembly.Load("CrystalDecisions.Windows.Forms, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304")
        Catch ex As FileNotFoundException
            ShowCrystalReportsSupportMessage()
        End Try
    End Sub

    Public Sub ShowCrystalReportsSupportMessage()
        MessageBox.Show("You must install Crystal Reports in order to print reports. " &
                        "Click the Help button to download the installer, or contact the Data Management Unit for assistance.",
                        "Missing Crystal Reports Runtime",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2,
                        0,
                        "http://dmu.georgiaair.org/iaip/pre-install/")
    End Sub

#End Region

#Region " Alternate booleans "

    Public Enum EnableOrDisable
        Disable = False
        Enable = True
    End Enum

    Public Enum OpenOrClosed
        Closed = False
        Open = True
    End Enum

#End Region

End Module
