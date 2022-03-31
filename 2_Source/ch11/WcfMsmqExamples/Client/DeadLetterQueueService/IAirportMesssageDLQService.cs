using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DeadLetterQueueService.AirportServiceReference;

namespace DeadLetterQueueService
{
    [ServiceContract(Namespace = "WcfMsmqExamples")]
    public interface IAirportMesssageDLQService
    {
        [OperationContract(IsOneWay = true)]
        void SubmitAirportMessage(AirportMessage message);
    }
}
