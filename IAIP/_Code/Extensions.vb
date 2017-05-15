﻿Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Collections.Generic
Imports System.Reflection
Imports System.ComponentModel
Imports System.Linq
Imports Microsoft.SqlServer.Server

Module Extensions

#Region " DataGridView "

#Region " DataGridView Columns "

    <Extension()>
    Public Sub SanelyResizeColumns(datagridview As DataGridView,
                                   Optional maxWidth As Integer = 275,
                                   Optional minWidth As Integer = 40)

        ' Resize all columns to fit current content:
        datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Loop through columns & explicitly set column width and undo AutoSizeMode
        Dim currentWidth As Integer
        For Each column As DataGridViewColumn In datagridview.Columns
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            currentWidth = column.Width
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            column.Width = Math.Max(minWidth, currentWidth)
            column.Width = Math.Min(maxWidth, column.Width)
        Next

        ' Allow user resizing of columns:
        datagridview.AllowUserToResizeColumns = True

    End Sub

    <Extension()>
    Public Sub MakeColumnsLookLikeLinks(dgv As DataGridView, col As Integer)
        dgv.MakeColumnsLookLikeLinks(New Integer() {col})
    End Sub

    <Extension()>
    Public Sub MakeColumnsLookLikeLinks(dgv As DataGridView, cols As Integer())
        For Each col As Integer In cols
            dgv.Columns(col).DefaultCellStyle.ForeColor = SystemColors.HotTrack
        Next
    End Sub

    <Extension()>
    Public Sub MakeCellLookLikeHoveredLink(dgv As DataGridView, row As Integer, col As Integer, Optional hover As Boolean = True)
        If hover Then
            dgv.Cursor = Cursors.Hand
            dgv.Rows(row).Cells(col).Style.ForeColor = Color.Blue
            dgv.Rows(row).Cells(col).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Underline)
        Else
            dgv.Cursor = Cursors.Default
            dgv.Rows(row).Cells(col).Style.ForeColor = SystemColors.HotTrack
            dgv.Rows(row).Cells(col).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Regular)
        End If
    End Sub

#End Region

#Region " DataGridView Excel export "

    <Extension()>
    Public Sub ExportToExcel(dataGridView As DataGridView, Optional sender As Object = Nothing)
        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then Exit Sub

        Dim dataTable As DataTable = GetDataTableFromDataGridView(dataGridView)
        dataTable.ExportToExcel(sender)
    End Sub

    Private Function GetDataTableFromDataGridView(dataGridView As DataGridView) As DataTable
        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then Return Nothing

        Dim dataTable As New DataTable

        If TypeOf dataGridView.DataSource Is DataSet Then
            dataTable = dataGridView.DataSource.Tables(dataGridView.DataMember)
        ElseIf TypeOf dataGridView.DataSource Is DataTable Then
            dataTable = dataGridView.DataSource
        Else
            Dim dtRow As DataRow
            For Each dgvColumn As DataGridViewColumn In dataGridView.Columns
                dataTable.Columns.Add(dgvColumn.Name)
            Next
            For Each dgvRow As DataGridViewRow In dataGridView.Rows
                dtRow = dataTable.NewRow
                For i As Integer = 0 To dataGridView.ColumnCount - 1
                    dtRow.Item(i) = dgvRow.Cells(i).Value
                Next
                dataTable.Rows.Add(dtRow)
            Next
        End If

        ' Replace column names with the defined column header text
        For i As Integer = 0 To dataGridView.Columns.Count - 1
            dataTable.Columns(i).Caption = dataGridView.Columns(i).HeaderText
        Next

        Return dataTable
    End Function

#End Region

#End Region

#Region " DataTable "

    <Extension()>
    Public Sub ExportToExcel(dataTable As DataTable, Optional sender As Object = Nothing)
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Exit Sub

        If sender IsNot Nothing Then
            sender.Cursor = Cursors.AppStarting
        End If

        Dim dialog As New SaveFileDialog()
        With dialog
            .Filter = "Excel File (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            .FileName = "Export_" & Date.Now.ToString("yyyy-MM-dd_HH.mm.ss") & ".xlsx"
            .InitialDirectory = GetUserSetting(UserSetting.ExcelExportLocation)
        End With

        If dialog.ShowDialog() = DialogResult.OK Then
            Dim errorMessage As String = ""
            Dim result As Boolean = False

            Try
                result = CreateExcelFileFromDataTable(dialog.FileName, dataTable, errorMessage)
            Catch ex As Exception
                ErrorReport(ex, errorMessage, MethodBase.GetCurrentMethod.Name)
            End Try

            If result Then
                If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                    SaveUserSetting(UserSetting.ExcelExportLocation, Path.GetDirectoryName(dialog.FileName))
                End If
                Process.Start(dialog.FileName)
            Else
                MessageBox.Show(errorMessage)
            End If
        End If

        dialog.Dispose()

        If sender IsNot Nothing Then
            sender.Cursor = Nothing
        End If
    End Sub

