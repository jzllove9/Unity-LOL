using LOLSever.dao.model;
using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.cache.impl
{
    public class AccountCache : IAccountCache
    {
        public int index = 0;
        /// <summary>
        /// 玩家连接对象与账号的映射绑定
        /// </summary>
        Dictionary<UserToken, string> onlineAccMap = new Dictionary<UserToken, string>();
        /// <summary>
        /// 玩家账号与玩家具体属性的映射绑定
        /// </summary>
        Dictionary<string, AccountModel> accMap = new Dictionary<string, AccountModel>();

        public bool hasAccount(string account)
        {
            return accMap.ContainsKey(account);
        }

        public bool match(string account, string password)
        {
            //判断账号是否存在
            if (!hasAccount(account)) return false;
            //判断密码是否正确
            return accMap[account].password.Equals(password);
        }

        public bool isOnline(string account)
        {
            //判断当前在线字典中是否存在此账号
            return onlineAccMap.ContainsValue(account);
        }

        public int getID(NetFrame.UserToken token)
        {
            //判断是否存在连接对象记录，防止非法访问
            if (!onlineAccMap.ContainsKey(token)) return -1;
            //返回绑定账号的ID
            return accMap[onlineAccMap[token]].id;
        }

        public void online(NetFrame.UserToken token, string account)
        {
            //添加映射
            onlineAccMap.Add(token, account);
        }

        public void offline(NetFrame.UserToken token)
        {
            if (onlineAccMap.ContainsKey(token)) onlineAccMap.Remove(token);
        }

        public void add(string account, string password)
        {
            AccountModel accountModel = new AccountModel();
            accountModel.account = account;
            accountModel.password = password;
            accountModel.id = index;
            accMap.Add(account, accountModel);

            index++;
        }
    }
}
