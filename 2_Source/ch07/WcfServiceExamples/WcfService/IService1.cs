using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    [ServiceContract(Namespace="WcfServiceExamples")]
    public interface IService1
    {
        [OperationContract]
        string SayHello(string name);
        [OperationContract]
        double Add(double d1, double d2);
        [OperationContract]
        double Divide(double d1, double d2);
    }
}
