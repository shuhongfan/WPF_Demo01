using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    public class StudentsDuplex : Student, IStudentsDuplex
    {
        private IStudentsDuplexCallback callback;
        public StudentsDuplex()
        {
            OperationContext context = OperationContext.Current;
            callback = context.GetCallbackChannel<IStudentsDuplexCallback>();
        }

        public void Login(string str)
        {
            callback.Receive("Hello，" + str);
            System.Threading.Thread.Sleep(1000);
            callback.Receive("你好，我是服务端。");
        }
    }
}
