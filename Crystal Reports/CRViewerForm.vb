Option Strict On

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class CRViewerForm

#Region "Properties"

    Private _crReportDocument As ReportDocument
    Public Property CRDocumentSource() As ReportDocument
        Get
            Return _crReportDocument
        End Get
        Set(ByVal value As ReportDocument)
            _crReportDocument = value
        End Set
    End Property

    Private _title As String
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

#End Region

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal reportDocument As ReportDocument, ByVal dataSource As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRDocumentSource = reportDocument
        Me.CRDocumentSource.SetDataSource(dataSource)
    End Sub

    Public Sub New(ByVal reportDocument As ReportDocument, ByVal dataSource As DataSet)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRDocumentSource = reportDocument
        Me.CRDocumentSource.SetDataSource(dataSource)
    End Sub

    Public Sub New(ByVal reportDocument As ReportDocument, ByVal dataSource As IDataReader)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRDocumentSource = reportDocument
        Me.CRDocumentSource.SetDataSource(dataSource)
    End Sub

    Private Sub CrystalReportViewerForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CRDocumentSource.Close()
    End Sub

    Private Sub CrystalReportViewerForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetTitle()
        ConfigureCrystalReports()
    End Sub

    Private Sub SetTitle()
        If Title IsNot Nothing Then Me.Text = "Report Preview: " & Title
    End Sub

    Private Sub ConfigureCrystalReports()
        If CRDocumentSource IsNot Nothing Then CRViewerControl.ReportSource = CRDocumentSource
    End Sub

End Class