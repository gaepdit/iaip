Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports Daramee.TaskDialogSharp

Module CrystalReports

#Region " Installation check "

    Private Enum Availability
        Untested
        Available
        Unavailable
    End Enum

    Private Property CrystalReportsAvailability As Availability = Availability.Untested

    Friend Function CrystalReportsIsAvailable() As Boolean
        Select Case CrystalReportsAvailability
            Case Availability.Available
                Return True

            Case Availability.Unavailable
                ShowCrystalReportsSupportMessage()
                Return False

            Case Else
                If TestCrystalReportsAvailability() Then
                    CrystalReportsAvailability = Availability.Available
                Else
                    CrystalReportsAvailability = Availability.Unavailable
                End If
                Return CrystalReportsIsAvailable()
        End Select
    End Function

    Private Function TestCrystalReportsAvailability() As Boolean
        Dim testAssembly As Reflection.Assembly
        Try
            testAssembly = Reflection.Assembly.ReflectionOnlyLoad("CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Friend Sub ShowCrystalReportsSupportMessage()
        Using bgw As New BackgroundWorker
            AddHandler bgw.DoWork,
            Sub()
                Dim td As New TaskDialog With {
                    .Title = "Crystal Reports Not Installed",
                    .MainIcon = TaskDialogIcon.Warning,
                    .MainInstruction = "Printing from the IAIP is currently unavailable.",
                    .Content = "The components needed for printing are missing. " &
                        "You can continue to use the IAIP, but you will not be able to print " &
                        "until installation is complete. " & vbNewLine & vbNewLine &
                        "Open the installation page for more information.",
                    .CommonButtons = TaskDialogCommonButtonFlags.Close,
                    .Buttons = {New TaskDialogButton(101, "Open Installation Page")},
                    .UseCommandLinks = False,
                    .DefaultButton = 1
                }

                AddHandler td.ButtonClicked,
                    Sub(sender As Object, e As ButtonClickedEventArgs)
                        If e.ButtonId = 101 Then
                            OpenPrereqInstallUrl()
                        End If
                    End Sub

                ' when the task dialog is closed, it throws an unhandled exception that ends 
                ' the background worker, but it still works as long as .Buttons is defined above
                td.Show()
            End Sub

            bgw.RunWorkerAsync()
        End Using
    End Sub

#End Region

#Region " Crystal Reports displayer "

    Public Sub SetUpCrystalReportViewer(report As ReportClass, crReportViewer As CrystalReportViewer)
        crReportViewer.ReportSource = report

        crReportViewer.ToolPanelView = ToolPanelViewType.None
        crReportViewer.DisplayToolbar = True
        crReportViewer.ShowRefreshButton = False
        crReportViewer.Visible = True
        crReportViewer.ShowGroupTreeButton = False
        crReportViewer.ShowLogo = False
        crReportViewer.ShowParameterPanelButton = False
        crReportViewer.ShowHideViewerTabs(VisibleOrNot.NotVisible)

        crReportViewer.Refresh()

        DAL.LogCrystalReportsUsage(report)
    End Sub

    <Extension>
    Public Sub ShowHideViewerTabs(CrystalReportViewer As CrystalReportViewer, visible As VisibleOrNot)
        ' http://bloggingabout.net/blogs/jschreuder/archive/2005/08/03/8760.aspx
        If CrystalReportViewer Is Nothing OrElse CrystalReportViewer.Controls.Count = 0 Then
            Return
        End If

        For Each control As Control In CrystalReportViewer.Controls
            If TypeOf control IsNot PageView Then
                Continue For
            End If

            Dim pageView As PageView = DirectCast(control, PageView)

            If pageView.Controls.Count = 0 Then
                Continue For
            End If

            Dim tab As TabControl = DirectCast(pageView.Controls(0), TabControl)

            Select Case visible
                Case VisibleOrNot.NotVisible
                    tab.ItemSize = New Size(0, 1)
                    tab.SizeMode = TabSizeMode.Fixed
                    tab.Appearance = TabAppearance.Buttons
                Case VisibleOrNot.Visible
                    tab.ItemSize = New Size(67, 18)
                    tab.SizeMode = TabSizeMode.Normal
                    tab.Appearance = TabAppearance.Normal
            End Select
        Next
    End Sub


#End Region

#Region " Parameter Fields utilities "

    <Extension>
    Public Sub AddParameterField(ParameterFields As ParameterFields, parameterFieldName As String, value As String)
        Dim DiscreteValue As New ParameterDiscreteValue
        DiscreteValue.Value = value

        Dim Field As New ParameterField
        Field.ParameterFieldName = parameterFieldName
        Field.CurrentValues.Add(DiscreteValue)

        ParameterFields.Add(Field)
    End Sub

#End Region

End Module
