using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// ParallelForExample4.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForExample4 : Page
    {
        public ParallelForExample4()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Stopwatch sw = new Stopwatch();
            textBlock1.Text = "";
            int n = int.Parse(textBox1.Text);
            AddInfo("此例子模拟每次循环至少用时100ms以上，请多次单击按钮观察\n");
            await ParaSumAsync(n);
            await SumAsync(n);
            this.Cursor = Cursors.Arrow;
        }

        private async Task ParaSumAsync(int n)
        {
            await Task.Delay(0);
            int total = 0;
            var cb = new ConcurrentBag<Data>();
            Func<int> subInit = () => 0;
            Func<int, ParallelLoopState, int, int> body = (i, loopState, subTotal) =>
            {
                Data data = new Data() { Name = "A" + i.ToString(), Number = i };
                cb.Add(data);
                subTotal += data.Number;
                //模拟每次循环至少用时100毫秒以上，因为该例子每次用时很少，体现不出并行的优势
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                return subTotal;
            };
            Action<int> action = (subTotal) =>
            {
                //由于total是线程全局变量，因此需要通过原子操作解决资源争用问题
                total = Interlocked.Add(ref total, subTotal);
            };
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, n, subInit, body, action);
            sw.Stop();
            string s = "";
            foreach (var v in cb)
            {
                s += v.Number.ToString() + "，";
            }
            AddInfo("每个对象中的数：{0}", s.TrimEnd('，'));
            AddInfo("并行用时：{0}ms，\t结果：{1}", sw.ElapsedMilliseconds, total);
        }

        private async Task SumAsync(int n)
        {
            int sum = 0;
            string s = "";
            var list = new List<Data>();
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
            {
                Data data = new Data() { Name = "A" + i.ToString(), Number = i };
                list.Add(data);
                sum += list[i].Number;
                s += list[i].Number.ToString() + "，";
                //模拟每次循环至少用时100毫秒以上
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            sw.Stop();
            AddInfo("每个对象中的数：{0}", s.TrimEnd('，'));
            AddInfo("非并行用时：{0}ms，\t结果：{1}", sw.ElapsedMilliseconds, sum);
        }

        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Text += string.Format(format, args) + "\n";
        }
    }
}
