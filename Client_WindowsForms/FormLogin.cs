using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormLogin : Form
    {
        private const int Port = 8888;
        private const string Address = "127.0.0.1";
        private readonly TcpClient _client;
        public NetworkStream Stream { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            try
            {
                _client = new TcpClient();
                _client.Connect(IPAddress.Parse(Address), Port);
                Stream = _client.GetStream();
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
                var getQuaterData = new FormGetQuaterData(_client);
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
