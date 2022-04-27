using System;
using System.Collections.Generic;
using System.IO;
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

namespace sy4_4
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

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string str = "abcd、中国";
            textBlock1.Text = string.Format("写入的数据：{0}\n", str);
            byte[] data = Encoding.UTF8.GetBytes(str);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                byte[] bytes = new byte[data.Length];
                ms.Position = 0;
                int n = ms.Read(bytes, 0, bytes.Length);
                string s = Encoding.UTF8.GetString(bytes, 0, n);
                textBlock1.Text += string.Format("读出的数据：{0}\n", s);
            }
        }
    }
}
