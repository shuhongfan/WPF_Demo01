using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    public class Service1 : IService1
    {
        public string SayHello(string name)
        {
            return string.Format("Hello, {0}", name);
        }
        public double Add(double d1, double d2)
        {
            return d1 + d2;
        }
        public double Divide(double d1, double d2)
        {
            return d1 / d2;
        }
        public MyData1 GetMyData1()
        {
            return new MyData1();
        }
    }
}
