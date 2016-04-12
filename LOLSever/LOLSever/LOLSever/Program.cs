using NetFrame;
using NetFrame.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever
{
    class Program
    {
        static void Main(string[] args)
        {
            SeverStart ss = new SeverStart(9000);
            ss.encode = MessageEncoding.encode;
            ss.decode = MessageEncoding.decode;
            ss.LD = LengthEncoding.decode;
            ss.LE = LengthEncoding.encode;
            ss.center = new HandlerCenter();
            ss.Start(6666);
            Console.WriteLine("服务器启动成功");
            while (true) { }
        }
    }
}
