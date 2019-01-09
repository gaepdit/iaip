Imports System.ComponentModel
Imports Iaip.Apb

''' <summary>
''' DataGridView with some tweaks I prefer
''' </summary>
''' <remarks>Disallows user editing. Hides row headers. Changes selection mode
''' to full single row. Auto-formats several data types.</remarks>
Public Class IaipDataGridView

    ' Custom Properties

    <Category("Appearance"), Description("Format for Decimal data typed columns.")>
    <DefaultValue("C0")>
    Public Property DecimalFieldFormat As String = "C0"

    <Category("Appearance"), Description("Format for Date data typed columns.")>
    <DefaultValue(DateFormat)>
    Public Property DateFieldFormat As String = DateFormat

    <Category("Appearance"), Description("Label for displaying results count.")>
    Public Property ResultsCountLabel As Label = Nothing

    <Category("Appearance"), Description("Format for displaying results count.")>
    Public Property ResultsCountLabelFormat As String = "{0} found"

    <Category("Behavior"), Description("Format first column like a link.")>
    <DefaultValue(False)>
    Public Property LinkifyFirstColumn As Boolean = False

    ' Private fields

    Private gridHoveredOrFocused As Boolean = False

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

    ' Methods

    Public Sub SelectNone()
        ClearSelection()
        CurrentCell = Nothing
    End Sub

    ' Events

    Private Sub IaipDataGridView_DataSourceChanged(sender As Object, e As EventArgs) Handles MyBase.DataSourceChanged
        If DataSource Is Nothing Then
            RemoveExcelExportButton()

            If ResultsCountLabel IsNot Nothing Then
                ResultsCountLabel.Text = String.Empty
            End If
        Else
            AddExcelExportButton()

            SanelyResizeColumns()

            If LinkifyFirstColumn Then
                MakeColumnLookLikeLinks(0)
            End If

            If ResultsCountLabel IsNot Nothing Then
                ResultsCountLabel.Text = String.Format(ResultsCountLabelFormat, Rows.Count)
            End If
        End If
    End Sub

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

    Private Sub IaipDataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
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

    Private Sub IaipDataGridView_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellMouseEnter
        If LinkifyFirstColumn AndAlso e.RowIndex <> -1 AndAlso e.RowIndex < RowCount AndAlso e.ColumnIndex = 0 Then
            MakeCellLookLikeHoveredLink(e.RowIndex, 0, True)
        End If
    End Sub

    Private Sub IaipDataGridView_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellMouseLeave
        If LinkifyFirstColumn AndAlso e.RowIndex <> -1 AndAlso e.RowIndex < RowCount AndAlso e.ColumnIndex = 0 Then
            MakeCellLookLikeHoveredLink(e.RowIndex, 0, False)
        End If
    End Sub

    Private Sub ButtonExportToExcel_Click(sender As Object, e As EventArgs) Handles ExportToExcelButton.Click
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

End Class
