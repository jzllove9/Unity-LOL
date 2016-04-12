using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetFrame
{
    public class SeverStart
    {
        Socket server;//服务器socket监听对象
        int maxClient;//最大接入数量
        Semaphore acceptClients;
        UserTokenPool pool;

        public LengthEncode LE;
        public LengthDecode LD;
        public encode encode;
        public decode decode;

        /// <summary>
        /// 消息处理中心，由外部应用传入
        /// </summary>
        public AbsHandlerCenter center;

        /// <summary>
        /// 初始化通信监听
        /// </summary>
        /// <param name="port">监听端口</param>
        public SeverStart(int max)
        {
            //实例化监听对象
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //设定服务器最大连接数
            this.maxClient = max;
        }

        public void Start(int port)
        {
            //创建连接池
            pool = new UserTokenPool(maxClient);
            //连接信号量
            acceptClients = new Semaphore(maxClient, maxClient);
            for (int i = 0; i < maxClient; i++)
            {
                UserToken token = new UserToken();
                //初始化token信息
                token.receiveSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.sendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.LD = LD;
                token.LE = LE;
                token.decode = decode;
                token.encode = encode;
                token.sendProcess = ProcessSend;
                token.closeProcess = ClientClose;
                token.center = center;
                pool.push(token);
            }

            try
            {
                //监听当前服务器网卡所有可用IP地址的port端口
                //外网IP 内网IP 192.168.x.x 本机IP127.0.0.1
                server.Bind(new IPEndPoint(IPAddress.Any, port));
                //将socket置于监听状态
                server.Listen(10);
                StartAccept(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// 开始客户端连接监听
        /// </summary>
        public void StartAccept(SocketAsyncEventArgs e)
        {
            //如果当前传入为空，说明调用新的客户端连接事件，否则移除当前客户端连接
            if (e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            }
            else
            {
                e.AcceptSocket = null;
            }
            //当前信号量-1
            acceptClients.WaitOne();
            bool result = server.AcceptAsync(e);
            //判断异步事件是否挂起，否说明立刻执行完成，直接处理事件，否则会在处理完成后触发Accept_Completed事件
            if (!result)
            {
                ProcessAccept(e);
            }
        }

        public void ProcessAccept(SocketAsyncEventArgs e)
        {
            //取出一个token连接对象 供新用户使用
            UserToken token = pool.pop();
            token.conn = e.AcceptSocket;
            //TODO 通知应用层 有客户端连接
            center.ClientConnect(token);
            //开启消息到达监听
            StartReceive(token);
            //释放当前异步对象
            StartAccept(e);
        }

        public void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        public void StartReceive(UserToken token)
        {
            try
            {
                //用户连接对象 开始异步接收
                bool result = token.conn.ReceiveAsync(token.receiveSAEA);
                //用户事件是否挂起
                if (!result)
                {
                    ProcessReceive(token.receiveSAEA);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceive(e);
            }
            else
            {
                ProcessSend(e);
            }
            //ProcessAccept(e);
        }

        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            //获取连接对象
            UserToken token = e.UserToken as UserToken;
            //判断网络消息接收是否成功
            if (token.receiveSAEA.BytesTransferred > 0 && token.receiveSAEA.SocketError == SocketError.Success)
            {
                byte[] message = new byte[token.receiveSAEA.BytesTransferred];
                Buffer.BlockCopy(token.receiveSAEA.Buffer, 0, message, 0, token.receiveSAEA.BytesTransferred);
                //处理接收到的消息
                token.receive(message);
                StartReceive(token);
            }
            else
            {
                if (token.receiveSAEA.SocketError != SocketError.Success)
                {
                    ClientClose(token, token.receiveSAEA.SocketError.ToString());
                }
                else
                {
                    //客户端向服务器发送空字节数据表明客户端异常断开
                    ClientClose(token, "客户端主动断开连接");
                }
            }
        }

        public void ProcessSend(SocketAsyncEventArgs e)
        {
            //获取连接对象
            UserToken token = e.UserToken as UserToken;
            if (e.SocketError != SocketError.Success)
            {
                ClientClose(token, e.SocketError.ToString());
            }
            else
            {
                //消息发送成功，回调成功
                token.writed();
            }
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="token">断开连接的用户对象</param>
        /// <param name="error">断开连接的错误编码</param>
        public void ClientClose(UserToken token, string error)
        {
            if (token.conn != null)
            {
                lock (token)
                {
                    //通知应用层面，客户端断开连接
                    center.ClientClose(token, error);
                    token.Close();
                    pool.push(token);
                    //信号量+1，供其他用户使用
                    acceptClients.Release();
                }
            }
        }
    }
}
