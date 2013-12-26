using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace AsyTcpServer
{
    public class TcpClientDisconnectEventArgs:EventArgs
    {
        /// <summary>
        /// 与客户端的连接已断开事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        public TcpClientDisconnectEventArgs(TcpClient tcpClient)
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException("tcpClient");
            }
            this.TcpClient = tcpClient;
        }
        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get; private set; }
    }
}
