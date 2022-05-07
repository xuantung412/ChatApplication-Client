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

namespace Business_Layer
{   

    /**
     * The system will store 2 information of the files: name and size. These is stored as Seriablizable so that It can be stored under bytes.
     * There are 2 basics variable is filename and size.
     */
    [Serializable()]
    public class FileInfo
    {
        /**
         * Constructor
         */
       
        public FileInfo(string filename, long size)
        {
            this.Filename = filename;
            this.Size = size;
        }

        public string Filename = string.Empty;  //Set default value to empty.

        public long Size = long.MinValue;   //Set default value
    }
}
