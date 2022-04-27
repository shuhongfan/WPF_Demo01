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

namespace sy3._4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StringBuilder sb = new StringBuilder();
            System.Reflection.Assembly a1 = System.Reflection.Assembly.Load("sy3.3");
            Type t1 = a1.GetType("sy3._3.MyClass");
            sb.AppendLine("执行MyConsoleApp.exe中MyClass对象的MethodA方法：");
            System.Reflection.MethodInfo myMethodA = t1.GetMethod("MethodA");
            object obj = Activator.CreateInstance(t1);
            sb.AppendLine((string)myMethodA.Invoke(obj, null));

            sb.AppendLine();
            textBlock1.Text = sb.ToString();

        }
    }
}
