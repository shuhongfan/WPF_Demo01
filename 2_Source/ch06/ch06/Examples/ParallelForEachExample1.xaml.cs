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
    /// ParallelForEachExample1.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForEachExample1 : Page
    {
        public ParallelForEachExample1()
        {
            InitializeComponent();
        }
        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            AddInfo("计算数组中每个元素的平方值（模拟每次循环至少用时100ms以上）");
            int n = 10;
            int[] a = Enumerable.Range(1, n).ToArray();
            AddInfo("原始值：{0}", string.Join("，", a));
            await ParaGetNumAsync(n, a);
            await GetNumAsync(n, a);
        }

        private async Task ParaGetNumAsync(int n, int[] a)
        {
            await Task.Delay(0);
            ConcurrentBag<double> cb = new ConcurrentBag<double>();
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.ForEach(a, (v) =>
            {
                cb.Add(v * v);
                //模拟每次循环至少用时100ms以上，因为该例子每次用时很少，体现不出并行的优势
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            });
            sw.Stop();
            double[] b = cb.ToArray();
            Array.Sort(b);
            AddInfo("并行用时：{0}ms，\t结果：{1}", sw.ElapsedMilliseconds, string.Join("，", b));
        }

        private async Task GetNumAsync(int n, int[] a)
        {
            List<double> list = new List<double>();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var v in a)
            {
                list.Add(v * v);
                //模拟每次循环至少用时100ms以上
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            sw.Stop();
            AddInfo("非并行用时：{0}ms，\t结果：{1}", sw.ElapsedMilliseconds,
                string.Join("，", list.ToArray()));
        }

        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Text += string.Format(format, args) + "\n";
        }
    }
}
