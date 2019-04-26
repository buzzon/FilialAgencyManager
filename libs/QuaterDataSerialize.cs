using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Libs
{
    [Serializable]
    public class QuaterDataSerialize
    {
        public DataTable[] Tables { get; set; }
        public Label[] Titles { get; private set; }

        public QuaterDataSerialize(DataGridView[] DataGridTables, Label[] Titles)
        {
            //DataGridView[] to DataTable[]
            Tables = new DataTable[DataGridTables.Length];
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new DataTable();
            }
            for (int i = 0; i < DataGridTables.Length; i++)
                foreach (DataGridViewColumn col in DataGridTables[i].Columns)
                    Tables[i].Columns.Add(col.Name);

            for (int i = 0; i < Tables.Length; i++)
                foreach (DataGridViewRow row in DataGridTables[i].Rows)
                {
                    DataRow dRow = Tables[i].NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    this.Tables[i].Rows.Add(dRow);
                }

            this.Titles = Titles;
        }

        public MemoryStream Serialize()
        {
            MemoryStream data = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(data, this);
                return data;
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }

        public string SerializeToString()
        {
            MemoryStream data = Serialize();
            byte[] bytes = new byte[data.Length];
            if (data.Length <= Int32.MaxValue)
            {
                return Encoding.UTF8.GetString(bytes, 0, (Int32)data.Length);
            }

            return ReadFromBuffer(data);
        }

        private static string ReadFromBuffer(MemoryStream data)
        {
            byte[] bytes = new byte[data.Length];
            string output = String.Empty;

            while (data.Position < data.Length)
            {
                int nBytes = data.Read(bytes, 0, bytes.Length);
                int nChars = Encoding.UTF8.GetCharCount(bytes, 0, nBytes);
                char[] chars = new char[nChars];
                nChars = Encoding.UTF8.GetChars(bytes, 0, nBytes, chars, 0);
                output += new String(chars, 0, nChars);
            }

            return output;
        }

        public QuaterDataSerialize Deserialize(MemoryStream data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                return (QuaterDataSerialize)binaryFormatter.Deserialize(data);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }
    }
}
