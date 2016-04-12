using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IHandler
{
    void MessageReceive(SocketModel model);
}

