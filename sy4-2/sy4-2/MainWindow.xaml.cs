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
namespace sy4_2
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
            StringBuilder sb = new StringBuilder();
            string s1 = "hello中国";
            Encoding unicode = Encoding.Unicode;
            byte[] bytes = unicode.GetBytes(s1);
            Encoding asc = Encoding.ASCII;
            byte[] b1 = Encoding.Convert(unicode,asc,bytes);
            string s2 = asc.GetString(b1);
            sb.AppendLine("转换后的ASCII字符");
            sb.AppendLine(s2);
            textBlock1.Text = sb.ToString();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            StringBuilder sb = new StringBuilder();
            string s = "ab,12,软件";
            textBlock1.Text = string.Format("被编码的字符串：{0}\n", s);
            EncodeDecode(s, Encoding.ASCII);
            EncodeDecode(s, Encoding.UTF8);
            EncodeDecode(s,Encoding.Unicode);
            EncodeDecode(s,Encoding.GetEncoding("GB2312"));
            EncodeDecode(s, Encoding.GetEncoding("GB18030"));
            textBlock1.Text += sb.ToString();
        }

        StringBuilder sb = new StringBuilder();
        private void EncodeDecode(string s, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(s);
            string str = encoding.GetString(bytes);
            string encodeResult = BitConverter.ToString(bytes);
            sb.AppendFormat("编码为：{0},编码结果为：{1}\n", encoding.EncodingName, encodeResult);
            sb.AppendFormat("解码结果：{0}\n", str);
            textBlock1.Text = sb.ToString();
        }
    }
}
