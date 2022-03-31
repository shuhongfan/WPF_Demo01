using System;
using System.Collections.Concurrent;
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
    /// ParallelForExample3.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForExample3 : Page
    {
        public ParallelForExample3()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            int n = int.Parse(textBox1.Text);
            AddInfo("向集合中添加 {0} 个对象（请多次单击按钮观察）\n", n);
            var cb = new ConcurrentBag<Data>();
            Action<int, ParallelLoopState> action = (i, loopState) =>
            {
                Data data = new Data() { Name = "A" + i.ToString(), Number = i };
                cb.Add(data);
                AddInfo("{0}\t", i);
                if (i == 10) loopState.Break();
            };
            try
            {
                var result = Parallel.For(0, n, action);
                AddInfo("\n任务是否全部正常完成：{0}", result.IsCompleted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Dispatcher.InvokeAsync(() =>
            {
                textBlock1.Text += string.Format(format, args);
            });
        }
    }
}
