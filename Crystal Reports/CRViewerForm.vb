Option Strict On

Imports System.Collections.Generic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Public Class CRViewerForm

#Region "Properties"

    Private _crReportDocument As ReportClass
    Public Property CRReportDocument() As ReportClass
        Get
            Return _crReportDocument
        End Get
        Set(ByVal value As ReportClass)
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

    Private _crParameters As Dictionary(Of String, String)
    Public Property CRParameters() As Dictionary(Of String, String)
        Get
            Return _crParameters
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _crParameters = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal reportDocument As ReportClass, ByVal dataSource As DataTable, Optional ByVal parameters As Dictionary(Of String, String) = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(dataSource)
        Me.CRParameters = parameters
    End Sub

    Public Sub New(ByVal reportDocument As ReportClass, ByVal dataSource As DataSet, Optional ByVal parameters As Dictionary(Of String, String) = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(dataSource)
        Me.CRParameters = parameters
    End Sub

    Public Sub New(ByVal reportDocument As ReportClass, ByVal dataSource As IDataReader, Optional ByVal parameters As Dictionary(Of String, String) = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(dataSource)
        Me.CRParameters = parameters
    End Sub

#End Region

#Region "Form events"

    Private Sub CrystalReportViewerForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CRReportDocument.Close()
    End Sub

    Private Sub CrystalReportViewerForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
        monitor.TrackFeature("Report." & CRReportDocument.ResourceName)

        If Me.Title IsNot Nothing Then SetFormTitle("Report Preview: " & Me.Title)
        If Me.CRReportDocument IsNot Nothing Then CRSetDocumentSource(CRViewerControl, Me.CRReportDocument)
        If Me.CRParameters IsNot Nothing Then CRSetParameters(Me.CRReportDocument, Me.CRParameters)
        CRViewerTabs(CRViewerControl, False)
    End Sub

#End Region

    Private Sub SetFormTitle(ByVal title As String)
        If title IsNot Nothing Then Me.Text = title
    End Sub

    Private Sub CRSetDocumentSource(ByVal viewer As CrystalReportViewer, ByVal document As ReportClass)
        If document IsNot Nothing AndAlso viewer IsNot Nothing Then viewer.ReportSource = document
    End Sub

    Private Sub CRSetParameters(ByVal document As ReportClass, ByVal parameters As Dictionary(Of String, String))
        Dim fieldDefinitions As ParameterFieldDefinitions = document.DataDefinition.ParameterFields
        Dim parameterValues As ParameterValues = New ParameterValues()

        For Each parameter As KeyValuePair(Of String, String) In parameters
            Dim discreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
            discreteValue.Value = parameter.Value
            parameterValues.Add(discreteValue)
            Dim fieldDefinition As ParameterFieldDefinition = fieldDefinitions(parameter.Key)
            fieldDefinition.ApplyCurrentValues(parameterValues)
        Next
    End Sub

    Private Sub CRViewerTabs(ByVal viewer As CrystalReportViewer, ByVal visible As Boolean)
        ' http://bloggingabout.net/blogs/jschreuder/archive/2005/08/03/8760.aspx
        If viewer IsNot Nothing Then
            For Each control As Control In viewer.Controls
                If TypeOf control Is PageView Then
                    Dim tab As TabControl = DirectCast(DirectCast(control, PageView).Controls(0), TabControl)
                    If Not visible Then
                        tab.ItemSize = New Size(0, 1)
                        tab.SizeMode = TabSizeMode.Fixed
                        tab.Appearance = TabAppearance.Buttons
                    Else
                        tab.ItemSize = New Size(67, 18)
                        tab.SizeMode = TabSizeMode.Normal
                        tab.Appearance = TabAppearance.Normal
                    End If
                End If
            Next
        End If
    End Sub

End Class