using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract(Namespace = "WcfServiceExamples")]
    public interface IStudentsService
    {
        //凡是希望客户端能看到的自定义类型，都要在接口中至少出现一次，否则无法拥有操作协定

        [OperationContract]
        Students GetData();

        [OperationContract]
        string Hello(string name);

        [OperationContract]
        void UpdateScore(Student student, int newScore);

        [OperationContract]
        string GetStudentsValue();

    }

}
