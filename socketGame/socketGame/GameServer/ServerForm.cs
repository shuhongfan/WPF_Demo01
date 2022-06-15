using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer
{
    public partial class ServerForm : Form
    {
        //游戏室允许进入的最多人数
        private int maxUsers = 300;
        //连接的用户
        List<User> userList = new List<User>();
        //游戏室开出的桌数
        private int maxTable = 100;
        private GameTable[] gameTable;
        //IP地址
        IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        //监听端口
        private int port = 4899;
        private TcpListener myListener;
        private Service service;
        public ServerForm()
        {
            InitializeComponent();
            service = new Service(lst_ServiceInfo);
        }

        /// <summary>
        /// 加载窗体的触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerForm_Load(object sender, EventArgs e)
        {
            lst_ServiceInfo.HorizontalScrollbar = true;
            btn_Stop.Enabled = false;
        }

        /// <summary>
        /// 【启动服务】按钮的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_GameTable.Text, out maxTable) == false
                || int.TryParse(txt_MaxPerson.Text, out maxUsers) == false)
            {
                MessageBox.Show("请输入在规定范围内的正整数");
                return;
            }
            if (maxUsers < 1 || maxUsers > 300)
            {
                MessageBox.Show("允许进入的人数只能在1-300之间");
                return;
            }
            if (maxTable < 1 || maxTable > 100)
            {
                MessageBox.Show("允许的桌数只能在1-100之间");
                return;
            }
            txt_MaxPerson.Enabled = false;
            txt_GameTable.Enabled = false;
            //初始化数组
            gameTable = new GameTable[maxTable];
            for (int i = 0; i < maxTable; i++)
            {
                gameTable[i] = new GameTable(lst_ServiceInfo);
            }
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            service.AddItem(string.Format("开始在{0}:{1}监听客户端连接", localAddress, port));
            //创建一个线程监听客户端连接请求
            ThreadStart ts = new ThreadStart(ListenClientConnect);
            Thread myThread = new Thread(ts);
            myThread.Start();
            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;
        }

        /// <summary>
        /// 【停止服务】按钮的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //停止向游戏桌发送棋子
            for (int i = 0; i < maxTable; i++)
            {
                gameTable[i].StopTimer();
            }
            service.AddItem(string.Format("目前连接的用户数：{0}", userList.Count));
            service.AddItem("开始停止服务，并依次使用户退出！");
            for (int i = 0; i < userList.Count; i++)
            {
                //关闭后，客户端接收字符串为null
                //使接收该客户的线程 ReceiveData 接收的字符串也为 null
                //从而达到结束线程的目的
                userList[i].client.Close();
            }
            //通过停止监听让myListener.AccepTcpClient()产生异常退出监听线程
            myListener.Stop();
            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
            txt_GameTable.Enabled = true;
            txt_MaxPerson.Enabled = true;
        }

        /// <summary>
        /// 接收客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    //等待用户进入
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击“停止监听”或者退出此窗体时AcceptTcpClient()会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接受一个客户端连接，就创建一个对应的线程循环接收客户端发来的信息
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                userList.Add(user);
                service.AddItem(string.Format("{0}进入",newClient.Client.RemoteEndPoint));
                service.AddItem(string.Format("当前连接用户数：{0}",userList.Count));
            }
        }

        /// <summary>
        /// 接收、处理客户端信息，每个客户1个线程，参数用户区分是哪个客户
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            TcpClient client = user.client;
            //是否正常退出接收线程
            bool normalExit = false;
            //用于控制是否退出循环
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.sr.ReadLine();
                }
                catch(Exception ex)
                {
                    //该客户底层套接字不存在时会出现异常
                    service.AddItem("接收数据失败，原因：" + ex.Message);
                }
                //TcpClient 对象将套接字进行了封装，如果TcpClient对象关闭了，
                //但是底层套接字未关闭，并不产生异常，但是读取的结果为 null
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //如果停止了监听，Connected 为 false
                        if (client.Connected == true)
                        {
                            service.AddItem(string.Format("与{0}失去联系，已停止接收该用户信息", 
                                client.Client.RemoteEndPoint));
                        }
                        //如果该用户正在游戏桌上，则退出游戏桌
                        RemoveClientfromPlayer(user);
                    }
                    //退出循环
                    break;
                }
                service.AddItem(string.Format("来自{0}:{1}", user.userName, receiveString));
                string[] splitString = receiveString.Split(',');
                int tableIndex = -1;    //桌号
                int side = -1;          //座位号
                int anotherSide = 01;   //对方座位号
                string sendString = "";
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":
                        //格式：Login,昵称
                        //该用户刚刚登陆
                        if (userList.Count > maxUsers)
                        {
                            sendString = "Sorry";
                            service.SendToOne(user, sendString);
                            service.AddItem("人数已满，拒绝" + splitString[1] + "进入游戏室");
                            exitWhile = true;
                        }
                        else
                        {
                            /*
                             *将用户昵称保存到用户列表中
                             *用于是引用类型，因此直接给user赋值也就是给userList中对应的user赋值
                             *用户名中包含其IP和端口的目的是为了帮助理解，实际游戏中一般不会显示IP的
                             */
                            user.userName = string.Format("[{0}--{1}]", splitString[1], client.Client.RemoteEndPoint);
                            //允许该用户进入游戏室，即将各桌是否有人的情况发送给该用户
                            sendString = "Tables," + this.GetOnlineString();
                            service.SendToOne(user, sendString);
                        }
                        break;
                    case "logout":
                        //格式：Logout
                        //用户退出游戏室
                        service.AddItem(string.Format("{0}退出游戏室", user.userName));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "sitdown":
                        //格式：SitDown,桌号,座位号
                        //该用户坐到某座位上
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        gameTable[tableIndex].gamePlayer[side].user = user;
                        gameTable[tableIndex].gamePlayer[side].someone = true;
                        service.AddItem(string.Format("{0}在第{1}桌第{2}座入座", 
                            user.userName, tableIndex + 1, side + 1));
                        //得到对家座位号
                        anotherSide = (side + 1) % 2;
                        //判断对方是否有人
                        if (gameTable[tableIndex].gamePlayer[anotherSide].someone == true)
                        {
                            //先告诉该用户对家已经入座
                            //发送格式：SitDown,座位号,用户名
                            sendString = string.Format("SitDown,{0},{1}",
                                anotherSide, gameTable[tableIndex].gamePlayer[anotherSide].user.userName);
                            service.SendToOne(user, sendString);
                        }
                        //同时告诉两个用户该用户入座（也可能对方无人）
                        //发送格式： SitDown,座位号,用户名
                        sendString = string.Format("SitDown,{0},{1}",
                            side, user.userName);
                        service.SendToBoth(gameTable[tableIndex], sendString);
                        //重新将游戏室各桌情况发送给所有用户
                        service.SendToAll(userList, "Tables," + this.GetOnlineString());
                        break;
                    case "getup":
                        //格式：GetUp,桌号,座位号
                        //用户离开座位,回到游戏室
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        service.AddItem(
                            string.Format("{0}离座，返回游戏室", user.userName));
                        gameTable[tableIndex].StopTimer();
                        //将离座信息同时发送给两个用户，以便客户端座位离座处理
                        //发送格式：GetUp,座位号,用户名
                        service.SendToBoth(gameTable[tableIndex], string.Format("GetUp,{0},{1}", side, user.userName));
                        //服务器进行离座处理
                        gameTable[tableIndex].gamePlayer[side].someone = false;
                        gameTable[tableIndex].gamePlayer[side].started = false;
                        gameTable[tableIndex].gamePlayer[side].grade = 0;
                        anotherSide = (side + 1) % 2;
                        if (gameTable[tableIndex].gamePlayer[anotherSide].someone == true)
                        {
                            gameTable[tableIndex].gamePlayer[anotherSide].started = false;
                            gameTable[tableIndex].gamePlayer[anotherSide].grade = 0;
                        }
                        //重新将游戏室各桌情况发送给所有用户
                        service.SendToAll(userList, "Tables," + this.GetOnlineString());
                        break;
                    case "level":
                        //格式：Time,桌号,难度级别
                        //设置难度级别
                        tableIndex = int.Parse(splitString[1]);
                        gameTable[tableIndex].SetTimerLevel((6 - int.Parse(splitString[2])) * 100);
                        service.SendToBoth(gameTable[tableIndex], receiveString);
                        break;
                    case "talk":
                        //格式：Talk,桌号,对话内容
                        tableIndex = int.Parse(splitString[1]);
                        //由于聊天内容可能包含逗号，所以需要特殊处理
                        sendString = string.Format("Talk,{0},{1}", user.userName
                            , receiveString.Substring(splitString[0].Length + splitString[1].Length));
                        service.SendToBoth(gameTable[tableIndex], sendString);
                        break;
                    case "start":
                        //格式：Start,桌号,座位号
                        //该用户单击了开始按钮
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        gameTable[tableIndex].gamePlayer[side].started = true;
                        if (side == 0)
                        {
                            anotherSide = 1;
                            sendString = "Message,黑方已开始。";
                        }
                        else
                        {
                            anotherSide = 0;
                            sendString = "Message,白方已开始。";
                        }
                        service.SendToBoth(gameTable[tableIndex], sendString);
                        if (gameTable[tableIndex].gamePlayer[anotherSide].started == true)
                        {
                            gameTable[tableIndex].ResetGrid();
                            gameTable[tableIndex].StartTimer();
                        }
                        break;
                    case "unsetdot":
                        //格式：UnsetDot,桌号,座位号,行,列,颜色
                        //消去客户点击的棋子
                        tableIndex = int.Parse(splitString[1]);
                        side = int.Parse(splitString[2]);
                        int xi = int.Parse(splitString[3]);
                        int xj = int.Parse(splitString[4]);
                        int color = int.Parse(splitString[5]);
                        gameTable[tableIndex].UnsetDot(xi, xj, color);
                        break;
                    default:
                        service.SendToAll(userList, "什么意思啊：" + receiveString);
                        break;
                }
            }
            userList.Remove(user);
            client.Close();
            service.AddItem(string.Format("有一个退出，剩余连接用户数：{0}", userList.Count));
        }

        /// <summary>
        /// 循环检测该用户是否坐到某游戏桌上，如果是，将其从游戏桌上移除，并终止该桌游戏
        /// </summary>
        /// <param name="user">客户端</param>
        private void RemoveClientfromPlayer(User user)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (gameTable[i].gamePlayer[j].user != null)
                    {
                        //判断是否同一对象
                        if (gameTable[i].gamePlayer[j].user == user)
                        {
                            StopPlayer(i, j);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 停止第 i 桌的游戏
        /// </summary>
        /// <param name="i">第 i 桌</param>
        /// <param name="j">座位号</param>
        private void StopPlayer(int i, int j)
        {
            gameTable[i].StopTimer();
            gameTable[i].gamePlayer[j].someone = false;
            gameTable[i].gamePlayer[j].started = false;
            gameTable[i].gamePlayer[j].grade = 0;
            int otherSide = (j + 1) % 2;
            if (gameTable[i].gamePlayer[otherSide].someone == true)
            {
                gameTable[i].gamePlayer[otherSide].started = false;
                gameTable[i].gamePlayer[otherSide].grade = 0;
                if(gameTable[i].gamePlayer[otherSide].user.client.Connected == true)
                {
                    //发送格式：Lost,座位号,用户名
                    service.SendToOne(gameTable[i].gamePlayer[otherSide].user,
                        string.Format("Lost,{0},{1}",
                        j, gameTable[i].gamePlayer[j].user.userName));
                }
            }
        }

        /// <summary>
        /// 获取每桌是否有人的字符串，每座用一位表示，0表示无人，1表示有人
        /// </summary>
        /// <returns></returns>
        private string GetOnlineString()
        {
            string str = "";
            for (int i = 0; i < gameTable.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    str += gameTable[i].gamePlayer[j].someone == true ? "1" : "0";
                }
            }
            return str;
        }

        /// <summary>
        /// 窗体关闭前触前的触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未单击开始服务就直接退出时，myListener为null
            if (myListener != null)
            {
                btn_Stop_Click(null, null);
            }
        }


    }
}
