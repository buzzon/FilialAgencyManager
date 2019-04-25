using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    [Serializable]
    class QuaterDataSerialize
    {
        private DataGridView[] Tables;
        private Label[] Titles;

        public void SetData(DataGridView[] Tables, Label[] Titles)
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
