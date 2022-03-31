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

namespace ch04.Examples
{
    /// <summary>
    /// EncodingPage.xaml 的交互逻辑
    /// </summary>
    public partial class EncodingExample : Page
    {
        StringBuilder sb;
        public EncodingExample()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            sb = new StringBuilder();
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding en = ei.GetEncoding();
                sb.AppendFormat("编码名称：{0}，说明：{1}\n", ei.Name, en.EncodingName);
            }
            textBlock1.Text = sb.ToString();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            sb = new StringBuilder();
            string s = "ab,12,软件";
            textBlock1.Text = string.Format("被编码的字符串：{0}\n", s);
            EncodeDecode(s, Encoding.ASCII);
            EncodeDecode(s, Encoding.UTF8);
            EncodeDecode(s, Encoding.Unicode);
            EncodeDecode(s, Encoding.GetEncoding("GB2312"));
            EncodeDecode(s, Encoding.GetEncoding("GB18030"));
            textBlock1.Text += sb.ToString();
        }

        private void EncodeDecode(string s, Encoding encoding)
        {
            //将字符串编码为字节数组
            byte[] bytes = encoding.GetBytes(s);
            //将字节数组解码为字符串
            string str = encoding.GetString(bytes);
            //显示结果
            string encodeResult = BitConverter.ToString(bytes);
            sb.AppendFormat("编码为：{0}，编码结果：{1}\n", encoding.EncodingName, encodeResult);
            sb.AppendFormat("解码结果：{0}\n", str);
        }
    }
}
