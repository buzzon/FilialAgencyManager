using Libs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormLogin : Form
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        TcpClient client = null;
        NetworkStream stream;

        public FormLogin()
        {
            InitializeComponent();
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(address), port);
                stream = client.GetStream();
                LoadSubsidiaryInCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSubsidiaryInCombobox()
        {
            comboBoxSubsidiary.Items.Clear();
            NetManager.Send(stream, string.Empty, CommandManager.Commands.SubsidiaryLoad);
            string input = NetManager.Receive(client, stream);
            comboBoxSubsidiary.Items.AddRange(NetManager.GetMessage(input).Split(NetManager.separator));
        }

        private void buttonSubsidiaryAdd_Click(object sender, EventArgs e)
        {
            NetManager.Send(stream, textBoxSubsidiaryAdd.Text, CommandManager.Commands.SubsidiaryAdd);
            string input = NetManager.Receive(client, stream);
            LoadSubsidiaryInCombobox();
            MessageBox.Show(NetManager.GetMessage(input));
        }

        private void buttonSendQuarterlyReport_Click(object sender, EventArgs e)
        {
            using (FormGetQuaterData getQuaterData = new FormGetQuaterData(client))
            {
                getQuaterData.ShowDialog(this);
            }
        }
    }
}
