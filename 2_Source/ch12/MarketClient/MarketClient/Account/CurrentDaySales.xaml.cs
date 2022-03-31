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

namespace MarketClient.Account
{
    /// <summary>
    /// CurrentDaySales.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentDaySales : Page
    {
        public CurrentDaySales()
        {
            InitializeComponent();
            this.Loaded += CurrentDaySales_Loaded;
        }

        void CurrentDaySales_Loaded(object sender, RoutedEventArgs e)
        {
            labelStatus.Content = "操作员：" + MainWindow.UserName;
            MarketServiceReference.MarketServiceClient client = new MarketServiceReference.MarketServiceClient();
            textBlock1.Text = string.Format("当日结算金额合计：{0:0.00} 元", client.GetCurrentDaySale());
            client.Close();
        }
    }
}
