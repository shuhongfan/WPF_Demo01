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

namespace ch05.Examples
{
    /// <summary>
    /// LambdaExample.xaml 的交互逻辑
    /// </summary>
    public partial class LambdaExample : Page
    {
        public LambdaExample()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            //查询泛型列表（找出小于4的数）
            List<int> n1 = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            sb.AppendFormat("List<int>中的数：{0}", string.Join(", ", n1.ToArray()));
            var q1 = n1.Where(i => i < 4);
            sb.AppendFormat("\n小于4的数：{0}", string.Join(", ", q1.ToArray()));
            sb.AppendLine("\n");

            //查询数组（找出每个数字对应的英文单词）
            int[] n2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            sb.AppendFormat("数字序列：{0}", string.Join(", ", n2));
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var q2 = n2.Select((n) => strings[n]);
            sb.AppendFormat("\n数字对应的单词：{0}", string.Join(", ", q2.ToArray()));

            //查询数组（找出所有偶数）
            var q3 = n2.Where((n) => n % 2 == 0);
            sb.AppendFormat("\n偶数：{0}", string.Join(", ", q3.ToArray()));
            sb.AppendLine();

            textBlock1.Text = sb.ToString();
        }
    }
}
