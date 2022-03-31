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

namespace ch06.Examples
{
    /// <summary>
    /// ParallelInvokeExample2.xaml 的交互逻辑
    /// </summary>
    public partial class ParallelInvokeExample2 : Page
    {
        private Random r = new Random();

        public ParallelInvokeExample2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();

            ParallelOptions options = new ParallelOptions();
            options.TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Parallel.Invoke(options, () => LoadImage(i, j));
                }
            }
        }

        private void LoadImage(int row, int col)
        {
            Image image = new Image();
            image.Source = ((Image)this.Resources["a" + r.Next(6)]).Source;
            image.Stretch = Stretch.Fill;
            Grid.SetRow(image, row);
            Grid.SetColumn(image, col);
            grid1.Children.Add(image);
        }
    }
}

