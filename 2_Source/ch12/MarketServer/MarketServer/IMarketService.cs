using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MarketServer
{
    [ServiceContract(Namespace = "MarketExample")]
    public interface IMarketService
    {
        //----结算员（Account）-------------
        [OperationContract]
        int GetNewId();

        [OperationContract]
        void SaveCurrentSale(string saleId, string name, int num, double unitPrice, double price, string operatorName);

        [OperationContract]
        double GetCurrentDaySale();

        //----管理员-------------
        //......

        //----销售人员-------------
        //......

        //----顾客（Customer）-------
        //.......
    }
}
