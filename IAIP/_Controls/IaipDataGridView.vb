Imports System.Collections.Generic
Imports System.ComponentModel
Imports Iaip.Apb

''' <summary>
''' DataGridView with some tweaks I prefer
''' </summary>
''' <remarks>Disallows user editing. Hides row headers. Changes selection mode
''' to full single row. Auto-formats several data types.</remarks>
Public Class IaipDataGridView

    ' Custom Properties

    <Category("Appearance"), Description("Automatically apply formatting for some column data types.")>
    <DefaultValue(True)>
    Public Property AutoFormatCells As Boolean = True

    <Category("Appearance"), Description("Format for Decimal data typed columns.")>
    <DefaultValue("C")>
    Public Property DecimalFieldFormat As String = "C"

    <Category("Appearance"), Description("Format for Date data typed columns.")>
    <DefaultValue(DateFormat)>
    Public Property DateFieldFormat As String = DateFormat

    <Category("Appearance"), Description("Label for displaying results count.")>
    Public Property ResultsCountLabel As Label = Nothing

    <Category("Appearance"), Description("Format for displaying results count.")>
    Public Property ResultsCountLabelFormat As String = "{0} found"

    <Category("Behavior"), Description("Format first column (index = 0) like a link.")>
    <DefaultValue(False)>
    Public Property LinkifyFirstColumn As Boolean
        Get
            Return _LinkifyFirstColumn
        End Get
        Set(value As Boolean)
            If _LinkifyFirstColumn <> value Then
                _LinkifyFirstColumn = value

                If value Then
                    LinkifyColumnByName = Nothing
                End If
            End If
        End Set
    End Property
    Private _LinkifyFirstColumn As Boolean

    <Category("Behavior"), Description("Format named column like a link.")>
    Public Property LinkifyColumnByName As String
        Get
            Return _LinkifyColumnByName
        End Get
        Set(value As String)
            If _LinkifyColumnByName <> value Then
                _LinkifyColumnByName = value

                If Not String.IsNullOrEmpty(value) Then
                    LinkifyFirstColumn = False
                End If
            End If
        End Set
    End Property
    Private _LinkifyColumnByName As String

    Private ReadOnly Property LinkifiedByColumnName As Boolean
        Get
            Return Not String.IsNullOrEmpty(LinkifyColumnByName)
        End Get
    End Property

    ' Modified defaults for DataGridView properties 

    <DefaultValue(False)>
    Public Overloads Property AllowUserToAddRows As Boolean
        Get
            Return MyBase.AllowUserToAddRows
        End Get
        Set(value As Boolean)
            If MyBase.AllowUserToAddRows <> value Then
                MyBase.AllowUserToAddRows = value
            End If
        End Set
    End Property

    <DefaultValue(False)>
    Public Overloads Property AllowUserToDeleteRows As Boolean
        Get
            Return MyBase.AllowUserToDeleteRows
        End Get
        Set(value As Boolean)
            If MyBase.AllowUserToDeleteRows <> value Then
                MyBase.AllowUserToDeleteRows = value
            End If
        End Set
    End Property

    <DefaultValue(True)>
    Public Overloads Property AllowUserToOrderColumns As Boolean
        Get
            Return MyBase.AllowUserToOrderColumns
        End Get
        Set(value As Boolean)
            If MyBase.AllowUserToOrderColumns <> value Then
                MyBase.AllowUserToOrderColumns = value
            End If
        End Set
    End Property

    <DefaultValue(False)>
    Public Overloads Property AllowUserToResizeRows As Boolean
        Get
            Return MyBase.AllowUserToResizeRows
        End Get
        Set(value As Boolean)
            If MyBase.AllowUserToResizeRows <> value Then
                MyBase.AllowUserToResizeRows = value
            End If
        End Set
    End Property

    <DefaultValue(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)>
    Public Overloads Property AutoSizeColumnsMode As DataGridViewAutoSizeColumnsMode
        Get
            Return MyBase.AutoSizeColumnsMode
        End Get
        Set(value As DataGridViewAutoSizeColumnsMode)
            If MyBase.AutoSizeColumnsMode <> value Then
                MyBase.AutoSizeColumnsMode = value
            End If
        End Set
    End Property

    <DefaultValue(DataGridViewEditMode.EditProgrammatically)>
    Public Overloads Property EditMode As DataGridViewEditMode
        Get
            Return MyBase.EditMode
        End Get
        Set(value As DataGridViewEditMode)
            If MyBase.EditMode <> value Then
                MyBase.EditMode = value
            End If
        End Set
    End Property

    <DefaultValue(GetType(Color), "0xFFDCDCDC")>
    Public Overloads Property GridColor As Color
        Get
            Return MyBase.GridColor
        End Get
        Set(value As Color)
            If MyBase.GridColor <> value Then
                MyBase.GridColor = value
            End If
        End Set
    End Property

    <DefaultValue(False)>
    Public Overloads Property MultiSelect As Boolean
        Get
            Return MyBase.MultiSelect
        End Get
        Set(value As Boolean)
            If MyBase.MultiSelect <> value Then
                MyBase.MultiSelect = value
            End If
        End Set
    End Property

    <DefaultValue(True)>
    Public Overloads Property [ReadOnly] As Boolean
        Get
            Return MyBase.[ReadOnly]
        End Get
        Set(value As Boolean)
            If MyBase.[ReadOnly] <> value Then
                MyBase.[ReadOnly] = value
            End If
        End Set
    End Property

    <DefaultValue(False)>
    Public Overloads Property RowHeadersVisible As Boolean
        Get
            Return MyBase.RowHeadersVisible
        End Get
        Set(value As Boolean)
            If MyBase.RowHeadersVisible <> value Then
                MyBase.RowHeadersVisible = value
            End If
        End Set
    End Property

    <DefaultValue(DataGridViewSelectionMode.FullRowSelect)>
    Public Overloads Property SelectionMode As DataGridViewSelectionMode
        Get
            Return MyBase.SelectionMode
        End Get
        Set(value As DataGridViewSelectionMode)
            If MyBase.SelectionMode <> value Then
                MyBase.SelectionMode = value
            End If
        End Set
    End Property

    ' Selection

    Public Sub SelectNone()
        ClearSelection()
        CurrentCell = Nothing
    End Sub

    Public Sub SelectRow(rowIndex As Integer)
        If Rows.Count = 0 Then
            Return
        End If

        If rowIndex < 0 Then
            SelectNone()
            Return
        End If

        Rows(rowIndex).Selected = True
        If Rows(rowIndex).Visible Then CurrentCell = Rows(rowIndex).Cells(FirstDisplayedScrollingColumnIndex)
        FirstDisplayedScrollingRowIndex = rowIndex
    End Sub

    ' Data Source

    Private Sub IaipDataGridView_DataSourceChanged(sender As Object, e As EventArgs) Handles MyBase.DataSourceChanged
        If DataSource Is Nothing Then
            AddBreadcrumb("IaipDataGridView: datasource removed", "Name", Name, Me)
            RemoveExcelExportButton()

            If ResultsCountLabel IsNot Nothing Then
                ResultsCountLabel.Text = String.Empty
            End If
        Else
            AddBreadcrumb("IaipDataGridView: datasource changed", "Name", Name, Me)
            AddExcelExportButton()

            If LinkifyFirstColumn Then
                MakeColumnLookLikeLinks(0)
            ElseIf LinkifiedByColumnName Then
                MakeColumnLookLikeLinks(LinkifyColumnByName)
            End If

            If ResultsCountLabel IsNot Nothing Then
                ResultsCountLabel.Text = String.Format(ResultsCountLabelFormat, Rows.Count)
            End If

            If Visible Then
                SanelyResizeColumns()
                resized = True
            Else
                resized = False
            End If
        End If
    End Sub

    Private resized As Boolean = False

    Private Sub IaipDataGridView_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not resized AndAlso Visible AndAlso DataSource IsNot Nothing Then
            SanelyResizeColumns()
            resized = True
        End If
    End Sub


    ' Export to Excel button

    Private gridHoveredOrFocused As Boolean

    Private Sub AddExcelExportButton()
        Controls.Add(ExportToExcelButton)
        ExportToExcelButton.Location = New Point(Math.Max(Width - 62, 10), Math.Max(Height - 52, 10))
        ExportToExcelButton.Size = New Size(42, 32)
        ExportToExcelButton.Visible = gridHoveredOrFocused
    End Sub

    Private Sub RemoveExcelExportButton()
        If Controls.Contains(ExportToExcelButton) Then
            Controls.Remove(ExportToExcelButton)
        End If
    End Sub

    Private Sub ExportToExcelButton_Click(sender As Object, e As EventArgs) Handles ExportToExcelButton.Click
        ExportToExcel(Me)
    End Sub

    Private Sub ActivateButton(sender As Object, e As EventArgs) Handles MyBase.MouseEnter, MyBase.Enter, ExportToExcelButton.MouseEnter
        gridHoveredOrFocused = True

        If DataSource IsNot Nothing Then
            ExportToExcelButton.Visible = True
        End If
    End Sub

    Private Sub DeactivateButton(sender As Object, e As EventArgs) Handles MyBase.MouseLeave, MyBase.Leave, ExportToExcelButton.MouseLeave
        If Not ClientRectangle.Contains(PointToClient(MousePosition)) Then
            gridHoveredOrFocused = False
            ExportToExcelButton.Visible = False
        End If
    End Sub

    ' Data Formatting

    Private Sub IaipDataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        If AutoFormatCells AndAlso e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            If TypeOf e.Value Is ApbFacilityId Then
                e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
            ElseIf TypeOf e.Value Is Date Then
                e.CellStyle.Format = DateFormat
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ElseIf TypeOf e.Value Is Decimal Then
                e.CellStyle.Format = DecimalFieldFormat
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        End If
    End Sub

    ' Link highlighting

    Private Sub IaipDataGridView_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellMouseEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < RowCount Then
            If LinkifyFirstColumn AndAlso e.ColumnIndex = 0 Then
                MakeCellLookLikeHoveredLink(e.RowIndex, 0, True)
            ElseIf LinkifiedByColumnName AndAlso Columns(e.ColumnIndex).Name = LinkifyColumnByName Then
                MakeCellLookLikeHoveredLink(e.RowIndex, LinkifyColumnByName, True)
            End If
        End If
    End Sub

    Private Sub IaipDataGridView_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellMouseLeave
        If e.RowIndex <> -1 AndAlso e.RowIndex < RowCount Then
            If LinkifyFirstColumn AndAlso e.ColumnIndex = 0 Then
                MakeCellLookLikeHoveredLink(e.RowIndex, 0, False)
            ElseIf LinkifiedByColumnName AndAlso Columns(e.ColumnIndex).Name = LinkifyColumnByName Then
                MakeCellLookLikeHoveredLink(e.RowIndex, LinkifyColumnByName, False)
            End If
        End If
    End Sub

    ' Link selected event

    Public Event CellLinkSelected As IaipDataGridViewCellLinkEventHandler

    Protected Overridable Sub OnCellLinkSelected(e As IaipDataGridViewCellLinkEventArgs)
        RaiseEvent CellLinkSelected(Me, e)
    End Sub

    Protected Overrides Sub OnCellEnter(e As DataGridViewCellEventArgs)
        ArgumentNotNull(e, NameOf(e))

        If e.RowIndex <> -1 AndAlso e.RowIndex < RowCount Then
            If LinkifyFirstColumn AndAlso Me(0, e.RowIndex).Value IsNot Nothing Then
                OnCellLinkSelected(New IaipDataGridViewCellLinkEventArgs(Me(0, e.RowIndex).Value))
            ElseIf LinkifiedByColumnName AndAlso Me(LinkifyColumnByName, e.RowIndex).Value IsNot Nothing Then
                OnCellLinkSelected(New IaipDataGridViewCellLinkEventArgs(Me(LinkifyColumnByName, e.RowIndex).Value))
            End If
        End If

        MyBase.OnCellEnter(e)
    End Sub

    ' Link activated event

    Public Event CellLinkActivated As IaipDataGridViewCellLinkEventHandler

    Protected Overridable Sub OnCellLinkActivated(e As IaipDataGridViewCellLinkEventArgs)
        Cursor = Cursors.WaitCursor
        RaiseEvent CellLinkActivated(Me, e)
        Cursor = Cursors.Default
    End Sub

    Protected Overrides Sub OnCellClick(e As DataGridViewCellEventArgs)
        ArgumentNotNull(e, NameOf(e))

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < RowCount Then
            If LinkifyFirstColumn AndAlso e.ColumnIndex = 0 Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(Me(0, e.RowIndex).Value))
            ElseIf LinkifiedByColumnName AndAlso Columns(e.ColumnIndex).Name = LinkifyColumnByName Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(Me(LinkifyColumnByName, e.RowIndex).Value))
            End If
        End If

        MyBase.OnCellClick(e)
    End Sub

    Protected Overrides Sub OnCellDoubleClick(e As DataGridViewCellEventArgs)
        ArgumentNotNull(e, NameOf(e))

        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 AndAlso e.RowIndex < Me.RowCount Then
            If LinkifyFirstColumn AndAlso e.ColumnIndex <> 0 Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(Me(0, e.RowIndex).Value))
            ElseIf LinkifiedByColumnName AndAlso Columns(e.ColumnIndex).Name <> LinkifyColumnByName Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(Me(LinkifyColumnByName, e.RowIndex).Value))
            End If
        End If

        MyBase.OnCellDoubleClick(e)
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        ArgumentNotNull(e, NameOf(e))

        e.Handled = (e.KeyCode = Keys.Enter) AndAlso (LinkifyFirstColumn OrElse LinkifiedByColumnName)
        MyBase.OnKeyDown(e)
    End Sub

    Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
        ArgumentNotNull(e, NameOf(e))

        If e.KeyCode = Keys.Enter Then
            If LinkifyFirstColumn Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(CurrentRow.Cells(0).Value))
            ElseIf LinkifiedByColumnName Then
                OnCellLinkActivated(New IaipDataGridViewCellLinkEventArgs(CurrentRow.Cells(LinkifyColumnByName).Value))
            End If
        End If

        MyBase.OnKeyUp(e)
    End Sub

    Public Delegate Sub IaipDataGridViewCellLinkEventHandler(sender As Object, e As IaipDataGridViewCellLinkEventArgs)

End Class

Public Class IaipDataGridViewCellLinkEventArgs
    Inherits EventArgs

    Public Sub New(linkValue As Object)
        Me.LinkValue = linkValue
    End Sub

    ''' <summary>
    ''' Gets an object representing the value of the linked cell.
    ''' </summary>
    ''' <returns>The value of the linked cell that was selected.</returns>
    Public ReadOnly Property LinkValue As Object
End Class
