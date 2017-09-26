using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumosCapataz.Domain;

namespace ConsumosCapataz.DAL
{
    public class RawMaterialsDal : DalBase
    {

        public RawMaterialsDal(ModelEntities context)
            : base(context)
        {
        }

        public RawMaterials Create(EnsolStockMovement rawMaterials, string orderType, string orderNumber)
        {
            if (rawMaterials == null)
            {
                throw new ArgumentNullException("role");
            }

            RawMaterials db = new RawMaterials();
            db.IdCompany = rawMaterials.IdCompany;
            db.ProcessOrder = rawMaterials.ProcessOrder;
            db.Code = rawMaterials.Code;
            db.Description = rawMaterials.Description;
            db.Batch = rawMaterials.Batch;
            db.Quantity = rawMaterials.Quantity;
            db.ECAStorageCode = rawMaterials.ECAStorageCode;
            db.ICAStorageCode = rawMaterials.ICAStorageCode;
            db.Message = rawMaterials.Message;
            db.ECA = rawMaterials.ECA;
            db.OrderType = orderType;
            db.OrderNumber = orderNumber;
            db.Stamp = DateTime.Now;

            //TODO: buscar forma correcta de vincular errores en la capa de datos hacia la capa de distribución.
            if (Exists(db.ProcessOrder.Value))
            {
                return null;
            }

            context.RawMaterials.Add(db);
            context.SaveChanges();

            return db;
        }

        public bool Exists(int processOrder)
        {
            IQueryable<RawMaterials> q = context.RawMaterials.Where(x => x.ProcessOrder == processOrder);
            return q.Any();
        }

        public List<RawMaterials> Get(int processOrder)
        {
            return context.RawMaterials.Where(x => x.ProcessOrder == processOrder).ToList();
        }
    }
}
