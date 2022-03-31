using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    public class AirportServiceDLQ : IAirportServiceDLQ
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitInfo(string info)
        {
            MainWindow.AddInfo("收到：{0} ", info);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitAirportMessage(AirportMessage message)
        {
            MainWindow.AddInfo("收到：{0} ", message.OriginalMessage);
            AirportMessages.Add(message);
        }
    }
}
