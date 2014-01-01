using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace AsyTcpServer
{
    public class TcpImageReceivedEventArgs<T>:EventArgs
    {
        public TcpImageReceivedEventArgs(TcpClient tcpClient, T uid)
        {
            TcpClient = tcpClient;
            this.uid = uid;
            
        }
        public TcpClient TcpClient { get; private set; }
        public T uid { get; private set; }
    }
}
