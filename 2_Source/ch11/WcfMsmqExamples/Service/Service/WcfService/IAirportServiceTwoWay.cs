using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    //例3：双向通信
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportServiceTwoWay
    {
        [OperationContract(IsOneWay = true)]
        void SubmitInfo(string info);

        [OperationContract(IsOneWay = true)]
        void SubmitAirportMessageTwoWay(AirportMessage message, string reportStatusTo);
    }
}
