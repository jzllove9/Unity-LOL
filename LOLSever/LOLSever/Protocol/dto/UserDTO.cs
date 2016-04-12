using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol.dto
{
    [Serializable]
    public class UserDTO
    {
        public int id;//唯一识别码
        public string name;//昵称
        public int level;
        public int exp;
        public int winCount;
        public int loseCount;
        public int runCount;

        public UserDTO() { }
        public UserDTO(int id, string name, int winCount, int loseCount, int runCount)
        {
            this.id = id;
            this.name = name;
            this.winCount = winCount;
            this.loseCount = loseCount;
            this.runCount = runCount;
        }
    }
}
