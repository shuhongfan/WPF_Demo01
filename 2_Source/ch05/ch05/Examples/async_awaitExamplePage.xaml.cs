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
    /// async_awaitExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class async_awaitExamplePage : Page
    {
        private bool isStop;
        private MyTasks t = new MyTasks();

        public async_awaitExamplePage()
        {
            InitializeComponent();
            MyHelps.ChangeState(btnStart, true, btnStop, false);
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MyHelps.ChangeState(btnStart, false, btnStop, true);
            textBlock1.Text = "开始执行任务......";
            isStop = false;
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

            st.Start();
            await t.Method1Async();
            long x = st.ElapsedMilliseconds;
            textBlock1.Text = "任务1执行完毕,用时：" + x + "毫秒";
            st.Restart();
            var sum = await t.Method2Async();
            x = st.ElapsedMilliseconds;
            textBlock1.Text += string.Format("\n任务2：结果{0}，用时{1}毫秒", sum, x);
            st.Restart();
            var a1 = await t.Method3Async(39, 8);
            x = st.ElapsedMilliseconds;
            textBlock1.Text += string.Format("\n任务3：商{0}，余数{1}，用时{2}毫秒", a1.Item1, a1.Item2, x);
            st.Restart();
            var a2 = await t.GetAverageAsync(20000000);
            x = st.ElapsedMilliseconds;
            textBlock1.Text += "\n产生2千万个随机数并计算其平均值，用时：" + x + "毫秒\n";
            st.Stop();
            while (isStop == false)
            {
                textBlock1.Text += await t.Method1Async("a");
                textBlock1.Text += await t.Method1Async("b");
            }
            textBlock1.Text += "\n任务执行完毕";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MyHelps.ChangeState(btnStart, true, btnStop, false);
            isStop = true;
        }
    }
}
