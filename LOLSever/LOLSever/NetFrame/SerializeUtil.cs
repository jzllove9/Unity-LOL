using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetFrame
{
    public class SerializeUtil
    {
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] encode(object value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bw = new BinaryFormatter();//二进制流序列化对象
            //将obj对象序列化成二进制数据 写入到内存流中
            bw.Serialize(ms, value);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            ms.Close();
            return result;
        }

        /// <summary>
        /// 对象反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object decode(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            BinaryFormatter bw = new BinaryFormatter();
            object result = bw.Deserialize(ms);
            ms.Close();
            return result;
        }
    }
}
