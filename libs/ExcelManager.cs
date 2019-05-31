using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace Libs
{
    public static class ExcelManager
    {
        public static Excel.Application ExcelApp;
        public static SaveFileDialog Dialog { get; set; }
        public static Excel.Workbook WorkSheet { get; set; }


        public static void Create(bool open)
        {
            ExcelApp = new Excel.Application
            {
                Visible = open,
                SheetsInNewWorkbook = 1
            };
        }

        public static void ExportToExcel()
        {
            Dialog = new SaveFileDialog()
            {
                InitialDirectory = "C:",
                Title = "Save as Excel File",
                FileName = "",
                Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx|Excel Files(2013)|*.xlsx"
            };
            if (Dialog.ShowDialog() != DialogResult.Cancel)
                Save();
        }

        public static void Fill(DataGridView[] Tables, string quarter, string subsidiary)
        {
            WorkSheet = ExcelApp.Workbooks.Add(Type.Missing);
            var sheet = (Excel.Worksheet)ExcelApp.Worksheets.Item[1];
            sheet.Name = "Отчет за " + quarter.Split('-')[0] + " " + subsidiary;

            //ExcelApp.Columns.ColumnWidth = 30;

            var startRow = 1;
            for (var i = 0; i<Tables.Length; i++)
            {
                TableHeader(Tables, ref startRow, i);
                FillTable(Tables[i], ref startRow);
            }

        }

        private static void TableHeader(DataGridView[] Tables, ref int startRow, int i)
        {
            Excel.Range range = ExcelApp.Range[ExcelApp.Cells[startRow, 1], ExcelApp.Cells[startRow, Tables[i].ColumnCount]];
            range.Merge(Type.Missing);
            range.Cells.Font.Size = 12;
            range.Font.Bold = true;
            range.EntireRow.AutoFit();
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ExcelApp.Cells[startRow, 1] = nameTables[i];
            startRow++;

        }

        private static void FillTable(DataGridView table, ref int startRow)
        {
            DrawingTable(table, startRow);
            //storing headers
            for (var i = 0; i < table.ColumnCount; i++)
            {
                ExcelApp.Cells[startRow, i + 1]  = table.Columns[i].HeaderText;
                ExcelApp.Cells[startRow, i + 1].EntireRow.Font.Bold = true;
                ExcelApp.Cells[startRow, i + 1].Interior.ColorIndex = 15;
                ExcelApp.Columns[i + 1].AutoFit();               
            }

            startRow++;

            //storing every cell to excel sheet
            for (var i = 0; i < table.ColumnCount; i++)
                for (var j = 0; j < table.RowCount; j++)
                    ExcelApp.Cells[startRow + j, i + 1] = table[i, j].Value.ToString();
            startRow += table.RowCount + 1;

        }

        private static void DrawingTable(DataGridView table, int startRow)
        {
            Excel.Range range = ExcelApp.Range[ExcelApp.Cells[startRow, 1], 
                ExcelApp.Cells[startRow + table.RowCount, table.ColumnCount]];
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle =
                Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight =
                Excel.XlBorderWeight.xlThick;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle =
                Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).Weight = 
                Excel.XlBorderWeight.xlThick;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle =
                Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).Weight =
                Excel.XlBorderWeight.xlThick;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = 
                Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).Weight = 
                Excel.XlBorderWeight.xlThick;
            range.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = 
                Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = 
                Excel.XlLineStyle.xlContinuous;
        }

        private static void Save()
        {
            ExcelApp.ActiveWorkbook.SaveCopyAs(Dialog.FileName);
            ExcelApp.ActiveWorkbook.Saved = true;
            ExcelApp.Quit();
            Dialog?.Dispose();
        }

        private static string[] nameTables = new string[]
        {
            "ОБОРАЧИВАЕМОСТЬ ДЗ ПО ОТРАСЛЯМ",
            "ВЫПОЛНЕНИЕ ПЛАНА ПО ПОГАШЕНИЮ ПРОСРОЧЕННОЙ ДЗ",
            "ВЫРУЧКА НВД НА ОДНОГО КЛИЕНТА",
            "КАЧЕСТВО БИЛЛИНГА",
            "ПРОИЗВОДИТЕЛЬНОСТЬ",
            "КАЧЕСТВО ПРЕДОСТАВЛЯЕМЫХ УСЛУГ",
            "КАЧЕСТВО СОБЛЮДЕНИЯ НОРМАТИВНО-ПРАВОВОЙ БАЗЫ",
            "БЕЗОПАСНОСТЬ ТРУДА",
            "КАЧЕСТВО РАБОТЫ С ПЕРСОНАЛОМ",
            "КАЧЕСТВО РАБОТЫ С ПЕРСОНАЛОМ"
        };

    }
}
