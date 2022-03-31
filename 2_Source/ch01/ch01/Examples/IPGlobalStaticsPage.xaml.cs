using System;
using System.Collections.Generic;
using System.Linq;
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
    /// IPGlobalStaticsPage.xaml 的交互逻辑
    /// </summary>
    public partial class IPGlobalStaticsPage : Page
    {
        public IPGlobalStaticsPage()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();
            sb.AppendLine("本机注册域名 : " + properties.DomainName);
            sb.AppendLine("接收数据包数 : " + ipstat.ReceivedPackets);
            sb.AppendLine("转发数据包数 : " + ipstat.ReceivedPacketsForwarded);
            sb.AppendLine("传送数据包数 : " + ipstat.ReceivedPacketsDelivered);
            sb.AppendLine("丢弃数据包数 : " + ipstat.ReceivedPacketsDiscarded);
            textBlock1.Text = sb.ToString();
        }
    }
}
