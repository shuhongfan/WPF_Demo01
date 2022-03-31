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

using Client.StudentsOneWayReference;

namespace Client.Examples
{
    /// <summary>
    /// StudentsOneWayPage.xaml 的交互逻辑
    /// </summary>
    public partial class StudentsOneWayPage : Page
    {
        public StudentsOneWayPage()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            StudentsOneWayClient client = new StudentsOneWayClient();
            textBlock1.Text = "原始数据：\n";
            textBlock1.Text += client.GetStudentsValue();
            client.Add(new Student { ID = 13004, Name = "王五", Score = 90 });
            client.Remove(13002);
            client.Remove(13003);
            textBlock1.Text += "删除13002、13003，添加13004后的数据：\n";
            textBlock1.Text += client.GetStudentsValue();
        }
    }
}
