using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.WcfService
{
    //例2：可变排队通信
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportServiceVolatile
    {
        [OperationContract(IsOneWay = true)]
        void SubmitInfo(string info);

        [OperationContract(IsOneWay = true)]
        void SubmitAirportMessageVolatile(int index, AirportMessage message);
    }
}
