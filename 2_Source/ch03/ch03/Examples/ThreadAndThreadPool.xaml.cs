using System;
using System.Collections.Generic;
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

namespace ch03.Examples
{
    /// <summary>
    /// ThreadAndThreadPool.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadAndThreadPool : Page
    {
        public ThreadAndThreadPool()
        {
            InitializeComponent();
            Helps.ChangeState(btnStart, true, btnStop, false);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Helps.ChangeState(btnStart, false, btnStop, true);
            MyClass.IsStop = false;
            textBlock1.Text = "";
            MyClass c = new MyClass(textBlock1);
            MyData state = new MyData { Message = "a", Info = "\n线程1已终止" };
            Thread thread1 = new Thread(c.MyMethod);
            thread1.IsBackground = true;
            thread1.Start(state);
            state = new MyData { Message = "b", Info = "\n线程2已终止" };
            Thread thread2 = new Thread(c.MyMethod);
            thread2.IsBackground = true;
            thread2.Start(state);
            state = new MyData { Message = "c", Info = "\n线程3已终止" };
            ThreadPool.QueueUserWorkItem(new WaitCallback(c.MyMethod), state);
            state = new MyData { Message = "d", Info = "\n线程4已终止" };
            ThreadPool.QueueUserWorkItem(new WaitCallback(c.MyMethod), state);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MyClass.IsStop = true;
            Helps.ChangeState(btnStart, true, btnStop, false);
        }
    }

    public class MyClass
    {
        public static volatile bool IsStop;
        TextBlock textBlock1;

        public MyClass(TextBlock textBlock1)
        {
            this.textBlock1 = textBlock1;
        }

        public void MyMethod(Object obj)
        {
            MyData state = obj as MyData;
            while (IsStop == false)
            {
                AddMessage(state.Message);
                Thread.Sleep(100);   //当前线程休眠100ms
            }
            AddMessage(state.Info);
        }

        private void AddMessage(string s)
        {
            textBlock1.Dispatcher.Invoke(() =>
            {
                textBlock1.Text += s;
            });
        }
    }

    public class MyData
    {
        public string Info { get; set; }
        public string Message { get; set; }
    }
}
