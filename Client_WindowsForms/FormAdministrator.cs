using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libs;

namespace Client_WindowsForms
{
    public partial class FormAdministrator : Form
    {
        private readonly System.Net.Sockets.TcpClient _client;
        private readonly System.Net.Sockets.NetworkStream _stream;

        public FormAdministrator(System.Net.Sockets.TcpClient client)
        {
            InitializeComponent();

            try
            {
                _client = client;
                _stream = client.GetStream();
                LoadSubsidiaryInCombobox();
            }
            catch (Exception)
            {
                Close();
            }
        }

        private void buttonSubsidiaryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSubsidiaryAdd.Text == "")
                    return;
                NetManager.Send(_stream, NetManager.ToBytes(textBoxSubsidiaryAdd.Text), CommandManager.Commands.SubsidiaryAdd);
                var input = NetManager.Receive(_client, _stream);
                var message = NetManager.ToString(NetManager.GetData(input));
                MessageBox.Show(message);
                LoadSubsidiaryInCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void buttonSubsidiaryDel_Click(object sender, EventArgs e)
        {
            if (comboBoxSubsidiary.SelectedItem == null)
            {
                MessageBox.Show(@"Филиал не указан или указан неверно.");
                return;
            }

            NetManager.Send(_stream, NetManager.ToBytes(comboBoxSubsidiary.SelectedItem.ToString()), CommandManager.Commands.SubsidiaryRemove);

            var input = NetManager.Receive(_client, _stream);
            var message = NetManager.ToString(NetManager.GetData(input));
            MessageBox.Show(message);

            LoadSubsidiaryInCombobox();
            comboBoxSubsidiary.Text = "";
        }
    }
}
