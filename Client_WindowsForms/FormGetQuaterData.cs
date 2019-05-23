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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
