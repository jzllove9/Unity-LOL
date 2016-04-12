using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetIO
{
    private static NetIO _instance;
    private Socket socket;
    private string ip = "127.0.0.1";
    private int port = 6666;
    private byte[] readbuff = new byte[1024];
    private List<byte> cache = new List<byte>();
    public List<SocketModel> messages = new List<SocketModel>();
    private bool isReading = false;

    public static NetIO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetIO();
            }
            return _instance;
        }
    }

    //私有构造，外部无法创建实例化
    private NetIO()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);
            //开启异步消息接收 消息到达后会直接写入缓冲区
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, null);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void ReceiveCallBack(IAsyncResult iar)
    {
        try
        {
            //获取消息长度，结束消息接收
            int length = socket.EndReceive(iar);
            byte[] message = new byte[length];
            Buffer.BlockCopy(readbuff, 0, message, 0, length);
            cache.AddRange(message);
            if (!isReading)
            {
                isReading = true;
                onData();
            }
            //开启异步消息接收 消息到达后会直接写入缓冲区
            //尾递归 无限开启与结束 形成socket通信循环
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, null);
        }
        catch (Exception e)
        {
            Debug.Log("远程服务器主动断开连接: " + e.Message);
            socket.Close();
        }
    }

    public void write(byte type, int area, int command, object message)
    {
        ByteArray ba = new ByteArray();
        ba.write(type);
        ba.write(area);
        ba.write(command);
        if (message != null)
        {
            ba.write(SerializeUtil.encode(message));
        }

        ByteArray arrL = new ByteArray();
        arrL.write(ba.Length);
        arrL.write(ba.getBuff());
        try
        {
            socket.Send(arrL.getBuff());
        }
        catch (Exception e) { Debug.Log("网络错误，请重新登录： " + e.Message); }
    }

    //缓存中有数据处理
    private void onData()
    {
        byte[] result = decode(ref cache);
        if (result == null)
        {
            isReading = false;
            return;
        }
        SocketModel message = mdecode(result);
        if (message == null)
        {
            isReading = false;
            return;
        }
        //消息处理
        messages.Add(message);
        //尾递归，防止在消息处理过程中有其他消息到达而没有经过处理
        onData();
    }

    public static byte[] decode(ref List<byte> cache)
    {
        if (cache.Count < 4) return null;
        MemoryStream ms = new MemoryStream(cache.ToArray());
        BinaryReader br = new BinaryReader(ms);
        int length = br.ReadInt32();
        //如果消息长度大于缓存中的数据长度，说明消息没有从缓存中读取完毕，等待下次消息到达后再进行处理
        if (length > ms.Length - ms.Position)
        {
            return null;
        }
        byte[] result = br.ReadBytes(length);
        cache.Clear();
        cache.AddRange(br.ReadBytes((int)(ms.Length - ms.Position)));
        br.Close();
        ms.Close();
        return result;
    }

    public static SocketModel mdecode(byte[] value)
    {
        ByteArray ba = new ByteArray(value);
        SocketModel model = new SocketModel();
        byte type;
        int area;
        int command;
        ba.read(out type);
        ba.read(out area);
        ba.read(out command);
        model.type = type;
        model.area = area;
        model.command = command;
        //读取协议后 判断是否还有数据需要读取 有则说明有消息体 进行消息读取
        if (ba.Readnable)
        {
            byte[] message;
            ba.read(out message, ba.Length - ba.Position);
            model.message = SerializeUtil.decode(message);
        }
        ba.Close();
        return model;
    }

}

