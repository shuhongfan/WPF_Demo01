using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

using Client.StudentsDuplexReference;

namespace Client.Examples
{
    /// <summary>
    /// StudentDuplexPage.xaml 的交互逻辑
    /// </summary>
    public partial class StudentsDuplexPage : Page, IStudentsDuplexCallback
    {
        private StudentsDuplexClient client;

        public StudentsDuplexPage()
        {
            InitializeComponent();
            this.Unloaded += StudentDuplexPage_Unloaded;
        }

        void StudentDuplexPage_Unloaded(object sender, RoutedEventArgs e)
        {
            client.Close();
        }

        private async void btn1_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            client = new StudentsDuplexClient(context);

            textBlock1.Text += "\n等待接收 ";
            Task login = client.LoginAsync("张三");
            while (login.IsCompleted == false)
            {
                textBlock1.Text += ". ";
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
            await login;
        }

        #region 实现IStudentsDuplexCallback接口
        public void Receive(string info)
        {
            textBlock1.Text += "\n收到服务端发来的信息：" + info;
        }
        #endregion
    }
}
