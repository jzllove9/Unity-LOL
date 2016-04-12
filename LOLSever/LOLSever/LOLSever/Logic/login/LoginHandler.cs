using LOLSever.biz;
using LOLSever.Logic.tool;
using NetFrame;
using NetFrame.auto;
using Protocol;
using Protocol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.Logic.Login
{
    public class LoginHandler : AbsOnceHandler, HandlerInterface
    {
        IAccountBiz accountBiz = BizFactory.accountBiz;

        //消息接收
        public void MessageReceive(NetFrame.UserToken token, NetFrame.auto.SocketModel model)
        {
            switch (model.command)
            {
                case LoginProtocol.LOGIN_CREQ:
                    login(token, model.GetMessage<AccountInfoDTO>());
                    break;
                case LoginProtocol.REG_CREQ:
                    reg(token, model.GetMessage<AccountInfoDTO>());
                    break;
            }
        }

        public void login(UserToken token, AccountInfoDTO value)
        {
            ExecutorPool.Instance.execute(
                    delegate()
                    {
                        int result = accountBiz.login(token, value.account, value.password);
                        write(token, LoginProtocol.LOGIN_SRES, result);
                    }
                );
        }

        public void reg(UserToken token, AccountInfoDTO value)
        {
            ExecutorPool.Instance.execute(
               delegate()
               {
                   int result = accountBiz.create(token, value.account, value.password);
                   write(token, LoginProtocol.REG_SRES, result);
               }
           );
        }

        //客户端连接
        public void ClientConnect(NetFrame.UserToken token)
        {
            ExecutorPool.Instance.execute(
               delegate()
               {

               }
             );
        }

        //连接断开
        public void ClientClose(NetFrame.UserToken token, string error)
        {
            ExecutorPool.Instance.execute(
                   delegate()
                   {
                       accountBiz.close(token);
                   }
                );
        }

        //重写GetType方法
        public override byte GetType()
        {
            return GameProtocol.Protocol.TYPE_LOGIN;
        }
    }
}
