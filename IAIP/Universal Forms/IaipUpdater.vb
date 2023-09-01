Imports System.ComponentModel
Imports System.Deployment.Application

Public Class IaipUpdater

    Private ReadOnly ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
    Private updating As Boolean = True

    Protected Overrides Sub OnLoad(e As EventArgs)
        AddHandler ad.UpdateCompleted, AddressOf AdUpdateCompleted
        AddHandler ad.UpdateProgressChanged, AddressOf AdUpdateProgressChanged
        AddHandler ad.CheckForUpdateCompleted, AddressOf AdCheckForUpdateCompleted

        AddBreadcrumb("IaipUpdater: opened", Me)
        MyBase.OnLoad(e)

        ad.UpdateAsync()
    End Sub

    Private Sub AdCheckForUpdateCompleted(sender As Object, e As CheckForUpdateCompletedEventArgs)
        AddBreadcrumb("IaipUpdater: check completed", New Generic.Dictionary(Of String, Object) From {
                      {"CurrentVersion", ad.CurrentVersion}, {"UpdatedVersion", ad.UpdatedVersion}}, Me)

    End Sub

    Private Sub AdUpdateProgressChanged(sender As Object, e As DeploymentProgressChangedEventArgs)
        DownloadProgress.Value = e.ProgressPercentage
        If IsHandleCreated Then SetTaskbarProgressValue(Handle, e.ProgressPercentage, 100)
    End Sub

    Private Sub AdUpdateCompleted(sender As Object, e As AsyncCompletedEventArgs)
        updating = False

        If e.Error IsNot Nothing Then
            DownloadProgress.Visible = False

            If IsHandleCreated Then SetTaskbarProgressState(Handle, TaskbarState.Error)

            If TypeOf e.Error Is DeploymentDownloadException Then
                UpdaterStatus.Text = "Network Error: Could not install the latest IAIP version right now. " & vbNewLine &
                    "Please check your network connection or try again later."
            Else
                UpdaterStatus.Text = "ERROR: Could not install the IAIP update. " & vbNewLine &
                    "Please report this error to EPD-IT." & vbNewLine & vbNewLine &
                    "Error Message: " & vbNewLine & e.Error.Message
            End If

            UpdaterButton.Visible = True
            UpdaterButton.Focus()

            AddBreadcrumb("IaipUpdater: udpate error", New Generic.Dictionary(Of String, Object) From {{"Error Type", e.Error.GetType}}, Me)
            Return
        End If

        AddBreadcrumb("IaipUpdater: restarting", Me)
        Application.Restart()
    End Sub

    Private Sub UpdaterButton_Click(sender As Object, e As EventArgs) Handles UpdaterButton.Click
        CloseIaip()
    End Sub

    Private Sub IaipUpdater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If updating Then e.Cancel = True
    End Sub

End Class
