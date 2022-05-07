using FileSharing_FTP_Client;
using FileSharing_FTP_Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void p2pMaster_Click(object sender, EventArgs e)
        {
            Server newServer = new Server();
            newServer.Show();
            //this.Dispose();
        }

        private void p2pSlave_Click(object sender, EventArgs e)
        {
            Client newSlave = new Client();
            newSlave.Show();
            //this.Dispose();
        }

        private void ChatApp_Click(object sender, EventArgs e)
        {
            UserAuth newChatApp = new UserAuth();
            newChatApp.Show();
            //this.Dispose();
        }
    }
}
