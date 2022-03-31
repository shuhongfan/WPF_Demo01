using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    //例2
    public class AirportServiceVolatile : IAirportServiceVolatile
    {
        public void SubmitInfo(string info)
        {
            MainWindow.AddInfo("收到：{0} ", info);
        }

        public void SubmitAirportMessageVolatile(int index, AirportMessage message)
        {
            //下面的代码应该对来自非事务队列（快速排队队列）的报文进行解析处理
            //......
            MainWindow.AddInfo("收到{0}：{1}", index, message.OriginalMessage);
            //下面的代码应该是入库处理，该例子没有入数据库，仅将其添加到报文集合中
            AirportMessages.Add(message);
            //报文处理完毕
        }

    }
}
