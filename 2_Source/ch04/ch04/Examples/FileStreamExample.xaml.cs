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
    /// FileStreamPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileStreamExample : Page
    {
        private string path;
        public FileStreamExample()
        {
            InitializeComponent();
            path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File1.txt");
        }
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "Hello，你好！\r\n", Encoding.UTF8);
            }
            ReadFromFile(path);
        }
        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            AppendToFile(path, "HelloWorld!\r\n");
        }
        private void ReadFromFile(string path)
        {
            textBlock1.Text = "";
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] bytes = new byte[1024];   //每次读取的缓存大小
                int num = fs.Read(bytes, 0, bytes.Length);
                while (num>0)
                {
                    textBlock1.Text += Encoding.UTF8.GetString(bytes, 0, num);
                    num = fs.Read(bytes, 0, bytes.Length);
                }
            }
        }
        private void AppendToFile(string path, string str)
        {
            using (FileStream fs = File.OpenWrite(path))
            {
                Byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Position = fs.Length;         //设置写入位置
                fs.Write(bytes, 0, bytes.Length); //写入
            }
            textBlock1.Text = "写入完毕。";
        }
    }
}
