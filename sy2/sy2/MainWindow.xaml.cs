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
using System.Windows.Controls.Ribbon;
using System.Windows.Ink;
using Microsoft.Win32;

namespace sy2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "墨迹文件|*.ink|所有文件|*.*";
            if (ofd.ShowDialog() == false)
            {
                return;
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                StrokeCollection strokes = new StrokeCollection(fs);

            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败：" + ex.StackTrace);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public void MenuSaveTo_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "ink2.ink";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.CurrentDirectory;
            sfd.Filter = "墨迹文件|*.ink|所有文件|*.*";
            sfd.FileName = System.IO.Path.Combine(sfd.InitialDirectory, fileName);
            if (sfd.ShowDialog() == false)
            {
                return;
            }
            fileName = sfd.FileName;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                // this.Strokes.Save(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.StackTrace);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public void MenuShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
