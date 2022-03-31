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

namespace DeadLetterQueueService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TextBlock TextBlockInfo { get; set; }
        private ServiceHost host;
        public MainWindow()
        {
            InitializeComponent();
            TextBlockInfo = textBlock1;
            string queueName = ConfigurationManager.AppSettings["deadLetterQueueName"];
            if (!MessageQueue.Exists(queueName)) MessageQueue.Create(queueName, true);

            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (host != null)
            {
                if (host.State == CommunicationState.Opened) host.Close();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            ChangeState(btnStart, false, btnStop, true);
            host = new ServiceHost(typeof(AirportMesssageDLQService));
            host.Open();
            AddInfo("死信队列服务已启动，监听的Uri为：");
            foreach (var v in host.Description.Endpoints)
            {
                AddInfo(v.ListenUri.ToString());
            }
            AddInfo("---------");
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            host.Close();
            AddInfo("死信队列服务已关闭");
            ChangeState(btnStart, true, btnStop, false);
        }

        private static void ChangeState(Button btnStart, bool isStart, Button btnStop, bool isStop)
        {
            btnStart.IsEnabled = isStart;
            btnStop.IsEnabled = isStop;
        }

        public static void AddInfo(string format, params object[] args)
        {
            TextBlockInfo.Text += string.Format(format, args) + "\n";
        }
    }
}
