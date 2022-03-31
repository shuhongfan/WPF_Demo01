using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

namespace ch01.Examples
{
    /// <summary>
    /// NetworkInterfacePage.xaml 的交互逻辑
    /// </summary>
    public partial class NetworkInterfacePage : Page
    {
        public NetworkInterfacePage()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            sb.AppendLine("适配器个数：" + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                //显示网络适配器描述信息、名称、类型、速度、MAC地址
                sb.AppendLine("-----------------第" + index + "个适配器信息-------------------");
                sb.AppendLine("描述信息：" + adapter.Description);
                sb.AppendLine("名称：" + adapter.Name);
                sb.AppendLine("类型：" + adapter.NetworkInterfaceType);
                sb.AppendLine("速度：" + adapter.Speed / 1000 / 1000 + "M");
                byte[] macBytes = adapter.GetPhysicalAddress().GetAddressBytes();
                sb.AppendLine("MAC地址：" + BitConverter.ToString(macBytes));
                //获取IPInterfaceProperties实例
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                //获取并显示DNS服务器IP地址信息
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress dns in dnsServers)
                    {
                        sb.AppendLine("DNS服务器IP地址：" + dns);
                    }
                }
            }
            textBlock1.Text = sb.ToString();
        }
    }
}
