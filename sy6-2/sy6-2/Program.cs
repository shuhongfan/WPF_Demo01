using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sy6_2
{
    internal class Program
    {
        private List<int> data = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
        private ParallelOptions options = new ParallelOptions();

        static void Main(string[] args)
        {
            Program p = new Program();
            // p.options.MaxDegreeOfParallelism = Environment.ProcessorCount;
            p.options.MaxDegreeOfParallelism = 7;
            Console.WriteLine(Environment.ProcessorCount);

            p.Demo01();
            p.Demo02();
            p.Demo03();
            p.Demo04();
        }

        public bool ShowProgressExecution = false;

        public void Demo01()
        {
            DateTime dt1 = DateTime.Now;
            for (int i = 0; i < data.Count; i++)
            {
                Thread.Sleep(100);
                if (ShowProgressExecution)
                {
                    Console.WriteLine(data[i]);
                }
            }
            DateTime dt2 = DateTime.Now;
            Console.WriteLine("普通for循环运行时长，{0}毫秒", (dt2 - dt1).TotalMilliseconds);
        }

        public void Demo02()
        {
            DateTime dt1 = DateTime.Now;
            foreach (var i in data)
            {
                Thread.Sleep(100);
                if (ShowProgressExecution)
                {
                    Console.WriteLine(i);
                }
            }

            DateTime dt2 = DateTime.Now;
            Console.WriteLine("普通foreach循环运行时长，{0}毫秒", (dt2 - dt1).TotalMilliseconds);
        }

        public void Demo03()
        {
            DateTime dt1 = DateTime.Now;

            Parallel.For(0, data.Count, i =>
            {
                Thread.Sleep(100);
                if (ShowProgressExecution)
                {
                    Console.WriteLine(i);
                }
            });

            DateTime dt2 = DateTime.Now;
            Console.WriteLine("并行运算for循环运行时长，{0}毫秒", (dt2 - dt1).TotalMilliseconds);
        }
        
        public void Demo04()
        {
            DateTime dt1 = DateTime.Now;

            Parallel.ForEach(data, i =>
            {
                Thread.Sleep(100);
                if (ShowProgressExecution)
                {
                    Console.WriteLine(i);
                }
            });

            DateTime dt2 = DateTime.Now;
            Console.WriteLine("并行运算foreach循环运行时长，{0}毫秒", (dt2 - dt1).TotalMilliseconds);
        }
    }
}
