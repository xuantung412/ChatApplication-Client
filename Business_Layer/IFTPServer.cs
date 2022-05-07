﻿/**
 * User: crossdeck
 * Published: 3 Jul 2016
 * Title: file-transfer-p2p
 * Link: https://github.com/crossdeck/file-transfer-p2p
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Business_Layer
{
    public delegate void PostedDataHandler(string user, byte[] data);
    public delegate void UpdateHandler(string user);

    [Serializable()]
    public struct UploadData
    {
        public string Filename;
        public byte[] File;
    }

    /*
     * Interface class created. This interface will be used in Server Application. Some declared function will be implemented later.
     */
    public interface IFTPServer
    {
        void Upload(string user, List<UploadData> files);
        void removeFiles(string user);
        void GetFiles(out List<FileInfo> files);
        void Connect(string user);
        void Disconnect(string user);
        void PostData(string user, byte[] data);
        event PostedDataHandler PostedData;
        event UpdateHandler Update;

    }

    public abstract class PostedData : System.MarshalByRefObject
    {
        public void Server_PostData(string user, byte[] data)
        {
            this.ImplementedPostData(user, data);
        }

        public void Server_Update(string user)
        {
            this.ImplementedUpdate(user);
        }

        public abstract void ImplementedPostData(string user, byte[] data);

        public abstract void ImplementedUpdate(string user);
    }
}
