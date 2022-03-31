using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
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
using Client.AirportServiceDLQReference;

namespace Client.Examples
{
    /// <summary>
    /// PerAppDLQPage.xaml 的交互逻辑
    /// </summary>
    public partial class DeadLetterPage : Page
    {
        private static TextBlock textBlockInfo;
        ServiceHost host;
        public DeadLetterPage()
        {
            InitializeComponent();
            this.Loaded += PerAppDLQPage_Loaded;
            this.Unloaded += PerAppDLQPage_Unloaded;
        }

        void PerAppDLQPage_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockInfo = textBlock1;
            string q = ConfigurationManager.AppSettings["queueNameDeadLetter"];
            if (!MessageQueue.Exists(q)) MessageQueue.Create(q, true);
        }

        void PerAppDLQPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                if (host.State == CommunicationState.Opened) host.Close();
            }
        }

        private async void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn2.IsEnabled = false;
            if (host != null)
            {
                if (host.State == CommunicationState.Opened) host.Close();
            }
            AirportServiceDLQClient client = new AirportServiceDLQClient();
            AirportMessage m = new AirportMessage()
            {
                AirportId = "0001",
                ForecastTime = DateTime.Now,
                ShortMessage = "0356 1157Z"
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                client.SubmitInfo("例4");
                client.SubmitAirportMessage(m);
                scope.Complete();
            }
            client.Close();
            textBlock1.Text += "已发送，请等待死信处理按钮可用。\n";
            await Task.Delay(TimeSpan.FromSeconds(5));
            btn2.IsEnabled = true;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                if (host.State == CommunicationState.Opened)
                {
                    host.Close();
                }
            }
            host = new ServiceHost(typeof(Service.DeadLetterService));
            host.Open(); 
            AddInfo("死信处理服务已启动");
        }

        public static void AddInfo(string format, params object[] args)
        {
            textBlockInfo.Text += string.Format(format, args) + "\n";
        }
    }
}