#End Region

#Region " Dictionary "

    <Extension()>
    Public Sub AddBlankRow(ByRef d As Dictionary(Of Integer, String), Optional blankPrompt As String = "")
        d.Add(0, blankPrompt)
    End Sub

#End Region

#Region " SplitContainer "

    ''' <summary>
    ''' Sets the SplitContainer.SplitterDistance property, while taking into account the SplitContainer's dimensions
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="dist">The desired SplitterDistance</param>
    ''' <remarks>If the desired SplitterDistance is incompatible with the dimension of the 
    ''' SplitContainer, nothing is changed and no error is returned</remarks>
    <Extension()>
    Public Sub SanelySetSplitterDistance(sc As SplitContainer, dist As Integer)
        Dim i As Integer = dist

        If sc.Orientation = Orientation.Horizontal Then

            ' It may not seem possible for the size of a SplitContainer to be smaller than
            ' the minimum sizes of its parts, but it can happen if the SplitContainer is
            ' docked in a Form that is itself resized until the SplitContainer is too small.
            ' In this situation, don't try to fix things, just bail.
            If (sc.Height < sc.Panel1MinSize + sc.Panel2MinSize) Then Exit Sub

            ' The order here shouldn't matter
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Height - sc.Panel2MinSize)

        Else
            ' Same as above, except for vertical orientation
            If (sc.Width < sc.Panel1MinSize + sc.Panel2MinSize) Then Exit Sub
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Width - sc.Panel2MinSize)
        End If

        sc.SplitterDistance = i
    End Sub

    ''' <summary>
    ''' Toggle SplitContainer.SplitterDistance between two given values
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="a">One of the values to toggle between</param>
    ''' <param name="b">One of the values to toggle between</param>
    ''' <remarks>The order of the parameters does not matter. If either parameter is incompatible with 
    ''' the dimension of the SplitContainer, nothing is changed and no error is returned.</remarks>
    <Extension()>
    Public Sub ToggleSplitterDistance(sc As SplitContainer, a As Integer, b As Integer)
        ' Bail if a or b are outside the allowable values for SplitterDistance
        If (a < sc.Panel1MinSize) OrElse (b < sc.Panel1MinSize) Then Exit Sub
        If (sc.Orientation = Orientation.Vertical) Then
            If (a > sc.Width - sc.Panel2MinSize) OrElse (b > sc.Width - sc.Panel2MinSize) Then Exit Sub
        Else
            If (a > sc.Height - sc.Panel2MinSize) OrElse (b > sc.Height - sc.Panel2MinSize) Then Exit Sub
        End If

        ' If current SplitterDistance is smaller than the average, set it to the larger value;
        ' otherwise, set it to the smaller value
        If (sc.SplitterDistance < (a + b) / 2) Then
            sc.SanelySetSplitterDistance(Math.Max(a, b))
        Else
            sc.SanelySetSplitterDistance(Math.Min(a, b))
        End If
    End Sub

#End Region

#Region " Enum "

    Private enumDescriptions As New Dictionary(Of String, String)

    ''' <summary>
    ''' If a Description attribute is present for an enum value, returns the description.
    ''' Otherwise, returns the normal ToString() representation of the enum value.
    ''' </summary>
    ''' <param name="e">The enum value to describe.</param>
    ''' <returns>The value of the Description attribute if present, else
    ''' the normal ToString() representation of the enum value.</returns>
    ''' <remarks>http://stackoverflow.com/a/14772005/212978</remarks>
    <DebuggerStepThrough>
    <Extension>
    Public Function GetDescription(e As [Enum]) As String
        Dim enumType As Type = e.GetType()
        Dim name As String = e.ToString()

        ' Construct a full name for this enum value
        Dim fullName As String = enumType.FullName + "." + name

        ' See if we have looked it up earlier
        Dim enumDescription As String = Nothing
        If enumDescriptions.TryGetValue(fullName, enumDescription) Then
            ' Yes we have - return previous value
            Return enumDescription
        End If

        ' Find the value of the Description attribute on this enum value
        Dim members As MemberInfo() = enumType.GetMember(name)
        If members IsNot Nothing AndAlso members.Length > 0 Then
            Dim descriptions() As Object = members(0).GetCustomAttributes(GetType(DescriptionAttribute), False)
            If descriptions IsNot Nothing AndAlso descriptions.Length > 0 Then
                ' Set name to description found
                name = DirectCast(descriptions(0), DescriptionAttribute).Description
            End If
        End If

        ' Save the name in the dictionary:
        enumDescriptions.Add(fullName, name)

        Return name
    End Function

    ''' <summary>
    ''' For a given flag enum value, returns an iterator of each flag that is set.
    ''' </summary>
    ''' <param name="flagValues">The flag enum value to iterate.</param>
    ''' <returns>An IEnumerable iterator of enums.</returns>
    <Extension>
    Public Iterator Function GetUniqueFlags(flagValues As [Enum]) As IEnumerable(Of [Enum])
        If Convert.ToInt32(flagValues) = 0 Then
            Yield flagValues
        Else
            Dim flag As ULong = 1
            For Each value As [Enum] In [Enum].GetValues(flagValues.GetType())
                Dim bits As ULong = Convert.ToUInt64(value)
                While flag < bits
                    flag <<= 1
                End While
                If flag = bits AndAlso flagValues.HasFlag(value) Then
                    Yield value
                End If
            Next
        End If
    End Function

    ''' <summary>
    ''' For a given flag enum value, returns an iterator of a string description of each flag that is set.
    ''' </summary>
    ''' <param name="flagValues">The flag enum value to iterate.</param>
    ''' <returns>An IEnumerable iterator of enum descriptions.</returns>
    <Extension>
    Public Iterator Function GetUniqueFlagDescriptions(flagValues As [Enum]) As IEnumerable(Of String)
        For Each value As [Enum] In flagValues.GetUniqueFlags
            Yield value.GetDescription
        Next
    End Function

    Public Function EnumToDataTable(enumType As Type) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Key", [Enum].GetUnderlyingType(enumType))
        dt.Columns.Add("Description", GetType(String))

        Dim e As [Enum]
        For Each name As String In [Enum].GetNames(enumType)
            e = [Enum].Parse(enumType, name)
            dt.Rows.Add(e, e.GetDescription)
        Next

        Return dt
    End Function

#End Region

#Region " Combobox "

    <Extension()>
    Public Sub BindToDictionary(c As ComboBox, d As Dictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToSortedDictionary(c As ComboBox, d As SortedDictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToDictionary(Of T)(c As ComboBox, d As Dictionary(Of T, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToKeyValuePairs(c As ComboBox, l As Dictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(l, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    ''' <summary>
    ''' Populates the combobox with the values and text descriptions of an Enum.
    ''' </summary>
    ''' <typeparam name="TEnum">The Enum to bind to.</typeparam>
    ''' <param name="c">The Combobox to bind.</param>
    <Extension()>
    Public Sub BindToEnum(Of TEnum)(c As ComboBox)
        Dim enumType As Type = GetType(TEnum)

        If enumType.BaseType IsNot GetType([Enum]) Then
            Throw New ArgumentException("TEnum must be of type System.Enum")
        End If

        Dim enumItems As Array = [Enum].GetValues(enumType)
        Dim enumDict As New Dictionary(Of [Enum], String)

        For Each enumItem As [Enum] In enumItems
            enumDict(enumItem) = enumItem.GetDescription
        Next

        c.BindToDictionary(enumDict)
    End Sub

#End Region

#Region " ICollection "

    <Extension>
    Public Function ContainsAny(Of T)(set1 As ICollection(Of T), set2 As ICollection(Of T)) As Boolean
        If set1 Is Nothing OrElse set2 Is Nothing Then Return False
        If set1 Is set2 Then Return True
        If set1.Count < set2.Count Then
            Dim hs As New HashSet(Of T)(set1)
            For Each v As T In set2
                If hs.Contains(v) Then Return True
            Next
        Else
            Dim hs As New HashSet(Of T)(set2)
            For Each v As T In set1
                If hs.Contains(v) Then Return True
            Next
        End If
        Return False
    End Function

#End Region

#Region " List "

    <Extension>
    Public Function AddBlankRowToList(l As List(Of String), Optional blankPrompt As String = "") As List(Of String)
        l.Insert(0, blankPrompt)
        Return l
    End Function

#End Region

End Module