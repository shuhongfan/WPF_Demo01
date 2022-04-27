using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sy3._2
{
    

    internal class MyClass
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

}
