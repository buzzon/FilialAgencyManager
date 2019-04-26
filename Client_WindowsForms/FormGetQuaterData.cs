﻿using Libs;
using System;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormGetQuaterData : Form
    {
        private TableManager tableManager;
        private string subsidiary;

        System.Net.Sockets.TcpClient client;
        System.Net.Sockets.NetworkStream stream;

        public FormGetQuaterData(System.Net.Sockets.TcpClient client, string subsidiary)
        {
            InitializeComponent();

            this.client = client;
            this.stream = client.GetStream();
            this.subsidiary = subsidiary;

            tableManager = new TableManager(Tables);
            TableManager.InitializeTables(Tables);
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tableManager.FillTable((DataGridView)sender);
        }

        private DataGridView[] Tables => new DataGridView[] {
            first_dataGridView,
            second_dataGridView,
            third_dataGridView,
            fourth_dataGridView,
            fifth_dataGridView,
            sixth_dataGridView,
            seventh_dataGridView,
            eighth_dataGridView,
            ninth_dataGridView,
            tenth_dataGridView };

        private Label[] Titles => new Label[] {
            label1,
            label2,
            label3,
            label4,
            label5,
            label6,
            label7,
            label8,
            label9 };

        private void buttonSend_Click(object sender, EventArgs e)
        {
            QuaterDataSerialize quaterData = new QuaterDataSerialize(subsidiary, comboBoxQuarter.SelectedItem.ToString() , Tables, Titles);
            byte[] vs = quaterData.Serialize();
            QuaterDataSerialize quaterDataDes = new QuaterDataSerialize();
            quaterDataDes = quaterDataDes.Deserialize(vs);
            try
            {
                NetManager.Send(stream, quaterData.Serialize(), CommandManager.Commands.QuaterDataSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
