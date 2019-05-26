using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    internal class TableManager
    {
        private delegate void TableFiller();
        private readonly Dictionary<DataGridView, TableFiller> _tablesFillers 
            = new Dictionary<DataGridView, TableFiller>();

        public TableManager(DataGridView[] tables)
        {
            var tableFillers = GetTableFillers(tables);
            for (var i = 0; i < tables.Length; i++)
                _tablesFillers[tables[i]] = tableFillers[i];
        }


        public static void InitializeTables(DataGridView[] tables)
        {
            tables[0].Rows.Add("Промышленность", "0", "0", "0");
            tables[0].Rows.Add("Население", "0", "0", "0");
            tables[0].Rows.Add("Бюджет", "0", "0", "0");
            tables[0].Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0");
            tables[0].Rows.Add("Прочее", "0", "0", "0");
            tables[1].Rows.Add("0", "0", "0", "0");
            tables[2].Rows.Add("Физическое", "0", "0", "0");
            tables[2].Rows.Add("Юридическое", "0", "0", "0");
            tables[3].Rows.Add("Физическое", "0", "0", "1", "0");
            tables[3].Rows.Add("Юридическое", "0", "0", "1", "0");
            tables[4].Rows.Add("0", "0", "0", "0", "0");
            tables[5].Rows.Add("0");
            tables[6].Rows.Add("0", "0", "0");
            tables[7].Rows.Add("Инциденты", "0", "0", "0");
            tables[7].Rows.Add("Техника безопасности", "0", "0", "0");
            tables[8].Rows.Add("0", "0", "0");
            tables[9].Rows.Add("0", "0", "0");
        }

        public void FillTable(DataGridView table)
        {
            _tablesFillers[table]();
        }

        private static bool TryParseCells<T>(IReadOnlyList<DataGridViewCell> cells, out T[] tableParams) where T : IComparable
        {
            var t = typeof(T);
            var method = t.GetMethod("TryParse", new [] { typeof(string), typeof(T).MakeByRefType() });

            tableParams = new T[cells.Count];
            for (var i = 0; i < cells.Count; i++)
            {
                var parameters = new object[] { cells[i].Value.ToString(), null };
                if (method != null && (!(bool)method.Invoke(null, parameters) || tableParams[i].CompareTo(default(T)) < 0))
                    return false;
                tableParams[i] = (T)parameters[1];
            }
            return true;
        }

        private TableFiller[] GetTableFillers(DataGridView[] tables)
        {
            var tablesFillers = new TableFiller[tables.Length];
            tablesFillers[0] = () =>
            {
                for (var i = 0; i < tables[0].RowCount; i++)
                {
                    var cells = new[] {tables[0][1, i], tables[0][2, i]};
                    if (!TryParseCells(cells, out double[] parameters) || parameters[1] <= 0) continue;
                    tables[0][3, i].Value = parameters[0] / parameters[1] * 366;
                }
            };

            tablesFillers[1] = () =>
            {
                DataGridViewCell[] cells = { tables[1][0, 0], tables[1][1, 0] };
                if (!TryParseCells(cells, out double[] parameters) || parameters[1] <= 0)
                    return;
                tables[1][2, 0].Value = parameters[0] / parameters[1] * 100;
                var cellValue = (double)tables[1][2, 0].Value;
                if (cellValue >= 120)
                    tables[1][3, 0].Value = 6;
                else if (cellValue >= 110)
                    tables[1][3, 0].Value = 5;
                else if (cellValue >= 100)
                    tables[1][3, 0].Value = 4;
                else if (cellValue >= 95)
                    tables[1][3, 0].Value = 3;
                else if (cellValue >= 90)
                    tables[1][3, 0].Value = 2;
                else
                    tables[1][3, 0].Value = 1;
            };

            tablesFillers[2] = () =>
            {
                var documentsCount = 0;
                for (var i = 0; i < tables[2].RowCount; i++)
                {
                    var cells = new [] { tables[2][1, i], tables[2][2, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0) continue;
                    tables[2][3, i].Value = (double) parameters[0] / parameters[1];
                    documentsCount += parameters[1];
                }
                tables[6][1, 0].Value = documentsCount;
                FillTable(tables[6]);
            };

            tablesFillers[3] = () =>
            {
                for (var i = 0; i < tables[3].RowCount; i++)
                {
                    var cells = new [] { tables[3][1, i], tables[3][2, i], tables[3][3, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0) continue;
                    tables[3][4, i].Value = (double) parameters[0] / parameters[1] * parameters[2] * 100;
                }
                tables[4][0, 0].Value = tables[3][2, 0].Value;
                tables[4][1, 0].Value = tables[3][2, 1].Value;
                FillTable(tables[4]);
            };

            tablesFillers[4] = () =>
            {
                DataGridViewCell[] cells = { tables[4][0, 0], tables[4][1, 0], tables[4][3, 0] };
                if (!TryParseCells(cells, out int[] parameters) || parameters[2] <= 0) return;
                tables[4][4, 0].Value = (double) (parameters[0] + parameters[1]) / parameters[2];
            };

            tablesFillers[5] = () => { };

            tablesFillers[6] = () =>
            {
                DataGridViewCell[] cells = { tables[6][0, 0], tables[6][1, 0] };
                if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0) return;
                tables[6][2, 0].Value = (double) parameters[0] / parameters[1] * 1000000;
            };

            tablesFillers[7] = () =>
            {
                for (var i = 0; i < tables[7].Rows.Count; i++)
                {
                    var cells = new [] { tables[7][1, i], tables[7][2, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0) continue;
                    tables[7][3, i].Value = (double) parameters[0] / parameters[1] * 100;
                }
            };

            tablesFillers[8] = () =>
            {
                DataGridViewCell[] cells = { tables[8][0, 0], tables[8][1, 0] };
                if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                    return;
                tables[8][2, 0].Value = (double)parameters[0] / parameters[1] * 100;
            };

            tablesFillers[9] = () =>
            {
                DataGridViewCell[] cells = { tables[9][0, 0], tables[9][1, 0] };
                if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                    return;
                tables[9][2, 0].Value = (double)parameters[0] / parameters[1] * 100;
            };

            return tablesFillers;
        }

    }
}
