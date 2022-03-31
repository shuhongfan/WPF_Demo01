using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfNetMeeting
{
    //会议室提供的多播服务协定
    //注意：多播不支持“请求/应答”模式，所有操作都必须用单向模式
    [ServiceContract(Namespace = "NetMeetingExample")]
    public interface IMeetingService
    {
        [OperationContract(IsOneWay = true)]
        void EnterRoom(string userName);

        [OperationContract(IsOneWay = true)]
        void Say(string userName, string message);

        [OperationContract(IsOneWay = true)]
        void ExitRoom(string userName);
    }
}
