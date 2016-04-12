using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetFrame.auto
{
    public class MessageEncoding
    {
        /// <summary>
        /// 消息体序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] encode(object value)
        {
            SocketModel model = value as SocketModel;
            ByteArray ba = new ByteArray();
            //从数据中读取三层协议 读取顺序必须和写入顺序相同
            ba.write(model.type);
            ba.write(model.area);
            ba.write(model.command);
            if (model.message != null)
            {
                ba.write(SerializeUtil.encode(model.message));
            }
            byte[] result = ba.getBuff();
            ba.Close();
            return result;
        }

        /// <summary>
        /// 消息体反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object decode(byte[] value)
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
}
