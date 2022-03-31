using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MarketServer
{
    public class MarketService : IMarketService
    {
        public int GetNewId()
        {
            int id = 1;
            using (MarketEntities entities = new MarketEntities())
            {
                var q = from t in entities.SalesList
                        select t.SaleId;
                if (q.Count() > 0)
                {
                    id += int.Parse(q.Max().Substring(6));
                }
            }
            return id;
        }

        public void SaveCurrentSale(string saleId, string name, int num, double unitPrice, double price, string operatorName)
        {
            using (MarketEntities entities = new MarketEntities())
            {
                SalesList t = new SalesList();
                t.SaleId = saleId;
                t.name = name;
                t.num = num;
                t.untiPrice = unitPrice;
                t.price = price;
                t.OperatorName = operatorName;
                entities.SalesList.Add(t);
                entities.SaveChanges();
            }
        }

        public double GetCurrentDaySale()
        {
            using (MarketEntities entities = new MarketEntities())
            {
                double result = (from t in entities.SalesList
                                 select t.price).Sum();
                return result;
            }
        }

    }
}
