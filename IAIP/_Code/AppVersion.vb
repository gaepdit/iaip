Imports System.Deployment.Application
Imports System.Linq
Imports System.Reflection

Public Module AppVersion

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

    Private Function GetVersionAsMajorMinorBuild(v As Version) As Version
        ' This converts a Version from four components to three
        If v.Revision = -1 Then Return v ' (A version with fewer than four components gets returned as-is)
        Return New Version(v.Major, v.Minor, v.Build)
    End Function

#End Region

#Region " App updater "

    Public Sub CheckForUpdate()
        Dim openFormCount As Integer = 0
        Dim okayForms As String() = New String() {NameOf(IAIPLogIn), NameOf(IAIPNavigation), NameOf(IaipAbout)}

        For Each f As Form In Application.OpenForms
            If Not (okayForms.Contains(f.Name)) Then
                openFormCount += 1
            End If
        Next

        If openFormCount > 0 Then
            MessageBox.Show("The IAIP cannot be updated if multiple IAIP windows are open. Please close them and try again",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ShowAllForms()
            Exit Sub
        End If

        Dim info As UpdateCheckInfo

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

End Module
