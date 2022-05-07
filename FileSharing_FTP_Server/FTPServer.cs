/**
 * User: crossdeck
 * Published: 3 Jul 2016
 * Title: file-transfer-p2p
 * Link: https://github.com/crossdeck/file-transfer-p2p
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSharing_FTP_Server
{

    public class FTPServer : System.MarshalByRefObject , Business_Layer.IFTPServer 
    {
        public static System.Windows.Forms.TextBox Logger = null;

        /*
         * This function is display message on log( Server).
         */
        public void AddLog(string text)
        {
            if (Logger.InvokeRequired)
            {
                Action<string> invoker = new Action<string>(AddLog);
                Logger.Invoke(invoker, text);
            }
            else
            {
                Logger.Text +=Environment.NewLine + text;
            }
        }

        public void Connect(string user)
        {
            if (Logger != null)
            {
                if (Logger.InvokeRequired)
                {
                    Action<string> invoker = new Action<string>(Connect);
                    Logger.Invoke(invoker,user);
                }
                else
                {
                    Logger.Text += string.Format("{0}>{1} is connected at [{2}].", Environment.NewLine, user, DateTime.Now.ToShortTimeString());
                }
            }
        }
        public void Disconnect(string user)
        {
            if (Logger != null)
            {
                if (Logger.InvokeRequired)
                {
                    Action<string> invoker = new Action<string>(Disconnect);
                    Logger.Invoke(invoker,user);
                }
                else
                {
                    Logger.Text += string.Format("{0}>{1} is Disconnected at [{2}].", Environment.NewLine, user, DateTime.Now.ToShortTimeString());
                }
            }
        }

        public void PostData(string user, byte[] data)
        {
            if (PostedData != null)
                PostedData(user, data);
        }

        public event Business_Layer.PostedDataHandler PostedData;

        /*
         * When a client sharing( upload) a file, server store it as temporary file for backup and reference.
         * 
         */
        public void Upload(string user,List<Business_Layer.UploadData> files)
        {
            //Check a share folder is available. If not, create a new one.
            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");
            //Load all files in uploaded/ shared file and write to local.
            foreach (Business_Layer.UploadData file in files)
            {
                System.IO.File.WriteAllBytes("Share\\File-IP-" + user + "-IP-" + file.Filename, file.File);
                AddLog(string.Format("> File: {0} has been uploaded at {1}. by {2}",file.Filename,DateTime.Now.ToShortTimeString(),user));
            }

            if (Update != null)
            {
                try
                {
                    Update(user);   //Trying update and provoke another user that they should Refresh the sharing file list.
                }
                catch (Exception e)
                {
                    //Do nothing due to maybe Peer has connection error.
                }
            }
        }


        public void removeFiles(string user)
        {
            AddLog("Removing reported files:....... ");
            //Check a share folder is available. If not, create a new one.
            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");

            DirectoryInfo serverFolder = new DirectoryInfo("Share");
            //Loop through existing file of server to remove a file that that IP.
            foreach (var file in serverFolder.GetFiles("*.*"))
            {
                AddLog("Found: " + file.Name +"        Match IP "+ user);
                if (file.Name.Contains(user))
                {
                    File.Delete("Share\\" + file.Name);
                    AddLog("File Removed: " + file.Name);
                }
            }

            if (Update != null)
            {
                try
                {
                    Update(user);
                }
                catch( Exception e)
                {
                    //Do nothing because some client/ peer/ slave can not connect due to temporary lost connection. This feature is not important as well =))
                }
            }
        }


        /**
         * This function will look at local share folders to get all available sharing files.
         * Everytime Client refresh, a new list will loop thorugh local files and send back to client about available sharing file.
         */
        public void GetFiles(out List<Business_Layer.FileInfo> files)
        {

            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");
            
            List<Business_Layer.FileInfo > list = new List<Business_Layer.FileInfo>();

            foreach (string file in System.IO.Directory.GetFiles("Share"))
            {
                list.Add(new Business_Layer.FileInfo(file, ((new System.IO.FileInfo(file).Length) / 1024)));
            }
            files = list;
        }


        public event Business_Layer.UpdateHandler Update;

    }
}
