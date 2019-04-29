using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_WindowsForms
{
    public partial class FormAdministrator : Form
    {
        public FormAdministrator()
        {
            InitializeComponent();
        }

        //private void buttonSubsidiaryAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        NetManager.Send(stream, textBoxSubsidiaryAdd.Text, CommandManager.Commands.SubsidiaryAdd);
        //        string input = NetManager.Receive(client, stream);
        //        LoadSubsidiaryInCombobox();
        //        MessageBox.Show(NetManager.GetMessage(input));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
