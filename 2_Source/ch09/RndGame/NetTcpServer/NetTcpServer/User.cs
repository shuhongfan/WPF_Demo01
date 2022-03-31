using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTcpServer
{
    public class User
    {
        public string UserName { get; set; }
        public readonly IRndGameServiceCallback callback;

        /// <summary>用户是否坐到某一桌的座位上</summary>
        public bool IsSitDown { get; set; }

        /// <summary>用户所坐的桌号(-1:大厅)</summary>
        public int Index { get; set; }

        /// <summary>用户所坐的座位号(0:黑方，1:白方)</summary>
        public int Side { get; set; }

        /// <summary>是否已经开始游戏</summary>
        public bool IsStarted { get; set; }

        public User(string userName, IRndGameServiceCallback callback)
        {
            this.UserName = userName;
            this.callback = callback;
        }
    }
}
