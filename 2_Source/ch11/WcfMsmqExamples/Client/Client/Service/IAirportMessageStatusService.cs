using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Client.Service
{
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportMessageStatusService
    {
        [OperationContract(IsOneWay = true)]
        void AirportMessageStatus(string id, string status);
    }
}
