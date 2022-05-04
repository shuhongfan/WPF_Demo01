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

namespace sy5_2
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
            Action a1 = () => sb.AppendLine("Action示例1（无输入参数）");
            a1();

            Action<int, int> a2 = (a, b) =>
             {
                 sb.AppendFormat("Action示例2(有2个输入参数)");
                 if (a > b)
                 {
                     sb.AppendLine("结果 a>b");
                 }
                 else if (a == b)
                 {
                     sb.AppendLine("结果：a==b");
                 }
                 else
                 {
                     sb.AppendLine("结果：a<b");
                 }
             };
            a2(3, 5);

            Func<bool> f1 = () => 3 <= 5;
            sb.AppendFormat("Func示例1(无序输入参数，返回类型bool)，结果为：{0}", f1());

            Func<int, bool> f2 = n =>
             {
                 return n < 5;
             };
            sb.AppendFormat("Func示例2（有一个输入参数，返回类型bool）结果为：{0}", f2(3));

            Func<string, bool, string> f3 = (s, b) =>
              {
                  if (b == false)
                  {
                      return s.ToLower();
                  }
                  else
                  {
                      return s.ToUpper();
                  }
              };
            sb.AppendFormat("示例3（有2个输入参数，返回类型string）,结果为：{1}\n",f3("This is a Book",true),f3("This is a Book",false));


            string[] words = { "orange","apple","Article" };
            var q = words.Select(a=>a.ToUpper());
            sb.AppendFormat("Func示例4（有1个输入参数，返回类型为string）,结果为：{0}",string.Join(",",q));
            textBlock1.Text = sb.ToString();
        }
    }
}
