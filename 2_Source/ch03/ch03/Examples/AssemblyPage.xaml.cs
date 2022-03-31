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

namespace ch03.Examples
{
    /// <summary>
    /// AssemblyPage.xaml 的交互逻辑
    /// </summary>
    public partial class AssemblyPage : Page
    {
        public AssemblyPage()
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();

            System.Reflection.Assembly a1 = System.Reflection.Assembly.Load("MyConsoleApp");
            Type t1 = a1.GetType("MyConsoleApp.MyClass");
            sb.AppendLine("执行MyConsoleApp.exe中MyClass对象的MethodA方法：");
            System.Reflection.MethodInfo myMethodA = t1.GetMethod("MethodA");
            object obj = Activator.CreateInstance(t1);
            sb.AppendLine((string)myMethodA.Invoke(obj, null));

            sb.AppendLine();

            System.Reflection.Assembly a2 = System.Reflection.Assembly.Load("MyClassLibrary");
            Type t2 = a2.GetType("MyClassLibrary.Class1");
            sb.AppendLine("\n执行MyClassLibrary.dll中Class1对象的MyMethod方法：");
            System.Reflection.MethodInfo myMethod = t2.GetMethod("MyMethod");
            object obj1 = Activator.CreateInstance(t2);
            sb.AppendLine("结果为：" + myMethod.Invoke(obj1, new Object[] { 3, 5 }));

            textBlock1.Text = sb.ToString();
        }
    }
}
