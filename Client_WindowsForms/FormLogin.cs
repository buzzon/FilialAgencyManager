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
            {
                var f = File.CreateText("ip.ini");
                f.Close();

                using (StreamWriter sw = new StreamWriter("ip.ini", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine("127.0.0.1");
                }
            }

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
            Hide();

            if (textBoxLogin.Text == "Admin")
            {
                try
                {
                    new FormAdministrator(_client).ShowDialog(this);
                }
                catch
                {
                    // ignored
                }
            }
            else
            {
                try
                {
                    new FormGetQuaterData(_client).ShowDialog(this);
                }
                catch
                {
                    // ignored
                }
            }
            Close();
        }
    }
}
