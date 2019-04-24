using Libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
            NetManager.Send(stream, string.Empty, NetManager.Commands.SubsidiaryLoad);
            string input = NetManager.Receive(client, stream);
            comboBoxSubsidiary.Items.AddRange(NetManager.GetMessage(input).Split(NetManager.separator));
        }

        private void buttonSubsidiaryAdd_Click(object sender, EventArgs e)
        {
            NetManager.Send(stream, textBoxSubsidiaryAdd.Text, NetManager.Commands.SubsidiaryAdd);

            string input = NetManager.Receive(client, stream);
            MessageBox.Show(NetManager.GetMessage(input));

            comboBoxSubsidiary.Items.Clear();
            LoadSubsidiaryInCombobox();
        }
    }
}
