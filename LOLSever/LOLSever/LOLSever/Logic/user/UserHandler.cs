using GameProtocol;
using LOLSever.biz;
using LOLSever.dao.model;
using LOLSever.Logic.tool;
using NetFrame;
using NetFrame.auto;
using Protocol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.Logic.user
{
    public class UserHandler : AbsOnceHandler, HandlerInterface
    {
        IUserBiz userBiz = BizFactory.userBiz;

        public void ClientConnect(NetFrame.UserToken token)
        {

        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.auto.SocketModel model)
        {
            switch (model.command)
            {
                case UserProtocol.CREATE_CREQ:
                    create(token, model.GetMessage<string>());
                    break;
                case UserProtocol.INFO_CREQ:
                    info(token);
                    break;
                case UserProtocol.ONLINE_CERQ:
                    online(token);
                    break;
            }
        }

        public void ClientClose(NetFrame.UserToken token, string error)
        {

        }

        private void create(UserToken token, string model)
        {
            ExecutorPool.Instance.execute(
               delegate()
               {
                   write(token, GameProtocol.UserProtocol.CREATE_SERS, userBiz.Create(token, model));
               }
            );
        }

        private void info(UserToken token)
        {
            ExecutorPool.Instance.execute(
                  delegate()
                  {
                      write(token, GameProtocol.UserProtocol.INFO_SRES, convert(userBiz.getModel(token)));
                  }
              );
        }

        private void online(UserToken token)
        {
            ExecutorPool.Instance.execute(
                  delegate()
                  {
                      write(token, GameProtocol.UserProtocol.ONLINE_SERS, convert(userBiz.OnLine(token)));
                  }
              );
        }

        /// <summary>
        /// 将UserModel类型转化为UserDTO类型
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        private UserDTO convert(UserModel userModel)
        {
            return new UserDTO(userModel.id, userModel.name, userModel.winCount, userModel.loseCount, userModel.runCount);
        }

    }
}
