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
    //例3
    public class AirportServiceTwoWay : IAirportServiceTwoWay
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitInfo(string info)
        {
            MainWindow.AddInfo("收到：{0} ", info);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitAirportMessageTwoWay(AirportMessage message, string reportStatusTo)
        {
            MainWindow.AddInfo("收到：{0} ", message.OriginalMessage);
            MainWindow.AddInfo("消息来自：{0}", reportStatusTo);
            message.Status = "正在处理";
            //下面的代码应该对报文进行解析处理，此处省略了解析过程
            //......
            ReportStatus(message, reportStatusTo);
            //下面的代码省略了入库处理过程，仅将其添加到报文集合中
            //......
            AirportMessages.Add(message);
            message.Status = "已处理";
            ReportStatus(message, reportStatusTo);
        }

        private static void ReportStatus(AirportMessage message, string reportStatusTo)
        {
            MainWindow.AddInfo("向该客户端回送处理状态：{0}",message.Status);
            //注意：回调的客户端地址是对方通过reportStatusTo传递过来的地址
            AirportMessageStatusServiceClient client = new AirportMessageStatusServiceClient(
                "NetMsmqBinding_IAirportMessageStatusService", new EndpointAddress(reportStatusTo));
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                //向该客户端发送状态回调信息
                client.AirportMessageStatus(message.MessageId, message.Status);
                scope.Complete();
            }
            client.Close();
        }
    }
}
