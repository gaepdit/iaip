Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Daramee.TaskDialogSharp

Friend Module ExceptionManager

    ''' <summary>
    ''' Handles reporting of errors.
    ''' </summary>
    ''' <param name="ex">The exception to be handled.</param>
    ''' <param name="context">A string representing the context of the error.</param>
    Friend Sub ErrorReport(ex As Exception, context As String, Optional displayErrorToUser As Boolean = True)
        ErrorReport(ex, Nothing, context, displayErrorToUser)
    End Sub

    ''' <summary>
    ''' Handles reporting of errors.
    ''' </summary>
    ''' <param name="ex">The exception to be handled.</param>
    ''' <param name="supplementalMessage">An string containing supplementary information to be logged.</param>
    ''' <param name="context">A string representing the calling function.</param>
    Friend Function ErrorReport(
            ex As Exception,
            supplementalMessage As String,
            context As String,
            Optional displayErrorToUser As Boolean = True,
            Optional unrecoverable As Boolean = False) As Boolean

        ' Don't log missing Crystal Reports runtime
        ' and don't exit application
        If SeemsLikeACrystalReportsIssue(ex) Then
            ShowCrystalReportsSupportMessage()
            Return False
        End If

        ' Don't log network down
        If SeemsLikeANetworkIssue(ex) Then
            ShowNetworkDownSupportMessage()
            Return unrecoverable
        End If

        ' If Task Canceled Exception, don't exit application
        If IsATaskCanceledException(ex) Then
            unrecoverable = False
        End If

        ' First, log the exception.
        Dim logged As Boolean = LogException(ex, context, supplementalMessage, unrecoverable)

        ' Second, display a dialog to the user describing the error and next steps.
        If displayErrorToUser Then
            ShowErrorDialog(ex, unrecoverable, logged)
        End If

        Return unrecoverable
    End Function

    Private Function IsATaskCanceledException(ex As Exception) As Boolean
        Return ex.GetType() = GetType(Threading.Tasks.TaskCanceledException)
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

    Public Sub ShowNetworkDownSupportMessage()
        Dim nav As IAIPNavigation = GetSingleForm(Of IAIPNavigation)()
        If nav Is Nothing Then Return

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
                    .Buttons = {New TaskDialogButton(99, "Exit IAIP")},
                    .UseCommandLinks = False,
                    .DefaultButton = TaskDialogCommonButtonFlags.OK
                }

                AddHandler td.ButtonClicked,
                Sub(sender As Object, e As ButtonClickedEventArgs)
                    If e.ButtonId = 99 Then
                        CloseIaip()
                    End If
                End Sub

                Try
                    td.Show()
                Catch ex As Exception
                    ' when the task dialog is closed, it throws an unhandled exception that ends 
                    ' the background worker, but it still works as long as .Buttons is defined above
                End Try
            End Sub

            bgw.RunWorkerAsync()
        End Using
    End Sub

    Private Sub ShowErrorDialog(ex As Exception, unrecoverable As Boolean, logged As Boolean)
        Using ed As New ExceptionDialog() With {
            .MyException = ex,
            .Unrecoverable = unrecoverable,
            .Logged = logged
        }
            ed.ShowDialog()
        End Using
    End Sub

End Module
