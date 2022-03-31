using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ch03.Examples
{
    public partial class StartStopProcess : Page
    {
        int fileIndex = 1;
        string fileName = "Notepad";
        List<Data> list = new List<Data>();
        public StartStopProcess()
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

    public class Data
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string TotalMemory { get; set; }
        public string StartTime { get; set; }
        public string FileName { get; set; }
    }
}
