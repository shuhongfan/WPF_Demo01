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
using Weather;

namespace sy7_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Weather.WeatherWebServiceSoapClient w = new Weather.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            string[] s = new string[23];//声明string数组存放返回结果  
            string city = this.textBox1.Text.Trim();//获得文本框录入的查询城市  
            s = w.getWeatherbyCityName(city);
            if (s[8] == "")
            {
                MessageBox.Show("输入错误，请重新输入", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                this.label3.Content = s[1] + " " + s[6];
                textBox2.Text = s[10];
            }

        }
    }
}
