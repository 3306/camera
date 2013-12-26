using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AsyTcpServer
{
    class AsyncTcpServer : IDisposable
    {
        #region Fields

        private TcpListener listener;
        private List<TcpClientState> clients;
        private bool disposed = false;

        #endregion

        #region Properties

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// 通信使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region Ctors
        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public AsyncTcpServer(int listenPort):this(IPAddress.Any,listenPort)
        { }
        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public AsyncTcpServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="localIPAddress"></param>
        /// <param name="listenport"></param>
        public AsyncTcpServer(IPAddress localIPAddress, int listenPort)
        {
            Address = localIPAddress;
            Port = listenPort;
            this.Encoding = Encoding.Default;

            clients = new List<TcpClientState>();

            listener = new TcpListener(Address, Port);
            listener.AllowNatTraversal(true);
        }
        #endregion

        #region Server

        #endregion
    }

    /// <summary>
  /// Internal class to join the TCP client and buffer together
  /// for easy management in the server
  /// </summary>
  internal class TcpClientState
  {
    /// <summary>
    /// Constructor for a new Client
    /// </summary>
    /// <param name="tcpClient">The TCP client</param>
    /// <param name="buffer">The byte array buffer</param>
    public TcpClientState(TcpClient tcpClient, byte[] buffer)
    {
      if (tcpClient == null)
        throw new ArgumentNullException("tcpClient");
      if (buffer == null)
        throw new ArgumentNullException("buffer");

      this.TcpClient = tcpClient;
      this.Buffer = buffer;
    }

    /// <summary>
    /// Gets the TCP Client
    /// </summary>
    public TcpClient TcpClient { get; private set; }

    /// <summary>
    /// Gets the Buffer.
    /// </summary>
    public byte[] Buffer { get; private set; }

    /// <summary>
    /// Gets the network stream
    /// </summary>
    public NetworkStream NetworkStream
    {
      get { return TcpClient.GetStream(); }
    }
  }
}
