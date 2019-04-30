using Libs;
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
                NetManager.Send(stream, new byte[1], CommandManager.Commands.SubsidiaryLoad);
                byte[] input = NetManager.Receive(client, stream);

                string message = NetManager.ToString(NetManager.GetData(input));

                string[] subsidiarys = message.Split('\n');
                for (int i = 0; i < subsidiarys.Length - 1; i++)
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
                QuaterDataSerialize quaterData = new QuaterDataSerialize(comboBoxSubsidiary.SelectedItem.ToString(), comboBoxQuarter.SelectedItem.ToString(), Tables, Titles);
                NetManager.Send(stream, quaterData.Serialize(), CommandManager.Commands.QuaterDataSave);

                byte[] input = NetManager.Receive(client, stream);
                MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));
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

        private void ButtonDownloadAnnualReport_Click(object sender, EventArgs e)
        {
            try
            {
                NetManager.Send(stream, NetManager.ToBytes(comboBoxSubsidiary.SelectedItem.ToString()), CommandManager.Commands.AnnualReport);

                byte[] input = NetManager.Receive(client, stream);
                MessageBox.Show(NetManager.ToString(NetManager.GetData(input)));

                byte[] annualReport = NetManager.GetData(NetManager.Receive(client, stream));
                QuaterDataSerialize annualReportData = new QuaterDataSerialize();
                annualReportData = annualReportData.Deserialize(annualReport);

                for (int i = 0; i < Tables.Length; i++)
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
