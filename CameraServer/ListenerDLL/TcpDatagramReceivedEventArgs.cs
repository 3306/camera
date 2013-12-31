using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;


namespace ListenerDLL
{
    /// <summary>
    /// 接收到数据报文事件参数
    /// </summary>
    /// <typeparam name="T">报文类型</typeparam>
    public class TcpDatagramReceivedEventArgs<T> : EventArgs
    {
        public TcpDatagramReceivedEventArgs(TcpClient tcpClient, T datagram)
        {
            TcpClient = tcpClient;
            Datagram = datagram;

        }
        public TcpClient TcpClient { get; private set; }
        public T Datagram { get; private set; }

    }
}
