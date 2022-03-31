using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
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

namespace Service
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static TextBlock TextBlockInfo;
        private ServiceHost[] hosts;

        public MainWindow()
        {
            InitializeComponent();

            TextBlockInfo = textBlock1;
            hosts = new ServiceHost[4];
            //获取服务专用的队列名
            string q0 = ConfigurationManager.AppSettings["queueNameTransacted"];
            string q1 = ConfigurationManager.AppSettings["queueNameVolatile"];
            string q2 = ConfigurationManager.AppSettings["queueNameTwoWay"];
            string q3 = ConfigurationManager.AppSettings["queueNameDLQ"];
            //如果队列不存在则创建，true表示创建事务队列，false表示创建非事务队列
            if (!MessageQueue.Exists(q0)) MessageQueue.Create(q0, true);
            if (!MessageQueue.Exists(q1)) MessageQueue.Create(q1, false);
            if (!MessageQueue.Exists(q2)) MessageQueue.Create(q2, true);
            if (!MessageQueue.Exists(q3)) MessageQueue.Create(q3, true);

            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var host in hosts)
            {
                if (host != null)
                {
                    if (host.State == CommunicationState.Opened) host.Close();
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            ChangeState(btnStart, false, btnStop, true);
            hosts[0] = new ServiceHost(typeof(WcfService.AirportService));
            hosts[1] = new ServiceHost(typeof(WcfService.AirportServiceVolatile));
            hosts[2] = new ServiceHost(typeof(WcfService.AirportServiceTwoWay));
            hosts[3] = new ServiceHost(typeof(WcfService.AirportServiceDLQ));
            foreach (var host in hosts)
            {
                host.Open();
            }
            AddInfo("服务已启动，监听的Uri为：");
            foreach (var host in hosts)
            {
                foreach (var v in host.Description.Endpoints)
                {
                    AddInfo(v.ListenUri.ToString());
                }
            }
            AddInfo("---------");
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            foreach (var host in hosts)
            {
                host.Close();
            }
            AddInfo("服务已关闭");
            ChangeState(btnStart, true, btnStop, false);
        }

        private static void ChangeState(Button btnStart, bool isStart, Button btnStop, bool isStop)
        {
            btnStart.IsEnabled = isStart;
            btnStop.IsEnabled = isStop;
        }

        public static void AddInfo(string format,params object[] args)
        {
            TextBlockInfo.Text += string.Format(format, args) + "\n";
        }
    }
}
