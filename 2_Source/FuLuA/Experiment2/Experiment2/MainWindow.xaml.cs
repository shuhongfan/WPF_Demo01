using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Experiment2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            labelError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s1 = txt1.Text + txtStart.Text;
            IPAddress ip1, ip2;
            if (IPAddress.TryParse(s1, out ip1) == false || IPAddress.TryParse(s1, out ip2) == false)
            {
                labelError.Visibility = System.Windows.Visibility.Visible;
                btn1.IsEnabled = false;
                return;
            }
            else
            {
                btn1.IsEnabled = true;
                labelError.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int start = int.Parse(txtStart.Text);
            int end = int.Parse(txtEnd.Text);
            if (start > end)
            {
                MessageBox.Show("终止值必须大于等于起始值", "地址范围有错");
                return;
            }
            listBoxInfo.Items.Clear();
            for (int i = start; i <= end; i++)
            {
                IPAddress ip = IPAddress.Parse(txt1.Text + i);
                Task.Run(() => Scan(ip));
            }
        }

        private void Scan(IPAddress ip)
        {
            Stopwatch w = Stopwatch.StartNew();
            string host = "";
            try
            {
                host = Dns.GetHostEntry(ip).HostName;
            }
            catch
            {
                host = "（不在线）";
            }
            w.Stop();
            listBoxInfo.Dispatcher.Invoke(
                () => listBoxInfo.Items.Add(
                      string.Format("扫描地址：{0}，扫描用时：{1}毫秒，主机DNS名称：{2}",
                      ip, w.ElapsedMilliseconds, host)
             ));
        }
    }
}
