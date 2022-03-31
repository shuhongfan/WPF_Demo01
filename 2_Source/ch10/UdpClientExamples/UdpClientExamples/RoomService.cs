using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UdpClientExamples
{
    /// <summary>
    /// 会议室，每个会议室都要有一个管理员
    /// </summary>
    public class RoomService
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private UdpClient client;
        private IPAddress multicastAddress = IPAddress.Parse("224.0.0.1");
        public bool isExit { get; set; }
        public IPEndPoint localEndPoint { get; set; }

        public RoomService(IPAddress localAddress,int servicePort)
        {
            localEndPoint = new IPEndPoint(localAddress, servicePort);
            client = new UdpClient(localEndPoint);
            ReceiveDataAsync();
        }

        public void CloseService()
        {
            isExit = true;
        }

        private async void ReceiveDataAsync()
        {
            client.JoinMulticastGroup(multicastAddress);
            while (isExit == false)
            {
                try
                {
                    var result = await client.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    string[] split = message.Split(',');
                    string command = split[0];
                    string args = message.Remove(0, split[0].Length + 1);
                    switch (command)
                    {
                        case "Login":  //格式：Login,用户名
                            {
                                string userName = args;
                                if (users.ContainsKey(userName))
                                {
                                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes("CanNotLogin,已有该用户名");
                                    client.Send(bytes, bytes.Length, result.RemoteEndPoint);
                                }
                                else
                                {
                                    User user = new User()
                                    {
                                        UserName = userName,
                                        UserEndPoint = result.RemoteEndPoint
                                    };
                                    ShowCurrentUsers(user);
                                    users.Add(userName, user);
                                    Multicast(message);
                                }
                                break;
                            }
                        case "Logout":  //格式：Logout,用户名
                            {
                                string userName = args;
                                users.Remove(userName);
                                Multicast(message);
                                break;
                            }
                        case "Say": //格式：Say,用户名,发言内容
                            {
                                Multicast(message);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            client.DropMulticastGroup(multicastAddress);
            client.Close();
        }

        /// <summary>用户刚登录时，将会议室现有人员列表发送给该用户</summary>
        private void ShowCurrentUsers(User user)
        {
            if (users.Count == 0) return;
            string s = "List"; //格式：List,以逗号分隔的人员姓名列表
            foreach (var userName in users.Keys)
            {
                s += "," + userName;
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            client.Send(bytes, bytes.Length, user.UserEndPoint);
        }

        private void Multicast(string message)
        {
            string hostname = multicastAddress.ToString();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            for (int i = 8001; i < 8004; i++)
            {
                client.Send(bytes, bytes.Length, hostname, i);
            }
        }

    }
    public class User
    {
        public string UserName { get; set; }
        public IPEndPoint UserEndPoint { get; set; }
    }

}
