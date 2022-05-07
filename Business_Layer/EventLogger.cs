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
using System.Diagnostics;

namespace Business_Layer
{
    public sealed class EventLogger
    {
        /**
         * The idea of this class is write to window event log.
         * Just simply write some piece information so It use sealed class.
         */
        public static void Logger(Exception ex, string part)
        {
            try
            {
                if (!EventLog.Exists("FTP File Sharing", "."))
                {
                    EventLog.CreateEventSource(new EventSourceCreationData("FTP File Sharing", "FTP File Sharing"));
                }

                EventLog.WriteEntry("FTP File Sharing", part + " : " + ex.Message, EventLogEntryType.Error);
            }
            catch { }
        }
    }
}
