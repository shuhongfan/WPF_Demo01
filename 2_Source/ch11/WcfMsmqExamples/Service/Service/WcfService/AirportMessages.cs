using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.WcfService
{
    //报文集合（保存接收到的报文），例子只是为了演示，实际应用中应该保存到数据库中
    public class AirportMessages
    {
        static Dictionary<string, AirportMessage> messages = new Dictionary<string, AirportMessage>();

        public static void Add(AirportMessage message)
        {
            if (!messages.ContainsKey(message.MessageId))
            {
                messages.Add(message.MessageId, message);
            }
        }

        public static void DeleteMessage(string id)
        {
            if (messages[id] != null) messages.Remove(id);
        }
    }
}
