using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UdpClientExamples.Examples
{
    /// <summary>
    /// NetMeeting.xaml 的交互逻辑
    /// </summary>
    public partial class NetMeetingClient : Window
    {
        public IPEndPoint localEndPoint { get; set; }
        public IPEndPoint remoteEndPoint { get; set; }
        public string UserName
        {
            get { return textBoxUserName.Text; }
            set { textBoxUserName.Text = value; }
        }
        private IPAddress multicastAddress = IPAddress.Parse("224.0.0.1");
        private UdpClient client;
        private bool isExit = false;
        private bool isLogin = false;
        public NetMeetingClient()
        {
            InitializeComponent();
            this.Loaded += NetMeeting_Loaded;
            this.Closing += NetMeeting_Closing;
        }

        void NetMeeting_Loaded(object sender, RoutedEventArgs e)
        {
            btnLogout.IsEnabled = false;
        }

        void NetMeeting_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isLogin)
            {
                SendMessage("Logout," + UserName);
            }
            isExit = true;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            client = new UdpClient(localEndPoint);
            ReceiveDataAsync();
            SendMessage("Login," + UserName);
            isLogin = true;
            btnLogin.IsEnabled = false;
            btnLogout.IsEnabled = true;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("Say," + UserName + "," + textBoxTalk.Text);
        }

        private void SendMessage(string sendString)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendString);
            client.Send(bytes, bytes.Length, remoteEndPoint);
        }

        private void AddUser(string userName)
        {
            Label label = new Label();
            label.Content = userName;
            listBox1.Items.Add(label);
        }

        private void RemoveUser(string userName)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Label label = listBox1.Items[i] as Label;
                if (label.Content.ToString() == userName)
                {
                    listBox1.Items.Remove(label);
                    break;
                }
            }
        }

        private void AddTalk(string format, params object[] args)
        {
            Label label = new Label();
            label.Content = string.Format(format, args);
            listBox2.Items.Add(label);
        }

        private async void ReceiveDataAsync()
        {
            client.JoinMulticastGroup(multicastAddress);
            while (isExit == false)
            {
                var result = await client.ReceiveAsync();
                string str = Encoding.UTF8.GetString(result.Buffer);
                string[] split = str.Split(',');
                string args = str.Remove(0, split[0].Length + 1);
                string command = split[0];
                int s = split[0].Length;
                switch (split[0])
                {
                    case "Login":  //格式：Login，用户名 
                        {
                            string userName = args;
                            AddUser(userName);
                            break;
                        }
                    case "List": //格式：List,以逗号分隔的人员姓名列表
                        {
                            for (int i = 1; i < split.Length; i++)
                            {
                                AddUser(split[i]);
                            }
                            break;
                        }
                    case "Say":  //格式：Say,用户名,发言内容
                        AddTalk("{0}：{1}", split[1], args.Remove(0, split[1].Length + 1));
                        break;
                    case "Logout": //格式：Logout,用户名
                        {
                            string userName = args;
                            if (userName != this.UserName)
                            {
                                RemoveUser(userName);
                            }
                            break;
                        }
                    case "CanNotLogin":
                        MessageBox.Show("无法进入会议室，原因：" + args);
                        this.Close();
                        break;
                }
            }
            client.DropMulticastGroup(this.multicastAddress);
            client.Close();
        }

    }
}
