using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ch06.Examples
{
    /// <summary>
    /// TaskFactoryExample.xaml 的交互逻辑
    /// </summary>
    public partial class TaskFactoryExample : Page
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public TaskFactoryExample()
        {
            InitializeComponent();
            btnHelps.ChangeState(btnStart, true, btnStop, false);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnHelps.ChangeState(btnStart, false, btnStop, true);
            textBlock1.Text = "";
            cts = new CancellationTokenSource();
            TaskFactory factory = new TaskFactory(
                cts.Token,
                TaskCreationOptions.LongRunning,
                TaskContinuationOptions.PreferFairness,
                TaskScheduler.FromCurrentSynchronizationContext());
            factory.StartNew(() => Method1Async());
            factory.StartNew(() => Method2Async());
        }


        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            btnHelps.ChangeState(btnStart, true, btnStop, false);
        }

        //任务1
        private async void Method1Async()
        {
            //下面的循环体模拟长时间执行的工作
            while (cts.IsCancellationRequested == false)
            {
                textBlock1.Text += "a";
                await Task.Delay(100); //等待100ms
            }
            //任务1全部完成后，提示该线程结束
            textBlock1.Text += Environment.NewLine + "任务Method1Async已终止";
        }

        //任务2
        private async void Method2Async()
        {
            while (cts.IsCancellationRequested == false)
            {
                textBlock1.Text += "b";
                await Task.Delay(100);
            }
            textBlock1.Text += Environment.NewLine + "任务Method2Async已终止";
        }
    }
}
