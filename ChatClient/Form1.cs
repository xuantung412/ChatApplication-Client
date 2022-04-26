﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The .net wrapper around WinSock sockets.
        /// </summary>
        TcpClient _client;
        String userName;
        /// <summary>
        /// Buffer to store incoming messages from the server.
        /// </summary>
        byte[] _buffer = new byte[4096];

        public Form1(String userName)
        {
            InitializeComponent();
            _client = new TcpClient();
            this.userName = userName;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Connect to the remote server. The IP address and port # could be
            // picked up from a settings file.
            _client.Connect("127.0.0.1", 54000);

            // Start reading the socket and receive any incoming messages
            _client.GetStream().BeginRead(_buffer, 0, _buffer.Length, Server_MessageReceived, null);
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
                    var str = Encoding.ASCII.GetString(tmp);

                    // Any actions that involve interacting with the UI must be done
                    // on the main thread. This method is being called on a worker
                    // thread so using the form's BeginInvoke() method is vital to
                    // ensure that the action is performed on the main thread.
                    BeginInvoke((Action)(() =>
                    {
                        ChatListBox.Items.Add(str);
                        ChatListBox.SelectedIndex = ChatListBox.Items.Count - 1;
                    }));
                }

                // Clear the buffer and start listening again
                Array.Clear(_buffer, 0, _buffer.Length);
                _client.GetStream().BeginRead(_buffer, 0, _buffer.Length, Server_MessageReceived, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Encode the message and send it out to the server.
            var msg = Encoding.ASCII.GetBytes(this.userName + ":\t" + textBox1.Text);
            _client.GetStream().Write(msg, 0, msg.Length);
            ChatListBox.Items.Add("You:\t " + textBox1.Text);
            // Clear the text box and set it's focus
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void saveChatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog locationSelection = new SaveFileDialog();
            locationSelection.ShowDialog();
            string dataToWrite = "";
            foreach (string chatMessage in this.ChatListBox.Items)
            {
                dataToWrite += chatMessage + Environment.NewLine;
            }
            dataToWrite += Environment.NewLine +"Saved at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+Environment.NewLine;
            dataToWrite += "Created by Internal Chat Application. Author: Tran Xuan Tung NGUYEN " + Environment.NewLine;
            File.WriteAllText(locationSelection.FileName+".txt", dataToWrite);
            MessageBox.Show("Successfully Save Chat History");
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application created by Tran Xuan Tung NGUYEN.\n Email: xuan.tung.nguyen811@gmail.com");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.SendButton.PerformClick();
            }
        }
    }
}
    
