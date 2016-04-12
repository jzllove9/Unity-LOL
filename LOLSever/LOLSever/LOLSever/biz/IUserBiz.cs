using LOLSever.dao.model;
using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.biz
{
    public interface IUserBiz
    {
        /// <summary>
        /// 创建召唤师
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool Create(UserToken token, string name);

       /// <summary>
       /// 获取连接对应的用户信息
       /// </summary>
       /// <param name="token"></param>
       /// <returns></returns>
        UserModel getModel(UserToken token);

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel getModel(int id);

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserModel OnLine(UserToken token);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="token"></param>
        void OffLine(UserToken token);

        /// <summary>
        /// 根据ID获取用户连接对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserToken getToken(int id);
    }
}
