using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Business_Layer;

namespace FileSharing_FTP_Server
{
    public partial class Server : Form
    {

        public Server()
        {
            InitializeComponent();
            ServerIPValue.Text = MachineInfo.GetJustIP();            
        }

        /**
         * Same with client, server also you SoapFormat to create connection to client.
         * Reference: https://docs.microsoft.com/en-us/dotnet/api/system.runtime.remoting.channels.soapserverformattersinkprovider?view=netframework-4.8
         * The Client will use TCP for hosting a file. The server should use another method to listening if they are on the same machine.
         */
        private void EstablishRemote()
        {
            SoapServerFormatterSinkProvider soap = new SoapServerFormatterSinkProvider();
            BinaryServerFormatterSinkProvider binary = new BinaryServerFormatterSinkProvider();
            soap.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            binary.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            soap.Next = binary;

            Hashtable table = new Hashtable();
            table.Add("port", ServerPortValue.Text);
            //Open server port and connection
            TcpChannel channel = new TcpChannel(table, null, soap);

            FTPServer.Logger = Logger;

            ChannelServices.RegisterChannel(channel,false);
            RemotingConfiguration.ApplicationName = "FTPServerAPP";
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FTPServer),"ftpserver.svr", WellKnownObjectMode.Singleton);

            Logger.Text += Environment.NewLine+ "***** TCP Channel has been published... *****";
        }

        /**
         * It handles the ServerPortValue textbox's Keypress event
         */

        private void ServerPortValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        /**
         * This function handle user button to start server. It will require sever port to start running.
         * 
         */
        private void StartServer_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ServerPortValue.Text)) //Check port of server valid.
            {
                MessageBox.Show("Please enter server port.", "Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EstablishRemote();
            ServerPortValue.ReadOnly  = true;
            StartServer.Enabled = false;
            ServerStatusMessage.Text = "Server has been started...";
        }

        /**
         * This function handle exit application. 
         */
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes )
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }


          
        }

        private void ServerIPValue_Click(object sender, EventArgs e)
        {

        }

        private void ServerPortValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void Logger_TextChanged(object sender, EventArgs e)
        {

        }

        private void ServerIP_Click(object sender, EventArgs e)
        {

        }

    }
}
