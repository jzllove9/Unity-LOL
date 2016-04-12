using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetFrame;
using LOLSever.Logic;
using LOLSever.Logic.Login;
using NetFrame.auto;
using GameProtocol;
using LOLSever.Logic.user;

namespace LOLSever
{
    public class HandlerCenter : AbsHandlerCenter
    {
        HandlerInterface login;
        HandlerInterface user;

        public HandlerCenter()
        {
            login = new LoginHandler();
            user = new UserHandler();
        }

        public override void ClientConnect(UserToken token)
        {
            Console.WriteLine("有客户端已经连接...");
        }

        public override void MessageReceive(UserToken token, object message)
        {
            SocketModel model = message as SocketModel;
            switch (model.type)
            {
                case GameProtocol.Protocol.TYPE_LOGIN:
                    login.MessageReceive(token, model);
                    break;
                case GameProtocol.Protocol.TYPE_USER:
                    user.MessageReceive(token, model);
                    break;
                default:
                    //模块未知，可能是客户端作弊，无视该请求
                    break;
            }
        }

        public override void ClientClose(UserToken token, string error)
        {
            Console.WriteLine("有客户端断开连接: " + error);
            login.ClientClose(token, error);
        }
    }
}
