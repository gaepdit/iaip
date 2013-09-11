' Code from 
' http://social.msdn.microsoft.com/Forums/windows/en-US/4efb9942-4a29-44ce-b723-ad56b6e31533/datagrid-to-excel-file-coding
'
Option Strict On
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Imports System.Runtime.InteropServices
Imports System.Reflection

Module ExcelExport
    ''' <summary>
    ''' Used to save a DataTable to an Excel file on the first sheet
    ''' </summary>
    ''' <param name="FileName">File name to write xml data into</param>
    ''' <param name="Table">Contains data to write to Excel</param>
    ''' <param name="SheetName">(Optional) Name for sheet1</param>
    ''' <param name="ErrorMessage">(Optional) Contains exception info if this function returns false</param>
    ''' <returns>Whether the operation failed as Boolean</returns>
    ''' <remarks>
    ''' Kevininstructor
    ''' If you want to write to another sheet other than sheet1 then you will need to tweak the
    ''' code and make sure after doing so you do release all objects. I mention this because 
    ''' when switching to a different sheet it is important to avoid calls that tunnel into 
    ''' the object model because they will orphan Runtime Callable Wrapper (RCW) on the heap 
    ''' that you will not be able to access in order to call Marshal.ReleaseComObject. Even 
    ''' so you may end up needing to manually call 
    ''' 
    ''' GC.Collect()
    ''' GC.WaitForPendingFinalizers()
    '''     GC needs to be called twice in order to get the Finalizers called
    '''     - the first time in, it simply makes a list of what is to be
    '''     finalized, the second time in, it actually the finalizing. Only
    '''     then will the object do its automatic ReleaseComObject.
    ''' GC.Collect()
    ''' GC.WaitForPendingFinalizers()
    ''' </remarks>
    Public Function ExportDataTableToExcel(ByVal FileName As String,
                                           ByVal Table As DataTable,
                                           <Out()> Optional ByRef ErrorMessage As String = "",
                                           Optional ByVal SheetName As String = "Sheet1"
                                                                                  ) As Boolean

        Dim result As Boolean = False
        Dim excelApp As Excel.Application = Nothing
        Dim excelWorkbook As Excel.Workbook = Nothing
        Dim excelWorksheet As Excel.Worksheet = Nothing
        Dim excelRange As Excel.Range = Nothing
        ErrorMessage = ""

        Try
            ' Create Excel Workbook & Worksheet
            excelApp = New Excel.Application()
            excelApp.AlertBeforeOverwriting = False
            excelApp.DisplayAlerts = False
            excelWorkbook = excelApp.Workbooks.Add(Type.Missing)
            excelWorkbook.SaveAs(FileName)
            excelWorksheet = DirectCast(excelApp.ActiveWorkbook.ActiveSheet, Excel.Worksheet)
            If Not String.IsNullOrEmpty(SheetName) Then excelWorksheet.Name = SheetName

            ' Create cell range and insert data from Table
            Dim UpperRangeCellValue As String = "A1"
            Dim LastColumn As String = ExcelColumnName(Table.Columns.Count)
            Dim LowerRangeCellValue As String = LastColumn & (Table.Rows.Count + 1).ToString()
            excelRange = excelWorksheet.Range(UpperRangeCellValue, LowerRangeCellValue)
            excelRange.Value2 = GetData(Table)

            ' Format columns and header row
            excelWorksheet.Range("A1", LastColumn & "1").Font.Bold = True
            excelWorksheet.Range(UpperRangeCellValue, LowerRangeCellValue).EntireColumn.AutoFit()

            excelWorkbook.Save()
            result = True
        Catch ex As System.Runtime.InteropServices.COMException
            If ex.ErrorCode = -2147221164 Then
                ErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                Return False
            ElseIf ex.ErrorCode = -2146827284 Then
                ErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
            Else
                ErrorMessage = String.Format("{0}{2}{1}", ex.Message, ex.ErrorCode, Environment.NewLine)
            End If
        Catch ex As Exception
            ErrorMessage = "Error in export: " & ex.Message
        Finally
            If excelWorkbook IsNot Nothing Then excelWorkbook.Close(Nothing, Nothing, Nothing)
            excelApp.Workbooks.Close()
            excelApp.Quit()

            If excelRange IsNot Nothing Then Marshal.ReleaseComObject(excelRange)
            If excelWorksheet IsNot Nothing Then Marshal.ReleaseComObject(excelWorksheet)
            If excelWorkbook IsNot Nothing Then Marshal.ReleaseComObject(excelWorkbook)
            If excelApp IsNot Nothing Then Marshal.ReleaseComObject(excelApp)

            excelRange = Nothing
            excelWorksheet = Nothing
            excelWorkbook = Nothing
            excelApp = Nothing
        End Try

        Return result
    End Function
    Private Function GetData(ByVal dt As DataTable) As Object(,)
        Dim Data As Object(,) = New Object((dt.Rows.Count + 1) - 1, dt.Columns.Count - 1) {}
        For j As Integer = 0 To dt.Columns.Count - 1
            Data(0, j) = dt.Columns(j).Caption
        Next
        Dim dHolder As New DateTime()
        Dim Holder As String = String.Empty
        For i As Integer = 1 To dt.Rows.Count
            For j As Integer = 0 To dt.Columns.Count - 1
                If dt.Columns(j).DataType Is dHolder.[GetType]() Then
                    If dt.Rows(i - 1)(j) IsNot DBNull.Value Then
                        DateTime.TryParse(dt.Rows(i - 1)(j).ToString(), dHolder)
                        Data(i, j) = dHolder.ToString("MM/dd/yy hh:mm tt")
                    Else
                        Data(i, j) = dt.Rows(i - 1)(j)
                    End If
                ElseIf dt.Columns(j).DataType Is Holder.[GetType]() Then
                    If dt.Rows(i - 1)(j) IsNot DBNull.Value Then
                        Holder = dt.Rows(i - 1)(j).ToString().Replace(Environment.NewLine, "")
                        Data(i, j) = Holder
                    Else
                        Data(i, j) = dt.Rows(i - 1)(j)
                    End If
                Else
                    Data(i, j) = dt.Rows(i - 1)(j)
                End If
            Next
            Application.DoEvents()
        Next
        Return Data
    End Function
    Private Function ExcelColumnName(ByVal index As Integer) As String
        ' http://stackoverflow.com/a/297214/212978
        Static chars() As Char = {"A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c, "V"c, "W"c, "X"c, "Y"c, "Z"c}

        index -= 1 ' adjust so it matches 0-indexed array rather than 1-indexed column

        Dim quotient As Integer = index \ 26 ' normal "/" operator rounds. "\" does integer division, which truncates
        If quotient > 0 Then
            ExcelColumnName = ExcelColumnName(quotient) & chars(index Mod 26)
        Else
            ExcelColumnName = chars(index Mod 26)
        End If
    End Function
End Module
