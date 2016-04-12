using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetFrame
{
    public abstract class AbsHandlerCenter
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="token">连接的客户端对象</param>
        public abstract void ClientConnect(UserToken token);
        /// <summary>
        /// 收到客户端消息
        /// </summary>
        /// <param name="token">发送消息的客户端对象</param>
        /// <param name="message">消息体</param>
        public abstract void MessageReceive(UserToken token, object message);
        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="token">断开连接的客户端对象</param>
        /// <param name="error">客户端断开的错误信息</param>
        public abstract void ClientClose(UserToken token, string error);
    }
}
