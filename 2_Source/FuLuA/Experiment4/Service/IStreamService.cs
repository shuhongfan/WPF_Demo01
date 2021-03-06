using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Namespace="StreamExperiment")]
    public interface IStreamService
    {
        [OperationContract]
        List<string> GetFilesInfo();

        [OperationContract]
        Stream DownloadStream(string fileName);
    }
}
