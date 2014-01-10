using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace AsyTcpServer
{
    /// <summary>
    /// 与客户端的连接已经建立事件参数
    /// </summary>
    public class TcpClientConnectedEventArgs : EventArgs
    {
        public TcpClientConnectedEventArgs(TcpClient tcpClient)
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException("tcpClient");
            }
            this.TcpClient = tcpClient;
            
        }
        public TcpClient TcpClient { get; private set; }
    }
}
