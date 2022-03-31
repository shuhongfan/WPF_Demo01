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
using Client.AirportServiceVolatileReference;
namespace Client.Examples
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class VolatilePage : Page
    {
        public VolatilePage()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //由于服务端配置为采用可变队列，所以该例子不能通过事务发送
            AirportServiceVolatileClient client = new AirportServiceVolatileClient();
            client.SubmitInfo("例2");
            Random r = new Random();
            int n = 5000;
            for (int i = 0; i < 10; i++)
            {
                AirportMessage m1 = new AirportMessage()
                {
                    AirportId = "0002",
                    ForecastTime = DateTime.Now,
                    ShortMessage = string.Format("{0:d4} {1:d4}Z", r.Next(n), r.Next(n))
                };
                client.SubmitAirportMessageVolatile(i, m1);
                textBlock1.Text += "第" + i + "个报文已发送。\n";
            }
            client.Close();
        }
    }
}
