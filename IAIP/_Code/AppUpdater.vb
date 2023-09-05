Imports System.Deployment.Application
Imports System.Linq

Public Module AppUpdater

    Public Function CheckForUpdate() As Boolean
        AddBreadcrumb("AppUpdater: CheckForUpdate started")

        Dim openFormCount As Integer = 0
        Dim okayForms As String() = {NameOf(IAIPLogIn), NameOf(IAIPNavigation), NameOf(IaipAbout)}

        For Each f As Form In Application.OpenForms
            If Not okayForms.Contains(f.Name) Then openFormCount += 1
        Next

        If openFormCount > 0 Then
            Return False
        End If

        Dim info As UpdateCheckInfo

        If Not ApplicationDeployment.IsNetworkDeployed Then
            AddBreadcrumb("AppUpdater: Not Network Deployed")
            Return False
        End If

        Dim ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

        Try
            info = ad.CheckForDetailedUpdate(False)
        Catch dde As DeploymentDownloadException
            AddBreadcrumb("AppUpdater: DeploymentDownloadException")
            MessageBox.Show("The IAIP cannot be updated right now. " & vbNewLine & vbNewLine &
                            "Please check your network connection or try again later. " & vbNewLine & vbNewLine &
                            "Error: " & dde.Message,
                            "Error")
            Return False
        Catch ioe As InvalidOperationException
            AddBreadcrumb("AppUpdater: InvalidOperationException")
            MessageBox.Show("This application cannot be updated. Please contact support for more information. " & vbNewLine & vbNewLine &
                            "Error: " & ioe.Message,
                            "Error")
            Return False
        End Try

        If Not info.UpdateAvailable Then
            AddBreadcrumb("AppUpdater: No update available")
            Return False
        End If

        Dim doUpdate As Boolean = True

        If info.IsUpdateRequired Then
            AddBreadcrumb("AppUpdater: Update required")
        Else
            AddBreadcrumb("AppUpdater: Update available")
            Dim dr As DialogResult
            dr = MessageBox.Show("An update is available (" &
                                 GetVersionAsMajorMinorBuild(info.AvailableVersion).ToString &
                                 "). Would you like to install it now?",
                                 "Update Available", MessageBoxButtons.YesNo)
            doUpdate = dr = DialogResult.Yes
        End If

        If Not doUpdate Then Return False

        Using updateForm As New IaipUpdater
            updateForm.ShowDialog()
            Return True
        End Using

    End Function

End Module
