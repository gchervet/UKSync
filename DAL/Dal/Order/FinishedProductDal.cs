using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumosCapataz.Domain;

namespace ConsumosCapataz.DAL
{
    public class FinishedProductDal : DalBase
    {

        public FinishedProductDal(ModelEntities context)
            : base(context)
        {
        }

        public FinishedProduct Create(EnsolStockMovement finishedProduct, string ica, string orderType, string orderNumber)
        {
            if (finishedProduct == null)
            {
                throw new ArgumentNullException("role");
            }

            FinishedProduct db = new FinishedProduct();
            db.IdCompany = finishedProduct.IdCompany;
            db.ProcessOrder = finishedProduct.ProcessOrder;
            db.Code = finishedProduct.Code;
            db.Description = finishedProduct.Description;
            db.Batch = finishedProduct.Batch;
            db.Quantity = finishedProduct.Quantity;
            db.ECAStorageCode = finishedProduct.ECAStorageCode;
            db.ICAStorageCode = finishedProduct.ICAStorageCode;
            db.Message = finishedProduct.Message;
            db.ICA = ica;
            db.OrderType = orderType;
            db.OrderNumber = orderNumber;
            db.Stamp = DateTime.Now;

            //TODO: buscar forma correcta de vincular errores en la capa de datos hacia la capa de distribución.
            if (Exists(db.ProcessOrder.Value))
            {
                return null;
            }

            context.FinishedProduct.Add(db);
            context.SaveChanges();

            return db;
        }

        public bool Exists(int processOrder)
        {
            IQueryable<FinishedProduct> q = context.FinishedProduct.Where(x => x.ProcessOrder == processOrder);
            return q.Any();
        }

        public List<FinishedProduct> Get(int processOrder)
        {
            return context.FinishedProduct.Where(x => x.ProcessOrder == processOrder).ToList();
        }
    }
}
