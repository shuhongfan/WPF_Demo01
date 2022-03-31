using System;
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

namespace Experiment3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            long[] t1 = await Task.Run(() => Multiply(200, 18, 27));
            textBlock1.Text = string.Format("测试1（矩阵1：200×18，矩阵2：18×27），用时：{1}毫秒", t1[0], t1[1]);

            long[] t2 = await Task.Run(() => Multiply(2000, 180, 270));
            textBlock1.Text += string.Format("\n测试2（矩阵1：2000×180，矩阵2：180×270），用时：{1}毫秒", t2[0], t2[1]);

            long[] t3 = await Task.Run(() => Multiply(3000, 200, 300));
            textBlock1.Text += string.Format("\n测试3（矩阵1：2000×200，矩阵2：200×300），用时：{1}毫秒", t3[0], t3[1]);
        }

        private long[] Multiply(int rowCount, int colCount, int colCount2)
        {
            long[] timeElapsed = new long[2];
            double[,] m1 = InitializeMatrix(rowCount, colCount);
            double[,] m2 = InitializeMatrix(colCount, colCount2);
            double[,] result = new double[rowCount, colCount2];

            // 并行
            stopwatch.Restart();
            result = new double[rowCount, colCount2];
            MultiplyMatricesParallel(m1, m2, result);
            stopwatch.Stop();
            timeElapsed[1] = stopwatch.ElapsedMilliseconds;
            return timeElapsed;
        }

        #region 并行
        /// <summary>
        /// 计算两个矩阵的乘积
        /// </summary>
        /// <param name="a">矩阵a</param>
        /// <param name="b">矩阵b</param>
        /// <param name="result">相乘的结果</param>
        public static void MultiplyMatricesParallel(double[,] a, double[,] b, double[,] result)
        {
            //如果是64位机，将int改为Int64可提高性能，但修改后将无法在32位机器上运行
            int aRows = a.GetLength(0);
            int aCols = a.GetLength(1);
            int bCols = b.GetLength(1);

            // 内循环不需要并行
            Action<int> action = i =>
            {
                for (int j = 0; j < bCols; j++)
                {
                    double temp = 0; //用一个临时变量可提高并行效率
                    for (int k = 0; k < aCols; k++)
                    {
                        temp += a[i, k] * b[k, j];
                    }
                    result[i, j] = temp;
                }
            };
            // 外循环并行执行
            Parallel.For(0, aRows, action);
        }
        #endregion

        public static double[,] InitializeMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];

            Random r = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = r.Next(100);
                }
            }
            return matrix;
        }
    }
}
