using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    [Serializable]
    public class QuaterDataSerialize
    {
        public DataGridView[] Tables { get; private set; }
        public Label[] Titles { get; private set; }

        public QuaterDataSerialize(DataGridView[] Tables, Label[] Titles)
        {
            this.Tables = Tables;
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
