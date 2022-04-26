using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace ChatClient
{
    public partial class UserAuth : Form
    {
        /// <summary>
        /// The .net wrapper around WinSock sockets.
        /// </summary>
        TcpClient _client;

        /// <summary>
        /// Buffer to store incoming messages from the server.
        /// </summary>
        byte[] _buffer = new byte[4096];
        public UserAuth()
        {
            InitializeComponent();
            _client = new TcpClient();
            // Connect to the remote server. The IP address and port # could be
            // picked up from a settings file.
            _client.Connect("127.0.0.1", 54000);

            // Start reading the socket and receive any incoming messages
            _client.GetStream().BeginRead(_buffer, 0, _buffer.Length, Server_MessageReceived, null);
        }

        private void loginLabel_Click(object sender, EventArgs e)
        {
            base.OnShown(e);




            //User login. Send message
            string messageString = "/CheckUser-" + this.usernameTextBox.Text + "-" + this.passwordTextBox.Text;
            var checkAuth = Encoding.ASCII.GetBytes(messageString);
            _client.GetStream().Write(checkAuth, 0, checkAuth.Length);

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Registration newRegistration = new Registration();
            newRegistration.Show();
        }

        private void Server_MessageReceived(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                // End the stream read
                var bytesIn = _client.GetStream().EndRead(ar);
                if (bytesIn > 0)
                {
                    // Create a string from the received data. For this server 
                    // our data is in the form of a simple string, but it could be
                    // binary data or a JSON object. Payload is your choice.
                    var tmp = new byte[bytesIn];
                    Array.Copy(_buffer, 0, tmp, 0, bytesIn);
                    String str = Encoding.ASCII.GetString(tmp);
                    //remove hidden char in string
                    string severMessage = new string(str.Where(c => !char.IsControl(c)).ToArray());
                    // Any actions that involve interacting with the UI must be done
                    // on the main thread. This method is being called on a worker
                    // thread so using the form's BeginInvoke() method is vital to
                    // ensure that the action is performed on the main thread.
                    BeginInvoke((Action)(() =>
                    {
                        if (severMessage.Equals("/AcceptLoggin"))
                        {
                            MessageBox.Show("Welcome to chat Sever","Loggin Successfully",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form1 newChat =new Form1(this.usernameTextBox.Text);
                            newChat.Show();
                            this.Hide();
                        }

                        if (severMessage.Equals("/RejectLoggin"))
                        {
                            this.passwordTextBox.Text = "";
                            this.usernameTextBox.Text = "";

                            usernameTextBox.Focus();
                            MessageBox.Show("Please check your User Name or Password","Invalid Account",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
                }

                // Clear the buffer and start listening again
                Array.Clear(_buffer, 0, _buffer.Length);
                _client.GetStream().BeginRead(_buffer, 0, _buffer.Length, Server_MessageReceived, null);
            }
        }

        private void UserAuth_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void UserAuth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoginButton.PerformClick();
            }
        }

        private void usernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoginButton.PerformClick();
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoginButton.PerformClick();
            }
        }
    }
}
