using LOLSever.dao.model;
using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.cache.impl
{
    public class UserCache : IUserCache
    {
        /// <summary>
        /// 用户ID和用户模型的映射表
        /// </summary>
        Dictionary<int, UserModel> idToModelDict = new Dictionary<int, UserModel>();
        /// <summary>
        /// 账号ID和角色ID之间的绑定
        /// </summary>
        Dictionary<int, int> accToUid = new Dictionary<int, int>();
        /// <summary>
        ///
        /// </summary>
        Dictionary<int, UserToken> idToToken = new Dictionary<int, UserToken>();
        Dictionary<UserToken, int> tokenToId = new Dictionary<UserToken, int>();

        int index = 0;

        public bool create(NetFrame.UserToken token, string name)
        {
            UserModel userModel = new UserModel();
            userModel.id = index++;
            userModel.name = name;
            idToModelDict.Add(userModel.id, userModel);
            return true;
        }

        public bool has(NetFrame.UserToken token)
        {
            return tokenToId.ContainsKey(token);
        }

        public bool hasByAccountId(int id)
        {
            return accToUid.ContainsKey(id);
        }

        public dao.model.UserModel getModel(NetFrame.UserToken token)
        {
            if (!has(token)) return null;
            return idToModelDict[tokenToId[token]];
        }

        public dao.model.UserModel getModel(int id)
        {
            return idToModelDict[id];
        }

        public void offLine(NetFrame.UserToken token)
        {
            //清空双向映射
            if (tokenToId.ContainsKey(token))
            {
                if (idToToken.ContainsKey(tokenToId[token]))
                {
                    idToToken.Remove(tokenToId[token]);
                }
                tokenToId.Remove(token);
            }
        }

        public NetFrame.UserToken getToken(int id)
        {
            return idToToken[id];
        }

        public UserModel onLine(UserToken token, int id)
        {
            idToToken.Add(id, token);
            tokenToId.Add(token, id);
            return idToModelDict[id];
        }

        public UserModel getByAccountId(int accountId)
        {
            if (!accToUid.ContainsKey(accountId)) return null;
            return idToModelDict[accToUid[accountId]];
        }

        public bool isOnline(int id)
        {
            return idToToken.ContainsKey(id);
        }
    }
}
