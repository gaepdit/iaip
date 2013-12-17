Option Strict On

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class CRViewerForm

    Private _crSource As ReportDocument
    Public Property CRDocumentSource() As ReportDocument
        Get
            Return _crSource
        End Get
        Set(ByVal value As ReportDocument)
            _crSource = value
        End Set
    End Property

    Private Sub CrystalReportViewerForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CRDocumentSource.Close()
    End Sub

    Private Sub CrystalReportViewerForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConfigureCrystalReports()
    End Sub

    Private Sub ConfigureCrystalReports()
        If CRDocumentSource IsNot Nothing Then CRViewerControl.ReportSource = CRDocumentSource
    End Sub

End Class