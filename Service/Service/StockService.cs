using ConsumosCapataz.DAL;
using ConsumosCapataz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Service
{
    public class StockService
    {
        private OrderDal orderDal;
        private FinishedProductDal finishedProductDal;
        private RawMaterialsDal rawMaterialsDal;

        public StockService(OrderDal ordenDal, FinishedProductDal finishedProductDal, RawMaterialsDal rawMaterialsDal)
        {
            this.orderDal = ordenDal;
            this.finishedProductDal = finishedProductDal;
            this.rawMaterialsDal = rawMaterialsDal;
        }

        public Order ConsumeOrder(int IdCompany, EnsolStockMovement finishedProduct, List<EnsolStockMovement> rawMaterials)
        {
            List<Order> ot = new List<Order>();
            if (finishedProductDal.Exists(finishedProduct.ProcessOrder))
            {
                Order order = new Order();
                FinishedProduct prod = finishedProductDal.Get(finishedProduct.ProcessOrder).First();
                order.FinishedProduct = finishedProduct;
                order.Type = prod.OrderType;
                order.Number = prod.OrderNumber;
                ot.Add(order);
            }
            else
            {
                ot = orderDal.SPECreaOT((CompanyEnum)IdCompany, finishedProduct.Code, finishedProduct.Quantity, finishedProduct.ICAStorageCode, "usuario", -1, "terminal");
                ot.First().FinishedProduct = orderDal.SPECreaICA((CompanyEnum)IdCompany, ot.First().Type, ot.First().Number, finishedProduct.Batch, finishedProduct.ProcessOrder, finishedProduct.Code, finishedProduct.Description, finishedProduct.Quantity, finishedProduct.ICAStorageCode, "usuario", -1, "terminal").First();

                finishedProductDal.Create(finishedProduct, ot.First().FinishedProduct.ECA, ot.First().Type, ot.First().Number);
            }

            ot.First().RawMaterials = orderDal.SPECreaECA((CompanyEnum)IdCompany, ot.First().Type, ot.First().Number, rawMaterials, "usuario", -1, "terminal");
            foreach (EnsolStockMovement item in ot.First().RawMaterials)
            {
                if (!string.IsNullOrEmpty(item.ECA))
                {
                    rawMaterialsDal.Create(item, ot.First().Type, ot.First().Number);
                }
            }

            if (!ot.First().RawMaterials.Where(w => string.IsNullOrEmpty(w.ECA)).Any())
                orderDal.SPECierreOT((CompanyEnum)IdCompany, ot.First().Type, ot.First().Number, "usuario", -1, "terminal");

            return ot.First();
        }
    }
}
