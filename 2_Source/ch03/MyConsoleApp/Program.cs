using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var c1 = new MyClass();
            Console.WriteLine(c1.MethodA());
            Console.WriteLine(c1.MethodB());
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
