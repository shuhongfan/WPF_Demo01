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
    /// InvokeAsyncExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class InvokeAsyncExamplePage : Page
    {
        private volatile bool isStop;
        public InvokeAsyncExamplePage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            isStop = false;
            textBlock1.Text = "";
            this.Dispatcher.InvokeAsync(() => MyMethodAsync("a"));
            this.Dispatcher.InvokeAsync(() => MyMethodAsync("b"));
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            isStop = true;
        }

        private async Task MyMethodAsync(string s)
        {
            while (isStop == false)
            {
                textBlock1.Text += s;
                await Task.Delay(100);
            }
            textBlock1.Text += "\n任务" + s + "已终止";
        }
    }
}
