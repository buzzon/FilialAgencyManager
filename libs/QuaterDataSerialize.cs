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
            DataTable[]  dataTables = new DataTable[DataGridTables.Length];
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
