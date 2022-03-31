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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UdpClientExamples.Examples;

namespace UdpClientExamples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPAddress ip;
        public MainWindow()
        {
            InitializeComponent();
            //获取本机所有IPv4地址
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var v in ips)
            {
                //判断是否为IPv4
                if (v.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = v; break;
                }
            }
        }

        private void btn1_click(object sender, RoutedEventArgs e)
        {
            ChatExample w1 = new ChatExample()
            {
                Title = "客户端1",  Owner = this,
                Left = this.Left - 360,  Top = this.Top - 50,
                localEndPoint = new IPEndPoint(ip, 8001),
                remoteEndPoint = new IPEndPoint(ip, 8002)
            };
            w1.textBoxSend.Text = "你还好吧！";  w1.Show();

            ChatExample w2 = new ChatExample()
            {
                Title = "客户端2",  Owner = this,
                Left = this.Left + 210,  Top = this.Top - 50,
                localEndPoint = new IPEndPoint(ip, 8002),
                remoteEndPoint = new IPEndPoint(ip, 8001)
            };
            w2.textBoxSend.Text = "嗯？";  w2.Show();
        }

        private void btn2_click(object sender, RoutedEventArgs e)
        {
            MulticastGroupExample w1 = new MulticastGroupExample()
            {
                Title = "客户端1",  Owner = this,
                Left = this.Left - 360,  Top = this.Top - 50,
                localEndPoint = new IPEndPoint(ip, 8001),
            };
            w1.textBoxSend.Text = "同志们好！";  w1.Show();

            MulticastGroupExample w2 = new MulticastGroupExample()
            {
                Title = "客户端2",  Owner = this,
                Left = this.Left + 210, Top = this.Top - 50,
                localEndPoint = new IPEndPoint(ip, 8002),
            };
            w2.textBoxSend.Text = "同志们辛苦了！"; w2.Show();
        }

        private void btn3_click(object sender, RoutedEventArgs e)
        {
            int servicePort = 8000;
            RoomService service = new RoomService(ip, servicePort);

            ShowClient("客户端1", "张三易", -360, -50, 8001, servicePort);
            ShowClient("客户端2", "李四耳", 210, -50, 8002, servicePort);
            ShowClient("客户端3", "王五伞", -100, 140, 8003, servicePort);
        }

        private void ShowClient(string title, string userName,
               double dx, double dy, int port, int servicePort)
        {
            NetMeetingClient w = new NetMeetingClient()
            {
                Title = title,
                Owner = this,
                Left = this.Left + dx,
                Top = this.Top + dy,
                localEndPoint = new IPEndPoint(ip, port),
                remoteEndPoint = new IPEndPoint(ip, servicePort)
            };
            w.UserName = userName;
            w.Show();
        }

        private bool IsMulticastAddress(IPAddress address)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                //该地址为IPv4
                byte[] addressBytes = address.GetAddressBytes();
                //将第1个字节和11100000相与，如果结果为11100000,说明该地址在224到239之间
                return ((addressBytes[0] & 0xE0) == 0xE0);
            }
            else
            {
                //该地址为IPv6 
                return address.IsIPv6Multicast;
            }
        }
    }
}
