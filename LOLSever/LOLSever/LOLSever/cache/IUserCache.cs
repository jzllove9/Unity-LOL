using LOLSever.dao.model;
using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.cache
{
    public interface IUserCache
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool create(UserToken token,string name);

        /// <summary>
        /// 根据连接对象判断是否拥有角色
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool has(UserToken token);

        /// <summary>
        /// 根据账号ID判断是否拥有角色
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool hasByAccountId(int id);

        /// <summary>
        /// 根据连接对象获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserModel getModel(UserToken token);

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel getModel(int id);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserModel onLine(UserToken token, int id);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="token"></param>
        void offLine(UserToken token);

        /// <summary>
        /// 根据账号ID获取连接对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserToken getToken(int id);

        /// <summary>
        /// 通过账号ID获取角色
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        UserModel getByAccountId(int accountId);

        /// <summary>
        /// 角色是否已经在线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool isOnline(int id);

    }
}
