using NetFrame;
using NetFrame.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.Logic
{
    public abstract class AbsOnceHandler
    {
        private byte type;
        private int area;

        public void SetArea(int area)
        {
            this.area = area;
        }
        public virtual int GetArea()
        {
            return area;
        }

        public void SetType(byte type)
        {
            this.type = type;
        }

        public virtual byte GetType()
        {
            return type;
        }

        #region 通过连接对象发送
        public void write(UserToken token, int command)
        {
            write(token, command, null);
        }
        public void write(UserToken token, int command, object message)
        {
            write(token, GetArea(), command, message);
        }
        public void write(UserToken token, int area, int command, object message)
        {
            write(token, GetType(), area, command, message);
        }
        public void write(UserToken token, byte type, int area, int command, object message)
        {
            byte[] value = MessageEncoding.encode(CreateSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            token.write(value);
        }
        #endregion

        #region 通过用户ID发送
        public void write(int id, int command)
        {

        }

        public void write(int id, int command, object message)
        {

        }

        public void write(int id, int area, int command, object message)
        {

        }
        public void write(int id, byte type, int area, int command, object message)
        {

        }
        #endregion

        public SocketModel CreateSocketModel(byte type, int area, int command, object message)
        {
            return new SocketModel(type, area, command, message);
        }
    }
}
