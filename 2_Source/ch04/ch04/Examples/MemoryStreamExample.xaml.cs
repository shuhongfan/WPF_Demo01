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

namespace ch04.Examples
{
    /// <summary>
    /// MemoryStreamExample.xaml 的交互逻辑
    /// </summary>
    public partial class MemoryStreamExample : Page
    {
        public MemoryStreamExample()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string str = "abcd、中国";
            textBlock1.Text = string.Format("写入的数据：{0}\n", str);
            Byte[] data = Encoding.UTF8.GetBytes(str);
            using (MemoryStream ms = new MemoryStream())
            {
                //将字节数组写入内存流
                ms.Write(data, 0, data.Length);
                //将当前内存流中的数据读取到字节数组中
                byte[] bytes = new byte[data.Length];
                ms.Position = 0;  //设置开始读取的位置
                int n = ms.Read(bytes, 0, bytes.Length);//读到bytes中
                string s = Encoding.UTF8.GetString(bytes, 0, n);
                textBlock1.Text += string.Format("读出的数据：{0}\n", s);
            }
        }
    }
}
