Imports System.ComponentModel
Imports System.Deployment.Application

Public Class IaipUpdater

    Public Property Mandatory As Boolean
    Private ReadOnly ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
    Private updating As Boolean = True

    Protected Overrides Sub OnLoad(e As EventArgs)
        AddHandler ad.UpdateCompleted, AddressOf ad_UpdateCompleted
        AddHandler ad.UpdateProgressChanged, AddressOf ad_UpdateProgressChanged
        AddHandler ad.CheckForUpdateCompleted, AddressOf ad_CheckForUpdateCompleted

        updaterButton.Visible = Not Mandatory

        AddBreadcrumb($"IaipUpdater: opened", New Generic.Dictionary(Of String, Object) From {{"Mandatory", Mandatory}}, Me)
        MyBase.OnLoad(e)

        ad.UpdateAsync()
    End Sub

    Private Sub ad_CheckForUpdateCompleted(sender As Object, e As CheckForUpdateCompletedEventArgs)
        AddBreadcrumb($"IaipUpdater: check completed",
                      New Generic.Dictionary(Of String, Object) From {
                      {"CurrentVersion", ad.CurrentVersion}, {"UpdatedVersion", ad.UpdatedVersion}}, Me)

    End Sub

    Private Sub ad_UpdateProgressChanged(sender As Object, e As DeploymentProgressChangedEventArgs)
        downloadProgress.Value = e.ProgressPercentage
        If IsHandleCreated Then
            SetTaskbarProgressValue(Handle, e.ProgressPercentage, 100)
        End If
    End Sub

    Private Sub ad_UpdateCompleted(sender As Object, e As AsyncCompletedEventArgs)
        updating = False

        If e.Cancelled Then
            downloadProgress.Value = 0
            If IsHandleCreated Then
                SetTaskbarProgressState(Handle, TaskbarState.Paused)
            End If

            updaterStatus.Text = "The IAIP update was cancelled."
            updaterButton.Text = "Close"
            updaterButton.Visible = True
            updaterButton.Focus()

            AddBreadcrumb($"IaipUpdater: update canceled", Me)
            Return
        End If

        If e.Error IsNot Nothing Then
            downloadProgress.Visible = False
            If IsHandleCreated Then
                SetTaskbarProgressState(Handle, TaskbarState.Error)
            End If

            If TypeOf e.Error Is DeploymentDownloadException Then
                updaterStatus.Text = "Network Error: Could not install the latest IAIP version right now. " & vbNewLine &
                    "Please check your network connection or try again later."
            Else
                updaterStatus.Text = "ERROR: Could not install the IAIP update. " & vbNewLine &
                    "Please report this error to the system administrator." & vbNewLine & vbNewLine &
                    "Error Message: " & vbNewLine & e.Error.Message
            End If

            updaterButton.Text = "Close"
            updaterButton.Visible = True
            updaterButton.Focus()

            AddBreadcrumb($"IaipUpdater: udpate error", New Generic.Dictionary(Of String, Object) From {{"Error Type", e.Error.GetType}}, Me)
            Return
        End If

        AddBreadcrumb($"IaipUpdater: restarting", Me)
        Application.Restart()
    End Sub

    Private Sub updaterButton_Click(sender As Object, e As EventArgs) Handles updaterButton.Click
        If updaterButton.Text = "Cancel" Then
            ad.UpdateAsyncCancel()
        ElseIf Mandatory Then
            CloseIaip()
        Else
            Close()
        End If
    End Sub

    Private Sub IaipUpdater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If updating Then
            e.Cancel = True
        End If
    End Sub

End Class