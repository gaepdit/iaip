Option Strict On

Imports System.Collections.Generic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class CRViewerForm

#Region " Properties "

    Private Property CRReportDocument() As ReportClass
    Private WriteOnly Property Title() As String
        Set(value As String)
            If value IsNot Nothing Then
                Me.Text = "Report Preview: " & value
            Else
                Me.Text = "Report Preview"
            End If
        End Set
    End Property
    Private Property CRParameters() As Dictionary(Of String, String)

#End Region

#Region " Constructors "

    ' ' No need to open an empty Crystal Reports Viewer, right?
    'Public Sub New()
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub

    ''' <summary>
    ''' Opens a Crystal Reports Viewer form and loads the specified document with the specified data and parameters
    ''' </summary>
    ''' <param name="reportDocument">The Crystal Reports report document to load</param>
    ''' <param name="dataTable">A DataTable to be used by the report</param>
    ''' <param name="parameters">A Dictionary of parameters to be used by the report</param>
    ''' <param name="title">The window title</param>
    Public Sub New(reportDocument As ReportClass, dataTable As DataTable, Optional parameters As Dictionary(Of String, String) = Nothing, Optional title As String = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Title = title
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(dataTable)
        Me.CRParameters = parameters
    End Sub

    ''' <summary>
    ''' Opens a Crystal Reports Viewer form and loads the specified document with the specified data and parameters
    ''' </summary>
    ''' <param name="reportDocument">The Crystal Reports report document to load</param>
    ''' <param name="dataSet">A DataTable to be used by the report</param>
    ''' <param name="parameters">A Dictionary of parameters to be used by the report</param>
    ''' <param name="title">The window title</param>
    Public Sub New(reportDocument As ReportClass, dataSet As DataSet, Optional parameters As Dictionary(Of String, String) = Nothing, Optional title As String = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Title = title
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(dataSet)
        Me.CRParameters = parameters
    End Sub

    ''' <summary>
    ''' Opens a Crystal Reports Viewer form and loads the specified document with the specified data and parameters
    ''' </summary>
    ''' <param name="reportDocument">The Crystal Reports report document to load</param>
    ''' <param name="data">IEnumerable data to be used by the report</param>
    ''' <param name="parameters">A Dictionary of parameters to be used by the report</param>
    ''' <param name="title">The window title</param>
    Public Sub New(reportDocument As ReportClass, data As IEnumerable, Optional parameters As Dictionary(Of String, String) = Nothing, Optional title As String = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Title = title
        Me.CRReportDocument = reportDocument
        Me.CRReportDocument.SetDataSource(data)
        Me.CRParameters = parameters
    End Sub

    ''' <summary>
    ''' Opens a Crystal Reports Viewer form and loads the specified document with the specified parameters
    ''' </summary>
    ''' <param name="reportDocument">The Crystal Reports report document to load</param>
    ''' <param name="parameters">A Dictionary of parameters to be used by the report</param>
    ''' <param name="title">The window title</param>
    Public Sub New(reportDocument As ReportClass, Optional parameters As Dictionary(Of String, String) = Nothing, Optional title As String = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Title = title
        Me.CRReportDocument = reportDocument
        Me.CRParameters = parameters
    End Sub

    ' ' Not currently used
    ' ''' <summary>
    ' ''' Opens a Crystal Reports Viewer form and loads the specified document with the specified data and parameters
    ' ''' </summary>
    ' ''' <param name="reportDocument">The Crystal Reports report document to load</param>
    ' ''' <param name="dataSource">A DataTable to be used by the report</param>
    ' ''' <param name="parameters">A Dictionary of parameters to be used by the report</param>
    ' ''' <param name="title">The window title</param>
    'Public Sub New(reportDocument As ReportClass, dataSource As IDataReader, Optional parameters As Dictionary(Of String, String) = Nothing, Optional title As String = Nothing)
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    '    Me.Title = title
    '    Me.CRReportDocument = reportDocument
    '    Me.CRReportDocument.SetDataSource(dataSource)
    '    Me.CRParameters = parameters
    'End Sub

#End Region

#Region " Form events "

    Private Sub CrystalReportViewerForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CRViewerControl.ReportSource = Nothing
        If CRReportDocument IsNot Nothing Then
            CRReportDocument.Close()
            CRReportDocument.Dispose()
        End If
    End Sub

    Private Sub CrystalReportViewerForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.CRReportDocument IsNot Nothing Then
            If Me.CRParameters IsNot Nothing Then CRSetParameters(CRReportDocument, CRParameters)
            CRViewerControl.ReportSource = Me.CRReportDocument
            CRViewerControl.ShowHideViewerTabs(VisibleOrNot.NotVisible)
        End If
    End Sub

#End Region

#Region " Utilities "

    Private Sub CRSetParameters(document As ReportClass, parameters As Dictionary(Of String, String))
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

#End Region

End Class