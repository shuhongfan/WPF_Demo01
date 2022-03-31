using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;
using Service.AirportMessageStatusServiceReference;
namespace Service.WcfService
{
    //例1
    public class AirportService : IAirportService
    {
        //TransactionScopeRequired为true表示客户端必须在事务范围内调用此方法
        //TransactionAutoComplete为true表示该方法完成后，自动完成该事务
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitInfo(string info)
        {
            MainWindow.AddInfo("收到：{0} ", info);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitAirportMessage(AirportMessage message)
        {
            //下面的代码应该对来自事务队列（可靠排队队列）的报文message进行解析处理
            //该例子没有演示报文解析过程，仅将原始报文显示出来
            //......
            MainWindow.AddInfo("收到：{0} ", message.OriginalMessage);
            //下面的代码应该是用事务进行入库处理，该例子仅将其添加到无事务功能的报文集合中
            AirportMessages.Add(message);
            //报文处理完毕
        }
    }
}
