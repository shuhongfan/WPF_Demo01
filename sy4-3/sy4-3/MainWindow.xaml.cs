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

namespace sy4_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;
        public MainWindow()
        {
            InitializeComponent();
            path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File1.txt");
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path,"Hello,你好！\r\n",Encoding.UTF8);
            }

            textBlock1.Text = "";
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] bytes = new byte[1024];
                int num = fs.Read(bytes, 0, bytes.Length);
                while (num>0)
                {
                    textBlock1.Text += Encoding.UTF8.GetString(bytes, 0, num);
                    num = fs.Read(bytes, 0, bytes.Length);
                }
            }
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            string str = "写入新的内容!\r\n";
            using (FileStream fs = File.OpenWrite(path))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Position = fs.Length;
                fs.Write(bytes,0,bytes.Length);
            }

            textBlock1.Text = "写入完毕";
        }
    }
}
