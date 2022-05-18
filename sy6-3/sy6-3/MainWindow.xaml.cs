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

namespace sy6_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            int n = int.Parse(textBox1.Text);
            Stopwatch sw = new Stopwatch();
            textBlock1.Text += "向集合中添加 " + n + " 个对象" + "\n";

            Action<int> action1 = NewAction();
            sw.Restart();
            Parallel.For(0, n, action1);
            sw.Stop();
            textBlock1.Text += "使用默认的并行选项，用时：" + sw.ElapsedMilliseconds + " ms\n";

            Action<int> action2 = NewAction();
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 4 * Environment.ProcessorCount;
            sw.Restart();
            Parallel.For(0, n, action2);
            sw.Stop();
            textBlock1.Text += "自定义并行选项，用时：" + sw.ElapsedMilliseconds + " ms，最大并行度：" + options.MaxDegreeOfParallelism + "\n";

            List<Data> datas = new List<Data>();
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                datas.Add(new Data()
                {
                    Name = "A"+i.ToString(),
                    Number = i
                });
            }
            sw.Stop();
            textBlock1.Text += "非并行用时：" + sw.ElapsedMilliseconds + " ms";
        }

        private Action<int> NewAction()
        {
            ConcurrentBag<Data> cb = new ConcurrentBag<Data>();
            Action<int> action = (i) =>
            {
                cb.Add(new Data()
                    {
                        Name = "A" + i.ToString(), 
                        Number = i
                    }
                );
            };
            return action;
        }
    }
}
