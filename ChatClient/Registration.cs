using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{

    public partial class Registration : Form
    {
        public TcpClient _client;
        byte[] _buffer = new byte[4096];
        Boolean checkUserAccountFromSever = false;
        string severMessageForRegistration = "";
        public Registration()
        {
            InitializeComponent();
            _client = new TcpClient();

        }

        private void Registration_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void registrationButton_Click(object sender, EventArgs e)
        {

            if (!(this.registrationPasswordTextBox.Text.Equals(this.registrationRepeatTextBox.Text)))
            {
                MessageBox.Show("Repeat Password Does Not Match. Please check!");
            }
            else
            {
                String registerAccount = "/RegisterAccount-" + this.registrationUserNameTextBox.Text + "-" + this.registrationPasswordTextBox.Text;
                var userNameAndPassword = Encoding.ASCII.GetBytes(registerAccount);
                //send message to sever
                _client.GetStream().Write(userNameAndPassword, 0, userNameAndPassword.Length);
                while (!this.checkUserAccountFromSever)
                {
                    await Task.Delay(1000);
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Connect to the remote server. The IP address and port # could be
            // picked up from a settings file.
            _client.Connect("127.0.0.1", 54000);

            // Start reading the socket and receive any incoming messages
            _client.GetStream().BeginRead(_buffer, 0, _buffer.Length,Server_MessageReceived,null);
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
                    string messageFromSever = Encoding.ASCII.GetString(tmp);
                    //remove hidden char in string
                    string severMessage = new string(messageFromSever.Where(c => !char.IsControl(c)).ToArray());
                    // Any actions that involve interacting with the UI must be done
                    // on the main thread. This method is being called on a worker
                    // thread so using the form's BeginInvoke() method is vital to
                    // ensure that the action is performed on the main thread.
                    BeginInvoke((Action)(() =>
                    {

                        if (severMessage.Equals("/AcceptNewRegistration"))
                        {
                            MessageBox.Show("Congradiation! Registration sucessfully.");
                            this.Dispose();
                        }
                        if (severMessage.Equals("/RejectRegistration"))
                        {
                            MessageBox.Show("Invalid! Your username already exist.");
                            this.registrationRepeatTextBox.Text = "";
                            this.registrationPasswordTextBox.Text = "";

                        }
                    }));
                }
                // Clear the buffer and start listening again
                Array.Clear(_buffer, 0, _buffer.Length);
                _client.GetStream().BeginRead(_buffer,0,_buffer.Length,Server_MessageReceived,null);
            }
        }

        private void Registration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.registrationButton.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelButton.PerformClick();
            }
        }

        private void registrationUserNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.registrationButton.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelButton.PerformClick();
            }
        }

        private void registrationPasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.registrationButton.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelButton.PerformClick();
            }
        }

        private void registrationRepeatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.registrationButton.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelButton.PerformClick();
            }
        }

        private void registrationIDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.registrationButton.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelButton.PerformClick();
            }
        }



        private void registrationRepeatTextBox_Leave(object sender, EventArgs e)
        {
            if (this.registrationPasswordTextBox.Text.Length > 0)
            {
                if (!(this.registrationPasswordTextBox.Text.Equals(this.registrationRepeatTextBox.Text)))
                {
                    MessageBox.Show("Repeat Password Does Not Match.");
                }
            }
        }
    }
}