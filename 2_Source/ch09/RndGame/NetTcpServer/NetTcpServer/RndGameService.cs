using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NetTcpServer
{
    /// <summary>处理每个登录用户</summary>
    public class RndGameService : IRndGameService
    {
        public RndGameService()
        {
            if (CC.Users == null)
            {
                CC.Users = new List<User>();
                CC.Rooms = new Room[CC.maxRooms];
                for (int i = 0; i < CC.maxRooms; i++)
                {
                    CC.Rooms[i] = new Room();
                }
            }
        }

        public void Login(string userName)
        {
            OperationContext context = OperationContext.Current;
            IRndGameServiceCallback callback = context.GetCallbackChannel<IRndGameServiceCallback>();
            User newUser = new User(userName, callback);
            CC.Users.Add(newUser);
            foreach (var user in CC.Users)
            {
                user.callback.ShowLogin(userName, CC.maxRooms);
            }
            SendRoomsInfoToAllUsers();
        }

        /// <summary>用户退出</summary>
        public void Logout(string userName)
        {
            User logoutUser = CC.GetUser(userName);
            foreach (var user in CC.Users)
            {
                //不需要发给退出用户
                if (user.UserName != logoutUser.UserName)
                {
                    user.callback.ShowLogout(userName);
                }
            }
            CC.Users.Remove(logoutUser);
            logoutUser = null; //将其设置为null后，WCF会自动关闭该客户端
            SendRoomsInfoToAllUsers();
        }

        /// <summary>用户入座,参数：用户名,桌号,座位号</summary>
        public void SitDown(string userName, int index, int side)
        {
            User p = CC.GetUser(userName);
            p.Index = index;
            p.Side = side;
            CC.Rooms[index].players[side] = p;
            //告诉入座玩家入座信息
            p.callback.ShowSitDown(userName, side);

            int anotherSide = (side + 1) % 2;  //同一桌的另一个玩家
            User p1 = CC.Rooms[index].players[anotherSide];
            if (p1 != null)
            {
                //告诉入座玩家另一个玩家是谁
                p.callback.ShowSitDown(p1.UserName, anotherSide);
                //告诉另一个玩家入座玩家是谁
                p1.callback.ShowSitDown(p.UserName, side);
            }
            //重新将游戏室各桌情况发送给所有用户
            SendRoomsInfoToAllUsers();
        }

        /// <summary>用户离开座位退出,参数：桌号,座位号,游戏是否已经开始</summary>
        public void GetUp(int index, int side)
        {
            CC.Rooms[index].timer.Stop();
            User p0 = CC.Rooms[index].players[side];
            User p1 = CC.Rooms[index].players[(side + 1) % 2];
            p0.callback.ShowGetUp(p0.UserName, side);
            CC.Rooms[index].players[side] = null; //注意该语句执行后p0!=null
            if (p1 != null)
            {
                p1.callback.ShowGetUp(p0.UserName, side);
                p1.IsStarted = false;
            }
            //重新将游戏室各桌情况发送给所有用户
            SendRoomsInfoToAllUsers();
        }

        /// <summary>该用户单击了开始按钮,参数：用户名,桌号,座位号</summary>
        public void Start(string userName, int index, int side)
        {
            User p0 = CC.Rooms[index].players[side];
            p0.IsStarted = true;
            p0.callback.ShowStart(side);
            int anotherSide = (side + 1) % 2;   //对方座位号
            User p1 = CC.Rooms[index].players[anotherSide];
            if (p1 != null)
            {
                p1.callback.ShowStart(side);
                if (p1.IsStarted)
                {
                    CC.Rooms[index].ResetGrid();
                    p0.callback.GameStart();
                    p1.callback.GameStart();
                    CC.Rooms[index].timer.Start();
                }
            }
        }

        /// <summary>消去客户单击的棋子,参数：桌号,行,列</summary>
        public void UnsetDot(int tableIndex, int row, int col)
        {
            CC.Rooms[tableIndex].UnsetGridDot(row, col);
        }

        /// <summary>设置难度级别,参数：桌号,难度级别(1～5)</summary>
        public void SetLevel(int index, int level)
        {
            CC.Rooms[index].timer.Interval = 1400 - (level - 1) * 300;
            if (CC.Rooms[index].players[0] != null)
            {
                CC.Rooms[index].players[0].callback.UpdateLevel(level);
            }
            if (CC.Rooms[index].players[1] != null)
            {
                CC.Rooms[index].players[1].callback.UpdateLevel(level);
            }
        }

        /// <summary>客户端发来的对话信息,参数：桌号,发送者用户名，对话内容</summary>
        public void Talk(int index, string userName, string message)
        {
            User p0 = CC.Rooms[index].players[0];
            User p1 = CC.Rooms[index].players[1];
            if (p0 != null) p0.callback.ShowTalk(userName, message);
            if (p1 != null) p1.callback.ShowTalk(userName, message);
        }

        /// <summary>每座位用一位表示，0表示无人，1表示有人</summary>
        private string GetTablesInfo()
        {
            string str = "";
            for (int i = 0; i < CC.Rooms.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    str += CC.Rooms[i].players[j] == null ? "0" : "1";
                }
            }
            return str;
        }

        /// <summary>将当前游戏室情况发送给所有用户</summary>
        private void SendRoomsInfoToAllUsers()
        {
            int userCount = CC.Users.Count;
            string roomInfo = this.GetTablesInfo();
            foreach (var user in CC.Users)
            {
                user.callback.UpdateTablesInfo(roomInfo, userCount);
            }
        }

    }
}
