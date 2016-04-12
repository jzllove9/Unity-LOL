using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol
{
    /// <summary>
    /// 服务协议
    /// </summary>
    public class LoginProtocol
    {
        public const int LOGIN_CREQ = 0;//客户端申请登录
        public const int LOGIN_SRES = 1;//服务器返回给客户端登陆结果

        public const int REG_CREQ = 2;//客户端申请注册
        public const int REG_SRES = 3;//服务器返回给客户端注册结果
    }
}
