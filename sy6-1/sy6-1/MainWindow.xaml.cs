using System;
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

namespace sy6_1
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
            int n = 20;
            int[] a = Enumerable.Range(1, n).ToArray();
            int[] b = Enumerable.Range(1, n).ToArray();
            int[] c = new int[n];

            Action<int> action = (i) =>
            {
                c[i] = a[i] + b[i];
                Thread.Sleep(100);
            };

            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, n, action);
            sw.Stop();
            textBlock1.Text += "并行用时：" + sw.ElapsedMilliseconds + "ms,结果：" + string.Join(",", c) + "\n";
            sw.Restart();
            DateTime dt1 = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                c[i] = a[i] + b[i];
                Thread.Sleep(100);
            }

            DateTime dt2 = DateTime.Now;
            sw.Stop();

            textBlock1.Text += "非并行用时：" + sw.ElapsedMilliseconds + "ms,结果：" + string.Join(",", c);
        }
    }
}
