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

namespace ch03
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button oldButton = new Button();
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            oldButton.Foreground = Brushes.Black;
            btn.Foreground = Brushes.Red;
            oldButton = btn;
            if (btn.Tag.ToString() == "例4")
            {
                var c = AppDomain.CurrentDomain;
                string exePath = c.BaseDirectory + @"\InkCanvasExample.exe";
                string typeName = "InkCanvasExample.MainWindow";
                var mainWindow = (Window)c.CreateInstanceFromAndUnwrap(exePath, typeName);
                frame1.Content = mainWindow.Content;
            }
            else
            {
                frame1.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
            }
        }
    }
}
