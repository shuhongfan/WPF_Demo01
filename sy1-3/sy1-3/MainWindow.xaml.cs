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

namespace sy1_3
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
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            sb.AppendLine("适配器个数：" + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                sb.AppendLine("------------第" + index + "个适配器信息" + "----------");
                sb.AppendLine("名称：" + adapter.Name);
                sb.AppendLine("类型：" + adapter.NetworkInterfaceType);
                sb.AppendLine("速度：" + adapter.Speed / 1000 / 1000 + "M");

                byte[] macBytes = adapter.GetPhysicalAddress().GetAddressBytes();
                sb.AppendLine("MAC地址：" + BitConverter.ToString(macBytes));

                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;

                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress dns in dnsServers)
                    {
                        sb.AppendLine("DNs服务器Ip地址：" + dns);
                    }
                }
                textBlock.Text=sb.ToString();
            }
        }
    }
}
