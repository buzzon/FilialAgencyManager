using Libs;
using System;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormGetQuaterData : Form
    {
        private readonly TableManager _tableManager;

        private readonly System.Net.Sockets.TcpClient _client;
        private readonly System.Net.Sockets.NetworkStream _stream;

        public FormGetQuaterData(System.Net.Sockets.TcpClient client)
        {
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
                    }
                }
            }

            LoadSubsidiaryInCombobox();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _tableManager.FillTable((DataGridView)sender);
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
            try
            {
                var quaterData = new QuaterDataSerialize(comboBoxSubsidiary.SelectedItem.ToString(), comboBoxQuarter.SelectedItem.ToString(), Tables, Titles);
                NetManager.Send(_stream, quaterData.Serialize(), CommandManager.Commands.QuaterDataSave);

                var input = NetManager.Receive(_client, _stream);
                MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataGridView[] Tables => new [] {
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

        private DataGridView[] _oldTables;

        private Label[] Titles => new [] {
            label1,
            label2,
            label3,
            label4,
            label5,
            label6,
            label7,
            label8,
            label9 };

        private void ButtonDownloadAnnualReport_Click(object sender, EventArgs e)
        {
            try
            {
                NetManager.Send(_stream, NetManager.ToBytes(comboBoxSubsidiary.SelectedItem.ToString()),
                                                            CommandManager.Commands.AnnualReport);

                var input = NetManager.Receive(_client, _stream);
                MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));

                var annualReport = NetManager.GetData(NetManager.Receive(_client, _stream));
                var annualReportData = new QuaterDataSerialize();
                annualReportData = annualReportData.Deserialize(annualReport);

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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
