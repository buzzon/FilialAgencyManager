﻿using Libs;
using System;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormGetQuaterData : Form
    {
        private TableManager tableManager;

        System.Net.Sockets.TcpClient client;
        System.Net.Sockets.NetworkStream stream;

        public FormGetQuaterData(System.Net.Sockets.TcpClient client)
        {
            InitializeComponent();

            this.client = client;
            this.stream = client.GetStream();

            tableManager = new TableManager(Tables);
            TableManager.InitializeTables(Tables);

            LoadSubsidiaryInCombobox();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tableManager.FillTable((DataGridView)sender);
        }

        private void LoadSubsidiaryInCombobox()
        {
            try
            {
                comboBoxSubsidiary.Items.Clear();
                NetManager.Send(stream, string.Empty, CommandManager.Commands.SubsidiaryLoad);
                string input = NetManager.Receive(client, stream);
                comboBoxSubsidiary.Items.AddRange(NetManager.GetMessage(input).Split(NetManager.separator));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            QuaterDataSerialize quaterData = new QuaterDataSerialize(comboBoxSubsidiary.SelectedItem.ToString(), comboBoxQuarter.SelectedItem.ToString() , Tables, Titles);
            try
            {
                NetManager.Send(stream, quaterData.Serialize(), CommandManager.Commands.QuaterDataSave);

                string input = NetManager.Receive(client, stream);
                MessageBox.Show(NetManager.GetMessage(input));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void buttonDownloadAnnualReport_Click(object sender, EventArgs e)
        {

        }
    }
}
