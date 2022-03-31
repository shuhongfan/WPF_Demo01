using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        private string userName;
        private TcpClient client;

        public User(TcpClient client, bool isTask)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            if (isTask)
            {
                Task.Run(() => ReceiveFromClient());
            }
            else
            {
                Thread t = new Thread(ReceiveFromClient);
                t.Start();
            }
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }

        /// <summary>处理接收的客户端数据</summary>
        public void ReceiveFromClient()
        {
            while (true)
            {
                string receiveString = null;
                try
                {
                    receiveString = br.ReadString();
                }
                catch
                {
                    CC.RemoveUser(this); return;
                }
                string[] split = receiveString.Split(',');
                switch (split[0])
                {
                    case "Login":  //格式：Login，用户名
                        userName = split[1];
                        CC.SendToAllClient(split[1] + "登录成功，当前用户数：" + CC.userList.Count);
                        break;
                    case "Logout":  //格式：Logout,用户名
                        CC.RemoveUser(this);
                        return;
                    case "Talk":  //格式：Talk，对话信息
                        CC.SendToAllClient(userName + "说：" + receiveString.Remove(0, 5));
                        break;
                    default:
                        throw new Exception("无法解析：" + receiveString);
                }
            }
        }
    }
}
