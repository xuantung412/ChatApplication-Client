/**
 * User: crossdeck
 * Published: 3 Jul 2016
 * Title: file-transfer-p2p
 * Link: https://github.com/crossdeck/file-transfer-p2p
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Management;
using System.Management.Instrumentation;

/**
 * This will act as a slave.
 * Every peer peform an action such as Refresh( to see available files), share file, the slave will called and perform that action.
 * 
 */
namespace Business_Layer
{
    public class MachineInfo
    {

        public static string MachineName = Environment.MachineName; //Get name of computer that run this code.
        public static IPEndPoint MachineIP; //Host and local port information to connect server. IP
        public static List<FileInfo> Files = new List<FileInfo>();  //Available file to download. This list will capture all files in tempory storage of server to display.
        /**
         *This function will get all files in folder to a list to display.
         */
        public static void GetFiles(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            foreach (string file in Directory.GetFiles(folder))
            {
                FileInfo info = new FileInfo(Path.GetFileName(file), (new System.IO.FileInfo(file).Length / 1024));
                Files.Add(info);
            }
        }


        /**
         * It gets the current machine IP address
         */

        public static IPEndPoint GetMachineIP()
        {
            IPEndPoint result = null;

             ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select IPAddress From Win32_NetworkAdapterConfiguration");

             foreach (ManagementObject obj in searcher.Get())
             {
                 if (obj["IPAddress"] != null)  //Find an IP address and stop the loop.Return that found IP
                 {
                     string[] ip = (string[])obj["IPAddress"];
                     result = new IPEndPoint(IPAddress.Parse(ip[0]), 9898);
                     break;
                 }
             }
        
            return result;
        }

        /**
         * Get only IP address and return
         * 
         */
        public static string GetJustIP()
        {
            string result = null;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select IPAddress From Win32_NetworkAdapterConfiguration");

            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj["IPAddress"] != null)
                {
                    string[] ip = (string[])obj["IPAddress"];
                    result = ip[0];
                    break;
                }
            }

            return result;
        }

    }
}
