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

namespace sy5_1
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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            // 查询泛型列表
            List<int> n1 = new List<int>{5,4,1,3,9,8,6,7,2,0};
            sb.AppendFormat("List<int>中的数：{0}", string.Join(",", n1.ToArray()));
            var q1 = n1.Where(i => i < 4);
            sb.AppendFormat("小于4的数：{0}", string.Join(",", q1));
            sb.AppendLine("\n");

            // 查询数组
            int[] n2 = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
            sb.AppendFormat("数字序列:{0}", string.Join(",", n2));
            string[] strings = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
            var q2 = n2.Select(n => strings[n]);
            sb.AppendFormat("数字对应的单词：{0}", string.Join(",", q2));

            // 查询数组
            var q3 = n2.Where(n => n % 2 == 0);
            sb.AppendFormat("偶数：{0}", string.Join(",", q3));
            sb.AppendLine();
            textBlock1.Text = sb.ToString();
        }
    }
}
