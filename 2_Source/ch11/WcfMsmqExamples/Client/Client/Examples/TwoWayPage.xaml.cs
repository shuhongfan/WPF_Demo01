using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.AirportServiceTwoWayReference;
using Client.Service;

namespace Client.Examples
{
    /// <summary>
    /// DuplexPage.xaml 的交互逻辑
    /// </summary>
    public partial class TwoWayPage : Page
    {
        private static TextBlock textBlockInfo;
        ServiceHost host;
        public TwoWayPage()
        {
            InitializeComponent();
            textBlockInfo = textBlock1;
            this.Loaded += TwoWayPage_Loaded;
            this.Unloaded += TwoWayPage_Unloaded;
        }

        void TwoWayPage_Unloaded(object sender, RoutedEventArgs e)
        {
            host.Close();
        }

        void TwoWayPage_Loaded(object sender, RoutedEventArgs e)
        {
            //获取队列名
            string q = ConfigurationManager.AppSettings["queueNameStatusService"];
            //如果队列不存在则创建，true表示创建事务队列，false表示创建非事务队列
            if (!MessageQueue.Exists(q)) MessageQueue.Create(q, true);
            host = new ServiceHost(typeof(AirportMessageStatusService));
            host.Open();
            textBlock1.Text = "接收状态通知的服务已启动\n";
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            AirportServiceTwoWayClient client = new AirportServiceTwoWayClient();
            client.SubmitInfo("例3");
            AirportMessage m = new AirportMessage();
            m.AirportId = "0003";
            m.ForecastTime = DateTime.Now;
            m.ShortMessage = "0333 1100Z";
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                string hostName = Dns.GetHostName();
                string address = "net.msmq://" + hostName + "/private/AirportMessageStatus";
                client.SubmitAirportMessageTwoWay(m, address);
                scope.Complete();
            }
            client.Close();
            textBlock1.Text += "已发送。\n";
        }

        public static void AddInfo(string format, params object[] args)
        {
            textBlockInfo.Text += string.Format(format, args) + "\n";
        }
    }
}
