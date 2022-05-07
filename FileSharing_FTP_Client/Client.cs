using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business_Layer;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace FileSharing_FTP_Client
{
    public partial class Client : Form
    {

        private IFTPServer Server = null;   //Variable will store connected sever. Default = null.
        public string fileLocation = "";

        public Client()
        {
            InitializeComponent();
        }
        /*
         * The basic idea is connecting to server and store the server as an Object so that we can called a function of a server without sending message or wait for server response.
         */
        private bool GetConnection()
        {
            bool connected = true;
            //Channel sinks connected to server by using IServerChannelSinkProvider and SoapFormatter
            //https://docs.microsoft.com/en-us/dotnet/api/system.runtime.remoting.channels.soapserverformattersinkprovider?view=netframework-4.8
            SoapServerFormatterSinkProvider soap = new SoapServerFormatterSinkProvider();
            BinaryServerFormatterSinkProvider binary = new BinaryServerFormatterSinkProvider();
            soap.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            binary.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            soap.Next = binary;

            Hashtable table = new Hashtable();
            table.Add("port", "0");

            TcpChannel channel = new TcpChannel(table, null, soap);
            ChannelServices.RegisterChannel(channel, false);
            //start connect to server and store as object.
            try
            {
                Server = (IFTPServer)Activator.GetObject(typeof(IFTPServer), string.Format("tcp://{0}:{1}/FTPServerAPP/ftpserver.svr", ServerIPValue.Text, ServerPortValue.Text));
            }
            catch(Exception ex)
            {  
                //Write to window log
                connected = false;
                EventLogger.Logger(ex, "Client - GetConnection");
            }

            if (Server == null)
            {
                connected = false;
                ChannelServices.UnregisterChannel(channel);
                MessageBox.Show("Cannot Connect to the Server", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return connected ;
            }
            //After connection, make server update everything.
            try
            {
                PostedData handler = new PostedData();
                handler.RefreshList += new EventHandler(handler_RefreshList);

                Server.PostedData += new PostedDataHandler(handler.Server_PostData);
                Server.Update += new UpdateHandler(handler.Server_Update);

                Server.Connect(MachineInfo.GetJustIP());

            }
            catch (Exception ex)
            {
                connected = false;
                ChannelServices.UnregisterChannel(channel);
                MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return connected;

        }

        /*
         * This method is used to notify client when there is an update from server( another client sharing a file and this client need to Refresh).
         * */
        void handler_RefreshList(object sender, EventArgs e)
        {
            MessageBox.Show("Please Refresh your list.", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void ServerPortValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void ServerIPValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        /*
         * This function is Upload a file for sharing.
         */
        private void ShareFolder_Click(object sender, EventArgs e)
        {
            //Open a dialog to select a file.
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "All|*.*";    //Acept all type of file.
                open.Multiselect = true;
                open.Title = "It selects the share folder for the FTP server.";
                if (open.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    List<UploadData> upload = new List<UploadData>();
                    foreach (string file in open.FileNames)
                    {
                        if ((new System.IO.FileInfo(file)).Length > 100000000) //Control the size of files.
                        {
                            MessageBox.Show("The file '" + file + "' size is more than 100MB, Please select a smaller file.", "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                        //After selected an uploading/ sharing files, set filename and path to upload to server.
                        UploadData data = new UploadData();
                        data.Filename = file.Split('\\')[file.Split('\\').Length - 1];
                        data.File = System.IO.File.ReadAllBytes(file);
                        upload.Add(data);
                    }
                
                    this.fileLocation = open.FileName;  //Store a file path of Client when It sharing. When there is a request from another client( peer/ slave), this variable will pass the file in the path to another peer
                    Server.Upload(MachineInfo.GetJustIP(), upload);

                //Open this as a server using TCP( Client- Server Architecture)
                this.operateAsServer();

            }
                Refresh_Click(null, null);
           
        }

        /**
         * This function is create connection to entertered server IP address. Display error message when there is an error.
         */
        private void StartClient_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ServerPortValue.Text))
            {
                MessageBox.Show("Please enter server port.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ServerIPValue.Text))
            {
                MessageBox.Show("Please enter server IP address.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ServerIPValue.Text, out ipAddress))
            {
                MessageBox.Show("Please enter correct server IP address.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!GetConnection())
                //If connection fail -> Stop this function and return nothing.
                return;
            //After successfully connected to server -> Enable other buttons and wait for user action.
            StartClient.Enabled = false;
            ShareFolder.Enabled = true;
            Refresh.Enabled = true;

        }


        /**
         * This function handle form closing. It will disconnect from server when you agree to exit from this Peer.
         */
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else
                {
                    if(Server != null)
                        Server.Disconnect(MachineInfo.GetJustIP());
                    Server = null;
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP File Sharing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = false;
            }

        }


        /**
         * This click event will trigger RefreshList funciton
         */
        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        /**
         * This function is take all available files from sever to display to combox box
         * For each available files, user can double-click
         */
        private void RefreshList()
        {
            if (ServerFileListView.InvokeRequired)
            {
                MethodInvoker invoker = new MethodInvoker(RefreshList);
                ServerFileListView.Invoke(invoker);
            }
            else
            {
                try
                {
                    //Get Files from sever as List
                    ServerFileListView.Items.Clear();
                    ServerFileListView.SuspendLayout();
                    List<Business_Layer.FileInfo> files = new List<Business_Layer.FileInfo>();
                    Server.GetFiles(out files);

                    //Display files from list to combo box.
                    foreach (Business_Layer.FileInfo file in files)
                    {
                        ListViewItem item = new ListViewItem((ServerFileListView.Items.Count + 1).ToString());
                        String[] getIP = file.Filename.Split(new String[] {"-IP-"},StringSplitOptions.None);
                        item.SubItems.Add(getIP[1]);
                        item.SubItems.Add(getIP[2]);
                        item.SubItems.Add(file.Size.ToString());
                        ServerFileListView.Items.Add(item);
                    }
                    ServerFileListView.ResumeLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /**
         * This function is download a selected files( from double clicked) from peer ID Address.. 
         * 
         */
        private void ServerFileListView_DockChanged(object sender, EventArgs e)
        {
            if (ServerFileListView.SelectedItems.Count < 1) //CHeck if server has files ? If no(count = 0) -> return null.
                return;
            //Get IP address from ListView to download.
            string selectedFileIP = ServerFileListView.SelectedItems[0].SubItems[1].Text;

            try
            {
                //Trying to connect and download file from Peer base on selected IP Peer Address
                requestFileDownload(ServerFileListView.SelectedItems[0].SubItems[1].Text);
            }
            catch(Exception e1)
            {
                MessageBox.Show("Invalid Remote Slave/ Peer to download. Reporting to Server to remove that Slave/ Peer", "Connection Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Server.removeFiles(selectedFileIP); //Report to server that IP Peer Address is not available for server remove that IP Peers and files.
            }

        }

        /*
         * This funciton is use TCP (Server- Client Architecture) to host a file.
         * The Basic idea is using message to request a service. Client send a message -> Server response with the request.
         */
        public void operateAsServer()
        {

            MessageBox.Show("Server Only allow 1 Slave for each IP( computer)!");
            while (true)
            {
                    //Create IP address for this Client base on IPv4 of this running machine
                    IPAddress localAdd = IPAddress.Parse(MachineInfo.GetJustIP());
                    TcpListener listener = new TcpListener(localAdd, 8000);
                    Console.WriteLine("Listening...");
                    listener.Start();   //start listening.

                    //incoming client connected
                    TcpClient client = listener.AcceptTcpClient();
                    //get the incoming data through a network stream
                    NetworkStream nwStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    //read incoming stream
                    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                    //convert the data received into a string
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);

                    if (dataReceived.Equals("Download File"))
                    {
                    //Receive as a command to download file.
                        Stream fileStream = File.OpenRead(this.fileLocation);
                        // Alocate memory space for the file
                        byte[] fileBuffer = new byte[fileStream.Length];
                        fileStream.Read(fileBuffer, 0, (int)fileStream.Length);
                        nwStream.Write(fileBuffer, 0, fileBuffer.GetLength(0));
                    }
                    else
                    {   //Receive not a command. Do not thing
                        Console.WriteLine("Received Message: " + dataReceived);
                    }

                    //Close and re-open using while loop.
                    client.Close();
                    listener.Stop();
                    Console.ReadLine();

            }


        }

        /*
         * This function is open a port to connect to identified server using TCP( CLient/ Server architecture).
         * 
         */
        public void requestFileDownload(string ip)
        {
            //Command send to server to request download file.
            string textToSend = "Download File";

            //Create TCP and connect to remote Peer/ Client
            TcpClient client = new TcpClient(ip, 8000);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //Send text to server to download file
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //Receive file from server as type byte[]
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            //Open a pop up window to save the file
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Download File.";
            save.SupportMultiDottedExtensions = false;
            save.Filter = "All|*.*";
            save.FileName = ServerFileListView.SelectedItems[0].SubItems[2].Text;
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                System.IO.File.WriteAllBytes(save.FileName, bytesToRead);
                MessageBox.Show(ServerFileListView.SelectedItems[0].SubItems[2].Text + " has been downloaded.","Download Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Console.ReadLine();
            client.Close();
        }
    }
}
