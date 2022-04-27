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

namespace sy4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
            {
                Encoding en = encodingInfo.GetEncoding();
                sb.AppendLine("字符名称：" + encodingInfo.Name + "  字符描述：" + en.EncodingName);
            }

            TextBox1.Text = sb.ToString();
        }
    }
}
