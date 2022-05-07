/**
 * User: crossdeck
 * Published: 3 Jul 2016
 * Title: file-transfer-p2p
 * Link: https://github.com/crossdeck/file-transfer-p2p
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FileSharing_FTP_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());
        }
    }
}
