using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch05.Examples
{
    public class MyTasks
    {
        /// <summary>用休眠100毫秒模拟处理过程</summary>
        public void Method1()
        {
            System.Threading.Thread.Sleep(100);
        }

        /// <summary>用休眠100毫秒模拟处理字符串的过程，并返回处理后的结果</summary>
        public string Method1(string s)
        {
            System.Threading.Thread.Sleep(100);
            return s;
        }

        /// <summary>计算1到1000的和</summary>
        public int Method2()
        {
            var range = Enumerable.Range(1, 1000);
            int n = range.Sum();
            return n;
        }

        /// <summary>计算n1除以n2的商和余数</summary>
        public Tuple<int, int> Method3(int n1, int n2)
        {
            var result = Tuple.Create(n1 / n2, n1 % n2);
            System.Threading.Thread.Sleep(100);
            return result;
        }

        /// <summary>用延迟500毫秒模拟异步处理过程</summary>
        public async Task Method1Async()
        {
            await Task.Delay(500);
        }


        /// <summary>用延迟100毫秒模拟异步处理字符串的过程，并返回处理后的结果</summary>
        public async Task<string> Method1Async(string s)
        {
            await Task.Delay(100);
            return s;
        }

        /// <summary>异步计算1到1000的和</summary>
        public async Task<int> Method2Async()
        {
            var range = Enumerable.Range(1, 1000);
            int n = range.Sum();
            await Task.Delay(0);
            return n;
        }

        /// <summary>异步计算n1除以n2的商和余数</summary>
        public async Task<Tuple<int, int>> Method3Async(int n1, int n2)
        {
            var result = Tuple.Create(n1 / n2, n1 % n2);
            await Task.Delay(0);
            return result;
        }

        /// <summary>
        /// 随机产生数组中每个元素的值，并返回其平均值
        /// </summary>
        /// <param name="arrayLength">数组元素个数</param>
        public async Task<double> GetAverageAsync(int arrayLength)
        {
            Random r = new Random();
            int[] nums = new int[arrayLength];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = r.Next();
            }
            await Task.Delay(0);
            return nums.Average();
        }
    }
}
