using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
namespace AsyTcpServer
{
    public class AsyncTcpServer : IDisposable
    {
        #region Fields
      
        private TcpListener listener;
        //唯一一个写图片流的客户状态
        public ConcurrentDictionary<string,TcpClientImageState> clients;
       //唯一一个发送图片的ip
        public string IP_send_Image;
        public string NameOfImageFromClient;
        public ConcurrentDictionary<string, TcpClientDefaultState> clients1;

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

        public int ClientCount { get; private set; }
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
            clients1 = new ConcurrentDictionary<string, TcpClientDefaultState>();
            clients = new ConcurrentDictionary<string, TcpClientImageState>();

            listener = new TcpListener(Address, Port);
            listener.AllowNatTraversal(true);
        }
        #endregion

        #region Server
        public AsyncTcpServer Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                ClientCount = 0;
                listener.Start();
                listener.BeginAcceptTcpClient(
                    new AsyncCallback(HandleTcpClientAccepted),listener);
            }
            return this;
        }
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="backlog"></param>
        /// <returns>异步TCP服务器</returns>
         public AsyncTcpServer Start(int backlog)
         {
             if(!IsRunning)
             {
                 IsRunning = true; 
                 ClientCount = 0;
                 listener.Start(backlog);
                 listener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted),listener);
             }
             return this;
         }
        public AsyncTcpServer Stop()
        {
            if (!IsRunning) return this;

            try
            {
                listener.Stop();

                foreach (var client in clients.Values)
                {
                    client.TcpClient.Client.Disconnect(false);
                }
                clients.Clear();
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            IsRunning = false;

            return this;
        }

        #endregion

        #region Receive
        /// <summary>
        /// 异步处理接受TCPclient
        /// </summary>
        /// <param name="ar"></param>
        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            DateTime timeID = DateTime.Now;
            if (!IsRunning) return;
            TcpListener tcpListener = (TcpListener)ar.AsyncState;
            TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);
            if (!tcpClient.Connected) return;
            //创建文件流
            //处理发来的分析后的图片中包含的人头数量
            string ip = tcpClient.Client.RemoteEndPoint.ToString().Substring(0,tcpClient.Client.RemoteEndPoint.ToString().LastIndexOf(":"));
            //有待观察
            Boolean Image_or_Num = ip.Equals(IP_send_Image);
            if (!Image_or_Num)
            {
                byte[] buffer = new byte[8];
                TcpClientDefaultState internalClient = new TcpClientDefaultState(tcpClient, buffer);
                //add client connection to cache
                string tcpClientKey = internalClient.TcpClient.Client.RemoteEndPoint.ToString();
                tcpClientKey = tcpClientKey.Substring(0, tcpClientKey.LastIndexOf(":"));
            
                clients1.AddOrUpdate(tcpClientKey, internalClient, (n, o) => { return internalClient; });
                RaiseClientConnected(tcpClient);

                //begin to read data
                NetworkStream networkStream = internalClient.NetworkStream;

                ContinueReadBufferDefault(internalClient, networkStream);


                //keep listening to accept next connection
                ContinueAcceptTcpClient(tcpListener);
            
            }
           //处理发来的图片字节
            else {
        //    DateTime timeID = DateTime .Now;
                Guid uid = Guid.NewGuid();
            string id = timeID.ToString().Replace("/","-").Replace(":","-");
            string FilePath = System.Environment.CurrentDirectory + "\\pic\\";
            string ChildDir = tcpClient.Client.RemoteEndPoint.ToString();
            ChildDir = ChildDir.Substring(0, ChildDir.LastIndexOf(":"));
            FilePath = FilePath +ChildDir+ Path.DirectorySeparatorChar;
            NameOfImageFromClient = id;
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            FileStream fs = new FileStream(FilePath +uid + ".jpg", FileMode.Create);
           
            byte[] buffer = new byte[8];
            TcpClientImageState internalClient = new TcpClientImageState(tcpClient, buffer,fs,timeID);

            //add client connection to cache
            string tcpClientKey = internalClient.TcpClient.Client.RemoteEndPoint.ToString();
            tcpClientKey = tcpClientKey.Substring(0,tcpClientKey.LastIndexOf(":"));
            clients.AddOrUpdate(tcpClientKey, internalClient, (n, o) => { return internalClient; });
            RaiseClientConnected(tcpClient);

            //begin to read data
            NetworkStream networkStream = internalClient.NetworkStream;
            
            ContinueReadBuffer(internalClient, networkStream);


            //keep listening to accept next connection
            ContinueAcceptTcpClient(tcpListener);
            }
            
        }
        private void ContinueAcceptTcpClient(TcpListener tcpListener)
        {
            try
            {
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted), tcpListener);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //处理传来的图片字节流
        private void ContinueReadBuffer(TcpClientImageState internalClient, NetworkStream networkStream)
        {
            try
            {
                   Stopwatch sw = new Stopwatch();
                   sw.Start();
                   networkStream.BeginRead(internalClient.Buffer, 0, internalClient.Buffer.Length, HandleDatagramReceived, internalClient);
                    sw.Stop();
                    Console.WriteLine("总运行时间：" + sw.Elapsed);
                    Console.WriteLine("测量实例得出的总运行时间（毫秒为单位）：" + sw.ElapsedMilliseconds);
                    Console.WriteLine("总运行时间(计时器刻度标识)：" + sw.ElapsedTicks);
                    Console.WriteLine("计时器是否运行：" + sw.IsRunning.ToString());
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }            
        }
        //处理发来的分析后的结果
        private void ContinueReadBufferDefault(TcpClientDefaultState internalClient, NetworkStream networkStream)
        {   
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                networkStream.BeginRead(internalClient.Buffer, 0, internalClient.Buffer.Length, HandleDatagramReceivedDefault, internalClient);
                sw.Stop();
                Console.WriteLine("总运行时间：" + sw.Elapsed);
                Console.WriteLine("测量实例得出的总运行时间（毫秒为单位）：" + sw.ElapsedMilliseconds);
                Console.WriteLine("总运行时间(计时器刻度标识)：" + sw.ElapsedTicks);
                Console.WriteLine("计时器是否运行：" + sw.IsRunning.ToString());

            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private void HandleDatagramReceived(IAsyncResult ar)
        {
           
            if (!IsRunning) return;
                
                
                TcpClientImageState internalClient = (TcpClientImageState)ar.AsyncState;
                NetworkStream networStream = internalClient.NetworkStream;                                          
                int numberOfReadBytes = 0;
                try
                {
                    numberOfReadBytes = networStream.EndRead(ar);
                }
                catch(Exception ex)
                {
                   
                    numberOfReadBytes = 0;
                    Console.WriteLine( ex.ToString());
                }
                if (numberOfReadBytes == 0)
                {
                    //connection has been closed
                   //TcpClientImageState internalClientToBeThrowAway;
                   //string tcpClientKey = internalClient.TcpClient.Client.RemoteEndPoint.ToString();
                   //clients.TryRemove(tcpClientKey, out internalClientToBeThrowAway);
                   //if (!internalClient.TcpClient.Connected)
                   //{
                   //    RaiseClientDisconnected(internalClient.TcpClient);
                   //}
                   internalClient.FileStream.Dispose();
               
                   return ;   
                }
                //received byte and trigger event notification
                byte[] receivedBytes = new byte[numberOfReadBytes];
                Buffer.BlockCopy(internalClient.Buffer,0,receivedBytes,0,numberOfReadBytes);
                internalClient.FileStream.Write(internalClient.Buffer,0,internalClient.Buffer.Length);
                RaiseDatagramReceived(internalClient.TcpClient, receivedBytes);
                RaisePlaintextReceived(internalClient.TcpClient, receivedBytes);

                // continue listening for tcp datagram packets
                networStream.BeginRead(internalClient.Buffer,0,internalClient.Buffer.Length,HandleDatagramReceived,internalClient);
              
            
        }
        //处理分析后发送的人头数量
        private void HandleDatagramReceivedDefault(IAsyncResult ar)
        {
            if (!IsRunning) return;


            TcpClientDefaultState internalClient = (TcpClientDefaultState)ar.AsyncState;
            NetworkStream networStream = internalClient.NetworkStream;
            int numberOfReadBytes = 0;
            try
            {
                numberOfReadBytes = networStream.EndRead(ar);
            }
            catch (Exception ex)
            {

                numberOfReadBytes = 0;
                Console.WriteLine(ex.ToString());
            }
            if (numberOfReadBytes == 0)
            {
                //connection has been closed
                //TcpClientImageState internalClientToBeThrowAway;
                //string tcpClientKey = internalClient.TcpClient.Client.RemoteEndPoint.ToString();
                //clients.TryRemove(tcpClientKey, out internalClientToBeThrowAway);
           //     if (!internalClient.TcpClient.Connected)
            //   {
             //      RaiseClientDisconnected(internalClient.TcpClient);
             //  }
              //  internalClient.NetworkStream.Dispose();
                return;
            }
            //received byte and trigger event notification
            byte[] receivedBytes = new byte[numberOfReadBytes];
            Buffer.BlockCopy(internalClient.Buffer, 0, receivedBytes, 0, numberOfReadBytes);
         
        //    internalClient.FileStream.Write(internalClient.Buffer, 0, internalClient.Buffer.Length);

            RaiseDatagramReceived(internalClient.TcpClient, receivedBytes);
           // RaisePlaintextReceived(internalClient.TcpClient, receivedBytes);

            // continue listening for tcp datagram packets
            networStream.BeginRead(internalClient.Buffer, 0, internalClient.Buffer.Length, HandleDatagramReceivedDefault, internalClient);

        }  

        #endregion 

        #region Event
        //接收到数据报文事件
        public event EventHandler<TcpDatagramReceivedEventArgs<byte[]>> DatagramReceived; 
        //接收到数据报文明文事件
        public event EventHandler<TcpDatagramReceivedEventArgs<string>> PlaintextReceived;

        private void RaiseDatagramReceived(TcpClient sender, byte[] datagram)
        {
            if (DatagramReceived != null)
            {
                DatagramReceived(this, new TcpDatagramReceivedEventArgs<byte[]>(sender, datagram));
            }
        }

        private void RaisePlaintextReceived(TcpClient sender, byte[] datagram)
        {
            if (PlaintextReceived != null)
            {
                PlaintextReceived(this, new TcpDatagramReceivedEventArgs<string>(sender, this.Encoding.GetString(datagram, 0, datagram.Length)));
            }
        }
        //与客户端的连接已建立事件
        public event EventHandler<TcpClientConnectedEventArgs> ClientConnected;
        //与客户端的连接已断开事件
        public event EventHandler<TcpClientDisconnectEventArgs> ClientDisconnected;

        private void RaiseClientConnected(TcpClient tcpClient)
        {
            if (ClientConnected != null)
            {
                ++ClientCount;
                ClientConnected(this, new TcpClientConnectedEventArgs(tcpClient));
                
            }
        }

        private void RaiseClientDisconnected(TcpClient tcpClient)
        {
            if (ClientDisconnected != null)
            {
                --ClientCount;
                ClientDisconnected(this, new TcpClientDisconnectEventArgs(tcpClient));
                
            }
        }
        #endregion

        #region Send
        private void GuardRunning()
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP server has not been started yet.");
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public void Send(TcpClient tcpClient, byte[] datagram)
        {
            GuardRunning();

            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient");

            if (datagram == null)
                throw new ArgumentNullException("datagram");

            try
            {
                NetworkStream stream = tcpClient.GetStream();
                if (stream.CanWrite)
                {
                    stream.BeginWrite(datagram, 0, datagram.Length, HandleDatagramWritten, tcpClient);
                }
            }
            catch (ObjectDisposedException ex)
            {
                //ExceptionHandler.Handle(ex);
            }
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public void Send(TcpClient tcpClient, string datagram)
        {
            Send(tcpClient, this.Encoding.GetBytes(datagram));
        }

        /// <summary>
        /// 发送报文至所有客户端
        /// </summary>
        /// <param name="datagram">报文</param>
        public void SendToAll(byte[] datagram)
        {
            GuardRunning();

            foreach (var client in clients.Values)
            {
                Send(client.TcpClient, datagram);
            }
        }

        /// <summary>
        /// 发送报文至所有客户端
        /// </summary>
        /// <param name="datagram">报文</param>
        public void SendToAll(string datagram)
        {
            GuardRunning();

            SendToAll(this.Encoding.GetBytes(datagram));
        }

        private void HandleDatagramWritten(IAsyncResult ar)
        {
            try
            {
                ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (IOException ex)
            {
            Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public void SyncSend(TcpClient tcpClient, byte[] datagram)
        {
            GuardRunning();

            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient");

            if (datagram == null)
                throw new ArgumentNullException("datagram");

            try
            {
                NetworkStream stream = tcpClient.GetStream();
                if (stream.CanWrite)
                {
                    stream.Write(datagram, 0, datagram.Length);
                }
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public void SyncSend(TcpClient tcpClient, string datagram)
        {
            SyncSend(tcpClient, this.Encoding.GetBytes(datagram));
        }

        /// <summary>
        /// 发送报文至所有客户端
        /// </summary>
        /// <param name="datagram">报文</param>
        public void SyncSendToAll(byte[] datagram)
        {
            GuardRunning();

            foreach (var client in clients.Values)
            {
                SyncSend(client.TcpClient, datagram);
            }
        }

        /// <summary>
        /// 发送报文至所有客户端
        /// </summary>
        /// <param name="datagram">报文</param>
        public void SyncSendToAll(string datagram)
        {
            GuardRunning();

            SyncSendToAll(this.Encoding.GetBytes(datagram));
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();

                        if (listener != null)
                        {
                            listener = null;
                        }
                    }
                    catch (SocketException ex)
                    {
                       // ExceptionHandler.Handle(ex);
                    }
                }

                disposed = true;
            }
        }

        #endregion

        #region test
        public void printclient()
        {
            foreach(var client in clients.Keys)
            {
                Console.WriteLine(client.ToString());
            }
        }
        #endregion

        #region MonitorClientConnected
        public void BeginMonitorClientConnected()
        {
            Thread t = new Thread(new ThreadStart(MonitorClientConnected));
            t.Start();
        }
        private void MonitorClientConnected()
        {
            while (true)
            {
                DateTime SysTime = DateTime.Now;
                foreach (var client in clients.Values)
                {
                    System.TimeSpan ts = SysTime.Subtract(client.UpdateTime);
                    Console.WriteLine(ts.ToString());
                    Console.WriteLine(ts.TotalSeconds.CompareTo(30));
                    if (ts.TotalSeconds.CompareTo(30)>0)
                    {
                        //connection has been closed
                        TcpClientImageState internalClientToBeThrowAway;
                        string tcpClientKey = client.TcpClient.Client.RemoteEndPoint.ToString();
                        tcpClientKey = tcpClientKey.Substring(0, tcpClientKey.LastIndexOf(":"));
                        clients.TryRemove(tcpClientKey, out internalClientToBeThrowAway);
                        RaiseClientDisconnected(client.TcpClient);
                        
                    }
                    

                }
                Thread.Sleep(1000);
            }
            
        }
        #endregion
    }
    //处理收到的分析图片人数结果
  public class TcpClientDefaultState
    {
        /// <summary>
        /// Constructor for a new Client
        /// </summary>
        /// <param name="tcpClient">The TCP client</param>
        /// <param name="buffer">The byte array buffer</param>
        public TcpClientDefaultState(TcpClient tcpClient, byte[] buffer)
        {
            if (tcpClient == null) throw new ArgumentNullException("tcpClient");
            if (buffer == null) throw new ArgumentNullException("buffer");

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
            get
            {
                return TcpClient.GetStream();
            }
        }
    }


    /// <summary>
  /// Internal class to join the TCP client and buffer together
  /// for easy management in the server
  /// </summary>
  /// 

   public class TcpClientImageState
  {
    /// <summary>
    /// Constructor for a new Client
    /// </summary>
    /// <param name="tcpClient">The TCP client</param>
    /// <param name="buffer">The byte array buffer</param>
    public TcpClientImageState(TcpClient tcpClient, byte[] buffer,FileStream fs,DateTime updatetime)
    {
      if (tcpClient == null)
        throw new ArgumentNullException("tcpClient");
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (fs == null)
          throw new ArgumentNullException("fs");
      if (updatetime == null)
          throw new ArgumentNullException("updatetime");
      this.TcpClient = tcpClient;
      this.Buffer = buffer;
      this.FileStream = fs;
      this.UpdateTime = updatetime;

        
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
    public FileStream FileStream{get;private set;}
    public DateTime UpdateTime { get; private set;}

  }
}
