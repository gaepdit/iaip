Option Strict On

Imports System.Collections.Generic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

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
    Public Sub New(ByVal reportDocument As ReportClass, ByVal dataTable As DataTable, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal title As String = Nothing)
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
    Public Sub New(ByVal reportDocument As ReportClass, ByVal dataSet As DataSet, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal title As String = Nothing)
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
    Public Sub New(ByVal reportDocument As ReportClass, ByVal data As IEnumerable, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal title As String = Nothing)
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
    Public Sub New(ByVal reportDocument As ReportClass, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal title As String = Nothing)
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
    'Public Sub New(ByVal reportDocument As ReportClass, ByVal dataSource As IDataReader, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal title As String = Nothing)
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

    Private Sub CrystalReportViewerForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CRViewerControl.ReportSource = Nothing
        If CRReportDocument IsNot Nothing Then
            CRReportDocument.Close()
            CRReportDocument.Dispose()
        End If
    End Sub

    Private Sub CrystalReportViewerForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Report." & CRReportDocument.ResourceName)

        If Me.CRReportDocument IsNot Nothing Then
            If Me.CRParameters IsNot Nothing Then CRSetParameters(CRReportDocument, CRParameters)
            CRViewerControl.ReportSource = Me.CRReportDocument
            CRViewerTabs(CRViewerControl, False)
        End If
    End Sub

#End Region

#Region " Utilities "
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
#End Region

End Class