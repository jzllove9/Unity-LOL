using LOLSever.cache;
using LOLSever.cache.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.biz.impl
{
    public class AccountBiz : IAccountBiz
    {
        IAccountCache accountCache = CacheFactory.accountCache;

        public int create(NetFrame.UserToken token, string account, string password)
        {
            if (accountCache.hasAccount(account)) return 1;
            accountCache.add(account, password);

            return 0;
        }

        public int login(NetFrame.UserToken token, string account, string password)
        {
            if (account == null || password == null) return -4;
            if (!accountCache.hasAccount(account)) return -1;
            if (accountCache.isOnline(account)) return -2;
            if (!accountCache.match(account, password)) return -3;
            accountCache.online(token, account);
            return 0;
        }

        public void close(NetFrame.UserToken token)
        {
            accountCache.offline(token);
        }

        public int get(NetFrame.UserToken token)
        {
            return accountCache.getID(token);
        }
    }
}
