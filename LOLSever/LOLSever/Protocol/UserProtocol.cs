using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol
{

    public class UserProtocol
    {
        public const int INFO_CREQ = 0; //获取自身数据
        public const int INFO_SRES = 1; //返回自身数据
        public const int CREATE_CREQ = 2;//申请创建角色
        public const int CREATE_SERS = 3;//返回创建结果
        public const int ONLINE_CERQ = 4;//用户上线
        public const int ONLINE_SERS = 5;//返回用户上线
    }
}
