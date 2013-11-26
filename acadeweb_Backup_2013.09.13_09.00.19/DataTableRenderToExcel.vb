Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Web
Imports NPOI
Imports NPOI.HPSF
Imports NPOI.HSSF
Imports NPOI.HSSF.UserModel
Imports NPOI.POIFS
Imports NPOI.Util

Public Class DataTableRenderToExcel
    Public Shared Function RenderDataTableToExcel(SourceTable As DataTable) As Stream
        Dim workbook As New HSSFWorkbook()
        Dim ms As New MemoryStream()
        Dim sheet As HSSFSheet = workbook.CreateSheet()
        Dim headerRow As HSSFRow = sheet.CreateRow(0)

        ' handling header.
        For Each column As DataColumn In SourceTable.Columns
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName)
        Next

        ' handling value.
        Dim rowIndex As Integer = 1

        For Each row As DataRow In SourceTable.Rows
            Dim dataRow As HSSFRow = sheet.CreateRow(rowIndex)

            For Each column As DataColumn In SourceTable.Columns
                Dim d As Double

                If Double.TryParse(row.Item(column).ToString, d) Then
                    dataRow.CreateCell(column.Ordinal).SetCellValue(d)
                Else
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row.Item(column).ToString)
                End If
            Next

            rowIndex += 1
        Next

        workbook.Write(ms)
        ms.Flush()
        ms.Position = 0

        sheet = Nothing
        headerRow = Nothing
        workbook = Nothing

        Return ms
    End Function

    Public Shared Sub RenderDataTableToExcel(SourceTable As DataTable, FileName As String)
        Dim ms As MemoryStream = TryCast(RenderDataTableToExcel(SourceTable), MemoryStream)
        Dim fs As New FileStream(FileName, FileMode.Create, FileAccess.Write)
        Dim data As Byte() = ms.ToArray()

        fs.Write(data, 0, data.Length)
        fs.Flush()
        fs.Close()

        data = Nothing
        ms = Nothing
        fs = Nothing
    End Sub

    Public Shared Function RenderDataTableFromExcel(ExcelFileStream As Stream, SheetName As String, HeaderRowIndex As Integer) As DataTable
        Dim workbook As New HSSFWorkbook(ExcelFileStream)
        Dim sheet As HSSFSheet = workbook.GetSheet(SheetName)

        Dim table As New DataTable()

        Dim headerRow As HSSFRow = sheet.GetRow(HeaderRowIndex)
        Dim cellCount As Integer = headerRow.LastCellNum

        For i As Integer = headerRow.FirstCellNum To cellCount - 1
            Dim column As New DataColumn(headerRow.GetCell(i).StringCellValue)
            table.Columns.Add(column)
        Next

        Dim rowCount As Integer = sheet.LastRowNum

        For i As Integer = (sheet.FirstRowNum + 1) To sheet.LastRowNum - 1
            Dim row As HSSFRow = sheet.GetRow(i)
            Dim dataRow As DataRow = table.NewRow()

            For j As Integer = row.FirstCellNum To cellCount - 1
                dataRow(j) = row.GetCell(j)
            Next
        Next

        ExcelFileStream.Close()
        workbook = Nothing
        sheet = Nothing
        Return table
    End Function

    Public Shared Function RenderDataTableFromExcel(ExcelFileStream As Stream, SheetIndex As Integer, HeaderRowIndex As Integer) As DataTable
        Dim workbook As New HSSFWorkbook(ExcelFileStream)
        Dim sheet As HSSFSheet = workbook.GetSheetAt(SheetIndex)

        Dim table As New DataTable()

        Dim headerRow As HSSFRow = sheet.GetRow(HeaderRowIndex)
        Dim cellCount As Integer = headerRow.LastCellNum

        For i As Integer = headerRow.FirstCellNum To cellCount - 1
            Dim column As New DataColumn(headerRow.GetCell(i).StringCellValue)
            table.Columns.Add(column)
        Next

        Dim rowCount As Integer = sheet.LastRowNum

        For i As Integer = (sheet.FirstRowNum + 1) To sheet.LastRowNum - 1
            Dim row As HSSFRow = sheet.GetRow(i)
            Dim dataRow As DataRow = table.NewRow()

            For j As Integer = row.FirstCellNum To cellCount - 1
                If row.GetCell(j) IsNot Nothing Then
                    dataRow(j) = row.GetCell(j).ToString()
                End If
            Next

            table.Rows.Add(dataRow)
        Next

        ExcelFileStream.Close()
        workbook = Nothing
        sheet = Nothing
        Return table
    End Function
End Class
