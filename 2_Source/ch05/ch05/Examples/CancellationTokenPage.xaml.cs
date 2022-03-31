using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// CancellationTokenPage.xaml 的交互逻辑
    /// </summary>
    public partial class CancellationTokenPage : Page
    {
        private CancellationTokenSource cts;

        public CancellationTokenPage()
        {
            InitializeComponent();
            MyHelps.ChangeState(btnStart, true, btnCancel, false);
        }

        private async Task MyMethodAsync(string s, CancellationToken ct)
        {
            ProgressBar p = new ProgressBar
            {
                Height = 10,
                Margin = new Thickness(2),
                Background = Brushes.AliceBlue
            };
            progressStackPanel.Children.Add(p);
            for (int i = 0; i < 50; i++)
            {
                if (ct.IsCancellationRequested) break;
                p.Value += 2;
                textBlock1.Text += s;
                await Task.Delay(100);
            }
            ct.ThrowIfCancellationRequested();
            textBlock1.Text += string.Format("\n 任务{0}完成", s);
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            progressStackPanel.Children.Clear();
            textBlock1.Text = "";
            cts = new CancellationTokenSource();
            MyHelps.ChangeState(btnStart, false, btnCancel, true);

            var a = MyMethodAsync("a", cts.Token);
            var b = MyMethodAsync("b", cts.Token);
            try
            {
                await a;
                await b;
            }
            catch
            {
                if (a.IsCanceled) textBlock1.Text += "\n任务a已取消";
                if (b.IsCanceled) textBlock1.Text += "\n任务b已取消";
            }
            cts = null;
            MyHelps.ChangeState(btnStart, true, btnCancel, false);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
