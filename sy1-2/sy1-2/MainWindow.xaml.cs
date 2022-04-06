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

namespace sy1_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("获取www.cctv.com的所有IP地址：");
            try
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses("www.cctv.com");
                foreach (IPAddress ip in ipAddresses)
                {
                    sb.AppendLine(ip.ToString());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "获取失败");
            }

            string hostName = Dns.GetHostName();
            sb.AppendLine("获取本机所有IP地址:");
            IPHostEntry me = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in me.AddressList)
            {
                if (ip.AddressFamily==AddressFamily.InterNetwork)
                {
                    sb.AppendLine("IPV4:"+ip.ToString());
                }
                else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    sb.AppendLine("ipv6:" + ip.ToString());
                }
                else
                {
                    sb.AppendLine("其他：" + ip.ToString());
                }
            }

            IPAddress localip = IPAddress.Parse("::1");
            Output(sb, localip);
            IPAddress localip1 = IPAddress.Parse("127.0.0.1");
            Output(sb, localip1);

            textBlock.Text = sb.ToString();
        }

        private static void Output(StringBuilder sb, IPAddress localip)
        {
            IPEndPoint iep = new IPEndPoint(localip, 80);
            if (localip.AddressFamily == AddressFamily.InterNetworkV6)
            {
                sb.Append("IPv6端点： " + iep.ToString());
            }
            else
            {
                sb.Append("IPv4端点： " + iep.ToString());
            }
            sb.Append("，端口 " + iep.Port);
            sb.Append("，地址 " + iep.Address);
            sb.AppendLine("，地址族 " + iep.AddressFamily);
        }

    }
}
