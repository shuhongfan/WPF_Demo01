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

using Client.StudentsServiceReference;

namespace Client.Examples
{
    /// <summary>
    /// StudentDataContractPage.xaml 的交互逻辑
    /// </summary>
    public partial class StudentsServicePage : Page
    {
        public StudentsServicePage()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            StudentsServiceClient client = new StudentsServiceClient();
            textBlock1.Text += "客户端调用服务端的Hello方法，服务端返回：\n";
            textBlock1.Text += client.Hello("张三");
            textBlock1.Text += "\n客户端调用服务端的GetStudentsValue方法，服务端返回：\n";
            textBlock1.Text += client.GetStudentsValue();

            client.Close();
        }
    }
}
