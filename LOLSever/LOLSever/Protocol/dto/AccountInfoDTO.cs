using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol.dto
{
    //传输模型规则：必须添加序列化标签
    [Serializable]
    
    public class AccountInfoDTO
    {
        public string account;
        public string password;
    }
}
