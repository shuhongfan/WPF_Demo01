using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ch06.Examples
{
    /// <summary>
    /// ParallelForDemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForExample1 : Page
    {
        public ParallelForExample1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            int n = 20;
            AddInfo("计算两个数组的和。");
            int[] a = Enumerable.Range(1, n).ToArray();
            int[] b = Enumerable.Range(1, n).ToArray();
            int[] c = new int[n];
            Action<int> action = (i) =>
            {
                c[i] = a[i] + b[i];
            };
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, n, action);
            sw.Stop();
            AddInfo("并行用时：{0}ms，结果：{1}", sw.ElapsedMilliseconds, string.Join(",", c));
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                c[i] = a[i] + b[i];
            }
            sw.Stop();
            AddInfo("非并行用时：{0}ms，结果：{1}", sw.ElapsedMilliseconds, string.Join(",", c));
        }
        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Text += string.Format(format, args) + "\n";
        }
    }
}
