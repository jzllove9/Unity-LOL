using LOLSever.cache;
using LOLSever.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.biz.impl
{
    /// <summary>
    /// 用户事物处理
    /// </summary>
    public class UserBiz : IUserBiz
    {
        IAccountBiz accountBiz = BizFactory.accountBiz;
        IUserCache userCache = CacheFactory.userCache;
        public bool Create(NetFrame.UserToken token, string name)
        {
            //账号是否登录，获取账号ID
            int accountID = accountBiz.get(token);
            if (accountID == -1) return false;
            //判断当前账号是否已经拥有角色
            if (userCache.hasByAccountId(accountID)) return false;

            return userCache.create(token, name);
        }

        public dao.model.UserModel getModel(NetFrame.UserToken token)
        {
            return userCache.getModel(token);
        }

        public dao.model.UserModel getModel(int id)
        {
            return userCache.getModel(id);
        }

        public UserModel OnLine(NetFrame.UserToken token)
        {
            //账号是否登录，获取账号ID
            int accountID = accountBiz.get(token);
            if (accountID == -1) return null;
            UserModel userModel = userCache.getByAccountId(accountID);
            if (userCache.isOnline(userModel.id)) return null;
            return userCache.onLine(token, accountID);
        }

        public void OffLine(NetFrame.UserToken token)
        {
            userCache.offLine(token);
        }

        public NetFrame.UserToken getToken(int id)
        {
            return userCache.getToken(id);
        }

    }
}
