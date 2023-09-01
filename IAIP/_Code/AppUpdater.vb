Imports System.Deployment.Application
Imports System.Linq

Public Module AppUpdater

    Public Sub CheckForUpdate(Optional silent As Boolean = False)
        AddBreadcrumb($"AppUpdater: CheckForUpdate started")

        Dim openFormCount As Integer = 0
        Dim okayForms As String() = {NameOf(IAIPLogIn), NameOf(IAIPNavigation), NameOf(IaipAbout)}

        For Each f As Form In Application.OpenForms
            If Not (okayForms.Contains(f.Name)) Then openFormCount += 1
        Next

        If openFormCount > 0 Then
            If Not silent Then
                MessageBox.Show("The IAIP cannot be updated if multiple IAIP windows are open. Please close them and try again",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ShowAllForms()
            End If
            Return
        End If

        Dim info As UpdateCheckInfo

        If Not ApplicationDeployment.IsNetworkDeployed Then
            AddBreadcrumb($"AppUpdater: Not Network Deployed")
            If Not silent Then MessageBox.Show("Not running as a Network Deployed Application.", "Error")
            Return
        End If

        Dim ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

        Try
            info = ad.CheckForDetailedUpdate(False)
        Catch dde As DeploymentDownloadException
            AddBreadcrumb($"AppUpdater: DeploymentDownloadException")
            MessageBox.Show("The IAIP cannot be updated right now. " & vbNewLine & vbNewLine &
                            "Please check your network connection or try again later. " & vbNewLine & vbNewLine &
                            "Error: " & dde.Message,
                            "Error")
            Return
        Catch ioe As InvalidOperationException
            AddBreadcrumb($"AppUpdater: InvalidOperationException")
            MessageBox.Show("This application cannot be updated. Please contact support for more information. " & vbNewLine & vbNewLine &
                            "Error: " & ioe.Message,
                            "Error")
            Return
        End Try

        If Not info.UpdateAvailable Then
            AddBreadcrumb($"AppUpdater: No update available")
            If Not silent Then MessageBox.Show("You have the latest version. No updates are available. :)",
                                               "No Update Available")
            Return
        End If

        Dim doUpdate As Boolean = True

        If info.IsUpdateRequired Then
            AddBreadcrumb($"AppUpdater: Update required")
        Else
            AddBreadcrumb($"AppUpdater: Update available")
            Dim dr As DialogResult
            dr = MessageBox.Show("An update is available (" &
                                 GetVersionAsMajorMinorBuild(info.AvailableVersion).ToString &
                                 "). Would you like to install it now?",
                                 "Update Available", MessageBoxButtons.YesNo)
            doUpdate = dr = DialogResult.Yes
        End If

        If doUpdate Then
            Using updateForm As New IaipUpdater
                updateForm.Mandatory = info.IsUpdateRequired
                updateForm.ShowDialog()
            End Using
        End If
    End Sub

End Module
