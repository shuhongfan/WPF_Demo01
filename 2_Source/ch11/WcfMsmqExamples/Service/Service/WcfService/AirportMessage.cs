using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.WcfService
{
    [DataContract(Namespace = "WcfMsmqExamples")]
    public class AirportMessage
    {
        [DataMember] public string AirportId; //4位的机场编号

        [DataMember] public DateTime ForecastTime;  //预报时间

        [DataMember] public string ShortMessage; //短期预报信息

        public string MessageId //报文编号（机场编号+预报时间） 
        {
            get
            {
                return string.Format("{0} {1:yyMM HHmm}", AirportId, ForecastTime);
            }
        }

        public string OriginalMessage //原始报文
        {
            get
            {
                return string.Format("{0} {1}", MessageId, ShortMessage);
            }
        }

        public string Status { get; internal set; }
    }
}
