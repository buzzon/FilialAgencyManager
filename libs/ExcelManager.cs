using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

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

            ExcelApp.Columns.ColumnWidth = 30;

            ExcelApp.Cells[1, 1] = quarter + " " + subsidiary; ;

            var startRow = 3;
            foreach (var table in Tables)
                FillTable(table, ref startRow);
        }

        private static void FillTable(DataGridView table, ref int startRow)
        {
            //storing headers
            for (var i = 0; i < table.ColumnCount; i++)
            {
                ExcelApp.Cells[startRow, i + 1] = table.Columns[i].HeaderText;
            }

            startRow++;

            //storing every cell to excel sheet
            for (var i = 0; i < table.ColumnCount; i++)
                for (var j = 0; j < table.RowCount; j++)
                    ExcelApp.Cells[startRow + j, i + 1] = table[i, j].Value.ToString();
            startRow += table.RowCount + 1;
        }

        private static void Save()
        {
            ExcelApp.ActiveWorkbook.SaveCopyAs(Dialog.FileName);
            ExcelApp.ActiveWorkbook.Saved = true;
            ExcelApp.Quit();
            Dialog?.Dispose();
        }
    }
}
