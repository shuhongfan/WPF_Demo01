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
    /// Broadcast.xaml 的交互逻辑
    /// </summary>
    public partial class MulticastGroupExample : Window
    {
        public IPEndPoint localEndPoint { get; set; }
        private IPAddress multicastAddress = IPAddress.Parse("224.0.0.1");
        private IPEndPoint multicastEndPoint1, multicastEndPoint2;
        private bool isExit = false;
        private UdpClient client;
        public MulticastGroupExample()
        {
            InitializeComponent();
            multicastEndPoint1 = new IPEndPoint(multicastAddress, 8001);
            multicastEndPoint2 = new IPEndPoint(multicastAddress, 8002);
            this.Loaded += BroadcastExample_Loaded;
            this.Closing += BroadcastExample_Closing;
        }

        void BroadcastExample_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTip.Text = string.Format("在端口{0}监听，加入的多播组为{1}",
                localEndPoint.Port, multicastAddress);
            client = new UdpClient(localEndPoint);
            Task.Run(() => ReceiveDataAsync());
        }
        void BroadcastExample_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isExit = true;
        }

        private async void ReceiveDataAsync()
        {
            client.JoinMulticastGroup(multicastAddress);
            while (isExit == false)
            {
                var result = await client.ReceiveAsync();
                textBlockReceiveTip.Dispatcher.Invoke(() =>
                {
                    textBlockReceiveTip.Text += string.Format("来自{0}：{1}\n",
                        result.RemoteEndPoint, Encoding.Unicode.GetString(result.Buffer));
                });
            }
            client.DropMulticastGroup(multicastEndPoint1.Address);
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(textBoxSend.Text);
            //由于例子是在同一台计算机上同时测试两个客户端，
            //而同一台计算机上UdpClient监听的端口不能相同，所以需要分别发送。
            //在实际应用中，一个客户端只需要一个UdpClient对象，只向一个端口发送即可
            await client.SendAsync(bytes, bytes.Length,multicastEndPoint1);
            await client.SendAsync(bytes, bytes.Length, multicastEndPoint2);
        }
    }
}
