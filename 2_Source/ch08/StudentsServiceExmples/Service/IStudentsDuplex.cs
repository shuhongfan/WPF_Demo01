using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract(Namespace="WcfExamples",
        SessionMode=SessionMode.Required,
        CallbackContract = typeof(IStudentsDuplexCallback))]

    //在服务端实现该接口
    public interface IStudentsDuplex
    {
        [OperationContract(IsOneWay = true)]
        void Login(string greeting);
    }

    //在客户端实现该接口
    public interface IStudentsDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string response);
    }

}
