using System;
using System.Collections.Concurrent;
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

namespace ch06.Examples
{
    /// <summary>
    /// ParallelForEachExample2.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelForEachExample2 : Page
    {
        public ParallelForEachExample2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "通过分区加快小型循环体的速度示例，结果：\n";
            var source = Enumerable.Range(0, 100).ToArray();
            var rangePartitioner = Partitioner.Create(0, source.Length);
            double[] results = new double[source.Length];
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = source[i] * source[i];
                }
            });
            textBlock1.Text += string.Join("，", results);
        }
    }
}
