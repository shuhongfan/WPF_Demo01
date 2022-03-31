using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// ClientWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadClientWindow : Window
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        private string remoteHost;
        private int remotePort = 51888;

        public ThreadClientWindow()
        {
            InitializeComponent();
            this.Closing += ClientWindow_Closing;
            remoteHost = Dns.GetHostName();
        }

        void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
            {
                SendMessage("Logout," + textBoxUserName.Text);//格式：Logout,用户名
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
            }
        }

        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Dispatcher.InvokeAsync(() =>
            {
                textBlock1.Text += string.Format(format, args) + "\n";
            });
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new TcpClient(remoteHost, remotePort);
                AddInfo("与服务端连接成功");
                btnLogin.IsEnabled = false;
            }
            catch
            {
                AddInfo("与服务端连接失败");
                return;
            }
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendMessage("Login," + textBoxUserName.Text);//格式：Login,用户名
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("Talk," + textBoxSend.Text);//格式：Talk,对话信息
            textBoxSend.Clear();
        }

        /// <summary>处理接收的服务器端数据</summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try { receiveString = br.ReadString(); }
                catch
                {
                    if (isExit == false) AddInfo("与服务器失去联系。");
                    break;
                }
                AddInfo(receiveString);
            }
        }

        /// <summary>向服务器端发送信息</summary>
        private void SendMessage(string message)
        {
            try { bw.Write(message); bw.Flush(); }
            catch { AddInfo("发送失败!"); }
        }
    }
}
