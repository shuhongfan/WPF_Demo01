using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace sy3._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int fileIndex = 1;
        private string fileName = "Notepad";
        List<Data> list = new List<Data>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string argument = Environment.CurrentDirectory + "\\myfile" + (fileIndex++) + ".txt";
            if (File.Exists(argument) == false)
            {
                File.CreateText(argument);
            }
            Process p = new Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = argument;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForInputIdle();
            RefreshProcessInfo();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Process[] myprocesses;
            myprocesses = Process.GetProcessesByName(fileName);
            foreach (Process p in myprocesses)
            {
                using (p)
                {
                    p.CloseMainWindow();
                    Thread.Sleep(1000);
                    p.WaitForExit();
                }
            }
            fileIndex = 0;
            RefreshProcessInfo();
            this.Cursor = Cursors.Arrow;
        }

        private void RefreshProcessInfo()
        {
            dataGrid1.ItemsSource = null;
            list.Clear();
            Process[] processes = Process.GetProcessesByName(fileName);
            foreach (Process p in processes)
            {
                list.Add(new Data()
                {
                    Id = p.Id,
                    ProcessName = p.ProcessName,
                    TotalMemory = string.Format("{0,10:0} KB", p.WorkingSet64 / 1024d),
                    StartTime = p.StartTime.ToString("yyyy-M-d HH:mm:ss"),
                    FileName = p.MainModule.FileName
                });
            }
            dataGrid1.ItemsSource = list;
        }

    }
}
