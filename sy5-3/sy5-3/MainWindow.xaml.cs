using System;
using System.Collections.Generic;
using System.Linq;
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

namespace sy5_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Timers.Timer timer1;
        private System.Windows.Threading.DispatcherTimer timer2;
        private System.Threading.Timer timer3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // 第一种创建Timer对象的方法
            timer1 = new System.Timers.Timer(500);
            timer1.AutoReset = true;
            timer1.Elapsed += (obj, args) =>
            {
                textBlock1.Dispatcher.InvokeAsync(() => textBlock1.Text += " * ");
            };
            textBlock1.Text = "timer1: ";
            timer1.Start();


            // 第二种创建Timer对象的方法
            timer2 = new System.Windows.Threading.DispatcherTimer();
            timer2.Interval = TimeSpan.FromMilliseconds(500);
            timer2.Tick += (obj, args) =>
            {
                textBlock2.Text += " * ";
            };
            textBlock2.Text = "timer2:";
            timer2.Start();

            var callback = new System.Threading.TimerCallback((obj) =>
            {
                textBlock3.Dispatcher.InvokeAsync(() => textBlock3.Text += " * ");
            });
            TimeSpan delayTime = new TimeSpan(0, 0, 0);
            TimeSpan intervalTime = new TimeSpan(0, 0, 0, 0, 500);
            textBlock3.Text = "timer3:";
            timer3 = new System.Threading.Timer(callback, null, delayTime, intervalTime);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Dispose();
            textBlock1.Text += "已停止";
            textBlock2.Text += "已停止";
            textBlock3.Text += "已停止";
        }
    }
}
