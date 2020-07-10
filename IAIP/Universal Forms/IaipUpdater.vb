Imports System.ComponentModel
Imports System.Deployment.Application

Public Class IaipUpdater

    Public Property Mandatory As Boolean
    Private ReadOnly ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

    Private Sub IaipUpdater_Load(sender As Object, e As EventArgs) Handles Me.Load
        updaterButton.Visible = Not Mandatory
        BeginUpdate()
    End Sub

    Private Sub BeginUpdate()
        AddHandler ad.UpdateCompleted, AddressOf ad_UpdateCompleted
        AddHandler ad.UpdateProgressChanged, AddressOf ad_UpdateProgressChanged

        SetTaskbarProgressState(Handle, TaskbarState.Normal)
        ad.UpdateAsync()
    End Sub

    Private Sub ad_UpdateProgressChanged(sender As Object, e As DeploymentProgressChangedEventArgs)
        downloadProgress.Value = e.ProgressPercentage
        SetTaskbarProgressValue(Handle, e.ProgressPercentage, 100)
    End Sub

    Private Sub ad_UpdateCompleted(sender As Object, e As AsyncCompletedEventArgs)
        If e.Cancelled Then
            downloadProgress.Visible = False
            SetTaskbarProgressState(Handle, TaskbarState.Paused)

            updaterStatus.Text = "The IAIP update was cancelled."
            updaterButton.Text = "Close"
            updaterButton.Focus()

            Return
        End If

        If e.Error IsNot Nothing Then
            downloadProgress.Visible = False
            SetTaskbarProgressState(Handle, TaskbarState.Error)

            If TypeOf e.Error Is DeploymentDownloadException Then
                updaterStatus.Text = "Network Error: Could not install the latest IAIP version right now. " & vbNewLine &
                    "Please check your network connection or try again later."
            Else
                updaterStatus.Text = "ERROR: Could not install the IAIP update. " & vbNewLine &
                    "Please report this error to the system administrator." & vbNewLine & vbNewLine &
                    "Error Message: " & vbNewLine & e.Error.Message
            End If

            updaterButton.Text = "Close"
            updaterButton.Focus()

            Return
        End If

        Application.Restart()
    End Sub

    Private Sub updaterButton_Click(sender As Object, e As EventArgs) Handles updaterButton.Click
        If updaterButton.Text = "Cancel" Then
            ad.UpdateAsyncCancel()
            Return
        End If

        If Mandatory Then
            CloseIaip()
        Else
            Close()
        End If
    End Sub

    Private Sub IaipUpdater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ad.UpdateAsyncCancel()

        If Mandatory Then
            CloseIaip()
        Else
            Close()
        End If
    End Sub

End Class