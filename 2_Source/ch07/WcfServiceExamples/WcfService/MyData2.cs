using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace WcfService
{
    [MessageContract]
    public class MyData2
    {
        [MessageHeader]
        public Header header { get; set; }
        [MessageBodyMember]
        public Body body { get; set; }
    }

    public class Header
    {
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public Header()
        {
            Description = "消息头";
            TransactionDate = DateTime.Now;
        }
    }

    public class Body
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public Body()
        {
            Name = "张三";
            Age = 20;
            Telephone = "1234567";
        }
    }
}