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

namespace MarketClient.Customer
{
    /// <summary>
    /// MarketGuide.xaml 的交互逻辑
    /// </summary>
    public partial class MarketGuide : Page
    {
        public MarketGuide()
        {
            InitializeComponent();
            this.Loaded += MarketGuide_Loaded;
        }

        void MarketGuide_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\MyTest.ink";
            ink1.LoadInkFromFile(path);
            ink1.EditingMode = InkCanvasEditingMode.None;
        }
    }
}
