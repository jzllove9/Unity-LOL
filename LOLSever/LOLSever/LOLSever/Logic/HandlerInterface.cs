using NetFrame;
using NetFrame.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.Logic
{
    public interface HandlerInterface
    {
        void ClientConnect(UserToken token);

        void MessageReceive(UserToken token, SocketModel model);

        void ClientClose(UserToken token, string error);
    }
}
