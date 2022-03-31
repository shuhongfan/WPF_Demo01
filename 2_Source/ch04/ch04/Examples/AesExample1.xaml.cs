using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// AesExample1.xaml 的交互逻辑
    /// </summary>
    public partial class AesExample1 : Page
    {
        public AesExample1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (pwdBox1.Password.Length < 5)
            {
                MessageBox.Show("密码不能低于5位");
                return;
            }
            byte[] key, iv;
            AesHelp.GenKeyIV(pwdBox1.Password, out key, out iv);
            textBlock1.Text = "原始字符串：" + textBox1.Text;
            //加密
            byte[] data1 = AesHelp.EncryptString(textBox1.Text, key, iv);
            string encryptedString = Convert.ToBase64String(data1);
            textBlock1.Text += "\n加密后的字符串：" + encryptedString;
            //解密
            byte[] data2 = Convert.FromBase64String(encryptedString);
            string s = AesHelp.DescrptString(data2, key, iv);
            textBlock1.Text += "\n解密后的字符串：" + s;
        }
    }
}
