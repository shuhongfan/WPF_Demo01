using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Client.AirportServiceDLQReference;

namespace Client.Service
{
    [ServiceBehavior(Namespace = "WcfMsmqExamples",
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single,
        AddressFilterMode = AddressFilterMode.Any)]
    public class DeadLetterService : IAirportServiceDLQ
    {
        AirportServiceDLQClient client;
        public DeadLetterService()
        {
            client = new AirportServiceDLQClient();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitInfo(string info)
        {
            //......处理过程与SubmitAirportMessage的处理过程类似，此处没有实现
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitAirportMessage(AirportMessage message)
        {
            AddInfo("报文 {0} 发送失败", message.AirportId);
            MsmqMessageProperty p = OperationContext.Current.IncomingMessageProperties[MsmqMessageProperty.Name] as MsmqMessageProperty;
            // 如果超时则重发，否则显示出错原因
            if (p.DeliveryFailure == DeliveryFailure.ReachQueueTimeout || p.DeliveryFailure == DeliveryFailure.ReceiveTimeout)
            {
                client.SubmitAirportMessage(message);
                AddInfo("报文 {0} 过期，已尝试重发", message.AirportId);
            }
            else
            {
                AddInfo("消息传递状态: {0}，失败原因: {1}", p.DeliveryStatus, p.DeliveryFailure);
                AddInfo("已取消发送该消息");
            }
        }

        private static void AddInfo(string format, params object[] args)
        {
            Examples.DeadLetterPage.AddInfo(format, args);
        }
    }
}
