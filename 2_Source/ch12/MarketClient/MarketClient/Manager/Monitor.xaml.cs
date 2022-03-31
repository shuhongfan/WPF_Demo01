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

namespace MarketClient.Manager
{
    /// <summary>
    /// Monitor.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor : Page
    {
        public Monitor()
        {
            InitializeComponent();
            media1.LoadedBehavior = MediaState.Manual;
            this.Loaded += Monitor_Loaded;
            this.Unloaded += Monitor_Unloaded;
        }

        void Monitor_Unloaded(object sender, RoutedEventArgs e)
        {
            media1.Stop();
            media1.Close();
        }

        void Monitor_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\MyVideoTest.ink";
            ink1.LoadInkFromFile(path);
            ink1.EditingMode = InkCanvasEditingMode.None;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            media1.Source = new Uri(@".\..\..\Manager\Videos\"+btn.Tag.ToString(), UriKind.Relative);
            media1.MediaEnded += media1_MediaEnded;
            media1.Play();
        }

        void media1_MediaEnded(object sender, RoutedEventArgs e)
        {
            media1.Position = TimeSpan.Zero;
        }
    }
}
