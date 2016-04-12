using LOLSever.biz;
using LOLSever.biz.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.biz
{
    public class BizFactory
    {
        public readonly static IAccountBiz accountBiz;
        public readonly static IUserBiz userBiz;

        static BizFactory()
        {
            accountBiz = new AccountBiz();
            userBiz = new UserBiz();
        }
    }
}
