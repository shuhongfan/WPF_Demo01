using System.Windows;
using System.Windows.Controls;

namespace Experiment1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>将要绘制的视频</summary>
        public static string VideoFileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButtonVideo_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            VideoFileName = rb.Content.ToString();
        }
    }
}
