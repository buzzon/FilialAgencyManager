using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Libs
{
    [Serializable]
    public class QuaterDataSerialize
    {
        public DataTable[] Tables { get; set; }
        public string[] Titles { get; private set; }
        public string Subsidiary { get; private set; }
        public string Quater { get; }

        public QuaterDataSerialize()
        {

        }

        public QuaterDataSerialize(string subsidiary, string quater, DataGridView[] dataGridTables, IReadOnlyList<Label> labelTitles)
        {
            Tables = GetDataTable(dataGridTables);
            Titles = GetString(labelTitles);
            Quater = quater;
            Subsidiary = subsidiary;
        }

        private static string[] GetString(IReadOnlyList<Label> labelTitles)
        {
            var titles = new string[labelTitles.Count];

            for (var i = 0; i < labelTitles.Count; i++)
                titles[i] = labelTitles[i].Text;

            return titles;
        }

        private static DataTable[] GetDataTable(IReadOnlyList<DataGridView> dataGridTables)
        {
            var dataTables = new DataTable[dataGridTables.Count];
            for (var i = 0; i < dataTables.Length; i++)
                dataTables[i] = new DataTable();

            for (var i = 0; i < dataGridTables.Count; i++)
                foreach (DataGridViewColumn col in dataGridTables[i].Columns)
                    dataTables[i].Columns.Add(col.Name);

            for (var i = 0; i < dataTables.Length; i++)
                foreach (DataGridViewRow row in dataGridTables[i].Rows)
                {
                    var dRow = dataTables[i].NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                        dRow[cell.ColumnIndex] = cell.Value;
                    dataTables[i].Rows.Add(dRow);
                }

            return dataTables;
        }

        public byte[] Serialize()
        {
            var data = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(data, this);
                return data.ToArray();
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }

        internal void AddData(QuaterDataSerialize quater)
        {
            if (Tables == null)
            {
                Tables = quater.Tables;
                Titles = quater.Titles;
                Subsidiary = quater.Subsidiary;
            }
            else
            {
                for (var i = 0; i < Tables[0].Rows.Count; i++)
                    for (var j = 1; j < 3; j++)
                        Tables[0].Rows[i][j] = Convert.ToDouble(Tables[0].Rows[i][j]) + Convert.ToDouble(quater.Tables[0].Rows[i][j]);

                for (var j = 0; j < 2; j++)
                    Tables[1].Rows[0][j] = Convert.ToDouble(Tables[1].Rows[0][j]) + Convert.ToDouble(quater.Tables[1].Rows[0][j]);

                for (var i = 0; i < Tables[2].Rows.Count; i++)
                    for (var j = 1; j < 3; j++)
                        Tables[2].Rows[i][j] = Convert.ToDouble(Tables[2].Rows[i][j]) + Convert.ToDouble(quater.Tables[2].Rows[i][j]);

                for (var i = 0; i < Tables[3].Rows.Count; i++)
                    for (var j = 1; j < 4; j++)
                        Tables[3].Rows[i][j] = Convert.ToDouble(Tables[3].Rows[i][j]) + Convert.ToDouble(quater.Tables[3].Rows[i][j]);

                for (var j = 2; j < 4; j++)
                    Tables[4].Rows[0][j] = Convert.ToDouble(Tables[4].Rows[0][j]) + Convert.ToDouble(quater.Tables[4].Rows[0][j]);

                Tables[5].Rows[0][0] = Convert.ToDouble(Tables[5].Rows[0][0]) + Convert.ToDouble(quater.Tables[5].Rows[0][0]);

                Tables[6].Rows[0][0] = Convert.ToDouble(Tables[6].Rows[0][0]) + Convert.ToDouble(quater.Tables[6].Rows[0][0]);

                for (var i = 0; i < Tables[7].Rows.Count; i++)
                    for (var j = 1; j < 3; j++)
                        Tables[7].Rows[i][j] = Convert.ToDouble(Tables[7].Rows[i][j]) + Convert.ToDouble(quater.Tables[7].Rows[i][j]);

                for (var j = 0; j < 2; j++)
                    Tables[8].Rows[0][j] = Convert.ToDouble(Tables[8].Rows[0][j]) + Convert.ToDouble(quater.Tables[8].Rows[0][j]);

                for (var j = 0; j < 2; j++)
                    Tables[9].Rows[0][j] = Convert.ToDouble(Tables[9].Rows[0][j]) + Convert.ToDouble(quater.Tables[9].Rows[0][j]);

            }
        }

        public QuaterDataSerialize Deserialize(byte[] data)
        {
            var binaryFormatter = new BinaryFormatter();
            try
            {
                return (QuaterDataSerialize)binaryFormatter.Deserialize(new MemoryStream(data));
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }
    }
}
