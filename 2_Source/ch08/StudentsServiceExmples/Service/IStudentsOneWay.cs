using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract(Namespace = "WcfServiceExamples")]
    public interface IStudentsOneWay
    {
        [OperationContract(IsOneWay = true)]
        void Clear();
        
        [OperationContract(IsOneWay = true)]
        void Add(Student student);
        
        [OperationContract(IsOneWay = true)]
        void Remove(int studentID);

        [OperationContract]
        string GetStudentsValue();
    }
}
