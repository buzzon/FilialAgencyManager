using System;
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
        public string subsidiary { get; private set; }
        public string quater { get; private set; }

        public QuaterDataSerialize()
        {

        }

        public QuaterDataSerialize(string subsidiary, string quater, DataGridView[] DataGridTables, Label[] LabelTitles)
        {
            Tables = GetDataTable(DataGridTables);
            Titles = GetString(LabelTitles);
            this.quater = quater;
            this.subsidiary = subsidiary;
        }

        private static string[] GetString(Label[] LabelTitles)
        {
            string[] titles = new string[LabelTitles.Length];

            for (int i = 0; i < LabelTitles.Length; i++)
                titles[i] = LabelTitles[i].Text;

            return titles;
        }

        private DataTable[] GetDataTable(DataGridView[] DataGridTables)
        {
            DataTable[] dataTables = new DataTable[DataGridTables.Length];
            for (int i = 0; i < dataTables.Length; i++)
                dataTables[i] = new DataTable();

            for (int i = 0; i < DataGridTables.Length; i++)
                foreach (DataGridViewColumn col in DataGridTables[i].Columns)
                    dataTables[i].Columns.Add(col.Name);

            for (int i = 0; i < dataTables.Length; i++)
                foreach (DataGridViewRow row in DataGridTables[i].Rows)
                {
                    DataRow dRow = dataTables[i].NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                        dRow[cell.ColumnIndex] = cell.Value;
                    dataTables[i].Rows.Add(dRow);
                }

            return dataTables;
        }

        public byte[] Serialize()
        {
            MemoryStream data = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(data, this);
                return data.ToArray(); ;
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
                subsidiary = quater.subsidiary;
            }
            else
            {
                for (int i = 0; i < Tables[0].Rows.Count; i++)
                    for (int j = 1; j < 3; j++)
                        Tables[0].Rows[i][j] = Convert.ToDouble(Tables[0].Rows[i][j]) + Convert.ToDouble(quater.Tables[0].Rows[i][j]);


                for (int j = 0; j < 2; j++)
                    Tables[1].Rows[0][j] = Convert.ToDouble(Tables[1].Rows[0][j]) + Convert.ToDouble(quater.Tables[1].Rows[0][j]);


                for (int i = 0; i < Tables[2].Rows.Count; i++)
                    for (int j = 1; j < 3; j++)
                        Tables[2].Rows[i][j] = Convert.ToDouble(Tables[2].Rows[i][j]) + Convert.ToDouble(quater.Tables[2].Rows[i][j]);

                for (int i = 0; i < Tables[3].Rows.Count; i++)
                    for (int j = 1; j < 4; j++)
                        Tables[3].Rows[i][j] = Convert.ToDouble(Tables[3].Rows[i][j]) + Convert.ToDouble(quater.Tables[3].Rows[i][j]);

                for (int j = 2; j < 4; j++)
                    Tables[4].Rows[0][j] = Convert.ToDouble(Tables[4].Rows[0][j]) + Convert.ToDouble(quater.Tables[4].Rows[0][j]);

                Tables[5].Rows[0][0] = Convert.ToDouble(Tables[5].Rows[0][0]) + Convert.ToDouble(quater.Tables[5].Rows[0][0]);

                Tables[6].Rows[0][0] = Convert.ToDouble(Tables[6].Rows[0][0]) + Convert.ToDouble(quater.Tables[6].Rows[0][0]);

                for (int i = 0; i < Tables[7].Rows.Count; i++)
                    for (int j = 1; j < 3; j++)
                        Tables[7].Rows[i][j] = Convert.ToDouble(Tables[7].Rows[i][j]) + Convert.ToDouble(quater.Tables[7].Rows[i][j]);

                for (int j = 0; j < 2; j++)
                    Tables[8].Rows[0][j] = Convert.ToDouble(Tables[8].Rows[0][j]) + Convert.ToDouble(quater.Tables[8].Rows[0][j]);

                for (int j = 0; j < 2; j++)
                    Tables[9].Rows[0][j] = Convert.ToDouble(Tables[9].Rows[0][j]) + Convert.ToDouble(quater.Tables[9].Rows[0][j]);

            }
        }

        public QuaterDataSerialize Deserialize(byte[] data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
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
