﻿using Libs;
using System;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormGetQuaterData : Form
    {
        private readonly TableManager _tableManager;

        private readonly System.Net.Sockets.TcpClient _client;
        private readonly System.Net.Sockets.NetworkStream _stream;

        public FormGetQuaterData(System.Net.Sockets.TcpClient client, Form mainForm)
        {
            MainForm = mainForm;
            InitializeComponent();

            _client = client;
            _stream = client.GetStream();

            _tableManager = new TableManager(Tables);
            TableManager.InitializeTables(Tables);

            _oldTables = Tables;
            for (var i = 0; i < Tables.Length; i++)
            {
                _oldTables[i] = new DataGridView();
                {
                    _oldTables[i].ColumnHeadersHeightSizeMode = Tables[i].ColumnHeadersHeightSizeMode;
                    for (var j = 0; j < Tables[i].Columns.Count; j++)
                    {
                        _oldTables[i].Columns.Add(j.ToString(), Tables[i].Columns[j].HeaderText);
                        _oldTables[i].Columns[j].Width = Tables[i].Columns[j].Width;
                        _oldTables[i].Columns[j].AutoSizeMode = Tables[i].Columns[j].AutoSizeMode;
                        _oldTables[i].Columns[j].ReadOnly = Tables[i].Columns[j].ReadOnly;
                        _oldTables[i].Columns[j].SortMode = Tables[i].Columns[j].SortMode;
                        _oldTables[i].Columns[j].DefaultCellStyle = Tables[i].Columns[j].DefaultCellStyle;
                    }
                }
            }

            LoadSubsidiaryInCombobox();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell.ColumnIndex == 1)
            {
                string cellTxt = (string)((DataGridView)sender).CurrentCell.Value;
                if (!double.TryParse(cellTxt, out double num))
                {
                    MessageBox.Show("Введены некорректные данные.");
                    ((DataGridView)sender).CurrentCell.Value = 0;
                    return;
                }
            }

            _tableManager.FillTable((DataGridView)sender);
        }


        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell.ColumnIndex != 1) return;
            var tb = (TextBox)e.Control;
            tb.KeyPress += tb_KeyPress;
        }

        private static void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void LoadSubsidiaryInCombobox()
        {
            try
            {
                comboBoxSubsidiary.Items.Clear();
                NetManager.Send(_stream, new byte[1], CommandManager.Commands.SubsidiaryLoad);
                var input = NetManager.Receive(_client, _stream);
                var message = NetManager.ToString(NetManager.GetData(input));
                var subsidiarys = message.Split('\n');
                for (var i = 0; i < subsidiarys.Length - 1; i++)
                    comboBoxSubsidiary.Items.Add(subsidiarys[i]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            if (comboBoxSubsidiary.SelectedItem == null)
            {
                MessageBox.Show(@"Не указан квартал или филиал.");
                return;
            }

            var quaterData = new QuaterDataSerialize(comboBoxSubsidiary.SelectedItem.ToString(), comboBoxQuarter.SelectedItem.ToString(), Tables, Titles);
            NetManager.Send(_stream, quaterData.Serialize(), CommandManager.Commands.QuaterDataSave);

            var input = NetManager.Receive(_client, _stream);
            MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));

        }

        private DataGridView[] Tables => new[] {
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

        private readonly DataGridView[] _oldTables;

        private Label[] Titles => new[] {
            label1,
            label2,
            label3,
            label4,
            label5,
            label6,
            label7,
            label8,
            label9 };

        public Form MainForm { get; set; }

        private void ButtonDownloadAnnualReport_Click(object sender, EventArgs e)
        {
            if (comboBoxSubsidiary.SelectedItem == null)
            {
                MessageBox.Show(@"Данного филиала не существует.");
                return;
            }

            NetManager.Send(_stream, NetManager.ToBytes(comboBoxSubsidiary.SelectedItem.ToString()),
                                                            CommandManager.Commands.AnnualReport);

            var input = NetManager.Receive(_client, _stream);
            MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));

            var annualReport = NetManager.GetData(NetManager.Receive(_client, _stream));
            var annualReportData = new QuaterDataSerialize();
            annualReportData = annualReportData.Deserialize(annualReport);

            if (annualReportData.Tables == null)
                return;

            for (var i = 0; i < Tables.Length; i++)
            {
                Tables[i].Columns.Clear();
                Tables[i].DataSource = annualReportData.Tables[i];
                Tables[i].ColumnHeadersHeightSizeMode = _oldTables[i].ColumnHeadersHeightSizeMode;
                for (var j = 0; j < Tables[i].Columns.Count; j++)
                {
                    Tables[i].Columns[j].Width = _oldTables[i].Columns[j].Width;
                    Tables[i].Columns[j].HeaderText = _oldTables[i].Columns[j].HeaderText;
                    Tables[i].Columns[j].AutoSizeMode = _oldTables[i].Columns[j].AutoSizeMode;
                    Tables[i].Columns[j].ReadOnly = _oldTables[i].Columns[j].ReadOnly;
                    Tables[i].Columns[j].SortMode = _oldTables[i].Columns[j].SortMode;
                    Tables[i].Columns[j].DefaultCellStyle = _oldTables[i].Columns[j].DefaultCellStyle;
                }
            }
        }

        private void buttonSaveExcel_Click(object sender, EventArgs e)
        {
            ExcelManager.Create(false);
            ExcelManager.Fill(Tables, comboBoxQuarter.Text, comboBoxSubsidiary.Text);
            ExcelManager.ExportToExcel();

        }

        private void buttonOpenExcel_Click(object sender, EventArgs e)
        {
            ExcelManager.Create(true);
            ExcelManager.Fill(Tables, comboBoxQuarter.Text, comboBoxSubsidiary.Text);
        }
    }
}
