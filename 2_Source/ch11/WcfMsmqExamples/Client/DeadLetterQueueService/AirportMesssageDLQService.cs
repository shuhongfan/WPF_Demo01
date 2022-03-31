using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using DeadLetterQueueService.AirportServiceReference;

namespace DeadLetterQueueService
{
    public class AirportMesssageDLQService : IAirportMesssageDLQService
    {
        AirportServiceClient airportService;
        public AirportMesssageDLQService()
        {
            airportService = new AirportServiceClient("NetMsmqBinding_IAirportService");
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitAirportMessage(AirportMessage message)
        {
            AddInfo("报文 {0} 发送失败", message);
            MsmqMessageProperty mqProp = OperationContext.Current.IncomingMessageProperties[MsmqMessageProperty.Name] as MsmqMessageProperty;

            AddInfo("消息传递状态: {0} ", mqProp.DeliveryStatus);
            AddInfo("消息传递失败原因: {0}\n", mqProp.DeliveryFailure);

            // 如果超时，则重发
            if (mqProp.DeliveryFailure == DeliveryFailure.ReachQueueTimeout ||
                mqProp.DeliveryFailure == DeliveryFailure.ReceiveTimeout)
            {
                // 重发
                AddInfo("尝试重发");
                airportService.SubmitAirportMessage(message);
            }
        }

        private static void AddInfo(string format, params object[] args)
        {
            MainWindow.AddInfo(format, args);
        }
    }
}
