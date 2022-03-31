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

namespace ch05.Examples
{
    /// <summary>
    /// TaskStatusExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class TaskStatusExamplePage : Page
    {
        CancellationTokenSource cts;

        public TaskStatusExamplePage()
        {
            InitializeComponent();
        }

        private void MyMethod(CancellationTokenSource cts)
        {
            var ct = cts.Token;
            while (ct.IsCancellationRequested == false)
            {
                //模拟长时间运行的工作，实际过程可能会引发其他类型的异常
                System.Threading.Thread.Sleep(100);
            }
            //如果接收到取消通知，引发OperationCanceledException类型的异常
            ct.ThrowIfCancellationRequested();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "开始执行任务......";
            cts = new CancellationTokenSource();
            //第2个参数cts.Token向该任注册发送取消通知，这样才能确保获取的任务状态是正确的
            var t1 = Task.Run(() => MyMethod(cts), cts.Token);
            textBlock1.Text += "\n任务状态（每秒获取1次）：";
            while (t1.IsCompleted == false)
            {
                textBlock1.Text += t1.Status + "--";
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            //由于任务执行过程中可能会出现各种异常，所以实际开发中需要用try-catch等待任务执行
            try { await t1; }
            catch (Exception ex) { textBlock1.Text += "\n" + ex.Message; }
            textBlock1.Text += string.Format(
                "\nStatus：{0}，IsCompleted：{1}，IsFaulted：{2}，IsCanceled：{3}",
                t1.Status, t1.IsCompleted, t1.IsFaulted, t1.IsCanceled);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
