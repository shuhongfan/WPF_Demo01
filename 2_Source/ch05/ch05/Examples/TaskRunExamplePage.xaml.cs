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

namespace ch05.Examples
{
    /// <summary>
    /// TaskRunExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class TaskRunExamplePage : Page
    {
        private System.Threading.CancellationTokenSource cts;
        private MyTasks t = new MyTasks();

        public TaskRunExamplePage()
        {
            InitializeComponent();
            MyHelps.ChangeState(btnStart, true, btnStop, false);
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MyHelps.ChangeState(btnStart, false, btnStop, true);
            cts = new System.Threading.CancellationTokenSource();
            textBlock1.Text = "开始执行任务......";

            try
            {
                await Task.Run(() => t.Method1(), cts.Token);
                textBlock1.Text += "\n任务1执行完毕";
                var sum = await Task.Run(() => t.Method2(), cts.Token);
                textBlock1.Text += "\n任务2（计算1到1000的和）结果为：" + sum;
                var a = await Task.Run(() => t.Method3(39, 8), cts.Token);
                textBlock1.Text += string.Format("\n任务3（求39除以8的商和余数）结果为：{0},{1}\n", a.Item1, a.Item2);

                while (true)
                {
                    textBlock1.Text += await Task.Run(() => t.Method1("a"), cts.Token);
                    textBlock1.Text += await Task.Run(() => t.Method1("b"), cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                textBlock1.Text += "\n任务被取消";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            MyHelps.ChangeState(btnStart, true, btnStop, false);
        }
    }
}
