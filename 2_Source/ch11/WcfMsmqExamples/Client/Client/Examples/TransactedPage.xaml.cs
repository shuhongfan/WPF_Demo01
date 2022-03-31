using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.AirportServiceReference;
namespace Client.Examples
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class TransactedPage : Page
    {
        AirportServiceClient client;
        public TransactedPage()
        {
            InitializeComponent();
            client = new AirportServiceClient();
            this.Unloaded += StandedPage_Unloaded;
        }

        void StandedPage_Unloaded(object sender, RoutedEventArgs e)
        {
            client.Close();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            client.SubmitInfo("例1"); //不通过事务发送（仅为演示，不建议这样用）
            AirportMessage m = new AirportMessage();
            m.AirportId = "0001";
            m.ForecastTime = DateTime.Now;
            m.ShortMessage = "0356 1157Z";
            //通过事务发送（建议的用法），如果出错，该事务范围内的所有操作都会回滚
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                client.SubmitAirportMessage(m); // 向队列提交报文
                scope.Complete(); // 完成事务
            }
            textBlock1.Text += "已发送。\n";
        }
    }
}
