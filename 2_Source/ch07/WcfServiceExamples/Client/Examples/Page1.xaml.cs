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

using Client.ServiceReference1;

namespace Client.Examples
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "客户端调用服务端的SayHello方法，服务端返回：\n";

            //创建服务客户端
            Service1Client client = new Service1Client();
            //调用服务
            string s = client.SayHello("欢迎学习WCF！");
            //关闭服务客户端并清理资源
            client.Close();

            textBlock1.Text += s;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text += "\n\n客户端调用服务端的多个方法，服务端返回：";

            Service1Client client = new Service1Client();

            string s = client.SayHello(client.Endpoint.Address.ToString());
            double r1 = client.Add(10, 20);
            double r2 = client.Divide(10, 20);

            client.Close();

            textBlock1.Text += string.Format("\n{0}", s);
            textBlock1.Text += string.Format("\n10 + 20 = {0}", r1);
            textBlock1.Text += string.Format("\n10 / 20 = {0}", r2);
        }
    }
}
