using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportServiceDLQ
    {
        [OperationContract(IsOneWay = true)]
        void SubmitInfo(string info);

        [OperationContract(IsOneWay = true)]
        void SubmitAirportMessage(AirportMessage message);
    }
}
