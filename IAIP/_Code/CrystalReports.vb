Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Runtime.CompilerServices
Imports CrystalDecisions.Shared

Module CrystalReports

#Region "Crystal Reports displayer"

    Public Sub SetUpCrystalReportViewer(report As ReportClass, crReportViewer As CrystalReportViewer, TabText As String)
        crReportViewer.ReportSource = report

        crReportViewer.ToolPanelView = ToolPanelViewType.None
        crReportViewer.DisplayToolbar = True
        crReportViewer.ShowRefreshButton = False
        crReportViewer.Visible = True
        crReportViewer.ShowGroupTreeButton = False
        crReportViewer.ShowLogo = False
        crReportViewer.ShowParameterPanelButton = False

        Dim i As Integer
        Do While i < crReportViewer.Controls.Count
            If TypeOf (crReportViewer.Controls(i)) Is PageView Then
                Dim j As Integer
                Do While j < crReportViewer.Controls(i).Controls.Count
                    If CType(crReportViewer.Controls(i).Controls(j), TabControl).TabPages.Count > 0 Then
                        ' Change the tab text
                        CType(crReportViewer.Controls(i).Controls(j), TabControl).TabPages.Item(0).Text = TabText
                        Exit Do
                    End If
                Loop
                Exit Do
            Else
                crReportViewer.Controls(i).Visible = False
            End If
        Loop

        crReportViewer.Refresh()
    End Sub

    <Extension>
    Public Sub ShowHideViewerTabs(CrystalReportViewer As CrystalReportViewer, visible As VisibleOrNot)
        ' http://bloggingabout.net/blogs/jschreuder/archive/2005/08/03/8760.aspx
        If CrystalReportViewer IsNot Nothing Then
            For Each control As Control In CrystalReportViewer.Controls
                If TypeOf control Is PageView Then
                    Dim tab As TabControl = DirectCast(DirectCast(control, PageView).Controls(0), TabControl)
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
                End If
            Next
        End If
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
