using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Client.Service
{
    public class AirportMessageStatusService : IAirportMessageStatusService
    {
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void AirportMessageStatus(string id, string status)
        {
            Client.Examples.TwoWayPage.AddInfo("收到--报文id：{0}，状态：{1} ", id, status);
        }
    }
}
