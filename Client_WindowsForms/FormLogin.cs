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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            try
            {
                FormGetQuaterData getQuaterData = new FormGetQuaterData(client);
                Hide();
                getQuaterData.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}
