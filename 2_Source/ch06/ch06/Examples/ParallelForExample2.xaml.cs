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
    /// ParallelForExample2.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForExample2 : Page
    {
        public ParallelForExample2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            int n = int.Parse(textBox1.Text);
            Stopwatch sw = new Stopwatch();
            AddInfo("向集合中添加 {0} 个对象（请多次单击按钮观察）", n);
            Action<int> action1 = NewAction();
            sw.Restart();
            Parallel.For(0, n, action1);
            sw.Stop();
            AddInfo("使用默认的并行选项，用时：{0}ms ", sw.ElapsedMilliseconds);

            Action<int> action2 = NewAction();
            ParallelOptions option = new ParallelOptions();
            option.MaxDegreeOfParallelism = 4 * Environment.ProcessorCount;
            sw.Restart();
            Parallel.For(0, n, option, action2);
            sw.Stop();
            AddInfo("自定义并行选项，用时：{0}ms，最大并行度：{1} ",
                sw.ElapsedMilliseconds,option.MaxDegreeOfParallelism);

            List<Data> data = new List<Data>();
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                data.Add(new Data() { Name = "A" + i.ToString(), Number = i });
            }
            sw.Stop();
            AddInfo("非并行用时：{0}ms ", sw.ElapsedMilliseconds);
        }
        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Text += string.Format(format, args) + "\n";
        }
        private Action<int> NewAction()
        {
            var cb = new ConcurrentBag<Data>();
            Action<int> action = (i) =>
            {
                cb.Add(new Data() { Name = "A" + i.ToString(), Number = i });
            };
            return action;
        }
    }
}
