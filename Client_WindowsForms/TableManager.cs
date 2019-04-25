using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    class TableManager
    {
        private delegate void TableFiller();
        private readonly Dictionary<DataGridView, TableFiller> tablesFillers = new Dictionary<DataGridView, TableFiller>();

        public TableManager(DataGridView[] tables)
        {
            TableFiller[] TableFillers = GetTableFillers(tables);
            for (int i = 0; i < tables.Length; i++)
                tablesFillers[tables[i]] = TableFillers[i];
        }


        public static void InitializeTables(DataGridView[] Tables)
        {
            Tables[0].Rows.Add("Промышленность", "0", "0", "0");
            Tables[0].Rows.Add("Население", "0", "0", "0");
            Tables[0].Rows.Add("Бюджет", "0", "0", "0");
            Tables[0].Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0");
            Tables[0].Rows.Add("Прочее", "0", "0", "0");
            Tables[1].Rows.Add("0", "0", "0", "0");
            Tables[2].Rows.Add("Физическое", "0", "0", "0");
            Tables[2].Rows.Add("Юридическое", "0", "0", "0");
            Tables[3].Rows.Add("Физическое", "0", "0", "1", "0");
            Tables[3].Rows.Add("Юридическое", "0", "0", "1", "0");
            Tables[4].Rows.Add("0", "0", "0", "0", "0");
            Tables[5].Rows.Add("0");
            Tables[6].Rows.Add("0", "0", "0");
            Tables[7].Rows.Add("Инциденты", "0", "0", "0");
            Tables[7].Rows.Add("Техника безопасности", "0", "0", "0");
            Tables[8].Rows.Add("0", "0", "0");
            Tables[9].Rows.Add("0", "0", "0");
        }

        public void FillTable(DataGridView table)
        {
            tablesFillers[table]();
        }

        private bool TryParseCells<T>(DataGridViewCell[] cells, out T[] tableParams) where T : IComparable
        {
            Type t = typeof(T);
            var method = t.GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });

            tableParams = new T[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                var parametrs = new object[] { cells[i].Value.ToString(), null };
                if (!(bool)method.Invoke(null, parametrs) || tableParams[i].CompareTo(default(T)) < 0)
                    return false;
                tableParams[i] = (T)parametrs[1];
            }
            return true;
        }

        private TableFiller[] GetTableFillers(DataGridView[] tables)
        {
            TableFiller[] tablesFillers = new TableFiller[tables.Length];
            tablesFillers[0] = () =>
            {
                DataGridViewCell[] cells;
                for (int i = 0; i < tables[0].RowCount; i++)
                {
                    cells = new DataGridViewCell[] { tables[0][1, i], tables[0][2, i] };
                    if (!TryParseCells(cells, out double[] parameters) || parameters[1] <= 0)
                        continue;
                    tables[0][3, i].Value = parameters[0] * 365 / parameters[1];
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
                int documentsCount = 0;
                DataGridViewCell[] cells;
                for (var i = 0; i < tables[2].RowCount; i++)
                {
                    cells = new DataGridViewCell[] { tables[2][1, i], tables[2][2, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                        continue;
                    tables[2][3, i].Value = (double)parameters[0] / parameters[1];
                    documentsCount += parameters[1];
                }
                tables[6][1, 0].Value = documentsCount;
                FillTable(tables[6]);
            };

            tablesFillers[3] = () =>
            {
                DataGridViewCell[] cells;
                for (var i = 0; i < tables[3].RowCount; i++)
                {
                    cells = new DataGridViewCell[] { tables[3][1, i], tables[3][2, i], tables[3][3, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                        continue;
                    tables[3][4, i].Value = (double)parameters[0] / parameters[1] * parameters[2] * 100;
                }
                tables[4][0, 0].Value = tables[3][2, 0].Value;
                tables[4][1, 0].Value = tables[3][2, 1].Value;
                FillTable(tables[4]);
            };

            tablesFillers[4] = () =>
            {
                DataGridViewCell[] cells = { tables[4][0, 0], tables[4][1, 0], tables[4][3, 0] };
                if (!TryParseCells(cells, out int[] parameters))
                    return;
                if (parameters[2] > 0)
                    tables[4][4, 0].Value = (double)(parameters[0] + parameters[1]) / parameters[2];
            };

            tablesFillers[5] = () => { };

            tablesFillers[6] = () =>
            {
                DataGridViewCell[] cells = { tables[6][0, 0], tables[6][1, 0] };
                if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                    return;
                tables[6][2, 0].Value = (double)parameters[0] / parameters[1];
            };

            tablesFillers[7] = () =>
            {
                DataGridViewCell[] cells;
                for (int i = 0; i < tables[7].Rows.Count; i++)
                {
                    cells = new DataGridViewCell[] { tables[7][1, i], tables[7][2, i] };
                    if (!TryParseCells(cells, out int[] parameters) || parameters[1] <= 0)
                        continue;
                    tables[7][3, i].Value = (double)parameters[0] / parameters[1];
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
