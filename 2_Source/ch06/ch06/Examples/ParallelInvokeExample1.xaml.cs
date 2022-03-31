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
    /// ParallelInvokeExample1.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelInvokeExample1 : Page
    {
        CancellationTokenSource cts;
        public ParallelInvokeExample1()
        {
            InitializeComponent();
            btnHelps.ChangeState(btnStart, true, btnStop, false);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            btnHelps.ChangeState(btnStart, false, btnStop, true);
            Action a1 = () => MyMethod("a");
            Action a2 = () => MyMethod("b");
            Action a3 = () => MyMethod("c");
            ParallelOptions options = new ParallelOptions();
            options.TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            cts = new CancellationTokenSource();
            options.CancellationToken = cts.Token;
            Parallel.Invoke(options, a1, a1, a2, a2, a2, a3);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            textBlock1.Text += "\n任务被取消";
            btnHelps.ChangeState(btnStart, true, btnStop, false);
        }

        private async void MyMethod(string s)
        {
            while (cts.IsCancellationRequested == false)
            {
                textBlock1.Text += s;
                await Task.Delay(100);
            }
        }
    }
}
