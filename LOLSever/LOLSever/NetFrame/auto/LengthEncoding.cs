using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetFrame.auto
{
    public class LengthEncoding
    {
        /// <summary>
        /// 粘包长度编码
        /// </summary>
        /// <param name="buff">消息体</param>
        /// <returns></returns>
        public static byte[] encode(byte[] buff)
        {
            //创建内存流对象
            MemoryStream ms = new MemoryStream();
            //写入二进制对象流
            BinaryWriter bw = new BinaryWriter(ms);
            //写入消息长度
            bw.Write(buff.Length);
            //写入消息本身
            bw.Write(buff);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            bw.Close();
            ms.Close();
            return result;
        }

        /// <summary>
        /// 粘包长度解码
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
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
    }
}
