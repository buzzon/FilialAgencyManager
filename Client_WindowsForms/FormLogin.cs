using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormLogin : Form
    {
        private const int Port = 8888;
        private readonly TcpClient _client;
        public NetworkStream Stream { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            try
            {
                _client = new TcpClient();
                _client.Connect(IPAddress.Parse(GetIp()), Port);
                Stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static string GetIp()
        {
            if (!File.Exists(@"ip.ini"))
                throw new ArgumentException(@"Файл ip.ini не обнаружен");

            string ip;
            using (var sr = new StreamReader(@"ip.ini", Encoding.Default))
            {
                ip = sr.ReadLine();
                if (ip == "") throw new ArgumentException(@"Укажите адрес сервера в файле ip.ini в формате 0.0.0.0");
            }
            return ip;
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            var getQuaterData = new FormGetQuaterData(_client, this);
            Hide();
            try
            {
                getQuaterData.ShowDialog(this);
            }
            catch
            {
                // ignored
            }

            Close();
        }
    }
}
