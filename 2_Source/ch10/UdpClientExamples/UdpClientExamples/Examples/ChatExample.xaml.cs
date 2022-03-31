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
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatExample : Window
    {
        public IPEndPoint localEndPoint { get; set; }
        public IPEndPoint remoteEndPoint { get; set; }
        private UdpClient client;
        public ChatExample()
        {
            InitializeComponent();
            this.Loaded += ChatExample_Loaded;
        }

        void ChatExample_Loaded(object sender, RoutedEventArgs e)
        {
            client = new UdpClient(localEndPoint);
            AddInfo("监听的IP地址和端口：{0}\n", localEndPoint);
            Task.Run(() => ReceiveDataAsync());
        }

        public async void ReceiveDataAsync()
        {
            while (true)
            {
                var result = await client.ReceiveAsync();
                string s = Encoding.Unicode.GetString(result.Buffer);
                AddInfo("来自{0}：{1}\n", result.RemoteEndPoint, s);
            }
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string s = textBoxSend.Text;
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            await client.SendAsync(bytes, bytes.Length, remoteEndPoint);
            AddInfo("向{0}发送：{1}\n", remoteEndPoint, s);
        }

        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Dispatcher.Invoke(() =>
            {
                textBlock1.Text += string.Format(format, args);
            });
        }
    }
}
