using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSever.dao.model
{
    public class UserModel
    {
        public int id;//唯一识别码
        public string name;//昵称
        public int level;
        public int exp;
        public int winCount;
        public int loseCount;
        public int runCount;

        public UserModel()
        {
            level = 0;
            exp = 0;
            winCount = 0;
            loseCount = 0;
            runCount = 0;
        }
    }
}
