Imports ClosedXML.Excel
Imports Xunit

Public Class ExcelExportTests

    <Fact>
    Public Sub AddWorksheetFromDataTable()
        Using table As New DataTable(), wb As New XLWorkbook()
            table.TableName = "Values"
            table.Columns.Add("Date", GetType(Date))
            table.Rows.Add(New DateTime(2001, 1, 1))
            table.Rows.Add(New DateTime(2002, 2, 2))

            FixDates(table)
            Dim ws As IXLWorksheet = wb.AddWorksheet(table)

            Assert.Equal("Date", ws.Cell("A1").Value)

            Dim val As Date
            Assert.True(ws.Cell("A2").TryGetValue(val))
            Assert.Equal(val, New DateTime(2001, 1, 1))

            Assert.True(ws.Cell("A3").TryGetValue(val))
            Assert.Equal(val, New DateTime(2002, 2, 2))
        End Using
    End Sub

    <Fact>
    Public Sub AddWorksheetWithInvalidDates()
        Using table As New DataTable(), wb As New XLWorkbook()
            table.TableName = "Values"
            table.Columns.Add("Date", GetType(Date))
            table.Rows.Add(New DateTime(1776, 7, 4))
            table.Rows.Add(New DateTime(2002, 2, 2))

            FixDates(table)
            Dim ws As IXLWorksheet = wb.AddWorksheet(table)

            Assert.Equal("Date", ws.Cell("A1").Value)

            Dim val As Date
            Assert.False(ws.Cell("A2").TryGetValue(val))
            Assert.Equal("", ws.Cell("A2").Value)

            Assert.True(ws.Cell("A3").TryGetValue(val))
            Assert.Equal(val, New DateTime(2002, 2, 2))
        End Using
    End Sub

    <Fact>
    Public Sub AddWorksheetWithDbNullDates()
        Using table As New DataTable(), wb As New XLWorkbook()
            table.TableName = "Values"
            table.Columns.Add("Date", GetType(Date))
            table.Rows.Add(DBNull.Value)
            table.Rows.Add(New DateTime(2002, 2, 2))

            FixDates(table)
            Dim ws As IXLWorksheet = wb.AddWorksheet(table)

            Assert.Equal("Date", ws.Cell("A1").Value)

            Dim val As Date
            Assert.False(ws.Cell("A2").TryGetValue(val))
            Assert.Equal("", ws.Cell("A2").Value)

            Assert.True(ws.Cell("A3").TryGetValue(val))
            Assert.Equal(val, New DateTime(2002, 2, 2))
        End Using
    End Sub

End Class
