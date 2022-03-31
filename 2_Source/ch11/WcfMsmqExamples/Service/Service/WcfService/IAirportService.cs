using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    //例1：事务队列（可靠排队）通信
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportService
    {
        [OperationContract(IsOneWay = true)]
        void SubmitInfo(string info);

        [OperationContract(IsOneWay = true)]
        void SubmitAirportMessage(AirportMessage message);
    }
}
