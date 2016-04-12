using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.biz
{

    public interface IAccountBiz
    {
        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="token">用户连接对象</param>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>返回登录结果 0.成功、 -4.输入不合法、-3.密码错误、-2.账号在线、-1 账号不存在 </returns>
        int login(UserToken token, string account, string password);

        /// <summary>
        /// 账号创建
        /// </summary>
        /// <param name="token">用户连接对象</param>
        /// <param name="account">注册的账号</param>
        /// <param name="password">注册的密码</param>
        /// <returns> 返回注册结果 0.成功、1.账号重复 、 2.账号不合法、3.密码不合法</returns>
        int create(UserToken token, string account, string password);

        /// <summary>
        /// 客户端断开连接(下线)
        /// </summary>
        /// <param name="token"></param>
        void close(UserToken token);

        /// <summary>
        /// 获取账号ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns>返回用户的登录账号ID</returns>
        int get(UserToken token);
    }
}
